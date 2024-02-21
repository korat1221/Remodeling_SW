using main.contentslist;
using main.subcontents.ConstructionCW;
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
        String Num, Name; String SelectZone_nonsplit;
        String HRVLocation, HRVVolumeControl, HRVLeakageLevel, HRVInsulationThickness;

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



    }
}
 