
namespace ZFame
{
	namespace Security
	{
		public class Encryption
		{
			#region Constructor
			public Encryption()
			{
			}
			#endregion

			#region Methods
            public bool DecryptRegisterKeys(string ValidationKey, out System.DateTime regDate)
            {
                regDate = System.DateTime.Now;
                System.Text.StringBuilder strDecrypt = new System.Text.StringBuilder();
                string strMyKeys;
                char myCodeDecrypt;
                int[] SecretCode = new int[] { 7, 10, 5, 14, 20, 3, 11, 9, 25 };
                System.IO.FileStream file;
                string STR_CNN_FILE = "ZFKey.key";

                try
                {
                    file = new System.IO.FileStream(System.Windows.Forms.Application.StartupPath + @"\" + STR_CNN_FILE, System.IO.FileMode.Open);
                }
                catch
                {
                    return false;
                }

                System.IO.StreamReader reader = new System.IO.StreamReader(file);
                strMyKeys = reader.ReadLine();

                string strTgl = "00000000", strDecryptedValidationKey = "";

                for (int i = 0, idxPjg = strTgl.Length, idxCode = 0, pjgIdxCode = 9; i < idxPjg; i++)
                {
                    myCodeDecrypt = (char)((int)((char)strMyKeys[i]) - SecretCode[idxCode++]);
                    if (idxCode >= pjgIdxCode)
                        idxCode = 0;
                    strDecrypt.Append(myCodeDecrypt);
                }
                strTgl = strDecrypt.ToString();

                try
                {
                    int iYear, iMonth, iDate;

                    iYear = int.Parse(strTgl.Substring(0, 4));
                    iMonth = int.Parse(strTgl.Substring(4, 2));
                    iDate = int.Parse(strTgl.Substring(6, 2));

                    regDate = new System.DateTime(iYear, iMonth, iDate);
                }
                catch
                {
                    regDate = System.DateTime.Now;
                }

                strDecrypt = new System.Text.StringBuilder();
                for (int i = strTgl.Length, idxPjg = strMyKeys.Length, idxCode = 0, pjgIdxCode = 9; i < idxPjg; i++)
                {
                    myCodeDecrypt = (char)((int)((char)strMyKeys[i]) - SecretCode[idxCode++]);
                    if (idxCode >= pjgIdxCode)
                        idxCode = 0;
                    strDecrypt.Append(myCodeDecrypt);
                }
                strDecryptedValidationKey = strDecrypt.ToString();
                
                strDecrypt = null;
                SecretCode = null;
                return (strDecryptedValidationKey == ValidationKey);
            }

            
            public bool DecryptConnString(out string Title, out string ConnectionString, out string KdCabang, out string warehousePenjualan, out string PrinterName, out string sqlServiceName)
            {
                ConnectionString = Title = KdCabang = warehousePenjualan = PrinterName = sqlServiceName = "";
                System.Text.StringBuilder strDecrypt = new System.Text.StringBuilder();
                string strMyConnString;
                char myCodeDecrypt;
                int[] SecretCode = new int[] { 20, 8, 18, 6, 16, 4 };
                System.IO.FileStream file;
                string STR_CNN_FILE = "CnnString.cnn";

                try
                {
                    file = new System.IO.FileStream(System.Windows.Forms.Application.StartupPath + @"\" + STR_CNN_FILE, System.IO.FileMode.Open);
                }
                catch
                {
                    return false;
                }

                System.IO.StreamReader reader = new System.IO.StreamReader(file);
                strMyConnString = reader.ReadLine();

                for (int i = 0, pjgConnString = strMyConnString.Length, idxCode = 0, pjgIdxCode = 6; i < pjgConnString; i++)
                {
                    myCodeDecrypt = (char)((int)((char)strMyConnString[i]) - SecretCode[idxCode++]);
                    if (idxCode >= pjgIdxCode)
                        idxCode = 0;
                    strDecrypt.Append(myCodeDecrypt);
                }
                Title = strDecrypt.ToString();

                #region Connection String
                strDecrypt = new System.Text.StringBuilder();
                strMyConnString = reader.ReadLine();

                for (int i = 0, pjgConnString = strMyConnString.Length, idxCode = 0, pjgIdxCode = 6; i < pjgConnString; i++)
                {
                    myCodeDecrypt = (char)((int)((char)strMyConnString[i]) - SecretCode[idxCode++]);
                    if (idxCode >= pjgIdxCode)
                        idxCode = 0;
                    strDecrypt.Append(myCodeDecrypt);
                }
                ConnectionString = strDecrypt.ToString();
                #endregion

                #region Kode Cabang
                strDecrypt = new System.Text.StringBuilder();
                strMyConnString = reader.ReadLine();

                for (int i = 0, pjgConnString = strMyConnString.Length, idxCode = 0, pjgIdxCode = 6; i < pjgConnString; i++)
                {
                    myCodeDecrypt = (char)((int)((char)strMyConnString[i]) - SecretCode[idxCode++]);
                    if (idxCode >= pjgIdxCode)
                        idxCode = 0;
                    strDecrypt.Append(myCodeDecrypt);
                }
                KdCabang = strDecrypt.ToString();
                #endregion

                #region Warehouse Penjualan Default
                strDecrypt = new System.Text.StringBuilder();
                strMyConnString = reader.ReadLine();

                for (int i = 0, pjgConnString = strMyConnString.Length, idxCode = 0, pjgIdxCode = 6; i < pjgConnString; i++)
                {
                    myCodeDecrypt = (char)((int)((char)strMyConnString[i]) - SecretCode[idxCode++]);
                    if (idxCode >= pjgIdxCode)
                        idxCode = 0;
                    strDecrypt.Append(myCodeDecrypt);
                }
                warehousePenjualan = strDecrypt.ToString();
                #endregion

                #region Printer Name
                strDecrypt = new System.Text.StringBuilder();
                strMyConnString = reader.ReadLine();

                for (int i = 0, pjgConnString = strMyConnString.Length, idxCode = 0, pjgIdxCode = 6; i < pjgConnString; i++)
                {
                    myCodeDecrypt = (char)((int)((char)strMyConnString[i]) - SecretCode[idxCode++]);
                    if (idxCode >= pjgIdxCode)
                        idxCode = 0;
                    strDecrypt.Append(myCodeDecrypt);
                }
                PrinterName = strDecrypt.ToString();
                #endregion

                #region SQL Server Service Name
                strDecrypt = new System.Text.StringBuilder();
                strMyConnString = reader.ReadLine();
                reader.Close();
                reader.Dispose();
                reader = null;
                file.Close();
                file.Dispose();
                file = null;

                for (int i = 0, pjgConnString = strMyConnString.Length, idxCode = 0, pjgIdxCode = 6; i < pjgConnString; i++)
                {
                    myCodeDecrypt = (char)((int)((char)strMyConnString[i]) - SecretCode[idxCode++]);
                    if (idxCode >= pjgIdxCode)
                        idxCode = 0;
                    strDecrypt.Append(myCodeDecrypt);
                }
                sqlServiceName = strDecrypt.ToString();
                #endregion

                strDecrypt = null;
                SecretCode = null;
                return true;
            }

			public virtual string Encrypt(string userID, string password)
			{
				if(userID.Trim().Length == 0 || password.Trim().Length == 0)
					throw new System.ArgumentNullException("paramNULL", "Invalid User ID or password!");
				int encryptor = userID.Length + password.Length;
				string passwordResult = string.Empty;
				System.Text.StringBuilder passwordBuilder = new System.Text.StringBuilder();
				byte tempBit = 0;
				System.Text.UTF8Encoding encode = new System.Text.UTF8Encoding();
			
				byte[] byteEncode = encode.GetBytes(password);
			
				for(int bitEncode=byteEncode.Length-1; bitEncode>0; bitEncode--)
				{
					tempBit = (byte)(byteEncode[bitEncode] - encryptor - bitEncode);
					passwordBuilder.AppendFormat(tempBit.ToString());
					passwordBuilder.AppendFormat(" ");
				}
				tempBit = (byte)(byteEncode[0] - encryptor);
				passwordBuilder.AppendFormat(tempBit.ToString());

				passwordResult = passwordBuilder.ToString();

                passwordBuilder = null;
                encode = null;
                byteEncode = null;

                return passwordResult;
			}

			public virtual string Decrypt(string userID, string password)
			{
				if(userID.Trim().Length == 0 || password.Trim().Length == 0)
					throw new System.ArgumentNullException("paramNULL", "Invalid User ID or password!");
				int decryptor = userID.Length + password.Length;
				System.Collections.ArrayList arrList = new System.Collections.ArrayList();
				string passwordResult = string.Empty;
				System.Text.StringBuilder passwordBuilder = new System.Text.StringBuilder();
				byte tempBit = 0, counter=0;
				System.Text.UTF8Encoding encode = new System.Text.UTF8Encoding();
			
				string[] temppassword = password.Split(' ');
			
				for(int str=temppassword.Length-1; str>=0; str--)
				{
					tempBit = (byte)(byte.Parse( temppassword[str] ) + decryptor + counter++);
					arrList.Add(tempBit);
				}

				byte[] byteDecode = (byte[]) arrList.ToArray(typeof(byte));
				passwordResult = encode.GetString(byteDecode);

                arrList = null;
                passwordBuilder = null;
                encode = null;
                temppassword = null;
                byteDecode = null;

				return passwordResult;
			}
			#endregion
		}

	}
}
