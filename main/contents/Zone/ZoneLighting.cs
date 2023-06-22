using main.subcontents.ZoneLighting;

namespace main.contents
{
    public partial class ZoneLighting : Form
    {

        //변수

        double Em, KA, FA;
        double Wr, Lr, A, hR, hm, hLi, hTa, K;
        double N; //조명 설치 개수

        
        double UFF; //LightMethod에 따라 정해지는 값
        double Foc; //ControlType에 따라 정해지는 값
        double Pj_lx;

        string LightNumber, LightType, Method, control, D_LightFi, D_LightPi, dimming;

       

        double U_LightFi, U_LightPi, U_Pn;
        double LightFL ;

        
        double Pj;
        double Fo, Fo1, Fo2, Fo3;
        double Fc;



        public ZoneLighting()
        {

            // 화면 뜨자마자 있었으면 하는거 전부 콤보박스로 몰아 넣기 
            InitializeComponent();

            //조명 이미지 로드 
            string[][] Image = Program.DB.getValue(DB.type.BaseDB, "메뉴아이콘", "하위메뉴아이콘", "하위메뉴명 = '존 조명정보'");
            Icon_pictureBox.Load(Program.gPath + Image[0][0]);
            Icon_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;

            //DB 불러올 때 에러날 수도 있으니까 try catch 하 


            //Zonelight profile 가져오기 
            //string[][]  ValueA = Program.DB.getValue(DB.type.ProjDB, "Form_ZoneLightprofile", "Em,KA,FA", "zoneNum='" + zoneNum + "'");
            string[][] ValueA = Program.DB.getValue(DB.type.ProjDB, "Form_ZoneLightprofile", "Em,KA,FA", "");
            int kk = -1;
            while (++kk < ValueA.Length)
            {
                Em = Convert.ToDouble(ValueA[kk][0]);
                KA = Convert.ToDouble(ValueA[kk][1]);
                FA = Convert.ToDouble(ValueA[kk][2]);
            }

            //Zonelight general 가져오기 
            ValueA = Program.DB.getValue(DB.type.ProjDB, "Form_ZoneLightgeneral", "zoneNum,Wr,Lr,A,hR,hm,hLi,hTa,K", "");
            kk = -1;
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



            //켜자마자 자동으로 층 및 명칭 불러오기 
            //https://agape93.tistory.com/6

            //조명방식 콤보박스
            Program.UTIL.FillComboBox(LightMethod_comboBox, "조명", "조명방식", "1");

            //제어방식 콤보박스
            Program.UTIL.FillComboBox(ControlType_comboBox, "조명", "제어종류", "1");

            //디밍유형 콤보박스
            Program.UTIL.FillComboBox(DimmingType_comboBox, "조명", "주광제어종류", "1");

            //집광채광 향 콤보박스
            Program.UTIL.FillComboBox(RenewDi_comboBox, "조명", "방위", "1");

            //집광채광 기울기 콤보박스
            Program.UTIL.FillComboBox(Slope_comboBox, "조명", "창기울기", "1");




        }



        private void GeneralPanel_Paint(object sender, PaintEventArgs e)
        {
            Panel p = (Panel)sender;
            ControlPaint.DrawBorder(e.Graphics, p.DisplayRectangle, Color.FromArgb(153, 180, 209), ButtonBorderStyle.Solid);
        }


       


        //public void Match_Pj_lx()
        //{
        //    String[][] value = Program.DB.getValue(DB.type.BaseDB, "조명_럭스당조명밀도", "값", "조명방식='" + Method + "' AND K = '" + K + "'");
        //    Pj_lx = Convert.ToDouble(value[0][0]);
        //    MessageBox.Show(Pj_lx.ToString());
        //}


        private void LightDB_button_Click(object sender, EventArgs e)
        {
           
            LightingDB lightingdb_form = new LightingDB();
            DialogResult result= lightingdb_form.ShowDialog();
            if (result == DialogResult.OK)
            {
                LightNumber = lightingdb_form.Select_Light[0];
                LightType = lightingdb_form.Select_Light[1];
                LightType_textBox.Text = LightType;

                LightFL = Convert.ToDouble(lightingdb_form.Select_Light[8]);
                FL_textBox.Text = string.Format("{0:F2}", LightFL);
                

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
            Match_Pjlx();
            Calc_Pj();

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
            MessageBox.Show(dimming);

        }




        //-----------------------------------------------------------------------------------------------------------------------------------------------------------



        public void  Match_Pjlx()
        {            
                String[][] value = Program.DB.getValue(DB.type.BaseDB, "조명_럭스당조명밀도", "값,UFF", "조명방식='" + Method + "' AND K = '" + K + "'");
                Pj_lx = Convert.ToDouble(value[0][0]);
                UFF = Convert.ToDouble(value[0][1]);
                //MessageBox.Show(UFF.ToString());
        }

        public void Match_Foc()
        {
            String[][] value = Program.DB.getValue(DB.type.BaseDB, "조명_조명제어", "Foc", "제어종류 = '" + control + "'");
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
            
            for (int i = 1; i <Fo_list.Length; i++)
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












    }
}
