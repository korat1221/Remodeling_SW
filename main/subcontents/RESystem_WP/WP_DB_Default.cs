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
using static System.ComponentModel.Design.ObjectSelectorEditor;

namespace main.subcontents.RESystem_WP
{
    public partial class WP_DB : Form
    {
        
        double Count_WPDB;
        public string SelectWP;
        int SelectRow;
        public String[] Select_WP = new string[15];
       
        string DefaultUse;

        public WP_DB(string defaultUse)
        {
            InitializeComponent(); this.Font = new Font(UTIL.Families[0], 9.75F, FontStyle.Regular);

            
            //pictureBox1.Load(Program.gPath + @"\images\" + @"\WindPower\" + @"image (6)" + @".png");
            pictureBox5.Load(Program.gPath + @"\images\" + @"\WindPower\" + @"image (8)" + @".png");
            pictureBox2.Load(Program.gPath + @"\images\" + @"\WindPower\" + @"image (7)" + @".png");
            pictureBox3.Load(Program.gPath + @"\images\" + @"\WindPower\" + @"image (1)" + @".png");
            pictureBox4.Load(Program.gPath + @"\images\" + @"\WindPower\" + @"image (2)" + @".png");
            pictureBox6.Load(Program.gPath + @"\images\" + @"\WindPower\" + @"image (3)" + @".png");
            pictureBox7.Load(Program.gPath + @"\images\" + @"\WindPower\" + @"image (4)" + @".png");
            pictureBox8.Load(Program.gPath + @"\images\" + @"\WindPower\" + @"image (5)" + @".png");

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
            WP_dataGridView.Columns.Add("A7", "정격출력.[Kw]");
            WP_dataGridView.Columns.Add("A8", "회전면적.[m"+Program.UTIL.Subscript(2, true)+"]");
            WP_dataGridView.Columns.Add("A9", "허브높이.[m]");
            WP_dataGridView.Columns.Add("A10", "시동풍속.[m/s]");
            WP_dataGridView.Columns.Add("A11", "최적풍속.[m/s]");
            WP_dataGridView.Columns.Add("A12", "종단풍속.[m/s]");
            WP_dataGridView.Columns.Add("A13", "전력계수.시동풍속.[-]");
            WP_dataGridView.Columns.Add("A14", "전력계수.최적풍속.[-]");
            WP_dataGridView.Columns.Add("A15", "전력계수.종단풍속.[-]");
            WP_dataGridView.Columns[0].Width = 40;
            WP_dataGridView.Columns[4].Visible = false;


            string[][] WP = Program.DB.getValue(DB.type.BaseDB_RESystem, "풍력DB", "번호,DB유형,제품명,제조사,타입,세부타입,정격출력,회전면적,허브높이,시동풍속,최적풍속,종단풍속,시동풍속전력계수,최적풍속전력계수,종단풍속전력계수", "");
            for (int n = 0; n < WP.Length; n++)
            {
                WP_dataGridView.Rows.Add();
                WP_dataGridView.Rows[n].Cells[1].Value = WP[n][0];
                WP_dataGridView.Rows[n].Cells[2].Value = WP[n][1];
                WP_dataGridView.Rows[n].Cells[3].Value = WP[n][2];
                WP_dataGridView.Rows[n].Cells[4].Value = WP[n][3];
                WP_dataGridView.Rows[n].Cells[5].Value = WP[n][4];
                WP_dataGridView.Rows[n].Cells[6].Value = WP[n][5];
                WP_dataGridView.Rows[n].Cells[7].Value = WP[n][6];
                WP_dataGridView.Rows[n].Cells[8].Value = WP[n][7];
                WP_dataGridView.Rows[n].Cells[9].Value = WP[n][8];
                WP_dataGridView.Rows[n].Cells[10].Value = WP[n][9];
                WP_dataGridView.Rows[n].Cells[11].Value = WP[n][10];
                WP_dataGridView.Rows[n].Cells[12].Value = WP[n][11];
                WP_dataGridView.Rows[n].Cells[13].Value = WP[n][12];
                WP_dataGridView.Rows[n].Cells[14].Value = WP[n][13];
                WP_dataGridView.Rows[n].Cells[15].Value = WP[n][14];
            }
        }

        private void WP_dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                WP_dataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
                SelectRow = e.RowIndex;
                DataGridViewRow row = WP_dataGridView.Rows[SelectRow];
                DataGridViewRow row2;
                for (int k = 0; k < Count_WPDB; k++)
                {
                    if (k != row.Index)
                    {
                        WP_dataGridView.Rows[k].Cells[0].Value = false;
                        row2 = WP_dataGridView.Rows[k];
                        row2.DefaultCellStyle.BackColor = Color.White;
                        row2.DefaultCellStyle.ForeColor = Color.Black;
                    }
                    else
                    {
                        row.DefaultCellStyle.BackColor = SystemColors.GradientInactiveCaption;
                        row.DefaultCellStyle.ForeColor = Color.Black;
                        row = WP_dataGridView.Rows[e.RowIndex];
                    }
                }
            }

        }

        private void Save_button_Click(object sender, EventArgs e)
        {

            // 번호,DB유형,제품명,제조사,타입,세부타입,정격출력,회전면적,허브높이,시동풍속,최적풍속,종단풍속,시동풍속전력계수,최적풍속전력계수,종단풍속전력계수
            DataGridViewRow row = WP_dataGridView.Rows[SelectRow];

            Select_WP[0] = row.Cells[1].Value.ToString(); //번호
            Select_WP[1] = row.Cells[2].Value.ToString(); //DB유형
            Select_WP[2] = row.Cells[3].Value.ToString(); //제품명
            Select_WP[3] = row.Cells[4].Value.ToString(); //제조사
            Select_WP[4] = row.Cells[5].Value.ToString();
            Select_WP[5] = row.Cells[6].Value.ToString();
            Select_WP[6] = row.Cells[7].Value.ToString();
            Select_WP[7] = row.Cells[8].Value.ToString();
            Select_WP[8] = row.Cells[9].Value.ToString();
            Select_WP[9] = row.Cells[10].Value.ToString();
            Select_WP[10] = row.Cells[11].Value.ToString();
            Select_WP[11] = row.Cells[12].Value.ToString();
            Select_WP[12] = row.Cells[13].Value.ToString();
            Select_WP[13] = row.Cells[14].Value.ToString();
            Select_WP[14] = row.Cells[15].Value.ToString();

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
