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
using static System.Net.WebRequestMethods;
using System.Reflection;
using File = System.IO.File;
using Microsoft.VisualBasic.Logging;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using System.Security.Policy;
using System.Timers;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;

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
        Form[] forms = new Form[] { new sub3dZoneInfo(), new sub3dBridgeInfo(), new sub3dSpaceInfo(), new sub3dCWInfo(), new sub3dWLInfo(), new sub3dRFInfo(), new sub3dFRInfo(), new sub3dWINInfo(), new sub3dDRInfo(), new sub3dIWInfo(), new sub3dSLInfo(), new TB_List(), new TB_property() };
        bool ticked = false;

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
        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            if (!ticked)
            {
                ticked = true;

                MessageBox.Show("치수 정밀도가 훼손된 모델입니다. 치수 정밀도 훼손 원인은 모델 생성 작업시 모델 회전 작업이 포함된 경우입니다.", "인식이 불완전하게 되었습니다.", MessageBoxButtons.OK);
            }
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
                        if (s.IndexOf("perfect:false") >= 0)
                        {
                            ticked = false;
                            timer1.Interval = 200;
                            timer1.Tick += new EventHandler(timer1_Tick);
                            timer1.Enabled = true;
                            s = s.Replace("perfect:false", "");
                        }
                        if (s.IndexOf("perfect:true") >= 0)
                        {
                            s = s.Replace("perfect:true", "");
                        }

                        s = s.Replace("@@@", "");
                        s = s.Replace("__PROJ_TYPE__", ProjectList.ProjectType);
                        Program.DB.executeSQL(DB.type.ProjDB, s);

                        Program.DB.deleteTable(DB.type.ProjDB, "Shade_3D");
                        string[][] Win = Program.DB.querySQL(DB.type.ProjDB, "Select 번호 From ZoneEnvelope_3D Where 외피유형 = '창호' or 외피유형 = '커튼월창' Order by 번호");
                        if (Win.Length > 0)
                        {
                            for (int k = 0; k < Win.Length; k++)
                            {
                                ZoneShade zoneshade = new ZoneShade(Win[k][0]);
                                zoneshade.Calc_방위각();
                                zoneshade.Calc_지형물음영();

                                zoneshade.Calc_상부음영();
                                zoneshade.Calc_좌측음영();
                                zoneshade.Calc_우측음영();
                                zoneshade.Calc_음영계수();
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
        public void Reload()
        {
            if (scriptable)
            {
                webView21.CoreWebView2.Reload();
                resetZoneDraw();
            }
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

        private void Export_button_Click(object sender, EventArgs e)
        {
            Export_3DInfo();

        }
        private void Export_3DInfo()
        {
            SaveFileDialog SaveFileDialog = new SaveFileDialog();
            SaveFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx";
            SaveFileDialog.InitialDirectory = System.IO.Directory.GetCurrentDirectory();

            string filepath = SaveFileDialog.InitialDirectory;
            excelApp = new Excel.Application();                             // 엑셀 어플리케이션 생성
            if (SaveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string FileName = SaveFileDialog.FileName;                // 엑셀 파일 저장 경로
                workBook = excelApp.Workbooks.Open(FileName);
                //Zone
                int index = 1;
                workSheet_Zone = workBook.Sheets[index];
                string[][] Data_Zone = Program.DB.querySQL(DB.type.ProjDB, "Select 존번호,층,존이름 from ZoneGeneral_3D Order by 존번호");

                workSheet_Zone.Cells[1, 1] = "번호";
                workSheet_Zone.Cells[1, 2] = "존번호";
                workSheet_Zone.Cells[1, 3] = "층";
                workSheet_Zone.Cells[1, 4] = "존이름";
                Excel.Range range_Zone = workSheet_Zone.Range["B2:D501"];
                range_Zone.Value2 = ""; //엑셀 포맵하기 

                for (int i = 0; i < Data_Zone.Length; i++)
                {
                    // 셀에 데이터 입력
                    for (int j = 0; j < 3; j++)
                    {
                        if (Data_Zone[i][j] != null)
                        {
                            workSheet_Zone.Cells[2 + i, j + 2] = Data_Zone[i][j];
                        }
                        else { workSheet_Zone.Cells[2 + i, j + 2] = ""; break; }
                    }
                }
                workSheet_Zone.Columns.AutoFit();                                    // 열 너비 자동 맞춤

                //Envelope
                index = index + 1;
                workSheet_Envelope = workBook.Sheets[index];
                string[][] Data_Envelope = Program.DB.querySQL(DB.type.ProjDB, "Select 아이디,번호,층,존,외피유형,커튼월부위,면적,인접존,방위,기울기,우측면돌출각도,좌측면돌출각도,상부돌출각도,주변요소음영각도,우측면돌출길이,좌측면돌출길이,상부돌출길이,주변요소음영길이,벽체길이,창호너비,창호높이 from ZoneEnvelope_3D order by 존");
                workSheet_Envelope.Cells[1, 1] = "번호";
                workSheet_Envelope.Cells[1, 2] = "외피아이디";
                workSheet_Envelope.Cells[1, 3] = "외피번호";
                workSheet_Envelope.Cells[1, 4] = "층";
                workSheet_Envelope.Cells[1, 5] = "존";
                workSheet_Envelope.Cells[1, 6] = "외피유형";
                workSheet_Envelope.Cells[1, 7] = "커튼월부위";
                workSheet_Envelope.Cells[1, 8] = "면적";
                workSheet_Envelope.Cells[1, 9] = "인접존";
                workSheet_Envelope.Cells[1, 10] = "방위";
                workSheet_Envelope.Cells[1, 11] = "기울기";
                workSheet_Envelope.Cells[1, 12] = "우측면돌출각도";
                workSheet_Envelope.Cells[1, 13] = "좌측면돌출각도";
                workSheet_Envelope.Cells[1, 14] = "상부돌출각도";
                workSheet_Envelope.Cells[1, 15] = "주변요소음영각도";
                workSheet_Envelope.Cells[1, 16] = "우측면돌출길이";
                workSheet_Envelope.Cells[1, 17] = "좌측면돌출길이";
                workSheet_Envelope.Cells[1, 18] = "상부돌출길이";
                workSheet_Envelope.Cells[1, 19] = "주변요소음영길이";
                workSheet_Envelope.Cells[1, 20] = "벽체길이";
                workSheet_Envelope.Cells[1, 21] = "창호너비";
                workSheet_Envelope.Cells[1, 22] = "창호높이";
                Excel.Range range_Envelope = workSheet_Envelope.Range["B2:W1001"];
                range_Envelope.Value2 = "";//엑셀 포맵하기 
                for (int i = 0; i < Data_Envelope.Length; i++)
                {
                    // 셀에 데이터 입력
                    for (int j = 0; j < 21; j++)
                    {
                        if (Data_Envelope[i][j] != null)
                        { workSheet_Envelope.Cells[2 + i, j + 2] = Data_Envelope[i][j]; }
                        else { workSheet_Envelope.Cells[2 + i, j + 2] = ""; break; }
                    }
                }
                workSheet_Envelope.Columns.AutoFit();                                    // 열 너비 자동 맞춤

                //TB
                index = index + 1;
                workSheet_TB = workBook.Sheets[index];
                string[][] Data_TB = Program.DB.getValue(DB.type.ProjDB, "ThermalBridge_3D", "열교항목,열교길이");
                workSheet_TB.Cells[1, 1] = "번호";
                workSheet_TB.Cells[1, 2] = "구분1";
                workSheet_TB.Cells[1, 3] = "구분2";
                workSheet_TB.Cells[1, 4] = "열교항목";
                workSheet_TB.Cells[1, 5] = "열교길이";
                Excel.Range range_TB = workSheet_TB.Range["B2:E501"];
                range_TB.Value2 = "";//엑셀 포맵하기 
                for (int i = 0; i < Data_TB.Length; i++)
                {
                    // 셀에 데이터 입력

                    switch (Data_TB[i][0])
                    {
                        case "평지붕+외벽[90]":
                            workSheet_TB.Cells[2 + i, 2] = "지붕";
                            workSheet_TB.Cells[2 + i, 3] = "평지붕";
                            break;
                        case "평지붕+외벽[270]":
                            workSheet_TB.Cells[2 + i, 2] = "지붕";
                            workSheet_TB.Cells[2 + i, 3] = "평지붕";
                            break;
                        case "평지붕+내벽":
                            workSheet_TB.Cells[2 + i, 2] = "지붕";
                            workSheet_TB.Cells[2 + i, 3] = "평지붕";
                            break;
                        case "경사지붕":
                            workSheet_TB.Cells[2 + i, 2] = "지붕";
                            workSheet_TB.Cells[2 + i, 3] = "경사지붕";
                            break;
                        case "경사지붕+외벽[수평]":
                            workSheet_TB.Cells[2 + i, 2] = "지붕";
                            workSheet_TB.Cells[2 + i, 3] = "경사지붕";
                            break;
                        case "경사지붕+외벽[경사]":
                            workSheet_TB.Cells[2 + i, 2] = "지붕";
                            workSheet_TB.Cells[2 + i, 3] = "경사지붕";
                            break;
                        case "층간슬라브+외벽":
                            workSheet_TB.Cells[2 + i, 2] = "외벽";
                            workSheet_TB.Cells[2 + i, 3] = "외벽";
                            break;
                        case "외벽+내벽":
                            workSheet_TB.Cells[2 + i, 2] = "외벽";
                            workSheet_TB.Cells[2 + i, 3] = "외벽";
                            break;
                        case "외벽+외벽[90]":
                            workSheet_TB.Cells[2 + i, 2] = "외벽";
                            workSheet_TB.Cells[2 + i, 3] = "외벽";
                            break;
                        case "외벽+외벽[270]":
                            workSheet_TB.Cells[2 + i, 2] = "외벽";
                            workSheet_TB.Cells[2 + i, 3] = "외벽";
                            break;
                        case "바닥+외벽[90]":
                            workSheet_TB.Cells[2 + i, 2] = "외벽";
                            workSheet_TB.Cells[2 + i, 3] = "바닥";
                            break;
                        case "바닥+외벽[270]":
                            workSheet_TB.Cells[2 + i, 2] = "외벽";
                            workSheet_TB.Cells[2 + i, 3] = "바닥";
                            break;

                    }


                    for (int j = 0; j < 2; j++)
                    {
                        if (Data_TB[i][j] != null)
                        {
                            workSheet_TB.Cells[2 + i, j + 4] = Data_TB[i][j];
                        }
                        else { workSheet_Envelope.Cells[2 + i, j + 4] = ""; break; }
                    }
                }
                workSheet_TB.Columns.AutoFit();                                    // 열 너비 자동 맞춤

                workBook.SaveAs(FileName, Excel.XlFileFormat.xlWorkbookDefault);    // 엑셀 파일 저장
                workBook.Close(true);
                excelApp.Quit();
                MessageBox.Show("3D 정보가 Export 되었습니다.");

            }
        }
        private void Import_button_Click(object sender, EventArgs e)
        {
            Import_3DInfo();
        }
        private void Import_3DInfo()
        {
            Program.DB.deleteValue(DB.type.ProjDB, "ZoneGeneral_3D", "");
            Program.DB.deleteValue(DB.type.ProjDB, "ZoneEnvelope_3D", "");
            Program.DB.deleteValue(DB.type.ProjDB, "Blind_3D", "");
            Program.DB.deleteValue(DB.type.ProjDB, "Shade_3D", "");
            Program.DB.deleteValue(DB.type.ProjDB, "ThermalBridge_3D", "");
            Program.DB.deleteValue(DB.type.ProjDB, "ZoneGeneral_Form", "");
            Program.DB.deleteValue(DB.type.ProjDB, "ZoneLighting_form", "");
            string file = "";
            DataRow row;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx";
            openFileDialog.InitialDirectory = System.IO.Directory.GetCurrentDirectory();

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                file = openFileDialog.FileName;
                try
                {
                    excelApp = new Excel.Application();
                    workBook = excelApp.Workbooks.Open(file);

                    //존정보 불러오기 
                    workSheet_Zone = workBook.Sheets[1];
                    int Row_Zone = 1, Column_Zone = 1;
                    for (int n = 1; n < workSheet_Zone.Rows.Count; n++)
                    {
                        if (workSheet_Zone.Cells[n, 1].Value == null || workSheet_Zone.Cells[n, 1].Value == "")
                        {
                            Row_Zone = n - 1; break;
                        }
                    }
                    for (int n = 1; n < workSheet_Zone.Columns.Count; n++)
                    {
                        if (workSheet_Zone.Cells[1, n].Value == null || workSheet_Zone.Cells[1, n].Value == "")
                        {
                            Column_Zone = n - 1; break;
                        }
                    }
                    string[] Value_Zone = new string[Column_Zone];
                    for (int i = 2; i <= Row_Zone; i++)
                    {
                        for (int j = 1; j <= Column_Zone; j++)
                        {
                            if (workSheet_Zone.Cells[i, j] != null && workSheet_Zone.Cells[i, j].Value2 != null)
                            { Value_Zone[j - 1] = Convert.ToString((workSheet_Zone.Cells[i, j].Value2)); }
                            else { Value_Zone[j - 1] = ""; }

                        }
                        if (Value_Zone[0] != null & Value_Zone[0] != "")
                        {
                            Program.DB.setValue(DB.type.ProjDB, "ZoneGeneral_3D", "존번호,층,존이름",
                          "'" + Value_Zone[0] + "','" + Value_Zone[2] + "','" + Value_Zone[3] + "'", "존번호");

                            Program.DB.setValue(DB.type.ProjDB, "ZoneGeneral_Form", "존번호,존이름",
                         "'" + Value_Zone[0] + "','" + Value_Zone[3] + "'", "존번호");
                        }

                    }

                    //외피정보 불러오기 
                    workSheet_Envelope = workBook.Sheets[2];
                    int Row_Envelope = 1, Column_Envelope = 1;
                    for (int n = 1; n < workSheet_Envelope.Rows.Count; n++)
                    {
                        if (workSheet_Envelope.Cells[n, 1].Value == null || workSheet_Envelope.Cells[n, 1].Value == "")
                        {
                            Row_Envelope = n - 1; break;
                        }
                    }
                    for (int n = 1; n < workSheet_Envelope.Columns.Count; n++)
                    {
                        if (workSheet_Envelope.Cells[1, n].Value == null || workSheet_Envelope.Cells[1, n].Value == "")
                        {
                            Column_Envelope = n - 1; break;
                        }
                    }
                    string[] Value_Envelope = new string[Column_Envelope];
                    for (int i = 2; i <= Row_Envelope; i++)
                    {
                        for (int j = 1; j <= Column_Envelope; j++)
                        {
                            if (workSheet_Envelope.Cells[i, j] != null && workSheet_Envelope.Cells[i, j].Value2 != null)
                            { Value_Envelope[j - 1] = Convert.ToString((workSheet_Envelope.Cells[i, j].Value2)); }
                            else { Value_Envelope[j - 1] = ""; }

                        }
                        if (Value_Envelope[0] != null && Value_Envelope[0] != "")
                        {

                            Program.DB.setValue(DB.type.ProjDB, "ZoneEnvelope_3D", "아이디,번호,층,존,외피유형,커튼월부위,면적,인접존,방위,기울기,우측면돌출각도,좌측면돌출각도,상부돌출각도,주변요소음영각도,우측면돌출길이,좌측면돌출길이,상부돌출길이,주변요소음영길이,벽체길이,창호너비,창호높이",
                          "'" + Value_Envelope[0] + "','" + Value_Envelope[2] + "','" + Value_Envelope[3] + "','"
                          + Value_Envelope[4] + "','" + Value_Envelope[5] + "','" + Value_Envelope[6] + "','" + Value_Envelope[7] + "','" + Value_Envelope[8] + "','"
                          + Value_Envelope[9] + "','" + Value_Envelope[10] + "','" + Value_Envelope[11] + "','" + Value_Envelope[12] + "','" + Value_Envelope[13] + "','"
                          + Value_Envelope[14] + "','" + Value_Envelope[15] + "','" + Value_Envelope[16] + "','" + Value_Envelope[17] + "','"
                          + Value_Envelope[18] + "','" + Value_Envelope[19] + "','" + Value_Envelope[20] + "','" + Value_Envelope[21] + "'", "번호");
                        }
                    }

                    //열교정보 불러오기 
                    workSheet_TB = workBook.Sheets[3];
                    int Row_TB = 1, Column_TB = 1;
                    for (int n = 1; n < workSheet_TB.Rows.Count; n++)
                    {
                        if (workSheet_TB.Cells[n, 4].Value == null || workSheet_TB.Cells[n, 4].Value == "")
                        {
                            Row_TB = n - 1; break;
                        }
                    }
                    for (int n = 1; n < workSheet_TB.Columns.Count; n++)
                    {
                        if (workSheet_TB.Cells[1, n].Value == null || workSheet_TB.Cells[1, n].Value == "")
                        {
                            Column_TB = n - 1; break;
                        }
                    }
                    string[] Value_TB = new string[Column_TB];
                    for (int i = 2; i <= Row_TB; i++)
                    {
                        for (int j = 1; j <= Column_TB; j++)
                        {
                            if (workSheet_TB.Cells[i, j] != null && workSheet_TB.Cells[i, j].Value2 != null)
                            { Value_TB[j - 1] = Convert.ToString((workSheet_TB.Cells[i, j].Value2)); }
                            else { Value_TB[j - 1] = ""; }

                        }
                        if (Value_TB[0] != null && Value_TB[0] != "")
                        {
                            Program.DB.setValue(DB.type.ProjDB, "ThermalBridge_3D", "번호,열교항목,열교길이",
                         "'" + Value_TB[0] + "','" + Value_TB[3] + "','" + Value_TB[4] + "'", "번호");
                        }

                    }
                    Program.DB.saveProject();
                    workBook.Close(true);
                    excelApp.Quit();

                    runScript("regenTree(" + System.Text.Json.JsonSerializer.Serialize(Program.DB.querySQL(DB.type.ProjDB, "Select 존번호 from ZoneGeneral_3D Ordery by 존번호")) + "," + System.Text.Json.JsonSerializer.Serialize(Program.DB.querySQL(DB.type.ProjDB, "Select 아이디,번호,존,외피유형,커튼월부위 From ZoneEnvelope_3D Order By 존")) + ")");

                    MessageBox.Show("3D 정보 엑셀을 Import 하였습니다.");

                    var timer = new System.Windows.Forms.Timer();
                    timer.Interval = 1500;
                    timer.Tick += (o, a) =>
                    {
                        timer.Stop();
                        MainContents.selID_old = "";
                        Program.UTIL.loadMainMenu(2);
                    };
                    timer.Start();

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
