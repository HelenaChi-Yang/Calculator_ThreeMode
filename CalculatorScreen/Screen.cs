using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Calculator;
using Newtonsoft.Json;

namespace ScreenUI
{
    /// <summary>
    /// UI介面
    /// </summary>
    public partial class Screen : Form
    {
        /// <summary>
        /// 計算機
        /// </summary>
        private TheCalculator TheCalculator;

        /// <summary>
        /// 畫面表格
        /// </summary>
        public Screen()
        {
            InitializeComponent();
            TheCalculator = new TheCalculator();
            textBox1.Text = ButtonSign.ZERO_SIGN;
        }

        /// <summary>
        /// 所有按鈕點擊事件
        /// </summary>
        /// <param name="sender">物件傳遞</param>
        /// <param name="e">事件</param>
        private async void AllButtomClick(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string buttonSign = WebUtility.UrlEncode(btn.Text);
            string id = GetHostIPAddress();

            string requestURL = $"https://localhost:44347/api/calculator/{id}?sign={buttonSign}";           
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(requestURL);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                TwoResultOnScreen result = JsonConvert.DeserializeObject<TwoResultOnScreen>(responseBody);

                resultBox.Text = result.ExpressionResult;
                textBox1.Text = result.OperandResult;
            }
        }

        /// <summary>
        /// 取得內部 IP Address
        /// </summary>
        /// <returns></returns>
        private string GetHostIPAddress()
        {
            List<string> lstIPAddress = new List<string>();
            IPHostEntry IpEntry = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ipa in IpEntry.AddressList)
            {
                if (ipa.AddressFamily == AddressFamily.InterNetwork)
                    lstIPAddress.Add(ipa.ToString());
            }
            string id = CalculateHash(lstIPAddress[0]);
            return id;
        }

        /// <summary>
        /// Knuth hash
        /// </summary>
        /// <param name="read">需要hash的IP</param>
        /// <returns></returns>
        private string CalculateHash(string read)
        {
            UInt64 hashedValue = 3074457345618258791ul;
            for (int i = 0; i < read.Length; i++)
            {
                hashedValue += read[i];
                hashedValue *= 3074457345618258799ul;
            }
            return hashedValue.ToString();
        }
    }
}
