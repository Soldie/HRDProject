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
    public partial class FrmRpt_Absensi : Form
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
        private ReportDataSource rsDataSource = new ReportDataSource(), rsDataSource2 = new ReportDataSource();
        private string[]
            namaParams = new string[] { "PeriodeAwal", "PeriodeAkhir", "Cabang" };
        private string[] valParams;

        private const string REPORT_NAME = "rptJamAbsensi.rdlc", REPORT_NAME2 = "rptJamAbsensi2.rdlc";
        private readonly string STARTUP_PATH;

        public FrmRpt_Absensi()
        {
            InitializeComponent();

            STARTUP_PATH = Application.StartupPath + @"\Reports\";

            #region Report Local
            reportViewer1.ProcessingMode = ProcessingMode.Local;

            rsDataSource.Name = "dsRptJamAbsensi";
            rsDataSource2.Name = "dsRptJamAbsensi2";
            
            #endregion

            LoadDataCombo();

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

            if (chkWithJamMakan.Checked)
                PrintAbsensi2(periodeAwal, periodeAkhir);
            else
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

            dsRptJamAbsensi.Clear();
            LoadSQLData(ref dsRptJamAbsensi, periodeAwal, periodeAkhir);

            localReport.DataSources.Add(rsDataSource);

            localReport.DataSources[0].Value = dsRptJamAbsensi.Tables["table01"];
        }

        private void PrintAbsensi2(DateTime periodeAwal, DateTime periodeAkhir)
        {
            localReport = reportViewer1.LocalReport;
            localReport.ReportPath = STARTUP_PATH + REPORT_NAME2;
            localReport.DataSources.Add(rsDataSource2);

            List<ReportParameter> rptParams = new List<ReportParameter>();

            valParams = new string[] { periodeAwal.ToString(), periodeAkhir.ToString() };
            int pjgParams = valParams.GetUpperBound(0);
            for (int i = 0; i <= pjgParams; i++)
                rptParams.Add(new ReportParameter(namaParams[i], valParams[i]));

            System.IO.Directory.SetCurrentDirectory(STARTUP_PATH);
            localReport.SetParameters(rptParams);

            dsRptJamAbsensi2.Clear();
            LoadSQLData2(ref dsRptJamAbsensi2, periodeAwal, periodeAkhir);

            localReport.DataSources.Add(rsDataSource2);

            localReport.DataSources[0].Value = dsRptJamAbsensi2.Tables["table01"];
        }

        private void LoadSQLData(ref dsRptJamAbsensi dataSet, DateTime periodeAwal, DateTime periodeAkhir)
        {
            try
            {
                using (SqlConnection sqlCnn = new SqlConnection(ZFame.Classes.clsVarProgram.DB_CONN_STRING))
                {
                    SqlCommand sqlCmd = new SqlCommand();
                    sqlCmd.Connection = sqlCnn;
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.CommandText = "rpt_ambiljamabsensibyjadwal";
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

        private void LoadSQLData2(ref dsRptJamAbsensi2 dataSet, DateTime periodeAwal, DateTime periodeAkhir)
        {
            try
            {
                using (SqlConnection sqlCnn = new SqlConnection(ZFame.Classes.clsVarProgram.DB_CONN_STRING))
                {
                    SqlCommand sqlCmd = new SqlCommand();
                    sqlCmd.Connection = sqlCnn;
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.CommandText = "rpt_ambiljamabsensi2";
                    sqlCmd.Parameters.Add("@PeriodeAwal", SqlDbType.DateTime).Value = dtpPeriodeAwal.Value.ToString();
                    sqlCmd.Parameters.Add("@PeriodeAkhir", SqlDbType.DateTime).Value = dtpPeriodeAkhir.Value.ToString();
                    sqlCmd.Parameters.Add("@kdcabang", SqlDbType.VarChar, 5).Value = KdCabang;
                    if (cboWarehouse.SelectedIndex > 0)
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
