namespace GMHAStats
{
    partial class frmMain
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
            this.tbName = new System.Windows.Forms.TextBox();
            this.tbTeam = new System.Windows.Forms.TextBox();
            this.tbGoals = new System.Windows.Forms.TextBox();
            this.tbAssists = new System.Windows.Forms.TextBox();
            this.lvStats = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnAdd = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.miVerify = new System.Windows.Forms.ToolStripMenuItem();
            this.tbPenalty = new System.Windows.Forms.TextBox();
            this.sfDlg = new System.Windows.Forms.SaveFileDialog();
            this.ofDlg = new System.Windows.Forms.OpenFileDialog();
            this.newsTextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbName
            // 
            this.tbName.Location = new System.Drawing.Point(12, 291);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(129, 20);
            this.tbName.TabIndex = 0;
            this.tbName.TextChanged += new System.EventHandler(this.tbName_TextChanged);
            this.tbName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbName_KeyDown);
            this.tbName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbName_KeyPress);
            this.tbName.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tbName_KeyUp);
            // 
            // tbTeam
            // 
            this.tbTeam.Location = new System.Drawing.Point(147, 291);
            this.tbTeam.Name = "tbTeam";
            this.tbTeam.Size = new System.Drawing.Size(91, 20);
            this.tbTeam.TabIndex = 1;
            // 
            // tbGoals
            // 
            this.tbGoals.Location = new System.Drawing.Point(244, 291);
            this.tbGoals.Name = "tbGoals";
            this.tbGoals.Size = new System.Drawing.Size(34, 20);
            this.tbGoals.TabIndex = 2;
            // 
            // tbAssists
            // 
            this.tbAssists.Location = new System.Drawing.Point(284, 291);
            this.tbAssists.Name = "tbAssists";
            this.tbAssists.Size = new System.Drawing.Size(32, 20);
            this.tbAssists.TabIndex = 3;
            // 
            // lvStats
            // 
            this.lvStats.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader6,
            this.columnHeader5});
            this.lvStats.Location = new System.Drawing.Point(12, 27);
            this.lvStats.Name = "lvStats";
            this.lvStats.Size = new System.Drawing.Size(401, 258);
            this.lvStats.TabIndex = 4;
            this.lvStats.TabStop = false;
            this.lvStats.UseCompatibleStateImageBehavior = false;
            this.lvStats.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Name";
            this.columnHeader1.Width = 134;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Team";
            this.columnHeader2.Width = 92;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "G";
            this.columnHeader3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader3.Width = 30;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "A";
            this.columnHeader4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader4.Width = 37;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "PM";
            this.columnHeader6.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader6.Width = 40;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Pts";
            this.columnHeader5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader5.Width = 38;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(372, 290);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(41, 20);
            this.btnAdd.TabIndex = 5;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.exportToolStripMenuItem,
            this.miVerify,
            this.newsTextToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(425, 24);
            this.menuStrip1.TabIndex = 6;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // exportToolStripMenuItem
            // 
            this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            this.exportToolStripMenuItem.Size = new System.Drawing.Size(53, 20);
            this.exportToolStripMenuItem.Text = "Export";
            this.exportToolStripMenuItem.Click += new System.EventHandler(this.exportToolStripMenuItem_Click);
            // 
            // miVerify
            // 
            this.miVerify.Name = "miVerify";
            this.miVerify.Size = new System.Drawing.Size(50, 20);
            this.miVerify.Text = "Verify";
            this.miVerify.Click += new System.EventHandler(this.miVerify_Click);
            // 
            // tbPenalty
            // 
            this.tbPenalty.Location = new System.Drawing.Point(322, 291);
            this.tbPenalty.Name = "tbPenalty";
            this.tbPenalty.Size = new System.Drawing.Size(32, 20);
            this.tbPenalty.TabIndex = 4;
            // 
            // newsTextToolStripMenuItem
            // 
            this.newsTextToolStripMenuItem.Name = "newsTextToolStripMenuItem";
            this.newsTextToolStripMenuItem.Size = new System.Drawing.Size(74, 20);
            this.newsTextToolStripMenuItem.Text = "News Text";
            this.newsTextToolStripMenuItem.Click += new System.EventHandler(this.newsTextToolStripMenuItem_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(425, 323);
            this.Controls.Add(this.tbPenalty);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.lvStats);
            this.Controls.Add(this.tbAssists);
            this.Controls.Add(this.tbGoals);
            this.Controls.Add(this.tbTeam);
            this.Controls.Add(this.tbName);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GMHA Stats";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.TextBox tbTeam;
        private System.Windows.Forms.TextBox tbGoals;
        private System.Windows.Forms.TextBox tbAssists;
        private System.Windows.Forms.ListView lvStats;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
        private System.Windows.Forms.TextBox tbPenalty;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.SaveFileDialog sfDlg;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog ofDlg;
        private System.Windows.Forms.ToolStripMenuItem miVerify;
        private System.Windows.Forms.ToolStripMenuItem newsTextToolStripMenuItem;
    }
}

