using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Collections;
using System.Globalization;

namespace HRDProject
{
    public partial class FrmData_SwitchOff : Form
    {
        private const string dbname = "HRD", Title = "Absensi",
            KdCabang = "AUR01",
            DISP_WAREHOUSE = "DisplayWarehouse",
            VAL_WAREHOUSE = "ValueWarehouse",
            DISP_DIVISI = "DisplayDivisi",
            VAL_DIVISI = "ValueDivisi", 
            KODE_WAR_PENJUALAN = "PST";
        private const int JML_COL_TETAP = 4;
        private int idx_nik = 1, idx_pilihan = 0;
        private string connstring = @"Data Source=ITSUPPORT\SQLEXPRESS2008R2;Database=" + dbname +
            ";Integrated Security=True;Connect Timeout=30; User Instance=False;"; //User ID=" + Uid + ";Password=" + Pwd;
        const int WORKSHEETSTARTROW = 5;
        const int WORKSHEETSTARTCOL = 4;
        private GlobalFunction gFunc = new GlobalFunction();

        public FrmData_SwitchOff()
        {
            InitializeComponent();

            //LoadDataCombo();
        }

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

                    ArrayList arrWarehouse = new ArrayList(), arrDivisi = new ArrayList();

                    if (sqlDR.Read())
                    {
                        iID = sqlDR.GetOrdinal("KdWarehouse");
                        iDesc = sqlDR.GetOrdinal("NmWarehouse");

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
                        cboWarehouse.SelectedValue = KODE_WAR_PENJUALAN;
                    }
                    else
                    {
                        cboWarehouse.DropDownStyle = ComboBoxStyle.DropDown;
                        cboWarehouse.Text = "Not Available";
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
                        cboDivisi.SelectedValue = "2";
                    }
                    else
                    {
                        cboDivisi.DropDownStyle = ComboBoxStyle.DropDown;
                        cboDivisi.Text = "Not Available";
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

        private bool IsKaryawanExists(int nik)
        {
            string hasil = "0";

            try
            {
                using (SqlConnection sqlCnn = new SqlConnection(ZFame.Classes.clsVarProgram.DB_CONN_STRING))
                {
                    SqlCommand sqlCmd = new SqlCommand();
                    sqlCmd.Connection = sqlCnn;
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.CommandText = "IsKaryawanExists";
                    sqlCmd.Parameters.Add("@nik", SqlDbType.Int).Value = nik;

                    sqlCnn.Open();

                    hasil = sqlCmd.ExecuteScalar().ToString();

                    sqlCmd = null;
                    sqlCnn.Close();
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error Execute Karyawan Data in Database!" + Environment.NewLine + Environment.NewLine +
                    exc.Message, Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return (hasil == "1");
        }

        private void SaveData(int nik, int nik2)
        {
            try
            {
                using (SqlConnection sqlCnn = new SqlConnection(ZFame.Classes.clsVarProgram.DB_CONN_STRING))
                {
                    SqlCommand sqlCmd = new SqlCommand();
                    sqlCmd.Connection = sqlCnn;
                    sqlCmd.CommandType = CommandType.StoredProcedure;

                    sqlCmd.CommandText = "ins_switchoff";
                    sqlCmd.Parameters.Add("@NIK", SqlDbType.Int).Value = nik;
                    sqlCmd.Parameters.Add("@NIK2", SqlDbType.Int).Value = nik2;
                    sqlCmd.Parameters.Add("@PeriodeAwal", SqlDbType.DateTime).Value = dtpPeriodeAwal.Value.ToString();
                    sqlCmd.Parameters.Add("@PeriodeAkhir", SqlDbType.DateTime).Value = dtpPeriodeAkhir.Value.ToString();
                    sqlCnn.Open();
                    sqlCmd.ExecuteNonQuery();

                    MessageBox.Show("Data has been Saved!", Title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("Cannot Save Data!" + Environment.NewLine + Environment.NewLine +
                    exc.Message, Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void LoadData(TextBox txtNIK, TextBox txtNama)
        {
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
                            txtNama.Text = dr.GetString(dr.GetOrdinal("Nama"));

                            dr.Close();
                            dr = null;
                            sqlCmd = null;
                            sqlCnn.Close();

                            txtNIK.Focus();
                            txtNIK.SelectAll();
                    }
                    else
                    {
                        txtNama.Text = "";
                        dr.Close();
                        dr = null;
                        sqlCmd = null;
                        sqlCnn.Close();
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

        private void btnLoad1_Click(object sender, EventArgs e)
        {
            LoadData(txtNIK, txtNama);
        }

        private void btnSearch1_Click(object sender, EventArgs e)
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

        private void btnSearch2_Click(object sender, EventArgs e)
        {
            FrmSearch_Karyawan frmSearch = new FrmSearch_Karyawan();

            frmSearch.ShowDialog();

            if (frmSearch.DialogResult == DialogResult.OK)
            {
                txtNIK2.Text = frmSearch.NIK;
                txtNama2.Text = frmSearch.Nama;
                txtNIK2_KeyPress(this, new KeyPressEventArgs(KeysENTER));
            }
            txtNIK2.Focus();

            frmSearch = null;
        }

        private void txtNIK_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == KeysENTER)
            {
                e.Handled = true;
                LoadData(txtNIK, txtNama);
            }
        }

        private void txtNIK2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == KeysENTER)
            {
                e.Handled = true;
                LoadData(txtNIK2, txtNama2);
            }
        }

        private static char KeysENTER
        {
            get
            {
                return (char)13;
            }
        }

        private void txtNIK_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.F3))
            {
                btnSearch1.PerformClick();
            }
        }

        private void txtNIK2_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.F3))
            {
                btnSearch2.PerformClick();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int nik, nik2;
            bool isErr = int.TryParse(txtNIK.Text, out nik);

            if (dtpPeriodeAwal.Value.ToString() == dtpPeriodeAkhir.Value.ToString())
            {
                MessageBox.Show("Tanggal Akhir tidak boleh sama dengan tanggal awal!!!", Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                dtpPeriodeAkhir.Focus();
                return;
            }

            if (!IsKaryawanExists(nik))
            {
                MessageBox.Show("Invalid Karyawan!!!", Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtNIK.Focus();
                return;
            }

            isErr = int.TryParse(txtNIK2.Text, out nik2);

            if (!IsKaryawanExists(nik))
            {
                MessageBox.Show("Invalid Karyawan!!!", Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtNIK2.Focus();
                return;
            }

            DialogResult dr;
            dr = MessageBox.Show("Yakin ingin switch off ?\nPerpindahan Off ini tidak bisa dibatalkan...", Title, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (dr == DialogResult.No)
            {
                btnSave.Focus();
                return;
            }

            SaveData(nik, nik2);
        }

    }
}
