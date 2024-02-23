using main.contentslist;
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
        String Num = "AHU01"; string Name; String SelectZone_nonsplit, AHUOptions;
        String HRVLocation, HRVVolumeControl, HRVLeakageLevel, HRVInsulationThickness;
        String AHULocation, AHUVolumeControl, AHULeakageLevel, AHUInsulationThickness;
        String PrehPrecOptions;
        double OASALength, EARALength, DuctInsulationThickness, DuctDiameter;
        ArrayList SelectZone_split = new ArrayList();

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

            //열회수설치위치 콤보박스
            HRVLocation_comboBox.Items.Clear();
            HRVLocation_comboBox.Items.Add("단열외피 내부");
            HRVLocation_comboBox.Items.Add("단열외피 외부");
            HRVLocation_comboBox.Items.Add("외기");

            //열회수누기등급 콤보박스
            HRVLeakageLevel_comboBox.Items.Clear();
            HRVLeakageLevel_comboBox.Items.Add("A1/B1/C1");
            HRVLeakageLevel_comboBox.Items.Add("A2/B2/C2");
            HRVLeakageLevel_comboBox.Items.Add("A3/B3/C3");

            //열회수풍량제어 콤보박스
            HRVVolumeControl_comboBox.Items.Clear();
            HRVVolumeControl_comboBox.Items.Add("수동 제어");
            HRVVolumeControl_comboBox.Items.Add("시간 제어");
            HRVVolumeControl_comboBox.Items.Add("실내공기질 중앙제어");
            HRVVolumeControl_comboBox.Items.Add("사용유무 제어(조명, 열화상 활용)");
            HRVVolumeControl_comboBox.Items.Add("재실자 수 제어");
            HRVVolumeControl_comboBox.Items.Add("실내공기질 개별제어");

            //열회수단열두께 콤보박스
            HRVInsulationThickness_comboBox.Items.Clear();
            HRVInsulationThickness_comboBox.Items.Add("10");
            HRVInsulationThickness_comboBox.Items.Add("20");
            HRVInsulationThickness_comboBox.Items.Add("30");
            HRVInsulationThickness_comboBox.Items.Add("40");

            //공조기설치위치 콤보박스
            AHULocation_comboBox.Items.Clear();
            AHULocation_comboBox.Items.Add("단열외피 내부");
            AHULocation_comboBox.Items.Add("단열외피 외부");
            AHULocation_comboBox.Items.Add("외기");

            //공조기누기등급 콤보박스
            AHULeakageLevel_comboBox.Items.Clear();
            AHULeakageLevel_comboBox.Items.Add("A1/B1/C1");
            AHULeakageLevel_comboBox.Items.Add("A2/B2/C2");
            AHULeakageLevel_comboBox.Items.Add("A3/B3/C3");

            //공조기풍량제어 콤보박스
            AHUVolumeControl_comboBox.Items.Clear();
            AHUVolumeControl_comboBox.Items.Add("수동 제어");
            AHUVolumeControl_comboBox.Items.Add("시간 제어");
            AHUVolumeControl_comboBox.Items.Add("실내공기질 중앙제어");
            AHUVolumeControl_comboBox.Items.Add("사용유무 제어(조명, 열화상 활용)");
            AHUVolumeControl_comboBox.Items.Add("재실자 수 제어");
            AHUVolumeControl_comboBox.Items.Add("실내공기질 개별제어");

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
            PrehControlOptions_comboBox.Items.Add("온도자동제어");

        }
        private void GeneralPanel_Paint(object sender, PaintEventArgs e)
        {
            Panel p = (Panel)sender;
            ControlPaint.DrawBorder(e.Graphics, p.DisplayRectangle, Color.FromArgb(153, 180, 209), ButtonBorderStyle.Solid);
        }

        private void Name_textBox_TextChanged(object sender, EventArgs e)
        {
            if (Name_textBox.Text != null)
            {
                Name = Name_textBox.Text.ToString();
            }
        }

        private void AHUOptions_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (AHUOptions_comboBox.SelectedItem != null)
            {
                AHUOptions = AHUOptions_comboBox.SelectedItem.ToString();
                LoadtabPage(AHUOptions);
                ChangeVisble_AHUOptions(AHUOptions);
            }
            else
            {
                AHUOptions = null;
            }
        }             

        private void LoadtabPage(String AHUOptions)
        {
            if (AHUOptions == "열회수기")
            {
                tabControl1.SelectedTab = tabControl1.TabPages["HRV_tabPage"];
            }
            else if (AHUOptions == "공조기")
            {
                tabControl1.SelectedTab = tabControl1.TabPages["AHU_tabPage"];
            }
        }

        //private void AHUoptions_button_Click(object sender, EventArgs e)
        //{
        //if (AHUOptions == "열회수기")
        //{
        //Load_HRVForm();
        //}

        //else if (AHUOptions == "공조기")
        //{
        //Load_AHUForm();
        //}
        //}

        #region 열회수기
        /////////////////////////////////////////////////////열회수기//////////////////////////////////////////////////////////////

        private void HRVLocation_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (HRVLocation_comboBox.SelectedItem != null)
            {
                HRVLocation = HRVLocation_comboBox.SelectedItem.ToString();
            }
            else
            {
                HRVLocation = null;
            }
        }

        private void HRVVolumeControl_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (HRVVolumeControl_comboBox.SelectedItem != null)
            {
                HRVVolumeControl = HRVVolumeControl_comboBox.SelectedItem.ToString();
            }
            else
            {
                HRVVolumeControl = null;
            }
        }

        private void HRVLeakageLevel_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (HRVLeakageLevel_comboBox.SelectedItem != null)
            {
                HRVLeakageLevel = HRVLeakageLevel_comboBox.SelectedItem.ToString();
            }
            else
            {
                HRVLeakageLevel = null;
            }
        }

        private void HRVInsulationThickness_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (HRVInsulationThickness_comboBox.SelectedItem != null)
            {
                HRVInsulationThickness = HRVInsulationThickness_comboBox.SelectedItem.ToString();
            }
            else
            {
                HRVInsulationThickness = null;
            }
        }

        private void ChangeVisble_AHUOptions(String AHUOptions)
        {
            if (AHUOptions == "열회수기")
            {
                HRVLocation_label.Visible = true;
                HRVLocation_comboBox.Visible = true;
                HRVLeakageLevel_label.Visible = true;
                HRVLeakageLevel_comboBox.Visible = true;
                HRVVolumeControl_label.Visible = true;
                HRVVolumeControl_comboBox.Visible = true;
                HRVInsulationThickness_Label.Visible = true;
                HRVInsulationThickness_comboBox.Visible = true;
                label2.Visible = true;
                HRV_dataGridView.Visible = true;
                AHULocation_label.Visible = false;
                AHULocation_comboBox.Visible = false;
                AHULeakageLevel_label.Visible = false;
                AHULeakageLevel_comboBox.Visible = false;
                AHUVolumeControl_label.Visible = false;
                AHUVolumeControl_comboBox.Visible = false;
                AHUInsulationThickness_label.Visible = false;
                AHUInsulationThickness_comboBox.Visible = false;
                label18.Visible = false;
                AHU_dataGridView.Visible = false;
            }
            else if (AHUOptions == "공조기")
            {
                HRVLocation_label.Visible = false;
                HRVLocation_comboBox.Visible = false;
                HRVLeakageLevel_label.Visible = false;
                HRVLeakageLevel_comboBox.Visible = false;
                HRVVolumeControl_label.Visible = false;
                HRVVolumeControl_comboBox.Visible = false;
                HRVInsulationThickness_Label.Visible = false;
                HRVInsulationThickness_comboBox.Visible = false;
                label2.Visible = false;
                HRV_dataGridView.Visible = false;
                AHULocation_label.Visible = true;
                AHULocation_comboBox.Visible = true;
                AHULeakageLevel_label.Visible = true;
                AHULeakageLevel_comboBox.Visible = true;
                AHUVolumeControl_label.Visible = true;
                AHUVolumeControl_comboBox.Visible = true;
                AHUInsulationThickness_label.Visible = true;
                AHUInsulationThickness_comboBox.Visible = true;
                label18.Visible = true;
                AHU_dataGridView.Visible = true;
            }
        }
        #endregion

        #region 공조기
        /////////////////////////////////////////////////////공조기//////////////////////////////////////////////////////////////

        private void AHULocation_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (AHULocation_comboBox.SelectedItem != null)
            {
                AHULocation = AHULocation_comboBox.SelectedItem.ToString();
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

        private void AHULeakageLevel_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (AHULeakageLevel_comboBox.SelectedItem != null)
            {
                AHULeakageLevel = AHULeakageLevel_comboBox.SelectedItem.ToString();
            }
            else
            {
                AHULeakageLevel = null;
            }
        }

        private void AHUInsulationThickness_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (AHUInsulationThickness_comboBox.SelectedItem != null)
            {
                AHUInsulationThickness = AHUInsulationThickness_comboBox.SelectedItem.ToString();
            }
            else
            {
                AHUInsulationThickness = null;
            }
        }
        #endregion

        #region 덕트
        /////////////////////////////////////////////////////덕트//////////////////////////////////////////////////////////////
        private void OASALength_textBox_TextChanged(object sender, EventArgs e)
        {
            if (OASALength_textBox.Text != null && OASALength_textBox.Text != "")
            { OASALength = Convert.ToDouble(OASALength_textBox.Text); }
        }
        private void EARALength_textBox_TextChanged(object sender, EventArgs e)
        {
            if (EARALength_textBox.Text != null && EARALength_textBox.Text != "")
            {
                EARALength = Convert.ToDouble(EARALength_textBox.Text);
            }
        }

        private void DuctInsulationThickness_textBox_TextChanged(object sender, EventArgs e)
        {
            if (DuctInsulationThickness_textBox.Text != null && DuctInsulationThickness_textBox.Text != "")
            { DuctInsulationThickness = Convert.ToDouble(DuctInsulationThickness_textBox.Text); }
        }
        private void DuctDiameter_textBox_TextChanged(object sender, EventArgs e)
        {
            if (DuctDiameter_textBox.Text != null && DuctDiameter_textBox.Text != "")
            {
                DuctDiameter = Convert.ToDouble(DuctDiameter_textBox.Text);
            }
        }
        #endregion

        #region 예열/예냉
        /////////////////////////////////////////////////////예열/예냉//////////////////////////////////////////////////////////////

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


            this.DialogResult = DialogResult.OK;
            this.Hide();
            //Program.getMenuForm().DoLoadForm(39, OnLoadListProc);
        }

        public static bool OnLoadListProc(Form form)
        {
            //List_HeatingSystem f = (List_HeatingSystem)form;
            //f.load_List();
            return true;
        }

        private void reset()
        {

        }

        #endregion

        #region 로드
        public void LoadData(String ID)            // 리스트에서 항목 더블 클릭시 - 뷰를 ID 의 getValue 값으로 채우기
        {
            reset();

            Num_textBox.Text = ID;
            Num = ID;

        }

        #endregion

        #region 리셋 
        public void ResetForm(String ID) // 리스트에서 추가 버튼 클릭시 - 뷰 초기화
        {
            Num_textBox.Text = ID;
            Num = ID;
        }

        #endregion




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
    }
}
