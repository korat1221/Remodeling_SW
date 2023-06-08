using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace main.subcontents
{
    public partial class Window_SpacerDB : Form
    {
        double Count_FrameDB;
        int SelectRow;
        String LE_CL_V;
        public String[] Select_WindowSpacer = new String[10];
        String UserNum, UserDBName, UserDBType1, UserDBType2, UserDBType3;
        Double UserDB_Psi_fix, UserDB_Psi_open;
        int Num;


        public Window_SpacerDB(String SingleDoubleType, String FrameMaterial, String LE_CL_V)
        {
            InitializeComponent();
            load_table_SpacerDB(SingleDoubleType, FrameMaterial);
            this.LE_CL_V = LE_CL_V;
            //사용자DB 구분1 콤보박스
            UserDBType1_comboBox.Items.Add("일반간봉");
            UserDBType1_comboBox.Items.Add(" 단열간봉");
            //사용자DB 구분2 콤보박스
            UserDBType2_comboBox.Items.Add("단창");
            UserDBType2_comboBox.Items.Add("이중창");
            UserDBType2_comboBox.SelectedItem = SingleDoubleType;
            UserDBType2_comboBox.Enabled = false;
            //사용자DB 구분3 콤보박스
            UserDBType3_comboBox.Items.Add("플라스틱");
            UserDBType3_comboBox.Items.Add("금속");
            UserDBType3_comboBox.Items.Add("금속_단열바");
            UserDBType3_comboBox.SelectedItem = FrameMaterial;
            UserDBType3_comboBox.Enabled = false;
            UserNum = Program.UTIL.CreateNum("User_WindowSpacer", "번호", "UGS_0");
            UserNum_textBox.Text = UserNum;
        }
        void load_table_SpacerDB(String SingleDoubleType, String FrameMaterial)
        {
            DataTable table_WindowSpacer = new DataTable();
            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
            Spacer_dataGridView.Columns.Clear();
            checkBoxColumn.HeaderText = "선택";
            checkBoxColumn.Name = "check";
            Spacer_dataGridView.Columns.Add(checkBoxColumn);
            table_WindowSpacer.Columns.Add("번호", typeof(string));
            table_WindowSpacer.Columns.Add("DB유형", typeof(string));
            table_WindowSpacer.Columns.Add("제품명", typeof(string));
            table_WindowSpacer.Columns.Add("구분1", typeof(string));
            table_WindowSpacer.Columns.Add("구분2", typeof(string));
            table_WindowSpacer.Columns.Add("구분3", typeof(string));
            table_WindowSpacer.Columns.Add("고정유리(CL)\r\n선형열관류율" + Environment.NewLine + "Ψg,fix\r\n[W/m·K]", typeof(string));
            table_WindowSpacer.Columns.Add("개폐유리(CL)\r\n선형열관류율" + Environment.NewLine + "Ψg,t\r\n[W/m·K]", typeof(string));
            table_WindowSpacer.Columns.Add("고정유리(LE)\r\n선형열관류율" + Environment.NewLine + "Ψg,fix\r\n[W/m·K]", typeof(string));
            table_WindowSpacer.Columns.Add("개폐유리(LE)\r\n선형열관류율" + Environment.NewLine + "Ψg,t\r\n[W/m·K]", typeof(string));

            try
            {
                string[][] User_WinSpacer = Program.DB.getValue(DB.type.ProjDB, "User_WindowSpacer", "번호,DB유형,제품명,구분1,구분2,구분3,고정유리_CL_선형열관류율,개폐유리_CL_선형열관류율,고정유리_LE_선형열관류율,개폐유리_LE_선형열관류율", "구분2 = '" + SingleDoubleType + "'AND 구분3 ='" + FrameMaterial + "'");
                for (int n = 0; n < User_WinSpacer.Length; n++)
                {
                    table_WindowSpacer.Rows.Add(User_WinSpacer[n][0], User_WinSpacer[n][1], User_WinSpacer[n][2], User_WinSpacer[n][3], User_WinSpacer[n][4], User_WinSpacer[n][5], User_WinSpacer[n][6], User_WinSpacer[n][7], User_WinSpacer[n][8], User_WinSpacer[n][9]);
                }
            }
            catch { }

            string[][] WinSpacer = Program.DB.getValue(DB.type.BaseDB, "창호간봉", "번호,DB유형,제품명,구분1,구분2,구분3,고정유리_CL_선형열관류율,개폐유리_CL_선형열관류율,고정유리_LE_선형열관류율,개폐유리_LE_선형열관류율", "구분2 = '" + SingleDoubleType + "'AND 구분3 ='" + FrameMaterial + "'");
            for (int n = 0; n < WinSpacer.Length; n++)
            {
                table_WindowSpacer.Rows.Add(WinSpacer[n][0], WinSpacer[n][1], WinSpacer[n][2], WinSpacer[n][3], WinSpacer[n][4], WinSpacer[n][5], WinSpacer[n][6], WinSpacer[n][7], WinSpacer[n][8], WinSpacer[n][9]);
            }
            Spacer_dataGridView.DataSource = table_WindowSpacer;
            Count_FrameDB = WinSpacer.Length;
        }


        private void UserDBName_textBox_TextChanged(object sender, EventArgs e)
        {
            UserDBName = UserDBName_textBox.Text;
        }

        private void UserDBType1_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UserDBType1 = UserDBType1_comboBox.SelectedItem.ToString();
        }

        private void UserDBType2_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UserDBType2 = UserDBType2_comboBox.SelectedItem.ToString();
        }

        private void UserDBType3_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UserDBType3 = UserDBType3_comboBox.SelectedItem.ToString();

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
                Program.DB.setValue(DB.type.ProjDB, "User_WindowSpacer", "번호,DB유형,제품명,구분1,구분2,구분3,고정유리_CL_선형열관류율,개폐유리_CL_선형열관류율,고정유리_LE_선형열관류율,개폐유리_LE_선형열관류율",
                    "'" + UserNum + "','" + "사용자" + "','" + UserDBName + "','" + UserDBType1 + "','" + UserDBType2 + "','" + UserDBType3 + "','" + UserDB_Psi_fix.ToString() + "','" + UserDB_Psi_open.ToString() + "','" + UserDB_Psi_fix.ToString() + "','" + UserDB_Psi_open.ToString() + "'", "번호");
                load_table_SpacerDB(UserDBType2, UserDBType3);
            }
            else
            {
                MessageBox.Show("모든 값을 입력해주세요.");
            }
        }

        private void Delete_button_Click(object sender, EventArgs e)
        {
            int k = Spacer_dataGridView.CurrentCell.RowIndex;
            if (k > -1)
            {
                if (Spacer_dataGridView.Rows[k].Cells[2].Value.ToString() == "사용자")
                {
                    if ((MessageBox.Show(Spacer_dataGridView.Rows[k].Cells[3].Value.ToString() + "을 삭제하시겠습니까?", "삭제 확인", MessageBoxButtons.YesNo) == DialogResult.Yes))
                    {
                        String Delete_Num = Spacer_dataGridView.Rows[k].Cells[1].Value.ToString();
                        Program.DB.deleteValue(DB.type.ProjDB, "User_WindowSpacer", "번호 ='" + Delete_Num + "'");
                        load_table_SpacerDB(UserDBType2, UserDBType3);
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
                Spacer_dataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
                SelectRow = e.RowIndex;
                DataGridViewRow row = Spacer_dataGridView.Rows[SelectRow];
                DataGridViewRow row2;
                for (int k = 0; k < Count_FrameDB; k++)
                {
                    if (k != row.Index)
                    {
                        Spacer_dataGridView.Rows[k].Cells[0].Value = false;
                        row2 = Spacer_dataGridView.Rows[k];
                        row2.DefaultCellStyle.BackColor = Color.White;
                        row2.DefaultCellStyle.ForeColor = Color.Black;
                    }
                    else
                    {
                        row.DefaultCellStyle.BackColor = SystemColors.GradientInactiveCaption;
                        row.DefaultCellStyle.ForeColor = Color.Black;
                        row = Spacer_dataGridView.Rows[e.RowIndex];
                    }
                }
            }
        }

        private void Save_button_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = Spacer_dataGridView.Rows[SelectRow];

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
