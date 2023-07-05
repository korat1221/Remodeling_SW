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

namespace main.subcontents.ZoneLighting
{
    public partial class LightingRenewDB : Form
    {

        int SelectRow;
        public String[] Select_Renew = new string[9];
        double Count_RenewDB;
        

        string UserNum, UserDB_Name, UserDB_Manufacture, UserDB_RenewType;
        double UserDB_Length1, UserDB_Length2, UserDB_eff, UserDB_A;



        public LightingRenewDB()
        {
            InitializeComponent();
            load_table_RenewDB();

            //집광채광 종류 콤보박스
            RenewType_comboBox.Items.Clear();
            RenewType_comboBox.Items.Add("광덕트");
            RenewType_comboBox.Items.Add("프리즘");
            RenewType_comboBox.Items.Add("실내루버형");
            RenewType_comboBox.SelectedIndex = 0;


            //번호
            UserNum = Program.UTIL.CreateNum("User_Renew", "번호", "UL_0");
            UserNum_textBox.Text = UserNum;



        }

        void load_table_RenewDB()
        {
            DataTable table_Renew = new DataTable();
            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
            Renew_dataGridView.Columns.Clear();
            checkBoxColumn.HeaderText = "선택";
            checkBoxColumn.Name = "check";
            Renew_dataGridView.Columns.Add(checkBoxColumn);
            table_Renew.Columns.Add("번호", typeof(string));
            table_Renew.Columns.Add("DB유형", typeof(string));
            table_Renew.Columns.Add("집광채광명칭", typeof(string));
            table_Renew.Columns.Add("집광채광종류", typeof(string));
            table_Renew.Columns.Add("제조사", typeof(string));
            table_Renew.Columns.Add("집광채광 효율" + Environment.NewLine + "[-]", typeof(string));
            table_Renew.Columns.Add("산광부 가로 길이" + Environment.NewLine + "[m]", typeof(string));
            table_Renew.Columns.Add("산광부 세로 길이" + Environment.NewLine + "[m]", typeof(string));
            table_Renew.Columns.Add("산광부 면적" + Environment.NewLine + "[m2]", typeof(string));



            string[][] Renew = Program.DB.getValue(DB.type.BaseDB_Lighting, "조명_집광채광DB", "번호,DB유형,집광채광명칭,집광채광종류,제조사,집광채광효율,산광부가로길이,산광부세로길이,산광부면적", "");

            for (int n = 0; n < Renew.Length; n++)
            {
                table_Renew.Rows.Add(Renew[n][0], Renew[n][1], Renew[n][2], Renew[n][3], Renew[n][4], Renew[n][5], Renew[n][6], Renew[n][7], Renew[n][8]);
            }
            Renew_dataGridView.DataSource = table_Renew;
            Count_RenewDB = Renew.Length;



            //집광채광 사용자 DB 추가 
            try
            {
                string[][] User_Renew = Program.DB.getValue(DB.type.ProjDB, "User_Renew", "번호,DB유형,집광채광명칭,집광채광종류,제조사,집광채광효율,산광부가로길이,산광부세로길이,산광부면적", "");
                for (int n = 0; n < User_Renew.Length; n++)
                {
                    table_Renew.Rows.Add(User_Renew[n][0], User_Renew[n][1], User_Renew[n][2], User_Renew[n][3], User_Renew[n][4], User_Renew[n][5], User_Renew[n][6], User_Renew[n][7], User_Renew[n][8]);
                }
            }
            catch { }

        }


        private void UserDBName_textBox_TextChanged(object sender, EventArgs e)
        {
            UserDB_Name = UserDBName_textBox.Text;
        }


        private void UserDB_Manufacture_textBox_TextChanged(object sender, EventArgs e)
        {
            UserDB_Manufacture = UserDB_Manufacture_textBox.Text;
        }

        private void RenewType_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UserDB_RenewType = RenewType_comboBox.SelectedItem.ToString();
        }

        private void UserDB_Length1_textBox_TextChanged(object sender, EventArgs e)
        {
            UserDB_Length1 = Convert.ToDouble(UserDB_Length1_textBox.Text);
        }

        private void UserDB_A_textBox_TextChanged(object sender, EventArgs e)
        {
            UserDB_A  = Convert.ToDouble(UserDB_A_textBox.Text);
        }

       

        private void UserDB_eff_textBox_TextChanged(object sender, EventArgs e)
        {
            UserDB_eff = Convert.ToDouble(UserDB_eff_textBox.Text);
        }

      
        private void UserDB_Length2_textBox_TextChanged(object sender, EventArgs e)
        {
            UserDB_Length2 = Convert.ToDouble(UserDB_Length2_textBox.Text);
        }


        //SetValue 
        private void AddUserDB_button_Click(object sender, EventArgs e)
        {
            if (UserDB_Name != null && UserDB_Manufacture != null && UserDB_RenewType != null && UserDB_eff != 0 && UserDB_Length1 != 0 && UserDB_Length2 != 0 && UserDB_A != 0)
            {
                Program.DB.setValue(DB.type.ProjDB, "User_Renew", "번호,DB유형,집광채광명칭,집광채광종류,제조사,집광채광효율,산광부가로길이,산광부세로길이,산광부면적",
                    "'" + UserNum + "','" + "사용자" + "','" + UserDB_Name + "','" + UserDB_RenewType + "','" + UserDB_Manufacture + "','" + UserDB_eff.ToString() + "','" + UserDB_Length1.ToString() + "','" + UserDB_Length2.ToString() + "','" + UserDB_A.ToString() + "'", "번호");
                load_table_RenewDB();
            }
            else
            {
                MessageBox.Show("모든 값을 입력해주세요.");
            }

        }


        private void Deletebutton_Click(object sender, EventArgs e)
        {
            int k = Renew_dataGridView.CurrentCell.RowIndex;
            if (k > -1)
            {
                if (Renew_dataGridView.Rows[k].Cells[2].Value.ToString() == "사용자")
                {
                    if ((MessageBox.Show(Renew_dataGridView.Rows[k].Cells[3].Value.ToString() + "을 삭제하시겠습니까?", "삭제 확인", MessageBoxButtons.YesNo) == DialogResult.Yes))
                    {
                        String Delete_Num = Renew_dataGridView.Rows[k].Cells[1].Value.ToString();
                        Program.DB.deleteValue(DB.type.ProjDB, "User_Renew", "번호 ='" + Delete_Num + "'");
                        load_table_RenewDB();
                    }
                }
                else
                {
                    MessageBox.Show("기본 DB는 삭제할 수 없습니다.");
                }
            }


        }





        //데이터그리드뷰 체크박스 선택 시
        private void Renew_dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                Renew_dataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
                SelectRow = e.RowIndex;
                DataGridViewRow row = Renew_dataGridView.Rows[SelectRow];
                DataGridViewRow row2;
                for (int k = 0; k < Count_RenewDB; k++)
                {
                    if (k != row.Index)
                    {
                        Renew_dataGridView.Rows[k].Cells[0].Value = false;
                        row2 = Renew_dataGridView.Rows[k];
                        row2.DefaultCellStyle.BackColor = Color.White;
                        row2.DefaultCellStyle.ForeColor = Color.Black;
                    }
                    else
                    {
                        row.DefaultCellStyle.BackColor = SystemColors.GradientInactiveCaption;
                        row.DefaultCellStyle.ForeColor = Color.Black;
                        row = Renew_dataGridView.Rows[e.RowIndex];
                    }
                }
            }
        }

 


        private void Save_button_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = Renew_dataGridView.Rows[SelectRow];
            Select_Renew[0] = row.Cells[1].Value.ToString(); //번호
            Select_Renew[1] = row.Cells[2].Value.ToString(); //DB유형
            Select_Renew[2] = row.Cells[3].Value.ToString(); //집광채광명칭
            Select_Renew[3] = row.Cells[4].Value.ToString(); //집광채광종류
            Select_Renew[4] = row.Cells[5].Value.ToString(); //제조사
            Select_Renew[5] = row.Cells[6].Value.ToString(); //집광채광효율
            Select_Renew[6] = row.Cells[7].Value.ToString(); //산광부가로길이
            Select_Renew[7] = row.Cells[8].Value.ToString(); //산광부세로길이
            Select_Renew[8] = row.Cells[9].Value.ToString(); //산광부면적


            this.DialogResult = DialogResult.OK;
            this.Close();

        }

    }
}
