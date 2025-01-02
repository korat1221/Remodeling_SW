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


namespace main.subcontents.HeatingSystem
{
    public partial class DH_DB : Form
    {
        ArrayList SelectRow = new ArrayList(); ArrayList SelectDH_split = new ArrayList();
        String DefaultUse;
        public string SelectDH;
        //HeatingSystem heatingSystem;
        string H_DHW; 
        // public DH_DB(HeatingSystem system)
        public DH_DB(String DefaultUse, String SelectDH_nonsplit, string H_DHW)
        {
            this.H_DHW = H_DHW;
            InitializeComponent(); this.Font = new Font(UTIL.Families[0], 9.75F, FontStyle.Regular);
            //heatingSystem = system;
            this.DefaultUse = DefaultUse;
            load_table_DB();
            string[][] Image = Program.DB.getValue(DB.type.BaseDB_HCneed, "메뉴아이콘", "하위메뉴아이콘", "하위메뉴명 = '장비일람표'");
            if (Image.Length > 0)
            {
                Icon_pictureBox.Load(Program.gPath + Image[0][0]);
                Icon_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            }
            if (SelectDH_nonsplit != null)
            {
                Load_SaveValue(SelectDH_nonsplit);
            }

        }

        void load_table_DB()
        {
            //DataTable DH_table = new DataTable();
            new StackedHeaderDecorator(DH_dataGridView, DataGridViewAutoSizeColumnsMode.Fill, datagridviewDesign);
            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
            DH_dataGridView.Columns.Clear();
            checkBoxColumn.HeaderText = "선택";
            checkBoxColumn.Name = "check";
            DH_dataGridView.Columns.Add(checkBoxColumn);

            DH_dataGridView.Columns.Add("A1", "번호");
            if (DefaultUse != "기본DB 적용")
            {
                DH_dataGridView.Columns.Add("A2", "명칭");
            }
            DH_dataGridView.Columns.Add("A3", "용도");
            if (DefaultUse != "기본DB 적용")
            {
                DH_dataGridView.Columns.Add("A4", "용량.[kW]");
            }
            DH_dataGridView.Columns.Add("A5", "1차측.공급온도.[℃]");
            DH_dataGridView.Columns.Add("A6", "1차측.환수온도.[℃]");
            DH_dataGridView.Columns.Add("A7", "2차측.공급온도.[℃]");
            DH_dataGridView.Columns.Add("A8", "2차측.환수온도.[℃]");
            DH_dataGridView.Columns[0].Width = 40;



            if (DefaultUse == "기본DB 적용")
            {
                string[][] DefaultDB_Value = Program.DB.getValue(DB.type.BaseDB_Heating, "지역난방", "번호,용도,공급온도1차,환수온도1차,공급온도2차,환수온도2차", "");
                if (DefaultDB_Value.Length > 0)
                {
                    for (int n = 0; n < DefaultDB_Value.Length; n++)
                    {
                        int nRow = DH_dataGridView.Rows.Add();
                        DH_dataGridView.Rows[nRow].Cells[1].Value = DefaultDB_Value[n][0];
                        DH_dataGridView.Rows[nRow].Cells[2].Value = DefaultDB_Value[n][1];
                        DH_dataGridView.Rows[nRow].Cells[3].Value = DefaultDB_Value[n][2];
                        DH_dataGridView.Rows[nRow].Cells[4].Value = DefaultDB_Value[n][3];
                        DH_dataGridView.Rows[nRow].Cells[5].Value = DefaultDB_Value[n][4];
                        DH_dataGridView.Rows[nRow].Cells[6].Value = DefaultDB_Value[n][5];
                    }
                }
            }
            else
            {
                string[][] User_Value = null;
                if (H_DHW == "난방")
                {
                    User_Value = Program.DB.getValue(DB.type.ProjDB, "User_DH", "번호,명칭,용도,용량,공급온도1차,환수온도1차,공급온도2차,환수온도2차", "용도 ='난방용'");                  
                }
                else
                {
                    User_Value = Program.DB.getValue(DB.type.ProjDB, "User_DH", "번호,명칭,용도,용량,공급온도1차,환수온도1차,공급온도2차,환수온도2차", "Not 용도 ='난방용' and Not 용도='흡수식'");
                }

                if (User_Value.Length > 0)
                {
                    for (int n = 0; n < User_Value.Length; n++)
                    {
                        DH_dataGridView.Rows.Add();
                        int nRow = DH_dataGridView.Rows.Count - 1;
                        DH_dataGridView.Rows[nRow].Cells[1].Value = User_Value[n][0];
                        DH_dataGridView.Rows[nRow].Cells[2].Value = User_Value[n][1];
                        DH_dataGridView.Rows[nRow].Cells[3].Value = User_Value[n][2];
                        DH_dataGridView.Rows[nRow].Cells[4].Value = User_Value[n][3];
                        DH_dataGridView.Rows[nRow].Cells[5].Value = User_Value[n][4];
                        DH_dataGridView.Rows[nRow].Cells[6].Value = User_Value[n][5];
                        DH_dataGridView.Rows[nRow].Cells[7].Value = User_Value[n][6];
                        DH_dataGridView.Rows[nRow].Cells[8].Value = User_Value[n][7];
                    }
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

            private void SelectCheckBox()
            {
                foreach (DataGridViewRow row in DH_dataGridView.Rows)
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
                        SelectDH += DH_dataGridView.Rows[Convert.ToInt16(SelectRow[k])].Cells[1].Value.ToString();
                    }
                    else
                    {
                        SelectDH += DH_dataGridView.Rows[Convert.ToInt16(SelectRow[k])].Cells[1].Value.ToString() + "+";
                    }
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            private void reset()
            {
                SelectRow.Clear();
                SelectDH_split.Clear();
                SelectDH = null;

                for (int n = 0; n < DH_dataGridView.Rows.Count; n++)
                {
                    DH_dataGridView.Rows[n].Cells[0].Value = false;
                }

            }
            private void Load_SaveValue(String SelectDH_nonsplit)
            {
                reset();
            string[] token = SelectDH_nonsplit.Split('+');
            SelectDH_split.Clear();
            foreach (var item in token)
            {
                SelectDH_split.Add(item.ToString());
            }
            for (int k = 0; k < SelectDH_split.Count; k++)
            {
                for (int n = 0; n < DH_dataGridView.Rows.Count; n++)
                {
                    if (DH_dataGridView.Rows[n].Cells[1].Value.ToString() == SelectDH_split[k].ToString())
                    {
                        DH_dataGridView.Rows[n].Cells[0].Value = true;
                    }
                }
            }
        }
        
    }
}
