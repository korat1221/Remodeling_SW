using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace main.contents
{
    public partial class ZoneUsage : Form
    {
        double DHWneed_1p, UseTime, HCTime, AHUTime, PersonNum, Area, AnnualUseDay, WeekUseDay;
        double PersonIHG_1day, PersonIHG, PersonIHG_Low, PersonIHG_Medium, PersonIHG_High;
        double EquipIHG_1day, EquipIHG, EquipIHG_Low, EquipIHG_Medium, EquipIHG_High, EquipIHG_Time;
        double theta_i_h_set, theta_i_c_set, Em;
        double OccupancyDensity, OccupancyDensity_Low, OccupancyDensity_Medium, OccupancyDensity_High;
        String OccupancyDensity_index;

        public ZoneUsage()
        {
            InitializeComponent();



            //콤보박스 리스트 생성 
            try
            {
                string connStr = @"Data Source=C:\Users\User\Documents\GitHub\Remodeling_SW\asset\basedb.sqlite";
                SQLiteConnection conn1 = new SQLiteConnection(connStr);
                conn1.Open();
                var cmd = new SQLiteCommand(conn1);

                //존 환기방식 콤보박스 

                String query = "SELECT * fROM index_환기방식";
                cmd = new SQLiteCommand(query, conn1);
                SQLiteDataReader Ventilation_index_rdr = cmd.ExecuteReader();
                while (Ventilation_index_rdr.Read())
                {
                    AHU_comboBox.Items.Add(Ventilation_index_rdr["환기방식"]);
                }

                //건물용도 콤보박스 

                query = "SELECT * fROM index_건물용도";
                cmd = new SQLiteCommand(query, conn1);
                SQLiteDataReader BuildingUse_index_rdr = cmd.ExecuteReader();
                while (BuildingUse_index_rdr.Read())
                {
                    BuildingUse_comboBox.Items.Add(BuildingUse_index_rdr["건물용도"]);

                }


            }
            catch (Exception ex) { }

            try
            {
                string connStr = @"Data Source=C:\Users\User\Documents\GitHub\Remodeling_SW\asset\basedb.sqlite";
                SQLiteConnection conn1 = new SQLiteConnection(connStr);
                conn1.Open();
                var cmd = new SQLiteCommand(conn1);

                //존 사용 시작/종료 콤보박스 
                String query = "SELECT * fROM index_시작종료시간";
                cmd = new SQLiteCommand(query, conn1);
                SQLiteDataReader StartEndTime_index_rdr = cmd.ExecuteReader();
                while (StartEndTime_index_rdr.Read())
                {
                    StartTime_comboBox.Items.Add(StartEndTime_index_rdr["시간"]);
                    EndTime_comboBox.Items.Add(StartEndTime_index_rdr["시간"]);
                }

                //주간 이용일수 콤보박스 
                query = "SELECT * fROM index_주간이용일수";
                cmd = new SQLiteCommand(query, conn1);
                SQLiteDataReader WeekUseDay_index_rdr = cmd.ExecuteReader();
                while (WeekUseDay_index_rdr.Read())
                {
                    WeekUseDay_comboBox.Items.Add(WeekUseDay_index_rdr["주간이용일수"]);
                }


                //기기밀도 콤보박스 
                query = "SELECT * fROM Index_재실밀도";
                cmd = new SQLiteCommand(query, conn1);
                SQLiteDataReader OccupanDensity_index_rdr = cmd.ExecuteReader();
                while (OccupanDensity_index_rdr.Read())
                {
                    EquipIHG_comboBox.Items.Add(OccupanDensity_index_rdr["재실밀도"]);
                }
            }
            catch (Exception ex) { }
        }


        private void GeneralPanel_Paint(object sender, PaintEventArgs e)
        {
            Panel p = (Panel)sender;
            ControlPaint.DrawBorder(e.Graphics, p.DisplayRectangle, Color.FromArgb(153, 180, 209), ButtonBorderStyle.Solid);
        }


        private void BuildingUse_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Usage_comboBox.Items.Clear();
            try
            {
                string connStr = @"Data Source=C:\Users\User\Documents\GitHub\Remodeling_SW\asset\basedb.sqlite";
                SQLiteConnection conn1 = new SQLiteConnection(connStr);
                conn1.Open();
                var cmd = new SQLiteCommand(conn1);


                //존 용도프로필 콤보박스 
                String query = "SELECT * fROM index_용도프로필 ";
                cmd = new SQLiteCommand(query, conn1);
                SQLiteDataReader Usage_index_rdr = cmd.ExecuteReader();
                int i = 0;
                while (Usage_index_rdr.Read())
                {
                    Usage_comboBox.Items.Add(Usage_index_rdr[BuildingUse_comboBox.SelectedItem.ToString()]);
                    if (Usage_comboBox.Items[i].ToString() == "")
                    {
                        Usage_comboBox.Items.RemoveAt(i);
                    }
                    i++;

                }


            }
            catch (Exception ex) { }
        }

        private void WeekUseDay_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string connStr = @"Data Source=C:\Users\User\Documents\GitHub\Remodeling_SW\asset\basedb.sqlite";
                SQLiteConnection conn1 = new SQLiteConnection(connStr);
                conn1.Open();
                var cmd = new SQLiteCommand(conn1);


                String query = "SELECT * fROM index_주간이용일수 ";
                cmd = new SQLiteCommand(query, conn1);
                SQLiteDataReader weekUseDay_index_rdr = cmd.ExecuteReader();
                while (weekUseDay_index_rdr.Read())
                {
                    if (WeekUseDay_comboBox.SelectedItem.ToString() == weekUseDay_index_rdr["주간이용일수"].ToString())
                    {
                        WeekUseDay = double.Parse(weekUseDay_index_rdr["일수"].ToString());
                    }
                }



                query = "SELECT * fROM 이용일수";
                cmd = new SQLiteCommand(query, conn1);
                SQLiteDataReader UseDay_Table_rdr = cmd.ExecuteReader();

                while (UseDay_Table_rdr.Read())
                {
                    if (UseDay_Table_rdr["일"].ToString()== "365")
                    {
                        AnnualUseDay = double.Parse(UseDay_Table_rdr[WeekUseDay_comboBox.SelectedItem.ToString()].ToString());
                        AnnualUseDay_textBox.Text = string.Format("{0:F0}", AnnualUseDay);
                    }
                }

            }
            catch (Exception ex) { }
        }

        private void Usage_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

            try
            {
                string connStr = @"Data Source=C:\Users\User\Documents\GitHub\Remodeling_SW\asset\basedb.sqlite";
                SQLiteConnection conn1 = new SQLiteConnection(connStr);
                conn1.Open();
                var cmd = new SQLiteCommand(conn1);


                //존 용도프로필 콤보박스 
                String query = "SELECT * fROM 용도프로필 ";
                cmd = new SQLiteCommand(query, conn1);
                SQLiteDataReader Usage_Table_rdr = cmd.ExecuteReader();


                while (Usage_Table_rdr.Read())
                {
                    if (Usage_comboBox.SelectedItem.ToString() == Usage_Table_rdr["용도명"].ToString())
                    {
                        DHWneed_1p = double.Parse(Usage_Table_rdr["급탕요구량"].ToString());
                        OccupancyDensity_Low = double.Parse(Usage_Table_rdr["재실밀도낮음"].ToString());
                        OccupancyDensity_Medium = double.Parse(Usage_Table_rdr["재실밀도보통"].ToString());
                        OccupancyDensity_High = double.Parse(Usage_Table_rdr["재실밀도높음"].ToString());
                        PersonIHG_Low = double.Parse(Usage_Table_rdr["인체발열낮음"].ToString());
                        PersonIHG_Medium = double.Parse(Usage_Table_rdr["인체발열보통"].ToString());
                        PersonIHG_High = double.Parse(Usage_Table_rdr["인체발열높음"].ToString());
                        EquipIHG_Low = double.Parse(Usage_Table_rdr["기기발열낮음"].ToString());
                        EquipIHG_Medium = double.Parse(Usage_Table_rdr["기기발열보통"].ToString());
                        EquipIHG_High = double.Parse(Usage_Table_rdr["기기발열높음"].ToString());
                        EquipIHG_Time = double.Parse(Usage_Table_rdr["기기일일이용시간"].ToString());
                        theta_i_h_set = double.Parse(Usage_Table_rdr["난방설정온도"].ToString());
                        theta_i_c_set = double.Parse(Usage_Table_rdr["냉방설정온도"].ToString());
                        Em = double.Parse(Usage_Table_rdr["조도"].ToString());
                    }
                }

            }
            catch (Exception ex) { }

            DHWneed_Cal(DHWneed_1p, PersonNum);
            OccupancyDensity_Cal(PersonNum, Area);
            PersonIHG_Cal(PersonIHG, UseTime);
            EquipIHG_Cal(EquipIHG_Time);
            theta_i_h_set_textBox.Text = String.Format("{0:F0}", theta_i_h_set) + "℃";
            theta_i_c_set_textBox.Text = String.Format("{0:F0}", theta_i_c_set) + "℃";
            Em_textBox.Text = String.Format("{0:F0}", Em) + "lx";

        }
        private void StartTime_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            TimeSpan ts;
            StartTime_image_textBox.Text = StartTime_comboBox.SelectedItem.ToString();

            if (EndTime_comboBox.SelectedItem != null)
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
                AHUTime_textBox.Text = AHUTime.ToString();
                PersonIHG_Cal(PersonIHG, UseTime);
            }
        }

        private void EndTime_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            TimeSpan ts;
            EndTime_image_textBox.Text = EndTime_comboBox.SelectedItem.ToString();

            if (StartTime_comboBox.SelectedItem != null)
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

        private void Area_textBox_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(Area_textBox.Text) == false)
            {
                Area = double.Parse(Area_textBox.Text);
                OccupancyDensity_Cal(PersonNum, Area);
                PersonIHG_Cal(PersonIHG, UseTime);
            }
        }

        private void EquipIHG_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            EquipIHG_Cal(EquipIHG_Time);

        }

        private void DHWneed_Cal(double DHWneed_1p, double PersonNum)
        {
            if (String.IsNullOrEmpty(PersonNum_textBox.Text) == false && Usage_comboBox.SelectedItem != null)
                DHWneed_textBox.Text = string.Format("{0:F1}", (DHWneed_1p * PersonNum));
            DHWneed_image_textBox.Text = string.Format("{0:F1}", (DHWneed_1p * PersonNum)) + "kWh/d";
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

        private void EquipIHG_Cal(double EquipIHG_Time)
        {
            if (EquipIHG_comboBox.SelectedItem != null && Usage_comboBox.SelectedItem != null)
            {
                //PersonIHG 단위 : W/m2
                switch (EquipIHG_comboBox.SelectedItem.ToString())
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
    }
}
