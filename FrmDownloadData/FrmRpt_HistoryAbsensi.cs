using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using System.Data.SqlClient;
using System.Collections;
using HRDProject.Datasets;
using System.Drawing.Printing;

namespace HRDProject
{
    public partial class FrmRpt_HistoryAbsensi : Form
    {
        private const string dbname = "HRD", Title = "Absensi", KdCabang = "AUR01",
            DISP_WAREHOUSE = "DisplayWarehouse",
            VAL_WAREHOUSE = "ValueWarehouse",
            DISP_DIVISI = "DisplayDivisi",
            VAL_DIVISI = "ValueDivisi", 
            KODE_WAR_PENJUALAN = "PST";

        private string connstring = @"Data Source=ITSUPPORT\SQLEXPRESS2008R2;Database=" + dbname +
            ";Integrated Security=True;Connect Timeout=30; User Instance=False;"; //User ID=" + Uid + ";Password=" + Pwd;

        LocalReport localReport;
        private ReportDataSource rsDataSource = new ReportDataSource();
        private string[]
            namaParams = new string[] { "PeriodeAwal", "PeriodeAkhir", "Cabang" };
        private string[] valParams;

        private const string REPORT_NAME = "rptHistoryAbsensi.rdlc";
        private readonly string STARTUP_PATH;

        private void btnLoad_Click(object sender, EventArgs e)
        {
            LoadDataKaryawan();
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

        private void txtNIK_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == KeysENTER)
            {
                e.Handled = true;
                LoadDataKaryawan();
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
                btnSearch.PerformClick();
            }
        }

        public FrmRpt_HistoryAbsensi()
        {
            InitializeComponent();

            STARTUP_PATH = Application.StartupPath + @"\Reports\";

            #region Report Local
            reportViewer1.ProcessingMode = ProcessingMode.Local;

            rsDataSource.Name = "dsRptHistoryAbsensi";
            
            #endregion

            LoadDataCombo();

            int dayofweek = (int)dtpPeriodeAwal.Value.DayOfWeek + 1;

            GlobalFunction gFunc = new GlobalFunction();
            gFunc.SetTextboxEnable(false, txtNama);

            dtpPeriodeAwal.Value = dtpPeriodeAwal.Value.AddDays(-dayofweek);
            dtpPeriodeAkhir.Value = dtpPeriodeAkhir.Value.AddDays(-1);
        }

        private void LoadDataKaryawan()
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
                        MessageBox.Show("Karyawan tidak terdaftar!", Title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                        cboDivisi.SelectedValue = "1";
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

        private void btnPrint_Click(object sender, EventArgs e)
        {
            PageSettings myPageSettings = new PageSettings();

            DateTime periodeAwal, periodeAkhir;

            periodeAwal = dtpPeriodeAwal.Value;
            periodeAkhir = dtpPeriodeAkhir.Value;

            reportViewer1.Reset();

            PrintAbsensi(periodeAwal, periodeAkhir);

            PaperSize size = new PaperSize();
            size.RawKind = (int)PaperKind.A4;

            PrinterResolution a = new PrinterResolution();
            a.Kind = PrinterResolutionKind.Draft;
            //myPageSettings.PaperSize = size;
            myPageSettings.Margins.Top = 10;
            myPageSettings.Margins.Bottom = 10;
            myPageSettings.Margins.Left = 0;
            myPageSettings.Margins.Right = 0;


            //myPageSettings.PrinterResolution = a;

            this.reportViewer1.SetPageSettings(myPageSettings);


            this.reportViewer1.RefreshReport();

            btnRefresh.Enabled = true;
        }

        private void PrintAbsensi(DateTime periodeAwal, DateTime periodeAkhir)
        {
            localReport = reportViewer1.LocalReport;
            localReport.ReportPath = STARTUP_PATH + REPORT_NAME;
            localReport.DataSources.Add(rsDataSource);

            List<ReportParameter> rptParams = new List<ReportParameter>();

            valParams = new string[] { periodeAwal.ToString(), periodeAkhir.ToString(), cboWarehouse.Text };
            int pjgParams = valParams.GetUpperBound(0);
            for (int i = 0; i <= pjgParams; i++)
                rptParams.Add(new ReportParameter(namaParams[i], valParams[i]));

            System.IO.Directory.SetCurrentDirectory(STARTUP_PATH);
            localReport.SetParameters(rptParams);

            dsRptHistoryAbsensi.Clear();
            LoadSQLData(ref dsRptHistoryAbsensi, periodeAwal, periodeAkhir);

            localReport.DataSources.Add(rsDataSource);

            localReport.DataSources[0].Value = dsRptHistoryAbsensi.Tables["table01"];
        }

        private void LoadSQLData(ref dsRptHistoryAbsensi dataSet, DateTime periodeAwal, DateTime periodeAkhir)
        {
            try
            {
                using (SqlConnection sqlCnn = new SqlConnection(ZFame.Classes.clsVarProgram.DB_CONN_STRING))
                {
                    SqlCommand sqlCmd = new SqlCommand();
                    sqlCmd.Connection = sqlCnn;
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.CommandText = "rpt_historyabsensi";
                    if (!string.IsNullOrWhiteSpace(txtNIK.Text))
                    {
                        int.TryParse(txtNIK.Text, out int nik);
                        sqlCmd.Parameters.Add("@NIK", SqlDbType.Int).Value = nik;
                    }
                    sqlCmd.Parameters.Add("@PeriodeAwal", SqlDbType.DateTime).Value = dtpPeriodeAwal.Value.ToString();
                    sqlCmd.Parameters.Add("@PeriodeAkhir", SqlDbType.DateTime).Value = dtpPeriodeAkhir.Value.ToString();
                    sqlCmd.Parameters.Add("@kdcabang", SqlDbType.VarChar, 5).Value = KdCabang;
                    if(cboWarehouse.SelectedIndex > 0)
                        sqlCmd.Parameters.Add("@kdwarehouse", SqlDbType.VarChar, 3).Value = cboWarehouse.SelectedValue.ToString();
                    sqlCmd.Parameters.Add("@divisi", SqlDbType.SmallInt).Value = cboDivisi.SelectedValue.ToString();
                    sqlCmd.Parameters.Add("@viewNickName", SqlDbType.Bit).Value = chkViewNickName.Checked;

                    sqlCnn.Open();

                    SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlCmd);

                    dataAdapter.Fill(dataSet, "table01");

                    sqlCmd = null;
                    sqlCnn.Close();
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("Cannot Load Data!" + Environment.NewLine + Environment.NewLine +
                    exc.Message, Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
