using Microsoft.Web.WebView2.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace main.contents
{
    public partial class Blind : Form
    {
        bool scriptable = false;

        public Blind()
        {
            InitializeComponent();
            InitializeAsync();

            webView21.Source = new Uri(Program.gPath + "chart_ctrl2.html", true);

        }
        async void InitializeAsync()
        {
            await webView21.EnsureCoreWebView2Async(null);
            webView21.CoreWebView2.NavigationCompleted += OnNaviCompleted;
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

        private void GeneralPanel_Paint(object sender, PaintEventArgs e)
        {
            Panel p = (Panel)sender;
            ControlPaint.DrawBorder(e.Graphics, p.DisplayRectangle, Color.FromArgb(153, 180, 209), ButtonBorderStyle.Solid);
        }

        private void Blind_VisibleChanged(object sender, EventArgs e)
        {
            try
            {
                string s = "", s2 = "";
                string[][] res1 = Program.DB.querySQL(DB.type.ProjDB, "SELECT 차양가동계수 FROM Blind_3D");

                for (int k = 0; k < res1.Length; k++)
                {
                    s += Convert.ToDouble(res1[k][0]) * 100 + ",";
                }

                string[][] Location = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "지역", "");
                string[][] res2 = Program.DB.querySQL(DB.type.BaseDB_HCneed, "SELECT AVG(일사량) FROM 기후데이터_전일사량 WHERE 지역명 = '" + Location[0][0] + "' AND 기간 LIKE '%월' GROUP BY 기간 ORDER BY 기간*1 ASC");

                for (int k = 0; k < res2.Length; k++)
                {
                    s2 += Convert.ToDouble(res2[k][0]) + ",";
                }

                runScript("drawChart3([{type:\"line\",data:[" + s + "],borderColor:\"#91D050\",backgroundColor:\"#91D050\",min:0,max:100},{type:\"bar\",data:[" + s2 + "],borderColor:\"#000\",backgroundColor:\"#F2F2F2\",min:0,max:150}])");
            }
            catch { }

        }
    }
}
