using Microsoft.Web.WebView2.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace main.contents.Result
{
    public partial class Algorithm_Cooling : Form
    {
        bool scriptable = false;
        public Algorithm_Cooling()
        {
            InitializeComponent(); this.Font = new Font(UTIL.Families[0], 9.75F, FontStyle.Regular);
            InitializeAsync();
        }
        async void InitializeAsync()
        {
            await webView21.EnsureCoreWebView2Async(null);
            webView21.CoreWebView2.WebMessageReceived += OnJSMessage;
            webView21.CoreWebView2.NavigationCompleted += OnNaviCompleted;
        }
        void OnJSMessage(object sender, CoreWebView2WebMessageReceivedEventArgs args)
        {
            try
            {
                String s = args.TryGetWebMessageAsString();
            }
            catch (Exception ex)
            {

            }
        }
        void OnNaviCompleted(object sender, CoreWebView2NavigationCompletedEventArgs args)
        {
            scriptable = true;
        }
        public void runScript(string script)
        {
            if (scriptable)
            {
                webView21.CoreWebView2.ExecuteScriptAsync(script);
            }
        }

        public void LoadData(string ID)            // 리스트에서 항목 더블 클릭시 - 뷰를 ID 의 getValue 값으로 채우기
        {
            string s, s2;
            string[][] 번호 = Program.DB.getValue(DB.type.ProjDB, "CoolingSystem_Form", "번호", "");
            List<object> items = new List<object>();
            List<object> data = new List<object>();
            List<object>[] ManData = new List<object>[700];
            List<string> chart_data = new List<string>();
            int i = -1, n;

            while (++i < 700)
            {
                ManData[i] = new List<object>();
            }

            i = -1;

            while (++i < 번호.Length)
            {
                items.Add("Algorithm_Cooling.htm"); // 예시 코드: 메인 메뉴 동적 할당
                string[][] Value = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "프로젝트번호");
                if (Value.Length > 0)
                {
                    ManData[0].Add(new { idx = i, val = Value[0][0] }); //프로젝트번호
                    data.Add(new { cname = "projectnum", data = ManData[0] });
                }
                ManData[1].Add(new { idx = i, val = 번호[i][0] }); //그림번호
                data.Add(new { cname = "coolingnum", data = ManData[1] });
                ManData[2].Add(new { idx = i, val = 번호[i][0] }); //번호
                data.Add(new { cname = "coolingnum2", data = ManData[2] });


            }

            s = System.Text.Json.JsonSerializer.Serialize(items.ToArray());
            s2 = System.Text.Json.JsonSerializer.Serialize(data.ToArray());

            string s3 = "";
            Debug.Print("start");
            runScript("init(" + s + "," + s2 + "," + "[" + s3 + "])");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            webView21.CoreWebView2.ShowPrintUI();
        }

    }
}