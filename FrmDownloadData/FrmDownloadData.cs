using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Data.Odbc;
using System.IO;
using System.Collections;
using System.Threading.Tasks;

namespace HRDProject
{
    public partial class FrmDownloadData : Form
    {
        private bool bIsConnected = false;//the boolean value identifies whether the device is connected
        private int iMachineNumber = 1;//the serial number of the device.After connecting the device ,this value will be changed.

        //Create Standalone SDK class dynamicly.
        public zkemkeeper.CZKEMClass axCZKEM1 = new zkemkeeper.CZKEMClass();

        private const string dbname = "HRD", Title = "Absensi";
        private string connstring = @"Data Source=ITSUPPORT\SQLEXPRESS2008R2;Database=" + dbname +
            ";Integrated Security=True;Connect Timeout=30; User Instance=False;"; //User ID=" + Uid + ";Password=" + Pwd;
        private string KdCabang = "AUR01",
              DISP_WAREHOUSE = "DisplayWarehouse",
              VAL_WAREHOUSE = "ValueWarehouse",
              DISP_JAM = "DisplayJam",
              VAL_JAM = "ValueJam",
              DISP_DIVISI = "DisplayDivisi",
              VAL_DIVISI = "ValueDivisi", 
              KODE_WAR_PENJUALAN = "PST";
        private int
            idx_nik = 0,
            idx_nama = 1,
            idx_devid = 2,
            idx_validator = 3;
        private const int BATAS_DOUBLE_ABSENSI = 3;
        private bool isNoRecord = false;
        private string filename;

        public FrmDownloadData()
        {
            InitializeComponent();
            ResetButton();

            txtFilename.Enabled = true;
            txtFilename.ReadOnly = true;

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

                    ArrayList arrWarehouse = new ArrayList(), arrJam = new ArrayList(),
                        arrDivisi = new ArrayList();

                    if (sqlDR.Read())
                    {
                        iID = sqlDR.GetOrdinal("KdWarehouse");
                        iDesc = sqlDR.GetOrdinal("NmWarehouse");

                        //arrWarehouse.Add(new ArrayWarehouse("all", "ALL"));
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
                        //cboWarehouse.SelectedValue = KODE_WAR_PENJUALAN;
                        cboWarehouse.SelectedIndex = -1;
                    }
                    else
                    {
                        cboWarehouse.DropDownStyle = ComboBoxStyle.DropDown;
                        cboWarehouse.Text = "Not Available";
                    }
                    sqlDR.Close();
                    sqlCmd.CommandText = "Get_Jam";
                    sqlCmd.Parameters.Clear();

                    sqlDR = sqlCmd.ExecuteReader();

                    if (sqlDR.Read())
                    {
                        iID = sqlDR.GetOrdinal("idjam");
                        iDesc = sqlDR.GetOrdinal("jam");

                        do
                        {
                            arrJam.Add(new ArrayJam(sqlDR.GetInt16(iID).ToString(), sqlDR.GetString(iDesc)));
                        }
                        while (sqlDR.Read());
                    }

                    if (arrJam.Count > 0)
                    {
                        cboJam.DropDownStyle = ComboBoxStyle.DropDownList;
                        cboJam.DataSource = arrJam;
                        cboJam.DisplayMember = DISP_JAM;
                        cboJam.ValueMember = VAL_JAM;
                        cboJam.SelectedIndex = -1;
                    }
                    else
                    {
                        cboJam.DropDownStyle = ComboBoxStyle.DropDown;
                        cboJam.Text = "Not Available";
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

        private void ResetButton()
        {
            btnLoadAbsensi.Enabled = btnLoadUser.Enabled =
                btnImport.Enabled = btnImportCSV.Enabled =
                btnSave.Enabled = btnSaveAbsensi.Enabled = false;
        }

        private void ResetDatagridviewColumns(string col1, string col2)
        {
            dgvGrid.DataSource = null;
            dgvGrid.Columns.Clear();
            dgvGrid.Columns.Add("_idxNIK", col1);
            dgvGrid.Columns.Add("_idxNama", col2);
            dgvGrid.Rows.Clear();
        }

        private void ResetDatagridviewColumns(string col1, string col2, string col3)
        {
            dgvGrid.DataSource = null;
            dgvGrid.Columns.Clear();
            dgvGrid.Columns.Add("_idxNIK", col1);
            dgvGrid.Columns.Add("_idxNama", col2);
            dgvGrid.Columns.Add("_idxMachineNo", col3);
            dgvGrid.Rows.Clear();
        }

        //function to get the user data from a filename
        private void btnLoadData_Click(object sender, EventArgs e)
        {
            if (tabAbsensiMethod.SelectedTab == tabUSB)
                LoadDataViaUSB_Fingertec();
            else
                LoadDataViaIPAddress_Solution();
        }

        //Download user's 9.0 or 10.0 arithmetic fingerprint templates(in strings)
        //Only TFT screen devices with firmware version Ver 6.60 version later support function "GetUserTmpExStr" and "GetUserTmpEx".
        //While you are using 9.0 fingerprint arithmetic and your device's firmware version is under ver6.60,you should use the functions "SSR_GetUserTmp" or 
        //"SSR_GetUserTmpStr" instead of "GetUserTmpExStr" or "GetUserTmpEx" in order to download the fingerprint templates.
        private void LoadDataViaIPAddress_Solution()
        {
            if (bIsConnected == false)
            {
                MessageBox.Show("Please connect the device first!", "Error");
                return;
            }

            string sdwEnrollNumber = "";
            string sName = "";
            string sPassword = "";
            int iPrivilege = 0;
            bool bEnabled = false;

            int idwFingerIndex;
            string sTmpData = "";
            int iTmpLength = 0;
            int iFlag = 0;

            axCZKEM1.EnableDevice(iMachineNumber, false);

            ResetDatagridviewColumns("NIK", "Nama");

            Cursor = Cursors.WaitCursor;


            axCZKEM1.ReadAllUserID(iMachineNumber);//read all the user information to the memory
            //axCZKEM1.ReadAllTemplate(iMachineNumber);//read all the users' fingerprint templates to the memory
            while (axCZKEM1.SSR_GetAllUserInfo(iMachineNumber, out sdwEnrollNumber, out sName, out sPassword, out iPrivilege, out bEnabled))//get all the users' information from the memory
            {
                dgvGrid.Rows.Add(new string[] { sdwEnrollNumber, sName });

                //for (idwFingerIndex = 0; idwFingerIndex < 10; idwFingerIndex++)
                //{
                //    if (axCZKEM1.GetUserTmpExStr(iMachineNumber, sdwEnrollNumber, idwFingerIndex, out iFlag, out sTmpData, out iTmpLength))//get the corresponding templates string and length from the memory
                //    {
                //        ListViewItem list = new ListViewItem();
                //        list.Text = sdwEnrollNumber;
                //        list.SubItems.Add(sName);
                //        list.SubItems.Add(idwFingerIndex.ToString());
                //        list.SubItems.Add(sTmpData);
                //        list.SubItems.Add(iPrivilege.ToString());
                //        list.SubItems.Add(sPassword);
                //        if (bEnabled == true)
                //        {
                //            list.SubItems.Add("true");
                //        }
                //        else
                //        {
                //            list.SubItems.Add("false");
                //        }
                //        list.SubItems.Add(iFlag.ToString());
                //        lvDownload.Items.Add(list);
                //    }
                //}
            }

            axCZKEM1.EnableDevice(iMachineNumber, true);
            Cursor = Cursors.Default;

            if (dgvGrid.Rows.Count > 1)
            {
                btnSave.Enabled = true;
                btnSaveAbsensi.Enabled = false;
            }

            btnConnect.PerformClick();
        }

        private void LoadDataViaUSB_Fingertec()
        {
            filename = txtFilename.Text;
            if (!File.Exists(filename))
            {
                MessageBox.Show("File tidak ditemukan...!", Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //short Index = btnGetUserFileUSB.//GetIndex(sender);
            object record = "";
            int enrollNo = 0;
            string name_Renamed = "";
            string pwd = "";
            int priv = 0;
            int TZgroup = 0;
            string TZinfo = "";
            string fileName = "";
            string[] row = null;

            ResetDatagridviewColumns("NIK", "Nama");

            if (txtFilename.Text == "")
            {
                MessageBox.Show("Required Field: FileName!!");
            }
            else
            {
                fileName = txtFilename.Text;

                if (axBioBridgeSDK1.ReadUserFile(fileName) == 0)
                {
                    do //read the user file into the memory buffer
                    {
                        if (enrollNo > 0)
                        {
                            row = new string[] { Convert.ToString(enrollNo), name_Renamed };
                            dgvGrid.Rows.Add(row);
                        }
                        //List1.Items.Add(("Enroll No: " + Convert.ToString(enrollNo) + " Name: " + name_Renamed));

                        //List1.Items.Add(("Enroll No: " + Convert.ToString(enrollNo) + " Name: " + name_Renamed + " Pwd: " + pwd + " Privilege: " + Convert.ToString(priv) + " TimeZone G: " + Convert.ToString(TZgroup) + " TimeZone Info: " + TZinfo));
                        //txtEnrollNoUSB.Text = Convert.ToString(enrollNo);
                        //txtNameUSB.Text = name_Renamed;
                        //txtPasswordUSB.Text = pwd;
                        //txtPriviledgeUSB.Text = Convert.ToString(priv);
                        //txtTimeZoneGUSB.Text = Convert.ToString(TZgroup);
                        //txtTimeZoneInfoUSB.Text = TZinfo;
                        //txtRecordSize.Text = Convert.ToString(record);
                    } while (axBioBridgeSDK1.GetUserFileData(ref enrollNo, ref name_Renamed, ref pwd, ref priv, ref TZgroup, ref TZinfo) == 0);

                    if (dgvGrid.Rows.Count > 1)
                    {
                        btnSave.Enabled = true;
                        btnSaveAbsensi.Enabled = false;
                    }
                }
                else
                {
                    btnSave.Enabled = btnSaveAbsensi.Enabled = false;
                    MessageBox.Show("No Record Found!!");
                }
            }
        }

        private void btnSelectData_Click(object sender, EventArgs e)
        {
            bool isdatfile, iscsvfile, isxlsfile;
            
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.ShowDialog();
            txtFilename.Text = openFileDialog1.FileName;
            filename = txtFilename.Text;

            ResetButton();

            try
            {
                isdatfile = IsFileWithExtension(txtFilename.Text, ".dat");
                iscsvfile = IsFileWithExtension(txtFilename.Text, ".csv");
                isxlsfile = IsFileWithExtension(txtFilename.Text, ".xls", ".xlsx");

                btnLoadAbsensi.Enabled = btnLoadUser.Enabled = isdatfile;
                btnImport.Enabled = isxlsfile;
                btnImportCSV.Enabled = iscsvfile;
            }
            catch
            {
            }
        }

        private bool IsFileWithExtension(string fileName, params string[] extensions)
        {
            bool isfilewithextension = false, isfilewithextension2 = false;

            isfilewithextension = (fileName != null);

            foreach(string extension in extensions)
                isfilewithextension2 = isfilewithextension2 || fileName.EndsWith(extension, StringComparison.Ordinal);

            return isfilewithextension && isfilewithextension2;
                
        }

        private void btnLoadAbsensi_Click(object sender, EventArgs e)
        {
            if (tabAbsensiMethod.SelectedTab == tabUSB)
                LoadLogUSB_Fingertec();
            else
                LoadLogViaIPAddress_Solution();
        }

        private void LoadLogViaIPAddress_Solution()
        {
            if (bIsConnected == false)
            {
                MessageBox.Show("Please connect the device first", "Error");
                return;
            }

            string sdwEnrollNumber = "";
            int idwTMachineNumber = 0;
            int idwEMachineNumber = 0;
            int idwVerifyMode = 0;
            int idwInOutMode = 0;
            int idwYear = 0;
            int idwMonth = 0;
            int idwDay = 0;
            int idwHour = 0;
            int idwMinute = 0;
            int idwSecond = 0;
            int idwWorkcode = 0;

            int idwErrorCode = 0;
            int iGLCount = 0;
            int iIndex = 0;

            string devID = txtDevId.Text;

            Cursor = Cursors.WaitCursor;

            ResetDatagridviewColumns("NIK", "Clock Date & Time", "Device ID");

            axCZKEM1.EnableDevice(iMachineNumber, false);//disable the device


            if (axCZKEM1.ReadGeneralLogData(iMachineNumber))//read all the attendance records to the memory
            {
                while (axCZKEM1.SSR_GetGeneralLogData(iMachineNumber, out sdwEnrollNumber, out idwVerifyMode,
                           out idwInOutMode, out idwYear, out idwMonth, out idwDay, out idwHour, out idwMinute, out idwSecond, ref idwWorkcode))//get records from the memory
                {
                    //dgvGrid.Rows.Add(new string[] { sdwEnrollNumber.ToString(), idwYear.ToString() + "-" + idwMonth.ToString("00") + "-" + idwDay.ToString("00") + " " + idwHour.ToString("00") + ":" + idwMinute.ToString("00") + ":" + idwSecond.ToString("00"), devID });
                    dgvGrid.Rows.Add(new string[] { sdwEnrollNumber.ToString(), idwYear.ToString() + "-" + idwMonth.ToString() + "-" + idwDay.ToString() + " " + idwHour.ToString() + ":" + idwMinute.ToString() + ":" + idwSecond.ToString(), devID });
                    //lvLogs.Items[iIndex].SubItems.Add(sdwEnrollNumber);//modify by Darcy on Nov.26 2009
                    //lvLogs.Items[iIndex].SubItems.Add(idwYear.ToString() + "-" + idwMonth.ToString() + "-" + idwDay.ToString() + " " + idwHour.ToString() + ":" + idwMinute.ToString() + ":" + idwSecond.ToString());

                    iGLCount++;
                    //lvLogs.Items.Add(iGLCount.ToString());
                    //lvLogs.Items[iIndex].SubItems.Add(sdwEnrollNumber);//modify by Darcy on Nov.26 2009
                    //lvLogs.Items[iIndex].SubItems.Add(idwVerifyMode.ToString());
                    //lvLogs.Items[iIndex].SubItems.Add(idwInOutMode.ToString());
                    //lvLogs.Items[iIndex].SubItems.Add(idwYear.ToString() + "-" + idwMonth.ToString() + "-" + idwDay.ToString() + " " + idwHour.ToString() + ":" + idwMinute.ToString() + ":" + idwSecond.ToString());
                    //lvLogs.Items[iIndex].SubItems.Add(idwWorkcode.ToString());
                    //iIndex++;
                }
            }
            else
            {
                Cursor = Cursors.Default;
                axCZKEM1.GetLastError(ref idwErrorCode);

                if (idwErrorCode != 0)
                {
                    MessageBox.Show("Reading data from terminal failed,ErrorCode: " + idwErrorCode.ToString(), "Error");
                }
                else
                {
                    MessageBox.Show("No data from terminal returns!", "Error");
                }
            }
            axCZKEM1.EnableDevice(iMachineNumber, true);//enable the device
            Cursor = Cursors.Default;

            if (dgvGrid.Rows.Count > 1)
            {
                btnSave.Enabled = false;
                btnSaveAbsensi.Enabled = true;
            }

            btnConnect.PerformClick();
        }


        private void LoadLogUSB_Fingertec()
        {
            filename = txtFilename.Text;
            if (!File.Exists(filename))
            {
                MessageBox.Show("File tidak ditemukan...!", Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string val1 = "";
            string val2 = "";
            string tmp = "";

            int fLen = FileSystem.FreeFile();
            FileSystem.FileOpen(fLen, txtFilename.Text, OpenMode.Binary, OpenAccess.Read, OpenShare.Shared, -1);

            tmp = new String(' ', Convert.ToInt32(FileSystem.FileLen(txtFilename.Text)));

            FileSystem.FileGet(fLen, ref tmp, -1, false);
            FileSystem.FileClose(fLen);

            val1 = tmp;
            string ReadFile = val1;

            int iRead = 10000;
            int iPos = 0;
            String FullData = "";
            String SubData = "";
            string[] splitter, splitter2;
            char[] par = new char[] { '\r', '\n' };
            string userid = string.Empty, tgl = string.Empty, devID = string.Empty;
            string[] row = null;

            if (chkSolutions.Checked)
                FullData = val1;
            else
            {
                while (val1.Length > iPos)
                {
                    if ((iPos + iRead) > val1.Length)
                        iRead = val1.Length - iPos;

                    val2 = "";
                    SubData = val1.Substring(iPos, iRead);

                    if (axBioBridgeSDK1.DecryptLog(SubData, ref val2) == 0)
                        FullData = FullData + val2;

                    iPos = iPos + iRead;
                }
            }

            ResetDatagridviewColumns("NIK", "Clock Date & Time", "Device ID");

            splitter = FullData.Split(par);
            foreach (string a in splitter)
            {
                if (a == "") continue;
                splitter2 = a.Split('\t');
                if (splitter2.GetUpperBound(0) > 1)
                {
                    userid = splitter2[0];
                    tgl = splitter2[1];
                    devID = splitter2[2];

                    row = new string[] { userid.Trim(), tgl, devID };
                    dgvGrid.Rows.Add(row);

                    //List1.Items.Add(userid + '\t' + tgl);
                }
            }

            if (dgvGrid.Rows.Count > 1)
            {
                btnSave.Enabled = false;
                btnSaveAbsensi.Enabled = true;
            }
            //groupBox2.PerformLayout();
            //groupBox2.Refresh();
            //btnSaveAbsensi.PerformLayout();
            //btnSaveAbsensi.Refresh();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveUserUSB_Fingertec();
        }

        private void SaveUserUSB_Fingertec()
        {
            if (cboWarehouse.SelectedIndex == -1)
            {
                MessageBox.Show("Silakan pilih cabang outlet untuk karyawan ini...!", Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                cboWarehouse.Focus();
                return;
            }

            if (cboJam.SelectedIndex == -1)
            {
                MessageBox.Show("Silakan pilih shift untuk karyawan ini...!", Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                cboJam.Focus();
                return;
            }

            DialogResult dr;

            btnSave.Enabled = false;
            SaveTemporaryKaryawan();
            GetTemporaryKaryawan();

            if (isNoRecord)
            {
                MessageBox.Show("Data ini telah pernah disimpan sebelumnya...!", Title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                dr = MessageBox.Show("Are you sure want to save this data ?", Title, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                if (dr == DialogResult.No)
                {
                    btnSelectData.Focus();
                    return;
                }
                SaveKaryawan();

                MessageBox.Show("Data has been saved!", Title, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void GetTemporaryKaryawan()
        {
            try
            {
                using (SqlConnection sqlCnn = new SqlConnection(ZFame.Classes.clsVarProgram.DB_CONN_STRING))
                {
                    string[] row;
                    int iNIK, iName;

                    SqlCommand sqlCmd = new SqlCommand();
                    sqlCmd.Connection = sqlCnn;
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.CommandText = "get_validate_karyawan";

                    sqlCnn.Open();

                    SqlDataReader dr = sqlCmd.ExecuteReader();

                    iNIK = dr.GetOrdinal("NIK");
                    iName = dr.GetOrdinal("nama");

                    if (dgvGrid.DataSource == null)
                        dgvGrid.Rows.Clear();
                    else
                        dgvGrid.DataSource = null;

                    if (dr.Read())
                    {
                        isNoRecord = false;
                        do
                        {
                            row = new string[]{
                                dr.GetInt32(iNIK).ToString(), 
                                dr.GetString(iName)
                            };

                            dgvGrid.Rows.Add(row);
                        } while (dr.Read());
                    }
                    else
                    {
                        isNoRecord = true;
                    }
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("Cannot Load Data Detail!" + Environment.NewLine + Environment.NewLine +
                    exc.Message, Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SaveTemporaryKaryawan()
        {
            try
            {
                                using (SqlConnection sqlCnn = new SqlConnection(ZFame.Classes.clsVarProgram.DB_CONN_STRING))
                {
                    sqlCnn.Open();

                    SqlTransaction sqlTrans = sqlCnn.BeginTransaction();

                    SqlCommand sqlCmd = sqlCnn.CreateCommand();
                    sqlCmd.Transaction = sqlTrans;

                    try
                    {
                        sqlCmd.CommandType = CommandType.Text;
                        sqlCmd.CommandText = "delete from tmp_karyawan";
                        sqlCmd.ExecuteNonQuery();

                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.CommandText = "ins_tmpkaryawan";
                        sqlCmd.Parameters.Add("@nik", SqlDbType.Int);
                        sqlCmd.Parameters.Add("@nama", SqlDbType.NVarChar, 255);

                        int nik = 0;

                        foreach (DataGridViewRow row in dgvGrid.Rows)
                        {
                            if (!row.IsNewRow)
                            {
                                int.TryParse(row.Cells[idx_nik].Value.ToString().Trim(), out nik);

                                sqlCmd.Parameters["@nik"].Value = nik;
                                sqlCmd.Parameters["@nama"].Value = row.Cells[idx_nama].Value.ToString().Trim();

                                sqlCmd.ExecuteNonQuery();
                            }
                        }

                        sqlTrans.Commit();
                    }
                    catch (Exception sqlExc)
                    {
                        sqlTrans.Rollback();
                        MessageBox.Show("Error In Database...Data cannot be deleted!\n\n" +
                            sqlExc.Message, Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("Cannot Save Data!" + exc.Message, Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SaveKaryawan()
        {
            try
            {
                using (SqlConnection sqlCnn = new SqlConnection(ZFame.Classes.clsVarProgram.DB_CONN_STRING))
                {
                    sqlCnn.Open();

                    SqlTransaction sqlTrans = sqlCnn.BeginTransaction();

                    SqlCommand sqlCmd = sqlCnn.CreateCommand();
                    sqlCmd.Transaction = sqlTrans;

                    try
                    {
                        /*
                        sqlCmd.CommandType = CommandType.Text;
                        sqlCmd.CommandText = "delete from ms_karyawan";
                        sqlCmd.ExecuteNonQuery();
                        */

                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.CommandText = "ins_karyawan";
                        sqlCmd.Parameters.Add("@nik", SqlDbType.Int);
                        sqlCmd.Parameters.Add("@KdCabang", SqlDbType.VarChar, 5).Value = KdCabang;
                        sqlCmd.Parameters.Add("@KdWarehouse", SqlDbType.VarChar, 3).Value = cboWarehouse.SelectedValue.ToString();
                        sqlCmd.Parameters.Add("@nama", SqlDbType.NVarChar, 255);    
                        sqlCmd.Parameters.Add("@idjam", SqlDbType.SmallInt).Value = cboJam.SelectedValue.ToString();
                        sqlCmd.Parameters.Add("@Divisi", SqlDbType.SmallInt).Value = cboDivisi.SelectedValue.ToString();                        
                        sqlCmd.Parameters.Add("@UserID", SqlDbType.NVarChar, 10).Value = "admin";

                        int nik = 0;

                        foreach (DataGridViewRow row in dgvGrid.Rows)
                        {
                            if (!row.IsNewRow)
                            {
                                int.TryParse(row.Cells[idx_nik].Value.ToString(), out nik);

                                sqlCmd.Parameters["@nik"].Value = nik;
                                sqlCmd.Parameters["@nama"].Value = row.Cells[idx_nama].Value.ToString();

                                sqlCmd.ExecuteNonQuery();
                            }
                        }

                        sqlTrans.Commit();
                        MessageBox.Show("Data has been saved!", Title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception sqlExc)
                    {
                        sqlTrans.Rollback();
                        MessageBox.Show("Error In Database...Data cannot be deleted!\n\n" +
                            sqlExc.Message, Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("Cannot Save Data!" + exc.Message, Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSaveAbsensi_Click(object sender, EventArgs e)
        {
            SaveLogUSB_Fingertec();
        }

        private void SaveLogUSB_Fingertec()
        {
            DialogResult dr;

            SaveTemporaryAbsensi();
            GetTemporaryAbsensi();

            if (isNoRecord)
            {
                MessageBox.Show("Data ini telah pernah disimpan sebelumnya...!", Title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (dgvGrid.Columns.Count <= idx_validator)
                    dgvGrid.Columns.Add("_val", "validator");

                FormatTemporaryAbsensi();

                dr = MessageBox.Show("Are you sure want to save this data ?", Title, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                if (dr == DialogResult.No)
                {
                    btnSelectData.Focus();
                    return;
                }
                SaveAbsensi();
                SaveAbsensiFilter();

                ResetDatagridviewColumns("NIK", "Nama");
                MessageBox.Show("Data has been saved!", Title, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void FormatTemporaryAbsensi()
        {
            int tmpnik, nik = 0, jmlRows = dgvGrid.Rows.Count;
            DateTime tmptgl = DateTime.Now, tgl = DateTime.Now;
            TimeSpan span;
            double totalMinutes;

            for (int i = 0; i < jmlRows - 1; i++)
            {
                tmpnik = int.Parse( dgvGrid.Rows[i].Cells[idx_nik].Value.ToString() );
                tmptgl = DateTime.Parse(dgvGrid.Rows[i].Cells[idx_nama].Value.ToString());

                if ( !string.IsNullOrEmpty( (string)dgvGrid.Rows[i].Cells[idx_validator].Value ) )
                    continue;

                nik = tmpnik;
                tgl = tmptgl;

                for (int j = i+1; j < jmlRows - 1; j++)
                {
                    tmpnik = int.Parse(dgvGrid.Rows[j].Cells[idx_nik].Value.ToString() );
                    tmptgl = DateTime.Parse(dgvGrid.Rows[j].Cells[idx_nama].Value.ToString());

                    if (nik != tmpnik)
                        break;
                    if (tgl.Date.Day != tmptgl.Date.Day)
                        break;
                    span = tmptgl - tgl;
                    totalMinutes = span.TotalMinutes;

                    if (totalMinutes <= BATAS_DOUBLE_ABSENSI)
                    {
                        dgvGrid.Rows[j].Cells[idx_validator].Value = "1";
                        //break;
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            btnLoadAbsensi.PerformClick();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            btnSaveAbsensi.PerformClick();
        }

        private void tabAbsensiMethod_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabAbsensiMethod.SelectedTab == tabIPAddress)
            {
                btnLoadAbsensi.Enabled = btnLoadUser.Enabled = true;
                btnImport.Enabled = btnImportCSV.Enabled = false;
            }
            else
            {
                btnLoadAbsensi.Enabled = btnLoadUser.Enabled =
                    btnImport.Enabled = btnImportCSV.Enabled = false;
            }
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            ConnectSolutionViaUSB();
        }

        private void ConnectSolutionViaUSB()
        {
            if (txtIP.Text.Trim() == "" || txtPort.Text.Trim() == "")
            {
                MessageBox.Show("IP and Port cannot be null", "Error");
                return;
            }
            int idwErrorCode = 0;

            Cursor = Cursors.WaitCursor;
            if (btnConnect.Text == "DisConnect")
            {
                axCZKEM1.Disconnect();
                bIsConnected = false;
                btnConnect.Text = "Connect";
                lblState.Text = "Current State:DisConnected";
                Cursor = Cursors.Default;
                return;
            }

            bIsConnected = axCZKEM1.Connect_Net(txtIP.Text, Convert.ToInt32(txtPort.Text));
            if (bIsConnected == true)
            {
                btnConnect.Text = "DisConnect";
                btnConnect.Refresh();
                lblState.Text = "Current State:Connected";
                iMachineNumber = 1;//In fact,when you are using the tcp/ip communication,this parameter will be ignored,that is any integer will all right.Here we use 1.
                axCZKEM1.RegEvent(iMachineNumber, 65535);//Here you can register the realtime events that you want to be triggered(the parameters 65535 means registering all)
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                MessageBox.Show("Unable to connect the device,ErrorCode=" + idwErrorCode.ToString(), "Error");
            }
            Cursor = Cursors.Default;
        }

        private void btnRptHistoryAbsensi_Click(object sender, EventArgs e)
        {
            FrmRpt_HistoryAbsensi rptAbsensi = new FrmRpt_HistoryAbsensi();
            rptAbsensi.Show();
        }

        private void GetTemporaryAbsensi()
        {
            try
            {
                using (SqlConnection sqlCnn = new SqlConnection(ZFame.Classes.clsVarProgram.DB_CONN_STRING))
                {
                    string[] row;
                    int iNIK, iClock, iDevID;

                    SqlCommand sqlCmd = new SqlCommand();
                    sqlCmd.Connection = sqlCnn;
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.CommandText = "get_validaterecord";

                    sqlCnn.Open();

                    SqlDataReader dr = sqlCmd.ExecuteReader();

                    iNIK = dr.GetOrdinal("NIK");
                    iClock = dr.GetOrdinal("clock_date");
                    iDevID = dr.GetOrdinal("devid");

                    try
                    {
                        dgvGrid.Rows.Clear();
                    }
                    catch
                    {
                        dgvGrid.DataSource = null;
                        dgvGrid.Columns.Add("_nik", "NIK");
                        dgvGrid.Columns.Add("_nama", "Nama");
                        dgvGrid.Columns.Add("_devID", "Device ID");
                    }

                    //if (dgvGrid.DataSource == null)
                    //    dgvGrid.Rows.Clear();
                    //else
                    //{
                    //    dgvGrid.DataSource = null;
                    //    dgvGrid.Columns.Add("_nik", "NIK");
                    //    dgvGrid.Columns.Add("_nama", "Nama");                        
                    //}

                    if (dr.Read())
                    {
                        isNoRecord = false;
                        do
                        {
                            row = new string[]{
                                dr.GetInt32(iNIK).ToString(), 
                                dr.GetDateTime(iClock).ToString(),
                                dr.GetInt32(iDevID).ToString()
                            };

                            dgvGrid.Rows.Add(row);
                        } while (dr.Read());
                    }
                    else
                    {
                        isNoRecord = true;
                    }
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("Cannot Load Data Detail!" + Environment.NewLine + Environment.NewLine +
                    exc.Message, Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SaveTemporaryAbsensi()
        {

            try
            {
                using (SqlConnection sqlCnn = new SqlConnection(ZFame.Classes.clsVarProgram.DB_CONN_STRING))
                {
                    sqlCnn.Open();

                    SqlTransaction sqlTrans = sqlCnn.BeginTransaction();

                    SqlCommand sqlCmd = sqlCnn.CreateCommand();
                    sqlCmd.Transaction = sqlTrans;

                    try
                    {
                        sqlCmd.CommandType = CommandType.Text;
                        sqlCmd.CommandText = "truncate table tmp_absensi";
                        sqlCmd.ExecuteNonQuery();

                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.CommandText = "ins_tmpabsensi";
                        sqlCmd.Parameters.Add("@nik", SqlDbType.Int);
                        sqlCmd.Parameters.Add("@clock_date", SqlDbType.DateTime);
                        sqlCmd.Parameters.Add("@devid", SqlDbType.Int);

                        int nik = 0, devid = 0;
                        DateTime clock_date;

                        foreach (DataGridViewRow row in dgvGrid.Rows)
                        {
                            if (!row.IsNewRow)
                            {
                                int.TryParse(row.Cells[idx_nik].Value.ToString().Trim(), out nik);
                                DateTime.TryParse(row.Cells[idx_nama].Value.ToString().Trim(), out clock_date);
                                int.TryParse(row.Cells[idx_devid].Value.ToString().Trim(), out devid);

                                clock_date = clock_date.AddMilliseconds(-clock_date.Millisecond);
                                sqlCmd.Parameters["@nik"].Value = nik;
                                sqlCmd.Parameters["@clock_date"].Value = clock_date;
                                sqlCmd.Parameters["@devid"].Value = devid;

                                sqlCmd.ExecuteNonQuery();
                            }
                        }

                        sqlTrans.Commit();
                    }
                    catch (Exception sqlExc)
                    {
                        sqlTrans.Rollback();
                        MessageBox.Show("Error In Database...Data cannot be deleted!\n\n" +
                            sqlExc.Message, Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("Cannot Save Data!" + exc.Message, Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SaveAbsensi()
        {
            try
            {
                using (SqlConnection sqlCnn = new SqlConnection(ZFame.Classes.clsVarProgram.DB_CONN_STRING))
                {
                    sqlCnn.Open();

                    SqlTransaction sqlTrans = sqlCnn.BeginTransaction();

                    SqlCommand sqlCmd = sqlCnn.CreateCommand();
                    sqlCmd.Transaction = sqlTrans;

                    try
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.CommandText = "ins_trxabsensi";
                        sqlCmd.Parameters.Add("@nik", SqlDbType.Int);
                        sqlCmd.Parameters.Add("@clock_date", SqlDbType.Date);
                        sqlCmd.Parameters.Add("@clock_time", SqlDbType.Time);
                        sqlCmd.Parameters.Add("@devid", SqlDbType.Int);

                        int nik = 0, devid = 0;
                        DateTime clock_date, clock_time;

                        foreach (DataGridViewRow row in dgvGrid.Rows)
                        {
                            if (!row.IsNewRow)
                            {
                                int.TryParse(row.Cells[idx_nik].Value.ToString().Trim(), out nik);
                                DateTime.TryParse(row.Cells[idx_nama].Value.ToString().Trim(), out clock_date);
                                DateTime.TryParse(row.Cells[idx_nama].Value.ToString().Trim(), out clock_time);
                                int.TryParse(row.Cells[idx_devid].Value.ToString().Trim(), out devid);

                                sqlCmd.Parameters["@nik"].Value = nik;
                                sqlCmd.Parameters["@clock_date"].Value = clock_date.Date;
                                sqlCmd.Parameters["@clock_time"].Value = clock_time.TimeOfDay;
                                sqlCmd.Parameters["@devid"].Value = devid;

                                sqlCmd.ExecuteNonQuery();
                            }
                        }

                        sqlTrans.Commit();
                    }
                    catch (Exception sqlExc)
                    {
                        sqlTrans.Rollback();
                        MessageBox.Show("Error In Database...Data cannot be deleted!\n\n" +
                            sqlExc.Message, Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("Cannot Save Data!" + exc.Message, Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SaveAbsensiFilter()
        {
            try
            {
                using (SqlConnection sqlCnn = new SqlConnection(ZFame.Classes.clsVarProgram.DB_CONN_STRING))
                {
                    sqlCnn.Open();

                    SqlTransaction sqlTrans = sqlCnn.BeginTransaction();

                    SqlCommand sqlCmd = sqlCnn.CreateCommand();
                    sqlCmd.Transaction = sqlTrans;

                    try
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.CommandText = "ins_trxabsensi_filter";
                        sqlCmd.Parameters.Add("@nik", SqlDbType.Int);
                        sqlCmd.Parameters.Add("@clock_date", SqlDbType.Date);
                        sqlCmd.Parameters.Add("@clock_time", SqlDbType.Time);
                        sqlCmd.Parameters.Add("@devid", SqlDbType.Int);

                        int nik = 0, devid = 0;
                        DateTime clock_date, clock_time;

                        foreach (DataGridViewRow row in dgvGrid.Rows)
                        {
                            if (!row.IsNewRow)
                            {
                                if (!string.IsNullOrEmpty((string)row.Cells[idx_validator].Value)) continue;

                                int.TryParse(row.Cells[idx_nik].Value.ToString().Trim(), out nik);
                                DateTime.TryParse(row.Cells[idx_nama].Value.ToString().Trim(), out clock_date);
                                DateTime.TryParse(row.Cells[idx_nama].Value.ToString().Trim(), out clock_time);
                                int.TryParse(row.Cells[idx_devid].Value.ToString().Trim(), out devid);

                                sqlCmd.Parameters["@nik"].Value = nik;
                                sqlCmd.Parameters["@clock_date"].Value = clock_date.Date;
                                sqlCmd.Parameters["@clock_time"].Value = clock_time.TimeOfDay;
                                sqlCmd.Parameters["@devid"].Value = devid;

                                sqlCmd.ExecuteNonQuery();
                            }
                        }

                        sqlTrans.Commit();
                    }
                    catch (Exception sqlExc)
                    {
                        sqlTrans.Rollback();
                        MessageBox.Show("Error In Database...Data cannot be deleted!\n\n" +
                            sqlExc.Message, Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("Cannot Save Data!" + exc.Message, Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            filename = txtFilename.Text;
            if (!File.Exists(filename))
            {
                MessageBox.Show("File tidak ditemukan...!", Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            } 
            
            string[] name = GetExcelSheetNames(txtFilename.Text);
            String constr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
                            txtFilename.Text +
                            ";Extended Properties='Excel 12.0 XML;HDR=YES;';";

            OleDbConnection con = new OleDbConnection(constr);
            OleDbCommand oconn = new OleDbCommand("Select * From [" + name[0] + "]", con);
            con.Open();

            OleDbDataAdapter sda = new OleDbDataAdapter(oconn);
            DataTable data = new DataTable();
            sda.Fill(data);

            dgvGrid.Columns.Clear();
            dgvGrid.DataSource = data;

            btnSave.Enabled = true;
            btnSaveAbsensi.Enabled = true;
        }

        public string[] GetExcelSheetNames(string excelFileName)
        {
            OleDbConnection con = null;
            DataTable dt = null;
            //String constr = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + excelFileName + ";Extended Properties=Excel 8.0;";

            String constr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
                excelFileName + ";Extended Properties='Excel 12.0 XML;HDR=YES;';";

            con = new OleDbConnection(constr);
            con.Open();
            dt = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

            if (dt == null)
            {
                return null;
            }

            String[] excelSheetNames = new String[dt.Rows.Count];
            int i = 0;

            foreach (DataRow row in dt.Rows)
            {
                excelSheetNames[i] = row["TABLE_NAME"].ToString();
                i++;
            }

            return excelSheetNames;
        }

        private void btnImportCSV_Click(object sender, EventArgs e)
        {
            filename = txtFilename.Text;
            if (!File.Exists(filename))
            {
                MessageBox.Show("File tidak ditemukan...!", Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            } 
            
            string constr = @"Driver={Microsoft Text Driver (*.txt; *.csv)};Dbq=" + Path.GetDirectoryName(Path.GetFullPath(txtFilename.Text)) + ";Extensions=csv,txt";

            OdbcConnection con = new OdbcConnection(constr);
            OdbcDataAdapter sda = new OdbcDataAdapter("Select * From [" + Path.GetFileName(txtFilename.Text) + "]", con);

            con.Open();

            DataTable data = new DataTable(txtFilename.Text);
            sda.Fill(data);

            dgvGrid.Columns.Clear();
            dgvGrid.DataSource = data;

            btnSave.Enabled = true;
            btnSaveAbsensi.Enabled = true;
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            FrmRpt_Absensi rptAbsensi = new FrmRpt_Absensi();
            rptAbsensi.Show();
        }

        private void btnRptRekapAbsensi_Click(object sender, EventArgs e)
        {
            FrmRpt_RekapAbsensi rptAbsensi = new FrmRpt_RekapAbsensi();
            rptAbsensi.Show();
        }

        private void btnPrintUM_Click(object sender, EventArgs e)
        {
            FrmRpt_RekapUM rptAbsensi = new FrmRpt_RekapUM();
            rptAbsensi.Show();
        }
    }

    public class ArrayJam
    {
        private string _ID, _Desc;

        public ArrayJam(string ID, string Description)
        {
            this._ID = ID;
            this._Desc = Description;
        }

        public string DisplayJam
        {
            get
            {
                return _Desc;
            }
        }

        public string ValueJam
        {
            get
            {
                return _ID;
            }
        }
    }

    public class ArrayWarehouse
    {
        private string _ID, _Desc;

        public ArrayWarehouse(string companyID, string company)
        {
            this._ID = companyID;
            this._Desc = company;
        }

        public string DisplayWarehouse
        {
            get
            {
                return _Desc;
            }
        }

        public string ValueWarehouse
        {
            get
            {
                return _ID;
            }
        }
    }

    public class ArrayAgama
    {
        private string _ID, _Desc;

        public ArrayAgama(string ID, string Description)
        {
            this._ID = ID;
            this._Desc = Description;
        }

        public string DisplayAgama
        {
            get
            {
                return _Desc;
            }
        }

        public string ValueAgama
        {
            get
            {
                return _ID;
            }
        }
    }

    public class ArrayDivisi
    {
        private string _ID, _Desc;

        public ArrayDivisi(string DivisiID, string Divisi)
        {
            this._ID = DivisiID;
            this._Desc = Divisi;
        }

        public string DisplayDivisi
        {
            get
            {
                return _Desc;
            }
        }

        public string ValueDivisi
        {
            get
            {
                return _ID;
            }
        }
    }

    public class ArrayAbsenSpesial
    {
        private string _ID, _Desc;

        public ArrayAbsenSpesial(string id, string description)
        {
            this._ID = id;
            this._Desc = description;
        }

        public string DisplayAbsenspesial
        {
            get
            {
                return _Desc;
            }
        }

        public string ValueAbsenspesial
        {
            get
            {
                return _ID;
            }
        }
    }

    
}
