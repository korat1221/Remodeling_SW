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
        int PVInverter_SelectRow, SelectRow;
        string 프로젝트유형;
        public string SelectInverter, SelectInverterEff;

        public PV_InverterDB()
        {
            InitializeComponent(); this.Font = new Font(UTIL.Families[0], 9.75F, FontStyle.Regular);

            string[][] Image = Program.DB.getValue(DB.type.BaseDB_HCneed, "메뉴아이콘", "하위메뉴아이콘", "하위메뉴명 = '태양광시스템'");
            if (Image.Length > 0)
            {
                pictureBox1.Load(Program.gPath + Image[0][0]);
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            }

            string[][] value = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "프로젝트유형번호");
            프로젝트유형 = value[0][0];

            load_table_PVInverterDB();

            //번호
            //UserNum = Program.UTIL.CreateNum("User_PVInverter", "번호", "UIV_0");
            //UserNum_textBox.Text = UserNum;
        }

        void load_table_PVInverterDB()
        {

            new StackedHeaderDecorator(PVInverter_dataGridView, DataGridViewAutoSizeColumnsMode.Fill);
            DataGridViewCheckBoxColumn checkBox = new DataGridViewCheckBoxColumn();
            PVInverter_dataGridView.Columns.Clear();

            checkBox.HeaderText = "선택";
            checkBox.Name = "check";
            PVInverter_dataGridView.Columns.Add(checkBox);


            PVInverter_dataGridView.Columns.Add("A1", "번호");
            PVInverter_dataGridView.Columns.Add("A2", "DB유형");
            PVInverter_dataGridView.Columns.Add("A3", "제품명");
            PVInverter_dataGridView.Columns.Add("A4", "제조사");
            PVInverter_dataGridView.Columns.Add("A5", "효율");

            //사용자 DB
            string[][] User_PVInverter = Program.DB.getValue(DB.type.ProjDB, "User_PVInverter", "번호,DB유형,제품명,제조사,효율", "");
            if (User_PVInverter.Length > 0)
            {
                for (int n = 0; n < User_PVInverter.Length; n++)
                {
                    PVInverter_dataGridView.Rows.Add();
                    int nRow = PVInverter_dataGridView.Rows.Count - 1;
                    PVInverter_dataGridView.Rows[nRow].Cells[1].Value = User_PVInverter[n][0];
                    PVInverter_dataGridView.Rows[nRow].Cells[2].Value = User_PVInverter[n][1];
                    PVInverter_dataGridView.Rows[nRow].Cells[3].Value = User_PVInverter[n][2];
                    PVInverter_dataGridView.Rows[nRow].Cells[4].Value = User_PVInverter[n][3];
                    PVInverter_dataGridView.Rows[nRow].Cells[5].Value = User_PVInverter[n][4];
                }
            }

            //표준 DB 
            string[][] PVInverter = Program.DB.getValue(DB.type.BaseDB_RESystem, "태양광인버터DB", "번호,DB유형,제품명,제조사,효율", "");
            if (PVInverter.Length > 0)
            {
                for (int n = 0; n < PVInverter.Length; n++)
                {
                    PVInverter_dataGridView.Rows.Add();
                    int nRow = PVInverter_dataGridView.Rows.Count - 1;
                    PVInverter_dataGridView.Rows[nRow].Cells[1].Value = PVInverter[n][0];
                    PVInverter_dataGridView.Rows[nRow].Cells[2].Value = PVInverter[n][1];
                    PVInverter_dataGridView.Rows[nRow].Cells[3].Value = PVInverter[n][2];
                    PVInverter_dataGridView.Rows[nRow].Cells[4].Value = PVInverter[n][3];
                    PVInverter_dataGridView.Rows[nRow].Cells[5].Value = PVInverter[n][4];
                }
            }
        }

        private void AddUserDB_button_Click(object sender, EventArgs e)
        {
            string UserNum = Program.UTIL.CreateNum("User_PVInverter", "번호", "UIV_0");
            Program.DB.setValue(DB.type.ProjDB, "User_PVInverter", "번호,프로젝트유형,DB유형",
                    "'" + UserNum + "','" + 프로젝트유형 + "','" + "사용자" + "'", "번호");
            load_table_PVInverterDB();
            
        }

        private void Deletebutton_Click(object sender, EventArgs e)
        {
            string DeletNum = PVInverter_dataGridView.Rows[PVInverter_SelectRow].Cells[1].Value.ToString();
            Program.DB.deleteValue(DB.type.ProjDB, "User_PVInverter", "번호 ='" + DeletNum + "'");
            PVInverter_dataGridView.Rows.Remove(PVInverter_dataGridView.Rows[PVInverter_SelectRow]);
        }
        private void Save_button_Click(object sender, EventArgs e)
        {

            Save();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private bool SelectCheckBox()
        {
            foreach (DataGridViewRow row in PVInverter_dataGridView.Rows)
            {
                if (Convert.ToBoolean(row.Cells["check"].Value))
                {
                    row.DefaultCellStyle.SelectionBackColor = SystemColors.GradientInactiveCaption;
                    SelectRow = row.Index;
                }
            }
            return true;
        }
        void Save()
        {
            for (int i = 0; i < PVInverter_dataGridView.RowCount; i++)
            {
                if (PVInverter_dataGridView.Rows[i].Cells[2].Value.ToString() == "사용자")
                {
                    string[] value = new string[5];

                    for (int k = 1; k < 6; k++)
                    {

                        if (PVInverter_dataGridView.Rows[i].Cells[k].Value == null || PVInverter_dataGridView.Rows[i].Cells[k].Value == "")
                        {
                            MessageBox.Show("빈칸을 채워주세요");
                            return;
                        }
                        else
                        {
                            value[k - 1] = PVInverter_dataGridView.Rows[i].Cells[k].Value.ToString();
                        }
                    }
                    Program.DB.setValue(DB.type.ProjDB, "User_PVInverter", "번호,프로젝트유형,DB유형,제품명,제조사,효율",
                        "'" + value[0] + "','" + 프로젝트유형 + "','" + value[1] + "','" + value[2] + "','" + value[3] + "','" + value[4] + "'", "번호");
                    
                }
                else continue;
            }

            for (int i = 0; i < PVInverter_dataGridView.RowCount; i++)
            {
                if (SelectCheckBox())
                {
                    SelectInverter = PVInverter_dataGridView.Rows[SelectRow].Cells[1].Value.ToString();
                    SelectInverterEff = PVInverter_dataGridView.Rows[SelectRow].Cells[5].Value.ToString();
                }
                else return;
            }
        }


        private void PVInverter_dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                PVInverter_dataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
                PVInverter_SelectRow = e.RowIndex;
            }
        }
    }
}
