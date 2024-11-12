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
        public string SelectWPnonsplit;

        public WP_DB()
        {
            InitializeComponent();
            //this.DefaultUse = defaultUse;

            string[][] Image = Program.DB.getValue(DB.type.BaseDB_HCneed, "메뉴아이콘", "하위메뉴아이콘", "하위메뉴명 = '풍력시스템'");

            
            if (Image.Length > 0)
            {
                Icon_pictureBox.Load(Program.gPath + Image[0][0]);
                Icon_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            }

            load_table_DB();

        }

        private void load_table_DB()   
        {
            tableMake();
            
            string[][] Value = Program.DB.getValue(DB.type.ProjDB, "User_WP", "번호,DB유형,제품명,제조사,타입,세부타입,정격출력,회전면적,허브높이,시동풍속,최적풍속,종단풍속,시동풍속전력계수,최적풍속전력계수,종단풍속전력계수,신규기존", "");
                
              for (int i = 0; i < Value.Length; i++)
              {
                        WP_dataGridView.Rows.Add();
                        int n = WP_dataGridView.Rows.Count - 1;
               
                        WP_dataGridView.Rows[n].Cells[1].Value = Value[n][0];
                        WP_dataGridView.Rows[n].Cells[2].Value = Value[n][1];
                        WP_dataGridView.Rows[n].Cells[3].Value = Value[n][2];
                        WP_dataGridView.Rows[n].Cells[4].Value = Value[n][3];
                        WP_dataGridView.Rows[n].Cells[5].Value = Value[n][4];
                        WP_dataGridView.Rows[n].Cells[6].Value = Value[n][5];
                        WP_dataGridView.Rows[n].Cells[7].Value = Value[n][6];
                        WP_dataGridView.Rows[n].Cells[8].Value = Value[n][7];
                        WP_dataGridView.Rows[n].Cells[9].Value = Value[n][8];
                        WP_dataGridView.Rows[n].Cells[10].Value = Value[n][9];
                        WP_dataGridView.Rows[n].Cells[11].Value = Value[n][10];
                        WP_dataGridView.Rows[n].Cells[12].Value = Value[n][11];
                        WP_dataGridView.Rows[n].Cells[13].Value = Value[n][12];
                        WP_dataGridView.Rows[n].Cells[14].Value = Value[n][13];
                        WP_dataGridView.Rows[n].Cells[15].Value = Value[n][14];
                        WP_dataGridView.Rows[n].Cells[16].Value = Value[n][15];
              }
        }
        private void tableMake()
        {
            new StackedHeaderDecorator(WP_dataGridView, DataGridViewAutoSizeColumnsMode.Fill, datagridviewDesign);
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
            WP_dataGridView.Columns.Add("A8", "회전면적.[㎡]");
            WP_dataGridView.Columns.Add("A9", "허브높이.[m]");
            WP_dataGridView.Columns.Add("A10", "시동풍속.[m/s]");
            WP_dataGridView.Columns.Add("A11", "최적풍속.[m/s]");
            WP_dataGridView.Columns.Add("A12", "종단풍속.[m/s]");
            WP_dataGridView.Columns.Add("A13", "전력계수.시동풍속.[-]");
            WP_dataGridView.Columns.Add("A14", "전력계수.최적풍속.[-]");
            WP_dataGridView.Columns.Add("A15", "전력계수.종단풍속.[-]");
            WP_dataGridView.Columns.Add("A16", "설치");
            
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
                    SelectWPnonsplit= row.Cells[1].Value.ToString();
                }
            }
        }


        private void Save_button_Click(object sender, EventArgs e)
        {
            SelectWPnonsplit = null;
            SelectCheckBox();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        public void reset()
        {
            SelectWPnonsplit= null;

            for (int n = 0; n < WP_dataGridView.Rows.Count; n++)
            {
                WP_dataGridView.Rows[n].Cells[0].Value = false;
            }
        }
    }
}
