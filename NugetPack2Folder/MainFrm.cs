using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace NugetPack2Folder
{
    public partial class MainFrm : Form
    {
        private XDocument _projectFile = null;
        private List<XElement> _lstRef = new List<XElement>();
        private FileInfo _theProjectFile = null;

        public MainFrm()
        {
            InitializeComponent();

            quitMenuItem.Click += delegate (object sender, EventArgs e) { this.Close(); };
            textBoxProbing.Text = Properties.Settings.Default.DefaultProbing;

            textBoxProbing.TextChanged += detailsOfReferenz.TextBoxProbing_TextChanged;

            SetComboBoxText();
        }

        private void SetComboBoxText()
        {
            comboBoxProbingVal.Items.Clear();
            comboBoxProbingVal.Items.AddRange( Helper.SplitProbing( textBoxProbing.Text));
            comboBoxProbingVal.SelectedIndex = 0;

            Properties.Settings.Default.DefaultProbing = String.Join(";", Helper.SplitProbing(textBoxProbing.Text));
            Properties.Settings.Default.Save();
        }

        private void OpenProjectFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var fileDialog = new OpenFileDialog() { Title = "Select project File" };

            fileDialog.Filter = "C# Project (*.csproj)|*.csproj|Target Files (*.targets)|*.targets| All Files (*.*)|*.*";
            fileDialog.DefaultExt = "csproj";
            fileDialog.Multiselect = false;
            var lastLocation = Properties.Settings.Default.LastLocation;
            if (String.IsNullOrEmpty(lastLocation) == false && Directory.Exists(lastLocation))
                fileDialog.InitialDirectory = Properties.Settings.Default.LastLocation;

            if (fileDialog.ShowDialog(this) == DialogResult.OK)
            {
                var fiName = fileDialog.FileName;

                SetupDialogData(fiName);
            }
        }

        private void SetupDialogData(string fiName)
        {

            _projectFile = XDocument.Load(fiName, LoadOptions.None);
            _theProjectFile = new FileInfo(fiName);
            Properties.Settings.Default.LastLocation = _theProjectFile.DirectoryName;
            Properties.Settings.Default.Save();

            var res = _projectFile.Descendants(Helper.GetXName(PTags.Reference))
                                                        .Where(s => s.Elements(Helper.GetXName(PTags.HintPath))
                                                        .Any(t => (t.Value?.ToLower().Contains(Helper.SearchNugetPack)).GetValueOrDefault(false)));

            toolStripStatusLabelFound.Text = $"Found {res.Count()} items!";

            _lstRef.Clear();
            _lstRef.AddRange(res);

            listViewReferenz.Items.Clear();
            detailsOfReferenz.Visible = false;
            var probing = textBoxProbing.Text.Split(new char[] { ';', ',' });
            var lstProjectFiles = new List<PrjContentObject>();

            var existingContent = _projectFile.Descendants(Helper.GetXName(PTags.Content)).Where(s => s.Element(Helper.GetXName(PTags.Link))?.Value != null);

            foreach (var item in _lstRef)
            {
                var hintPath = item.Element(Helper.GetXName(PTags.HintPath)).Value;
                if (String.IsNullOrWhiteSpace(hintPath))
                    continue;
                var probingValue = probing.FirstOrDefault() ?? Properties.Resources.DefaultSubdirName;
                var existOld = existingContent.FirstOrDefault(s => String.Equals(s.Attribute(Helper.Attr_Include).Value, hintPath, StringComparison.InvariantCultureIgnoreCase));

                if (null != existOld)
                {
                    probingValue = Path.GetDirectoryName(existOld.Element(Helper.GetXName(PTags.Link)).Value);
                }

                var tagItem = new PrjContentObject(item, _theProjectFile.DirectoryName, probingValue) { OldElement = existOld };

                if(null != existOld)
                {
                    var cpOption = existOld.Element(Helper.GetXName(PTags.CopyToOutputDirectory))?.Value;

                    if(cpOption!= null)
                    {
                        CopyOption opt;
                        if(Enum.TryParse<CopyOption>(cpOption,out opt))
                        {
                            tagItem.CpOption = opt;
                        }
                    }
                }

                if (!lstProjectFiles.Any(s => String.Equals(s.ReferenzPath, tagItem.ReferenzPath, StringComparison.InvariantCultureIgnoreCase)))
                {
                    var cplocal = item.Element(Helper.GetXName(PTags.Private))?.Value ?? "True";

                    var modulename = Path.GetFileName(hintPath);
                    var lstItemVal = new ListViewItem(new String[] { modulename, cplocal });

                    lstItemVal.Checked = Boolean.Parse(cplocal);

                    var lstitem = listViewReferenz.Items.Add(lstItemVal);

                    lstitem.Tag = tagItem;
                    lstitem.ToolTipText = $"Hintpath is {tagItem.ReferenzPath}";
                    lstProjectFiles.Add(tagItem);
                }

            }

            listViewReferenz.View = View.Details;
            this.toolStripMenuItemSave.Enabled = listViewReferenz.Items.Count > 0;
        }

        private void ListViewReferenz_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            var item = e.Item;

            if (null != item)
            {
                item.SubItems[1].Text = item.Checked.ToString();
            }
        }

        private void ListViewReferenz_SelectedIndexChanged(object sender, EventArgs e)
        {
            var item = listViewReferenz.SelectedItems.Cast<ListViewItem>().FirstOrDefault();

            if (null != item)
            {
                detailsOfReferenz.SetItemData(item.Tag as PrjContentObject, textBoxProbing.Text.Split(new char[] { ';', ',' }));
                detailsOfReferenz.Visible = true;

            }
            else
            {
                detailsOfReferenz.Visible = false;
                detailsOfReferenz.SetItemData(null, null);
            }
        }

        private void TextBoxProbing_Validating(object sender, CancelEventArgs e)
        {
            var txt = sender as TextBox;

            if (String.IsNullOrWhiteSpace(txt.Text))
            {
                MessageBox.Show("Textbox darf nicht leer sein", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;

            }
            else
            {
                var items = txt.Text.Split(new char[] { ',', ';' });

                if (items.Any(s => String.IsNullOrWhiteSpace(s)))
                {
                    MessageBox.Show("Textbox enthält leere elemente", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Cancel = true;
                }
            }

        }

        private void TextBoxProbing_Validated(object sender, EventArgs e)
        {
            SetComboBoxText();
        }

        private void ButtonSetProbing_Click(object sender, EventArgs e)
        {
            foreach( var item in listViewReferenz.Items.Cast<ListViewItem>() )
            {
                var content = item.Tag as PrjContentObject;

                if(null != content)
                {
                    content.ProbingPath = comboBoxProbingVal.Text;
                }
            }
        }

        private void SaveData2PrjFile()
        {
            if (null != _theProjectFile)
            {
                var linkObj = listViewReferenz.Items.Cast<ListViewItem>().Select(s => s.Tag as PrjContentObject).Where(s => s.AddToOutput);

                if (!linkObj.Any())
                    return;

                Helper.BackUpFile(_theProjectFile);

                var hashProbing = new HashSet<String>(StringComparer.InvariantCultureIgnoreCase);
                XElement xGrp = _projectFile.Element(Helper.GetXName(PTags.Project))?.Elements().Cast<XElement>().FirstOrDefault(s => s.Name == Helper.GetXName(PTags.ItemGroup) && s.Attribute(Helper.Attr_Label)?.Value == Helper.Attr_Label_Value);

                if (null == xGrp)
                {
                    xGrp = new XElement(Helper.GetXName(PTags.ItemGroup), new XAttribute(Helper.Attr_Label, Helper.Attr_Label_Value));
                    var itemGrp = _projectFile.Descendants(Helper.GetXName(PTags.ItemGroup)).FirstOrDefault();
                    if(null == itemGrp)
                    {
                        _projectFile.Element(Helper.GetXName(PTags.Project)).Add(xGrp);
                    }
                    else 
                    {
                        itemGrp.AddAfterSelf(xGrp);
                    }
                }

                var referenz = _projectFile.Descendants(Helper.GetXName(PTags.Reference)).
                                     Where(s => s.Element(Helper.GetXName(PTags.HintPath)) != null).
                                     Select(s => new { element = s, hintPath = s.Element(Helper.GetXName(PTags.HintPath)).Value });

                foreach (var item in linkObj)
                {
                    if (item.AddToOutput == false)
                        continue;

                    var content = item.Containers;
                    hashProbing.Add(item.ProbingPath);

                    var ref2ContentElements = from refs in referenz
                                              join con in content on refs.hintPath equals con.Attribute(Helper.Attr_Include)?.Value
                                              select new { referenzElement = refs.element, contentElement = con };

                    // Set copy local to false
                    foreach (var refItem in ref2ContentElements)
                    {
                        var ePrivate = refItem.referenzElement.Element(Helper.GetXName(PTags.Private));

                        if (null == ePrivate)
                        {
                            refItem.referenzElement.Add(new XElement(Helper.GetXName(PTags.Private)) { Value = "False" });
                        }
                        else
                        {
                            ePrivate.Value = "False";
                        }
                    }

                    var resExistingContent = (from existCont in _projectFile.Descendants(Helper.GetXName(PTags.Content))
                                             join newCont in content on existCont.Attribute(Helper.Attr_Include)?.Value.ToLower() equals newCont.Attribute(Helper.Attr_Include).Value.ToLower()
                                             select new { existingElement = existCont, newElement = newCont }).ToArray();

                    var resNewContent = content.Where(s => !resExistingContent.Any(t => t.newElement.Attribute(Helper.Attr_Include).Value == s.Attribute(Helper.Attr_Include).Value));

                    foreach (var item2 in resExistingContent)
                    {
                        item2.existingElement.AddAfterSelf(item2.newElement);
                        item2.existingElement.Remove();
                    }

                    foreach(var item2 in resNewContent)
                    {
                        xGrp.Add(item2);
                    }

                }

                _projectFile.Save(_theProjectFile.FullName);

                var cfgFile = new FileInfo(Path.Combine(_theProjectFile.DirectoryName, "app.config"));

                if (cfgFile.Exists)
                {
                    Helper.BackUpFile(cfgFile);
                    XDocument xdoc = XDocument.Load(cfgFile.FullName);
                    Helper.AddProbing(xdoc, hashProbing.ToArray());
                    xdoc.Save(cfgFile.FullName);
                }

                SetupDialogData(_theProjectFile.FullName);

            }
        }

        private void ToolStripMenuItemSave_Click(object sender, EventArgs e)
        {
            SaveData2PrjFile();
        }
    }
}
