using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace main.subcontents.RESystem_PV
{
    public partial class PV_BatteryDB : Form
    {
        double Count_PVBatteryDB;
        int SelectRow;
        public String[] Select_PVBattery = new string[7];
        String UserNum, UserDB_Name, UserDB_Manufacture, UserDB_type;
        double UserDB_V, UserDB_Ah;


        public PV_BatteryDB()
        {
            InitializeComponent();
            load_table_PVbatteryeDB();

            //type combobox
            BatteryType_Combobox.Items.Add("리튬 및 리튬 결합");
            BatteryType_Combobox.Items.Add("니켈-철");
            BatteryType_Combobox.Items.Add("납 및 납젤");

            //번호
            UserNum = Program.UTIL.CreateNum("User_PVBattery", "번호", "UVT_0");
            UserNum_textBox.Text = UserNum;
        }

        void load_table_PVbatteryeDB()
        {
            //데이터 그리드뷰 만들기
            DataTable table_PVBattery = new DataTable();
            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
            PVBattery_dataGridView.Columns.Clear();
            checkBoxColumn.HeaderText = "선택";
            checkBoxColumn.Name = "check";
            PVBattery_dataGridView.Columns.Add(checkBoxColumn);
            table_PVBattery.Columns.Add("번호", typeof(string));
            table_PVBattery.Columns.Add("DB유형", typeof(string));
            table_PVBattery.Columns.Add("제품명", typeof(string));
            table_PVBattery.Columns.Add("제조사", typeof(string));
            table_PVBattery.Columns.Add("전력" + Environment.NewLine + "V", typeof(string));
            table_PVBattery.Columns.Add("암페어시" + Environment.NewLine + "Ah", typeof(string));
            table_PVBattery.Columns.Add("배터리타입", typeof(string));

            //사용자 DB 추가
            try
            {
                string[][] User_PVBattery = Program.DB.getValue(DB.type.ProjDB, "User_PVBattery", "번호,DB유형,제품명,제조사,전력,암페어시,배터리타입", "");
                for (int n = 0; n < User_PVBattery.Length; n++)
                {
                    table_PVBattery.Rows.Add(User_PVBattery[n][0], User_PVBattery[n][1], User_PVBattery[n][2], User_PVBattery[n][3], User_PVBattery[n][4], User_PVBattery[n][5], User_PVBattery[n][6]);
                }

                PVBattery_dataGridView.DataSource = table_PVBattery;
                Count_PVBatteryDB = User_PVBattery.Length;
            }
            catch { }

            ////표준 DB 불러오기
            //string[][] PVBattery = Program.DB.getValue(DB.type.BaseDB_RESystem, "태양광배터리DB", "번호,DB유형,제품명,제조사,전력,암페어시,배터리타입", "");

            //for (int n = 0; n < PVBattery.Length; n++)
            //{
            //    table_PVBattery.Rows.Add(PVBattery[n][0], PVBattery[n][1], PVBattery[n][2], PVBattery[n][3], String.Format("{0:F2}", Convert.ToDouble(PVBattery[n][4])), String.Format("{0:F2}", Convert.ToDouble(PVBattery[n][5])), PVBattery[n][6]);
            //}
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void PVBattery_dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                PVBattery_dataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
                SelectRow = e.RowIndex;
                DataGridViewRow row = PVBattery_dataGridView.Rows[SelectRow];
                DataGridViewRow row2;
                for (int k = 0; k < Count_PVBatteryDB; k++)
                {
                    if (k != row.Index)
                    {
                        PVBattery_dataGridView.Rows[k].Cells[0].Value = false;
                        row2 = PVBattery_dataGridView.Rows[k];
                        row2.DefaultCellStyle.BackColor = Color.White;
                        row2.DefaultCellStyle.ForeColor = Color.Black;
                    }
                    else
                    {
                        row.DefaultCellStyle.BackColor = SystemColors.GradientInactiveCaption;
                        row.DefaultCellStyle.ForeColor = Color.Black;
                        row = PVBattery_dataGridView.Rows[e.RowIndex];
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
        private void BatteryType_Combobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UserDB_type = BatteryType_Combobox.SelectedItem.ToString();
        }

        private void UserDB_V_TextBox_TextChanged(object sender, EventArgs e)
        {
            int result;

            if (int.TryParse(UserDB_V_TextBox.Text, out result) == true)
            {
                UserDB_V = Convert.ToDouble(UserDB_V_TextBox.Text);
            }
            else
            {
                MessageBox.Show("숫자를 입력하세요.");
            }
        }

        private void UserDB_Ah_TextBox_TextChanged(object sender, EventArgs e)
        {
            int result;

            if (int.TryParse(UserDB_Ah_TextBox.Text, out result) == true)
            {
                UserDB_Ah = Convert.ToDouble(UserDB_Ah_TextBox.Text);
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
            if (UserDB_Name != null && UserDB_Manufacture != null && UserDB_type != null && UserDB_V != 0 && UserDB_Ah != 0)
            {
                Program.DB.setValue(DB.type.ProjDB, "User_PVBattery", "번호,프로젝트유형,DB유형,제품명,제조사,전력,암페어시,배터리타입",
                    "'" + UserNum + "','" + 프로젝트유형[0][0] + "','" + "사용자" + "','" + UserDB_Name + "','" + UserDB_Manufacture + "','" + UserDB_V.ToString() + "','" + UserDB_Ah.ToString() + "','" + UserDB_type + "'", "번호");
                load_table_PVbatteryeDB();
            }
            else
            {
                MessageBox.Show("모든 값을 입력해주세요.");
            }
        }

        private void Deletebutton_Click(object sender, EventArgs e)
        {
            int k = PVBattery_dataGridView.CurrentCell.RowIndex;
            if (k > -1)
            {
                if (PVBattery_dataGridView.Rows[k].Cells[2].Value.ToString() == "사용자")
                {
                    if ((MessageBox.Show(PVBattery_dataGridView.Rows[k].Cells[3].Value.ToString() + "을 삭제하시겠습니까?", "삭제 확인", MessageBoxButtons.YesNo) == DialogResult.Yes))
                    {
                        String Delete_Num = PVBattery_dataGridView.Rows[k].Cells[1].Value.ToString();
                        Program.DB.deleteValue(DB.type.ProjDB, "User_PVBattery", "번호 ='" + Delete_Num + "'");
                        load_table_PVbatteryeDB();
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
            // 번호,DB유형,제품명,제조사,전력,암페어시,배터리타입
            DataGridViewRow row = PVBattery_dataGridView.Rows[SelectRow];
            Select_PVBattery[0] = row.Cells[1].Value.ToString(); //번호
            Select_PVBattery[1] = row.Cells[2].Value.ToString(); //DB유형
            Select_PVBattery[2] = row.Cells[3].Value.ToString(); //제품명
            Select_PVBattery[3] = row.Cells[4].Value.ToString(); //제조사
            Select_PVBattery[4] = row.Cells[5].Value.ToString(); //전력
            Select_PVBattery[5] = row.Cells[6].Value.ToString(); //암페어시
            Select_PVBattery[6] = row.Cells[7].Value.ToString(); //배터리타입

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
