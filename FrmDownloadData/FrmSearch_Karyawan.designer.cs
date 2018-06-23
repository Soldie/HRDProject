namespace HRDProject
{
    partial class FrmSearch_Karyawan
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.txtNama = new System.Windows.Forms.TextBox();
            this.lblNama = new System.Windows.Forms.Label();
            this.dgvGrid = new System.Windows.Forms.DataGridView();
            this._NIK = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._Nama = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._NickName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._Kdwarehouse = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnEdit);
            this.panel1.Controls.Add(this.btnRefresh);
            this.panel1.Controls.Add(this.btnOK);
            this.panel1.Controls.Add(this.txtNama);
            this.panel1.Controls.Add(this.lblNama);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 348);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(691, 70);
            this.panel1.TabIndex = 0;
            // 
            // btnEdit
            // 
            this.btnEdit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEdit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEdit.Location = new System.Drawing.Point(89, 34);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(64, 23);
            this.btnEdit.TabIndex = 6;
            this.btnEdit.Text = "&Cancel";
            // 
            // btnRefresh
            // 
            this.btnRefresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Location = new System.Drawing.Point(246, 6);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(64, 23);
            this.btnRefresh.TabIndex = 4;
            this.btnRefresh.TabStop = false;
            this.btnRefresh.Text = "&Refresh";
            this.btnRefresh.EnabledChanged += new System.EventHandler(this.btnRefresh_EnabledChanged);
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnOK
            // 
            this.btnOK.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOK.Location = new System.Drawing.Point(17, 34);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(64, 23);
            this.btnOK.TabIndex = 5;
            this.btnOK.Text = "&OK";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // txtNama
            // 
            this.txtNama.Location = new System.Drawing.Point(55, 8);
            this.txtNama.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
            this.txtNama.MaxLength = 255;
            this.txtNama.Name = "txtNama";
            this.txtNama.Size = new System.Drawing.Size(185, 20);
            this.txtNama.TabIndex = 1;
            this.txtNama.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNama_KeyPress);
            this.txtNama.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtNama_KeyUp);
            // 
            // lblNama
            // 
            this.lblNama.AutoSize = true;
            this.lblNama.Location = new System.Drawing.Point(14, 11);
            this.lblNama.Name = "lblNama";
            this.lblNama.Size = new System.Drawing.Size(35, 13);
            this.lblNama.TabIndex = 0;
            this.lblNama.Text = "N&ame";
            this.lblNama.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dgvGrid
            // 
            this.dgvGrid.AllowUserToAddRows = false;
            this.dgvGrid.AllowUserToDeleteRows = false;
            this.dgvGrid.AllowUserToOrderColumns = true;
            this.dgvGrid.AllowUserToResizeRows = false;
            this.dgvGrid.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.dgvGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this._NIK,
            this._Nama,
            this._NickName,
            this._Kdwarehouse});
            this.dgvGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvGrid.Location = new System.Drawing.Point(0, 0);
            this.dgvGrid.MultiSelect = false;
            this.dgvGrid.Name = "dgvGrid";
            this.dgvGrid.ReadOnly = true;
            this.dgvGrid.RowHeadersVisible = false;
            this.dgvGrid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.DarkGreen;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Gold;
            this.dgvGrid.RowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvGrid.Size = new System.Drawing.Size(691, 348);
            this.dgvGrid.TabIndex = 1;
            this.dgvGrid.DoubleClick += new System.EventHandler(this.dgvGrid_DoubleClick);
            this.dgvGrid.KeyUp += new System.Windows.Forms.KeyEventHandler(this.dgvGrid_KeyUp);
            // 
            // _NIK
            // 
            this._NIK.HeaderText = "NIK";
            this._NIK.Name = "_NIK";
            this._NIK.ReadOnly = true;
            // 
            // _Nama
            // 
            this._Nama.HeaderText = "Nama";
            this._Nama.Name = "_Nama";
            this._Nama.ReadOnly = true;
            // 
            // _NickName
            // 
            this._NickName.HeaderText = "Panggilan";
            this._NickName.Name = "_NickName";
            this._NickName.ReadOnly = true;
            // 
            // _Kdwarehouse
            // 
            this._Kdwarehouse.HeaderText = "Cabang";
            this._Kdwarehouse.Name = "_Kdwarehouse";
            this._Kdwarehouse.ReadOnly = true;
            // 
            // FrmSearch_Karyawan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(691, 418);
            this.Controls.Add(this.dgvGrid);
            this.Controls.Add(this.panel1);
            this.Name = "FrmSearch_Karyawan";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Search Karyawan";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmSearch_Customer_FormClosed);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dgvGrid;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.TextBox txtNama;
        private System.Windows.Forms.Label lblNama;
        private System.Windows.Forms.DataGridViewTextBoxColumn _NIK;
        private System.Windows.Forms.DataGridViewTextBoxColumn _Nama;
        private System.Windows.Forms.DataGridViewTextBoxColumn _NickName;
        private System.Windows.Forms.DataGridViewTextBoxColumn _Kdwarehouse;
    }
}