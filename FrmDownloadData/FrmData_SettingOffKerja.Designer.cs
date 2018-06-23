namespace HRDProject
{
    partial class FrmData_SettingOffKerja
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
            this._pilih = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this._NIK = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._Nama = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._NickName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.dtpPeriodeAwal = new System.Windows.Forms.DateTimePicker();
            this.dtpPeriodeAkhir = new System.Windows.Forms.DateTimePicker();
            this.lblPeriode = new System.Windows.Forms.Label();
            this.lblsd = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkHari6 = new System.Windows.Forms.CheckBox();
            this.chkHari5 = new System.Windows.Forms.CheckBox();
            this.chkHari4 = new System.Windows.Forms.CheckBox();
            this.chkHari3 = new System.Windows.Forms.CheckBox();
            this.chkHari2 = new System.Windows.Forms.CheckBox();
            this.chkHari1 = new System.Windows.Forms.CheckBox();
            this.chkHari0 = new System.Windows.Forms.CheckBox();
            this.btnLoad = new System.Windows.Forms.Button();
            this.cboDivisi = new System.Windows.Forms.ComboBox();
            this.lblDivisi = new System.Windows.Forms.Label();
            this.cboWarehouse = new System.Windows.Forms.ComboBox();
            this.lblWarehouse = new System.Windows.Forms.Label();
            this.btnGetPeriode = new System.Windows.Forms.Button();
            this.txtTahun = new System.Windows.Forms.TextBox();
            this.lblTahun = new System.Windows.Forms.Label();
            this.cboBulan = new System.Windows.Forms.ComboBox();
            this.lblBulan = new System.Windows.Forms.Label();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGrid)).BeginInit();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dgvGrid);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 252);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(695, 171);
            this.panel2.TabIndex = 3;
            // 
            // dgvGrid
            // 
            this.dgvGrid.AllowUserToAddRows = false;
            this.dgvGrid.AllowUserToOrderColumns = true;
            this.dgvGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this._pilih,
            this._NIK,
            this._Nama,
            this._NickName});
            this.dgvGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvGrid.Location = new System.Drawing.Point(0, 0);
            this.dgvGrid.Name = "dgvGrid";
            this.dgvGrid.Size = new System.Drawing.Size(695, 171);
            this.dgvGrid.TabIndex = 0;
            // 
            // _pilih
            // 
            this._pilih.HeaderText = "Pilih";
            this._pilih.Name = "_pilih";
            this._pilih.Width = 50;
            // 
            // _NIK
            // 
            this._NIK.HeaderText = "NIK";
            this._NIK.Name = "_NIK";
            // 
            // _Nama
            // 
            this._Nama.HeaderText = "Nama";
            this._Nama.Name = "_Nama";
            // 
            // _NickName
            // 
            this._NickName.HeaderText = "Panggilan";
            this._NickName.Name = "_NickName";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(21, 214);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(101, 214);
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
            this.dtpPeriodeAwal.Location = new System.Drawing.Point(93, 119);
            this.dtpPeriodeAwal.Name = "dtpPeriodeAwal";
            this.dtpPeriodeAwal.Size = new System.Drawing.Size(110, 20);
            this.dtpPeriodeAwal.TabIndex = 2;
            // 
            // dtpPeriodeAkhir
            // 
            this.dtpPeriodeAkhir.CustomFormat = "dd-MMM-yyyy";
            this.dtpPeriodeAkhir.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpPeriodeAkhir.Location = new System.Drawing.Point(238, 120);
            this.dtpPeriodeAkhir.Name = "dtpPeriodeAkhir";
            this.dtpPeriodeAkhir.Size = new System.Drawing.Size(114, 20);
            this.dtpPeriodeAkhir.TabIndex = 2;
            // 
            // lblPeriode
            // 
            this.lblPeriode.AutoSize = true;
            this.lblPeriode.Location = new System.Drawing.Point(15, 121);
            this.lblPeriode.Name = "lblPeriode";
            this.lblPeriode.Size = new System.Drawing.Size(66, 13);
            this.lblPeriode.TabIndex = 3;
            this.lblPeriode.Text = "Periode OFF";
            // 
            // lblsd
            // 
            this.lblsd.AutoSize = true;
            this.lblsd.Location = new System.Drawing.Point(209, 121);
            this.lblsd.Name = "lblsd";
            this.lblsd.Size = new System.Drawing.Size(23, 13);
            this.lblsd.TabIndex = 3;
            this.lblsd.Text = "s/d";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnGetPeriode);
            this.panel1.Controls.Add(this.txtTahun);
            this.panel1.Controls.Add(this.lblTahun);
            this.panel1.Controls.Add(this.cboBulan);
            this.panel1.Controls.Add(this.lblBulan);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.btnLoad);
            this.panel1.Controls.Add(this.cboDivisi);
            this.panel1.Controls.Add(this.lblDivisi);
            this.panel1.Controls.Add(this.cboWarehouse);
            this.panel1.Controls.Add(this.lblWarehouse);
            this.panel1.Controls.Add(this.lblsd);
            this.panel1.Controls.Add(this.lblPeriode);
            this.panel1.Controls.Add(this.dtpPeriodeAkhir);
            this.panel1.Controls.Add(this.dtpPeriodeAwal);
            this.panel1.Controls.Add(this.btnClose);
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(695, 252);
            this.panel1.TabIndex = 2;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkHari6);
            this.groupBox1.Controls.Add(this.chkHari5);
            this.groupBox1.Controls.Add(this.chkHari4);
            this.groupBox1.Controls.Add(this.chkHari3);
            this.groupBox1.Controls.Add(this.chkHari2);
            this.groupBox1.Controls.Add(this.chkHari1);
            this.groupBox1.Controls.Add(this.chkHari0);
            this.groupBox1.Location = new System.Drawing.Point(20, 148);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(529, 60);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Pilih Off Hari ...";
            // 
            // chkHari6
            // 
            this.chkHari6.AutoSize = true;
            this.chkHari6.Location = new System.Drawing.Point(442, 29);
            this.chkHari6.Name = "chkHari6";
            this.chkHari6.Size = new System.Drawing.Size(61, 17);
            this.chkHari6.TabIndex = 13;
            this.chkHari6.Text = "Minggu";
            this.chkHari6.UseVisualStyleBackColor = true;
            // 
            // chkHari5
            // 
            this.chkHari5.AutoSize = true;
            this.chkHari5.Location = new System.Drawing.Point(372, 29);
            this.chkHari5.Name = "chkHari5";
            this.chkHari5.Size = new System.Drawing.Size(54, 17);
            this.chkHari5.TabIndex = 12;
            this.chkHari5.Text = "Sabtu";
            this.chkHari5.UseVisualStyleBackColor = true;
            // 
            // chkHari4
            // 
            this.chkHari4.AutoSize = true;
            this.chkHari4.Location = new System.Drawing.Point(297, 29);
            this.chkHari4.Name = "chkHari4";
            this.chkHari4.Size = new System.Drawing.Size(54, 17);
            this.chkHari4.TabIndex = 11;
            this.chkHari4.Text = "Jumat";
            this.chkHari4.UseVisualStyleBackColor = true;
            // 
            // chkHari3
            // 
            this.chkHari3.AutoSize = true;
            this.chkHari3.Location = new System.Drawing.Point(226, 29);
            this.chkHari3.Name = "chkHari3";
            this.chkHari3.Size = new System.Drawing.Size(54, 17);
            this.chkHari3.TabIndex = 10;
            this.chkHari3.Text = "Kamis";
            this.chkHari3.UseVisualStyleBackColor = true;
            // 
            // chkHari2
            // 
            this.chkHari2.AutoSize = true;
            this.chkHari2.Location = new System.Drawing.Point(158, 29);
            this.chkHari2.Name = "chkHari2";
            this.chkHari2.Size = new System.Drawing.Size(52, 17);
            this.chkHari2.TabIndex = 9;
            this.chkHari2.Text = "Rabu";
            this.chkHari2.UseVisualStyleBackColor = true;
            // 
            // chkHari1
            // 
            this.chkHari1.AutoSize = true;
            this.chkHari1.Location = new System.Drawing.Point(81, 29);
            this.chkHari1.Name = "chkHari1";
            this.chkHari1.Size = new System.Drawing.Size(58, 17);
            this.chkHari1.TabIndex = 8;
            this.chkHari1.Text = "Selasa";
            this.chkHari1.UseVisualStyleBackColor = true;
            // 
            // chkHari0
            // 
            this.chkHari0.AutoSize = true;
            this.chkHari0.Location = new System.Drawing.Point(6, 29);
            this.chkHari0.Name = "chkHari0";
            this.chkHari0.Size = new System.Drawing.Size(53, 17);
            this.chkHari0.TabIndex = 7;
            this.chkHari0.Text = "Senin";
            this.chkHari0.UseVisualStyleBackColor = true;
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(358, 119);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(75, 23);
            this.btnLoad.TabIndex = 13;
            this.btnLoad.Text = "&Load";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // cboDivisi
            // 
            this.cboDivisi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDivisi.FormattingEnabled = true;
            this.cboDivisi.Location = new System.Drawing.Point(302, 13);
            this.cboDivisi.Name = "cboDivisi";
            this.cboDivisi.Size = new System.Drawing.Size(146, 21);
            this.cboDivisi.TabIndex = 8;
            // 
            // lblDivisi
            // 
            this.lblDivisi.AutoSize = true;
            this.lblDivisi.Location = new System.Drawing.Point(252, 15);
            this.lblDivisi.Name = "lblDivisi";
            this.lblDivisi.Size = new System.Drawing.Size(32, 13);
            this.lblDivisi.TabIndex = 7;
            this.lblDivisi.Text = "Divisi";
            // 
            // cboWarehouse
            // 
            this.cboWarehouse.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboWarehouse.FormattingEnabled = true;
            this.cboWarehouse.Location = new System.Drawing.Point(93, 11);
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
            // btnGetPeriode
            // 
            this.btnGetPeriode.Location = new System.Drawing.Point(164, 78);
            this.btnGetPeriode.Name = "btnGetPeriode";
            this.btnGetPeriode.Size = new System.Drawing.Size(75, 23);
            this.btnGetPeriode.TabIndex = 22;
            this.btnGetPeriode.Text = "Get Periode";
            this.btnGetPeriode.UseVisualStyleBackColor = true;
            this.btnGetPeriode.Click += new System.EventHandler(this.btnGetPeriode_Click);
            // 
            // txtTahun
            // 
            this.txtTahun.Location = new System.Drawing.Point(93, 80);
            this.txtTahun.Name = "txtTahun";
            this.txtTahun.Size = new System.Drawing.Size(60, 20);
            this.txtTahun.TabIndex = 21;
            // 
            // lblTahun
            // 
            this.lblTahun.AutoSize = true;
            this.lblTahun.Location = new System.Drawing.Point(12, 83);
            this.lblTahun.Name = "lblTahun";
            this.lblTahun.Size = new System.Drawing.Size(38, 13);
            this.lblTahun.TabIndex = 20;
            this.lblTahun.Text = "Tahun";
            // 
            // cboBulan
            // 
            this.cboBulan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboBulan.FormattingEnabled = true;
            this.cboBulan.Location = new System.Drawing.Point(93, 48);
            this.cboBulan.Name = "cboBulan";
            this.cboBulan.Size = new System.Drawing.Size(146, 21);
            this.cboBulan.TabIndex = 19;
            // 
            // lblBulan
            // 
            this.lblBulan.AutoSize = true;
            this.lblBulan.Location = new System.Drawing.Point(17, 48);
            this.lblBulan.Name = "lblBulan";
            this.lblBulan.Size = new System.Drawing.Size(34, 13);
            this.lblBulan.TabIndex = 18;
            this.lblBulan.Text = "Bulan";
            // 
            // FrmData_SettingOffKerja
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(695, 423);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "FrmData_SettingOffKerja";
            this.Text = "Maintenance - Setting Off Kerja Karyawan";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvGrid)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.DateTimePicker dtpPeriodeAwal;
        private System.Windows.Forms.DateTimePicker dtpPeriodeAkhir;
        private System.Windows.Forms.Label lblPeriode;
        private System.Windows.Forms.Label lblsd;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dgvGrid;
        private System.Windows.Forms.ComboBox cboWarehouse;
        private System.Windows.Forms.Label lblWarehouse;
        private System.Windows.Forms.ComboBox cboDivisi;
        private System.Windows.Forms.Label lblDivisi;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chkHari6;
        private System.Windows.Forms.CheckBox chkHari5;
        private System.Windows.Forms.CheckBox chkHari4;
        private System.Windows.Forms.CheckBox chkHari3;
        private System.Windows.Forms.CheckBox chkHari2;
        private System.Windows.Forms.CheckBox chkHari1;
        private System.Windows.Forms.CheckBox chkHari0;
        private System.Windows.Forms.DataGridViewCheckBoxColumn _pilih;
        private System.Windows.Forms.DataGridViewTextBoxColumn _NIK;
        private System.Windows.Forms.DataGridViewTextBoxColumn _Nama;
        private System.Windows.Forms.DataGridViewTextBoxColumn _NickName;
        private System.Windows.Forms.Button btnGetPeriode;
        private System.Windows.Forms.TextBox txtTahun;
        private System.Windows.Forms.Label lblTahun;
        private System.Windows.Forms.ComboBox cboBulan;
        private System.Windows.Forms.Label lblBulan;
    }
}