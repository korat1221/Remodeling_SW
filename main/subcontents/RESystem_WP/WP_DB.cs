using main.subcontents.RESystem_WP;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.WebRequestMethods;

namespace main.subcontents
{
    public partial class WP_DB : Form
    {
        String DefaultUse;
        public string SelectWP2; //장비일람표에서 선택
        public string SelectWP2_DB; // 풍력에서 선택

        public WP_DB(String defaultUse)
        {
            InitializeComponent();
            this.DefaultUse = defaultUse;

            string[][] Image = Program.DB.getValue(DB.type.BaseDB_HCneed, "메뉴아이콘", "하위메뉴아이콘", "하위메뉴명 = '풍력시스템'");

            
            if (Image.Length > 0)
            {
                Icon_pictureBox.Load(Program.gPath + Image[0][0]);
                Icon_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            }

            load_table_DB(DefaultUse);

        }

        private void load_table_DB(string _defaultuse)   
        {
            tableMake(DefaultUse);
            if (DefaultUse == "기본DB 적용")
            {   
                string[][] Value = Program.DB.getValue(DB.type.BaseDB_RESystem, "풍력DB", "번호,DB유형,제품명,제조사,타입,세부타입,정격출력,회전면적,허브높이,시동풍속,최적풍속,종단풍속,시동풍속전력계수,최적풍속전력계수,종단풍속전력계수","");
                if (Value.Length > 0)
                {
                    for (int i = 0; i < Value.Length; i++)
                    {
                        WP_dataGridView.Rows.Add();
                        int n = WP_dataGridView.Rows.Count - 1;
                        WP_dataGridView.Rows[n].Cells[1].Value = Value[0][0];
                        WP_dataGridView.Rows[n].Cells[2].Value = Value[0][1];
                        WP_dataGridView.Rows[n].Cells[3].Value = Value[0][2];
                        WP_dataGridView.Rows[n].Cells[4].Value = Value[0][3];
                        WP_dataGridView.Rows[n].Cells[5].Value = Value[0][4];
                        WP_dataGridView.Rows[n].Cells[6].Value = Value[0][5];
                        WP_dataGridView.Rows[n].Cells[7].Value = Value[0][6];
                        WP_dataGridView.Rows[n].Cells[8].Value = Value[0][7];
                        WP_dataGridView.Rows[n].Cells[9].Value = Value[0][8];
                        WP_dataGridView.Rows[n].Cells[10].Value = Value[0][9];
                        WP_dataGridView.Rows[n].Cells[11].Value = Value[0][10];
                        WP_dataGridView.Rows[n].Cells[12].Value = Value[0][11];
                        WP_dataGridView.Rows[n].Cells[13].Value = Value[0][12];
                        WP_dataGridView.Rows[n].Cells[14].Value = Value[0][13];
                        WP_dataGridView.Rows[n].Cells[15].Value = Value[0][14];
                    }
                }

            }
            else if (DefaultUse == "장비일람표 DB")
            {
                string[][] Value = Program.DB.getValue(DB.type.ProjDB, "User_WP", "번호,DB유형,제품명,제조사,타입,세부타입,정격출력,회전면적,허브높이,시동풍속,최적풍속,종단풍속,시동풍속전력계수,최적풍속전력계수,종단풍속전력계수,신규기존", "");
                string[][] check = Program.DB.getValue(DB.type.ProjDB, "FuelCell_Form", "연료전지", ""); //////////////수정
                if (Value.Length > 0)
                {
                    for (int i = 0; i < Value.Length; i++)
                    {
                        WP_dataGridView.Rows.Add();
                        int n = WP_dataGridView.Rows.Count - 1;
                        for (int k = 0; k < check.Length; k++)
                        {
                            if (Value[i][0] == check[k][0])
                            {
                                WP_dataGridView.Rows[n].Cells[0].Value = true;
                                break;
                            }
                        }
                        WP_dataGridView.Rows[n].Cells[1].Value = Value[0][0];
                        WP_dataGridView.Rows[n].Cells[2].Value = Value[0][1];
                        WP_dataGridView.Rows[n].Cells[3].Value = Value[0][2];
                        WP_dataGridView.Rows[n].Cells[4].Value = Value[0][3];
                        WP_dataGridView.Rows[n].Cells[5].Value = Value[0][4];
                        WP_dataGridView.Rows[n].Cells[6].Value = Value[0][5];
                        WP_dataGridView.Rows[n].Cells[7].Value = Value[0][6];
                        WP_dataGridView.Rows[n].Cells[8].Value = Value[0][7];
                        WP_dataGridView.Rows[n].Cells[9].Value = Value[0][8];
                        WP_dataGridView.Rows[n].Cells[10].Value = Value[0][9];
                        WP_dataGridView.Rows[n].Cells[11].Value = Value[0][10];
                        WP_dataGridView.Rows[n].Cells[12].Value = Value[0][11];
                        WP_dataGridView.Rows[n].Cells[13].Value = Value[0][12];
                        WP_dataGridView.Rows[n].Cells[14].Value = Value[0][13];
                        WP_dataGridView.Rows[n].Cells[15].Value = Value[0][14];
                        WP_dataGridView.Rows[n].Cells[16].Value = Value[0][15];

                    }
                }
            }
        }
        private void tableMake(string _DefaultUse)
        {
            new StackedHeaderDecorator(WP_dataGridView, DataGridViewAutoSizeColumnsMode.Fill, datagridviewDesign);
            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
            WP_dataGridView.Columns.Clear();
            checkBoxColumn.HeaderText = "선택";
            checkBoxColumn.Name = "check";
            WP_dataGridView.Columns.Add(checkBoxColumn);
            WP_dataGridView.Columns.Add("A1", "번호");

            //굳이 다르게 할 이유가 없을 거 같은데
            //////////////수정
            if (DefaultUse == "기본DB 적용")
            {
                WP_dataGridView.Columns.Add("A2", "DB유형");
                WP_dataGridView.Columns.Add("A3", "제품명");
                WP_dataGridView.Columns.Add("A4", "제조사");
                WP_dataGridView.Columns.Add("A5", "타입");
                WP_dataGridView.Columns.Add("A6", "세부타입");
                WP_dataGridView.Columns.Add("A7", "정격출력");
                WP_dataGridView.Columns.Add("A8", "회전면적");
                WP_dataGridView.Columns.Add("A9", "허브높이");
                WP_dataGridView.Columns.Add("A10", "시동풍속");
                WP_dataGridView.Columns.Add("A11", "최적풍속");
                WP_dataGridView.Columns.Add("A12", "종단풍속");
                WP_dataGridView.Columns.Add("A13", "시동풍속전력계수");
                WP_dataGridView.Columns.Add("A14", "최적풍속전력계수");
                WP_dataGridView.Columns.Add("A15", "종단풍속전력계수");
            }

            else if (DefaultUse == "장비일람표 DB")
            {
                WP_dataGridView.Columns.Add("A2", "DB유형");
                WP_dataGridView.Columns.Add("A3", "제품명");
                WP_dataGridView.Columns.Add("A4", "제조사");
                WP_dataGridView.Columns.Add("A5", "타입");
                WP_dataGridView.Columns.Add("A6", "세부타입");
                WP_dataGridView.Columns.Add("A7", "정격출력");
                WP_dataGridView.Columns.Add("A8", "회전면적");
                WP_dataGridView.Columns.Add("A9", "허브높이");
                WP_dataGridView.Columns.Add("A10", "시동풍속");
                WP_dataGridView.Columns.Add("A11", "최적풍속");
                WP_dataGridView.Columns.Add("A12", "종단풍속");
                WP_dataGridView.Columns.Add("A13", "시동풍속전력계수");
                WP_dataGridView.Columns.Add("A14", "최적풍속전력계수");
                WP_dataGridView.Columns.Add("A15", "종단풍속전력계수");
                WP_dataGridView.Columns.Add("A16", "설치");
            }
        }
        private Boolean datagridviewDesign(DataGridViewCell cell, int column, int row)
        {
           
            if (row % 2 == 1)
            {
                cell.Style.BackColor = SystemColors.InactiveBorder;
                cell.Style.ForeColor = Color.Black;
                cell.Style.SelectionBackColor = SystemColors.InactiveBorder;
                cell.Style.SelectionForeColor = Color.Black;
                return true;
            }
            else
            {
                cell.Style.BackColor = Color.FromArgb(255, 255, 255);
                cell.Style.ForeColor = Color.Black;
                cell.Style.SelectionBackColor = Color.FromArgb(255, 255, 255);
                cell.Style.SelectionForeColor = Color.Black;
                return true;
            }

        }

        private void SelectCheckBox()
        {
            foreach (DataGridViewRow row in WP_dataGridView.Rows)
            {
                if (Convert.ToBoolean(row.Cells["check"].Value))
                {
                    row.DefaultCellStyle.SelectionBackColor = SystemColors.GradientInactiveCaption;
                    SelectWP2_DB = row.Cells[1].Value.ToString();
                }
            }
        }


        private void Save_button_Click(object sender, EventArgs e)
        {
            SelectWP2_DB = null;
            SelectCheckBox();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        public void reset()
        {
            SelectWP2_DB = null;

            for (int n = 0; n < WP_dataGridView.Rows.Count; n++)
            {
                WP_dataGridView.Rows[n].Cells[0].Value = false;
            }
        }
    }
}
