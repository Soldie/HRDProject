namespace HRDProject
{
    partial class FrmRpt_RekapUM
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
            this.panel2 = new System.Windows.Forms.Panel();
            this.dgvGrid = new System.Windows.Forms.DataGridView();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.dtpPeriodeAwal = new System.Windows.Forms.DateTimePicker();
            this.dtpPeriodeAkhir = new System.Windows.Forms.DateTimePicker();
            this.lblPeriode = new System.Windows.Forms.Label();
            this.lblsd = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.chkUseColor = new System.Windows.Forms.CheckBox();
            this.chkViewNickName = new System.Windows.Forms.CheckBox();
            this.cboDivisi = new System.Windows.Forms.ComboBox();
            this.lblDivisi = new System.Windows.Forms.Label();
            this.cboWarehouse = new System.Windows.Forms.ComboBox();
            this.lblWarehouse = new System.Windows.Forms.Label();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnFormatGrid = new System.Windows.Forms.Button();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGrid)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dgvGrid);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 115);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(695, 308);
            this.panel2.TabIndex = 3;
            // 
            // dgvGrid
            // 
            this.dgvGrid.AllowUserToOrderColumns = true;
            this.dgvGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvGrid.Location = new System.Drawing.Point(0, 0);
            this.dgvGrid.Name = "dgvGrid";
            this.dgvGrid.Size = new System.Drawing.Size(695, 308);
            this.dgvGrid.TabIndex = 0;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(21, 77);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnRefresh.TabIndex = 0;
            this.btnRefresh.Text = "&Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(276, 77);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // dtpPeriodeAwal
            // 
            this.dtpPeriodeAwal.CustomFormat = "dd-MMM-yyyy";
            this.dtpPeriodeAwal.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpPeriodeAwal.Location = new System.Drawing.Point(67, 44);
            this.dtpPeriodeAwal.Name = "dtpPeriodeAwal";
            this.dtpPeriodeAwal.Size = new System.Drawing.Size(110, 20);
            this.dtpPeriodeAwal.TabIndex = 2;
            // 
            // dtpPeriodeAkhir
            // 
            this.dtpPeriodeAkhir.CustomFormat = "dd-MMM-yyyy";
            this.dtpPeriodeAkhir.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpPeriodeAkhir.Location = new System.Drawing.Point(207, 44);
            this.dtpPeriodeAkhir.Name = "dtpPeriodeAkhir";
            this.dtpPeriodeAkhir.Size = new System.Drawing.Size(114, 20);
            this.dtpPeriodeAkhir.TabIndex = 2;
            // 
            // lblPeriode
            // 
            this.lblPeriode.AutoSize = true;
            this.lblPeriode.Location = new System.Drawing.Point(18, 48);
            this.lblPeriode.Name = "lblPeriode";
            this.lblPeriode.Size = new System.Drawing.Size(43, 13);
            this.lblPeriode.TabIndex = 3;
            this.lblPeriode.Text = "Periode";
            // 
            // lblsd
            // 
            this.lblsd.AutoSize = true;
            this.lblsd.Location = new System.Drawing.Point(180, 48);
            this.lblsd.Name = "lblsd";
            this.lblsd.Size = new System.Drawing.Size(23, 13);
            this.lblsd.TabIndex = 3;
            this.lblsd.Text = "s/d";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.chkUseColor);
            this.panel1.Controls.Add(this.chkViewNickName);
            this.panel1.Controls.Add(this.cboDivisi);
            this.panel1.Controls.Add(this.lblDivisi);
            this.panel1.Controls.Add(this.cboWarehouse);
            this.panel1.Controls.Add(this.lblWarehouse);
            this.panel1.Controls.Add(this.lblsd);
            this.panel1.Controls.Add(this.lblPeriode);
            this.panel1.Controls.Add(this.dtpPeriodeAkhir);
            this.panel1.Controls.Add(this.dtpPeriodeAwal);
            this.panel1.Controls.Add(this.btnClose);
            this.panel1.Controls.Add(this.btnExport);
            this.panel1.Controls.Add(this.btnFormatGrid);
            this.panel1.Controls.Add(this.btnRefresh);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(695, 115);
            this.panel1.TabIndex = 2;
            // 
            // chkUseColor
            // 
            this.chkUseColor.AutoSize = true;
            this.chkUseColor.Checked = true;
            this.chkUseColor.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkUseColor.Location = new System.Drawing.Point(536, 16);
            this.chkUseColor.Name = "chkUseColor";
            this.chkUseColor.Size = new System.Drawing.Size(78, 17);
            this.chkUseColor.TabIndex = 22;
            this.chkUseColor.Text = "Use Colour";
            this.chkUseColor.UseVisualStyleBackColor = true;
            // 
            // chkViewNickName
            // 
            this.chkViewNickName.AutoSize = true;
            this.chkViewNickName.Checked = true;
            this.chkViewNickName.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkViewNickName.Location = new System.Drawing.Point(428, 16);
            this.chkViewNickName.Name = "chkViewNickName";
            this.chkViewNickName.Size = new System.Drawing.Size(102, 17);
            this.chkViewNickName.TabIndex = 22;
            this.chkViewNickName.Text = "View NickName";
            this.chkViewNickName.UseVisualStyleBackColor = true;
            // 
            // cboDivisi
            // 
            this.cboDivisi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDivisi.FormattingEnabled = true;
            this.cboDivisi.Location = new System.Drawing.Point(276, 14);
            this.cboDivisi.Name = "cboDivisi";
            this.cboDivisi.Size = new System.Drawing.Size(146, 21);
            this.cboDivisi.TabIndex = 8;
            // 
            // lblDivisi
            // 
            this.lblDivisi.AutoSize = true;
            this.lblDivisi.Location = new System.Drawing.Point(226, 16);
            this.lblDivisi.Name = "lblDivisi";
            this.lblDivisi.Size = new System.Drawing.Size(32, 13);
            this.lblDivisi.TabIndex = 7;
            this.lblDivisi.Text = "Divisi";
            // 
            // cboWarehouse
            // 
            this.cboWarehouse.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboWarehouse.FormattingEnabled = true;
            this.cboWarehouse.Location = new System.Drawing.Point(67, 12);
            this.cboWarehouse.Name = "cboWarehouse";
            this.cboWarehouse.Size = new System.Drawing.Size(146, 21);
            this.cboWarehouse.TabIndex = 6;
            // 
            // lblWarehouse
            // 
            this.lblWarehouse.AutoSize = true;
            this.lblWarehouse.Location = new System.Drawing.Point(17, 14);
            this.lblWarehouse.Name = "lblWarehouse";
            this.lblWarehouse.Size = new System.Drawing.Size(44, 13);
            this.lblWarehouse.TabIndex = 5;
            this.lblWarehouse.Text = "Cabang";
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(195, 77);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(75, 23);
            this.btnExport.TabIndex = 0;
            this.btnExport.Text = "Export Excel";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnFormatGrid
            // 
            this.btnFormatGrid.Location = new System.Drawing.Point(102, 77);
            this.btnFormatGrid.Name = "btnFormatGrid";
            this.btnFormatGrid.Size = new System.Drawing.Size(87, 23);
            this.btnFormatGrid.TabIndex = 0;
            this.btnFormatGrid.Text = "Reformat Grid";
            this.btnFormatGrid.UseVisualStyleBackColor = true;
            this.btnFormatGrid.Click += new System.EventHandler(this.btnFormatGrid_Click);
            // 
            // FrmRpt_RekapUM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(695, 423);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "FrmRpt_RekapUM";
            this.Text = "Aurelia Group - Rekap Absensi Karyawan";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvGrid)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.DateTimePicker dtpPeriodeAwal;
        private System.Windows.Forms.DateTimePicker dtpPeriodeAkhir;
        private System.Windows.Forms.Label lblPeriode;
        private System.Windows.Forms.Label lblsd;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dgvGrid;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Button btnFormatGrid;
        private System.Windows.Forms.ComboBox cboWarehouse;
        private System.Windows.Forms.Label lblWarehouse;
        private System.Windows.Forms.ComboBox cboDivisi;
        private System.Windows.Forms.Label lblDivisi;
        private System.Windows.Forms.CheckBox chkViewNickName;
        private System.Windows.Forms.CheckBox chkUseColor;
    }
}