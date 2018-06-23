using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace ZFame.Classes
{
    /*
private class gilo
{
        
    private void GetImagesFromDatabase()
{
    try
    {
        //Initialize SQL Server connection.
        SqlConnection CN = new SqlConnection(txtConnectionString.Text);

        //Initialize SQL adapter.
        SqlDataAdapter ADAP = new SqlDataAdapter("Select * from ImagesStore", CN);

        //Initialize Dataset.
        DataSet DS = new DataSet();

        //Fill dataset with ImagesStore table.
        ADAP.Fill(DS, "ImagesStore");

        //Fill Grid with dataset.
        dataGridView1.DataSource = DS.Tables["ImagesStore"];
    }
    catch(Exception ex)
    {
        MessageBox.Show(ex.ToString());
    }

  //Store image to a local file.
        pictureBox1.Image.Save("c:\test_picture.jpg",
System.Drawing.Imaging.ImageFormat.Jpeg);

}

        
//Open file into a filestream and 
//read data in a byte array.
    private byte[] ReadFile(string sPath)
{
    //Initialize byte array with a null value initially.
    byte[] data = null;

    //Use FileInfo object to get file size.
    FileInfo fInfo = new FileInfo(sPath);
    long numBytes = fInfo.Length;

    //Open FileStream to read file
    FileStream fStream = new FileStream(sPath, FileMode.Open, FileAccess.Read);

    //Use BinaryReader to read file stream into byte array.
    BinaryReader br = new BinaryReader(fStream);

    //When you use BinaryReader, you need to 

    //supply number of bytes to read from file.
    //In this case we want to read entire file. 

    //So supplying total number of bytes.
    data = br.ReadBytes((int)numBytes);
    return data;
}
   
    private void cmdSave_Click(object sender, EventArgs e)
{
try
{
    //Read Image Bytes into a byte array
    byte[] imageData = ReadFile(txtImagePath.Text);

    //Initialize SQL Server Connection
    SqlConnection CN = new SqlConnection(txtConnectionString.Text);

    //Set insert query
    string qry = "insert into ImagesStore (OriginalPath,ImageData) _
                                values(@OriginalPath, @ImageData)";

    //Initialize SqlCommand object for insert.
    SqlCommand SqlCom = new SqlCommand(qry, CN);

    //We are passing Original Image Path and 
    //Image byte data as sql parameters.
    SqlCom.Parameters.Add(new SqlParameter("@OriginalPath", 
                                (object)txtImagePath.Text));
                                    
    SqlCom.Parameters.Add(new SqlParameter("@ImageData", 
                                        (object)imageData));

    //Open connection and execute insert query.
    CN.Open();
    SqlCom.ExecuteNonQuery();
    CN.Close();

    //Close form and return to list or images.
    this.Close();
}
catch
{
}
}
}
*/

    class clsFunction
    {
        #region One Process Only
        /// <summary>
        /// Check running processes for an already-running instance. Implements a simple and
        /// always effective algorithm to find currently running processes with a main window
        /// matching a given substring and focus it.
        /// Combines code written by Lion Shi (MS) and Sam Allen.
        /// </summary>

        /// <summary>
            /// Stores a required string that must be present in the window title for it
            /// to be detected.
            /// </summary>
            private static string _requiredString;

            /// <summary>
            /// Contains signatures for C++ DLLs using interop.
            /// </summary>
            private static class NativeMethods
            {
                [DllImport("user32.dll")]
                public static extern bool ShowWindowAsync(IntPtr hWnd, int nCmdShow);

                [DllImport("user32.dll")]
                public static extern bool SetForegroundWindow(IntPtr hWnd);

                [DllImport("user32.dll")]
                public static extern bool EnumWindows(EnumWindowsProcDel lpEnumFunc,
                    Int32 lParam);

                [DllImport("user32.dll")]
                public static extern int GetWindowThreadProcessId(IntPtr hWnd,
                    ref Int32 lpdwProcessId);

                [DllImport("user32.dll")]
                public static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString,
                    Int32 nMaxCount);

                public const int SW_SHOWNORMAL = 1;
            }

            public delegate bool EnumWindowsProcDel(IntPtr hWnd, Int32 lParam);

            /// <summary>
            /// Perform finding and showing of running window.
            /// </summary>
            /// <returns>Bool, which is important and must be kept to match up
            /// with system call.</returns>
            static private bool EnumWindowsProc(IntPtr hWnd, Int32 lParam)
            {
                int processId = 0;
                NativeMethods.GetWindowThreadProcessId(hWnd, ref processId);

                StringBuilder caption = new StringBuilder(1024);
                NativeMethods.GetWindowText(hWnd, caption, 1024);

                // Use IndexOf to make sure our required string is in the title.
                if (processId == lParam && (caption.ToString().IndexOf(_requiredString,
                    StringComparison.OrdinalIgnoreCase) != -1))
                    {
                    // Restore the window.
                    NativeMethods.ShowWindowAsync(hWnd, NativeMethods.SW_SHOWNORMAL);
                    NativeMethods.SetForegroundWindow(hWnd);
                }
                return true; // Keep this.
            }

            /// <summary>
            /// Find out if we need to continue to load the current process. If we
            /// don't focus the old process that is equivalent to this one.
            /// </summary>
            /// <param name=forceTitle>This string must be contained in the window
            /// to restore. Use a string that contains the most
            /// unique sequence possible. If the program has windows with the string
            /// "Journal", pass that word.</param>
            /// <returns>False if no previous process was activated. True if we did
            /// focus a previous process and should simply exit the current one.</returns>
            public static bool IsOnlyProcess(string forceTitle)
            {
                _requiredString = forceTitle;

                foreach (System.Diagnostics.Process proc in System.Diagnostics.Process.GetProcessesByName(System.Diagnostics.Process.GetCurrentProcess().ProcessName))
                {
                    if (proc.Id != System.Diagnostics.Process.GetCurrentProcess().Id)
                    {
                        NativeMethods.EnumWindows(new EnumWindowsProcDel(EnumWindowsProc),
                            proc.Id);
                        return false;
                    }
                }
                return true;
            }
        #endregion

        //private const int SW_SHOWMINIMIZED = 2, 
        //    SW_SHOWMAXIMIZED = 3, SW_RESTORE = 9;

        //[DllImport("User32.dll")]
        //private static extern int ShowWindowAsync(IntPtr hWnd, int swCommand);
        //private static void SwitchToRunningApp(System.Diagnostics.Process[] RunningProcesses)
        //{
        //    ShowWindowAsync(RunningProcesses[0].MainWindowHandle, SW_SHOWMINIMIZED);
        //    ShowWindowAsync(RunningProcesses[0].MainWindowHandle, SW_RESTORE);
        //}

        public static bool IsAppRunning(out System.Diagnostics.Process[] currentProcess)
        {
            System.Diagnostics.Process[] RunningProcesses = null;
            try
            {
                RunningProcesses = System.Diagnostics.Process.GetProcessesByName(System.Diagnostics.Process.GetCurrentProcess().ProcessName);

                currentProcess = RunningProcesses;
                RunningProcesses = null;
                if (RunningProcesses.Length > 1)
                    return true;
                else
                    return false;
            }
            catch
            {
                RunningProcesses = null;
                currentProcess = null;
                return false;
            }
        }

        public static double PembulatanRatusan(double uang)
        {
            return PembulatanAngka(uang, 100);
        }

        private static double PembulatanAngka(double uang, double PEMBULATAN)
        {
            double pecahan;

            if (uang > PEMBULATAN)
            {
                pecahan = ((int)((uang % PEMBULATAN) / (PEMBULATAN / 2)) == 0 ? 0 : PEMBULATAN);
                uang = (Math.Floor((uang / PEMBULATAN)) * PEMBULATAN) + pecahan;
            }

            return uang;
        }

        [DllImport("wininet.dll")]
        private extern static bool InternetGetConnectedState(out int Description, int ReservedValue);
        public static bool IsConnectedToInternet()
        {
            int Desc;
            return InternetGetConnectedState(out Desc, 0);
        }
    }
}