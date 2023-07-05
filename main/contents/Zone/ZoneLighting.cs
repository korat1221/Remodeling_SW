using main.contentslist;
using main.subcontents.ZoneLighting;

namespace main.contents
{
    public partial class ZoneLighting : Form
    {

        //변수

        //존 정보(가져오는 값)
        double Em, KA, FA;
        double Wr, Lr, A, hR, hm, hLi, hTa, K;
        
        string facade_di, glass1, facade_shade, facade_dimming;
        double Zone_f_Aca, Zone_f_a, Zone_f_b, Zone_f_AD, f_τD65_SNA, K1, K2, K3, γSh_lsh, γSh_hA, γSh_vA;
        
        public string roof_di, roof_glass, roof_shade, roof_dimming;
        public double r_Aca, r_aD, r_bD, r_AD, γF, γW, As, Bs, hs, hw, hg, Da, r_τD65_SNA, r_τD65_SA, Kobl_1, Kobl_2, Kobl_3;


        //테이블에서 가져오는 값
        double UFF; //LightMethod에 따라 매칭값
        double Foc; //ControlType에 따라 매칭값
        double Pj_lx; //매칭값


        //ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ



        //선택해야하는 정보(save 되어야 함)

        //RenewDB저장값
        public double Reneweff,  U_RenewLenght1, U_RenewLenght2, U_RenewA;
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




            //DB 불러올 때 에러날 수도 있으니까 try catch 하기
            //Zonelight profile 가져오기 
            try
            {
                string[][] ValueA = Program.DB.getValue(DB.type.ProjDB, "Form_ZoneLightprofile", "Em,KA,FA", "");
                int kk = -1;
                while (++kk < ValueA.Length)
                {
                    Em = Convert.ToDouble(ValueA[kk][0]);
                    KA = Convert.ToDouble(ValueA[kk][1]);
                    FA = Convert.ToDouble(ValueA[kk][2]);
                }
            }


            catch (IOException e)
            {
                if (e.Source != null)
                    //Console.WriteLine("IOException source: {0}", e.Source);
                    throw;
            }



            //Zonelight general 가져오기 

            try
            {
                string[][] ValueA = Program.DB.getValue(DB.type.ProjDB, "Form_ZoneLightgeneral", "zoneNum,Wr,Lr,A,hR,hm,hLi,hTa,K", "");
                int kk = -1;
                while (++kk < ValueA.Length)
                {
                    Wr = Convert.ToDouble(ValueA[kk][1]);
                    Lr = Convert.ToDouble(ValueA[kk][2]);
                    A = Convert.ToDouble(ValueA[kk][3]);
                    hR = Convert.ToDouble(ValueA[kk][4]);
                    hm = Convert.ToDouble(ValueA[kk][5]);
                    hLi = Convert.ToDouble(ValueA[kk][6]);
                    hTa = Convert.ToDouble(ValueA[kk][7]);
                    K = Convert.ToDouble(ValueA[kk][8]);
                }
            }

            catch (IOException e)
            {
                if (e.Source != null)
                    //Console.WriteLine("IOException source: {0}", e.Source);
                    throw;
            }


            //facade1 가져오기 
            try
            {
                string[][] ValueA = Program.DB.getValue(DB.type.ProjDB, "facade1", "direction,Aca,a,b,AD,glass,τD65_SNA,K1,K2,K3,shade,dimming,γSh_lsh,γSh_lsh,γSh_vA", "");
                int kk = -1;
                while (++kk < ValueA.Length)
                {
                    facade_di = ValueA[kk][0];
                    Zone_f_Aca = Convert.ToDouble(ValueA[kk][1]);
                    Zone_f_a = Convert.ToDouble(ValueA[kk][2]);
                    Zone_f_b = Convert.ToDouble(ValueA[kk][3]);
                    Zone_f_AD = Convert.ToDouble(ValueA[kk][4]);
                    glass1 = ValueA[kk][5];
                    f_τD65_SNA = Convert.ToDouble(ValueA[kk][6]);
                    K1 = Convert.ToDouble(ValueA[kk][7]);
                    K2 = Convert.ToDouble(ValueA[kk][8]);
                    K3 = Convert.ToDouble(ValueA[kk][9]);
                    facade_shade = ValueA[kk][10];
                    facade_dimming = ValueA[kk][11];
                    γSh_lsh = Convert.ToDouble(ValueA[kk][12]);
                    γSh_hA = Convert.ToDouble(ValueA[kk][13]);
                    γSh_vA = Convert.ToDouble(ValueA[kk][14]);
                }
            }

            catch (IOException e)
            {
                if (e.Source != null)
                    //Console.WriteLine("IOException source: {0}", e.Source);
                    throw;
            }



            //rooflight1 가져오기 
            try
            {
                string[][] ValueA = Program.DB.getValue(DB.type.ProjDB, "rooflight1", "zoneNum,direction,Aca,a,b,AD,glasstype,γF,γW,a_s,b_s,hS,hw,hg,Da,τD65_SNA,τD65_SA,Kobl_1,Kobl_2,Kobl_3,shading,dimmingtype", "");
                int kk = -1;
                while (++kk < ValueA.Length)
                {
                    roof_di = ValueA[kk][1];
                    r_Aca = Convert.ToDouble(ValueA[kk][2]);
                    r_aD = Convert.ToDouble(ValueA[kk][3]);
                    r_bD = Convert.ToDouble(ValueA[kk][4]);
                    r_AD = Convert.ToDouble(ValueA[kk][5]);
                    roof_glass = ValueA[kk][6];
                    γF = Convert.ToDouble(ValueA[kk][7]);
                    γW = Convert.ToDouble(ValueA[kk][8]);
                    As = Convert.ToDouble(ValueA[kk][9]);
                    Bs = Convert.ToDouble(ValueA[kk][10]);
                    hs = Convert.ToDouble(ValueA[kk][11]);
                    hw = Convert.ToDouble(ValueA[kk][12]);
                    hg = Convert.ToDouble(ValueA[kk][13]);
                    Da = Convert.ToDouble(ValueA[kk][14]);
                    r_τD65_SNA = Convert.ToDouble(ValueA[kk][15]);
                    r_τD65_SA = Convert.ToDouble(ValueA[kk][16]);
                    Kobl_1 = Convert.ToDouble(ValueA[kk][17]);
                    Kobl_2 = Convert.ToDouble(ValueA[kk][18]);
                    Kobl_3 = Convert.ToDouble(ValueA[kk][19]);
                    roof_shade = ValueA[kk][20];
                    roof_dimming = ValueA[kk][21];
                }
            }

            catch (IOException e)
            {
                if (e.Source != null)
                    //Console.WriteLine("IOException source: {0}", e.Source);
                    throw;
            }






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
            //Load_Lamp_image();

        }


        private void LightMethod_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Method = LightMethod_comboBox.SelectedItem.ToString();
            Match_Pjlx();
            Calc_Pj();
        }


        private void ControlType_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            control = ControlType_comboBox.SelectedItem.ToString();

            //MessageBox.Show(Foc.ToString());
            Match_Foc();
            Calc_Fo();
            Calc_Fc();
        }


       
        private void DimmingType_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            dimming = DimmingType_comboBox.SelectedItem.ToString();
            // MessageBox.Show(dimming);

        }



        private void NaturalType_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

            NaturalType = NaturalType_comboBox.SelectedItem.ToString();
            Load_NaturalType_image(NaturalType);
            NaturalType_case1();
            Load_Shade_image();
            WindowInfo2();
            Load_AD2_image();
            Load_Shade_image();

        }



        private void NaturalDB_button_Click(object sender, EventArgs e)
        {

            // double facade;

            if (NaturalType == "파사드")
            {
                LightingNatural_facade naturallighting_facade = new LightingNatural_facade(NaturalType);
                DialogResult result = naturallighting_facade.ShowDialog();
                if (result == DialogResult.OK)
                {
                    this.facade = naturallighting_facade.facadetype;
                    this.doubleskinglass = naturallighting_facade.doubleskinglasstype;
                    this.atriumglass = naturallighting_facade.atriumglasstype;
                    this.zoneW = naturallighting_facade.W;
                    this.zoneL = naturallighting_facade.L;
                    this.zoneH = naturallighting_facade.H;
                    this.zoneGlassLight = naturallighting_facade.glasslight;

                    Load_NaturalType2_image();
                  


                }

            }

            if (NaturalType == "천창")
            {
                LightingNatural_roof naturallighting_roof = new LightingNatural_roof(NaturalType);
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
            //textBox1.Text = this.facade;
           
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

                }

                else
                {
                    U_RenewLenght1 = Convert.ToDouble(renewdb_form.Select_Renew[6]);
                    U_RenewLenght2 = Convert.ToDouble(renewdb_form.Select_Renew[7]);
                    U_RenewA = Convert.ToDouble(renewdb_form.Select_Renew[8]);


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
            //NaturalType_case1();
            WindowInfo();
            WindowInfo2();
            //Load_Shade_image();
            Load_AD_image();
            side_active();
        }
      

        private void Renew_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            RenewCheck();

            RenewInfo();
            //Load_Renew2_image();
            if (Renew_checkBox.Checked && RenewType_textBox.Text !=null)
            { 
                Load_RenewType_image(RenewName);
            }
            else
            { Main_pictureBox2.Image = null; }

        }
       

        //차양 유무에 따른 그림 변화
        private void Shade_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShadeType = Shade_comboBox.SelectedItem.ToString();
            if (ShadeType != null)
            {
                Load_Shade_image();
            }
        }



        //-----------------------------------------------------------------------------------------------------------------------------------------------------------



        public void Match_Pjlx()
        {
            String[][] value = Program.DB.getValue(DB.type.BaseDB_Lighting, "조명_럭스당조명밀도", "값,UFF", "조명방식='" + Method + "' AND K = '" + K + "'");
            Pj_lx = Convert.ToDouble(value[0][0]);
            UFF = Convert.ToDouble(value[0][1]);
            //MessageBox.Show(UFF.ToString());
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

                else if (D_LightFi != "VAR")
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
            L5_textBox.Text = LightFi;
            L6_textBox.Text = LightW;
            L8_textBox.Text = LightFL.ToString();

            L1_label.Visible = true;
            L2_label.Visible = true;
            L4_label.Visible = true;
            L5_label.Visible = true;
            L6_label.Visible = true;
            L8_label.Visible = true;
            label5.Visible = true;
            label6.Visible = true;

            label5.ForeColor = Color.Black;
            label6.ForeColor = Color.Black;

        }


        //주창 정보 보이기
        private void WindowInfo()
        {
            if (Natural_checkBox.Checked == true)
            {
                shade1_label.Visible = true;
                Shade2_label.Visible = true;
                Shade3_label.Visible = true;
                Shade4_label.Visible = true;
                Shade5_label.Visible = true;
                Shade6_label.Visible = true;
                Shade7_label.Visible = true;

                Window1_textBox.Visible = true;
                label4.Visible = true;
                WindowW_textBox.Visible = true;
                label7.Visible = true;
                Windowcount_textBox.Visible = true;
                WindowA_textBox.Visible = true;

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
                Shade5_label.Visible = false;
                Shade6_label.Visible = false;
                Shade7_label.Visible = false;

                Window1_textBox.Visible = false;
                label4.Visible = false;
                WindowW_textBox.Visible = false;
                label7.Visible = false;
                Windowcount_textBox.Visible = false;
                WindowA_textBox.Visible = false;


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
                WindowA_textBox.Text = Zone_f_Aca.ToString();

            }

            else if (NaturalType == "천창")
            {
                Window1_textBox.Text = roof_di;
                WindowA_textBox.Text = r_Aca.ToString();

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
                R1_textBox.Visible= true;
                R2_textBox.Visible= true;
                R3_textBox.Visible= true;

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
                D_label.Visible = true;
                direction_textBox.Visible = true;
                Aca_textBox.Visible = true;
                D_textBox.Visible = true;
                Acam2_label.Visible = true;
                Main_pictureBox3.Visible = true;

            }

            else
            {

                NaturalType_comboBox.Visible = false;
                NaturalDB_button.Visible = false;
                Direction_label.Visible = false;
                Aca_label.Visible = false;
                D_label.Visible = false;
                direction_textBox.Visible = false;
                Aca_textBox.Visible = false;
                D_textBox.Visible = false;
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
                Aca_textBox.Text = Zone_f_Aca.ToString();
                D_textBox.Text = 1.36.ToString();
            }

            else if (NaturalType == "천창")
            {
                direction_textBox.Text = roof_di;
                Aca_textBox.Text = r_Aca.ToString();
                D_textBox.Text = 1.28.ToString();
            }

            else
            {
                direction_textBox.Text = "";
                Aca_textBox.Text = "";
                D_textBox.Text = "";
            }

        }


        //조명 종류에 따른 그림 로드 

        //private void Load_Lamp_image()
        //{
        //    try {
        //        string[][] Image = Program.DB.getValue(DB.type.BaseDB_Lighting, "조명_램프분류이미지", "이미지", "조명분류 = '" + LightType2 + "'");
        //        Lamp_pictureBox.Load(Program.gPath + Image[0][0]);
        //        Lamp_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
        //    } catch { }
            

        //}

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


        //집광채광 정보창 그림 로드 
        //private void Load_Renew2_image()
        //{
        //    if (RenewName != null && Renew_checkBox.Checked)
        //    {
        //        string[][] Image = Program.DB.getValue(DB.type.BaseDB_Lighting, "조명_집광채광이미지", "이미지", "집광채광 = '" + "광덕트제품" + "'");
        //        Renew2_pictureBox.Load(Program.gPath + Image[0][0]);
        //        Renew2_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
        //    }


        //    //else
        //    //{
        //    // Renew2_pictureBox.Image = null;
        //    //}


        //}





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
                type_pictureBox.Visible= false;
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

            else if (Natural_checkBox.Checked && NaturalType == "천창" )
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
                A_label.Visible= true;
                A_textBox.Visible= true;
                AD_label.Visible= true;
                AD_textBox.Visible= true;
                aad_label.Visible= true;
                aad_textBox.Visible= true;
                bbd_label.Visible = true;
                bbd_textBox.Visible= true;
                NA_label.Visible= true;
                NA_textBox.Visible= true;
                label10.Visible = true;
                label11.Visible= true;
                label12.Visible= true;
                label13.Visible= true;
                label14.Visible= true;

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


         
        //존명칭 로드
        private void ZoneLighting_VisibleChanged(object sender, EventArgs e)
        {
            String ID = main.MainContents.selID;
            ID = ID.Substring(19, 9);
            Zone_textBox.Text = ID;
        }


        private void Save_button_Click(object sender, EventArgs e)
        {
            if(LightType == null)
            {
                MessageBox.Show("조명 종류를 선택하세요.");
            }


            else
            {
                Save();
            }

        }

        public static bool OnLoadListProc(Form form)
        {
            List_ConstructionWall f = (List_ConstructionWall)form;
            f.load_List();
            return true;
        }


        private void Save()
        {
            Program.DB.setValue(DB.type.ProjDB, "ZoneLighting", "번호,조명방식,제어방식,디밍유형,조명개수,조명밀도,재실계수,재실계수1,재실계수2,재실계수3,조도제어계수," +
                "자연채광유형,파사드,이중외피유리,아트리움유리,파사드유리빛투과율,파사드너비,파사드길이,파사드높이,천창,천창유리각,천창수평측면각,천창장변부길이,천창단변부길이,천창수평상부높이,차양," +
                "집광채광번호,집광채광명칭,집광채광종류,집광채광효율,집광채광면적,"+
                "표준길이1,표준길이2,표준너비,사용자길이1,사용자길이2,사용자면적,"+
                "조명번호, 등기구명칭, 램프유형, 컨버터_안정기, 광속, 소비전력, 조명계수,"+
                "표준광속, 표준소비전력,사용자광속, 사용자소비전력,사용자예상전력",
                
                "'" + Zone_textBox.Text + "','" + Method + "','" + control + "','" + dimming + "','" + N.ToString() + "','" + Pj.ToString() + "','" +
                Fo.ToString() + "','" + Fo1.ToString() + "','" + Fo2.ToString() + "','" + Fo3.ToString() + "','" + Fc.ToString() + "','" +
                NaturalType + "','" + facade + "','"+ doubleskinglass + "','" + atriumglass + "','"+zoneGlassLight + "','" + zoneW + "','"+zoneL + "','"+zoneH+ "','" + roof + "','" + zoneRoofAngle1 + "','" + zoneRoofAngle2 + "','" + zoneRoofLenght1 + "','" + zoneRoofLenght2 + "','" + zoneRoofLenght3 +"','" + ShadeType + "','" +
                RenewNum + "','" + RenewName + "','" + RenewName2 + "','" + Reneweff.ToString() + "','" + RenewA.ToString() + "','" +
                D_RenewLenght1 + "','" + D_RenewLenght2 + "','" + D_RenewA + "','" + U_RenewLenght1.ToString() + "','" + U_RenewLenght2.ToString() + "','" + U_RenewA.ToString() + "','" +
                LightNumber + "','" + LightType + "','" + LightType2 + "','" + LightConverter + "','" + LightFi + "','" + LightW + "','" + LightFL.ToString() + "','" +
                D_LightFi + "','" + D_LightPi + "','" + U_LightFi.ToString() + "','" + U_LightPi.ToString() + "','" + U_Pn.ToString()
                + "'", "번호");

            this.DialogResult = DialogResult.OK;
            this.Hide();
            Program.getMenuForm().DoLoadForm(34, OnLoadListProc);



        }

        private void reset()
        {
            Zone_textBox.Text = "";
            Floor_textBox.Text = "";
            LightName_textBox.Text = "";

            LightType = null;
            Method = null;
            control = null;
            dimming = null;

            FL_textBox.Text = null;
            Pj_textbox.Text = null;
            fc_textBox.Text = null;

            Natural_checkBox.Checked = false;
            NaturalType = null;
            direction_textBox.Text = null;
            Aca_textBox.Text = null;
            D_textBox.Text = null;

            Renew_checkBox.Checked = false;
            RenewName = null;
            RenewDi_comboBox.SelectedItem = null;
            Slope_comboBox.SelectedItem = null;

            Main_pictureBox.Visible = false;
            Main_pictureBox2.Visible = false;
            Main_pictureBox3.Visible = false;


            LightType = null;
            LightType2 = null;
            LightConverter = null;
            LightFi = null;
            LightW = null;
            LightFL = 0;


            Window1_textBox.Text = null;
            WindowW_textBox.Text = null;
            Windowcount_textBox.Text = null;
            WindowA_textBox.Text = null;

            Shade_comboBox.SelectedItem = null;
            shade2_textBox.Text = null;



            R1_textBox.Text = null;
            R2_textBox.Text = null;
            R3_textBox.Text = null;


            type_pictureBox.Visible = false;
            A_textBox.Text = null;
            AD_textBox.Text = null;
            bbd_textBox.Text = null;
            aad_textBox.Text = null;
            NA_textBox.Text = null;


        }

        //존 리스트 클릭시 로드하도록?  //위에 추가한것때문에 추후에 고쳐야 함
        public void LoadData(String ID)
        {
            reset();

            try
            {
                Zone_textBox.Text =ID;

                String[][] Load = Program.DB.getValue(DB.type.ProjDB, "ZoneLighting", "번호,조명방식,제어방식,디밍유형,조명개수,조명밀도,재실계수,재실계수1,재실계수2,재실계수3,조도제어계수," +
                "자연채광유형,파사드,이중외피유리,아트리움유리,파사드유리빛투과율,파사드너비,파사드길이,파사드높이,천창,천창유리각,천창수평측면각,천창장변부길이,천창단변부길이,천창수평상부높이,차양," +
                "집광채광번호,집광채광명칭,집광채광종류,집광채광효율,집광채광면적," +
                "표준길이1,표준길이2,표준너비,사용자길이1,사용자길이2,사용자면적," +
                "조명번호, 등기구명칭, 램프유형, 컨버터_안정기, 광속, 소비전력, 조명계수," +
                "표준광속, 표준소비전력,사용자광속, 사용자소비전력,사용자예상전력",
                "번호 = '" + ID + "'");


                Zone_textBox.Text = Load[0][1];
                Method = Load[0][2];
                control = Load[0][3];
                dimming = Load[0][4];
                N = Convert.ToDouble(Load[0][5]);
                Pj = Convert.ToDouble(Load[0][6]);
                Fo = Convert.ToDouble(Load[0][7]);
                Fo1 = Convert.ToDouble(Load[0][8]);
                Fo2 = Convert.ToDouble(Load[0][9]);
                Fo3 = Convert.ToDouble(Load[0][10]);
                Fc = Convert.ToDouble(Load[0][11]);

                if (Natural_checkBox.Checked)
                {
                    NaturalType = Load[0][12];
                    facade = Load[0][13];
                    roof = Load[0][14];
                    ShadeType = Load[0][15];
                }

                if (Renew_checkBox.Checked)
                {


                    RenewName = Load[0][16];
                    RenewName2 = Load[0][17];
                    Reneweff = Convert.ToDouble(Load[0][18]);
                    RenewA = Load[0][19];
                    D_RenewLenght1 = Load[0][20];
                    D_RenewLenght2 = Load[0][21];
                    D_RenewA = Load[0][22];
                    U_RenewLenght1 = Convert.ToDouble(Load[0][23]);
                    U_RenewLenght2 = Convert.ToDouble(Load[0][24]);
                    U_RenewA = Convert.ToDouble(Load[0][25]);

                }

                LightNumber = Load[0][26];
                LightType = Load[0][27];
                LightType2 = Load[0][28];
                LightConverter = Load[0][29];
                LightFi = Load[0][30];
                LightW = Load[0][31];
                LightFL = Convert.ToDouble(Load[0][32]);

                D_LightFi = Load[0][33];
                D_LightPi = Load[0][34];
                U_LightFi = Convert.ToDouble(Load[0][35]);
                U_LightPi = Convert.ToDouble(Load[0][36]);
                U_Pn = Convert.ToDouble(Load[0][37]);



            }


            catch { }

        }




    }

}
