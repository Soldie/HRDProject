namespace HRDProject
{
    partial class FrmData_SettingAbsenSpesial
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
            this.btnSave = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.dtpPeriodeAwal = new System.Windows.Forms.DateTimePicker();
            this.dtpPeriodeAkhir = new System.Windows.Forms.DateTimePicker();
            this.lblPeriode = new System.Windows.Forms.Label();
            this.lblsd = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cboInfo = new System.Windows.Forms.ComboBox();
            this.lblInfo = new System.Windows.Forms.Label();
            this.txtKeterangan = new System.Windows.Forms.TextBox();
            this.lblKeterangan = new System.Windows.Forms.Label();
            this.txtNama = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnLoad = new System.Windows.Forms.Button();
            this.txtNIK = new System.Windows.Forms.TextBox();
            this.lblNIK = new System.Windows.Forms.Label();
            this.txtTahun = new System.Windows.Forms.TextBox();
            this.lblTahun = new System.Windows.Forms.Label();
            this.cboBulan = new System.Windows.Forms.ComboBox();
            this.lblBulan = new System.Windows.Forms.Label();
            this.cboDivisi = new System.Windows.Forms.ComboBox();
            this.lblDivisi = new System.Windows.Forms.Label();
            this.cboWarehouse = new System.Windows.Forms.ComboBox();
            this.lblWarehouse = new System.Windows.Forms.Label();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(13, 178);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 17;
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(93, 178);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 18;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // dtpPeriodeAwal
            // 
            this.dtpPeriodeAwal.CustomFormat = "dd-MMM-yyyy";
            this.dtpPeriodeAwal.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpPeriodeAwal.Location = new System.Drawing.Point(93, 108);
            this.dtpPeriodeAwal.Name = "dtpPeriodeAwal";
            this.dtpPeriodeAwal.Size = new System.Drawing.Size(110, 20);
            this.dtpPeriodeAwal.TabIndex = 12;
            this.dtpPeriodeAwal.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dtpPeriodeAwal_KeyPress);
            // 
            // dtpPeriodeAkhir
            // 
            this.dtpPeriodeAkhir.CustomFormat = "dd-MMM-yyyy";
            this.dtpPeriodeAkhir.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpPeriodeAkhir.Location = new System.Drawing.Point(241, 108);
            this.dtpPeriodeAkhir.Name = "dtpPeriodeAkhir";
            this.dtpPeriodeAkhir.Size = new System.Drawing.Size(114, 20);
            this.dtpPeriodeAkhir.TabIndex = 14;
            this.dtpPeriodeAkhir.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dtpPeriodeAkhir_KeyPress);
            // 
            // lblPeriode
            // 
            this.lblPeriode.AutoSize = true;
            this.lblPeriode.Location = new System.Drawing.Point(17, 109);
            this.lblPeriode.Name = "lblPeriode";
            this.lblPeriode.Size = new System.Drawing.Size(46, 13);
            this.lblPeriode.TabIndex = 11;
            this.lblPeriode.Text = "Tanggal";
            // 
            // lblsd
            // 
            this.lblsd.AutoSize = true;
            this.lblsd.Location = new System.Drawing.Point(212, 109);
            this.lblsd.Name = "lblsd";
            this.lblsd.Size = new System.Drawing.Size(23, 13);
            this.lblsd.TabIndex = 13;
            this.lblsd.Text = "s/d";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cboInfo);
            this.panel1.Controls.Add(this.lblInfo);
            this.panel1.Controls.Add(this.txtKeterangan);
            this.panel1.Controls.Add(this.lblKeterangan);
            this.panel1.Controls.Add(this.txtNama);
            this.panel1.Controls.Add(this.btnSearch);
            this.panel1.Controls.Add(this.btnRefresh);
            this.panel1.Controls.Add(this.btnLoad);
            this.panel1.Controls.Add(this.txtNIK);
            this.panel1.Controls.Add(this.lblNIK);
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
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(531, 214);
            this.panel1.TabIndex = 0;
            // 
            // cboInfo
            // 
            this.cboInfo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboInfo.FormattingEnabled = true;
            this.cboInfo.Location = new System.Drawing.Point(93, 140);
            this.cboInfo.Name = "cboInfo";
            this.cboInfo.Size = new System.Drawing.Size(146, 21);
            this.cboInfo.TabIndex = 16;
            this.cboInfo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cboInfo_KeyPress);
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.Location = new System.Drawing.Point(17, 143);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(25, 13);
            this.lblInfo.TabIndex = 15;
            this.lblInfo.Text = "Info";
            // 
            // txtKeterangan
            // 
            this.txtKeterangan.Location = new System.Drawing.Point(93, 73);
            this.txtKeterangan.MaxLength = 255;
            this.txtKeterangan.Name = "txtKeterangan";
            this.txtKeterangan.Size = new System.Drawing.Size(269, 20);
            this.txtKeterangan.TabIndex = 10;
            this.txtKeterangan.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtKeterangan_KeyPress);
            // 
            // lblKeterangan
            // 
            this.lblKeterangan.AutoSize = true;
            this.lblKeterangan.Location = new System.Drawing.Point(17, 76);
            this.lblKeterangan.Name = "lblKeterangan";
            this.lblKeterangan.Size = new System.Drawing.Size(62, 13);
            this.lblKeterangan.TabIndex = 9;
            this.lblKeterangan.Text = "Keterangan";
            // 
            // txtNama
            // 
            this.txtNama.Location = new System.Drawing.Point(159, 42);
            this.txtNama.MaxLength = 255;
            this.txtNama.Name = "txtNama";
            this.txtNama.Size = new System.Drawing.Size(203, 20);
            this.txtNama.TabIndex = 6;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(446, 40);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 8;
            this.btnSearch.Text = "Searc&h";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(368, 40);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(75, 23);
            this.btnLoad.TabIndex = 7;
            this.btnLoad.Text = "&Load";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // txtNIK
            // 
            this.txtNIK.Location = new System.Drawing.Point(93, 42);
            this.txtNIK.Name = "txtNIK";
            this.txtNIK.Size = new System.Drawing.Size(60, 20);
            this.txtNIK.TabIndex = 5;
            this.txtNIK.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNIK_KeyPress);
            // 
            // lblNIK
            // 
            this.lblNIK.AutoSize = true;
            this.lblNIK.Location = new System.Drawing.Point(17, 45);
            this.lblNIK.Name = "lblNIK";
            this.lblNIK.Size = new System.Drawing.Size(25, 13);
            this.lblNIK.TabIndex = 4;
            this.lblNIK.Text = "NIK";
            // 
            // txtTahun
            // 
            this.txtTahun.Location = new System.Drawing.Point(302, 12);
            this.txtTahun.Name = "txtTahun";
            this.txtTahun.Size = new System.Drawing.Size(60, 20);
            this.txtTahun.TabIndex = 3;
            this.txtTahun.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTahun_KeyPress);
            // 
            // lblTahun
            // 
            this.lblTahun.AutoSize = true;
            this.lblTahun.Location = new System.Drawing.Point(258, 15);
            this.lblTahun.Name = "lblTahun";
            this.lblTahun.Size = new System.Drawing.Size(38, 13);
            this.lblTahun.TabIndex = 2;
            this.lblTahun.Text = "Tahun";
            // 
            // cboBulan
            // 
            this.cboBulan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboBulan.FormattingEnabled = true;
            this.cboBulan.Location = new System.Drawing.Point(93, 12);
            this.cboBulan.Name = "cboBulan";
            this.cboBulan.Size = new System.Drawing.Size(146, 21);
            this.cboBulan.TabIndex = 1;
            this.cboBulan.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cboBulan_KeyPress);
            // 
            // lblBulan
            // 
            this.lblBulan.AutoSize = true;
            this.lblBulan.Location = new System.Drawing.Point(17, 12);
            this.lblBulan.Name = "lblBulan";
            this.lblBulan.Size = new System.Drawing.Size(34, 13);
            this.lblBulan.TabIndex = 0;
            this.lblBulan.Text = "Bulan";
            // 
            // cboDivisi
            // 
            this.cboDivisi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDivisi.FormattingEnabled = true;
            this.cboDivisi.Location = new System.Drawing.Point(537, 180);
            this.cboDivisi.Name = "cboDivisi";
            this.cboDivisi.Size = new System.Drawing.Size(146, 21);
            this.cboDivisi.TabIndex = 22;
            this.cboDivisi.Visible = false;
            // 
            // lblDivisi
            // 
            this.lblDivisi.AutoSize = true;
            this.lblDivisi.Location = new System.Drawing.Point(487, 182);
            this.lblDivisi.Name = "lblDivisi";
            this.lblDivisi.Size = new System.Drawing.Size(32, 13);
            this.lblDivisi.TabIndex = 21;
            this.lblDivisi.Text = "Divisi";
            this.lblDivisi.Visible = false;
            // 
            // cboWarehouse
            // 
            this.cboWarehouse.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboWarehouse.FormattingEnabled = true;
            this.cboWarehouse.Location = new System.Drawing.Point(328, 178);
            this.cboWarehouse.Name = "cboWarehouse";
            this.cboWarehouse.Size = new System.Drawing.Size(146, 21);
            this.cboWarehouse.TabIndex = 20;
            this.cboWarehouse.Visible = false;
            // 
            // lblWarehouse
            // 
            this.lblWarehouse.AutoSize = true;
            this.lblWarehouse.Location = new System.Drawing.Point(252, 181);
            this.lblWarehouse.Name = "lblWarehouse";
            this.lblWarehouse.Size = new System.Drawing.Size(44, 13);
            this.lblWarehouse.TabIndex = 19;
            this.lblWarehouse.Text = "Cabang";
            this.lblWarehouse.Visible = false;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(368, 11);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnRefresh.TabIndex = 7;
            this.btnRefresh.Text = "&Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // FrmData_SettingAbsenSpesial
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(531, 214);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FrmData_SettingAbsenSpesial";
            this.Text = "Maintenance - Setting Absensi Spesial";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.DateTimePicker dtpPeriodeAwal;
        private System.Windows.Forms.DateTimePicker dtpPeriodeAkhir;
        private System.Windows.Forms.Label lblPeriode;
        private System.Windows.Forms.Label lblsd;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox cboWarehouse;
        private System.Windows.Forms.Label lblWarehouse;
        private System.Windows.Forms.ComboBox cboDivisi;
        private System.Windows.Forms.Label lblDivisi;
        private System.Windows.Forms.TextBox txtTahun;
        private System.Windows.Forms.Label lblTahun;
        private System.Windows.Forms.ComboBox cboBulan;
        private System.Windows.Forms.Label lblBulan;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.TextBox txtNIK;
        private System.Windows.Forms.Label lblNIK;
        private System.Windows.Forms.TextBox txtNama;
        private System.Windows.Forms.TextBox txtKeterangan;
        private System.Windows.Forms.Label lblKeterangan;
        private System.Windows.Forms.ComboBox cboInfo;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.Button btnRefresh;
    }
}