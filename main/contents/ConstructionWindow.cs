using main.subcontents;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace main.contents
{
    public partial class ConstructionWindow : Form
    {
        String Type, UwMehod, FrameType, SingleDoubleType, FrameMaterial, FrameName, GlassName, SpacerName, InstallName;
        string[][] Frame; string[][] Glass; string[][] Spacer; string[][] Install;

        public ConstructionWindow()
        {
            InitializeComponent();
            Program.DB.initTable(DB.type.CalcDB, "Select_WindowFrame"); //선택한 프레임 정보 저장할 table 생성
            Program.DB.initTable(DB.type.CalcDB, "Select_WindowGlass"); //선택한 유리 정보 저장할 table 생성
            Program.DB.initTable(DB.type.CalcDB, "Select_WindowSpacer"); //선택한 유리 정보 저장할 table 생성
            Program.DB.initTable(DB.type.CalcDB, "Select_WindowInstall"); //선택한 유리 정보 저장할 table 생성
            //프레임종류 콤보박스
            Program.UTIL.FillComboBox_ByCategory(Frame_comboBox, "창호", "프레임시스템", "1");
            //설치위치 콤보박스
            Program.UTIL.FillComboBox_ByCategory(Install_comboBox, "창호", "구조", "1");


        }

        private void GeneralPanel_Paint(object sender, PaintEventArgs e)
        {
            Panel p = (Panel)sender;
            ControlPaint.DrawBorder(e.Graphics, p.DisplayRectangle, Color.FromArgb(153, 180, 209), ButtonBorderStyle.Solid);
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            Type = "기존창호";
            Type_textBox.Text = Type;
            Add_Uw_CheckBox(Type);
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            Type = "신규";
            Type_textBox.Text = Type;
            Add_Uw_CheckBox(Type);
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            Type = "철거 후 신규";
            Type_textBox.Text = Type;
            Add_Uw_CheckBox(Type);
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            Type = "외부(커튼월)덧댐";
            Type_textBox.Text = Type;
            Add_Uw_CheckBox(Type);
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            Type = "내부덧댐";
            Type_textBox.Text = Type;
            Add_Uw_CheckBox(Type);
        }

        public void Add_Uw_CheckBox(String Type)
        {
            Uw_comboBox.Items.Clear();
            switch (Type)
            {
                case "기존창호":
                    Uw_comboBox.Items.Add("계산");
                    Uw_comboBox.Items.Add("법규");
                    Uw_comboBox.Items.Add("진단");
                    break;

                case "신규":
                    Uw_comboBox.Items.Add("계산");
                    Uw_comboBox.Items.Add("법규");
                    break;

                case "철거 후 신규":
                    Uw_comboBox.Items.Add("계산");
                    Uw_comboBox.Items.Add("법규");
                    break; ;

                case "외부(커튼월)덧댐":
                    Uw_comboBox.Items.Add("계산");
                    break;

                case "내부덧댐":
                    Uw_comboBox.Items.Add("계산");
                    break;
            }

            Uw_comboBox.SelectedIndex = 0;
        }

        private void Uw_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Uw_comboBox.SelectedItem == "계산" || Uw_comboBox.SelectedItem == "진단")
            {
                DiIndiCal_comboBox.Enabled = true;
                DiIndiCal_comboBox.Visible = true;
                //계산 시 직접/간접 콤보박스
                Program.UTIL.FillComboBox_ByCategory(DiIndiCal_comboBox, "창호", "실외조건", "1");
            }
            else
            {
                DiIndiCal_comboBox.SelectedItem = null;
                DiIndiCal_comboBox.Enabled = false;
                DiIndiCal_comboBox.Visible = false;

            }

        }

        private void Frame_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            FrameType = Program.UTIL.SelectedItem_ByComboBox(Frame_comboBox);
            switch (FrameType)
            {
                case "단창_SL":
                    SingleDoubleType = "단창";
                    break;

                case "단창_T/T":
                    SingleDoubleType = "단창";
                    break;

                case "이중창_SL":
                    SingleDoubleType = "이중창";
                    break;
            }
        }


        private void FrameDB_button_Click(object sender, EventArgs e)
        {
            Window_FrameDB window_frameDB_form = new Window_FrameDB(FrameType);
            DialogResult result = window_frameDB_form.ShowDialog();
            if (result == DialogResult.OK)
            {
                Frame = Program.DB.getValue(DB.type.CalcDB, "Select_WindowFrame", "번호,제품명,제조사,프레임종류,프레임재료,개폐부프레임열관류율,고정부프레임열관류율,중간바프레임열관류율,개폐부프레임두께,고정부프레임두께,중간바프레임두께", "");
                FrameName = Frame[0][2];
                FrameMaterial = Frame[0][5];
                switch (FrameMaterial)
                {
                    case "플라스틱":
                        FrameMaterial = FrameMaterial;
                        break;

                    case "금속":
                        FrameMaterial = FrameMaterial;
                        break;

                    case "금속_단열바":
                        FrameMaterial = "금속";
                        break;
                }
                FrameName_textBox.Text = FrameName;
            }
        }

        private void Glass_button_Click(object sender, EventArgs e)
        {
            Window_GlassDB window_glassDB_form = new Window_GlassDB();
            DialogResult result = window_glassDB_form.ShowDialog();
            if (result == DialogResult.OK)
            {
                Glass = Program.DB.getValue(DB.type.CalcDB, "Select_WindowGlass", "번호,제품명,제조사,복층_삼중_단창,아르곤_공기,LE_CL_V,열관류율,태양열취득율,빛투과율,외부반사율,내부반사율", "");

                GlassName = Glass[0][2];
                GlassName_textBox.Text = GlassName;
            }

        }

        private void Spacer_button_Click(object sender, EventArgs e)
        {
            Window_SpacerDB window_spacerDB_form = new Window_SpacerDB(SingleDoubleType, FrameMaterial);
            DialogResult result = window_spacerDB_form.ShowDialog();
            if (result == DialogResult.OK)
            {
                Spacer = Program.DB.getValue(DB.type.CalcDB, "Select_WindowSpacer", "번호,제품명,구분1,구분2,구분3,고정유리_CL_선형열관류율,개폐유리_CL_선형열관류율,고정유리_LE_선형열관류율,개폐유리_LE_선형열관류율", "");

                SpacerName = Spacer[0][3];
                SpacerName_textBox.Text = SpacerName;
            }


        }

        private void Install_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            InstallName = Program.UTIL.SelectedItem_ByComboBox(Install_comboBox);

        }

        private void Install_button_Click(object sender, EventArgs e)
        {
            Window_InstallDB window_installDB_form = new Window_InstallDB(InstallName,SingleDoubleType, FrameMaterial);
            DialogResult result = window_installDB_form.ShowDialog();
            if (result == DialogResult.OK)
            {
                Install = Program.DB.getValue(DB.type.CalcDB, "Select_WindowInstall", "번호,제품명,구분1,구분2,구분3,구분4,상부설치선형열관류율,측면설치선형열관류율,하부설치선형열관류율", "");

                InstallName = Install[0][6];
                Install_textBox.Text = InstallName;
            }


        }
    }
}
