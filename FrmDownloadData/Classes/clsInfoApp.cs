//created : Tuesday, 6 May 2014, 17:00 WIB

namespace HRDProject
{
	/// <summary>
	/// Represent an instance of Application's Information.
	/// </summary>
	public class InfoApp
	{
		#region Constructor
		/// <summary>
		/// Initialize a new instance of the ZFamily.InfoApp Class.
		/// </summary>
		public InfoApp()
		{
		}
		#endregion

		#region Properties

        public static int Def_Duplicate_Mutasi
        {
            set;
            get;
        }

        public static int Def_Duplicate_NewMutasi
        {
            set;
            get;
        }

        public static int Def_Duplicate_Opname
        {
            set;
            get;
        }

        public static int Def_Duplicate_POS
        {
            set;
            get;
        }


        public static int Def_Duplicate_POS_Grosir
        {
            set;
            get;
        }

        public static int Def_Duplicate_RetPOS
        {
            set;
            get;
        }

        public static int Def_Duplicate_Pembelian
        {
            set;
            get;
        }

        public static int Def_Duplicate_RetPembelian
        {
            set;
            get;
        }

        public static int Def_Duplicate_Penjualan
        {
            set;
            get;
        }

        public static int Def_Duplicate_RetPenjualan
        {
            set;
            get;
        }

        public static string IconName
        {
            set;
            get;
        }

        public static string IconPath
        {
            set;
            get;
        }

        public static string MyAssemblyName
        {
            set;
            get;
        }

        public static string KODETRX_NEWMUTASI
        {
            set;
            get;
        }
        
        public static string KODETRX_MUTASI
        {
            set;
            get;
        }

        public static string KODETRX_PEMBELIAN
        {
            set;
            get;
        }

        public static string KODETRX_PENJUALAN
        {
            set;
            get;
        }

        public static string KODETRX_PEMBELIANRET
        {
            set;
            get;
        }

        public static string KODETRX_PENJUALANRET
        {
            set;
            get;
        }

        public static string KODETRX_POS
        {
            set;
            get;
        }

        public static string KODETRX_POS_GROSIR
        {
            set;
            get;
        }

        public static string KODETRX_POSRET
        {
            set;
            get;
        }

        public static string KODETRX_POSRET_GROSIR
        {
            set;
            get;
        }

        public static string KODETRX_OPENING
        {
            set;
            get;
        }

        public static string KODETRX_STOCKOPNAME
        {
            set;
            get;
        }

        public static string KODETRX_STOCKOPNAME_BATCH
        {
            set;
            get;
        }

        public static string KODETRX_STOCKOPNAME_BATCH_PROCESSING
        {
            set;
            get;
        }

        public static string KODETRX_ASSEMBLY
        {
            set;
            get;
        }

        public static string KODETRX_DISASSEMBLY
        {
            set;
            get;
        }

        public static int MaxInsertAllowed
        {
            set;
            get;
        }

        public static decimal MaxAmbilAllowed
        {
            set;
            get;
        }
        
        public static System.Windows.Forms.MessageBoxDefaultButton Default_Messagebox_Button
        {
            set;
            get;
        }

        public static bool IsLocalServer
        {
            set;
            get;
        }

        public static bool IsHasClosedCassa
        {
            set;
            get;
        }

        public static char[] CHAR_SEPARATOR
        {
            set;
            get;
        }

        public static string[] HEADER
        {
            set;
            get;

        }

        public static string ValidationKey
        {
            set;
            get;
        }

        public static System.DateTime DateRegistered
        {
            set;
            get;
        }

        public static bool FullVersion
        {
            set;
            get;
        }

        public static string[] FOOTER
        {
            set;
            get;
        }

        public static int POS_TENGAH
        {
            set;
            get;
        }

        public static int JARAK_AWAL
        {
            set;
            get;
        }

        public static int SPACE_AKHIR_KERTAS
        {
            set;
            get;
        }

        public static int LEBAR_KERTAS
        {
            set;
            get;
        }

        public static System.DateTime DateEnd
        {
            set;
            get;
        }

		public static string Path_Database
		{
            set;
            get;
        }

        public static System.DateTime LoginTime
        {
            set;
            get;
        }

		public static string Path_Picture
		{
            set;
            get;
        }

		public static string Path_Report
		{
            set;
            get;
        }

        public static string BranchName
        {
            set;
            get;
        }

        public static string PrinterName
        {
            set;
            get;
        }

        public static string KODE_MSSERVERSERVICE
        {
            set;
            get;
        }

        public static string CassaNo
        {
            set;
            get;
        }

        public static string WarehouseIDDefaultPenjualan
        {
            set;
            get;
        }

        public static string BranchID
        {
            set;
            get;
        }

        public static System.Drawing.Color DefaultParentBackgroundColour
        {
            set;
            get;
        }

		public static string CompanyName
		{
            set;
            get;
        }

		public static string CompanyID
		{
            set;
            get;
        }

		/// <summary>
		/// Gets or Sets User Password.
		/// </summary>
        //public static string UserPassword
        //{
        //    set 
        //    { 
        //        strUserPwd = value; 
        //    }
        //    get 
        //    { 
        //        return strUserPwd; 
        //    }
        //}

        public static string ServerName
        {
            set;
            get;
        }

		public static string UserName
		{
            set;
            get;
        }

		public static string UserID
		{
            set;
            get;
        }

        public static string ProgramVersion
        {
            set;
            get;
        }

		public static string Title
		{
            set;
            get;
        }

		public static int CurrentMonth
		{
            set;
            get;
        }

		public static int CurrentYear
		{
            set;
            get;
        }

		public static System.Globalization.CultureInfo Culture
		{
            set;
            get;
        }

		public static System.Globalization.DateTimeFormatInfo DTFormatInfo
		{
            set;
            get;
        }

		public static System.Globalization.NumberFormatInfo NFormatInfo
		{
            set;
            get;
        }
		#endregion

		#region Abstract Class
		public abstract class Languages
		{
			public const string INDONESIA = "id-ID";
			public const string INGGRIS = "en-US";
		};

		public abstract class DecimalDigits
		{
			public const int NONE = 0;
			public const int ONE = 1;
			public const int TWO = 2;
			public const int THREE = 3;
			public const int FOUR = 4;
			public const int FIVE = 5;
		};
		#endregion
	}
}
