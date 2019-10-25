namespace ChaosRpgCharGen
{
    partial class ReviewConfigAndHelp
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
            this.mTabContrAdjust = new MetroFramework.Controls.MetroTabControl();
            this.mTgPgProgram = new MetroFramework.Controls.MetroTabPage();
            this.mBtnPrevious = new MetroFramework.Controls.MetroTile();
            this.mBtnNext = new MetroFramework.Controls.MetroTile();
            this.pctrBxAppDescr = new System.Windows.Forms.PictureBox();
            this.mTgPgConfig = new MetroFramework.Controls.MetroTabPage();
            this.btnConfSave = new MetroFramework.Controls.MetroTile();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.chckBDscpBenefProff = new System.Windows.Forms.CheckBox();
            this.chckBDscpBenefOrdinary = new System.Windows.Forms.CheckBox();
            this.mTgPgGame = new MetroFramework.Controls.MetroTabPage();
            this.mTabContrAdjust.SuspendLayout();
            this.mTgPgProgram.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pctrBxAppDescr)).BeginInit();
            this.mTgPgConfig.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mTabContrAdjust
            // 
            this.mTabContrAdjust.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mTabContrAdjust.Controls.Add(this.mTgPgProgram);
            this.mTabContrAdjust.Controls.Add(this.mTgPgConfig);
            this.mTabContrAdjust.Controls.Add(this.mTgPgGame);
            this.mTabContrAdjust.Location = new System.Drawing.Point(23, 63);
            this.mTabContrAdjust.Name = "mTabContrAdjust";
            this.mTabContrAdjust.SelectedIndex = 0;
            this.mTabContrAdjust.Size = new System.Drawing.Size(605, 479);
            this.mTabContrAdjust.TabIndex = 1;
            this.mTabContrAdjust.UseSelectable = true;
            // 
            // mTgPgProgram
            // 
            this.mTgPgProgram.Controls.Add(this.mBtnPrevious);
            this.mTgPgProgram.Controls.Add(this.mBtnNext);
            this.mTgPgProgram.Controls.Add(this.pctrBxAppDescr);
            this.mTgPgProgram.HorizontalScrollbarBarColor = true;
            this.mTgPgProgram.HorizontalScrollbarHighlightOnWheel = false;
            this.mTgPgProgram.HorizontalScrollbarSize = 10;
            this.mTgPgProgram.Location = new System.Drawing.Point(4, 38);
            this.mTgPgProgram.Name = "mTgPgProgram";
            this.mTgPgProgram.Size = new System.Drawing.Size(597, 437);
            this.mTgPgProgram.TabIndex = 0;
            this.mTgPgProgram.Text = "Segítség a programhoz";
            this.mTgPgProgram.VerticalScrollbarBarColor = true;
            this.mTgPgProgram.VerticalScrollbarHighlightOnWheel = false;
            this.mTgPgProgram.VerticalScrollbarSize = 10;
            // 
            // mBtnPrevious
            // 
            this.mBtnPrevious.ActiveControl = null;
            this.mBtnPrevious.Cursor = System.Windows.Forms.Cursors.Hand;
            this.mBtnPrevious.Location = new System.Drawing.Point(-1, 3);
            this.mBtnPrevious.Name = "mBtnPrevious";
            this.mBtnPrevious.Size = new System.Drawing.Size(31, 438);
            this.mBtnPrevious.Style = MetroFramework.MetroColorStyle.White;
            this.mBtnPrevious.TabIndex = 4;
            this.mBtnPrevious.Text = "<";
            this.mBtnPrevious.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.mBtnPrevious.TileImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.mBtnPrevious.TileTextFontWeight = MetroFramework.MetroTileTextWeight.Regular;
            this.mBtnPrevious.UseCustomForeColor = true;
            this.mBtnPrevious.UseSelectable = true;
            this.mBtnPrevious.Click += new System.EventHandler(this.mBtnPrevious_Click);
            // 
            // mBtnNext
            // 
            this.mBtnNext.ActiveControl = null;
            this.mBtnNext.Cursor = System.Windows.Forms.Cursors.Hand;
            this.mBtnNext.Location = new System.Drawing.Point(564, 3);
            this.mBtnNext.Name = "mBtnNext";
            this.mBtnNext.Size = new System.Drawing.Size(31, 434);
            this.mBtnNext.Style = MetroFramework.MetroColorStyle.White;
            this.mBtnNext.TabIndex = 3;
            this.mBtnNext.Text = ">";
            this.mBtnNext.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.mBtnNext.TileTextFontWeight = MetroFramework.MetroTileTextWeight.Bold;
            this.mBtnNext.UseCustomForeColor = true;
            this.mBtnNext.UseSelectable = true;
            this.mBtnNext.Click += new System.EventHandler(this.mBtnNext_Click);
            // 
            // pctrBxAppDescr
            // 
            this.pctrBxAppDescr.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pctrBxAppDescr.Location = new System.Drawing.Point(31, 0);
            this.pctrBxAppDescr.Name = "pctrBxAppDescr";
            this.pctrBxAppDescr.Size = new System.Drawing.Size(530, 441);
            this.pctrBxAppDescr.TabIndex = 2;
            this.pctrBxAppDescr.TabStop = false;
            // 
            // mTgPgConfig
            // 
            this.mTgPgConfig.Controls.Add(this.btnConfSave);
            this.mTgPgConfig.Controls.Add(this.panel1);
            this.mTgPgConfig.HorizontalScrollbarBarColor = true;
            this.mTgPgConfig.HorizontalScrollbarHighlightOnWheel = false;
            this.mTgPgConfig.HorizontalScrollbarSize = 10;
            this.mTgPgConfig.Location = new System.Drawing.Point(4, 38);
            this.mTgPgConfig.Name = "mTgPgConfig";
            this.mTgPgConfig.Size = new System.Drawing.Size(562, 402);
            this.mTgPgConfig.TabIndex = 3;
            this.mTgPgConfig.Text = "Beállítások";
            this.mTgPgConfig.VerticalScrollbarBarColor = true;
            this.mTgPgConfig.VerticalScrollbarHighlightOnWheel = false;
            this.mTgPgConfig.VerticalScrollbarSize = 10;
            // 
            // btnConfSave
            // 
            this.btnConfSave.ActiveControl = null;
            this.btnConfSave.Location = new System.Drawing.Point(401, 112);
            this.btnConfSave.Name = "btnConfSave";
            this.btnConfSave.Size = new System.Drawing.Size(75, 39);
            this.btnConfSave.Style = MetroFramework.MetroColorStyle.Teal;
            this.btnConfSave.TabIndex = 5;
            this.btnConfSave.Text = "Mentés";
            this.btnConfSave.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnConfSave.TileTextFontWeight = MetroFramework.MetroTileTextWeight.Regular;
            this.btnConfSave.UseSelectable = true;
            this.btnConfSave.Click += new System.EventHandler(this.btnConfSave_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.chckBDscpBenefProff);
            this.panel1.Controls.Add(this.chckBDscpBenefOrdinary);
            this.panel1.Location = new System.Drawing.Point(21, 23);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(458, 83);
            this.panel1.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(198, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(257, 39);
            this.label2.TabIndex = 5;
            this.label2.Text = "A beállítások nem befolyásolják a tárolt karaktereket,\r\nde a továbbiakban e-szeri" +
    "nt fog számolni minden\r\n további jártasság-szintet!";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(149, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Kedvező költséggel legyenek:";
            // 
            // chckBDscpBenefProff
            // 
            this.chckBDscpBenefProff.AutoSize = true;
            this.chckBDscpBenefProff.Location = new System.Drawing.Point(22, 53);
            this.chckBDscpBenefProff.Name = "chckBDscpBenefProff";
            this.chckBDscpBenefProff.Size = new System.Drawing.Size(123, 17);
            this.chckBDscpBenefProff.TabIndex = 2;
            this.chckBDscpBenefProff.Text = "Szakmai jártasságok";
            this.chckBDscpBenefProff.UseVisualStyleBackColor = true;
            // 
            // chckBDscpBenefOrdinary
            // 
            this.chckBDscpBenefOrdinary.AutoSize = true;
            this.chckBDscpBenefOrdinary.Location = new System.Drawing.Point(22, 30);
            this.chckBDscpBenefOrdinary.Name = "chckBDscpBenefOrdinary";
            this.chckBDscpBenefOrdinary.Size = new System.Drawing.Size(137, 17);
            this.chckBDscpBenefOrdinary.TabIndex = 3;
            this.chckBDscpBenefOrdinary.Text = "Hétköznapi jártasságok";
            this.chckBDscpBenefOrdinary.UseVisualStyleBackColor = true;
            // 
            // mTgPgGame
            // 
            this.mTgPgGame.HorizontalScrollbarBarColor = true;
            this.mTgPgGame.HorizontalScrollbarHighlightOnWheel = false;
            this.mTgPgGame.HorizontalScrollbarSize = 10;
            this.mTgPgGame.Location = new System.Drawing.Point(4, 38);
            this.mTgPgGame.Name = "mTgPgGame";
            this.mTgPgGame.Size = new System.Drawing.Size(562, 402);
            this.mTgPgGame.TabIndex = 1;
            this.mTgPgGame.Text = "Segítség a játékhoz";
            this.mTgPgGame.VerticalScrollbarBarColor = true;
            this.mTgPgGame.VerticalScrollbarHighlightOnWheel = false;
            this.mTgPgGame.VerticalScrollbarSize = 10;
            // 
            // ReviewConfigAndHelp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(651, 565);
            this.Controls.Add(this.mTabContrAdjust);
            this.Name = "ReviewConfigAndHelp";
            this.Resizable = false;
            this.Text = "Leírás és beállítások";
            this.TextAlign = MetroFramework.Forms.MetroFormTextAlign.Center;
            this.mTabContrAdjust.ResumeLayout(false);
            this.mTgPgProgram.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pctrBxAppDescr)).EndInit();
            this.mTgPgConfig.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private MetroFramework.Controls.MetroTabControl mTabContrAdjust;
        private MetroFramework.Controls.MetroTabPage mTgPgProgram;
        private MetroFramework.Controls.MetroTabPage mTgPgGame;
        private MetroFramework.Controls.MetroTabPage mTgPgConfig;
        private System.Windows.Forms.CheckBox chckBDscpBenefOrdinary;
        private System.Windows.Forms.CheckBox chckBDscpBenefProff;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pctrBxAppDescr;
        private MetroFramework.Controls.MetroTile btnConfSave;
        private System.Windows.Forms.Label label2;
        private MetroFramework.Controls.MetroTile mBtnNext;
        private MetroFramework.Controls.MetroTile mBtnPrevious;
    }
}