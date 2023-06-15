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
        public String[] Select_WindowSpacer = new String[10];
        String UserNum, UserDBName, UserDB_Manufacture, UserDBType1, UserDBType2;
        Double UserDB_Psi_fix, UserDB_Psi_open;
        int Num;


        public CW_PanelDB()
        {
            InitializeComponent();
            load_table_SpacerDB();     
            UserNum = Program.UTIL.CreateNum("User_CWSpacer", "번호", "UCS_0");
            UserNum_textBox.Text = UserNum;
        }
        void load_table_SpacerDB()
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
            table_CWPanel.Columns.Add("투습저항계수" + Environment.NewLine + "dry", typeof(string));
            table_CWPanel.Columns.Add("투습저항계수" + Environment.NewLine + "wet", typeof(string));
            table_CWPanel.Columns.Add("비열" + Environment.NewLine + "с\r\n[kJ/(kg·K)]", typeof(string));

            //try
            //{
            //    string[][] User_CWSpacer = Program.DB.getValue(DB.type.ProjDB, "User_CWSpacer", "번호,DB유형,제품명,제조사,구분1,구분3,고정유리_CL_선형열관류율,개폐유리_CL_선형열관류율,고정유리_LE_선형열관류율,개폐유리_LE_선형열관류율", "구분3 ='" + FrameTpe + "'");
            //    for (int n = 0; n < User_CWSpacer.Length; n++)
            //    {
            //        table_CWPanel.Rows.Add(User_CWSpacer[n][0], User_CWSpacer[n][1], User_CWSpacer[n][2], User_CWSpacer[n][3], User_CWSpacer[n][4], User_CWSpacer[n][5], User_CWSpacer[n][6], User_CWSpacer[n][7], User_CWSpacer[n][8], User_CWSpacer[n][9]);
            //    }
            //}
            //catch { }

            string[][] CWPanel = Program.DB.getValue(DB.type.BaseDB, "열전도율", "비고,재료명,종류,초기열전도율,ISO10456기준열전도율,밀도,투습저항계수dry,투습저항계수wet,비열", "구분 = '단열재'");
            String dbnum;
            for (int n = 0; n < CWPanel.Length; n++)
            {
                if(n+1<10)
                {
                    dbnum = "Ins_00"+(n+1).ToString();
                }
                else
                {
                    dbnum = "Ins_0" + (n + 1).ToString();
                }
                table_CWPanel.Rows.Add(dbnum, CWPanel[n][0], CWPanel[n][1], CWPanel[n][2], CWPanel[n][3], CWPanel[n][4], CWPanel[n][5], CWPanel[n][6], CWPanel[n][7], CWPanel[n][8]);
            }
            Panel_dataGridView.DataSource = table_CWPanel;
            Count_FrameDB = CWPanel.Length;
        }


        private void UserDBName_textBox_TextChanged(object sender, EventArgs e)
        {
            UserDBName = UserDBName_textBox.Text;
        }

        private void UserDB_Manufacture_textBox_TextChanged(object sender, EventArgs e)
        {
            UserDB_Manufacture = UserDB_Manufacture_textBox.Text;
        }
        private void UserDBType1_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UserDBType1 = UserDBType1_comboBox.SelectedItem.ToString();
        }


        private void UserDBType2_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UserDBType2 = UserDBType2_comboBox.SelectedItem.ToString();

        }

        private void UserDB_Psi_fix_textBox_TextChanged(object sender, EventArgs e)
        {
            UserDB_Psi_fix = Convert.ToDouble(UserDB_Psi_fix_textBox.Text);
        }

        private void UserDB_Psi_open_textBox_TextChanged(object sender, EventArgs e)
        {
            UserDB_Psi_open = Convert.ToDouble(UserDB_Psi_open_textBox.Text);
        }

        private void AddUserDB_button_Click(object sender, EventArgs e)
        {

            if (UserDBName != null && UserDBType1 != null && UserDB_Psi_fix != 0 && UserDB_Psi_open != 0)
            {
                Program.DB.setValue(DB.type.ProjDB, "User_CWSpacer", "번호,DB유형,제품명,제조사,구분1,구분3,고정유리_CL_선형열관류율,개폐유리_CL_선형열관류율,고정유리_LE_선형열관류율,개폐유리_LE_선형열관류율",
                    "'" + UserNum + "','" + "사용자" + "','" + UserDBName + "','" + UserDB_Manufacture + "','" + UserDBType1 + "','" + UserDBType2 + "','" + UserDB_Psi_fix.ToString() + "','" + UserDB_Psi_open.ToString() + "','" + UserDB_Psi_fix.ToString() + "','" + UserDB_Psi_open.ToString() + "'", "번호");
                load_table_SpacerDB();
            }
            else
            {
                MessageBox.Show("모든 값을 입력해주세요.");
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
                        Program.DB.deleteValue(DB.type.ProjDB, "User_CWSpacer", "번호 ='" + Delete_Num + "'");
                        load_table_SpacerDB();
                    }
                }
                else
                {
                    MessageBox.Show("기본 DB는 삭제할 수 없습니다.");
                }
            }
        }

        //데이터그리드뷰 체크박스 선택 시
        private void Spacer_dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
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
                Select_WindowSpacer[i] = row.Cells[i + 2].Value.ToString();
            }

            Select_WindowSpacer[0] = row.Cells[1].Value.ToString();
            Select_WindowSpacer[9] = LE_CL_V;

            this.DialogResult = DialogResult.OK;
            this.Close();

        }

    }
}
