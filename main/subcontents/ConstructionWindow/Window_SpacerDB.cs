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
        public String[] Select_WindowSpacer = new String[11];
        String UserNum, UserDB_Manufacture, UserDBName, UserDBType1, UserDBType2, UserDBType3;
        Double UserDB_Psi_fix, UserDB_Psi_open;
        int Num;


        public Window_SpacerDB(String SingleDoubleType, String FrameMaterial, String LE_CL_V)
        {
            InitializeComponent();
            new StackedHeaderDecorator(Spacer_dataGridView, DataGridViewAutoSizeColumnsMode.Fill, datagridviewDesign);
            load_table_SpacerDB(SingleDoubleType, FrameMaterial);
            this.LE_CL_V = LE_CL_V;
            //사용자DB 구분1 콤보박스
            UserDBType1_comboBox.Items.Add("일반간봉");
            UserDBType1_comboBox.Items.Add("단열간봉");
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
            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
            Spacer_dataGridView.Columns.Clear();
            checkBoxColumn.HeaderText = "선택";
            checkBoxColumn.Name = "check";
            Spacer_dataGridView.Columns.Add(checkBoxColumn);

            Spacer_dataGridView.Columns.Add("A1", "번호");
            Spacer_dataGridView.Columns.Add("A2", "DB유형");
            Spacer_dataGridView.Columns.Add("A3", "제품명");
            Spacer_dataGridView.Columns.Add("A4", "제조사");
            Spacer_dataGridView.Columns.Add("A5", "구분1");
            Spacer_dataGridView.Columns.Add("A6", "구분2");
            Spacer_dataGridView.Columns.Add("A7", "구분3");
            Spacer_dataGridView.Columns.Add("A8", "선형열관류율.고정유리(CL).Ψg,fix.[W/m·K]");
            Spacer_dataGridView.Columns.Add("A9", "선형열관류율.개폐유리(CL).Ψg,t.[W/m·K]");
            Spacer_dataGridView.Columns.Add("A10", "선형열관류율.고정유리(LE).Ψg,fix.[W/m·K]");
            Spacer_dataGridView.Columns.Add("A11", "선형열관류율.개폐유리(LE).Ψg,t.[W/m·K]");
            Spacer_dataGridView.Columns[8].Width = 150;
            Spacer_dataGridView.Columns[9].Width = 80;
            Spacer_dataGridView.Columns[10].Width = 100;
            Spacer_dataGridView.Columns[11].Width = 100;
           
            string[][] User_WinSpacer = Program.DB.getValue(DB.type.ProjDB, "User_WindowSpacer", "번호,DB유형,제품명,제조사,구분1,구분2,구분3,고정유리_CL_선형열관류율,개폐유리_CL_선형열관류율,고정유리_LE_선형열관류율,개폐유리_LE_선형열관류율", "구분2 = '" + SingleDoubleType + "'AND 구분3 ='" + FrameMaterial + "'");
            if(User_WinSpacer.Length > 0)
            {
                for (int n = 0; n < User_WinSpacer.Length; n++)
                {
                    Spacer_dataGridView.Rows.Add();
                    int nRow = Spacer_dataGridView.Rows.Count - 1;
                    for (int k = 0; k < 11; k++)
                    {
                        Spacer_dataGridView.Rows[nRow].Cells[k + 1].Value = User_WinSpacer[n][k];
                    }                   
                }
            }
                
         

            string[][] WinSpacer = Program.DB.getValue(DB.type.BaseDB_HCneed, "창호간봉", "번호,DB유형,제품명,구분1,구분2,구분3,고정유리_CL_선형열관류율,개폐유리_CL_선형열관류율,고정유리_LE_선형열관류율,개폐유리_LE_선형열관류율", "구분2 = '" + SingleDoubleType + "'AND 구분3 ='" + FrameMaterial + "'");
            if(WinSpacer.Length > 0) 
            {
                for (int n = 0; n < WinSpacer.Length; n++)
                {
                    Spacer_dataGridView.Rows.Add();
                    int nRow = Spacer_dataGridView.Rows.Count - 1;
                    Spacer_dataGridView.Rows[nRow].Cells[1].Value = WinSpacer[n][0];
                    Spacer_dataGridView.Rows[nRow].Cells[2].Value = WinSpacer[n][1];
                    Spacer_dataGridView.Rows[nRow].Cells[3].Value = WinSpacer[n][2];
                    if (WinSpacer[n][1] == "표준")
                    {
                        Spacer_dataGridView.Rows[nRow].Cells[4].Value = "계산값";
                    }
                    else Spacer_dataGridView.Rows[nRow].Cells[4].Value = "(주)윈체";//추후 간봉 테이블 제조사  추가 해야함
                    for (int k = 3; k < 10; k++)
                    {
                        Spacer_dataGridView.Rows[nRow].Cells[k + 2].Value = WinSpacer[n][k];
                    }                   
                }
            }
            Count_FrameDB = WinSpacer.Length;
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
                string[][] 프로젝트유형 = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "프로젝트유형번호");
                Program.DB.setValue(DB.type.ProjDB, "User_WindowSpacer", "번호,프로젝트유형,DB유형,제품명,제조사,구분1,구분2,구분3,고정유리_CL_선형열관류율,개폐유리_CL_선형열관류율,고정유리_LE_선형열관류율,개폐유리_LE_선형열관류율",
                    "'" + UserNum + "','" + 프로젝트유형[0][0] + "','" + "사용자" + "','" + UserDBName + "','" + UserDB_Manufacture + "','" + UserDBType1 + "','" + UserDBType2 + "','" + UserDBType3 + "','" + UserDB_Psi_fix.ToString() + "','" + UserDB_Psi_open.ToString() + "','" + UserDB_Psi_fix.ToString() + "','" + UserDB_Psi_open.ToString() + "'", "번호");
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
                //for (int k = 0; k < Count_FrameDB; k++)
                //{
                //    if (k != row.Index)
                //    {
                //        Spacer_dataGridView.Rows[k].Cells[0].Value = false;
                //        row2 = Spacer_dataGridView.Rows[k];
                //        row2.DefaultCellStyle.BackColor = Color.White;
                //        row2.DefaultCellStyle.ForeColor = Color.Black;
                //    }
                //    else
                //    {
                //        row.DefaultCellStyle.BackColor = SystemColors.GradientInactiveCaption;
                //        row.DefaultCellStyle.ForeColor = Color.Black;
                //        row = Spacer_dataGridView.Rows[e.RowIndex];
                //    }
                //}
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
            Select_WindowSpacer[10] = LE_CL_V;

            this.DialogResult = DialogResult.OK;
            this.Close();

        }

    }
}
