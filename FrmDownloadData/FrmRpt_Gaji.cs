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
using System.Globalization;
using System.Drawing.Printing;

namespace HRDProject
{
    public partial class FrmRpt_Gaji : Form
    {
        private const string dbname = "HRD", Title = "Absensi", KdCabang = "AUR01",
            DISP_WAREHOUSE = "DisplayWarehouse", VAL_WAREHOUSE = "ValueWarehouse", 
            DISP_DIVISI = "DisplayDivisi",
            VAL_DIVISI = "ValueDivisi", 
            KODE_WAR_PENJUALAN = "PST";

        private string connstring = @"Data Source=ITSUPPORT\SQLEXPRESS2008R2;Database=" + dbname +
            ";Integrated Security=True;Connect Timeout=30; User Instance=False;"; //User ID=" + Uid + ";Password=" + Pwd;

        LocalReport localReport;
        private ReportDataSource rsDataSource = new ReportDataSource();
        private string[]
            namaParams = new string[] { "PeriodeAwal", "PeriodeAkhir" };
        private string[] valParams;

        private const string REPORT_NAME = "rptPrintGaji.rdlc";
        private readonly string STARTUP_PATH;
        private int tahun, bulan;

        public FrmRpt_Gaji()
        {
            InitializeComponent();

            STARTUP_PATH = Application.StartupPath + @"\Reports\";

            txtTahun.Text = DateTime.Now.Year.ToString();

            for (int i = 1; i <= 12; i++)
                cboBulan.Items.Add(DateTimeFormatInfo.CurrentInfo.GetMonthName(i));

            cboBulan.SelectedIndex = DateTime.Now.Month - 1;

            #region Report Local
            reportViewer1.ProcessingMode = ProcessingMode.Local;

            rsDataSource.Name = "dsPrintGaji";
            
            #endregion

            LoadDataCombo();
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

        private void ValidateTahun()
        {
            bool isErr;

            bulan = cboBulan.SelectedIndex + 1;
            isErr = int.TryParse(txtTahun.Text, out tahun);

            if (!isErr)
            {
                tahun = DateTime.Now.Year;
                txtTahun.Text = tahun.ToString();
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            PageSettings myPageSettings = new PageSettings();

            ValidateTahun();

            reportViewer1.Reset();

            PrintGaji(tahun, bulan);

            myPageSettings.Margins.Top = 10;
            myPageSettings.Margins.Bottom = 0;
            myPageSettings.Margins.Left = 0;
            myPageSettings.Margins.Right = 0;

            //PaperSize size = new PaperSize();
            //size.RawKind = (int)PaperKind.Statement;

            PaperSize size = new PaperSize("Custom", 827, 550);
            myPageSettings.PaperSize = size;

            PrinterResolution a = new PrinterResolution();
            a.Kind = PrinterResolutionKind.Draft;
            myPageSettings.PrinterResolution = a;

            this.reportViewer1.SetPageSettings(myPageSettings);

            this.reportViewer1.RefreshReport();

            btnRefresh.Enabled = true;
        }

        private void PrintGaji(int tahun, int bulan)
        {
            localReport = reportViewer1.LocalReport;
            localReport.ReportPath = STARTUP_PATH + REPORT_NAME;
            localReport.DataSources.Add(rsDataSource);

            List<ReportParameter> rptParams = new List<ReportParameter>();

            //valParams = new string[] { periodeAwal.ToString(), periodeAkhir.ToString() };
            //int pjgParams = valParams.GetUpperBound(0);
            //for (int i = 0; i <= pjgParams; i++)
            //    rptParams.Add(new ReportParameter(namaParams[i], valParams[i]));

            System.IO.Directory.SetCurrentDirectory(STARTUP_PATH);
            localReport.SetParameters(rptParams);

            dsPrintGaji.Clear();
            LoadSQLData(ref dsPrintGaji, tahun, bulan);

            localReport.DataSources.Add(rsDataSource);

            localReport.DataSources[0].Value = dsPrintGaji.Tables["table01"];
        }

        private void LoadSQLData(ref dsPrintGaji dataSet, int tahun, int bulan)
        {
            try
            {
                using (SqlConnection sqlCnn = new SqlConnection(ZFame.Classes.clsVarProgram.DB_CONN_STRING))
                {
                    SqlCommand sqlCmd = new SqlCommand();
                    sqlCmd.Connection = sqlCnn;
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.CommandText = "rpt_printgaji";
                    sqlCmd.Parameters.Add("@Tahun", SqlDbType.SmallInt).Value = tahun;
                    sqlCmd.Parameters.Add("@Bulan", SqlDbType.TinyInt).Value = bulan;
                    sqlCmd.Parameters.Add("@kdcabang", SqlDbType.VarChar, 5).Value = KdCabang;
                    sqlCmd.Parameters.Add("@kdwarehouse", SqlDbType.VarChar, 3).Value = cboWarehouse.SelectedValue.ToString();
                    sqlCmd.Parameters.Add("@divisi", SqlDbType.SmallInt).Value = cboDivisi.SelectedValue.ToString();
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
