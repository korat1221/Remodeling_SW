using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.ComponentModel.Design.ObjectSelectorEditor;

namespace main.subcontents.RESystem_PV
{
    public partial class PV_InverterDB : Form
    {
        double Count_PVInverterDB;
        int SelectRow;
        public String[] Select_PVInverter = new string[5];
        String UserNum, UserDB_Name, UserDB_Manufacture;
        double UserDB_EURO;

        public PV_InverterDB()
        {
            InitializeComponent();
            load_table_PVInverterDB();

            //번호
            UserNum = Program.UTIL.CreateNum("User_PVInverter", "번호", "UIV_0");
            UserNum_textBox.Text = UserNum;
        }

        void load_table_PVInverterDB()
        {
            //데이터 그리드뷰 만들기
            DataTable table_PVInverter = new DataTable();
            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
            PVInverter_dataGridView.Columns.Clear();
            checkBoxColumn.HeaderText = "선택";
            checkBoxColumn.Name = "check";
            PVInverter_dataGridView.Columns.Add(checkBoxColumn);
            table_PVInverter.Columns.Add("번호", typeof(string));
            table_PVInverter.Columns.Add("DB유형", typeof(string));
            table_PVInverter.Columns.Add("제품명", typeof(string));
            table_PVInverter.Columns.Add("제조사", typeof(string));
            table_PVInverter.Columns.Add("EURO효율" + Environment.NewLine + "%", typeof(string));

            //사용자 DB 추가
            try
            {
                string[][] User_PVInverter = Program.DB.getValue(DB.type.ProjDB, "User_PVInverter", "번호,DB유형,제품명,제조사,EURO효율", "");
                for (int n = 0; n < User_PVInverter.Length; n++)
                {
                    table_PVInverter.Rows.Add(User_PVInverter[n][0], User_PVInverter[n][1], User_PVInverter[n][2], User_PVInverter[n][3], User_PVInverter[n][4]);
                }
            }
            catch { }


            //표준 DB 불러오기
            string[][] PVInverter = Program.DB.getValue(DB.type.BaseDB_RESystem, "태양광인버터DB", "번호,DB유형,제품명,제조사,EURO효율", "");

            for (int n = 0; n < PVInverter.Length; n++)
            {
                table_PVInverter.Rows.Add(PVInverter[n][0], PVInverter[n][1], PVInverter[n][2], PVInverter[n][3], String.Format("{0:F2}", Convert.ToDouble(PVInverter[n][4])));
            }

            PVInverter_dataGridView.DataSource = table_PVInverter;
            Count_PVInverterDB = PVInverter.Length;
        }

        private void PVInverter_dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                PVInverter_dataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
                SelectRow = e.RowIndex;
                DataGridViewRow row = PVInverter_dataGridView.Rows[SelectRow];
                DataGridViewRow row2;
                for (int k = 0; k < Count_PVInverterDB; k++)
                {
                    if (k != row.Index)
                    {
                        PVInverter_dataGridView.Rows[k].Cells[0].Value = false;
                        row2 = PVInverter_dataGridView.Rows[k];
                        row2.DefaultCellStyle.BackColor = Color.White;
                        row2.DefaultCellStyle.ForeColor = Color.Black;
                    }
                    else
                    {
                        row.DefaultCellStyle.BackColor = SystemColors.GradientInactiveCaption;
                        row.DefaultCellStyle.ForeColor = Color.Black;
                        row = PVInverter_dataGridView.Rows[e.RowIndex];
                    }
                }
            }
        }

        private void UserDBName_textBox_TextChanged(object sender, EventArgs e)
        {
            UserDB_Name = UserDBName_textBox.Text;
        }

        private void UserDB_Manufacture_textBox_TextChanged(object sender, EventArgs e)
        {
            UserDB_Manufacture = UserDB_Manufacture_textBox.Text;
        }

        private void UserDB_Euro_TextBox_TextChanged(object sender, EventArgs e)
        {
            int result;

            if (int.TryParse(UserDB_Euro_TextBox.Text, out result) == true)
            {
                UserDB_EURO = Convert.ToDouble(UserDB_Euro_TextBox.Text);
            }
            else
            {
                MessageBox.Show("숫자를 입력하세요.");
            }
        }

        //SetValue
        private void AddUserDB_button_Click(object sender, EventArgs e)
        {
            if (UserDB_Name != null && UserDB_Manufacture != null && UserDB_EURO != 0)
            {
                Program.DB.setValue(DB.type.ProjDB, "User_PVInverter", "번호,DB유형,제품명,제조사,EURO효율",
                    "'" + UserNum + "','" + "사용자" + "','" + UserDB_Name + "','" + UserDB_Manufacture + "','" + UserDB_EURO.ToString() + "'", "번호");
                load_table_PVInverterDB();
            }
            else
            {
                MessageBox.Show("모든 값을 입력해주세요.");
            }
        }

        private void Deletebutton_Click(object sender, EventArgs e)
        {
            int k = PVInverter_dataGridView.CurrentCell.RowIndex;
            if (k > -1)
            {
                if (PVInverter_dataGridView.Rows[k].Cells[2].Value.ToString() == "사용자")
                {
                    if ((MessageBox.Show(PVInverter_dataGridView.Rows[k].Cells[3].Value.ToString() + "을 삭제하시겠습니까?", "삭제 확인", MessageBoxButtons.YesNo) == DialogResult.Yes))
                    {
                        String Delete_Num = PVInverter_dataGridView.Rows[k].Cells[1].Value.ToString();
                        Program.DB.deleteValue(DB.type.ProjDB, "User_PVInverter", "번호 ='" + Delete_Num + "'");
                        load_table_PVInverterDB();
                    }
                }
                else
                {
                    MessageBox.Show("기본 DB는 삭제할 수 없습니다.");
                }
            }
        }
        private void Save_button_Click(object sender, EventArgs e)
        {
            // 번호,DB유형,제품명,제조사,EURO효율
            DataGridViewRow row = PVInverter_dataGridView.Rows[SelectRow];
            Select_PVInverter[0] = row.Cells[1].Value.ToString(); //번호
            Select_PVInverter[1] = row.Cells[2].Value.ToString(); //DB유형
            Select_PVInverter[2] = row.Cells[3].Value.ToString(); //제품명
            Select_PVInverter[3] = row.Cells[4].Value.ToString(); //제조사
            Select_PVInverter[4] = row.Cells[5].Value.ToString(); //EURO효율

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void UserDB_Kpk_textbox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
