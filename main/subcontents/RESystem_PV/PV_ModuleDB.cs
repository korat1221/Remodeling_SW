using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace main.subcontents.RESystem_PV
{
    public partial class PV_ModuleDB : Form
    {

        double Count_PVModuleDB;
        int SelectRow;
        public String[] Select_PVModule = new string[10];
        String UserNum, UserDB_Name, UserDB_Manufacture, UserDB_year, UserDB_celltype;
        double UserDB_width, UserDB_height, UserDB_output, UserDB_Kpk;

        public PV_ModuleDB()
        {
            InitializeComponent();
            load_table_PVModuleDB();

            //제작년도 COMBOBOX
            UserDB_year_comboBox.Items.Clear();
            UserDB_year_comboBox.Items.Add("25년 이내");
            UserDB_year_comboBox.Items.Add("25년 이상");

            //Cell Type COMBOBOX
            UserDB_celltype_comboBox.Items.Add("단결정(Single Cry. Si.)");
            UserDB_celltype_comboBox.Items.Add("다결정(Poly Cry. Si.)");
            UserDB_celltype_comboBox.Items.Add("비결정질 Si 박막");
            UserDB_celltype_comboBox.Items.Add("그외 Si 박막");
            UserDB_celltype_comboBox.Items.Add("CIGS 박막");
            UserDB_celltype_comboBox.Items.Add("CdTe 박막");

            //번호
            UserNum = Program.UTIL.CreateNum("User_PVModule", "번호", "UPV_0");
            UserNum_textBox.Text = UserNum;

        }

        void load_table_PVModuleDB()
        {
            //데이터 그리드뷰 만들기
            DataTable table_PVModule = new DataTable();
            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
            PVModule_dataGridView.Columns.Clear();
            checkBoxColumn.HeaderText = "선택";
            checkBoxColumn.Name = "check";
            PVModule_dataGridView.Columns.Add(checkBoxColumn);
            table_PVModule.Columns.Add("번호", typeof(string));
            table_PVModule.Columns.Add("DB유형", typeof(string));
            table_PVModule.Columns.Add("제품명", typeof(string));
            table_PVModule.Columns.Add("제조사", typeof(string));
            table_PVModule.Columns.Add("제작년도", typeof(string));
            table_PVModule.Columns.Add("CELLTYPE", typeof(string));
            table_PVModule.Columns.Add("Kpk" + Environment.NewLine + "kW/m2", typeof(string));
            table_PVModule.Columns.Add("가로길이" + Environment.NewLine + "m", typeof(string));
            table_PVModule.Columns.Add("세로길이" + Environment.NewLine + "m", typeof(string));
            table_PVModule.Columns.Add("정격출력" + Environment.NewLine + "W", typeof(string));

            //사용자 DB 추가
            try
            {
                string[][] User_PVModule = Program.DB.getValue(DB.type.ProjDB, "User_PVModule", "번호,DB유형,제품명,제조사,제작년도,CELLTYPE,Kpk,가로길이,세로길이,정격출력", "");
                for (int n = 0; n < User_PVModule.Length; n++)
                {
                    table_PVModule.Rows.Add(User_PVModule[n][0], User_PVModule[n][1], User_PVModule[n][2], User_PVModule[n][3], User_PVModule[n][4], User_PVModule[n][5], User_PVModule[n][6], User_PVModule[n][7], User_PVModule[n][8], User_PVModule[n][9]);
                }
            }
            catch { }


            //표준 DB 불러오기
            string[][] PVModule = Program.DB.getValue(DB.type.BaseDB_RESystem, "태양광모듈DB", "번호,DB유형,제품명,제조사,제작년도,CELLTYPE,Kpk,가로길이,세로길이,정격출력", "");

            for (int n = 0; n < PVModule.Length; n++)
            {
                table_PVModule.Rows.Add(PVModule[n][0], PVModule[n][1], PVModule[n][2], PVModule[n][3], PVModule[n][4], PVModule[n][5], String.Format("{0:F2}", Convert.ToDouble(PVModule[n][6])), PVModule[n][7], PVModule[n][8], PVModule[n][9]);
            }

            PVModule_dataGridView.DataSource = table_PVModule;
            Count_PVModuleDB = PVModule.Length;
        }

        private void Door_dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                PVModule_dataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
                SelectRow = e.RowIndex;
                DataGridViewRow row = PVModule_dataGridView.Rows[SelectRow];
                DataGridViewRow row2;
                for (int k = 0; k < Count_PVModuleDB; k++)
                {
                    if (k != row.Index)
                    {
                        PVModule_dataGridView.Rows[k].Cells[0].Value = false;
                        row2 = PVModule_dataGridView.Rows[k];
                        row2.DefaultCellStyle.BackColor = Color.White;
                        row2.DefaultCellStyle.ForeColor = Color.Black;
                    }
                    else
                    {
                        row.DefaultCellStyle.BackColor = SystemColors.GradientInactiveCaption;
                        row.DefaultCellStyle.ForeColor = Color.Black;
                        row = PVModule_dataGridView.Rows[e.RowIndex];
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

        private void UserDB_year_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UserDB_year = UserDB_year_comboBox.SelectedItem.ToString();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            UserDB_celltype = UserDB_celltype_comboBox.SelectedItem.ToString();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            int result;

            if (int.TryParse(UserDB_width_textBox.Text, out result) == true)
            {
                UserDB_width = Convert.ToDouble(UserDB_width_textBox.Text);
                caculation_Kpk();
            }
            else
            {
                MessageBox.Show("숫자를 입력하세요.");
            }

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            int result;

            if (int.TryParse(UserDB_height_textBox.Text, out result) == true)
            {
                UserDB_height = Convert.ToDouble(UserDB_height_textBox.Text);
                caculation_Kpk();
            }
            else
            {
                MessageBox.Show("숫자를 입력하세요.");
            }

        }

        private void UserDB_output_textBox_TextChanged(object sender, EventArgs e)
        {

            int result;

            if (int.TryParse(UserDB_output_textBox.Text, out result) == true)
            {
                UserDB_output = Convert.ToDouble(UserDB_output_textBox.Text);
                caculation_Kpk();
            }
            else
            {
                MessageBox.Show("숫자를 입력하세요.");
            }
        }

        private void caculation_Kpk()
        {
            if (UserDB_height != 0 && UserDB_width != 0 && UserDB_output != 0)
            {
                UserDB_Kpk = UserDB_output / (UserDB_height * UserDB_width) / 1000;
            }

            UserDB_Kpk_textbox.Text = string.Format("{0:0.00}", UserDB_Kpk);
        }

        //SetValue
        private void AddUserDB_button_Click(object sender, EventArgs e)
        {
            if (UserDB_Name != null && UserDB_Manufacture != null && UserDB_year != null && UserDB_celltype != null && UserDB_width != 0 && UserDB_height != 0 && UserDB_output != 0)
            {
                Program.DB.setValue(DB.type.ProjDB, "User_PVModule", "번호,DB유형,제품명,제조사,제작년도,CELLTYPE,Kpk,가로길이,세로길이,정격출력",
                    "'" + UserNum + "','" + "사용자" + "','" + UserDB_Name + "','" + UserDB_Manufacture + "','" + UserDB_year + "','" + UserDB_celltype + "','" + UserDB_Kpk.ToString() + "','" + UserDB_width.ToString() + "','" + UserDB_height.ToString() + "','" + UserDB_output.ToString() + "'", "번호");
                load_table_PVModuleDB();
            }
            else
            {
                MessageBox.Show("모든 값을 입력해주세요.");
            }
        }

        private void Deletebutton_Click(object sender, EventArgs e)
        {
            int k = PVModule_dataGridView.CurrentCell.RowIndex;
            if (k > -1)
            {
                if (PVModule_dataGridView.Rows[k].Cells[2].Value.ToString() == "사용자")
                {
                    if ((MessageBox.Show(PVModule_dataGridView.Rows[k].Cells[3].Value.ToString() + "을 삭제하시겠습니까?", "삭제 확인", MessageBoxButtons.YesNo) == DialogResult.Yes))
                    {
                        String Delete_Num = PVModule_dataGridView.Rows[k].Cells[1].Value.ToString();
                        Program.DB.deleteValue(DB.type.ProjDB, "User_PVModule", "번호 ='" + Delete_Num + "'");
                        load_table_PVModuleDB();
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
            // 번호,DB유형,제품명,제조사,제작년도,CELLTYPE,Kpk,가로길이,세로길이,정격출력
            DataGridViewRow row = PVModule_dataGridView.Rows[SelectRow];
            Select_PVModule[0] = row.Cells[1].Value.ToString(); //번호
            Select_PVModule[1] = row.Cells[2].Value.ToString(); //DB유형
            Select_PVModule[2] = row.Cells[3].Value.ToString(); //제품명
            Select_PVModule[3] = row.Cells[4].Value.ToString(); //제조사
            Select_PVModule[4] = row.Cells[5].Value.ToString(); //제작년도
            Select_PVModule[5] = row.Cells[6].Value.ToString(); //CELLTYPE
            Select_PVModule[6] = row.Cells[7].Value.ToString(); //Kpk
            Select_PVModule[7] = row.Cells[8].Value.ToString(); //가로길이
            Select_PVModule[8] = row.Cells[9].Value.ToString(); //세로길이
            Select_PVModule[9] = row.Cells[10].Value.ToString(); //정격출력

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
