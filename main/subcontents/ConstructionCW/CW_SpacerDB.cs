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
    public partial class CW_SpacerDB : Form
    {
        double Count_FrameDB;
        int SelectRow;
        String LE_CL_V;
        public String[] Select_Spacer = new String[10];
        String UserNum, UserDBName, UserDB_Manufacture, UserDBType1, UserDBType2;
        Double UserDB_Psi_fix, UserDB_Psi_open;
        int Num;


        public CW_SpacerDB(String FrameType, String LE_CL_V)
        {
            InitializeComponent();
            load_table_SpacerDB(FrameType);
            this.LE_CL_V = LE_CL_V;
            //사용자DB 구분1 콤보박스
            UserDBType1_comboBox.Items.Add("일반간봉");
            UserDBType1_comboBox.Items.Add("단열간봉");
            //사용자DB 구분2 콤보박스
            UserDBType2_comboBox.Items.Add("STS");
            UserDBType2_comboBox.Items.Add("일반ALU");
            UserDBType2_comboBox.Items.Add("단열ALU");
            UserDBType2_comboBox.SelectedItem = FrameType;
            UserDBType2_comboBox.Enabled = false;
            UserNum = Program.UTIL.CreateNum("User_CWSpacer", "번호", "UCS_0");
            UserNum_textBox.Text = UserNum;
        }
        void load_table_SpacerDB(String FrameTpe)
        {
            new StackedHeaderDecorator(Spacer_dataGridView);
            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
            Spacer_dataGridView.Columns.Clear();
            checkBoxColumn.HeaderText = "선택";
            checkBoxColumn.Name = "check";
            Spacer_dataGridView.Columns.Add(checkBoxColumn);

            Spacer_dataGridView.Columns.Add("A1", "번호");
            Spacer_dataGridView.Columns.Add("A1", "DB유형");
            Spacer_dataGridView.Columns.Add("A1", "제품명");
            Spacer_dataGridView.Columns.Add("A1", "제조사");
            Spacer_dataGridView.Columns.Add("A1", "구분1");
            Spacer_dataGridView.Columns.Add("A1", "구분2");
            Spacer_dataGridView.Columns.Add("A1", "선형열관류율.고정유리(CL).Ψg,mt.[W/m·K]");
            Spacer_dataGridView.Columns.Add("A1", "선형열관류율.개폐유리(CL).Ψg,fr.[W/m·K]");
            Spacer_dataGridView.Columns.Add("A1", "선형열관류율.고정유리(LE).Ψg,mt.[W/m·K]");
            Spacer_dataGridView.Columns.Add("A1", "선형열관류율.개폐유리(LE).Ψg,fr.[W/m·K]");
            //table_CWSpacer.Columns.Add("번호", typeof(string));
            //table_CWSpacer.Columns.Add("DB유형", typeof(string));
            //table_CWSpacer.Columns.Add("제품명", typeof(string));
            //table_CWSpacer.Columns.Add("제조사", typeof(string));
            //table_CWSpacer.Columns.Add("구분1", typeof(string));
            //table_CWSpacer.Columns.Add("구분2", typeof(string));
            //table_CWSpacer.Columns.Add("고정유리(CL)\r\n선형열관류율" + Environment.NewLine + "Ψg,mt\r\n[W/m·K]", typeof(string));
            //table_CWSpacer.Columns.Add("개폐유리(CL)\r\n선형열관류율" + Environment.NewLine + "Ψg,fr\r\n[W/m·K]", typeof(string));
            //table_CWSpacer.Columns.Add("고정유리(LE)\r\n선형열관류율" + Environment.NewLine + "Ψg,mt\r\n[W/m·K]", typeof(string));
            //table_CWSpacer.Columns.Add("개폐유리(LE)\r\n선형열관류율" + Environment.NewLine + "Ψg,fr\r\n[W/m·K]", typeof(string));

            try
            {
                string[][] User_CWSpacer = Program.DB.getValue(DB.type.ProjDB, "User_CWSpacer", "번호,DB유형,제품명,제조사,구분1,구분3,고정유리_CL_선형열관류율,개폐유리_CL_선형열관류율,고정유리_LE_선형열관류율,개폐유리_LE_선형열관류율", "구분3 ='" + FrameTpe + "'");
                for (int n = 0; n < User_CWSpacer.Length; n++)
                {
                    Spacer_dataGridView.Rows.Add();
                    int nRow = Spacer_dataGridView.Rows.Count - 1;
                    for (int k = 0; k < 10; k++)
                    {
                        Spacer_dataGridView.Rows[nRow].Cells[k + 1].Value = User_CWSpacer[n][k];
                    }
                    //table_CWSpacer.Rows.Add(User_CWSpacer[n][0], User_CWSpacer[n][1], User_CWSpacer[n][2], User_CWSpacer[n][3], User_CWSpacer[n][4], User_CWSpacer[n][5], User_CWSpacer[n][6], User_CWSpacer[n][7], User_CWSpacer[n][8], User_CWSpacer[n][9]);
                }
            }
            catch { }

            string[][] CWSpacer = Program.DB.getValue(DB.type.BaseDB_HCneed, "커튼월간봉", "번호,DB유형,제품명,제조사,구분1,구분3,고정유리_CL_선형열관류율,개폐유리_CL_선형열관류율,고정유리_LE_선형열관류율,개폐유리_LE_선형열관류율", "구분3 ='" + FrameTpe + "'");
            for (int n = 0; n < CWSpacer.Length; n++)
            {
                Spacer_dataGridView.Rows.Add();
                int nRow = Spacer_dataGridView.Rows.Count - 1;
                for (int k = 0; k < 10; k++)
                {
                    Spacer_dataGridView.Rows[nRow].Cells[k + 1].Value = CWSpacer[n][k];
                }
                //table_CWSpacer.Rows.Add(CWSpacer[n][0], CWSpacer[n][1], CWSpacer[n][2], CWSpacer[n][3], CWSpacer[n][4], CWSpacer[n][5], CWSpacer[n][6], CWSpacer[n][7], CWSpacer[n][8], CWSpacer[n][9]);
            }
            //Spacer_dataGridView.DataSource = table_CWSpacer;
            Count_FrameDB = CWSpacer.Length;
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
                load_table_SpacerDB(UserDBType2);
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
                        Program.DB.deleteValue(DB.type.ProjDB, "User_CWSpacer", "번호 ='" + Delete_Num + "'");
                        load_table_SpacerDB(UserDBType2);
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
            }
        }

        private void Save_button_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = Spacer_dataGridView.Rows[SelectRow];

            for (int i = 1; i < (row.Cells.Count - 2); i++)
            {
                Select_Spacer[i] = row.Cells[i + 2].Value.ToString();
            }

            Select_Spacer[0] = row.Cells[1].Value.ToString();
            Select_Spacer[9] = LE_CL_V;

            this.DialogResult = DialogResult.OK;
            this.Close();

        }

    }
}
