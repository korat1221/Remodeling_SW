using main.contentslist;
using main.subcontents.AHUSystem;
using main.subcontents.ConstructionCW;
using main.subcontents.DHWSystem;
using main.subcontents.HeatingSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.Entity.Core.Metadata.Edm;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Numerics;
using System.Reflection;
using System.Reflection.PortableExecutable;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static main.DB;
using static System.ComponentModel.Design.ObjectSelectorEditor;
using static System.Net.Mime.MediaTypeNames;

namespace main.contents
{
    public partial class AHUSystem : Form
    {
        string Type;
        String Num = "AHU01"; string Name; String SelectZone_nonsplit, SelectSystem, AHUOptions;
        String AHULocation, AHUVolumeControl, AHULeakageTestMethod, AHULeakageLevel1, AHULeakageLevel2;
        double AHUInsulationThickness;
        string DuctLeakageLevel;
        double OALength, EALength;
        double SALength, RALength, DuctInsulationThickness, DuctDiameter;
        ArrayList SelectZone_split = new ArrayList();
        String TABOptions, PipeIns; double PipeIns_Ramda;
        string PrehPrecOptions, PrehControlOptions, GroundOptions, CooltubeMaterial;
        double PrehPower, GroundDepth, CooltubeDiameter, CooltubeThickness, CooltubeLength;
        double AnnualCoolingNeed, AnnualHeatingNeed;
        double CoolingLoad, HeatingLoad;
        double 계산된_냉방출력, 계산된_난방출력;

        string[][] 프로젝트유형;
        public AHUSystem()
        {

            InitializeComponent();
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
            PrehPrecOptions_comboBox.Items.Add("쿨튜브");
            PrehPrecOptions_comboBox.Items.Add("프리히터");

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

            //프리히터제어 콤보박스
            PrehControlOptions_comboBox.Items.Clear();
            PrehControlOptions_comboBox.Items.Add("on/off제어");
            PrehControlOptions_comboBox.Items.Add("인버터제어");

            //덕트단열재 설정
            PipeIns_Ramda = 0.035;
            PipeIns_Ramda_textBox.Text = PipeIns_Ramda.ToString();
            PipeIns_textBox.Text = "일반 보온재";

        }
        private void GeneralPanel_Paint(object sender, PaintEventArgs e)
        {
            Panel p = (Panel)sender;
            ControlPaint.DrawBorder(e.Graphics, p.DisplayRectangle, Color.FromArgb(153, 180, 209), ButtonBorderStyle.Solid);
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
                AHUOptions = AHUOptions_comboBox.SelectedItem.ToString();
                ChangeAHUOptions(AHUOptions);
            }
            else
            {
                AHUOptions = "";
            }
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
            else
            {
            }
            SelectSystem = "";
            HRV_dataGridView.Columns.Clear();
            HRV_dataGridView.Rows.Clear();
            Name_textBox.Text = "";
            Name = "";
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

        ///////////////////////////////////////////////////////////존////////////////////////////////////////////////////////////////////
        #region 존

        private void Zone_button_Click(object sender, EventArgs e)
        {
            AHU_Zone AHUzone = new AHU_Zone(Num, SelectZone_nonsplit, AHUOptions);
            DialogResult result = AHUzone.ShowDialog();
            if (result == DialogResult.OK)
            {
                if (AHUzone.SelectZone != null)
                {
                    SelectZone_nonsplit = AHUzone.SelectZone;
                    Split_Zone(AHUzone.SelectZone);
                    Cal_Qb();
                }
            }
        }

        private void Split_Zone(String nonSplit)
        {
            String 내용;
            if (nonSplit != null)
            {
                if (nonSplit.Contains("+"))
                {
                    string[] token = nonSplit.Split('+');
                    SelectZone_split.Clear();
                    foreach (var item in token)
                    {
                        SelectZone_split.Add(item.ToString());
                    }
                    내용 = SelectZone_split[0].ToString() + " 외 " + (SelectZone_split.Count - 1).ToString() + "개";
                }
                else
                {
                    SelectZone_split.Clear();
                    SelectZone_split.Add(nonSplit);
                    내용 = SelectZone_split[0].ToString();
                }
                Zone_textBox.Text = 내용;
            }
            else { 내용 = ""; }

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
                    AnnualHeatingNeed += Convert.ToDouble(난방[0][0]);
                    HeatingLoad += Convert.ToDouble(난방[0][1]);
                }
                string[][] 냉방 = Program.DB.getValue(DB.type.ProjDB, "Zone_HCneed_Result", "Qb_a,Q_max", "번호 ='" + SelectZone_split[i] + "' AND 난방_냉방 = '냉방'");
                if (냉방.Length > 0)
                {
                    AnnualCoolingNeed += Convert.ToDouble(냉방[0][0]);
                    CoolingLoad += Convert.ToDouble(냉방[0][1]);
                }
            }
            AnnualHeatingNeed_textBox.Text = AnnualHeatingNeed.ToString("0.0");
            HeatingLoad_textBox.Text = HeatingLoad.ToString("0.0");
            AnnualCoolingNeed_textBox.Text = AnnualCoolingNeed.ToString("0.0");
            CoolingLoad_textBox.Text = CoolingLoad.ToString("0.0");
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
                AHULeakageLevel1 = "";
                AHULeakageLevel2 = "";
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
                AHU_HRV AHU_HRV = new AHU_HRV(AHUOptions, SelectSystem);
                DialogResult result = AHU_HRV.ShowDialog();
                if (result == DialogResult.OK)
                {
                    if (AHU_HRV.SelectSystem != null)
                    {
                        SelectSystem = AHU_HRV.SelectSystem;
                        load_Table_HRV(SelectSystem);
                    }
                }
            }
            else
            {
                AHU_HRV AHU_HRV = new AHU_HRV(AHUOptions, SelectSystem);
                DialogResult result = AHU_HRV.ShowDialog();
                if (result == DialogResult.OK)
                {
                    if (AHU_HRV.SelectSystem != null)
                    {
                        SelectSystem = AHU_HRV.SelectSystem;
                        load_Table_AHU(SelectSystem);
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
            HRV_dataGridView.Columns.Add("A20", "가습기.용량.[kg/h]");
            HRV_dataGridView.Columns.Add("A21", "송풍기.풍량.급기.[CMH]");
            HRV_dataGridView.Columns.Add("A22", "송풍기.풍량.배기.[CMH]");
            HRV_dataGridView.Columns.Add("A23", "송풍기.정압.급기.[Pa]");
            HRV_dataGridView.Columns.Add("A24", "송풍기.정압.배기.[Pa]");
            HRV_dataGridView.Columns.Add("A25", "송풍기.팬동력.급기.[kW]");
            HRV_dataGridView.Columns.Add("A26", "송풍기.팬동력.배기.[kW]");
            HRV_dataGridView.Columns.Add("A27", "송풍기.모터제어");
            for (int i = 0; i < 28; i++)
            { HRV_dataGridView.Columns[i].Width = 80; }



            string[][] Value = Program.DB.getValue(DB.type.ProjDB, "User_AHU", "번호,명칭,공조방식,열회수유형,온도교환효율_냉방,온도교환효율_난방,전열교환효율_냉방,전열교환효율_난방,습도교환효율_냉방,습도교환효율_난방,냉각코일출력,냉각코일_입구_건구온도,냉각코일_입구_습구온도,냉각코일_출구_건구온도,냉각코일_출구_습구온도,난방코일출력,난방코일_입구온도,난방코일_출구온도,가습기유형,가습기습도수준,가습기용량,급기풍량,배기풍량,급기정압,배기정압,급기팬동력,배기팬동력,모터제어,설치유형", "번호='" + SelectAHU + "'");
            if (Value.Length > 0)
            {
                Num_textBox.Text = Value[0][0];
                Num = Value[0][0];
                Name_textBox.Text = Value[0][1];
                Name = Value[0][1];
                int nRow = HRV_dataGridView.Rows.Add();
                for (int i = 0; i < 28; i++)
                { HRV_dataGridView.Rows[nRow].Cells[i].Value = Value[0][i]; }

                if (Value[0][28] == "기존") { radioButton1.Checked = true; radioButton2.Checked = false; radioButton3.Checked = false; }
                else if (Value[0][28] == "신규") { radioButton1.Checked = false; radioButton2.Checked = true; radioButton3.Checked = false; }
                else { radioButton1.Checked = false; radioButton2.Checked = false; radioButton3.Checked = true; }

                Cal_Coil(Convert.ToDouble(Value[0][11]), Convert.ToDouble(Value[0][12]), Convert.ToDouble(Value[0][13]), Convert.ToDouble(Value[0][14]), Convert.ToDouble(Value[0][16]), Convert.ToDouble(Value[0][17]), Convert.ToDouble(Value[0][21]));
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
            HRV_dataGridView.Columns.Add("A9", "팬.모터제어");
            HRV_dataGridView.Columns.Add("A10", "소비전력.[W]");


            string[][] Value = Program.DB.getValue(DB.type.ProjDB, "User_HRV", "번호,명칭,열회수유형, 온도교환효율_냉방, 온도교환효율_난방, 습도교환효율_냉방, 습도교환효율_난방, 팬풍량, 팬정압, 모터제어, 팬동력,설치유형", "번호 ='" + SelectHRV + "'");
            if (Value.Length > 0)
            {
                Num_textBox.Text = Value[0][0];
                Num = Value[0][0];
                Name_textBox.Text = Value[0][1];
                Name = Value[0][1];

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
            계산된_난방출력 = (난방코일_입구온도 - 난방코일_출구온도) * 0.34 * 급기풍량 / 3600;
        }

        private void AHULocation_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (AHULocation_comboBox.SelectedItem != null)
            {
                AHULocation = AHULocation_comboBox.SelectedItem.ToString();
                Change_DuctLabel(AHULocation);
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
                AHUInsulationThickness = Convert.ToDouble(AHUInsulationThickness_comboBox.SelectedItem.ToString());
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
            if (OASALength_textBox.Text != null && OASALength_textBox.Text != "")
            {
                if (AHULocation != "단열외피 내부")
                {
                    SALength = Convert.ToDouble(OASALength_textBox.Text);
                    OALength = 0;
                }
                else
                {
                    OALength = Convert.ToDouble(OASALength_textBox.Text);
                    SALength = 0;
                }
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
            if (EARALength_textBox.Text != null && EARALength_textBox.Text != "")
            {
                if (AHULocation != "단열외피 내부")
                {
                    RALength = Convert.ToDouble(EARALength_textBox.Text);
                    EALength = 0;
                }
                else
                {
                    EALength = Convert.ToDouble(EARALength_textBox.Text);
                    RALength = 0;
                }
            }
        }


        private void DuctInsulationThickness_textBox_TextChanged(object sender, EventArgs e)
        {
            if (DuctInsulationThickness_textBox.Text != null && DuctInsulationThickness_textBox.Text != "")
            {
                DuctInsulationThickness = Convert.ToDouble(DuctInsulationThickness_textBox.Text);
            }
        }
        private void DuctDiameter_textBox_TextChanged(object sender, EventArgs e)
        {
            if (DuctDiameter_textBox.Text != null && DuctDiameter_textBox.Text != "")
            {
                DuctDiameter = Convert.ToDouble(DuctDiameter_textBox.Text);
            }
        }

        private void PipeIns_button_Click(object sender, EventArgs e)
        {
            CW_PanelDB InsDB_form = new CW_PanelDB();
            DialogResult result = InsDB_form.ShowDialog();
            if (result == DialogResult.OK)
            {
                PipeIns = InsDB_form.Select_CWPanel[1];
                PipeIns_textBox.Text = PipeIns;
                PipeIns_Ramda = Convert.ToDouble(InsDB_form.Select_CWPanel[4]);
                PipeIns_Ramda_textBox.Text = PipeIns_Ramda.ToString();
            }
            else
            {
                PipeIns_Ramda = 0.035;
                PipeIns_Ramda_textBox.Text = PipeIns_Ramda.ToString();
                PipeIns_textBox.Text = "일반 보온재";
                PipeIns= "일반 보온재";
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
                CooltubeInfo_groupBox.Visible = true;
            }
            else if (PrehPrecOptions == "프리히터")
            {
                GroundInfo_groupBox.Visible = false;
                CooltubeInfo_groupBox.Visible = false;
                PrehInfo_groupBox.Visible = true;
            }
            else
            {
                GroundInfo_groupBox.Visible = false;
                CooltubeInfo_groupBox.Visible = false;
                PrehInfo_groupBox.Visible = false;
            }
        }

        private void PrehControlOptions_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (AHUOptions_comboBox.SelectedItem != null)
            {
                PrehControlOptions = PrehControlOptions_comboBox.SelectedItem.ToString();
            }
            else
            {
                PrehControlOptions = null;
            }
        }

        private void PrehPower_textBox_TextChanged(object sender, EventArgs e)
        {
            if (PrehPower_textBox.Text != null && PrehPower_textBox.Text != "")
            {
                PrehPower = Convert.ToDouble(PrehPower_textBox.Text.ToString());
            }
            else
            {
                PrehPower = 0;
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
            if (GroundDepth_textBox.Text != null && GroundDepth_textBox.Text != "")
            {
                GroundDepth = Convert.ToDouble(GroundDepth_textBox.Text.ToString());
            }
            else
            {
                GroundDepth = 0;
            }
        }

        private void CooltubeDiameter_textBox_TextChanged(object sender, EventArgs e)
        {
            if (CooltubeDiameter_textBox.Text != null && CooltubeDiameter_textBox.Text != "")
            {
                CooltubeDiameter = Convert.ToDouble(CooltubeDiameter_textBox.Text.ToString());
            }
            else
            {
                CooltubeDiameter = 0;
            }

        }

        private void CooltubeThickness_textBox_TextChanged(object sender, EventArgs e)
        {
            if (CooltubeThickness_textBox.Text != null && CooltubeThickness_textBox.Text != "")
            {
                CooltubeThickness = Convert.ToDouble(CooltubeThickness_textBox.Text.ToString());
            }
            else
            {
                CooltubeThickness = 0;
            }
        }

        private void CooltubeLength_textBox_TextChanged(object sender, EventArgs e)
        {
            if (CooltubeLength_textBox.Text != null && CooltubeLength_textBox.Text != "")
            {
                CooltubeLength = Convert.ToDouble(CooltubeLength_textBox.Text.ToString());
            }
            else
            {
                CooltubeLength = 0;
            }
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

        #region 세이브
        private void Save_button_Click(object sender, EventArgs e)
        {
            Save();

        }
        private void Save()
        {
            Program.DB.setValue(DB.type.ProjDB, "AHUSystem_Form", "번호,프로젝트유형,명칭,존", "'" + Num_textBox.Text + "','" + 프로젝트유형[0][0] + "','" + Name + "','" + SelectZone_nonsplit + "'", "번호");
            Program.DB.setValue(DB.type.ProjDB, "AHUSystem_Form", "번호,유형,시스템번호", "'" + Num_textBox.Text + "','" + AHUOptions + "','" + SelectSystem + "'", "번호");
            Program.DB.setValue(DB.type.ProjDB, "AHUSystem_Form", "번호,설치위치,풍량제어,누기시험방법,누기등급1,누기등급2,공조기단열두께,TAB실시유무", "'" + Num_textBox.Text + "','" + AHULocation + "','" + AHUVolumeControl + "','" + AHULeakageTestMethod + "','" + AHULeakageLevel1 + "','" + AHULeakageLevel2 + "','" + AHUInsulationThickness.ToString() + "','" + TABOptions + "'", "번호");
            Program.DB.setValue(DB.type.ProjDB, "AHUSystem_Form", "번호,덕트누기수준,OA덕트길이,EA덕트길이,SA덕트길이,RA덕트길이,덕트단열두께,덕트관경,덕트단열재,덕트단열재열전도율", "'" + Num_textBox.Text + "','" + DuctLeakageLevel + "','" + OALength.ToString() + "','" + EALength.ToString() + "','" + SALength.ToString() + "','" + RALength.ToString() + "','" + DuctInsulationThickness.ToString() + "','" + DuctDiameter.ToString() + "','" + PipeIns + "','" + PipeIns_Ramda.ToString() + "'", "번호");
            Program.DB.setValue(DB.type.ProjDB, "AHUSystem_Form", "번호,예열예냉유형,프리히터제어유형,프리히터용량", "'" + Num_textBox.Text + "','" + PrehPrecOptions + "','" + PrehControlOptions + "','" + PrehPower + "'", "번호");
            Program.DB.setValue(DB.type.ProjDB, "AHUSystem_Form", "번호,토양유형,지중깊이,쿨튜브관경,쿨튜브두께,쿨튜브길이,쿨튜브재질", "'" + Num_textBox.Text + "','" + GroundOptions + "','" + GroundDepth.ToString() + "','" + CooltubeDiameter.ToString() + "','" + CooltubeThickness.ToString() + "','" + CooltubeLength.ToString() + "','" + CooltubeMaterial + "'", "번호");
            this.DialogResult = DialogResult.OK;
            this.Hide();
            Program.getMenuForm().DoLoadForm(57, OnLoadListProc);
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
            Num = null; Name = null; SelectZone_nonsplit = null; SelectSystem = null; AHUOptions = null;
            AHULocation = null; AHUVolumeControl = null; AHULeakageTestMethod = null; AHULeakageLevel1 = null; AHULeakageLevel2 = null;
            AHUInsulationThickness = 0;
            DuctLeakageLevel = null;
            OALength = 0; EALength = 0;
            SALength = 0; RALength = 0; DuctInsulationThickness = 0; DuctDiameter = 0;
            SelectZone_split.Clear();
            TABOptions = null; PipeIns = null; PipeIns_Ramda = 0;
            PrehPrecOptions = null; PrehControlOptions = null; GroundOptions = null; CooltubeMaterial = null;
            PrehPower = 0; GroundDepth = 0; CooltubeDiameter = 0; CooltubeThickness = 0; CooltubeLength = 0;
            AnnualCoolingNeed = 0; AnnualHeatingNeed = 0;
            CoolingLoad = 0; HeatingLoad = 0;
            계산된_냉방출력 = 0; 계산된_난방출력 = 0;

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
            PrehControlOptions_comboBox.SelectedItem = null;

            OASALength_textBox.Text = null;
            EARALength_textBox.Text = null;
            DuctDiameter_textBox.Text = null;
            DuctInsulationThickness_textBox.Text = null;
            PipeIns_textBox.Text = null;
            PipeIns_Ramda_textBox.Text = null;
            PrehPower_textBox.Text = null;
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

            string[][] Value = Program.DB.getValue(DB.type.ProjDB, "AHUSystem_Form", "명칭,유형,존", "번호 = '" + ID + "'");
            if (Value.Length > 0)
            {
                Name_textBox.Text = Value[0][0];
                Name = Value[0][0];

                AHUOptions_comboBox.SelectedItem = Value[0][1];
                AHUOptions = Value[0][1];
                ChangeAHUOptions(AHUOptions);
                if (AHUOptions == "공조기")
                {
                    load_Table_AHU(Num);
                }
                else
                {
                    load_Table_HRV(Num);
                }
                SelectZone_nonsplit = Value[0][2];
                Split_Zone(SelectZone_nonsplit);
                Cal_Qb();
            }

            Value = Program.DB.getValue(DB.type.ProjDB, "AHUSystem_Form", "설치위치, 풍량제어, 누기시험방법, 누기등급1, 공조기단열두께, TAB실시유무", "번호 = '" + ID + "'");
            if (Value.Length > 0)
            {
                AHULocation_comboBox.SelectedItem = Value[0][0];
                AHULocation = Value[0][0];
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
                AHUInsulationThickness = Convert.ToDouble(Value[0][4]);

                TABOptions_comboBox.SelectedItem = Value[0][5];
                TABOptions = Value[0][5];
                Check_DuctLeakageLevel();

            }

            Value = Program.DB.getValue(DB.type.ProjDB, "AHUSystem_Form", "OA덕트길이,EA덕트길이,SA덕트길이,RA덕트길이,덕트단열두께,덕트관경,덕트단열재,덕트단열재열전도율", "번호 = '" + ID + "'");
            if (Value.Length > 0)
            {

                if (AHULocation == "단열외피 내부")
                {
                    OASALength_textBox.Text = Value[0][0];
                    OALength = Convert.ToDouble(Value[0][0]);

                    EARALength_textBox.Text = Value[0][1];
                    EALength = Convert.ToDouble(Value[0][1]);
                }
                else
                {
                    OASALength_textBox.Text = Value[0][2];
                    SALength = Convert.ToDouble(Value[0][2]);

                    EARALength_textBox.Text = Value[0][3];
                    RALength = Convert.ToDouble(Value[0][3]);
                }

                DuctInsulationThickness_textBox.Text = Value[0][4];
                DuctInsulationThickness = Convert.ToDouble(Value[0][4]);

                DuctDiameter_textBox.Text = Value[0][5];
                DuctDiameter = Convert.ToDouble(Value[0][5]);

                PipeIns_textBox.Text = Value[0][6];
                PipeIns = Value[0][6];

                PipeIns_Ramda_textBox.Text = Value[0][7];
                PipeIns_Ramda = Convert.ToDouble(Value[0][7]);
            }

            Value = Program.DB.getValue(DB.type.ProjDB, "AHUSystem_Form", "예열예냉유형", "번호 = '" + ID + "'");
            if (Value.Length > 0)
            {
                PrehPrecOptions_comboBox.SelectedItem = Value[0][0];
                PrehPrecOptions = Value[0][0];
                ChangeVisble_PrehPrecOptions(PrehPrecOptions);
            }

            Value = Program.DB.getValue(DB.type.ProjDB, "AHUSystem_Form", "프리히터제어유형,프리히터용량", "번호 = '" + ID + "'");
            if (Value.Length > 0)
            {
                PrehControlOptions_comboBox.SelectedItem = Value[0][0];
                PrehControlOptions = Value[0][0];

                PrehPower_textBox.Text = Value[0][1];
                PrehPower = Convert.ToDouble(Value[0][1]);
            }
            else
            {
                PrehInfo_groupBox.Visible = false;
            }

            Value = Program.DB.getValue(DB.type.ProjDB, "AHUSystem_Form", "토양유형,지중깊이,쿨튜브관경,쿨튜브두께,쿨튜브길이,쿨튜브재질", "번호 = '" + ID + "'");
            if (Value.Length > 0)
            {
                GroundOptions_comboBox.SelectedItem = Value[0][0];
                GroundOptions = Value[0][0];

                GroundDepth_textBox.Text = Value[0][1];
                GroundDepth = Convert.ToDouble(Value[0][1]);

                CooltubeDiameter_textBox.Text = Value[0][2];
                CooltubeDiameter = Convert.ToDouble(Value[0][2]);

                CooltubeThickness_textBox.Text = Value[0][3];
                CooltubeThickness = Convert.ToDouble(Value[0][3]);

                CooltubeLength_textBox.Text = Value[0][4];
                CooltubeLength = Convert.ToDouble(Value[0][4]);

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

    }
}
