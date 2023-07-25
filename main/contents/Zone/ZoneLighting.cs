using main.contentslist;
using main.subcontents.ZoneLighting;
using System.Collections.Generic;
using System.Security.Policy;

namespace main.contents
{
    public partial class ZoneLighting : Form
    {

        //변수
        String ZoneNum, ZoneName, Layer;
        //존 정보(가져오는 값)
        double Em, KA, FA;
        double Wr, Lr, A, hR, hm, hLi, hTa, K;

        string facade_di, Main_WinCW, facade_shade, facade_dimming, Usage;
        double Zone_f_Aca, Zone_f_a, Zone_f_b, Zone_f_AD, f_τD65_SNA, K1, K2, K3, γSh_lsh, γSh_hA, γSh_vA, f_τD65_SA;
        String Main_glass;
        public string roof_di, roof_glass, roof_shade, roof_dimming;
        public double r_Aca, r_aD, r_bD, r_AD, γF, γW, As, Bs, hs, hw, hg, Da, r_τD65_SNA, r_τD65_SA, Kobl_1, Kobl_2, Kobl_3;


        //테이블에서 가져오는 값
        double UFF; //LightMethod에 따라 매칭값
        double Foc; //ControlType에 따라 매칭값
        double Pj_lx; //매칭값


        //ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ
        //선택해야하는 정보(save 되어야 함)

        //RenewDB저장값
        public double Reneweff, U_RenewLenght1, U_RenewLenght2, U_RenewA;
        public string D_RenewLenght1, D_RenewLenght2, D_RenewA;
        public string RenewNum, RenewName, RenewName2, RenewA;

        //LightDB저장값
        string LightNumber, LightType, LightType2, LightConverter, LightFi, LightW;



        double LightFL;
        string D_LightFi, D_LightPi;
        double U_LightFi, U_LightPi, U_Pn;

        //선택값
        string Method, control, dimming;
        //계산값
        double Pj;
        double Fo, Fo1, Fo2, Fo3, Fc;
        double N; //조명 설치 개수

        //차양선택값
        string ShadeType;

        //자연채광 선택값
        string NaturalType;

        //별도창
        string facade, doubleskinglass, atriumglass;
        double zoneW, zoneL, zoneH, zoneGlassLight;

        string roof;
        double zoneRoofAngle1, zoneRoofAngle2, zoneRoofLenght1, zoneRoofLenght2, zoneRoofLenght3;



        //ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ



        public ZoneLighting()
        {

            // 화면 뜨자마자 있었으면 하는거 전부 콤보박스로 몰아 넣기 
            InitializeComponent();

            //조명 이미지 로드 
            string[][] Image = Program.DB.getValue(DB.type.BaseDB_HCneed, "메뉴아이콘", "하위메뉴아이콘", "하위메뉴명 = '존 조명정보'");
            Icon_pictureBox.Load(Program.gPath + Image[0][0]);
            Icon_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;

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
            Program.UTIL.FillComboBox(DB.type.BaseDB_Lighting, NaturalType_comboBox, "조명", "자연채광 유형1", "1");


            //차양종류 콤보박스 
            Shade_comboBox.Items.Add("없음");
            Shade_comboBox.Items.Add("외부_베네치안");

        }


        private void GeneralPanel_Paint(object sender, PaintEventArgs e)
        {
            Panel p = (Panel)sender;
            ControlPaint.DrawBorder(e.Graphics, p.DisplayRectangle, Color.FromArgb(153, 180, 209), ButtonBorderStyle.Solid);
        }

        private void LightDB_button_Click(object sender, EventArgs e)
        {

            LightingDB lightingdb_form = new LightingDB();
            DialogResult result = lightingdb_form.ShowDialog();
            if (result == DialogResult.OK)
            {
                LightNumber = lightingdb_form.Select_Light[0];
                LightType = lightingdb_form.Select_Light[2];
                LightType2 = lightingdb_form.Select_Light[3];
                LightConverter = lightingdb_form.Select_Light[5];
                LightFi = lightingdb_form.Select_Light[6];
                LightW = lightingdb_form.Select_Light[7];
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
            Calc_Fc();
            //Load_Lamp_image();

        }


        private void LightMethod_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (LightMethod_comboBox.SelectedItem != null)
            {
                Method = LightMethod_comboBox.SelectedItem.ToString();
                Match_Pjlx();
                Calc_Pj();
            }
        }


        private void ControlType_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ControlType_comboBox.SelectedItem != null)
            {
                control = ControlType_comboBox.SelectedItem.ToString();

                Match_Foc();
                Calc_Fo();
                Calc_Fc();
            }
        }



        private void DimmingType_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DimmingType_comboBox.SelectedItem != null)
            {
                dimming = DimmingType_comboBox.SelectedItem.ToString();
            }
        }



        private void NaturalType_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (NaturalType_comboBox.SelectedItem != null)
            {
                NaturalType = NaturalType_comboBox.SelectedItem.ToString();
                Load_NaturalType_image(NaturalType);
                NaturalType_case1();
                WindowInfo2();
                Load_AD2_image();
                Load_Shade_image();
            }
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
                    this.doubleskinglass = naturallighting_facade.doubleskinglasstype;
                    this.atriumglass = naturallighting_facade.atriumglasstype;
                    this.zoneW = naturallighting_facade.W;
                    this.zoneL = naturallighting_facade.L;
                    this.zoneH = naturallighting_facade.H;
                    this.zoneGlassLight = naturallighting_facade.Tao;

                    Load_NaturalType2_image();
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
            if (RenewType_textBox.Text != null)
            { Load_RenewType_image(RenewName); }
            else
            {
                Main_pictureBox2.Visible = false;
            }

        }


        private void Natural_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            NaturalCheck();
            Load_NaturalType_image(NaturalType);
            NaturalType_case1();
            WindowInfo();
            WindowInfo2();
            Load_AD_image();
            side_active();
        }


        private void Renew_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            RenewCheck();
            RenewInfo();
            if (Renew_checkBox.Checked && RenewType_textBox.Text != null)
            {
                Load_RenewType_image(RenewName);
            }
            else
            { Main_pictureBox2.Image = null; }

        }


        //차양 유무에 따른 그림 변화
        private void Shade_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Shade_comboBox.SelectedItem != null)
            {
                ShadeType = Shade_comboBox.SelectedItem.ToString();
                if (ShadeType != null)
                {
                    Load_Shade_image();
                }
            }
        }



        //-----------------------------------------------------------------------------------------------------------------------------------------------------------



        public void Match_Pjlx()
        {
            try
            {
                String[][] value = Program.DB.getValue(DB.type.BaseDB_Lighting, "조명_럭스당조명밀도", "값,UFF", "조명방식='" + Method + "' AND K = '" + K + "'");
                Pj_lx = Convert.ToDouble(value[0][0]);
                UFF = Convert.ToDouble(value[0][1]);
            }
            catch { }

        }

        public void Match_Foc()
        {
            String[][] value = Program.DB.getValue(DB.type.BaseDB_Lighting, "조명_조명제어", "Foc", "제어종류 = '" + control + "'");
            Foc = Convert.ToDouble(value[0][0]);
        }


        public void Calc_Pj()
        {
            if (LightType != null)
            {
                if (D_LightFi == "VAR")
                {
                    Pj = Em * FA * KA * LightFL * Foc * (0.8 / 0.67) * Pj_lx;
                }

                else
                {

                    if (LightType.Contains("LED"))
                    {
                        N = (Em * A) / (U_LightFi * UFF * 0.67 * 1.1);
                        U_Pn = U_LightPi * N;
                        Pj = U_Pn / A;

                    }

                    else
                    {
                        N = (Em * A) / (U_LightFi * UFF * 0.67);
                        U_Pn = U_LightPi * N;
                        Pj = U_Pn / A;
                    }
                }
            }

            Pj_textbox.Text = string.Format("{0:F2}", Pj);
        }

        public void Calc_Fo()
        {
            Fo1 = (1 - (1 - Foc) * FA / 0.2);
            Fo2 = (Foc + 0.2 - FA);
            Fo3 = ((7 - 10 * Foc) * (FA - 1));

            double[] Fo_list = { Fo1, Fo2, Fo3 };
            double max = Fo1;

            for (int i = 1; i < Fo_list.Length; i++)
            {
                if (max < Fo_list[i])
                {
                    max = Fo_list[i];
                }
            }
            Fo = max;

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


            }

            fc_textBox.Text = string.Format("{0:F2}", Fc * Fo);
        }


        //조명 정보 텍스트 박스에 넣기 
        private void LightInfo()
        {



            LightType_textBox.Text = LightType;
            FL_textBox.Text = string.Format("{0:F2}", LightFL);
            L1_textBox.Text = LightType;
            L2_textBox.Text = LightType2;
            L4_textBox.Text = LightConverter;
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


            L8_textBox.Text = LightFL.ToString();

            label5.ForeColor = Color.Black;
            label6.ForeColor = Color.Black;

        }


        //주창 정보 보이기
        private void WindowInfo()
        {
            shade2_textBox.Text = f_τD65_SA.ToString();

            if (Natural_checkBox.Checked == true)
            {
                shade1_label.Visible = true;
                Shade2_label.Visible = true;
                Shade3_label.Visible = true;
                Shade4_label.Visible = true;
                Shade7_label.Visible = true;

                Window1_textBox.Visible = true;
                label7.Visible = true;
                WindowA_textBox.Visible = true;
                Window_glass_textBox.Visible = true;
                Window_Tao_textBox.Visible = true;
                Window_glass_label.Visible = true;
                Window_Tao_label.Visible = true;

                Shade_comboBox.Visible = true;
                shade2_textBox.Visible = true;

                label2.Visible = true;
                label3.Visible = true;
            }

            else if (Natural_checkBox.Checked == false)
            {
                shade1_label.Visible = false;
                Shade2_label.Visible = false;
                Shade3_label.Visible = false;
                Shade4_label.Visible = false;
                Shade7_label.Visible = false;

                Window1_textBox.Visible = false;
                label7.Visible = false;
                WindowA_textBox.Visible = false;
                Window_glass_textBox.Visible = false;
                Window_Tao_textBox.Visible = false;
                Window_glass_label.Visible = false;
                Window_Tao_label.Visible = false;

                Shade_comboBox.Visible = false;
                shade2_textBox.Visible = false;

                label2.Visible = false;
                label3.Visible = false;

            }

        }


        // 주창 정보 들어오기 

        private void WindowInfo2()
        {
            if (NaturalType == "파사드")
            {
                Window1_textBox.Text = facade_di;
                WindowA_textBox.Text = string.Format("{0:F2}", Zone_f_Aca);
                Window_glass_textBox.Text = Main_glass;
                Window_Tao_textBox.Text = string.Format("{0:F3}", f_τD65_SNA);

            }

            else if (NaturalType == "천창")
            {
                Window1_textBox.Text = roof_di;
                WindowA_textBox.Text = string.Format("{0:F2}", r_Aca);
                Window_glass_textBox.Text = Main_glass;
                Window_Tao_textBox.Text = string.Format("{0:F3}", f_τD65_SNA);
            }

            else;
        }


        //집광채광 정보 들어오기 
        private void RenewInfo()
        {
            if (Renew_checkBox.Checked)
            {
                R1_label.Visible = true;
                R2_label.Visible = true;
                R3_label.Visible = true;
                label8.Visible = true;
                label9.Visible = true;
                R1_textBox.Visible = true;
                R2_textBox.Visible = true;
                R3_textBox.Visible = true;

            }

            else
            {
                R1_label.Visible = false;
                R2_label.Visible = false;
                R3_label.Visible = false;
                label8.Visible = false;
                label9.Visible = false;
                R1_textBox.Visible = false;
                R2_textBox.Visible = false;
                R3_textBox.Visible = false;
            }
        }


        private void Load_NaturalType_image(String Type)
        {
            if (Natural_checkBox.Checked == false)
            {
                string[][] Image = Program.DB.getValue(DB.type.BaseDB_Lighting, "조명_자연채광대분류이미지", "이미지", "자연채광대분류 = '" + "해당없음" + "'");
                Main_pictureBox.Load(Program.gPath + Image[0][0]);
            }

            else
            {
                string[][] Image = Program.DB.getValue(DB.type.BaseDB_Lighting, "조명_자연채광대분류이미지", "이미지", "자연채광대분류 = '" + Type + "'");
                Main_pictureBox.Load(Program.gPath + Image[0][0]);
            }

            Main_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            this.Main_pictureBox.Controls.Add(this.Main_pictureBox2);
            this.Main_pictureBox2.Controls.Add(this.Main_pictureBox3);

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

        }



        //자연채광 체크박스 활성화 비활성화
        public void NaturalCheck()
        {
            if (Natural_checkBox.Checked)
            {
                NaturalType_comboBox.Visible = true;
                NaturalDB_button.Visible = true;
                Direction_label.Visible = true;
                Aca_label.Visible = true;
                direction_textBox.Visible = true;
                Aca_textBox.Visible = true;
                Acam2_label.Visible = true;
                Main_pictureBox3.Visible = true;

            }

            else
            {

                NaturalType_comboBox.Visible = false;
                NaturalDB_button.Visible = false;
                Direction_label.Visible = false;
                Aca_label.Visible = false;
                direction_textBox.Visible = false;
                Aca_textBox.Visible = false;
                Acam2_label.Visible = false;
                Main_pictureBox3.Visible = false;


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
            }

            else
            {

                RenewDB_button.Visible = false;
                RenewDi_comboBox.Visible = false;
                Slope_comboBox.Visible = false;
                RenewDi_label.Visible = false;
                Slope_label.Visible = false;
                RenewType_textBox.Visible = false;
            }

        }


        private void Load_RenewType_image(String Type)
        {
            //집광 채광 종류가 null이 아닐 경우 그림에 들어가도록

            string[][] Image = Program.DB.getValue(DB.type.BaseDB_Lighting, "조명_집광채광이미지", "이미지", "");
            Main_pictureBox2.Visible = true;
            Main_pictureBox2.Load(Program.gPath + Image[0][0]);
            Main_pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;

            this.Main_pictureBox2.Location = new Point(0, 0);
            this.Main_pictureBox2.BackColor = Color.Transparent;

        }




        public void NaturalType_case1()
        {


            // 자연채광 유형에 따른 해당 값 
            if (NaturalType == "파사드")
            {
                direction_textBox.Text = facade_di;
                Aca_textBox.Text = string.Format("{0:F2}", Zone_f_Aca);
            }

            else if (NaturalType == "천창")
            {
                direction_textBox.Text = roof_di;
                Aca_textBox.Text = string.Format("{0:F2}", r_Aca);
            }

            else
            {
                direction_textBox.Text = "";
                Aca_textBox.Text = "";
            }

        }


        //차양 유무에 따른 그림 로드 
        private void Load_Shade_image()
        {

            if (Natural_checkBox.Checked && NaturalType == "파사드" && ShadeType != "없음")
            {
                string[][] Image = Program.DB.getValue(DB.type.BaseDB_Lighting, "조명_차양이미지", "이미지", "차양 = '" + "파사드차양" + "'");
                Main_pictureBox3.Load(Program.gPath + Image[0][0]);
                Main_pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
                this.Main_pictureBox3.Location = new Point(0, 0);
                this.Main_pictureBox3.BackColor = Color.Transparent;

            }

            else if (Natural_checkBox.Checked && NaturalType == "파사드" && ShadeType == "없음")
            {
                string[][] Image = Program.DB.getValue(DB.type.BaseDB_Lighting, "조명_차양이미지", "이미지", "차양 = '" + "파사드" + "'");
                Main_pictureBox3.Load(Program.gPath + Image[0][0]);
                Main_pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
                this.Main_pictureBox3.Location = new Point(0, 0);
                this.Main_pictureBox3.BackColor = Color.Transparent;
            }

            else if (Natural_checkBox.Checked && NaturalType == "천창" && ShadeType != "없음")
            {
                string[][] Image = Program.DB.getValue(DB.type.BaseDB_Lighting, "조명_차양이미지", "이미지", "차양 = '" + "천창차양" + "'");
                Main_pictureBox3.Load(Program.gPath + Image[0][0]);
                Main_pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
                this.Main_pictureBox3.Location = new Point(0, 0);
                this.Main_pictureBox3.BackColor = Color.Transparent;
            }

            else if (Natural_checkBox.Checked && NaturalType == "천창" && ShadeType == "없음")
            {
                string[][] Image = Program.DB.getValue(DB.type.BaseDB_Lighting, "조명_차양이미지", "이미지", "차양 = '" + "천창" + "'");
                Main_pictureBox3.Load(Program.gPath + Image[0][0]);
                Main_pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
                this.Main_pictureBox3.Location = new Point(0, 0);
                this.Main_pictureBox3.BackColor = Color.Transparent;
            }

            else;

        }




        //대분류에 따른 변화 (콤보박스에 걸기)
        private void Load_AD_image()
        {


            if (Natural_checkBox.Checked)
            {
                type_pictureBox.Visible = true;
                string[][] Image = Program.DB.getValue(DB.type.BaseDB_Lighting, "조명_주광면적이미지", "이미지", "주광면적 = '" + "일반 파사드" + "'");
                type_pictureBox.Load(Program.gPath + Image[0][0]);
                type_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;

            }

            else
            {
                type_pictureBox.Visible = false;
            }
        }


        //상세 선택에 따른 변화 (체크박스에 걸기)
        private void Load_AD2_image()
        {
            if (Natural_checkBox.Checked && NaturalType == "파사드" && facade == "일반 파사드")
            {
                string[][] Image = Program.DB.getValue(DB.type.BaseDB_Lighting, "조명_주광면적이미지", "이미지", "주광면적 = '" + "일반 파사드" + "'");
                type_pictureBox.Load(Program.gPath + Image[0][0]);
                type_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;

            }

            else if (Natural_checkBox.Checked && NaturalType == "파사드" && facade == "이중외피")
            {
                string[][] Image = Program.DB.getValue(DB.type.BaseDB_Lighting, "조명_주광면적이미지", "이미지", "주광면적 = '" + "이중외피" + "'");
                type_pictureBox.Load(Program.gPath + Image[0][0]);
                type_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            }

            else if (Natural_checkBox.Checked && NaturalType == "파사드" && facade == "중정")
            {
                string[][] Image = Program.DB.getValue(DB.type.BaseDB_Lighting, "조명_주광면적이미지", "이미지", "주광면적 = '" + "중정" + "'");
                type_pictureBox.Load(Program.gPath + Image[0][0]);
                type_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            }

            else if (Natural_checkBox.Checked && NaturalType == "파사드" && facade == "아트리움")
            {
                string[][] Image = Program.DB.getValue(DB.type.BaseDB_Lighting, "조명_주광면적이미지", "이미지", "주광면적 = '" + "아트리움" + "'");
                type_pictureBox.Load(Program.gPath + Image[0][0]);
                type_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            }

            else if (Natural_checkBox.Checked && NaturalType == "천창")
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
            if (Natural_checkBox.Checked)
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
            f.load_List();
            return true;
        }

        private void Save_button_Click(object sender, EventArgs e)
        {
            if (LightType == null)
            {
                MessageBox.Show("조명 종류를 선택하세요.");
            }
            else if (Natural_checkBox.Checked == true)
            {
                if (NaturalType == null)
                {
                    MessageBox.Show("자연채광 정보를 입력하세요.");
                }
                else
                {
                    Save();
                }
            }
            else if (Renew_checkBox.Checked == true)
            {
                if (RenewNum == null)
                {
                    MessageBox.Show("집광채광을 선택하세요.");
                }
                else
                {
                    Save();
                }
            }
            else
            {
                Save();
            }

        }



        private void Save()
        {
            Program.DB.setValue(DB.type.ProjDB, "ZoneLighting_form", "번호,조명방식,제어방식,디밍유형,조명개수,조명밀도,재실계수,재실계수1,재실계수2,재실계수3,조도제어계수," +
                "조명번호, 등기구명칭, 램프유형, 컨버터_안정기, 광속, 소비전력, 조명계수," +
                "자연채광체크,집광채광체크",
                "'" + Num_textBox.Text + "','" + Method + "','" + control + "','" + dimming + "','" + N.ToString() + "','" + Pj.ToString() + "','" +
                Fo.ToString() + "','" + Fo1.ToString() + "','" + Fo2.ToString() + "','" + Fo3.ToString() + "','" + Fc.ToString() + "','" +
                LightNumber + "','" + LightType + "','" + LightType2 + "','" + LightConverter + "','" + LightFi + "','" + LightW + "','" + LightFL.ToString() + "','" +
                Natural_checkBox.Checked.ToString() + "','" + Renew_checkBox.Checked.ToString()
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
                Program.DB.setValue(DB.type.ProjDB, "ZoneLighting_form", "번호,사용자광속, 사용자소비전력,사용자예상전력",
                "'" + Num_textBox.Text + "','" +
                U_LightFi.ToString() + "','" + U_LightPi.ToString() + "','" + U_Pn.ToString()
              + "'", "번호");
            }


            if (Natural_checkBox.Checked == true)
            {
                Program.DB.setValue(DB.type.ProjDB, "ZoneLighting_form", "번호,자연채광유형,파사드,이중외피유리,아트리움유리,파사드유리빛투과율,파사드너비,파사드길이,파사드높이,천창,천창유리각,천창수평측면각,천창장변부길이,천창단변부길이,천창수평상부높이,차양",
                "'" + Num_textBox.Text + "','" +
               NaturalType + "','" + facade + "','" + doubleskinglass + "','" + atriumglass + "','" + zoneGlassLight + "','" + zoneW + "','" + zoneL + "','" + zoneH + "','" + roof + "','" + zoneRoofAngle1 + "','" + zoneRoofAngle2 + "','" + zoneRoofLenght1 + "','" + zoneRoofLenght2 + "','" + zoneRoofLenght3 + "','" + ShadeType
                + "'", "번호");
            }
            else { }



            if (Renew_checkBox.Checked == true)
            {
                Program.DB.setValue(DB.type.ProjDB, "ZoneLighting_form", "번호,집광채광번호,집광채광명칭,집광채광종류,집광채광효율,집광채광면적",
               "'" + Num_textBox.Text + "','" +
                 RenewNum + "','" + RenewName + "','" + RenewName2 + "','" + Reneweff.ToString() + "','" + RenewA
               + "'", "번호");

                if (RenewNum.Contains("DL"))
                {
                    Program.DB.setValue(DB.type.ProjDB, "ZoneLighting_form", "번호,표준길이1,표준길이2,표준너비",
              "'" + Num_textBox.Text + "','" +
                D_RenewLenght1 + "','" + D_RenewLenght2 + "','" + D_RenewA
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

            MessageBox.Show(ZoneNum + "[" + ZoneName + "] 정보를 저장하였습니다.");
            this.DialogResult = DialogResult.OK;
            this.Hide();
            Program.getMenuForm().DoLoadForm(33, OnLoadListProc);



        }
        private void reset()
        {
            Num_textBox.Text = "";
            Layer_textBox.Text = "";
            ZoneName_textBox.Text = "";


            FL_textBox.Text = null;
            Pj_textbox.Text = null;
            fc_textBox.Text = null;

            Natural_checkBox.Checked = false;
            direction_textBox.Text = null;
            Aca_textBox.Text = null;

            Renew_checkBox.Checked = false;
            RenewName = null;


            LightType = null;
            LightType2 = null;
            LightConverter = null;
            LightFi = null;
            LightW = null;
            LightFL = 0;


            Window1_textBox.Text = null;
            WindowA_textBox.Text = null;
            shade2_textBox.Text = null;



            R1_textBox.Text = null;
            R2_textBox.Text = null;
            R3_textBox.Text = null;

            A_textBox.Text = null;
            AD_textBox.Text = null;
            bbd_textBox.Text = null;
            aad_textBox.Text = null;
            NA_textBox.Text = null;

        }


        //존 리스트 클릭시 로드
        public void LoadData(String ID)
        {
            reset();
            Load_OtherFormData();

            try
            {
                Num_textBox.Text = ID;

                String[][] Load = Program.DB.getValue(DB.type.ProjDB, "ZoneLighting_form", "번호,조명방식,제어방식,디밍유형,조명개수,조명밀도,재실계수,재실계수1,재실계수2,재실계수3,조도제어계수",
                "번호 = '" + ZoneNum + "'");

                Num_textBox.Text = Load[0][0];

                Method = Load[0][1];
                LightMethod_comboBox.SelectedItem = Method;

                control = Load[0][2];
                ControlType_comboBox.SelectedItem = control;

                dimming = Load[0][3];
                DimmingType_comboBox.SelectedItem = dimming;

                N = Convert.ToDouble(Load[0][4]);

                Pj = Convert.ToDouble(Load[0][5]);
                Pj_textbox.Text = string.Format("{0:F2}", Pj);

                Fo = Convert.ToDouble(Load[0][6]);
                Fo1 = Convert.ToDouble(Load[0][7]);
                Fo2 = Convert.ToDouble(Load[0][8]);
                Fo3 = Convert.ToDouble(Load[0][9]);
                Fc = Convert.ToDouble(Load[0][10]);
                fc_textBox.Text = string.Format("{0:F2}", Fc * Fo);

                Load = Program.DB.getValue(DB.type.ProjDB, "ZoneLighting_form", "자연채광유형,파사드,이중외피유리,아트리움유리,파사드유리빛투과율,파사드너비,파사드길이,파사드높이,천창,천창유리각,천창수평측면각,천창장변부길이,천창단변부길이,천창수평상부높이,차양,자연채광체크",
                "번호 = '" + ZoneNum + "'");

                Natural_checkBox.Checked = Convert.ToBoolean(Load[0][15]);

                if (Natural_checkBox.Checked)
                {
                    NaturalType = Load[0][0];
                    NaturalType_comboBox.SelectedItem = NaturalType;

                    facade = Load[0][1];
                    doubleskinglass = Load[0][2];
                    atriumglass = Load[0][3];
                    zoneGlassLight = Convert.ToDouble(Load[0][4]);
                    zoneW = Convert.ToDouble(Load[0][5]);
                    zoneL = Convert.ToDouble(Load[0][6]);
                    zoneH = Convert.ToDouble(Load[0][7]);

                    roof = Load[0][8];
                    zoneRoofAngle1 = Convert.ToDouble(Load[0][9]);
                    zoneRoofAngle2 = Convert.ToDouble(Load[0][10]);
                    zoneRoofLenght1 = Convert.ToDouble(Load[0][11]);
                    zoneRoofLenght2 = Convert.ToDouble(Load[0][12]);
                    zoneRoofLenght3 = Convert.ToDouble(Load[0][13]);

                    ShadeType = Load[0][14];
                    Shade_comboBox.SelectedItem = ShadeType;


                }


                Load = Program.DB.getValue(DB.type.ProjDB, "ZoneLighting_form", "집광채광번호,집광채광명칭,집광채광종류,집광채광효율,집광채광면적,표준길이1,표준길이2,표준너비,사용자길이1,사용자길이2,사용자면적,집광채광체크",
                "번호 = '" + ZoneNum + "'");

                Renew_checkBox.Checked = Convert.ToBoolean(Load[0][11]);
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
                        D_RenewA = Load[0][7];
                        R1_textBox.Text = RenewName2;
                        R2_textBox.Text = D_RenewA;
                        R3_textBox.Text = Reneweff.ToString();
                    }

                    else
                    {
                        U_RenewLenght1 = Convert.ToDouble(Load[0][8]);
                        U_RenewLenght2 = Convert.ToDouble(Load[0][9]);
                        U_RenewA = Convert.ToDouble(Load[0][10]);
                        R1_textBox.Text = RenewName2;
                        R2_textBox.Text = U_RenewA.ToString();
                        R3_textBox.Text = Reneweff.ToString();
                    }

                }

                Load = Program.DB.getValue(DB.type.ProjDB, "ZoneLighting_form", "조명번호, 등기구명칭, 램프유형, 컨버터_안정기, 광속, 소비전력, 조명계수," +
                "표준광속, 표준소비전력,사용자광속, 사용자소비전력,사용자예상전력",
                "번호 = '" + ZoneNum + "'");


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
                FL_textBox.Text = string.Format("{0:F2}", LightFL);

                //D_LightFi = Load[0][7];
                //D_LightPi = Load[0][8];

                //U_LightFi = Convert.ToDouble(Load[0][9]);
                //U_LightPi = Convert.ToDouble(Load[0][10]);

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

                U_Pn = Convert.ToDouble(Load[0][11]);

                Load_NaturalType_image(NaturalType);

                if (RenewType_textBox.Text != null)
                { Load_RenewType_image(RenewName); }
                else
                {
                    Main_pictureBox2.Visible = false;
                }
                Load_Shade_image();
                Load_AD_image();

            }
            catch { }

            try
            {
                string[][] Image = Program.DB.getValue(DB.type.BaseDB_HCneed, "메뉴아이콘", "하위메뉴아이콘", "하위메뉴명 = '존 조명정보'");
                Icon_pictureBox.Load(Program.gPath + Image[0][0]);
                Icon_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            }
            catch { }


        }

        public void ResetForm(String ID) // 리스트에서 추가 버튼 클릭시 - 뷰 초기화
        {
            Num_textBox.Text = ID;
            ZoneNum = ID;
            Load_OtherFormData();
        }

        //존명칭 로드
        private void ZoneLighting_VisibleChanged(object sender, EventArgs e)
        {
            if (main.MainContents.currentForm == main.MainContents.FormID.ZoneLighting)
            {
                String ID = main.MainContents.selID;
                ID = ID.Substring(19, 10);
                Num_textBox.Text = ID;
                ZoneNum = ID;
                LoadData(ZoneNum);
            }
        }

        private void Load_OtherFormData()
        {
            //    double Em, KA, FA;
            //    double Wr, Lr, A, hR, hm, hLi, hTa, K;

            //    string facade_di, glass1, facade_shade, facade_dimming;
            //    double Zone_f_Aca, Zone_f_a, Zone_f_b, Zone_f_AD, f_τD65_SNA, K1, K2, K3, γSh_lsh, γSh_hA, γSh_vA;

            //public string roof_di, roof_glass, roof_shade, roof_dimming;
            //public double r_Aca, r_aD, r_bD, r_AD, γF, γW, As, Bs, hs, hw, hg, Da, r_τD65_SNA, r_τD65_SA, Kobl_1, Kobl_2, Kobl_3;


            try
            {
                //존이름 불러오기
                String[][] Value = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_Form", "존이름,천장고,용도프로필", "존번호 = '" + ZoneNum + "'");

                ZoneName = Value[0][0];
                ZoneName_textBox.Text = ZoneName;

                hR = Convert.ToDouble(Value[0][1]);
                Usage = Value[0][2];
                K = 1.25; //////////////////////추후 계산 식으로 변경해야함

            }
            catch { }

            try
            {
                //층정보 불러오기
                String[][] General_3D = Program.DB.getValue(DB.type.ProjDB, "Zonegeneral_3D", "층,바닥면적", "존번호 = '" + ZoneNum + "'");

                Layer = General_3D[0][0];
                Layer_textBox.Text = Layer;

                A = Convert.ToDouble(General_3D[0][1]); //나중에 순바닥면적으로 고쳐야함 지금은 그냥 바닥면적임
                A_textBox.Text = string.Format("{0:F2}", A);

                //hLi = Convert.ToDouble(General_3D[0][5]); //상인방 높이 3D에서 찾아야함
            }
            catch { }

            //Zonelight profile 가져오기 
            try
            {
                string[][] ValueA = Program.DB.getValue(DB.type.BaseDB_HCneed, "용도프로필", "조도,이용영역계수,조명이용시부재율,작업면높이", "용도명 = '" + Usage + "'");
                Em = Convert.ToDouble(ValueA[0][0]);
                KA = Convert.ToDouble(ValueA[0][1]);
                FA = Convert.ToDouble(ValueA[0][2]);
                hTa = Convert.ToDouble(ValueA[0][3]);
            }
            catch { }


            //facade1 가져오기 
            try
            {
                ////////////////////////////////////////////////////////////////////주 향 찾기//////////////////////////////////////////////////////////////////////////////////////////////////////////
                //존에 있는 모든 창호 및 커튼월 불러와서 주향 찾기 
                String[][] TotalEnvelope_Win = Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope_3D", "번호,면적,방위", "존 = '" + ZoneNum + "' And 외피유형 = '창호'");
                String[][] TotalEnvelope_CW = Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope_3D", "번호,면적,방위", "존 = '" + ZoneNum + "' And 외피유형 = '커튼월창' And 커튼월부위 = '유리부분'");

                double[] AreaSum_Win = new double[9]; //향별 창호 면적합계
                double[] AreaSum_CW = new double[9]; //향별 커튼월 면적합계
                double[] AreaSum_Total = new double[9]; //향별 창호과 커튼월 면적합계

                String[] Direction = { "수평", "남", "남동", "남서", "동", "서", "북서", "북동", "북" };

                for (int j = 0; j < Direction.Length; j++)
                {
                    for (int k = 0; k < TotalEnvelope_Win.Length; k++)
                    {
                        if (TotalEnvelope_Win[k][2] == Direction[j])
                            AreaSum_Win[j] += Convert.ToDouble(TotalEnvelope_Win[k][1]);
                    }
                }

                for (int j = 0; j < Direction.Length; j++)
                {
                    for (int k = 0; k < TotalEnvelope_CW.Length; k++)
                    {
                        if (TotalEnvelope_CW[k][2] == Direction[j])
                            AreaSum_CW[j] += Convert.ToDouble(TotalEnvelope_CW[k][1]);
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

                ////////////////////////////////////////////////////////주향 기준 실너비(그 향의 벽체길이) 깊이 계산하기////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                String[][] Wall_Length = Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope_3D", "벽체길이", "존 = '" + ZoneNum + "' AND 방위 = '" + facade_di + "' And 외피유형 = '외벽'");

                Wr = 0;
                for(int j =0; j <Wall_Length.Length;j++)
                {
                    Wr += Convert.ToDouble(Wall_Length[j][0]);
                }

                Lr = A / Wr;

                ////////////////////////////////////////////////////////주 향의 창호 커튼월 높이, 너비 계산하기////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                // String[][] MEnvelope_Wina = Program.DB.querySQL(DB.type.ProjDB, " select b.창호높이,b.창호면적 FROM ZoneEnvelope_3D AS a INNER JOIN SubWindow AS b ON a.구조체 = b.명칭");


                //주향 창호 및 커튼월 정보 불러오기
                String[][] MEnvelope_Win = Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope_3D", "번호, 면적, 구조체,구조체번호", "존 = '" + ZoneNum + "' AND 방위 = '" + facade_di + "' And 외피유형 = '창호'");
                String[][] MEnvelope_CW = Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope_3D", "번호, 면적, 구조체,구조체번호", "존 = '" + ZoneNum + "' AND 방위 = '" + facade_di + "' And 외피유형 = '커튼월창' And 커튼월부위 = '유리부분'");

                //주향의 창호 면적합계와 면적가중 높이 계산
                double AVG_Height = 0;
                double AVG_Width;
                double[] Height_Wins = new double[MEnvelope_Win.Length]; double[] Area_Wins = new double[MEnvelope_Win.Length];
                int i = -1;
                while (++i < MEnvelope_Win.Length)
                {
                    String[][] SubWindow = Program.DB.getValue(DB.type.ProjDB, "SubWindow", "창호높이,창호면적", "번호 = '" + MEnvelope_Win[i][3].ToString() + "'");
                    Height_Wins[i] = Convert.ToDouble(SubWindow[0][0]);
                    Area_Wins[i] = Convert.ToDouble(SubWindow[0][1]);
                }

                double[] Height_CWs = new double[MEnvelope_CW.Length]; double[] Area_CWs = new double[MEnvelope_CW.Length];
                //주향의 커튼월 면적합계와 면적가중 높이 계산
                i = -1;
                while (++i < MEnvelope_CW.Length)
                {
                    // String[][] SubWindow = Program.DB.getValue(DB.type.ProjDB, "ConstructionCW", "창호높이,창호면적", "번호 = '" + Envelope_Win[i][2] + "'");  <<3D에서 커튼월 유리부분 높이 필요함 
                    Height_CWs[i] = 3.5;
                    Area_CWs[i] = Convert.ToDouble(MEnvelope_CW[i][1]);
                }


                double A_H_Win = 0;
                double A_H_CW = 0;
                i = -1;
                while (++i < MEnvelope_Win.Length)
                {
                    A_H_Win += (Height_Wins[i] * Area_Wins[i]);
                }
                i = -1;
                while (++i < MEnvelope_CW.Length)
                {
                    A_H_CW += (Height_CWs[i] * Area_CWs[i]);
                }

                AVG_Height = (A_H_Win + A_H_CW) / Zone_f_Aca;
                AVG_Width = Zone_f_Aca / AVG_Height;

                ////////////////////////////////////////////////////////////////////////////////////주향 창호 빛투과율 찾기///////////////////////////////////////////////////////////////////////
                //창호 구조체 타입 찾기 > SubWindow(사이즈별)임 MainWindow(창호 자재 조합유형별)아님 
                String[][] Win_Type = Program.DB.getValue_dedupe(DB.type.ProjDB, "ZoneEnvelope_3D", "구조체,구조체번호", "존 = '" + ZoneNum + "' And 방위 = '" + facade_di + "' And 외피유형 = '창호'");

                //창호 구조체 타입별로 면적 합계 구하기
                double AreaSum_Wins = 0;
                double[] AreaSum_ConstructionWin = new double[Win_Type.Length];
                for (int j = 0; j < MEnvelope_Win.Length; j++)
                {
                    for (int k = 0; k < Win_Type.Length; k++)
                    {
                        if (MEnvelope_Win[j][3] == Win_Type[k][1])
                        { AreaSum_ConstructionWin[k] += Convert.ToDouble(MEnvelope_Win[j][1]); }
                    }
                }

                double MaxSum_Win = AreaSum_ConstructionWin.Max(); //창호 구조체 타입별로 면적 합계 중 가장 큰 값
                int index = 0;
                for (int k = 0; k < Win_Type.Length; k++)
                {
                    if (AreaSum_ConstructionWin[k] == AreaSum_ConstructionWin.Max())
                    { index = k; }  //창호 구조체 타입별로 면적 합계 중 가장 큰 값의 인덱스
                }


                String MainType_SubWin = Win_Type[index][1]; // 창호 구조체 타입별로 면적 합계 중 가장 큰 값의 구조체 유형 > SubWindow(사이즈별)임 MainWindow(창호 자재 조합유형별)아님 

                //찾은 SubWindow정보에서 MainWindow 찾기 
                String[][] MainType_Win = Program.DB.getValue(DB.type.ProjDB, "SubWindow", "상위창호번호 ", "번호 = '" + MainType_SubWin + "'");


                //찾은 주 창호 유형의 빛투과율 찾기 
                String[][] MainType_Win_Value = Program.DB.getValue(DB.type.ProjDB, "ConstructionWindow", "빛투과율, 유리종류", "번호 = '" + MainType_Win[0][0].ToString() + "'");




                ////////////////////////////////////////////////////////////////////////////////////주향 커튼월 빛투과율 찾기///////////////////////////////////////////////////////////////////////
                //커튼월창 구조체 타입 찾기 
                String[][] CW_Type = Program.DB.getValue_dedupe(DB.type.ProjDB, "ZoneEnvelope_3D", "구조체,구조체번호", "존 = '" + ZoneNum + "' And 방위 = '" + facade_di + "' And 외피유형 = '커튼월창'");

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

                double MaxSum_CW = AreaSum_ConstructionCW.Max(); //커튼월창 구조체 타입별로 면적 합계 중 가장 큰 값
                index = 0;
                for (int k = 0; k < CW_Type.Length; k++)
                {
                    if (AreaSum_ConstructionCW[k] == AreaSum_ConstructionCW.Max())
                    { index = k; }  //창호 구조체 타입별로 면적 합계 중 가장 큰 값의 인덱스
                }


                //찾은 주 커튼월창 유형의 빛투과율 찾기 
                String[][] MainType_CW_Value = Program.DB.getValue(DB.type.ProjDB, "ConstructionCW", "빛투과율, 고정유리종류, 번호", "번호 = '" + CW_Type[index][1] + "'");
                String MainType_CW = MainType_CW_Value[0][2].ToString(); // 커튼월창 구조체 타입별로 면적 합계 중 가장 큰 값의 구조체 유형  


                ////////////////////////////////////////////////////////////////////////////////////창호와 커튼월창 중 진짜 메인 찾기///////////////////////////////////////////////////////////////////////
                if (MaxSum_CW > MaxSum_Win)
                {
                    Main_WinCW = CW_Type[index][0];
                    f_τD65_SNA = Convert.ToDouble(MainType_CW_Value[0][0]);
                    Main_glass = MainType_CW_Value[0][1];
                }
                else
                {
                    Main_WinCW = MainType_Win[0][0];
                    f_τD65_SNA = Convert.ToDouble(MainType_Win_Value[0][0]);
                    Main_glass = MainType_Win_Value[0][1];
                }


                facade_shade = "외부_베네치안"; //임시로 차양 유형 
                f_τD65_SA = 0.3; //임시로 차양 빛투과율 
            }

            catch { }


            //rooflight1 가져오기 >>>>>>>>>>천창 나중에 구조체파트부터 수정해야함 
            try
            {
                string[][] ValueA = Program.DB.getValue(DB.type.ProjDB, "rooflight1", "zoneNum,direction,Aca,a,b,AD,glasstype,γF,γW,a_s,b_s,hS,hw,hg,Da,τD65_SNA,τD65_SA,Kobl_1,Kobl_2,Kobl_3,shading,dimmingtype", "");
                int kk = -1;
                while (++kk < ValueA.Length)
                {

                    r_Aca = Convert.ToDouble(ValueA[kk][2]); //주창면적합
                    r_aD = Convert.ToDouble(ValueA[kk][3]); //주광이용깊이
                    r_bD = Convert.ToDouble(ValueA[kk][4]); //주광이용길이
                    r_AD = Convert.ToDouble(ValueA[kk][5]); //주광면적 

                    roof_glass = ValueA[kk][6];  //주창유리
                    r_τD65_SNA = Convert.ToDouble(ValueA[kk][15]); //주창유리빛투과율

                    roof_shade = ValueA[kk][20]; //차양
                    r_τD65_SA = Convert.ToDouble(ValueA[kk][16]); // 차양 차양 가동시 빛 투과율 



                }
            }

            catch { }

        }

    }

}
