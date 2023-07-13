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
            string[][] ZoneG = Program.DB.getValue(DB.type.ProjDB, "Zonegeneral", "zoneName,zoneUsage,zoneHC,θi_h_set,θi_c_set,Δθi_NA,Fx,Fx_fl,Fx_wl,θs_c,θi_h_min,θe_min,θSUP_Wi,Mode_night,Mode_we,twd_d,th_op_d_we,th_op_d,dwd_a,ZoneArea,zoneHeight,qI_p,qI_fac,Cwirk_A,VA_we,VA_wd,n50,e,f,Vmech_SUP_we,Vmech_SUP_wd,Vmech_ETA_we,Vmech_ETA_wd,ηV_mech,ηχV_mech,χi_c_set,χi_h_set,Vmech_SUP_z,Vmech_ETA_z,ρacp_a");
            List<object> items = new List<object>();
            List<object> data = new List<object>();
            List<object> _data = new List<object>();
            int i = -1;

            while (++i < ZoneG.Length)
            {
                items.Add("zpage.html"); // 예시 코드: 메인 메뉴 동적 할당

                s2 = ZoneG[i][1];
                s = s2.Substring(0, 2);
                string[][] rec = Program.DB.getValue(DB.type.BaseDB_HCneed, " 용도프로필", "용도명","항목='" + s + "'");

                _data.Add(new { idx = i, val = rec[0][0]}); // 예시 코드: 메인 메뉴 동적 할당
            }

            data.Add(new { cname = "cls-profile-name", data = _data }); // 예시 코드: 메인 메뉴 동적 할당

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
