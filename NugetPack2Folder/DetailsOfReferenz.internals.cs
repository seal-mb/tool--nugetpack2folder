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
    public partial class DetailsOfReferenz
    {

        private void BuildTreeView ()
        {
            treeViewElements.Nodes.Clear();

            if ( null == _itemData )
                return;

            var elements = _itemData.Containers.Select( s => s.Element( Helper.GetXName( PTags.Link) ).Value ).Select( s => new { full = s, split = s.Split( '\\' ) } );

            foreach ( var element in elements )
            {
                TreeNode prevNode = null;
                foreach ( var e in element.split )
                {
                    var node = null == prevNode ? treeViewElements.Nodes.Cast<TreeNode>().FirstOrDefault( s => s.Text == e ) : prevNode.Nodes.Cast<TreeNode>().FirstOrDefault( s => s.Text == e );

                    if ( null == node )
                    {
                        if ( null == prevNode )
                        {
                            prevNode = treeViewElements.Nodes.Add(e);
                        }
                        else
                        {
                            prevNode = prevNode.Nodes.Add(e);
                        }

                    }
                    else
                    {
                        prevNode = node;
                    }

                }
            }

            treeViewElements.ExpandAll();
        }

        public void SetItemData ( PrjContentObject itemData, String[] probingData )
        {
            if ( null != _itemData )
            {
                _itemData.PropertyChanged -= ItemData_PropertyChanged;
            }

            _itemData = itemData;

            checkBoxAddToOutput.DataBindings.Clear();
            comboBoxProbingPath.DataBindings.Clear();
            comboBoxCpOption.DataBindings.Clear();
            treeViewElements.Nodes.Clear();
            linkLabelPathRef.Text = "";

            comboBoxProbingPath.Items.Clear();
            if ( null != probingData )
            {
                comboBoxProbingPath.Items.AddRange(probingData);
            }

            if ( null != _itemData )
            {
                checkBoxAddToOutput.DataBindings.Add("Checked", itemData, "AddToOutput");
                comboBoxProbingPath.DataBindings.Add("Text", itemData, "ProbingPath");

                comboBoxCpOption.DataBindings.Add(new Binding("Text", itemData, "CpOption"));
                linkLabelPathRef.Text = itemData.NugetPathRef;
                _itemData.PropertyChanged += ItemData_PropertyChanged;
                BuildTreeView();
            }
        }

    }
}
