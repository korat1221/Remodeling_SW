using main.info;
using Microsoft.Web.WebView2.Core;

namespace main.contents
{
    public partial class sub3dCWInfo : Form
    {
        bool scriptable = false;

        public sub3dCWInfo()
        {
            InitializeComponent(); this.Font = new Font(UTIL.Families[0], 9.75F, FontStyle.Regular);
            InitializeAsync();

            webView22.Source = new Uri(Program.gPath + "chart_ctrl2.html", true);
        }
        async void InitializeAsync()
        {
            await webView22.EnsureCoreWebView2Async(null);
            webView22.CoreWebView2.NavigationCompleted += OnNaviCompleted;
        }
        void OnNaviCompleted(object sender, CoreWebView2NavigationCompletedEventArgs args)
        {
            scriptable = true;
        }
        public void runScript(string script)
        {
            if (scriptable)
            {
                webView22.CoreWebView2.ExecuteScriptAsync(script);
            }
        }

        private void onVisibleChanged(object sender, EventArgs e)
        {
            double R1, R2, L1, L2, S1, S2, T1, T2, uw, install;
            string Type, InstallType, FrameMaterial, SingleDoubleType, InstallName, UCWtype;
            double Area;

            //string[][] rec = Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope_3D", "벽체길이,구조체,번호", "아이디 = '" + ID + "'");
            string[][] rec = Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope_3D", "존,우측면돌출각도,좌측면돌출각도,상부돌출각도,주변요소음영각도,우측면돌출길이,좌측면돌출길이,상부돌출길이,주변요소음영길이,번호,방위,기울기,구조체번호,면적,창호너비,창호높이", "아이디 = '" + main.MainContents.selectInfo[2] + "'");

            if (rec.Length > 0)
            {
                tabControl1.Visible = true;
                string _ID = rec[0][9];

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


                //커튼월정보 불러오기  // *************************창호 너비 높이 면적은 존 인벨롭에서 들어오는 값으로 해야함 (임시방편)
                String[][] CWLoad = Program.DB.getValue(DB.type.ProjDB, "ConstructionCW", "번호,명칭,커튼월면적,너비,높이,프레임종류,고정유리종류,개폐유리종류,간봉종류,설치유형,태양열취득률,빛투과율,설치열교가산치,커튼월창유효열관류율,Type,설치종류,Ucw적용방법", "번호 = '" + rec[0][12] + "'");

                if (CWLoad.Length > 0 && CWLoad[0][0] != "")
                {
                    tabControl1.TabPages.Remove(tabPage1);
                    tabControl1.TabPages.Add(tabPage1);
                    // 텍스트정보 
                    Name_textBox.Text = rec[0][9];
                    Name_textBox1.Text = CWLoad[0][1];

                    Area = Program.UTIL.ToDoubleOrZero(rec[0][13]);
                    Area_textBox.Text = String.Format("{0:F2}", Area);

                    Width_textBox.Text = String.Format("{0:F2}", Program.UTIL.ToDoubleOrZero(rec[0][14]));
                    height_textBox.Text = String.Format("{0:F2}", Program.UTIL.ToDoubleOrZero(rec[0][15]));
                    if (CWLoad[0][9] == "내단열" || CWLoad[0][9] == "외단열")
                    {
                        install_textBox.Text = "콘크리트조 " + CWLoad[0][9];
                    }
                    else { install_textBox.Text = CWLoad[0][9]; }

                    glass_textBox.Text = CWLoad[0][6];
                    glass2_textBox.Text = CWLoad[0][7];
                    frame_textBox.Text = CWLoad[0][5];
                    Spacer_textBox.Text = CWLoad[0][8];

                    shgc_textBox.Text = CWLoad[0][10];
                    SHGC_on_textBox.Text = CWLoad[0][10];
                    SHGC_off_textBox.Text = CWLoad[0][10];

                    light_textBox.Text = CWLoad[0][11];
                    Tao_on_textBox.Text = CWLoad[0][11];
                    Tao_off_textBox.Text = CWLoad[0][11];

                    uw = Program.UTIL.ToDoubleOrZero(CWLoad[0][13]);
                    uw_textBox.Text = uw.ToString("0.000") + " W/m" + Program.UTIL.Subscript(2, true) + "K";
                    install = Program.UTIL.ToDoubleOrZero(CWLoad[0][12]);
                    inst_textBox.Text = install.ToString("0.000") + " W/m" + Program.UTIL.Subscript(2, true) + "K";

                    Type = CWLoad[0][14];
                    InstallType = CWLoad[0][9];
                    InstallName = CWLoad[0][15];

                    UCWtype = CWLoad[0][16];

                    //UCWtype에 따른 비활성화/활성화
                    if (UCWtype == "계산")
                    {
                        label3.Visible = true;
                        label6.Visible = true;
                        frame_textBox.Visible = true;
                        Spacer_textBox.Visible = true;
                    }
                    else
                    {
                        label3.Visible = false;
                        label6.Visible = false;
                        frame_textBox.Visible = false;
                        Spacer_textBox.Visible = false;
                    }


                    //그림로드
                    string[][] Image2 = Program.DB.getValue(DB.type.BaseDB_HCneed, "커튼월구조유형이미지", "이미지", "구조유형 = '" + Type + "'");
                    CWType_pictureBox.Visible = true;
                    CWType_pictureBox.Load(Program.gPath + Image2[0][0]);
                    CWType_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;

                    string[][] Image3 = Program.DB.getValue(DB.type.BaseDB_HCneed, "커튼월설치열교이미지", "이미지", "구분1 = '" + InstallType + "' AND 구분3 = '" + InstallName + "'");
                    CWInstall_pictureBox.Visible = true;
                    CWInstall_pictureBox.Load(Program.gPath + Image3[0][0]);
                    CWInstall_pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                }
                else
                {
                    tabControl1.TabPages.Remove(tabPage1);
                }
                string s = "";
                string[][] res = Program.DB.querySQL(DB.type.ProjDB, "SELECT 음영계수 FROM Shade_3D WHERE 유형 = '최종음영' AND 번호 = '" + _ID + "' ORDER BY 월*1 ASC");

                if (res.Length > 0)
                {
                    webView22.Visible = true;
                }
                else
                {
                    webView22.Visible = false;
                }

                if (res.Length > 0)
                {
                    for (int k = 0; k < res.Length; k++)
                    {
                        s += Program.UTIL.ToDoubleOrZero(res[k][0]) * 100 + ",";
                    }

                    runScript("drawChart([{type:\"line\",data:[" + s + "],borderColor:\"#91D050\",backgroundColor:\"#91D050\"}])");
                }

            }
            else
            {
                tabControl1.Visible = false;
            }

            //차양정보 불러오기
            String[][] BlindValue = Program.DB.querySQL(DB.type.ProjDB, "select a.제품명,a.종류,a.설치,a.투과수준,a.색깔,a.외부반사율,a.내부반사율,a.투과율,a.흡수율,a.제어방식1,a.제어방식2,b.방위,b.기울기 FROM ConstructionBlind AS  a INNER JOIN ZoneEnvelope_3D AS b ON a.번호 = b.차양적용 where b.아이디 = '" + rec[0][9] + "'");

            if (BlindValue.Length > 0 && BlindValue[0][0] != "")
            {
                tabControl1.TabPages.Remove(Blind_tabPage);
                tabControl1.TabPages.Add(Blind_tabPage);
                BlindName_textBox.Text = BlindValue[0][0];
                BlindType_textBox.Text = BlindValue[0][1];
                BlindInstall_textBox.Text = BlindValue[0][2];
                BlindTrans_textBox.Text = BlindValue[0][3];
                BlindColor_textBox.Text = BlindValue[0][4];
                BlindControl_textBox.Text = BlindValue[0][9];
                LoadGraph(BlindValue[0][10], BlindValue[0][11], BlindValue[0][12]);

                String[][] Blind = Program.DB.getValue(DB.type.ProjDB, "Blind_3D", "차양포함태양열취득률,차양포함빛투과율", "아이디 = '" + rec[0][9] + "'");
                if (Blind.Length > 0)
                {
                    SHGC_on_textBox.Text = Program.UTIL.ToDoubleOrZero(Blind[0][0]).ToString("0.000");
                    Tao_on_textBox.Text = Program.UTIL.ToDoubleOrZero(Blind[0][1]).ToString("0.000");
                }
            }
            else
            {
                tabControl1.TabPages.Remove(Blind_tabPage);
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
                    if (res1.Length > 0)
                    {
                        webView21.Visible = true;
                        s += Program.UTIL.ToDoubleOrZero(res1[0][0]) * 100 + ",";
                    }
                    else
                    {
                        webView21.Visible = false;
                    }

                }
                res1 = Program.DB.getValue(DB.type.BaseDB_HCneed, "기후데이터_차양가동계수_" + ControlType2, "계수", "지역명= '" + Location[0][0] + "' And 방향 ='" + Direction + "' And 기간 = '" + 12.ToString() + "월'");
                s += Program.UTIL.ToDoubleOrZero(res1[0][0]) * 100;



                int cwDegree = (int)Program.UTIL.ToDoubleOrZero(Slope);
                int cwDirection = CALC.ConvertDirectionWord(Direction);
                CALC.Run_Climate_RESystem(cwDegree, cwDirection);
                double[] cwItot = CALC.Itot_mth.GetValueOrDefault((cwDegree, cwDirection));
                for (int mth = 0; mth < 12; mth++)
                {
                    s2 += (cwItot != null ? cwItot[mth] : 0) + ",";
                }

                runScript("drawChart3([{type:\"line\",data:[" + s + "],borderColor:\"#91D050\",backgroundColor:\"#91D050\",min:0,max:100},{type:\"bar\",data:[" + s2 + "],borderColor:\"#000\",backgroundColor:\"#F2F2F2\",min:0,max:150}])");

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
            string basePath = Program.gPath + "Manual\\1.contents\\11.3D_Construction\\8.CW";

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
            string basePath = Program.gPath + "Manual\\1.contents\\11.3D_Construction\\9.CW_Shade";

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
