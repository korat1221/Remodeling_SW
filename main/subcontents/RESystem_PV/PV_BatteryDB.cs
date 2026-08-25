using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
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
        int PVBattery_SelectRow, SelectRow;
        string 프로젝트유형;
        public string SelectBattery, SelectBatteryCa, SelectBatteryType;

        public PV_BatteryDB()
        {
            InitializeComponent(); this.Font = new Font(UTIL.Families[0], 9.75F, FontStyle.Regular);
            Program.UTIL.IgnoreGridError(this);

            //이미지 만들기
            string[][] Image = Program.DB.getValue(DB.type.BaseDB_HCneed, "메뉴아이콘", "하위메뉴아이콘", "하위메뉴명 = '태양광시스템'");
            if (Image.Length > 0)
            {
                pictureBox1.Load(Program.gPath + Image[0][0]);
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            }

            string[][] value = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "프로젝트유형번호");
            프로젝트유형 = value[0][0];

            load_PVbatteryeDB();
        }

        void load_PVbatteryeDB()
        {
            new StackedHeaderDecorator(PVBattery_dataGridView, DataGridViewAutoSizeColumnsMode.Fill);
            DataGridViewCheckBoxColumn checkBox = new DataGridViewCheckBoxColumn();
            PVBattery_dataGridView.Columns.Clear();

            checkBox.HeaderText = "선택";
            checkBox.Name = "check";
            PVBattery_dataGridView.Columns.Add(checkBox);

            PVBattery_dataGridView.Columns.Add("A1", "번호");
            PVBattery_dataGridView.Columns.Add("A2", "DB유형");
            PVBattery_dataGridView.Columns.Add("A3", "제품명");
            PVBattery_dataGridView.Columns.Add("A4", "제조사");
            PVBattery_dataGridView.Columns.Add("A5", "정격전력.kW");

            DataGridViewComboBoxColumn BaT = new DataGridViewComboBoxColumn();
            BaT.HeaderText = "장치유형";
            BaT.Items.AddRange(new string[] { "리튬이온배터리", "니켈배터리", "납축배터리" });
            PVBattery_dataGridView.Columns.Add(BaT);

            PVBattery_dataGridView.Columns[0].Width = 40;
            PVBattery_dataGridView.Columns[6].Width = 140;


            //사용자 DB 추가
            string[][] User_PVBattery = Program.DB.getValue(DB.type.ProjDB, "User_PVBattery", "번호,DB유형,제품명,제조사,정격전력,배터리타입", "");
            if (User_PVBattery.Length > 0)
            {
                for (int n = 0; n < User_PVBattery.Length; n++)
                {
                    int nRow = PVBattery_dataGridView.Rows.Add();
                    PVBattery_dataGridView.Rows[nRow].Cells[1].Value = User_PVBattery[n][0];
                    PVBattery_dataGridView.Rows[nRow].Cells[2].Value = User_PVBattery[n][1];
                    PVBattery_dataGridView.Rows[nRow].Cells[3].Value = User_PVBattery[n][2];
                    PVBattery_dataGridView.Rows[nRow].Cells[4].Value = User_PVBattery[n][3];
                    PVBattery_dataGridView.Rows[nRow].Cells[5].Value = User_PVBattery[n][4];
                    PVBattery_dataGridView.Rows[nRow].Cells[6].Value = User_PVBattery[n][5];
                }
            }
        }


        private void PVBattery_dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                PVBattery_dataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
                PVBattery_SelectRow = e.RowIndex;
            }
        }

        private void AddUserDB_button_Click(object sender, EventArgs e)
        {
            string UserNum = Program.UTIL.CreateNum("User_PVBattery", "번호", "UBT_0");
            Program.DB.setValue(DB.type.ProjDB, "User_PVBattery", "번호,프로젝트유형,DB유형",
                    "'" + UserNum + "','" + 프로젝트유형 + "','" + "사용자" + "'", "번호");
            load_PVbatteryeDB();
            Program.DB.saveProject();
        }

        private void Deletebutton_Click(object sender, EventArgs e)
        {
            string DeletNum = PVBattery_dataGridView.Rows[PVBattery_SelectRow].Cells[1].Value.ToString();
            Program.DB.deleteValue(DB.type.ProjDB, "User_PVBattery", "번호 ='" + DeletNum + "'");
            PVBattery_dataGridView.Rows.Remove(PVBattery_dataGridView.Rows[PVBattery_SelectRow]);
        }



        private void Save_button_Click(object sender, EventArgs e)
        {
            if (Save())
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        bool Save()
        {
            if (SelectCheckBox())
            {
                SelectBattery = PVBattery_dataGridView.Rows[SelectRow].Cells[1].Value.ToString();
                SelectBatteryCa = PVBattery_dataGridView.Rows[SelectRow].Cells[5].Value.ToString();
                SelectBatteryType = PVBattery_dataGridView.Rows[SelectRow].Cells[6].Value.ToString();

                for (int i = 0; i < PVBattery_dataGridView.RowCount; i++)
                {
                    if (PVBattery_dataGridView.Rows[i].Cells[2].Value.ToString() == "사용자")
                    {
                        string[] value = new string[6];

                        for (int k = 0; k < 6; k++)
                        {

                            if (PVBattery_dataGridView.Rows[i].Cells[k+1].Value == null || PVBattery_dataGridView.Rows[i].Cells[k+1].Value == "")
                            {
                                MessageBox.Show("빈칸을 채워 주세요.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return false;
                            }
                            else
                            {
                                value[k] = PVBattery_dataGridView.Rows[i].Cells[k + 1].Value.ToString();
                            }
                        }
                        Program.DB.setValue(DB.type.ProjDB, "User_PVBattery", "번호,프로젝트유형,DB유형,제품명,제조사,정격전력,배터리타입",
                            "'" + value[0] + "','" + 프로젝트유형 + "','" + value[1] + "','" + value[2] + "','" + value[3] + "','" + value[4] + "','" + value[5] + "'", "번호");
                        Program.DB.saveProject();
                    }
                }
                return true;
            }
            else
            {
                MessageBox.Show("배터리를 선택해 주세요.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
        }
        private bool SelectCheckBox()
        {
            SelectRow = 100;
            foreach (DataGridViewRow row in PVBattery_dataGridView.Rows)
            {
                if (Convert.ToBoolean(row.Cells["check"].Value))
                {
                    row.DefaultCellStyle.SelectionBackColor = SystemColors.GradientInactiveCaption;
                    SelectRow = row.Index;
                }
            }
            if (SelectRow == 100)
            {
                return false;
            }
            else return true;
        }

        private void PVBattery_dataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            double k = 0;
            if (e.ColumnIndex == 5) 
            {
                if(Program.UTIL.data_inputcheck(PVBattery_dataGridView, e.RowIndex, 5, 1))
                {
                    k = 1;
                }
            }
        }
    }
}
