using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Globalization;
using System.Collections;

namespace HRDProject
{
    public partial class FrmData_Karyawan : Form
    {
        private void CloseAllManagedResx()
        {
            gFunc = null;
            privileges = null;
            this.Dispose(true);
        }

        private void ChangeIconForm()
        {
            //System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            //System.IO.Stream stream = assembly.GetManifestResourceStream(InfoApp.MyAssemblyName + "." + InfoApp.IconPath + "." + InfoApp.IconName);
            //this.Icon = new Icon(stream);
        }

        #region Private Variables
        private readonly NumberFormatInfo nFi;
        private GlobalFunction gFunc = new GlobalFunction();
        private Privileges privileges = new Privileges();
        private string strTitle, strUserID;
        private readonly MessageBoxDefaultButton defButton;
        private string KdCabang = "AUR01",
              DISP_WAREHOUSE = "DisplayWarehouse",
              VAL_WAREHOUSE = "ValueWarehouse",
              DISP_JAM = "DisplayJam",
              VAL_JAM = "ValueJam",
              KODE_WAR_PENJUALAN = "PST",
              DISP_DIVISI = "DisplayDivisi",
              VAL_DIVISI = "ValueDivisi",
              DISP_AGAMA = "DisplayAgama",
              VAL_AGAMA = "ValueAgama";
        #endregion

        #region Constructor

        public FrmData_Karyawan()
            : this(false, false, false, false, false, false)
        {
        }

        public FrmData_Karyawan(bool p_bViewed, bool p_bAdded, bool p_bEdited, bool p_bDeleted, bool p_bPrinted, bool p_bDownloaded)
        {
            InitializeComponent();

            ChangeIconForm();

            defButton = InfoApp.Default_Messagebox_Button;
            privileges.Viewed = p_bViewed;
            privileges.Added = p_bAdded;
            privileges.Edited = p_bEdited;
            privileges.Deleted = p_bDeleted;
            privileges.Printed = p_bPrinted;
            privileges.Downloaded = p_bDownloaded;
            Title = InfoApp.Title;
            UserID = InfoApp.UserID;
            nFi = InfoApp.NFormatInfo;

            LoadDataCombo();
            ClearForm();
        }

        #endregion

        #region Properties
        private bool IsSaved
        {
            get
            {
                return this.Tag.ToString().Equals("SAVE");
            }
        }

        private string Title
        {
            set
            {
                strTitle = value;
            }
            get
            {
                return strTitle;
            }
        }

        private string UserID
        {
            set
            {
                strUserID = value;
            }
            get
            {
                return strUserID;
            }
        }

        private static char KeysENTER
        {
            get
            {
                return (char)13;
            }
        }
        #endregion

        #region Methods
        private void ValidateButton(params Button[] buttons)
        {
            foreach (Button button in buttons)
            {
                if (button == btnSave)
                    btnSave.Enabled = true;//privileges.Added || privileges.Edited;
                else
                    if (button == btnEdit)
                        btnEdit.Enabled = true; //privileges.Edited;
                    else
                        if (button == btnDelete)
                            btnDelete.Enabled = true; //privileges.Deleted;
            }
        }

        private void LoadData()
        {
            //bsml;
            //if (!privileges.Viewed && !privileges.Added) return;

            try
            {
                int nik;

                int.TryParse(txtNIK.Text, out nik);

                using (SqlConnection sqlCnn = new SqlConnection(ZFame.Classes.clsVarProgram.DB_CONN_STRING))
                {
                    SqlCommand sqlCmd = new SqlCommand();
                    sqlCmd.Connection = sqlCnn;
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.CommandText = "get_karyawan";
                    sqlCmd.Parameters.Add("@nik", SqlDbType.Int).Value = nik;

                    sqlCnn.Open();

                    SqlDataReader dr = sqlCmd.ExecuteReader();

                    if (dr.Read())
                    {
                        //if (!privileges.Viewed)
                        //{
                        //    MessageBox.Show("You don't have privileges to view this data!", Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        //    txtNIK.Focus();
                        //}
                        //else
                        {
                            txtNama.Text = dr.GetString(dr.GetOrdinal("Nama"));
                            txtNickName.Text = dr.GetString(dr.GetOrdinal("NickName"));
                            txtNoRek.Text = dr.GetString(dr.GetOrdinal("NoRek"));
                            txtGajiPokok.Text = dr.GetDecimal(dr.GetOrdinal("gajipokok")).ToString("N2", nFi);
                            txtUangMakan.Text = dr.GetDecimal(dr.GetOrdinal("uangmakan")).ToString("N2", nFi);
                            chkHitungUangMakan.Checked = dr.GetBoolean(dr.GetOrdinal("ishitungum"));

                            cboWarehouse.SelectedValue = dr.GetString(dr.GetOrdinal("kdwarehouse"));
                            cboJam.SelectedValue = dr.GetInt16(dr.GetOrdinal("idjam")).ToString();
                            cboDivisi.SelectedValue = dr.GetInt16(dr.GetOrdinal("divisi")).ToString();
                            cboAgama.SelectedValue = dr.GetByte(dr.GetOrdinal("agamaid")).ToString();

                            if (dr.GetString( dr.GetOrdinal("StsRc") ).ToUpper() == "D")
                            {
                                lblStsrc.Visible = true;
                                lblStsrc.Text = "Deleted Record";
                                gFunc.SetButtonEnable(false, btnSave, btnEdit, btnDelete);
                            }
                            else
                            {
                                lblStsrc.Visible = false;
                                this.Tag = "EDIT";
                                ValidateButton(btnEdit, btnDelete);
                            }

                            dr.Close();
                            dr = null;
                            sqlCmd = null;
                            sqlCnn.Close();

                            txtNIK.Focus();
                            txtNIK.SelectAll();                            
                        }
                    }
                    else
                    {
                        dr.Close();
                        dr = null;
                        sqlCmd = null;
                        sqlCnn.Close();
                        InputData();
                    }
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("Cannot Load Data!" + Environment.NewLine + Environment.NewLine +
                    exc.Message, Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtNIK.Focus();
            }
        }

        private void InputData()
        {
            //bsml;
            //if (!privileges.Added)
            //{
            //    this.Tag = "SAVE";
            //    gFunc.SetButtonEnable(false, btnSave, btnEdit, btnDelete);
            //    return;
            //}

            ClearInput();
            ValidateButton(btnSave);
            SetComponentEnabled(true);
            cboWarehouse.Focus();
        }

        private void SaveData()
        {
            bool isErr;
            double gajipokok = 0, uangmakan = 0;

            isErr = !double.TryParse(txtGajiPokok.Text, out gajipokok);
            if (isErr == true)
            {
                MessageBox.Show("Error Gaji Pokok!!!", Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtGajiPokok.Focus();
                return;
            }

            isErr = !double.TryParse(txtUangMakan.Text, out uangmakan);
            if (isErr == true)
            {
                MessageBox.Show("Error Uang Makan!!!", Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtUangMakan.Focus();
                return;
            }

            try
            {
                using (SqlConnection sqlCnn = new SqlConnection(ZFame.Classes.clsVarProgram.DB_CONN_STRING))
                {
                    SqlCommand sqlCmd = new SqlCommand();
                    sqlCmd.Connection = sqlCnn;
                    sqlCmd.CommandType = CommandType.StoredProcedure;

                    if (IsSaved)
                        sqlCmd.CommandText = "Ins_karyawan";
                    else
                        sqlCmd.CommandText = "upd_karyawan";

                    sqlCmd.Parameters.Add("@nik", SqlDbType.Int).Value = txtNIK.Text;
                    sqlCmd.Parameters.Add("@KdCabang", SqlDbType.VarChar, 5).Value = KdCabang;
                    sqlCmd.Parameters.Add("@KdWarehouse", SqlDbType.VarChar, 3).Value = cboWarehouse.SelectedValue.ToString();
                    sqlCmd.Parameters.Add("@stsrc", SqlDbType.Char, 1).Value = "A";
                    sqlCmd.Parameters.Add("@nama", SqlDbType.NVarChar, 255).Value = txtNama.Text;
                    sqlCmd.Parameters.Add("@nickname", SqlDbType.NVarChar, 255).Value = txtNickName.Text;
                    sqlCmd.Parameters.Add("@idjam", SqlDbType.SmallInt).Value = cboJam.SelectedValue.ToString();
                    sqlCmd.Parameters.Add("@Divisi", SqlDbType.SmallInt).Value = cboDivisi.SelectedValue.ToString();
                    sqlCmd.Parameters.Add("@norek", SqlDbType.VarChar, 20).Value = txtNoRek.Text;
                    sqlCmd.Parameters.Add("@gajipokok", SqlDbType.Money).Value = gajipokok;
                    sqlCmd.Parameters.Add("@uangmakan", SqlDbType.Money).Value = uangmakan;
                    sqlCmd.Parameters.Add("@ishitungum", SqlDbType.Bit).Value = chkHitungUangMakan.Checked;
                    sqlCmd.Parameters.Add("@AgamaID", SqlDbType.TinyInt).Value = cboAgama.SelectedValue.ToString();
                    sqlCmd.Parameters.Add("@UserID", SqlDbType.NVarChar, 10).Value = "admin";

                    sqlCnn.Open();
                    sqlCmd.ExecuteNonQuery();
                }

                ClearForm();
                MessageBox.Show("Data has been saved!", Title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtNIK.Focus();
            }
            catch (Exception exc)
            {
                MessageBox.Show("Data cannot be saved!\n\n" +
                    exc.Message, Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void EditData()
        {
            this.Tag = "EDIT";
            btnSearch.Enabled = btnEdit.Enabled = false;
            btnSave.Enabled = true; //privileges.Edited;
            SetComponentEnabled(true);
            txtNama.Focus();
        }

        private void DeleteData()
        {
            DialogResult dr;
            dr = MessageBox.Show("Are you sure want to delete this data ?", Title, MessageBoxButtons.YesNo, MessageBoxIcon.Question, defButton);
            if (dr == DialogResult.No)
            {
                txtNIK.Focus();
                txtNIK.SelectAll();
                return;
            }
            try
            {
                using (SqlConnection sqlCnn = new SqlConnection(ZFame.Classes.clsVarProgram.DB_CONN_STRING))
                {
                    SqlCommand sqlCmd = new SqlCommand();
                    sqlCmd.Connection = sqlCnn;
                    sqlCmd.CommandType = CommandType.StoredProcedure;

                    sqlCmd.CommandText = "Del_Karyawan";

                    sqlCmd.Parameters.Add("@nik", SqlDbType.Int).Value = txtNIK.Text;
                    sqlCmd.Parameters.Add("@UserID", SqlDbType.NVarChar, 10).Value = "admin";

                    sqlCnn.Open();
                    sqlCmd.ExecuteNonQuery();
                }

                ClearForm();
                MessageBox.Show("Data has been deleted!", Title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtNIK.Focus();
            }
            catch (Exception exc)
            {
                MessageBox.Show("Cannot Delete Data!" + Environment.NewLine + Environment.NewLine +
                    exc.Message, Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetComponentEnabled(bool enabled)
        {
            gFunc.SetTextboxEnable(!enabled, txtNIK);
            gFunc.SetTextboxEnable(enabled, txtNama, txtNickName, txtNoRek, txtGajiPokok, txtUangMakan);
            gFunc.SetCheckboxEnable(enabled, chkHitungGaji, chkHitungUangMakan);
            gFunc.SetComboboxEnable(enabled, cboWarehouse, cboDivisi, cboJam, cboAgama);
        }

        private void ClearForm()
        {
            this.Tag = "SAVE";
            lblStsrc.Visible = false;
            btnSearch.Enabled = true;
            //chkIsCanDebt.Checked = chkIsCanRetur.Checked = false;

            gFunc.SetButtonEnable(false, btnSave, btnEdit, btnDelete);
            SetComponentEnabled(false);
            gFunc.ClearTextbox(txtNIK, txtNama, txtNickName, txtNoRek, txtGajiPokok, txtUangMakan);
        }

        private void ClearInput()
        {
            this.Tag = "SAVE";
            lblStsrc.Visible = false;
            btnSearch.Enabled = false;
            //chkIsCanDebt.Checked = false;
            //chkIsCanRetur.Checked = false;
            gFunc.SetButtonEnable(false, btnSave, btnEdit, btnDelete);
            gFunc.ClearTextbox(txtNama, txtNickName, txtNoRek, txtGajiPokok, txtUangMakan);
        }

        #endregion

        #region Events
        private void txtNIK_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == KeysENTER)
            {
                e.Handled = true;
                LoadData();
            }
        }

        private void Button_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            switch (btn.Name)
            {
                case "btnSave":
                    SaveData();
                    break;
                case "btnEdit":
                    EditData();
                    break;
                case "btnDelete":
                    DeleteData();
                    break;
                case "btnClear":
                    ClearForm();
                    txtNIK.Focus();
                    break;
                case "btnClose":
                    this.Close();
                    break;
            }
        }

        private void btnSearch_Click(object sender, System.EventArgs e)
        {
            FrmSearch_Karyawan frmSearch = new FrmSearch_Karyawan();

            frmSearch.ShowDialog();

            if (frmSearch.DialogResult == DialogResult.OK)
            {
                txtNIK.Text = frmSearch.NIK;
                txtNama.Text = frmSearch.Nama;
                txtNIK_KeyPress(this, new KeyPressEventArgs(KeysENTER));
            }
            txtNIK.Focus();

            frmSearch = null;
        }

        #endregion

        public bool LoadDataCombo()
        {
            try
            {
                using (SqlConnection sqlCnn = new SqlConnection(ZFame.Classes.clsVarProgram.DB_CONN_STRING))
                {
                    int iID = 0, iDesc = 0;

                    SqlCommand sqlCmd = new SqlCommand();
                    sqlCmd.Connection = sqlCnn;
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.CommandText = "Get_Warehouse";
                    sqlCmd.Parameters.Add("@KdCabang", SqlDbType.VarChar, 5).Value = KdCabang;

                    sqlCnn.Open();

                    SqlDataReader sqlDR = sqlCmd.ExecuteReader();

                    ArrayList arrWarehouse = new ArrayList(), arrJam = new ArrayList(),
                        arrDivisi = new ArrayList(), arrAgama = new ArrayList();

                    if (sqlDR.Read())
                    {
                        iID = sqlDR.GetOrdinal("KdWarehouse");
                        iDesc = sqlDR.GetOrdinal("NmWarehouse");

                        //arrWarehouse.Add(new ArrayWarehouse("all", "ALL"));
                        do
                        {
                            arrWarehouse.Add(new ArrayWarehouse(sqlDR.GetString(iID), sqlDR.GetString(iDesc)));
                        }
                        while (sqlDR.Read());
                    }

                    if (arrWarehouse.Count > 0)
                    {
                        cboWarehouse.DropDownStyle = ComboBoxStyle.DropDownList;
                        cboWarehouse.DataSource = arrWarehouse;
                        cboWarehouse.DisplayMember = DISP_WAREHOUSE;
                        cboWarehouse.ValueMember = VAL_WAREHOUSE;
                        //cboWarehouse.SelectedValue = KODE_WAR_PENJUALAN;
                    }
                    else
                    {
                        cboWarehouse.DropDownStyle = ComboBoxStyle.DropDown;
                        cboWarehouse.Text = "Not Available";
                    }
                    sqlDR.Close();
                    sqlCmd.CommandText = "Get_Jam";
                    sqlCmd.Parameters.Clear();

                    sqlDR = sqlCmd.ExecuteReader();

                    if (sqlDR.Read())
                    {
                        iID = sqlDR.GetOrdinal("idjam");
                        iDesc = sqlDR.GetOrdinal("jam");

                        do
                        {
                            arrJam.Add(new ArrayJam(sqlDR.GetInt16(iID).ToString(), sqlDR.GetString(iDesc)));
                        }
                        while (sqlDR.Read());
                    }

                    if (arrJam.Count > 0)
                    {
                        cboJam.DropDownStyle = ComboBoxStyle.DropDownList;
                        cboJam.DataSource = arrJam;
                        cboJam.DisplayMember = DISP_JAM;
                        cboJam.ValueMember = VAL_JAM;
                    }
                    else
                    {
                        cboJam.DropDownStyle = ComboBoxStyle.DropDown;
                        cboJam.Text = "Not Available";
                    }

                    sqlDR.Close();

                    sqlCmd.CommandText = "Get_Divisi";
                    sqlCmd.Parameters.Clear();
                    sqlDR = sqlCmd.ExecuteReader();
                    if (sqlDR.Read())
                    {
                        iID = sqlDR.GetOrdinal("Divisi");
                        iDesc = sqlDR.GetOrdinal("Description");

                        do
                        {
                            arrDivisi.Add(new ArrayDivisi(sqlDR.GetInt16(iID).ToString(), sqlDR.GetString(iDesc)));
                        }
                        while (sqlDR.Read());
                    }

                    if (arrWarehouse.Count > 0)
                    {
                        cboDivisi.DropDownStyle = ComboBoxStyle.DropDownList;
                        cboDivisi.DataSource = arrDivisi;
                        cboDivisi.DisplayMember = DISP_DIVISI;
                        cboDivisi.ValueMember = VAL_DIVISI;
                    }
                    else
                    {
                        cboDivisi.DropDownStyle = ComboBoxStyle.DropDown;
                        cboDivisi.Text = "Not Available";
                    }

                    sqlDR.Close();

                    sqlCmd.CommandText = "Get_Agama";
                    sqlCmd.Parameters.Clear();
                    sqlDR = sqlCmd.ExecuteReader();
                    if (sqlDR.Read())
                    {
                        iID = sqlDR.GetOrdinal("AgamaID");
                        iDesc = sqlDR.GetOrdinal("Agama");

                        do
                        {
                            arrAgama.Add(new ArrayAgama(sqlDR.GetByte(iID).ToString(), sqlDR.GetString(iDesc)));
                        }
                        while (sqlDR.Read());
                    }

                    if (arrWarehouse.Count > 0)
                    {
                        cboAgama.DropDownStyle = ComboBoxStyle.DropDownList;
                        cboAgama.DataSource = arrAgama;
                        cboAgama.DisplayMember = DISP_AGAMA;
                        cboAgama.ValueMember = VAL_AGAMA;
                    }
                    else
                    {
                        cboAgama.DropDownStyle = ComboBoxStyle.DropDown;
                        cboAgama.Text = "Not Available";
                    }

                    sqlDR.Close();
                    sqlDR = null;
                    sqlCmd = null;
                    sqlCnn.Close();

                    return true;
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("Cannot Load Data!" + Environment.NewLine + Environment.NewLine +
                    exc.Message, Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private class ArrayMembership
        {
            private string _ID, _Description;

            public ArrayMembership(string _ID, string _Description)
            {
                this._ID = _ID;
                this._Description = _Description;
            }

            public string DisplayMembership
            {
                get
                {
                    return this._Description;
                }
            }

            public string ValueMembership
            {
                get
                {
                    return this._ID;
                }
            }
        }

        private void txtNIK_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.F3))
            {
                btnSearch.PerformClick();
            }
        }

        private void FrmData_Customer_FormClosed(object sender, FormClosedEventArgs e)
        {
            CloseAllManagedResx();
        }

        private void cboWarehouse_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == KeysENTER)
            {
                e.Handled = true;
                txtNama.Focus();
            }
        }

        private void txtNama_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == KeysENTER)
            {
                e.Handled = true;
                txtNickName.Focus();
            }

        }

        private void txtNickName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == KeysENTER)
            {
                e.Handled = true;
                cboJam.Focus();
            }

        }

        private void cboJam_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == KeysENTER)
            {
                e.Handled = true;
                cboDivisi.Focus();
            }

        }

        private void cboDivisi_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == KeysENTER)
            {
                e.Handled = true;
                txtNoRek.Focus();
            }

        }

        private void txtNoRek_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == KeysENTER)
            {
                e.Handled = true;
                txtGajiPokok.Focus();
            }

        }

        private void txtGajiPokok_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == KeysENTER)
            {
                e.Handled = true;
                txtUangMakan.Focus();
            }

        }

        private void txtUangMakan_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == KeysENTER)
            {
                e.Handled = true;
                btnSave.Focus();
            }

        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            LoadData();
        }



    }
}
