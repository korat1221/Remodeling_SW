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
//
using System.Runtime.InteropServices;
using static System.Net.WebRequestMethods;
using System.Reflection;
using File = System.IO.File;
using Microsoft.VisualBasic.Logging;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using System.Security.Policy;
using System.Timers;
using System.Drawing.Imaging;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;
using System.Security.Cryptography;
using System.Reflection.Emit;
using main.info;


namespace main.contents
{
    public partial class Model : Form, IConfirmable
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
        string sURLOld = "";

        public Model()
        {
            InitializeComponent(); this.Font = new Font(UTIL.Families[0], 9.75F, FontStyle.Regular);

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

            tmSQLExec.Interval = 200;
            tmSQLExec.Tick += new EventHandler(tmSQLExecTick);
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
        private void tmSQLExecTick(object sender, EventArgs e)
        {
            String path = Program.gPath + "projects\\execute.sql";

            if (File.Exists(path))
            {
                try
                {
                    String sql = File.ReadAllText(Program.gPath + "projects\\execute.sql");

                    File.Delete(path);

                    sql = sql.Replace("__PROJ_TYPE__", ProjectList.ProjectType);
                    Program.DB.executeSQL(DB.type.ProjDB, sql);
                    Program.DB.deleteTable(DB.type.ProjDB, "Shade_3D");


                    resetZoneDraw();
                    Program.DB.saveProject();

                    runScript("location.reload();");

                    Program.UTIL.loadMainMenu(2);
                }
                catch
                {
                }
            }
        }
        public bool ValidateAndSave(bool isManualSave = false)
        {
            //화면 전환 시 저장 > 안 됨 
            try
            {
                foreach (Form form in splitContainer1.Panel2.Controls)
                {
                    if (form.Name == "sub3dZoneInfo")
                    {
                        sub3dZoneInfo f = (sub3dZoneInfo)form;

                        f.Save_Envelope();

                        return true;
                    }
                }
                return false;

            }
            catch (Exception ex)
            {
                // 디버깅 중단점 방지를 위해 예외를 무시하거나 로그만 남김
                System.Diagnostics.Debug.WriteLine($"ValidateAndSave 오류: {ex.Message}");
                return false;
            }
        }
        void OnJSMessage(object sender, CoreWebView2WebMessageReceivedEventArgs args)
        {
            Program.UTIL.selectWall(args.TryGetWebMessageAsString());
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
        private void Save_Image()
        {
            try
            {
                // 캡쳐할 영역의 크기 설정
                int captureWidth = (int)(splitContainer1.Panel1.Width * 0.52);
                int captureHeight = (int)(splitContainer1.Panel1.Height * 0.55);

                // Panel1의 가운데를 기준으로 캡쳐할 영역의 위치 계산
                int centerX = splitContainer1.Panel1.Width / 2;
                int centerY = splitContainer1.Panel1.Height / 2;

                // 캡쳐할 영역의 좌표 설정
                int captureX = centerX - captureWidth / 2;
                int captureY = centerY - captureHeight / 2;

                // 캡쳐할 영역을 Rectangle로 설정
                Rectangle captureRectangle = new Rectangle(captureX, captureY, captureWidth, captureHeight);

                // 비트맵 생성
                Bitmap bmp = new Bitmap(captureRectangle.Width, captureRectangle.Height);

                using (Graphics g = Graphics.FromImage(bmp))
                {
                    // 특정 영역을 캡쳐
                    g.CopyFromScreen(splitContainer1.Panel1.PointToScreen(captureRectangle.Location), Point.Empty, captureRectangle.Size);
                }
                string pid = "0000-00-00";
                string[][] Value = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "프로젝트번호");
                if (Value.Length > 0)
                {
                    pid = Value[0][0];
                }
                Directory.CreateDirectory(Program.gPath + "\\print\\img\\" + pid);
                // 저장할 파일 경로 설정
                string ImageName = "/print/img/" + pid + "/Building.png";
                string imagePath = Program.gPath + ImageName; // 최종 경로

                // 비트맵을 파일로 저장
                bmp.Save(imagePath, System.Drawing.Imaging.ImageFormat.Png);
            }
            catch (Exception ex)
            {
                MessageBox.Show("오류 발생: " + ex.Message);
            }

        }
        private void Save_button_Click(object sender, EventArgs e)
        {
            foreach (Form form in splitContainer1.Panel2.Controls)
            {
                if (form.Name == "sub3dZoneInfo")
                {
                    sub3dZoneInfo f = (sub3dZoneInfo)form;
                    Save_Image();
                    Program.DB.deleteTable(DB.type.ProjDB, "Shade_3D");
                    string[][] Win = Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope_3D", "번호", "외피유형 = '창호' or 외피유형 = '커튼월창'");
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
                    //           redrawList();

                    Program.DB.saveProject();

                    MessageBox.Show("저장되었습니다.");
                    return;
                }
            }
        }



        private void Model_VisibleChanged(object sender, EventArgs e)
        {
            tmSQLExec.Enabled = Visible;

            if (Visible)
            {
                CalculateModel();
            }
        }
        private void WebView21_SizeChanged(object sender, EventArgs e)
        {
            CalculateModel(true);
        }

        public void CalculateModel(bool force = false)
        {
            string url = "http://localhost:3000/3d/editor/?pid=" + ProjectList.CurProjID;

            if (sURLOld != url)
            {
                webView21.Source = new Uri(url, true);
                sURLOld = url;
            }
            else if (force)
            {
                runScript("location.reload();");
            }
        }

        private void info_Click(object sender, EventArgs e)
        {
            string basePath = Program.gPath + "Manual\\1.contents\\10.3D\\1.Main";

            // 경로가 존재하는지 확인
            if (Directory.Exists(basePath))
            {
                SlideViewer slideViewer = new SlideViewer(basePath);
                slideViewer.Show();
            }
            else
            {
                MessageBox.Show("The folder path does not exist.");
            }
        }
    }
}
