using main.subcontents.ConstructionCW;
using main.subcontents.RESystem_PV;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace main.contents
{
    public partial class PV : Form
    {

        //RESystem RESystem;

        //double a;

        #region Main Form Variable

        //설치정보
        double PVwidthnum, PVheightnum; //가로, 세로 개수
        double PVcapacity_Kw; // 설치용량
        double PVArea_m2; //총면적
        string Orientation, Slope; //방위, 경사
        double PVLshobst_m, PVHshobst_m; //지형물까지의 거리, 지형물의 높이

        //태양광 계통도

        #endregion //Main Form Variable

        #region DB Variable

        //PVModuleDB
        string PVModuleNumber, PVModuleName, PVmanu_year, PVcelltype;
        double PVKpk_kW_m2, PVwidth_m, PVheight_m, PVPn_W, PVnumber;
        //index
        public double PVmanuyearfa;

        //PVInverterDB
        string PVInverterNumber, PVInvertername;
        double PVInverterηEU;

        //PVBatteryDB
        String PVBatteryNumber, PVbatteryname, PVbatterytype;
        double PVV_V, PVAH_Ah, PVCnenn_kW;

        #endregion / DB Variable

        #region Input Variable

        //[일반 정보 변수]
        //화면
        string ReEnergyNumber, PVname, RenewableEnergySourceType;
        string PVtype, PVsystem, PVbipvtype;

        //계산
        public double PVfperf;

        //[일사량 정보 변수]
        //database
        public double[] PVIs_W_m2 = new double[12];
        public double[] PVdmth = { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
        public double PVIref_kW_m2 = 1;

        //[배터리 정보 변수]
        //index
        public double PVηDoD, PVtDIS, PVηBatt;

        //[매칭계수 정보 변수]
        //database
        // public double[] PVEPusel_kWh = new double[12];
        public double[] PVEPusel_kWh = { 11303, 10060, 8361, 6905, 5697, 6601, 7208, 7659, 7015, 5833, 7655, 10053 }; //
        public double PVEPusel_kWh_a;

        //index
        public double PVκ = 1;
        public double PVn = 1;

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

        //배터리
        public double PVγQ, PVCeff, PVCQ;

        //계통연계
        public double[] PVEprelusedEPus_kWh = new double[12];//월별
        public double PVEprelusedEPus_kWh_a;//연간
        public double PVfmatch, PVx;

        //독립형
        public double[] PVQfnutzPVi_kWh = new double[12]; //월별
        public double PVQfnutzPVi_kWh_a; //연간
        public double PVfBatt, PVQbattlossa_kWh;

        //그리드이동 전기에너지
        public double[] PVEexpelgrid_kWh = new double[12]; //월별
        public double PVEexpelgrid_kWh_a; //연간

        //음영감소계수
        public double[] PVFshobstpvt = new double[12];
        public double[] PVhshobst_m = new double[12];
        public double[] PVhshobstwid_m = new double[12];
        public double[] Ishdirtotpvt_W = new double[12];

        #endregion / Calculation Variable

        String[][] 지역;

        public PV()
        {
            InitializeComponent();

            #region getvalue

            지역 = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "지역", "");

            string[][] Image = Program.DB.getValue(DB.type.BaseDB_HCneed, "메뉴아이콘", "하위메뉴아이콘", "하위메뉴명 = '태양광시스템'");
            pictureBox1.Load(Program.gPath + Image[0][0]);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;

            #endregion / getvalue

            #region combobox

            //자동으로 콤보박스 불러오기
            PVsystem_Combobox.Items.Add("계통연계형");
            PVsystem_Combobox.Items.Add("독립형");

            PVtype_Combobox.Items.Add("고정식");
            PVtype_Combobox.Items.Add("추적식");
            PVtype_Combobox.Items.Add("BIPV");

            BIPVType_Combobox.Items.Add("외벽");
            BIPVType_Combobox.Items.Add("지붕");
            BIPVType_Combobox.Items.Add("창호");
            BIPVType_Combobox.Items.Add("커튼월창");
            BIPVType_Combobox.Items.Add("루버형");
            BIPVType_Combobox.Items.Add("블라인드형");

            slope.Items.Add("0˚");
            slope.Items.Add("30˚");
            slope.Items.Add("45˚");
            slope.Items.Add("60˚");
            slope.Items.Add("90˚");

            orientation.Items.Add("수평");
            orientation.Items.Add("남");
            orientation.Items.Add("남동");
            orientation.Items.Add("남서");
            orientation.Items.Add("동");
            orientation.Items.Add("서");
            orientation.Items.Add("북서");
            orientation.Items.Add("북동");
            orientation.Items.Add("북");

            #endregion / combobox

        }


        private void GeneralPanel_Paint(object sender, PaintEventArgs e)
        {
            Panel p = (Panel)sender;
            ControlPaint.DrawBorder(e.Graphics, p.DisplayRectangle, Color.FromArgb(153, 180, 209), ButtonBorderStyle.Solid);

            //RESystem = new RESystem();

            //a = RESystem.PVInverterηEU;
        }

        private void AdditionalPanel_Paint(object sender, PaintEventArgs e)
        {
            Panel p = (Panel)sender;
            ControlPaint.DrawBorder(e.Graphics, p.DisplayRectangle, Color.FromArgb(153, 180, 209), ButtonBorderStyle.Solid);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Panel p = (Panel)sender;
            ControlPaint.DrawBorder(e.Graphics, p.DisplayRectangle, Color.FromArgb(153, 180, 209), ButtonBorderStyle.Solid);
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {
            Panel p = (Panel)sender;
            ControlPaint.DrawBorder(e.Graphics, p.DisplayRectangle, Color.FromArgb(153, 180, 209), ButtonBorderStyle.Solid);
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            Panel p = (Panel)sender;
            ControlPaint.DrawBorder(e.Graphics, p.DisplayRectangle, Color.FromArgb(153, 180, 209), ButtonBorderStyle.Solid);
        }

        private void system_SelectedIndexChanged(object sender, EventArgs e)
        {
            PVsystem = PVsystem_Combobox.SelectedItem.ToString();

            if (PVsystem == "계통연계형")
            {
                batteryimage.Visible = false;
                Battery.Visible = false;
                Batteryname.Visible = false;
                BatteryDB_button.Visible = false;
                Batterycapacity_n.Visible = false;
                Batterycapacity.Visible = false;
                Batterycapacity_s.Visible = false;

                PVgrid.Visible = true;
                pvgrid_s.Visible = true;
                matchingfactor.Visible = true;
                matchingfacor_n.Visible = true;
            }
            if (PVsystem == "독립형")
            {
                batteryimage.Visible = true;
                Battery.Visible = true;
                Batteryname.Visible = true;
                BatteryDB_button.Visible = true;
                Batterycapacity_n.Visible = true;
                Batterycapacity.Visible = true;
                Batterycapacity_s.Visible = true;

                PVgrid.Visible = false;
                pvgrid_s.Visible = false;
                matchingfactor.Visible = false;
                matchingfacor_n.Visible = false;
            }
        }

        private void Type_SelectedIndexChanged(object sender, EventArgs e)
        {
            PVtype = PVtype_Combobox.SelectedItem.ToString();

            if (PVtype != null)
            {
                if (PVtype != "BIPV")
                {
                    BIPV.Visible = false;
                    BIPVType_Combobox.Visible = false;
                }
                else
                {
                    BIPV.Visible = true;
                    BIPVType_Combobox.Visible = true;
                }
            }
            Load_PVTypeimage();

        }

        private void obsheight_TextChanged(object sender, EventArgs e)
        {
            int result;

            if (int.TryParse(obsheight.Text, out result) == true)
            {
                PVHshobst_m = Convert.ToDouble(obsheight.Text);

                if (obsheight != null)
                {
                    obsheight_j.Text = obsheight.Text.ToString();
                }
            }
            else
            {
                MessageBox.Show("숫자를 입력하세요.");
            }

            PVShading_getvalue();

        }

        private void distance_TextChanged(object sender, EventArgs e)
        {
            int result;

            if (int.TryParse(distance.Text, out result) == true)
            {
                PVLshobst_m = Convert.ToDouble(distance.Text);

                if (distance != null)
                {
                    distance_j.Text = distance.Text.ToString();
                }
            }
            else
            {
                MessageBox.Show("숫자를 입력하세요.");
            }

            PVShading_getvalue();

        }

        private void Load_PVTypeimage()
        {
            if (PVtype_Combobox.SelectedItem == "고정식" || PVtype_Combobox.SelectedItem == "추적식")
            {
                string[][] Image = Program.DB.getValue(DB.type.BaseDB_RESystem, "태양광타입별이미지", "이미지", "대분류 = '" + "고정식" + "'AND 종류 = '" + "60˚" + "'");
                PVTypeBox.Load(Program.gPath + Image[0][0]);

                if (slope.SelectedItem != null)
                {
                    string[][] Image1 = Program.DB.getValue(DB.type.BaseDB_RESystem, "태양광타입별이미지", "이미지", "대분류 = '" + "고정식" + "'AND 종류 = '" + slope.SelectedItem + "'");
                    PVTypeBox.Load(Program.gPath + Image1[0][0]);
                }
            }

            if (PVtype_Combobox.SelectedItem == "BIPV")
            {
                string[][] Image = Program.DB.getValue(DB.type.BaseDB_RESystem, "태양광타입별이미지", "이미지", "대분류 = '" + "BIPV" + "'AND 종류 = '" + "외벽" + "'");
                PVTypeBox.Load(Program.gPath + Image[0][0]);

                if (BIPVType_Combobox.SelectedItem != null)
                {
                    string[][] Image1 = Program.DB.getValue(DB.type.BaseDB_RESystem, "태양광타입별이미지", "이미지", "대분류 = '" + "BIPV" + "'AND 종류 = '" + BIPVType_Combobox.SelectedItem + "'");
                    PVTypeBox.Load(Program.gPath + Image1[0][0]);
                }
            }
        }

        private void PVModuleType_TextChanged(object sender, EventArgs e)
        {
            if (PVModuleType.Text == null || PVModuleType.Text != "단결정(Single Cry. Si.)" || PVModuleType.Text != "다결정(Poly Cry. Si.)" || PVModuleType.Text != "비결정질 Si 박막" || PVModuleType.Text != "그외 Si 박막" || PVModuleType.Text != "CIGS 박막" || PVModuleType.Text != "CdTe 박막")
            {
                install.Text = "설치 개수";
                width.Visible = true;

                height.Text = "세로";
                height.ForeColor = SystemColors.ControlText;

                height_n.Visible = true;
            }

            if (PVModuleType.Text == "단결정(Single Cry. Si.)" || PVModuleType.Text == "다결정(Poly Cry. Si.)" || PVModuleType.Text == "비결정질 Si 박막" || PVModuleType.Text == "그외 Si 박막" || PVModuleType.Text == "CIGS 박막" || PVModuleType.Text == "CdTe 박막")
            {
                install.Text = "설치 용량";
                width.Visible = false;

                height.Text = "kW";
                height.ForeColor = SystemColors.ControlDark;

                height_n.Visible = false;
            }
        }

        private void PVDB_button_Click(object sender, EventArgs e)
        {
            PV_ModuleDB PV_ModuleDB_form = new PV_ModuleDB("장비일람표 DB");

            DialogResult result = PV_ModuleDB_form.ShowDialog();
            if (result == DialogResult.OK)
            {
                PVModuleNumber = PV_ModuleDB_form.Select_PVModule[0];
                PVModuleName = PV_ModuleDB_form.Select_PVModule[2];
                PVmanu_year = PV_ModuleDB_form.Select_PVModule[4];
                PVcelltype = PV_ModuleDB_form.Select_PVModule[5];
                if (PVModuleName == "단결정(Single Cry. Si.)" || PVModuleName == "다결정(Poly Cry. Si.)" || PVModuleName == "비결정질 Si 박막" || PVModuleName == "그외 Si 박막" || PVModuleName == "CIGS 박막" || PVModuleName == "CdTe 박막")
                {
                    PVKpk_kW_m2 = Convert.ToDouble(PV_ModuleDB_form.Select_PVModule[6]);
                }
                else
                {
                    PVKpk_kW_m2 = Convert.ToDouble(PV_ModuleDB_form.Select_PVModule[6]);
                    PVwidth_m = Convert.ToDouble(PV_ModuleDB_form.Select_PVModule[7]);
                    PVheight_m = Convert.ToDouble(PV_ModuleDB_form.Select_PVModule[8]);
                    PVPn_W = Convert.ToDouble(PV_ModuleDB_form.Select_PVModule[9]);
                }
            }

            PVModuleType.Text = PVModuleName;
            PVEfficiency.Text = string.Format("{0:F2}", PVKpk_kW_m2 * 100);

            PV_MainForm_Calculation_TotalArea();// 전체면적 계산
            PV_MainForm_Calculation_TotalCapacity(); //설치용량 계산

        }

        private void BIPVType_SelectedIndexChanged(object sender, EventArgs e)
        {
            PVbipvtype = BIPVType_Combobox.SelectedItem.ToString();
            Load_PVTypeimage();
        }

        private void slope_SelectedIndexChanged(object sender, EventArgs e)
        {
            Slope = slope.SelectedItem.ToString();
            Load_PVTypeimage();
            PVIs_W_m2_getvalue();
        }

        private void InverterDB_button_Click(object sender, EventArgs e)
        {
            PV_InverterDB PV_InverterDB_form = new PV_InverterDB();

            DialogResult result = PV_InverterDB_form.ShowDialog();
            if (result == DialogResult.OK)
            {
                PVInverterNumber = PV_InverterDB_form.Select_PVInverter[0];
                PVInvertername = PV_InverterDB_form.Select_PVInverter[2];
                PVInverterηEU = Convert.ToDouble(PV_InverterDB_form.Select_PVInverter[4]);
            }

            Inverter.Text = PVInvertername;
            InverterEfficiency.Text = string.Format("{0:F2}", PVInverterηEU);
        }

        private void BatteryDB_button_Click(object sender, EventArgs e)
        {
            PV_BatteryDB PV_BatteryDB_form = new PV_BatteryDB();

            DialogResult result = PV_BatteryDB_form.ShowDialog();
            if (result == DialogResult.OK)
            {
                PVBatteryNumber = PV_BatteryDB_form.Select_PVBattery[0];
                PVbatteryname = PV_BatteryDB_form.Select_PVBattery[2];
                PVV_V = Convert.ToDouble(PV_BatteryDB_form.Select_PVBattery[4]);
                PVAH_Ah = Convert.ToDouble(PV_BatteryDB_form.Select_PVBattery[5]);
                PVbatterytype = PV_BatteryDB_form.Select_PVBattery[6];
            }

            Batteryname.Text = PVbatteryname;
            PVCnenn_kW = Convert.ToDouble(PVV_V) * Convert.ToDouble(PVAH_Ah) / 1000;
            if (PVCnenn_kW == 0)
            { }
            else
            {
                Batterycapacity.Text = string.Format("{0:F2}", PVCnenn_kW);
            }
        }

        private void width_n_TextChanged_1(object sender, EventArgs e)
        {
            int result;

            if (int.TryParse(width_n.Text, out result) == true)
            {
                PVwidthnum = Convert.ToDouble(width_n.Text);
                PV_MainForm_Calculation_TotalArea(); //총면적 계산
                PV_MainForm_Calculation_TotalCapacity(); //설치용량 계산
            }
            else
            {
                MessageBox.Show("숫자를 입력하세요.");
            }
        }

        private void PV_MainForm_Calculation_TotalArea()
        {
            if (PVModuleType.Text == "단결정(Single Cry. Si.)" || PVModuleType.Text == "다결정(Poly Cry. Si.)" || PVModuleType.Text == "비결정질 Si 박막" || PVModuleType.Text == "그외 Si 박막" || PVModuleType.Text == "CIGS 박막" || PVModuleType.Text == "CdTe 박막")
            {
                if (width_n.Text != "")
                {
                    PVcapacity_Kw = Convert.ToDouble(width_n.Text);
                    PVArea_m2 = PVcapacity_Kw / PVKpk_kW_m2;
                }
            }
            else
            {
                if (width_n.Text != "" && height_n.Text != "")
                {
                    PVArea_m2 = PVwidth_m * PVheight_m * PVwidthnum * PVheightnum;
                }
            }

            if (width_n.Text != "" || height_n.Text != "")
            {
                textBox13.Text = string.Format("{0:F2}", PVArea_m2);//총 면적 넣기
            }
            else { textBox13.Text = ""; }

        }

        private void PV_MainForm_Calculation_TotalCapacity()
        {
            if (PVModuleType.Text == "단결정(Single Cry. Si.)" || PVModuleType.Text == "다결정(Poly Cry. Si.)" || PVModuleType.Text == "비결정질 Si 박막" || PVModuleType.Text == "그외 Si 박막" || PVModuleType.Text == "CIGS 박막" || PVModuleType.Text == "CdTe 박막")
            {
                if (width_n.Text != "")
                {
                    allcapacity.Text = string.Format("{0:F2}", PVwidthnum);
                }
                else { }
            }
            else
            {
                if (width_n.Text != "" && height_n.Text != "")
                {
                    allcapacity.Text = string.Format("{0:F2}", PVwidthnum * PVheightnum * PVPn_W / 1000);
                }
                else { }
            }
        }

        private void height_n_TextChanged_1(object sender, EventArgs e)
        {
            int result;

            if (int.TryParse(height_n.Text, out result) == true)
            {
                PVheightnum = Convert.ToDouble(height_n.Text);
                PV_MainForm_Calculation_TotalArea(); //총면적 계산
                PV_MainForm_Calculation_TotalCapacity(); //설치용량 계산
            }
            else
            {
                MessageBox.Show("숫자를 입력하세요.");
            }
        }

        private void orientation_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            Orientation = orientation.SelectedItem.ToString();

            PVIs_W_m2_getvalue();
        }

        private void panel2_Paint_1(object sender, PaintEventArgs e)
        {
            Panel p = (Panel)sender;
            ControlPaint.DrawBorder(e.Graphics, p.DisplayRectangle, Color.FromArgb(153, 180, 209), ButtonBorderStyle.Solid);
        }

        //getvalue
        public void PVIs_W_m2_getvalue()
        {
            if (Orientation != null && Slope != null)
            {
                //전일사량불러오기
                string[][] token = Program.DB.getValue(DB.type.BaseDB_HCneed, "기후데이터_전일사량", "일사량", "지역명 ='" + 지역[0][0] + "' AND 방향 ='" + Orientation + "' AND  각도 = '" + Slope + "'");
                for (int i = 0; i < 12; i++)
                {
                    PVIs_W_m2[i] = Convert.ToDouble(token[i][0]);
                }
            }
            else { }
        }

        public void PVShading_getvalue()
        {
            if (distance.Text != null && obsheight.Text != null)
            {
                //직달일사량불러오기
                string[][] token1 = Program.DB.getValue(DB.type.BaseDB_HCneed, "기후데이터_직달일사량", "일사량", "지역명 ='" + 지역[0][0] + "' AND 방향 ='" + Orientation + "' AND  각도 = '" + Slope + "'");
                //확산일사량불러오기
                string[][] token2 = Program.DB.getValue(DB.type.BaseDB_HCneed, "기후데이터_산란일사량", "일사량", "지역명 ='" + 지역[0][0] + "' AND 방향 ='" + Orientation + "' AND  각도 = '" + Slope + "'");
                //태양고도각 불러오기
                string[][] token3 = Program.DB.getValue(DB.type.BaseDB_HCneed, "기후데이터_고도각", "고도각", "지역명 ='" + 지역[0][0] + "' AND 방향 ='" + Orientation + "' AND  각도 = '" + Slope + "'");

                for (int i = 0; i < 12; i++)
                {
                    PVIdirtot_W_m2[i] = Convert.ToDouble(token1[i][0]);
                    PVIdiftot_W_m2[i] = Convert.ToDouble(token2[i][0]);
                    PVαsol[i] = Convert.ToDouble(token3[i][0]);
                }
            }
            else { }
        }

        //태양광 계산 파트
        public void PVcaculation()
        {
            calculation_PVhshobst_m();
            calculation_PVhshobstwid_m();
            calculation_Ishdirtotpvt_W();
            calculation_PVFshobstpvt();
            calculation_Esolm_kWh();
            calculation_Ppk_kW();
            calculation_Eelpvoutm_kWh();
            //calculation_PVEelpvoutm_kWh_m2();
            calculation_PVEelpvouta_kWh();
            calculation_PVefficiency();
            calculation_PVγQ();
            calculation_PVCeff();
            calculation_PVCQ();
            calculation_PVfmatch();
            calculation_PVEprelusedEPus_kWh();
            calculation_PVfBatt_kWh();
            calculation_PVQfnutzPVi_kWh();
            calculation_PVEexpelgrid_kWh();
        }

        #region Calculation

        Re_FC_Energy_kWh use = new Re_FC_Energy_kWh();

        //수직음영길이
        public void calculation_PVhshobst_m()
        {
            PVLPVlen_m = PVheight_m * PVheightnum;

            if (PVModuleName == "단결정(Single Cry. Si.)" || PVModuleName == "다결정(Poly Cry. Si.)" || PVModuleName == "비결정질 Si 박막" || PVModuleName == "그외 Si 박막" || PVModuleName == "CIGS 박막" || PVModuleName == "CdTe 박막")
            {
                PVLPVlen_m = Math.Sqrt(PVArea_m2);
            }

            PVLPVwid_m = PVArea_m2 / PVLPVlen_m;

            for (int i = 0; i < 12; i++)
            {
                PVhshobst_m[i] = use.Re_PV_hshobst_m(PVLPVlen_m, PVHshobst_m, PVLshobst_m, PVαsol[i]);
            }
        }

        //수평음영길이
        public void calculation_PVhshobstwid_m()
        {
            for (int i = 0; i < 12; i++)
            {
                PVhshobstwid_m[i] = use.Re_PV_hshobstwid_m(PVhshobst_m[i], PVαsol[i]);
            }
        }

        //직달일사 음영적용
        public void calculation_Ishdirtotpvt_W()
        {
            for (int i = 0; i < 12; i++)
            {
                Ishdirtotpvt_W[i] = use.Re_PV_Ishdir_m(PVArea_m2, PVLPVlen_m, PVhshobst_m[i], Slope, PVhshobstwid_m[i], PVLPVwid_m, PVIdirtot_W_m2[i]);
            }

        }

        //음영계수
        public void calculation_PVFshobstpvt()
        {

            for (int i = 0; i < 12; i++)
            {
                PVFshobstpvt[i] = use.Re_PV_Fshobstpvt_(PVIdirtot_W_m2[i], PVIdiftot_W_m2[i], Ishdirtotpvt_W[i], PVArea_m2);
            }

        }

        //태양광 모듈에 들어오는 전일사량
        public void calculation_Esolm_kWh()
        {

            for (int i = 0; i < 12; i++)
            {
                PVEsolm_kWh_m2[i] = use.Re_PV_Esolm_kWh(PVIs_W_m2[i], PVdmth[i], PVFshobstpvt[i]);
            }

        }

        //표준-테스트-조건에서 최대성능 
        public void calculation_Ppk_kW()
        {
            if (PVmanu_year == "25년 이내")
            {
                PVmanuyearfa = 1;
            }
            if (PVmanu_year == "25년 이상")
            {
                PVmanuyearfa = 0.9;
            }
            PVPpk_kW = use.Re_PV_Ppk_kW(PVKpk_kW_m2, PVArea_m2, PVmanuyearfa);
        }

        //태양광 시스템에 의해 생성된 전기 에너지
        public void calculation_Eelpvoutm_kWh()
        {
            if (PVtype == "고정식" || PVtype == "추적식")
            {
                PVfperf = 0.82;
            }
            else if (PVtype == "BIPV")
            {
                if (PVbipvtype == "외벽" || PVbipvtype == "지붕" || PVbipvtype == "창호" || PVbipvtype == "커튼월창")
                {
                    PVfperf = 0.80;
                }
                else if (PVbipvtype == "루버형" || PVbipvtype == "블라인드형")
                {
                    PVfperf = 0.82;
                }
            }

            for (int i = 0; i < 12; i++)
            {
                PVEelpvoutm_kWh[i] = use.Re_PV_Eelpvoutm_kWh(PVEsolm_kWh_m2[i], PVPpk_kW, PVfperf, PVInverterηEU, PVIref_kW_m2);
                PVEelpvoutm_kWh_m2[i] = PVEelpvoutm_kWh[i] / PVArea_m2;
            }
        }

        ////태양광 시스템에 의해 생성된 전기 에너지(단위당)
        //public void calculation_PVEelpvoutm_kWh_m2()
        //{
        //   for (int i = 0; i < 12; i++)
        //    {
        //        PVEelpvoutm_kWh_m2[i] = PVEelpvoutm_kWh[i] / PVArea_m2;
        //    }
        //}

        //태양광 시스템에 의해 생성된 전기 에너지 연간 전기 에너지
        public void calculation_PVEelpvouta_kWh()
        {
            for (int i = 0; i < 12; i++)
            {
                PVEelpvouta_kWh_a += PVEelpvoutm_kWh[i];
            }
        }

        //평균효율
        public void calculation_PVefficiency()
        {
            double PVEelpvoutm_kWhaver = PVEelpvoutm_kWh.Average();
            double PVEsolm_kWh_m2aver = PVEsolm_kWh_m2.Average();

            PVefficiency = PVEelpvoutm_kWhaver / PVEsolm_kWh_m2aver / PVArea_m2;

        }

        //발전기 규격에 대한 지수
        public void calculation_PVγQ()
        {
            if (PVsystem == "독립형")
            {
                for (int i = 0; i < 12; i++)
                {
                    PVEPusel_kWh_a += PVEPusel_kWh[i];
                }

                PVγQ = use.Re_PV_γQ_kW_MWh_a(PVPpk_kW, PVEPusel_kWh_a);
            }

        }

        //배터리 용량
        public void calculation_PVCeff()
        {

            if (PVsystem == "독립형")
            {
                if (PVbatterytype == "리튬 및 리튬 결합")
                {
                    PVηDoD = 0.83;
                }
                if (PVbatterytype == "니켈-철")
                {
                    PVηDoD = 0.7;
                }
                if (PVbatterytype == "납 및 납젤")
                {
                    PVηDoD = 0.48;
                }

                PVCnenn_kW = PVV_V * PVAH_Ah / 1000;

                PVCeff = use.Re_PV_Ceff_kWh(PVCnenn_kW, PVηDoD);
            }
        }

        //배터리 규격에 대한 지표
        public void calculation_PVCQ()
        {
            if (PVsystem == "독립형")
            {
                PVCQ = use.Re_PV_CQ_kWh(PVCeff, PVEPusel_kWh_a);
            }
        }

        //소요량에 대한 생산량의 비
        public void calculation_PVfmatch()
        {
            for (int i = 0; i < 12; i++)
            {
                PVEPusel_kWh_a += PVEPusel_kWh[i];
            }

            PVx = use.Re_PV_x_kWh(PVEelpvouta_kWh_a, PVEPusel_kWh_a);
            PVfmatch = use.Re_PV_fmatch_kWh(PVx, PVn, PVκ);
        }

        //'계통연계시 이용된 월별 에너지량
        public void calculation_PVEprelusedEPus_kWh()
        {
            for (int i = 0; i < 12; i++)
            {
                PVEprelusedEPus_kWh[i] = use.Re_PV_EprelusedEPus_kWh(PVfmatch, PVEelpvoutm_kWh[i]);
                PVEprelusedEPus_kWh_a += PVEprelusedEPus_kWh[i];
            }
        }

        //배터리에 대한 수정계수
        public void calculation_PVfBatt_kWh()
        {
            if (PVsystem == "독립형")
            {
                PVfBatt = use.Re_PV_fBatt_kWh(PVγQ, PVCQ);
            }
        }

        //독립형시 이용된 월별 에너지량
        public void calculation_PVQfnutzPVi_kWh()
        {
            if (PVsystem == "독립형")
            {
                for (int i = 0; i < 12; i++)
                {
                    PVQfnutzPVi_kWh[i] = Math.Min(use.Re_PV_QfnutzPVi_kWh(PVfBatt, PVEprelusedEPus_kWh[i]), PVEelpvoutm_kWh[i]);
                    PVQfnutzPVi_kWh_a += PVQfnutzPVi_kWh[i];
                }
            }
        }

        //계통연계시 그리드로 이동하는 에너지량
        public void calculation_PVEexpelgrid_kWh()
        {
            if (PVsystem == "계통연계형")
            {
                for (int i = 0; i < 12; i++)
                {
                    PVEexpelgrid_kWh[i] = PVEelpvoutm_kWh[i] - PVEprelusedEPus_kWh[i];
                    PVEexpelgrid_kWh_a += PVEexpelgrid_kWh[i];
                }
            }
        }

        private void Caculation_Button_Click(object sender, EventArgs e)
        {
            #region Reset

            Calculation_Reset();

            #endregion / Reset

            #region Calculation

            if (BIPVType_Combobox.Visible != false && Batteryname.Visible != false && height_n.Visible != false) //1
            {
                if (PVbipvtype != "" && Batteryname.Text != "" && height_n.Text != "" && PVtype != "" && PVsystem != "" && PVModuleType.Text != "" && Inverter.Text != "" && width_n.Text != "" && Orientation != "" && Slope != "" && distance.Text != "" && obsheight.Text != "")
                {
                    calculation_PVhshobst_m();
                    calculation_PVhshobstwid_m();
                    calculation_Ishdirtotpvt_W();
                    calculation_PVFshobstpvt();
                    calculation_Esolm_kWh();
                    calculation_Ppk_kW();
                    calculation_Eelpvoutm_kWh();
                    //calculation_PVEelpvoutm_kWh_m2();
                    calculation_PVEelpvouta_kWh();
                    calculation_PVefficiency();
                    calculation_PVγQ();
                    calculation_PVCeff();
                    calculation_PVCQ();
                    calculation_PVfBatt_kWh();
                    calculation_PVfmatch();
                    calculation_PVEprelusedEPus_kWh();
                    calculation_PVQfnutzPVi_kWh();
                    calculation_PVEexpelgrid_kWh();
                }
                else
                {
                    MessageBox.Show("모든 입력값을 입력하세요.");
                }
            }
            else if (Batteryname.Visible == false && BIPVType_Combobox.Visible != false && height_n.Visible != false) //2
            {
                if (PVtype != "" && PVsystem != "" && PVbipvtype != "" && PVModuleType.Text != "" && Inverter.Text != "" && width_n.Text != "" && height_n.Text != "" && Orientation != "" && Slope != "" && distance.Text != "" && obsheight.Text != "")
                {
                    calculation_PVhshobst_m();
                    calculation_PVhshobstwid_m();
                    calculation_Ishdirtotpvt_W();
                    calculation_PVFshobstpvt();
                    calculation_Esolm_kWh();
                    calculation_Ppk_kW();
                    calculation_Eelpvoutm_kWh();
                    //calculation_PVEelpvoutm_kWh_m2();
                    calculation_PVEelpvouta_kWh();
                    calculation_PVefficiency();
                    calculation_PVγQ();
                    calculation_PVCeff();
                    calculation_PVCQ();
                    calculation_PVfmatch();
                    calculation_PVEprelusedEPus_kWh();
                    calculation_PVfBatt_kWh();
                    calculation_PVQfnutzPVi_kWh();
                    calculation_PVEexpelgrid_kWh();
                }
                else
                {
                    MessageBox.Show("모든 입력값을 입력하세요.");
                }
            }
            else if (Batteryname.Visible != false && BIPVType_Combobox.Visible == false && height_n.Visible != false) //3
            {
                if (PVtype != "" && PVsystem != "" && PVModuleType.Text != "" && Inverter.Text != "" && Batteryname.Text != "" && width_n.Text != "" && height_n.Text != "" && Orientation != "" && Slope != "" && distance.Text != "" && obsheight.Text != "")
                {
                    calculation_PVhshobst_m();
                    calculation_PVhshobstwid_m();
                    calculation_Ishdirtotpvt_W();
                    calculation_PVFshobstpvt();
                    calculation_Esolm_kWh();
                    calculation_Ppk_kW();
                    calculation_Eelpvoutm_kWh();
                    //calculation_PVEelpvoutm_kWh_m2();
                    calculation_PVEelpvouta_kWh();
                    calculation_PVefficiency();
                    calculation_PVγQ();
                    calculation_PVCeff();
                    calculation_PVCQ();
                    calculation_PVfmatch();
                    calculation_PVEprelusedEPus_kWh();
                    calculation_PVfBatt_kWh();
                    calculation_PVQfnutzPVi_kWh();
                    calculation_PVEexpelgrid_kWh();
                }
                else
                {
                    MessageBox.Show("모든 입력값을 입력하세요.");
                }
            }
            else if (Batteryname.Visible != false && BIPVType_Combobox.Visible != false && height_n.Visible == false) //4
            {
                if (PVtype != "" && PVsystem != "" && PVbipvtype != "" && PVModuleType.Text != "" && Inverter.Text != "" && Batteryname.Text != "" && width_n.Text != "" && Orientation != "" && Slope != "" && distance.Text != "" && obsheight.Text != "")
                {
                    calculation_PVhshobst_m();
                    calculation_PVhshobstwid_m();
                    calculation_Ishdirtotpvt_W();
                    calculation_PVFshobstpvt();
                    calculation_Esolm_kWh();
                    calculation_Ppk_kW();
                    calculation_Eelpvoutm_kWh();
                    //calculation_PVEelpvoutm_kWh_m2();
                    calculation_PVEelpvouta_kWh();
                    calculation_PVefficiency();
                    calculation_PVγQ();
                    calculation_PVCeff();
                    calculation_PVCQ();
                    calculation_PVfBatt_kWh();
                    calculation_PVfmatch();
                    calculation_PVEprelusedEPus_kWh();
                    calculation_PVQfnutzPVi_kWh();
                    calculation_PVEexpelgrid_kWh();
                }
                else
                {
                    MessageBox.Show("모든 입력값을 입력하세요.");
                }
            }
            if (Batteryname.Visible == false && BIPVType_Combobox.Visible == false && height_n.Visible != false) //5
            {
                if (PVtype != "" && PVsystem != "" && PVModuleType.Text != "" && Inverter.Text != "" && width_n.Text != "" && height_n.Text != "" && Orientation != "" && Slope != "" && distance.Text != "" && obsheight.Text != "")
                {
                    calculation_PVhshobst_m();
                    calculation_PVhshobstwid_m();
                    calculation_Ishdirtotpvt_W();
                    calculation_PVFshobstpvt();
                    calculation_Esolm_kWh();
                    calculation_Ppk_kW();
                    calculation_Eelpvoutm_kWh();
                    //calculation_PVEelpvoutm_kWh_m2();
                    calculation_PVEelpvouta_kWh();
                    calculation_PVefficiency();
                    calculation_PVγQ();
                    calculation_PVCeff();
                    calculation_PVCQ();
                    calculation_PVfBatt_kWh();
                    calculation_PVfmatch();
                    calculation_PVEprelusedEPus_kWh();
                    calculation_PVQfnutzPVi_kWh();
                    calculation_PVEexpelgrid_kWh();
                }
                else
                {
                    MessageBox.Show("모든 입력값을 입력하세요.");
                }
            }
            if (Batteryname.Visible == false && BIPVType_Combobox.Visible != false && height_n.Visible == false) //6
            {
                if (PVtype != "" && PVsystem != "" && PVbipvtype != "" && PVModuleType.Text != "" && Inverter.Text != "" && width_n.Text != "" && Orientation != "" && Slope != "" && distance.Text != "" && obsheight.Text != "")
                {
                    calculation_PVhshobst_m();
                    calculation_PVhshobstwid_m();
                    calculation_Ishdirtotpvt_W();
                    calculation_PVFshobstpvt();
                    calculation_Esolm_kWh();
                    calculation_Ppk_kW();
                    calculation_Eelpvoutm_kWh();
                    //calculation_PVEelpvoutm_kWh_m2();
                    calculation_PVEelpvouta_kWh();
                    calculation_PVefficiency();
                    calculation_PVγQ();
                    calculation_PVCeff();
                    calculation_PVCQ();
                    calculation_PVfBatt_kWh();
                    calculation_PVfmatch();
                    calculation_PVEprelusedEPus_kWh();
                    calculation_PVQfnutzPVi_kWh();
                    calculation_PVEexpelgrid_kWh();
                }
                else
                {
                    MessageBox.Show("모든 입력값을 입력하세요.");
                }
            }
            if (Batteryname.Visible != false && BIPVType_Combobox.Visible == false && height_n.Visible == false) //7
            {
                if (PVtype != "" && PVsystem != "" && PVModuleType.Text != "" && Inverter.Text != "" && Batteryname.Text != "" && width_n.Text != "" && Orientation != "" && Slope != "" && distance.Text != "" && obsheight.Text != "")
                {
                    calculation_PVhshobst_m();
                    calculation_PVhshobstwid_m();
                    calculation_Ishdirtotpvt_W();
                    calculation_PVFshobstpvt();
                    calculation_Esolm_kWh();
                    calculation_Ppk_kW();
                    calculation_Eelpvoutm_kWh();
                    //calculation_PVEelpvoutm_kWh_m2();
                    calculation_PVEelpvouta_kWh();
                    calculation_PVefficiency();
                    calculation_PVγQ();
                    calculation_PVCeff();
                    calculation_PVCQ();
                    calculation_PVfBatt_kWh();
                    calculation_PVfmatch();
                    calculation_PVEprelusedEPus_kWh();
                    calculation_PVQfnutzPVi_kWh();
                    calculation_PVEexpelgrid_kWh();
                }
                else
                {
                    MessageBox.Show("모든 입력값을 입력하세요.");
                }
            }
            if (Batteryname.Visible == false && BIPVType_Combobox.Visible == false && height_n.Visible == false) //1
            {
                if (PVtype != "" && PVsystem != "" && PVModuleType.Text != "" && Inverter.Text != "" && width_n.Text != "" && Orientation != "" && Slope != "" && distance.Text != "" && obsheight.Text != "")
                {
                    calculation_PVhshobst_m();
                    calculation_PVhshobstwid_m();
                    calculation_Ishdirtotpvt_W();
                    calculation_PVFshobstpvt();
                    calculation_Esolm_kWh();
                    calculation_Ppk_kW();
                    calculation_Eelpvoutm_kWh();
                    //calculation_PVEelpvoutm_kWh_m2();
                    calculation_PVEelpvouta_kWh();
                    calculation_PVefficiency();
                    calculation_PVγQ();
                    calculation_PVCeff();
                    calculation_PVCQ();
                    calculation_PVfBatt_kWh();
                    calculation_PVfmatch();
                    calculation_PVEprelusedEPus_kWh();
                    calculation_PVQfnutzPVi_kWh();
                    calculation_PVEexpelgrid_kWh();
                }
                else
                {
                    MessageBox.Show("모든 입력값을 입력하세요.");
                }
            }

            #endregion / Calculation

            #region Form 

            PVproduction.Text = string.Format("{0:F2}", PVEelpvouta_kWh_a);
            averagecpacity.Text = string.Format("{0:F2}", PVefficiency * 100);

            if (PVsystem == "독립형")
            {
                PVusing.Text = string.Format("{0:F2}", PVQfnutzPVi_kWh_a);

            }
            else if (PVsystem == "계통연계형")
            {
                PVusing.Text = string.Format("{0:F2}", PVEprelusedEPus_kWh_a);
                PVgrid.Text = string.Format("{0:F2}", PVEexpelgrid_kWh_a);
                matchingfacor_n.Text = string.Format("{0:F2}", PVfmatch);
            }

            #endregion /Form
        }

        private void Calculation_Reset()
        {
            Array.Clear(PVEprelusedEPus_kWh, 0, 12);
            Array.Clear(PVQfnutzPVi_kWh, 0, 12);
            Array.Clear(PVEexpelgrid_kWh, 0, 12);
            Array.Clear(PVEelpvoutm_kWh, 0, 12);

            PVEPusel_kWh_a = 0;
            PVEprelusedEPus_kWh_a = 0;
            PVQfnutzPVi_kWh_a = 0;
            PVEexpelgrid_kWh_a = 0;
            PVEelpvouta_kWh_a = 0;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }

    #endregion / Calculation

    #region method

    public class Re_FC_Energy_kWh
    {
        double Esolm, Ppk, Eelpvoutm, γQ, Ceff, CQ, fBatt, EprelusedEPus, fmatch, x, QfnutzPVi, Fshobstpvt, hshobst, hshobstwid, Ishdir;


        //일사량
        public double Re_PV_Esolm_kWh(double Ls, double dmth, double Fshobstpvt)
        {
            Esolm = Ls * dmth * 24 / 1000 * Fshobstpvt;

            return Esolm;
        }

        //최대 성능
        public double Re_PV_Ppk_kW(double Kpk, double A, double manu_year_fa)
        {

            Ppk = Kpk * A * manu_year_fa;

            return Ppk;

        }

        //태양광을 통해 생성된 전기 에너지
        public double Re_PV_Eelpvoutm_kWh(double Esolm, double Ppk, double Fperf, double ηEU, double Iref)
        {

            Eelpvoutm = Esolm * Ppk * Fperf * (ηEU / 100) / Iref;

            return Eelpvoutm;

        }

        //발전기 규격에 대한 지수
        public double Re_PV_γQ_kW_MWh_a(double Ppk, double Qelges)
        {

            γQ = Ppk / Qelges * 1000;

            return γQ;

        }

        //배터리 용량
        public double Re_PV_Ceff_kWh(double Cnenm, double ηDoD)
        {
            Ceff = Cnenm * ηDoD;

            return Ceff;

        }

        //배터리 규격에 대한 지표
        public double Re_PV_CQ_kWh(double Ceff, double Qelgesa)
        {

            CQ = (Ceff / Qelgesa) * 1000;

            return CQ;

        }

        //배터리에 대한 수정계수
        public double Re_PV_fBatt_kWh(double γQ, double CQ)
        {

            fBatt = Math.Max(1, (0.2 * Math.Log(γQ) + 1.85) * Math.Pow(CQ, 0.1 * Math.Log(γQ) + 0.25));

            return fBatt;

        }

        //계통연계시 이용된 월별 에너지량
        public double Re_PV_EprelusedEPus_kWh(double fmatch, double Eprel)
        {

            EprelusedEPus = fmatch * Eprel;

            return EprelusedEPus;

        }

        //매칭계수
        public double Re_PV_fmatch_kWh(double x, double n, double k)
        {

            fmatch = (Math.Pow(x, n) + 1 / Math.Pow(x, n) - k) / (Math.Pow(x, n) + 1 / Math.Pow(x, n));

            return fmatch;

        }

        //소요량에 대한 생산량의 비
        public double Re_PV_x_kWh(double Eprel, double EPusel)
        {

            x = Eprel / EPusel;

            return x;

        }

        //독립형시 이용된 월별 에너지량
        public double Re_PV_QfnutzPVi_kWh(double fBatt, double QfnutzPVoBa)
        {

            QfnutzPVi = fBatt * QfnutzPVoBa;

            return QfnutzPVi;

        }

        //음영계수
        public double Re_PV_Fshobstpvt_(double Idir, double Idif, double Ishdir, double area)
        {

            Fshobstpvt = (Ishdir + Idif * area) / (Idir * area + Idif * area);

            return Fshobstpvt;

        }

        //수직음영길이
        public double Re_PV_hshobst_m(double LPVlen, double Hshobst, double Lshobst, double asol)
        {

            hshobst = Math.Min(LPVlen, Math.Max(0, Hshobst - Lshobst * Math.Tan(asol * Math.PI / 180.0)));

            return hshobst;

        }

        //수평음영길이
        public double Re_PV_hshobstwid_m(double hshobst, double asol)
        {

            hshobstwid = hshobst / Math.Tan(asol * Math.PI / 180.0);

            return hshobstwid;

        }

        //직달일사 음영 적용
        public double Re_PV_Ishdir_m(double area, double LPVlen, double hshobst, string β, double hshobstwid, double LPVwid, double Idir)
        {
            double β1;

            β = β.Substring(0, 2);
            β1 = Convert.ToDouble(β);

            Ishdir = (area - Math.Min(LPVlen, Math.Sqrt(Math.Pow((hshobst / ((Math.Tan(β1 * Math.PI / 180.0) + hshobst / hshobstwid))), 2) + Math.Pow((Math.Tan(β1 * Math.PI / 180.0) * (hshobst / (Math.Tan(β1 * Math.PI / 180.0) + hshobst / hshobstwid))), 2))) * LPVwid) * Idir;

            if (hshobst == 0)
            {
                Ishdir = (area - Math.Min(LPVlen, 0) * LPVwid) * Idir;
            }

            return Ishdir;
        }
    }

    #endregion / method

    //<0821. 다음 할일>
    //1. 태양광, 인버터, 배터리 DB 화면 만들기
    //1-1. 태양광 모듈 DB 화면 만들기(8월 31일 완료)
    //1-2. 인버터 DB 화면 만들기(9월 1일 완료)
    //1-3. 배터리 DB 화면 만들기(9월 1일 완료)

    //2. DB 화면과 PV 전체 화면과 연결(9월 5일 완료)

    //3. 계산식과 연결(9월 15일 완료)

    //4. 추가적인 진행은 List 이후에 진행
    //ex) save, reset, getvalue 등

}
