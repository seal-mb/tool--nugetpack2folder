using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace NugetPack2Folder
{
    public partial class MainFrm
    {

        private void SetupDialogData(string fiName)
        {

            _projectFile = XDocument.Load( fiName, LoadOptions.None );
            _theProjectFile = new FileInfo( fiName );
            Properties.Settings.Default.LastLocation = _theProjectFile.DirectoryName;
            Properties.Settings.Default.Save();

            var res = _projectFile.Descendants( Helper.GetXName( PTags.Reference ) )
                                                        .Where( s => s.Elements( Helper.GetXName( PTags.HintPath ) )
                                                         .Any( t => (t.Value?.ToLower().Contains( Helper.SearchNugetPack )).GetValueOrDefault( false ) ) );

            toolStripStatusLabelFound.Text = $"Found {res.Count()} items!";

            _lstRef.Clear();
            _lstRef.AddRange( res );

            listViewReferenz.Items.Clear();
            detailsOfReferenz.Visible = false;
            var probing = textBoxProbing.Text.Split( new char[] { ';', ',' } );
            var lstProjectFiles = new List<PrjContentObject>();

            var existingContent = _projectFile.Descendants( Helper.GetXName( PTags.Content ) ).Where( s => s.Element( Helper.GetXName( PTags.Link ) )?.Value != null );

            foreach (var item in _lstRef)
            {
                var hintPath = item.Element( Helper.GetXName( PTags.HintPath ) ).Value;
                if (String.IsNullOrWhiteSpace( hintPath ))
                    continue;
                var probingValue = probing.FirstOrDefault() ?? Properties.Resources.DefaultSubdirName;
                var existOld = existingContent.FirstOrDefault( s => String.Equals( s.Attribute( Helper.Attr_Include ).Value, hintPath, StringComparison.InvariantCultureIgnoreCase ) );

                if (null != existOld)
                {
                    probingValue = Path.GetDirectoryName( existOld.Element( Helper.GetXName( PTags.Link ) ).Value );
                }

                var tagItem = new PrjContentObject( item, _theProjectFile.DirectoryName, probingValue ) { OldElement = existOld };

                if (null != existOld)
                {
                    var cpOption = existOld.Element( Helper.GetXName( PTags.CopyToOutputDirectory ) )?.Value;

                    if (cpOption != null)
                    {
                        CopyOption opt;
                        if (Enum.TryParse<CopyOption>( cpOption, out opt ))
                        {
                            tagItem.CpOption = opt;
                        }
                    }
                }

                if (!lstProjectFiles.Any( s => String.Equals( s.ReferenzPath, tagItem.ReferenzPath, StringComparison.InvariantCultureIgnoreCase ) ))
                {
                    var cplocal = item.Element( Helper.GetXName( PTags.Private ) )?.Value ?? "True";

                    var modulename = Path.GetFileName( hintPath );
                    var lstItemVal = new ListViewItem( new String[] { modulename, cplocal } );

                    lstItemVal.Checked = Boolean.Parse( cplocal );

                    var lstitem = listViewReferenz.Items.Add( lstItemVal );

                    lstitem.Tag = tagItem;
                    lstitem.ToolTipText = $"Hintpath is {tagItem.ReferenzPath}";
                    lstProjectFiles.Add( tagItem );
                }

            }

            listViewReferenz.View = View.Details;
            this.toolStripMenuItemSave.Enabled = listViewReferenz.Items.Count > 0;
        }

        private void SaveData2PrjFile()
        {
            if (null != _theProjectFile)
            {
                var linkObj = listViewReferenz.Items.Cast<ListViewItem>().Select( s => s.Tag as PrjContentObject ).Where( s => s.AddToOutput );

                if (!linkObj.Any())
                    return;

                Helper.BackUpFile( _theProjectFile );

                var hashProbing = new HashSet<String>( StringComparer.InvariantCultureIgnoreCase );
                XElement xGrp = _projectFile.Element( Helper.GetXName( PTags.Project ) )?.Elements().Cast<XElement>().FirstOrDefault( s => s.Name == Helper.GetXName( PTags.ItemGroup ) && s.Attribute( Helper.Attr_Label )?.Value == Helper.Attr_Label_Value );

                if (null == xGrp)
                {
                    xGrp = new XElement( Helper.GetXName( PTags.ItemGroup ), new XAttribute( Helper.Attr_Label, Helper.Attr_Label_Value ) );
                    var itemGrp = _projectFile.Descendants( Helper.GetXName( PTags.ItemGroup ) ).FirstOrDefault();
                    if (null == itemGrp)
                    {
                        _projectFile.Element( Helper.GetXName( PTags.Project ) ).Add( xGrp );
                    }
                    else
                    {
                        itemGrp.AddAfterSelf( xGrp );
                    }
                }

                var referenz = _projectFile.Descendants( Helper.GetXName( PTags.Reference ) ).
                                     Where( s => s.Element( Helper.GetXName( PTags.HintPath ) ) != null ).
                                     Select( s => new { element = s, hintPath = s.Element( Helper.GetXName( PTags.HintPath ) ).Value } );

                foreach (var item in linkObj)
                {
                    if (item.AddToOutput == false)
                        continue;

                    var content = item.Containers;
                    hashProbing.Add( item.ProbingPath );

                    var ref2ContentElements = from refs in referenz
                                              join con in content on refs.hintPath equals con.Attribute( Helper.Attr_Include )?.Value
                                              select new { referenzElement = refs.element, contentElement = con };

                    // Set copy local to false
                    foreach (var refItem in ref2ContentElements)
                    {
                        var ePrivate = refItem.referenzElement.Element( Helper.GetXName( PTags.Private ) );

                        if (null == ePrivate)
                        {
                            refItem.referenzElement.Add( new XElement( Helper.GetXName( PTags.Private ) ) { Value = "False" } );
                        }
                        else
                        {
                            ePrivate.Value = "False";
                        }
                    }

                    var resExistingContent = (from existCont in _projectFile.Descendants( Helper.GetXName( PTags.Content ) )
                                              join newCont in content on existCont.Attribute( Helper.Attr_Include )?.Value.ToLower() equals newCont.Attribute( Helper.Attr_Include ).Value.ToLower()
                                              select new { existingElement = existCont, newElement = newCont }).ToArray();

                    var resNewContent = content.Where( s => !resExistingContent.Any( t => t.newElement.Attribute( Helper.Attr_Include ).Value == s.Attribute( Helper.Attr_Include ).Value ) );

                    foreach (var item2 in resExistingContent)
                    {
                        item2.existingElement.AddAfterSelf( item2.newElement );
                        item2.existingElement.Remove();
                    }

                    foreach (var item2 in resNewContent)
                    {
                        xGrp.Add( item2 );
                    }

                }

                _projectFile.Save( _theProjectFile.FullName );

                var cfgFile = new FileInfo( Path.Combine( _theProjectFile.DirectoryName, "app.config" ) );

                if (cfgFile.Exists)
                {
                    Helper.BackUpFile( cfgFile );
                    XDocument xdoc = XDocument.Load( cfgFile.FullName );
                    Helper.AddProbing( xdoc, hashProbing.ToArray() );
                    xdoc.Save( cfgFile.FullName );
                }

                SetupDialogData( _theProjectFile.FullName );

            }
        }

        private void SetComboBoxText()
        {
            comboBoxProbingVal.Items.Clear();
            comboBoxProbingVal.Items.AddRange( Helper.SplitProbing( textBoxProbing.Text ) );
            comboBoxProbingVal.SelectedIndex = 0;

            Properties.Settings.Default.DefaultProbing = String.Join( ";", Helper.SplitProbing( textBoxProbing.Text ) );
            Properties.Settings.Default.Save();
        }

    }
}
