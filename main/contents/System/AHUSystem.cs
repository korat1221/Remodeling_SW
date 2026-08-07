using main.contentslist;
using main.info;
using main.subcontents;
using main.subcontents.AHUSystem;
using main.subcontents.ConstructionCW;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Diagnostics;
using System.Diagnostics.Contracts;


namespace main.contents
{
    public partial class AHUSystem : Form, IConfirmable
    {
        string Type;
        String Num; string Name; String AHUOptions;
        String AHULocation, AHUVolumeControl, AHULeakageTestMethod, AHULeakageLevel1, AHULeakageLevel2;
        double AHUInsulationThickness;
        string DuctLeakageLevel;
        double OALength, EALength;
        double SALength, RALength, DuctInsulationThickness, DuctDiameter;
        ArrayList SelectZone_split = new ArrayList();
        String TABOptions, PipeIns; double PipeIns_Ramda;
        string PrehPrecOptions, GroundOptions, CooltubeMaterial;
        double PrehPower, GroundDepth, CooltubeDiameter, CooltubeThickness, CooltubeLength;
        double Preh_PumpPower, Preh_CoilPower;
        double AnnualCoolingNeed, AnnualHeatingNeed;
        double CoolingLoad, HeatingLoad;
        double 계산된_냉방출력, 계산된_난방출력;
        string HRVType, CoilType;


        string[][] 프로젝트유형;
        public AHUSystem()
        {

            InitializeComponent(); this.Font = new Font(UTIL.Families[0], 9.75F, FontStyle.Regular);
            string[][] Image = Program.DB.getValue(DB.type.BaseDB_HCneed, "메뉴아이콘", "하위메뉴아이콘", "하위메뉴명 = '공조시스템'");
            if (Image.Length > 0)
            {
                Icon_pictureBox.Load(Program.gPath + Image[0][0]);
                Icon_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            }
            프로젝트유형 = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "프로젝트유형번호");

            //기계환기유형 콤보박스
            AHUOptions_comboBox.Items.Clear();
            AHUOptions_comboBox.Items.Add("열회수기");
            AHUOptions_comboBox.Items.Add("공조기");

            //공조기설치위치 콤보박스
            AHULocation_comboBox.Items.Clear();
            AHULocation_comboBox.Items.Add("단열외피 내부");
            AHULocation_comboBox.Items.Add("단열외피 외부");
            AHULocation_comboBox.Items.Add("외기");

            //공조기누기시험방법 콤보박스
            AHULeakageTestMethod_comboBox.Items.Clear();
            AHULeakageTestMethod_comboBox.Items.Add("압력차 누기시험");
            AHULeakageTestMethod_comboBox.Items.Add("챔버 내 가스추적시험");
            AHULeakageTestMethod_comboBox.Items.Add("덕트 내 가스추적시험");
            AHULeakageTestMethod_comboBox.Items.Add("시험 미실시");

            //공조기풍량제어 콤보박스
            AHUVolumeControl_comboBox.Items.Clear();
            AHUVolumeControl_comboBox.Items.Add("제어없음");
            AHUVolumeControl_comboBox.Items.Add("시간제어");
            AHUVolumeControl_comboBox.Items.Add("중앙제어");
            AHUVolumeControl_comboBox.Items.Add("실내공기질 개별제어");


            //TAB 콤보박스
            TABOptions_comboBox.Items.Clear();
            TABOptions_comboBox.Items.Add("TAB미실시");
            TABOptions_comboBox.Items.Add("TAB실시");

            //공조기단열두께 콤보박스
            AHUInsulationThickness_comboBox.Items.Clear();
            AHUInsulationThickness_comboBox.Items.Add("10");
            AHUInsulationThickness_comboBox.Items.Add("20");
            AHUInsulationThickness_comboBox.Items.Add("30");
            AHUInsulationThickness_comboBox.Items.Add("40");

            //예열/예냉유형 콤보박스
            PrehPrecOptions_comboBox.Items.Clear();
            PrehPrecOptions_comboBox.Items.Add("없음");
            PrehPrecOptions_comboBox.Items.Add("전기예열기");
            PrehPrecOptions_comboBox.Items.Add("온수예열기");

            //토양유형 콤보박스
            GroundOptions_comboBox.Items.Clear();
            GroundOptions_comboBox.Items.Add("습한 흙");
            GroundOptions_comboBox.Items.Add("건조한 모래");
            GroundOptions_comboBox.Items.Add("습한 모래");
            GroundOptions_comboBox.Items.Add("습한 진흙");
            GroundOptions_comboBox.Items.Add("젖은 진흙");

            //쿨튜브재질 콤보박스
            CooltubeMaterial_comboBox.Items.Clear();
            CooltubeMaterial_comboBox.Items.Add("PP");
            CooltubeMaterial_comboBox.Items.Add("PE");
            CooltubeMaterial_comboBox.Items.Add("PVC");
            CooltubeMaterial_comboBox.Items.Add("철근콘크리트");

            //덕트단열재 설정
            PipeIns_Ramda = 0.035;
            PipeIns_Ramda_textBox.Text = PipeIns_Ramda.ToString();
            PipeIns_textBox.Text = "보온단열재";
            PipeIns = "보온단열재";

            LocationpictureBox.Parent = ImagePanel;
            MainpictureBox.Parent = LocationpictureBox;
            PrehPrecpictureBox.Parent = MainpictureBox;
            hcoilpictureBox.Parent = MainpictureBox;
            ccoilpictureBox.Parent = MainpictureBox;
            HumidifierpictureBox.Parent = MainpictureBox;
            VAVpictureBox.Parent = MainpictureBox;
        }
        private void GeneralPanel_Paint(object sender, PaintEventArgs e)
        {
            Panel p = (Panel)sender;
            ControlPaint.DrawBorder(e.Graphics, p.DisplayRectangle, System.Drawing.SystemColors.Control, ButtonBorderStyle.Solid);
        }
        private void ImagePanel_Paint(object sender, PaintEventArgs e)
        {
            Panel p = (Panel)sender;
            ControlPaint.DrawBorder(e.Graphics, p.DisplayRectangle, System.Drawing.SystemColors.Control, ButtonBorderStyle.Solid);
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            Type = "기존";
            Check_DuctLeakageLevel();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            Type = "신규";
            Check_DuctLeakageLevel();
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            Type = "철거 후 신규";
            Check_DuctLeakageLevel();
        }
        private void AHUOptions_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (AHUOptions_comboBox.SelectedItem != null)
            {
                if (AHUOptions != AHUOptions_comboBox.SelectedItem.ToString())
                { Clear_SelectSystem(); }
                else { }

                AHUOptions = AHUOptions_comboBox.SelectedItem.ToString();
                ChangeAHUOptions(AHUOptions);
            }
            else
            {
                AHUOptions = "";
            }
        }
        private void Clear_SelectSystem()
        {
            Num = ""; Name = "";
            Num_textBox.Text = "";
            Name_textBox.Text = "";
            HRV_dataGridView.Columns.Clear();
            HRV_dataGridView.Rows.Clear();
        }
        private void ChangeAHUOptions(String AHUOptions)
        {
            if (AHUOptions == "열회수기")
            {
                AHU_tabPage.Text = "열회수기";
                TABOptions_label.Visible = false;
                TABOptions_comboBox.Visible = false;
                TABOptions = "";
            }
            else if (AHUOptions == "공조기")
            {
                AHU_tabPage.Text = "공조기";
                TABOptions_label.Visible = true;
                TABOptions_comboBox.Visible = true;
            }
            //  Load_Zone();
        }

        private void AHUoptions_button_Click(object sender, EventArgs e)
        {
            if (AHUOptions == "열회수기")
            {
                Load_Systemform();
            }
            else if (AHUOptions == "공조기")
            {
                Load_Systemform();
            }
        }

        private void AHUZone_Button_Click(object sender, EventArgs e)
        {
            AHUZoneVent form = new AHUZoneVent(AHUOptions, Num);
            form.ShowDialog();
        }

        ///////////////////////////////////////////////////////////존////////////////////////////////////////////////////////////////////
        #region 존

        private void Load_Zone()
        {
            String 내용 = "";
            SelectZone_split.Clear();
            string[][] value = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_Form", "존번호", "선택열회수기 = '" + Num + "' and 환기유무='True'");
            if (value.Length > 0)
            {
                for (int k = 0; k < value.Length; k++)
                {
                    SelectZone_split.Add(value[k][0]);
                }
                if (value.Length > 1) { 내용 = SelectZone_split[0].ToString() + " 외 " + (SelectZone_split.Count - 1).ToString() + "개"; }
                else { 내용 = SelectZone_split[0].ToString(); }
            }
            Zone_textBox.Text = 내용;
            Cal_Qb();
        }


        private void Cal_Qb()
        {
            AnnualHeatingNeed = 0;
            HeatingLoad = 0;
            AnnualCoolingNeed = 0;
            CoolingLoad = 0;
            for (int i = 0; i < SelectZone_split.Count; i++)
            {
                string[][] 난방 = Program.DB.getValue(DB.type.ProjDB, "Zone_HCneed_Result", "Qb_a,Q_max", "번호 ='" + SelectZone_split[i] + "' AND 난방_냉방 = '난방'");
                if (난방.Length > 0)
                {
                    AnnualHeatingNeed += Program.UTIL.ToDoubleOrZero(난방[0][0]);
                    AnnualHeatingNeed_textBox.Text = AnnualHeatingNeed.ToString();
                    Program.UTIL.textBox_doubleComa(AnnualHeatingNeed_textBox, true, 0);

                    HeatingLoad += Program.UTIL.ToDoubleOrZero(난방[0][1]) / 1000;
                    HeatingLoad_textBox.Text = HeatingLoad.ToString();
                    Program.UTIL.textBox_doubleComa(HeatingLoad_textBox, true, 2);
                }
                string[][] 냉방 = Program.DB.getValue(DB.type.ProjDB, "Zone_HCneed_Result", "Qb_a,Q_max", "번호 ='" + SelectZone_split[i] + "' AND 난방_냉방 = '냉방'");
                if (냉방.Length > 0)
                {
                    AnnualCoolingNeed += Program.UTIL.ToDoubleOrZero(냉방[0][0]);
                    AnnualCoolingNeed_textBox.Text = AnnualCoolingNeed.ToString();
                    Program.UTIL.textBox_doubleComa(AnnualCoolingNeed_textBox, true, 0);

                    CoolingLoad += Program.UTIL.ToDoubleOrZero(냉방[0][1]) / 1000;
                    CoolingLoad_textBox.Text = CoolingLoad.ToString();
                    Program.UTIL.textBox_doubleComa(CoolingLoad_textBox, true, 2);
                }
            }
        }
        #endregion

        /////////////////////////////////////////////////////열회수기 공조기//////////////////////////////////////////////////////////////
        #region 열회수기 공조기

        private void AHULeakageTestMethod_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (AHULeakageTestMethod_comboBox.SelectedItem != null && AHULeakageTestMethod_comboBox.SelectedItem.ToString() != "")
            {
                AHULeakageTestMethod = AHULeakageTestMethod_comboBox.SelectedItem.ToString();
                Change_AHULeakageOptions(AHULeakageTestMethod);
            }
            else
            {
                AHULeakageTestMethod = "";
                Change_AHULeakageOptions(AHULeakageTestMethod);
            }
        }


        private void AHULeakageLevel_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (AHULeakageLevel_comboBox.SelectedItem != null && AHULeakageLevel_comboBox.SelectedItem.ToString() != "")
            {
                AHULeakageLevel1 = AHULeakageLevel_comboBox.SelectedItem.ToString();
                Chaeck_AHULeakageLevel(AHULeakageLevel1);
            }
            else
            {
                AHULeakageLevel1 = "";
                Chaeck_AHULeakageLevel(AHULeakageLevel1);
            }
        }
        private void Change_AHULeakageOptions(string AHULeakageTestMethod)
        {
            //공조기누기시험방법 콤보박스
            if (AHULeakageTestMethod == "압력차 누기시험")
            {
                AHULeakageLevel_label.Visible = true;
                AHULeakageLevel_comboBox.Visible = true;
                AHULeakageLevel_comboBox.Items.Clear();
                AHULeakageLevel_comboBox.Items.Add("A1[누기율≤ 2%]");
                AHULeakageLevel_comboBox.Items.Add("A2[누기율≤ 5%]");
                AHULeakageLevel_comboBox.Items.Add("A3[누기율≤ 10%]");
            }
            else if (AHULeakageTestMethod == "챔버 내 가스추적시험")
            {
                AHULeakageLevel_label.Visible = true;
                AHULeakageLevel_comboBox.Visible = true;
                AHULeakageLevel_comboBox.Items.Clear();
                AHULeakageLevel_comboBox.Items.Add("B1[SA 공기 내 가스비율≤ 1%]");
                AHULeakageLevel_comboBox.Items.Add("B2[SA 공기 내 가스비율≤ 2%]");
                AHULeakageLevel_comboBox.Items.Add("B3[SA 공기 내 가스비율≤ 6%]");

            }
            else if (AHULeakageTestMethod == "덕트 내 가스추적시험")
            {
                AHULeakageLevel_label.Visible = true;
                AHULeakageLevel_comboBox.Visible = true;
                AHULeakageLevel_comboBox.Items.Clear();
                AHULeakageLevel_comboBox.Items.Add("C1[SA 공기 내 가스비율≤ 0.5%]");
                AHULeakageLevel_comboBox.Items.Add("C2[SA 공기 내 가스비율≤ 2%]");
                AHULeakageLevel_comboBox.Items.Add("C3[SA 공기 내 가스비율≤ 4%]");
            }
            else
            {
                AHULeakageLevel_label.Visible = false;
                AHULeakageLevel_comboBox.Visible = false;
                AHULeakageLevel2 = "A3/B3/C3";
            }
        }
        private void Chaeck_AHULeakageLevel(string AHULeakageLevel1)
        {
            if (AHULeakageLevel1 == "A1[누기율≤ 2%]" || AHULeakageLevel1 == "B1[SA 공기 내 가스비율≤ 1 %]" || AHULeakageLevel1 == "C1[SA 공기 내 가스비율≤ 0.5%]")
            {
                AHULeakageLevel2 = "A1/B1/C1";
            }
            else if (AHULeakageLevel1 == "A2[누기율≤ 5%]" || AHULeakageLevel1 == "B2[SA 공기 내 가스비율≤ 2%]" || AHULeakageLevel1 == "C2[SA 공기 내 가스비율≤ 2%]")
            {
                AHULeakageLevel2 = "A2/B2/C2";
            }
            else
            {
                AHULeakageLevel2 = "A3/B3/C3";
            }
        }

        private void TABOptions_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (TABOptions_comboBox.SelectedItem != null)
            {
                TABOptions = TABOptions_comboBox.SelectedItem.ToString();
                Check_DuctLeakageLevel();
            }
            else
            {
                TABOptions = "";
                Check_DuctLeakageLevel();
            }
        }

        private void Check_DuctLeakageLevel()
        {
            if (TABOptions == "TAB실시")
            {
                DuctLeakageLevel = "TAB실시";
            }
            else if (Type == "신규" || Type == "철거 후 신규")
            {
                DuctLeakageLevel = "신축건물";
            }
            else
            {
                DuctLeakageLevel = "기존건물";
            }
        }

        private void Load_Systemform()
        {
            if (AHUOptions == "열회수기")
            {
                AHU_HRV AHU_HRV = new AHU_HRV(AHUOptions, Num);
                DialogResult result = AHU_HRV.ShowDialog();
                if (result == DialogResult.OK)
                {
                    if (AHU_HRV.SelectSystem != null)
                    {
                        Num = AHU_HRV.SelectSystem;
                        load_Table_HRV(Num);
                        Load_Zone();
                        LoadLocationImage();
                        LoadMainImage();
                        LoadPrehPrecImage();
                    }

                }
            }
            else
            {
                AHU_HRV AHU_HRV = new AHU_HRV(AHUOptions, Num);
                DialogResult result = AHU_HRV.ShowDialog();
                if (result == DialogResult.OK)
                {
                    if (AHU_HRV.SelectSystem != null)
                    {
                        Num = AHU_HRV.SelectSystem;
                        load_Table_AHU(Num);
                        Load_Zone();
                        LoadMainImage();
                        LoadLocationImage();
                        LoadPrehPrecImage();
                    }
                }
            }
        }
        private void load_Table_AHU(string SelectAHU)
        {

            new StackedHeaderDecorator(HRV_dataGridView, DataGridViewAutoSizeColumnsMode.None);
            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
            HRV_dataGridView.Columns.Clear();

            HRV_dataGridView.Columns.Add("A0", "번호");
            HRV_dataGridView.Columns.Add("A1", "명칭");
            HRV_dataGridView.Columns.Add("A2", "공조방식");
            HRV_dataGridView.Columns.Add("A3", "열회수.유형");
            HRV_dataGridView.Columns.Add("A4", "열회수.온도교환효율.냉방.[%]");
            HRV_dataGridView.Columns.Add("A5", "열회수.온도교환효율.난방.[%]");
            HRV_dataGridView.Columns.Add("A6", "열회수.유효전열교환효율.냉방.[%]");
            HRV_dataGridView.Columns.Add("A7", "열회수.유효전열교환효율.난방.[%]");
            HRV_dataGridView.Columns.Add("A8", "열회수.습도교환효율.냉방.[%]");
            HRV_dataGridView.Columns.Add("A9", "열회수.습도교환효율.난방.[%]");
            HRV_dataGridView.Columns.Add("A10", "냉각코일.출력.[kW]");
            HRV_dataGridView.Columns.Add("A11", "냉각코일.입구온도.[℃_DB]");
            HRV_dataGridView.Columns.Add("A12", "냉각코일.입구온도.[℃_WB]");
            HRV_dataGridView.Columns.Add("A13", "냉각코일.출구온도.[℃_DB]");
            HRV_dataGridView.Columns.Add("A14", "냉각코일.출구온도.[℃_WB]");
            HRV_dataGridView.Columns.Add("A15", "난방코일.출력.[kW]");
            HRV_dataGridView.Columns.Add("A16", "난방코일.입구온도.[℃_DB]");
            HRV_dataGridView.Columns.Add("A17", "난방코일.출구온도.[℃_DB]");
            HRV_dataGridView.Columns.Add("A18", "가습기.유형");
            HRV_dataGridView.Columns.Add("A19", "가습기.습도수준");
            HRV_dataGridView.Columns["A19"].Visible = false; // 계산에 미사용(가습 에너지소요량 공식에 습도수준 항목 없음)
            HRV_dataGridView.Columns.Add("A20", "가습기.용량.[kg/h]");
            HRV_dataGridView.Columns.Add("A21", "송풍기.풍량.급기.[CMH]");
            HRV_dataGridView.Columns.Add("A22", "송풍기.풍량.배기.[CMH]");
            HRV_dataGridView.Columns.Add("A23", "송풍기.정압.급기.[Pa]");
            HRV_dataGridView.Columns.Add("A24", "송풍기.정압.배기.[Pa]");
            HRV_dataGridView.Columns["A23"].Visible = false; // 계산에 미사용(Cal_AHU.cs Pressure_SA 참조 없음)
            HRV_dataGridView.Columns["A24"].Visible = false; // 계산에 미사용(Cal_AHU.cs Pressure_EA 참조 없음)
            HRV_dataGridView.Columns.Add("A25", "송풍기.팬동력.급기.[kW]");
            HRV_dataGridView.Columns.Add("A26", "송풍기.팬동력.배기.[kW]");
            HRV_dataGridView.Columns.Add("A27", "송풍기.모터제어");
            for (int i = 0; i < 28; i++)
            { HRV_dataGridView.Columns[i].Width = 80; }



            string[][] Value = Program.DB.getValue(DB.type.ProjDB, "User_AHU", "번호,명칭,공조방식,열회수유형,온도교환효율_냉방,온도교환효율_난방,전열교환효율_냉방,전열교환효율_난방,습도교환효율_냉방,습도교환효율_난방,냉각코일출력,냉각코일_입구_건구온도,냉각코일_입구_습구온도,냉각코일_출구_건구온도,냉각코일_출구_습구온도,난방코일출력,난방코일_입구온도,난방코일_출구온도,가습기유형,가습기습도수준,가습기용량,급기풍량,배기풍량,급기정압,배기정압,급기팬동력,배기팬동력,모터제어,설치유형", "번호='" + SelectAHU + "'");
            if (Value.Length > 0)
            {
                Num_textBox.Text = SelectAHU;
                Name = Value[0][1];
                Name_textBox.Text = Name;

                int nRow = HRV_dataGridView.Rows.Add();
                for (int i = 0; i < 28; i++)
                { HRV_dataGridView.Rows[nRow].Cells[i].Value = Value[0][i]; }

                if (Value[0][28] == "기존") { radioButton1.Checked = true; radioButton2.Checked = false; radioButton3.Checked = false; }
                else if (Value[0][28] == "신규") { radioButton1.Checked = false; radioButton2.Checked = true; radioButton3.Checked = false; }
                else { radioButton1.Checked = false; radioButton2.Checked = false; radioButton3.Checked = true; }

                Cal_Coil(Program.UTIL.ToDoubleOrZero(Value[0][11]), Program.UTIL.ToDoubleOrZero(Value[0][12]), Program.UTIL.ToDoubleOrZero(Value[0][13]), Program.UTIL.ToDoubleOrZero(Value[0][14]), Program.UTIL.ToDoubleOrZero(Value[0][16]), Program.UTIL.ToDoubleOrZero(Value[0][17]), Program.UTIL.ToDoubleOrZero(Value[0][21]));
            }
        }
        private void load_Table_HRV(string SelectHRV)
        {

            new StackedHeaderDecorator(HRV_dataGridView, DataGridViewAutoSizeColumnsMode.Fill);
            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
            HRV_dataGridView.Columns.Clear();

            HRV_dataGridView.Columns.Add("A0", "번호");
            HRV_dataGridView.Columns.Add("A1", "명칭");
            HRV_dataGridView.Columns.Add("A2", "열회수.유형");
            HRV_dataGridView.Columns.Add("A3", "열회수.온도교환효율.냉방.[%]");
            HRV_dataGridView.Columns.Add("A4", "열회수.온도교환효율.난방.[%]");
            HRV_dataGridView.Columns.Add("A5", "열회수.습도교환효율.냉방.[%]");
            HRV_dataGridView.Columns.Add("A6", "열회수.습도교환효율.난방.[%]");
            HRV_dataGridView.Columns.Add("A7", "팬.풍량.[CMH]");
            HRV_dataGridView.Columns.Add("A8", "팬.정압.[Pa]");
            HRV_dataGridView.Columns["A8"].Visible = false; // 계산에 미사용(Cal_AHU.cs 정압 참조 없음)
            HRV_dataGridView.Columns.Add("A9", "팬.모터제어");
            HRV_dataGridView.Columns.Add("A10", "소비전력.[W]");


            string[][] Value = Program.DB.getValue(DB.type.ProjDB, "User_HRV", "번호,명칭,열회수유형, 온도교환효율_냉방, 온도교환효율_난방, 습도교환효율_냉방, 습도교환효율_난방, 팬풍량, 팬정압, 모터제어, 팬동력,설치유형", "번호 ='" + SelectHRV + "'");
            if (Value.Length > 0)
            {
                Num_textBox.Text = SelectHRV;
                Name = Value[0][1];
                Name_textBox.Text = Name;
                HRV_dataGridView.Rows.Add();
                int nRow = HRV_dataGridView.Rows.Count - 1;
                for (int i = 0; i < 11; i++)
                {
                    HRV_dataGridView.Rows[nRow].Cells[i].Value = Value[0][i];
                }
                if (Value[0][11] == "기존") { radioButton1.Checked = true; radioButton2.Checked = false; radioButton3.Checked = false; }
                else if (Value[0][11] == "신규") { radioButton1.Checked = false; radioButton2.Checked = true; radioButton3.Checked = false; }
                else { radioButton1.Checked = false; radioButton2.Checked = false; radioButton3.Checked = true; }
            }
        }
        public void Cal_Coil(double 냉각코일_입구_건구온도, double 냉각코일_입구_습구온도, double 냉각코일_출구_건구온도, double 냉각코일_출구_습구온도, double 난방코일_입구온도, double 난방코일_출구온도, double 급기풍량)
        {
            double 냉각코일_입구_상대습도 = (냉각코일_입구_습구온도 + 5.809 - 0.697 * 냉각코일_입구_건구온도) / ((0.058 + 0.003 * 냉각코일_입구_건구온도) * 100);
            double 냉각코일_출구_상대습도 = (냉각코일_출구_습구온도 + 5.809 - 0.697 * 냉각코일_출구_건구온도) / ((0.058 + 0.003 * 냉각코일_출구_건구온도) * 100);

            double 냉각코일_입구_절대습도 = 0.622 * (611.2 * Math.Pow(Math.E, 17.62 * 냉각코일_입구_건구온도 / (243.12 + 냉각코일_입구_건구온도))) * 냉각코일_입구_상대습도 / (101325 - (611.2 * Math.Pow(Math.E, 17.62 * 냉각코일_입구_건구온도 / (243.12 + 냉각코일_입구_건구온도))) * 냉각코일_입구_상대습도);
            double 냉각코일_입구_엔탈피 = 1.006 * 냉각코일_입구_건구온도 + 냉각코일_입구_절대습도 * (2500 + 1.86 * 냉각코일_입구_건구온도);

            double 냉각코일_출구_절대습도 = 0.622 * (611.2 * Math.Pow(Math.E, 17.62 * 냉각코일_출구_건구온도 / (243.12 + 냉각코일_출구_건구온도))) * 냉각코일_출구_상대습도 / (101325 - (611.2 * Math.Pow(Math.E, 17.62 * 냉각코일_출구_건구온도 / (243.12 + 냉각코일_출구_건구온도))) * 냉각코일_출구_상대습도);
            double 냉각코일_출구_엔탈피 = 1.006 * 냉각코일_출구_건구온도 + 냉각코일_출구_절대습도 * (2500 + 1.86 * 냉각코일_출구_건구온도);

            계산된_냉방출력 = (냉각코일_입구_엔탈피 - 냉각코일_출구_엔탈피) * 1.204 * 급기풍량 / 3600;
            계산된_난방출력 = (난방코일_출구온도 - 난방코일_입구온도) * 0.34 * 급기풍량 / 1000;
        }

        private void AHULocation_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (AHULocation_comboBox.SelectedItem != null)
            {
                AHULocation = AHULocation_comboBox.SelectedItem.ToString();
                Change_DuctLabel(AHULocation);
                LoadLocationImage();
            }
            else
            {
                AHULocation = null;
            }
        }

        private void AHUVolumeControl_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (AHUVolumeControl_comboBox.SelectedItem != null)
            {
                AHUVolumeControl = AHUVolumeControl_comboBox.SelectedItem.ToString();
            }
            else
            {
                AHUVolumeControl = null;
            }
        }

        private void AHUInsulationThickness_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (AHUInsulationThickness_comboBox.SelectedItem != null)
            {
                AHUInsulationThickness = Program.UTIL.ToDoubleOrZero(AHUInsulationThickness_comboBox.SelectedItem.ToString());
            }
            else
            {
                AHUInsulationThickness = 0;
            }
        }
        #endregion

        ///////////////////////////////////////////////////////////덕트//////////////////////////////////////////////////////////////////
        #region 덕트
        private void OASALength_textBox_TextChanged(object sender, EventArgs e)
        {
            if (AHULocation != "단열외피 내부")
            {
                SALength = Program.UTIL.textBox_doubleComa(OASALength_textBox, false, 2);
                OALength = 0;
            }
            else
            {
                OALength = Program.UTIL.textBox_doubleComa(OASALength_textBox, false, 2);
                SALength = 0;
            }
        }

        private void Change_DuctLabel(String AHULocation)
        {
            if (AHULocation != "단열외피 내부")
            {
                DuctLength_label.Text = "공조기부터 외피라인까지";
                OASALength_label.Text = "SA 덕트길이"; ;
                EARALength_label.Text = "RA 덕트길이"; ;

            }
            else
            {
                DuctLength_label.Text = "";
                OASALength_label.Text = "OA 덕트길이"; ;
                EARALength_label.Text = "EA덕트 길이";
            }
        }

        private void EARALength_textBox_TextChanged(object sender, EventArgs e)
        {
            if (AHULocation != "단열외피 내부")
            {
                RALength = Program.UTIL.textBox_doubleComa(EARALength_textBox, false, 2);
                EALength = 0;
            }
            else
            {
                EALength = Program.UTIL.textBox_doubleComa(EARALength_textBox, false, 2);
                RALength = 0;
            }
        }


        private void DuctInsulationThickness_textBox_TextChanged(object sender, EventArgs e)
        {
            DuctInsulationThickness = Program.UTIL.textBox_doubleComa(DuctInsulationThickness_textBox, false, 3);
        }
        private void DuctDiameter_textBox_TextChanged(object sender, EventArgs e)
        {
            DuctDiameter = Program.UTIL.textBox_doubleComa(DuctDiameter_textBox, false, 3);
        }

        private void PipeIns_button_Click(object sender, EventArgs e)
        {
            MaterialDB InsDB_form = new MaterialDB();
            DialogResult result = InsDB_form.ShowDialog();
            if (result == DialogResult.OK)
            {
                PipeIns = InsDB_form.Select[1];
                PipeIns_textBox.Text = PipeIns;
                PipeIns_Ramda = Program.UTIL.ToDoubleOrZero(InsDB_form.Select[4]);
                PipeIns_Ramda_textBox.Text = PipeIns_Ramda.ToString();
            }
            else
            {
                PipeIns_Ramda = 0.035;
                PipeIns_Ramda_textBox.Text = PipeIns_Ramda.ToString();
                PipeIns_textBox.Text = "보온단열재";
                PipeIns = "보온단열재";
            }
        }


        #endregion

        ////////////////////////////////////////////////////////예열/예냉////////////////////////////////////////////////////////////////
        #region 예열/예냉

        private void PrehPrecOptions_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (PrehPrecOptions_comboBox.SelectedItem != null)
            {
                PrehPrecOptions = PrehPrecOptions_comboBox.SelectedItem.ToString();
                ChangeVisble_PrehPrecOptions(PrehPrecOptions);
                LoadPrehPrecImage();
            }
            else
            {
                PrehPrecOptions = null;
            }
        }

        private void ChangeVisble_PrehPrecOptions(String PrehPrecOptions)
        {
            if (PrehPrecOptions == "쿨튜브")
            {
                GroundInfo_groupBox.Visible = true;
                GroundInfo_groupBox.BringToFront();
                CooltubeInfo_groupBox.Visible = true;
                PrehInfo_groupBox.Visible = false;
                PrehTHInfo_groupBox.Visible = false;
            }
            else if (PrehPrecOptions == "전기예열기")
            {
                PrehInfo_groupBox.Visible = true;
                PrehInfo_groupBox.BringToFront();
                GroundInfo_groupBox.Visible = false;
                CooltubeInfo_groupBox.Visible = false;
                PrehTHInfo_groupBox.Visible = false;
                int a = preheating_power();
                prepower_label.Text = Program.UTIL.doubleComa(a.ToString(), 0);
            }
            else if (PrehPrecOptions == "온수예열기")
            {
                PrehTHInfo_groupBox.Visible = true;
                PrehTHInfo_groupBox.BringToFront();
                GroundInfo_groupBox.Visible = false;
                CooltubeInfo_groupBox.Visible = false;
                PrehInfo_groupBox.Visible = false;
                double a_kw = preheating_power() / 1000.0;
                coilpower_label.Text = Program.UTIL.doubleComa(a_kw.ToString(), 1);
            }
            else
            {
                GroundInfo_groupBox.Visible = false;
                CooltubeInfo_groupBox.Visible = false;
                PrehInfo_groupBox.Visible = false;
                PrehTHInfo_groupBox.Visible = false;
            }
        }

        int preheating_power()
        {
            //제품 풍량
            //기후최소온도
            //결빙방지온도
            string[][] Location = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "지역", "");
            double[] theta_e_min = new double[12];
            double Volume = 0, theta_defrost = 0;
            if (Location.Length > 0)
            {
                for (int mth = 1; mth < 13; mth++)
                {
                    string[][] 기후 = Program.DB.getValue(DB.type.BaseDB_HCneed, "기후데이터_온도습도", "최소온도", "지역명 ='" + Location[0][0] + "'And 기간 = '" + mth + "월'");
                    theta_e_min[mth - 1] = Program.UTIL.ToDoubleOrZero(기후[0][0].ToString());
                }
            }

            if (AHUOptions == "열회수기")
            {
                string[][] 풍량 = Program.DB.getValue(DB.type.ProjDB, "User_HRV", "팬풍량,열회수유형", "번호 = '" + Num + "'");
                Volume = Program.UTIL.ToDoubleOrZero(풍량[0][0].ToString());
                string[][] 결빙방지 = Program.DB.getValue(DB.type.BaseDB_AHU, "결빙방지온도", "결빙방지온도", "건물유형 ='비주거건물' And 열회수기유형 = '" + 풍량[0][1].ToString() + "'");
                {
                    theta_defrost = Program.UTIL.ToDoubleOrZero(결빙방지[0][0].ToString());
                }
            }
            else
            {
                string[][] 풍량 = Program.DB.getValue(DB.type.ProjDB, "User_AHU", "급기풍량,열회수유형", "번호 = '" + Num + "'");
                Volume = Program.UTIL.ToDoubleOrZero(풍량[0][0].ToString());
                string[][] 결빙방지 = Program.DB.getValue(DB.type.BaseDB_AHU, "결빙방지온도", "결빙방지온도", "건물유형 ='비주거건물' And 열회수기유형 = '" + 풍량[0][1].ToString() + "'");
                {
                    theta_defrost = Program.UTIL.ToDoubleOrZero(결빙방지[0][0].ToString());
                }
            }
            double Ppreh_default = 0.34 * Volume * (theta_defrost - (theta_e_min.Min()));
            return (int)Ppreh_default;
        }

        private void PrehPower_textBox_TextChanged(object sender, EventArgs e)
        {
            if (PrehPower_textBox.Text != null && PrehPower_textBox.Text != "")
            {
                PrehPower = Program.UTIL.ToDoubleOrZero(PrehPower_textBox.Text.ToString());
            }
            else
            {
                PrehPower = 0;
            }
        }

        private void PumpPower_textBox_TextChanged(object sender, EventArgs e)
        {
            if (PumpPower_textBox.Text != null && PumpPower_textBox.Text != "")
            {
                Preh_PumpPower = Program.UTIL.ToDoubleOrZero(PumpPower_textBox.Text.ToString());
            }
            else
            {
                Preh_PumpPower = 0;
            }
        }

        private void CoilPower_textBox_TextChanged(object sender, EventArgs e)
        {
            if (CoilPower_textBox.Text != null && CoilPower_textBox.Text != "")
            {
                Preh_CoilPower = Program.UTIL.ToDoubleOrZero(CoilPower_textBox.Text.ToString());
            }
            else
            {
                Preh_CoilPower = 0;
            }
        }

        private void GroundOptions_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (GroundOptions_comboBox.SelectedItem != null)
            {
                GroundOptions = GroundOptions_comboBox.SelectedItem.ToString();
            }
            else
            {
                GroundOptions = null;
            }
        }

        private void GroundDepth_textBox_TextChanged(object sender, EventArgs e)
        {
            GroundDepth = Program.UTIL.textBox_doubleComa(GroundDepth_textBox, false, 3);
        }

        private void CooltubeDiameter_textBox_TextChanged(object sender, EventArgs e)
        {
            CooltubeDiameter = Program.UTIL.textBox_doubleComa(CooltubeDiameter_textBox, false, 2);
        }

        private void CooltubeThickness_textBox_TextChanged(object sender, EventArgs e)
        {
            CooltubeThickness = Program.UTIL.textBox_doubleComa(CooltubeThickness_textBox, false, 2);
        }

        private void CooltubeLength_textBox_TextChanged(object sender, EventArgs e)
        {
            CooltubeLength = Program.UTIL.textBox_doubleComa(CooltubeLength_textBox, false, 3);
        }

        private void CooltubeMaterial_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CooltubeMaterial_comboBox.SelectedItem != null)
            {
                CooltubeMaterial = CooltubeMaterial_comboBox.SelectedItem.ToString();
            }
            else
            {
                CooltubeMaterial = null;
            }
        }
        #endregion

        ///////////////////////////////////////그림 넣기///////////////////////////////////////////////////////////
        #region 이미지

        private void LoadMainImage() // 1.열회수기/공조기 그림넣기
        {
            if (AHUOptions == "열회수기")
            {
                string[][] Value = Program.DB.getValue(DB.type.ProjDB, "User_HRV", "열회수유형", "번호 ='" + Num_textBox.Text + "'");
                if (Value.Length > 0)
                {
                    if (Value[0][0] != "판형")
                    {
                        HRVType = "회전형";
                    }
                    else
                    {
                        HRVType = "판형";
                    }
                }
                string[][] Image = Program.DB.getValue(DB.type.BaseDB_AHU, "공조시스템이미지", "이미지", "항목유형 = '" + AHUOptions + "' And 설비유형='" + HRVType + "'");
                MainpictureBox.Load(Program.gPath + Image[0][0]);
                MainpictureBox.Location = new Point(0, 0);
                MainpictureBox.Size = new Size(790, 365);
                MainpictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            }
            else if (AHUOptions == "공조기")
            {
                string[][] Value = Program.DB.getValue(DB.type.ProjDB, "User_AHU", "열회수유형", "번호 ='" + Num_textBox.Text + "'");
                if (Value.Length > 0)
                {
                    if (Value[0][0] != "판형" && Value[0][0] != "없음")
                    {
                        HRVType = "회전형";
                    }
                    else if (Value[0][0] == "판형")
                    {
                        HRVType = "판형";
                    }
                    else
                    {
                        HRVType = "없음";
                    }
                }
                string[][] Image = Program.DB.getValue(DB.type.BaseDB_AHU, "공조시스템이미지", "이미지", "항목유형 = '" + AHUOptions + "' And 설비유형='" + HRVType + "'");
                MainpictureBox.Load(Program.gPath + Image[0][0]);
                MainpictureBox.Location = new Point(0, 0);
                MainpictureBox.Size = new Size(790, 365);
                MainpictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                LoadCoilImage();
                LoadHumidifierImage();
                LoadVAVImage();
            }
        }

        private void LoadLocationImage() // 2. 설치위치 그림넣기
        {
            if (AHULocation == "" || AHULocation == null)
            {
                AHULocation_comboBox.SelectedItem = "단열외피 내부";
            }
            else
            {
                string[][] Image = Program.DB.getValue(DB.type.BaseDB_AHU, "공조시스템이미지", "이미지", "항목유형 = '" + AHUOptions + "' And 설비유형 = '" + AHULocation + "'");
                if (Image.Length > 0)
                {
                    LocationpictureBox.Load(Program.gPath + Image[0][0]);
                    LocationpictureBox.Location = new Point(100, 0);
                    LocationpictureBox.Size = new Size(790, 365);
                    LocationpictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                }
            }
        }

        private void LoadPrehPrecImage() // 3.예열/예냉 그림넣기
        {
            if (PrehPrecOptions == "전기예열기")
            {
                string[][] Image = Program.DB.getValue(DB.type.BaseDB_AHU, "공조시스템이미지", "이미지", "항목유형 = '예열예냉' And 설비유형='" + PrehPrecOptions + "'");
                PrehPrecpictureBox.Visible = true;
                PrehPrecpictureBox.Load(Program.gPath + Image[0][0]);
                PrehPrecpictureBox.Location = new Point(220, 190);
                PrehPrecpictureBox.Size = new Size(40, 80);
                PrehPrecpictureBox.SizeMode = PictureBoxSizeMode.Zoom;

            }
            else if (PrehPrecOptions == "쿨튜브")
            {
                string[][] Image = Program.DB.getValue(DB.type.BaseDB_AHU, "공조시스템이미지", "이미지", "항목유형 = '예열예냉' And 설비유형='" + PrehPrecOptions + "'");
                PrehPrecpictureBox.Visible = true;
                PrehPrecpictureBox.Load(Program.gPath + Image[0][0]);
                PrehPrecpictureBox.Location = new Point(12, 177);
                PrehPrecpictureBox.Size = new Size(240, 170);
                PrehPrecpictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            }
            else
            {
                PrehPrecpictureBox.Visible = false;
            }
        }

        private void LoadCoilImage() // 4.냉방/난방코일 그림넣기
        {
            string[][] Value = Program.DB.getValue(DB.type.ProjDB, "User_AHU", "냉각코일출력,난방코일출력", "번호 ='" + Num_textBox.Text + "'");
            if (Value.Length > 0)
            {
                if (double.TryParse(Value[0][0].ToString(), out double coolresult) && coolresult > 0) //냉방코일 그림
                {
                    if (HRVType == "판형")
                    {
                        string[][] image = Program.DB.getValue(DB.type.BaseDB_AHU, "공조시스템이미지", "이미지", "항목유형 = '냉방코일' And 설비유형='" + HRVType + "'");
                        ccoilpictureBox.Visible = true;
                        ccoilpictureBox.Load(Program.gPath + image[0][0]);
                        ccoilpictureBox.Location = new Point(395, 100);
                        ccoilpictureBox.Size = new Size(40, 80);
                        ccoilpictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                    }
                    else if (HRVType == "회전형")
                    {
                        string[][] image = Program.DB.getValue(DB.type.BaseDB_AHU, "공조시스템이미지", "이미지", "항목유형 = '냉방코일' And 설비유형='" + HRVType + "'");
                        ccoilpictureBox.Visible = true;
                        ccoilpictureBox.Load(Program.gPath + image[0][0]);
                        ccoilpictureBox.Location = new Point(395, 188);
                        ccoilpictureBox.Size = new Size(40, 80);
                        ccoilpictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                    }
                }
                if (double.TryParse(Value[0][0].ToString(), out double heatresult) && heatresult > 0) //난방코일 그림
                {
                    if (HRVType == "판형")
                    {
                        string[][] image = Program.DB.getValue(DB.type.BaseDB_AHU, "공조시스템이미지", "이미지", "항목유형 = '난방코일' And 설비유형='" + HRVType + "'");
                        hcoilpictureBox.Visible = true;
                        hcoilpictureBox.Load(Program.gPath + image[0][0]);
                        hcoilpictureBox.Location = new Point(427, 100);
                        hcoilpictureBox.Size = new Size(40, 80);
                        hcoilpictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                    }
                    else if (HRVType == "회전형")
                    {
                        string[][] image = Program.DB.getValue(DB.type.BaseDB_AHU, "공조시스템이미지", "이미지", "항목유형 = '난방코일' And 설비유형='" + HRVType + "'");
                        hcoilpictureBox.Visible = true;
                        hcoilpictureBox.Load(Program.gPath + image[0][0]);
                        hcoilpictureBox.Location = new Point(427, 188);
                        hcoilpictureBox.Size = new Size(40, 80);
                        hcoilpictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                    }
                }
            }
        }

        private void LoadHumidifierImage() // 5.가습기 그림넣기
        {
            string[][] Value = Program.DB.getValue(DB.type.ProjDB, "User_AHU", "가습기용량", "번호 ='" + Num_textBox.Text + "'");

            if (double.TryParse(Value[0][0].ToString(), out double heatresult) && heatresult > 0)
            {
                if (HRVType == "판형")
                {
                    string[][] image = Program.DB.getValue(DB.type.BaseDB_AHU, "공조시스템이미지", "이미지", "항목유형 = '가습기' And 설비유형='" + HRVType + "'");
                    HumidifierpictureBox.Visible = true;
                    HumidifierpictureBox.Load(Program.gPath + image[0][0]);
                    HumidifierpictureBox.Location = new Point(459, 100);
                    HumidifierpictureBox.Size = new Size(30, 80);
                    HumidifierpictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                }
                else if (HRVType == "회전형")
                {
                    string[][] image = Program.DB.getValue(DB.type.BaseDB_AHU, "공조시스템이미지", "이미지", "항목유형 = '가습기' And 설비유형='" + HRVType + "'");
                    HumidifierpictureBox.Visible = true;
                    HumidifierpictureBox.Load(Program.gPath + image[0][0]);
                    HumidifierpictureBox.Location = new Point(459, 188);
                    HumidifierpictureBox.Size = new Size(30, 80);
                    HumidifierpictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                }
            }
        }

        private void LoadVAVImage() // 6.변풍량 그림넣기
        {
            string[][] Value = Program.DB.getValue(DB.type.ProjDB, "User_AHU", "공조방식", "번호 ='" + Num_textBox.Text + "'");
            if (Value[0][0] == "변풍량")
            {
                string[][] Image = Program.DB.getValue(DB.type.BaseDB_AHU, "공조시스템이미지", "이미지", "항목유형 = 'VAV' And 설비유형 = '" + Value[0][0].ToString() + "'");
                if (HRVType == "판형")
                {
                    VAVpictureBox.Visible = true;
                    VAVpictureBox.Load(Program.gPath + Image[0][0]);
                    VAVpictureBox.Location = new Point(608, 122);
                    VAVpictureBox.Size = new Size(118, 55);
                    VAVpictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                }
                else if (HRVType == "회전형")
                {
                    VAVpictureBox.Visible = true;
                    VAVpictureBox.Load(Program.gPath + Image[0][0]);
                    VAVpictureBox.Location = new Point(608, 196);
                    VAVpictureBox.Size = new Size(118, 55);
                    VAVpictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                }
            }
            else
            {
                VAVpictureBox.Visible = false;
            }
        }

        #endregion

        #region 세이브
        public bool ValidateAndSave(bool isManualSave = false)
        {
            try
            {
                if (!ValidateZoneVentilationComplete())
                {
                    return false;
                }
                Save_Image();
                Save(isManualSave);
                none_AHU_HRV_check();
                return true;
            }
            catch (Exception ex)
            {
                // 디버깅 중단점 방지를 위해 예외를 무시하거나 로그만 남김
                System.Diagnostics.Debug.WriteLine($"ValidateAndSave 오류: {ex.Message}");
                return false;
            }
        }
        // 이 설비에 연결된 모든 존이 AHUZoneVent_Form(존별 급배기량)을 저장했는지 확인 —
        // 빠진 존이 있으면 경고하고 저장 자체를 막음(AHUZoneVent_Form 입력이 이제 필수라서)
        private bool ValidateZoneVentilationComplete()
        {
            string[][] Zones = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_Form", "존번호,존이름", "선택열회수기 = '" + Num_textBox.Text + "' and 환기유무='True'");
            ArrayList missing = new ArrayList();
            for (int i = 0; i < Zones.Length; i++)
            {
                string[][] Saved = Program.DB.getValue(DB.type.ProjDB, "AHUZoneVent_Form", "급기량,배기량", "설비 = '" + Num_textBox.Text + "' And 존 = '" + Zones[i][0] + "'");
                if (Saved.Length == 0)
                {
                    missing.Add(Zones[i][0] + " " + Zones[i][1]);
                }
            }
            if (missing.Count > 0)
            {
                // ZoneGeneral.cs의 ValidateAndSave() 패턴과 동일: 먼저 "저장하시겠습니까?"를 묻고,
                // 예 → 뭐가 빠졌는지 안내 후 저장 막음(false) / 아니오 → 저장 없이 그냥 나가게(true)
                // 해서, 실수로 들어왔다가 못 나가고 갇히는 상황을 방지함
                DialogResult res = MessageBox.Show("저장하시겠습니까?", "저장", MessageBoxButtons.YesNo);
                if (res == DialogResult.Yes)
                {
                    const int maxListed = 10;
                    int shownCount = Math.Min(missing.Count, maxListed);
                    string[] shown = new string[shownCount];
                    missing.CopyTo(0, shown, 0, shownCount);
                    string listText = string.Join("\n", shown);
                    if (missing.Count > maxListed)
                    {
                        listText += "\n외 " + (missing.Count - maxListed) + "개 존 더";
                    }
                    MessageBox.Show("급배기량이 입력되지 않은 존이 " + missing.Count + "개 있습니다:\n" + listText + "\n\n\"+\" 버튼을 눌러 존별 급배기량을 먼저 입력해주세요.");
                    return false;
                }
                else
                {
                    return true;
                }
            }
            return true;
        }
        private void Save(bool isManualSave = false)
        {
            Program.DB.setValue(DB.type.ProjDB, "AHUSystem_Form", "번호,프로젝트유형,명칭", "'" + Num_textBox.Text + "','" + 프로젝트유형[0][0] + "','" + Name + "'", "번호");
            Program.DB.setValue(DB.type.ProjDB, "AHUSystem_Form", "번호,유형", "'" + Num_textBox.Text + "','" + AHUOptions + "'", "번호");
            Program.DB.setValue(DB.type.ProjDB, "AHUSystem_Form", "번호,설치위치,풍량제어,누기시험방법,누기등급1,누기등급2,공조기단열두께,TAB실시유무", "'" + Num_textBox.Text + "','" + AHULocation + "','" + AHUVolumeControl + "','" + AHULeakageTestMethod + "','" + AHULeakageLevel1 + "','" + AHULeakageLevel2 + "','" + AHUInsulationThickness.ToString() + "','" + TABOptions + "'", "번호");
            Program.DB.setValue(DB.type.ProjDB, "AHUSystem_Form", "번호,덕트누기수준,OA덕트길이,EA덕트길이,SA덕트길이,RA덕트길이,덕트단열두께,덕트관경,덕트단열재,덕트단열재열전도율", "'" + Num_textBox.Text + "','" + DuctLeakageLevel + "','" + OALength.ToString() + "','" + EALength.ToString() + "','" + SALength.ToString() + "','" + RALength.ToString() + "','" + DuctInsulationThickness.ToString() + "','" + DuctDiameter.ToString() + "','" + PipeIns + "','" + PipeIns_Ramda.ToString() + "'", "번호");
            if (PrehPrecOptions != "전기예열기")
            {
                PrehPower = 0;
            }
            Program.DB.setValue(DB.type.ProjDB, "AHUSystem_Form", "번호,예열예냉유형,프리히터용량", "'" + Num_textBox.Text + "','" + PrehPrecOptions + "','" + PrehPower + "'", "번호");
            if (PrehPrecOptions != "온수예열기")
            {
                Preh_PumpPower = 0;
                Preh_CoilPower = 0;
            }
            Program.DB.setValue(DB.type.ProjDB, "AHUSystem_Form", "번호,온수예열기펌프출력,온수예열기코일출력", "'" + Num_textBox.Text + "','" + Preh_PumpPower + "','" + Preh_CoilPower + "'", "번호");
            if (PrehPrecOptions != "쿨튜브")
            {
                GroundOptions = "";
                GroundDepth = 0;
                CooltubeDiameter = 0;
                CooltubeThickness = 0;
                CooltubeLength = 0;
                CooltubeMaterial = "";
            }
            Program.DB.setValue(DB.type.ProjDB, "AHUSystem_Form", "번호,토양유형,지중깊이,쿨튜브관경,쿨튜브두께,쿨튜브길이,쿨튜브재질", "'" + Num_textBox.Text + "','" + GroundOptions + "','" + GroundDepth.ToString() + "','" + CooltubeDiameter.ToString() + "','" + CooltubeThickness.ToString() + "','" + CooltubeLength.ToString() + "','" + CooltubeMaterial + "'", "번호");
            Program.DB.saveProject();
        }
        private void Save_Image()
        {
            try
            {
                Bitmap bmp = new Bitmap(ImagePanel.Width, ImagePanel.Height);
                ImagePanel.DrawToBitmap(bmp, new Rectangle(0, 0, ImagePanel.Width, ImagePanel.Height));

                string pid = "0000-00-00";
                string[][] Value = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "프로젝트번호");
                if (Value.Length > 0)
                {
                    pid = Value[0][0];
                }

                Directory.CreateDirectory(Program.gPath + "\\projects\\" + pid);

                string ImageName = "/projects/" + pid + "/" + Num + ".png";
                string imagePath = Program.gPath + ImageName;

                bmp.Save(imagePath, System.Drawing.Imaging.ImageFormat.Png);
            }
            catch (Exception ex)
            {
                MessageBox.Show("오류 발생: " + ex.Message);
            }
        }

        public static bool OnLoadListProc(Form form)
        {
            List_AHUSystem f = (List_AHUSystem)form;
            f.load_List();
            return true;
        }

        private void reset()
        {
            Type = null;
            Num = null; Name = null; Num = null; AHUOptions = null;
            AHULocation = null; AHUVolumeControl = null; AHULeakageTestMethod = null; AHULeakageLevel1 = null; AHULeakageLevel2 = null;
            AHUInsulationThickness = 0;
            DuctLeakageLevel = null;
            OALength = 0; EALength = 0;
            SALength = 0; RALength = 0; DuctInsulationThickness = 0; DuctDiameter = 0;
            SelectZone_split.Clear();
            TABOptions = null; PipeIns = null; PipeIns_Ramda = 0;
            PrehPrecOptions = null; GroundOptions = null; CooltubeMaterial = null;
            PrehPower = 0; GroundDepth = 0; CooltubeDiameter = 0; CooltubeThickness = 0; CooltubeLength = 0;
            Preh_PumpPower = 0; Preh_CoilPower = 0;
            AnnualCoolingNeed = 0; AnnualHeatingNeed = 0;
            CoolingLoad = 0; HeatingLoad = 0;
            계산된_냉방출력 = 0; 계산된_난방출력 = 0;
            HRVType = null;

            Num_textBox.Text = null;
            Name_textBox.Text = null;
            Zone_textBox.Text = null;
            AnnualCoolingNeed_textBox.Text = null;
            AnnualHeatingNeed_textBox.Text = null;
            CoolingLoad_textBox.Text = null;
            HeatingLoad_textBox.Text = null;

            AHUOptions_comboBox.SelectedItem = null;
            AHULocation_comboBox.SelectedItem = null;
            AHULeakageTestMethod_comboBox.SelectedItem = null;
            AHULeakageLevel_comboBox.SelectedItem = null;
            AHUVolumeControl_comboBox.SelectedItem = null;
            TABOptions_comboBox.SelectedItem = null;
            AHUInsulationThickness_comboBox.SelectedItem = null;
            PrehPrecOptions_comboBox.SelectedItem = null;
            GroundOptions_comboBox.SelectedItem = null;
            CooltubeMaterial_comboBox.SelectedItem = null;

            OASALength_textBox.Text = null;
            EARALength_textBox.Text = null;
            DuctDiameter_textBox.Text = null;
            DuctInsulationThickness_textBox.Text = null;
            PipeIns_textBox.Text = null;
            PipeIns_Ramda_textBox.Text = null;
            PrehPower_textBox.Text = null;
            PumpPower_textBox.Text = null;
            CoilPower_textBox.Text = null;
            GroundDepth_textBox.Text = null;
            CooltubeDiameter_textBox.Text = null;
            CooltubeThickness_textBox.Text = null;
            CooltubeLength_textBox.Text = null;

            HRV_dataGridView.Columns.Clear();
            HRV_dataGridView.Rows.Clear();
        }

        #endregion

        #region 로드

        public void LoadData(String ID)            // 리스트에서 항목 더블 클릭시 - 뷰를 ID 의 getValue 값으로 채우기
        {
            reset();

            Num_textBox.Text = ID;
            Num = ID;

            string[][] Value = Program.DB.getValue(DB.type.ProjDB, "AHUSystem_Form", "명칭,유형,설치위치", "번호 = '" + ID + "'");
            if (Value.Length > 0)
            {
                Name_textBox.Text = Value[0][0];
                Name = Value[0][0];
                AHUOptions = Value[0][1];
                AHULocation = Value[0][2];
                AHULocation_comboBox.SelectedItem = AHULocation;
                AHUOptions_comboBox.SelectedItem = AHUOptions;

                ChangeAHUOptions(AHUOptions);
                Load_Zone();
                LoadMainImage();

                if (AHUOptions == "공조기")
                {
                    load_Table_AHU(Num);
                }
                else
                {
                    load_Table_HRV(Num);
                }
            }

            Value = Program.DB.getValue(DB.type.ProjDB, "AHUSystem_Form", "설치위치, 풍량제어, 누기시험방법, 누기등급1, 공조기단열두께, TAB실시유무", "번호 = '" + ID + "'");
            if (Value.Length > 0)
            {

                Change_DuctLabel(AHULocation);

                AHUVolumeControl_comboBox.SelectedItem = Value[0][1];
                AHUVolumeControl = Value[0][1];

                AHULeakageTestMethod_comboBox.SelectedItem = Value[0][2];
                AHULeakageTestMethod = Value[0][2];
                Change_AHULeakageOptions(AHULeakageTestMethod);

                AHULeakageLevel_comboBox.SelectedItem = Value[0][3];
                AHULeakageLevel1 = Value[0][3];
                Chaeck_AHULeakageLevel(AHULeakageLevel1);

                AHUInsulationThickness_comboBox.SelectedItem = Value[0][4];
                AHUInsulationThickness = Program.UTIL.ToDoubleOrZero(Value[0][4]);

                TABOptions_comboBox.SelectedItem = Value[0][5];
                TABOptions = Value[0][5];
                Check_DuctLeakageLevel();

            }

            Value = Program.DB.getValue(DB.type.ProjDB, "AHUSystem_Form", "OA덕트길이,EA덕트길이,SA덕트길이,RA덕트길이,덕트단열두께,덕트관경,덕트단열재,덕트단열재열전도율", "번호 = '" + ID + "'");
            if (Value.Length > 0)
            {

                if (AHULocation == "단열외피 내부")
                {
                    OALength = Program.UTIL.ToDoubleOrZero(Value[0][0]);
                    OASALength_textBox.Text = OALength.ToString();
                    Program.UTIL.textBox_doubleComa(OASALength_textBox, true, 2);

                    EALength = Program.UTIL.ToDoubleOrZero(Value[0][1]);
                    EARALength_textBox.Text = EALength.ToString();
                    Program.UTIL.textBox_doubleComa(EARALength_textBox, true, 2);
                }
                else
                {
                    SALength = Program.UTIL.ToDoubleOrZero(Value[0][2]);
                    OASALength_textBox.Text = SALength.ToString();
                    Program.UTIL.textBox_doubleComa(OASALength_textBox, true, 2);

                    RALength = Program.UTIL.ToDoubleOrZero(Value[0][3]);
                    EARALength_textBox.Text = RALength.ToString();
                    Program.UTIL.textBox_doubleComa(EARALength_textBox, true, 2);
                }

                DuctInsulationThickness = Program.UTIL.ToDoubleOrZero(Value[0][4]);
                DuctInsulationThickness_textBox.Text = DuctInsulationThickness.ToString();
                Program.UTIL.textBox_doubleComa(DuctInsulationThickness_textBox, true, 3);

                DuctDiameter = Program.UTIL.ToDoubleOrZero(Value[0][5]);
                DuctDiameter_textBox.Text = DuctDiameter.ToString();
                Program.UTIL.textBox_doubleComa(DuctDiameter_textBox, true, 3);

                PipeIns = Value[0][6];
                PipeIns_textBox.Text = PipeIns.ToString();

                PipeIns_Ramda = Program.UTIL.ToDoubleOrZero(Value[0][7]);
                PipeIns_Ramda_textBox.Text = PipeIns_Ramda.ToString();
                Program.UTIL.textBox_doubleComa(PipeIns_Ramda_textBox, true, 3);
            }

            Value = Program.DB.getValue(DB.type.ProjDB, "AHUSystem_Form", "예열예냉유형", "번호 = '" + ID + "'");
            if (Value.Length > 0)
            {
                PrehPrecOptions_comboBox.SelectedItem = Value[0][0];
            }

            Value = Program.DB.getValue(DB.type.ProjDB, "AHUSystem_Form", "프리히터용량", "번호 = '" + ID + "'");
            if (Value.Length > 0)
            {
                PrehPower_textBox.Text = Value[0][0];
                PrehPower = Program.UTIL.ToDoubleOrZero(Value[0][0]);
            }
            else
            {
                PrehInfo_groupBox.Visible = false;
            }

            Value = Program.DB.getValue(DB.type.ProjDB, "AHUSystem_Form", "온수예열기펌프출력,온수예열기코일출력", "번호 = '" + ID + "'");
            if (Value.Length > 0)
            {
                Preh_PumpPower = Program.UTIL.ToDoubleOrZero(Value[0][0]);
                PumpPower_textBox.Text = Preh_PumpPower.ToString();

                Preh_CoilPower = Program.UTIL.ToDoubleOrZero(Value[0][1]);
                CoilPower_textBox.Text = Preh_CoilPower.ToString();
            }
            else
            {
                PrehTHInfo_groupBox.Visible = false;
            }

            Value = Program.DB.getValue(DB.type.ProjDB, "AHUSystem_Form", "토양유형,지중깊이,쿨튜브관경,쿨튜브두께,쿨튜브길이,쿨튜브재질", "번호 = '" + ID + "'");
            if (Value.Length > 0)
            {
                GroundOptions_comboBox.SelectedItem = Value[0][0];
                GroundOptions = Value[0][0];

                GroundDepth = Program.UTIL.ToDoubleOrZero(Value[0][1]);
                GroundDepth_textBox.Text = GroundDepth.ToString();
                Program.UTIL.textBox_doubleComa(GroundDepth_textBox, true, 3);

                CooltubeDiameter = Program.UTIL.ToDoubleOrZero(Value[0][2]);
                CooltubeDiameter_textBox.Text = CooltubeDiameter.ToString();
                Program.UTIL.textBox_doubleComa(CooltubeDiameter_textBox, true, 2);

                CooltubeThickness = Program.UTIL.ToDoubleOrZero(Value[0][3]);
                CooltubeThickness_textBox.Text = CooltubeThickness.ToString();
                Program.UTIL.textBox_doubleComa(CooltubeThickness_textBox, true, 2);

                CooltubeLength = Program.UTIL.ToDoubleOrZero(Value[0][4]);
                CooltubeLength_textBox.Text = CooltubeLength.ToString();
                Program.UTIL.textBox_doubleComa(CooltubeLength_textBox, true, 3);

                CooltubeMaterial_comboBox.SelectedItem = Value[0][5];
                CooltubeMaterial = Value[0][5];
            }
            else
            {
                GroundInfo_groupBox.Visible = false;
                CooltubeInfo_groupBox.Visible = false;
            }

        }

        #endregion

        #region 리셋 
        public void ResetForm(String ID) // 리스트에서 추가 버튼 클릭시 - 뷰 초기화
        {
            Num_textBox.Text = ID;
            Num = ID;
        }

        #endregion
        void none_AHU_HRV_check()
        {
            string[][] check = Program.DB.getValue(DB.type.ProjDB, "AHUSystem_Form", "번호", "");
            if (check.Length > 0)
            {
                int j = 0;
                string[][] zonecheck = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_Form", "존번호,선택열회수기", "");
                for (int i = 0; i < check.Length; i++)
                {
                    for (int k = 0; k < zonecheck.Length; k++)
                    {
                        if (check[i][0] == zonecheck[k][1])
                        {
                            j++;
                        }
                    }
                    if (j == 0)
                    {
                        Program.DB.deleteValue(DB.type.ProjDB, "AHUSystem_Form", "번호 = '" + check[i][0] + "'");
                    }
                }
            }
        }

        private void infoAHU_Click(object sender, EventArgs e)
        {
            string basePath = Program.gPath + "Manual\\1.contents\\23.AHU";

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
