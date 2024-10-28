using main.contentslist;
using main.subcontents.RESystem_PV;
using Microsoft.Web.WebView2.Core;
using System;

namespace main.contents
{
    public partial class PV : Form
    {

        //RESystem RESystem;

        //double a;

        #region Main Form Variable
        bool scriptable = false;
        //설치정보
        String Num, Name;
        string VentilationType, PVsystem;
        double width_n, height_n; //가로, 세로 개수
        double PVcapacity_Kw; // 설치용량
        double PVArea_m2; //총면적
        string Orientation, Slope; //방위, 경사
        double PVLshobst_m, PVHshobst_m; //지형물까지의 거리, 지형물의 높이
        string[][] 프로젝트유형;
        //태양광 계통도

        #endregion //Main Form Variable

        #region DB Variable

        //PVModuleDB
        string PVModuleNumber, PVModule, PVmanu_year;
        double PVKpk_kW_m2, PVwidth_m, PVheight_m, PVPn_W;
        //index
        public double PVmanuyearfa;

        //PVInverterDB 
        string PVInverterNumber, Inverter;
        double InverterEfficiency;

        //PVBatteryDB
        String PVBatteryNumber, Battery;
        double PVV_V, PVAH_Ah, Batterycapacity;

        #endregion / DB Variable

        #region Input Variable

        //계산
        public double PVfperf;

        //[일사량 정보 변수]
        //database
        public double[] PVIs_W_m2 = new double[12];
        public double[] PVdmth = { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
        public double PVIref_kW_m2 = 1;


        //음영계수
        public double PVLPVwid_m, PVLPVlen_m;
        public double[] PVIdirtot_W_m2 = new double[12];
        public double[] PVIdiftot_W_m2 = new double[12];
        public double[] PVαsol = new double[12];


        #endregion / Input Variable

        #region Calculation Variable

        //일사량_kWh/(m2.month)
        public double[] PVEsolm_kWh_m2 = new double[12];

        //최대출력
        public double PVPpk_kW;

        //생성된 전기에너지
        public double[] PVEelpvoutm_kWh = new double[12]; //월별
        public double[] PVEelpvoutm_kWh_m2 = new double[12]; //단위당
        public double PVEelpvouta_kWh_a; //연간
        public double PVefficiency; //평균효율


        //음영감소계수
        public double[] PVFshobstpvt = new double[12];
        public double[] PVhshobst_m = new double[12];
        public double[] PVhshobstwid_m = new double[12];
        public double[] Ishdirtotpvt_W = new double[12];
        double Esolm, Ppk, Eelpvoutm, Fshobstpvt, hshobst, hshobstwid, Ishdir;

        #endregion / Calculation Variable

        String[][] 지역;

        public PV()
        {
            InitializeComponent();
            InitializeAsync();
            webView21.Source = new Uri(Program.gPath + "threejs\\public\\chart_ctrl2.html", true);
            지역 = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "지역", "");

            string[][] Image = Program.DB.getValue(DB.type.BaseDB_HCneed, "메뉴아이콘", "하위메뉴아이콘", "하위메뉴명 = '태양광시스템'");
            if (Image.Length > 0)
            {
                pictureBox1.Load(Program.gPath + Image[0][0]);
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            }

            프로젝트유형 = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "프로젝트유형번호");
           


            //PVsystem_combobox.Items.Clear();
            //PVsystem_combobox.Items.AddRange(new string[] { "독립형", "계통연계형" });

            Battery_label.Visible = false;
            Battery_textBox.Visible = false;
            BatteryDB_button.Visible = false;
            Batterycapacity_n.Visible = false;
            Batterycapacity_textBox.Visible = false;
            Batterycapacity_s.Visible = false;
        }
        private void PV_Table()
        {
            new StackedHeaderDecorator(PV_dataGridView, DataGridViewAutoSizeColumnsMode.Fill);
            PV_dataGridView.Columns.Clear();
            PV_dataGridView.Columns.Add("A0", "번호");
            PV_dataGridView.Columns.Add("A1", "명칭");
            PV_dataGridView.Columns.Add("A2", "개수");
            PV_dataGridView.Columns.Add("A3", "면적.[m2]");

            DataGridViewComboBoxColumn direction = new DataGridViewComboBoxColumn();
            direction.HeaderText = "설치정보.방위";
            direction.Items.AddRange(new string[] { "수평", "남", "남동", "남서", "동", "서", "북서", "북동", "북" });
            PV_dataGridView.Columns.Add(direction);

            DataGridViewComboBoxColumn slope = new DataGridViewComboBoxColumn();
            slope.HeaderText = "설치정보.기울기";
            slope.Items.AddRange(new string[] { "0˚", "30˚", "45˚", "60˚", "90˚" });
            PV_dataGridView.Columns.Add(slope);

            DataGridViewComboBoxColumn type = new DataGridViewComboBoxColumn();
            type.HeaderText = "설치정보.후면유형";
            type.Items.AddRange(new string[] { "통기없음", "미세통기층", "통기층" }); // PVfperf = 0.76 , 0.8, 0.82
            PV_dataGridView.Columns.Add(slope);

            PV_dataGridView.Columns.Add("A7", "음영정보.거리[m]"); //width_combo
            PV_dataGridView.Columns.Add("A8", "음영정보.높이[m]"); //height_combo
            PV_dataGridView.Columns.Add("A9", "제품정보.길이[m2]");
            PV_dataGridView.Columns.Add("A10", "제품정보.높이[m2]");
            PV_dataGridView.Columns.Add("A11", "제품정보.정격출력[W]");
            PV_dataGridView.Columns.Add("A12", "제품정보.성능[Kpk]");
            PV_dataGridView.Columns.Add("A13", "설치");
        }
        async void InitializeAsync()
        {
            await webView21.EnsureCoreWebView2Async(null);
            webView21.CoreWebView2.NavigationCompleted += OnNaviCompleted;
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
        private void PVType_ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (PVType_ComboBox.Text == "계통연계형")
            {
                Battery_label.Visible = false;
                Battery_textBox.Visible = false;
                BatteryDB_button.Visible = false;
                Batterycapacity_n.Visible = false;
                Batterycapacity_textBox.Visible = false;
                Batterycapacity_s.Visible = false;
            }
            else if (PVType_ComboBox.Text == "독립형")
            {
                Battery_label.Visible = true;
                Battery_textBox.Visible = true;
                BatteryDB_button.Visible = true;
                Batterycapacity_n.Visible = true;
                Batterycapacity_textBox.Visible = true;
                Batterycapacity_s.Visible = true;

            }
        }

        private void PVModuleDB_button_Click(object sender, EventArgs e)
        {
            PV_ModuleDB PV_ModuleDB_form = new PV_ModuleDB("장비일람표 DB");
            DialogResult result = PV_ModuleDB_form.ShowDialog();
            if (result == DialogResult.OK)
            {
                Load_PV_Table();
                //

                PVModuleNumber = PV_ModuleDB_form.Select_PVModule[0];
                PVModule = PV_ModuleDB_form.Select_PVModule[2];
                PVmanu_year = PV_ModuleDB_form.Select_PVModule[4];

                PVKpk_kW_m2 = Convert.ToDouble(PV_ModuleDB_form.Select_PVModule[6]);
                PVwidth_m = Convert.ToDouble(PV_ModuleDB_form.Select_PVModule[7]);
                PVheight_m = Convert.ToDouble(PV_ModuleDB_form.Select_PVModule[8]);
                PVPn_W = Convert.ToDouble(PV_ModuleDB_form.Select_PVModule[9]);

                if (PVmanu_year == "25년 이내")
                {
                    PVmanuyearfa = 1;
                }
                if (PVmanu_year == "25년 이상")
                {
                    PVmanuyearfa = 0.9;
                }
            }

            PVModule_textBox.Text = PVModule;
        }

        //private void PVHshobst_m_textBox_TextChanged(object sender, EventArgs e)
        //{
        //    int result;
        //    if (PVHshobst_m_textBox == null || PVHshobst_m_textBox.Text == "")
        //    {
        //    }
        //    else if (int.TryParse(PVHshobst_m_textBox.Text, out result) == true)
        //    {
        //        PVHshobst_m = Convert.ToDouble(PVHshobst_m_textBox.Text);
        //        PVHshobst_m_imge_textBox.Text = PVHshobst_m_textBox.Text.ToString();
        //    }
        //    else
        //    {
        //        MessageBox.Show("숫자를 입력하세요.");
        //    }

        //    PVShading_getvalue();

        //}

        //private void PVLshobst_m_textBox_TextChanged(object sender, EventArgs e)
        //{
        //    int result;
        //    if (PVLshobst_m_textBox.Text == null || PVLshobst_m_textBox.Text == "") { }
        //    else if (int.TryParse(PVLshobst_m_textBox.Text, out result) == true)
        //    {
        //        PVLshobst_m = Convert.ToDouble(PVLshobst_m_textBox.Text);
        //        PVLshobst_m_image_textBox.Text = PVLshobst_m_textBox.Text.ToString();
        //    }
        //    else
        //    {
        //        MessageBox.Show("숫자를 입력하세요.");
        //    }

        //    PVShading_getvalue();

        //}

       
        private void Load_PV_Table()
        {

            PV_Table();
            PV_dataGridView.Rows.Clear();
            string[][] User_Value = Program.DB.getValue(DB.type.ProjDB, "User_PVModule", "번호,DB유형,제품명,제조사,제작년도,CELLTYPE,가로길이,세로길이,정격출력,Kpk,신규기존", "번호 = '" + PVModuleNumber + "'");
            if (User_Value.Length > 0)
            {
                int nRow = PV_dataGridView.Rows.Add();
                PV_dataGridView.Rows[nRow].Cells[0].Value = User_Value[0][0];
                PV_dataGridView.Rows[nRow].Cells[1].Value = User_Value[0][1];
                PV_dataGridView.Rows[nRow].Cells[2].Value = User_Value[0][2];
                PV_dataGridView.Rows[nRow].Cells[3].Value = User_Value[0][3];
                PV_dataGridView.Rows[nRow].Cells[4].Value = User_Value[0][4];
                PV_dataGridView.Rows[nRow].Cells[5].Value = User_Value[0][5];
                PV_dataGridView.Rows[nRow].Cells[6].Value = User_Value[0][6];
                PV_dataGridView.Rows[nRow].Cells[7].Value = User_Value[0][7];
                PV_dataGridView.Rows[nRow].Cells[8].Value = User_Value[0][8];
                PV_dataGridView.Rows[nRow].Cells[9].Value = User_Value[0][9];
            }
        }

        //private void slope_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (slope_comboBox.SelectedItem != null)
        //    {
        //        Slope = slope_comboBox.SelectedItem.ToString();
        //        PVIs_W_m2_getvalue();
        //    }
        //}

        //private void InverterDB_button_Click(object sender, EventArgs e)
        //{
        //    PV_InverterDB PV_InverterDB_form = new PV_InverterDB();

        //    DialogResult result = PV_InverterDB_form.ShowDialog();
        //    if (result == DialogResult.OK)
        //    {
        //        PVInverterNumber = PV_InverterDB_form.Select_PVInverter[0];
        //        Inverter = PV_InverterDB_form.Select_PVInverter[2];
        //        InverterEfficiency = Convert.ToDouble(PV_InverterDB_form.Select_PVInverter[4]);
        //    }

        //    Inverter_textBox.Text = Inverter;
        //    InverterEfficiency_textBox.Text = string.Format("{0:F2}", InverterEfficiency);
        //}

        //private void BatteryDB_button_Click(object sender, EventArgs e)
        //{
        //    PV_BatteryDB PV_BatteryDB_form = new PV_BatteryDB();

        //    DialogResult result = PV_BatteryDB_form.ShowDialog();
        //    if (result == DialogResult.OK)
        //    {
        //        PVBatteryNumber = PV_BatteryDB_form.Select_PVBattery[0];
        //        Battery = PV_BatteryDB_form.Select_PVBattery[2];
        //        PVV_V = Convert.ToDouble(PV_BatteryDB_form.Select_PVBattery[4]);
        //        PVAH_Ah = Convert.ToDouble(PV_BatteryDB_form.Select_PVBattery[5]);
        //    }

        //    Battery_textBox.Text = Battery;
        //    Calc_Battery_Capacity();
        //}
        //private void Calc_Battery_Capacity()
        //{
        //    Batterycapacity = Convert.ToDouble(PVV_V) * Convert.ToDouble(PVAH_Ah) / 1000;
        //    if (Batterycapacity == 0)
        //    { }
        //    else
        //    {
        //        Batterycapacity_textBox.Text = string.Format("{0:F2}", Batterycapacity);
        //    }
        //}


        //private void PV_MainForm_Calculation_TotalCapacity()
        //{

        //    if (width_n_textBox.Text != "" && height_n_textBox.Text != "")
        //    {
        //        PVcapacity_Kw = width_n * height_n * PVPn_W / 1000;
        //        allcapacity_textBox.Text = string.Format("{0:F2}", width_n * height_n * PVPn_W / 1000);
        //    }
        //    else { }
        //}
        //public void PVIs_W_m2_getvalue() //일사량정보
        //{
        //    if (Orientation != null && Slope != null)
        //    {
        //        for (int mth = 0; mth < 12; mth++)
        //        {
        //            //전일사량불러오기
        //            string[][] token = Program.DB.getValue(DB.type.BaseDB_HCneed, "기후데이터_전일사량", "일사량", "지역명 ='" + 지역[0][0] + "' AND 방향 ='" + Orientation + "' AND  각도 = '" + Slope + "' and 기간 ='" + (mth + 1).ToString() + "월'");
        //            if (token.Length > 0)
        //            {
        //                PVIs_W_m2[mth] = Convert.ToDouble(token[0][0]);
        //            }
        //        }
        //    }
        //    else { }
        //}

        //public void PVShading_getvalue()
        //{
        //    if (PVLshobst_m_textBox.Text != null && PVHshobst_m_textBox.Text != null)
        //    {
        //        for (int mth = 0; mth < 12; mth++)
        //        {
        //            //직달일사량불러오기
        //            string[][] token1 = Program.DB.getValue(DB.type.BaseDB_HCneed, "기후데이터_직달일사량", "일사량", "지역명 ='" + 지역[0][0] + "' AND 방향 ='" + Orientation + "' AND  각도 = '" + Slope + "' and 기간 ='" + (mth + 1).ToString() + "월'");
        //            //확산일사량불러오기
        //            string[][] token2 = Program.DB.getValue(DB.type.BaseDB_HCneed, "기후데이터_산란일사량", "일사량", "지역명 ='" + 지역[0][0] + "' AND 방향 ='" + Orientation + "' AND  각도 = '" + Slope + "' and 기간 ='" + (mth + 1).ToString() + "월'");
        //            //태양고도각 불러오기
        //            string[][] token3 = Program.DB.getValue(DB.type.BaseDB_HCneed, "기후데이터_고도각", "고도각", "지역명 ='" + 지역[0][0] + "' AND 방향 ='" + Orientation + "' AND  각도 = '" + Slope + "' and 기간 ='" + (mth + 1).ToString() + "월'");

        //            if (token1.Length > 0)
        //            {
        //                PVIdirtot_W_m2[mth] = Convert.ToDouble(token1[0][0]);
        //                PVIdiftot_W_m2[mth] = Convert.ToDouble(token2[0][0]);
        //                PVαsol[mth] = Convert.ToDouble(token3[0][0]);
        //            }
        //        }
        //    }
        //    else { }
        //}

        //#region Calculation
        ////수직음영길이
        //public double Re_PV_hshobst_m(double LPVlen, double Hshobst, double Lshobst, double asol)
        //{
        //    hshobst = Math.Min(LPVlen, Math.Max(0, Hshobst - Lshobst * Math.Tan(asol * Math.PI / 180.0)));
        //    return hshobst;
        //}
        ////수평음영길이
        //public double Re_PV_hshobstwid_m(double hshobst, double asol)
        //{
        //    hshobstwid = hshobst / Math.Tan(asol * Math.PI / 180.0);
        //    return hshobstwid;
        //}
        ////직달일사 음영 적용
        //public double Re_PV_Ishdir_m(double area, double LPVlen, double hshobst, string β, double hshobstwid, double LPVwid, double Idir)
        //{
        //    double β1;
        //    β = β.Substring(0, 2);
        //    β1 = Convert.ToDouble(β);
        //    Ishdir = (area - Math.Min(LPVlen, Math.Sqrt(Math.Pow((hshobst / ((Math.Tan(β1 * Math.PI / 180.0) + hshobst / hshobstwid))), 2) + Math.Pow((Math.Tan(β1 * Math.PI / 180.0) * (hshobst / (Math.Tan(β1 * Math.PI / 180.0) + hshobst / hshobstwid))), 2))) * LPVwid) * Idir;
        //    if (hshobst == 0)
        //    {
        //        Ishdir = (area - Math.Min(LPVlen, 0) * LPVwid) * Idir;
        //    }
        //    return Ishdir;
        //}
        ////음영계수
        //public double Re_PV_Fshobstpvt_(double Idir, double Idif, double Ishdir, double area)
        //{
        //    Fshobstpvt = (Ishdir + Idif * area) / (Idir * area + Idif * area);
        //    return Fshobstpvt;
        //}
        ////일사량
        //public double Re_PV_Esolm_kWh(double Ls, double dmth, double Fshobstpvt)
        //{
        //    Esolm = Ls * dmth * 24 / 1000 * Fshobstpvt;
        //    return Esolm;
        //}
        ////최대 성능
        //public double Re_PV_Ppk_kW(double Kpk, double A, double manu_year_fa)
        //{
        //    Ppk = Kpk * A * manu_year_fa;
        //    return Ppk;
        //}

        ////태양광을 통해 생성된 전기 에너지
        //public double Re_PV_Eelpvoutm_kWh(double Esolm, double Ppk, double Fperf, double ηEU, double Iref)
        //{
        //    Eelpvoutm = Esolm * Ppk * Fperf * (ηEU / 100) / Iref;
        //    return Eelpvoutm;
        //}

        //#endregion / Calculation

        //private void Save()
        //{

        //    #region Calculation

        //    for (int mth = 0; mth < 12; mth++)
        //    {
        //        //수직음영길이
        //        PVhshobst_m[mth] = Re_PV_hshobst_m(PVLPVlen_m, PVHshobst_m, PVLshobst_m, PVαsol[mth]);
        //        //수평음영길이
        //        PVhshobstwid_m[mth] = Re_PV_hshobstwid_m(PVhshobst_m[mth], PVαsol[mth]);
        //        //직달일사 음영적용
        //        Ishdirtotpvt_W[mth] = Re_PV_Ishdir_m(PVArea_m2, PVLPVlen_m, PVhshobst_m[mth], Slope, PVhshobstwid_m[mth], PVLPVwid_m, PVIdirtot_W_m2[mth]);
        //        //음영계수
        //        PVFshobstpvt[mth] = Re_PV_Fshobstpvt_(PVIdirtot_W_m2[mth], PVIdiftot_W_m2[mth], Ishdirtotpvt_W[mth], PVArea_m2);
        //        //태양광 모듈에 들어오는 전일사량
        //        PVEsolm_kWh_m2[mth] = Re_PV_Esolm_kWh(PVIs_W_m2[mth], PVdmth[mth], PVFshobstpvt[mth]);
        //        //표준-테스트-조건에서 최대성능 
        //        PVPpk_kW = Re_PV_Ppk_kW(PVKpk_kW_m2, PVArea_m2, PVmanuyearfa);
        //        //태양광 시스템에 의해 생성된 전기 에너지
        //        PVEelpvoutm_kWh[mth] = Re_PV_Eelpvoutm_kWh(PVEsolm_kWh_m2[mth], PVPpk_kW, PVfperf, InverterEfficiency, PVIref_kW_m2);
        //        PVEelpvoutm_kWh_m2[mth] = PVEelpvoutm_kWh[mth] / PVArea_m2;
        //        //태양광 시스템에 의해 생성된 전기 에너지 연간 전기 에너지
        //        PVEelpvouta_kWh_a += PVEelpvoutm_kWh[mth];
        //        //평균효율
        //        double PVEelpvoutm_kWhaver = PVEelpvoutm_kWh.Average();
        //        double PVEsolm_kWh_m2aver = PVEsolm_kWh_m2.Average();
        //        PVefficiency = PVEelpvoutm_kWhaver / PVEsolm_kWh_m2aver / PVArea_m2;
        //        averagecpacity_textBox.Text = string.Format("{0:F2}", PVefficiency * 100);
        //        #endregion / Calculation
        //    }


        //    Program.DB.setValue(DB.type.ProjDB, "PV_Form", "번호,프로젝트유형,명칭", "'" + Num_textBox.Text + "','" + 프로젝트유형[0][0] + "','" + Name + "'", "번호");
        //    Program.DB.setValue(DB.type.ProjDB, "PV_Form", "번호,모듈번호,인버터번호,인버터명칭,인버터효율,배터리번호,배터리용량", "'" + Num_textBox.Text + "','" + PVModuleNumber + "','" + PVInverterNumber + "','" + Inverter + "','" + InverterEfficiency.ToString() + "','" + PVBatteryNumber + "','" + Batterycapacity + "'", "번호");
        //    Program.DB.setValue(DB.type.ProjDB, "PV_Form", "번호,통풍유무,계통유형,가로개수,세로개수,용량,면적,방위,기울기,지형물거리,지형물높이", "'" + Num_textBox.Text + "','" + VentilationType + "','" + PVsystem + "','" + width_n + "','" + height_n + "','" + PVcapacity_Kw + "','" + PVArea_m2 + "','" + Orientation + "','" + Slope + "','" + PVLshobst_m + "','" + PVHshobst_m + "'", "번호");
        //    for (int mth = 0; mth < 12; mth++)
        //    {
        //        Program.DB.setValue(DB.type.ProjDB, "PV_Result", "번호,월," +
        //            "수직음영길이,수평음영길이,직달일사음영적용,음영계수," +
        //            "태양광전일사량,최대성능,평균효율,전기생산량",
        //            "'" + Num_textBox.Text + "','" + (mth + 1).ToString() + "월','" +
        //            PVhshobst_m[mth] + "','" + PVhshobstwid_m[mth] + "','" + Ishdirtotpvt_W[mth] + "','" + PVFshobstpvt[mth] + "','" +
        //           PVEsolm_kWh_m2[mth] + "','" + PVPpk_kW + "','" + PVefficiency + "','" + PVEelpvoutm_kWh[mth] + "'", "번호,월");

        //    }
        //}
        private void LoadGraph(String Orientation, String Slope)
        {
            try
            {
                string s = "", s2 = "";
                string[][] Location = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "지역", "");
                string[][] res1;
                string[][] res2;
                double max1=0, max2=0; 
                for (int mth = 0; mth < 11; mth++)
                {
                    s += PVEelpvoutm_kWh[mth] + ",";
                    res2 = Program.DB.querySQL(DB.type.BaseDB_HCneed, "SELECT 일사량 From 기후데이터_전일사량 Where 지역명 ='" + Location[0][0] + "' AND 방향='" + Orientation + "' And 각도='" + Slope + "'And 기간 ='" + (mth + 1) + "월'");
                    if (res2.Length > 0)
                    { s2 += Convert.ToDouble(res2[0][0]) + ","; }
                }

                s += PVEelpvoutm_kWh[11];
                res2 = Program.DB.querySQL(DB.type.BaseDB_HCneed, "SELECT 일사량 From 기후데이터_전일사량 Where 지역명 ='" + Location[0][0] + "' AND 방향='" + Orientation + "' And 각도='" + Slope + "'And 기간 ='" + (12) + "월'");
                if (res2.Length > 0)
                { s2 += Convert.ToDouble(res2[0][0]); }

                res2 = Program.DB.querySQL(DB.type.BaseDB_HCneed, "SELECT Max(일사량) From 기후데이터_전일사량 Where 지역명 ='" + Location[0][0] + "' AND 방향='" + Orientation + "' And 각도='" + Slope + "'and not 기간='연간값'");
                
                
                if (res2.Length > 0)
                {
                    int n2 =( (int)Convert.ToDouble(res2[0][0])).ToString().Length;
                    max2 = Convert.ToInt64(Convert.ToDouble(res2[0][0]) / Math.Pow(10, n2 - 1)) * Math.Pow(10, n2 - 1) + Math.Pow(10, n2 - 1)/2; 
                
                }
                int n1 = ((int)PVEelpvoutm_kWh.Max()).ToString().Length;
                max1 = Convert.ToInt64((PVEelpvoutm_kWh.Max())/ Math.Pow(10, n1 - 1)) * Math.Pow(10, n1 - 1) + Math.Pow(10, n1 - 1)/2;
                
                runScript("drawChart_pv([{type:\"line\",label:\"전기생산량\",data:[" + s + "],tension: 0.4,borderColor:\"#91D050\",backgroundColor:\"#91D050\",min:0,max:"+max1+"},{type:\"bar\",label:\"일사량(kWh/m²·mth)\",data:[" + s2 + "],borderColor:\"#000\",backgroundColor:\"#F2F2F2\",min:0,max:"+max2+",dash:false,barPercentage:0.7}])");
            }
            catch { }
        }
        //private void Reset()
        //{
        //    Name_textBox.Text = null;
        //    Name = null;

        //    VentilationType_comboBox.SelectedItem = null;
        //    VentilationType = null;

        //    PVsystem_combobox.SelectedItem = null;
        //    PVsystem = null;

        //    width_n_textBox.Text = null;
        //    width_n = 0;

        //    height_n_textBox.Text = null;
        //    height_n = 0; //가로, 세로 개수

        //    allcapacity_textBox.Text = null;
        //    PVcapacity_Kw = 0; // 설치용량

        //    PVArea_m2_textBox.Text = null;
        //    PVArea_m2 = 0; //총면적

        //    orientation_comboBox.SelectedItem = null;
        //    Orientation = null;

        //    slope_comboBox.SelectedItem = null;
        //    Slope = null; //방위, 경사

        //    PVLshobst_m_textBox.Text = null;
        //    PVLshobst_m = 0;

        //    PVHshobst_m_textBox.Text = null;
        //    PVHshobst_m = 0; //지형물까지의 거리, 지형물의 높이

        //    PVModule_textBox.Text = null;
        //    PVModuleNumber = null;
        //    PVModule = null;
        //    PVmanu_year = null;
        //    PVKpk_kW_m2 = 0;
        //    PVwidth_m = 0;
        //    PVheight_m = 0;
        //    PVPn_W = 0;
        //    PVmanuyearfa = 0;

        //    Inverter_textBox.Text = null;
        //    PVInverterNumber = null;
        //    Inverter = null;
        //    InverterEfficiency = 0;

        //    Battery_textBox.Text = null;
        //    PVBatteryNumber = null;
        //    Battery = null;
        //    PVV_V = 0;
        //    PVAH_Ah = 0;
        //    Batterycapacity = 0;

        //    PVfperf = 0;

        //    Array.Clear(PVIs_W_m2, 0, 12);
        //    PVIref_kW_m2 = 1;

        //    PVLPVwid_m = 0;
        //    PVLPVlen_m = 0;
        //    Array.Clear(PVIdirtot_W_m2, 0, 12);
        //    Array.Clear(PVIdiftot_W_m2, 0, 12);
        //    Array.Clear(PVαsol, 0, 12);

        //    //일사량_kWh/(m2.month)
        //    Array.Clear(PVEsolm_kWh_m2, 0, 12);

        //    //최대출력
        //    PVPpk_kW = 0;

        //    //생성된 전기에너지
        //    Array.Clear(PVEelpvoutm_kWh, 0, 12);  //월별
        //    Array.Clear(PVEelpvoutm_kWh_m2, 0, 12); //단위당
        //    PVEelpvouta_kWh_a = 0; //연간
        //    PVefficiency = 0; //평균효율
        //                      //음영감소계수
        //    Array.Clear(PVFshobstpvt, 0, 12);
        //    Array.Clear(PVhshobst_m, 0, 12);
        //    Array.Clear(PVhshobstwid_m, 0, 12);
        //    Array.Clear(Ishdirtotpvt_W, 0, 12);
        //    Esolm = 0; Ppk = 0; Eelpvoutm = 0; Fshobstpvt = 0; hshobst = 0; hshobstwid = 0; Ishdir = 0;
        //}

        //private void Save_button_Click(object sender, EventArgs e)
        //{
        //    Save();
        //    LoadGraph(Orientation, Slope);
        //    MessageBox.Show("저장 되었습니다.");
        //}
        public void LoadData(String ID)            // 리스트에서 항목 더블 클릭시 - 뷰를 ID 의 getValue 값으로 채우기
        {
            //Reset();

            Num_textBox.Text = ID;
            Num = ID;

            string[][] Value = Program.DB.getValue(DB.type.ProjDB, "PV_Form", "명칭", "번호='" + Num + "'");
            if (Value.Length > 0)
            {
                Name_textBox.Text = Value[0][0];
                Name = Value[0][0];
            }

            Value = Program.DB.getValue(DB.type.ProjDB, "PV_Form", "모듈번호,인버터번호,인버터명칭,인버터효율,배터리번호,배터리용량", "번호='" + Num + "'");
            if (Value.Length > 0)
            {
                PVModuleNumber = Value[0][0];
                Load_PV_Table();
                string[][] module = Program.DB.getValue(DB.type.ProjDB, "User_PVModule", "제품명,제작년도,가로길이,세로길이,정격출력,Kpk", "번호 = '" + PVModuleNumber + "'");
                if (module.Length > 0)
                {
                    PVModule_textBox.Text = module[0][0];
                    PVModule = module[0][0];
                    PVmanu_year = module[0][1];
                    PVwidth_m = Convert.ToDouble(module[0][2]);
                    PVheight_m = Convert.ToDouble(module[0][3]);
                    PVPn_W = Convert.ToDouble(module[0][4]);
                    PVKpk_kW_m2 = Convert.ToDouble(module[0][5]);

                    if (PVmanu_year == "25년 이내")
                    {
                        PVmanuyearfa = 1;
                    }
                    if (PVmanu_year == "25년 이상")
                    {
                        PVmanuyearfa = 0.9;
                    }
                }

                PVInverterNumber = Value[0][1];
                Inverter = Value[0][2];
                Inverter_textBox.Text = Inverter;
                InverterEfficiency = Convert.ToDouble(Value[0][3]);
                InverterEfficiency_textBox.Text = string.Format("{0:F2}", InverterEfficiency);

                PVBatteryNumber = Value[0][4];
                Batterycapacity = Convert.ToDouble(Value[0][5]);
                Batterycapacity_textBox.Text = string.Format("{0:F2}", Batterycapacity);
                string[][] battery = Program.DB.getValue(DB.type.ProjDB, "User_PVBattery", "제품명,전력,암페어시,배터리타입", "번호 ='" + PVBatteryNumber + "'");
                if (battery.Length > 0)
                {
                    Battery_textBox.Text = battery[0][0];
                    Battery = battery[0][0];
                    PVV_V = Convert.ToDouble(battery[0][1]);
                    PVAH_Ah = Convert.ToDouble(battery[0][2]);
                }
            }

            for (int mth = 0; mth < 12; mth++)
            {
                Value = Program.DB.getValue(DB.type.ProjDB, "PV_Result", "수직음영길이,수평음영길이,직달일사음영적용,음영계수,태양광전일사량,최대성능,평균효율,전기생산량", "번호='" + Num + "' And 월 ='" + (mth + 1).ToString() + "월'");
                if (Value.Length > 0)
                {
                    PVhshobst_m[mth] = Convert.ToDouble(Value[0][0]);
                    PVhshobstwid_m[mth] = Convert.ToDouble(Value[0][1]);
                    Ishdirtotpvt_W[mth] = Convert.ToDouble(Value[0][2]);
                    PVFshobstpvt[mth] = Convert.ToDouble(Value[0][3]);
                    PVEsolm_kWh_m2[mth] = Convert.ToDouble(Value[0][4]);
                    PVPpk_kW = Convert.ToDouble(Value[0][5]);
                    PVefficiency = Convert.ToDouble(Value[0][6]);
                    PVEelpvoutm_kWh[mth] = Convert.ToDouble(Value[0][7]);
                }
            }

            averagecpacity_textBox.Text = string.Format("{0:F2}", PVefficiency * 100);
            //LoadGraph(Orientation, Slope);
        }
        public static bool OnLoadListProc(Form form)
        {
            List_PV f = (List_PV)form;
            f.load_List();
            return true;
        }
        public void ResetForm(String ID) // 리스트에서 추가 버튼 클릭시 - 뷰 초기화
        {
            Num_textBox.Text = ID;
            Num = ID;
        }

        private void Previous_button_Click(object sender, EventArgs e)
        {
            if ((MessageBox.Show("이전 화면으로 이동하시겠습니까?", "이전 화면 이동", MessageBoxButtons.YesNo) == DialogResult.Yes))
            {
                this.DialogResult = DialogResult.OK;
                this.Hide();
                Program.getMenuForm().DoLoadForm(53, OnLoadListProc);
            }
        }

    }
}
