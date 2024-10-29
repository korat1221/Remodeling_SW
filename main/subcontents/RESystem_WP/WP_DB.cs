using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace main.subcontents.RESystem_WP
{
    public partial class WP_DB : Form
    {

        double Count_PVModuleDB;
        int SelectRow;
        public String[] Select_WP = new string[10];
        string DefaultUse;

        public WP_DB(string defaultUse)
        {
            InitializeComponent(); this.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular);
            DefaultUse = defaultUse;
            load_table_WPDB();

        }

        void load_table_WPDB()
        {
            //데이터 그리드뷰 만들기
            new StackedHeaderDecorator(WP_dataGridView, DataGridViewAutoSizeColumnsMode.Fill);
            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
            WP_dataGridView.Columns.Clear();
            checkBoxColumn.HeaderText = "선택";
            checkBoxColumn.Name = "check";
            WP_dataGridView.Columns.Add(checkBoxColumn);
            WP_dataGridView.Columns.Add("A1", "번호");
            WP_dataGridView.Columns.Add("A2", "DB유형");
            WP_dataGridView.Columns.Add("A3", "제품명");
            WP_dataGridView.Columns.Add("A4", "제조사");
            WP_dataGridView.Columns.Add("A5", "타입");
            WP_dataGridView.Columns.Add("A6", "세부타입");
            WP_dataGridView.Columns.Add("A7", "정격출력");
            WP_dataGridView.Columns.Add("A8", "회전면적");
            WP_dataGridView.Columns.Add("A9", "허브높이");
            WP_dataGridView.Columns.Add("A10", "시동풍속");
            WP_dataGridView.Columns.Add("A11", "종단풍속");
            WP_dataGridView.Columns.Add("A12", "최적풍속");
            WP_dataGridView.Columns.Add("A13", "전력계수.시동풍속.Cp,min");
            WP_dataGridView.Columns.Add("A14", "전력계수.최적풍속.Cp,op");
            WP_dataGridView.Columns.Add("A15", "전력계수.종단풍속.Cp,max");


            //if (DefaultUse == "기본DB 적용")
            //{
            //    string[][] WP = Program.DB.getValue(DB.type.BaseDB_RESystem, "태양광모듈DB", "번호,DB유형,제품명,제조사,제작년도,CELLTYPE,Kpk", "");
            //    for (int n = 0; n < WP.Length; n++)
            //    {
            //        WP_dataGridView.Rows.Add();
            //        WP_dataGridView.Rows[n].Cells[1].Value = WP[n][0];
            //        WP_dataGridView.Rows[n].Cells[2].Value = WP[n][1];
            //        WP_dataGridView.Rows[n].Cells[3].Value = WP[n][2];
            //        WP_dataGridView.Rows[n].Cells[4].Value = WP[n][3];
            //        WP_dataGridView.Rows[n].Cells[5].Value = WP[n][4];
            //        WP_dataGridView.Rows[n].Cells[6].Value = WP[n][5];
            //        WP_dataGridView.Rows[n].Cells[7].Value = WP[n][6];
            //    }
            //}

            //사용자 DB 추가
            try
            {
                string[][] User_WP = Program.DB.getValue(DB.type.ProjDB, "User_WP", "번호,DB유형,제품명,제조사,제작년도,CELLTYPE,Kpk,가로길이,세로길이,정격출력", "");
                for (int n = 0; n < User_WP.Length; n++)
                {
                    //table_WP.Rows.Add(User_WP[n][0], User_WP[n][1], User_WP[n][2], User_WP[n][3], User_WP[n][4], User_WP[n][5], User_WP[n][6], User_WP[n][7], User_WP[n][8], User_WP[n][9]);
                }
            }
            catch { }
        }
        private void WP_DB_Load(object sender, EventArgs e)
        {

        }

        private void WP_dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
