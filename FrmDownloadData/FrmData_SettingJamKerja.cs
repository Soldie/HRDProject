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
    public partial class FrmData_SettingJamKerja : Form
    {
        List<DayOfWeek> hariOff = new List<DayOfWeek>();
        //List<DayOfWeek> hariPiket = new List<DayOfWeek>();

        private string
            JamMasuk = "08:30",
            JamKeluar = "21:00",
            JamToleransiMasuk,
            JamToleransiKeluar;

        private const string dbname = "HRD", Title = "Absensi",
            KdCabang = "AUR01",
            DISP_WAREHOUSE = "DisplayWarehouse",
            VAL_WAREHOUSE = "ValueWarehouse",
            DISP_DIVISI = "DisplayDivisi",
            VAL_DIVISI = "ValueDivisi",
            KODE_WAR_PENJUALAN = "PST";
        private const int JML_COL_TETAP = 4;
        private int idx_nik = 1, idx_pilihan = 0;
        //private string connstring = @"Data Source=ITSUPPORT\SQLEXPRESS2008R2;Database=" + dbname +
        //    ";Integrated Security=True;Connect Timeout=30; User Instance=False;"; //User ID=" + Uid + ";Password=" + Pwd;
        const int WORKSHEETSTARTROW = 5;
        const int WORKSHEETSTARTCOL = 4;
        private GlobalFunction gFunc = new GlobalFunction();

        public FrmData_SettingJamKerja()
        {
            InitializeComponent();


            txtJamKerjaMasuk0.Text = JamMasuk;
            txtJamKerjaKeluar0.Text = JamKeluar;
            txtJamKerjaMasuk1.Text = JamMasuk;
            txtJamKerjaKeluar1.Text = JamKeluar;
            txtJamKerjaMasuk2.Text = JamMasuk;
            txtJamKerjaKeluar2.Text = JamKeluar;
            txtJamKerjaMasuk3.Text = JamMasuk;
            txtJamKerjaKeluar3.Text = JamKeluar;
            txtJamKerjaMasuk4.Text = JamMasuk;
            txtJamKerjaKeluar4.Text = JamKeluar;
            txtJamKerjaMasuk5.Text = JamMasuk;
            txtJamKerjaKeluar5.Text = JamKeluar;
            txtJamKerjaMasuk6.Text = JamMasuk;
            txtJamKerjaKeluar6.Text = JamKeluar;

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
            if (dtpPeriodeAwal.Value.Month != (cboBulan.SelectedIndex + 1))
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

        private string GetJamMasukBerdasarkanHari(DateTime tgl)
        {
            switch(tgl.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    return txtJamKerjaMasuk0.Text;
                case DayOfWeek.Tuesday:
                    return txtJamKerjaMasuk1.Text;
                case DayOfWeek.Wednesday:
                    return txtJamKerjaMasuk2.Text;
                case DayOfWeek.Thursday:
                    return txtJamKerjaMasuk3.Text;
                case DayOfWeek.Friday:
                    return txtJamKerjaMasuk4.Text;
                case DayOfWeek.Saturday:
                    return txtJamKerjaMasuk5.Text;
                case DayOfWeek.Sunday:
                    return txtJamKerjaMasuk6.Text;
                default:
                    return "";
            }
        }

        private string GetJamKeluarBerdasarkanHari(DateTime tgl)
        {
            switch (tgl.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    return txtJamKerjaKeluar0.Text;
                case DayOfWeek.Tuesday:
                    return txtJamKerjaKeluar1.Text;
                case DayOfWeek.Wednesday:
                    return txtJamKerjaKeluar2.Text;
                case DayOfWeek.Thursday:
                    return txtJamKerjaKeluar3.Text;
                case DayOfWeek.Friday:
                    return txtJamKerjaKeluar4.Text;
                case DayOfWeek.Saturday:
                    return txtJamKerjaKeluar5.Text;
                case DayOfWeek.Sunday:
                    return txtJamKerjaKeluar6.Text;
                default:
                    return "";
            }
        }

        private string GetJamToleransiMasukBerdasarkanHari(DateTime tgl)
        {
            switch (tgl.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    return txtJamKerjaTolMasuk0.Text;
                case DayOfWeek.Tuesday:
                    return txtJamKerjaTolMasuk1.Text;
                case DayOfWeek.Wednesday:
                    return txtJamKerjaTolMasuk2.Text;
                case DayOfWeek.Thursday:
                    return txtJamKerjaTolMasuk3.Text;
                case DayOfWeek.Friday:
                    return txtJamKerjaTolMasuk4.Text;
                case DayOfWeek.Saturday:
                    return txtJamKerjaTolMasuk5.Text;
                case DayOfWeek.Sunday:
                    return txtJamKerjaTolMasuk6.Text;
                default:
                    return "";
            }
        }

        private string GetJamToleransiKeluarBerdasarkanHari(DateTime tgl)
        {
            switch (tgl.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    return txtJamKerjaTolKeluar0.Text;
                case DayOfWeek.Tuesday:
                    return txtJamKerjaTolKeluar1.Text;
                case DayOfWeek.Wednesday:
                    return txtJamKerjaTolKeluar2.Text;
                case DayOfWeek.Thursday:
                    return txtJamKerjaTolKeluar3.Text;
                case DayOfWeek.Friday:
                    return txtJamKerjaTolKeluar4.Text;
                case DayOfWeek.Saturday:
                    return txtJamKerjaTolKeluar5.Text;
                case DayOfWeek.Sunday:
                    return txtJamKerjaTolKeluar6.Text;
                default:
                    return "";
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
                    sqlCmd.CommandText = "Del_JadwalKerja";
                    sqlCmd.Parameters.Add("@NIK", SqlDbType.Int);
                    sqlCmd.Parameters.Add("@PeriodeAwal", SqlDbType.Date).Value = dtpPeriodeAwal.Value;
                    sqlCmd.Parameters.Add("@PeriodeAkhir", SqlDbType.Date).Value = dtpPeriodeAkhir.Value;
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

                    sqlCmd.Parameters.Clear();
                    sqlCmd.CommandText = "Del_JadwalOffKerja";
                    sqlCmd.Parameters.Add("@NIK", SqlDbType.Int);
                    sqlCmd.Parameters.Add("@PeriodeAwal", SqlDbType.Date).Value = dtpPeriodeAwal.Value;
                    sqlCmd.Parameters.Add("@PeriodeAkhir", SqlDbType.Date).Value = dtpPeriodeAkhir.Value;
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

                    sqlCmd.Parameters.Clear();
                    sqlCmd.CommandText = "INS_JadwalKerja";
                    sqlCmd.Parameters.Add("@NIK", SqlDbType.Int);
                    sqlCmd.Parameters.Add("@tgl", SqlDbType.Date);
                    sqlCmd.Parameters.Add("@jammasuk", SqlDbType.Time);
                    sqlCmd.Parameters.Add("@jamkeluar", SqlDbType.Time);
                    sqlCmd.Parameters.Add("@toleransimasuk", SqlDbType.Time);
                    sqlCmd.Parameters.Add("@toleransikeluar", SqlDbType.Time);

                    DateTime tglAkhir = dtpPeriodeAkhir.Value.Date;

                    for (DateTime tgl = dtpPeriodeAwal.Value.Date; tgl <= tglAkhir;)
                    {
                        //var isOff = hariOff.Contains(tgl.DayOfWeek);
                        //if (!isOff)
                        //{
                            //var isPiket = hariPiket.Contains(tgl.DayOfWeek);
                            //sqlCmd.Parameters["@tgl"].Value = tgl;
                            //sqlCmd.Parameters["@jammasuk"].Value = (isPiket ? txtJamPiketMasuk.Text : txtJamKerjaMasuk0.Text);
                            //sqlCmd.Parameters["@jamkeluar"].Value = (isPiket ? txtJamPiketKeluar.Text : txtJamKerjaKeluar0.Text);
                            //sqlCmd.Parameters["@toleransimasuk"].Value = (isPiket ? txtJamPiketTolMasuk.Text : txtJamKerjaTolMasuk0.Text);
                            //sqlCmd.Parameters["@toleransikeluar"].Value = (isPiket ? txtJamPiketTolKeluar.Text : txtJamKerjaTolKeluar0.Text);

                            sqlCmd.Parameters["@tgl"].Value = tgl;
                            sqlCmd.Parameters["@jammasuk"].Value = GetJamMasukBerdasarkanHari(tgl);
                            sqlCmd.Parameters["@jamkeluar"].Value = GetJamKeluarBerdasarkanHari(tgl);
                            sqlCmd.Parameters["@toleransimasuk"].Value = GetJamToleransiMasukBerdasarkanHari(tgl);
                            sqlCmd.Parameters["@toleransikeluar"].Value = GetJamToleransiKeluarBerdasarkanHari(tgl);

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
                        //}

                        tgl = tgl.AddDays(1);
                    }

                    sqlCmd.Parameters.Clear();
                    sqlCmd.CommandText = "INS_JadwalOffKerja";
                    sqlCmd.Parameters.Add("@NIK", SqlDbType.Int);
                    sqlCmd.Parameters.Add("@tgl", SqlDbType.Date);

                    tglAkhir = dtpPeriodeAkhir.Value.Date;
                    for (DateTime tgl = dtpPeriodeAwal.Value.Date; tgl <= tglAkhir;)
                    {
                        var isOff = hariOff.Contains(tgl.DayOfWeek);
                        if (isOff)
                        {
                            sqlCmd.Parameters["@tgl"].Value = tgl;
                            
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
                        }

                        tgl = tgl.AddDays(1);
                    }

                    sqlTrans.Commit();
                    MessageBox.Show("Data has been Saved!", Title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnLoad.PerformClick();
                }
            }
            catch (Exception exc)
            {
                if(sqlTrans.Connection != null)
                    sqlTrans.Rollback();

                MessageBox.Show("Cannot Save Data!" + Environment.NewLine + Environment.NewLine +
                    exc.Message, Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /*
        private bool ValidasiJamKerja()
        {
            if (!DateTime.TryParse(txtJamKerjaMasuk0.Text, out DateTime tmpJamKerjaMasuk))
            {
                MessageBox.Show("Invalid Jam");
                txtJamKerjaMasuk0.Focus();
                return false;
            }
            if (!DateTime.TryParse(txtJamKerjaKeluar0.Text, out DateTime tmpJamKerjaKeluar))
            {
                MessageBox.Show("Invalid Jam");
                txtJamKerjaKeluar0.Focus();
                return false;
            }
            if (!DateTime.TryParse(txtJamPiketMasuk.Text, out DateTime tmpJamPiketMasuk))
            {
                MessageBox.Show("Invalid Jam");
                txtJamPiketMasuk.Focus();
                return false;
            }
            if (!DateTime.TryParse(txtJamPiketKeluar.Text, out DateTime tmpJamPiketKeluar))
            {
                MessageBox.Show("Invalid Jam");
                txtJamPiketKeluar.Focus();
                return false;
            }

            if (txtJamKerjaMasuk0.Text.Length < 5)
            {
                MessageBox.Show("Invalid Jam");
                txtJamKerjaMasuk0.Focus();
                return false;
            }
            if (txtJamKerjaKeluar0.Text.Length < 5)
            {
                MessageBox.Show("Invalid Jam");
                txtJamKerjaKeluar0.Focus();
                return false;
            }
            if (txtJamPiketMasuk.Text.Length < 5)
            {
                MessageBox.Show("Invalid Jam");
                txtJamPiketMasuk.Focus();
                return false;
            }
            if (txtJamPiketKeluar.Text.Length < 5)
            {
                MessageBox.Show("Invalid Jam");
                txtJamPiketKeluar.Focus();
                return false;
            }

            JamToleransiMasuk = txtJamKerjaMasuk0.Text;
            JamToleransiKeluar = txtJamKerjaKeluar0.Text;
            JamPiketToleransiMasuk = txtJamPiketMasuk.Text;
            JamPiketToleransiKeluar = txtJamPiketKeluar.Text;

            if (string.IsNullOrWhiteSpace(txtJamKerjaTolMasuk0.Text))
                txtJamKerjaTolMasuk0.Text = JamToleransiMasuk;
            if (string.IsNullOrWhiteSpace(txtJamKerjaTolKeluar0.Text))
                txtJamKerjaTolKeluar0.Text = JamToleransiKeluar;
            if (string.IsNullOrWhiteSpace(txtJamPiketTolMasuk.Text))
                txtJamPiketTolMasuk.Text = JamPiketToleransiMasuk;
            if (string.IsNullOrWhiteSpace(txtJamPiketTolKeluar.Text))
                txtJamPiketTolKeluar.Text = JamPiketToleransiKeluar;

            if (!DateTime.TryParse(txtJamKerjaTolMasuk0.Text, out DateTime tmpJamKerjaTolMasuk))
            {
                MessageBox.Show("Invalid Jam");
                txtJamKerjaTolMasuk0.Focus();
                return false;
            }
            if (!DateTime.TryParse(txtJamKerjaTolKeluar0.Text, out DateTime tmpJamKerjaTolKeluar))
            {
                MessageBox.Show("Invalid Jam");
                txtJamKerjaTolKeluar0.Focus();
                return false;
            }
            if (!DateTime.TryParse(txtJamPiketTolMasuk.Text, out DateTime tmpJamPiketTolMasuk))
            {
                MessageBox.Show("Invalid Jam");
                txtJamPiketTolMasuk.Focus();
                return false;
            }
            if (!DateTime.TryParse(txtJamPiketTolKeluar.Text, out DateTime tmpJamPiketTolKeluar))
            {
                MessageBox.Show("Invalid Jam");
                txtJamPiketTolKeluar.Focus();
                return false;
            }

            if (txtJamKerjaTolMasuk0.Text.Length < 5)
            {
                MessageBox.Show("Invalid Jam");
                txtJamKerjaTolMasuk0.Focus();
                return false;
            }
            if (txtJamKerjaTolKeluar0.Text.Length < 5)
            {
                MessageBox.Show("Invalid Jam");
                txtJamKerjaTolKeluar0.Focus();
                return false;
            }
            if (txtJamPiketTolMasuk.Text.Length < 5)
            {
                MessageBox.Show("Invalid Jam");
                txtJamPiketTolMasuk.Focus();
                return false;
            }
            if (txtJamPiketTolKeluar.Text.Length < 5)
            {
                MessageBox.Show("Invalid Jam");
                txtJamPiketTolKeluar.Focus();
                return false;
            }
            return true;
        }
        */

        private bool ValidasiJamKerja(TextBox txtJamKerjaMasuk, TextBox txtJamKerjaKeluar, 
            TextBox txtJamKerjaTolMasuk, TextBox txtJamKerjaTolKeluar)
        {
            if (!DateTime.TryParse(txtJamKerjaMasuk.Text, out DateTime tmpJamKerjaMasuk))
            {
                MessageBox.Show("Invalid Jam");
                txtJamKerjaMasuk.Focus();
                return false;
            }
            if (!DateTime.TryParse(txtJamKerjaKeluar.Text, out DateTime tmpJamKerjaKeluar))
            {
                MessageBox.Show("Invalid Jam");
                txtJamKerjaKeluar.Focus();
                return false;
            }

            if (txtJamKerjaMasuk.Text.Length < 5)
            {
                MessageBox.Show("Invalid Jam");
                txtJamKerjaMasuk.Focus();
                return false;
            }
            if (txtJamKerjaKeluar.Text.Length < 5)
            {
                MessageBox.Show("Invalid Jam");
                txtJamKerjaKeluar.Focus();
                return false;
            }

            JamToleransiMasuk = txtJamKerjaMasuk.Text;
            JamToleransiKeluar = txtJamKerjaKeluar.Text;

            if (string.IsNullOrWhiteSpace(txtJamKerjaTolMasuk.Text))
                txtJamKerjaTolMasuk.Text = JamToleransiMasuk;
            if (string.IsNullOrWhiteSpace(txtJamKerjaTolKeluar.Text))
                txtJamKerjaTolKeluar.Text = JamToleransiKeluar;

            if (!DateTime.TryParse(txtJamKerjaTolMasuk.Text, out DateTime tmpJamKerjaTolMasuk))
            {
                MessageBox.Show("Invalid Jam");
                txtJamKerjaTolMasuk.Focus();
                return false;
            }
            if (!DateTime.TryParse(txtJamKerjaTolKeluar.Text, out DateTime tmpJamKerjaTolKeluar))
            {
                MessageBox.Show("Invalid Jam");
                txtJamKerjaTolKeluar.Focus();
                return false;
            }

            if (txtJamKerjaTolMasuk.Text.Length < 5)
            {
                MessageBox.Show("Invalid Jam");
                txtJamKerjaTolMasuk.Focus();
                return false;
            }
            if (txtJamKerjaTolKeluar.Text.Length < 5)
            {
                MessageBox.Show("Invalid Jam");
                txtJamKerjaTolKeluar.Focus();
                return false;
            }

            return true;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            if (!ValidasiJamKerja(txtJamKerjaMasuk0, txtJamKerjaKeluar0, txtJamKerjaTolMasuk0, txtJamKerjaTolKeluar0)) return;
            if (!ValidasiJamKerja(txtJamKerjaMasuk1, txtJamKerjaKeluar1, txtJamKerjaTolMasuk1, txtJamKerjaTolKeluar1)) return;
            if (!ValidasiJamKerja(txtJamKerjaMasuk2, txtJamKerjaKeluar2, txtJamKerjaTolMasuk2, txtJamKerjaTolKeluar2)) return;
            if (!ValidasiJamKerja(txtJamKerjaMasuk3, txtJamKerjaKeluar3, txtJamKerjaTolMasuk3, txtJamKerjaTolKeluar3)) return;
            if (!ValidasiJamKerja(txtJamKerjaMasuk4, txtJamKerjaKeluar4, txtJamKerjaTolMasuk4, txtJamKerjaTolKeluar4)) return;
            if (!ValidasiJamKerja(txtJamKerjaMasuk5, txtJamKerjaKeluar5, txtJamKerjaTolMasuk5, txtJamKerjaTolKeluar5)) return;
            if (!ValidasiJamKerja(txtJamKerjaMasuk6, txtJamKerjaKeluar6, txtJamKerjaTolMasuk6, txtJamKerjaTolKeluar6)) return;

            hariOff.Clear();
            if (chkHari0.Checked)
                hariOff.Add(DayOfWeek.Monday);
            if (chkHari1.Checked)
                hariOff.Add(DayOfWeek.Tuesday);
            if (chkHari2.Checked)
                hariOff.Add(DayOfWeek.Wednesday);
            if (chkHari3.Checked)
                hariOff.Add(DayOfWeek.Thursday);
            if (chkHari4.Checked)
                hariOff.Add(DayOfWeek.Friday);
            if (chkHari5.Checked)
                hariOff.Add(DayOfWeek.Saturday);
            if (chkHari6.Checked)
                hariOff.Add(DayOfWeek.Sunday);
            
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
            //else
            //{
            //    MessageBox.Show("Pilih Hari Off, minimal satu hari!", Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}

            dr = MessageBox.Show("Are you sure want to save this data ?", Title, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (dr == DialogResult.No)
            {
                dgvGrid.Focus();
                return;
            }

            SaveDataDetail(tmpString.ToString());
            this.Close();
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
