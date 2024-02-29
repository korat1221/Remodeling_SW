using main.contents;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.Entity.Core.Metadata.Edm;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace main.subcontents.AHUSystem
{
    public partial class AHU_HRV : Form
    {
        ArrayList SelectRow = new ArrayList(); ArrayList SelectHRV_split = new ArrayList();
        String DefaultUse;
        public string SelectHRV;
        

        
        public AHU_HRV(String SelectHRV)
        {
            InitializeComponent();
                        
            load_table_DB();
            string[][] Image = Program.DB.getValue(DB.type.BaseDB_HCneed, "메뉴아이콘", "하위메뉴아이콘", "하위메뉴명 = '장비일람표'");
            if (Image.Length > 0)
            {
                Icon_pictureBox.Load(Program.gPath + Image[0][0]);
                Icon_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            }
            if (SelectHRV != null)
            {
                Load_SaveValue();
            }
        }

        void load_table_DB()
        {
            
            new StackedHeaderDecorator(HRV_dataGridView, DataGridViewAutoSizeColumnsMode.Fill, datagridviewDesign);
            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
            HRV_dataGridView.Columns.Clear();
            checkBoxColumn.HeaderText = "선택";
            checkBoxColumn.Name = "check";
            HRV_dataGridView.Columns.Add(checkBoxColumn);

            HRV_dataGridView.Columns.Add("A1", "번호");
            HRV_dataGridView.Columns.Add("A2", "명칭");
            HRV_dataGridView.Columns.Add("A3", "열회수.유형");
            HRV_dataGridView.Columns.Add("A4", "열회수.온도교환효율.냉방.[%]");
            HRV_dataGridView.Columns.Add("A5", "열회수.온도교환효율.난방.[%]");            
            HRV_dataGridView.Columns.Add("A6", "열회수.습도교환효율.냉방.[%]");
            HRV_dataGridView.Columns.Add("A7", "열회수.습도교환효율.난방.[%]");
            HRV_dataGridView.Columns.Add("A8", "팬.풍량.[CMH]");
            HRV_dataGridView.Columns.Add("A9", "팬.정압.[Pa]");
            HRV_dataGridView.Columns.Add("A10", "팬.모터제어");
            HRV_dataGridView.Columns.Add("A11", "소비전력.[W]");
            

            //string[][] User_Value = Program.DB.getValue(DB.type.ProjDB, "User_HRV", "번호,명칭,열회수유형, 온도교환효율_냉방, 온도교환효율_난방, 습도교환효율_냉방, 습도교환효율_난방, 팬풍량, 팬정압, 모터제어, 팬동력");
            //if (User_Value.Length > 0)
            //{
            //    for (int n = 0; n < User_Value.Length; n++)
            //    {
            //        string 온도교환효율_냉방 = "", 온도교환효율_난방 = "", 습도교환효율_냉방 = "", 습도교환효율_난방 = "", 팬풍량 = "", 팬정압 = "", 팬동력 = "";
                    
            //        if (User_Value[n][3] != null && User_Value[n][3] != "")
            //        {
            //            온도교환효율_냉방 = string.Format("{0:F0}", Convert.ToDouble(User_Value[n][3]));
            //        }
            //        if (User_Value[n][4] != null && User_Value[n][4] != "")
            //        {
            //            온도교환효율_난방 = string.Format("{0:F0}", Convert.ToDouble(User_Value[n][4]));
            //        }
            //        if (User_Value[n][5] != null && User_Value[n][5] != "")
            //        {
            //            습도교환효율_냉방 = string.Format("{0:F0}", Convert.ToDouble(User_Value[n][5]));
            //        }
            //        if (User_Value[n][6] != null && User_Value[n][6] != "")
            //        {
            //            습도교환효율_난방 = string.Format("{0:F0}", Convert.ToDouble(User_Value[n][6]));
            //        }
            //        if (User_Value[n][7] != null && User_Value[n][7] != "")
            //        {
            //            팬풍량 = string.Format("{0:F0}", Convert.ToDouble(User_Value[n][7]));
            //        }
            //        if (User_Value[n][8] != null && User_Value[n][8] != "")
            //        {
            //            팬정압 = string.Format("{0:F0}", Convert.ToDouble(User_Value[n][8]));
            //        }
            //        if (User_Value[n][10] != null && User_Value[n][10] != "")
            //        {
            //            팬동력 = string.Format("{0:F0}", Convert.ToDouble(User_Value[n][10]));
            //        }

            //        HRV_dataGridView.Rows.Add();
            //        int nRow = HRV_dataGridView.Rows.Count - 1;
            //        HRV_dataGridView.Rows[nRow].Cells[1].Value = User_Value[n][0];
            //        HRV_dataGridView.Rows[nRow].Cells[2].Value = User_Value[n][1];
            //        HRV_dataGridView.Rows[nRow].Cells[3].Value = User_Value[n][2];
            //        HRV_dataGridView.Rows[nRow].Cells[4].Value = 온도교환효율_냉방;
            //        HRV_dataGridView.Rows[nRow].Cells[5].Value = 온도교환효율_난방;
            //        HRV_dataGridView.Rows[nRow].Cells[6].Value = 습도교환효율_냉방;
            //        HRV_dataGridView.Rows[nRow].Cells[7].Value = 습도교환효율_난방;
            //        HRV_dataGridView.Rows[nRow].Cells[8].Value = 팬풍량;
            //        HRV_dataGridView.Rows[nRow].Cells[9].Value = 팬정압;
            //        HRV_dataGridView.Rows[nRow].Cells[10].Value = User_Value[n][9];
            //        HRV_dataGridView.Rows[nRow].Cells[11].Value = 팬동력;
                    
            //    }
            //}
            
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
            foreach (DataGridViewRow row in HRV_dataGridView.Rows)
            {
                if (Convert.ToBoolean(row.Cells["check"].Value))
                {
                    row.DefaultCellStyle.SelectionBackColor = SystemColors.GradientInactiveCaption;
                    SelectRow.Add(row.Index);
                }
            }
        }

        private void Save_button_Click(object sender, EventArgs e)
        {
            SelectRow.Clear();
            SelectCheckBox();
            for (int k = 0; k < SelectRow.Count; k++)
            {
                if (k == SelectRow.Count - 1)
                {
                    SelectHRV += HRV_dataGridView.Rows[Convert.ToInt16(SelectRow[k])].Cells[1].Value.ToString();
                }
                else
                {
                    SelectHRV += HRV_dataGridView.Rows[Convert.ToInt16(SelectRow[k])].Cells[1].Value.ToString() + "+";
                }
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        
        private void Load_SaveValue()
        {
            for (int n = 0; n < HRV_dataGridView.Rows.Count; n++)
            {
                if (HRV_dataGridView.Rows[n].Cells[1].Value.ToString() == SelectHRV.ToString())
                {
                    HRV_dataGridView.Rows[n].Cells[0].Value = true;
                }
            }
        }
    }

}
