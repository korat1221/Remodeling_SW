using Microsoft.Web.WebView2.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace main.contents.Result
{
    public partial class PrintReport : Form
    {
        bool scriptable = false;
        public PrintReport()
        {
            InitializeComponent();

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

        public void LoadData(String ID)            // 리스트에서 항목 더블 클릭시 - 뷰를 ID 의 getValue 값으로 채우기
        {
            string s, s2;
            string[][] ZoneG = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_Form", "존번호,존이름,실제어방식,냉난방유무,환기유무,환기방식,온도교환효율,전열교환효율,용도프로필,천장고,시작시간,종료시간,주이용일,재실자수,기기발열수준,일일급탕요구량,냉난방시간,사용시간,공조시간,연이용일수,재실밀도,재실수준,일일인체발열,면적당인체발열,일일기기발열,면적당기기발열,순체적,환기횟수,이용일환기량,비이용일환기량");
            List<object> items = new List<object>();
            List<object> data = new List<object>();
            List<object> _data = new List<object>();
            List<object> _data2 = new List<object>();
            List<object> _data3 = new List<object>();
            List<object> _data4 = new List<object>();

            int i = -1;

            while (++i < ZoneG.Length)
            {
                items.Add("zprint7.html"); // 예시 코드: 메인 메뉴 동적 할당


                _data.Add(new { idx = i, val = ZoneG[i][8] }); 
                _data2.Add(new { idx = i, val = ZoneG[i][26] });
                _data3.Add(new { idx = i, val = ZoneG[i][0] });
                _data4.Add(new { idx = i, val = ZoneG[i][1] });
            }

            data.Add(new { cname = "cls-profile-name", data = _data }); 
            data.Add(new { cname = "cls-volume", data = _data2 });
            data.Add(new { cname = "cls-sub-name", data = _data3 });
            data.Add(new { cname = "cls-zone-name", data = _data4 });





            s = System.Text.Json.JsonSerializer.Serialize(items.ToArray());
            s2 = System.Text.Json.JsonSerializer.Serialize(data.ToArray());

            runScript("init(" + s + "," + s2 + ")");
        }




        private void button1_Click(object sender, EventArgs e)
        {
            webView21.CoreWebView2.ShowPrintUI();
        }
    }
}
