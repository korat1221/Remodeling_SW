using main.contentslist;
using System;
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

namespace main.contents
{
    public partial class General : Form
    {
        String ProjectName, ProjectType, Target, Diagnosis;
        String BuildingCategory, BuildingUse, BuildingName, BuildingLocation, Climate, BylawClimate;
        String WallType, RoofType, Year, Month;
        double ConstrucitonDate, BylawDate;
        double GrossArea, BuildingArea;
        String AboveGround, UnderGround;
        String ReviewerName, ReviewerLocation, ReviewerCompany, ReviewYear, ReviewMonth;
        double ReviewDate;

        public General()
        {
            InitializeComponent();
            string[][] Image = Program.DB.getValue(DB.type.BaseDB_HCneed, "메뉴아이콘", "하위메뉴아이콘", "하위메뉴명 = '일반정보'");
            Icon_pictureBox.Load(Program.gPath + Image[0][0]);
            Icon_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            //프로젝트명
            ProjectName = "화곡 119 안전센터 검토";
            if (ProjectName != null) { ProjectName_textBox.Text = ProjectName.ToString(); }
            else { }

            //프로젝트유형
            ProjectType = "리트로핏";
            if (ProjectType != null) { ProjectType_textBox.Text = ProjectType.ToString(); }
            else { }


            //사업성능목표 콤보박스
            Program.UTIL.FillComboBox(DB.type.BaseDB_HCneed, Target_comboBox, "건물", "사업 성능 목표", "1");
            //건물진단실시 콤보박스
            Program.UTIL.FillComboBox(DB.type.BaseDB_HCneed, Diagnosis_comboBox, "건물", "건물 진단 실시", "1");
            //건물대상 콤보박스
            Program.UTIL.FillComboBox_Parents(BuildingCategory_comboBox, "존일반", "건물용도", "1");
            //기후데이터 콤보박스
            Program.UTIL.FillComboBox(DB.type.BaseDB_HCneed, Climate_comboBox, "건물", "지역", "18");
            //외벽 구조유형 콤보박스
            Program.UTIL.FillComboBox(DB.type.BaseDB_HCneed, WallType_comboBox, "건물", "외벽 구조유형", "1");
            //지붕 구조유형 콤보박스
            Program.UTIL.FillComboBox(DB.type.BaseDB_HCneed, RoofType_comboBox, "건물", "지붕 구조유형", "1");


            //준공연월 연 콤보박스
            Year_comboBox.Items.Clear();
            for (int i = 0; i < 100; i++)
            {
                Year_comboBox.Items.Add((2040 - i).ToString());
            }
            Year_comboBox.SelectedItem = (2023).ToString();
            //준공연월 월 콤보박스
            Month_comboBox.Items.Clear();
            for (int i = 0; i < 12; i++)
            {
                Month_comboBox.Items.Add((i + 1).ToString());
            }
            Month_comboBox.SelectedIndex = 0;

            //작성시기 연 콤보박스
            ReviewYear_comboBox.Items.Clear();
            for (int i = 0; i < 10; i++)
            {
                ReviewYear_comboBox.Items.Add((2030 - i).ToString());
            }
            ReviewYear_comboBox.SelectedItem = (2023).ToString();
            //작성시기 월 콤보박스
            ReviewMonth_comboBox.Items.Clear();
            for (int i = 0; i < 12; i++)
            {
                ReviewMonth_comboBox.Items.Add((i + 1).ToString());
            }
            ReviewMonth_comboBox.SelectedIndex = 0;

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

        }

        private void GeneralPanel_Paint(object sender, PaintEventArgs e)
        {
            Panel p = (Panel)sender;
            ControlPaint.DrawBorder(e.Graphics, p.DisplayRectangle, System.Drawing.Color.FromArgb(153, 180, 209), ButtonBorderStyle.Solid);
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            Panel p = (Panel)sender;
            ControlPaint.DrawBorder(e.Graphics, p.DisplayRectangle, System.Drawing.Color.FromArgb(153, 180, 209), ButtonBorderStyle.Solid);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Panel p = (Panel)sender;
            ControlPaint.DrawBorder(e.Graphics, p.DisplayRectangle, System.Drawing.Color.FromArgb(153, 180, 209), ButtonBorderStyle.Solid);
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {
            Panel p = (Panel)sender;
            ControlPaint.DrawBorder(e.Graphics, p.DisplayRectangle, System.Drawing.Color.FromArgb(153, 180, 209), ButtonBorderStyle.Solid);
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {
            Panel p = (Panel)sender;
            ControlPaint.DrawBorder(e.Graphics, p.DisplayRectangle, System.Drawing.Color.FromArgb(153, 180, 209), ButtonBorderStyle.Solid);
        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {
            Panel p = (Panel)sender;
            ControlPaint.DrawBorder(e.Graphics, p.DisplayRectangle, System.Drawing.Color.FromArgb(153, 180, 209), ButtonBorderStyle.Solid);
        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {
            Panel p = (Panel)sender;
            ControlPaint.DrawBorder(e.Graphics, p.DisplayRectangle, System.Drawing.Color.FromArgb(153, 180, 209), ButtonBorderStyle.Solid);
        }

        private void Target_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Target_comboBox.SelectedItem != null)
            {
                Target = Target_comboBox.SelectedItem.ToString();
            }
        }
        private void Diagnosis_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Diagnosis_comboBox.SelectedItem != null)
            {
                Diagnosis = Diagnosis_comboBox.SelectedItem.ToString();
            }
        }

        private void BuildingCategory_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (BuildingCategory_comboBox.SelectedItem != null)
            {
                BuildingCategory = Program.UTIL.SelectedItem_ByComboBox(BuildingCategory_comboBox);
                Program.UTIL.FillComboBox_ByComboBox(BuildingUse_comboBox, BuildingCategory_comboBox, "1");
            }
        }

        private void BuildingUse_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (BuildingUse_comboBox.SelectedItem != null)
            {
                BuildingUse = Program.UTIL.SelectedItem_ByComboBox(BuildingUse_comboBox);
            }
        }

        private void Climate_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Climate_comboBox.SelectedItem != null)
            {
                string[][] Value = Program.DB.getValue(DB.type.BaseDB_HCneed, "기후인덱스", "지역명,지역구분", "지역인덱스 = '" + Climate_comboBox.SelectedItem.ToString() + "'");
                Climate = Value[0][0];
                BylawClimate = Value[0][1];
                ByRawClimate_textBox.Text = BylawClimate;
            }
        }

        private void WallType_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (WallType_comboBox.SelectedItem != null)
            {
                WallType = WallType_comboBox.SelectedItem.ToString();
            }
        }

        private void RoofType_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (RoofType_comboBox.SelectedItem != null)
            {
                RoofType = RoofType_comboBox.SelectedItem.ToString();
            }

        }

        private void Year_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Year_comboBox.SelectedItem != null)
            {
                Year = Year_comboBox.SelectedItem.ToString();
                Calc_LawDate();

                if (BylawDate == 2013.1)
                {
                    BylawDate_textBox.Text = "2013.10";
                }
                else { BylawDate_textBox.Text = Convert.ToString(BylawDate); }
            }
        }

        private void Month_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Month_comboBox.SelectedItem != null)
            {
                Month = Month_comboBox.SelectedItem.ToString();
                Calc_LawDate();

                if (BylawDate == 2013.1)
                {
                    BylawDate_textBox.Text = "2013.10";
                }
                else { BylawDate_textBox.Text = Convert.ToString(BylawDate); }
            }
        }

        private void Calc_LawDate()
        {
            if (Convert.ToDouble(Month) < 10)
            {
                ConstrucitonDate = Convert.ToDouble((Year + ".0" + Month));
            }
            else
            {
                ConstrucitonDate = Convert.ToDouble((Year + "." + Month));
            }
            string[][] Value = Program.DB.getValue_dedupe(DB.type.BaseDB_HCneed, "법규열관류율", "시기", "");

            for (int i = 0; i < Value.Length; i++)
            {
                if (Convert.ToDouble(Value[i][0]) < ConstrucitonDate)
                {
                    BylawDate = Convert.ToDouble(Value[i][0]);
                }
                else { }
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
        private void GrossArea_textBox_TextChanged(object sender, EventArgs e)
        {
            if (GrossArea_textBox.Text != null) { try { GrossArea = Convert.ToDouble(GrossArea_textBox.Text); } catch { } }
            else { }
        }

        private void BuildingArea_textBox_TextChanged(object sender, EventArgs e)
        {
            if (BuildingArea_textBox.Text != null) { try { BuildingArea = Convert.ToDouble(BuildingArea_textBox.Text); } catch { } }
            else { }
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
            if (ReviewYear_comboBox.SelectedItem != null)
            {
                ReviewYear = ReviewYear_comboBox.SelectedItem.ToString();
                Calc_ReviewDate();
            }
        }

        private void ReviewMonth_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ReviewMonth_comboBox.SelectedItem != null)
            {
                ReviewMonth = ReviewMonth_comboBox.SelectedItem.ToString();
                Calc_ReviewDate();
            }
        }
        private void Calc_ReviewDate()
        {
            if (Convert.ToDouble(Month) < 10)
            {
                ReviewDate = Convert.ToDouble((ReviewYear + ".0" + ReviewMonth));
            }
            else
            {
                ReviewDate = Convert.ToDouble((ReviewYear + "." + ReviewMonth));
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
            Program.DB.setValue(DB.type.ProjDB, "BuildingGeneral", "프로젝트명,프로젝트유형,사업성능목표,건물진단실시," +
                "건물대상,건물용도,건물명,주소,지역인덱스,지역,지역구분," +
                "외벽구조유형,지붕구조유형,준공연도,준공월," +
                "준공시기,법규시기," +
                "연면적,건축면적," +
                "지상층수,지하층수," +
                "작성자,작성자주소,작성자회사,작성연도,작성월,작성시기",
            "'" + ProjectName + "','" + ProjectType + "','" + Target + "','" + Diagnosis + "','" +
            BuildingCategory + "','" + BuildingUse + "','" + BuildingName + "','" + BuildingLocation + "','" + Climate_comboBox.SelectedItem.ToString() + "','" + Climate + "','" + BylawClimate + "','" +
            WallType + "','" + RoofType + "','" + Year + "','" + Month + "','" +
            ConstrucitonDate.ToString() + "','" + BylawDate.ToString() + "','" +
            GrossArea.ToString() + "','" + BuildingArea.ToString() + "','" +
            AboveGround + "','" + UnderGround + "','" +
            ReviewerName + "','" + ReviewerLocation + "','" + ReviewerCompany + "','" + ReviewYear + "','" + ReviewMonth + "','" +
            ReviewDate.ToString()
                 + "'", "프로젝트명");

            MessageBox.Show("저장되었습니다.");
        }

        private void reset()
        {
        }
        public void LoadData(String ID)            // 리스트에서 항목 더블 클릭시 - 뷰를 ID 의 getValue 값으로 채우기
        {

        }

        public void ResetForm(String ID) // 리스트에서 추가 버튼 클릭시 - 뷰 초기화
        {
        }

    }
}
