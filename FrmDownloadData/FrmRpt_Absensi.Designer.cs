using HRDProject.Datasets;

namespace HRDProject
{
    partial class FrmRpt_Absensi
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
            this.dsRptJamAbsensi = new HRDProject.Datasets.dsRptJamAbsensi();
            this.dsRptJamAbsensi2 = new HRDProject.Datasets.dsRptJamAbsensi2();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cboDivisi = new System.Windows.Forms.ComboBox();
            this.lblDivisi = new System.Windows.Forms.Label();
            this.cboWarehouse = new System.Windows.Forms.ComboBox();
            this.lblWarehouse = new System.Windows.Forms.Label();
            this.chkWithJamMakan = new System.Windows.Forms.CheckBox();
            this.lblsd = new System.Windows.Forms.Label();
            this.lblPeriode = new System.Windows.Forms.Label();
            this.dtpPeriodeAkhir = new System.Windows.Forms.DateTimePicker();
            this.dtpPeriodeAwal = new System.Windows.Forms.DateTimePicker();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.chkViewNickName = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dsRptJamAbsensi)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsRptJamAbsensi2)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dsRptJamAbsensi
            // 
            this.dsRptJamAbsensi.DataSetName = "dsRptJamAbsensi";
            this.dsRptJamAbsensi.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // dsRptJamAbsensi2
            // 
            this.dsRptJamAbsensi2.DataSetName = "dsRptJamAbsensi2";
            this.dsRptJamAbsensi2.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.chkViewNickName);
            this.panel1.Controls.Add(this.cboDivisi);
            this.panel1.Controls.Add(this.lblDivisi);
            this.panel1.Controls.Add(this.cboWarehouse);
            this.panel1.Controls.Add(this.lblWarehouse);
            this.panel1.Controls.Add(this.chkWithJamMakan);
            this.panel1.Controls.Add(this.lblsd);
            this.panel1.Controls.Add(this.lblPeriode);
            this.panel1.Controls.Add(this.dtpPeriodeAkhir);
            this.panel1.Controls.Add(this.dtpPeriodeAwal);
            this.panel1.Controls.Add(this.btnClose);
            this.panel1.Controls.Add(this.btnRefresh);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(747, 110);
            this.panel1.TabIndex = 0;
            // 
            // cboDivisi
            // 
            this.cboDivisi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDivisi.FormattingEnabled = true;
            this.cboDivisi.Location = new System.Drawing.Point(285, 17);
            this.cboDivisi.Name = "cboDivisi";
            this.cboDivisi.Size = new System.Drawing.Size(146, 21);
            this.cboDivisi.TabIndex = 10;
            // 
            // lblDivisi
            // 
            this.lblDivisi.AutoSize = true;
            this.lblDivisi.Location = new System.Drawing.Point(236, 21);
            this.lblDivisi.Name = "lblDivisi";
            this.lblDivisi.Size = new System.Drawing.Size(32, 13);
            this.lblDivisi.TabIndex = 9;
            this.lblDivisi.Text = "Divisi";
            // 
            // cboWarehouse
            // 
            this.cboWarehouse.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboWarehouse.FormattingEnabled = true;
            this.cboWarehouse.Location = new System.Drawing.Point(72, 18);
            this.cboWarehouse.Name = "cboWarehouse";
            this.cboWarehouse.Size = new System.Drawing.Size(146, 21);
            this.cboWarehouse.TabIndex = 6;
            // 
            // lblWarehouse
            // 
            this.lblWarehouse.AutoSize = true;
            this.lblWarehouse.Location = new System.Drawing.Point(22, 20);
            this.lblWarehouse.Name = "lblWarehouse";
            this.lblWarehouse.Size = new System.Drawing.Size(44, 13);
            this.lblWarehouse.TabIndex = 5;
            this.lblWarehouse.Text = "Cabang";
            // 
            // chkWithJamMakan
            // 
            this.chkWithJamMakan.AutoSize = true;
            this.chkWithJamMakan.Location = new System.Drawing.Point(331, 48);
            this.chkWithJamMakan.Name = "chkWithJamMakan";
            this.chkWithJamMakan.Size = new System.Drawing.Size(118, 17);
            this.chkWithJamMakan.TabIndex = 4;
            this.chkWithJamMakan.Text = "Include Break Time";
            this.chkWithJamMakan.UseVisualStyleBackColor = true;
            // 
            // lblsd
            // 
            this.lblsd.AutoSize = true;
            this.lblsd.Location = new System.Drawing.Point(184, 49);
            this.lblsd.Name = "lblsd";
            this.lblsd.Size = new System.Drawing.Size(23, 13);
            this.lblsd.TabIndex = 3;
            this.lblsd.Text = "s/d";
            // 
            // lblPeriode
            // 
            this.lblPeriode.AutoSize = true;
            this.lblPeriode.Location = new System.Drawing.Point(22, 49);
            this.lblPeriode.Name = "lblPeriode";
            this.lblPeriode.Size = new System.Drawing.Size(43, 13);
            this.lblPeriode.TabIndex = 3;
            this.lblPeriode.Text = "Periode";
            // 
            // dtpPeriodeAkhir
            // 
            this.dtpPeriodeAkhir.CustomFormat = "dd-MMM-yyyy";
            this.dtpPeriodeAkhir.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpPeriodeAkhir.Location = new System.Drawing.Point(211, 45);
            this.dtpPeriodeAkhir.Name = "dtpPeriodeAkhir";
            this.dtpPeriodeAkhir.Size = new System.Drawing.Size(114, 20);
            this.dtpPeriodeAkhir.TabIndex = 2;
            // 
            // dtpPeriodeAwal
            // 
            this.dtpPeriodeAwal.CustomFormat = "dd-MMM-yyyy";
            this.dtpPeriodeAwal.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpPeriodeAwal.Location = new System.Drawing.Point(71, 45);
            this.dtpPeriodeAwal.Name = "dtpPeriodeAwal";
            this.dtpPeriodeAwal.Size = new System.Drawing.Size(110, 20);
            this.dtpPeriodeAwal.TabIndex = 2;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(106, 78);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(25, 78);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnRefresh.TabIndex = 0;
            this.btnRefresh.Text = "&Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.reportViewer1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 110);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(747, 318);
            this.panel2.TabIndex = 1;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(747, 318);
            this.reportViewer1.TabIndex = 0;
            // 
            // chkViewNickName
            // 
            this.chkViewNickName.AutoSize = true;
            this.chkViewNickName.Checked = true;
            this.chkViewNickName.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkViewNickName.Location = new System.Drawing.Point(450, 18);
            this.chkViewNickName.Name = "chkViewNickName";
            this.chkViewNickName.Size = new System.Drawing.Size(102, 17);
            this.chkViewNickName.TabIndex = 23;
            this.chkViewNickName.Text = "View NickName";
            this.chkViewNickName.UseVisualStyleBackColor = true;
            // 
            // FrmRpt_Absensi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(747, 428);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "FrmRpt_Absensi";
            this.Text = "Aurelia Group - Jam Absensi Karyawan";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.dsRptJamAbsensi)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsRptJamAbsensi2)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblsd;
        private System.Windows.Forms.Label lblPeriode;
        private System.Windows.Forms.DateTimePicker dtpPeriodeAkhir;
        private System.Windows.Forms.DateTimePicker dtpPeriodeAwal;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Panel panel2;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;

        private dsRptJamAbsensi2 dsRptJamAbsensi2;
        private dsRptJamAbsensi dsRptJamAbsensi;
        private System.Windows.Forms.CheckBox chkWithJamMakan;
        private System.Windows.Forms.ComboBox cboWarehouse;
        private System.Windows.Forms.Label lblWarehouse;
        private System.Windows.Forms.ComboBox cboDivisi;
        private System.Windows.Forms.Label lblDivisi;
        private System.Windows.Forms.CheckBox chkViewNickName;

    }
}