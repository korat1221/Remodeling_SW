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

namespace main.contents.Building
{
    public partial class EnergyUse : Form
    {
        bool scriptable = false;

        public EnergyUse()
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
            updateGraph();
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
        private void updateGraph()
        {
            string s = "";

            if (checkBox1.Checked)
            {
                string s2 = "[2441,2407,2370,2227,2126,2734,3343,4106,3468,2502,2239,2287]";
                s += "{type:\"line\",data:" + s2 + ",borderColor:\"#5B9BD5\",backgroundColor:\"#5B9BD5\",dash:true},";
            }

            if (checkBox2.Checked)
            {
                string s2 = "[2531,2316,2292,2237,2202,2675,4119,4344,3216,2458,2172,2363]";
                s += "{type:\"line\",data:" + s2 + ",borderColor:\"#70AD47\",backgroundColor:\"#70AD47\",dash:true},";
            }

            if (checkBox5.Checked)
            {
                string s2 = "[2860,2762,2613,2231,2103,2715,3693,3680,2872,3236,4469,3329]";
                s += "{type:\"line\",data:" + s2 + ",borderColor:\"#4472C4\",backgroundColor:\"#4472C4\",dash:true},";
            }

            if (checkBox4.Checked)
            {
                string s2 = "[2610,2495,2425,2232,2144,2708,3718,4043,3185,2480,2205,2325]";
                s += "{type:\"line\",data:" + s2 + ",borderColor:\"#ED7D31\",backgroundColor:\"#ED7D31\",dash:false},";
            }

            runScript("drawChart2([" + s + "])");
        }
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            updateGraph();
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            updateGraph();
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            updateGraph();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            updateGraph();
        }
    }
}
