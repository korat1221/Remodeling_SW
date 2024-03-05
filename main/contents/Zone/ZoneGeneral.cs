using main.contentslist;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Security.AccessControl;
using System.Security.Cryptography.X509Certificates;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.MonthCalendar;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace main.contents
{
    public partial class ZoneGeneral : Form
    {
        String ZoneNum;
        String RoomControl, Ground, HCType, AHUType;
        double DHWneed_1p, DHWneed, UseTime, HCTime, AHUTime, PersonNum, Length, Depth, NetArea, CeilingHeight, NetVolume, VentilationRate, Volume_wd, Volume_we, AnnualUseDay, WeekUseDay;
        double PersonIHG_1day, PersonIHG, PersonIHG_Low, PersonIHG_Medium, PersonIHG_High; //PersonIHG 단위 : W/m2
        double EquipIHG_1day, EquipIHG, EquipIHG_Low, EquipIHG_Medium, EquipIHG_High, EquipIHG_Time; //EquipIHG 단위 : W/m2
        double theta_i_h_set, theta_i_c_set, Em, VA, VA_we;
        double OccupancyDensity, OccupancyDensity_Low, OccupancyDensity_Medium, OccupancyDensity_High;
        String OccupancyDensity_index, EquipIHG_index;
        String ZoneName, BuildingCategory, BuildingUse, Usage, StartTime, EndTime;
        double η, η2;
        static string Layer;
        public ZoneGeneral()
        {
            InitializeComponent();
            string[][] Image = Program.DB.getValue(DB.type.BaseDB_HCneed, "메뉴아이콘", "하위메뉴아이콘", "하위메뉴명 = '존 일반정보'");
            if (Image.Length > 0)
            {
                Icon_pictureBox.Load(Program.gPath + Image[0][0]);
                Icon_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            }

            Image = Program.DB.getValue(DB.type.BaseDB_HCneed, "메뉴아이콘", "상위메뉴아이콘_on2", "상위메뉴명 = '결과 정보'");


            Image = Program.DB.getValue(DB.type.BaseDB_HCneed, "용도프로필이미지", "이미지", "대분류 = '메인'");
            if (Image.Length > 0)
            {
                Main_pictureBox.Load(Program.gPath + Image[0][0]);
                Main_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                Main_pictureBox.Controls.Add(RoomControl_pictureBox);
            }


            //존 환기방식 콤보박스
            Program.UTIL.FillComboBox(DB.type.BaseDB_HCneed, AHU_comboBox, "존일반", "환기방식", "");
            //실 제어방식
            Program.UTIL.FillComboBox(DB.type.BaseDB_HCneed, RoomControl_comboBox, "존일반", "건물 자동화 온도조절", "1");
            //존 사용 시작/종료 콤보박스 
            Program.UTIL.FillComboBox(DB.type.BaseDB_HCneed, StartTime_comboBox, "존일반", "이용일 시작 및 종료시간", "");
            Program.UTIL.FillComboBox(DB.type.BaseDB_HCneed, EndTime_comboBox, "존일반", "이용일 시작 및 종료시간", "");
            //주간 이용일수 콤보박스 
            Program.UTIL.FillComboBox(DB.type.BaseDB_HCneed, WeekUseDay_comboBox, "존일반", "주간이용일", "4");
            //기기밀도 콤보박스 
            Program.UTIL.FillComboBox(DB.type.BaseDB_HCneed, EquipIHG_comboBox, "존일반", "밀도", "1");

            Load_GroundImage();
            Heating_checkBox.Checked = true;
            Cooling_checkBox.Checked = true;
            Check_HC();
            Check_AHU();
            Ventilation_checkBox.Checked = false;
            Load_AHUImage();
        }


        private void GeneralPanel_Paint(object sender, PaintEventArgs e)
        {
            Panel p = (Panel)sender;
            ControlPaint.DrawBorder(e.Graphics, p.DisplayRectangle, Color.FromArgb(153, 180, 209), ButtonBorderStyle.Solid);
        }


        private void Floor_textBox_TextChanged(object sender, EventArgs e)
        {
            if (Layer_textBox.Text != null)
            {
                Layer = Layer_textBox.Text;
                GetValue_ZoneEnvelope();
            }
        }

        //실 제어방식  
        private void RoomControl_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (RoomControl_comboBox.SelectedItem != null)
            {
                RoomControl = RoomControl_comboBox.SelectedItem.ToString();
                Load_RoomControlImage();
            }

        }
        private void Load_RoomControlImage()
        {

            string[][] Image = Program.DB.getValue(DB.type.BaseDB_HCneed, "용도프로필이미지", "이미지", "대분류 = '제어' AND 소분류 = '" + RoomControl + "'");
            if (Image.Length > 0)
            {
                RoomControl_pictureBox.Load(Program.gPath + Image[0][0]);
                RoomControl_pictureBox.Visible = true;
                RoomControl_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                RoomControl_pictureBox.BackColor = Color.Transparent;
                RoomControl_pictureBox.Location = new Point(0, 0);
            }

            RoomControl_pictureBox.Controls.Add(Ground_pictureBox);
        }

        private void Load_GroundImage()
        {
            string[][] Image = Program.DB.getValue(DB.type.BaseDB_HCneed, "용도프로필이미지", "이미지", "대분류 = '지면접합' AND 소분류 = '" + Ground + "'");
            if (Image.Length > 0)
            {
                Ground_pictureBox.Load(Program.gPath + Image[0][0]);
                Ground_pictureBox.Visible = true;
                Ground_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                Ground_pictureBox.BackColor = Color.Transparent;
                Ground_pictureBox.Location = new Point(0, 0);
            }
            Ground_pictureBox.Controls.Add(HC_pictureBox);
        } 
        private void Heating_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            Check_HC();
        }

        private void Cooling_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            Check_HC();
        }

        private void Check_HC()
        {
            if (Heating_checkBox.Checked == true)
            {
                if (Cooling_checkBox.Checked == true)
                { HCType = "냉난방"; }
                else { HCType = "난방"; }
            }
            else if (Cooling_checkBox.Checked == true)
            { HCType = "냉방"; }
            else { HCType = "비냉난방"; }
            Load_HCImage();

            if (HCType == "비냉난방")
            {
                StartTime_image_textBox.BackColor = Color.FromArgb(217, 217, 217);
                EndTime_image_textBox.BackColor = Color.FromArgb(217, 217, 217);
                theta_i_c_set_textBox.BackColor = Color.FromArgb(217, 217, 217);
                theta_i_h_set_textBox.BackColor = Color.FromArgb(217, 217, 217);
                DHWneed_image_textBox.BackColor = Color.FromArgb(217, 217, 217);
                Em_textBox.BackColor = Color.FromArgb(217, 217, 217);
                PersonIHG_image_textBox.BackColor = Color.FromArgb(217, 217, 217);
                EquipIHG_image_textBox.BackColor = Color.FromArgb(217, 217, 217);
                RA_Volume_textBox.BackColor = Color.FromArgb(217, 217, 217);
                SA_Volume_textBox.BackColor = Color.FromArgb(217, 217, 217);

            }
            else
            {
                StartTime_image_textBox.BackColor = Color.FromArgb(253, 245, 230);
                EndTime_image_textBox.BackColor = Color.FromArgb(253, 245, 230);
                theta_i_c_set_textBox.BackColor = Color.FromArgb(253, 245, 230);
                theta_i_h_set_textBox.BackColor = Color.FromArgb(253, 245, 230);
                DHWneed_image_textBox.BackColor = Color.FromArgb(253, 245, 230);
                Em_textBox.BackColor = Color.FromArgb(253, 245, 230);
                PersonIHG_image_textBox.BackColor = Color.FromArgb(253, 245, 230);
                EquipIHG_image_textBox.BackColor = Color.FromArgb(253, 245, 230);
                RA_Volume_textBox.BackColor = Color.FromArgb(253, 245, 230);
                SA_Volume_textBox.BackColor = Color.FromArgb(253, 245, 230);
            }
        }
        private void Load_HCImage()
        {
            if (HCType != null)
            {
                string[][] Image = Program.DB.getValue(DB.type.BaseDB_HCneed, "용도프로필이미지", "이미지", "대분류 = '냉난방유무' AND 소분류 = '" + HCType + "'");
                if (Image.Length > 0)
                {
                    HC_pictureBox.Load(Program.gPath + Image[0][0]);
                    HC_pictureBox.Visible = true;
                    HC_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                    HC_pictureBox.BackColor = Color.Transparent;
                    HC_pictureBox.Location = new Point(0, 0);
                }
                HC_pictureBox.Controls.Add(AHU_pictureBox);
            }
        }
        private void Ventilation_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            Check_AHU();
        }

        private void AHU_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (AHU_comboBox.SelectedItem != null)
            {
                AHUType = AHU_comboBox.SelectedItem.ToString();
                Check_AHU();
            }
        }
        private void Check_AHU()
        {
            if (Ventilation_checkBox.Checked)
            {
                Load_AHUImage();
                AHU_label.Visible = true;
                AHU_comboBox.Visible = true;
                AHU_comboBox.Enabled = true;
                η_label.Visible = true;
                η_label2.Visible = true;
                η_textBox.Visible = true;
                η_label.Enabled = true;
                η_label2.Enabled = true;
                η_textBox.Enabled = true;
                η2_label.Visible = true;
                η2_label2.Visible = true;
                η2_textBox.Visible = true;
                η2_label.Enabled = true;
                η2_label2.Enabled = true;
                η2_textBox.Enabled = true;

            }
            else
            {
                AHUType = "none";
                AHU_label.Visible = false;
                AHU_comboBox.Visible = false;
                AHU_comboBox.Enabled = false;
                AHU_pictureBox.Visible = false;
                η_label.Visible = false;
                η_label2.Visible = false;
                η_textBox.Visible = false;
                η_label.Enabled = false;
                η_label2.Enabled = false;
                η_textBox.Enabled = false;
                η2_label.Visible = false;
                η2_label2.Visible = false;
                η2_textBox.Visible = false;
                η2_label.Enabled = false;
                η2_label2.Enabled = false;
                η2_textBox.Enabled = false;
            }
        }
        private void Load_AHUImage()
        {

            if (AHU_comboBox.SelectedItem != null)
            {
                AHUType = AHU_comboBox.SelectedItem.ToString();
                string[][] Image = Program.DB.getValue(DB.type.BaseDB_HCneed, "용도프로필이미지", "이미지", "대분류 = '환기유무' AND 소분류 = '" + AHUType + "'");
                if (Image.Length > 0)
                {
                    AHU_pictureBox.Load(Program.gPath + Image[0][0]);
                    AHU_pictureBox.Visible = true;
                    AHU_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                    AHU_pictureBox.BackColor = Color.Transparent;
                    AHU_pictureBox.Location = new Point(0, 0);
                }
            }
            if (Ventilation_checkBox.Checked)
            {
                if (AHUType == "열회수환기")
                {
                    SA_Volume_textBox.Visible = true;
                    RA_Volume_textBox.Visible = false;

                    η_label.Visible = true;
                    η_label2.Visible = true;
                    η2_label.Visible = true;
                    η2_label2.Visible = true;
                    η2_textBox.Visible = true;
                    η_textBox.Visible = true;
                    η2_textBox.Visible = true;
                }
                else if (AHUType == "배기환기(3종)")
                {
                    SA_Volume_textBox.Visible = false;
                    RA_Volume_textBox.Visible = true;

                    η_label.Visible = false;
                    η_label2.Visible = false;
                    η2_label.Visible = false;
                    η2_label2.Visible = false;
                    η2_textBox.Visible = false;
                    η_textBox.Visible = false;
                    η2_textBox.Visible = false;
                }
                else
                {
                    SA_Volume_textBox.Visible = false;
                    RA_Volume_textBox.Visible = false;

                    η_label.Visible = false;
                    η_label2.Visible = false;
                    η2_label.Visible = false;
                    η2_label2.Visible = false;
                    η2_textBox.Visible = false;
                    η_textBox.Visible = false;
                    η2_textBox.Visible = false;
                }
            }
            else
            {
                SA_Volume_textBox.Visible = false;
                RA_Volume_textBox.Visible = false;
                η_label.Visible = false;
                η_label2.Visible = false;
                η2_label.Visible = false;
                η2_label2.Visible = false;
                η2_textBox.Visible = false;
                η_textBox.Visible = false;
                η2_textBox.Visible = false;
            }

        }


        private void η_textBox_TextChanged(object sender, EventArgs e)
        {
            if (η_textBox.Text != null&& η_textBox.Text != "")
            {
                if (Convert.ToDouble(η_textBox.Text) == 0)
                { return; }
                if (Convert.ToDouble(η_textBox.Text) < 1)
                { MessageBox.Show("백분율 단위로 입력하세요."); }
                else { η = Convert.ToDouble(η_textBox.Text) / 100; }
            }
        }

        private void η2_textBox_TextChanged(object sender, EventArgs e)
        {
            if (η2_textBox.Text != null && η2_textBox.Text !="")
            {
                if (Convert.ToDouble(η2_textBox.Text) == 0)
                { return; }
                if (Convert.ToDouble(η2_textBox.Text) < 1)
                { MessageBox.Show("백분율 단위로 입력하세요."); }
                else { η2 = Convert.ToDouble(η2_textBox.Text) / 100; }
            }

        }     
        //주간이용일수 선택 시 연간이용일수 계산
        private void WeekUseDay_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (WeekUseDay_comboBox.SelectedItem != null)
            {
                string[][] res = Program.DB.getValue(DB.type.BaseDB_HCneed, "주간이용일수", "일수", "이용일수" + " = '" + WeekUseDay_comboBox.SelectedItem.ToString() + "' ");
                if (res.Length > 0)
                {
                    WeekUseDay = Convert.ToDouble(res[0][0].ToString());
                    AnnualUseDay = Convert.ToDouble(Program.UTIL.GetValue2_BySelectComboBox(WeekUseDay_comboBox, "이용일수", "주간일수", "월='연간'", "이용일수"));
                    AnnualUseDay_textBox.Text = string.Format("{0:F0}", AnnualUseDay);
                }
            }
        }

        //용도프로필 선택에 따라 값 설정
        private void Usage_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataRowView? Usage_item = Usage_comboBox.SelectedItem as DataRowView;
            if (Usage_item != null)
            {
                Usage = Program.UTIL.SelectedItem_ByComboBox(Usage_comboBox);
                Calc_Usage();
            }
        }
        private void Calc_Usage()
        {
            DataRowView? Usage_item = Usage_comboBox.SelectedItem as DataRowView;
            if (Usage_item != null)
            {
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
                VA_we = Convert.ToDouble(Program.UTIL.GetValue_BySelectComboBox(Usage_comboBox, "용도프로필", "용도명", "비이용일최소외기도입량"));
                DHWneed_Cal(DHWneed_1p, PersonNum);
                OccupancyDensity_Cal(PersonNum, NetArea);
                PersonIHG_Cal(PersonIHG, UseTime);
                EquipIHG_Cal(EquipIHG_Time);
                Calc_VentilationVolume(NetArea, NetVolume, VA, VA_we);
                theta_i_h_set_textBox.Text = String.Format("{0:F0}", theta_i_h_set) + "℃";
                theta_i_c_set_textBox.Text = String.Format("{0:F0}", theta_i_c_set) + "℃";
                Em_textBox.Text = String.Format("{0:F0}", Em) + "lx";
            }
        }

        //시작 및 종료시간에 따라 시간 계산 
        private void StartTime_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (StartTime_comboBox.SelectedItem != null)
            { StartTime = StartTime_comboBox.SelectedItem.ToString(); }
            StartTime_image_textBox.Text = StartTime;
            Calc_Time();
        }
        //시작 및 종료시간에 따라 시간 계산  
        private void EndTime_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (EndTime_comboBox.SelectedItem != null)
            { EndTime = EndTime_comboBox.SelectedItem.ToString(); }
            EndTime_image_textBox.Text = EndTime;
            Calc_Time();
        }
        private void Calc_Time()
        {
            TimeSpan ts;
            if (StartTime_comboBox.SelectedItem != null && EndTime_comboBox.SelectedItem != null)
            {
                if (StartTime_comboBox.SelectedItem.ToString() == EndTime_comboBox.SelectedItem.ToString())
                {
                    UseTime = 24;
                    HCTime = 24;
                    AHUTime = 24;
                    UseTime_textBox.Text = UseTime.ToString();
                    HCTime_textBox.Text = HCTime.ToString();
                    PersonIHG_Cal(PersonIHG, UseTime);
                }
                else
                {
                    ts = DateTime.Parse(EndTime_comboBox.SelectedItem.ToString()) - DateTime.Parse(StartTime_comboBox.SelectedItem.ToString());
                    if (Double.Parse(ts.Hours.ToString()) >= 0)
                    { UseTime = Double.Parse(ts.Hours.ToString()); }
                    else
                    { UseTime = Double.Parse(ts.Hours.ToString()) + 24; }

                    HCTime = UseTime + 1;
                    AHUTime = UseTime + 1;
                    UseTime_textBox.Text = UseTime.ToString();
                    HCTime_textBox.Text = HCTime.ToString();
                    PersonIHG_Cal(PersonIHG, UseTime);
                }

            }

        }

        private void PersonNum_textBox_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(PersonNum_textBox.Text) == false)
            {
                PersonNum = double.Parse(PersonNum_textBox.Text);
                DHWneed_Cal(DHWneed_1p, PersonNum);
                OccupancyDensity_Cal(PersonNum, NetArea);
                PersonIHG_Cal(PersonIHG, UseTime);
            }
        }
        private void NetArea_textBox_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(NetArea_textBox.Text) == false)
            {
                NetArea = double.Parse(NetArea_textBox.Text);
                OccupancyDensity_Cal(PersonNum, NetArea);
                PersonIHG_Cal(PersonIHG, UseTime);
                NetVolume = NetArea * CeilingHeight;
                NetVolume_textBox.Text = String.Format("{0:F1}", NetVolume);
                Calc_VentilationVolume(NetArea, NetVolume, VA, VA_we);

            }
        }

        private void CeilingHeight_textBox_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(CeilingHeight_textBox.Text) == false)
            {
                CeilingHeight = double.Parse(CeilingHeight_textBox.Text);
                NetVolume = NetArea * CeilingHeight;
                NetVolume_textBox.Text = String.Format("{0:F1}", NetVolume);
                Calc_VentilationVolume(NetArea, NetVolume, VA, VA_we);
            }

        }
        private void EquipIHG_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (EquipIHG_comboBox.SelectedItem != null)
            {
                EquipIHG_index = EquipIHG_comboBox.SelectedItem.ToString();
                EquipIHG_Cal(EquipIHG_Time);
            }
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
            if (PersonNum != 0 && String.IsNullOrEmpty(NetArea_textBox.Text) == false)
            {
                OccupancyDensity = Area / PersonNum;
                OccupancyDensity_textBox.Text = string.Format("{0:F1}", OccupancyDensity);
            }
            else if (PersonNum == 0)
            {
                OccupancyDensity = 0;
                OccupancyDensity_textBox.Text = string.Format("{0:F1}", OccupancyDensity);
            }

            if (String.IsNullOrEmpty(PersonNum_textBox.Text) == false && String.IsNullOrEmpty(NetArea_textBox.Text) == false && Usage_comboBox.SelectedItem != null)
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
          
                PersonIHG_1day = PersonIHG * UseTime;
                PersonIHG_textBox.Text = string.Format("{0:F1}", PersonIHG_1day);
                PersonIHG_image_textBox.Text = string.Format("{0:F0}", PersonIHG_1day) + "Wh/m²·d";
        }

        //기기밀도수준 및 용도프로필 선택에 따라 기기발열 계산 
        private void EquipIHG_Cal(double EquipIHG_Time)
        {

            DataRowView? Usage_item = Usage_comboBox.SelectedItem as DataRowView;

            if (EquipIHG_index != null && Usage_item != null && Usage_item.Row.ItemArray.Length >= 3)
            {
                //PersonIHG 단위 : W/m2
                switch (EquipIHG_index)
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
        private void Calc_VentilationVolume(double Area, double NetVolume, double VA, double VA_we)
        {

            Volume_wd = Area * VA;
            Volume_wd_textBox.Text = String.Format("{0:F1}", Volume_wd);
            SA_Volume_textBox.Text = String.Format("{0:F1}", Volume_wd) + "m³/h";

            Volume_we = Area * VA_we;
            RA_Volume_textBox.Text = String.Format("{0:F1}", Volume_we) + "m³/h";

            if (NetVolume != null && NetVolume != 0)
            { VentilationRate = Volume_wd / NetVolume; }
            else { VentilationRate = 0; }
            VentilationRate_textBox.Text = String.Format("{0:F1}", VentilationRate);
        }

        private void Save_button_Click(object sender, EventArgs e)
        {
            if (ZoneName == null)
            {
                MessageBox.Show("존 이름을 입력하세요.");
            }
            else if (Ventilation_checkBox.Checked)
            {
                if (AHUType == null)
                {
                    MessageBox.Show("환기방식을 선택하세요.");
                }
                else if (η == 0 || η2 == 0)
                { MessageBox.Show("온도교환효율과 전열교환효율을 선택하세요."); }
                else
                {
                    save();
                }
            }
            else if (Usage == null)
            {
                MessageBox.Show("용도프로필을 선택하세요.");
            }
            else if (CeilingHeight == 0)
            {
                MessageBox.Show("천장고를 입력하세요.");
            }
            else if (PersonNum_textBox.Text == null)
            {
                MessageBox.Show("재실자수를 입력하세요.");
            }
            else
            {
                save();
            }
        }
        public static bool OnLoadListProc(Form form)
        {
            List_Zone f = (List_Zone)form;
            f.load_List(Layer);
            return true;
        }

        private void save()
        {
            //Save_Image();
            //존일반정보 폼에 해당하는 정보만 저장 
            //건물정보, 3D정보는 저장 안함
            string[][] 프로젝트유형 = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "프로젝트유형번호");
            Program.DB.setValue(DB.type.ProjDB, "ZoneGeneral_Form", "존번호,프로젝트유형,존이름,실제어방식,냉난방유무,환기유무,환기방식,온도교환효율_냉방,전열교환효율_냉방,온도교환효율_난방,전열교환효율_난방," +
                "용도프로필,천장고,시작시간,종료시간,주이용일,재실자수,기기발열수준," +
                "일일급탕요구량,냉난방시간,사용시간,공조시간,연이용일수,재실밀도,재실수준,일일인체발열,면적당인체발열,일일기기발열,면적당기기발열," +
                "순체적,환기횟수,이용일환기량,비이용일환기량,순바닥면적",
            "'" + ZoneNum + "','" + 프로젝트유형[0][0] + "','" + ZoneName + "','" + RoomControl + "','" + HCType + "','" + Ventilation_checkBox.Checked.ToString() + "','" + AHUType + "','" + η.ToString() + "','" + η2.ToString() + "','" + η.ToString() + "','" + η2.ToString() + "','"
            + Usage + "','" + CeilingHeight.ToString() + "','" + StartTime + "','" + EndTime + "','" + WeekUseDay.ToString() + "','" + PersonNum_textBox.Text + "','" + EquipIHG_index + "','"
            + DHWneed.ToString() + "','" + HCTime.ToString() + "','" + UseTime.ToString() + "','" + AHUTime.ToString() + "','" + AnnualUseDay.ToString() + "','"
            + OccupancyDensity.ToString() + "','" + OccupancyDensity_index + "','" + PersonIHG_1day.ToString() + "','" + PersonIHG.ToString() + "','" + EquipIHG_1day.ToString() + "','" + EquipIHG.ToString() + "','"
            + NetVolume.ToString() + "','" + VentilationRate.ToString() + "','" + Volume_wd.ToString() + "','" + Volume_we.ToString() + "','"
            + NetArea.ToString() + "'", "존번호");

            MessageBox.Show(ZoneNum + "[" + ZoneName + "] 정보를 저장하였습니다.");
            this.DialogResult = DialogResult.OK;
            this.Hide();
            Program.getMenuForm().DoLoadForm(33, OnLoadListProc);
        }
        //private void Save_Image()
        //{
        //    MemoryStream ms = new MemoryStream();
        //    string ImageName = "/threejs/public/img/"+ZoneNum+".png";
        //    Bitmap bmp = new Bitmap(AdditionalPanel.Width, AdditionalPanel.Height);
        //    AdditionalPanel.DrawToBitmap(bmp, new System.Drawing.Rectangle(0, 0, AdditionalPanel.Width, AdditionalPanel.Height));
        //   // bmp.Save(ms ,System.Drawing.Imaging.ImageFormat.Png); //you could ave in BPM, PNG  etc format.
        //    bmp.Save(Program.gPath + ImageName, System.Drawing.Imaging.ImageFormat.Png); //you could ave in BPM, PNG  etc format.
        //    byte[] Pic_arr = new byte[ms.Length];
        //    ms.Position = 0;
        //    ms.Read(Pic_arr, 0, Pic_arr.Length);
        //    ms.Close();

        //}
        private void reset()
        {
            ZoneName_textBox.Text = "";
            // RoomControl = null; HCType = null; AHUType = null; OccupancyDensity_index = null; 
            DHWneed_1p = 0; DHWneed = 0; UseTime = 0; HCTime = 0; AHUTime = 0; PersonNum = 0; CeilingHeight = 0; NetArea = 0; NetVolume = 0; VentilationRate = 0; Volume_wd = 0; //AnnualUseDay = 0;// WeekUseDay = 0;
            PersonIHG_1day = 0; PersonIHG = 0; PersonIHG_Low = 0; PersonIHG_Medium = 0; PersonIHG_High = 0; //PersonIHG 단위 : W/m2
            EquipIHG_1day = 0; EquipIHG = 0; EquipIHG_Low = 0; EquipIHG_Medium = 0; EquipIHG_High = 0;  //EquipIHG 단위 : W/m2
            theta_i_h_set = 0; theta_i_c_set = 0; Em = 0; VA = 0;
            OccupancyDensity = 0; OccupancyDensity_Low = 0; OccupancyDensity_Medium = 0; OccupancyDensity_High = 0;
            AHU_comboBox.SelectedItem = null;
            AHUType = "";
            RoomControl_comboBox.SelectedIndex = 0;
            StartTime_comboBox.SelectedItem = null;
            EndTime_comboBox.SelectedItem = null;
            Usage_comboBox.SelectedItem = null;
            WeekUseDay_comboBox.SelectedItem = null;

            //WeekUseDay_comboBox.SelectedIndex = 3;
            EquipIHG_comboBox.SelectedIndex = 0;

            CeilingHeight_textBox.Text = "";
            PersonNum_textBox.Text = "";

            NetArea = 0;
            NetArea_textBox.Text = "";

            String[] ConstructionType = { "커튼월창", "외벽", "지붕", "최하층바닥", "창호", "외부출입문", "내벽", "층간바닥" };


            Heating_checkBox.Checked = true;
            Cooling_checkBox.Checked = true;
            Check_HC();
            Ventilation_checkBox.Checked = false;
            Check_AHU();
            Load_AHUImage();
            Calc_Time();
            Calc_Usage();
            DHWneed_image_textBox.Text = "";
            EndTime_image_textBox.Text = "";
            EquipIHG_image_textBox.Text = "";
            PersonIHG_image_textBox.Text = "";
            StartTime_image_textBox.Text = "";
            theta_i_c_set_textBox.Text = "";
            theta_i_h_set_textBox.Text = "";
            UseTime_textBox.Text = "";
            VentilationRate_textBox.Text = "";
            Volume_wd_textBox.Text = "";
            η2_textBox.Text = "";
            η2 = 0;
            η_textBox.Text = "";
            η = 0;
            CW_textBox.Text = "";
            Wall_textBox.Text = "";
            Roof_textBox.Text = "";
            Window_textBox.Text = "";
            Floor_textBox.Text = "";
            InWall_textBox.Text = "";
            Door_textBox.Text = "";
            OccupancyDensity_textBox.Text = "";

        }

        public void LoadData(String ID)            // 리스트에서 항목 더블 클릭시 - 뷰를 ID 의 getValue 값으로 채우기
        {
            reset();
            Load_OtherFormData();

            String[][] Value = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_Form", "존이름,실제어방식,냉난방유무,환기유무,환기방식,온도교환효율_냉방,전열교환효율_냉방,온도교환효율_난방,전열교환효율_난방," +
                "용도프로필,천장고,시작시간,종료시간,주이용일,재실자수,기기발열수준," +
                "일일급탕요구량,냉난방시간,사용시간,공조시간,연이용일수,재실밀도,재실수준,일일인체발열,면적당인체발열,일일기기발열,면적당기기발열," +
                "순체적,환기횟수,이용일환기량,비이용일환기량,순바닥면적", "존번호 = '" + ZoneNum + "'");
            if (Value.Length > 0)
            {
                ZoneName = Value[0][0];
                ZoneName_textBox.Text = ZoneName;

                RoomControl = Value[0][1];
                RoomControl_comboBox.SelectedItem = RoomControl;

                HCType = Value[0][2];
                if (HCType == "비냉난방")
                {
                    Heating_checkBox.Checked = false;
                    Cooling_checkBox.Checked = false;
                }
                else if (HCType == "난방")
                {
                    Heating_checkBox.Checked = true;
                    Cooling_checkBox.Checked = false;
                }
                else if (HCType == "냉방")
                {
                    Heating_checkBox.Checked = false;
                    Cooling_checkBox.Checked = false;
                }
                else
                {
                    Heating_checkBox.Checked = true;
                    Cooling_checkBox.Checked = true;
                }
                Check_HC();
                if (Value[0][3] != "")
                {
                    Ventilation_checkBox.Checked = Convert.ToBoolean(Value[0][3]);
                }

                AHUType = Value[0][4];
                AHU_comboBox.SelectedItem = AHUType;
                Check_AHU();
                Load_AHUImage();

                if (Value[0][5] != "")
                {
                    η = Convert.ToDouble(Value[0][5]);
                    η_textBox.Text = string.Format("{0:F1}", η * 100);
                }
                if (Value[0][6] != "")
                {
                    η2 = Convert.ToDouble(Value[0][6]);
                    η2_textBox.Text = string.Format("{0:F1}", η2 * 100);
                }

                Usage_comboBox.SelectedItem = Usage;
                DataRowView? item = Usage_comboBox.SelectedItem as DataRowView;
                //Usage = Value[0][7];


                if (item != null)
                {
                    Usage = item.Row.ItemArray[0].ToString();
                }
                if (Value[0][31] != "")
                {
                    NetArea = Convert.ToDouble(Value[0][31]);
                    NetArea_textBox.Text = NetArea.ToString();
                }
                if (Value[0][10] != "")
                {
                    CeilingHeight = Convert.ToDouble(Value[0][10]);
                    CeilingHeight_textBox.Text = CeilingHeight.ToString();
                }
                if (Value[0][11] != "")
                {
                    StartTime = Value[0][11];
                    StartTime_comboBox.SelectedItem = StartTime;
                }
                if (Value[0][12] != "")
                {
                    EndTime = Value[0][12];
                    EndTime_comboBox.SelectedItem = EndTime;
                }
                if (Value[0][13] != "")
                {
                    WeekUseDay = Convert.ToDouble(Value[0][13]);
                    WeekUseDay_comboBox.SelectedItem = "주 " + WeekUseDay.ToString() + ".0 일 근무";
                }
                if (Value[0][14] != "")
                {
                    PersonNum = Convert.ToDouble(Value[0][14]);
                    PersonNum_textBox.Text = PersonNum.ToString();
                }
                if (Value[0][15] != "")
                {
                    EquipIHG_index = Value[0][15];
                    EquipIHG_comboBox.SelectedItem = EquipIHG_index;
                }
                if (Value[0][16] != "")
                {
                    DHWneed = Convert.ToDouble(Value[0][16]);
                    DHWneed_textBox.Text = string.Format("{0:F1}", (DHWneed));
                    DHWneed_image_textBox.Text = string.Format("{0:F1}", (DHWneed)) + "kWh/d";
                }
                if (Value[0][17] != "")
                {
                    HCTime = Convert.ToDouble(Value[0][17]);
                    HCTime_textBox.Text = HCTime.ToString();
                }
                if (Value[0][18] != "")
                {
                    UseTime = Convert.ToDouble(Value[0][18]);
                    UseTime_textBox.Text = UseTime.ToString();
                }
                if (Value[0][19] != "")
                {
                    AHUTime = Convert.ToDouble(Value[0][19]);
                }
                if (Value[0][20] != "")
                {
                    AnnualUseDay = Convert.ToDouble(Value[0][20]);
                    AnnualUseDay_textBox.Text = string.Format("{0:F0}", AnnualUseDay);
                }
                if (Value[0][21] != "")
                {
                    OccupancyDensity = Convert.ToDouble(Value[0][21]);
                    OccupancyDensity_textBox.Text = string.Format("{0:F1}", OccupancyDensity);
                    OccupancyDensity_index = Value[0][22];
                    OccupancyDensity_index_textBox.Text = OccupancyDensity_index;
                }
                if (Value[0][23] != "")
                {
                    PersonIHG_1day = Convert.ToDouble(Value[0][23]);
                    PersonIHG = Convert.ToDouble(Value[0][24]);
                    PersonIHG_textBox.Text = string.Format("{0:F1}", PersonIHG_1day);
                    PersonIHG_image_textBox.Text = string.Format("{0:F0}", PersonIHG_1day) + "Wh/m²·d";
                }
                if (Value[0][25] != "")
                {
                    EquipIHG_1day = Convert.ToDouble(Value[0][25]);
                    EquipIHG = Convert.ToDouble(Value[0][26]);
                    EquipIHG_textBox.Text = string.Format("{0:F1}", EquipIHG_1day);
                    EquipIHG_image_textBox.Text = string.Format("{0:F0}", EquipIHG_1day) + "Wh/m²·d";
                }
                if (Value[0][27] != "")
                {
                    NetVolume = Convert.ToDouble(Value[0][27]);
                    NetVolume_textBox.Text = String.Format("{0:F1}", NetVolume);
                }
                if (Value[0][28] != "")
                {
                    VentilationRate = Convert.ToDouble(Value[0][28]);
                    VentilationRate_textBox.Text = String.Format("{0:F1}", VentilationRate);
                }
                if (Value[0][29] != "")
                {
                    Volume_wd = Convert.ToDouble(Value[0][29]);
                    Volume_wd_textBox.Text = String.Format("{0:F1}", Volume_wd);
                    SA_Volume_textBox.Text = String.Format("{0:F1}", Volume_wd) + "m³/h";
                }
                if (Value[0][30] != "")
                {
                    Volume_we = Convert.ToDouble(Value[0][30]);
                    RA_Volume_textBox.Text = String.Format("{0:F1}", Volume_we) + "m³/h";
                }
            }
        }

        public void ResetForm(String ID) // 리스트에서 추가 버튼 클릭시 - 뷰 초기화
        {
            Num_textBox.Text = ID;
            ZoneNum = ID;
            Load_OtherFormData();
        }

        private void ZoneGeneral_VisibleChanged(object sender, EventArgs e)
        {
            if (main.MainContents.currentForm == main.MainContents.FormID.ZoneGeneral)
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
            //건물대상,용도
            String[][] BuildingValue = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "건물대상,건물용도", "");
            if (BuildingValue.Length > 0)
            {
                BuildingCategory = BuildingValue[0][0];
                BuildingUse = BuildingValue[0][1];
            }
            string id = "";
            String[][] Index = Program.DB.getValue(DB.type.BaseDB_HCneed, "인덱스", "아이디", "이름 = '" + BuildingUse + "'");
            if (Index.Length > 0)
            { id = Index[0][0].ToString(); }

            if (id != "")
            {
                string[][] res = Program.DB.querySQL(DB.type.BaseDB_HCneed, "SELECT 이름, 값, 아이디 FROM 인덱스 WHERE 부모아이디=" + id);


                //Usage = Value[0][7];
                String[][] Value = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_Form", "용도프로필", "존번호 = '" + ZoneNum + "'");

                if (Value.Length > 0)
                {
                    if (Value[0][0] != "")
                    {
                        for (int i = 0; i < res.Length; i++)
                        {
                            if (Value[0][0] == res[i][0])
                            {
                                Program.UTIL.FillComboBox_Category(Usage_comboBox, res, (i + 1).ToString());
                            }
                        }
                    }
                    else
                    {
                        Program.UTIL.FillComboBox_Category(Usage_comboBox, res, (1).ToString());
                    }

                }
                else
                {
                    Program.UTIL.FillComboBox_Category(Usage_comboBox, res, (1).ToString());
                }
            }

            GetValue_ZoneEnvelope();

            //3D 외피정보
            String[][] 층 = Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope_3D", "층", "존 = '" + ZoneNum + "'");
            String[][] General_3D = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_3D", "층,지면접합유형,바닥면적,존이름", "존번호 = '" + ZoneNum + "'");
            if (층.Length > 0)
            {
                Layer = 층[0][0];
                Layer_textBox.Text = Layer;
            }
            if (General_3D.Length > 0)
            {
                Ground = General_3D[0][1];
                Load_GroundImage();

                ZoneName = General_3D[0][3];
                ZoneName_textBox.Text = ZoneName;
                /////////////////////////////// //순바닥면적계산/////////////////////////////////////////////////////

                String[][] Wall = Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope_3D", "벽체길이,구조체번호", "존 = '" + ZoneNum + "' And 외피유형 = '외벽'");
                double Area_WallInWall = 0;
                if (Wall.Length > 0)
                {
                    if (Wall[0][1] == "" || Wall[0][1] == null)
                    {
                        MessageBox.Show("3D 모델 화면에서 외피 구조체종류부터 입력해주세요.");
                    }
                    for (int j = 0; j < Wall.Length; j++)
                    {
                        double Wall_d, Wall_A;
                        String[][] Wall_Value = Program.DB.getValue(DB.type.ProjDB, "ConstructionWall",
                         "U적용방법, 두께합계,단열재두께,구조유형,열관류율," +
                         "재료1종류,재료1두께," +
                         "재료2종류,재료2두께," +
                         "재료3종류,재료3두께," +
                         "재료4종류,재료4두께," +
                         "재료5종류,재료5두께," +
                         "재료6종류,재료6두께," +
                         "재료7종류,재료7두께," +
                         "재료8종류,재료8두께," +
                         "재료9종류,재료9두께," +
                         "재료10종류,재료10두께", "번호 = '" + Wall[j][1] + "'");

                        if (Wall_Value[0][3] == "콘크리트조")
                        {
                            if (Wall_Value[0][0] == "계산")
                            {
                                Wall_d = 0;

                                for (int k = 0; k < 10; k++)
                                {
                                    if (Wall_Value[0][2 * k + 5] != "")
                                    {
                                        String[][] Material = Program.DB.getValue(DB.type.BaseDB_HCneed, "열전도율", "구분", "재료명 = '" + Wall_Value[0][2 * k + 5] + "'");
                                        if (Material.Length > 0)
                                        {
                                            if (Material[0][0] != "콘크리트")
                                            {
                                                Wall_d += Convert.ToDouble(Wall_Value[0][2 * k + 6]) / 1000;
                                            }
                                            else
                                            {
                                                break;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                Wall_d = 0.075 + Convert.ToDouble(Wall_Value[0][2]) / 1000; //내단열로 가정함
                            }
                        }
                        else
                        {
                           // Wall_d = 0.015 + Convert.ToDouble(Wall[j][2]) / 2;
                            Wall_d = 0.015 + Convert.ToDouble(Wall_Value[0][2]) / 1000;
                        }

                        Wall_A = Wall_d * Convert.ToDouble(Wall[j][0]);
                        Area_WallInWall += Wall_A;
                    }
                }

                String[][] InWall = Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope_3D", "벽체길이", "존 = '" + ZoneNum + "' And 외피유형 = '내벽'");
                if (InWall.Length > 0)
                {
                    for (int j = 0; j < InWall.Length; j++)
                    {
                        double InWall_d, InWall_A;
                        InWall_d = 0.05;
                        InWall_A = InWall_d * Convert.ToDouble(InWall[j][0]);
                        Area_WallInWall += InWall_A;
                    }
                }

                NetArea = Convert.ToDouble(General_3D[0][2]) - Area_WallInWall;
                NetArea_textBox.Text = string.Format("{0:F2}", NetArea);
            }
        }

        private void GetValue_ZoneEnvelope()
        {
            //3D 외피정보
                String[][] Envelope_3D = Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope_3D", "외피유형,면적", "존 = '" + ZoneNum + "'");
                if (Envelope_3D.Length > 0)
                {
                    //외피별 면적 합계 계산
                    //"커튼월창", "외벽", "지붕", "최하층바닥", "창호", "외부출입문", "내벽", "층간바닥" 
                    int[] Construction_Count = new int[8]; double[] Construction_AreaSum = new double[8];
                    String[] ConstructionType = { "커튼월창", "외벽", "지붕", "최하층바닥", "창호", "외부출입문", "내벽", "층간바닥" };
                    int i = -1;
                    while (++i < Envelope_3D.Length)
                    {
                        for (int k = 0; k < ConstructionType.Length; k++)
                        {
                            if (Envelope_3D[i][0] == ConstructionType[k])
                            {
                                Construction_AreaSum[k] += Convert.ToDouble(Envelope_3D[i][1]);
                            }
                        }
                    }
                    if (Construction_AreaSum[0] != 0)
                    { CW_textBox.Text = string.Format("{0:F1}", Construction_AreaSum[0]) + "m²"; }

                    if (Construction_AreaSum[1] != 0)
                    { Wall_textBox.Text = string.Format("{0:F1}", Construction_AreaSum[1]) + "m²"; }

                    if (Construction_AreaSum[2] != 0)
                    { Roof_textBox.Text = string.Format("{0:F1}", Construction_AreaSum[2]) + "m²"; }

                    if (Construction_AreaSum[3] != 0)
                    { Floor_textBox.Text = string.Format("{0:F1}", Construction_AreaSum[3]) + "m²"; }

                    if (Construction_AreaSum[4] != 0)
                    { Window_textBox.Text = string.Format("{0:F1}", Construction_AreaSum[4]) + "m²"; }

                    if (Construction_AreaSum[5] != 0)
                    { Door_textBox.Text = string.Format("{0:F1}", Construction_AreaSum[5]) + "m²"; }

                    if (Construction_AreaSum[6] != 0)
                    { InWall_textBox.Text = string.Format("{0:F1}", Construction_AreaSum[6]) + "m²"; }
                }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Program.CALC.run(new string[] {
                "존 계산"
            });

        }


    }
}
