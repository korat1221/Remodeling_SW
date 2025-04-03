using main.contentslist;
using main.subcontents.ZoneLighting;
using System;
using System.Collections.Generic;
using System.Security.Policy;

namespace main.contents
{
    public partial class ZoneLighting : Form
    {

        //변수
        String ZoneNum, ZoneName;
        //존 정보(가져오는 값)
        double Em, KA, FA;
        double Wr, Lr, A, hR, hm, hLi, hTa, K, admax, ad, bdsimple, bd, AD, unAD;
        string facade_di, Main_WinCW, facade_shade, facade_dimming, Usage;
        double Zone_f_Aca, Zone_f_a, Zone_f_b, Zone_f_AD, f_τD65_SNA, K1, K2, K3, γSh_lsh, γSh_hA, γSh_vA, f_τD65_SA;// AVG_Height, AVG_Width;
        String Main_glass, MainType_ID;
        public string roof_di, roof_glass, roof_shade, roof_dimming;
        public string none_di;
        public double r_Aca, r_aD, r_bD, r_AD, γF, γW, As, Bs, hs, hw, hg, Da, r_τD65_SNA, r_τD65_SA, Kobl_1, Kobl_2, Kobl_3;
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
        public double Reneweff, U_RenewLenght1, U_RenewLenght2, U_RenewA, RenewSlope;
        public string D_RenewLenght1, D_RenewLenght2, D_RenewA, RenewDi;
        public string RenewNum, RenewName, RenewName2, RenewA;
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
        double N; //조명 설치 개수
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
                lm_W = Convert.ToDouble(lightingdb_form.Select_Light[8]);  //광효율
                LightFL = Convert.ToDouble(lightingdb_form.Select_Light[9]);
                //Default의 경우 VAR이지만 사용자 DB의 경우 숫자
                //사용자 이름 나중에 LP 포함해서 하지 말아야겠다

                if (LightNumber.Contains("LP"))
                {
                    D_LightFi = lightingdb_form.Select_Light[6];
                    D_LightPi = lightingdb_form.Select_Light[7];
                }

                else
                {
                    U_LightFi = Convert.ToDouble(lightingdb_form.Select_Light[6]);
                    U_LightPi = Convert.ToDouble(lightingdb_form.Select_Light[7]);
                }
            }

            LightInfo();
            Match_Pjlx();
            Calc_Pj();
            Calc_Fo();
            Calc_Fc();
            Pci_Value();
            //Load_Lamp_image();
            Calc_AD();
        }


        private void LightMethod_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (LightMethod_comboBox.SelectedItem != null)
            {
                Method = LightMethod_comboBox.SelectedItem.ToString();
                Match_Pjlx();
                Calc_Pj();
                Calc_Fo();
                Calc_Fc();
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
            Check_MainDirection();
            if (NaturalType == "파사드")
            {
                LightingNatural_facade naturallighting_facade = new LightingNatural_facade(NaturalType, ZoneNum);
                DialogResult result = naturallighting_facade.ShowDialog();
                if (result == DialogResult.OK)
                {
                    this.facade = naturallighting_facade.facadetype;
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
                    this.zoneRoofAngle1 = naturallighting_roof.roofangle1;
                    this.zoneRoofAngle2 = naturallighting_roof.roofangle2;
                    this.zoneRoofLenght1 = naturallighting_roof.rooflength1;
                    this.zoneRoofLenght2 = naturallighting_roof.rooflength2;
                    this.zoneRoofLenght3 = naturallighting_roof.rooflength3;
                }
            }
            Load_AD2_image();
            Calc_AD();
            Load_NaturalType2_image();


        }

        private void RenewDB_button_Click(object sender, EventArgs e)
        {
            //Default의 경우 VAR이지만 사용자 DB의 경우 숫자
            LightingRenewDB renewdb_form = new LightingRenewDB();
            DialogResult result = renewdb_form.ShowDialog();
            if (result == DialogResult.OK)
            {
                RenewNum = renewdb_form.Select_Renew[0];
                RenewName = renewdb_form.Select_Renew[2];
                RenewName2 = renewdb_form.Select_Renew[3];
                Reneweff = Convert.ToDouble(renewdb_form.Select_Renew[5]);
                RenewA = renewdb_form.Select_Renew[8];

                RenewType_textBox.Text = RenewName;

                R1_textBox.Text = RenewName2;
                R2_textBox.Text = RenewA;
                R3_textBox.Text = Reneweff.ToString();

                if (RenewNum.Contains("DL"))
                {
                    D_RenewLenght1 = renewdb_form.Select_Renew[6];
                    D_RenewLenght2 = renewdb_form.Select_Renew[7];
                    D_RenewA = renewdb_form.Select_Renew[8];

                    R1_textBox.Text = RenewName2;
                    R2_textBox.Text = D_RenewA;
                    R3_textBox.Text = Reneweff.ToString();
                }
                else
                {
                    U_RenewLenght1 = Convert.ToDouble(renewdb_form.Select_Renew[6]);
                    U_RenewLenght2 = Convert.ToDouble(renewdb_form.Select_Renew[7]);
                    U_RenewA = Convert.ToDouble(renewdb_form.Select_Renew[8]);

                    R1_textBox.Text = RenewName2;
                    R2_textBox.Text = U_RenewA.ToString();
                    R3_textBox.Text = Reneweff.ToString();
                }
            }

        }
        private void Renew_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            RenewCheck();

            RenewDi_comboBox.SelectedItem = RenewDi;
            Slope_comboBox.SelectedItem = RenewSlope;
        }
        //-----------------------------------------------------------------------------------------------------------------------------------------------------------

        public void Match_Pjlx()
        {
            try
            {
                K = Lr * Wr / (hm * (Lr + Wr));
                double[] data = { 0.6, 0.8, 1, 1.25, 1.5, 2, 2.5, 3, 4, 5 };
                double target = K;
                var min = data.Min(x => Math.Abs(x - target));
                K = data.First(y => Math.Abs(y - target) == min);


                String[][] value = Program.DB.getValue(DB.type.BaseDB_Lighting, "조명_럭스당조명밀도", "값,UFF", "조명방식='" + Method + "' AND K = '" + K + "'");
                if (value.Length > 0)
                {
                    Pj_lx = Convert.ToDouble(value[0][0]);
                    UFF = Convert.ToDouble(value[0][1]);
                }
            }
            catch { }
        }

        public void Match_Foc()
        {
            String[][] value = Program.DB.getValue(DB.type.BaseDB_Lighting, "조명_조명제어", "Foc", "제어종류 = '" + control + "'");
            if (value.Length > 0)
            {
                Foc = Convert.ToDouble(value[0][0]);
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
                    N = 0;
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
                    { N = 0; }
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
            L8_textBox.Text = LightFL.ToString();

            label5.ForeColor = Color.Black;
            label6.ForeColor = Color.Black;
        }

        //주창 정보 보이기
        private void WindowInfo()
        {

            if (facadeButton.Checked == true || roofButton.Checked == true)
            {
                shade1_label.Visible = true;
                Shade2_label.Visible = true;
                Shade3_label.Visible = true;
                Shade4_label.Visible = true;
                Shade5_label.Visible = true;
                Shade7_label.Visible = true;
                facadeButton.Visible = true;
                roofButton.Visible = true;

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

            else if (facadeButton.Checked == false && roofButton.Checked == false)
            {
                shade1_label.Visible = false;
                Shade2_label.Visible = false;
                Shade3_label.Visible = false;
                Shade4_label.Visible = false;
                Shade5_label.Visible = false;
                Shade7_label.Visible = false;
                facadeButton.Visible = false;
                roofButton.Visible = false;

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
                bdsimple = Zone_f_Aca / (hLi - hTa);
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
            }
            else if (NaturalType == "천창")
            {
                admax = 2.5 * (hLi - hTa);
                ad = Math.Min(admax, Lr);
                bdsimple = r_Aca / (hLi - hTa);
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
            }
            else;
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
                Window1_textBox.Text = facade_di;
                WindowA_textBox.Text = Zone_f_Aca.ToString();
                Program.UTIL.textBox_doubleComa(WindowA_textBox, true, 2);

                Window_glass_textBox.Text = Main_glass;
                Window_Tao_textBox.Text = f_τD65_SNA.ToString();
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
                Window1_textBox.Text = roof_di;
                WindowA_textBox.Text = r_Aca.ToString();
                Program.UTIL.textBox_doubleComa(WindowA_textBox, true, 2);

                Window_glass_textBox.Text = Main_glass;
                Window_Tao_textBox.Text = r_τD65_SNA.ToString();
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
            if (facadeButton.Checked == false && roofButton.Checked == false)
            {
                string[][] Image = Program.DB.getValue(DB.type.BaseDB_Lighting, "조명_자연채광대분류이미지", "이미지", "자연채광대분류 = '" + "해당없음" + "'");
                if (Image.Length > 0)
                {
                    Main_pictureBox.Visible = true;
                    Main_pictureBox.Location = new Point(0, 0);
                    Main_pictureBox.Load(Program.gPath + Image[0][0]);
                    Main_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                }
            }
            else
            {
                string[][] Image = Program.DB.getValue(DB.type.BaseDB_Lighting, "조명_자연채광대분류이미지", "이미지", "자연채광대분류 = '" + Type + "'");
                if (Image.Length > 0)
                {
                    Main_pictureBox.Visible = true;
                    Main_pictureBox.Location = new Point(0, 0);
                    Main_pictureBox.Load(Program.gPath + Image[0][0]);
                    Main_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                }
            }

        }

        private void Load_NaturalType2_image()
        {
            if (this.facade == "중정")
            {
                string[][] Image = Program.DB.getValue(DB.type.BaseDB_Lighting, "조명_자연채광대분류이미지", "이미지", "자연채광대분류 = '" + "중정" + "'");
                Main_pictureBox.Load(Program.gPath + Image[0][0]);
            }

            else if (this.facade == "아트리움")
            {
                string[][] Image = Program.DB.getValue(DB.type.BaseDB_Lighting, "조명_자연채광대분류이미지", "이미지", "자연채광대분류 = '" + "아트리움" + "'");
                Main_pictureBox.Load(Program.gPath + Image[0][0]);
            }

            else if (this.facade == "이중외피")
            {
                string[][] Image = Program.DB.getValue(DB.type.BaseDB_Lighting, "조명_자연채광대분류이미지", "이미지", "자연채광대분류 = '" + "이중외피" + "'");
                Main_pictureBox.Load(Program.gPath + Image[0][0]);
            }

            else if (this.facade == "일반 파사드")
            {
                string[][] Image = Program.DB.getValue(DB.type.BaseDB_Lighting, "조명_자연채광대분류이미지", "이미지", "자연채광대분류 = '" + "파사드" + "'");
                Main_pictureBox.Load(Program.gPath + Image[0][0]);
            }
            else;
            // this.Main_pictureBox.Location = new Point(0, 0);
        }



        //자연채광 체크박스 활성화 비활성화
        public void NaturalCheck()
        {
            if (facadeButton.Checked == true || roofButton.Checked == true)
            {
                roofButton.Visible = true;
                facadeButton.Visible = true;
                //NaturalType_comboBox.Visible = true;
                NaturalDB_button.Visible = true;
                Direction_label.Visible = true;
                Aca_label.Visible = true;
                direction_textBox.Visible = true;
                Aca_textBox.Visible = true;
                Acam2_label.Visible = true;
                type_pictureBox.Visible = true;
            }

            else
            {
                roofButton.Visible = false;
                facadeButton.Visible = false;
                //NaturalType_comboBox.Visible = false;
                NaturalDB_button.Visible = false;
                Direction_label.Visible = false;
                Aca_label.Visible = false;
                direction_textBox.Visible = false;
                Aca_textBox.Visible = false;
                Acam2_label.Visible = false;
                type_pictureBox.Visible = false;
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
                Main_pictureBox3.Location = new Point(0, 0);
                Main_pictureBox3.BackColor = Color.Transparent;
                Main_pictureBox3.Parent = Main_pictureBox2;
            }
        }

        public void NaturalType_case1()
        {
            // 자연채광 유형에 따른 해당 값 
            if (NaturalType == "파사드")
            {
                direction_textBox.Text = facade_di;
                Aca_textBox.Text = Zone_f_Aca.ToString();
                Program.UTIL.textBox_doubleComa(Aca_textBox, true, 2);

            }
            else if (NaturalType == "천창")
            {
                direction_textBox.Text = roof_di;
                Aca_textBox.Text = r_Aca.ToString();
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
            if (facadeButton.Checked == true && NaturalType == "파사드" && ShadeType != "차양없음")
            {
                Image = Program.DB.getValue(DB.type.BaseDB_Lighting, "조명_차양이미지", "이미지", "차양 = '" + "파사드차양" + "'");
            }
            else if (facadeButton.Checked == true && NaturalType == "파사드" && ShadeType == "차양없음")
            {
                Image = Program.DB.getValue(DB.type.BaseDB_Lighting, "조명_차양이미지", "이미지", "차양 = '" + "파사드" + "'");
            }
            else if (roofButton.Checked == true && NaturalType == "천창" && ShadeType != "차양없음")
            {
                Image = Program.DB.getValue(DB.type.BaseDB_Lighting, "조명_차양이미지", "이미지", "차양 = '" + "천창차양" + "'");
            }
            else if (roofButton.Checked == true && NaturalType == "천창" && ShadeType == "차양없음")
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
                Main_pictureBox2.BackColor = Color.Transparent;
                Main_pictureBox2.Parent = Main_pictureBox;
            }

        }

        //상세 선택에 따른 변화 (체크박스에 걸기)
        private void Load_AD2_image()
        {
            if (facadeButton.Checked == true || roofButton.Checked == true && NaturalType == "파사드" && facade == "일반 파사드")
            {
                string[][] Image = Program.DB.getValue(DB.type.BaseDB_Lighting, "조명_주광면적이미지", "이미지", "주광면적 = '" + "일반 파사드" + "'");
                type_pictureBox.Load(Program.gPath + Image[0][0]);
                type_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            }

            else if (facadeButton.Checked == true || roofButton.Checked == true && NaturalType == "파사드" && facade == "이중외피")
            {
                string[][] Image = Program.DB.getValue(DB.type.BaseDB_Lighting, "조명_주광면적이미지", "이미지", "주광면적 = '" + "이중외피" + "'");
                type_pictureBox.Load(Program.gPath + Image[0][0]);
                type_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            }

            else if (facadeButton.Checked == true || roofButton.Checked == true && NaturalType == "파사드" && facade == "중정")
            {
                string[][] Image = Program.DB.getValue(DB.type.BaseDB_Lighting, "조명_주광면적이미지", "이미지", "주광면적 = '" + "중정" + "'");
                type_pictureBox.Load(Program.gPath + Image[0][0]);
                type_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            }

            else if (facadeButton.Checked == true || roofButton.Checked == true && NaturalType == "파사드" && facade == "아트리움")
            {
                string[][] Image = Program.DB.getValue(DB.type.BaseDB_Lighting, "조명_주광면적이미지", "이미지", "주광면적 = '" + "아트리움" + "'");
                type_pictureBox.Load(Program.gPath + Image[0][0]);
                type_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            }

            else if (facadeButton.Checked == true || roofButton.Checked == true && NaturalType == "천창")
            {
                string[][] Image = Program.DB.getValue(DB.type.BaseDB_Lighting, "조명_주광면적이미지", "이미지", "주광면적 = '" + "천창" + "'");
                type_pictureBox.Load(Program.gPath + Image[0][0]);
                type_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            }
            else;
        }


        //사이드 활성화 
        private void side_active()
        {
            if (facadeButton.Checked == true || roofButton.Checked == true)
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

            else
            {
                A_label.Visible = false;
                A_textBox.Visible = false;
                AD_label.Visible = false;
                AD_textBox.Visible = false;
                aad_label.Visible = false;
                aad_textBox.Visible = false;
                bbd_label.Visible = false;
                bbd_textBox.Visible = false;
                NA_label.Visible = false;
                NA_textBox.Visible = false;
                label10.Visible = false;
                label11.Visible = false;
                label12.Visible = false;
                label13.Visible = false;
                label14.Visible = false;
            }
        }

        public static bool OnLoadListProc(Form form)
        {
            List_Zone f = (List_Zone)form;
            f.load_List(Layer);
            return true;
        }

        private void Save_button_Click(object sender, EventArgs e)
        {
            if (LightType == null)
            {
                MessageBox.Show("조명 종류를 선택하세요.");
            }
            else if (Renew_checkBox.Checked == true && RenewNum == null)
            {
                MessageBox.Show("집광채광을 선택하세요.");
            }
            else if ((NaturalType == "천창") && (zoneRoofLenght1 == 0 || zoneRoofLenght2 == 0 || zoneRoofLenght3 == 0))
            {
                MessageBox.Show("천창 상세 길이 정보를 입력하세요.");
            }
            else if ((this.facade == "아트리움") && (facadeW == 0 || facadeL == 0 || facadeH == 0))
            {
                MessageBox.Show(" 아트리움 상세 길이 정보를 입력하세요.");

            }
            else if ((this.facade == "중정") && (facadeW == 0 || facadeL == 0 || facadeH == 0))
            {
                MessageBox.Show(" 중정 상세 길이 정보를 입력하세요.");

            }
            else
            {
                Save();
            }
        }

        private void Save()
        {
            Program.DB.setValue(DB.type.ProjDB, "ZoneLighting_form", "번호,너비,길이,순바닥면적,상인방높이,작업면높이,공간계수,기준조도," +
                "조명방식,제어방식,디밍유형,조명밀도,조명예상전력," +
                "대기전력,재실계수,조도제어계수," +
                "조명번호, 등기구명칭, 램프유형, 컨버터_안정기, 광효율, 조명계수,조명개수," +
                "집광채광체크",
                "'" + Num_textBox.Text + "','" + Wr + "','" + Lr + "','" + A + "','" + hLi + "','" + hTa + "','" + K + "','" + Em + "','" +
                Method + "','" + control + "','" + dimming + "','" + Pj.ToString() + "','" + Pn.ToString() + "','" +
                Pci.ToString() + "','" + Fo.ToString() + "','" + Fc.ToString() + "','" +
                LightNumber + "','" + LightType + "','" + LightType2 + "','" + LightConverter + "','" + lm_W + "','" + LightFL.ToString() + "','" + N.ToString() + "','" +
                Renew_checkBox.Checked.ToString()
                + "'", "번호");

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



            if (facadeButton.Checked == true || roofButton.Checked == true)
            {
                Program.DB.setValue(DB.type.ProjDB, "ZoneLighting_form", "번호,자연채광유형,주향,주창면적합,주창유리종류,주창아이디,차양,주광길이,주광깊이,주광면적,비주광면적",
                    "'" + Num_textBox.Text + "','" +
                   NaturalType + "','" + direction_textBox.Text + "','" + Convert.ToDouble(Aca_textBox.Text.ToString()) + "','" + Main_glass + "','" + MainType_ID + "','" + Blind3_textBox.Text + "','" + bd + "','" + ad + "','" + AD + "','" + unAD
                    + "'", "번호");


                if (facadeButton.Checked == true)
                {
                    Program.DB.setValue(DB.type.ProjDB, "ZoneLighting_form", "번호,서브유형,주창유리빛투과율,주창유리면적비,이중외피유리,아트리움유리,파사드유리빛투과율,파사드너비,파사드길이,파사드높이",
                    "'" + Num_textBox.Text + "','" +
                   facade + "','" + f_τD65_SNA + "','" + K1 + "','" + doubleskinglass + "','" + atriumglass + "','" + zoneGlassLight + "','" + facadeW + "','" + facadeL + "','" + facadeH
                    + "'", "번호");
                }
                else if (roofButton.Checked == true)
                {
                    Program.DB.setValue(DB.type.ProjDB, "ZoneLighting_form", "번호,서브유형,주창유리빛투과율,주창유리면적비,천창유리각,천창수평측면각,천창장변부길이,천창단변부길이,천창수평상부높이",
                    "'" + Num_textBox.Text + "','" +
                   roof + "','" + r_τD65_SNA + "','" + Kobl_1 + "','" + zoneRoofAngle1 + "','" + zoneRoofAngle2 + "','" + zoneRoofLenght1 + "','" + zoneRoofLenght2 + "','" + zoneRoofLenght3
                    + "'", "번호");
                }
                else { }
            }
            else { }



            if (Renew_checkBox.Checked == true)
            {
                Program.DB.setValue(DB.type.ProjDB, "ZoneLighting_form", "번호,집광채광번호,집광채광명칭,집광채광종류,집광채광향,집광채광각도,집광채광효율,집광채광면적",
               "'" + Num_textBox.Text + "','" +
                 RenewNum + "','" + RenewName + "','" + RenewName2 + "','" + RenewDi_comboBox.Text + "','" + RenewSlope.ToString() + "','" + Reneweff.ToString() + "','" + Convert.ToDouble(R2_textBox.Text.ToString())
               + "'", "번호");

                if (RenewNum.Contains("DL"))
                {
                    Program.DB.setValue(DB.type.ProjDB, "ZoneLighting_form", "번호,표준길이1,표준길이2",
              "'" + Num_textBox.Text + "','" +
                D_RenewLenght1 + "','" + D_RenewLenght2
              + "'", "번호");
                }

                else
                {
                    Program.DB.setValue(DB.type.ProjDB, "ZoneLighting_form", "번호,사용자길이1,사용자길이2,사용자면적",
             "'" + Num_textBox.Text + "','" +
               U_RenewLenght1.ToString() + "','" + U_RenewLenght2.ToString() + "','" + U_RenewA.ToString()
             + "'", "번호");
                }
            }
            else { }

            Program.DB.saveProject();
            MessageBox.Show(ZoneNum + "[" + ZoneName + "] 정보를 저장하였습니다.");
            this.DialogResult = DialogResult.OK;
            this.Hide();
            Program.getMenuForm().DoLoadForm(33, OnLoadListProc);
        }
        private void reset()
        {
            Num_textBox.Text = "";
            ZoneName_textBox.Text = "";
            FL_textBox.Text = "";
            Pj_textbox.Text = "";
            fc_textBox.Text = "";
            facadeButton.Checked = false;
            roofButton.Checked = false;
            direction_textBox.Text = "";
            Aca_textBox.Text = ""; ;
            Renew_checkBox.Checked = true;
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
        }

        //존 리스트 클릭시 로드
        public void LoadData(String ID)
        {

            reset();
            Load_OtherFormData();


            Num_textBox.Text = ID;

            String[][] Load = Program.DB.getValue(DB.type.ProjDB, "ZoneLighting_form", "번호,조명방식,제어방식,디밍유형,조명밀도,대기전력,재실계수,조도제어계수",
            "번호 = '" + ZoneNum + "'");
            if (Load.Length > 0)
            {
                Num_textBox.Text = Load[0][0];

                Method = Load[0][1];
                LightMethod_comboBox.SelectedItem = Method;

                control = Load[0][2];
                ControlType_comboBox.SelectedItem = control;

                dimming = Load[0][3];
                DimmingType_comboBox.SelectedItem = dimming;

                Pj = Convert.ToDouble(Load[0][4]);
                Pj_textbox.Text = Pj.ToString();
                Program.UTIL.textBox_doubleComa(Pj_textbox, true, 2);

                Pci = Convert.ToDouble(Load[0][5]);
                Pci_textBox.Text = Pci.ToString();
                Program.UTIL.textBox_doubleComa(Pci_textBox, true, 1);

                Fo = Convert.ToDouble(Load[0][6]);

                Fc = Convert.ToDouble(Load[0][7]);
                fc_textBox.Text = (Fc * Fo).ToString();
                Program.UTIL.textBox_doubleComa(fc_textBox, true, 2);

                String[][] Value = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_Form", "주이용일", "존번호='" + ZoneNum + "'");
                dayofuse = Convert.ToDouble(Value[0][0]);
            }

            Load_NaturalType_image(NaturalType);
            Load_Shade_image(ShadeType);

            Load = Program.DB.getValue(DB.type.ProjDB, "ZoneLighting_form", "자연채광유형,주향,주창면적합,서브유형,이중외피유리,아트리움유리,파사드유리빛투과율,파사드너비,파사드길이,파사드높이,천창유리각,천창수평측면각,천창장변부길이,천창단변부길이,천창수평상부높이,차양",
            "번호 = '" + ZoneNum + "'");
            if (Load.Length > 0)
            {
                NaturalType = Load[0][0];
                if (facadeButton.Checked == true || roofButton.Checked == true)
                {
                    if (NaturalType == "파사드")
                    {
                        facade_di = Load[0][1];
                        Zone_f_Aca = Convert.ToDouble(Load[0][2]);
                        facade = Load[0][3];
                        doubleskinglass = Load[0][4];
                        atriumglass = Load[0][5];
                        zoneGlassLight = Convert.ToDouble(Load[0][6]);
                        facadeW = Convert.ToDouble(Load[0][7]);
                        facadeL = Convert.ToDouble(Load[0][8]);
                        facadeH = Convert.ToDouble(Load[0][9]);
                    }
                    else if (NaturalType == "천창")
                    {
                        roof_di = Load[0][1];
                        r_Aca = Convert.ToDouble(Load[0][2]);
                        roof = Load[0][3];
                        zoneRoofAngle1 = Convert.ToDouble(Load[0][10]);
                        zoneRoofAngle2 = Convert.ToDouble(Load[0][11]);
                        zoneRoofLenght1 = Convert.ToDouble(Load[0][12]);
                        zoneRoofLenght2 = Convert.ToDouble(Load[0][13]);
                        zoneRoofLenght3 = Convert.ToDouble(Load[0][14]);
                    }
                    else { }

                    ShadeType = Load[0][15];

                    if (facade != null)
                    {
                        facadeButton.Checked = true;
                    }
                    else
                    {
                        roofButton.Checked = true;
                    }
                }
            }

            Load = Program.DB.getValue(DB.type.ProjDB, "ZoneLighting_form", "집광채광번호,집광채광명칭,집광채광종류,집광채광효율,집광채광면적,표준길이1,표준길이2,사용자길이1,사용자길이2,사용자면적,집광채광체크,집광채광향,집광채광각도",
            "번호 = '" + ZoneNum + "'");
            if (Load.Length > 0)
            {
                Renew_checkBox.Checked = Convert.ToBoolean(Load[0][10]);
                if (Renew_checkBox.Checked)
                {
                    RenewNum = Load[0][0];
                    RenewName = Load[0][1];
                    RenewName2 = Load[0][2];
                    Reneweff = Convert.ToDouble(Load[0][3]);
                    RenewA = Load[0][4];
                    RenewType_textBox.Text = RenewName;
                    R1_textBox.Text = RenewName2;
                    R2_textBox.Text = RenewA;
                    R3_textBox.Text = Reneweff.ToString();
                    if (RenewNum.Contains("DL"))
                    {
                        D_RenewLenght1 = Load[0][5];
                        D_RenewLenght2 = Load[0][6];
                        D_RenewA = Load[0][4];
                        R1_textBox.Text = RenewName2;
                        R2_textBox.Text = D_RenewA;
                        R3_textBox.Text = Reneweff.ToString();
                    }

                    else
                    {
                        U_RenewLenght1 = Convert.ToDouble(Load[0][7]);
                        U_RenewLenght2 = Convert.ToDouble(Load[0][8]);
                        U_RenewA = Convert.ToDouble(Load[0][9]);
                        R1_textBox.Text = RenewName2;
                        R2_textBox.Text = U_RenewA.ToString();
                        R3_textBox.Text = Reneweff.ToString();
                    }
                    RenewDi_comboBox.Text = Load[0][11];
                    Slope_comboBox.Text = Load[0][12];
                }

            }

            Load = Program.DB.getValue(DB.type.ProjDB, "ZoneLighting_form", "조명번호, 등기구명칭, 램프유형, 컨버터_안정기, 광속, 소비전력, 조명계수, 표준광속, 표준소비전력,사용자광속, 사용자소비전력,조명예상전력",
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
                LightFL = Convert.ToDouble(Load[0][6]);
                L8_textBox.Text = LightFL.ToString();
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

                Pn = Convert.ToDouble(Load[0][11]);
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
            //존이름 불러오기
            String[][] Value = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_Form", "존이름,천장고,용도프로필,순바닥면적", "존번호 = '" + ZoneNum + "'");
            if (Value.Length > 0)
            {
                ZoneName = Value[0][0];
                ZoneName_textBox.Text = ZoneName;
                if (Value[0][1] != "")
                {
                    hR = Convert.ToDouble(Value[0][1]);
                    hm = hR;
                }

                Usage = Value[0][2];
                if (Value[0][3] != "")
                {
                    A = Convert.ToDouble(Value[0][3]); //순바닥면적
                    A_textBox.Text = A.ToString();
                    Program.UTIL.textBox_doubleComa(A_textBox, true, 2);
                }


                //층정보 불러오기
                String[][] General_3D = Program.DB.getValue(DB.type.ProjDB, "Zonegeneral_3D", "층,층고", "존번호 = '" + ZoneNum + "'");

                Layer = General_3D[0][0] + "F";
                hLi = 2.5;

                //Zonelight profile 가져오기 
                string[][] ValueA = Program.DB.getValue(DB.type.BaseDB_HCneed, "용도프로필", "조도,이용영역계수,조명이용시부재율,작업면높이", "용도명 = '" + Usage + "'");
                if (ValueA[0][0] != "")
                {
                    Em = Convert.ToDouble(ValueA[0][0]);
                }
                if (ValueA[0][1] != "")
                {
                    KA = Convert.ToDouble(ValueA[0][1]);
                }
                if (ValueA[0][2] != "")
                {
                    FA = Convert.ToDouble(ValueA[0][2]);
                }
                if (ValueA[0][3] != "")
                {
                    hTa = Convert.ToDouble(ValueA[0][3]);
                }


                Check_MainDirection();
                if (NaturalType == "파사드")
                {
                    Calc_Facade_Data();
                }
                else if (NaturalType == "천창")
                {
                    Calc_Roof_Data();
                }
                else
                {
                    Calc_None_Data();
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

        private void Check_MainDirection()
        {
            ////////////////////////////////////////////////////////////////////파사드 주 향 찾기//////////////////////////////////////////////////////////////////////////////////////////////////////////
            //존에 있는 모든 창호 및 커튼월 불러와서 주향 찾기 
            String[][] TotalEnvelope_Win = Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope_3D", "번호,면적,방위,천창유무", "존 = '" + ZoneNum + "' And 외피유형 = '창호'");
            String[][] TotalEnvelope_CW = Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope_3D", "번호,면적,방위,천창유무", "존 = '" + ZoneNum + "' And 외피유형 = '커튼월창' And 커튼월부위 = '유리부분'");
            double[] AreaSum_Win = new double[9]; //향별 창호 면적합계
            double[] AreaSum_CW = new double[9]; //향별 커튼월 면적합계
            double[] AreaSum_Total = new double[9]; //향별 창호과 커튼월 면적합계
            String[] Direction = { "수평", "남", "남동", "남서", "동", "서", "북서", "북동", "북" };
            if (TotalEnvelope_Win.Length > 0)
            {
                for (int k = 0; k < TotalEnvelope_Win.Length; k++)
                {
                    if (TotalEnvelope_Win[k][3] != "천창있음")
                    {

                        for (int j = 0; j < Direction.Length; j++)
                        {
                            if (TotalEnvelope_Win[k][2] == Direction[j])
                                AreaSum_Win[j] += Convert.ToDouble(TotalEnvelope_Win[k][1]);

                        }
                    }
                }
            }
            if (TotalEnvelope_CW.Length > 0)
            {
                for (int k = 0; k < TotalEnvelope_CW.Length; k++)
                {
                    if (TotalEnvelope_CW[k][3] != "천창있음")
                    {
                        for (int j = 0; j < Direction.Length; j++)
                        {
                            if (TotalEnvelope_CW[k][2] == Direction[j])
                                AreaSum_CW[j] += Convert.ToDouble(TotalEnvelope_CW[k][1]);
                        }
                    }
                }
            }
            for (int j = 0; j < Direction.Length; j++)
            {
                AreaSum_Total[j] = AreaSum_Win[j] + AreaSum_CW[j];
            }

            for (int j = 0; j < Direction.Length; j++)
            {
                if (AreaSum_Total[j] == AreaSum_Total.Max())
                {
                    facade_di = Direction[j]; //주향
                    Zone_f_Aca = AreaSum_Total[j]; //주향의 커튼월과 창호 면적합
                }
            }
            ////////////////////////////////////////////////////////////////////천창 주 향 찾기//////////////////////////////////////////////////////////////////////////////////////////////////////////
            //존에 있는 모든 창호 및 커튼월 불러와서 주향 찾기 
            String[][] TotalEnvelope_Win2 = Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope_3D", "번호,면적,방위", "존 = '" + ZoneNum + "' And 외피유형 = '창호' And 천창유무 = '천창있음'");
            String[][] TotalEnvelope_CW2 = Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope_3D", "번호,면적,방위", "존 = '" + ZoneNum + "' And 외피유형 = '커튼월창' And 커튼월부위 = '유리부분' And 천창유무 = '천창있음'");

            double[] AreaSum_Win2 = new double[9]; //향별 창호 면적합계
            double[] AreaSum_CW2 = new double[9]; //향별 커튼월 면적합계
            double[] AreaSum_Total2 = new double[9]; //향별 창호과 커튼월 면적합계

            String[] Direction2 = { "수평", "남", "남동", "남서", "동", "서", "북서", "북동", "북" };
            if (TotalEnvelope_Win2.Length > 0)
            {
                for (int j = 0; j < Direction2.Length; j++)
                {
                    for (int k = 0; k < TotalEnvelope_Win2.Length; k++)
                    {
                        if (TotalEnvelope_Win2[k][2] == Direction2[j])
                            AreaSum_Win2[j] += Convert.ToDouble(TotalEnvelope_Win2[k][1]);
                    }
                }
            }
            if (TotalEnvelope_CW2.Length > 0)
            {
                for (int j = 0; j < Direction2.Length; j++)
                {
                    for (int k = 0; k < TotalEnvelope_CW2.Length; k++)
                    {
                        if (TotalEnvelope_CW2[k][2] == Direction2[j])
                            AreaSum_CW2[j] += Convert.ToDouble(TotalEnvelope_CW2[k][1]);
                    }
                }
            }
            for (int j = 0; j < Direction2.Length; j++)
            {
                AreaSum_Total2[j] = AreaSum_Win2[j] + AreaSum_CW2[j];
            }

            for (int j = 0; j < Direction2.Length; j++)
            {

                if (AreaSum_Total2[j] == AreaSum_Total2.Max())
                {
                    roof_di = Direction2[j]; //주향
                    r_Aca = AreaSum_Total2[j]; //주향의 커튼월과 창호 면적합
                }
            }

            ////////////////////////////////////////////////////////////////////일반실(천창도 파사드도 아닌거) 주 향 찾기//////////////////////////////////////////////////////////////////////////////////////////////////////////
            //존에 있는 모든 창호 및 커튼월 불러와서 주향 찾기 X
            //해당 존에 커튼월 및 창호가 없을 경우에는 주향을 그 존의 가장 긴 변으로 하기 
            String[][] Envelope_Wall = Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope_3D", "번호,벽체길이,방위", "존 = '" + ZoneNum + "' And 외피유형 = '외벽'");
            double[] WallLength = new double[9]; // 향별 외벽 벽체길이
            String[] Direction3 = { "수평", "남", "남동", "남서", "동", "서", "북서", "북동", "북" };
            if (Envelope_Wall.Length > 0)
            {
                for (int j = 0; j < Direction3.Length; j++)
                {
                    for (int k = 0; k < Envelope_Wall.Length; k++)
                    {
                        if (WallLength[j] == WallLength.Max())
                        {
                            none_di = Envelope_Wall[k][2];
                        }
                    }
                }
            }

            if (Zone_f_Aca == 0 && r_Aca == 0)
            {
                roofButton.Checked = false;
                facadeButton.Checked = false;
                NaturalType = "해당없음";

            }
            else
            {
                //Main_pictureBox.Visible = true;
                if (Zone_f_Aca > r_Aca)
                {
                    NaturalType = "파사드";
                    facadeButton.Checked = true;
                    Load_AD2_image();
                }
                else
                {
                    roofButton.Checked = true;
                    NaturalType = "천창";
                    Load_AD2_image();
                }
            }

        }
        private void Calc_Facade_Data()
        {
            //facade1 가져오기 
            ////////////////////////////////////////////////////////주향 기준 실너비(그 향의 벽체길이) 깊이 계산하기////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            String[][] Wall_Length = Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope_3D", "벽체길이", "존 = '" + ZoneNum + "' AND 방위 = '" + facade_di + "' And 외피유형 = '외벽'");
            if (Wall_Length.Length > 0)
            {
                Wr = 0;
                for (int j = 0; j < Wall_Length.Length; j++)
                {
                    if (Wall_Length[j][0] == "") { Wr += 1; }
                    else { Wr += Convert.ToDouble(Wall_Length[j][0]); }
                }
                Lr = A / Wr;
            }


            ////////////////////////////////////////////////////////주 향의 창호 커튼월 높이, 너비 계산하기////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // String[][] MEnvelope_Wina = Program.DB.querySQL(DB.type.ProjDB, " select b.창호높이,b.창호면적 FROM ZoneEnvelope_3D AS a INNER JOIN SubWindow AS b ON a.구조체 = b.명칭");


            //주향 창호 및 커튼월 정보 불러오기
            // String[][] MEnvelope_Win = Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope_3D", "번호, 면적, 구조체,구조체번호", "존 = '" + ZoneNum + "' AND 방위 = '" + facade_di + "' And 외피유형 = '창호' And 천창유무 <> '천창있음'");

            String[][] MEnvelope_Win = Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope_3D", "번호, 면적, 천창유무,구조체,구조체번호", "존 = '" + ZoneNum + "' AND 방위 = '" + facade_di + "' And 외피유형 = '창호'");
            //  String[][] MEnvelope_CW = Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope_3D", "번호, 면적, 구조체,구조체번호", "존 = '" + ZoneNum + "' AND 방위 = '" + facade_di + "' And 외피유형 = '커튼월창' And 커튼월부위 = '유리부분' And 천창유무 <> '천창있음'");
            String[][] MEnvelope_CW = Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope_3D", "번호, 면적, 천창유무,구조체,구조체번호", "존 = '" + ZoneNum + "' AND 방위 = '" + facade_di + "' And 외피유형 = '커튼월창' And 커튼월부위 = '유리부분'");

            ////////////////////////////////////////////////////////주향 창호 OR 커튼월 유형 및 빛 투과율 찾기 (세가지 케이스)////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            //창호 구조체 타입 찾기 > SubWindow(사이즈별)임 MainWindow(창호 자재 조합유형별)아님 
            String[][] Win_Type = Program.DB.getValue_SameCheck(DB.type.ProjDB, "ZoneEnvelope_3D", "구조체,구조체번호,번호", "존 = '" + ZoneNum + "' And 방위 = '" + facade_di + "' And 외피유형 = '창호'");
            //커튼월창 구조체 타입 찾기 
            String[][] CW_Type = Program.DB.getValue_SameCheck(DB.type.ProjDB, "ZoneEnvelope_3D", "구조체,구조체번호,번호", "존 = '" + ZoneNum + "' And 방위 = '" + facade_di + "' And 외피유형 = '커튼월창'");

            double AreaSum_Wins = 0;
            double[] AreaSum_ConstructionWin = new double[Win_Type.Length];
            double MaxSum_Win;
            int index;
            String MainType_SubWin;
            String[][] MainType_Win;
            String[][] MainType_Win_Value;

            double MaxSum_CW;
            String[][] MainType_CW_Value;
            String MainType_CW;

            if (CW_Type.Length == 0)
            {
                //창호 구조체 타입별로 면적 합계 구하기
                if (MEnvelope_Win.Length > 0 && Win_Type.Length > 0)
                {
                    for (int j = 0; j < MEnvelope_Win.Length; j++)
                    {
                        if (MEnvelope_Win[j][2] != "천창있음")
                        {
                            for (int k = 0; k < Win_Type.Length; k++)
                            {
                                if (MEnvelope_Win[j][3] == Win_Type[k][0])
                                {
                                    AreaSum_ConstructionWin[k] += Convert.ToDouble(MEnvelope_Win[j][1]);
                                }
                            }
                        }
                    }
                }
                MaxSum_Win = AreaSum_ConstructionWin.Max(); //창호 구조체 타입별로 면적 합계 중 가장 큰 값
                index = 0;
                for (int k = 0; k < Win_Type.Length; k++)
                {
                    if (AreaSum_ConstructionWin[k] == AreaSum_ConstructionWin.Max())
                    { index = k; }  //창호 구조체 타입별로 면적 합계 중 가장 큰 값의 인덱스
                }


                MainType_SubWin = Win_Type[index][1]; // 창호 구조체 타입별로 면적 합계 중 가장 큰 값의 구조체 유형 > SubWindow(사이즈별)임 MainWindow(창호 자재 조합유형별)아님 
                MainType_ID = Win_Type[index][2];/////////////////////////// 창호 구조체 타입별로 면적 합계 중 가장 큰 값의 구조체 ID

                //찾은 SubWindow정보에서 MainWindow 찾기 
                MainType_Win = Program.DB.getValue(DB.type.ProjDB, "SubWindow", "상위창호번호,유리면적비", "번호 = '" + MainType_SubWin + "'");
                if (MainType_Win.Length > 0)
                {
                    //찾은 주 창호 유형의 빛투과율 찾기 
                    MainType_Win_Value = Program.DB.getValue(DB.type.ProjDB, "ConstructionWindow", "빛투과율, 유리종류", "번호 = '" + MainType_Win[0][0].ToString() + "'");
                    Main_WinCW = MainType_Win[0][0];
                    K1 = Convert.ToDouble(MainType_Win[0][1]);
                    if (MainType_Win_Value.Length > 0)
                    {
                        f_τD65_SNA = Convert.ToDouble(MainType_Win_Value[0][0]);
                        Main_glass = MainType_Win_Value[0][1];
                    }
                }

            }
            else if (Win_Type.Length == 0)
            {
                //커튼월창 구조체 타입별로 면적 합계 구하기
                double[] AreaSum_ConstructionCW = new double[CW_Type.Length];
                if (MEnvelope_CW.Length > 0 && CW_Type.Length > 0)
                {
                    for (int j = 0; j < MEnvelope_CW.Length; j++)
                    {
                        if (MEnvelope_CW[j][2] != "천창있음")
                        {
                            for (int k = 0; k < CW_Type.Length; k++)
                            {
                                if (MEnvelope_CW[j][4] == CW_Type[k][1])
                                { AreaSum_ConstructionCW[k] += Convert.ToDouble(MEnvelope_CW[j][1]); }
                            }
                        }
                    }
                }

                MaxSum_CW = AreaSum_ConstructionCW.Max(); //커튼월창 구조체 타입별로 면적 합계 중 가장 큰 값
                index = 0;
                for (int k = 0; k < CW_Type.Length; k++)
                {
                    if (AreaSum_ConstructionCW[k] == AreaSum_ConstructionCW.Max())
                    { index = k; }  //창호 구조체 타입별로 면적 합계 중 가장 큰 값의 인덱스
                }
                if (CW_Type.Length > 0)
                {
                    MainType_CW_Value = Program.DB.getValue(DB.type.ProjDB, "ConstructionCW", "빛투과율, 고정유리종류, 번호, 유리부분유리면적비", "번호 = '" + CW_Type[index][1] + "'");
                    if (MainType_CW_Value.Length > 0)
                    {
                        MainType_CW = MainType_CW_Value[0][2].ToString(); // 커튼월창 구조체 타입별로 면적 합계 중 가장 큰 값의 구조체 유형  
                        MainType_ID = CW_Type[index][2];

                        Main_WinCW = CW_Type[index][0];
                        f_τD65_SNA = Convert.ToDouble(MainType_CW_Value[0][0]);
                        Main_glass = MainType_CW_Value[0][1];
                        K1 = Convert.ToDouble(MainType_CW_Value[0][3]);
                    }
                }

            }
            else
            {
                //창호 구조체 타입별로 면적 합계 구하기
                if (MEnvelope_Win.Length > 0)
                {
                    for (int j = 0; j < MEnvelope_Win.Length; j++)
                    {
                        if (MEnvelope_Win[j][2] != "천창있음")
                        {
                            for (int k = 0; k < Win_Type.Length; k++)
                            {
                                if (MEnvelope_Win[j][3] == Win_Type[k][0])
                                { AreaSum_ConstructionWin[k] += Convert.ToDouble(MEnvelope_Win[j][1]); }
                            }
                        }
                    }
                }
                MaxSum_Win = AreaSum_ConstructionWin.Max(); //창호 구조체 타입별로 면적 합계 중 가장 큰 값
                index = 0;
                for (int k = 0; k < Win_Type.Length; k++)
                {
                    if (AreaSum_ConstructionWin[k] == AreaSum_ConstructionWin.Max())
                    { index = k; }  //창호 구조체 타입별로 면적 합계 중 가장 큰 값의 인덱스
                }


                MainType_SubWin = Win_Type[index][1]; // 창호 구조체 타입별로 면적 합계 중 가장 큰 값의 구조체 유형 > SubWindow(사이즈별)임 MainWindow(창호 자재 조합유형별)아님 
                MainType_ID = Win_Type[index][2];/////////////////////////// 창호 구조체 타입별로 면적 합계 중 가장 큰 값의 구조체 ID


                //찾은 SubWindow정보에서 MainWindow 찾기 
                MainType_Win = Program.DB.getValue(DB.type.ProjDB, "SubWindow", "상위창호번호, 유리면적비", "번호 = '" + MainType_SubWin + "'");


                //찾은 주 창호 유형의 빛투과율 찾기 
                if (MainType_Win.Length > 0)
                {
                    MainType_Win_Value = Program.DB.getValue(DB.type.ProjDB, "ConstructionWindow", "빛투과율, 유리종류", "번호 = '" + MainType_Win[0][0].ToString() + "'");
                    if (MainType_Win_Value.Length > 0)
                    {
                        //커튼월창 구조체 타입별로 면적 합계 구하기
                        double[] AreaSum_ConstructionCW = new double[CW_Type.Length];
                        for (int j = 0; j < MEnvelope_CW.Length; j++)
                        {
                            if (MEnvelope_CW[j][2] != "천창있음")
                            {
                                for (int k = 0; k < CW_Type.Length; k++)
                                {
                                    if (MEnvelope_CW[j][4] == CW_Type[k][1])
                                    { AreaSum_ConstructionCW[k] += Convert.ToDouble(MEnvelope_CW[j][1]); }
                                }
                            }
                        }

                        MaxSum_CW = AreaSum_ConstructionCW.Max(); //커튼월창 구조체 타입별로 면적 합계 중 가장 큰 값
                        int index2 = 0;
                        for (int k = 0; k < CW_Type.Length; k++)
                        {
                            if (AreaSum_ConstructionCW[k] == AreaSum_ConstructionCW.Max())
                            { index2 = k; }  //창호 구조체 타입별로 면적 합계 중 가장 큰 값의 인덱스
                        }
                        if (CW_Type.Length > 0)
                        {
                            MainType_CW_Value = Program.DB.getValue(DB.type.ProjDB, "ConstructionCW", "빛투과율, 고정유리종류, 번호, 유리부분유리면적비", "번호 = '" + CW_Type[index2][1] + "'");
                            if (MainType_CW_Value.Length > 0)
                            {
                                MainType_CW = MainType_CW_Value[0][2].ToString(); // 커튼월창 구조체 타입별로 면적 합계 중 가장 큰 값의 구조체 유형  
                                MainType_ID = CW_Type[index2][2];

                                Main_WinCW = CW_Type[index2][0];
                                f_τD65_SNA = Convert.ToDouble(MainType_CW_Value[0][0]);
                                Main_glass = MainType_CW_Value[0][1];


                                //창호랑 커튼월 다 구해놓고 둘 중 큰거 판별 
                                if (MaxSum_CW > MaxSum_Win)
                                {
                                    Main_WinCW = CW_Type[index2][0];
                                    MainType_ID = CW_Type[index2][2];
                                    f_τD65_SNA = Convert.ToDouble(MainType_CW_Value[0][0]);
                                    Main_glass = MainType_CW_Value[0][1];
                                    K1 = Convert.ToDouble(MainType_CW_Value[0][3]);
                                }
                                else
                                {
                                    Main_WinCW = MainType_Win[0][0];
                                    MainType_ID = Win_Type[index][2];
                                    f_τD65_SNA = Convert.ToDouble(MainType_Win_Value[0][0]);
                                    Main_glass = MainType_Win_Value[0][1];
                                    K1 = Convert.ToDouble(MainType_Win[0][1]);
                                }
                            }
                        }
                    }
                }
            }
        }

        private void Calc_Roof_Data()
        {
            //rooflight1 가져오기 
            ////////////////////////////////////////////////////////주향 기준 실너비(그 향의 벽체길이) 깊이 계산하기////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            String[][] Wall_Length = Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope_3D", "벽체길이", "존 = '" + ZoneNum + "' AND 방위 = '" + roof_di + "' And 외피유형 = '외벽'");

            Wr = 0;
            if (Wall_Length.Length > 0)
            {
                for (int j = 0; j < Wall_Length.Length; j++)
                {
                    if (Wall_Length[j][0] == "") { Wr += 1; }
                    else
                    {
                        Wr += Convert.ToDouble(Wall_Length[j][0]);
                    }
                }
            }

            Lr = A / Wr;


            ////////////////////////////////////////////////////////주 향의 창호 커튼월 높이, 너비 계산하기////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // String[][] MEnvelope_Wina = Program.DB.querySQL(DB.type.ProjDB, " select b.창호높이,b.창호면적 FROM ZoneEnvelope_3D AS a INNER JOIN SubWindow AS b ON a.구조체 = b.명칭");


            //주향 창호 및 커튼월 정보 불러오기
            String[][] MEnvelope_Win = Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope_3D", "번호, 면적, 구조체,구조체번호", "존 = '" + ZoneNum + "' AND 방위 = '" + roof_di + "' And 외피유형 = '창호' And 천창유무 = '천창있음'");
            String[][] MEnvelope_CW = Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope_3D", "번호, 면적, 구조체,구조체번호", "존 = '" + ZoneNum + "' AND 방위 = '" + roof_di + "' And 외피유형 = '커튼월창' And 커튼월부위 = '유리부분' And 천창유무 = '천창있음'");

            ////////////////////////////////////////////////////////주향 창호 OR 커튼월 유형 및 빛 투과율 찾기 (세가지 케이스)////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            //창호 구조체 타입 찾기 > SubWindow(사이즈별)임 MainWindow(창호 자재 조합유형별)아님 
            String[][] Win_Type = Program.DB.getValue_SameCheck(DB.type.ProjDB, "ZoneEnvelope_3D", "구조체,구조체번호,번호", "존 = '" + ZoneNum + "' And 방위 = '" + roof_di + "' And 외피유형 = '창호' And 천창유무 = '천창있음'");
            //커튼월창 구조체 타입 찾기 
            String[][] CW_Type = Program.DB.getValue_SameCheck(DB.type.ProjDB, "ZoneEnvelope_3D", "구조체,구조체번호,번호", "존 = '" + ZoneNum + "' And 방위 = '" + roof_di + "' And 외피유형 = '커튼월창' And 천창유무 = '천창있음'");

            double AreaSum_Wins = 0;
            double[] AreaSum_ConstructionWin = new double[Win_Type.Length];
            double MaxSum_Win;
            int index;
            String MainType_SubWin;
            String MainType_ID;
            String[][] MainType_Win;

            String[][] MainType_Win_Value;

            double MaxSum_CW;
            String[][] MainType_CW_Value;
            String MainType_CW;


            if (CW_Type.Length == 0)
            {
                //창호 구조체 타입별로 면적 합계 구하기

                for (int j = 0; j < MEnvelope_Win.Length; j++)
                {
                    if (Win_Type.Length > 0 && MEnvelope_Win.Length > 0)
                    {
                        for (int k = 0; k < Win_Type.Length; k++)
                        {
                            if (MEnvelope_Win[j][3] == Win_Type[k][0])
                            { AreaSum_ConstructionWin[k] += Convert.ToDouble(MEnvelope_Win[j][1]); }
                        }
                    }
                }
                MaxSum_Win = AreaSum_ConstructionWin.Max(); //창호 구조체 타입별로 면적 합계 중 가장 큰 값
                index = 0;
                for (int k = 0; k < Win_Type.Length; k++)
                {
                    if (AreaSum_ConstructionWin[k] == AreaSum_ConstructionWin.Max())
                    { index = k; }  //창호 구조체 타입별로 면적 합계 중 가장 큰 값의 인덱스
                }

                MainType_SubWin = Win_Type[index][1]; // 창호 구조체 타입별로 면적 합계 중 가장 큰 값의 구조체 유형 > SubWindow(사이즈별)임 MainWindow(창호 자재 조합유형별)아님 
                MainType_ID = Win_Type[index][2];/////////////////////////// 창호 구조체 타입별로 면적 합계 중 가장 큰 값의 구조체 ID

                //찾은 SubWindow정보에서 MainWindow 찾기 
                MainType_Win = Program.DB.getValue(DB.type.ProjDB, "SubWindow", "상위창호번호, 유리면적비", "번호 = '" + MainType_SubWin + "'");

                //찾은 주 창호 유형의 빛투과율 찾기 
                MainType_Win_Value = Program.DB.getValue(DB.type.ProjDB, "ConstructionWindow", "빛투과율, 유리종류", "번호 = '" + MainType_Win[0][0].ToString() + "'");

                if (MainType_Win.Length > 0 && MainType_Win_Value.Length > 0)
                {
                    Main_WinCW = MainType_Win[0][0];
                    r_τD65_SNA = Convert.ToDouble(MainType_Win_Value[0][0]);
                    Main_glass = MainType_Win_Value[0][1];
                    Kobl_1 = Convert.ToDouble(MainType_Win[0][1]);
                }
            }
            else if (Win_Type.Length == 0)
            {
                //커튼월창 구조체 타입별로 면적 합계 구하기
                double[] AreaSum_ConstructionCW = new double[CW_Type.Length];
                if (MEnvelope_CW.Length > 0 && CW_Type.Length > 0)
                {
                    for (int j = 0; j < MEnvelope_CW.Length; j++)
                    {
                        for (int k = 0; k < CW_Type.Length; k++)
                        {
                            if (MEnvelope_CW[j][3] == CW_Type[k][1])
                            { AreaSum_ConstructionCW[k] += Convert.ToDouble(MEnvelope_CW[j][1]); }
                        }
                    }
                }

                MaxSum_CW = AreaSum_ConstructionCW.Max(); //커튼월창 구조체 타입별로 면적 합계 중 가장 큰 값
                index = 0;
                for (int k = 0; k < CW_Type.Length; k++)
                {
                    if (AreaSum_ConstructionCW[k] == AreaSum_ConstructionCW.Max())
                    { index = k; }  //창호 구조체 타입별로 면적 합계 중 가장 큰 값의 인덱스
                }
                if (CW_Type.Length > 0)
                {
                    MainType_CW_Value = Program.DB.getValue(DB.type.ProjDB, "ConstructionCW", "빛투과율, 고정유리종류, 번호, 유리부분유리면적비", "번호 = '" + CW_Type[index][1] + "'");
                    if (MainType_CW_Value.Length > 0)
                    {
                        MainType_CW = MainType_CW_Value[0][2].ToString(); // 커튼월창 구조체 타입별로 면적 합계 중 가장 큰 값의 구조체 유형  
                        MainType_ID = CW_Type[index][2];

                        Main_WinCW = CW_Type[index][0];
                        r_τD65_SNA = Convert.ToDouble(MainType_CW_Value[0][0]);
                        Main_glass = MainType_CW_Value[0][1];
                        Kobl_1 = Convert.ToDouble(MainType_CW_Value[0][3]);
                    }
                }
            }
            else
            {
                //창호 구조체 타입별로 면적 합계 구하기
                if (MEnvelope_Win.Length > 0)
                {
                    for (int j = 0; j < MEnvelope_Win.Length; j++)
                    {
                        for (int k = 0; k < Win_Type.Length; k++)
                        {
                            if (MEnvelope_Win[j][3] == Win_Type[k][0])
                            { AreaSum_ConstructionWin[k] += Convert.ToDouble(MEnvelope_Win[j][1]); }
                        }
                    }
                }
                MaxSum_Win = AreaSum_ConstructionWin.Max(); //창호 구조체 타입별로 면적 합계 중 가장 큰 값
                index = 0;
                for (int k = 0; k < Win_Type.Length; k++)
                {
                    if (AreaSum_ConstructionWin[k] == AreaSum_ConstructionWin.Max())
                    { index = k; }  //창호 구조체 타입별로 면적 합계 중 가장 큰 값의 인덱스
                }

                if (Win_Type.Length > 0)
                {
                    MainType_SubWin = Win_Type[index][1]; // 창호 구조체 타입별로 면적 합계 중 가장 큰 값의 구조체 유형 > SubWindow(사이즈별)임 MainWindow(창호 자재 조합유형별)아님 
                    MainType_ID = Win_Type[index][2];/////////////////////////// 창호 구조체 타입별로 면적 합계 중 가장 큰 값의 구조체 ID

                    //찾은 SubWindow정보에서 MainWindow 찾기 
                    MainType_Win = Program.DB.getValue(DB.type.ProjDB, "SubWindow", "상위창호번호, 유리면적비", "번호 = '" + MainType_SubWin + "'");
                    if (MainType_Win.Length > 0)
                    {
                        //찾은 주 창호 유형의 빛투과율 찾기 
                        MainType_Win_Value = Program.DB.getValue(DB.type.ProjDB, "ConstructionWindow", "빛투과율, 유리종류", "번호 = '" + MainType_Win[0][0].ToString() + "'");

                        //커튼월창 구조체 타입별로 면적 합계 구하기
                        double[] AreaSum_ConstructionCW = new double[CW_Type.Length];
                        for (int j = 0; j < MEnvelope_CW.Length; j++)
                        {
                            for (int k = 0; k < CW_Type.Length; k++)
                            {
                                if (MEnvelope_CW[j][3] == CW_Type[k][1])
                                { AreaSum_ConstructionCW[k] += Convert.ToDouble(MEnvelope_CW[j][1]); }
                            }
                        }

                        MaxSum_CW = AreaSum_ConstructionCW.Max(); //커튼월창 구조체 타입별로 면적 합계 중 가장 큰 값
                        index = 0;
                        for (int k = 0; k < CW_Type.Length; k++)
                        {
                            if (AreaSum_ConstructionCW[k] == AreaSum_ConstructionCW.Max())
                            { index = k; }  //창호 구조체 타입별로 면적 합계 중 가장 큰 값의 인덱스
                        }
                        if (CW_Type.Length > 0)
                        {
                            MainType_CW_Value = Program.DB.getValue(DB.type.ProjDB, "ConstructionCW", "빛투과율, 고정유리종류, 번호, 유리부분유리면적비", "번호 = '" + CW_Type[index][1] + "'");
                            if (MainType_CW_Value.Length > 0)
                            {
                                MainType_CW = MainType_CW_Value[0][2].ToString(); // 커튼월창 구조체 타입별로 면적 합계 중 가장 큰 값의 구조체 유형  
                                MainType_ID = CW_Type[index][2];

                                Main_WinCW = CW_Type[index][0];

                                r_τD65_SNA = Convert.ToDouble(MainType_CW_Value[0][0]);
                                Main_glass = MainType_CW_Value[0][1];

                                if (MaxSum_CW > MaxSum_Win)
                                {
                                    Main_WinCW = CW_Type[index][0];
                                    MainType_ID = CW_Type[index][2];
                                    r_τD65_SNA = Convert.ToDouble(MainType_CW_Value[0][0]);
                                    Main_glass = MainType_CW_Value[0][1];
                                    Kobl_1 = Convert.ToDouble(MainType_CW_Value[0][3]);

                                }
                                else
                                {
                                    if (MainType_Win.Length > 0)
                                    {
                                        Main_WinCW = MainType_Win[0][0];
                                        MainType_ID = Win_Type[index][2];
                                        if (MainType_Win_Value.Length > 0)
                                        {
                                            r_τD65_SNA = Convert.ToDouble(MainType_Win_Value[0][0]);
                                            Main_glass = MainType_Win_Value[0][1];
                                            Kobl_1 = Convert.ToDouble(MainType_Win[0][1]);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void Calc_None_Data()
        {
            ////////////////////////////////////////////////////////주향 기준 실너비(그 향의 벽체길이) 깊이 계산하기////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            String[][] Wall_Length = Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope_3D", "벽체길이", "존 = '" + ZoneNum + "' AND 방위 = '" + none_di + "' And 외피유형 = '외벽'");

            if (Wall_Length.Length > 0)
            {
                Wr = 0;
                for (int j = 0; j < Wall_Length.Length; j++)
                {
                    Wr += Convert.ToDouble(Wall_Length[j][0]);
                }
                Lr = A / Wr;
            }

        }

        private void CheckNaturalType()
        {
            NaturalCheck();
            NaturalType_case1();
            WindowInfo();
            side_active();
        }

    }

}
