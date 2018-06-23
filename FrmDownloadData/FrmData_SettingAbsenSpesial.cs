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
    public partial class FrmData_SettingAbsenSpesial : Form
    {
        private const string dbname = "HRD", Title = "Absensi",
            KdCabang = "AUR01",
            DISP_WAREHOUSE = "DisplayWarehouse",
            VAL_WAREHOUSE = "ValueWarehouse",
            DISP_DIVISI = "DisplayDivisi",
            VAL_DIVISI = "ValueDivisi",
            DISP_ABSENSPESIAL = "DisplayAbsenSpesial",
            VAL_ABSENSPESIAL = "ValueAbsenSpesial",
            KODE_WAR_PENJUALAN = "PST";
        private const int JML_COL_TETAP = 4;
        private int idx_nik = 1, idx_pilihan = 0;
        private string connstring = @"Data Source=ITSUPPORT\SQLEXPRESS2008R2;Database=" + dbname +
            ";Integrated Security=True;Connect Timeout=30; User Instance=False;"; //User ID=" + Uid + ";Password=" + Pwd;
        const int WORKSHEETSTARTROW = 5;
        const int WORKSHEETSTARTCOL = 4;
        private GlobalFunction gFunc = new GlobalFunction();
        int tahun, bulan, nik;
        bool isErr;

        public FrmData_SettingAbsenSpesial()
        {
            InitializeComponent();

            LoadDataCombo();

            txtTahun.Text = DateTime.Now.Year.ToString();

            for (int i = 1; i <= 12; i++)
                cboBulan.Items.Add(DateTimeFormatInfo.CurrentInfo.GetMonthName(i));
            //cboBulan.Items.AddRange(DateTimeFormatInfo.CurrentInfo.MonthNames);

            cboBulan.SelectedIndex = DateTime.Now.Month - 1;

            dtpPeriodeAwal.Value = dtpPeriodeAkhir.Value = DateTime.Now;

            RefreshPeriode();
            gFunc.SetTextboxEnable(false, txtNama);
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

                    ArrayList arrWarehouse = new ArrayList(), arrDivisi = new ArrayList(),
                        arrAbsenspesial = new ArrayList();

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

                    sqlCmd.CommandText = "GET_KodeAbsenSpesial";
                    sqlCmd.Parameters.Clear();
                    sqlDR = sqlCmd.ExecuteReader();
                    if (sqlDR.Read())
                    {
                        iID = sqlDR.GetOrdinal("id");
                        iDesc = sqlDR.GetOrdinal("Absen_spesial");

                        do
                        {
                            arrAbsenspesial.Add(new ArrayAbsenSpesial(sqlDR.GetString(iID), sqlDR.GetString(iDesc)));
                        }
                        while (sqlDR.Read());
                    }

                    if (arrWarehouse.Count > 0)
                    {
                        cboInfo.DropDownStyle = ComboBoxStyle.DropDownList;
                        cboInfo.DataSource = arrAbsenspesial;
                        cboInfo.DisplayMember = DISP_ABSENSPESIAL;
                        cboInfo.ValueMember = VAL_ABSENSPESIAL;
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

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSearch_Click(object sender, EventArgs e)
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
                        MessageBox.Show("Data Karyawan ini tidak ketemu...!", Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtNIK.Focus();
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

        private void txtNIK_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == KeysENTER)
            {
                e.Handled = true;
                LoadData();
                txtKeterangan.Focus();
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private static char KeysENTER
        {
            get
            {
                return (char)13;
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshPeriode();
        }

        private void ValidateTahun()
        {
            bulan = cboBulan.SelectedIndex + 1;
            isErr = int.TryParse(txtTahun.Text, out tahun);

            if (isErr)
            {
                tahun = DateTime.Now.Year;
                txtTahun.Text = tahun.ToString();
            }
        }

        private void RefreshPeriode()
        {
            ValidateTahun();

            //dtpPeriodeAwal.MinDate = dtpPeriodeAkhir.MinDate = new DateTime(tahun, bulan, 1);
            //dtpPeriodeAwal.MaxDate = dtpPeriodeAkhir.MaxDate = new DateTime(tahun, bulan, DateTime.DaysInMonth(tahun, bulan));
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveData();
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

        private void SaveData()
        {
            ValidateTahun();

            isErr = int.TryParse(txtNIK.Text, out nik);

            if (!IsKaryawanExists(nik))
            {
                MessageBox.Show("Invalid Karyawan!!!", Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtNIK.Focus();
                return;
            }

            DialogResult dr = MessageBox.Show("Are you sure want to save this data ?", Title, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (dr == DialogResult.No)
            {
                txtNIK.Focus();
                return;
            }

            SqlTransaction sqlTrans = null;
            try
            {
                using (SqlConnection sqlCnn = new SqlConnection(ZFame.Classes.clsVarProgram.DB_CONN_STRING))
                {
                    sqlCnn.Open();
                    sqlTrans = sqlCnn.BeginTransaction();
                    SqlCommand sqlCmd = sqlCnn.CreateCommand();
                    sqlCmd.Transaction = sqlTrans;

                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.CommandText = "del_absensi_spesial";
                    sqlCmd.Parameters.Add("@NIK", SqlDbType.Int).Value = nik;
                    sqlCmd.Parameters.Add("@Tahun", SqlDbType.SmallInt).Value = tahun;
                    sqlCmd.Parameters.Add("@Bulan", SqlDbType.TinyInt).Value = bulan;
                    sqlCmd.Parameters.Add("@userid", SqlDbType.NVarChar, 10).Value = "admin";
                    sqlCmd.ExecuteNonQuery();

                    sqlCmd.Parameters.Clear();
                    sqlCmd.CommandText = "ins_absensi_spesial";
                    sqlCmd.Parameters.Add("@NIK", SqlDbType.Int).Value = nik;
                    sqlCmd.Parameters.Add("@clock_date", SqlDbType.Date);
                    sqlCmd.Parameters.Add("@id_absensi_spesial", SqlDbType.Char, 1).Value = cboInfo.SelectedValue.ToString();
                    sqlCmd.Parameters.Add("@Tahun", SqlDbType.SmallInt).Value = tahun;
                    sqlCmd.Parameters.Add("@Bulan", SqlDbType.TinyInt).Value = bulan;
                    sqlCmd.Parameters.Add("@keterangan", SqlDbType.VarChar, 255).Value = txtKeterangan.Text;
                    sqlCmd.Parameters.Add("@userid", SqlDbType.NVarChar, 10).Value = "admin";

                    int tglawal = dtpPeriodeAwal.Value.Day, tglakhir = dtpPeriodeAkhir.Value.Day;
                    DateTime tgl;

                    for (int a = tglawal; a <= tglakhir; a++)
                    {
                        tgl = new DateTime(tahun, bulan, a);
                        sqlCmd.Parameters["@clock_date"].Value = tgl;

                        sqlCmd.ExecuteNonQuery();
                    }

                    sqlTrans.Commit();
                    MessageBox.Show("Data has been Saved!", Title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtNIK.Focus();
                }
            }
            catch (Exception exc)
            {
                if (sqlTrans != null)
                    sqlTrans.Rollback();

                MessageBox.Show("Cannot Save Data!" + Environment.NewLine + Environment.NewLine +
                    exc.Message, Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cboBulan_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == KeysENTER)
            {
                e.Handled = true;
                txtTahun.Focus();
            }
        }

        private void txtTahun_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == KeysENTER)
            {
                e.Handled = true;
                btnRefresh.PerformClick();
                txtNIK.Focus();
            }
        }

        private void txtKeterangan_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == KeysENTER)
            {
                e.Handled = true;
                dtpPeriodeAwal.Focus();
            }
        }

        private void dtpPeriodeAwal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == KeysENTER)
            {
                e.Handled = true;
                dtpPeriodeAkhir.Focus();
            }
        }

        private void dtpPeriodeAkhir_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == KeysENTER)
            {
                e.Handled = true;
                cboInfo.Focus();
            }
        }

        private void cboInfo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == KeysENTER)
            {
                e.Handled = true;
                btnSave.Focus();
            }
        }

    }
}
