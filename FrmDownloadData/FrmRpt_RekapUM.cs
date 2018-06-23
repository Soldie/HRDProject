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

namespace HRDProject
{
    public partial class FrmRpt_RekapUM : Form
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
            JML_COL_TETAP = 3,
            JML_COL_TETAP_AWAL = 2,
            JML_HARI_DALAM_SEMINGGU = 7; 
        private int idx_uangmakan, idx_harikerja, 
            idx_nik = 0,
            idx_nama = 1;
        private string connstring = @"Data Source=ITSUPPORT\SQLEXPRESS2008R2;Database=" + dbname +
            ";Integrated Security=True;Connect Timeout=30; User Instance=False;"; //User ID=" + Uid + ";Password=" + Pwd;
        const int WORKSHEETSTARTROW = 5;
        const int WORKSHEETSTARTCOL = 4;
        private GlobalFunction gFunc = new GlobalFunction();

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

        public FrmRpt_RekapUM()
        {
            InitializeComponent();

            dgvGrid.RowHeadersVisible = true;
            dgvGrid.RowHeadersDefaultCellStyle.Padding = new Padding(dgvGrid.RowHeadersWidth);
            dgvGrid.RowPostPaint += new DataGridViewRowPostPaintEventHandler(dgvGrid_RowPostPaint);

            LoadDataCombo();

            dgvGrid.AllowUserToAddRows = false;
            btnFormatGrid.Enabled = btnExport.Enabled = false;

            int dayofweek = (int)dtpPeriodeAwal.Value.DayOfWeek + 1;

            dtpPeriodeAwal.Value = dtpPeriodeAwal.Value.AddDays(-dayofweek);
            dtpPeriodeAkhir.Value = dtpPeriodeAkhir.Value.AddDays(-1);
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

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection sqlCnn = new SqlConnection(ZFame.Classes.clsVarProgram.DB_CONN_STRING))
                {
                    SqlCommand sqlCmd = new SqlCommand();
                    sqlCmd.Connection = sqlCnn;
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.CommandText = "rpt_rekapabsensi";
                    sqlCmd.Parameters.Add("@PeriodeAwal", SqlDbType.DateTime).Value = dtpPeriodeAwal.Value.ToString();
                    sqlCmd.Parameters.Add("@PeriodeAkhir", SqlDbType.DateTime).Value = dtpPeriodeAkhir.Value.ToString();
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

                    dgvGrid.Columns[idx_nama].Frozen = true;

                    //utk hapus kolom off hari
                    dgvGrid.Columns.RemoveAt(dgvGrid.Columns.Count - 1);

                    idx_uangmakan = dgvGrid.Columns.Count - 1;
                    //dgvGrid.Columns[dgvGrid.Columns.Count - 1].Visible = false;
                    dgvGrid.Columns.Add("_jmlhari", "Jumlah Hari UM");
                    idx_harikerja = dgvGrid.Columns.Count - 1;
                    btnFormatGrid.Enabled = true;
                    btnExport.Enabled = false;

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
                                        for (int i = JML_COL_TETAP_AWAL; i < idx_uangmakan; i++)
                                        {
                                            if (dgvGrid.Columns[i].Name == clock_date.ToString("yyyy-MM-dd"))
                                            {
                                                row.Cells[i].Value = "2";
                                                //if (clock_date.DayOfWeek == DayOfWeek.Saturday && (i == idx_uangmakan - 1))
                                                //    row.Cells[i].Value = 1;
                                                //else
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
                                        for (int i = JML_COL_TETAP_AWAL; i < idx_uangmakan; i++)
                                        {
                                            if (dgvGrid.Columns[i].Name == clock_date.ToString("yyyy-MM-dd"))
                                            {
                                                if (row.Cells[i].Value.ToString() != "2")
                                                {
                                                    row.Cells[i].Value = "3";
                                                    break;
                                                }
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
                                        for (int i = JML_COL_TETAP_AWAL; i < idx_uangmakan; i++)
                                        {
                                            if (dgvGrid.Columns[i].Name == clock_date.ToString("yyyy-MM-dd"))
                                            {
                                                if (row.Cells[i].Value.ToString() != "2")
                                                {
                                                    row.Cells[i].Value = "4";
                                                    //if (clock_date.DayOfWeek == DayOfWeek.Saturday && (i == idx_uangmakan - 1))
                                                    //    row.Cells[i].Value = 1;
                                                    //else
                                                    //    row.Cells[i].Value = 4;
                                                    break;
                                                }
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
                                        for (int i = JML_COL_TETAP_AWAL; i < idx_uangmakan; i++)
                                        {
                                            if (dgvGrid.Columns[i].Name == clock_date.ToString("yyyy-MM-dd"))
                                            {
                                                //if (string.IsNullOrWhiteSpace(row.Cells[i].Value.ToString()) || row.Cells[i].Value.ToString() == "0")
                                                row.Cells[i].Value = "6";
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
                                        for (int i = JML_COL_TETAP_AWAL; i < idx_uangmakan; i++)
                                        {
                                            if (dgvGrid.Columns[i].Name == clock_date.ToString("yyyy-MM-dd"))
                                            {
                                                //if (string.IsNullOrWhiteSpace(row.Cells[i].Value.ToString()) || row.Cells[i].Value.ToString() == "0")
                                                row.Cells[i].Value = "8";
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
                                        for (int i = JML_COL_TETAP_AWAL; i < idx_uangmakan; i++)
                                        {
                                            if (dgvGrid.Columns[i].Name == clock_date.ToString("yyyy-MM-dd"))
                                            {
                                                if (row.Cells[i].Value.ToString() != "1" && row.Cells[i].Value.ToString() != "3")
                                                    row.Cells[i].Value = "5";
                                                else
                                                    if (row.Cells[i].Value.ToString() == "1")
                                                        row.Cells[i].Value = "7";
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
                btnFormatGrid.Enabled = btnExport.Enabled = false;
                MessageBox.Show("Cannot Load Data!" + Environment.NewLine + Environment.NewLine +
                    exc.Message, Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

                if (colCount > 1 && colCount < (jmlCol - JML_COL_TETAP))
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
                    xlRange.Value2 = dgvGrid.Rows[rowCount].Cells[colCount].Value.ToString();
                    //xlRange.Font.Background = dataGridView1.Rows[rowCount].Cells[colCount].Style.BackColor;
                    //xlRange.Font.Color = dgvGrid.Rows[rowCount].Cells[colCount].Style.ForeColor.ToArgb();
                    if (colCount > 1 && colCount < (jmlCol - JML_COL_TETAP))
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

        private void btnFormatGrid_Click(object sender, EventArgs e)
        {
            int jmlCols = dgvGrid.Columns.Count, jmlhari_um = 0, jmlhari_gaji = 0,
                counterhari = 0, jmlhari_um_perminggu = 0, jml_off = 0, jml_telat = 0,
                jml_tidakmasuk = 0, double_tidak_masuk = 0, jml_ijinsakit = 0, masuk_waktu_off = 0,
                jml_TidakAbsenPulang = 0;
            bool isAbsenSekali = false, isTidakMasukBerurutan = false,
                isPulangcepat = false;



            DateTime[] tmp_dayoff;

            DataGridViewCellStyle styleMasuk, styleAbsen, styleOff, styleOneFingerprint, styleTelat, stylePulangCepat,
                styleOffMasuk, styleIjinSakit, styleLain2;
            styleIjinSakit = new DataGridViewCellStyle();
            styleIjinSakit.BackColor = Color.Orange;
            styleLain2 = new DataGridViewCellStyle();
            styleLain2.BackColor = Color.PapayaWhip;
            styleOffMasuk = new DataGridViewCellStyle();
            styleOffMasuk.BackColor = Color.Pink;
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

            tmp_dayoff = new DateTime[jmlCols - JML_COL_TETAP];
            //dgvGrid.Columns[jmlCols - 1].Visible = false;

            for (int i = JML_COL_TETAP_AWAL; i <= jmlCols - JML_COL_TETAP; i++)
            {
                tmp_dayoff[i - 2] = DateTime.Parse( dgvGrid.Columns[i].HeaderText );
                dgvGrid.Columns[i].ValueType = typeof(string);
            }

            //int totalharioff = jmlCols - JML_COL_TETAP - 1;
            //totalharioff = (totalharioff / 7);

            foreach (DataGridViewRow row in dgvGrid.Rows)
            {
                if (!row.IsNewRow)
                {
                    double_tidak_masuk = jml_tidakmasuk = jmlhari_um = jmlhari_gaji = jmlhari_um_perminggu =
                        jml_off = jml_telat = jml_ijinsakit = masuk_waktu_off = jml_TidakAbsenPulang = 0;
                    counterhari = 1;
                    isPulangcepat = false;
                    isAbsenSekali = false;
                    isTidakMasukBerurutan = false;
                    for (int i = JML_COL_TETAP_AWAL; i <= jmlCols - JML_COL_TETAP; i++, counterhari++)
                    {
                        switch (row.Cells[i].Value.ToString())
                        {
                            case "0":
                                if(isTidakMasukBerurutan)
                                    double_tidak_masuk++;
                                double_tidak_masuk++;

                                //if (chkUseColor.Checked)
                                    row.Cells[i].Style = styleAbsen;
                                //else
                                //    row.Cells[i].Value = STAT_ALPA;

                                isAbsenSekali = true;
                                isTidakMasukBerurutan = true;
                                break;
                            case "1":
                                isTidakMasukBerurutan = false;
                                jmlhari_um++;
                                jmlhari_um_perminggu++;
                                jmlhari_gaji++;
                                //if (chkUseColor.Checked)
                                    row.Cells[i].Style = styleMasuk;
                                //else
                                //    row.Cells[i].Value = STAT_KERJA;
                                break;
                            case "2": //absen 1 kali
                                isTidakMasukBerurutan = false;
                                jml_TidakAbsenPulang++;
                                //if (chkUseColor.Checked)
                                    row.Cells[i].Style = styleOneFingerprint;
                                //else
                                //    row.Cells[i].Value = STAT_TIDAKABSEN;

                                break;
                            case "3": //telat
                                isTidakMasukBerurutan = false;
                                jmlhari_gaji++;
                                jml_telat++;
                                //if (chkUseColor.Checked)
                                    row.Cells[i].Style = styleTelat;
                                //else
                                //    row.Cells[i].Value = STAT_TELAT;
                                break;
                            case "4": // pulang cepat
                                isTidakMasukBerurutan = false;
                                isPulangcepat = true;
                                //if (chkUseColor.Checked)
                                    row.Cells[i].Style = stylePulangCepat;
                                //else
                                //    row.Cells[i].Value = STAT_PULANGCEPAT;
                                break;
                            case "5": //off kerja
                                isTidakMasukBerurutan = false;
                                jml_off++;
                                //if (chkUseColor.Checked)
                                    row.Cells[i].Style = styleOff;
                                //else
                                //    row.Cells[i].Value = STAT_OFF;
                                break;
                            case "6": //IJIN / SAKIT
                                isTidakMasukBerurutan = false;
                                jml_ijinsakit++;
                                //if (chkUseColor.Checked)
                                    row.Cells[i].Style = styleIjinSakit;
                                //else
                                //    row.Cells[i].Value = STAT_IJIN;
                                break;
                            case "7": //Masuk Sewaktu OFF
                                isTidakMasukBerurutan = false;
                                masuk_waktu_off++;
                                jmlhari_um++;
                                jml_off++;
                                jmlhari_um_perminggu++;
                                jmlhari_gaji++;
                                //if (chkUseColor.Checked)
                                    row.Cells[i].Style = styleOffMasuk;
                                //else
                                //    row.Cells[i].Value = STAT_OFFMASUK;
                                break;
                            case "8": //ijin tidak resmi
                                isTidakMasukBerurutan = false;
                                jml_ijinsakit++;
                                //if (chkUseColor.Checked)
                                    row.Cells[i].Style = styleLain2;
                                //else
                                //    row.Cells[i].Value = STAT_IJIN;
                                break;
                        }

                        if (counterhari % JML_HARI_DALAM_SEMINGGU == 0)
                        {
                            if (cboDivisi.SelectedValue.ToString() == "2")
                            {
                                if (jml_off - masuk_waktu_off <= 2)
                                {
                                    if (!isPulangcepat)
                                    {
                                        if (!isAbsenSekali)
                                        {
                                            if (jmlhari_um_perminggu >= JML_HARI_DALAM_SEMINGGU)
                                                jmlhari_um++;
                                            else
                                                if ((jmlhari_um_perminggu + jml_off + jml_telat) >= JML_HARI_DALAM_SEMINGGU)
                                                {
                                                    if (isPulangcepat || isAbsenSekali)
                                                        break;
                                                    if (jml_TidakAbsenPulang > 0)
                                                        break;
                                                    if (jml_off > 2)
                                                        break;
                                                    if(jml_ijinsakit == 0)
                                                        jmlhari_um++;
                                                }
                                        }
                                    }
                                }
                            }
                            counterhari = jmlhari_um_perminggu = jml_off = masuk_waktu_off = 0;
                        }
                    }

                    jmlhari_um -= double_tidak_masuk;
                    jmlhari_um_perminggu -= double_tidak_masuk;

                    row.Cells[jmlCols - 1].Value = jmlhari_um;
                }
            }

            //dgvGrid.Columns.Add("_totalbonus", "Total Bonus");
            dgvGrid.Columns.Add("_totaluangmakan", "Total Uang Makan");
            int idx_totaluangmakan = dgvGrid.Columns.Count - 1;
            double totaluang = 0, tmpharikerja = 0, tmpuangmakan = 0;
            foreach (DataGridViewRow row in dgvGrid.Rows)
            {
                if (!row.IsNewRow)
                {
                    double.TryParse(row.Cells[idx_harikerja].Value.ToString(), out tmpharikerja);
                    double.TryParse(row.Cells[idx_uangmakan].Value.ToString(), out tmpuangmakan);
                    
                    totaluang = tmpharikerja * tmpuangmakan;
                    row.Cells[idx_totaluangmakan].Value = totaluang;
                }
            }

            //dgvGrid.Columns.RemoveAt(dgvGrid.Columns.Count - 3);

            btnFormatGrid.Enabled = false;
            btnExport.Enabled = true;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
