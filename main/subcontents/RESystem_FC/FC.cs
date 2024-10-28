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
using static System.Net.WebRequestMethods;

namespace main.subcontents
{
    public partial class FC : Form
    {
        String DefaultUse;
        public string SelectFCnonsplit;
        List<int> SelectRow = new List<int>();

        public FC(string defaultUse)
        {
            InitializeComponent();
            this.DefaultUse = defaultUse;

            string[][] Image = Program.DB.getValue(DB.type.BaseDB_HCneed, "메뉴아이콘", "하위메뉴아이콘", "하위메뉴명 = '연료전지'");

            
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
                string[][] value = Program.DB.getValue(DB.type.BaseDB_RESystem, "연료전지DB", "번호,DB유형,제품명,제조사,전기출력,전기효율,열출력,열효율","");
                if (value.Length > 0)
                {
                    for (int i = 0; i < value.Length; i++)
                    {
                        FC_dataGridView.Rows.Add();
                        int n = FC_dataGridView.Rows.Count - 1;
                        FC_dataGridView.Rows[n].Cells[1].Value = value[i][0];
                        FC_dataGridView.Rows[n].Cells[2].Value = value[i][1];
                        FC_dataGridView.Rows[n].Cells[3].Value = value[i][2];
                        FC_dataGridView.Rows[n].Cells[4].Value = value[i][3];
                        FC_dataGridView.Rows[n].Cells[5].Value = value[i][4];
                        FC_dataGridView.Rows[n].Cells[6].Value = value[i][5];
                        FC_dataGridView.Rows[n].Cells[7].Value = value[i][6];
                        FC_dataGridView.Rows[n].Cells[8].Value = value[i][7];
                    }
                }
            }
            else if (DefaultUse == "장비일람표 DB")
            {
                string[][] value = Program.DB.getValue(DB.type.ProjDB, "User_FC", "번호,명칭,연료,전기출력,전기효율,열출력,열효율,설치","");
                string[][] check = Program.DB.getValue(DB.type.ProjDB, "FuelCell_Form", "연료전지", "");
                if (value.Length > 0)
                {
                    for (int i = 0; i < value.Length; i++)
                    {
                        FC_dataGridView.Rows.Add();
                        int n = FC_dataGridView.Rows.Count - 1;
                        for (int k = 0; k < check.Length; k++)
                        {
                            if (value[i][0] == check[k][0])
                            {
                                FC_dataGridView.Rows[n].Cells[0].Value = true;
                                break;
                            }
                        }                        
                        FC_dataGridView.Rows[n].Cells[1].Value = value[i][0];
                        FC_dataGridView.Rows[n].Cells[2].Value = value[i][1];
                        FC_dataGridView.Rows[n].Cells[3].Value = value[i][2];
                        FC_dataGridView.Rows[n].Cells[4].Value = value[i][3];
                        FC_dataGridView.Rows[n].Cells[5].Value = value[i][4];
                        FC_dataGridView.Rows[n].Cells[6].Value = value[i][5];
                        FC_dataGridView.Rows[n].Cells[7].Value = value[i][6];
                        FC_dataGridView.Rows[n].Cells[8].Value = value[i][7];
                    }
                }
            }
        }
        private void tableMake(string _DefaultUse)
        {
            new StackedHeaderDecorator(FC_dataGridView, DataGridViewAutoSizeColumnsMode.Fill, datagridviewDesign);
            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
            FC_dataGridView.Columns.Clear();
            checkBoxColumn.HeaderText = "선택";
            checkBoxColumn.Name = "check";
            FC_dataGridView.Columns.Add(checkBoxColumn);
            FC_dataGridView.Columns.Add("A1", "번호");

            if (DefaultUse == "기본DB 적용")
            {
                FC_dataGridView.Columns.Add("A2", "DB유형");
                FC_dataGridView.Columns.Add("A3", "제품명");
                FC_dataGridView.Columns.Add("A4", "제조사");
                FC_dataGridView.Columns.Add("A5", "전기.출력[kW]");
                FC_dataGridView.Columns.Add("A6", "전기.효율[%]");
                FC_dataGridView.Columns.Add("A7", "열.출력[kW]");
                FC_dataGridView.Columns.Add("A8", "열.효율[%]");
            }

            else if (DefaultUse == "장비일람표 DB")
            {
                FC_dataGridView.Columns.Add("A2", "명칭");
                FC_dataGridView.Columns.Add("A3", "연료");
                FC_dataGridView.Columns.Add("A4", "전기.출력[kW]");
                FC_dataGridView.Columns.Add("A5", "전기.효율[%]");
                FC_dataGridView.Columns.Add("A6", "열.출력[kW]");
                FC_dataGridView.Columns.Add("A7", "열.효율[%]");
                FC_dataGridView.Columns.Add("A8", "설치");
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

        private bool SelectCheckBox()
        {
            SelectRow.Clear();
            foreach (DataGridViewRow row in FC_dataGridView.Rows)
            {
                if (Convert.ToBoolean(row.Cells["check"].Value))
                {
                    row.DefaultCellStyle.SelectionBackColor = SystemColors.GradientInactiveCaption;
                    SelectRow.Add(row.Index);
                }
            }
            return true;
        }


        private void Save_button_Click(object sender, EventArgs e)
        {
            if (SelectCheckBox() == false)
            {
                return;
            }
            for(int k = 0; k<SelectRow.Count;k++)
            {
                if (k == SelectRow.Count - 1)
                {
                    this.SelectFCnonsplit += FC_dataGridView.Rows[SelectRow[k]].Cells[1].Value.ToString();
                }
                else
                {
                    this.SelectFCnonsplit += FC_dataGridView.Rows[SelectRow[k]].Cells[1].Value.ToString() + "+";
                }
            }
            
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        public void reset()
        {
            SelectFCnonsplit = null;

            for (int n = 0; n < FC_dataGridView.Rows.Count; n++)
            {
                FC_dataGridView.Rows[n].Cells[0].Value = false;
            }
        }
    }
}
