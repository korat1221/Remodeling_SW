using main.contentslist;
using System;
using main.subcontents.BuildingGeneral;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Net;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using static System.ComponentModel.Design.ObjectSelectorEditor;
using static main.DB;
using System.Data.Entity.Core.Metadata.Edm;
using System.Security.Cryptography;
using System.Xml.Linq;

using Eagle._Components.Public;
using System.Drawing.Text;
using main.subcontents.HeatingSystem;
using System.Text.RegularExpressions;

namespace main.contents
{
    public partial class General : Form
    {
        String ProjectName, ProjectType, ProjectNum;
        String BuildingCategory, BuildingUse, BuildingName, BuildingLocation, Climate, BylawClimate;
        double Year, Month;
        double ConstrucitonDate, BylawDate;
        double GrossArea, BuildingArea;
        String AboveGround, UnderGround;
        String ReviewerName, ReviewerLocation, ReviewerCompany;
        double ReviewYear, ReviewMonth;
        double ReviewDate;
        double[] law = new double[11];
        string OldProject;
        string BlowDoorTest;
        bool Door_Infil = false, Win_Infil = false, ElecWiring_Infil = false, Pipe_Infil = false;
        double n50;

        public General()
        {
            InitializeComponent(); this.Font = new Font(UTIL.Families[0], 9.75F, FontStyle.Regular);
            string[][] Image = Program.DB.getValue(DB.type.BaseDB_HCneed, "메뉴아이콘", "하위메뉴아이콘", "하위메뉴명 = '일반정보'");
            if (Image.Length > 0)
            {
                Icon_pictureBox.Load(Program.gPath + Image[0][0]);
                Icon_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            }

            label23.Text = "m" + Program.UTIL.Subscript(2, true);
            label24.Text = "m" + Program.UTIL.Subscript(2, true);
            //프로젝트명
            string[][] res = Program.DB.querySQL(DB.type.ProjListDB, "SELECT title FROM projects WHERE current='1'");
            ProjectName = res[0][0];
            if (ProjectName != null) { ProjectName_textBox.Text = ProjectName.ToString(); }
            else { }

            //프로젝트유형
            string[][] 번호 = Program.DB.querySQL(DB.type.ProjListDB, "Select type from projects where current = '1'");
            if (번호.Length > 0)
            {
                switch (번호[0][0])
                {
                    case "1":
                        ProjectType = "기존";
                        OldProject_label.Visible = false;
                        OldProject_textBox.Visible = false;
                        break;
                    case "2":
                        ProjectType = "리트로핏";
                        OldProject_label.Visible = true;
                        OldProject_textBox.Visible = true;
                        break;
                    case "3":
                        ProjectType = "리모델링";
                        OldProject_label.Visible = true;
                        OldProject_textBox.Visible = true;
                        break;
                    case "4":
                        ProjectType = "신규";
                        OldProject_label.Visible = false;
                        OldProject_textBox.Visible = false;
                        break;
                }
                if (ProjectType != null) { ProjectType_textBox.Text = ProjectType.ToString(); }
                else { }
            }

            //건물대상 콤보박스
            LoadOption_BuildingCategory();

            //기후데이터 콤보박스
            string[][] Value = Program.DB.getValue(DB.type.BaseDB_HCneed, "인덱스", "이름", "종류 = '2'");
            if (Value.Length > 0)
            {
                Climate_comboBox.Items.Clear();
                for (int i = 0; i < Value.Length; i++)
                {
                    Climate_comboBox.Items.Add(Value[i][0]);
                }
            }

            //준공연월 연 콤보박스
            Year_comboBox.Items.Clear();
            for (int i = 0; i < 100; i++)
            {
                Year_comboBox.Items.Add((2040 - i).ToString());
            }
            //준공연월 월 콤보박스
            Month_comboBox.Items.Clear();
            for (int i = 0; i < 12; i++)
            {
                Month_comboBox.Items.Add((i + 1).ToString());
            }

            //작성시기 연 콤보박스
            ReviewYear_comboBox.Items.Clear();
            for (int i = 0; i < 10; i++)
            {
                ReviewYear_comboBox.Items.Add((2030 - i).ToString());
            }
            //작성시기 월 콤보박스
            ReviewMonth_comboBox.Items.Clear();
            for (int i = 0; i < 12; i++)
            {
                ReviewMonth_comboBox.Items.Add((i + 1).ToString());
            }
            //지상층수
            AboveGround_comboBox.Items.Clear();
            for (int i = 0; i < 30; i++)
            {
                AboveGround_comboBox.Items.Add((i + 1).ToString());
            }
            AboveGround_comboBox.SelectedIndex = 0;

            //지하층수
            UnderGround_comboBox.Items.Clear();
            for (int i = 0; i < 11; i++)
            {
                UnderGround_comboBox.Items.Add((i).ToString());
            }
            UnderGround_comboBox.SelectedIndex = 0;

            //기밀측정여부
            BlowDoorTest_comboBox.Items.Clear();
            BlowDoorTest_comboBox.Items.Add("기밀 테스트 실시");
            BlowDoorTest_comboBox.Items.Add("기밀 테스트 미실시");
            BlowDoorTest_comboBox.SelectedIndex = 1;
            BlowDoorTest = "기밀 테스트 미실시";

            Door_False_radioButton.Checked = true;
            Win_False_radioButton.Checked = true;
            ElecWiring_False_radioButton.Checked = true;
            Pipe_False_radioButton.Checked = true;
        }
        private void BuildingCategory_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (BuildingCategory_comboBox.SelectedItem != null && BuildingCategory_comboBox.SelectedItem.ToString() != "")
            {
                BuildingCategory = BuildingCategory_comboBox.SelectedItem.ToString();
                LoadOption_BuildingUse();
            }
        }

        private void BuildingUse_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (BuildingUse_comboBox.SelectedItem != null)
            {
                BuildingUse = BuildingUse_comboBox.SelectedItem.ToString();
            }
        }

        private void LoadOption_BuildingCategory()
        {
            string[][] V = Program.DB.querySQL(DB.type.BaseDB_HCneed, "Select a.이름 From 인덱스 as a Inner Join 인덱스분류 as b  on a.종류=b.아이디 Where b.종류='존일반' and b.이름='건물용도' Order by a.값 ");
            if (V.Length > 0)
            {
                BuildingCategory_comboBox.Items.Clear();
                for (int a = 0; a < V.Length; a++)
                {
                    BuildingCategory_comboBox.Items.Add(V[a][0]);
                }
            }
        }
        private void LoadOption_BuildingUse()
        {
            BuildingUse_comboBox.Items.Clear();
            string[][] V1 = Program.DB.querySQL(DB.type.BaseDB_HCneed, "Select 아이디 From 인덱스 Where 이름='" + BuildingCategory_comboBox.SelectedItem.ToString() + "'");
            if (V1.Length > 0)
            {
                string[][] V2 = Program.DB.querySQL(DB.type.BaseDB_HCneed, "Select 이름 From 인덱스 Where 부모아이디='" + V1[0][0] + "'");
                if (V2.Length > 0)
                {
                    for (int a = 0; a < V2.Length; a++)
                    {
                        BuildingUse_comboBox.Items.Add(V2[a][0]);
                    }
                    if (BuildingCategory_comboBox.SelectedItem.ToString() == "기타유형")
                    {
                        BuildingUse_comboBox.SelectedIndex = 0;
                        BuildingUse_label.Visible = false;
                        BuildingUse_comboBox.Visible = false;
                    }
                    else
                    {
                        BuildingUse_comboBox.SelectedIndex = 0;
                        BuildingUse_label.Visible = true;
                        BuildingUse_comboBox.Visible = true;
                    }

                }
            }
        }

        private void GeneralPanel_Paint(object sender, PaintEventArgs e)
        {
            Panel p = (Panel)sender;
            ControlPaint.DrawBorder(e.Graphics, p.DisplayRectangle, System.Drawing.SystemColors.Control, ButtonBorderStyle.Solid);
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            Panel p = (Panel)sender;
           // ControlPaint.DrawBorder(e.Graphics, p.DisplayRectangle, System.Drawing.SystemColors.Control, ButtonBorderStyle.Solid);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Panel p = (Panel)sender;
           // ControlPaint.DrawBorder(e.Graphics, p.DisplayRectangle, System.Drawing.SystemColors.Control, ButtonBorderStyle.Solid);
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {
            Panel p = (Panel)sender;
           // ControlPaint.DrawBorder(e.Graphics, p.DisplayRectangle, System.Drawing.SystemColors.Control, ButtonBorderStyle.Solid);
        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {
            Panel p = (Panel)sender;
          //  ControlPaint.DrawBorder(e.Graphics, p.DisplayRectangle, System.Drawing.SystemColors.Control, ButtonBorderStyle.Solid);
        }

        private void panel7_Paint(object sender, PaintEventArgs e)
        {
            Panel p = (Panel)sender;
          //  ControlPaint.DrawBorder(e.Graphics, p.DisplayRectangle, System.Drawing.SystemColors.Control, ButtonBorderStyle.Solid);
        }

        private void BlowDoorTest_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (BlowDoorTest_comboBox.SelectedItem != null)
            {
                BlowDoorTest = BlowDoorTest_comboBox.SelectedItem.ToString();
                Change_BlowDoorTest();
            }
        }
        private void Change_BlowDoorTest()
        {
            if (BlowDoorTest == "기밀 테스트 실시")
            {
                BlowDoor_button.Visible = true;

                Door_label.Visible = false;
                Door_groupBox.Visible = false;

                Win_label.Visible = false;
                Win_groupBox.Visible = false;

                ElecWiring_label.Visible = false;
                ElecWiring_groupBox.Visible = false;

                Pipe_label.Visible = false;
                Pipe_groupBox.Visible = false;
            }
            else
            {
                BlowDoor_button.Visible = false;

                Door_label.Visible = true;
                Door_groupBox.Visible = true;

                Win_label.Visible = true;
                Win_groupBox.Visible = true;

                ElecWiring_label.Visible = true;
                ElecWiring_groupBox.Visible = true;

                Pipe_label.Visible = true;
                Pipe_groupBox.Visible = true;
            }
        }
        private void Cal_Infiltration(string BlowDoorTest, bool door, bool win, bool elec, bool pipe)
        {
            if (BlowDoorTest == "기밀 테스트 미실시")
            {
                string[] check = new string[4];
                if (door) { check[0] = "적용"; } else { check[0] = "미적용"; }
                if (win) { check[1] = "적용"; } else { check[1] = "미적용"; }
                if (elec) { check[2] = "적용"; } else { check[2] = "미적용"; }
                if (pipe) { check[3] = "적용"; } else { check[3] = "미적용"; }

                string[][] Value = Program.DB.getValue(DB.type.BaseDB_HCneed, "기밀", "n50", "방풍출입문 ='" + check[0] + "' and 창호='" + check[1] + "' and 배선='" + check[2] + "' and 배관='" + check[3] + "'");
                if (Value.Length > 0)
                {
                    n50 = Convert.ToDouble(Value[0][0]);
                    n50_textBox.Text = n50.ToString("0.0");
                }
            }
        }

        private void Door_True_radioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (Door_True_radioButton.Checked == true)
            {
                Door_Infil = true;
                Cal_Infiltration(BlowDoorTest, Door_Infil, Win_Infil, ElecWiring_Infil, Pipe_Infil);
            }
        }

        private void Door_False_radioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (Door_False_radioButton.Checked == true)
            {
                Door_Infil = false;
                Cal_Infiltration(BlowDoorTest, Door_Infil, Win_Infil, ElecWiring_Infil, Pipe_Infil);
            }
        }

        private void Win_True_radioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (Win_True_radioButton.Checked == true)
            {
                Win_Infil = true;
                Cal_Infiltration(BlowDoorTest, Door_Infil, Win_Infil, ElecWiring_Infil, Pipe_Infil);
            }
        }

        private void Win_False_radioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (Win_False_radioButton.Checked == true)
            {
                Win_Infil = false;
                Cal_Infiltration(BlowDoorTest, Door_Infil, Win_Infil, ElecWiring_Infil, Pipe_Infil);
            }
        }

        private void ElecWiring_True_radioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (ElecWiring_True_radioButton.Checked == true)
            {
                ElecWiring_Infil = true;
                Cal_Infiltration(BlowDoorTest, Door_Infil, Win_Infil, ElecWiring_Infil, Pipe_Infil);
            }
        }

        private void ElecWiring_False_radioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (ElecWiring_False_radioButton.Checked == true)
            {
                ElecWiring_Infil = false;
                Cal_Infiltration(BlowDoorTest, Door_Infil, Win_Infil, ElecWiring_Infil, Pipe_Infil);
            }
        }

        private void Duct_True_radioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (Pipe_True_radioButton.Checked == true)
            {
                Pipe_Infil = true;
                Cal_Infiltration(BlowDoorTest, Door_Infil, Win_Infil, ElecWiring_Infil, Pipe_Infil);
            }
        }

        private void Pipe_False_radioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (Pipe_False_radioButton.Checked == true)
            {
                Pipe_Infil = false;
                Cal_Infiltration(BlowDoorTest, Door_Infil, Win_Infil, ElecWiring_Infil, Pipe_Infil);
            }

        }


        private void Climate_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Climate_comboBox.SelectedItem != null)
            {
                string[][] Value = Program.DB.getValue(DB.type.BaseDB_HCneed, "기후인덱스", "지역명,지역구분", "지역인덱스 = '" + Climate_comboBox.SelectedItem.ToString() + "'");
                if (Value.Length > 0)
                {
                    Climate = Value[0][0];
                    BylawClimate = Value[0][1];
                    ByRawClimate_textBox.Text = BylawClimate;
                }
            }
        }

        private void Year_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (Year_comboBox.SelectedItem != null && Year_comboBox.SelectedItem.ToString() != "")
            {
                Year = Convert.ToDouble(Year_comboBox.SelectedItem.ToString());
                Calc_LawDate();

            }
        }

        private void Month_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Month_comboBox.SelectedItem != null && Month_comboBox.SelectedItem.ToString() != "")
            {
                Month = Convert.ToDouble(Month_comboBox.SelectedItem.ToString());
                Calc_LawDate();
            }
        }

        private void Calc_LawDate()
        {
            if (Year != null && Month != null)
            {
                if (Convert.ToDouble(Month) < 10)
                {
                    ConstrucitonDate = Convert.ToDouble((Year + ".0" + Month));
                }
                else
                {
                    ConstrucitonDate = Convert.ToDouble((Year + "." + Month));
                }
                string[][] Value = Program.DB.getValue_SameCheck(DB.type.BaseDB_HCneed, "법규열관류율", "시기", "");
                if (Value.Length > 0)
                {
                    for (int i = 0; i < Value.Length; i++)
                    {
                        law[i] = Convert.ToDouble(Value[i][0]);
                    }
                }

                Array.Sort(law);
                for (int k = 0; k < 11; k++)
                {
                    if (ConstrucitonDate >= 2018.09)
                    {
                        BylawDate = 2018.09;
                        break;
                    }
                    else if (law[k] >= ConstrucitonDate)
                    {
                        if (k != 0)
                        {
                            BylawDate = law[k - 1];
                            break;
                        }
                        else
                        {
                            BylawDate = 1979.09;
                            break;
                        }
                    }
                    else { }
                }

                if (BylawDate == 2013.1)
                {
                    BylawDate_textBox.Text = "2013.10";
                }
                else { BylawDate_textBox.Text = Convert.ToString(BylawDate); }
            }

        }
        private void BuildingName_textBox_TextChanged(object sender, EventArgs e)
        {
            if (BuildingName_textBox.Text != null) { BuildingName = BuildingName_textBox.Text.ToString(); }
            else { }
        }

        private void BuildingLocation_textBox_TextChanged(object sender, EventArgs e)
        {
            if (BuildingLocation_textBox.Text != null) { BuildingLocation = BuildingLocation_textBox.Text.ToString(); }
            else { }
        }
        public void GrossArea_textBox_TextChanged(object sender, EventArgs e)
        {
            GrossArea = Program.UTIL.textBox_doubleComa(GrossArea_textBox, false, 2);
        }

        private void BuildingArea_textBox_TextChanged(object sender, EventArgs e)
        {
            BuildingArea = Program.UTIL.textBox_doubleComa(BuildingArea_textBox , false, 2);
        }
        private void AboveGround_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (AboveGround_comboBox.SelectedItem != null)
            {
                AboveGround = AboveGround_comboBox.SelectedItem.ToString();
            }
        }

        private void UnderGround_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (UnderGround_comboBox.SelectedItem != null)
            {
                UnderGround = UnderGround_comboBox.SelectedItem.ToString();
            }
        }
        private void ReviewerName_textBox_TextChanged(object sender, EventArgs e)
        {
            if (ReviewerName_textBox.Text != null) { ReviewerName = ReviewerName_textBox.Text.ToString(); }
            else { }
        }

        private void ReviewerLocation_textBox_TextChanged(object sender, EventArgs e)
        {
            if (ReviewerLocation_textBox.Text != null) { ReviewerLocation = ReviewerLocation_textBox.Text.ToString(); }
            else { }
        }

        private void ReviewerCompany_textBox_TextChanged(object sender, EventArgs e)
        {
            if (ReviewerCompany_textBox.Text != null) { ReviewerCompany = ReviewerCompany_textBox.Text.ToString(); }
            else { }
        }

        private void ReviewYear_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ReviewYear_comboBox.SelectedItem != null && ReviewYear_comboBox.SelectedItem.ToString() != "")
            {
                ReviewYear = Convert.ToDouble(ReviewYear_comboBox.SelectedItem.ToString());
                Calc_ReviewDate();
            }
        }

        private void ReviewMonth_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ReviewMonth_comboBox.SelectedItem != null && ReviewMonth_comboBox.SelectedItem.ToString() != "")
            {
                ReviewMonth = Convert.ToDouble(ReviewMonth_comboBox.SelectedItem.ToString());
                Calc_ReviewDate();
            }
        }
        private void Calc_ReviewDate()
        {
            if (Convert.ToDouble(ReviewMonth) < 10)
            {
                ReviewDate = Convert.ToDouble((ReviewYear + ".0" + ReviewMonth));
            }
            else
            {
                ReviewDate = Convert.ToDouble((ReviewYear + "." + ReviewMonth));
            }
        }

        private void BlowDoor_button_Click(object sender, EventArgs e)
        {
            BlowDoorTest form = new BlowDoorTest();
            DialogResult result = form.ShowDialog();
            if (result == DialogResult.OK)
            {
                n50 = form.n50;
                n50_textBox.Text = form.n50.ToString("0.0");
            }
        }

        private void Save_button_Click(object sender, EventArgs e)
        {
            if (BuildingName == null)
            {
                MessageBox.Show("건물명을 입력하세요.");
            }
            else if (BuildingLocation == null)
            {
                MessageBox.Show("주소를 입력하세요.");
            }
            else if (GrossArea == 0)
            {
                MessageBox.Show("연면적을 입력하세요.");
            }
            else if (BuildingArea == 0)
            {
                MessageBox.Show("건축면적을 입력하세요.");
            }
            else if (ReviewerName == null)
            {
                MessageBox.Show("작성자 이름을 입력하세요.");
            }
            else if (ReviewerName == null)
            {
                MessageBox.Show("작성자 이름을 입력하세요.");
            }
            else { Save(); }
        }

        private void Save()
        {
            string ProjectTypeNum = null;
            switch (ProjectType)
            {
                case "기존":
                    ProjectTypeNum = "1";
                    break;
                case "리트로핏":
                    ProjectTypeNum = "2";
                    break;
                case "리모델링":
                    ProjectTypeNum = "3";
                    break;
                case "신규":
                    ProjectTypeNum = "4";
                    break;
            }

            string[][] 번호 = Program.DB.querySQL(DB.type.ProjListDB, "Select pnum from projects where current = '1'");
            Program.DB.setValue(DB.type.ProjDB, "BuildingGeneral", "프로젝트번호,프로젝트명,프로젝트유형,프로젝트유형번호,기존프로젝트," +
                "건물대상,건물용도,건물명,주소,지역인덱스,지역,지역구분," +
                "준공연도,준공월," +
                "준공시기,법규시기," +
                "연면적,건축면적," +
                "지상층수,지하층수," +
                "작성자,작성자주소,작성자회사,작성연도,작성월,작성시기",
            "'" + 번호[0][0] + "','" + ProjectName + "','" + ProjectType + "','" + ProjectTypeNum + "','" + OldProject + "','" +
            BuildingCategory + "','" + BuildingUse + "','" + BuildingName + "','" + BuildingLocation + "','" + Climate_comboBox.SelectedItem.ToString() + "','" + Climate + "','" + BylawClimate + "','" +
            Year + "','" + Month + "','" +
            ConstrucitonDate.ToString() + "','" + BylawDate.ToString() + "','" +
            GrossArea.ToString() + "','" + BuildingArea.ToString() + "','" +
            AboveGround + "','" + UnderGround + "','" +
            ReviewerName + "','" + ReviewerLocation + "','" + ReviewerCompany + "','" + ReviewYear + "','" + ReviewMonth + "','" +
            ReviewDate.ToString()
                 + "'", "프로젝트번호");

            Program.DB.setValue(DB.type.ProjDB, "BuildingGeneral", "프로젝트번호,기밀측정여부,n50," +
               "출입문기밀여부," +
               "창호기밀여부," +
               "배선기밀여부," +
               "배관기밀여부",
                "'" + 번호[0][0] + "','" + BlowDoorTest + "','" + n50.ToString() + "','" +
                Door_Infil + "','" +
                Win_Infil + "','" +
                ElecWiring_Infil + "','" +
                Pipe_Infil + "'", "프로젝트번호");

            MessageBox.Show("저장되었습니다.");
        }

        private void reset()
        {
            ProjectName = null;
            ProjectName_textBox.Text = null;

            ProjectType = null;
            ProjectType_textBox.Text = null;

            ProjectNum = null;
            ProjectNum_textBox.Text = null;

            BuildingName = null;
            BuildingName_textBox.Text = null;

            BuildingLocation = null;
            BuildingLocation_textBox.Text = null;

            GrossArea = 0;
            GrossArea_textBox.Text = null;
            BuildingArea = 0;
            BuildingArea_textBox.Text = null;

            AboveGround_comboBox.SelectedIndex = 0;

            UnderGround_comboBox.SelectedIndex = 0;

            ReviewerName = null;
            ReviewerName_textBox.Text = null;

            ReviewerLocation = null;
            ReviewerLocation_textBox.Text = null;

            ReviewerCompany = null;
            ReviewerCompany_textBox.Text = null;


        }
        public void LoadData(String ID)            // 리스트에서 항목 더블 클릭시 - 뷰를 ID 의 getValue 값으로 채우기
        {
            reset();

            string[][] Value1 = Program.DB.querySQL(DB.type.ProjListDB, "Select type, title from projects where current = '1'");
            String[][] Value = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "프로젝트명,프로젝트유형,프로젝트번호,건물진단실시," +
            "건물대상,건물용도,건물명,주소,지역인덱스,지역,지역구분," +
            "외벽구조유형,지붕구조유형,준공연도,준공월," +
            "준공시기,법규시기," +
            "연면적,건축면적," +
            "지상층수,지하층수," +
            "작성자,작성자주소,작성자회사,작성연도,작성월,작성시기,기존프로젝트", "");
            if (Value1.Length > 0)
            {
                ProjectName = Value1[0][1];
                ProjectName_textBox.Text = ProjectName.ToString();

                switch (Value1[0][0])
                {
                    case "1":
                        ProjectType = "기존";
                        break;
                    case "2":
                        ProjectType = "리트로핏";
                        break;
                    case "3":
                        ProjectType = "리모델링";
                        break;
                    case "4":
                        ProjectType = "신규";
                        break;

                }
                ProjectType_textBox.Text = ProjectType.ToString();

            }
            if (Value.Length > 0)
            {
                ProjectNum = Value[0][2];
                ProjectNum_textBox.Text = ProjectNum.ToString();

                LoadOption_BuildingCategory();
                if (Value[0][4] != "")
                {
                    BuildingCategory_comboBox.SelectedItem = Value[0][4];
                }

                if (Value[0][5] != "")
                {
                    BuildingUse_comboBox.SelectedItem = Value[0][5];
                }

                BuildingName = Value[0][6];
                BuildingName_textBox.Text = BuildingName;

                BuildingLocation = Value[0][7];
                BuildingLocation_textBox.Text = BuildingLocation;

                Climate_comboBox.SelectedItem = Value[0][8];
                Climate = Value[0][9];
                BylawClimate = Value[0][10];
                ByRawClimate_textBox.Text = BylawClimate;
                if (Value[0][13] != "")
                {
                    Year = Convert.ToDouble(Value[0][13]);
                    Year_comboBox.SelectedItem = Year.ToString();
                }
                if (Value[0][14] != "")
                {
                    Month = Convert.ToDouble(Value[0][14]);
                    Month_comboBox.SelectedItem = Month.ToString();
                }

                if (Value[0][15] != "")
                {
                    ConstrucitonDate = Convert.ToDouble(Value[0][15]);
                    Calc_LawDate();
                }

                if (Value[0][17] != "")
                {
                    GrossArea = Convert.ToDouble(Value[0][17]);
                    GrossArea_textBox.Text = GrossArea.ToString();
                    Program.UTIL.textBox_doubleComa(GrossArea_textBox, true, 2);
                }

                if (Value[0][18] != "")
                {
                    BuildingArea = Convert.ToDouble(Value[0][18]);
                    BuildingArea_textBox.Text = BuildingArea.ToString();
                    Program.UTIL.textBox_doubleComa(BuildingArea_textBox, true, 2);
                }

                AboveGround = Value[0][19];
                AboveGround_comboBox.SelectedItem = AboveGround;

                UnderGround = Value[0][20];
                UnderGround_comboBox.SelectedItem = UnderGround;

                ReviewerName = Value[0][21];
                ReviewerName_textBox.Text = ReviewerName;

                ReviewerLocation = Value[0][22];
                ReviewerLocation_textBox.Text = ReviewerLocation;

                ReviewerCompany = Value[0][23];
                ReviewerCompany_textBox.Text = ReviewerCompany;
                if (Value[0][24] != "")
                {
                    ReviewYear = Convert.ToDouble(Value[0][24]);
                    ReviewYear_comboBox.SelectedItem = ReviewYear.ToString();
                }

                if (Value[0][25] != "")
                {
                    ReviewMonth = Convert.ToDouble(Value[0][25]);
                    ReviewMonth_comboBox.SelectedItem = ReviewMonth.ToString();
                }

                if (Value[0][26] != "")
                {
                    ReviewDate = Convert.ToDouble(Value[0][26]);
                    Calc_ReviewDate();
                }
                OldProject = Value[0][27];
                OldProject_textBox.Text = OldProject;
            }


            Value = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "기밀측정여부," +
                "출입문기밀여부," +
                "창호기밀여부," +
                "배선기밀여부," +
                "배관기밀여부", "");
            if (Value.Length > 0)
            {
                if (Value[0][0] != "")
                {
                    BlowDoorTest = Value[0][0];
                }
                else
                {
                    BlowDoorTest = "기밀 테스트 미실시";
                }
                BlowDoorTest_comboBox.SelectedItem = Value[0][0];
                Change_BlowDoorTest();

                if (Value[0][1] != null && Value[0][1] != "")
                {
                    if (Convert.ToBoolean(Value[0][1]))
                    {
                        Door_True_radioButton.Checked = true;
                    }
                    else
                    {
                        Door_False_radioButton.Checked = true;
                    }
                }

                if (Value[0][2] != null && Value[0][2] != "")
                {
                    if (Convert.ToBoolean(Value[0][2]))
                    {
                        Win_True_radioButton.Checked = true;
                    }
                    else
                    {
                        Win_False_radioButton.Checked = true;
                    }
                }
                if (Value[0][3] != null && Value[0][3] != "")
                {
                    if (Convert.ToBoolean(Value[0][3]))
                    {
                        ElecWiring_True_radioButton.Checked = true;
                    }
                    else
                    {
                        ElecWiring_False_radioButton.Checked = true;
                    }
                }
                if (Value[0][4] != null && Value[0][4] != "")
                {
                    if (Convert.ToBoolean(Value[0][4]))
                    {
                        Pipe_True_radioButton.Checked = true;
                    }
                    else
                    {
                        Pipe_False_radioButton.Checked = true;
                    }
                }
            }

            Value = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "n50", "");

            if (Value.Length > 0 && Value[0][0] != "")
            {
                n50 = Convert.ToDouble(Value[0][0]);
                n50_textBox.Text = Convert.ToDouble(Value[0][0]).ToString("0.0");
            }
        }

    }
}
