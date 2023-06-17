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
            InitializeComponent();
            UserNum = Program.UTIL.CreateNum("User_Material", "번호", "UM_0");
            UserNum_textBox.Text = UserNum;

            string[][] Type2 = Program.DB.getValue(DB.type.BaseDB, "열전도율", "종류2", "구분 = '단열재'");
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
        void load_table_PanelDB()
        {
            DataTable table_CWPanel = new DataTable();
            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
            Panel_dataGridView.Columns.Clear();
            checkBoxColumn.HeaderText = "선택";
            checkBoxColumn.Name = "check";
            Panel_dataGridView.Columns.Add(checkBoxColumn);
            table_CWPanel.Columns.Add("번호", typeof(string));
            table_CWPanel.Columns.Add("DB유형", typeof(string));
            table_CWPanel.Columns.Add("재료명", typeof(string));
            table_CWPanel.Columns.Add("종류1", typeof(string));
            table_CWPanel.Columns.Add("종류2", typeof(string));
            table_CWPanel.Columns.Add("열전도율" + Environment.NewLine + "λ\r\n[W/m·K]", typeof(string));
            table_CWPanel.Columns.Add("밀도" + Environment.NewLine + "ρ\r\n[kg/m³]", typeof(string));
            table_CWPanel.Columns.Add("비열" + Environment.NewLine + "с\r\n[kJ/kg·K]", typeof(string));
            table_CWPanel.Columns.Add("투습저항계수" + Environment.NewLine + "dry", typeof(string));
            table_CWPanel.Columns.Add("투습저항계수" + Environment.NewLine + "wet", typeof(string));
            table_CWPanel.Columns.Add("비고", typeof(string));

            try
            {
                string[][] User_CWPanel = Program.DB.getValue(DB.type.ProjDB, "User_Material", "번호,DB유형,재료명,종류2,종류1,열전도율,밀도,비열,투습저항계수dry,투습저항계수wet,비고", "구분 = '단열재'");
                for (int n = 0; n < User_CWPanel.Length; n++)
                {
                    table_CWPanel.Rows.Add(User_CWPanel[n][0], User_CWPanel[n][1], User_CWPanel[n][2], User_CWPanel[n][3], User_CWPanel[n][4], User_CWPanel[n][5], User_CWPanel[n][6], User_CWPanel[n][7], User_CWPanel[n][8], User_CWPanel[n][9], User_CWPanel[n][10]);
                }
            }
            catch { }

            string[][] CWPanel = Program.DB.getValue(DB.type.BaseDB, "열전도율", "재료명,종류2,종류1,열전도율,밀도,비열,투습저항계수dry,투습저항계수wet,비고", "구분 = '단열재'");
            String dbnum;
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
                table_CWPanel.Rows.Add(dbnum, "표준", CWPanel[n][0], CWPanel[n][1], CWPanel[n][2], CWPanel[n][3], CWPanel[n][4], CWPanel[n][5], CWPanel[n][6], CWPanel[n][7], CWPanel[n][8]);
            }
            Panel_dataGridView.DataSource = table_CWPanel;
            Count_FrameDB = CWPanel.Length;
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

            if (UserDBName != null && UserDBType1 != null && UserDB_Conductivity != 0)
            {
                Program.DB.setValue(DB.type.ProjDB, "User_Material", "번호,DB유형,구분,재료명,종류2,종류1,열전도율,밀도,투습저항계수dry,투습저항계수wet,비열,비고",
                    "'" + UserNum + "','" + "사용자" + "','" + "단열재" + "','" + UserDBName + "','" + UserDBType1 + "','" + UserDBType2 + "','" + UserDB_Conductivity.ToString() + "','" + UserDB_Density.ToString() + "','" + UserDB_dry.ToString() + "','" + UserDB_wet.ToString() + "','" + UserDB_c.ToString() + "','" + UserDB_Note + "'", "번호");
                load_table_PanelDB();
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
                DataGridViewRow row = Panel_dataGridView.Rows[SelectRow];
                DataGridViewRow row2;
                for (int k = 0; k < Count_FrameDB; k++)
                {
                    if (k != row.Index)
                    {
                        Panel_dataGridView.Rows[k].Cells[0].Value = false;
                        row2 = Panel_dataGridView.Rows[k];
                        row2.DefaultCellStyle.BackColor = Color.White;
                        row2.DefaultCellStyle.ForeColor = Color.Black;
                    }
                    else
                    {
                        row.DefaultCellStyle.BackColor = SystemColors.GradientInactiveCaption;
                        row.DefaultCellStyle.ForeColor = Color.Black;
                        row = Panel_dataGridView.Rows[e.RowIndex];
                    }
                }
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

    }
}
