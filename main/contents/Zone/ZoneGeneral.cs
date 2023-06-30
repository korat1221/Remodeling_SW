using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace main.contents
{
    public partial class ZoneGeneral : Form
    {
        private String ZoneNum;

        double DHWneed_1p, DHWneed, UseTime, HCTime, AHUTime, PersonNum, Length, Depth, Area, CelingHeight, NetVolume, VentilationRate, VentilationVolume, AnnualUseDay, WeekUseDay;
        double PersonIHG_1day, PersonIHG, PersonIHG_Low, PersonIHG_Medium, PersonIHG_High; //PersonIHG 단위 : W/m2
        double EquipIHG_1day, EquipIHG, EquipIHG_Low, EquipIHG_Medium, EquipIHG_High, EquipIHG_Time; //EquipIHG 단위 : W/m2
        double theta_i_h_set, theta_i_c_set, Em, VA;
        double OccupancyDensity, OccupancyDensity_Low, OccupancyDensity_Medium, OccupancyDensity_High;
        String OccupancyDensity_index, EquipIHG_index;
        String ZoneName, Floor, BuildingCategory, BuildingUse, Usage, StartTime, EndTime;


        public ZoneGeneral()
        {
            InitializeComponent();
            Program.DB.initTable(DB.type.CalcDB, "ZoneGeneral");
            //Program.DB.initTables(DB.type.CalcDB);
            Zone_comboBox.Items.Add("1F_Zone02");
            Zone_comboBox.Items.Add("1F_Zone04");

            //콤보박스 리스트 생성 
            //존 환기방식 콤보박스
            Program.UTIL.FillComboBox(DB.type.BaseDB_HCneed, AHU_comboBox, "존일반", "환기방식", "1");
            //건물대상 콤보박스
            Program.UTIL.FillComboBox_Parents(BuildingCategory_comboBox, "존일반", "건물용도", "1");
            //존 사용 시작/종료 콤보박스 
            Program.UTIL.FillComboBox(DB.type.BaseDB_HCneed, StartTime_comboBox, "존일반", "이용일 시작 및 종료시간", "8");
            Program.UTIL.FillComboBox(DB.type.BaseDB_HCneed, EndTime_comboBox, "존일반", "이용일 시작 및 종료시간", "19");
            //주간 이용일수 콤보박스 
            Program.UTIL.FillComboBox(DB.type.BaseDB_HCneed, WeekUseDay_comboBox, "존일반", "주간이용일", "1");
            //기기밀도 콤보박스 
            Program.UTIL.FillComboBox(DB.type.BaseDB_HCneed, EquipIHG_comboBox, "존일반", "밀도", "1");


        }




        private void GeneralPanel_Paint(object sender, PaintEventArgs e)
        {
            Panel p = (Panel)sender;
            ControlPaint.DrawBorder(e.Graphics, p.DisplayRectangle, Color.FromArgb(153, 180, 209), ButtonBorderStyle.Solid);
        }


        private void Zone_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ZoneNum = Zone_comboBox.SelectedItem.ToString();
        }


        private void Floor_textBox_TextChanged(object sender, EventArgs e)
        {
            Floor = Floor_textBox.Text;
        }
        private void ZoneName_textBox_TextChanged(object sender, EventArgs e)
        {
            ZoneName = ZoneName_textBox.Text;
        }

        //건물대상 선택 시 건물용도 콤보박스 생성
        private void BuildingCategory_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            BuildingCategory = Program.UTIL.SelectedItem_ByComboBox(BuildingCategory_comboBox);
            Program.UTIL.FillComboBox_ByComboBox(BuildingUse_comboBox, BuildingCategory_comboBox, "1");
        }

        //건물용도 선택 시 용도프로필 콤보박스 생성
        private void BuildingUse_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            BuildingUse = Program.UTIL.SelectedItem_ByComboBox(BuildingUse_comboBox);
            Program.UTIL.FillComboBox_ByComboBox(Usage_comboBox, BuildingUse_comboBox, "1");
        }


        //주간이용일수 선택 시 연간이용일수 계산
        private void WeekUseDay_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[][] res = Program.DB.getValue(DB.type.BaseDB_HCneed, "주간이용일수", "일수", "이용일수" + " = '" + WeekUseDay_comboBox.SelectedItem.ToString() + "' ");
            WeekUseDay = Convert.ToDouble(res[0][0].ToString());
            AnnualUseDay = Convert.ToDouble(Program.UTIL.GetValue2_BySelectComboBox(WeekUseDay_comboBox, "이용일수", "주간일수", "월='연간'", "이용일수"));
            AnnualUseDay_textBox.Text = string.Format("{0:F0}", AnnualUseDay);

        }

        //용도프로필 선택에 따라 값 설정
        private void Usage_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Usage = Program.UTIL.SelectedItem_ByComboBox(Usage_comboBox);
            DHWneed_1p = Convert.ToDouble(Program.UTIL.GetValue_BySelectComboBox(Usage_comboBox, "용도프로필", "용도명", "급탕요구량"));
            OccupancyDensity_Low = Convert.ToDouble(Program.UTIL.GetValue_BySelectComboBox(Usage_comboBox, "용도프로필", "용도명", "재실밀도낮음"));
            OccupancyDensity_Medium = Convert.ToDouble(Program.UTIL.GetValue_BySelectComboBox(Usage_comboBox, "용도프로필", "용도명", "재실밀도보통"));
            OccupancyDensity_High = Convert.ToDouble(Program.UTIL.GetValue_BySelectComboBox(Usage_comboBox, "용도프로필", "용도명", "재실밀도높음"));
            PersonIHG_Low = Convert.ToDouble(Program.UTIL.GetValue_BySelectComboBox(Usage_comboBox, "용도프로필", "용도명", "인체발열낮음"));
            PersonIHG_Medium = Convert.ToDouble(Program.UTIL.GetValue_BySelectComboBox(Usage_comboBox, "용도프로필", "용도명", "인체발열보통"));
            PersonIHG_High = Convert.ToDouble(Program.UTIL.GetValue_BySelectComboBox(Usage_comboBox, "용도프로필", "용도명", "인체발열높음"));
            EquipIHG_Low = Convert.ToDouble(Program.UTIL.GetValue_BySelectComboBox(Usage_comboBox, "용도프로필", "용도명", "기기발열낮음"));
            EquipIHG_Medium = Convert.ToDouble(Program.UTIL.GetValue_BySelectComboBox(Usage_comboBox, "용도프로필", "용도명", "기기발열보통"));
            EquipIHG_High = Convert.ToDouble(Program.UTIL.GetValue_BySelectComboBox(Usage_comboBox, "용도프로필", "용도명", "기기발열높음"));
            EquipIHG_Time = Convert.ToDouble(Program.UTIL.GetValue_BySelectComboBox(Usage_comboBox, "용도프로필", "용도명", "기기일일이용시간"));
            theta_i_h_set = Convert.ToDouble(Program.UTIL.GetValue_BySelectComboBox(Usage_comboBox, "용도프로필", "용도명", "난방설정온도"));
            theta_i_c_set = Convert.ToDouble(Program.UTIL.GetValue_BySelectComboBox(Usage_comboBox, "용도프로필", "용도명", "냉방설정온도"));
            Em = Convert.ToDouble(Program.UTIL.GetValue_BySelectComboBox(Usage_comboBox, "용도프로필", "용도명", "조도"));
            VA = Convert.ToDouble(Program.UTIL.GetValue_BySelectComboBox(Usage_comboBox, "용도프로필", "용도명", "이용일최소외기도입량"));


            DHWneed_Cal(DHWneed_1p, PersonNum);
            OccupancyDensity_Cal(PersonNum, Area);
            PersonIHG_Cal(PersonIHG, UseTime);
            EquipIHG_Cal(EquipIHG_Time);
            Calc_VentilationVolume(Area, NetVolume, VA);
            theta_i_h_set_textBox.Text = String.Format("{0:F0}", theta_i_h_set) + "℃";
            theta_i_c_set_textBox.Text = String.Format("{0:F0}", theta_i_c_set) + "℃";
            Em_textBox.Text = String.Format("{0:F0}", Em) + "lx";

        }

        //시작 및 종료시간에 따라 시간 계산 
        private void StartTime_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            StartTime = StartTime_comboBox.SelectedItem.ToString(); ;
            TimeSpan ts;
            StartTime_image_textBox.Text = StartTime;

            if (StartTime_comboBox.SelectedItem != null && EndTime_comboBox.SelectedItem != null)
            {
                ts = DateTime.Parse(EndTime) - DateTime.Parse(StartTime);
                if (Double.Parse(ts.Hours.ToString()) >= 0)
                { UseTime = Double.Parse(ts.Hours.ToString()); }
                else
                { UseTime = Double.Parse(ts.Hours.ToString()) + 24; }

                HCTime = UseTime + 1;
                AHUTime = UseTime + 1;
                UseTime_textBox.Text = UseTime.ToString();
                HCTime_textBox.Text = HCTime.ToString();
                AHUTime_textBox.Text = AHUTime.ToString();
                PersonIHG_Cal(PersonIHG, UseTime);
            }
        }
        //시작 및 종료시간에 따라 시간 계산  
        private void EndTime_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            EndTime = EndTime_comboBox.SelectedItem.ToString();
            TimeSpan ts;
            EndTime_image_textBox.Text = EndTime;


            if (StartTime_comboBox.SelectedItem != null && EndTime_comboBox.SelectedItem != null)
            {
                ts = DateTime.Parse(EndTime) - DateTime.Parse(StartTime);
                if (Double.Parse(ts.Hours.ToString()) >= 0)
                { UseTime = Double.Parse(ts.Hours.ToString()); }
                else
                { UseTime = Double.Parse(ts.Hours.ToString()) + 24; }

                HCTime = UseTime + 1;
                AHUTime = UseTime + 1;
                UseTime_textBox.Text = UseTime.ToString();
                HCTime_textBox.Text = HCTime.ToString();
                AHUTime_textBox.Text = AHUTime.ToString();
                PersonIHG_Cal(PersonIHG, UseTime);
            }
        }

        private void PersonNum_textBox_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(PersonNum_textBox.Text) == false)
            {
                PersonNum = double.Parse(PersonNum_textBox.Text);
                DHWneed_Cal(DHWneed_1p, PersonNum);
                OccupancyDensity_Cal(PersonNum, Area);
                PersonIHG_Cal(PersonIHG, UseTime);
            }
        }
        private void Length_textBox_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(Length_textBox.Text) == false)
            { Area = double.Parse(Length_textBox.Text); }
        }
        private void Depth_textBox_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(Depth_textBox.Text) == false)
            { Area = double.Parse(Depth_textBox.Text); }
        }
        private void Area_textBox_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(Area_textBox.Text) == false)
            {
                Area = double.Parse(Area_textBox.Text);
                OccupancyDensity_Cal(PersonNum, Area);
                PersonIHG_Cal(PersonIHG, UseTime);
                NetVolume = Area * CelingHeight;
                NetVolume_textBox.Text = String.Format("{0:F1}", NetVolume);
                Calc_VentilationVolume(Area, NetVolume, VA);

            }
        }

        private void CeilingHeight_textBox_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(CeilingHeight_textBox.Text) == false)
            {
                CelingHeight = double.Parse(CeilingHeight_textBox.Text);
                NetVolume = Area * CelingHeight;
                NetVolume_textBox.Text = String.Format("{0:F1}", NetVolume);
                Calc_VentilationVolume(Area, NetVolume, VA);
            }

        }
        private void EquipIHG_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            EquipIHG_index = EquipIHG_comboBox.SelectedItem.ToString();
            EquipIHG_Cal(EquipIHG_Time);
        }

        //용도프로필 선택에 따라 급탕 계산
        private void DHWneed_Cal(double DHWneed_1p, double PersonNum)
        {
            DataRowView? item = Usage_comboBox.SelectedItem as DataRowView;
            if (item != null && item.Row.ItemArray.Length >= 3)
            {
                if (String.IsNullOrEmpty(PersonNum_textBox.Text) == false)
                {
                    DHWneed = DHWneed_1p * PersonNum;
                    DHWneed_textBox.Text = string.Format("{0:F1}", (DHWneed));
                    DHWneed_image_textBox.Text = string.Format("{0:F1}", (DHWneed)) + "kWh/d";
                }
            }
        }

        private void OccupancyDensity_Cal(double PersonNum, double Area)
        {
            if (String.IsNullOrEmpty(PersonNum_textBox.Text) == false && String.IsNullOrEmpty(Area_textBox.Text) == false)
            {
                OccupancyDensity = Area / PersonNum;
                OccupancyDensity_textBox.Text = string.Format("{0:F1}", OccupancyDensity);
            }

            if (String.IsNullOrEmpty(PersonNum_textBox.Text) == false && String.IsNullOrEmpty(Area_textBox.Text) == false && Usage_comboBox.SelectedItem != null)
            {
                if (OccupancyDensity_High <= OccupancyDensity && OccupancyDensity <= OccupancyDensity_Low)
                { OccupancyDensity_index = "보통"; }
                else if (OccupancyDensity <= OccupancyDensity_High)
                { OccupancyDensity_index = "높음"; }
                else
                { OccupancyDensity_index = "낮음"; }

                //PersonIHG 단위 : W/m2
                switch (OccupancyDensity_index)
                {
                    case "낮음":
                        PersonIHG = PersonIHG_Low;
                        break;
                    case "보통":
                        PersonIHG = PersonIHG_Medium;
                        break;
                    case "높음":
                        PersonIHG = PersonIHG_High;
                        break;
                }
                OccupancyDensity_index_textBox.Text = OccupancyDensity_index;
            }

        }

        private void PersonIHG_Cal(double PersonIHG, double UseTime)
        {
            if (PersonIHG != 0 && UseTime != 0)
            {
                PersonIHG_1day = PersonIHG * UseTime;
                PersonIHG_textBox.Text = string.Format("{0:F1}", PersonIHG_1day);
                PersonIHG_image_textBox.Text = string.Format("{0:F0}", PersonIHG_1day) + "Wh/m²·d";
            }
        }

        //기기밀도수준 및 용도프로필 선택에 따라 기기발열 계산 
        private void EquipIHG_Cal(double EquipIHG_Time)
        {
            DataRowView? EquipIHG_item = EquipIHG_comboBox.SelectedItem as DataRowView;
            DataRowView? Usage_item = Usage_comboBox.SelectedItem as DataRowView;

            if (EquipIHG_item != null && EquipIHG_item.Row.ItemArray.Length >= 3 && Usage_item != null && Usage_item.Row.ItemArray.Length >= 3)
            {
                //PersonIHG 단위 : W/m2
                switch (EquipIHG_item.Row.ItemArray[0].ToString())
                {
                    case "낮음":
                        EquipIHG = EquipIHG_Low;
                        break;
                    case "보통":
                        EquipIHG = EquipIHG_Medium;
                        break;
                    case "높음":
                        EquipIHG = EquipIHG_High;
                        break;
                }
                EquipIHG_1day = EquipIHG * EquipIHG_Time;
                EquipIHG_textBox.Text = string.Format("{0:F1}", EquipIHG_1day);
                EquipIHG_image_textBox.Text = string.Format("{0:F0}", EquipIHG_1day) + "Wh/m²·d";

            }
        }
        private void Calc_VentilationVolume(double Area, double NetVolume, double VA)
        {

            VentilationVolume = Area * VA;
            VentilationVolume_textBox.Text = String.Format("{0:F1}", VentilationVolume);

            if (NetVolume != null && NetVolume != 0)
            { VentilationRate = VentilationVolume / NetVolume; }
            else { VentilationRate = 0; }
            VentilationRate_textBox.Text = String.Format("{0:F1}", VentilationRate);
        }

        private void Save_button_Click(object sender, EventArgs e)
        {
            Program.DB.setValue(DB.type.CalcDB, "ZoneGeneral", "존번호,존이름,층,길이,깊이,바닥면적,용도프로필,천장고,시작시간,종료시간,주이용일,재실자수,기기발열수준," +
                "일일급탕요구량,냉난방시간,사용시간,공조시간,연이용일수,재실밀도,재실수준,일일인체발열,면적당인체발열,일일기기발열,면적당기기발열,순체적,환기횟수,환기량",
            "'" + ZoneNum + "','" + ZoneName + "','" + Floor + "','" + Length.ToString() + "','" + Depth.ToString() + "','"
            + Area.ToString() + "','" + Usage + "','" + CeilingHeight_textBox.Text + "','" + StartTime + "','" + EndTime + "','"
            + WeekUseDay.ToString() + "','" + PersonNum_textBox.Text + "','" + EquipIHG_index + "','"
            + DHWneed.ToString() + "','" + HCTime.ToString() + "','" + UseTime.ToString() + "','" + AHUTime.ToString() + "','" + AnnualUseDay.ToString() + "','"
            + OccupancyDensity.ToString() + "','" + OccupancyDensity_index + "','" + PersonIHG_1day.ToString() + "','" + PersonIHG.ToString() + "','" + EquipIHG_1day.ToString() + "','"
            + EquipIHG.ToString() + "','" + NetVolume.ToString() + "','" + VentilationRate.ToString() + "','" + VentilationVolume.ToString() + "'", "존번호");

            string[][] ZoneE = Program.DB.getValue(DB.type.CalcDB, "ZoneGeneral", "존이름", "존번호='" + ZoneNum + "'");
            MessageBox.Show(ZoneE[0][0]);

        }
        private void reset()
        { }

         public void LoadData(String ID)            // 리스트에서 항목 더블 클릭시 - 뷰를 ID 의 getValue 값으로 채우기
        {
            reset();
            try
            {
                Zone_comboBox.SelectedItem = ID;
                ZoneNum = ID;
            }
            catch { }
          }

        public void ResetForm(String ID) // 리스트에서 추가 버튼 클릭시 - 뷰 초기화
        {
            Zone_comboBox.SelectedItem = ID;
            ZoneNum = ID;
        }

    }
}
