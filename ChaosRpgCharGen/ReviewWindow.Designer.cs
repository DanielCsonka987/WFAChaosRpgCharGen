namespace ChaosRpgCharGen
{
    partial class ReviewWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReviewWindow));
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.dtgrdCharList = new System.Windows.Forms.DataGridView();
            this.mBtnNew = new MetroFramework.Controls.MetroTile();
            this.mBtnClose = new MetroFramework.Controls.MetroTile();
            this.mBtnOpen = new MetroFramework.Controls.MetroTile();
            this.mBtnDelete = new MetroFramework.Controls.MetroTile();
            this.label1 = new System.Windows.Forms.Label();
            this.lblDetails = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgrdCharList)).BeginInit();
            this.SuspendLayout();
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.metroLabel1.Location = new System.Drawing.Point(203, 87);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(175, 25);
            this.metroLabel1.TabIndex = 5;
            this.metroLabel1.Text = "Bejegyzett karakterek:";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(5, 297);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(192, 192);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            // 
            // dtgrdCharList
            // 
            this.dtgrdCharList.AllowUserToAddRows = false;
            this.dtgrdCharList.AllowUserToDeleteRows = false;
            this.dtgrdCharList.AllowUserToOrderColumns = true;
            this.dtgrdCharList.AllowUserToResizeRows = false;
            this.dtgrdCharList.BackgroundColor = System.Drawing.Color.White;
            this.dtgrdCharList.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dtgrdCharList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgrdCharList.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dtgrdCharList.GridColor = System.Drawing.Color.White;
            this.dtgrdCharList.Location = new System.Drawing.Point(203, 115);
            this.dtgrdCharList.MultiSelect = false;
            this.dtgrdCharList.Name = "dtgrdCharList";
            this.dtgrdCharList.RowHeadersVisible = false;
            this.dtgrdCharList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtgrdCharList.Size = new System.Drawing.Size(230, 374);
            this.dtgrdCharList.TabIndex = 7;
            // 
            // mBtnNew
            // 
            this.mBtnNew.ActiveControl = null;
            this.mBtnNew.Location = new System.Drawing.Point(23, 87);
            this.mBtnNew.Name = "mBtnNew";
            this.mBtnNew.Size = new System.Drawing.Size(155, 39);
            this.mBtnNew.Style = MetroFramework.MetroColorStyle.Lime;
            this.mBtnNew.TabIndex = 8;
            this.mBtnNew.Text = "Új létrehozása";
            this.mBtnNew.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.mBtnNew.TileTextFontWeight = MetroFramework.MetroTileTextWeight.Regular;
            this.mBtnNew.UseSelectable = true;
            this.mBtnNew.Click += new System.EventHandler(this.mbtnNew_Click);
            // 
            // mBtnClose
            // 
            this.mBtnClose.ActiveControl = null;
            this.mBtnClose.Location = new System.Drawing.Point(23, 223);
            this.mBtnClose.Name = "mBtnClose";
            this.mBtnClose.Size = new System.Drawing.Size(155, 39);
            this.mBtnClose.Style = MetroFramework.MetroColorStyle.Silver;
            this.mBtnClose.TabIndex = 9;
            this.mBtnClose.Text = "Kilépés";
            this.mBtnClose.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.mBtnClose.TileTextFontWeight = MetroFramework.MetroTileTextWeight.Regular;
            this.mBtnClose.UseSelectable = true;
            this.mBtnClose.Click += new System.EventHandler(this.mBtnClose_Click);
            // 
            // mBtnOpen
            // 
            this.mBtnOpen.ActiveControl = null;
            this.mBtnOpen.Location = new System.Drawing.Point(23, 133);
            this.mBtnOpen.Name = "mBtnOpen";
            this.mBtnOpen.Size = new System.Drawing.Size(155, 39);
            this.mBtnOpen.TabIndex = 10;
            this.mBtnOpen.Text = "Megnyitás";
            this.mBtnOpen.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.mBtnOpen.TileTextFontWeight = MetroFramework.MetroTileTextWeight.Regular;
            this.mBtnOpen.UseSelectable = true;
            this.mBtnOpen.Click += new System.EventHandler(this.mBtnOpen_Click);
            // 
            // mBtnDelete
            // 
            this.mBtnDelete.ActiveControl = null;
            this.mBtnDelete.Location = new System.Drawing.Point(23, 178);
            this.mBtnDelete.Name = "mBtnDelete";
            this.mBtnDelete.Size = new System.Drawing.Size(155, 39);
            this.mBtnDelete.Style = MetroFramework.MetroColorStyle.Red;
            this.mBtnDelete.TabIndex = 11;
            this.mBtnDelete.Text = "Törlés";
            this.mBtnDelete.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.mBtnDelete.TileTextFontWeight = MetroFramework.MetroTileTextWeight.Regular;
            this.mBtnDelete.UseSelectable = true;
            this.mBtnDelete.Click += new System.EventHandler(this.mBtnDelete_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(144, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(159, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "A második kiadáshoz, verzió 1.0";
            // 
            // lblDetails
            // 
            this.lblDetails.AutoSize = true;
            this.lblDetails.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblDetails.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblDetails.Location = new System.Drawing.Point(49, 281);
            this.lblDetails.Name = "lblDetails";
            this.lblDetails.Size = new System.Drawing.Size(106, 13);
            this.lblDetails.TabIndex = 13;
            this.lblDetails.Text = "Leírás és beállítások";
            this.lblDetails.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lblDetails.Click += new System.EventHandler(this.lblDetails_Click);
            // 
            // ReviewWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(456, 512);
            this.Controls.Add(this.lblDetails);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.mBtnDelete);
            this.Controls.Add(this.mBtnOpen);
            this.Controls.Add(this.mBtnClose);
            this.Controls.Add(this.mBtnNew);
            this.Controls.Add(this.dtgrdCharList);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.metroLabel1);
            this.MaximizeBox = false;
            this.Name = "ReviewWindow";
            this.Resizable = false;
            this.Text = "Káosz karakterkészítő";
            this.TextAlign = MetroFramework.Forms.MetroFormTextAlign.Center;
            this.VisibleChanged += new System.EventHandler(this.ReviewWindow_VisibleChanged);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgrdCharList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.DataGridView dtgrdCharList;
        private MetroFramework.Controls.MetroTile mBtnNew;
        private MetroFramework.Controls.MetroTile mBtnClose;
        private MetroFramework.Controls.MetroTile mBtnOpen;
        private MetroFramework.Controls.MetroTile mBtnDelete;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblDetails;
    }
}

