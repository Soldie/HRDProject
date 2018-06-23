namespace HRDProject
{
    partial class FrmRpt_RekapAbsensi
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
            this.chkDeleteRecord = new System.Windows.Forms.CheckBox();
            this.chkUseColor = new System.Windows.Forms.CheckBox();
            this.chkViewNickName = new System.Windows.Forms.CheckBox();
            this.chkAnggapMasuk = new System.Windows.Forms.CheckBox();
            this.chkShowTelat = new System.Windows.Forms.CheckBox();
            this.btnPrintGaji = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnGetPeriode = new System.Windows.Forms.Button();
            this.txtTahun = new System.Windows.Forms.TextBox();
            this.lblTahun = new System.Windows.Forms.Label();
            this.cboBulan = new System.Windows.Forms.ComboBox();
            this.lblBulan = new System.Windows.Forms.Label();
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
            this.panel2.Location = new System.Drawing.Point(0, 154);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1003, 269);
            this.panel2.TabIndex = 3;
            // 
            // dgvGrid
            // 
            this.dgvGrid.AllowUserToOrderColumns = true;
            this.dgvGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvGrid.Location = new System.Drawing.Point(0, 0);
            this.dgvGrid.Name = "dgvGrid";
            this.dgvGrid.Size = new System.Drawing.Size(1003, 269);
            this.dgvGrid.TabIndex = 0;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(21, 108);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnRefresh.TabIndex = 0;
            this.btnRefresh.Text = "&Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(357, 108);
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
            this.dtpPeriodeAwal.Location = new System.Drawing.Point(93, 45);
            this.dtpPeriodeAwal.Name = "dtpPeriodeAwal";
            this.dtpPeriodeAwal.Size = new System.Drawing.Size(110, 20);
            this.dtpPeriodeAwal.TabIndex = 2;
            // 
            // dtpPeriodeAkhir
            // 
            this.dtpPeriodeAkhir.CustomFormat = "dd-MMM-yyyy";
            this.dtpPeriodeAkhir.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpPeriodeAkhir.Location = new System.Drawing.Point(233, 45);
            this.dtpPeriodeAkhir.Name = "dtpPeriodeAkhir";
            this.dtpPeriodeAkhir.Size = new System.Drawing.Size(114, 20);
            this.dtpPeriodeAkhir.TabIndex = 2;
            // 
            // lblPeriode
            // 
            this.lblPeriode.AutoSize = true;
            this.lblPeriode.Location = new System.Drawing.Point(15, 48);
            this.lblPeriode.Name = "lblPeriode";
            this.lblPeriode.Size = new System.Drawing.Size(43, 13);
            this.lblPeriode.TabIndex = 3;
            this.lblPeriode.Text = "Periode";
            // 
            // lblsd
            // 
            this.lblsd.AutoSize = true;
            this.lblsd.Location = new System.Drawing.Point(206, 49);
            this.lblsd.Name = "lblsd";
            this.lblsd.Size = new System.Drawing.Size(23, 13);
            this.lblsd.TabIndex = 3;
            this.lblsd.Text = "s/d";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.chkDeleteRecord);
            this.panel1.Controls.Add(this.chkUseColor);
            this.panel1.Controls.Add(this.chkViewNickName);
            this.panel1.Controls.Add(this.chkAnggapMasuk);
            this.panel1.Controls.Add(this.chkShowTelat);
            this.panel1.Controls.Add(this.btnPrintGaji);
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Controls.Add(this.btnGetPeriode);
            this.panel1.Controls.Add(this.txtTahun);
            this.panel1.Controls.Add(this.lblTahun);
            this.panel1.Controls.Add(this.cboBulan);
            this.panel1.Controls.Add(this.lblBulan);
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
            this.panel1.Size = new System.Drawing.Size(1003, 154);
            this.panel1.TabIndex = 2;
            // 
            // chkDeleteRecord
            // 
            this.chkDeleteRecord.AutoSize = true;
            this.chkDeleteRecord.Location = new System.Drawing.Point(897, 15);
            this.chkDeleteRecord.Name = "chkDeleteRecord";
            this.chkDeleteRecord.Size = new System.Drawing.Size(125, 17);
            this.chkDeleteRecord.TabIndex = 24;
            this.chkDeleteRecord.Text = "Hapus Data bulan ini";
            this.chkDeleteRecord.UseVisualStyleBackColor = true;
            // 
            // chkUseColor
            // 
            this.chkUseColor.AutoSize = true;
            this.chkUseColor.Checked = true;
            this.chkUseColor.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkUseColor.Location = new System.Drawing.Point(813, 16);
            this.chkUseColor.Name = "chkUseColor";
            this.chkUseColor.Size = new System.Drawing.Size(78, 17);
            this.chkUseColor.TabIndex = 23;
            this.chkUseColor.Text = "Use Colour";
            this.chkUseColor.UseVisualStyleBackColor = true;
            // 
            // chkViewNickName
            // 
            this.chkViewNickName.AutoSize = true;
            this.chkViewNickName.Checked = true;
            this.chkViewNickName.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkViewNickName.Location = new System.Drawing.Point(21, 78);
            this.chkViewNickName.Name = "chkViewNickName";
            this.chkViewNickName.Size = new System.Drawing.Size(102, 17);
            this.chkViewNickName.TabIndex = 21;
            this.chkViewNickName.Text = "View NickName";
            this.chkViewNickName.UseVisualStyleBackColor = true;
            // 
            // chkAnggapMasuk
            // 
            this.chkAnggapMasuk.AutoSize = true;
            this.chkAnggapMasuk.Location = new System.Drawing.Point(568, 15);
            this.chkAnggapMasuk.Name = "chkAnggapMasuk";
            this.chkAnggapMasuk.Size = new System.Drawing.Size(239, 17);
            this.chkAnggapMasuk.TabIndex = 20;
            this.chkAnggapMasuk.Text = "Tgl Terakhir dianggap masuk kalau absen 1x";
            this.chkAnggapMasuk.UseVisualStyleBackColor = true;
            // 
            // chkShowTelat
            // 
            this.chkShowTelat.AutoSize = true;
            this.chkShowTelat.Checked = true;
            this.chkShowTelat.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkShowTelat.Location = new System.Drawing.Point(428, 16);
            this.chkShowTelat.Name = "chkShowTelat";
            this.chkShowTelat.Size = new System.Drawing.Size(124, 17);
            this.chkShowTelat.TabIndex = 20;
            this.chkShowTelat.Text = "Tampilkan Hari Telat";
            this.chkShowTelat.UseVisualStyleBackColor = true;
            // 
            // btnPrintGaji
            // 
            this.btnPrintGaji.Location = new System.Drawing.Point(608, 108);
            this.btnPrintGaji.Name = "btnPrintGaji";
            this.btnPrintGaji.Size = new System.Drawing.Size(75, 23);
            this.btnPrintGaji.TabIndex = 19;
            this.btnPrintGaji.Text = "Print Gaji";
            this.btnPrintGaji.UseVisualStyleBackColor = true;
            this.btnPrintGaji.Click += new System.EventHandler(this.btnPrintGaji_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(276, 108);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 18;
            this.btnSave.Text = "Save to DB";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnGetPeriode
            // 
            this.btnGetPeriode.Location = new System.Drawing.Point(499, 78);
            this.btnGetPeriode.Name = "btnGetPeriode";
            this.btnGetPeriode.Size = new System.Drawing.Size(75, 23);
            this.btnGetPeriode.TabIndex = 17;
            this.btnGetPeriode.Text = "Get Periode";
            this.btnGetPeriode.UseVisualStyleBackColor = true;
            this.btnGetPeriode.Click += new System.EventHandler(this.btnGetPeriode_Click);
            // 
            // txtTahun
            // 
            this.txtTahun.Location = new System.Drawing.Point(428, 80);
            this.txtTahun.Name = "txtTahun";
            this.txtTahun.Size = new System.Drawing.Size(60, 20);
            this.txtTahun.TabIndex = 16;
            // 
            // lblTahun
            // 
            this.lblTahun.AutoSize = true;
            this.lblTahun.Location = new System.Drawing.Point(388, 83);
            this.lblTahun.Name = "lblTahun";
            this.lblTahun.Size = new System.Drawing.Size(38, 13);
            this.lblTahun.TabIndex = 15;
            this.lblTahun.Text = "Tahun";
            // 
            // cboBulan
            // 
            this.cboBulan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboBulan.FormattingEnabled = true;
            this.cboBulan.Location = new System.Drawing.Point(428, 48);
            this.cboBulan.Name = "cboBulan";
            this.cboBulan.Size = new System.Drawing.Size(146, 21);
            this.cboBulan.TabIndex = 14;
            // 
            // lblBulan
            // 
            this.lblBulan.AutoSize = true;
            this.lblBulan.Location = new System.Drawing.Point(388, 49);
            this.lblBulan.Name = "lblBulan";
            this.lblBulan.Size = new System.Drawing.Size(34, 13);
            this.lblBulan.TabIndex = 13;
            this.lblBulan.Text = "Bulan";
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
            this.btnExport.Location = new System.Drawing.Point(195, 108);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(75, 23);
            this.btnExport.TabIndex = 0;
            this.btnExport.Text = "Export Excel";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnFormatGrid
            // 
            this.btnFormatGrid.Location = new System.Drawing.Point(102, 108);
            this.btnFormatGrid.Name = "btnFormatGrid";
            this.btnFormatGrid.Size = new System.Drawing.Size(87, 23);
            this.btnFormatGrid.TabIndex = 0;
            this.btnFormatGrid.Text = "Reformat Grid";
            this.btnFormatGrid.UseVisualStyleBackColor = true;
            this.btnFormatGrid.Click += new System.EventHandler(this.btnFormatGrid_Click);
            // 
            // FrmRpt_RekapAbsensi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1003, 423);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "FrmRpt_RekapAbsensi";
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
        private System.Windows.Forms.Button btnGetPeriode;
        private System.Windows.Forms.TextBox txtTahun;
        private System.Windows.Forms.Label lblTahun;
        private System.Windows.Forms.ComboBox cboBulan;
        private System.Windows.Forms.Label lblBulan;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnPrintGaji;
        private System.Windows.Forms.CheckBox chkShowTelat;
        private System.Windows.Forms.CheckBox chkViewNickName;
        private System.Windows.Forms.CheckBox chkAnggapMasuk;
        private System.Windows.Forms.CheckBox chkUseColor;
        private System.Windows.Forms.CheckBox chkDeleteRecord;
    }
}