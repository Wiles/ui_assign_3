namespace GMHAStandings
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
            this.cbHome = new System.Windows.Forms.ComboBox();
            this.udHome = new System.Windows.Forms.NumericUpDown();
            this.udVisitor = new System.Windows.Forms.NumericUpDown();
            this.cbVisitor = new System.Windows.Forms.ComboBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lvStandings = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.dtpGame = new System.Windows.Forms.DateTimePicker();
            this.ofDlg = new System.Windows.Forms.OpenFileDialog();
            this.sfDlg = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.udHome)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udVisitor)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbHome
            // 
            this.cbHome.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbHome.DropDownWidth = 200;
            this.cbHome.FormattingEnabled = true;
            this.cbHome.Location = new System.Drawing.Point(104, 249);
            this.cbHome.MaxDropDownItems = 10;
            this.cbHome.Name = "cbHome";
            this.cbHome.Size = new System.Drawing.Size(89, 21);
            this.cbHome.TabIndex = 0;
            // 
            // udHome
            // 
            this.udHome.Location = new System.Drawing.Point(199, 249);
            this.udHome.Name = "udHome";
            this.udHome.Size = new System.Drawing.Size(34, 20);
            this.udHome.TabIndex = 1;
            // 
            // udVisitor
            // 
            this.udVisitor.Location = new System.Drawing.Point(334, 249);
            this.udVisitor.Name = "udVisitor";
            this.udVisitor.Size = new System.Drawing.Size(34, 20);
            this.udVisitor.TabIndex = 3;
            // 
            // cbVisitor
            // 
            this.cbVisitor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbVisitor.DropDownWidth = 200;
            this.cbVisitor.FormattingEnabled = true;
            this.cbVisitor.Location = new System.Drawing.Point(239, 249);
            this.cbVisitor.MaxDropDownItems = 10;
            this.cbVisitor.Name = "cbVisitor";
            this.cbVisitor.Size = new System.Drawing.Size(89, 21);
            this.cbVisitor.TabIndex = 2;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(374, 248);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(39, 21);
            this.btnAdd.TabIndex = 4;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.exportToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(425, 24);
            this.menuStrip1.TabIndex = 5;
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
            // lvStandings
            // 
            this.lvStandings.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader8,
            this.columnHeader9,
            this.columnHeader7});
            this.lvStandings.Location = new System.Drawing.Point(12, 27);
            this.lvStandings.Name = "lvStandings";
            this.lvStandings.Size = new System.Drawing.Size(401, 216);
            this.lvStandings.TabIndex = 6;
            this.lvStandings.UseCompatibleStateImageBehavior = false;
            this.lvStandings.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Team";
            this.columnHeader1.Width = 140;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "G";
            this.columnHeader2.Width = 27;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "W";
            this.columnHeader3.Width = 27;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "L";
            this.columnHeader4.Width = 27;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "T";
            this.columnHeader5.Width = 27;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "PT";
            this.columnHeader6.Width = 27;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "GF";
            this.columnHeader8.Width = 27;
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "GA";
            this.columnHeader9.Width = 27;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Diff";
            this.columnHeader7.Width = 50;
            // 
            // dtpGame
            // 
            this.dtpGame.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpGame.Location = new System.Drawing.Point(12, 250);
            this.dtpGame.Name = "dtpGame";
            this.dtpGame.Size = new System.Drawing.Size(86, 20);
            this.dtpGame.TabIndex = 7;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(425, 280);
            this.Controls.Add(this.dtpGame);
            this.Controls.Add(this.lvStandings);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.udVisitor);
            this.Controls.Add(this.cbVisitor);
            this.Controls.Add(this.udHome);
            this.Controls.Add(this.cbHome);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GMHA Standings";
            this.Load += new System.EventHandler(this.frmMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.udHome)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udVisitor)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbHome;
        private System.Windows.Forms.NumericUpDown udHome;
        private System.Windows.Forms.NumericUpDown udVisitor;
        private System.Windows.Forms.ComboBox cbVisitor;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
        private System.Windows.Forms.ListView lvStandings;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.DateTimePicker dtpGame;
        private System.Windows.Forms.OpenFileDialog ofDlg;
        private System.Windows.Forms.SaveFileDialog sfDlg;
    }
}

