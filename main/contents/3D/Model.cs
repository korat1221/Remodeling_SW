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
using System.Diagnostics;

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
        System.Windows.Forms.Timer tmBridge = new System.Windows.Forms.Timer();
        string bridgeResult = "";

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

            tmBridge.Interval = 200;
            tmBridge.Tick += new EventHandler(tmBridgeProc);
        }
        void tmBridgeProc(object sender, EventArgs e)
        {
            tmBridge.Stop();
            if (bridgeResult != "")
            {
                String s = bridgeResult;

                bridgeResult = "";

                try
                {
                    int n;

                    if ((n = s.IndexOf("@@@")) >= 0)
                    {
                        String json = s.Substring(n + 3);
                        Program.UTIL.write3DModel(json);
                        Program.DB.executeSQL(DB.type.ProjDB, s.Substring(0, n));
                        //             Program.UTIL.reloadWebCtrl();

                        string[][] Win = Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope_3D", "아이디", "외피유형 = '창호'");
                        if (Win.Length > 0)
                        {
                            for (int k = 0; k < Win.Length; k++)
                            {
                                ZoneShade zoneshade = new ZoneShade(Win[k][0]);
                                //zoneshade.Calc_방위각();
                                //MessageBox.Show(zoneshade.태양우측방위각[1].ToString());
                                zoneshade.Calc_방위각();
                                zoneshade.Calc_지형물음영();

                                zoneshade.Calc_상부음영();
                                zoneshade.Calc_좌측음영();
                                zoneshade.Calc_우측음영();
                                zoneshade.Calc_음영계수();

                                //for (int i = 0; i < 12; i++)
                                //{
                                //    MessageBox.Show(zoneshade.상부돌출음영길이좌[i].ToString());
                                //}

                                //MessageBox.Show(zoneshade.지형물수평음영길이[1].ToString());
                                //zoneshade.Calc_상부음영();
                                //zoneshade.Calc_좌측음영();
                                //zoneshade.Calc_우측음영();
                                //zoneshade.Calc_음영계수();
                                zoneshade.Save();
                            }
                        }
                        resetZoneDraw();
                        Program.UTIL.loadMainMenu(2);
                        Program.DB.saveProject();
                    }
                    else
                    {
                        Program.UTIL.selectWall(s);
                    }
                }
                catch (Exception ex)
                {
                }
            }
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
            tmBridge.Stop();
            bridgeResult = args.TryGetWebMessageAsString();
            tmBridge.Start();
        }
        private void resetZoneDraw()
        {
            foreach (Form form in splitContainer1.Panel2.Controls)
            {
                if (form.Name == "sub3dZoneInfo")
                {
                    sub3dZoneInfo f = (sub3dZoneInfo)form;
                    
                    f.resetSID();
                    return;
                }
            }
        }

        void OnNaviCompleted(object sender, CoreWebView2NavigationCompletedEventArgs args)
        {
            scriptable = true;

            runScript("load3DModel(" + Program.UTIL.read3DModel() + ")");

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
