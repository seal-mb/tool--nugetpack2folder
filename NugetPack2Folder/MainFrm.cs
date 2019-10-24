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

        public MainFrm ()
        {
            InitializeComponent();

            quitMenuItem.Click += delegate ( object sender, EventArgs e )
            { this.Close(); };
            textBoxProbing.Text = Properties.Settings.Default.DefaultProbing;

            textBoxProbing.TextChanged += detailsOfReferenz.TextBoxProbing_TextChanged;

            SetComboBoxText();
        }

        private void OpenProjectFileToolStripMenuItem_Click ( object sender, EventArgs e )
        {
            var fileDialog = new OpenFileDialog() { Title = "Select project File" };

            fileDialog.Filter = "C# Project (*.csproj)|*.csproj|Target Files (*.targets)|*.targets| All Files (*.*)|*.*";
            fileDialog.DefaultExt = "csproj";
            fileDialog.Multiselect = false;
            var lastLocation = Properties.Settings.Default.LastLocation;
            if ( String.IsNullOrEmpty(lastLocation) == false && Directory.Exists(lastLocation) )
                fileDialog.InitialDirectory = Properties.Settings.Default.LastLocation;

            if ( fileDialog.ShowDialog(this) == DialogResult.OK )
            {
                var fiName = fileDialog.FileName;

                SetupDialogData(fiName);
            }
        }

        private void ListViewReferenz_ItemChecked ( object sender, ItemCheckedEventArgs e )
        {
            var item = e.Item;

            if ( null != item )
            {
                item.SubItems[1].Text = item.Checked.ToString();
            }
        }

        private void ListViewReferenz_SelectedIndexChanged ( object sender, EventArgs e )
        {
            var item = listViewReferenz.SelectedItems.Cast<ListViewItem>().FirstOrDefault();

            if ( null != item )
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

        private void TextBoxProbing_Validating ( object sender, CancelEventArgs e )
        {
            var txt = sender as TextBox;

            if ( String.IsNullOrWhiteSpace(txt.Text) )
            {
                MessageBox.Show("Textbox darf nicht leer sein", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;

            }
            else
            {
                var items = txt.Text.Split(new char[] { ',', ';' });

                if ( items.Any(s => String.IsNullOrWhiteSpace(s)) )
                {
                    MessageBox.Show("Textbox enthält leere elemente", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Cancel = true;
                }
            }

        }

        private void TextBoxProbing_Validated ( object sender, EventArgs e )
        {
            SetComboBoxText();
        }

        private void ButtonSetProbing_Click ( object sender, EventArgs e )
        {
            foreach ( var item in listViewReferenz.Items.Cast<ListViewItem>() )
            {
                var content = item.Tag as PrjContentObject;

                if ( null != content )
                {
                    content.ProbingPath = comboBoxProbingVal.Text;
                }
            }
        }

        private void ToolStripMenuItemSave_Click ( object sender, EventArgs e )
        {
            SaveData2PrjFile();
        }
    }
}
