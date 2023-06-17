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
            InitializeComponent();
            load_table_DoorFrameDB();
            UserNum = Program.UTIL.CreateNum("User_CWDoorFrmae", "번호", "UCWD_0");
            UserNum_textBox.Text = UserNum;

            //구분 콤보박스
            Program.UTIL.FillComboBox(UserDB_Type_comboBox, "커튼월", "프레임도어", "1");
        }
        void load_table_DoorFrameDB()
        {
            DataTable table_CWDoor = new DataTable();
            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
            Door_dataGridView.Columns.Clear();
            checkBoxColumn.HeaderText = "선택";
            checkBoxColumn.Name = "check";
            Door_dataGridView.Columns.Add(checkBoxColumn);
            table_CWDoor.Columns.Add("번호", typeof(string));
            table_CWDoor.Columns.Add("DB유형", typeof(string));
            table_CWDoor.Columns.Add("제품명", typeof(string));
            table_CWDoor.Columns.Add("제조사", typeof(string));
            table_CWDoor.Columns.Add("구분", typeof(string));
            table_CWDoor.Columns.Add("프레임\r\n열관류율" + Environment.NewLine + "Uf,d\r\n[W/m²·K]", typeof(string));
            table_CWDoor.Columns.Add("프레임\r\n두께" + Environment.NewLine + "dd\n[m]", typeof(string));
            try
            {
                string[][] User_CWDoorFrame = Program.DB.getValue(DB.type.ProjDB, "User_CWDoorFrame", "번호,DB유형,제품명,제조사,구분,프레임열관류율,프레임두께", "");
                for (int n = 0; n < User_CWDoorFrame.Length; n++)
                {
                    table_CWDoor.Rows.Add(User_CWDoorFrame[n][0], User_CWDoorFrame[n][1], User_CWDoorFrame[n][2], User_CWDoorFrame[n][3], User_CWDoorFrame[n][4], User_CWDoorFrame[n][5], User_CWDoorFrame[n][6]);
                }
            }
            catch { }

            string[][] CWDoorFrame = Program.DB.getValue(DB.type.BaseDB, "커튼월도어프레임", "번호,DB유형,제품명,제조사,프레임열관류율,프레임두께", "");
            for (int n = 0; n < CWDoorFrame.Length; n++)
            {
                table_CWDoor.Rows.Add(CWDoorFrame[n][0], CWDoorFrame[n][1], CWDoorFrame[n][2], CWDoorFrame[n][3], CWDoorFrame[n][2], CWDoorFrame[n][4], CWDoorFrame[n][5]);
            }
            Door_dataGridView.DataSource = table_CWDoor;
            Count_FrameDB = CWDoorFrame.Length;
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

            if (UserDBName != null && UserDB_Ufd != 0 && UserDB_dfd != 0)
            {
                Program.DB.setValue(DB.type.ProjDB, "User_CWDoorFrame", "번호,DB유형,제품명,제조사,구분,프레임열관류율,프레임두께",
                    "'" + UserNum + "','" + "사용자" + "','" + UserDBName + "','" + UserDB_Manufacture + "','" + UserDB_Type + "','" + UserDB_Ufd.ToString() + "','" + UserDB_dfd.ToString() + "'", "번호");
                load_table_DoorFrameDB();
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
                DataGridViewRow row = Door_dataGridView.Rows[SelectRow];
                DataGridViewRow row2;
                for (int k = 0; k < Count_FrameDB; k++)
                {
                    if (k != row.Index)
                    {
                        Door_dataGridView.Rows[k].Cells[0].Value = false;
                        row2 = Door_dataGridView.Rows[k];
                        row2.DefaultCellStyle.BackColor = Color.White;
                        row2.DefaultCellStyle.ForeColor = Color.Black;
                    }
                    else
                    {
                        row.DefaultCellStyle.BackColor = SystemColors.GradientInactiveCaption;
                        row.DefaultCellStyle.ForeColor = Color.Black;
                        row = Door_dataGridView.Rows[e.RowIndex];
                    }
                }
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
