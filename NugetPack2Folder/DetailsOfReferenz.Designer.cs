namespace NugetPack2Folder
{
    partial class DetailsOfReferenz
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.checkBoxAddToOutput = new System.Windows.Forms.CheckBox();
            this.labelProbingPath = new System.Windows.Forms.Label();
            this.comboBoxProbingPath = new System.Windows.Forms.ComboBox();
            this.groupBoxElements = new System.Windows.Forms.GroupBox();
            this.treeViewElements = new System.Windows.Forms.TreeView();
            this.linkLabelPathRef = new System.Windows.Forms.LinkLabel();
            this.labelCopyOption = new System.Windows.Forms.Label();
            this.comboBoxCpOption = new System.Windows.Forms.ComboBox();
            this.groupBoxElements.SuspendLayout();
            this.SuspendLayout();
            // 
            // checkBoxAddToOutput
            // 
            this.checkBoxAddToOutput.AutoSize = true;
            this.checkBoxAddToOutput.Checked = true;
            this.checkBoxAddToOutput.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxAddToOutput.Location = new System.Drawing.Point(20, 17);
            this.checkBoxAddToOutput.Name = "checkBoxAddToOutput";
            this.checkBoxAddToOutput.Size = new System.Drawing.Size(90, 17);
            this.checkBoxAddToOutput.TabIndex = 0;
            this.checkBoxAddToOutput.Text = "Add to output";
            this.checkBoxAddToOutput.UseVisualStyleBackColor = true;
            // 
            // labelProbingPath
            // 
            this.labelProbingPath.AutoSize = true;
            this.labelProbingPath.Location = new System.Drawing.Point(20, 42);
            this.labelProbingPath.Name = "labelProbingPath";
            this.labelProbingPath.Size = new System.Drawing.Size(71, 13);
            this.labelProbingPath.TabIndex = 1;
            this.labelProbingPath.Text = "Probing Path:";
            // 
            // comboBoxProbingPath
            // 
            this.comboBoxProbingPath.FormattingEnabled = true;
            this.comboBoxProbingPath.Location = new System.Drawing.Point(97, 39);
            this.comboBoxProbingPath.Name = "comboBoxProbingPath";
            this.comboBoxProbingPath.Size = new System.Drawing.Size(400, 21);
            this.comboBoxProbingPath.TabIndex = 2;
            this.comboBoxProbingPath.Validating += new System.ComponentModel.CancelEventHandler(this.ComboBoxProbingPath_Validating);
            this.comboBoxProbingPath.Validated += new System.EventHandler(this.ComboBoxProbingPath_Validated);
            // 
            // groupBoxElements
            // 
            this.groupBoxElements.Controls.Add(this.treeViewElements);
            this.groupBoxElements.Location = new System.Drawing.Point(20, 136);
            this.groupBoxElements.Name = "groupBoxElements";
            this.groupBoxElements.Size = new System.Drawing.Size(483, 244);
            this.groupBoxElements.TabIndex = 5;
            this.groupBoxElements.TabStop = false;
            this.groupBoxElements.Text = "Elemets to move";
            // 
            // treeViewElements
            // 
            this.treeViewElements.Location = new System.Drawing.Point(7, 20);
            this.treeViewElements.Name = "treeViewElements";
            this.treeViewElements.Size = new System.Drawing.Size(470, 211);
            this.treeViewElements.TabIndex = 0;
            // 
            // linkLabelPathRef
            // 
            this.linkLabelPathRef.AutoSize = true;
            this.linkLabelPathRef.Location = new System.Drawing.Point(27, 100);
            this.linkLabelPathRef.Name = "linkLabelPathRef";
            this.linkLabelPathRef.Size = new System.Drawing.Size(0, 13);
            this.linkLabelPathRef.TabIndex = 6;
            this.linkLabelPathRef.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabelPathRef_LinkClicked);
            // 
            // labelCopyOption
            // 
            this.labelCopyOption.AutoSize = true;
            this.labelCopyOption.Location = new System.Drawing.Point(20, 65);
            this.labelCopyOption.Name = "labelCopyOption";
            this.labelCopyOption.Size = new System.Drawing.Size(68, 13);
            this.labelCopyOption.TabIndex = 3;
            this.labelCopyOption.Text = "Copy Option:";
            // 
            // comboBoxCpOption
            // 
            this.comboBoxCpOption.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCpOption.FormattingEnabled = true;
            this.comboBoxCpOption.Items.AddRange(new object[] {
            "Always",
            "PreserveNewest"});
            this.comboBoxCpOption.Location = new System.Drawing.Point(97, 62);
            this.comboBoxCpOption.Name = "comboBoxCpOption";
            this.comboBoxCpOption.Size = new System.Drawing.Size(175, 21);
            this.comboBoxCpOption.TabIndex = 4;
            this.comboBoxCpOption.SelectedIndexChanged += new System.EventHandler(this.ComboBoxCpOption_SelectedIndexChanged);
            // 
            // DetailsOfReferenz
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.comboBoxCpOption);
            this.Controls.Add(this.labelCopyOption);
            this.Controls.Add(this.linkLabelPathRef);
            this.Controls.Add(this.groupBoxElements);
            this.Controls.Add(this.comboBoxProbingPath);
            this.Controls.Add(this.labelProbingPath);
            this.Controls.Add(this.checkBoxAddToOutput);
            this.Name = "DetailsOfReferenz";
            this.Size = new System.Drawing.Size(529, 404);
            this.groupBoxElements.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox checkBoxAddToOutput;
        private System.Windows.Forms.Label labelProbingPath;
        private System.Windows.Forms.ComboBox comboBoxProbingPath;
        private System.Windows.Forms.GroupBox groupBoxElements;
        private System.Windows.Forms.TreeView treeViewElements;
        private System.Windows.Forms.LinkLabel linkLabelPathRef;
        private System.Windows.Forms.Label labelCopyOption;
        private System.Windows.Forms.ComboBox comboBoxCpOption;
    }
}
