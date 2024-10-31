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
    public partial class CW_DoorFrameDB : Form
    {
        double Count_FrameDB;
        int SelectRow;
        public String[] Select_DoorFrame = new String[6];
        String UserNum, UserDBName, UserDB_Manufacture, UserDB_Type;
        Double UserDB_Ufd, UserDB_dfd;
        int Num;


        public CW_DoorFrameDB()
        {
            InitializeComponent(); this.Font = new Font(UTIL.Families[0], 9.75F, FontStyle.Regular);



            load_table_DoorFrameDB();
            UserNum = Program.UTIL.CreateNum("User_CWDoorFrmae", "번호", "UCWD_0");
            UserNum_textBox.Text = UserNum;

            //구분 콤보박스
            Program.UTIL.FillComboBox(DB.type.BaseDB_HCneed, UserDB_Type_comboBox, "커튼월", "프레임도어", "1");
        }
        void load_table_DoorFrameDB()
        {
            new StackedHeaderDecorator(Door_dataGridView, DataGridViewAutoSizeColumnsMode.Fill, datagridviewDesign);
            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
            Door_dataGridView.Columns.Clear();
            checkBoxColumn.HeaderText = "선택";
            checkBoxColumn.Name = "check";
            Door_dataGridView.Columns.Add(checkBoxColumn);

            Door_dataGridView.Columns.Add("A1", "번호");
            Door_dataGridView.Columns.Add("A2", "DB유형");
            Door_dataGridView.Columns.Add("A3", "제품명");
            Door_dataGridView.Columns.Add("A4", "제조사");
            Door_dataGridView.Columns.Add("A5", "구분");
            Door_dataGridView.Columns.Add("A6", "프레임.열관류율.Uf,d\r\n[W/m²·K]");
            Door_dataGridView.Columns.Add("A7", "프레임.두께.dd\r\n[m]");
         
                string[][] User_CWDoorFrame = Program.DB.getValue(DB.type.ProjDB, "User_CWDoorFrame", "번호,DB유형,제품명,제조사,구분,프레임열관류율,프레임두께", "");
            if(User_CWDoorFrame.Length > 0 )
            {
                for (int n = 0; n < User_CWDoorFrame.Length; n++)
                {
                    Door_dataGridView.Rows.Add();
                    int nRow = Door_dataGridView.Rows.Count - 1;
                    for (int k = 0; k < 7; k++)
                    {
                        Door_dataGridView.Rows[nRow].Cells[k + 1].Value = User_CWDoorFrame[n][k];
                    }
                }
            }               
            string[][] CWDoorFrame = Program.DB.getValue(DB.type.BaseDB_HCneed, "커튼월도어프레임", "번호,DB유형,제품명,제조사,프레임열관류율,프레임두께", "");
            if(CWDoorFrame.Length > 0 )
            {
                for (int n = 0; n < CWDoorFrame.Length; n++)
                {
                    Door_dataGridView.Rows.Add();
                    int nRow = Door_dataGridView.Rows.Count - 1;
                    Door_dataGridView.Rows[nRow].Cells[1].Value = CWDoorFrame[n][0];
                    Door_dataGridView.Rows[nRow].Cells[2].Value = CWDoorFrame[n][1];
                    Door_dataGridView.Rows[nRow].Cells[3].Value = CWDoorFrame[n][2];
                    Door_dataGridView.Rows[nRow].Cells[4].Value = CWDoorFrame[n][3];
                    Door_dataGridView.Rows[nRow].Cells[5].Value = CWDoorFrame[n][2];
                    Door_dataGridView.Rows[nRow].Cells[6].Value = CWDoorFrame[n][4];
                    Door_dataGridView.Rows[nRow].Cells[7].Value = CWDoorFrame[n][5];
                }
            }
            Count_FrameDB = CWDoorFrame.Length;
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
            else return false;
        }


        private void UserDBName_textBox_TextChanged(object sender, EventArgs e)
        {
            UserDBName = UserDBName_textBox.Text;
        }

        private void UserDB_Manufacture_textBox_TextChanged(object sender, EventArgs e)
        {
            UserDB_Manufacture = UserDB_Manufacture_textBox.Text;
        }

        private void UserDB_Type_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UserDB_Type = UserDB_Type_comboBox.SelectedItem.ToString();
        }
        private void UserDB_Ufd_textBox_TextChanged(object sender, EventArgs e)
        {
            UserDB_Ufd = Convert.ToDouble(UserDB_Ufd_textBox.Text);
        }

        private void UserDB_dfd_textBox_TextChanged(object sender, EventArgs e)
        {
            UserDB_dfd = Convert.ToDouble(UserDB_dfd_textBox.Text);
        }

        private void AddUserDB_button_Click(object sender, EventArgs e)
        {
            string[][] 프로젝트유형 = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "프로젝트유형번호");
            if (UserDBName != null && UserDB_Ufd != 0 && UserDB_dfd != 0)
            {
                Program.DB.setValue(DB.type.ProjDB, "User_CWDoorFrame", "번호,프로젝트유형,DB유형,제품명,제조사,구분,프레임열관류율,프레임두께",
                    "'" + UserNum + "','" + 프로젝트유형[0][0] + "','" + "사용자" + "','" + UserDBName + "','" + UserDB_Manufacture + "','" + UserDB_Type + "','" + UserDB_Ufd.ToString() + "','" + UserDB_dfd.ToString() + "'", "번호");
                load_table_DoorFrameDB();
                UserNum = Program.UTIL.CreateNum("User_CWDoorFrmae", "번호", "UCWD_0");
                UserNum_textBox.Text = UserNum;
            }
            else
            {
                MessageBox.Show("모든 값을 입력해주세요.");
            }
        }

        private void Delete_button_Click(object sender, EventArgs e)
        {
            int k = Door_dataGridView.CurrentCell.RowIndex;
            if (k > -1)
            {
                if (Door_dataGridView.Rows[k].Cells[2].Value.ToString() == "사용자")
                {
                    if ((MessageBox.Show(Door_dataGridView.Rows[k].Cells[3].Value.ToString() + "을 삭제하시겠습니까?", "삭제 확인", MessageBoxButtons.YesNo) == DialogResult.Yes))
                    {
                        String Delete_Num = Door_dataGridView.Rows[k].Cells[1].Value.ToString();
                        Program.DB.deleteValue(DB.type.ProjDB, "User_CWDoorFrame", "번호 ='" + Delete_Num + "'");
                        load_table_DoorFrameDB();
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
                Door_dataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
                SelectRow = e.RowIndex;
            }
        }

        private void Save_button_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = Door_dataGridView.Rows[SelectRow];

            for (int i = 1; i < (row.Cells.Count - 2); i++)
            {
                Select_DoorFrame[i] = row.Cells[i + 2].Value.ToString();
            }

            Select_DoorFrame[0] = row.Cells[1].Value.ToString();

            this.DialogResult = DialogResult.OK;
            this.Close();

        }

    }
}
