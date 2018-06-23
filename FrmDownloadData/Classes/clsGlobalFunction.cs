using System.Linq;

namespace HRDProject
{
	/// <summary>
	/// Represent an instance of Function Global.
	/// </summary>
	public class GlobalFunction
	{
		#region Constructor
		/// <summary>
		/// Initialize a new instance of the ZFamily.GlobalFunction Class.
		/// </summary>
		public GlobalFunction()
		{
		}
		#endregion

		#region Methods

        #region Fungsi Translate Terbilang Angka
        public string Translate(int angka)
        {
            string tmp = angka.ToString();

            return Translate(tmp, tmp.Length);
        }

        private string Translate(string tex)
        {
            return Translate(tex, 0);
        }

        private string Translate(string tex, int LengthLeft)
        {
            string currentNum, nextNum, nextTex, tmp = string.Empty;
            int nextTexLength = 0, sisakiri = 0, sisakanan = 0,
                pjgTex = tex.Length;

            nextTexLength = tex.Length;
            if (nextTexLength > 14)
            {
                return "Unknown Value!!!";
            }

            if (nextTexLength <= 3)
            {
                currentNum = tex.Substring(0, 1);
                nextTexLength = nextTexLength - 1;

                if (nextTexLength > 0)
                {
                    nextTex = tex.Substring(pjgTex - nextTexLength, nextTexLength);// Right(tex, nextTexLength)
                    nextNum = nextTex.Substring(0, 1);
                }
                else
                {
                    nextTex = nextNum = "";
                }

                switch (int.Parse(currentNum))
                {
                    case 1:
                        if (nextTexLength > 1)
                            tmp = "SE" + Suffix(nextTexLength);
                        else
                            if (nextTexLength > 0)
                            {
                                tex = nextTex;
                                currentNum = nextTex.Substring(0, 1);
                                nextTexLength = nextTexLength - 1;
                                nextTex = tex.Substring(pjgTex - nextTexLength - 1, nextTexLength);
                                try
                                {
                                    nextNum = nextTex.Substring(0, 1);
                                }
                                catch
                                {
                                }
                                switch (int.Parse(currentNum))
                                {
                                    case 0:
                                        tmp = "SEPULUH ";
                                        break;
                                    case 1:
                                        tmp = "SEBELAS ";
                                        break;
                                    case 2:
                                        tmp = "DUA BELAS ";
                                        break;
                                    case 3:
                                        tmp = "TIGA BELAS ";
                                        break;
                                    case 4:
                                        tmp = "EMPAT BELAS ";
                                        break;
                                    case 5:
                                        tmp = "LIMA BELAS ";
                                        break;
                                    case 6:
                                        tmp = "ENAM BELAS ";
                                        break;
                                    case 7:
                                        tmp = "TUJUH BELAS ";
                                        break;
                                    case 8:
                                        tmp = "DELAPAN BELAS ";
                                        break;
                                    case 9:
                                        tmp = "SEMBILAN BELAS ";
                                        break;
                                }
                            }
                            else
                                tmp = "SATU ";
                        break;
                    case 2:
                        tmp = "DUA " + Suffix(nextTexLength);
                        break;
                    case 3:
                        tmp = "TIGA " + Suffix(nextTexLength);
                        break;
                    case 4:
                        tmp = "EMPAT " + Suffix(nextTexLength);
                        break;
                    case 5:
                        tmp = "LIMA " + Suffix(nextTexLength);
                        break;
                    case 6:
                        tmp = "ENAM " + Suffix(nextTexLength);
                        break;
                    case 7:
                        tmp = "TUJUH " + Suffix(nextTexLength);
                        break;
                    case 8:
                        tmp = "DELAPAN " + Suffix(nextTexLength);
                        break;
                    case 9:
                        tmp = "SEMBILAN " + Suffix(nextTexLength);
                        break;
                }

                if (nextTexLength > 0)
                    tmp = tmp + Translate(nextTex);
            }

            else
            {
                sisakiri = nextTexLength % 3;
                if (sisakiri == 0)
                    sisakiri = 3;

                sisakanan = nextTexLength - sisakiri;
                tmp = Translate(tex.Substring(0, sisakiri), sisakanan);

                if (tmp == "SATU " && sisakanan == 3)
                {
                    tmp = "SE";
                    tmp = tmp + Suffix(sisakanan);
                }
                else
                    if (tmp != "")
                    {
                        tmp = tmp + Suffix(sisakanan);
                    }
                tmp = tmp + Translate(tex.Substring(pjgTex - sisakanan));
            }

            return tmp;
        }

        private string Suffix(int length)
        {
            string tmp = "";

            switch (length)
            {
                case 1:
                    tmp = "PULUH ";
                    break;
                case 2:
                    tmp = "RATUS ";
                    break;
                case 3:
                    tmp = "RIBU ";
                    break;
                case 6:
                    tmp = "JUTA ";
                    break;
                case 9:
                    tmp = "MILYAR ";
                    break;
                case 12:
                    tmp = "TRILIUN ";
                    break;
            }

            return tmp;
        }
        #endregion

        public System.DateTime GetFirstDayOfMonth(System.DateTime dtDate)
        {
            return new System.DateTime(dtDate.Year, dtDate.Month, 1);
            
            //System.DateTime dtFrom = dtDate;

            //dtFrom = dtFrom.AddDays(-(dtFrom.Day - 1));

            //return dtFrom;
        }

        public System.DateTime GetFirstDayOfMonth(int iYear, int iMonth)
        {
            return new System.DateTime(iYear, iMonth, 1);

            //System.DateTime dtFrom = new System.DateTime(System.DateTime.Now.Year, iMonth, 1);

            //dtFrom = dtFrom.AddDays(-(dtFrom.Day - 1));

            //return dtFrom;
        }

        public System.DateTime GetLastDayOfMonth(int iYear, int iMonth)
        {
            return new System.DateTime(iYear, iMonth, System.DateTime.DaysInMonth(iYear, iMonth));

            //System.DateTime dtTo = new System.DateTime(System.DateTime.Now.Year, iMonth, 1);

            //dtTo = dtTo.AddMonths(1);
            //dtTo = dtTo.AddDays(-(dtTo.Day));

            //return dtTo;
        }

        public System.DateTime GetLastDayOfMonth(System.DateTime dtDate)
        {
            return new System.DateTime(dtDate.Year, dtDate.Month, System.DateTime.DaysInMonth(dtDate.Year, dtDate.Month));
            
            //    System.DateTime dtTo = dtDate;

        //    dtTo = dtTo.AddMonths(1);
        //    dtTo = dtTo.AddDays(-(dtTo.Day));

        //    return dtTo;
        }

        public string GetMd5HashFromFile(string fileName)
        {
            System.IO.FileStream file = new System.IO.FileStream(fileName, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] retVal = md5.ComputeHash(file);
            file.Close();

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            for (int i = 0; i < retVal.Length; i++)
            {
                sb.Append(retVal[i].ToString("x2"));
            }
            return sb.ToString();
        }


		/// <summary>
		/// Sets the interactivity of buttons.
		/// </summary>
		/// <param name="enabled">value of True/False.</param>
		/// <param name="buttons">array of button.</param>
		public void SetButtonEnable(bool enabled, params System.Windows.Forms.Button[] buttons)
		{
			foreach(System.Windows.Forms.Button button in buttons)
				button.Enabled = enabled;
		}

		/// <summary>
		/// Sets the interactivity of Date/Time Picker.
		/// </summary>
		/// <param name="enabled">value of True/False.</param>
		/// <param name="dateTimePickers">array of DateTimePicker.</param>
		public void SetDTPickerEnable(bool enabled, params System.Windows.Forms.DateTimePicker[] dateTimePickers)
		{
			foreach(System.Windows.Forms.DateTimePicker dateTimePicker in dateTimePickers)
				dateTimePicker.Enabled = enabled;
		}

		/// <summary>
		/// Sets the interactivity of TextBox.
		/// </summary>
		/// <param name="enabled">value of True/False.</param>
		/// <param name="textBoxes">array of TextBox.</param>
		public void SetTextboxEnable(bool enabled, params System.Windows.Forms.TextBox[] textBoxes)
		{
			foreach(System.Windows.Forms.TextBox textBox in textBoxes)
				textBox.Enabled = enabled;
		}

        /// <summary>
        /// Sets the interactivity of TextBox.
        /// </summary>
        /// <param name="enabled">value of True/False.</param>
        /// <param name="textBoxes">array of TextBox.</param>
        public void SetTextboxDisableWithCopy(params System.Windows.Forms.TextBox[] textBoxes)
        {
            foreach (System.Windows.Forms.TextBox textBox in textBoxes)
            {
                textBox.Enabled = true;
                textBox.ReadOnly = true;
                textBox.BackColor = System.Drawing.Color.LightCyan;
            }
        }

		/// <summary>
		/// Clear the System.Windows.Forms.Control.Text property to Empty String ("").
		/// </summary>
		/// <param name="textBoxes">array of TextBox.</param>
		public void ClearTextbox(params System.Windows.Forms.TextBox[] textBoxes)
		{
			foreach(System.Windows.Forms.TextBox textBox in textBoxes)
				textBox.ResetText();
		}

        /// <summary>
        /// Clear the System.Windows.Forms.Control.Text property to Empty String ("").
        /// </summary>
        /// <param name="textBoxes">array of TextBox.</param>
        public void ClearMaskText(params System.Windows.Forms.MaskedTextBox[] maskTexts)
        {
            foreach (System.Windows.Forms.MaskedTextBox maskText in maskTexts)
                maskText.ResetText();
        }

        public void FillMonths(System.Windows.Forms.ComboBox comboBox)
		{
			comboBox.BeginUpdate();
			comboBox.Items.Clear();
			System.Globalization.DateTimeFormatInfo dFi = InfoApp.DTFormatInfo;
			comboBox.Items.AddRange ( dFi.MonthNames );
			comboBox.Items.Remove("");
			comboBox.SelectedIndex = System.DateTime.Today.Month - 1;
			comboBox.EndUpdate();
		}

		public void SetCheckboxEnable(bool enabled, params System.Windows.Forms.CheckBox[] checkBoxes)
		{
			foreach(System.Windows.Forms.CheckBox checkBox in checkBoxes)
				checkBox.Enabled = enabled;
		}

        public void SetMaskTextEnable(bool enabled, params System.Windows.Forms.MaskedTextBox[] maskTexts)
        {
            foreach (System.Windows.Forms.MaskedTextBox maskText in maskTexts)
                maskText.Enabled = enabled;
        }
        
        public void SetComboboxEnable(bool enabled, params System.Windows.Forms.ComboBox[] comboBoxes)
		{
			foreach(System.Windows.Forms.ComboBox comboBox in comboBoxes)
				comboBox.Enabled = enabled;
		}

        public void SetListViewEnable(bool enabled, params System.Windows.Forms.ListView[] listViews)
        {
            foreach (System.Windows.Forms.ListView listview in listViews)
                listview.Enabled = enabled;
        }

		public void SetRadioButtonEnable(bool enabled, params System.Windows.Forms.RadioButton[] radioButtons)
		{
			foreach(System.Windows.Forms.RadioButton radioButton in radioButtons)
				radioButton.Enabled = enabled;
		}

		public void ClearLabel(params System.Windows.Forms.Label[] labels)
		{
			foreach(System.Windows.Forms.Label label in labels)
				label.ResetText();
		}

		public void AddColumnDetails(System.Windows.Forms.ListView listView, params string[] itemDetails)
		{
			System.Windows.Forms.ListViewItem items = new System.Windows.Forms.ListViewItem(itemDetails);

			listView.Items.Add(items);
		}

		public void SetListViewItemColor(System.Drawing.Color backColor, System.Drawing.Color foreColor, params System.Windows.Forms.ListViewItem[] items)
		{
			foreach(System.Windows.Forms.ListViewItem item in items)
			{
				item.BackColor = backColor;
				item.ForeColor = foreColor;
			}
		}

		public void SetListViewItemColor(System.Drawing.Color backColor, System.Drawing.Color foreColor, System.Windows.Forms.ListView listView, params int[] indexItems)
		{
			foreach(int indexItem in indexItems)
			{
				listView.Items[indexItem].BackColor = backColor;
				listView.Items[indexItem].ForeColor = foreColor;
			}
		}

		public void SetListViewSubItemColor(System.Drawing.Color backColor, System.Drawing.Color foreColor, System.Windows.Forms.ListViewItem item, params System.Windows.Forms.ListViewItem.ListViewSubItem[] subItems)
		{
			foreach(System.Windows.Forms.ListViewItem.ListViewSubItem subItem in subItems)
			{
				subItem.BackColor = backColor;
				subItem.ForeColor = foreColor;
			}
		}

        //private System.Net.IPAddress GetIPAddress(string ComputerName, bool isReturnIPAddress)
        //{
        //    System.Net.IPAddress[] tempIP;
        //    string tmpBuilder = string.Empty;

        //    //tempIP = System.Net.Dns.Resolve( ComputerName ).AddressList;
        //    tempIP = System.Net.Dns.GetHostEntry( ComputerName ).AddressList;
            
        //    System.Text.StringBuilder strBuilder = new System.Text.StringBuilder();
        //    for(int i=0; i<tempIP.Length; i++)
        //    {
        //        strBuilder.Append(tempIP[i]);
        //    }

        //    tmpBuilder = strBuilder.ToString();
        //    tempIP = null;
        //    strBuilder = null;
        //    return System.Net.IPAddress.Parse(tmpBuilder);
        //}


        #region Get IPAddress new version 
        private static System.Net.IPAddress CalculateNetwork(System.Net.NetworkInformation.UnicastIPAddressInformation addr)
        {
            // The mask will be null in some scenarios, like a dhcp address 169.254.x.x
            if (addr.IPv4Mask == null)
                return null;

            var ip = addr.Address.GetAddressBytes();
            var mask = addr.IPv4Mask.GetAddressBytes();
            var result = new System.Byte[4];
            for (int i = 0; i < 4; ++i)
            {
                result[i] = (System.Byte)(ip[i] & mask[i]);
            }

            return new System.Net.IPAddress(result);
        }

        public string GetIPAddress(string ComputerName)
        {
            string tmpIPAddressV4 = string.Empty;

            var nics = System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces();
            foreach (var nic in nics)
            {
                var ipProps = nic.GetIPProperties();

                // We're only interested in IPv4 addresses for this example.
                var ipv4Addrs = ipProps.UnicastAddresses.Where(addr => addr.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork);

                foreach (var addr in ipv4Addrs)
                {
                    var network = CalculateNetwork(addr);
                    if (network != null)
                    {
                        if (addr.IPv4Mask.ToString() == "255.255.255.0")
                        {
                            tmpIPAddressV4 = addr.Address.ToString();
                            return tmpIPAddressV4;
                        }
                    }

                    //Console.WriteLine("Addr: {0}   Mask: {1}  Network: {2}", addr.Address, addr.IPv4Mask, network);
                }
            }

            return tmpIPAddressV4;
        }
        #endregion


        //cara lain untuk mengambil IPADDRESS
        public string GetIPAddress2(string ComputerName)
        {
            string tmpBuilder = string.Empty;
            System.Text.StringBuilder info = new System.Text.StringBuilder();
            //System.Net.IPHostEntry host = System.Net.Dns.GetHostByName(ComputerName);
            System.Net.IPHostEntry host = System.Net.Dns.GetHostEntry(ComputerName);

            for (int index = 0; index < host.AddressList.Length; index++)
            {
                info.Append(host.AddressList[index].ToString());
            }

            tmpBuilder = info.ToString();
            info = null;
            host = null;

            return tmpBuilder;
        }

        //cara lain untuk mengambil IPADDRESS
        public string GetIPAddress3(string ComputerName)
        {
            string tmpBuilder = string.Empty;
            System.Collections.ArrayList arrIP = new System.Collections.ArrayList();

            System.Net.IPHostEntry host = System.Net.Dns.GetHostEntry(ComputerName);


            foreach (System.Net.IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    arrIP.Add(ip.ToString());
                    //info.Append(ip.ToString());
                }
            }

            try
            {
                //apabila konek ke inet, set ke arrIP[1], kalau tidak, set ke arrIP[0]
                tmpBuilder = arrIP[1].ToString();
            }
            catch
            {
                tmpBuilder = "127.0.0.1";
            }
            
            host = null;

            return tmpBuilder;
        }

		public object[] Splitter(string words, string separator)
		{
			System.Collections.ArrayList arrString = new System.Collections.ArrayList();
			int indexElement = -1, lastIndex = 0, pjWords = words.Length, pjSeparator = separator.Length;
			
			indexElement = words.IndexOf(separator, 0);

			while(indexElement != -1)
			{
				indexElement = words.IndexOf(separator, lastIndex);
			
				arrString.Add( words.Substring(lastIndex, (indexElement == -1 ? pjWords - lastIndex : indexElement - lastIndex)).ToString() );
				lastIndex = indexElement + pjSeparator;
			}
			return arrString.ToArray();
		}

		public System.Drawing.Printing.PaperSize Paper(System.Drawing.Printing.PrintDocument document, System.Drawing.Printing.PaperKind paperKind)
		{
			System.Drawing.Printing.PaperSize paper = null;

			foreach(System.Drawing.Printing.PaperSize paperSize in document.PrinterSettings.PaperSizes)
			{
				if(paperSize.Kind.Equals(paperKind))
				{
					paper = paperSize;
					break;
				}
			}
			
			return paper;
		}
		#endregion
	}
}
