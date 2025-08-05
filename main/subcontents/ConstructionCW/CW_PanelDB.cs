using main.info;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace main.subcontents.ConstructionCW
{
    public partial class CW_PanelDB : Form
    {
        double Count_FrameDB;
        int SelectRow;
        String LE_CL_V;
        public String[] Select_CWPanel = new String[10];
        String UserNum, UserDBName, UserDBType1, UserDBType2, UserDB_Note;
        Double UserDB_Conductivity, UserDB_Density, UserDB_dry, UserDB_wet, UserDB_c;
        int Num;
        List<String> List = new List<String>();


        public CW_PanelDB()
        {
            InitializeComponent(); this.Font = new Font(UTIL.Families[0], 9.75F, FontStyle.Regular);
            UserNum = Program.UTIL.CreateNum("User_Material", "번호", "UM_0");
            UserNum_textBox.Text = UserNum;

            string[][] Type2 = Program.DB.getValue(DB.type.BaseDB_HCneed, "열전도율", "종류2", "구분 = '단열재'");
            if (Type2.Length > 0)
            {
                List.Add(Type2[0][0]);
                for (int n = 1; n < Type2.Length; n++)
                {
                    if (Type2[n - 1][0] != Type2[n][0])
                    {
                        List.Add(Type2[n][0]);
                    }
                }
                string[] Array = List.ToArray();
                UserDB_Type1_comboBox.Items.Clear();
                UserDB_Type1_comboBox.Items.AddRange(Array);
                UserDB_Type1_comboBox.SelectedIndex = 0;
                load_table_PanelDB();
            }
        }
        void load_table_PanelDB()
        {
            new StackedHeaderDecorator(Panel_dataGridView, DataGridViewAutoSizeColumnsMode.Fill, datagridviewDesign);
            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
            Panel_dataGridView.Columns.Clear();
            checkBoxColumn.HeaderText = "선택";
            checkBoxColumn.Name = "check";
            Panel_dataGridView.Columns.Add(checkBoxColumn);

            Panel_dataGridView.Columns.Add("A1", "번호");
            Panel_dataGridView.Columns.Add("A2", "DB유형");
            Panel_dataGridView.Columns.Add("A3", "재료명");
            Panel_dataGridView.Columns.Add("A4", "종류1");
            Panel_dataGridView.Columns.Add("A5", "종류2");
            Panel_dataGridView.Columns.Add("A6", "열전도율");
            Panel_dataGridView.Columns.Add("A7", "밀도");
            Panel_dataGridView.Columns.Add("A8", "비열");
            Panel_dataGridView.Columns.Add("A9", "투습저항계수.dry");
            Panel_dataGridView.Columns.Add("A10", "투습저항계수.wet");
            Panel_dataGridView.Columns.Add("A11", "비고");

            string[][] User_CWPanel = Program.DB.getValue(DB.type.ProjDB, "User_Material", "번호,DB유형,재료명,종류2,종류1,열전도율,밀도,비열,투습저항계수dry,투습저항계수wet,비고", "구분 = '단열재'");
            if (User_CWPanel.Length > 0)
            {
                for (int n = 0; n < User_CWPanel.Length; n++)
                {
                    Panel_dataGridView.Rows.Add();
                    int nRow = Panel_dataGridView.Rows.Count - 1;
                    for (int k = 0; k < 11; k++)
                    {
                        Panel_dataGridView.Rows[nRow].Cells[k + 1].Value = User_CWPanel[n][k];
                    }
                }
            }

            string[][] CWPanel = Program.DB.getValue(DB.type.BaseDB_HCneed, "열전도율", "재료명,종류2,종류1,열전도율,밀도,비열,투습저항계수dry,투습저항계수wet,비고", "구분 = '단열재'");
            String dbnum;
            if (CWPanel.Length > 0)
            {
                for (int n = 0; n < CWPanel.Length; n++)
                {
                    if (n + 1 < 10)
                    {
                        dbnum = "M_00" + (n + 1).ToString();
                    }
                    else
                    {
                        dbnum = "M_0" + (n + 1).ToString();
                    }

                    Panel_dataGridView.Rows.Add();
                    int nRow = Panel_dataGridView.Rows.Count - 1;
                    Panel_dataGridView.Rows[nRow].Cells[1].Value = dbnum;
                    Panel_dataGridView.Rows[nRow].Cells[2].Value = "표준";
                    for (int k = 0; k < 9; k++)
                    {
                        Panel_dataGridView.Rows[nRow].Cells[k + 3].Value = CWPanel[n][k];
                    }
                }
            }
            Count_FrameDB = CWPanel.Length;
        }
        private Boolean datagridviewDesign(DataGridViewCell cell, int column, int row)
        {
            if (row % 2 == 1)
            {
                cell.Style.BackColor = Color.FromArgb(251, 251, 251);
                cell.Style.ForeColor = Color.Black;
                cell.Style.SelectionBackColor = Color.FromArgb(251, 251, 251);
                cell.Style.SelectionForeColor = Color.Black;
                return true;
            }
            else
            {
                cell.Style.BackColor = Color.FromArgb(255, 255, 255);
                cell.Style.ForeColor = Color.Black;
                cell.Style.SelectionBackColor = Color.FromArgb(255, 255, 255);
                cell.Style.SelectionForeColor = Color.Black;
                return true;
            }
        }


        private void UserDBName_textBox_TextChanged(object sender, EventArgs e)
        {
            UserDBName = UserDBName_textBox.Text;
        }

        private void UserDB_Type1_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UserDBType1 = UserDB_Type1_comboBox.SelectedItem.ToString();
        }

        private void UserDB_Type2_textBox_TextChanged(object sender, EventArgs e)
        {
            UserDBType2 = UserDB_Type2_textBox.Text;
        }
        private void UserDB_Conductivity_textBox_TextChanged(object sender, EventArgs e)
        {
            UserDB_Conductivity = Convert.ToDouble(UserDB_Conductivity_textBox.Text);
        }

        private void UserDB_Density_textBox_TextChanged(object sender, EventArgs e)
        {
            UserDB_Density = Convert.ToDouble(UserDB_Density_textBox.Text);
        }

        private void UserDB_c_textBox_TextChanged(object sender, EventArgs e)
        {
            UserDB_c = Convert.ToDouble(UserDB_c_textBox.Text);
        }

        private void UserDB_dry_textBox_TextChanged(object sender, EventArgs e)
        {
            UserDB_dry = Convert.ToDouble(UserDB_dry_textBox.Text);
        }

        private void UserDB_wet_textBox_TextChanged(object sender, EventArgs e)
        {
            UserDB_wet = Convert.ToDouble(UserDB_wet_textBox.Text);
        }


        private void UserDB_Note_textBox_TextChanged(object sender, EventArgs e)
        {
            UserDB_Note = UserDB_Note_textBox.Text;
        }
        private void AddUserDB_button_Click(object sender, EventArgs e)
        {
            string[][] 프로젝트유형 = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "프로젝트유형번호");
            if (UserDBName != null && UserDBType1 != null && UserDB_Conductivity != 0)
            {
                Program.DB.setValue(DB.type.ProjDB, "User_Material", "번호,프로젝트유형,DB유형,구분,재료명,종류2,종류1,열전도율,밀도,투습저항계수dry,투습저항계수wet,비열,비고",
                    "'" + UserNum + "','" + 프로젝트유형[0][0] + "','" + "사용자" + "','" + "단열재" + "','" + UserDBName + "','" + UserDBType1 + "','" + UserDBType2 + "','" + UserDB_Conductivity.ToString() + "','" + UserDB_Density.ToString() + "','" + UserDB_dry.ToString() + "','" + UserDB_wet.ToString() + "','" + UserDB_c.ToString() + "','" + UserDB_Note + "'", "번호");
                load_table_PanelDB();
                Program.DB.saveProject();
            }
            else
            {
                MessageBox.Show("재료명, 종류1, 열전도율은 필수 입력 항목 입니다.");
            }
        }

        private void Delete_button_Click(object sender, EventArgs e)
        {
            int k = Panel_dataGridView.CurrentCell.RowIndex;
            if (k > -1)
            {
                if (Panel_dataGridView.Rows[k].Cells[2].Value.ToString() == "사용자")
                {
                    if ((MessageBox.Show(Panel_dataGridView.Rows[k].Cells[3].Value.ToString() + "을 삭제하시겠습니까?", "삭제 확인", MessageBoxButtons.YesNo) == DialogResult.Yes))
                    {
                        String Delete_Num = Panel_dataGridView.Rows[k].Cells[1].Value.ToString();
                        Program.DB.deleteValue(DB.type.ProjDB, "User_Material", "번호 ='" + Delete_Num + "'");
                        load_table_PanelDB();
                    }
                }
                else
                {
                    MessageBox.Show("기본 DB는 삭제할 수 없습니다.");
                }
            }
        }

        //데이터그리드뷰 체크박스 선택 시
        private void Panel_dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                Panel_dataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
                SelectRow = e.RowIndex;
            }
        }

        private void Save_button_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = Panel_dataGridView.Rows[SelectRow];

            for (int i = 1; i < (row.Cells.Count - 2); i++)
            {
                Select_CWPanel[i] = row.Cells[i + 2].Value.ToString();
            }

            Select_CWPanel[0] = row.Cells[1].Value.ToString();

            this.DialogResult = DialogResult.OK;
            this.Close();

        }

        private void info_Click(object sender, EventArgs e)
        {

            string basePath = Program.gPath + "Manual\\2.subcontents\\8.CW\\2.PanelDB";

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
