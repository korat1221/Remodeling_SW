using main.contentslist;
using main.info;
using main.subcontents.AHUSystem;
using main.subcontents.ZoneGeneral;
using System.Collections;
using System.Data;

namespace main.contents
{
    public partial class ZoneGeneral : Form, IConfirmable
    {
        String ZoneNum; String SelectPreZone_nonsplit; ArrayList SelectPreZone_split = new ArrayList();
        String RoomControl = "일반", Ground, HCType, AHUType;
        public double DHWneed_1p, DHWneed, UseTime, HCTime, AHUTime, PersonNum, Length, Depth, NetArea, CeilingHeight, NetVolume, VentilationRate, Volume_wd, Volume_we, AnnualUseDay, WeekUseDay;
        public double PersonIHG_1day, PersonIHG, PersonIHG_Low, PersonIHG_Medium, PersonIHG_High; //PersonIHG 단위 : W/m2
        public double EquipIHG_1day, EquipIHG, EquipIHG_Low, EquipIHG_Medium, EquipIHG_High, EquipIHG_Time; //EquipIHG 단위 : W/m2
        public double theta_i_h_set, theta_i_c_set, Em, VA, VA_we;
        public double OccupancyDensity, OccupancyDensity_Low, OccupancyDensity_Medium, OccupancyDensity_High;
        public String OccupancyDensity_index, EquipIHG_index;
        public String HumidC_index, HumidH_index;
        public double HumidC, HumidH; //냉방/난방 설정 절대습도 단위 : g/kg'
        public String ZoneName, BuildingCategory, BuildingUse, Usage, StartTime, EndTime;
        string SelectHRV; string 증축여부;
        static string Layer;
        public ZoneGeneral()
        {
            InitializeComponent(); this.Font = new Font(UTIL.Families[0], 9.75F, FontStyle.Regular);
            string[][] Image = Program.DB.getValue(DB.type.BaseDB_HCneed, "메뉴아이콘", "하위메뉴아이콘", "하위메뉴명 = '존 일반정보'");
            if (Image.Length > 0)
            {
                Icon_pictureBox.Load(Program.gPath + Image[0][0]);
                Icon_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            }

            Image = Program.DB.getValue(DB.type.BaseDB_HCneed, "메뉴아이콘", "상위메뉴아이콘_on2", "상위메뉴명 = '결과 정보'");


            label54.Text  = "m"+Program.UTIL.Subscript(2,true);
            label63.Text = "m" + Program.UTIL.Subscript(3, true) + "/h";
            label35.Text = "Wh/m" + Program.UTIL.Subscript(2, true) + "d";
            label31.Text = "Wh/m" + Program.UTIL.Subscript(2, true) + "d";
            label24.Text = "m" + Program.UTIL.Subscript(2, true) + "/인";
            //존 환기방식 콤보박스
            //Program.UTIL.FillComboBox(DB.type.BaseDB_HCneed, AHU_comboBox, "존일반", "환기방식", "");
            AHU_comboBox.Items.Clear();
            AHU_comboBox.Items.Add("배기환기(3종)");
            AHU_comboBox.Items.Add("열회수기");
            AHU_comboBox.Items.Add("공조기");

            //존 사용 시작/종료 콤보박스 
            Program.UTIL.FillComboBox(DB.type.BaseDB_HCneed, StartTime_comboBox, "존일반", "이용일 시작 및 종료시간", "");
            Program.UTIL.FillComboBox(DB.type.BaseDB_HCneed, EndTime_comboBox, "존일반", "이용일 시작 및 종료시간", "");
            //주간 이용일수 콤보박스 
            Program.UTIL.FillComboBox(DB.type.BaseDB_HCneed, WeekUseDay_comboBox, "존일반", "주간이용일", "4");
            //기기밀도 콤보박스
            Program.UTIL.FillComboBox(DB.type.BaseDB_HCneed, EquipIHG_comboBox, "존일반", "밀도", "1");

            //냉방/난방 설정 습도등급 콤보박스 (EN 16798-1:2017 표B.16)
            HumidC_comboBox.Items.Clear();
            HumidC_comboBox.Items.Add("항온항습");
            HumidC_comboBox.Items.Add("습도고려");
            HumidC_comboBox.Items.Add("습도고려안함");

            HumidH_comboBox.Items.Clear();
            HumidH_comboBox.Items.Add("항온항습");
            HumidH_comboBox.Items.Add("습도고려");
            HumidH_comboBox.Items.Add("습도고려안함");


            Heating_checkBox.Checked = true;
            Cooling_checkBox.Checked = true;
            Image = Program.DB.getValue(DB.type.BaseDB_HCneed, "용도프로필이미지", "이미지", "대분류 = '냉난방환기' And 냉난방유무='냉난방' and 환기유무='none'");
            if (Image.Length > 0)
            {
                Main_pictureBox.Load(Program.gPath + Image[0][0]);
                Main_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                Load_RoomControlImage();
                Load_GroundImage();
            }
            Check_HC();
            Check_AHU();
            Ventilation_checkBox.Checked = false;
            Load_MainImage(HCType, AHUType);

            StartTime_image_Label.Parent = Ground_pictureBox;
            EndTime_image_Label.Parent = Ground_pictureBox;
            theta_i_c_set_Label.Parent = Ground_pictureBox;
            theta_i_h_set_Label.Parent = Ground_pictureBox;
            DHWneed_image_Label.Parent = Ground_pictureBox;
            Em_Label.Parent = Ground_pictureBox;
            PersonIHG_image_Label.Parent = Ground_pictureBox;
            EquipIHG_image_Label.Parent = Ground_pictureBox;
            RA_Volume_Label.Parent = Ground_pictureBox;
            SA_Volume_Label.Parent = Ground_pictureBox;

            StartTime_image_Label.BackColor = Color.Transparent;
            EndTime_image_Label.BackColor = Color.Transparent;
            theta_i_c_set_Label.BackColor = Color.Transparent;
            theta_i_h_set_Label.BackColor = Color.Transparent;
            DHWneed_image_Label.BackColor = Color.Transparent;
            Em_Label.BackColor = Color.Transparent;
            PersonIHG_image_Label.BackColor = Color.Transparent;
            EquipIHG_image_Label.BackColor = Color.Transparent;
            RA_Volume_Label.BackColor = Color.Transparent;
            SA_Volume_Label.BackColor = Color.Transparent;
        }



        private void GeneralPanel_Paint(object sender, PaintEventArgs e)
        {
            Panel p = (Panel)sender;
            ControlPaint.DrawBorder(e.Graphics, p.DisplayRectangle, System.Drawing.SystemColors.Control, ButtonBorderStyle.Solid);
        }



        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            Panel p = (Panel)sender;
            ControlPaint.DrawBorder(e.Graphics, p.DisplayRectangle, System.Drawing.SystemColors.Control, ButtonBorderStyle.Solid);
        }

        private void AdditionalPanel_Paint(object sender, PaintEventArgs e)
        {
            Panel p = (Panel)sender;
            ControlPaint.DrawBorder(e.Graphics, p.DisplayRectangle, System.Drawing.SystemColors.Control, ButtonBorderStyle.Solid);
        }
        private void Load_RoomControlImage()
        {
            string[][] Image = Program.DB.getValue(DB.type.BaseDB_HCneed, "용도프로필이미지", "이미지", "대분류 = '제어' AND 소분류 = '" + RoomControl + "'");
            if (Image.Length > 0)
            {
                RoomControl_pictureBox.Load(Program.gPath + Image[0][0]);
                RoomControl_pictureBox.Visible = true;
                RoomControl_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                RoomControl_pictureBox.Parent = Main_pictureBox;
                RoomControl_pictureBox.BackColor = Color.Transparent;
                RoomControl_pictureBox.Location = new Point(0, 0);
            }

        }

        private void Load_GroundImage()
        {
            string[][] Image = Program.DB.getValue(DB.type.BaseDB_HCneed, "용도프로필이미지", "이미지", "대분류 = '지면접합' AND 소분류 = '" + Ground + "'");
            if (Image.Length > 0)
            {
                Ground_pictureBox.Load(Program.gPath + Image[0][0]);
                Ground_pictureBox.Visible = true;
                Ground_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                Ground_pictureBox.Parent = RoomControl_pictureBox;
                Ground_pictureBox.BackColor = Color.Transparent;
                Ground_pictureBox.Location = new Point(0, 0);
            }

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
            Load_MainImage(HCType, AHUType);


        }
        private void Load_MainImage(string HCType, string AHUType)
        {
            if (HCType != null && AHUType != null)
            {
                string AHUType_;
                if (AHUType == "공조기") { AHUType_ = "열회수기"; }
                else { AHUType_ = AHUType; }
                string[][] Image = Program.DB.getValue(DB.type.BaseDB_HCneed, "용도프로필이미지", "이미지", "대분류 = '냉난방환기' AND 냉난방유무 = '" + HCType + "' and 환기유무='" + AHUType_ + "'");
                if (Image.Length > 0)
                {
                    Main_pictureBox.Load(Program.gPath + Image[0][0]);
                    Main_pictureBox.Visible = true;
                    Main_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                    Main_pictureBox.BackColor = Color.Transparent;
                    Main_pictureBox.Location = new Point(0, 0);
                }
            }
        }

        #region 기존 존 
        private void PreZone_button_Click(object sender, EventArgs e)
        {
            PreZone prezone = new PreZone(ZoneNum, SelectPreZone_nonsplit, Layer);
            DialogResult result = prezone.ShowDialog();
            if (result == DialogResult.OK)
            {
                if (prezone.SelectZone != null)
                {
                    SelectPreZone_nonsplit = prezone.SelectZone;
                    Split_Zone(prezone.SelectZone);
                }
            }
        }
        private void Split_Zone(String nonSplit)
        {
            String 내용;
            if (nonSplit != null && nonSplit != "")
            {
                if (nonSplit.Contains("+"))
                {
                    string[] token = nonSplit.Split('+');
                    SelectPreZone_split.Clear();
                    foreach (var item in token)
                    {
                        SelectPreZone_split.Add(item.ToString());
                    }
                    내용 = SelectPreZone_split[0].ToString() + " 외 " + (SelectPreZone_split.Count - 1).ToString() + "개";
                }
                else
                {
                    SelectPreZone_split.Clear();
                    SelectPreZone_split.Add(nonSplit);
                    내용 = SelectPreZone_split[0].ToString();
                }
                PreZone_textBox.Text = 내용;
            }
            else { 내용 = ""; }

        }
        #endregion
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

                AHU_label.Visible = true;
                AHU_comboBox.Visible = true;
                AHU_comboBox.Enabled = true;
                
                if (AHU_comboBox.SelectedItem == null) 
                { 
                    AHU_button.Visible = false; AHU_textBox.Visible = false; AHU_label2.Visible = false; 
                }
                //else if (AHU_comboBox.SelectedItem.ToString() == "배기환기(3종)") 
                //{ 
                //    AHU_button.Visible = false; AHU_textBox.Visible = false; AHU_label2.Visible = false; AHUType = AHU_comboBox.SelectedItem.ToString(); 
                //}
                else 
                {
                    AHU_button.Visible = true; AHU_textBox.Visible = true; AHU_label2.Visible = true; AHUType = AHU_comboBox.SelectedItem.ToString(); 
                }

                if (AHUType == "공조기") 
                {
                    AHU_label2.Text = "공조기"; 
                } 
                else 
                { 
                    AHU_label2.Text = "열회수기"; 
                }

                if (AHUType == "열회수기" || AHUType == "공조기") 
                {
                    AHU_label2.Text = AHUType;
                    SA_Volume_Label.Visible = true; RA_Volume_Label.Visible = false;
                }
                else if (AHUType == "배기환기(3종)") 
                {
                    AHU_label2.Text = AHUType;
                    SA_Volume_Label.Visible = false; RA_Volume_Label.Visible = true; 
                }
                else 
                { 
                    SA_Volume_Label.Visible = false; RA_Volume_Label.Visible = false; 
                }
            }
            else
            {
                AHUType = "none";
                AHU_label.Visible = false;
                AHU_comboBox.Visible = false;
                AHU_comboBox.Enabled = false;
                AHU_button.Visible = false; AHU_textBox.Visible = false; AHU_label2.Visible = false;
                SA_Volume_Label.Visible = false;
                RA_Volume_Label.Visible = false;
            }
            Load_MainImage(HCType, AHUType);
        }
        private void AHU_button_Click(object sender, EventArgs e)
        {
            if (AHUType == "공조기")
            {
                AHU_HRV AHU_HRV = new AHU_HRV(AHUType, SelectHRV);
                DialogResult result = AHU_HRV.ShowDialog();
                if (result == DialogResult.OK)
                {
                    if (AHU_HRV.SelectSystem != null)
                    {
                        SelectHRV = AHU_HRV.SelectSystem;
                        if (SelectHRV != null) { AHU_textBox.Text = SelectHRV; }
                    }
                }
            }
            else if(AHUType =="열회수기")
            {
                AHU_HRV AHU_HRV = new AHU_HRV(AHUType, SelectHRV);
                DialogResult result = AHU_HRV.ShowDialog();
                if (result == DialogResult.OK)
                {
                    if (AHU_HRV.SelectSystem != null)
                    {
                        SelectHRV = AHU_HRV.SelectSystem;
                        if (SelectHRV != null) { AHU_textBox.Text = SelectHRV; }
                    }
                }
            }
            else if (AHUType == "배기환기(3종)") //class
            {
                AHU_HRV AHU_HRV = new AHU_HRV(AHUType, SelectHRV);
                DialogResult result = AHU_HRV.ShowDialog();
                if (result == DialogResult.OK)
                {
                    if (AHU_HRV.SelectSystem != null)
                    {
                        SelectHRV = AHU_HRV.SelectSystem;
                        if (SelectHRV != null) { AHU_textBox.Text = SelectHRV; }
                    }
                }
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
                    WeekUseDay = Program.UTIL.ToDoubleOrZero(res[0][0].ToString());
                    AnnualUseDay = Program.UTIL.ToDoubleOrZero(Program.UTIL.GetValue2_BySelectComboBox(WeekUseDay_comboBox, "이용일수", "주간일수", "월='연간'", "이용일수"));
                    AnnualUseDay_textBox.Text = AnnualUseDay.ToString();
                    Program.UTIL.textBox_doubleComa(AnnualUseDay_textBox, true, 0);
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
                DHWneed_1p = Program.UTIL.ToDoubleOrZero(Program.UTIL.GetValue_BySelectComboBox(Usage_comboBox, "용도프로필", "용도명", "급탕요구량"));
                OccupancyDensity_Low = Program.UTIL.ToDoubleOrZero(Program.UTIL.GetValue_BySelectComboBox(Usage_comboBox, "용도프로필", "용도명", "재실밀도낮음"));
                OccupancyDensity_Medium = Program.UTIL.ToDoubleOrZero(Program.UTIL.GetValue_BySelectComboBox(Usage_comboBox, "용도프로필", "용도명", "재실밀도보통"));
                OccupancyDensity_High = Program.UTIL.ToDoubleOrZero(Program.UTIL.GetValue_BySelectComboBox(Usage_comboBox, "용도프로필", "용도명", "재실밀도높음"));
                PersonIHG_Low = Program.UTIL.ToDoubleOrZero(Program.UTIL.GetValue_BySelectComboBox(Usage_comboBox, "용도프로필", "용도명", "인체발열낮음"));
                PersonIHG_Medium = Program.UTIL.ToDoubleOrZero(Program.UTIL.GetValue_BySelectComboBox(Usage_comboBox, "용도프로필", "용도명", "인체발열보통"));
                PersonIHG_High = Program.UTIL.ToDoubleOrZero(Program.UTIL.GetValue_BySelectComboBox(Usage_comboBox, "용도프로필", "용도명", "인체발열높음"));
                EquipIHG_Low = Program.UTIL.ToDoubleOrZero(Program.UTIL.GetValue_BySelectComboBox(Usage_comboBox, "용도프로필", "용도명", "기기발열낮음"));
                EquipIHG_Medium = Program.UTIL.ToDoubleOrZero(Program.UTIL.GetValue_BySelectComboBox(Usage_comboBox, "용도프로필", "용도명", "기기발열보통"));
                EquipIHG_High = Program.UTIL.ToDoubleOrZero(Program.UTIL.GetValue_BySelectComboBox(Usage_comboBox, "용도프로필", "용도명", "기기발열높음"));
                EquipIHG_Time = Program.UTIL.ToDoubleOrZero(Program.UTIL.GetValue_BySelectComboBox(Usage_comboBox, "용도프로필", "용도명", "기기일일이용시간"));
                theta_i_h_set = Program.UTIL.ToDoubleOrZero(Program.UTIL.GetValue_BySelectComboBox(Usage_comboBox, "용도프로필", "용도명", "난방설정온도"));
                theta_i_c_set = Program.UTIL.ToDoubleOrZero(Program.UTIL.GetValue_BySelectComboBox(Usage_comboBox, "용도프로필", "용도명", "냉방설정온도"));
                Em = Program.UTIL.ToDoubleOrZero(Program.UTIL.GetValue_BySelectComboBox(Usage_comboBox, "용도프로필", "용도명", "조도"));
                VA = Program.UTIL.ToDoubleOrZero(Program.UTIL.GetValue_BySelectComboBox(Usage_comboBox, "용도프로필", "용도명", "이용일최소외기도입량"));
                VA_we = Program.UTIL.ToDoubleOrZero(Program.UTIL.GetValue_BySelectComboBox(Usage_comboBox, "용도프로필", "용도명", "비이용일최소외기도입량"));
                DHWneed_Cal(DHWneed_1p, PersonNum);
                OccupancyDensity_Cal(PersonNum, NetArea);
                PersonIHG_Cal(PersonIHG, UseTime);
                EquipIHG_Cal(EquipIHG_Time);
                Calc_VentilationVolume(NetArea, NetVolume, VA, VA_we);
                theta_i_h_set_Label.Text = String.Format("{0:F0}", theta_i_h_set) + "℃";
                theta_i_c_set_Label.Text = String.Format("{0:F0}", theta_i_c_set) + "℃";
                Em_Label.Text = String.Format("{0:F0}", Em) + "lx";
            }
        }

        //시작 및 종료시간에 따라 시간 계산 
        private void StartTime_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (StartTime_comboBox.SelectedItem != null)
            { StartTime = StartTime_comboBox.SelectedItem.ToString(); }
            StartTime_image_Label.Text = StartTime;
            Calc_Time();
        }
        //시작 및 종료시간에 따라 시간 계산  
        private void EndTime_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (EndTime_comboBox.SelectedItem != null)
            { EndTime = EndTime_comboBox.SelectedItem.ToString(); }
            EndTime_image_Label.Text = EndTime;
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
                    Program.UTIL.textBox_doubleComa(UseTime_textBox, true, 0);
                    HCTime_textBox.Text = HCTime.ToString();
                    Program.UTIL.textBox_doubleComa(HCTime_textBox, true, 0);
                    PersonIHG_Cal(PersonIHG, UseTime);
                    EquipIHG_Cal(EquipIHG_Time); //usetime 반영되므로 추가 
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
                    Program.UTIL.textBox_doubleComa(UseTime_textBox, true, 0);
                    HCTime_textBox.Text = HCTime.ToString();
                    Program.UTIL.textBox_doubleComa(HCTime_textBox, true, 0);
                    PersonIHG_Cal(PersonIHG, UseTime);
                    EquipIHG_Cal(EquipIHG_Time); //usetime 반영되므로 추가 
                }

            }

        }

        private void PersonNum_textBox_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(PersonNum_textBox.Text) == false)
            {
                PersonNum = Program.UTIL.textBox_doubleComa(PersonNum_textBox, false, 0);
                DHWneed_Cal(DHWneed_1p, PersonNum);
                OccupancyDensity_Cal(PersonNum, NetArea);
                PersonIHG_Cal(PersonIHG, UseTime);
            }
        }
        private void NetArea_textBox_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(NetArea_textBox.Text) == false)
            {
                NetArea = Program.UTIL.textBox_doubleComa(NetArea_textBox, false, 2);
                OccupancyDensity_Cal(PersonNum, NetArea);
                PersonIHG_Cal(PersonIHG, UseTime);
                NetVolume = NetArea * CeilingHeight;

                NetVolume_textBox.Text = NetVolume.ToString();
                Program.UTIL.textBox_doubleComa(NetVolume_textBox, true, 1);

                Calc_VentilationVolume(NetArea, NetVolume, VA, VA_we);
            }
        }

        private void CeilingHeight_textBox_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(CeilingHeight_textBox.Text) == false)
            {
                CeilingHeight = Program.UTIL.textBox_doubleComa(CeilingHeight_textBox, false, 2);
                NetVolume = NetArea * CeilingHeight;

                NetVolume_textBox.Text = NetVolume.ToString();
                Program.UTIL.textBox_doubleComa(NetVolume_textBox, true, 1);

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

        private void HumidC_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (HumidC_comboBox.SelectedItem != null)
            {
                HumidC_index = HumidC_comboBox.SelectedItem.ToString();
                HumidC_Cal();
            }
        }

        private void HumidH_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (HumidH_comboBox.SelectedItem != null)
            {
                HumidH_index = HumidH_comboBox.SelectedItem.ToString();
                HumidH_Cal();
            }
        }

        //냉방 설정 습도등급 선택에 따라 실내 냉방 설정 절대습도 산정 (BaseDB_HCneed.습도설정, EN 16798-1:2017 표B.16 근거, 단위 : g/kg')
        private void HumidC_Cal()
        {
            if (HumidC_index != null)
            {
                string[][] value = Program.DB.getValue(DB.type.BaseDB_HCneed, "습도설정", "냉방설정습도", "등급='" + HumidC_index + "'");
                if (value.Length > 0)
                {
                    HumidC = Program.UTIL.ToDoubleOrZero(value[0][0]);
                    HumidC_textBox.Text = string.Format("{0:F1}", HumidC);
                }
            }
        }

        //난방 설정 습도등급 선택에 따라 실내 난방 설정 절대습도 산정 (BaseDB_HCneed.습도설정, EN 16798-1:2017 표B.16 근거, 단위 : g/kg')
        private void HumidH_Cal()
        {
            if (HumidH_index != null)
            {
                string[][] value = Program.DB.getValue(DB.type.BaseDB_HCneed, "습도설정", "난방설정습도", "등급='" + HumidH_index + "'");
                if (value.Length > 0)
                {
                    HumidH = Program.UTIL.ToDoubleOrZero(value[0][0]);
                    HumidH_textBox.Text = string.Format("{0:F1}", HumidH);
                }
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

                    DHWneed_textBox.Text = DHWneed.ToString();
                    Program.UTIL.textBox_doubleComa(DHWneed_textBox, true, 1);
                    DHWneed_image_Label.Text = string.Format("{0:F1}", (DHWneed)) + "kWh/d";
                }
            }
        }

        private void OccupancyDensity_Cal(double PersonNum, double Area)
        {
            if (PersonNum != 0 && String.IsNullOrEmpty(NetArea_textBox.Text) == false)
            {
                OccupancyDensity = Area / PersonNum;
                OccupancyDensity_textBox.Text = OccupancyDensity.ToString();
                Program.UTIL.textBox_doubleComa(OccupancyDensity_textBox, true, 1);
            }
            else if (PersonNum == 0)
            {
                OccupancyDensity = 0;
                OccupancyDensity_textBox.Text = OccupancyDensity.ToString();
                Program.UTIL.textBox_doubleComa(OccupancyDensity_textBox, true, 0);
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

            double 일일이용시간 = Program.UTIL.ToDoubleOrZero(Program.UTIL.GetValue_BySelectComboBox(Usage_comboBox, "용도프로필", "용도명", "일일이용시간"));
            double 사람일일이용시간 = Program.UTIL.ToDoubleOrZero(Program.UTIL.GetValue_BySelectComboBox(Usage_comboBox, "용도프로필", "용도명", "사람일일이용시간"));

            PersonIHG_1day = PersonIHG * UseTime * 사람일일이용시간 / 일일이용시간;
            PersonIHG_textBox.Text = string.Format("{0:F1}", PersonIHG_1day);
            PersonIHG_image_Label.Text = string.Format("{0:F0}", PersonIHG_1day) + "Wh/m2d";
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
                double 일일이용시간 = Program.UTIL.ToDoubleOrZero(Program.UTIL.GetValue_BySelectComboBox(Usage_comboBox, "용도프로필", "용도명", "일일이용시간"));
                double 기기일일이용시간 = Program.UTIL.ToDoubleOrZero(Program.UTIL.GetValue_BySelectComboBox(Usage_comboBox, "용도프로필", "용도명", "기기일일이용시간"));
                EquipIHG_1day = EquipIHG * UseTime * 기기일일이용시간 / 일일이용시간;

                EquipIHG_textBox.Text = EquipIHG_1day.ToString();
                Program.UTIL.textBox_doubleComa(EquipIHG_textBox, true, 1);
                EquipIHG_image_Label.Text = string.Format("{0:F0}", EquipIHG_1day) + "Wh/m2d";
            }
        }
        private void Calc_VentilationVolume(double Area, double NetVolume, double VA, double VA_we)
        {

            Volume_wd = Area * VA;
            Volume_wd_textBox.Text = Volume_wd.ToString();
            Program.UTIL.textBox_doubleComa(Volume_wd_textBox, true, 1);
            SA_Volume_Label.Text = String.Format("{0:F1}", Volume_wd) + "m3/h";

            Volume_we = Area * VA_we;
            RA_Volume_Label.Text = String.Format("{0:F1}", Volume_wd) + "m3/h";

            if (NetVolume != null && NetVolume != 0)
            { VentilationRate = Volume_wd / NetVolume; }
            else { VentilationRate = 0; }
            VentilationRate_textBox.Text = VentilationRate.ToString();
            Program.UTIL.textBox_doubleComa(VentilationRate_textBox, true, 1);
        }

       
        private void Save_Image()
        {
            try
            {
                // 캡쳐할 영역의 위치와 크기 설정
                Rectangle captureRectangle = Ground_pictureBox.RectangleToScreen(Ground_pictureBox.ClientRectangle); //RectangleToScree는 화면 상 좌표를 읽음, ClientRectangle는 그림의 크기를 읽음 

                // 비트맵 생성
                Bitmap bmp = new Bitmap(captureRectangle.Width, captureRectangle.Height);

                using (Graphics g = Graphics.FromImage(bmp))
                {
                    // 특정 영역을 캡쳐
                    g.CopyFromScreen(captureRectangle.Location, Point.Empty, captureRectangle.Size);
                }

                string pid = "0000-00-00";
                string[][] Value = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "프로젝트번호");
                if (Value.Length > 0)
                {
                    pid = Value[0][0];
                }
                Directory.CreateDirectory(Program.gPath + "\\projects\\" + pid);
                // 저장할 파일 경로 설정
                string ImageName = "/projects/" + pid + "/" + ZoneNum + ".png";
                string imagePath = Program.gPath + ImageName; // 최종 경로


                // 비트맵을 파일로 저장
                bmp.Save(imagePath, System.Drawing.Imaging.ImageFormat.Png);
            }
            catch (Exception ex)
            {
                MessageBox.Show("오류 발생: " + ex.Message);
            }
        }
        public bool ValidateAndSave(bool isManualSave = false)
        {
            try
            {
                // 존 이름조차 없으면 아직 만들다 만 존 → 저장할 것 없음. 화면 전환은 막지 않는다.
                if (string.IsNullOrEmpty(ZoneNum) || ZoneName == null)
                {
                    if (isManualSave && ZoneName == null)
                    {
                        MessageBox.Show("존 이름을 입력하세요.");
                    }
                    return true;
                }

                // 빠진 항목을 모은다.
                List<string> missing = new List<string>();
                if (Ventilation_checkBox.Checked && AHUType == null)
                {
                    missing.Add("환기방식");
                }
                if (Usage == null)
                {
                    missing.Add("용도프로필");
                }
                if (CeilingHeight == 0)
                {
                    missing.Add("천장고");
                }
                if (StartTime_comboBox.SelectedItem == null || EndTime_comboBox.SelectedItem == null)
                {
                    missing.Add("사용시간");
                }
                if (WeekUseDay_comboBox.SelectedItem == null)
                {
                    missing.Add("주이용일");
                }
                if (string.IsNullOrEmpty(PersonNum_textBox.Text))
                {
                    missing.Add("재실자수");
                }

                // 안내(막지는 않는다) + 미입력항목 목록을 DB에 함께 저장한다.
                // DB 저장값은 콤마가 안 들어가도록 '+'로 잇는다.
                if (missing.Count > 0)
                {
                    MessageBox.Show(string.Join(", ", missing) + " 항목이 비어 있습니다.");
                }
                Save(string.Join("+", missing), isManualSave);
                return true;
            }
            catch (Exception ex)
            {
                // 디버깅 중단점 방지를 위해 예외를 무시하거나 로그만 남김
                System.Diagnostics.Debug.WriteLine($"ValidateAndSave 오류: {ex.Message}");
                return true;
            }
        }

        private void Save(string missingItems, bool isManualSave = false)
        {
            Save_Image();
            //존일반정보 폼에 해당하는 정보만 저장
            //건물정보, 3D정보는 저장 안함
            string[][] 프로젝트유형 = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "프로젝트유형번호");
            Program.DB.setValue(DB.type.ProjDB, "ZoneGeneral_Form", "존번호,프로젝트유형,존이름,실제어방식,냉난방유무,환기유무,환기방식," +
                "용도프로필,천장고,시작시간,종료시간,주이용일,재실자수,기기발열수준," +
                "일일급탕요구량,냉난방시간,사용시간,공조시간,연이용일수,재실밀도,재실수준,일일인체발열,면적당인체발열,일일기기발열,면적당기기발열," +
                "순체적,환기횟수,이용일환기량,비이용일환기량,순바닥면적,선택열회수기,기존존,증축여부,냉방습도,난방습도,미입력항목",
            "'" + ZoneNum + "','" + 프로젝트유형[0][0] + "','" + ZoneName + "','" + RoomControl + "','" + HCType + "','" + Ventilation_checkBox.Checked.ToString() + "','" + AHUType + "','"
            + Usage + "','" + CeilingHeight.ToString() + "','" + StartTime + "','" + EndTime + "','" + WeekUseDay.ToString() + "','" + PersonNum + "','" + EquipIHG_index + "','"
            + DHWneed.ToString() + "','" + HCTime.ToString() + "','" + UseTime.ToString() + "','" + AHUTime.ToString() + "','" + AnnualUseDay.ToString() + "','"
            + OccupancyDensity.ToString() + "','" + OccupancyDensity_index + "','" + PersonIHG_1day.ToString() + "','" + PersonIHG.ToString() + "','" + EquipIHG_1day.ToString() + "','" + EquipIHG.ToString() + "','"
            + NetVolume.ToString() + "','" + VentilationRate.ToString() + "','" + Volume_wd.ToString() + "','" + Volume_we.ToString() + "','"
            + NetArea.ToString() + "','" + SelectHRV + "','" + SelectPreZone_nonsplit + "','" + 증축여부 + "','" + HumidC_index + "','" + HumidH_index + "','" + missingItems + "'", "존번호");

                        
        }
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
            StartTime_comboBox.SelectedItem = null;
            EndTime_comboBox.SelectedItem = null;
            Usage_comboBox.SelectedItem = null;
            WeekUseDay_comboBox.SelectedItem = null;

            //WeekUseDay_comboBox.SelectedIndex = 3;
            EquipIHG_comboBox.SelectedIndex = 0;

            HumidC = 0; HumidH = 0;
            HumidC_comboBox.SelectedIndex = 2; //기본값 : 습도고려안함
            HumidH_comboBox.SelectedIndex = 2; //기본값 : 습도고려안함

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
            Calc_Time();
            Calc_Usage();
            DHWneed_image_Label.Text = "";
            EndTime_image_Label.Text = "";
            EquipIHG_image_Label.Text = "";
            PersonIHG_image_Label.Text = "";
            StartTime_image_Label.Text = "";
            theta_i_c_set_Label.Text = "";
            theta_i_h_set_Label.Text = "";
            UseTime_textBox.Text = "";
            VentilationRate_textBox.Text = "";
            Volume_wd_textBox.Text = "";
            CW_textBox.Text = "";
            Wall_textBox.Text = "";
            Roof_textBox.Text = "";
            Window_textBox.Text = "";
            Floor_textBox.Text = "";
            InWall_textBox.Text = "";
            Door_textBox.Text = "";
            OccupancyDensity_textBox.Text = "";
            PreZone_textBox.Text = "";
            SelectPreZone_nonsplit = "";
        }

        public void LoadData(String ID)            // 리스트에서 항목 더블 클릭시 - 뷰를 ID 의 getValue 값으로 채우기
        {
            reset();
            Load_OtherFormData();

            String[][] Value = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_Form", "존이름,실제어방식,냉난방유무,환기유무,환기방식,선택열회수기," +
                "용도프로필,천장고,시작시간,종료시간,주이용일,재실자수,기기발열수준," +
                "일일급탕요구량,냉난방시간,사용시간,공조시간,연이용일수,재실밀도,재실수준,일일인체발열,면적당인체발열,일일기기발열,면적당기기발열," +
                "순체적,환기횟수,이용일환기량,비이용일환기량,순바닥면적,냉방습도,난방습도", "존번호 = '" + ZoneNum + "'");
            if (Value.Length > 0)
            {
                ZoneName = Value[0][0];
                ZoneName_textBox.Text = ZoneName;

                RoomControl = Value[0][1];

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
                    Cooling_checkBox.Checked = true;
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
                Load_MainImage(HCType, AHUType);

                SelectHRV = Value[0][5];
                AHU_textBox.Text = SelectHRV;

                Usage_comboBox.SelectedItem = Usage;
                DataRowView? item = Usage_comboBox.SelectedItem as DataRowView;

                if (item != null)
                {
                    Usage = item.Row.ItemArray[0].ToString();
                }
                if (Value[0][28] != "")
                {
                    NetArea = Program.UTIL.ToDoubleOrZero(Value[0][28]);
                    NetArea_textBox.Text = NetArea.ToString();
                    Program.UTIL.textBox_doubleComa(NetArea_textBox, true, 2);
                }
                if (Value[0][29] != "")
                {
                    HumidC_index = Value[0][29];
                    HumidC_comboBox.SelectedItem = HumidC_index;
                }
                if (Value[0][30] != "")
                {
                    HumidH_index = Value[0][30];
                    HumidH_comboBox.SelectedItem = HumidH_index;
                }
                if (Value[0][7] != "")
                {
                    CeilingHeight = Program.UTIL.ToDoubleOrZero(Value[0][7]);
                    CeilingHeight_textBox.Text = CeilingHeight.ToString();
                    Program.UTIL.textBox_doubleComa(CeilingHeight_textBox, true, 2);
                }
                if (Value[0][8] != "")
                {
                    StartTime = Value[0][8];
                    StartTime_comboBox.SelectedItem = StartTime;
                }
                if (Value[0][9] != "")
                {
                    EndTime = Value[0][9];
                    EndTime_comboBox.SelectedItem = EndTime;
                }
                if (Value[0][10] != "")
                {
                    WeekUseDay = Program.UTIL.ToDoubleOrZero(Value[0][10]);
                    WeekUseDay_comboBox.SelectedItem = "주 " + WeekUseDay.ToString() + ".0 일 근무";
                }
                if (Value[0][11] != "")
                {
                    PersonNum = Program.UTIL.ToDoubleOrZero(Value[0][11]);
                    PersonNum_textBox.Text = PersonNum.ToString();
                    Program.UTIL.textBox_doubleComa(PersonNum_textBox, true, 0);
                }
                if (Value[0][12] != "")
                {
                    EquipIHG_index = Value[0][12];
                    EquipIHG_comboBox.SelectedItem = EquipIHG_index;
                }
                if (Value[0][13] != "")
                {
                    DHWneed = Program.UTIL.ToDoubleOrZero(Value[0][13]);
                    DHWneed_textBox.Text = DHWneed.ToString();
                    Program.UTIL.textBox_doubleComa(DHWneed_textBox, true, 1);
                    DHWneed_image_Label.Text = string.Format("{0:F1}", (DHWneed)) + "kWh/d";
                }
                if (Value[0][14] != "")
                {
                    HCTime = Program.UTIL.ToDoubleOrZero(Value[0][14]);
                    HCTime_textBox.Text = HCTime.ToString();
                    Program.UTIL.textBox_doubleComa(HCTime_textBox, true, 0);
                }
                if (Value[0][15] != "")
                {
                    UseTime = Program.UTIL.ToDoubleOrZero(Value[0][15]);
                    UseTime_textBox.Text = UseTime.ToString();
                    Program.UTIL.textBox_doubleComa(UseTime_textBox, true, 0);
                }
                if (Value[0][16] != "")
                {
                    AHUTime = Program.UTIL.ToDoubleOrZero(Value[0][16]);
                }
                if (Value[0][17] != "")
                {
                    AnnualUseDay = Program.UTIL.ToDoubleOrZero(Value[0][17]);
                    AnnualUseDay_textBox.Text = AnnualUseDay.ToString();
                    Program.UTIL.textBox_doubleComa(AnnualUseDay_textBox, true, 0);
                }
                if (Value[0][18] != "")
                {
                    OccupancyDensity = Program.UTIL.ToDoubleOrZero(Value[0][18]);
                    OccupancyDensity_textBox.Text = OccupancyDensity.ToString();
                    Program.UTIL.textBox_doubleComa(OccupancyDensity_textBox, true, 1);
                }
                if (Value[0][19] != "")
                {
                    OccupancyDensity_index = Value[0][19];
                    OccupancyDensity_index_textBox.Text = OccupancyDensity_index;
                }
                if (Value[0][20] != "")
                {
                    PersonIHG_1day = Program.UTIL.ToDoubleOrZero(Value[0][20]);
                    PersonIHG = Program.UTIL.ToDoubleOrZero(Value[0][21]);
                    PersonIHG_textBox.Text = PersonIHG_1day.ToString();
                    Program.UTIL.textBox_doubleComa(PersonIHG_textBox, true, 1);
                    PersonIHG_image_Label.Text = string.Format("{0:F0}", PersonIHG_1day) + "Wh/m2d";
                }
                if (Value[0][22] != "")
                {
                    EquipIHG_1day = Program.UTIL.ToDoubleOrZero(Value[0][22]);
                    EquipIHG = Program.UTIL.ToDoubleOrZero(Value[0][22]);
                    EquipIHG_textBox.Text = EquipIHG_1day.ToString();
                    Program.UTIL.textBox_doubleComa(EquipIHG_textBox, true, 1);
                    EquipIHG_image_Label.Text = string.Format("{0:F0}", EquipIHG_1day) + "Wh/m2d";
                }
                if (Value[0][24] != "")
                {
                    NetVolume = Program.UTIL.ToDoubleOrZero(Value[0][24]);
                    NetVolume_textBox.Text = NetVolume.ToString();
                    Program.UTIL.textBox_doubleComa(NetVolume_textBox, true, 1);
                }
                if (Value[0][25] != "")
                {
                    VentilationRate = Program.UTIL.ToDoubleOrZero(Value[0][25]);
                    VentilationRate_textBox.Text = VentilationRate.ToString();
                    Program.UTIL.textBox_doubleComa(VentilationRate_textBox, true, 1);
                }
                if (Value[0][26] != "")
                {
                    Volume_wd = Program.UTIL.ToDoubleOrZero(Value[0][26]);
                    Volume_wd_textBox.Text = Volume_wd.ToString();
                    Program.UTIL.textBox_doubleComa(Volume_wd_textBox, true, 1);
                    SA_Volume_Label.Text = String.Format("{0:F1}", Volume_wd) + "m3/h";
                    RA_Volume_Label.Text = String.Format("{0:F1}", Volume_wd) + "m3/h";
                }
                if (Value[0][27] != "")
                {
                    Volume_we = Program.UTIL.ToDoubleOrZero(Value[0][27]);
                }
            }
            Value = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_Form", "기존존,증축여부", "존번호 = '" + ZoneNum + "'");
            if (Value.Length > 0)
            {
                SelectPreZone_nonsplit = Value[0][0];
                Split_Zone(SelectPreZone_nonsplit);
                if (Value[0][0] == "true")
                {
                    radioButton1.Checked = true;
                }
                else
                {
                    radioButton2.Checked = true;
                }
            }

            Calc_Time();
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
                int v1 = ID.IndexOf("Zone") + 4; //Zone 번호 위치 
                int v2 = ID.IndexOf("_", v1); //Zone 다음 "_"의 위치 
                if (v2 < 0)
                {
                    v2 = ID.IndexOf("\"", v1); //Zone 다음 "_"의 위치 
                }
                ID = ID.Substring(19, v2 - 19);
                Num_textBox.Text = ID;
                ZoneNum = ID;
                LoadData(ZoneNum);
            }
        }
        private void Load_OtherFormData()
        {
            //건물대상,용도
            String[][] BuildingValue = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "건물대상,건물용도,프로젝트유형번호", "");
            if (BuildingValue.Length > 0)
            {
                BuildingCategory = BuildingValue[0][0];
                BuildingUse = BuildingValue[0][1];
                if (BuildingValue[0][2] == "1")
                {
                    PreZone_label.Visible = false;
                    PreZone_textBox.Visible = false;
                    PreZone_button.Visible = false;
                    groupBox1.Visible = false;
                    radioButton1.Checked = false;
                    radioButton2.Checked = true;
                    SelectPreZone_nonsplit = ZoneNum;
                }
                else if (BuildingValue[0][2] == "2")
                {
                    PreZone_label.Visible = true;
                    PreZone_textBox.Visible = true;
                    PreZone_button.Visible = true;
                    groupBox1.Visible = true;
                    radioButton1.Checked = false;
                    radioButton2.Checked = true;
                    groupBox1.Enabled = false;
                    SelectPreZone_nonsplit = ZoneNum;
                }
                else if (BuildingValue[0][2] == "3")
                {
                    PreZone_label.Visible = true;
                    PreZone_textBox.Visible = true;
                    PreZone_button.Visible = true;
                    groupBox1.Visible = true;
                    radioButton1.Checked = false;
                    radioButton2.Checked = true;
                    groupBox1.Enabled = true;
                }
                else
                {
                    PreZone_label.Visible = false;
                    PreZone_textBox.Visible = false;
                    PreZone_button.Visible = false;
                    groupBox1.Visible = false;
                    radioButton1.Checked = false;
                    radioButton2.Checked = true;
                }
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
            }
            if (General_3D.Length > 0)
            {


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
                                                Wall_d += Program.UTIL.ToDoubleOrZero(Wall_Value[0][2 * k + 6]) / 1000;
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
                                Wall_d = 0.075 + Program.UTIL.ToDoubleOrZero(Wall_Value[0][2]) / 1000; //내단열로 가정함
                            }
                        }
                        else
                        {

                            // Wall_d = 0.015 + Program.UTIL.ToDoubleOrZero(Wall[j][2]) / 2;
                            Wall_d = 0.015 + Program.UTIL.ToDoubleOrZero(Wall_Value[0][2]) / 1000;
                        }

                        double value;
                        if (double.TryParse(Wall[j][0], out value))
                        {
                            Wall_A = Wall_d * Program.UTIL.ToDoubleOrZero(Wall[j][0]);
                            Area_WallInWall += Wall_A;
                        }
                        else
                        {
                            string[][] floor = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_3D", "바닥면적", "존번호='" + ZoneNum + "'");
                            if (floor.Length > 0)
                            {
                                NetArea = Program.UTIL.ToDoubleOrZero(floor[0][0]) * 0.85;
                                goto _goto;
                            }
                        }
                    }
                }

                String[][] InWall = Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope_3D", "벽체길이", "존 = '" + ZoneNum + "' And 외피유형 = '내벽'");
                if (InWall.Length > 0)
                {
                    for (int j = 0; j < InWall.Length; j++)
                    {
                        double InWall_d, InWall_A;
                        InWall_d = 0.05;
                        InWall_A = InWall_d * Program.UTIL.ToDoubleOrZero(InWall[j][0]);
                        Area_WallInWall += InWall_A;
                    }
                }

                NetArea = Program.UTIL.ToDoubleOrZero(General_3D[0][2]) - Area_WallInWall;

            _goto:
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
                            Construction_AreaSum[k] += Program.UTIL.ToDoubleOrZero(Envelope_3D[i][1]);
                        }
                    }
                }
                if (Construction_AreaSum[0] != 0)
                {
                    CW_textBox.Text = Construction_AreaSum[0].ToString();
                    Program.UTIL.textBox_doubleComa(CW_textBox, true, 1);
                    CW_textBox.Text = CW_textBox.Text.ToString() + "m2";
                }
                if (Construction_AreaSum[1] != 0)
                {
                    Wall_textBox.Text = Construction_AreaSum[1].ToString();
                    Program.UTIL.textBox_doubleComa(Wall_textBox, true, 1);
                    Wall_textBox.Text = Wall_textBox.Text.ToString() + "m2";
                }

                if (Construction_AreaSum[2] != 0)
                {
                    Roof_textBox.Text = Construction_AreaSum[2].ToString();
                    Program.UTIL.textBox_doubleComa(Roof_textBox, true, 1);
                    Roof_textBox.Text = Roof_textBox.Text.ToString() + "m2";
                }

                if (Construction_AreaSum[3] != 0)
                {
                    Floor_textBox.Text = Construction_AreaSum[3].ToString();
                    Program.UTIL.textBox_doubleComa(Floor_textBox, true, 1);
                    Floor_textBox.Text = Floor_textBox.Text.ToString() + "m2";
                }

                if (Construction_AreaSum[4] != 0)
                {
                    Window_textBox.Text = Construction_AreaSum[4].ToString();
                    Program.UTIL.textBox_doubleComa(Window_textBox, true, 1);
                    Window_textBox.Text = Window_textBox.Text.ToString() + "m2";
                }

                if (Construction_AreaSum[5] != 0)
                {
                    Door_textBox.Text = Construction_AreaSum[5].ToString();
                    Program.UTIL.textBox_doubleComa(Door_textBox, true, 1);
                    Door_textBox.Text = Door_textBox.Text.ToString() + "m2";
                }

                if (Construction_AreaSum[6] != 0)
                {
                    InWall_textBox.Text = Construction_AreaSum[6].ToString();
                    Program.UTIL.textBox_doubleComa(InWall_textBox, true, 1);
                    InWall_textBox.Text = InWall_textBox.Text.ToString() + "m2";
                }

                if (Construction_AreaSum[3] > 0) //최하층 바닥 존재
                {
                    String[][] Value = Program.DB.querySQL(DB.type.ProjDB, "Select a.기초설치 From ConstructionFloor as a Inner Join ZoneEnvelope_3D as b on a.번호=b.구조체번호 Where b.존 = '" + ZoneNum + "'");
                    if (Value.Length > 0)
                    {
                        if (Value[0][0] == "비단열지하실" || Value[0][0] == "단열지하실" || Value[0][0] == "지면위")
                        {
                            Ground = Value[0][0];
                        }
                        else
                        {
                            Ground = "층간슬라브";
                        }
                    }
                }
                else
                {
                    Ground = "층간슬라브";
                }

                Load_GroundImage();
            }

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                증축여부 = "true";
            }
            else
            {
                증축여부 = "false";
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                증축여부 = "false";
            }
            else
            {
                증축여부 = "true";
            }
        }

        private void info_Click(object sender, EventArgs e)
        {
            string basePath = Program.gPath + "Manual\\1.contents\\13.ZoneGeneral";

            // 경로가 존재하는지 확인
            if (Directory.Exists(basePath))
            {
                SlideViewer slideViewer = new SlideViewer(basePath);
                slideViewer.Show();
            }
            else
            {
                MessageBox.Show("The folder path does not exist.");
            }
        }
    }
}
