using main.contentslist;
using main.info;
using main.subcontents.ZoneLighting;
using System;
using System.Collections.Generic;
using System.Security.Policy;
using System.Windows.Forms;

namespace main.contents
{
    public partial class ZoneLighting : Form, IConfirmable
    {
        //변수
        String ZoneNum, ZoneName;
        //존 정보(가져오는 값)
        double Em, KA, FA;
        double Wr, Lr, A, hR, hm, hLi, hTa, K, admax, ad, bdsimple, bd, AD, unAD, LightInstallHeight; //hm: 조명과 작업면사이의 거리, hR: 천장과 작업면사이의 거리
        string Facade_MainDi, Main_WinCW = null, facade_shade, facade_dimming, Usage;
        double Facade_Aca, Zone_f_a, Zone_f_b, Zone_f_AD, Facade_τD65_SNA, Facade_GlassRatio, K2, K3, γSh_lsh, γSh_hA, γSh_vA, f_τD65_SA;// AVG_Height, AVG_Width;
        String Main_glass, MainType_ID;
        public string Roof_MainDi, roof_glass, roof_shade, roof_dimming;
        public string None_MainDi;
        public double Roof_Aca, r_aD, r_bD, r_AD, γF, γW, As, Bs, hs, hw, hg, Da, Roof_τD65_SNA, Roof_τD65_SA, Roof_GlassRatio;
        //테이블에서 가져오는 값
        double UFF; //LightMethod에 따라 매칭값
        string dbform;
        double Foc; //ControlType에 따라 매칭값
        double Pci, Pj_lx; //매칭값
        double dayofuse;
        static String Layer;
        //ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ
        //선택해야하는 정보(save 되어야 함)

        //RenewDB저장값
        public string RenewNum, RenewType, RenewName, RenewArt, RenewEff, RenewL1, RenewL2, RenewA, RenewDirec, RenewSlope;
        bool RenewBool; //적용 여부
        //LightDB저장값
        string LightNumber, LightType, LightType2, LightConverter, LightFi, LightW;
        double LightFL, lm_W;
        string D_LightFi, D_LightPi;
        double U_LightFi, U_LightPi;
        //선택값
        string Method, control, dimming;
        //계산값
        double Pj, Pn;
        double Fo, Fo1, Fo2, Fo3, Fc;
        double N = 1; //조명 설치 개수
        //차양선택값
        string ShadeType;
        //자연채광 선택값
        string NaturalType;
        //별도창
        string facade, doubleskinglass, atriumglass;
        double facadeW, facadeL, facadeH, zoneGlassLight;
        string roof;
        double zoneRoofAngle1, zoneRoofAngle2, zoneRoofLenght1, zoneRoofLenght2, zoneRoofLenght3;

        //ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ

        public ZoneLighting()
        {

            // 화면 뜨자마자 있었으면 하는거 전부 콤보박스로 몰아 넣기 
            InitializeComponent(); this.Font = new Font(UTIL.Families[0], 9.75F, FontStyle.Regular);

            //조명 이미지 로드 
            string[][] Image = Program.DB.getValue(DB.type.BaseDB_HCneed, "메뉴아이콘", "하위메뉴아이콘", "하위메뉴명 = '존 조명정보'");
            if (Image.Length > 0)
            {
                Icon_pictureBox.Load(Program.gPath + Image[0][0]);
                Icon_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            }

            //  Load_OtherFormData();

            //켜자마자 자동으로 층 및 명칭 불러오기 
            //https://agape93.tistory.com/6

            //조명방식 콤보박스
            Program.UTIL.FillComboBox(DB.type.BaseDB_Lighting, LightMethod_comboBox, "조명", "조명방식", "1");

            //제어방식 콤보박스
            Program.UTIL.FillComboBox(DB.type.BaseDB_Lighting, ControlType_comboBox, "조명", "제어종류", "1");

            //디밍유형 콤보박스
            Program.UTIL.FillComboBox(DB.type.BaseDB_Lighting, DimmingType_comboBox, "조명", "주광제어종류", "1");

            //집광채광 향 콤보박스
            Program.UTIL.FillComboBox(DB.type.BaseDB_Lighting, RenewDi_comboBox, "조명", "방위", "1");

            //집광채광 기울기 콤보박스
            Program.UTIL.FillComboBox(DB.type.BaseDB_Lighting, Slope_comboBox, "조명", "창기울기", "1");

            //자연채광 대분류 콤보박스
            //Program.UTIL.FillComboBox(DB.type.BaseDB_Lighting, NaturalType_comboBox, "조명", "자연채광 유형1", "1");

            //면적기호작성
            label18.Text = "m\u00B2";
            label19.Text = "m\u00B2";
            label20.Text = "m\u00B2";
            lightload_unit.Text = "W/m\u00B2";
            areaunit1.Text = "m\u00B2";
            areaunit2.Text = "m\u00B2";

            Main_pictureBox2.Parent = Main_pictureBox;
            Main_pictureBox3.Parent = Main_pictureBox2;
            Ltype_pictureBox.Parent = Main_pictureBox3;
        }

        private void GeneralPanel_Paint(object sender, PaintEventArgs e)
        {
            Panel p = (Panel)sender;
            ControlPaint.DrawBorder(e.Graphics, p.DisplayRectangle, System.Drawing.SystemColors.Control, ButtonBorderStyle.Solid);
        }
        private void panel3_Paint(object sender, PaintEventArgs e)
        {
            Panel p = (Panel)sender;
            ControlPaint.DrawBorder(e.Graphics, p.DisplayRectangle, System.Drawing.SystemColors.Control, ButtonBorderStyle.Solid);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Panel p = (Panel)sender;
            ControlPaint.DrawBorder(e.Graphics, p.DisplayRectangle, System.Drawing.SystemColors.Control, ButtonBorderStyle.Solid);
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            Panel p = (Panel)sender;
            ControlPaint.DrawBorder(e.Graphics, p.DisplayRectangle, System.Drawing.SystemColors.Control, ButtonBorderStyle.Solid);
        }
        private void panel4_Paint(object sender, PaintEventArgs e)
        {
            Panel p = (Panel)sender;
            ControlPaint.DrawBorder(e.Graphics, p.DisplayRectangle, System.Drawing.SystemColors.Control, ButtonBorderStyle.Solid);
        }
        private void panel6_Paint(object sender, PaintEventArgs e)
        {
            Panel p = (Panel)sender;
            ControlPaint.DrawBorder(e.Graphics, p.DisplayRectangle, System.Drawing.SystemColors.Control, ButtonBorderStyle.Solid);
        }
        private void LightDB_button_Click(object sender, EventArgs e)
        {
            LightType = null;
            Pj_textbox.Text = "0.00";
            LightingDB lightingdb_form = new LightingDB();
            DialogResult result = lightingdb_form.ShowDialog();
            if (result == DialogResult.OK)
            {
                LightNumber = lightingdb_form.Select_Light[0];
                dbform = lightingdb_form.Select_Light[1];
                LightType = lightingdb_form.Select_Light[2];
                LightType2 = lightingdb_form.Select_Light[3];
                LightConverter = lightingdb_form.Select_Light[5];
                LightFi = lightingdb_form.Select_Light[6];
                LightW = lightingdb_form.Select_Light[7];
                lm_W = Program.UTIL.ToDoubleOrZero(lightingdb_form.Select_Light[8]);  //광효율
                LightFL = Program.UTIL.ToDoubleOrZero(lightingdb_form.Select_Light[9]);
                //Default의 경우 VAR이지만 사용자 DB의 경우 숫자
                //사용자 이름 나중에 LP 포함해서 하지 말아야겠다

                if (LightNumber.Contains("LP"))
                {
                    D_LightFi = lightingdb_form.Select_Light[6];
                    D_LightPi = lightingdb_form.Select_Light[7];
                }

                else
                {
                    U_LightFi = Program.UTIL.ToDoubleOrZero(lightingdb_form.Select_Light[6]);
                    U_LightPi = Program.UTIL.ToDoubleOrZero(lightingdb_form.Select_Light[7]);
                }
            }
            LightInfo();
            Match_Pjlx();
            Calc_Pj();
            Calc_Fo();
            Calc_Fc();
            Pci_Value();
            Calc_AD();
        }


        private void LightMethod_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (LightMethod_comboBox.SelectedItem != null)
            {
                Method = LightMethod_comboBox.SelectedItem.ToString();

                string[][] Image = Program.DB.getValue(DB.type.BaseDB_Lighting, "조명_램프분류이미지", "이미지", "조명분류 = '" + Method + "'");
                if (Image.Length > 0)
                {
                    Ltype_pictureBox.Size = new System.Drawing.Size(60, 120);
                    Ltype_pictureBox.Location = new Point(250, 13);
                    Ltype_pictureBox.Load(Program.gPath + Image[0][0]);
                    Ltype_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                    Ltype_pictureBox.BackColor = Color.Transparent;
                }

                string[][] val = Program.DB.getValue_SameCheck(DB.type.BaseDB_Lighting, "조명_럭스당조명밀도", "UFF", "조명방식='" + Method + "'");
                UFF = Program.UTIL.ToDoubleOrZero(val[0][0]);

                if (LightType_textBox.Text != null && LightType_textBox.Text != "")
                {
                    string[][] check = Program.DB.getValue(DB.type.BaseDB_Lighting, "조명_DB", "DB유형", "등기구명칭 = '" + LightType_textBox.Text + "'");
                    dbform = check[0][0];
                }
                else
                {
                    return;
                }

                if (lightHeight_textBox.Text != null && lightHeight_textBox.Text != "")
                {
                    Match_Pjlx();
                    Calc_Pj();
                    Calc_Fo();
                    Calc_Fc();
                }
            }
        }
        private void lightHeight_textBox_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(lightHeight_textBox.Text) == false)
            {
                LightInstallHeight = Program.UTIL.ToDoubleOrZero(lightHeight_textBox.Text);
                LightMethod_comboBox_SelectedIndexChanged(sender, e);
                Calc_AD();
            }
        }
      

        private void ControlType_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ControlType_comboBox.SelectedItem != null)
            {
                control = ControlType_comboBox.SelectedItem.ToString();


                if (control == "스마트제어")
                {
                    label4.Visible = true;
                    Pci_textBox.Visible = true;
                }
                else if (control == "일반제어")
                {
                    label4.Visible = false;
                    Pci_textBox.Visible = false;
                }
                Match_Foc();
                Calc_Fo();
                Calc_Fc();
                Pci_Value();
            }
        }
        private void DimmingType_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DimmingType_comboBox.SelectedItem != null)
            {
                dimming = DimmingType_comboBox.SelectedItem.ToString();
            }
        }

        private void Pci_textBox_TextChanged(object sender, EventArgs e)
        {
            Pci_textBox.ForeColor = Color.Gray;
            //Pci_Value();
            Pci = Program.UTIL.textBox_doubleComa(Pci_textBox, false, 1);
        }


        private void NaturalDB_button_Click(object sender, EventArgs e)
        {
            if (NaturalType == "파사드")
            {
                LightingNatural_facade naturallighting_facade = new LightingNatural_facade(NaturalType, ZoneNum);
                DialogResult result = naturallighting_facade.ShowDialog();
                if (result == DialogResult.OK)
                {
                    this.facade = naturallighting_facade.facadetype;
                    Subtype.Text = facade;
                    this.doubleskinglass = naturallighting_facade.doubleskinglasstype;
                    this.atriumglass = naturallighting_facade.atriumglasstype;
                    this.facadeW = naturallighting_facade.W;
                    this.facadeL = naturallighting_facade.L;
                    this.facadeH = naturallighting_facade.H;
                    this.zoneGlassLight = naturallighting_facade.Tao;
                }
            }

            if (NaturalType == "천창")
            {
                LightingNatural_roof naturallighting_roof = new LightingNatural_roof(NaturalType, ZoneNum);
                DialogResult result = naturallighting_roof.ShowDialog();
                if (result == DialogResult.OK)
                {
                    this.roof = naturallighting_roof.rooftype;
                    Subtype.Text = roof;
                    this.zoneRoofAngle1 = naturallighting_roof.roofangle1;
                    this.zoneRoofAngle2 = naturallighting_roof.roofangle2;
                    this.zoneRoofLenght1 = naturallighting_roof.rooflength1;
                    this.zoneRoofLenght2 = naturallighting_roof.rooflength2;
                    this.zoneRoofLenght3 = naturallighting_roof.rooflength3;
                }
            }
            Load_AD2_image();
            Calc_AD();
            Load_NaturalType_image(NaturalType);
        }

        private void RenewDB_button_Click(object sender, EventArgs e)
        {
            //Default의 경우 VAR이지만 사용자 DB의 경우 숫자
            LightingRenewDB renewdb_form = new LightingRenewDB();
            DialogResult result = renewdb_form.ShowDialog();
            if (result == DialogResult.OK)
            {
                RenewNum = renewdb_form.Select_Renew; //번호
                RenewType = renewdb_form.Select_dbType; //사용자, 표준
                RenewBool = true; //
                if (RenewType == "사용자")
                {
                    string[][] check = Program.DB.getValue(DB.type.ProjDB, "User_Renew", "집광채광명칭,집광채광종류,집광채광효율,산광부가로길이,산광부세로길이,산광부면적", "번호 = '" + RenewNum + "'");
                    if (check.Length > 0)
                    {
                        RenewName = check[0][0].ToString();
                        RenewArt = check[0][1].ToString();
                        RenewEff = check[0][2].ToString();
                        RenewL1 = check[0][3].ToString();
                        RenewL2 = check[0][4].ToString();
                        RenewA = check[0][5].ToString();
                    }

                }
                else
                {
                    string[][] check = Program.DB.getValue(DB.type.BaseDB_Lighting, "조명_집광채광DB", "집광채광명칭,집광채광종류,집광채광효율,산광부가로길이,산광부세로길이,산광부면적", "번호 = '" + RenewNum + "'");
                    if (check.Length > 0)
                    {
                        RenewName = check[0][0].ToString();
                        RenewArt = check[0][1].ToString();
                        RenewEff = check[0][2].ToString();
                        RenewL1 = check[0][3].ToString();
                        RenewL2 = check[0][4].ToString();
                        RenewA = check[0][5].ToString();
                    }
                }
                RenewType_textBox.Text = RenewName;
                R1_textBox.Text = RenewArt;
                R2_textBox.Text = RenewA;
                R3_textBox.Text = RenewEff;
            }
        }
        private void Renew_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            RenewCheck();

            RenewDi_comboBox.SelectedItem = RenewDirec;
            Slope_comboBox.SelectedItem = RenewSlope;
        }
        //-----------------------------------------------------------------------------------------------------------------------------------------------------------

        public void Match_Pjlx()
        {
            try
            {
                hm = LightInstallHeight - hTa; //조명과 작업면 사이의 거리
                K = Lr * Wr / (hm * (Lr + Wr));
                double[] data = { 0.6, 0.8, 1, 1.25, 1.5, 2, 2.5, 3, 4, 5 };
                double x0 = 0, x1 = 0, y0 = 0, y1 = 0;
                if (K <= 0.6)
                {
                    x0 = data[0];
                    x1 = data[0];
                }
                else if (0.6 < K && K < 5)
                {
                    for (int i = 0; i < 10; i++)
                    {
                        if (K < data[i])
                        {
                            x0 = data[i - 1];
                            x1 = data[i];
                            break;
                        }
                    }
                }
                else
                {
                    x0 = data[9];
                    x1 = data[9];
                }
                //선형보간법 적용
                String[][] val1 = Program.DB.getValue(DB.type.BaseDB_Lighting, "조명_럭스당조명밀도", "값", "조명방식='" + Method + "' AND K = '" + x0.ToString() + "'");
                String[][] val2 = Program.DB.getValue(DB.type.BaseDB_Lighting, "조명_럭스당조명밀도", "값", "조명방식='" + Method + "' AND K = '" + x1.ToString() + "'");
                if (val1.Length > 0 && val2.Length > 0)
                {
                    y0 = Program.UTIL.ToDoubleOrZero(val1[0][0].ToString());
                    y1 = Program.UTIL.ToDoubleOrZero(val2[0][0].ToString());

                    if (x1 - x0 == 0)
                    {
                        Pj_lx = y0;
                    }
                    else Pj_lx = y0 + (K - x0) * (y1 - y0) / (x1 - x0);
                }
            }
            catch { }
        }

        public void Match_Foc()
        {
            String[][] value = Program.DB.getValue(DB.type.BaseDB_Lighting, "조명_조명제어", "Foc", "제어종류 = '" + control + "'");
            if (value.Length > 0)
            {
                Foc = Program.UTIL.ToDoubleOrZero(value[0][0]);
            }
        }
        public void Calc_Pj()
        {
            if (LightType != null)
            {
                if (dbform == "표준")
                {
                    Pj = Em * KA * LightFL * (0.8 / 0.67) * Pj_lx;
                    Pn = Pj * A;
                    N = 1;
                }
                else
                {
                    //if (LightType.Contains("LED"))
                    //{
                    //    N = (Em * A) / (U_LightFi * UFF * 0.67 * 1.1);
                    //    Pn = U_LightPi * N;
                    //    Pj = Pn / A;
                    //}
                    //else
                    //{
                    //    N = (Em * A) / (U_LightFi * UFF * 0.67);
                    //    Pn = U_LightPi * N;
                    //    Pj = Pn / A;
                    //}                    
                    N = Em / (U_LightFi / A);
                    if (double.IsNaN(N))
                    { N = 1; }
                    Pn = U_LightPi * N;
                    Pj = Pn / A;
                }
                Pj_textbox.Text = Pj.ToString();
                Program.UTIL.textBox_doubleComa(Pj_textbox, true, 2);
            }
            else
            {
                Pj_textbox.Text = "0.00";
            }
        }

        //대기전력 값 
        public void Pci_Value()
        {
            Pci_textBox.Text = Pci.ToString();
            Program.UTIL.textBox_doubleComa(Pj_textbox, true, 2);
            if (control == "일반제어")
            {
                Pci = 0;
            }
            else
            {
                if (control == "스마트제어" && dbform == "표준")
                {
                    Pci = 1.5;
                }
                else if (control == "스마트제어" && dbform != "표준")
                {
                    Pci_textBox.Text = "";
                }
            }

        }

        public void Calc_Fo()
        {
            Fo1 = (1 - ((1 - Foc) * FA) / 0.2);
            Fo2 = Foc + 0.2 - FA;
            Fo3 = ((7 - 10 * Foc) * (FA - 1));

            List<double> list = new List<double>();
            {
                list.Add(Fo1);
                list.Add(Fo2);
                list.Add(Fo3);
            }

            double min = list.Min();

            //double[] Fo_list = { Fo1, Fo2, Fo3 };
            //double max = Fo1;
            //for (int i = 1; i < Fo_list.Length; i++)
            //{
            //    if (max < Fo_list[i])
            //    {
            //        max = Fo_list[i];
            //    }
            //}
            Fo = min;
        }

        public void Calc_Fc()
        {
            if (LightType != null)
            {
                if (control == "스마트제어")
                {
                    Fc = (1 + 0.67) / 2;
                }

                else if (control == "일반제어")
                {
                    Fc = 1;
                }
                fc_textBox.Text = (Fc * Fo).ToString();
                Program.UTIL.textBox_doubleComa(fc_textBox, true, 2);
            }
            else
            {
                fc_textBox.Text = "0.00";
            }
        }


        //조명 정보 텍스트 박스에 넣기 
        private void LightInfo()
        {
            LightType_textBox.Text = LightType;
            FL_textBox.Text = LightFL.ToString();
            Program.UTIL.textBox_doubleComa(FL_textBox, true, 2);
            L1_textBox.Text = LightType;
            L2_textBox.Text = LightType2;
            L4_textBox.Text = LightConverter;

            if (LightNumber != null)
            {
                if (LightNumber.Contains("LP"))
                {
                    L5_textBox.Text = D_LightFi;
                    L6_textBox.Text = D_LightPi;
                }
                else
                {
                    L5_textBox.Text = U_LightFi.ToString();
                    L6_textBox.Text = U_LightPi.ToString();
                }
            }
            L8_textBox.Text = lm_W.ToString();

            label5.ForeColor = Color.Black;
            label6.ForeColor = Color.Black;
        }

        //주창 정보 보이기
        private void WindowInfo()
        {

            if (FacadeButton.Checked == true || RoofButton.Checked == true)
            {
                shade1_label.Visible = true;
                Shade2_label.Visible = true;
                Shade3_label.Visible = true;
                Shade4_label.Visible = true;
                Shade5_label.Visible = true;
                Shade7_label.Visible = true;

                Window1_textBox.Visible = true;
                label7.Visible = true;
                WindowA_textBox.Visible = true;
                Window_glass_textBox.Visible = true;
                Window_Tao_textBox.Visible = true;
                Window_glass_label.Visible = true;
                Window_Tao_label.Visible = true;

                Blind_textBox.Visible = true;
                Blind2_textBox.Visible = true;
                Blind3_textBox.Visible = true;
                label2.Visible = true;
                label3.Visible = true;
            }

            else if (FacadeButton.Checked == false && RoofButton.Checked == false)
            {
                shade1_label.Visible = false;
                Shade2_label.Visible = false;
                Shade3_label.Visible = false;
                Shade4_label.Visible = false;
                Shade5_label.Visible = false;
                Shade7_label.Visible = false;


                Window1_textBox.Visible = false;
                label7.Visible = false;
                WindowA_textBox.Visible = false;
                Window_glass_textBox.Visible = false;
                Window_Tao_textBox.Visible = false;
                Window_glass_label.Visible = false;
                Window_Tao_label.Visible = false;

                Blind_textBox.Visible = false;
                Blind2_textBox.Visible = false;
                Blind3_textBox.Visible = false;
                label2.Visible = false;
                label3.Visible = false;

            }
        }

        public void Calc_AD()
        {
            if (NaturalType == "파사드")
            {
                admax = 2.5 * (hLi - hTa);
                ad = Math.Min(admax, Lr);
                bdsimple = Facade_Aca / (hLi - hTa);
                if (bdsimple > 0.5 * Wr)
                {
                    bd = Wr;
                }
                else
                {
                    bd = bdsimple;
                }
                AD = ad * bd;
                unAD = A - AD;
                areaunit1.Visible = true;
                areaunit2.Visible = true;
            }
            else if (NaturalType == "천창")
            {
                admax = 2.5 * (hLi - hTa);
                ad = Math.Min(admax, Lr);
                bdsimple = Roof_Aca / (hLi - hTa);
                if (bdsimple > 0.5 * Wr)
                {
                    bd = Wr;
                }
                else
                {
                    bd = bdsimple;
                }
                AD = ad * bd;
                unAD = A - AD;
                areaunit1.Visible = true;
                areaunit2.Visible = true;
            }
            else
            {
                ad = 0;
                bd = 0;
                AD = 0;
                unAD = A;
            }
            AD_textBox.Text = string.Format("{0:N2}", AD);
            aad_textBox.Text = string.Format("{0:N2}", ad);
            bbd_textBox.Text = string.Format("{0:N2}", bd);
            NA_textBox.Text = string.Format("{0:N2}", unAD);
        }

        // 주창 정보 들어오기 
        private void WindowInfo2()
        {
            Calc_AD();
            //차양명칭 및 투과율 가져오기 
            //String[][] Blind = Program.DB.querySQL(DB.type.ProjDB, "select a.차양적용 From ZoneEnvelope_3D AS a INNER JOIN ZoneLighting_form AS b ON a.존 = b.번호 where a.존 = '" + ZoneNum + "' AND a.번호 =  b.주창아이디");
            String[][] Blind = Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope_3D", "차양적용", "번호='" + MainType_ID + "'");
            if (Blind.Length > 0)
            {
                String[][] Blind2 = null;
                if (Blind[0][0] == "")
                {
                    Shade4_label.Visible = false;
                    Blind2_textBox.Visible = false;
                    Blind_textBox.Text = "없음";
                    Blind3_textBox.Text = "차양없음";
                    ShadeType = Blind3_textBox.Text;
                }
                else
                {
                    Blind2 = Program.DB.getValue(DB.type.ProjDB, "ConstructionBlind", "명칭,투과율,제어방식1", "번호='" + Blind[0][0] + "'");
                    if (Blind2.Length > 0)
                    {
                        Shade4_label.Visible = true;
                        Blind2_textBox.Visible = true;
                        Blind_textBox.Text = Blind2[0][0];
                        Blind2_textBox.Text = Blind2[0][1];
                        Blind3_textBox.Text = Blind2[0][2];
                        ShadeType = Blind3_textBox.Text;
                    }
                }
            }

            if (NaturalType == "파사드")
            {
                Window1_textBox.Text = Facade_MainDi;
                WindowA_textBox.Text = Facade_Aca.ToString();
                Program.UTIL.textBox_doubleComa(WindowA_textBox, true, 2);

                Window_glass_textBox.Text = Main_glass;
                Window_Tao_textBox.Text = Facade_τD65_SNA.ToString();
                Program.UTIL.textBox_doubleComa(Window_Tao_textBox, true, 3);

                bbd_textBox.Text = bd.ToString();
                Program.UTIL.textBox_doubleComa(bbd_textBox, true, 2);

                aad_textBox.Text = ad.ToString();
                Program.UTIL.textBox_doubleComa(aad_textBox, true, 2);

                AD_textBox.Text = AD.ToString();
                Program.UTIL.textBox_doubleComa(AD_textBox, true, 2);

                NA_textBox.Text = unAD.ToString();
                Program.UTIL.textBox_doubleComa(NA_textBox, true, 2);
            }
            else if (NaturalType == "천창")
            {
                Window1_textBox.Text = Roof_MainDi;
                WindowA_textBox.Text = Roof_Aca.ToString();
                Program.UTIL.textBox_doubleComa(WindowA_textBox, true, 2);

                Window_glass_textBox.Text = Main_glass;
                Window_Tao_textBox.Text = Roof_τD65_SNA.ToString();
                Program.UTIL.textBox_doubleComa(Window_Tao_textBox, true, 3);

                bbd_textBox.Text = bd.ToString();
                Program.UTIL.textBox_doubleComa(bbd_textBox, true, 2);
                aad_textBox.Text = ad.ToString();
                Program.UTIL.textBox_doubleComa(aad_textBox, true, 2);
                AD_textBox.Text = AD.ToString();
                Program.UTIL.textBox_doubleComa(AD_textBox, true, 2);
                NA_textBox.Text = unAD.ToString();
                Program.UTIL.textBox_doubleComa(NA_textBox, true, 2);
            }
            else;

        }

        private void Load_NaturalType_image(String Type)
        {
            string[][] image = null;
            if (FacadeButton.Checked == false && RoofButton.Checked == false)
            {
                Subtype.Text = "해당없음";
                image = Program.DB.getValue(DB.type.BaseDB_Lighting, "조명_자연채광대분류이미지", "이미지", "자연채광대분류 = '" + "해당없음" + "'");
            }
            else if (Type == "파사드" && (facade == null || facade == ""))
            {
                image = Program.DB.getValue(DB.type.BaseDB_Lighting, "조명_자연채광대분류이미지", "이미지", "자연채광대분류 = '" + Type + "'");
            }
            else if (Type == "파사드" && facade != null && facade != "")
            {
                if (facade == "일반 파사드")
                {
                    image = Program.DB.getValue(DB.type.BaseDB_Lighting, "조명_자연채광대분류이미지", "이미지", "자연채광대분류 = '" + Type + "'");
                }
                else
                {
                    image = Program.DB.getValue(DB.type.BaseDB_Lighting, "조명_자연채광대분류이미지", "이미지", "자연채광대분류 = '" + facade + "'");
                }
            }
            else if (Type == "천창")
            {
                image = Program.DB.getValue(DB.type.BaseDB_Lighting, "조명_자연채광대분류이미지", "이미지", "자연채광대분류 = '" + Type + "'");
            }

            if (image.Length > 0)
            {
                Main_pictureBox.Location = new Point(0, 0);
                Main_pictureBox.Load(Program.gPath + image[0][0]);
                Main_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            }
        }


        //자연채광 체크박스 활성화 비활성화
        public void NaturalCheck()
        {
            if (FacadeButton.Checked == true || RoofButton.Checked == true)
            {
                //NaturalType_comboBox.Visible = true;
                NaturalDB_button.Visible = true;
                Direction_label.Visible = true;
                Aca_label.Visible = true;
                direction_textBox.Visible = true;
                Aca_textBox.Visible = true;
                Acam2_label.Visible = true;
                type_pictureBox.Visible = true;
            }
        }

        //집광채광 체크박스 활성화 비활성화
        public void RenewCheck()
        {
            if (Renew_checkBox.Checked)
            {
                RenewDB_button.Visible = true;
                RenewDi_comboBox.Visible = true;
                Slope_comboBox.Visible = true;
                RenewDi_label.Visible = true;
                Slope_label.Visible = true;
                RenewType_textBox.Visible = true;
                R1_label.Visible = true;
                R2_label.Visible = true;
                R3_label.Visible = true;
                label8.Visible = true;
                label9.Visible = true;
                R1_textBox.Visible = true;
                R2_textBox.Visible = true;
                R3_textBox.Visible = true;
                Load_RenewType_image(true);
                RenewBool = true;
            }

            else
            {
                RenewDB_button.Visible = false;
                RenewDi_comboBox.Visible = false;
                Slope_comboBox.Visible = false;
                RenewDi_label.Visible = false;
                Slope_label.Visible = false;
                RenewType_textBox.Visible = false;
                R1_label.Visible = false;
                R2_label.Visible = false;
                R3_label.Visible = false;
                label8.Visible = false;
                label9.Visible = false;
                R1_textBox.Visible = false;
                R2_textBox.Visible = false;
                R3_textBox.Visible = false;
                Load_RenewType_image(false);

                RenewNum = null;
                RenewType = null;
                RenewName = null;
                RenewArt = null;
                RenewEff = null;
                RenewL1 = null;
                RenewL2 = null;
                RenewA = null;
                RenewDirec = null;
                RenewSlope = null;
                R1_textBox.Text = null;
                R2_textBox.Text = null;
                R3_textBox.Text = null;
                RenewBool = false;
                RenewType_textBox.Text = null;
                RenewDi_comboBox.Text = null;
                Slope_comboBox.Text = null;
            }

        }

        private void Load_RenewType_image(Boolean check)
        {
            //집광 채광 종류가 null이 아닐 경우 그림에 들어가도록
            string[][] Image;
            if (check == true)
            {
                Image = Program.DB.getValue(DB.type.BaseDB_Lighting, "조명_집광채광이미지", "이미지", "집광채광 ='광덕트'");
            }
            else
            {
                Image = Program.DB.getValue(DB.type.BaseDB_Lighting, "조명_집광채광이미지", "이미지", "집광채광 ='없음'");
            }
            if (Image.Length > 0)
            {
                Main_pictureBox3.Visible = true;
                Main_pictureBox3.Load(Program.gPath + Image[0][0]);
                Main_pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
                Main_pictureBox3.Location = new Point(14, 20);
            }
        }

        public void NaturalType_case1()
        {
            // 자연채광 유형에 따른 해당 값 
            if (NaturalType == "파사드")
            {
                direction_textBox.Text = Facade_MainDi;
                Aca_textBox.Text = Facade_Aca.ToString();
                Program.UTIL.textBox_doubleComa(Aca_textBox, true, 2);

            }
            else if (NaturalType == "천창")
            {
                direction_textBox.Text = Roof_MainDi;
                Aca_textBox.Text = Roof_Aca.ToString();
                Program.UTIL.textBox_doubleComa(Aca_textBox, true, 2);
            }
            else
            {
                direction_textBox.Text = "";
                Aca_textBox.Text = "";
            }
        }

        //차양 유무에 따른 그림 로드 
        private void Load_Shade_image(String Type)
        {
            string[][] Image = null;
            if (FacadeButton.Checked == true && NaturalType == "파사드" && ShadeType != "차양없음")
            {
                Image = Program.DB.getValue(DB.type.BaseDB_Lighting, "조명_차양이미지", "이미지", "차양 = '" + "파사드차양" + "'");
            }
            else if (FacadeButton.Checked == true && NaturalType == "파사드" && ShadeType == "차양없음")
            {
                Image = Program.DB.getValue(DB.type.BaseDB_Lighting, "조명_차양이미지", "이미지", "차양 = '" + "파사드" + "'");
            }
            else if (RoofButton.Checked == true && NaturalType == "천창" && ShadeType != "차양없음")
            {
                Image = Program.DB.getValue(DB.type.BaseDB_Lighting, "조명_차양이미지", "이미지", "차양 = '" + "천창차양" + "'");
            }
            else if (RoofButton.Checked == true && NaturalType == "천창" && ShadeType == "차양없음")
            {
                Image = Program.DB.getValue(DB.type.BaseDB_Lighting, "조명_차양이미지", "이미지", "차양 = '" + "천창" + "'");
            }
            else
            {
                Image = Program.DB.getValue(DB.type.BaseDB_Lighting, "조명_차양이미지", "이미지", "차양 = '" + "없음" + "'");
            }

            if (Image.Length > 0)
            {
                Main_pictureBox2.Load(Program.gPath + Image[0][0]);
                Main_pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
                Main_pictureBox2.Location = new Point(0, 0);
            }

        }

        //상세 선택에 따른 변화 (체크박스에 걸기)
        private void Load_AD2_image() //주광면적 이미지 작성
        {
            if (facade != "" && facade != null)
            {
                string[][] Image = Program.DB.getValue(DB.type.BaseDB_Lighting, "조명_주광면적이미지", "이미지", "주광면적 = '" + facade + "'");
                type_pictureBox.Load(Program.gPath + Image[0][0]);
                type_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            }
            else
            {
                string[][] Image = Program.DB.getValue(DB.type.BaseDB_Lighting, "조명_주광면적이미지", "이미지", "주광면적 = '일반 파사드'");
                type_pictureBox.Load(Program.gPath + Image[0][0]);
                type_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            }
        }

        private void side_active()
        {
            A_label.Visible = true;
            A_textBox.Visible = true;
            AD_label.Visible = true;
            AD_textBox.Visible = true;
            aad_label.Visible = true;
            aad_textBox.Visible = true;
            bbd_label.Visible = true;
            bbd_textBox.Visible = true;
            NA_label.Visible = true;
            NA_textBox.Visible = true;
            label10.Visible = true;
            label11.Visible = true;
            label12.Visible = true;
            label13.Visible = true;
            label14.Visible = true;
        }

        public static bool OnLoadListProc(Form form)
        {
            List_Zone f = (List_Zone)form;
            f.load_List(Layer);
            return true;
        }

        public bool ValidateAndSave(bool isManualSave = false)
        {
            try
            {
                if (LightType == null)
                {
                    DialogResult res = MessageBox.Show("저장하시겠습니까?", "저장", MessageBoxButtons.YesNo);
                    if (res == DialogResult.Yes)
                    {
                        MessageBox.Show("조명 종류를 선택하세요.");
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else if (lightHeight_textBox.Text == "" || lightHeight_textBox.Text == null)
                {
                    DialogResult res = MessageBox.Show("저장하시겠습니까?", "저장", MessageBoxButtons.YesNo);
                    if (res == DialogResult.Yes)
                    {
                        MessageBox.Show("조명 설치 높이를 입력하세요.");
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else if (Renew_checkBox.Checked == true && RenewNum == null)
                {
                    DialogResult res = MessageBox.Show("저장하시겠습니까?", "저장", MessageBoxButtons.YesNo);
                    if (res == DialogResult.Yes)
                    {
                        MessageBox.Show("집광채광을 선택하세요.");
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else if ((NaturalType == "천창") && (zoneRoofLenght1 == 0 || zoneRoofLenght2 == 0 || zoneRoofLenght3 == 0))
                {
                    DialogResult res = MessageBox.Show("저장하시겠습니까?", "저장", MessageBoxButtons.YesNo);
                    if (res == DialogResult.Yes)
                    {
                        MessageBox.Show("천창 상세 길이 정보를 입력하세요.");
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }

                else if ((this.facade == "아트리움") && (facadeW == 0 || facadeL == 0 || facadeH == 0))
                {
                    DialogResult res = MessageBox.Show("저장하시겠습니까?", "저장", MessageBoxButtons.YesNo);
                    if (res == DialogResult.Yes)
                    {
                        MessageBox.Show(" 아트리움 상세 길이 정보를 입력하세요.");
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else if ((this.facade == "중정") && (facadeW == 0 || facadeL == 0 || facadeH == 0))
                {
                    DialogResult res = MessageBox.Show("저장하시겠습니까?", "저장", MessageBoxButtons.YesNo);
                    if (res == DialogResult.Yes)
                    {
                        MessageBox.Show(" 중정 상세 길이 정보를 입력하세요.");
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else if (LightType_textBox.Text == "" || LightType_textBox.Text == null)
                {
                    DialogResult res = MessageBox.Show("저장하시겠습니까?", "저장", MessageBoxButtons.YesNo);
                    if (res == DialogResult.Yes)
                    {
                        MessageBox.Show(" 조명 종류를 선택해 주세요.");
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    Save_Image();
                    Save(isManualSave);
                    return true;
                }
            }
            catch (Exception ex)
            {
                // 디버깅 중단점 방지를 위해 예외를 무시하거나 로그만 남김
                System.Diagnostics.Debug.WriteLine($"ValidateAndSave 오류: {ex.Message}");
                return false;
            }
        }


        private void Save(bool isManualSave = false) //공간계수 대신 조명설치 높이값을 저장함
        {
            Program.DB.setValue(DB.type.ProjDB, "ZoneLighting_form", "번호,너비,길이,순바닥면적,상인방높이,작업면높이,조명설치높이,기준조도," +
                "조명방식,제어방식,디밍유형,조명밀도,조명예상전력," +
                "대기전력,재실계수,조도제어계수," +
                "조명번호, 등기구명칭, 램프유형, 컨버터_안정기, 광효율, 조명계수,조명개수," +
                "집광채광체크,주광길이,주광깊이,주광면적,비주광면적",
                "'" + Num_textBox.Text + "','" + Wr + "','" + Lr + "','" + A + "','" + hLi + "','" + hTa + "','" + LightInstallHeight + "','" + Em + "','" +
                Method + "','" + control + "','" + dimming + "','" + Pj.ToString() + "','" + Pn.ToString() + "','" +
                Pci.ToString() + "','" + Fo.ToString() + "','" + Fc.ToString() + "','" +
                LightNumber + "','" + LightType + "','" + LightType2 + "','" + LightConverter + "','" + lm_W + "','" + LightFL.ToString() + "','" + N.ToString() + "','" +
                Renew_checkBox.Checked.ToString() + "','" +
                bd + "','" + ad + "','" + AD + "','" + unAD + "'", "번호");

            if (LightNumber.Contains("LP"))
            {
                Program.DB.setValue(DB.type.ProjDB, "ZoneLighting_form", "번호,표준광속, 표준소비전력",
                "'" + Num_textBox.Text + "','" +
                D_LightFi + "','" + D_LightPi
                 + "'", "번호");
            }
            else
            {
                Program.DB.setValue(DB.type.ProjDB, "ZoneLighting_form", "번호,사용자광속, 사용자소비전력",
                "'" + Num_textBox.Text + "','" +
                U_LightFi.ToString() + "','" + U_LightPi.ToString()
              + "'", "번호");
            }

            if (FacadeButton.Checked == true || RoofButton.Checked == true)
            {
                Program.DB.setValue(DB.type.ProjDB, "ZoneLighting_form", "번호,자연채광유형,주향,주창면적합,주창유리종류,주창아이디,차양",
                    "'" + Num_textBox.Text + "','" +
                   NaturalType + "','" + direction_textBox.Text + "','" + Program.UTIL.ToDoubleOrZero(Aca_textBox.Text.ToString()) + "','" + Main_glass + "','" + MainType_ID + "','" + Blind3_textBox.Text + "'", "번호");


                if (FacadeButton.Checked == true)
                {
                    Program.DB.setValue(DB.type.ProjDB, "ZoneLighting_form", "번호,서브유형,주창유리빛투과율,주창유리면적비,이중외피유리,아트리움유리,파사드유리빛투과율,파사드너비,파사드길이,파사드높이",
                    "'" + Num_textBox.Text + "','" +
                   facade + "','" + Facade_τD65_SNA + "','" + Facade_GlassRatio + "','" + doubleskinglass + "','" + atriumglass + "','" + zoneGlassLight + "','" + facadeW + "','" + facadeL + "','" + facadeH
                    + "'", "번호");
                }
                else if (RoofButton.Checked == true)
                {
                    Program.DB.setValue(DB.type.ProjDB, "ZoneLighting_form", "번호,서브유형,주창유리빛투과율,주창유리면적비,천창유리각,천창수평측면각,천창장변부길이,천창단변부길이,천창수평상부높이",
                    "'" + Num_textBox.Text + "','" +
                   roof + "','" + Roof_τD65_SNA + "','" + Roof_GlassRatio + "','" + zoneRoofAngle1 + "','" + zoneRoofAngle2 + "','" + zoneRoofLenght1 + "','" + zoneRoofLenght2 + "','" + zoneRoofLenght3
                    + "'", "번호");
                }
                else { }
            }
            else
            {
                Program.DB.setValue(DB.type.ProjDB, "ZoneLighting_form", "번호,자연채광유형,서브유형",
                   "'" + Num_textBox.Text + "','해당없음','해당없음'", "번호");

            }

            Program.DB.setValue(DB.type.ProjDB, "ZoneLighting_form", "번호,집광채광체크,집광채광번호,집광채광명칭,집광채광종류,집광채광향,집광채광각도,집광채광효율,집광채광면적,사용자면적",
               "'" + ZoneNum + "','" + RenewBool.ToString() + "','" + RenewNum + "','" + RenewName + "','" + RenewArt + "','" + RenewDirec + "','" + RenewSlope + "','" + RenewEff + "','" + RenewA + "','" + RenewType + "'", "번호");
            if (RenewType == "사용자")
            {
                Program.DB.setValue(DB.type.ProjDB, "ZoneLighting_form", "번호,사용자길이1,사용자길이2",
                   "'" + ZoneNum + "','" + RenewL1 + "','" + RenewL2 + "'", "번호");
            }
            else
            {
                Program.DB.setValue(DB.type.ProjDB, "ZoneLighting_form", "번호,표준길이1,표준길이2",
                   "'" + ZoneNum + "','" + RenewL1 + "','" + RenewL2 + "'", "번호");
            }
            Program.DB.saveProject();
        }

        private void Save_Image()
        {
            try
            {
                Bitmap bmp = new Bitmap(panel4.Width, panel4.Height);
                panel4.DrawToBitmap(bmp, new Rectangle(0, 0, panel4.Width, panel4.Height));

                string pid = "0000-00-00";
                string[][] Value = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "프로젝트번호");
                if (Value.Length > 0)
                {
                    pid = Value[0][0];
                }

                Directory.CreateDirectory(Program.gPath + "\\projects\\" + pid);

                string ImageName = "/projects/" + pid + "/" + ZoneNum + "_light" + ".png";
                string imagePath = Program.gPath + ImageName;

                bmp.Save(imagePath, System.Drawing.Imaging.ImageFormat.Png);
            }
            catch (Exception ex)
            {
                MessageBox.Show("오류 발생: " + ex.Message);
            }
        }


        private void reset()
        {
            ZoneName_textBox.Text = "";
            FL_textBox.Text = "";
            Pj_textbox.Text = "";
            fc_textBox.Text = "";
            FacadeButton.Checked = false;
            RoofButton.Checked = false;
            direction_textBox.Text = "";
            Aca_textBox.Text = ""; ;

            areaunit1.Visible = false;
            areaunit2.Visible = false;

            Subtype.Text = null;
            Renew_checkBox.Checked = false;
            RenewName = null;
            LightType = null;
            LightType2 = null;
            LightConverter = null;
            LightFi = null;
            LightW = null;
            LightFL = 0;
            Window1_textBox.Text = "";
            WindowA_textBox.Text = "";
            //Blind_textBox.Text = "";
            Blind2_textBox.Text = "";
            Blind3_textBox.Text = "";
            R1_textBox.Text = "";
            R2_textBox.Text = "";
            R3_textBox.Text = "";
            A_textBox.Text = "";
            AD_textBox.Text = "";
            bbd_textBox.Text = "";
            aad_textBox.Text = "";
            NA_textBox.Text = "";
            Pci_textBox.Text = "";
            LightType_textBox.Text = "";
            ShadeType = null;
            L1_textBox.Text = "";
            L2_textBox.Text = "";
            L4_textBox.Text = "";
            L5_textBox.Text = "";
            L6_textBox.Text = "";
            L8_textBox.Text = "";
            RenewDi_comboBox.Text = "";
            Slope_comboBox.Text = "";

            lightHeight_textBox.Text = "";
        }

        //존 리스트 클릭시 로드
        public void LoadData(String ID)
        {

            reset();
            side_active();

            Load_OtherFormData();


            String[][] Load = Program.DB.getValue(DB.type.ProjDB, "ZoneLighting_form", "번호,조명방식,제어방식,디밍유형,조명밀도,대기전력,재실계수,조도제어계수,광효율",
            "번호 = '" + ZoneNum + "'");
            if (ZoneNum != null && Load.Length > 0)
            {
                Method = Load[0][1];
                LightMethod_comboBox.SelectedItem = Method;

                control = Load[0][2];
                ControlType_comboBox.SelectedItem = control;

                dimming = Load[0][3];
                DimmingType_comboBox.SelectedItem = dimming;

                Pj = Program.UTIL.ToDoubleOrZero(Load[0][4]);
                Pj_textbox.Text = Pj.ToString();
                Program.UTIL.textBox_doubleComa(Pj_textbox, true, 2);

                Pci = Program.UTIL.ToDoubleOrZero(Load[0][5]);
                Pci_textBox.Text = Pci.ToString();
                Program.UTIL.textBox_doubleComa(Pci_textBox, true, 1);

                Fo = Program.UTIL.ToDoubleOrZero(Load[0][6]);

                Fc = Program.UTIL.ToDoubleOrZero(Load[0][7]);
                fc_textBox.Text = (Fc * Fo).ToString();
                Program.UTIL.textBox_doubleComa(fc_textBox, true, 2);

                lm_W = Program.UTIL.ToDoubleOrZero(Load[0][8]);

                String[][] Value = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_Form", "주이용일", "존번호='" + ZoneNum + "'");
                dayofuse = Program.UTIL.ToDoubleOrZero(Value[0][0]);
            }

            Load_NaturalType_image(NaturalType);
            Load_Shade_image(ShadeType);

            Load = Program.DB.getValue(DB.type.ProjDB, "ZoneLighting_form", "자연채광유형,주향,주창면적합,서브유형,이중외피유리,아트리움유리,파사드유리빛투과율,파사드너비,파사드길이,파사드높이,천창유리각,천창수평측면각,천창장변부길이,천창단변부길이,천창수평상부높이,차양",
            "번호 = '" + ZoneNum + "'");
            if (Load.Length > 0)
            {
                NaturalType = Load[0][0];
                Subtype.Text = Load[0][3].ToString();
                if (FacadeButton.Checked == true || RoofButton.Checked == true)
                {
                    if (NaturalType == "파사드")
                    {
                        Facade_MainDi = Load[0][1];
                        Facade_Aca = Program.UTIL.ToDoubleOrZero(Load[0][2]);
                        facade = Load[0][3];
                        doubleskinglass = Load[0][4];
                        atriumglass = Load[0][5];
                        zoneGlassLight = Program.UTIL.ToDoubleOrZero(Load[0][6]);
                        facadeW = Program.UTIL.ToDoubleOrZero(Load[0][7]);
                        facadeL = Program.UTIL.ToDoubleOrZero(Load[0][8]);
                        facadeH = Program.UTIL.ToDoubleOrZero(Load[0][9]);
                    }
                    else if (NaturalType == "천창")
                    {
                        Roof_MainDi = Load[0][1];
                        Roof_Aca = Program.UTIL.ToDoubleOrZero(Load[0][2]);
                        roof = Load[0][3];
                        zoneRoofAngle1 = Program.UTIL.ToDoubleOrZero(Load[0][10]);
                        zoneRoofAngle2 = Program.UTIL.ToDoubleOrZero(Load[0][11]);
                        zoneRoofLenght1 = Program.UTIL.ToDoubleOrZero(Load[0][12]);
                        zoneRoofLenght2 = Program.UTIL.ToDoubleOrZero(Load[0][13]);
                        zoneRoofLenght3 = Program.UTIL.ToDoubleOrZero(Load[0][14]);
                    }
                    else { }

                    ShadeType = Load[0][15];

                    if (facade != null)
                    {
                        FacadeButton.Checked = true;
                    }
                    else
                    {
                        RoofButton.Checked = true;
                    }
                }
            }
            //사용자면적은 집광채광유형임 (사용자 or 표준)
            Load = Program.DB.getValue(DB.type.ProjDB, "ZoneLighting_form", "집광채광체크,집광채광번호,사용자면적,집광채광명칭,집광채광종류,집광채광효율,집광채광면적,표준길이1,표준길이2,사용자길이1,사용자길이2,집광채광향,집광채광각도",
            "번호 = '" + ZoneNum + "'");
            if (Load.Length > 0)
            {
                Renew_checkBox.Checked = Convert.ToBoolean(Load[0][0]);
                if (Renew_checkBox.Checked)
                {
                    RenewNum = Load[0][1];
                    RenewType = Load[0][2];
                    RenewName = Load[0][3];
                    RenewArt = Load[0][4];
                    RenewEff = Load[0][5];
                    RenewA = Load[0][6];
                    RenewType_textBox.Text = RenewName;
                    R1_textBox.Text = RenewArt;
                    R2_textBox.Text = RenewA;
                    R3_textBox.Text = RenewEff;
                    if (RenewType == "사용자")
                    {
                        RenewL1 = Load[0][9];
                        RenewL2 = Load[0][10];
                    }
                    else
                    {
                        RenewL1 = Load[0][7];
                        RenewL2 = Load[0][8];
                    }
                    RenewDirec = Load[0][11];
                    RenewSlope = Load[0][12];

                    RenewDi_comboBox.Text = RenewDirec;
                    Slope_comboBox.Text = RenewSlope;
                }

            }

            Load = Program.DB.getValue(DB.type.ProjDB, "ZoneLighting_form", "조명번호, 등기구명칭, 램프유형, 컨버터_안정기, 광속, 소비전력, 조명계수, 표준광속, 표준소비전력,사용자광속, 사용자소비전력,조명예상전력,조명설치높이,광효율",
            "번호 = '" + ZoneNum + "'");
            if (Load.Length > 0)
            {
                LightNumber = Load[0][0];
                LightType = Load[0][1];
                LightType_textBox.Text = LightType;
                L1_textBox.Text = LightType;
                LightType2 = Load[0][2];
                L2_textBox.Text = LightType2;
                LightConverter = Load[0][3];
                L4_textBox.Text = LightConverter;
                LightFi = Load[0][4];
                LightW = Load[0][5];
                LightFL = Program.UTIL.ToDoubleOrZero(Load[0][6]);
                L8_textBox.Text = Load[0][6].ToString(); //광효율(lm/W)
                Program.UTIL.textBox_doubleComa(L8_textBox, true, 2);
                FL_textBox.Text = LightFL.ToString();
                Program.UTIL.textBox_doubleComa(FL_textBox, true, 2);

                if (LightNumber.Contains("LP"))
                {
                    L5_textBox.Text = Load[0][7];
                    L6_textBox.Text = Load[0][8];
                }
                else
                {
                    L5_textBox.Text = Load[0][9];
                    L6_textBox.Text = Load[0][10];
                }

                Pn = Program.UTIL.ToDoubleOrZero(Load[0][11]);
                LightInstallHeight = Program.UTIL.ToDoubleOrZero(Load[0][12]); //조명설치높이 반영함-공간계수
                lightHeight_textBox.Text = LightInstallHeight.ToString();
                string[][] Img = Program.DB.getValue(DB.type.BaseDB_Lighting, "조명_램프분류이미지", "이미지", "조명분류 = '" + Method + "'");
                if (Img.Length > 0)
                {
                    Ltype_pictureBox.Size = new System.Drawing.Size(60, 120);
                    Ltype_pictureBox.Location = new Point(250, 13);
                    Ltype_pictureBox.Load(Program.gPath + Img[0][0]);
                    Ltype_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;

                }
            }

            Load_AD2_image();

            string[][] Image = Program.DB.getValue(DB.type.BaseDB_HCneed, "메뉴아이콘", "하위메뉴아이콘", "하위메뉴명 = '존 조명정보'");
            if (Image.Length > 0)
            {
                Icon_pictureBox.Load(Program.gPath + Image[0][0]);
                Icon_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            }

        }

        //존명칭 로드
        private void ZoneLighting_VisibleChanged(object sender, EventArgs e)
        {
            if (main.MainContents.currentForm == main.MainContents.FormID.ZoneLighting)
            {
                String ID = main.MainContents.selID;
                int v1 = ID.IndexOf("Zone") + 4; //Zone 번호 위치 
                int v2 = ID.IndexOf("_", v1); //Zone 다음 "_"의 위치 
                ID = ID.Substring(19, v2 - 19);
                Num_textBox.Text = ID;
                ZoneNum = ID;
                LoadData(ZoneNum);
            }
        }

        private void Load_OtherFormData()
        {
            try
            {
                String[][] Value = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_Form", "존이름,천장고,용도프로필,순바닥면적", "존번호 = '" + ZoneNum + "'");
                if (Value.Length > 0)
                {
                    ZoneName = Value[0][0];
                    ZoneName_textBox.Text = ZoneName;
                    hR = Program.UTIL.ToDoubleOrZero(Value[0][1]);
                    LightInstallHeight = hR;
                    lightHeight_textBox.Text = LightInstallHeight.ToString();

                    Usage = Value[0][2];

                    A = Program.UTIL.ToDoubleOrZero(Value[0][3]); //순바닥면적
                    A_textBox.Text = A.ToString();
                    Program.UTIL.textBox_doubleComa(A_textBox, true, 2);


                    //층정보 불러오기
                    String[][] General_3D = Program.DB.getValue(DB.type.ProjDB, "Zonegeneral_3D", "층,층고", "존번호 = '" + ZoneNum + "'");

                    Layer = General_3D[0][0] + "F";


                    //Zonelight profile 가져오기 
                    string[][] ValueA = Program.DB.getValue(DB.type.BaseDB_HCneed, "용도프로필", "조도,이용영역계수,조명이용시부재율,작업면높이", "용도명 = '" + Usage + "'");
                    Em = Program.UTIL.ToDoubleOrZero(ValueA[0][0]);
                    KA = Program.UTIL.ToDoubleOrZero(ValueA[0][1]);
                    FA = Program.UTIL.ToDoubleOrZero(ValueA[0][2]);
                    hTa = Program.UTIL.ToDoubleOrZero(ValueA[0][3]);

                    Check_MainDirection();
                    if (NaturalType == "파사드")
                    {
                        Calc_MainWindowData(Facade_MainDi, false);
                        Subtype.Text = "일반 파사드";
                    }
                    else if (NaturalType == "천창")
                    {
                        Calc_MainWindowData(Roof_MainDi, true);
                        Subtype.Text = "일반형";
                    }
                    else
                    {
                        Calc_MainWindowData(None_MainDi, false);
                    }
                    WindowInfo2();
                    CheckNaturalType();
                    LightInfo();
                    Match_Pjlx();
                    Calc_Pj();
                    Calc_Fo();
                    Calc_Fc();
                    Pci_Value();
                    //Calc_AD();
                }
            }
            catch { }
            //존이름 불러오기

        }

        //존의 창호+커튼월 면적을 방위별로 합산해서, 파사드 주향/천창 주향/외벽만 있을 때의 주향을 각각 찾고
        //면적이 더 큰 쪽(파사드 vs 천창)으로 NaturalType을 확정한다
        private void Check_MainDirection()
        {
            String[] Direction = { "수평", "남", "남동", "남서", "동", "서", "북서", "북동", "북" };
            double[] FacadeArea = new double[Direction.Length];
            double[] RoofArea = new double[Direction.Length];
            double[] WallLength = new double[Direction.Length];

            //존의 외피 전체(창호,커튼월,외벽 등)를 한 번에 조회해서 외피유형별로 갈라 담는다
            String[][] Envelope = Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope_3D", "외피유형,커튼월부위,면적,벽체길이,방위,천창유무", "존 = '" + ZoneNum + "'");

            for (int i = 0; i < Direction.Length; i++)
            {
                for (int j = 0; j < Envelope.Length; j++)
                {
                    if (Envelope[j][4] == Direction[i])
                    {
                        if (Envelope[j][0] == "창호" || (Envelope[j][0] == "커튼월창" && Envelope[j][1] != "패널부분"))
                        {
                            if (Envelope[j][5] == "천창있음")
                            {
                                RoofArea[i] += Program.UTIL.ToDoubleOrZero(Envelope[j][2]);
                            }
                            else
                            {
                                FacadeArea[i] += Program.UTIL.ToDoubleOrZero(Envelope[j][2]);
                            }
                        }
                        else if (Envelope[j][0] == "외벽")
                        {
                            WallLength[i] += Program.UTIL.ToDoubleOrZero(Envelope[j][3]);
                        }
                    }
                }
            }

            int FacadeIndex = 0;
            int RoofIndex = 0;
            int WallIndex = 0;
            for (int i = 1; i < Direction.Length; i++)
            {
                if (FacadeArea[i] > FacadeArea[FacadeIndex]) { FacadeIndex = i; }
                if (RoofArea[i] > RoofArea[RoofIndex]) { RoofIndex = i; }
                if (WallLength[i] > WallLength[WallIndex]) { WallIndex = i; }
            }

            Facade_MainDi = Direction[FacadeIndex];
            Facade_Aca = FacadeArea[FacadeIndex];
            Roof_MainDi = Direction[RoofIndex];
            Roof_Aca = RoofArea[RoofIndex];
            None_MainDi = Direction[WallIndex];

            if (Facade_Aca == 0 && Roof_Aca == 0)
            {
                FacadeButton.Checked = false;
                RoofButton.Checked = false;
                NaturalType = "해당없음";
                Subtype.Text = NaturalType;
            }
            else if (Facade_Aca > Roof_Aca)
            {
                FacadeButton.Checked = true;
                NaturalType = "파사드";
            }
            else
            {
                RoofButton.Checked = true;
                NaturalType = "천창";
            }
        }

        //방위 하나를 받아서 그 방위의 실 폭(Wr)과 실 깊이(Lr)를 계산한다 (파사드/천창/해당없음 세 경우 모두 동일한 계산)
        private void Calc_RoomSize(string direction)
        {
            String[][] WallLength = Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope_3D", "벽체길이", "존 = '" + ZoneNum + "' AND 방위 = '" + direction + "' And (외피유형 = '외벽' or 외피유형='커튼월창')");

            Wr = 0;
            for (int i = 0; i < WallLength.Length; i++)
            {
                Wr += Program.UTIL.ToDoubleOrZero(WallLength[i][0]);
            }

            Lr = Wr > 0 ? A / Wr : 0;
        }

        //주향의 실 치수(Wr,Lr)를 구하고, 그 방위의 창호+커튼월 중 면적이 가장 큰 구조체 하나를 찾아
        //빛투과율/유리종류/유리면적비/상인방높이를 채운다. isRoof로 파사드용/천창용 필드에 나눠 담는다
        private void Calc_MainWindowData(string direction, bool isRoof)
        {
            Calc_RoomSize(direction);

            string SkylightCondition = isRoof ? "천창유무 = '천창있음'" : "천창유무 <> '천창있음'";

            //창호와 커튼월(패널부분 제외 - 유리부분+출입문부분 포함)을 한 번에 조회한다 (같은 표라서 조건만 or로 묶음, 외피유형 컬럼으로 나중에 구분)
            String[][] Glazing = Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope_3D", "번호,면적,구조체,구조체번호,상인방높이,외피유형",
                "존 = '" + ZoneNum + "' AND 방위 = '" + direction + "' And " + SkylightCondition +" And (외피유형 = '창호' or (외피유형 = '커튼월창' And 커튼월부위 <> '패널부분'))");

            //구조체번호별로 면적을 더하면서, 그 즉시 지금까지 중 제일 큰 합계인지도 같이 체크한다
            Dictionary<string, double> GlazingArea = new Dictionary<string, double>();
            double MaxArea = 0;
            string MaxId = null;
            bool MaxIsWindow = true;

            for (int i = 0; i < Glazing.Length; i++)
            {
                string Id = Glazing[i][3];
                double Area = Program.UTIL.ToDoubleOrZero(Glazing[i][1]);
                if (GlazingArea.ContainsKey(Id)) { GlazingArea[Id] += Area; }
                else { GlazingArea[Id] = Area; }

                if (GlazingArea[Id] > MaxArea)
                {
                    MaxArea = GlazingArea[Id];
                    MaxId = Id;
                    MaxIsWindow = Glazing[i][5] == "창호";
                    MainType_ID = Glazing[i][0];
                    Main_WinCW = Glazing[i][2];
                    hLi = Program.UTIL.ToDoubleOrZero(Glazing[i][4]);
                }
            }

            if (MaxId != null)
            {
                //구조체번호(MaxId)가 정해졌으니, 창호면 SubWindow/ConstructionWindow, 커튼월이면 ConstructionCW에서 상세값을 읽어온다
                //둘 다 [0]=빛투과율, [1]=유리종류, [2]=유리면적비 순서로 맞춰서 받는다
                String[][] GlassInfo = null;
                if (MaxIsWindow)
                {
                    GlassInfo = Program.DB.querySQL(DB.type.ProjDB, "select b.빛투과율,b.유리종류,a.유리면적비 FROM SubWindow AS a INNER JOIN ConstructionWindow AS b ON a.상위창호번호 = b.번호 where a.번호 = '" + MaxId + "'");
                }
                else
                {
                    GlassInfo = Program.DB.getValue(DB.type.ProjDB, "ConstructionCW", "빛투과율,고정유리종류,유리부분유리면적비", "번호 = '" + MaxId + "'");
                }

                if (GlassInfo.Length > 0)
                {
                    Main_glass = GlassInfo[0][1];

                    if (isRoof)
                    {
                        Roof_τD65_SNA = Program.UTIL.ToDoubleOrZero(GlassInfo[0][0]);
                        Roof_GlassRatio = Program.UTIL.ToDoubleOrZero(GlassInfo[0][2]);
                    }
                    else
                    {
                        Facade_τD65_SNA = Program.UTIL.ToDoubleOrZero(GlassInfo[0][0]);
                        Facade_GlassRatio = Program.UTIL.ToDoubleOrZero(GlassInfo[0][2]);
                    }
                }
            }
        }

        private void CheckNaturalType()
        {
            NaturalCheck();
            NaturalType_case1();
            WindowInfo();
        }

        private void RenewDi_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            RenewDirec = RenewDi_comboBox.Text;
        }

        private void Slope_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            RenewSlope = Slope_comboBox.Text;
        }

        private void infoLighting_Click(object sender, EventArgs e)
        {
            string basePath = Program.gPath + "Manual\\1.contents\\15.ZoneLight";

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
