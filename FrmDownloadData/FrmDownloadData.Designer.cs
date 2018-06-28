namespace HRDProject
{
    partial class FrmDownloadData
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmDownloadData));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
            this.axBioBridgeSDK1 = new AxBioBridgeSDKLib.AxBioBridgeSDK();
            this._MyKode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._ProductID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._Button = new System.Windows.Forms.DataGridViewButtonColumn();
            this._ProductName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._Stok = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._Qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._HargaJual = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._HargaJualAsal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._HargaBeli = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._KdSat = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._TotalHarga = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._Disc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._KdDisc = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this._TotalDisc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvGrid = new System.Windows.Forms.DataGridView();
            this._nik = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._nama = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panSelectMethod = new System.Windows.Forms.Panel();
            this.tabAbsensiMethod = new System.Windows.Forms.TabControl();
            this.tabUSB = new System.Windows.Forms.TabPage();
            this.lblFileName = new System.Windows.Forms.Label();
            this.txtFilename = new System.Windows.Forms.TextBox();
            this.btnSelectData = new System.Windows.Forms.Button();
            this.tabIPAddress = new System.Windows.Forms.TabPage();
            this.lblState = new System.Windows.Forms.Label();
            this.btnConnect = new System.Windows.Forms.Button();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.txtDevId = new System.Windows.Forms.TextBox();
            this.txtIP = new System.Windows.Forms.TextBox();
            this.lblDevId = new System.Windows.Forms.Label();
            this.lblPort = new System.Windows.Forms.Label();
            this.lblIPAddress = new System.Windows.Forms.Label();
            this.panUserRecords = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cboDivisi = new System.Windows.Forms.ComboBox();
            this.lblDivisi = new System.Windows.Forms.Label();
            this.cboJam = new System.Windows.Forms.ComboBox();
            this.cboWarehouse = new System.Windows.Forms.ComboBox();
            this.lblJam = new System.Windows.Forms.Label();
            this.lblWarehouse = new System.Windows.Forms.Label();
            this.btnLoadUser = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnReport = new System.Windows.Forms.Button();
            this.btnPrintUM = new System.Windows.Forms.Button();
            this.btnRptRekapAbsensi = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnLoadAbsensi = new System.Windows.Forms.Button();
            this.btnImportCSV = new System.Windows.Forms.Button();
            this.chkSolutions = new System.Windows.Forms.CheckBox();
            this.btnImport = new System.Windows.Forms.Button();
            this.btnSaveAbsensi = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnRptHistoryAbsensi = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.axBioBridgeSDK1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGrid)).BeginInit();
            this.panel1.SuspendLayout();
            this.panSelectMethod.SuspendLayout();
            this.tabAbsensiMethod.SuspendLayout();
            this.tabUSB.SuspendLayout();
            this.tabIPAddress.SuspendLayout();
            this.panUserRecords.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // axBioBridgeSDK1
            // 
            this.axBioBridgeSDK1.Enabled = true;
            this.axBioBridgeSDK1.Location = new System.Drawing.Point(723, 68);
            this.axBioBridgeSDK1.Name = "axBioBridgeSDK1";
            this.axBioBridgeSDK1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axBioBridgeSDK1.OcxState")));
            this.axBioBridgeSDK1.Size = new System.Drawing.Size(167, 84);
            this.axBioBridgeSDK1.TabIndex = 26;
            this.axBioBridgeSDK1.Visible = false;
            // 
            // _MyKode
            // 
            dataGridViewCellStyle10.BackColor = System.Drawing.Color.Bisque;
            this._MyKode.DefaultCellStyle = dataGridViewCellStyle10;
            this._MyKode.Frozen = true;
            this._MyKode.HeaderText = "Code";
            this._MyKode.Name = "_MyKode";
            this._MyKode.ReadOnly = true;
            this._MyKode.Visible = false;
            // 
            // _ProductID
            // 
            this._ProductID.Frozen = true;
            this._ProductID.HeaderText = "Product ID";
            this._ProductID.MaxInputLength = 20;
            this._ProductID.Name = "_ProductID";
            this._ProductID.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this._ProductID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // _Button
            // 
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            dataGridViewCellStyle11.NullValue = "F3";
            this._Button.DefaultCellStyle = dataGridViewCellStyle11;
            this._Button.Frozen = true;
            this._Button.HeaderText = "";
            this._Button.MinimumWidth = 35;
            this._Button.Name = "_Button";
            this._Button.ReadOnly = true;
            this._Button.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this._Button.Text = "F3";
            this._Button.UseColumnTextForButtonValue = true;
            this._Button.Width = 35;
            // 
            // _ProductName
            // 
            dataGridViewCellStyle12.BackColor = System.Drawing.Color.Bisque;
            this._ProductName.DefaultCellStyle = dataGridViewCellStyle12;
            this._ProductName.HeaderText = "Name";
            this._ProductName.MaxInputLength = 255;
            this._ProductName.MinimumWidth = 300;
            this._ProductName.Name = "_ProductName";
            this._ProductName.ReadOnly = true;
            this._ProductName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this._ProductName.Width = 500;
            // 
            // _Stok
            // 
            dataGridViewCellStyle13.BackColor = System.Drawing.Color.Bisque;
            this._Stok.DefaultCellStyle = dataGridViewCellStyle13;
            this._Stok.HeaderText = "Stok";
            this._Stok.Name = "_Stok";
            this._Stok.ReadOnly = true;
            this._Stok.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // _Qty
            // 
            this._Qty.HeaderText = "Jml Jual";
            this._Qty.Name = "_Qty";
            this._Qty.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this._Qty.Width = 75;
            // 
            // _HargaJual
            // 
            dataGridViewCellStyle14.BackColor = System.Drawing.Color.Bisque;
            this._HargaJual.DefaultCellStyle = dataGridViewCellStyle14;
            this._HargaJual.HeaderText = "Harga";
            this._HargaJual.Name = "_HargaJual";
            this._HargaJual.ReadOnly = true;
            this._HargaJual.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this._HargaJual.Width = 75;
            // 
            // _HargaJualAsal
            // 
            this._HargaJualAsal.HeaderText = "Harga Jual di DB";
            this._HargaJualAsal.Name = "_HargaJualAsal";
            this._HargaJualAsal.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this._HargaJualAsal.Visible = false;
            // 
            // _HargaBeli
            // 
            this._HargaBeli.HeaderText = "Harga Beli";
            this._HargaBeli.Name = "_HargaBeli";
            this._HargaBeli.Visible = false;
            // 
            // _KdSat
            // 
            dataGridViewCellStyle15.BackColor = System.Drawing.Color.Bisque;
            this._KdSat.DefaultCellStyle = dataGridViewCellStyle15;
            this._KdSat.HeaderText = "Satuan";
            this._KdSat.Name = "_KdSat";
            this._KdSat.ReadOnly = true;
            this._KdSat.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this._KdSat.Width = 50;
            // 
            // _TotalHarga
            // 
            dataGridViewCellStyle16.BackColor = System.Drawing.Color.Bisque;
            this._TotalHarga.DefaultCellStyle = dataGridViewCellStyle16;
            this._TotalHarga.HeaderText = "Total Harga";
            this._TotalHarga.Name = "_TotalHarga";
            this._TotalHarga.ReadOnly = true;
            this._TotalHarga.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // _Disc
            // 
            this._Disc.HeaderText = "Disc (%)";
            this._Disc.Name = "_Disc";
            this._Disc.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this._Disc.Width = 80;
            // 
            // _KdDisc
            // 
            dataGridViewCellStyle17.BackColor = System.Drawing.Color.Bisque;
            this._KdDisc.DefaultCellStyle = dataGridViewCellStyle17;
            this._KdDisc.HeaderText = "Tipe Disc";
            this._KdDisc.Name = "_KdDisc";
            // 
            // _TotalDisc
            // 
            dataGridViewCellStyle18.BackColor = System.Drawing.Color.Bisque;
            this._TotalDisc.DefaultCellStyle = dataGridViewCellStyle18;
            this._TotalDisc.HeaderText = "Total Disc";
            this._TotalDisc.Name = "_TotalDisc";
            this._TotalDisc.ReadOnly = true;
            this._TotalDisc.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this._TotalDisc.Width = 130;
            // 
            // dgvGrid
            // 
            this.dgvGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this._nik,
            this._nama});
            this.dgvGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvGrid.Location = new System.Drawing.Point(0, 0);
            this.dgvGrid.Name = "dgvGrid";
            this.dgvGrid.Size = new System.Drawing.Size(984, 146);
            this.dgvGrid.TabIndex = 28;
            // 
            // _nik
            // 
            this._nik.HeaderText = "NIK";
            this._nik.Name = "_nik";
            // 
            // _nama
            // 
            this._nama.HeaderText = "Nama";
            this._nama.Name = "_nama";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panSelectMethod);
            this.panel1.Controls.Add(this.panUserRecords);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(984, 301);
            this.panel1.TabIndex = 30;
            // 
            // panSelectMethod
            // 
            this.panSelectMethod.Controls.Add(this.tabAbsensiMethod);
            this.panSelectMethod.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panSelectMethod.Location = new System.Drawing.Point(0, 0);
            this.panSelectMethod.Name = "panSelectMethod";
            this.panSelectMethod.Size = new System.Drawing.Size(984, 148);
            this.panSelectMethod.TabIndex = 40;
            // 
            // tabAbsensiMethod
            // 
            this.tabAbsensiMethod.Controls.Add(this.tabUSB);
            this.tabAbsensiMethod.Controls.Add(this.tabIPAddress);
            this.tabAbsensiMethod.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabAbsensiMethod.Location = new System.Drawing.Point(0, 0);
            this.tabAbsensiMethod.Name = "tabAbsensiMethod";
            this.tabAbsensiMethod.SelectedIndex = 0;
            this.tabAbsensiMethod.Size = new System.Drawing.Size(984, 148);
            this.tabAbsensiMethod.TabIndex = 39;
            this.tabAbsensiMethod.SelectedIndexChanged += new System.EventHandler(this.tabAbsensiMethod_SelectedIndexChanged);
            // 
            // tabUSB
            // 
            this.tabUSB.Controls.Add(this.lblFileName);
            this.tabUSB.Controls.Add(this.txtFilename);
            this.tabUSB.Controls.Add(this.btnSelectData);
            this.tabUSB.Location = new System.Drawing.Point(4, 22);
            this.tabUSB.Name = "tabUSB";
            this.tabUSB.Padding = new System.Windows.Forms.Padding(3);
            this.tabUSB.Size = new System.Drawing.Size(976, 122);
            this.tabUSB.TabIndex = 0;
            this.tabUSB.Text = "USB";
            this.tabUSB.UseVisualStyleBackColor = true;
            // 
            // lblFileName
            // 
            this.lblFileName.AutoSize = true;
            this.lblFileName.Location = new System.Drawing.Point(6, 22);
            this.lblFileName.Name = "lblFileName";
            this.lblFileName.Size = new System.Drawing.Size(54, 13);
            this.lblFileName.TabIndex = 31;
            this.lblFileName.Text = "File Name";
            // 
            // txtFilename
            // 
            this.txtFilename.Location = new System.Drawing.Point(92, 19);
            this.txtFilename.Name = "txtFilename";
            this.txtFilename.Size = new System.Drawing.Size(302, 20);
            this.txtFilename.TabIndex = 2;
            // 
            // btnSelectData
            // 
            this.btnSelectData.Location = new System.Drawing.Point(400, 17);
            this.btnSelectData.Name = "btnSelectData";
            this.btnSelectData.Size = new System.Drawing.Size(75, 23);
            this.btnSelectData.TabIndex = 0;
            this.btnSelectData.Text = "Browse";
            this.btnSelectData.UseVisualStyleBackColor = true;
            this.btnSelectData.Click += new System.EventHandler(this.btnSelectData_Click);
            // 
            // tabIPAddress
            // 
            this.tabIPAddress.Controls.Add(this.lblState);
            this.tabIPAddress.Controls.Add(this.btnConnect);
            this.tabIPAddress.Controls.Add(this.txtPort);
            this.tabIPAddress.Controls.Add(this.txtDevId);
            this.tabIPAddress.Controls.Add(this.txtIP);
            this.tabIPAddress.Controls.Add(this.lblDevId);
            this.tabIPAddress.Controls.Add(this.lblPort);
            this.tabIPAddress.Controls.Add(this.lblIPAddress);
            this.tabIPAddress.Location = new System.Drawing.Point(4, 22);
            this.tabIPAddress.Name = "tabIPAddress";
            this.tabIPAddress.Padding = new System.Windows.Forms.Padding(3);
            this.tabIPAddress.Size = new System.Drawing.Size(907, 122);
            this.tabIPAddress.TabIndex = 1;
            this.tabIPAddress.Text = "IP Address";
            this.tabIPAddress.UseVisualStyleBackColor = true;
            // 
            // lblState
            // 
            this.lblState.AutoSize = true;
            this.lblState.ForeColor = System.Drawing.Color.Crimson;
            this.lblState.Location = new System.Drawing.Point(393, 12);
            this.lblState.Name = "lblState";
            this.lblState.Size = new System.Drawing.Size(138, 13);
            this.lblState.TabIndex = 11;
            this.lblState.Text = "Current State:Disconnected";
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(300, 7);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(75, 23);
            this.btnConnect.TabIndex = 10;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(232, 8);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(53, 20);
            this.txtPort.TabIndex = 9;
            this.txtPort.Text = "4370";
            // 
            // txtDevId
            // 
            this.txtDevId.Location = new System.Drawing.Point(82, 34);
            this.txtDevId.Name = "txtDevId";
            this.txtDevId.Size = new System.Drawing.Size(95, 20);
            this.txtDevId.TabIndex = 8;
            this.txtDevId.Text = "1";
            // 
            // txtIP
            // 
            this.txtIP.Location = new System.Drawing.Point(82, 8);
            this.txtIP.Name = "txtIP";
            this.txtIP.Size = new System.Drawing.Size(95, 20);
            this.txtIP.TabIndex = 8;
            this.txtIP.Text = "192.168.1.201";
            // 
            // lblDevId
            // 
            this.lblDevId.AutoSize = true;
            this.lblDevId.Location = new System.Drawing.Point(12, 38);
            this.lblDevId.Name = "lblDevId";
            this.lblDevId.Size = new System.Drawing.Size(53, 13);
            this.lblDevId.TabIndex = 0;
            this.lblDevId.Text = "Device Id";
            // 
            // lblPort
            // 
            this.lblPort.AutoSize = true;
            this.lblPort.Location = new System.Drawing.Point(200, 12);
            this.lblPort.Name = "lblPort";
            this.lblPort.Size = new System.Drawing.Size(26, 13);
            this.lblPort.TabIndex = 0;
            this.lblPort.Text = "Port";
            // 
            // lblIPAddress
            // 
            this.lblIPAddress.AutoSize = true;
            this.lblIPAddress.Location = new System.Drawing.Point(12, 12);
            this.lblIPAddress.Name = "lblIPAddress";
            this.lblIPAddress.Size = new System.Drawing.Size(58, 13);
            this.lblIPAddress.TabIndex = 0;
            this.lblIPAddress.Text = "IP Address";
            // 
            // panUserRecords
            // 
            this.panUserRecords.Controls.Add(this.groupBox1);
            this.panUserRecords.Controls.Add(this.btnRptHistoryAbsensi);
            this.panUserRecords.Controls.Add(this.btnReport);
            this.panUserRecords.Controls.Add(this.btnPrintUM);
            this.panUserRecords.Controls.Add(this.btnRptRekapAbsensi);
            this.panUserRecords.Controls.Add(this.groupBox2);
            this.panUserRecords.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panUserRecords.Location = new System.Drawing.Point(0, 148);
            this.panUserRecords.Name = "panUserRecords";
            this.panUserRecords.Size = new System.Drawing.Size(984, 153);
            this.panUserRecords.TabIndex = 39;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cboDivisi);
            this.groupBox1.Controls.Add(this.lblDivisi);
            this.groupBox1.Controls.Add(this.cboJam);
            this.groupBox1.Controls.Add(this.cboWarehouse);
            this.groupBox1.Controls.Add(this.lblJam);
            this.groupBox1.Controls.Add(this.lblWarehouse);
            this.groupBox1.Controls.Add(this.btnLoadUser);
            this.groupBox1.Controls.Add(this.btnSave);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(297, 141);
            this.groupBox1.TabIndex = 33;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Karyawan";
            // 
            // cboDivisi
            // 
            this.cboDivisi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDivisi.FormattingEnabled = true;
            this.cboDivisi.Location = new System.Drawing.Point(83, 82);
            this.cboDivisi.Name = "cboDivisi";
            this.cboDivisi.Size = new System.Drawing.Size(185, 21);
            this.cboDivisi.TabIndex = 31;
            // 
            // lblDivisi
            // 
            this.lblDivisi.AutoSize = true;
            this.lblDivisi.Location = new System.Drawing.Point(23, 84);
            this.lblDivisi.Name = "lblDivisi";
            this.lblDivisi.Size = new System.Drawing.Size(32, 13);
            this.lblDivisi.TabIndex = 30;
            this.lblDivisi.Text = "Divisi";
            // 
            // cboJam
            // 
            this.cboJam.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboJam.FormattingEnabled = true;
            this.cboJam.Location = new System.Drawing.Point(83, 55);
            this.cboJam.Name = "cboJam";
            this.cboJam.Size = new System.Drawing.Size(185, 21);
            this.cboJam.TabIndex = 29;
            // 
            // cboWarehouse
            // 
            this.cboWarehouse.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboWarehouse.FormattingEnabled = true;
            this.cboWarehouse.Location = new System.Drawing.Point(83, 28);
            this.cboWarehouse.Name = "cboWarehouse";
            this.cboWarehouse.Size = new System.Drawing.Size(185, 21);
            this.cboWarehouse.TabIndex = 29;
            // 
            // lblJam
            // 
            this.lblJam.AutoSize = true;
            this.lblJam.Location = new System.Drawing.Point(23, 57);
            this.lblJam.Name = "lblJam";
            this.lblJam.Size = new System.Drawing.Size(28, 13);
            this.lblJam.TabIndex = 28;
            this.lblJam.Text = "Shift";
            // 
            // lblWarehouse
            // 
            this.lblWarehouse.AutoSize = true;
            this.lblWarehouse.Location = new System.Drawing.Point(23, 31);
            this.lblWarehouse.Name = "lblWarehouse";
            this.lblWarehouse.Size = new System.Drawing.Size(44, 13);
            this.lblWarehouse.TabIndex = 28;
            this.lblWarehouse.Text = "Cabang";
            // 
            // btnLoadUser
            // 
            this.btnLoadUser.Location = new System.Drawing.Point(16, 112);
            this.btnLoadUser.Name = "btnLoadUser";
            this.btnLoadUser.Size = new System.Drawing.Size(75, 23);
            this.btnLoadUser.TabIndex = 0;
            this.btnLoadUser.Text = "Load User";
            this.btnLoadUser.UseVisualStyleBackColor = true;
            this.btnLoadUser.Click += new System.EventHandler(this.btnLoadData_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(97, 112);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 27;
            this.btnSave.Text = "&Save User";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnReport
            // 
            this.btnReport.Location = new System.Drawing.Point(624, 10);
            this.btnReport.Name = "btnReport";
            this.btnReport.Size = new System.Drawing.Size(120, 23);
            this.btnReport.TabIndex = 30;
            this.btnReport.Text = "Print Jam Absensi";
            this.btnReport.UseVisualStyleBackColor = true;
            this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
            // 
            // btnPrintUM
            // 
            this.btnPrintUM.Location = new System.Drawing.Point(748, 10);
            this.btnPrintUM.Name = "btnPrintUM";
            this.btnPrintUM.Size = new System.Drawing.Size(116, 23);
            this.btnPrintUM.TabIndex = 35;
            this.btnPrintUM.Text = "Print Uang Makan";
            this.btnPrintUM.UseVisualStyleBackColor = true;
            this.btnPrintUM.Click += new System.EventHandler(this.btnPrintUM_Click);
            // 
            // btnRptRekapAbsensi
            // 
            this.btnRptRekapAbsensi.Location = new System.Drawing.Point(870, 10);
            this.btnRptRekapAbsensi.Name = "btnRptRekapAbsensi";
            this.btnRptRekapAbsensi.Size = new System.Drawing.Size(116, 23);
            this.btnRptRekapAbsensi.TabIndex = 30;
            this.btnRptRekapAbsensi.Text = "Print Rekap Gaji";
            this.btnRptRekapAbsensi.UseVisualStyleBackColor = true;
            this.btnRptRekapAbsensi.Click += new System.EventHandler(this.btnRptRekapAbsensi_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnLoadAbsensi);
            this.groupBox2.Controls.Add(this.btnImportCSV);
            this.groupBox2.Controls.Add(this.chkSolutions);
            this.groupBox2.Controls.Add(this.btnImport);
            this.groupBox2.Controls.Add(this.btnSaveAbsensi);
            this.groupBox2.Location = new System.Drawing.Point(317, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(301, 141);
            this.groupBox2.TabIndex = 34;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Record Absensi";
            // 
            // btnLoadAbsensi
            // 
            this.btnLoadAbsensi.Location = new System.Drawing.Point(28, 29);
            this.btnLoadAbsensi.Name = "btnLoadAbsensi";
            this.btnLoadAbsensi.Size = new System.Drawing.Size(91, 23);
            this.btnLoadAbsensi.TabIndex = 0;
            this.btnLoadAbsensi.Text = "Load Absensi";
            this.btnLoadAbsensi.UseVisualStyleBackColor = true;
            this.btnLoadAbsensi.Click += new System.EventHandler(this.btnLoadAbsensi_Click);
            // 
            // btnImportCSV
            // 
            this.btnImportCSV.Location = new System.Drawing.Point(125, 29);
            this.btnImportCSV.Name = "btnImportCSV";
            this.btnImportCSV.Size = new System.Drawing.Size(75, 23);
            this.btnImportCSV.TabIndex = 30;
            this.btnImportCSV.Text = "Import CSV";
            this.btnImportCSV.UseVisualStyleBackColor = true;
            this.btnImportCSV.Click += new System.EventHandler(this.btnImportCSV_Click);
            // 
            // chkSolutions
            // 
            this.chkSolutions.AutoSize = true;
            this.chkSolutions.Location = new System.Drawing.Point(28, 91);
            this.chkSolutions.Name = "chkSolutions";
            this.chkSolutions.Size = new System.Drawing.Size(145, 17);
            this.chkSolutions.TabIndex = 32;
            this.chkSolutions.Text = "Mesin sidik jari \"Solution\"";
            this.chkSolutions.UseVisualStyleBackColor = true;
            // 
            // btnImport
            // 
            this.btnImport.Location = new System.Drawing.Point(206, 29);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(75, 23);
            this.btnImport.TabIndex = 30;
            this.btnImport.Text = "Import Excel";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // btnSaveAbsensi
            // 
            this.btnSaveAbsensi.Location = new System.Drawing.Point(28, 58);
            this.btnSaveAbsensi.Name = "btnSaveAbsensi";
            this.btnSaveAbsensi.Size = new System.Drawing.Size(91, 23);
            this.btnSaveAbsensi.TabIndex = 29;
            this.btnSaveAbsensi.Text = "Save Absensi";
            this.btnSaveAbsensi.UseVisualStyleBackColor = true;
            this.btnSaveAbsensi.Click += new System.EventHandler(this.btnSaveAbsensi_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dgvGrid);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 301);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(984, 146);
            this.panel2.TabIndex = 31;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btnClose);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 447);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(984, 35);
            this.panel3.TabIndex = 32;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(12, 5);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnRptHistoryAbsensi
            // 
            this.btnRptHistoryAbsensi.Location = new System.Drawing.Point(624, 39);
            this.btnRptHistoryAbsensi.Name = "btnRptHistoryAbsensi";
            this.btnRptHistoryAbsensi.Size = new System.Drawing.Size(120, 23);
            this.btnRptHistoryAbsensi.TabIndex = 30;
            this.btnRptHistoryAbsensi.Text = "Print History Absensi";
            this.btnRptHistoryAbsensi.UseVisualStyleBackColor = true;
            this.btnRptHistoryAbsensi.Click += new System.EventHandler(this.btnRptHistoryAbsensi_Click);
            // 
            // FrmDownloadData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 482);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.axBioBridgeSDK1);
            this.Name = "FrmDownloadData";
            this.Text = "Aurelia Group Data Absensi";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.axBioBridgeSDK1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGrid)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panSelectMethod.ResumeLayout(false);
            this.tabAbsensiMethod.ResumeLayout(false);
            this.tabUSB.ResumeLayout(false);
            this.tabUSB.PerformLayout();
            this.tabIPAddress.ResumeLayout(false);
            this.tabIPAddress.PerformLayout();
            this.panUserRecords.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public AxBioBridgeSDKLib.AxBioBridgeSDK axBioBridgeSDK1;
        private System.Windows.Forms.DataGridViewTextBoxColumn _MyKode;
        private System.Windows.Forms.DataGridViewTextBoxColumn _ProductID;
        private System.Windows.Forms.DataGridViewButtonColumn _Button;
        private System.Windows.Forms.DataGridViewTextBoxColumn _ProductName;
        private System.Windows.Forms.DataGridViewTextBoxColumn _Stok;
        private System.Windows.Forms.DataGridViewTextBoxColumn _Qty;
        private System.Windows.Forms.DataGridViewTextBoxColumn _HargaJual;
        private System.Windows.Forms.DataGridViewTextBoxColumn _HargaJualAsal;
        private System.Windows.Forms.DataGridViewTextBoxColumn _HargaBeli;
        private System.Windows.Forms.DataGridViewTextBoxColumn _KdSat;
        private System.Windows.Forms.DataGridViewTextBoxColumn _TotalHarga;
        private System.Windows.Forms.DataGridViewTextBoxColumn _Disc;
        private System.Windows.Forms.DataGridViewComboBoxColumn _KdDisc;
        private System.Windows.Forms.DataGridViewTextBoxColumn _TotalDisc;
        private System.Windows.Forms.DataGridView dgvGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn _nik;
        private System.Windows.Forms.DataGridViewTextBoxColumn _nama;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Panel panSelectMethod;
        private System.Windows.Forms.TabControl tabAbsensiMethod;
        private System.Windows.Forms.TabPage tabUSB;
        private System.Windows.Forms.Label lblFileName;
        private System.Windows.Forms.TextBox txtFilename;
        private System.Windows.Forms.Button btnSelectData;
        private System.Windows.Forms.TabPage tabIPAddress;
        private System.Windows.Forms.Label lblState;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.TextBox txtIP;
        private System.Windows.Forms.Label lblPort;
        private System.Windows.Forms.Label lblIPAddress;
        private System.Windows.Forms.Panel panUserRecords;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cboDivisi;
        private System.Windows.Forms.Label lblDivisi;
        private System.Windows.Forms.ComboBox cboJam;
        private System.Windows.Forms.ComboBox cboWarehouse;
        private System.Windows.Forms.Label lblJam;
        private System.Windows.Forms.Label lblWarehouse;
        private System.Windows.Forms.Button btnLoadUser;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnReport;
        private System.Windows.Forms.Button btnPrintUM;
        private System.Windows.Forms.Button btnRptRekapAbsensi;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnLoadAbsensi;
        private System.Windows.Forms.Button btnImportCSV;
        private System.Windows.Forms.CheckBox chkSolutions;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.Button btnSaveAbsensi;
        private System.Windows.Forms.TextBox txtDevId;
        private System.Windows.Forms.Label lblDevId;
        private System.Windows.Forms.Button btnRptHistoryAbsensi;
    }
}

