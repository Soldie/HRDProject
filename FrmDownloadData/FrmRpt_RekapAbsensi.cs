using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace HRDProject
{
    public partial class FrmRpt_RekapAbsensi : Form
    {
        private const string STAT_KERJA = "M", STAT_ALPA = "A", STAT_IJIN = "I", STAT_SAKIT = "S",
            STAT_OFFMASUK = "OM", STAT_OFF = "O", STAT_PULANGCEPAT = "PC",
            STAT_TELAT = "TA", STAT_TIDAKABSEN = "PTA";

        private const string dbname = "HRD", Title = "Absensi",
            KdCabang = "AUR01",
            DISP_WAREHOUSE = "DisplayWarehouse",
            VAL_WAREHOUSE = "ValueWarehouse",
            DISP_DIVISI = "DisplayDivisi",
            VAL_DIVISI = "ValueDivisi", 
            KODE_WAR_PENJUALAN = "PST";
        private const int 
            JML_COL_TETAP_AKHIR = 5,
            JML_COL_TETAP_AWAL = 2,
            JML_HARI_DALAM_SEMINGGU = 7;
        private int
            idx_nik = 0,
            idx_nama = 1, 
            idx_harikerja, 
            idx_harikerja2,
            idx_sakit,
            idx_totalbonus,
            idx_lain2, 
            idx_keterangan;
        private string connstring = @"Data Source=ITSUPPORT\SQLEXPRESS2008R2;Database=" + dbname +
            ";Integrated Security=True;Connect Timeout=30; User Instance=False;"; //User ID=" + Uid + ";Password=" + Pwd;
        const decimal POTONGAN_PERHARI = 20000,
            TAMBAHAN_FULL = 100000;
        const int WORKSHEETSTARTROW = 5;
        const int WORKSHEETSTARTCOL = 4;
        private GlobalFunction gFunc = new GlobalFunction();
        int tahun, bulan, nik;
        bool isErr;

        private void dgvGrid_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            var grid = sender as DataGridView;
            var rowIdx = (e.RowIndex + 1).ToString();

            var centerFormat = new StringFormat()
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };

            var headerBounds = new Rectangle(e.RowBounds.Left, e.RowBounds.Top, grid.RowHeadersWidth, e.RowBounds.Height);
            e.Graphics.DrawString(rowIdx, new Font("Tahoma", 8, FontStyle.Bold), SystemBrushes.ControlText, headerBounds, centerFormat);
        }

        public FrmRpt_RekapAbsensi()
        {
            InitializeComponent();

            dgvGrid.RowHeadersVisible = true;
            dgvGrid.RowHeadersDefaultCellStyle.Padding = new Padding(dgvGrid.RowHeadersWidth);
            dgvGrid.RowPostPaint += new DataGridViewRowPostPaintEventHandler(dgvGrid_RowPostPaint);

            LoadDataCombo();

            dgvGrid.AllowUserToOrderColumns = false;
            dgvGrid.AllowUserToAddRows = false;
            btnFormatGrid.Enabled = btnExport.Enabled = btnSave.Enabled = false;

            txtTahun.Text = DateTime.Now.Year.ToString();

            for(int i=1; i<=12; i++)
                cboBulan.Items.Add(DateTimeFormatInfo.CurrentInfo.GetMonthName(i));
            //cboBulan.Items.AddRange(DateTimeFormatInfo.CurrentInfo.MonthNames);

            cboBulan.SelectedIndex = DateTime.Now.Month - 1;

            Get_MonthPeriode(txtTahun.Text, cboBulan.SelectedIndex + 1);                        
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

                        arrWarehouse.Add(new ArrayWarehouse("all", "ALL"));
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

        private void LoadDataGridview(DataGridView dgvGrid, DateTimePicker dtpPeriodeAwal, DateTimePicker dtpPeriodeAkhir, out int idx_hariKerja)
        {
            idx_hariKerja = 0;
            try
            {
                using (SqlConnection sqlCnn = new SqlConnection(ZFame.Classes.clsVarProgram.DB_CONN_STRING))
                {
                    SqlCommand sqlCmd = new SqlCommand();
                    sqlCmd.Connection = sqlCnn;
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.CommandText = "rpt_rekapabsensi";
                    sqlCmd.Parameters.Add("@PeriodeAwal", SqlDbType.Date).Value = dtpPeriodeAwal.Value.ToString();
                    sqlCmd.Parameters.Add("@PeriodeAkhir", SqlDbType.Date).Value = dtpPeriodeAkhir.Value.ToString();
                    sqlCmd.Parameters.Add("@kdcabang", SqlDbType.VarChar, 5).Value = KdCabang;
                    if (cboWarehouse.SelectedIndex > 0)
                        sqlCmd.Parameters.Add("@kdwarehouse", SqlDbType.VarChar, 3).Value = cboWarehouse.SelectedValue.ToString();
                    sqlCmd.Parameters.Add("@divisi", SqlDbType.SmallInt).Value = cboDivisi.SelectedValue.ToString();
                    sqlCmd.Parameters.Add("@viewNickName", SqlDbType.Bit).Value = chkViewNickName.Checked;

                    sqlCnn.Open();

                    SqlDataAdapter sda = new SqlDataAdapter(sqlCmd);
                    DataTable data = new DataTable();
                    sda.Fill(data);

                    dgvGrid.DataSource = null;
                    dgvGrid.Columns.Clear();
                    dgvGrid.DataSource = data;

                    dgvGrid.Columns.RemoveAt(dgvGrid.Columns.Count - 1);
                    dgvGrid.Columns.RemoveAt(dgvGrid.Columns.Count - 1);

                    dgvGrid.Columns[idx_nama].Frozen = true;

                    dgvGrid.Columns.Add("_jmlharikerja", "Jumlah Hari Kerja");
                    idx_hariKerja = dgvGrid.Columns.Count - 1;

                    btnFormatGrid.Enabled = true;
                    btnExport.Enabled = btnSave.Enabled = false;

                    foreach (DataGridViewColumn column in dgvGrid.Columns)
                    {
                        column.SortMode = DataGridViewColumnSortMode.NotSortable;
                        column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    }

                    #region ABSEN 1 KALI = VALUE 2
                    sqlCmd.Parameters.RemoveAt(sqlCmd.Parameters.Count-1);
                    sqlCmd.CommandText = "rpt_rekapabsensi_onefingerprint";
                    SqlDataReader dr = sqlCmd.ExecuteReader();

                    int iNIK = 0, iTgl = 0, nik_val = 0;
                    DateTime clock_date;

                    if (dr.Read())
                    {
                        iNIK = dr.GetOrdinal("nik");
                        iTgl = dr.GetOrdinal("clock_date");

                        do
                        {
                            nik_val = dr.GetInt32(iNIK);
                            clock_date = dr.GetDateTime(iTgl);

                            foreach (DataGridViewRow row in dgvGrid.Rows)
                            {
                                if (!row.IsNewRow)
                                {
                                    if (int.Parse(row.Cells[idx_nik].Value.ToString()) == nik_val)
                                    {
                                        for (int i = JML_COL_TETAP_AWAL; i < idx_hariKerja; i++)
                                        {
                                            if (dgvGrid.Columns[i].Name == dtpPeriodeAkhir.Value.ToString("yyyy-MM-dd"))
                                            {
                                                if (chkAnggapMasuk.Checked)
                                                {
                                                    row.Cells[i].Value = 1; //dianggap masuk
                                                    break;
                                                }
                                                else
                                                {
                                                    row.Cells[i].Value = 2;
                                                    break;
                                                }
                                            }
                                            else
                                            if (dgvGrid.Columns[i].Name == clock_date.ToString("yyyy-MM-dd"))
                                            {
                                                row.Cells[i].Value = 2;
                                                break;
                                            }
                                        }
                                        break;
                                    }
                                }
                            }
                        } while (dr.Read());
                    }

                    dr.Close();
                    #endregion

                    #region TELAT = VALUE 3
                    sqlCmd.CommandText = "rpt_rekapabsensi_telat";
                    dr = sqlCmd.ExecuteReader();

                    if (dr.Read())
                    {
                        iNIK = dr.GetOrdinal("nik");
                        iTgl = dr.GetOrdinal("clock_date");

                        do
                        {
                            nik_val = dr.GetInt32(iNIK);
                            clock_date = dr.GetDateTime(iTgl);

                            foreach (DataGridViewRow row in dgvGrid.Rows)
                            {
                                if (!row.IsNewRow)
                                {
                                    if (int.Parse(row.Cells[idx_nik].Value.ToString()) == nik_val)
                                    {
                                        for (int i = JML_COL_TETAP_AWAL; i < idx_hariKerja; i++)
                                        {
                                            if (dgvGrid.Columns[i].Name == dtpPeriodeAkhir.Value.ToString("yyyy-MM-dd"))
                                            {
                                                if (chkAnggapMasuk.Checked)
                                                {
                                                    row.Cells[i].Value = 1; //dianggap masuk
                                                    break;
                                                }
                                                else
                                                {
                                                    if (row.Cells[i].Value.ToString() != "2")
                                                        if (chkShowTelat.Checked)
                                                            row.Cells[i].Value = 3;
                                                    break;
                                                }
                                            }
                                            else
                                            if (dgvGrid.Columns[i].Name == clock_date.ToString("yyyy-MM-dd"))
                                            {
                                                if (row.Cells[i].Value.ToString() != "2")
                                                    if(chkShowTelat.Checked)
                                                        row.Cells[i].Value = 3;
                                                break;
                                            }
                                        }
                                        break;
                                    }
                                }
                            }
                        } while (dr.Read());
                    }

                    dr.Close();
                    #endregion

                    #region PULANG CEPAT = VALUE 4
                    sqlCmd.CommandText = "rpt_rekapabsensi_pulangcepat";
                    dr = sqlCmd.ExecuteReader();

                    if (dr.Read())
                    {
                        iNIK = dr.GetOrdinal("nik");
                        iTgl = dr.GetOrdinal("clock_date");

                        do
                        {
                            nik_val = dr.GetInt32(iNIK);
                            clock_date = dr.GetDateTime(iTgl);

                            foreach (DataGridViewRow row in dgvGrid.Rows)
                            {
                                if (!row.IsNewRow)
                                {
                                    if (int.Parse(row.Cells[idx_nik].Value.ToString()) == nik_val)
                                    {
                                        for (int i = JML_COL_TETAP_AWAL; i < idx_hariKerja; i++)
                                        {
                                            if (dgvGrid.Columns[i].Name == dtpPeriodeAkhir.Value.ToString("yyyy-MM-dd"))
                                            {
                                                if (chkAnggapMasuk.Checked)
                                                {
                                                    row.Cells[i].Value = 1; //dianggap masuk
                                                    break;
                                                }
                                                else
                                                {
                                                    if (row.Cells[i].Value.ToString() != "2")
                                                        row.Cells[i].Value = 4;
                                                    break;
                                                }
                                            }
                                            else
                                                if (dgvGrid.Columns[i].Name == clock_date.ToString("yyyy-MM-dd"))
                                                {
                                                    if (row.Cells[i].Value.ToString() != "2")
                                                        row.Cells[i].Value = 4;
                                                    break;
                                                }
                                        }
                                        break;
                                    }
                                }
                            }
                        } while (dr.Read());
                    }

                    dr.Close();
                    #endregion

                    #region IJIN SAKIT = 6
                    sqlCmd.CommandText = "rpt_rekapabsensi_ijin_sakit";
                    dr = sqlCmd.ExecuteReader();

                    if (dr.Read())
                    {
                        iNIK = dr.GetOrdinal("nik");
                        iTgl = dr.GetOrdinal("clock_date");

                        do
                        {
                            nik_val = dr.GetInt32(iNIK);
                            clock_date = dr.GetDateTime(iTgl);

                            foreach (DataGridViewRow row in dgvGrid.Rows)
                            {
                                if (!row.IsNewRow)
                                {
                                    if (int.Parse(row.Cells[idx_nik].Value.ToString()) == nik_val)
                                    {
                                        for (int i = JML_COL_TETAP_AWAL; i < idx_hariKerja; i++)
                                        {
                                            if (dgvGrid.Columns[i].Name == clock_date.ToString("yyyy-MM-dd"))
                                            {
                                                //if (string.IsNullOrWhiteSpace(row.Cells[i].Value.ToString()) || row.Cells[i].Value.ToString() == "0")
                                                row.Cells[i].Value = 6;
                                                break;
                                            }
                                        }
                                        break;
                                    }
                                }
                            }
                        } while (dr.Read());
                    }

                    dr.Close();
                    #endregion

                    #region IJIN LAIN2 = 8
                    sqlCmd.CommandText = "rpt_rekapabsensi_ijin_lain2";
                    dr = sqlCmd.ExecuteReader();

                    if (dr.Read())
                    {
                        iNIK = dr.GetOrdinal("nik");
                        iTgl = dr.GetOrdinal("clock_date");

                        do
                        {
                            nik_val = dr.GetInt32(iNIK);
                            clock_date = dr.GetDateTime(iTgl);

                            foreach (DataGridViewRow row in dgvGrid.Rows)
                            {
                                if (!row.IsNewRow)
                                {
                                    if (int.Parse(row.Cells[idx_nik].Value.ToString()) == nik_val)
                                    {
                                        for (int i = JML_COL_TETAP_AWAL; i < idx_hariKerja; i++)
                                        {
                                            if (dgvGrid.Columns[i].Name == clock_date.ToString("yyyy-MM-dd"))
                                            {
                                                //if (string.IsNullOrWhiteSpace(row.Cells[i].Value.ToString()) || row.Cells[i].Value.ToString() == "0")
                                                row.Cells[i].Value = 8;
                                                break;
                                            }
                                        }
                                        break;
                                    }
                                }
                            }
                        } while (dr.Read());
                    }

                    dr.Close();
                    #endregion

                    #region OFF = VALUE 5
                    sqlCmd.CommandText = "rpt_rekapabsensi_off";
                    dr = sqlCmd.ExecuteReader();

                    if (dr.Read())
                    {
                        iNIK = dr.GetOrdinal("nik");
                        iTgl = dr.GetOrdinal("clock_date");

                        do
                        {
                            nik_val = dr.GetInt32(iNIK);
                            clock_date = dr.GetDateTime(iTgl);

                            foreach (DataGridViewRow row in dgvGrid.Rows)
                            {
                                if (!row.IsNewRow)
                                {
                                    if (int.Parse(row.Cells[idx_nik].Value.ToString()) == nik_val)
                                    {
                                        for (int i = JML_COL_TETAP_AWAL; i < idx_hariKerja; i++)
                                        {
                                            if (dgvGrid.Columns[i].Name == clock_date.ToString("yyyy-MM-dd"))
                                            {
                                                if (row.Cells[i].Value.ToString() != "1")
                                                    row.Cells[i].Value = 5;
                                                else
                                                    if (row.Cells[i].Value.ToString() == "1")
                                                        row.Cells[i].Value = 7;
                                                break;
                                            }
                                        }
                                        break;
                                    }
                                }
                            }
                        } while (dr.Read());
                    }

                    dr.Close();
                    #endregion

                }
            }
            catch (Exception exc)
            {
                btnFormatGrid.Enabled = btnExport.Enabled = btnSave.Enabled = false;
                MessageBox.Show("Cannot Load Data!" + Environment.NewLine + Environment.NewLine +
                    exc.Message, Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadDataGridviewByJadwal(DataGridView dgvGrid, DateTimePicker dtpPeriodeAwal, DateTimePicker dtpPeriodeAkhir, out int idx_hariKerja)
        {
            idx_hariKerja = 0;
            try
            {
                using (SqlConnection sqlCnn = new SqlConnection(ZFame.Classes.clsVarProgram.DB_CONN_STRING))
                {
                    SqlCommand sqlCmd = new SqlCommand();
                    sqlCmd.Connection = sqlCnn;
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.CommandText = "rpt_rekapabsensibyjadwal";
                    sqlCmd.Parameters.Add("@PeriodeAwal", SqlDbType.Date).Value = dtpPeriodeAwal.Value.ToString();
                    sqlCmd.Parameters.Add("@PeriodeAkhir", SqlDbType.Date).Value = dtpPeriodeAkhir.Value.ToString();
                    sqlCmd.Parameters.Add("@kdcabang", SqlDbType.VarChar, 5).Value = KdCabang;
                    if (cboWarehouse.SelectedIndex > 0)
                        sqlCmd.Parameters.Add("@kdwarehouse", SqlDbType.VarChar, 3).Value = cboWarehouse.SelectedValue.ToString();
                    sqlCmd.Parameters.Add("@divisi", SqlDbType.SmallInt).Value = cboDivisi.SelectedValue.ToString();
                    sqlCmd.Parameters.Add("@viewNickName", SqlDbType.Bit).Value = chkViewNickName.Checked;

                    sqlCnn.Open();

                    SqlDataAdapter sda = new SqlDataAdapter(sqlCmd);
                    DataTable data = new DataTable();
                    sda.Fill(data);

                    dgvGrid.DataSource = null;
                    dgvGrid.Columns.Clear();
                    dgvGrid.DataSource = data;

                    dgvGrid.Columns.RemoveAt(dgvGrid.Columns.Count - 1);
                    dgvGrid.Columns.RemoveAt(dgvGrid.Columns.Count - 1);

                    dgvGrid.Columns[idx_nama].Frozen = true;

                    dgvGrid.Columns.Add("_jmlharikerja", "Jumlah Hari Kerja");
                    idx_hariKerja = dgvGrid.Columns.Count - 1;

                    btnFormatGrid.Enabled = true;
                    btnExport.Enabled = btnSave.Enabled = false;

                    foreach (DataGridViewColumn column in dgvGrid.Columns)
                    {
                        column.SortMode = DataGridViewColumnSortMode.NotSortable;
                        column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    }

                    #region ABSEN 1 KALI = VALUE 2
                    sqlCmd.Parameters.RemoveAt(sqlCmd.Parameters.Count - 1);
                    sqlCmd.CommandText = "rpt_rekapabsensi_onefingerprint";
                    SqlDataReader dr = sqlCmd.ExecuteReader();

                    int iNIK = 0, iTgl = 0, nik_val = 0;
                    DateTime clock_date;

                    if (dr.Read())
                    {
                        iNIK = dr.GetOrdinal("nik");
                        iTgl = dr.GetOrdinal("clock_date");

                        do
                        {
                            nik_val = dr.GetInt32(iNIK);
                            clock_date = dr.GetDateTime(iTgl);

                            foreach (DataGridViewRow row in dgvGrid.Rows)
                            {
                                if (!row.IsNewRow)
                                {
                                    if (int.Parse(row.Cells[idx_nik].Value.ToString()) == nik_val)
                                    {
                                        for (int i = JML_COL_TETAP_AWAL; i < idx_hariKerja; i++)
                                        {
                                            if (dgvGrid.Columns[i].Name == dtpPeriodeAkhir.Value.ToString("yyyy-MM-dd"))
                                            {
                                                if (chkAnggapMasuk.Checked)
                                                {
                                                    row.Cells[i].Value = 1; //dianggap masuk
                                                    break;
                                                }
                                                else
                                                {
                                                    row.Cells[i].Value = 2;
                                                    break;
                                                }
                                            }
                                            else
                                            if (dgvGrid.Columns[i].Name == clock_date.ToString("yyyy-MM-dd"))
                                            {
                                                row.Cells[i].Value = 2;
                                                break;
                                            }
                                        }
                                        break;
                                    }
                                }
                            }
                        } while (dr.Read());
                    }

                    dr.Close();
                    #endregion

                    #region TELAT = VALUE 3
                    sqlCmd.CommandText = "rpt_rekapabsensibyjadwal_telat";
                    dr = sqlCmd.ExecuteReader();

                    if (dr.Read())
                    {
                        iNIK = dr.GetOrdinal("nik");
                        iTgl = dr.GetOrdinal("clock_date");

                        do
                        {
                            nik_val = dr.GetInt32(iNIK);
                            clock_date = dr.GetDateTime(iTgl);

                            foreach (DataGridViewRow row in dgvGrid.Rows)
                            {
                                if (!row.IsNewRow)
                                {
                                    if (int.Parse(row.Cells[idx_nik].Value.ToString()) == nik_val)
                                    {
                                        for (int i = JML_COL_TETAP_AWAL; i < idx_hariKerja; i++)
                                        {
                                            if (dgvGrid.Columns[i].Name == dtpPeriodeAkhir.Value.ToString("yyyy-MM-dd"))
                                            {
                                                if (chkAnggapMasuk.Checked)
                                                {
                                                    row.Cells[i].Value = 1; //dianggap masuk
                                                    break;
                                                }
                                                else
                                                {
                                                    if (row.Cells[i].Value.ToString() != "2")
                                                        if (chkShowTelat.Checked)
                                                            row.Cells[i].Value = 3;
                                                    break;
                                                }
                                            }
                                            else
                                            if (dgvGrid.Columns[i].Name == clock_date.ToString("yyyy-MM-dd"))
                                            {
                                                if (row.Cells[i].Value.ToString() != "2")
                                                    if (chkShowTelat.Checked)
                                                        row.Cells[i].Value = 3;
                                                break;
                                            }
                                        }
                                        break;
                                    }
                                }
                            }
                        } while (dr.Read());
                    }

                    dr.Close();
                    #endregion

                    #region PULANG CEPAT = VALUE 4
                    sqlCmd.CommandText = "rpt_rekapabsensi_pulangcepat";
                    dr = sqlCmd.ExecuteReader();

                    if (dr.Read())
                    {
                        iNIK = dr.GetOrdinal("nik");
                        iTgl = dr.GetOrdinal("clock_date");

                        do
                        {
                            nik_val = dr.GetInt32(iNIK);
                            clock_date = dr.GetDateTime(iTgl);

                            foreach (DataGridViewRow row in dgvGrid.Rows)
                            {
                                if (!row.IsNewRow)
                                {
                                    if (int.Parse(row.Cells[idx_nik].Value.ToString()) == nik_val)
                                    {
                                        for (int i = JML_COL_TETAP_AWAL; i < idx_hariKerja; i++)
                                        {
                                            if (dgvGrid.Columns[i].Name == dtpPeriodeAkhir.Value.ToString("yyyy-MM-dd"))
                                            {
                                                if (chkAnggapMasuk.Checked)
                                                {
                                                    row.Cells[i].Value = 1; //dianggap masuk
                                                    break;
                                                }
                                                else
                                                {
                                                    if (row.Cells[i].Value.ToString() != "2")
                                                        row.Cells[i].Value = 4;
                                                    break;
                                                }
                                            }
                                            else
                                                if (dgvGrid.Columns[i].Name == clock_date.ToString("yyyy-MM-dd"))
                                            {
                                                if (row.Cells[i].Value.ToString() != "2")
                                                    row.Cells[i].Value = 4;
                                                break;
                                            }
                                        }
                                        break;
                                    }
                                }
                            }
                        } while (dr.Read());
                    }

                    dr.Close();
                    #endregion

                    #region IJIN SAKIT = 6
                    sqlCmd.CommandText = "rpt_rekapabsensi_ijin_sakit";
                    dr = sqlCmd.ExecuteReader();

                    if (dr.Read())
                    {
                        iNIK = dr.GetOrdinal("nik");
                        iTgl = dr.GetOrdinal("clock_date");

                        do
                        {
                            nik_val = dr.GetInt32(iNIK);
                            clock_date = dr.GetDateTime(iTgl);

                            foreach (DataGridViewRow row in dgvGrid.Rows)
                            {
                                if (!row.IsNewRow)
                                {
                                    if (int.Parse(row.Cells[idx_nik].Value.ToString()) == nik_val)
                                    {
                                        for (int i = JML_COL_TETAP_AWAL; i < idx_hariKerja; i++)
                                        {
                                            if (dgvGrid.Columns[i].Name == clock_date.ToString("yyyy-MM-dd"))
                                            {
                                                //if (string.IsNullOrWhiteSpace(row.Cells[i].Value.ToString()) || row.Cells[i].Value.ToString() == "0")
                                                row.Cells[i].Value = 6;
                                                break;
                                            }
                                        }
                                        break;
                                    }
                                }
                            }
                        } while (dr.Read());
                    }

                    dr.Close();
                    #endregion

                    #region IJIN LAIN2 = 8
                    sqlCmd.CommandText = "rpt_rekapabsensi_ijin_lain2";
                    dr = sqlCmd.ExecuteReader();

                    if (dr.Read())
                    {
                        iNIK = dr.GetOrdinal("nik");
                        iTgl = dr.GetOrdinal("clock_date");

                        do
                        {
                            nik_val = dr.GetInt32(iNIK);
                            clock_date = dr.GetDateTime(iTgl);

                            foreach (DataGridViewRow row in dgvGrid.Rows)
                            {
                                if (!row.IsNewRow)
                                {
                                    if (int.Parse(row.Cells[idx_nik].Value.ToString()) == nik_val)
                                    {
                                        for (int i = JML_COL_TETAP_AWAL; i < idx_hariKerja; i++)
                                        {
                                            if (dgvGrid.Columns[i].Name == clock_date.ToString("yyyy-MM-dd"))
                                            {
                                                //if (string.IsNullOrWhiteSpace(row.Cells[i].Value.ToString()) || row.Cells[i].Value.ToString() == "0")
                                                row.Cells[i].Value = 8;
                                                break;
                                            }
                                        }
                                        break;
                                    }
                                }
                            }
                        } while (dr.Read());
                    }

                    dr.Close();
                    #endregion

                    #region OFF = VALUE 5
                    sqlCmd.CommandText = "rpt_rekapabsensibyjadwal_off";
                    dr = sqlCmd.ExecuteReader();

                    if (dr.Read())
                    {
                        iNIK = dr.GetOrdinal("nik");
                        iTgl = dr.GetOrdinal("clock_date");

                        do
                        {
                            nik_val = dr.GetInt32(iNIK);
                            clock_date = dr.GetDateTime(iTgl);

                            foreach (DataGridViewRow row in dgvGrid.Rows)
                            {
                                if (!row.IsNewRow)
                                {
                                    if (int.Parse(row.Cells[idx_nik].Value.ToString()) == nik_val)
                                    {
                                        for (int i = JML_COL_TETAP_AWAL; i < idx_hariKerja; i++)
                                        {
                                            if (dgvGrid.Columns[i].Name == clock_date.ToString("yyyy-MM-dd"))
                                            {
                                                if (row.Cells[i].Value.ToString() != "1")
                                                    row.Cells[i].Value = 5;
                                                else
                                                    if (row.Cells[i].Value.ToString() == "1")
                                                    row.Cells[i].Value = 7;
                                                break;
                                            }
                                        }
                                        break;
                                    }
                                }
                            }
                        } while (dr.Read());
                    }

                    dr.Close();
                    #endregion

                }
            }
            catch (Exception exc)
            {
                btnFormatGrid.Enabled = btnExport.Enabled = btnSave.Enabled = false;
                MessageBox.Show("Cannot Load Data!" + Environment.NewLine + Environment.NewLine +
                    exc.Message, Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            //LoadDataGridview(dgvGrid, dtpPeriodeAwal, dtpPeriodeAkhir, out idx_harikerja);
            LoadDataGridviewByJadwal(dgvGrid, dtpPeriodeAwal, dtpPeriodeAkhir, out idx_harikerja);
            //LoadDataGridview(dgvGrid2, dtpPeriodeAwal, dtpPeriodeAkhir, out idx_harikerja2);
        }

        private void copyAlltoClipboard()
        {

            dgvGrid.SelectAll();
            DataObject dataObj = dgvGrid.GetClipboardContent();
            if (dataObj != null)
                Clipboard.SetDataObject(dataObj);
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            Export2Excel();
        }

        private void Export2ExcelByCopyOnly()
        {
            copyAlltoClipboard();
            Microsoft.Office.Interop.Excel.Application xlexcel;
            Microsoft.Office.Interop.Excel.Workbook xlWorkBook;
            Microsoft.Office.Interop.Excel.Worksheet xlWorkSheet;
            object misValue = System.Reflection.Missing.Value;
            xlexcel = new Microsoft.Office.Interop.Excel.Application();
            xlexcel.Visible = true;
            xlWorkBook = xlexcel.Workbooks.Add(misValue);
            xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
            Microsoft.Office.Interop.Excel.Range CR = (Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Cells[1, 1];
            CR.Select();
            xlWorkSheet.PasteSpecial(CR, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, true);
        }

        private void Export2Excel()
        {
            DateTime tgl;
            var excelApp = new Microsoft.Office.Interop.Excel.Application();
            excelApp.Visible = true;
            Microsoft.Office.Interop.Excel.Workbook excelbk = excelApp.Workbooks.Add(Type.Missing);
            Microsoft.Office.Interop.Excel.Worksheet xlWorkSheet1 = (Microsoft.Office.Interop.Excel.Worksheet)excelbk.Worksheets["Sheet1"];

            int worksheetcol = WORKSHEETSTARTCOL;
            int jmlCol = dgvGrid.Columns.Count;

            for (int colCount = 0; colCount < jmlCol; colCount++)
            {
                Microsoft.Office.Interop.Excel.Range xlRange = (Microsoft.Office.Interop.Excel.Range)xlWorkSheet1.Cells[WORKSHEETSTARTROW, worksheetcol];

                if (colCount > 1 && colCount < (jmlCol - JML_COL_TETAP_AKHIR))
                {
                    try
                    {
                        tgl = DateTime.Parse(dgvGrid.Columns[colCount].Name);
                        xlRange.Value2 = tgl.ToString("dd");
                        //xlRange.Value2 = tgl.ToString("dd-MMM-yyyy") + "(" + tgl.DayOfWeek.ToString().ToUpper().Substring(0, 3) + ")";   
                    }
                    catch
                    {
                    }
                }
                else
                {
                    xlRange.Value2 = dgvGrid.Columns[colCount].HeaderText;
                }

                worksheetcol += 1;
            }

            int worksheetRow = WORKSHEETSTARTROW + 1;
            for (int rowCount = 0; rowCount < dgvGrid.Rows.Count; rowCount++)
            {
                if (dgvGrid.Rows[rowCount].IsNewRow) continue;

                worksheetcol = WORKSHEETSTARTCOL;
                for (int colCount = 0; colCount < jmlCol; colCount++)
                {
                    Microsoft.Office.Interop.Excel.Range xlRange = (Microsoft.Office.Interop.Excel.Range)xlWorkSheet1.Cells[worksheetRow, worksheetcol];
                    if(dgvGrid.Rows[rowCount].Cells[colCount].Value == null)
                        xlRange.Value2 = "";
                    else
                        xlRange.Value2 = dgvGrid.Rows[rowCount].Cells[colCount].Value.ToString();
                    //xlRange.Font.Background = dataGridView1.Rows[rowCount].Cells[colCount].Style.BackColor;
                    //xlRange.Font.Color = dgvGrid.Rows[rowCount].Cells[colCount].Style.ForeColor.ToArgb();
                    if (colCount > 1 && colCount < (jmlCol - (JML_COL_TETAP_AKHIR)))
                    {
                        if (chkUseColor.Checked)
                        {
                            xlRange.Value2 = "";
                            xlRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(dgvGrid.Rows[rowCount].Cells[colCount].Style.BackColor);
                        }
                        else
                        {
                            switch (dgvGrid.Rows[rowCount].Cells[colCount].Value.ToString())
                            {
                                case "0":
                                    xlRange.Value2 = STAT_ALPA;
                                    break;
                                case "1":
                                    xlRange.Value2 = STAT_KERJA;
                                    break;
                                case "2":
                                    xlRange.Value2 = STAT_TIDAKABSEN;
                                    break;
                                case "3":
                                    xlRange.Value2 = STAT_TELAT;
                                    break;
                                case "4":
                                    xlRange.Value2 = STAT_PULANGCEPAT;
                                    break;
                                case "5":
                                    xlRange.Value2 = STAT_OFF;
                                    break;
                                case "6":
                                    xlRange.Value2 = STAT_IJIN;
                                    break;
                                case "7":
                                    xlRange.Value2 = STAT_OFFMASUK;
                                    break;
                                case "8":
                                    xlRange.Value2 = STAT_IJIN;
                                    break;
                            }
                        }
                    }
                    //if (dgvGrid.Rows[rowCount].Cells[colCount].Style.Font != null)
                    //{
                    //    xlRange.Font.Bold = dgvGrid.Rows[rowCount].Cells[colCount].Style.Font.Bold;
                    //    xlRange.Font.Italic = dgvGrid.Rows[rowCount].Cells[colCount].Style.Font.Italic;
                    //    xlRange.Font.Underline = dgvGrid.Rows[rowCount].Cells[colCount].Style.Font.Underline;
                    //    xlRange.Font.FontStyle = dgvGrid.Rows[rowCount].Cells[colCount].Style.Font.FontFamily;
                    //}
                    worksheetcol += 1;
                }
                worksheetRow += 1;
            }
        }

        private void FormatGrid(DataGridView dgvGrid)
        {
            int jmlCols = dgvGrid.Columns.Count;                
            DateTime[] tmp_dayoff;

            DataGridViewCellStyle styleMasuk, styleAbsen, styleOff, styleOneFingerprint, styleTelat, stylePulangCepat,
                styleIjinSakit, styleOffMasuk, styleLain2;
            styleOffMasuk = new DataGridViewCellStyle();
            styleOffMasuk.BackColor = Color.Pink;
            styleLain2 = new DataGridViewCellStyle();
            styleLain2.BackColor = Color.PapayaWhip;
            styleMasuk = new DataGridViewCellStyle();
            styleMasuk.BackColor = Color.LightGreen;
            styleAbsen = new DataGridViewCellStyle();
            styleAbsen.BackColor = Color.Red;
            styleOff = new DataGridViewCellStyle();
            styleOff.BackColor = Color.LightGray;
            styleOneFingerprint = new DataGridViewCellStyle();
            styleOneFingerprint.BackColor = Color.Blue;
            styleTelat = new DataGridViewCellStyle();
            styleTelat.BackColor = Color.Yellow;
            stylePulangCepat = new DataGridViewCellStyle();
            stylePulangCepat.BackColor = Color.Magenta;
            styleIjinSakit = new DataGridViewCellStyle();
            styleIjinSakit.BackColor = Color.Orange;

            tmp_dayoff = new DateTime[jmlCols - JML_COL_TETAP_AWAL];
            //dgvGrid.Columns[jmlCols - 1].Visible = false;

            for (int i = JML_COL_TETAP_AWAL; i <= jmlCols - JML_COL_TETAP_AWAL; i++)
            {
                tmp_dayoff[i - 2] = DateTime.Parse(dgvGrid.Columns[i].HeaderText);
            }

            foreach (DataGridViewRow row in dgvGrid.Rows)
            {
                if (!row.IsNewRow)
                {
                    for (int i = JML_COL_TETAP_AWAL; i <= jmlCols - JML_COL_TETAP_AWAL; i++)
                    {
                        // (row.Cells[i].Value.ToString() == "0" ? styleAbsen : styleMasuk);
                        switch (row.Cells[i].Value.ToString())
                        {
                            case "0": //ga masuk
                                row.Cells[i].Style = styleAbsen;
                                break;
                            case "1": //masuk kerja
                                row.Cells[i].Style = styleMasuk;
                                break; 
                            case "2": // hanya absen 1 kali
                                row.Cells[i].Style = styleOneFingerprint;
                                break;
                            case "3": //telat
                                row.Cells[i].Style = styleTelat;
                                break;
                            case "4": //pulang cepat
                                row.Cells[i].Style = stylePulangCepat;
                                break;
                            case "5": //off kerja
                                row.Cells[i].Style = styleOff;
                                break;
                            case "6": //ijin resmi/ sakit
                                row.Cells[i].Style = styleIjinSakit;
                                break;
                            case "7": //Masuk Sewaktu OFF
                                row.Cells[i].Style = styleOffMasuk;
                                break;
                            case "8": //ijin tidak resmi
                                row.Cells[i].Style = styleLain2;
                                break;
                        }
                    }
                }
            }
        }

        private void btnFormatGrid_Click(object sender, EventArgs e)
        {
            int jmlhari_gaji = 0, jmlhari_bonus = 0, jml_sakit = 0,
                jmlCols = dgvGrid.Columns.Count;

            FormatGrid(dgvGrid);

            dgvGrid.Columns.Add("_totalbonus", "Hari Bonus");
            idx_totalbonus = dgvGrid.Columns.Count - 1;

            dgvGrid.Columns.Add("_sakit", "Sakit/Ijin");
            idx_sakit = dgvGrid.Columns.Count - 1;

            foreach (DataGridViewRow row in dgvGrid.Rows)
            {
                if (!row.IsNewRow)
                {
                    jmlhari_gaji = jml_sakit = 0;
                    for (int i = JML_COL_TETAP_AWAL; i <= jmlCols - JML_COL_TETAP_AWAL; i++)
                    {
                        switch (row.Cells[i].Style.BackColor.Name)
                        {
                            case "Red":
                                if(DateTime.ParseExact(dgvGrid.Columns[i].HeaderText, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture).DayOfWeek == DayOfWeek.Sunday)
                                    jmlhari_gaji-=2;
                                break;
                            case "Orange": //sakit / ijin resmi
                                jml_sakit++;
                                break;
                            case "Pink": //off masuk
                            //case "LightGray": //off
                            case "LightGreen": //masuk kerja
                            case "Yellow": //telat
                                jmlhari_gaji++;
                                break;
                        }
                    }

                    row.Cells[idx_sakit].Value = jml_sakit;
                    row.Cells[idx_harikerja].Value = jmlhari_gaji;
                }
            }

            jmlCols = dgvGrid.Columns.Count;
            int totalharioff = jmlCols - JML_COL_TETAP_AWAL - 1;
            totalharioff = (totalharioff / JML_HARI_DALAM_SEMINGGU) * 2;
            //if (totalharioff != 8 && totalharioff != 10)
            //{
            //    MessageBox.Show("Periode Bonus salah, periode bonus harus 4 minggu atau 5 minggu!", Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    dtpPeriodeAwalBonus.Focus();
            //    return;
            //}


            int tmpHariAkhir = dtpPeriodeAkhir.Value.Day + JML_COL_TETAP_AWAL - 1;
            foreach (DataGridViewRow row in dgvGrid.Rows)
            {
                
                if (!row.IsNewRow)
                {
                    jmlhari_bonus = 0;
                    for (int i = JML_COL_TETAP_AWAL; i <= jmlCols - JML_COL_TETAP_AWAL; i++)
                    {
                        switch (row.Cells[i].Style.BackColor.Name)
                        {
                            case "Pink": //off masuk
                            case "LightGray": //off
                                if (i == JML_COL_TETAP_AWAL)
                                {
                                    if (row.Cells[i + 1].Style.BackColor.Name.Equals("LightGreen") || row.Cells[i + 1].Style.BackColor.Name.Equals("Pink"))
                                        jmlhari_bonus++;
                                }
                                else
                                    if (i == tmpHariAkhir)
                                    {
                                        if (row.Cells[i - 1].Style.BackColor.Name.Equals("LightGreen") || row.Cells[i - 1].Style.BackColor.Name.Equals("Pink"))
                                            jmlhari_bonus++;
                                    }
                                    else
                                    {
                                        if (row.Cells[i - 1].Style.BackColor.Name.Equals("LightGreen") || row.Cells[i - 1].Style.BackColor.Name.Equals("Pink"))
                                            if (row.Cells[i + 1].Style.BackColor.Name.Equals("LightGreen") || row.Cells[i + 1].Style.BackColor.Name.Equals("Pink"))
                                                jmlhari_bonus++;
                                    }
                                //jmlhari_bonus++;
                                break;
                        }
                    }

                    dgvGrid.Rows[row.Index].Cells[idx_totalbonus].Value = ( jmlhari_bonus < 0 ? 0 : jmlhari_bonus );
                }
            }


            

            //double totaluangmakan = 0, tmpharikerja = 0, tmpuangmakan = 0, tmpharium = 0;
            //foreach (DataGridViewRow row in dgvGrid.Rows)
            //{
            //    if (!row.IsNewRow)
            //    {
            //        //double.TryParse(row.Cells[idx_harikerja].Value.ToString(), out tmpharikerja);
            //        double.TryParse(row.Cells[idx_hariUM].Value.ToString(), out tmpharium);
            //        double.TryParse(row.Cells[idx_uangmakan].Value.ToString(), out tmpuangmakan);
            //        totaluangmakan = tmpharium * tmpuangmakan;
            //        row.Cells[idx_totaluangmakan].Value = totaluangmakan;
            //    }
            //}

            dgvGrid.Columns.Add("_lain2", "Lain2");
            idx_lain2 = dgvGrid.Columns.Count - 1;

            dgvGrid.Columns.Add("_keterangan", "Keterangan");
            idx_keterangan = dgvGrid.Columns.Count - 1;


            int potonganHari = 0;
            int idxLain2 = dgvGrid.Columns.Count - 2;
            decimal tmpPotongan = 0;
            foreach (DataGridViewRow row in dgvGrid.Rows)
            {
                if (!row.IsNewRow)
                {
                    potonganHari = 0;
                    for (int i = JML_COL_TETAP_AWAL; i <= jmlCols - JML_COL_TETAP_AWAL; i++)
                    {
                        switch (row.Cells[i].Style.BackColor.Name)
                        {
                            case "Blue": //absen cuma sekali
                            case "Yellow": //telat
                                potonganHari++;
                                break;
                        }
                    }
                    tmpPotongan = -(potonganHari * POTONGAN_PERHARI);
                    row.Cells[idxLain2].Value = tmpPotongan.ToString("N0");
                }
            }

            bool isFull = false;
            
            foreach (DataGridViewRow row in dgvGrid.Rows)
            {
                if (!row.IsNewRow)
                {
                    isFull = true;
                    for (int i = JML_COL_TETAP_AWAL; i <= jmlCols - JML_COL_TETAP_AWAL; i++)
                    {
                        switch (row.Cells[i].Style.BackColor.Name)
                        {
                            case "Blue": //absen cuma sekali
                            case "Yellow": //telat
                            case "Orange":
                            case "Red":
                            case "PapayaWhip":
                            case "Magenta":
                                isFull = false;
                                break;
                        }
                        if (!isFull)
                            break;
                    }
                    if (isFull)
                    {
                        decimal.TryParse(row.Cells[idxLain2].Value.ToString(), out tmpPotongan);
                        tmpPotongan += TAMBAHAN_FULL;
                        row.Cells[idxLain2].Value = tmpPotongan.ToString("N0");
                    }
                }
            }

            btnFormatGrid.Enabled = false;
            btnExport.Enabled = btnSave.Enabled = true;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Get_MonthPeriode(string tahun, int bulan)
        {
            int iYear = 0;
            int.TryParse(tahun, out iYear);

            if (iYear == 0)
                iYear = DateTime.Now.Year;

            dtpPeriodeAwal.Value = gFunc.GetFirstDayOfMonth(iYear, bulan);
            dtpPeriodeAkhir.Value = gFunc.GetLastDayOfMonth(iYear, bulan);

            //dtpPeriodeAwalBonus.Value = gFunc.GetFirstDayOfMonth(iYear, bulan);
            //dtpPeriodeAkhirBonus.Value = gFunc.GetLastDayOfMonth(iYear, bulan);

            //int dayofweek = (int)dtpPeriodeAwalBonus.Value.DayOfWeek;

            ////if(dayofweek != 0)
            //dtpPeriodeAwalBonus.Value = dtpPeriodeAwalBonus.Value.AddDays(-dayofweek);

            //dayofweek = (int)dtpPeriodeAkhirBonus.Value.DayOfWeek + 1;
            //if (dayofweek != (int)DayOfWeek.Saturday + 1)
            //    dtpPeriodeAkhirBonus.Value = dtpPeriodeAkhirBonus.Value.AddDays(-dayofweek);
        }

        private void btnGetPeriode_Click(object sender, EventArgs e)
        {
            Get_MonthPeriode(txtTahun.Text, cboBulan.SelectedIndex + 1);           
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveData();
        }

        private void ValidateTahun()
        {
            bulan = cboBulan.SelectedIndex + 1;
            isErr = int.TryParse(txtTahun.Text, out tahun);

            if (!isErr)
            {
                tahun = DateTime.Now.Year;
                txtTahun.Text = tahun.ToString();
            }
        }

        private void SaveData()
        {
            byte jml_harikerja, jml_bonusoff, jml_sakit,
                jml_hari = (byte)System.DateTime.DaysInMonth(dtpPeriodeAwal.Value.Year, dtpPeriodeAwal.Value.Month);
            //(byte)(dtpPeriodeAkhir.Value.Day - dtpPeriodeAwal.Value.Day + 1);
            double lain2 = 0;
            string keterangan = string.Empty;

            ValidateTahun();

            DialogResult dr = MessageBox.Show("Are you sure want to save this data ?", Title, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (dr == DialogResult.No)
            {
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

                    if (chkDeleteRecord.Checked)
                    {
                        if (cboWarehouse.SelectedIndex > 0)
                        {
                            sqlCmd.CommandText = "del_gaji";
                            //sqlCmd.Parameters.Add("@NIK", SqlDbType.Int).Value = nik;
                            sqlCmd.Parameters.Add("@kdcabang", SqlDbType.VarChar, 5).Value = KdCabang;
                            sqlCmd.Parameters.Add("@kdwarehouse", SqlDbType.VarChar, 3).Value = cboWarehouse.SelectedValue.ToString();
                            sqlCmd.Parameters.Add("@divisi", SqlDbType.SmallInt).Value = cboDivisi.SelectedValue.ToString();
                            sqlCmd.Parameters.Add("@Tahun", SqlDbType.SmallInt).Value = tahun;
                            sqlCmd.Parameters.Add("@Bulan", SqlDbType.TinyInt).Value = bulan;
                            sqlCmd.ExecuteNonQuery();

                            sqlCmd.Parameters.Clear();
                        }
                    }

                    sqlCmd.CommandText = "ins_gaji";
                    sqlCmd.Parameters.Add("@NIK", SqlDbType.Int);
                    sqlCmd.Parameters.Add("@Tahun", SqlDbType.SmallInt).Value = tahun;
                    sqlCmd.Parameters.Add("@Bulan", SqlDbType.TinyInt).Value = bulan;
                    sqlCmd.Parameters.Add("@jml_hari", SqlDbType.TinyInt).Value = jml_hari;
                    sqlCmd.Parameters.Add("@jml_harikerja", SqlDbType.TinyInt);
                    sqlCmd.Parameters.Add("@jml_bonusoff", SqlDbType.TinyInt);
                    sqlCmd.Parameters.Add("@jml_sakit", SqlDbType.TinyInt);

                    //sqlCmd.Parameters.Add("@potongan", SqlDbType.Money).Value = bulan;
                    //sqlCmd.Parameters.Add("@bonus", SqlDbType.Money).Value = bulan;
                    //sqlCmd.Parameters.Add("@total", SqlDbType.Money).Value = bulan;
                    sqlCmd.Parameters.Add("@lain2", SqlDbType.Money);
                    sqlCmd.Parameters.Add("@keterangan", SqlDbType.VarChar, 255);

                    foreach (DataGridViewRow row in dgvGrid.Rows)
                    {
                        if (!row.IsNewRow)
                        {
                            nik = int.Parse( row.Cells[idx_nik].Value.ToString() );
                            jml_harikerja = byte.Parse(row.Cells[idx_harikerja].Value.ToString());
                            jml_bonusoff = byte.Parse(row.Cells[idx_totalbonus].Value.ToString());
                            jml_sakit = byte.Parse(row.Cells[idx_sakit].Value.ToString());
                            
                            //if (row.Cells[idx_keterangan].Value == null)
                            //    keterangan = string.Empty;
                            //else
                            //    keterangan = row.Cells[idx_keterangan].Value.ToString();

                            keterangan = ((string)row.Cells[idx_keterangan].Value ?? string.Empty);
                            double.TryParse((string)row.Cells[idx_lain2].Value ?? "0", out lain2);


                            //if (row.Cells[idx_lain2].Value == null)
                            //    lain2 = 0;
                            //else
                            //    double.TryParse(row.Cells[idx_lain2].Value.ToString() , out lain2);

                            sqlCmd.Parameters["@NIK"].Value = nik;
                            sqlCmd.Parameters["@jml_harikerja"].Value = jml_harikerja;
                            sqlCmd.Parameters["@jml_bonusoff"].Value = jml_bonusoff;
                            sqlCmd.Parameters["@jml_sakit"].Value = jml_sakit;
                            sqlCmd.Parameters["@keterangan"].Value = keterangan;
                            sqlCmd.Parameters["@lain2"].Value = lain2;

                            sqlCmd.ExecuteNonQuery();
                        }
                    }

                    sqlTrans.Commit();
                    MessageBox.Show("Data has been Saved!", Title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception exc)
            {
                try
                {
                    if (sqlTrans != null)
                        sqlTrans.Rollback();
                }
                catch
                {
                }

                MessageBox.Show("Cannot Save Data!" + Environment.NewLine + Environment.NewLine +
                    exc.Message, Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnPrintGaji_Click(object sender, EventArgs e)
        {
            FrmRpt_Gaji rptGaji = new FrmRpt_Gaji();
            rptGaji.Show();
        }
        
    }
}
