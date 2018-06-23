using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZFame.Classes
{
    class clsVarProgram
    {
        public static bool IS_APP_CONNECTED = false;
        public static string 
            DATABASE_NAME = "MiniMarket",
            UID = "sa", PWD = "@sa", 
            DATABASE_SAFEFILE = DATABASE_NAME + ".mdf",
            DATABASE_PATH = @"c:\Program Files\Microsoft SQL Server\MSSQL.1\MSSQL\Data\",
            DATABASE_BACKUP_PATH = @"c:\Program Files\Microsoft SQL Server\MSSQL.1\MSSQL\Backup\",
            DATABASE_FILE = DATABASE_PATH + DATABASE_SAFEFILE,
            RPT_UID = Uid, RPT_PWD = Pwd, RPT_DATASOURCE = "dsDATA";
        //public static Uri REPORT_SERVER_URL = new Uri(@"http://localhost/ReportServer");
        public const string REPORT_PATH_SERVER = @"/rptMiniMarket/", 
            REPORT_PATH = @"Reports\";

        //.\SQLExpress;AttachDbFilename=E:\Program Files\Microsoft SQL Server\MSSQL.1\MSSQL\Data\DataInventory.mdf;
        //AttachDbFilename=" + DATABASE_FILE + |DataDirectory|mydbfile.mdf;
        public static string 
            _DB_CONN_STRING = @"Data Source=ZFServer;Database=" + Database_Name +
            ";Integrated Security=False;Connect Timeout=30; User Instance=False;User ID=" + Uid + ";Password=" + Pwd;

        ///
//        dbnmpntw  	Named Pipes
//dbmslpcn 	Shared Memory (local machine connections only, might fail when moving to production...)
//dbmssocn 	Winsock TCP/IP
//dbmsspxn 	SPX/IPX
//dbmsvinn 	Banyan Vines
//dbmsrpcn 	Multi-Protocol (Windows RPC)
//dbmsadsn 	Apple Talk
//dbmsgnet 	VIA
        //IP :    190.190.200.100,1433
        public static string _DB_CONN_STRING2 = @"Data Source=192.168.1.2,1433;Network Library=DBMSSOCN;Initial Catalog=" + Database_Name +
            ";Integrated Security=False;Connect Timeout=30; User Instance=False;User ID=" + Uid + ";Password=" + Pwd;

        public static string Database_Name
        {
            set;
            get;
        }

        public static string Uid
        {
            set;
            get;
        }

        public static string Pwd
        {
            set;
            get;
        }

        public static string DB_CONN_STRING_LITE
        {
            set;
            get;
        }

        public static string DB_CONN_STRING
        {
            set;
            get;
        }

    }
}
