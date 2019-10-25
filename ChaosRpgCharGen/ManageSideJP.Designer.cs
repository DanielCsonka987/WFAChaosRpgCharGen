namespace ChaosRpgCharGen
{
    partial class ManageSideJP
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
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnBack = new MetroFramework.Controls.MetroTile();
            this.lblTextStarterJP = new System.Windows.Forms.Label();
            this.lblTextSpentJP = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblTextCollectedJP = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblTextAvailableJP = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblTextSumJP = new System.Windows.Forms.Label();
            this.txtBNewJP = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.dtgrvJPGainList = new System.Windows.Forms.DataGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnRemoveJP = new MetroFramework.Controls.MetroTile();
            this.btnNewJP = new MetroFramework.Controls.MetroTile();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgrvJPGainList)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 101);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Összes JP";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnBack);
            this.panel1.Controls.Add(this.lblTextStarterJP);
            this.panel1.Controls.Add(this.lblTextSpentJP);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.lblTextCollectedJP);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.lblTextAvailableJP);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.lblTextSumJP);
            this.panel1.Location = new System.Drawing.Point(23, 63);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(156, 143);
            this.panel1.TabIndex = 2;
            // 
            // btnBack
            // 
            this.btnBack.ActiveControl = null;
            this.btnBack.Location = new System.Drawing.Point(78, 101);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(75, 39);
            this.btnBack.Style = MetroFramework.MetroColorStyle.Silver;
            this.btnBack.TabIndex = 13;
            this.btnBack.Text = "Vissza";
            this.btnBack.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnBack.TileTextFontWeight = MetroFramework.MetroTileTextWeight.Regular;
            this.btnBack.UseSelectable = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // lblTextStarterJP
            // 
            this.lblTextStarterJP.AutoSize = true;
            this.lblTextStarterJP.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblTextStarterJP.Location = new System.Drawing.Point(12, 33);
            this.lblTextStarterJP.Name = "lblTextStarterJP";
            this.lblTextStarterJP.Size = new System.Drawing.Size(36, 13);
            this.lblTextStarterJP.TabIndex = 17;
            this.lblTextStarterJP.Text = "érték";
            // 
            // lblTextSpentJP
            // 
            this.lblTextSpentJP.AutoSize = true;
            this.lblTextSpentJP.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblTextSpentJP.Location = new System.Drawing.Point(83, 32);
            this.lblTextSpentJP.Name = "lblTextSpentJP";
            this.lblTextSpentJP.Size = new System.Drawing.Size(36, 13);
            this.lblTextSpentJP.TabIndex = 13;
            this.lblTextSpentJP.Text = "érték";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(7, 55);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(58, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "Gyűjtött JP";
            // 
            // lblTextCollectedJP
            // 
            this.lblTextCollectedJP.AutoSize = true;
            this.lblTextCollectedJP.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblTextCollectedJP.Location = new System.Drawing.Point(12, 75);
            this.lblTextCollectedJP.Name = "lblTextCollectedJP";
            this.lblTextCollectedJP.Size = new System.Drawing.Size(36, 13);
            this.lblTextCollectedJP.TabIndex = 15;
            this.lblTextCollectedJP.Text = "érték";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(77, 55);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Elérhető JP";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(7, 12);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(52, 13);
            this.label7.TabIndex = 16;
            this.label7.Text = "Kezdő JP";
            // 
            // lblTextAvailableJP
            // 
            this.lblTextAvailableJP.AutoSize = true;
            this.lblTextAvailableJP.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblTextAvailableJP.Location = new System.Drawing.Point(82, 76);
            this.lblTextAvailableJP.Name = "lblTextAvailableJP";
            this.lblTextAvailableJP.Size = new System.Drawing.Size(36, 13);
            this.lblTextAvailableJP.TabIndex = 13;
            this.lblTextAvailableJP.Text = "érték";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(78, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Elköltött JP";
            // 
            // lblTextSumJP
            // 
            this.lblTextSumJP.AutoSize = true;
            this.lblTextSumJP.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblTextSumJP.Location = new System.Drawing.Point(12, 120);
            this.lblTextSumJP.Name = "lblTextSumJP";
            this.lblTextSumJP.Size = new System.Drawing.Size(36, 13);
            this.lblTextSumJP.TabIndex = 13;
            this.lblTextSumJP.Text = "érték";
            // 
            // txtBNewJP
            // 
            this.txtBNewJP.Location = new System.Drawing.Point(14, 22);
            this.txtBNewJP.Name = "txtBNewJP";
            this.txtBNewJP.Size = new System.Drawing.Size(82, 20);
            this.txtBNewJP.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 6);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(32, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Új JP";
            // 
            // dtgrvJPGainList
            // 
            this.dtgrvJPGainList.AllowUserToAddRows = false;
            this.dtgrvJPGainList.AllowUserToDeleteRows = false;
            this.dtgrvJPGainList.AllowUserToResizeRows = false;
            this.dtgrvJPGainList.BackgroundColor = System.Drawing.Color.White;
            this.dtgrvJPGainList.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dtgrvJPGainList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgrvJPGainList.Location = new System.Drawing.Point(185, 62);
            this.dtgrvJPGainList.MultiSelect = false;
            this.dtgrvJPGainList.Name = "dtgrvJPGainList";
            this.dtgrvJPGainList.RowHeadersVisible = false;
            this.dtgrvJPGainList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtgrvJPGainList.Size = new System.Drawing.Size(158, 297);
            this.dtgrvJPGainList.TabIndex = 3;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnRemoveJP);
            this.panel2.Controls.Add(this.btnNewJP);
            this.panel2.Controls.Add(this.txtBNewJP);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Location = new System.Drawing.Point(43, 222);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(110, 137);
            this.panel2.TabIndex = 12;
            // 
            // btnRemoveJP
            // 
            this.btnRemoveJP.ActiveControl = null;
            this.btnRemoveJP.Location = new System.Drawing.Point(16, 93);
            this.btnRemoveJP.Name = "btnRemoveJP";
            this.btnRemoveJP.Size = new System.Drawing.Size(75, 39);
            this.btnRemoveJP.Style = MetroFramework.MetroColorStyle.Red;
            this.btnRemoveJP.TabIndex = 14;
            this.btnRemoveJP.Text = "Törlés";
            this.btnRemoveJP.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnRemoveJP.TileTextFontWeight = MetroFramework.MetroTileTextWeight.Regular;
            this.btnRemoveJP.UseSelectable = true;
            this.btnRemoveJP.Click += new System.EventHandler(this.btnRemoveJP_Click);
            // 
            // btnNewJP
            // 
            this.btnNewJP.ActiveControl = null;
            this.btnNewJP.Location = new System.Drawing.Point(16, 48);
            this.btnNewJP.Name = "btnNewJP";
            this.btnNewJP.Size = new System.Drawing.Size(75, 39);
            this.btnNewJP.Style = MetroFramework.MetroColorStyle.Orange;
            this.btnNewJP.TabIndex = 13;
            this.btnNewJP.Text = "Felvétel";
            this.btnNewJP.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnNewJP.TileTextFontWeight = MetroFramework.MetroTileTextWeight.Regular;
            this.btnNewJP.UseSelectable = true;
            this.btnNewJP.Click += new System.EventHandler(this.btnNewJP_Click);
            // 
            // ManageSideJP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(372, 387);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.dtgrvJPGainList);
            this.Controls.Add(this.panel1);
            this.Name = "ManageSideJP";
            this.Resizable = false;
            this.Text = "Szerzett JP kezelése";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ManageSideJP_FormClosed);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgrvJPGainList)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dtgrvJPGainList;
        private System.Windows.Forms.TextBox txtBNewJP;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblTextAvailableJP;
        private System.Windows.Forms.Label lblTextSpentJP;
        private System.Windows.Forms.Label lblTextSumJP;
        private MetroFramework.Controls.MetroTile btnRemoveJP;
        private MetroFramework.Controls.MetroTile btnNewJP;
        private MetroFramework.Controls.MetroTile btnBack;
        private System.Windows.Forms.Label lblTextCollectedJP;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblTextStarterJP;
        private System.Windows.Forms.Label label7;
    }
}