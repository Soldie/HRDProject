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
    public partial class FrmData_SettingOffKerja : Form
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

        public FrmData_SettingOffKerja()
        {
            InitializeComponent();

            btnSave.Enabled = false;

            LoadDataCombo();

            txtTahun.Text = DateTime.Now.Year.ToString();

            for (int i = 1; i <= 12; i++)
                cboBulan.Items.Add(DateTimeFormatInfo.CurrentInfo.GetMonthName(i));
            //cboBulan.Items.AddRange(DateTimeFormatInfo.CurrentInfo.MonthNames);

            cboBulan.SelectedIndex = DateTime.Now.Month - 1;

            Get_MonthPeriode(txtTahun.Text, cboBulan.SelectedIndex + 1);                        
        }

        private void Get_MonthPeriode(string tahun, int bulan)
        {
            int iYear = 0;
            int.TryParse(tahun, out iYear);

            if (iYear == 0)
                iYear = DateTime.Now.Year;

            dtpPeriodeAwal.Value = gFunc.GetFirstDayOfMonth(iYear, bulan);
            dtpPeriodeAkhir.Value = gFunc.GetLastDayOfMonth(iYear, bulan);

            int dayofweek = (int)dtpPeriodeAwal.Value.DayOfWeek;

            //if(dayofweek != 0)
            dtpPeriodeAwal.Value = dtpPeriodeAwal.Value.AddDays(-dayofweek);
            if(dtpPeriodeAwal.Value.Month != (cboBulan.SelectedIndex + 1))
                dtpPeriodeAwal.Value = dtpPeriodeAwal.Value.AddDays(7);

            dayofweek = (int)dtpPeriodeAkhir.Value.DayOfWeek;
            dtpPeriodeAkhir.Value = dtpPeriodeAkhir.Value.AddDays(-dayofweek);
            dtpPeriodeAkhir.Value = dtpPeriodeAkhir.Value.AddDays(6);
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

        private void SaveDataDetail(string offhari)
        {
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
                    sqlCmd.CommandText = "ins_offkerja";
                    sqlCmd.Parameters.Add("@NIK", SqlDbType.Int);
                    sqlCmd.Parameters.Add("@PeriodeAwal", SqlDbType.DateTime).Value = dtpPeriodeAwal.Value.ToString();
                    sqlCmd.Parameters.Add("@PeriodeAkhir", SqlDbType.DateTime).Value = dtpPeriodeAkhir.Value.ToString();
                    sqlCmd.Parameters.Add("@offhari", SqlDbType.VarChar, 10).Value = offhari;

                    foreach (DataGridViewRow row in dgvGrid.Rows)
                    {
                        if (!row.IsNewRow)
                        {
                            if ((bool)row.Cells[idx_pilihan].Value)
                            {
                                sqlCmd.Parameters["@NIK"].Value = row.Cells[idx_nik].Value.ToString();

                                sqlCmd.ExecuteNonQuery();
                            }
                        }
                    }

                    sqlTrans.Commit();
                    MessageBox.Show("Data has been Saved!", Title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnLoad.PerformClick();
                }
            }
            catch (Exception exc)
            {
                if(sqlTrans != null)
                    sqlTrans.Rollback();

                MessageBox.Show("Cannot Save Data!" + Environment.NewLine + Environment.NewLine +
                    exc.Message, Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            DialogResult dr;
            StringBuilder tmpString = new StringBuilder();

            if (chkHari0.Checked)
                tmpString.Append("0,");
            if (chkHari1.Checked)
                tmpString.Append("1,");
            if (chkHari2.Checked)
                tmpString.Append("2,");
            if (chkHari3.Checked)
                tmpString.Append("3,");
            if (chkHari4.Checked)
                tmpString.Append("4,");
            if (chkHari5.Checked)
                tmpString.Append("5,");
            if (chkHari6.Checked)
                tmpString.Append("6,");

            if (tmpString.Length > 0)
                tmpString.Remove(tmpString.Length - 1, 1);
            else
            {
                MessageBox.Show("Pilih Hari Off, minimal satu hari!", Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            dr = MessageBox.Show("Are you sure want to save this data ?", Title, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (dr == DialogResult.No)
            {
                dgvGrid.Focus();
                return;
            }

            SaveDataDetail(tmpString.ToString());

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool LoadData()
        {
            object[] rows;

            try
            {
                dgvGrid.Rows.Clear();
                using (SqlConnection sqlCnn = new SqlConnection(ZFame.Classes.clsVarProgram.DB_CONN_STRING))
                {
                    int iNIK = 0, iNama = 0, iStatus = 0,
                        iNickName = 0;
                    string stsRc;

                    SqlCommand sqlCmd = new SqlCommand();
                    sqlCmd.Connection = sqlCnn;
                    sqlCmd.CommandType = CommandType.StoredProcedure;

                    sqlCmd.CommandText = "get_listkaryawan";
                    sqlCmd.Parameters.Add("@kdcabang", SqlDbType.VarChar, 5).Value = KdCabang;
                    sqlCmd.Parameters.Add("@kdwarehouse", SqlDbType.VarChar, 3).Value = cboWarehouse.SelectedValue.ToString();
                    sqlCmd.Parameters.Add("@divisi", SqlDbType.SmallInt).Value = cboDivisi.SelectedValue.ToString();

                    sqlCnn.Open();

                    SqlDataReader sqlDR = sqlCmd.ExecuteReader();

                    if (sqlDR.Read())
                    {
                        iStatus = sqlDR.GetOrdinal("StsRc");
                        iNIK = sqlDR.GetOrdinal("NIK");
                        iNama = sqlDR.GetOrdinal("Nama");
                        iNickName = sqlDR.GetOrdinal("NickName");
                        
                        do
                        {
                            switch (sqlDR.GetString(iStatus).ToUpper())
                            {
                                case "A":
                                    stsRc = "Active";
                                    break;
                                case "I":
                                    stsRc = "Inactive";
                                    break;
                                case "D":
                                    stsRc = "Deleted";
                                    break;
                                default:
                                    stsRc = "Unknown";
                                    break;
                            }
                            rows = new object[] {
                                           false,
                                            sqlDR.GetInt32(iNIK).ToString(),
                                            sqlDR.GetString(iNama),
                                            sqlDR.GetString(iNickName)
                            };
                            dgvGrid.Rows.Add(rows);
                            if (stsRc == "Deleted")
                            {
                                dgvGrid.Rows[dgvGrid.Rows.GetLastRow(DataGridViewElementStates.Visible)].DefaultCellStyle.SelectionForeColor =
                                    dgvGrid.Rows[dgvGrid.Rows.GetLastRow(DataGridViewElementStates.Visible)].DefaultCellStyle.ForeColor = Color.Red;
                            }
                        } while (sqlDR.Read());

                        dgvGrid.PerformLayout();
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

        private void btnLoad_Click(object sender, EventArgs e)
        {
            btnSave.Enabled = LoadData();
        }

        private void btnGetPeriode_Click(object sender, EventArgs e)
        {
            Get_MonthPeriode(txtTahun.Text, cboBulan.SelectedIndex + 1);           
        }

    }
}
