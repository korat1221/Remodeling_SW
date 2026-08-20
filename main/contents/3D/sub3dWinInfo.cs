using main.info;
using main.subcontents.ConstructionRoof;

using Microsoft.Web.WebView2.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace main.contents
{
    public partial class sub3dWINInfo : Form
    {
        bool scriptable = false;
        double shgc, light, Ug, Glass_Ex, Glass_In;
        public sub3dWINInfo()
        {
            InitializeComponent(); this.Font = new Font(UTIL.Families[0], 9.75F, FontStyle.Regular);
            InitializeAsync();
            webView21.Source = new Uri(Program.gPath + "threejs\\public\\chart_ctrl2.html", true);
            webView22.Source = new Uri(Program.gPath + "threejs\\public\\chart_ctrl2.html", true);
        }
        async void InitializeAsync()
        {
            await webView21.EnsureCoreWebView2Async(null);
            webView21.CoreWebView2.NavigationCompleted += OnNaviCompleted;
            await webView22.EnsureCoreWebView2Async(null);
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
        private void onVisibleChanged(object sender, EventArgs e)
        {
            double R1, R2, L1, L2, S1, S2, T1, T2, uw, install;
            string Type, InstallType, FrameMaterial, SingleDoubleType, InstallName;
            double Area;

            string[][] ValueA = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "지역", "");   //지역, 프로젝트 조건?

            String[][] RES = Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope_3D", "번호", "아이디 = '" + main.MainContents.selectInfo[2] + "'");
            string 번호 = null;
            if (RES.Length > 0)
            {
                번호 = RES[0][0];
            }


            string[][] rec = Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope_3D", "존,우측면돌출각도,좌측면돌출각도,상부돌출각도,주변요소음영각도,우측면돌출길이,좌측면돌출길이,상부돌출길이,주변요소음영길이,번호,방위,기울기,구조체번호,면적,창호너비,창호높이", "번호 = '" + 번호 + "'");

            if (rec.Length > 0)
            {
                tabControl1.Visible = true;
                //음영정보 이미지로드
                string[][] Image = Program.DB.getValue(DB.type.BaseDB_HCneed, "음영이미지", "이미지", "분류 = '이미지1'");
                pictureBox1.Load(Program.gPath + Image[0][0]);
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;

                string[][] Image1 = Program.DB.getValue(DB.type.BaseDB_HCneed, "음영이미지", "이미지", "분류 = '이미지2'");
                pictureBox2.Load(Program.gPath + Image1[0][0]);
                pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;

                if (rec[0][5] != "")
                {
                    tabControl1.TabPages.Remove(tabPage2);
                    tabControl1.TabPages.Add(tabPage2);
                }
                else
                {
                    tabControl1.TabPages.Remove(tabPage2);
                }
                //음영정보
                R1 = Program.UTIL.ToDoubleOrZero(rec[0][5]);
                R1_textBox.Text = R1.ToString("0.00") + "m";
                R2 = Program.UTIL.ToDoubleOrZero(rec[0][1]);
                R2_textBox.Text = R2.ToString("0.00") + "°";
                L1 = Program.UTIL.ToDoubleOrZero(rec[0][6]);
                L1_textBox.Text = L1.ToString("0.00") + "m";
                L2 = Program.UTIL.ToDoubleOrZero(rec[0][2]);
                L2_textBox.Text = L2.ToString("0.00") + "°";
                S1 = Program.UTIL.ToDoubleOrZero(rec[0][8]);
                S1_textBox.Text = S1.ToString("0.00") + "m";
                S2 = Program.UTIL.ToDoubleOrZero(rec[0][4]);
                S2_textBox.Text = S2.ToString("0.00") + "°";
                T1 = Program.UTIL.ToDoubleOrZero(rec[0][7]);
                T1_textBox.Text = T1.ToString("0.00") + "m";
                T2 = Program.UTIL.ToDoubleOrZero(rec[0][3]);
                T2_textBox.Text = T2.ToString("0.00") + "°";

                //창호정보 불러오기 // *************************창호 너비 높이 면적은 존 인벨롭에서 들어오는 값으로 해야함 (임시방편)
                String[][] SubLoad = Program.DB.getValue(DB.type.ProjDB, "SubWindow", "번호,명칭,상위창호번호,창호면적,창호너비,창호높이,창호유효열관류율,설치열교가산치", "번호 = '" + rec[0][12] + "'");

                //구조체 지정 전까지 면적,너비,높이 빼고 전부 다 안보이게
                if (rec[0][12] == "")
                {
                    label4.Visible = false;
                    install_textBox.Visible = false;
                    label2.Visible = false;
                    glass_textBox.Visible = false;
                    label3.Visible = false;
                    frame_textBox.Visible = false;
                    label6.Visible = false;
                    Spacer_textBox.Visible = false;
                    label7.Visible = false;
                    shgc_textBox.Visible = false;
                    label9.Visible = false;
                    light_textBox.Visible = false;
                    label11.Visible = false;
                    uw_textBox.Visible = false;
                    label13.Visible = false;
                    inst_textBox.Visible = false;
                    WindowType_pictureBox.Visible = false;
                    WindowInstall_pictureBox.Visible = false;
                    label10.Visible = false;
                    label12.Visible = false;
                }
                else
                {
                    label8.Visible = true;
                    height_textBox.Visible = true;
                    label16.Visible = true;
                    label4.Visible = true;
                    install_textBox.Visible = true;
                    label2.Visible = true;
                    glass_textBox.Visible = true;
                    frame_textBox.Visible = true;
                    Spacer_textBox.Visible = true;
                    label7.Visible = true;
                    shgc_textBox.Visible = true;
                    label9.Visible = true;
                    light_textBox.Visible = true;
                    label11.Visible = true;
                    uw_textBox.Visible = true;
                    label13.Visible = true;
                    inst_textBox.Visible = true;
                    WindowType_pictureBox.Visible = true;
                    WindowInstall_pictureBox.Visible = true;
                }

                //정보
                Name_textBox.Text = rec[0][9];
                Area = Program.UTIL.ToDoubleOrZero(rec[0][13]);
                Area_textBox.Text = String.Format("{0:F2}", Area);
                Width_textBox.Text = rec[0][14] == "" ? "0" : String.Format("{0:F2}", Program.UTIL.ToDoubleOrZero(rec[0][14]));
                height_textBox.Text = rec[0][15] == "" ? "0" : String.Format("{0:F2}", Program.UTIL.ToDoubleOrZero(rec[0][15]));

                if (SubLoad.Length > 0 && SubLoad[0][0] != "")
                {
                    tabControl1.TabPages.Remove(tabPage1);
                    tabControl1.TabPages.Add(tabPage1);
                    String[][] MainLoad = Program.DB.getValue(DB.type.ProjDB, "ConstructionWindow", "번호,창호명칭,프레임재료,프레임종류,유리종류,간봉종류,설치유형,설치종류,태양열취득률,빛투과율,Type,설치유형,프레임재료,이중단창,설치종류,유리열관류율", "번호 = '" + SubLoad[0][2] + "'");

                    // 텍스트정보 
                    Name_textBox1.Text = SubLoad[0][1];
                    if (MainLoad[0][6] == "내단열" || MainLoad[0][6] == "외단열")
                    { install_textBox.Text = "콘크리트조 " + MainLoad[0][6]; }
                    else { install_textBox.Text = MainLoad[0][6]; }
                    glass_textBox.Text = MainLoad[0][4];
                    frame_textBox.Text = MainLoad[0][2];
                    Spacer_textBox.Text = MainLoad[0][5];
                    shgc_textBox.Text = MainLoad[0][8];
                    shgc = Program.UTIL.ToDoubleOrZero(MainLoad[0][8]);
                    SHGC_off_textBox.Text = shgc.ToString("0.000");
                    SHGC_on_textBox.Text = shgc.ToString("0.000");

                    light_textBox.Text = MainLoad[0][9];
                    light = Program.UTIL.ToDoubleOrZero(MainLoad[0][9]);
                    Tao_off_textBox.Text = light.ToString("0.000");
                    Tao_on_textBox.Text = light.ToString("0.000");

                    uw = Program.UTIL.ToDoubleOrZero(SubLoad[0][6]);
                    uw_textBox.Text = uw.ToString("0.000");
                    install = Program.UTIL.ToDoubleOrZero(SubLoad[0][7]);
                    inst_textBox.Text = install.ToString("0.000");
                    Type = MainLoad[0][10];
                    InstallType = MainLoad[0][11];
                    FrameMaterial = MainLoad[0][12];
                    SingleDoubleType = MainLoad[0][13];
                    InstallName = MainLoad[0][14];
                    Ug = Program.UTIL.ToDoubleOrZero(MainLoad[0][15]);
                    Ug_off_textBox.Text = Ug.ToString("0.000");
                    Ug_on_textBox.Text = Ug.ToString("0.000");

                    //그림로드
                    string[][] Image2 = Program.DB.getValue(DB.type.BaseDB_HCneed, "창호구조유형이미지", "이미지", "구조유형 = '" + Type + "'");
                    WindowType_pictureBox.Visible = true;
                    WindowType_pictureBox.Load(Program.gPath + Image2[0][0]);
                    WindowType_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;

                    string[][] Image3 = Program.DB.getValue(DB.type.BaseDB_HCneed, "창호설치열교이미지", "이미지열교유형", "구분1 = '" + InstallType + "' AND 구분2 = '" + FrameMaterial + "' AND 구분3 = '" + SingleDoubleType + "' AND 구분4 = '" + InstallName + "'");
                    WindowInstall_pictureBox.Visible = true;
                    WindowInstall_pictureBox.Load(Program.gPath + Image3[0][0]);
                    WindowInstall_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                    LoadGraph2(번호);

                }
                else
                {
                    tabControl1.TabPages.Remove(tabPage1);
                }
                //차양정보 불러오기
                String[][] BlindValue = Program.DB.querySQL(DB.type.ProjDB, "select a.제품명,a.종류,a.설치,a.투과수준,a.색깔,a.외부반사율,a.내부반사율,a.투과율,a.흡수율,a.제어방식1,a.제어방식2,b.방위,b.기울기 FROM ConstructionBlind AS  a INNER JOIN ZoneEnvelope_3D AS b ON a.번호 = b.차양적용 where b.번호 = '" + 번호 + "'");

                if (BlindValue.Length > 0 && BlindValue[0][0] != "")
                {
                    tabControl1.TabPages.Remove(Blind_tabPage);
                    tabControl1.TabPages.Add(Blind_tabPage);

                    label20.Visible = true;
                    label19.Visible = true;
                    label18.Visible = true;
                    label17.Visible = true;
                    label25.Visible = true;

                    BlindName_textBox.Text = BlindValue[0][0];
                    BlindType_textBox.Text = BlindValue[0][1];
                    BlindInstall_textBox.Text = BlindValue[0][2];
                    BlindTrans_textBox.Text = BlindValue[0][3];
                    BlindColor_textBox.Text = BlindValue[0][4];
                    BlindControl_textBox.Text = BlindValue[0][9];

                    LoadGraph(BlindValue[0][10], BlindValue[0][11], BlindValue[0][12]);

                    String[][] Blind = Program.DB.getValue(DB.type.ProjDB, "Blind_3D", "차양포함태양열취득률,차양포함빛투과율", "번호 = '" + 번호 + "'");
                    if (Blind.Length > 0)
                    {
                        SHGC_on_textBox.Text = Program.UTIL.ToDoubleOrZero(Blind[0][0]).ToString("0.000");
                        Tao_on_textBox.Text = Program.UTIL.ToDoubleOrZero(Blind[0][1]).ToString("0.000");
                    }
                }
                else
                {
                    tabControl1.TabPages.Remove(Blind_tabPage);
                    BlindName_textBox.Text = "차양 없음";
                    label20.Visible = false;
                    label19.Visible = false;
                    label18.Visible = false;
                    label17.Visible = false;
                    label25.Visible = false;
                }

            }
            else
            {
                tabControl1.Visible = false;
            }
            tabPageOrder();
        }

        private void LoadGraph(String ControlType2, String Direction, string Slope)
        {
            try
            {
                string s = "", s2 = "";
                string[][] Location = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "지역", "");
                string[][] res1;
                for (int mth = 1; mth < 12; mth++)
                {
                    res1 = Program.DB.getValue(DB.type.BaseDB_HCneed, "기후데이터_차양가동계수_" + ControlType2, "계수", "지역명= '" + Location[0][0] + "' And 방향 ='" + Direction + "' And 기간 = '" + mth.ToString() + "월'");
                    s += Program.UTIL.ToDoubleOrZero(res1[0][0]) * 100 + ",";
                }
                res1 = Program.DB.getValue(DB.type.BaseDB_HCneed, "기후데이터_차양가동계수_" + ControlType2, "계수", "지역명= '" + Location[0][0] + "' And 방향 ='" + Direction + "' And 기간 = '" + 12.ToString() + "월'");
                if (res1.Length > 0)
                {
                    webView21.Visible = true;
                    s += Program.UTIL.ToDoubleOrZero(res1[0][0]) * 100;
                }
                else
                {
                    webView21.Visible = false;
                }

                int winDegree = (int)Program.UTIL.ToDoubleOrZero(Slope);
                int winDirection = CALC.ConvertDirectionWord(Direction);
                CALC.Run_Climate_RESystem(winDegree, winDirection);
                double[] winItot = CALC.Itot_mth.GetValueOrDefault((winDegree, winDirection));
                for (int mth = 0; mth < 11; mth++)
                {
                    s2 += (winItot != null ? winItot[mth] : 0) + ",";
                }
                s2 += winItot != null ? winItot[11] : 0;
                string unit = "kWh/m" + Program.UTIL.Subscript(2, true) + "·mth";
                runScript("drawChart4([{type:\"line\",label:\"차양가동율\",data:[" + s + "],borderColor:\"#91D050\",backgroundColor:\"#91D050\",min:0,max:100,tension: 0.4},{type:\"bar\",label:\"일사량(" + unit + ")\",data:[" + s2 + "],borderColor:\"#000\",backgroundColor:\"#F2F2F2\",min:0,max:300,barPercentage:0.7}])");

            }
            catch { }
        }

        private void LoadGraph2(String Num)
        {
            try
            {
                string s = "", s3 = "";
                string[][] res1;
                for (int mth = 1; mth < 12; mth++)
                {
                    res1 = Program.DB.getValue(DB.type.ProjDB, "Shade_3D", "음영계수", "번호= '" + Num + "' And 월 = '" + mth.ToString() + "월'");
                    s += Program.UTIL.ToDoubleOrZero(res1[0][0]) * 100 + ",";
                }
                res1 = Program.DB.getValue(DB.type.ProjDB, "Shade_3D", "음영계수", "번호= '" + Num + "' And 월 = '" + 12.ToString() + "월'");
                if (res1.Length > 0)
                {
                    webView22.Visible = true;
                    s += Program.UTIL.ToDoubleOrZero(res1[0][0]) * 100;
                }
                else
                {
                    webView22.Visible = false;
                }
                string s2 = "[" + s + "]";
                s3 += "{type:\"line\",label:\"음영계수\",data:" + s2 + ",borderColor:\"#70AD47\",backgroundColor:\"#70AD47\",dash:true,tension: 0.4},";
                webView22.CoreWebView2.ExecuteScriptAsync("drawChart2([" + s3 + "],100, 10, true)");
            }
            catch { }
        }

        private void tabPageOrder()
        {
            if (tabControl1.TabPages.Count == 1)
            {
                tabControl1.TabPages.Clear();
                tabControl1.TabPages.Add(tabPage1);

            }
            else if (tabControl1.TabPages.Count == 2)
            {
                tabControl1.TabPages.Clear();
                tabControl1.TabPages.Add(tabPage1);
                tabControl1.TabPages.Add(tabPage2);

            }
            else if (tabControl1.TabPages.Count == 3)
            {
                tabControl1.TabPages.Clear();
                tabControl1.TabPages.Add(tabPage1);
                tabControl1.TabPages.Add(tabPage2);
                tabControl1.TabPages.Add(Blind_tabPage);

            }
        }

        private void info_Click(object sender, EventArgs e)
        {
            string basePath = Program.gPath + "Manual\\1.contents\\11.3D_Construction\\6.Window";

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

        private void info_shade_Click(object sender, EventArgs e)
        {
            string basePath = Program.gPath + "Manual\\1.contents\\11.3D_Construction\\7.Window_Shade";

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
