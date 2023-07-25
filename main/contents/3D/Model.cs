using main.contentslist;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using main.subcontents;
using main.subcontents.ConstructionCW;
using System.Net;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using static System.ComponentModel.Design.ObjectSelectorEditor;
using Microsoft.Web.WebView2.Core;
using static main.MainContents;
using System.IO;
using main.contents.Building;
using main.subcontents.ConstructionWindow;
using main.contents;

namespace main.contents
{
    public partial class Model : Form
    {
        bool scriptable = false;

        public enum FormID
        {
            ZoneInfo = 0,
            BridgeInfo,
            SpaceInfo,
            CWInfo,
            WLInfo,
            RFInfo,
            FRInfo,
            WINInfo,
            DRInfo,
            IWInfo,
            SLInfo
        };
        Form[] forms = new Form[] { new sub3dZoneInfo(), new sub3dBridgeInfo(), new sub3dSpaceInfo(), new sub3dCWInfo(), new sub3dWLInfo(), new sub3dRFInfo(), new sub3dFRInfo(), new sub3dWINInfo(), new sub3dDRInfo(), new sub3dIWInfo(), new sub3dSLInfo() };


        public Model()
        {
            InitializeComponent();

            int i = -1;
            while (++i < forms.Length)
            {
                forms[i].TopLevel = false;
                forms[i].ShowInTaskbar = false;
                forms[i].Dock = DockStyle.Fill;
                splitContainer1.Panel2.Controls.Add(forms[i]);
            }

            InitializeAsync();
        }
        async void InitializeAsync()
        {
            await webView21.EnsureCoreWebView2Async(null);
            webView21.CoreWebView2.WebMessageReceived += OnJSMessage;
            webView21.CoreWebView2.NavigationCompleted += OnNaviCompleted;
        }
        public void DoLoadForm(int idx)
        {
            int i = -1;
            while (++i < forms.Length)
            {
                forms[i].Hide();
            }

            forms[idx].Show();
        }

        void OnJSMessage(object sender, CoreWebView2WebMessageReceivedEventArgs args)
        {
            try
            {
                int n;
                String s = args.TryGetWebMessageAsString();

                if ((n = s.IndexOf("@@@")) >= 0)
                {
                    String json = s.Substring(n + 3);
                    Program.UTIL.write3DModel(Program.ProjName + ".json", json);
                    Program.DB.executeSQL(DB.type.ProjDB, s.Substring(0, n));
                    Program.UTIL.reloadWebCtrl();
                }
            }
            catch (Exception ex)
            {

            }
        }
        void OnNaviCompleted(object sender, CoreWebView2NavigationCompletedEventArgs args)
        {
            scriptable = true;

            runScript("load3DModel(" + Program.UTIL.read3DModel(Program.ProjName + ".json") + ")");

        }
        public void runScript(string script)
        {
            if (scriptable)
            {
                webView21.CoreWebView2.ExecuteScriptAsync(script);
            }
        }

        private void Save_button_Click(object sender, EventArgs e)
        {
            foreach (Form form in splitContainer1.Panel2.Controls)
            {
                if (form.Name == "sub3dZoneInfo")
                {
                    sub3dZoneInfo f = (sub3dZoneInfo)form;
                    string s = f.Save();

                    runScript("updateObjInfo(" + s + ")");
                    return;
                }
            }
        }
    }
}
