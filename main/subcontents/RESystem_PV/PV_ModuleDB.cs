using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
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
        string DefaultUse;
        public PV_ModuleDB(string defaultUse)
        {
            InitializeComponent();
            DefaultUse = defaultUse;
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
            new StackedHeaderDecorator(PVModule_dataGridView, DataGridViewAutoSizeColumnsMode.Fill);
            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
            PVModule_dataGridView.Columns.Clear();
            checkBoxColumn.HeaderText = "선택";
            checkBoxColumn.Name = "check";
            PVModule_dataGridView.Columns.Add(checkBoxColumn);
            PVModule_dataGridView.Columns.Add("A1", "번호");
            PVModule_dataGridView.Columns.Add("A2", "DB유형");
            PVModule_dataGridView.Columns.Add("A3", "제품명");
            PVModule_dataGridView.Columns.Add("A4", "제조사");
            PVModule_dataGridView.Columns.Add("A5", "제작년도");
            PVModule_dataGridView.Columns.Add("A6", "Cell Type");
            PVModule_dataGridView.Columns.Add("A7", "Kpk");
            if (DefaultUse != "기본DB 적용")
            {
                PVModule_dataGridView.Columns.Add("A8", "가로길이");
                PVModule_dataGridView.Columns.Add("A9", "세로길이");
                PVModule_dataGridView.Columns.Add("A10", "정격출력");
            }

            if (DefaultUse == "기본DB 적용")
            {
                string[][] PVModule = Program.DB.getValue(DB.type.BaseDB_RESystem, "태양광모듈DB", "번호,DB유형,제품명,제조사,제작년도,CELLTYPE,Kpk", "");                
                for (int n = 0; n < PVModule.Length; n++)
                {
                    PVModule_dataGridView.Rows.Add();
                    PVModule_dataGridView.Rows[n].Cells[1].Value = PVModule[n][0];
                    PVModule_dataGridView.Rows[n].Cells[2].Value = PVModule[n][1];
                    PVModule_dataGridView.Rows[n].Cells[3].Value = PVModule[n][2];
                    PVModule_dataGridView.Rows[n].Cells[4].Value = PVModule[n][3];
                    PVModule_dataGridView.Rows[n].Cells[5].Value = PVModule[n][4];
                    PVModule_dataGridView.Rows[n].Cells[6].Value = PVModule[n][5];
                    PVModule_dataGridView.Rows[n].Cells[7].Value = PVModule[n][6];                  
                }
            }

            //사용자 DB 추가
            try
            {
                string[][] User_PVModule = Program.DB.getValue(DB.type.ProjDB, "User_PVModule", "번호,DB유형,제품명,제조사,제작년도,CELLTYPE,Kpk,가로길이,세로길이,정격출력", "");
                for (int n = 0; n < User_PVModule.Length; n++)
                {
                    PVModule_dataGridView.Rows.Add();
                    PVModule_dataGridView.Rows[n].Cells[1].Value = User_PVModule [n][0];
                    PVModule_dataGridView.Rows[n].Cells[2].Value = User_PVModule[n][1];
                    PVModule_dataGridView.Rows[n].Cells[3].Value = User_PVModule[n][2];
                    PVModule_dataGridView.Rows[n].Cells[4].Value = User_PVModule[n][3];
                    PVModule_dataGridView.Rows[n].Cells[5].Value = User_PVModule[n][4];
                    PVModule_dataGridView.Rows[n].Cells[6].Value = User_PVModule[n][5];
                    PVModule_dataGridView.Rows[n].Cells[7].Value = User_PVModule[n][6];
                    PVModule_dataGridView.Rows[n].Cells[8].Value = User_PVModule[n][7];
                    PVModule_dataGridView.Rows[n].Cells[9].Value = User_PVModule[n][8];
                    PVModule_dataGridView.Rows[n].Cells[10].Value = User_PVModule[n][9];
                }
            }
            catch { }
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
            string[][] 프로젝트유형 = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "프로젝트유형번호");
            if (UserDB_Name != null && UserDB_Manufacture != null && UserDB_year != null && UserDB_celltype != null && UserDB_width != 0 && UserDB_height != 0 && UserDB_output != 0)
            {
                Program.DB.setValue(DB.type.ProjDB, "User_PVModule", "번호,프로젝트유형,DB유형,제품명,제조사,제작년도,CELLTYPE,Kpk,가로길이,세로길이,정격출력",
                    "'" + UserNum + "','" + 프로젝트유형[0][0] +"','" + "사용자" + "','" + UserDB_Name + "','" + UserDB_Manufacture + "','" + UserDB_year + "','" + UserDB_celltype + "','" + UserDB_Kpk.ToString() + "','" + UserDB_width.ToString() + "','" + UserDB_height.ToString() + "','" + UserDB_output.ToString() + "'", "번호");
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
