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

namespace main.subcontents.RESystem_FC
{
    public partial class FCSList : Form
    {
        public string SelectFCList;
        ArrayList SelectRow = new ArrayList();
        public FCSList(string System, string Systemnum)
        {
            InitializeComponent();
            string[][] Image = Program.DB.getValue(DB.type.BaseDB_HCneed, "메뉴아이콘", "하위메뉴아이콘", "하위메뉴명 = '연료전지'");
            Icon_pictureBox.Load(Program.gPath + Image[0][0]);
            Icon_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;

            //테이블 작성(축열탱크가 있는 난방또는 급탕설비항목)
            Tablemake();
            if (System == "난방")
            {
                string[][] value = Program.DB.getValue(DB.type.ProjDB, "HeatingSystem_Form", "번호,명칭,축열유무,축열용량", "축열유무 = '축열탱크 있음'");
                if (value.Length > 0)
                {
                    for (int i = 0; i < value.Length; i++)
                    {
                        HW_dataGridView.Rows.Add();
                        int n = HW_dataGridView.Rows.Count - 1;
                        HW_dataGridView.Rows[n].Cells[1].Value = value[i][0];
                        HW_dataGridView.Rows[n].Cells[2].Value = value[i][1];
                        HW_dataGridView.Rows[n].Cells[3].Value = value[i][2];
                        HW_dataGridView.Rows[n].Cells[4].Value = value[i][3];
                    }
                }
                string[][] lvalue = Program.DB.getValue(DB.type.ProjDB, "FuelCell_Form", "난방설비", " 번호 = '" + Systemnum + "'");
                if(lvalue.Length > 0)
                {
                    Load_SaveValue(lvalue[0][0].ToString());
                }
            }
            else if (System == "급탕")
            {
                string[][] value = Program.DB.getValue(DB.type.ProjDB, "DHWSystem_Form", "번호,명칭,축열유무,축열용량", "축열유무 = '축열탱크 있음'");
                if (value.Length > 0)
                {
                    for (int i = 0; i < value.Length; i++)
                    {
                        HW_dataGridView.Rows.Add();
                        int n = HW_dataGridView.Rows.Count - 1;
                        HW_dataGridView.Rows[n].Cells[1].Value = value[i][0];
                        HW_dataGridView.Rows[n].Cells[2].Value = value[i][1];
                        HW_dataGridView.Rows[n].Cells[3].Value = value[i][2];
                        HW_dataGridView.Rows[n].Cells[4].Value = value[i][3];
                    }
                }
                string[][] lvalue = Program.DB.getValue(DB.type.ProjDB, "FuelCell_Form", "급탕설비", " 번호 = '" + Systemnum + "'");
                if (lvalue.Length > 0)
                {
                    Load_SaveValue(lvalue[0][0].ToString());
                }
            }
        }

        private void Tablemake()
        {
            new StackedHeaderDecorator(HW_dataGridView, DataGridViewAutoSizeColumnsMode.Fill, datagridviewDesign);
            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
            HW_dataGridView.Columns.Clear();
            checkBoxColumn.HeaderText = "선택";
            checkBoxColumn.Name = "check";
            HW_dataGridView.Columns.Add(checkBoxColumn);
            HW_dataGridView.Columns.Add("A1", "번호"); //난방급탕설비 번호
            HW_dataGridView.Columns.Add("A2", "명칭"); //명칭 이름
            HW_dataGridView.Columns.Add("A3", "축열유무"); //축열유무
            HW_dataGridView.Columns.Add("A4", "축열용량"); //축열용량

            HW_dataGridView.Columns[0].Width = 40;
            HW_dataGridView.Columns[3].Width = 100;
        }



        //그리드 디자인

        private void GeneralPanel_Paint(object sender, PaintEventArgs e)
        {
            Panel p = (Panel)sender;
            ControlPaint.DrawBorder(e.Graphics, p.DisplayRectangle, Color.FromArgb(153, 180, 209), ButtonBorderStyle.Solid);
        }

        private void Save_button_Click(object sender, EventArgs e)
        {
            SelectRow.Clear();
            SelectCheckBox();
            SelectFCList = null;
            if (SelectRow.Count > 0)
            {
                for (int k = 0; k < SelectRow.Count; k++)
                {
                    if (k == SelectRow.Count - 1)
                    {
                        SelectFCList += HW_dataGridView.Rows[Convert.ToInt16(SelectRow[k])].Cells[1].Value.ToString();
                    }
                    else
                    {
                        SelectFCList += HW_dataGridView.Rows[Convert.ToInt16(SelectRow[k])].Cells[1].Value.ToString() + "+";
                    }
                }
            }
            
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void SelectCheckBox()
        {
            foreach (DataGridViewRow row in HW_dataGridView.Rows)
            {
                if (Convert.ToBoolean(row.Cells["check"].Value))
                {
                    row.DefaultCellStyle.SelectionBackColor = SystemColors.GradientInactiveCaption;
                    SelectRow.Add(row.Index);
                }
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


        private void reset()
        {
            SelectFCList = null;

            for (int n = 0; n < HW_dataGridView.Rows.Count; n++)
            {
                HW_dataGridView.Rows[n].Cells[0].Value = false;
            }
        }

        private void Load_SaveValue(string FcList)
        {
            reset();
            string[] token = FcList.Split('+');
           
            for (int k = 0; k < token.Length; k++)
            {
                for (int n = 0; n < HW_dataGridView.Rows.Count; n++)
                {
                    if (HW_dataGridView.Rows[n].Cells[1].Value.ToString() == token[k].ToString())
                    {
                        HW_dataGridView.Rows[n].Cells[0].Value = true;
                    }
                }
            }
        }
    }
}

