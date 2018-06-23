using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace HRDProject
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ZFame.Security.Encryption funcEncrypt = new ZFame.Security.Encryption();
            string title, connString, kdCabang, warehouseIDDef, printerName, sqlServiceName;

            if (!funcEncrypt.DecryptConnString(out title, out connString, out kdCabang, out warehouseIDDef, out printerName, out sqlServiceName))
            {
                MessageBox.Show("File Connection String Cannot be found!!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
                return;
            }

            InfoApp.BranchID = kdCabang;
            InfoApp.PrinterName = printerName;
            InfoApp.Title = title;
            InfoApp.WarehouseIDDefaultPenjualan = warehouseIDDef;
            InfoApp.KODE_MSSERVERSERVICE = sqlServiceName;
            InfoApp.Default_Messagebox_Button = MessageBoxDefaultButton.Button1;
            InfoApp.DefaultParentBackgroundColour = System.Drawing.Color.AliceBlue;

            #region Set Icon Automatically
            InfoApp.MyAssemblyName = "HRDProject";
            InfoApp.IconPath = "icons";
            InfoApp.IconName = "AppIcon.ico";
            #endregion

            //ZFame.Classes.clsVarProgram.DB_CONN_STRING = ZFame.Classes.clsVarProgram._DB_CONN_STRING2;
            ZFame.Classes.clsVarProgram.DB_CONN_STRING = connString;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new FrmData_SettingOffKerja());
            //Application.Run(new FrmData_SettingAbsenSpesial());
            //Application.Run(new FrmData_SettingJamKerja());
            Application.Run(new FrmDownloadData());

            //Application.Run(new FrmData_SwitchOff());
            //Application.Run(new FrmData_Karyawan());
        }
    }
}
