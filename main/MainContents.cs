using main.contents;
using main.subcontents;
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

namespace main
{
    public partial class MainContents : Form
    {
        public enum FormID
        {
            ConstructionWall = 0,
            ConstructionRoof,
            ZoneGeneral,
            ZoneEnvelope,
            Model,
            ConstructionWindow,
            FormDebug,

        }
        Form[] forms = new Form[] { new ConstructionWall(), new ConstructionRoof(), new ZoneGeneral(), new ZoneEnvelope(), new Model(), new ConstructionWindow(), new FormDebug() };


        public MainContents()
        {
            InitializeComponent();

            webView21.Source = new Uri(Program.gPath + "menu.html");
            webView21.Location.Offset(0, 0);
            webView21.Size = this.ClientSize;

            InitializeAsync();

            foreach (FormMain openForm in Application.OpenForms)
            {
                if (openForm.Name == "FormMain")
                {
                    int i = -1;
                    while (++i < forms.Length)
                    {
                        forms[i].TopLevel = false;
                        openForm.splitContainer1.Panel2.Controls.Add(forms[i]);
                    }

                    return;
                }
            }
        }

        async void InitializeAsync()
        {
            await webView21.EnsureCoreWebView2Async(null);
            webView21.CoreWebView2.WebMessageReceived += OnJSMessage;
        }
        void OnJSMessage(object sender, CoreWebView2WebMessageReceivedEventArgs args)
        {
            int idx = Int32.Parse(args.TryGetWebMessageAsString());

            if (idx >= 0 && idx < 7)
            {
                DoLoadForm(idx);
            }
        }
        void DoLoadForm(int idx)
        {
            int i = -1;
            while (++i < forms.Length)
            {
                forms[i].Hide();
            }
            forms[idx].Show();
        }

        public void DoResizeMain(Size sz)
        {
            int i = -1;
            while (++i < forms.Length)
            {
                forms[i].Size = sz;
            }
        }

        private void OnResize(object sender, EventArgs e)
        {
            webView21.Location.Offset(0, 0);
            webView21.Size = this.ClientSize;
            webView21.Height = webView21.Size.Height - 4;
        }

    }
}
