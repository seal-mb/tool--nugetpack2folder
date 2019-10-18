namespace NugetPack2Folder
{
    partial class MainFrm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainFrm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.dateiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openProjectFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.quitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabelFound = new System.Windows.Forms.ToolStripStatusLabel();
            this.splitContainerMain = new System.Windows.Forms.SplitContainer();
            this.buttonSetProbing = new System.Windows.Forms.Button();
            this.comboBoxProbingVal = new System.Windows.Forms.ComboBox();
            this.textBoxProbing = new System.Windows.Forms.TextBox();
            this.labelProbing = new System.Windows.Forms.Label();
            this.splitContainerValues = new System.Windows.Forms.SplitContainer();
            this.listViewReferenz = new System.Windows.Forms.ListView();
            this.columnHeaderPath = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderCopy = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.detailsOfReferenz = new NugetPack2Folder.DetailsOfReferenz();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).BeginInit();
            this.splitContainerMain.Panel1.SuspendLayout();
            this.splitContainerMain.Panel2.SuspendLayout();
            this.splitContainerMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerValues)).BeginInit();
            this.splitContainerValues.Panel1.SuspendLayout();
            this.splitContainerValues.Panel2.SuspendLayout();
            this.splitContainerValues.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dateiToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(865, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // dateiToolStripMenuItem
            // 
            this.dateiToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openProjectFileToolStripMenuItem,
            this.toolStripSeparator1,
            this.quitMenuItem});
            this.dateiToolStripMenuItem.Name = "dateiToolStripMenuItem";
            this.dateiToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.dateiToolStripMenuItem.Text = "&Datei";
            // 
            // openProjectFileToolStripMenuItem
            // 
            this.openProjectFileToolStripMenuItem.Name = "openProjectFileToolStripMenuItem";
            this.openProjectFileToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.openProjectFileToolStripMenuItem.Text = "Open Project File";
            this.openProjectFileToolStripMenuItem.Click += new System.EventHandler(this.OpenProjectFileToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(161, 6);
            // 
            // quitMenuItem
            // 
            this.quitMenuItem.Name = "quitMenuItem";
            this.quitMenuItem.Size = new System.Drawing.Size(164, 22);
            this.quitMenuItem.Text = "&Quit";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelFound});
            this.statusStrip1.Location = new System.Drawing.Point(0, 528);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(865, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabelFound
            // 
            this.toolStripStatusLabelFound.Name = "toolStripStatusLabelFound";
            this.toolStripStatusLabelFound.Size = new System.Drawing.Size(41, 17);
            this.toolStripStatusLabelFound.Text = "Empty";
            // 
            // splitContainerMain
            // 
            this.splitContainerMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainerMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerMain.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainerMain.IsSplitterFixed = true;
            this.splitContainerMain.Location = new System.Drawing.Point(0, 24);
            this.splitContainerMain.Name = "splitContainerMain";
            this.splitContainerMain.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerMain.Panel1
            // 
            this.splitContainerMain.Panel1.Controls.Add(this.buttonSetProbing);
            this.splitContainerMain.Panel1.Controls.Add(this.comboBoxProbingVal);
            this.splitContainerMain.Panel1.Controls.Add(this.textBoxProbing);
            this.splitContainerMain.Panel1.Controls.Add(this.labelProbing);
            this.splitContainerMain.Panel1MinSize = 10;
            // 
            // splitContainerMain.Panel2
            // 
            this.splitContainerMain.Panel2.Controls.Add(this.splitContainerValues);
            this.splitContainerMain.Size = new System.Drawing.Size(865, 504);
            this.splitContainerMain.SplitterDistance = 100;
            this.splitContainerMain.TabIndex = 2;
            // 
            // buttonSetProbing
            // 
            this.buttonSetProbing.Location = new System.Drawing.Point(291, 41);
            this.buttonSetProbing.Name = "buttonSetProbing";
            this.buttonSetProbing.Size = new System.Drawing.Size(145, 23);
            this.buttonSetProbing.TabIndex = 3;
            this.buttonSetProbing.Text = "Set all Items to this probing";
            this.buttonSetProbing.UseVisualStyleBackColor = true;
            this.buttonSetProbing.Click += new System.EventHandler(this.ButtonSetProbing_Click);
            // 
            // comboBoxProbingVal
            // 
            this.comboBoxProbingVal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxProbingVal.FormattingEnabled = true;
            this.comboBoxProbingVal.Location = new System.Drawing.Point(66, 41);
            this.comboBoxProbingVal.Name = "comboBoxProbingVal";
            this.comboBoxProbingVal.Size = new System.Drawing.Size(219, 21);
            this.comboBoxProbingVal.TabIndex = 2;
            // 
            // textBoxProbing
            // 
            this.textBoxProbing.Location = new System.Drawing.Point(66, 14);
            this.textBoxProbing.Name = "textBoxProbing";
            this.textBoxProbing.Size = new System.Drawing.Size(738, 20);
            this.textBoxProbing.TabIndex = 1;
            this.textBoxProbing.Validating += new System.ComponentModel.CancelEventHandler(this.TextBoxProbing_Validating);
            this.textBoxProbing.Validated += new System.EventHandler(this.TextBoxProbing_Validated);
            // 
            // labelProbing
            // 
            this.labelProbing.AutoSize = true;
            this.labelProbing.Location = new System.Drawing.Point(13, 16);
            this.labelProbing.Name = "labelProbing";
            this.labelProbing.Size = new System.Drawing.Size(46, 13);
            this.labelProbing.TabIndex = 0;
            this.labelProbing.Text = "Probing:";
            // 
            // splitContainerValues
            // 
            this.splitContainerValues.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainerValues.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerValues.Location = new System.Drawing.Point(0, 0);
            this.splitContainerValues.Name = "splitContainerValues";
            // 
            // splitContainerValues.Panel1
            // 
            this.splitContainerValues.Panel1.Controls.Add(this.listViewReferenz);
            // 
            // splitContainerValues.Panel2
            // 
            this.splitContainerValues.Panel2.Controls.Add(this.detailsOfReferenz);
            this.splitContainerValues.Size = new System.Drawing.Size(863, 398);
            this.splitContainerValues.SplitterDistance = 287;
            this.splitContainerValues.TabIndex = 0;
            // 
            // listViewReferenz
            // 
            this.listViewReferenz.CheckBoxes = true;
            this.listViewReferenz.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderPath,
            this.columnHeaderCopy});
            this.listViewReferenz.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewReferenz.HideSelection = false;
            this.listViewReferenz.Location = new System.Drawing.Point(0, 0);
            this.listViewReferenz.MultiSelect = false;
            this.listViewReferenz.Name = "listViewReferenz";
            this.listViewReferenz.Size = new System.Drawing.Size(283, 394);
            this.listViewReferenz.TabIndex = 0;
            this.listViewReferenz.UseCompatibleStateImageBehavior = false;
            this.listViewReferenz.View = System.Windows.Forms.View.Details;
            this.listViewReferenz.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.ListViewReferenz_ItemChecked);
            this.listViewReferenz.SelectedIndexChanged += new System.EventHandler(this.ListViewReferenz_SelectedIndexChanged);
            // 
            // columnHeaderPath
            // 
            this.columnHeaderPath.Text = "Module Name";
            this.columnHeaderPath.Width = 186;
            // 
            // columnHeaderCopy
            // 
            this.columnHeaderCopy.Text = "Copy local";
            this.columnHeaderCopy.Width = 221;
            // 
            // detailsOfReferenz
            // 
            this.detailsOfReferenz.Dock = System.Windows.Forms.DockStyle.Fill;
            this.detailsOfReferenz.Location = new System.Drawing.Point(0, 0);
            this.detailsOfReferenz.Name = "detailsOfReferenz";
            this.detailsOfReferenz.Size = new System.Drawing.Size(568, 394);
            this.detailsOfReferenz.TabIndex = 0;
            this.detailsOfReferenz.Visible = false;
            // 
            // MainFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(865, 550);
            this.Controls.Add(this.splitContainerMain);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainFrm";
            this.Text = "Nuget to subfolder";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.splitContainerMain.Panel1.ResumeLayout(false);
            this.splitContainerMain.Panel1.PerformLayout();
            this.splitContainerMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).EndInit();
            this.splitContainerMain.ResumeLayout(false);
            this.splitContainerValues.Panel1.ResumeLayout(false);
            this.splitContainerValues.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerValues)).EndInit();
            this.splitContainerValues.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem dateiToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openProjectFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem quitMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelFound;
        private System.Windows.Forms.SplitContainer splitContainerMain;
        private System.Windows.Forms.TextBox textBoxProbing;
        private System.Windows.Forms.Label labelProbing;
        private System.Windows.Forms.SplitContainer splitContainerValues;
        private System.Windows.Forms.ListView listViewReferenz;
        private System.Windows.Forms.ColumnHeader columnHeaderPath;
        private System.Windows.Forms.ColumnHeader columnHeaderCopy;
        private DetailsOfReferenz detailsOfReferenz;
        private System.Windows.Forms.ComboBox comboBoxProbingVal;
        private System.Windows.Forms.Button buttonSetProbing;
    }
}

