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

namespace main.subcontents.RESystem_WP
{
    public partial class WP_InverterDB : Form
    {
        double Count_WPInverter;
        int SelectRow;
        public string SelectWPInverter;
        public String[] Select_WPInverter = new string[5];
        String UserNum, UserDB_Name, UserDB_Manufacture;
        double UserDB_EURO;
        DataGridViewCheckBoxColumn WPInverter_checkBoxColumn = new DataGridViewCheckBoxColumn();


        public WP_InverterDB()
        {
            InitializeComponent(); this.Font = new Font(UTIL.Families[0], 9.75F, FontStyle.Regular);
            load_table_WPInverter();

            //번호
            UserNum = Program.UTIL.CreateNum("User_WPInverter", "번호", "UIV_0");
            UserNum_textBox.Text = UserNum;
        }
      
        void load_table_WPInverter()
        {
            new StackedHeaderDecorator(WPInverter_dataGridView, DataGridViewAutoSizeColumnsMode.Fill);

            //데이터 그리드뷰 만들기
            DataTable table_WPInverter = new DataTable();
            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
            WPInverter_dataGridView.Columns.Clear();
            checkBoxColumn.HeaderText = "선택";
            checkBoxColumn.Name = "check";
            WPInverter_dataGridView.Columns.Add(checkBoxColumn);
            table_WPInverter.Columns.Add("번호", typeof(string));
            table_WPInverter.Columns.Add("DB유형", typeof(string));
            table_WPInverter.Columns.Add("제품명", typeof(string));
            table_WPInverter.Columns.Add("제조사", typeof(string));
            table_WPInverter.Columns.Add("EURO효율" + Environment.NewLine + "%", typeof(string));


            //사용자 DB 추가
            string[][] User_WPInverter = Program.DB.getValue(DB.type.ProjDB, "User_WPInverter", "번호,DB유형,제품명,제조사,EURO효율", "");
            if (User_WPInverter.Length > 0)
            {
                for (int n = 0; n < User_WPInverter.Length; n++)
                {
                    table_WPInverter.Rows.Add(User_WPInverter[n][0], User_WPInverter[n][1], User_WPInverter[n][2], User_WPInverter[n][3], User_WPInverter[n][4]);
                }
            }


            //표준 DB 불러오기
            string[][] WPInverter = Program.DB.getValue(DB.type.BaseDB_RESystem, "풍력인버터DB", "번호,DB유형,제품명,제조사,EURO효율", "");
            if (WPInverter.Length > 0)
            {
                for (int n = 0; n < WPInverter.Length; n++)
                {
                    table_WPInverter.Rows.Add(WPInverter[n][0], WPInverter[n][1], WPInverter[n][2], WPInverter[n][3], String.Format("{0:F2}", Program.UTIL.ToDoubleOrZero(WPInverter[n][4])));
                }
            }

            WPInverter_dataGridView.DataSource = table_WPInverter;
            Count_WPInverter = WPInverter.Length;

        }

 
        private void WPInverter_dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                WPInverter_dataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
                SelectRow = e.RowIndex;
                DataGridViewRow row = WPInverter_dataGridView.Rows[SelectRow];
                DataGridViewRow row2;
                for (int k = 0; k < Count_WPInverter; k++)
                {
                    if (k != row.Index)
                    {
                        WPInverter_dataGridView.Rows[k].Cells[0].Value = false;
                        row2 = WPInverter_dataGridView.Rows[k];
                        row2.DefaultCellStyle.BackColor = Color.White;
                        row2.DefaultCellStyle.ForeColor = Color.Black;
                    }
                    else
                    {
                        row.DefaultCellStyle.BackColor = SystemColors.GradientInactiveCaption;
                        row.DefaultCellStyle.ForeColor = Color.Black;
                        row = WPInverter_dataGridView.Rows[e.RowIndex];
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
     
        private void UserDB_EURO_TextBox_TextChanged(object sender, EventArgs e)
        {
            int result;

            if (int.TryParse(UserDB_EURO_TextBox.Text, out result) == true)
            {
                UserDB_EURO = Program.UTIL.ToDoubleOrZero(UserDB_EURO_TextBox.Text);
            }
            else
            {
                MessageBox.Show("숫자를 입력하세요.");
            }

        }

        //SetValue
        private void AddUserDB_button_Click(object sender, EventArgs e)
        {
            string[][] 프로젝트유형 = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "프로젝트유형번호");
            if (UserDB_Name != null && UserDB_Manufacture != null && UserDB_EURO != 0)
            {
                Program.DB.setValue(DB.type.ProjDB, "User_WPInverter", "번호,프로젝트유형,DB유형,제품명,제조사,EURO효율",
                    "'" + UserNum + "','" + 프로젝트유형[0][0] + "','" + "사용자" + "','" + UserDB_Name + "','" + UserDB_Manufacture + "','" + UserDB_EURO.ToString() + "'", "번호");
                load_table_WPInverter();
                
            }
            else
            {
                MessageBox.Show("모든 값을 입력해주세요.");
            }
        }

        private void Deletebutton_Click(object sender, EventArgs e)
        {

            int k = WPInverter_dataGridView.CurrentCell.RowIndex;
            if (k > -1)
            {
                if (WPInverter_dataGridView.Rows[k].Cells[2].Value.ToString() == "사용자")
                {
                    if ((MessageBox.Show(WPInverter_dataGridView.Rows[k].Cells[3].Value.ToString() + "을 삭제하시겠습니까?", "삭제 확인", MessageBoxButtons.YesNo) == DialogResult.Yes))
                    {
                        String Delete_Num = WPInverter_dataGridView.Rows[k].Cells[1].Value.ToString();
                        Program.DB.deleteValue(DB.type.ProjDB, "User_WPInverter", "번호 ='" + Delete_Num + "'");
                        load_table_WPInverter();
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
            DataGridViewRow row = WPInverter_dataGridView.Rows[SelectRow];
            Select_WPInverter[0] = row.Cells[1].Value.ToString(); //번호
            Select_WPInverter[1] = row.Cells[2].Value.ToString(); //DB유형
            Select_WPInverter[2] = row.Cells[3].Value.ToString(); //제품명
            Select_WPInverter[3] = row.Cells[4].Value.ToString(); //제조사
            Select_WPInverter[4] = row.Cells[5].Value.ToString(); //EURO효율
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
