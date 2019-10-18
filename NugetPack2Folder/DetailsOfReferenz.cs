using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace NugetPack2Folder
{
    public partial class DetailsOfReferenz : UserControl
    {
        PrjContentObject _itemData = null;

        public DetailsOfReferenz()
        {
            InitializeComponent();

        }

        private void BuildTreeView()
        {
            treeViewElements.Nodes.Clear();

            if (null == _itemData)
                return;

            var elements = _itemData.Containers.Select(s => s.Element(MainFrm._xmlns + "Link").Value).Select(s => new { full = s, split = s.Split('\\') });

            foreach (var element in elements)
            {
                TreeNode prevNode = null;
                foreach (var e in element.split)
                {
                    var node = null == prevNode ? treeViewElements.Nodes.Cast<TreeNode>().FirstOrDefault(s => s.Text == e) : prevNode.Nodes.Cast<TreeNode>().FirstOrDefault(s => s.Text == e);

                    if (null == node)
                    {
                        if (null == prevNode)
                        {
                            prevNode = treeViewElements.Nodes.Add(e);
                        }
                        else
                        {
                            prevNode = prevNode.Nodes.Add(e);
                        }

                        prevNode.Collapse(true);
                    }
                    else
                    {
                        prevNode = node;
                    }

                }
            }

        }

        public void SetItemData(PrjContentObject itemData, String[] probingData)
        {
            if(null != _itemData)
            {
                _itemData.PropertyChanged -= ItemData_PropertyChanged;
            }

            _itemData = itemData;

            checkBoxAddToOutput.DataBindings.Clear();
            comboBoxProbingPath.DataBindings.Clear();
            treeViewElements.Nodes.Clear();
            linkLabelPathRef.Text = "";

            comboBoxProbingPath.Items.Clear();
            if (null != probingData)
            {
                comboBoxProbingPath.Items.AddRange(probingData);
            }

            if (null != _itemData)
            {
                checkBoxAddToOutput.DataBindings.Add("Checked", itemData, "AddToOutput");
                comboBoxProbingPath.DataBindings.Add("Text", itemData, "ProbingPath");
                linkLabelPathRef.Text = itemData.NugetPathRef;
                _itemData.PropertyChanged += ItemData_PropertyChanged;
                BuildTreeView();

            }
        }

        private void ItemData_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == "ProbingPath")
            {
                BuildTreeView();
            }
        }

        public void TextBoxProbing_TextChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {

                TextBox txt = sender as TextBox;
                if(null != txt)
                {
                    comboBoxProbingPath.Items.Clear();
                    var items = (txt.Text.Split(new char[] { ',', ';' }).Select(s => s.Trim())).ToArray();
                    comboBoxProbingPath.Items.AddRange(items);
                }
            }

        }

        private void ComboBoxProbingPath_Validating(object sender, CancelEventArgs e)
        {
            ComboBox box = sender as  ComboBox;

            if(  null != box )
            {
                if (String.IsNullOrWhiteSpace(box.Text))
                {
                    MessageBox.Show("Box darf nicht leer sein", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Cancel = true;

                }
            }
        }

        private void ComboBoxProbingPath_Validated(object sender, EventArgs e)
        {
            BuildTreeView();
        }

        private void DetailsOfReferenz_Load(object sender, EventArgs e)
        {

        }

        private void LinkLabelPathRef_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if(!String.IsNullOrWhiteSpace(linkLabelPathRef.Text))
            {
                ProcessStartInfo start = new ProcessStartInfo() { FileName = "explorer.exe", Arguments = linkLabelPathRef.Text };

                Process.Start(start);
            }
        }
    }
}
