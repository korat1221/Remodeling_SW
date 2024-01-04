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
using Microsoft.Win32;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;

namespace main.contents
{
    public partial class Model : Form
    {


        bool scriptable = false;
        static Excel.Application excelApp = null;
        static Excel.Workbook workBook = null;
        static Excel.Worksheet workSheet_Zone = null;
        static Excel.Worksheet workSheet_Envelope = null;
        static Excel.Worksheet workSheet_TB = null;
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
        private void Import_3DInfo()
        {
            string file = "";
            DataRow row;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx";
            openFileDialog.InitialDirectory = System.IO.Directory.GetCurrentDirectory();
            Program.DB.deleteTable(DB.type.CalcDB, "ZoneGeneral_3D");
            Program.DB.deleteTable(DB.type.CalcDB, "ZoneEnvelope_3D");
            Program.DB.deleteTable(DB.type.CalcDB, "ThermalBridge_3D");

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                file = openFileDialog.FileName;
                try
                {
                    excelApp = new Excel.Application();
                    workBook = excelApp.Workbooks.Open(file);

                    //존정보 불러오기 
                    workSheet_Zone = workBook.Sheets[1];
                    Excel.Range excelRange_Zone = workSheet_Zone.UsedRange;
                    string[] Value_Zone = new string[excelRange_Zone.Columns.Count];
                    for (int i = 2; i <= excelRange_Zone.Rows.Count; i++)
                    {
                        for (int j = 1; j <= excelRange_Zone.Columns.Count; j++)
                        {
                            if (excelRange_Zone.Cells[i, j] != null && excelRange_Zone.Cells[i, j].Value2 != null)
                            { Value_Zone[j - 1] = Convert.ToString((excelRange_Zone.Cells[i, j].Value2)); }
                            else { Value_Zone[j - 1] = ""; }

                        }
                        Program.DB.setValue(DB.type.ProjDB, "ZoneGeneral_3D", "존번호,층,바닥면적",
                          "'" + Value_Zone[0] + "','" + Value_Zone[1] + "','" +  Value_Zone[2] + "'", "존번호");
                    }

                    //외피정보 불러오기 
                    workSheet_Envelope = workBook.Sheets[2];
                    Excel.Range excelRange_Envelope = workSheet_Envelope.UsedRange;
                    string[] Value_Envelope = new string[excelRange_Envelope.Columns.Count];
                    for (int i = 2; i <= excelRange_Envelope.Rows.Count; i++)
                    {
                        for (int j = 1; j <= excelRange_Envelope.Columns.Count; j++)
                        {
                            if (excelRange_Envelope.Cells[i, j] != null && excelRange_Envelope.Cells[i, j].Value2 != null)
                            { Value_Envelope[j - 1] = Convert.ToString((excelRange_Envelope.Cells[i, j].Value2)); }
                            else { Value_Envelope[j - 1] = ""; }

                        }
                        Program.DB.setValue(DB.type.ProjDB, "ZoneEnvelope_3D", "번호,층,존,외피유형,커튼월부위,면적,인접존,방위,기울기,우측면돌출길이,좌측면돌출길이,상부돌출길이,주변요소음영길이,벽체길이,창호너비,창호높이",
                          "'" + Value_Envelope[0] + "','" + Value_Envelope[1] + "','" + Value_Envelope[2] + "','" + Value_Envelope[3] + "','"
                          + Value_Envelope[4] + "','" + Value_Envelope[5] + "','" + Value_Envelope[6] + "','" + Value_Envelope[7] + "','" + Value_Envelope[8] + "','"
                          + Value_Envelope[9] + "','" + Value_Envelope[10] + "','" + Value_Envelope[11] + "','" + Value_Envelope[12] + "','" + Value_Envelope[13] + "','"
                          + Value_Envelope[14] + "','" + Value_Envelope[15] + "'", "번호");
                    }

                    //열교정보 불러오기 
                    workSheet_TB = workBook.Sheets[3];
                    Excel.Range excelRange_TB = workSheet_TB.UsedRange;
                    string[] Value_TB = new string[excelRange_TB.Columns.Count];
                    for (int i = 2; i <= excelRange_TB.Rows.Count; i++)
                    {
                        for (int j = 1; j <= excelRange_TB.Columns.Count; j++)
                        {
                            if (excelRange_TB.Cells[i, j] != null && excelRange_TB.Cells[i, j].Value2 != null)
                            { Value_TB[j - 1] = Convert.ToString((excelRange_TB.Cells[i, j].Value2)); }
                            else { Value_TB[j - 1] = ""; }

                        }
                        Program.DB.setValue(DB.type.ProjDB, "ThermalBridge_3D", "번호,열교항목,열교길이",
                          "'" + Value_TB[0] + "','" + Value_TB[1] + "','" + Value_TB[2] + "'", "번호");
                    }


                    MessageBox.Show("3D 정보 엑셀을 Import 하였습니다.");
                    workBook.Close(true);
                    excelApp.Quit();

                }
                finally
                {
                    ReleaseObject(workSheet_Zone); //객체 해제 메소드
                    ReleaseObject(workSheet_Envelope); //객체 해제 메소드
                    ReleaseObject(workSheet_TB); //객체 해제 메소드
                    ReleaseObject(workBook); //객체 해제 메소드
                    ReleaseObject(excelApp); //객체 해제 메소드}
                }
            }

        }
        static void ReleaseObject(object obj)// 액셀 객체 해제 메소드 
        {
            try
            {
                if (obj != null)
                {
                    Marshal.ReleaseComObject(obj); // 액셀 객체 해제
                    obj = null;
                }
            }
            catch (Exception ex)
            {
                obj = null;
                throw ex;
            }
            finally
            {
                GC.Collect(); // 가비지 수집
            }
        }
    }
}
