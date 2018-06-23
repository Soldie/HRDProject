namespace HRDProject
{
    partial class FrmData_SwitchOff
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnSearch2 = new System.Windows.Forms.Button();
            this.btnSearch1 = new System.Windows.Forms.Button();
            this.btnLoad2 = new System.Windows.Forms.Button();
            this.btnLoad1 = new System.Windows.Forms.Button();
            this.txtNIK2 = new System.Windows.Forms.TextBox();
            this.txtNIK = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lblNIK = new System.Windows.Forms.Label();
            this.cboDivisi = new System.Windows.Forms.ComboBox();
            this.lblDivisi = new System.Windows.Forms.Label();
            this.cboWarehouse = new System.Windows.Forms.ComboBox();
            this.lblWarehouse = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblNama = new System.Windows.Forms.Label();
            this.txtNama = new System.Windows.Forms.TextBox();
            this.txtNama2 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(10, 151);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(90, 151);
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
            this.dtpPeriodeAwal.Location = new System.Drawing.Point(486, 12);
            this.dtpPeriodeAwal.Name = "dtpPeriodeAwal";
            this.dtpPeriodeAwal.Size = new System.Drawing.Size(114, 20);
            this.dtpPeriodeAwal.TabIndex = 2;
            // 
            // dtpPeriodeAkhir
            // 
            this.dtpPeriodeAkhir.CustomFormat = "dd-MMM-yyyy";
            this.dtpPeriodeAkhir.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpPeriodeAkhir.Location = new System.Drawing.Point(486, 101);
            this.dtpPeriodeAkhir.Name = "dtpPeriodeAkhir";
            this.dtpPeriodeAkhir.Size = new System.Drawing.Size(114, 20);
            this.dtpPeriodeAkhir.TabIndex = 2;
            // 
            // lblPeriode
            // 
            this.lblPeriode.AutoSize = true;
            this.lblPeriode.Location = new System.Drawing.Point(363, 16);
            this.lblPeriode.Name = "lblPeriode";
            this.lblPeriode.Size = new System.Drawing.Size(117, 13);
            this.lblPeriode.TabIndex = 3;
            this.lblPeriode.Text = "Tidak masuk di tanggal";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.lblNama);
            this.panel1.Controls.Add(this.txtNama2);
            this.panel1.Controls.Add(this.txtNama);
            this.panel1.Controls.Add(this.btnSearch2);
            this.panel1.Controls.Add(this.btnSearch1);
            this.panel1.Controls.Add(this.btnLoad2);
            this.panel1.Controls.Add(this.btnLoad1);
            this.panel1.Controls.Add(this.txtNIK2);
            this.panel1.Controls.Add(this.txtNIK);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.lblNIK);
            this.panel1.Controls.Add(this.cboDivisi);
            this.panel1.Controls.Add(this.lblDivisi);
            this.panel1.Controls.Add(this.cboWarehouse);
            this.panel1.Controls.Add(this.lblWarehouse);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.lblPeriode);
            this.panel1.Controls.Add(this.dtpPeriodeAkhir);
            this.panel1.Controls.Add(this.dtpPeriodeAwal);
            this.panel1.Controls.Add(this.btnClose);
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(630, 187);
            this.panel1.TabIndex = 2;
            // 
            // btnSearch2
            // 
            this.btnSearch2.Location = new System.Drawing.Point(274, 96);
            this.btnSearch2.Name = "btnSearch2";
            this.btnSearch2.Size = new System.Drawing.Size(75, 23);
            this.btnSearch2.TabIndex = 17;
            this.btnSearch2.Text = "Searc&h";
            this.btnSearch2.UseVisualStyleBackColor = true;
            this.btnSearch2.Click += new System.EventHandler(this.btnSearch2_Click);
            // 
            // btnSearch1
            // 
            this.btnSearch1.Location = new System.Drawing.Point(274, 9);
            this.btnSearch1.Name = "btnSearch1";
            this.btnSearch1.Size = new System.Drawing.Size(75, 23);
            this.btnSearch1.TabIndex = 17;
            this.btnSearch1.Text = "Searc&h";
            this.btnSearch1.UseVisualStyleBackColor = true;
            this.btnSearch1.Click += new System.EventHandler(this.btnSearch1_Click);
            // 
            // btnLoad2
            // 
            this.btnLoad2.Location = new System.Drawing.Point(193, 96);
            this.btnLoad2.Name = "btnLoad2";
            this.btnLoad2.Size = new System.Drawing.Size(75, 23);
            this.btnLoad2.TabIndex = 16;
            this.btnLoad2.Text = "&Load";
            this.btnLoad2.UseVisualStyleBackColor = true;
            // 
            // btnLoad1
            // 
            this.btnLoad1.Location = new System.Drawing.Point(193, 9);
            this.btnLoad1.Name = "btnLoad1";
            this.btnLoad1.Size = new System.Drawing.Size(75, 23);
            this.btnLoad1.TabIndex = 16;
            this.btnLoad1.Text = "&Load";
            this.btnLoad1.UseVisualStyleBackColor = true;
            this.btnLoad1.Click += new System.EventHandler(this.btnLoad1_Click);
            // 
            // txtNIK2
            // 
            this.txtNIK2.Location = new System.Drawing.Point(90, 98);
            this.txtNIK2.Name = "txtNIK2";
            this.txtNIK2.Size = new System.Drawing.Size(100, 20);
            this.txtNIK2.TabIndex = 15;
            this.txtNIK2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNIK2_KeyPress);
            this.txtNIK2.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtNIK2_KeyUp);
            // 
            // txtNIK
            // 
            this.txtNIK.Location = new System.Drawing.Point(90, 11);
            this.txtNIK.Name = "txtNIK";
            this.txtNIK.Size = new System.Drawing.Size(100, 20);
            this.txtNIK.TabIndex = 15;
            this.txtNIK.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNIK_KeyPress);
            this.txtNIK.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtNIK_KeyUp);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 101);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(25, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "NIK";
            // 
            // lblNIK
            // 
            this.lblNIK.AutoSize = true;
            this.lblNIK.Location = new System.Drawing.Point(15, 14);
            this.lblNIK.Name = "lblNIK";
            this.lblNIK.Size = new System.Drawing.Size(25, 13);
            this.lblNIK.TabIndex = 14;
            this.lblNIK.Text = "NIK";
            // 
            // cboDivisi
            // 
            this.cboDivisi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDivisi.FormattingEnabled = true;
            this.cboDivisi.Location = new System.Drawing.Point(300, 196);
            this.cboDivisi.Name = "cboDivisi";
            this.cboDivisi.Size = new System.Drawing.Size(146, 21);
            this.cboDivisi.TabIndex = 8;
            this.cboDivisi.Visible = false;
            // 
            // lblDivisi
            // 
            this.lblDivisi.AutoSize = true;
            this.lblDivisi.Location = new System.Drawing.Point(250, 198);
            this.lblDivisi.Name = "lblDivisi";
            this.lblDivisi.Size = new System.Drawing.Size(32, 13);
            this.lblDivisi.TabIndex = 7;
            this.lblDivisi.Text = "Divisi";
            this.lblDivisi.Visible = false;
            // 
            // cboWarehouse
            // 
            this.cboWarehouse.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboWarehouse.FormattingEnabled = true;
            this.cboWarehouse.Location = new System.Drawing.Point(91, 194);
            this.cboWarehouse.Name = "cboWarehouse";
            this.cboWarehouse.Size = new System.Drawing.Size(146, 21);
            this.cboWarehouse.TabIndex = 6;
            this.cboWarehouse.Visible = false;
            // 
            // lblWarehouse
            // 
            this.lblWarehouse.AutoSize = true;
            this.lblWarehouse.Location = new System.Drawing.Point(15, 197);
            this.lblWarehouse.Name = "lblWarehouse";
            this.lblWarehouse.Size = new System.Drawing.Size(44, 13);
            this.lblWarehouse.TabIndex = 5;
            this.lblWarehouse.Text = "Cabang";
            this.lblWarehouse.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 75);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Switch Off dengan";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(363, 103);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "di tanggal";
            // 
            // lblNama
            // 
            this.lblNama.AutoSize = true;
            this.lblNama.Location = new System.Drawing.Point(15, 42);
            this.lblNama.Name = "lblNama";
            this.lblNama.Size = new System.Drawing.Size(35, 13);
            this.lblNama.TabIndex = 18;
            this.lblNama.Text = "Nama";
            // 
            // txtNama
            // 
            this.txtNama.Location = new System.Drawing.Point(90, 42);
            this.txtNama.MaxLength = 255;
            this.txtNama.Name = "txtNama";
            this.txtNama.Size = new System.Drawing.Size(185, 20);
            this.txtNama.TabIndex = 19;
            // 
            // txtNama2
            // 
            this.txtNama2.Location = new System.Drawing.Point(90, 125);
            this.txtNama2.MaxLength = 255;
            this.txtNama2.Name = "txtNama2";
            this.txtNama2.Size = new System.Drawing.Size(185, 20);
            this.txtNama2.TabIndex = 19;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 125);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 13);
            this.label4.TabIndex = 18;
            this.label4.Text = "Nama";
            // 
            // FrmData_SwitchOff
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(630, 187);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FrmData_SwitchOff";
            this.Text = "Maintenance - Switch Off";
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
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox cboWarehouse;
        private System.Windows.Forms.Label lblWarehouse;
        private System.Windows.Forms.ComboBox cboDivisi;
        private System.Windows.Forms.Label lblDivisi;
        private System.Windows.Forms.Button btnSearch1;
        private System.Windows.Forms.Button btnLoad1;
        private System.Windows.Forms.TextBox txtNIK;
        private System.Windows.Forms.Label lblNIK;
        private System.Windows.Forms.Button btnSearch2;
        private System.Windows.Forms.Button btnLoad2;
        private System.Windows.Forms.TextBox txtNIK2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblNama;
        private System.Windows.Forms.TextBox txtNama2;
        private System.Windows.Forms.TextBox txtNama;
    }
}