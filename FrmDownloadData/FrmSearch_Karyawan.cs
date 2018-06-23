using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using System.Data.SqlClient;

namespace HRDProject
{
    public partial class FrmSearch_Karyawan : Form
    {
        private void ChangeIconForm()
        {
            //System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            //System.IO.Stream stream = assembly.GetManifestResourceStream(InfoApp.MyAssemblyName + "." + InfoApp.IconPath + "." + InfoApp.IconName);
            //this.Icon = new Icon(stream);
        }

        private void CloseAllManagedResx()
        {
            gFunc = null;
        }

        #region Private Variables
        private readonly CultureInfo cultureInfo;
        private readonly DateTimeFormatInfo dFi;
        private readonly NumberFormatInfo nFi;
        private GlobalFunction gFunc = new GlobalFunction();
        private string strTitle;
        private const int idxNIK = 0,
            idxNama = 1, 
            idxNickName = 2,
            idxKdwarehouse = 3;
        private int current_rowGrid = 0;
        #endregion

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

        #region Constructor
        public FrmSearch_Karyawan()
        {
            InitializeComponent();

            dgvGrid.RowHeadersVisible = true;
            dgvGrid.RowHeadersDefaultCellStyle.Padding = new Padding(dgvGrid.RowHeadersWidth);
            dgvGrid.RowPostPaint += new DataGridViewRowPostPaintEventHandler(dgvGrid_RowPostPaint);

            ChangeIconForm();

            Title = InfoApp.Title;
            cultureInfo = InfoApp.Culture;
            dFi = InfoApp.DTFormatInfo;
            nFi = InfoApp.NFormatInfo;

            btnOK.Enabled = false;
        }
        #endregion

        #region Methods
        private void LoadData()
        {
            string[] rows;

            try
            {
                dgvGrid.Rows.Clear();
                using (SqlConnection sqlCnn = new SqlConnection(ZFame.Classes.clsVarProgram.DB_CONN_STRING))
                {
                    int iNIK = 0, iNama = 0, iStatus = 0, 
                        iNickName = 0, iNmWarehouse = 0, no = 1;
                    string stsRc;

                    SqlCommand sqlCmd = new SqlCommand();
                    sqlCmd.Connection = sqlCnn;
                    sqlCmd.CommandType = CommandType.StoredProcedure;

                    sqlCmd.CommandText = "Get_SearchKaryawan";
                    if (!string.IsNullOrEmpty(txtNama.Text.Trim()))
                        sqlCmd.Parameters.Add("@Nama", SqlDbType.NVarChar, 255).Value = txtNama.Text;

                    sqlCnn.Open();

                    SqlDataReader sqlDR = sqlCmd.ExecuteReader();

                    if (sqlDR.Read())
                    {
                        iStatus = sqlDR.GetOrdinal("StsRc");
                        iNIK = sqlDR.GetOrdinal("NIK");
                        iNama = sqlDR.GetOrdinal("Nama");
                        iNickName = sqlDR.GetOrdinal("NickName");
                        iNmWarehouse = sqlDR.GetOrdinal("NmWarehouse");

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
                            rows = new string[] {
                                            //no.ToString(),
                                            sqlDR.GetInt32(iNIK).ToString(),
                                            sqlDR.GetString(iNama),
                                            sqlDR.GetString(iNickName),
                                            sqlDR.GetString(iNmWarehouse)
                            };
                            no++;
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
                    btnOK.Enabled = (dgvGrid.RowCount == 0 ? false : true);
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("Cannot Load Data!" + Environment.NewLine + Environment.NewLine +
                    exc.Message, Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Properties
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

        private static char KeysENTER
        {
            get
            {
                return (char)13;
            }
        }
        public string NIK
        {
            get
            {
                return dgvGrid.Rows[current_rowGrid].Cells[idxNIK].Value.ToString();
            }
        }

        public string Nama
        {
            get
            {
                return dgvGrid.Rows[current_rowGrid].Cells[idxNama].Value.ToString();
            }
        }

        #endregion

        #region Events
        //private void dgvGrid_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    if (e.KeyChar == KeysENTER)
        //    {
        //        e.Handled = true;
        //        btnOK.PerformClick();
        //    }
        //}

        private void dgvGrid_DoubleClick(object sender, EventArgs e)
        {
            btnOK.PerformClick();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            current_rowGrid = dgvGrid.SelectedRows[0].Index;
        }

        #endregion

        private void dgvGrid_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.F1))
            {
                txtNama.Focus();
            }
            else
                if (e.Control && e.KeyCode.Equals(Keys.Enter))
            {
                btnOK.PerformClick();
            }
        }

        private void FrmSearch_Customer_FormClosed(object sender, FormClosedEventArgs e)
        {
            CloseAllManagedResx();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            btnRefresh.Enabled = false;
            LoadData();
            int rowCount = dgvGrid.RowCount;
            btnOK.Enabled = (rowCount == 0 ? false : true);
            if (btnOK.Enabled)
                dgvGrid.Focus();
            btnRefresh.Enabled = true;
        }

        private void btnRefresh_EnabledChanged(object sender, EventArgs e)
        {
            if (dgvGrid.RowCount > 0)
                btnOK.Enabled = btnRefresh.Enabled;
        }

        private void txtCustomerName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == KeysENTER)
            {
                e.Handled = true;
                btnRefresh.PerformClick();
            }
        }

        private void txtCustomerName_KeyUp(object sender, KeyEventArgs e)
        {
            
        }

        private void txtCustomerID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == KeysENTER)
            {
                e.Handled = true;
                txtNama.Text = "";
                btnRefresh.PerformClick();
            }
        }

        private void txtCustomerID_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.F1))
            {
                dgvGrid.Focus();
            }
        }

        private void txtNama_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == KeysENTER)
            {
                e.Handled = true;
                btnRefresh.PerformClick();
            }
        }

        private void txtNama_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.F1))
            {
                dgvGrid.Focus();
            }
        }

    }
}
