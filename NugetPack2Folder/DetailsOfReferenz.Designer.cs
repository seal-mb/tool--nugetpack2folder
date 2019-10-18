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
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxProbingPath = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.treeViewElements = new System.Windows.Forms.TreeView();
            this.linkLabelPathRef = new System.Windows.Forms.LinkLabel();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // checkBoxAddToOutput
            // 
            this.checkBoxAddToOutput.AutoSize = true;
            this.checkBoxAddToOutput.Checked = true;
            this.checkBoxAddToOutput.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxAddToOutput.Location = new System.Drawing.Point(20, 23);
            this.checkBoxAddToOutput.Name = "checkBoxAddToOutput";
            this.checkBoxAddToOutput.Size = new System.Drawing.Size(90, 17);
            this.checkBoxAddToOutput.TabIndex = 1;
            this.checkBoxAddToOutput.Text = "Add to output";
            this.checkBoxAddToOutput.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Probing Path:";
            // 
            // comboBoxProbingPath
            // 
            this.comboBoxProbingPath.FormattingEnabled = true;
            this.comboBoxProbingPath.Location = new System.Drawing.Point(97, 53);
            this.comboBoxProbingPath.Name = "comboBoxProbingPath";
            this.comboBoxProbingPath.Size = new System.Drawing.Size(400, 21);
            this.comboBoxProbingPath.TabIndex = 3;
            this.comboBoxProbingPath.Validating += new System.ComponentModel.CancelEventHandler(this.ComboBoxProbingPath_Validating);
            this.comboBoxProbingPath.Validated += new System.EventHandler(this.ComboBoxProbingPath_Validated);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.treeViewElements);
            this.groupBox1.Location = new System.Drawing.Point(20, 136);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(483, 244);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Elemets to move";
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
            this.linkLabelPathRef.Location = new System.Drawing.Point(27, 97);
            this.linkLabelPathRef.Name = "linkLabelPathRef";
            this.linkLabelPathRef.Size = new System.Drawing.Size(0, 13);
            this.linkLabelPathRef.TabIndex = 5;
            this.linkLabelPathRef.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabelPathRef_LinkClicked);
            // 
            // DetailsOfReferenz
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.linkLabelPathRef);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.comboBoxProbingPath);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.checkBoxAddToOutput);
            this.Name = "DetailsOfReferenz";
            this.Size = new System.Drawing.Size(871, 553);
            this.Load += new System.EventHandler(this.DetailsOfReferenz_Load);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox checkBoxAddToOutput;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxProbingPath;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TreeView treeViewElements;
        private System.Windows.Forms.LinkLabel linkLabelPathRef;
    }
}
