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

namespace main.subcontents
{
    public partial class FC_DB : Form
    {
        double Count_FCDB;
        int SelectRow;
        public String[] Select_FC = new string[9];
        string DefaultUse;
        public FC_DB(string defaultUse)
        {
            InitializeComponent();
            DefaultUse = defaultUse;
            load_tableFCDB();
        }

        void load_tableFCDB()
        {            //데이터 그리드뷰 만들기
            new StackedHeaderDecorator(FC_dataGridView, DataGridViewAutoSizeColumnsMode.Fill);
            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
            FC_dataGridView.Columns.Clear();
            checkBoxColumn.HeaderText = "선택";
            checkBoxColumn.Name = "check";
            FC_dataGridView.Columns.Add(checkBoxColumn);
            FC_dataGridView.Columns.Add("A1", "번호");
            FC_dataGridView.Columns.Add("A4", "제조사");
            FC_dataGridView.Columns.Add("A5", "연료전지종류");
            FC_dataGridView.Columns.Add("A7", "정격효율.[%]");
            FC_dataGridView.Columns.Add("A8", "발전효율.[%]");

            if (DefaultUse != "기본DB 적용")
            {
                FC_dataGridView.Columns.Add("A2", "DB유형");
                FC_dataGridView.Columns.Add("A3", "제품명");
                FC_dataGridView.Columns.Add("A6", "시스템출력.[kW]");
                FC_dataGridView.Columns.Add("A9", "축열탱크");
            }

            if (DefaultUse == "기본DB 적용")
            {
                string[][] FC = Program.DB.getValue(DB.type.BaseDB_RESystem, "연료전지DB", "번호,제조사,연료전지종류,정격효율,발전효율", "");
                for (int n = 0; n < FC.Length; n++)
                {
                    FC_dataGridView.Rows.Add();
                    FC_dataGridView.Rows[n].Cells[1].Value = FC[n][0];
                    FC_dataGridView.Rows[n].Cells[2].Value = FC[n][1];
                    FC_dataGridView.Rows[n].Cells[3].Value = FC[n][2];
                    FC_dataGridView.Rows[n].Cells[4].Value = FC[n][3];
                    FC_dataGridView.Rows[n].Cells[5].Value = FC[n][4];

                }
            }

            //사용자 DB 추가
            try
            {
                string[][] User_FC = Program.DB.getValue(DB.type.ProjDB, "User_FC", "번호,DB유형,제품명,제조사,연료전지종류,시스템출력,정격효율,발전효율,축열탱크", "");
                for (int n = 0; n < User_FC.Length; n++)
                {
                    //table_PVModule.Rows.Add(User_PVModule[n][0], User_PVModule[n][1], User_PVModule[n][2], User_PVModule[n][3], User_PVModule[n][4], User_PVModule[n][5], User_PVModule[n][6], User_PVModule[n][7], User_PVModule[n][8], User_PVModule[n][9]);
                }
            }
            catch { }

        }

        private void FC_dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                FC_dataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
                SelectRow = e.RowIndex;
                DataGridViewRow row = FC_dataGridView.Rows[SelectRow];
                DataGridViewRow row2;
                for (int k = 0; k < Count_FCDB; k++)
                {
                    if (k != row.Index)
                    {
                        FC_dataGridView.Rows[k].Cells[0].Value = false;
                        row2 = FC_dataGridView.Rows[k];
                        row2.DefaultCellStyle.BackColor = Color.White;
                        row2.DefaultCellStyle.ForeColor = Color.Black;
                    }
                    else
                    {
                        row.DefaultCellStyle.BackColor = SystemColors.GradientInactiveCaption;
                        row.DefaultCellStyle.ForeColor = Color.Black;
                        row = FC_dataGridView.Rows[e.RowIndex];
                    }
                }
            }

        }

        private void FC_DB_Load(object sender, EventArgs e)
        {

        }


        private void Save_button_Click(object sender, EventArgs e)
        {
            // 번호,DB유형,제품명,제조사,제작년도,CELLTYPE,Kpk,가로길이,세로길이,정격출력
            DataGridViewRow row = FC_dataGridView.Rows[SelectRow];
            Select_FC[0] = row.Cells[1].Value.ToString(); //번호
            //Select_FC[1] = row.Cells[2].Value.ToString(); //DB유형
            //Select_FC[2] = row.Cells[3].Value.ToString(); //제품명
            Select_FC[1] = row.Cells[2].Value.ToString(); //제조사
            Select_FC[2] = row.Cells[3].Value.ToString(); //연료전지종류
            //Select_FC[5] = row.Cells[6].Value.ToString(); //시스템출력
            Select_FC[3] = row.Cells[4].Value.ToString(); //정격효율
            Select_FC[4] = row.Cells[5].Value.ToString(); //발전효율
            //Select_FC[8] = row.Cells[9].Value.ToString(); //축열탱크

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void GeneralPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
