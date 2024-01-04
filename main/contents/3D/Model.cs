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
            string bridgeResult = args.TryGetWebMessageAsString();

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

                        if (json.IndexOf("perfect:false") >= 0)
                        {
                            MessageBox.Show("치수 정밀도가 훼손된 모델입니다. 치수 정밀도 훼손 원인은 모델 생성 작업시 모델 회전 작업이 포함된 경우입니다.", "인식이 불완전하게 되었습니다.", MessageBoxButtons.OK);
                        }

                        //                        Program.UTIL.write3DModel(json);
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
                        Program.DB.saveProject();

                        runScript("location.reload();");

                        Program.UTIL.loadMainMenu(2);
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

            runScript("load3DModel('" + ProjectList.CurProjID + "')");

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

        private void Import_button_Click(object sender, EventArgs e)
        {
            Import_3DInfo();
        }
        void Import_3DInfo()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = ".csv files (*.csv)|*.csv";
            openFileDialog.InitialDirectory = System.IO.Directory.GetCurrentDirectory();

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                Program.DB.deleteTable(DB.type.CalcDB, "ZoneEnvelope_3D");

                try
                {
                    using (StreamReader sr = new StreamReader(openFileDialog.FileName))
                    {
                        int n = 0;
                        while (!sr.EndOfStream)
                        {
                            string[] token = sr.ReadLine().Split(',');
                            if (n == 0)
                            {
                            }
                            else
                            {
                                Program.DB.setValue(DB.type.CalcDB, "ZoneEnvelope_3D", "번호,층,존,외피유형,커튼월부위,면적,인접존,방위,기울기,우측면돌출길이,좌측면돌출길이,상부돌출길이,주변요소음영길이,벽체길이,창호너비,창호높이",
                                "'" + token[0] + "','" + token[1] + "','" + token[2] + "','" + token[3] + "','"
                                + token[4] + "','" + token[5] + "','" + token[6] + "','" + token[7] + "','" + token[8] + "','"
                                + token[9] + "','" + token[10] + "','" + token[11] + "','" + token[12] + "','" + token[13] + "','"
                                + token[14] + "','" + token[15] + "'", "번호");
                            }
                            n++;
                        }
                    }
                }
                catch
                {
                    MessageBox.Show("파일의 형식이 올바르지않습니다. 데이터를 확인해주세요.");
                }
            }
        }
    }
}
