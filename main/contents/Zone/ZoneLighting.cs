using main.subcontents.ZoneLighting;

namespace main.contents
{
    public partial class ZoneLighting : Form
    {

        //변수

        double Em, KA, FA;
        double Wr, Lr, A, hR, hm, hLi, hTa, K;
        double N; //조명 설치 개수
        string facade_di, glass1, facade_shade, facade_dimming;
        double Zone_f_Aca, Zone_f_a, Zone_f_b, Zone_f_AD, f_τD65_SNA, K1, K2, K3, γSh_lsh, γSh_hA, γSh_vA;

        public string roof_di, roof_glass, roof_shade, roof_dimming;
        public double r_Aca, r_aD, r_bD, r_AD, γF, γW, As, Bs, hs, hw, hg, Da, r_τD65_SNA, r_τD65_SA, Kobl_1, Kobl_2, Kobl_3;

        public string RenewName2, RenewA;
        string facade, roof;

        public double Reneweff;


        double UFF; //LightMethod에 따라 정해지는 값
        double Foc; //ControlType에 따라 정해지는 값
        double Pj_lx;

        string LightNumber, LightType, LightType2, LightConverter, Method, control, D_LightFi, D_LightPi, dimming, LightFi, LightW, ShadeType;
        double U_LightFi, U_LightPi, U_Pn;
        double LightFL;
        double Pj;
        double Fo, Fo1, Fo2, Fo3;

        string RenewName;

        double Fc;

        string NaturalType, Type;//자연채광 대분류 , 신재생
        string Floor;

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

            //존 콤보박스
            Zone_comboBox.Items.Add("1F_Zone02");
            Zone_comboBox.Items.Add("1F_Zone04");

            //차양종류 콤보박스 
            Shade_comboBox.Items.Add("없음");
            Shade_comboBox.Items.Add("외부_베네치안");



        }




        private void GeneralPanel_Paint(object sender, PaintEventArgs e)
        {
            Panel p = (Panel)sender;
            ControlPaint.DrawBorder(e.Graphics, p.DisplayRectangle, Color.FromArgb(153, 180, 209), ButtonBorderStyle.Solid);
        }

        private void Floor_textBox_TextChanged(object sender, EventArgs e)
        {
            Floor = Floor_textBox.Text;
        }


        private void LightDB_button_Click(object sender, EventArgs e)
        {

            LightingDB lightingdb_form = new LightingDB();
            DialogResult result = lightingdb_form.ShowDialog();
            if (result == DialogResult.OK)
            {
                LightNumber = lightingdb_form.Select_Light[0];
                LightType = lightingdb_form.Select_Light[1];
                LightType2 = lightingdb_form.Select_Light[2];
                LightConverter = lightingdb_form.Select_Light[4];
                LightFi = lightingdb_form.Select_Light[5];
                LightW = lightingdb_form.Select_Light[6];
                LightFL = Convert.ToDouble(lightingdb_form.Select_Light[8]);

                //LightType_textBox.Text = LightType;
                //FL_textBox.Text = string.Format("{0:F2}", LightFL);


                //Default의 경우 VAR이지만 사용자 DB의 경우 숫자
                //사용자 이름 나중에 LP 포함해서 하지 말아야겠다

                if (LightNumber.Contains("LP"))
                {
                    D_LightFi = lightingdb_form.Select_Light[5];
                    D_LightPi = lightingdb_form.Select_Light[6];
                }

                else
                {
                    U_LightFi = Convert.ToDouble(lightingdb_form.Select_Light[5]);
                    U_LightPi = Convert.ToDouble(lightingdb_form.Select_Light[6]);

                }

            }
            LightInfo();
            Match_Pjlx();
            Calc_Pj();
            Load_Lamp_image();

        }


        private void LightMethod_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Method = LightMethod_comboBox.SelectedItem.ToString();
            Match_Pjlx();
            Calc_Pj();
        }


        private void ControlType_comboBox_SelectedIndexChanged_1(object sender, EventArgs e)
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
                }


            }
            //textBox1.Text = this.facade;
        }



        private void RenewDB_button_Click(object sender, EventArgs e)
        {

            RenewDB renewdb_form = new RenewDB();
            DialogResult result = renewdb_form.ShowDialog();
            if (result == DialogResult.OK)
            {

                RenewName = renewdb_form.Select_Renew[1];
                RenewName2 = renewdb_form.Select_Renew[2];
                Reneweff = Convert.ToDouble(renewdb_form.Select_Renew[4]);
                RenewA = renewdb_form.Select_Renew[7];


                RenewType_textBox.Text = RenewName;

                R1_textBox.Text = RenewName2;
                R2_textBox.Text = RenewA;
                R3_textBox.Text = Reneweff.ToString();



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
            //Load_Shade_image();
        }

        private void Renew_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            RenewCheck();

            RenewInfo();
            Load_Renew2_image();
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


        //주창 정보 들어오기 
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

                Shade1_textBox.Visible = true;
                label4.Visible = true;
                ShadeW_textBox.Visible = true;
                label7.Visible = true;
                Sahdecount_textBox.Visible = true;
                ShadeA_textBox.Visible = true;

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

                Shade1_textBox.Visible = false;
                label4.Visible = false;
                ShadeW_textBox.Visible = false;
                label7.Visible = false;
                Sahdecount_textBox.Visible = false;
                ShadeA_textBox.Visible = false;


                Shade_comboBox.Visible = false;
                shade2_textBox.Visible = false;

                label2.Visible = false;
                label3.Visible = false;


            }

        }


        //집광채광 정보 들어오기 
        private void RenewInfo()
        {
            R1_label.Visible = true;
            R2_label.Visible = true;
            R3_label.Visible = true;




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

        private void Load_Lamp_image()
        {
            try {
                string[][] Image = Program.DB.getValue(DB.type.BaseDB_Lighting, "조명_램프분류이미지", "이미지", "조명분류 = '" + LightType2 + "'");
                Lamp_pictureBox.Load(Program.gPath + Image[0][0]);
                Lamp_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            } catch { }
            

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


        //집광채광 정보창 그림 로드 
        private void Load_Renew2_image()
        {
            if (RenewName != null && Renew_checkBox.Checked)
            {
                string[][] Image = Program.DB.getValue(DB.type.BaseDB_Lighting, "조명_집광채광이미지", "이미지", "집광채광 = '" + "광덕트제품" + "'");
                Renew2_pictureBox.Load(Program.gPath + Image[0][0]);
                Renew2_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            }


            //else
            //{
            // Renew2_pictureBox.Image = null;
            //}


        }
        private void ZoneLighting_VisibleChanged(object sender, EventArgs e)
        {
            String ID = main.MainContents.selID;
            ID = ID.Substring(19, 9);
            Zone_textBox.Text = ID;
        }
    }

}
