using Microsoft.Web.WebView2.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace main.subcontents.Info
{
    public partial class Info_Roof : Form
    {
        bool scriptable = false;
        int page = 0; 
        public Info_Roof()
        {
            InitializeComponent(); this.Font = new Font(UTIL.Families[0], 9.75F, FontStyle.Regular);
            LoadHtmlFile(page);
        }
        private async void LoadHtmlFile(int page)
        {
            string htmlFilePath = Program.gPath + "/threejs/public/print/Info/zerofix--v3.html"; 
            switch (page)
            {
                case 0:
                    htmlFilePath = Program.gPath + "/threejs/public/print/Info/zerofix--v3.html";

                    break;
                case 1:
                     htmlFilePath = Program.gPath + "/threejs/public/print/Info/001_133144.html";
                    break;
            }
            await webView21.EnsureCoreWebView2Async();
            webView21.Source = new Uri(htmlFilePath);
        }

        private void Previous_button_Click(object sender, EventArgs e)
        {
            page--;
            if(page >=0)
            {
                LoadHtmlFile(page);
            }
            else
            {
                MessageBox.Show("첫 페이지 입니다.");
                page++;
            }
        }

        private void Next_button_Click(object sender, EventArgs e)
        {
            page++;
            if (page <2)
            {
                LoadHtmlFile(page);
            }
            else
            {
                MessageBox.Show("마지막 페이지 입니다.");
                page--;
            }
        }
    }
}
