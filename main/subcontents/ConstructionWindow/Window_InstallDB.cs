using System;
using System.Collections;
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
    public partial class Window_InstallDB : Form
    {
        String FrameType;
        double Count_InstallDB;
        int SelectRow;
        public String[] Select_WindowInstall = new string[9];
        String InstallType, SingleDoubleType, FrameMaterial;
        string[][] WinInstall;
        String UserNum, UserDBName, UserDBType1, UserDBType2, UserDBType3, UserDBType4;
        Double UserDB_Psi_InstallTop, UserDB_Psi_InstallSide, UserDB_Psi_InstallButtom;
        int Num;

        public Window_InstallDB(String InstallType, String SingleDoubleType, String FrameMaterial)
        {
            InitializeComponent(); this.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular);
            this.InstallType = InstallType;
            this.SingleDoubleType = SingleDoubleType;
            this.FrameMaterial = FrameMaterial;
            load_table_InstallDB();
            //사용자DB 구분1 콤보박스
            UserDBType1_comboBox.Items.Add("내단열");
            UserDBType1_comboBox.Items.Add("외단열");
            UserDBType1_comboBox.Items.Add("목구조");
            UserDBType1_comboBox.Items.Add("경량철골조");
            UserDBType1_comboBox.SelectedItem = InstallType;
            UserDBType1_comboBox.Enabled = false;
            //사용자DB 구분2 콤보박스
            UserDBType2_comboBox.Items.Add("플라스틱");
            UserDBType2_comboBox.Items.Add("금속");
            UserDBType2_comboBox.SelectedItem = FrameMaterial;
            UserDBType2_comboBox.Enabled = false;
            //사용자DB 구분3 콤보박스
            UserDBType3_comboBox.Items.Add("단창");
            UserDBType3_comboBox.Items.Add("이중창");
            UserDBType3_comboBox.SelectedItem = SingleDoubleType;
            UserDBType3_comboBox.Enabled = false;
            //사용자DB 구분4 콤보박스
            Program.UTIL.FillComboBox(DB.type.BaseDB_HCneed, UserDBType4_comboBox, "창호", "설치위치", "1");
            UserNum = Program.UTIL.CreateNum("User_WindowInstall", "번호", "UWS_0");
            UserNum_textBox.Text = UserNum;
        }
        public Window_InstallDB(String InstallType)
        {
            InitializeComponent(); this.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular);
            this.InstallType = InstallType;
            load_table_InstallDB();
            //사용자DB 구분1 콤보박스
            UserDBType1_comboBox.Items.Add("내단열");
            UserDBType1_comboBox.Items.Add("외단열");
            UserDBType1_comboBox.Items.Add("목구조");
            UserDBType1_comboBox.Items.Add("경량철골조");
            UserDBType1_comboBox.SelectedItem = InstallType;
            UserDBType1_comboBox.Enabled = false;
            //사용자DB 구분2 콤보박스
            UserDBType2_comboBox.Items.Add("플라스틱");
            UserDBType2_comboBox.Items.Add("금속");
            UserDBType2_comboBox.SelectedIndex = 0;
            //사용자DB 구분3 콤보박스
            UserDBType3_comboBox.Items.Add("단창");
            UserDBType3_comboBox.Items.Add("이중창");
            UserDBType2_comboBox.SelectedIndex = 0;
            //사용자DB 구분4 콤보박스
            Program.UTIL.FillComboBox(DB.type.BaseDB_HCneed, UserDBType4_comboBox, "창호", "설치위치", "1");
        }

        void load_table_InstallDB()
        {
            new StackedHeaderDecorator(Install_dataGridView, DataGridViewAutoSizeColumnsMode.Fill, datagridviewDesign);
            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
            Install_dataGridView.Columns.Clear();
            checkBoxColumn.HeaderText = "선택";
            checkBoxColumn.Name = "check";
            Install_dataGridView.Columns.Add(checkBoxColumn);

            Install_dataGridView.Columns.Add("A1", "번호");
            Install_dataGridView.Columns.Add("A2", "DB유형");
            Install_dataGridView.Columns.Add("A3", "제품명");
            Install_dataGridView.Columns.Add("A4", "구분1");
            Install_dataGridView.Columns.Add("A5", "구분2");
            Install_dataGridView.Columns.Add("A6", "구분3");
            Install_dataGridView.Columns.Add("A7", "구분4");
            Install_dataGridView.Columns.Add("A8", "설치선형열관류율.상부.Ψg,top.[W/m·K]");
            Install_dataGridView.Columns.Add("A9", "설치선형열관류율.측면.Ψg,side.[W/m·K]");
            Install_dataGridView.Columns.Add("A10", "설치선형열관류율.하부.Ψg,buttom.[W/m·K]");


            string[][] User_WinInstall = Program.DB.getValue(DB.type.ProjDB, "User_WindowInstall", "번호,DB유형,제품명,구분1,구분2,구분3,구분4,상부설치선형열관류율,측면설치선형열관류율,하부설치선형열관류율", "구분1 = '" + InstallType + "'");
            if (User_WinInstall.Length > 0)
            {
                for (int n = 0; n < User_WinInstall.Length; n++)
                {
                    Install_dataGridView.Rows.Add();
                    int nRow = Install_dataGridView.Rows.Count - 1;
                    for (int k = 0; k < 10; k++)
                    {
                        Install_dataGridView.Rows[nRow].Cells[k + 1].Value = User_WinInstall[n][k];
                    }

                }
            }

            if (InstallType != null && SingleDoubleType != null && FrameMaterial != null)
            {
                WinInstall = Program.DB.getValue(DB.type.BaseDB_HCneed, "창호설치열교", "번호,DB유형,제품명,구분1,구분2,구분3,구분4,상부설치선형열관류율,측면설치선형열관류율,하부설치선형열관류율", "구분1 = '" + InstallType + "'AND 구분2 = '" + FrameMaterial + "'AND 구분3 ='" + SingleDoubleType + "'");
            }
            else
            {
                WinInstall = Program.DB.getValue(DB.type.BaseDB_HCneed, "창호설치열교", "번호,DB유형,제품명,구분1,구분2,구분3,구분4,상부설치선형열관류율,측면설치선형열관류율,하부설치선형열관류율", "구분1 = '" + InstallType + "'");
            }
            if(WinInstall.Length > 0)
            {
                for (int n = 0; n < WinInstall.Length; n++)
                {
                    Install_dataGridView.Rows.Add();
                    int nRow = Install_dataGridView.Rows.Count - 1;
                    for (int k = 0; k < 10; k++)
                    {
                        Install_dataGridView.Rows[nRow].Cells[k + 1].Value = WinInstall[n][k];
                    }

                }
            }           
            Count_InstallDB = WinInstall.Length;
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

        private void UserDBType4_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UserDBType4 = UserDBType4_comboBox.SelectedItem.ToString();
        }

        private void UserDB_Psi_InstallTop_TextChanged(object sender, EventArgs e)
        {
            UserDB_Psi_InstallTop = Convert.ToDouble(UserDB_Psi_InstallTop_textBox.Text);
        }

        private void UserDB_Psi_InstallSide_TextChanged(object sender, EventArgs e)
        {
            UserDB_Psi_InstallSide = Convert.ToDouble(UserDB_Psi_InstallSide_textBox.Text);
        }

        private void UserDB_Psi_InstallButtom_TextChanged(object sender, EventArgs e)
        {
            UserDB_Psi_InstallButtom = Convert.ToDouble(UserDB_Psi_InstallButtom_textBox.Text);
        }

        private void AddUserDB_button_Click(object sender, EventArgs e)
        {
            if (UserDBName != null && UserDBType2 != null && UserDBType3 != null && UserDBType4 != null && UserDB_Psi_InstallTop != 0 && UserDB_Psi_InstallSide != 0 && UserDB_Psi_InstallButtom != 0)
            {
                string[][] 프로젝트유형 = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "프로젝트유형번호");
                Program.DB.setValue(DB.type.ProjDB, "User_WindowInstall", "번호,프로젝트유형,DB유형,제품명,구분1,구분2,구분3,구분4,상부설치선형열관류율,측면설치선형열관류율,하부설치선형열관류율",
                    "'" + UserNum + "','" + 프로젝트유형[0][0] + "','" + "사용자" + "','" + UserDBName + "','" + UserDBType1 + "','" + UserDBType2 + "','" + UserDBType3 + "','" + UserDBType4 + "','" + UserDB_Psi_InstallTop.ToString() + "','" + UserDB_Psi_InstallSide.ToString() + "','" + UserDB_Psi_InstallSide.ToString() + "'", "번호");
                load_table_InstallDB();
            }
            else
            {
                MessageBox.Show("모든 값을 입력해주세요.");
            }

        }

        private void Deletebutton_Click(object sender, EventArgs e)
        {
            int k = Install_dataGridView.CurrentCell.RowIndex;
            if (k > -1)
            {
                if (Install_dataGridView.Rows[k].Cells[2].Value.ToString() == "사용자")
                {
                    if ((MessageBox.Show(Install_dataGridView.Rows[k].Cells[3].Value.ToString() + "을 삭제하시겠습니까?", "삭제 확인", MessageBoxButtons.YesNo) == DialogResult.Yes))
                    {
                        String Delete_Num = Install_dataGridView.Rows[k].Cells[1].Value.ToString();
                        Program.DB.deleteValue(DB.type.ProjDB, "User_WindowInstall", "번호 ='" + Delete_Num + "'");
                        load_table_InstallDB();
                    }
                }
                else
                {
                    MessageBox.Show("기본 DB는 삭제할 수 없습니다.");
                }
            }

        }
        //데이터그리드뷰 체크박스 선택 시
        private void Install_dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                Install_dataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
                SelectRow = e.RowIndex;               
            }
        }

        private void Save_button_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = Install_dataGridView.Rows[SelectRow];
            for (int i = 1; i < row.Cells.Count - 2; i++)
            {
                Select_WindowInstall[i] = row.Cells[i + 2].Value.ToString();
            }
            Select_WindowInstall[0] = row.Cells[1].Value.ToString();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
