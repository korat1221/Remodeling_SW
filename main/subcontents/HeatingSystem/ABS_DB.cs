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
    public partial class ABS_DB : Form
    {
        ArrayList SelectRow = new ArrayList(); ArrayList SelectABS_split = new ArrayList();
        String DefaultUse;
        public string SelectABS;
        //HeatingSystem heatingSystem;

        // public ABS_DB(HeatingSystem system)
        public ABS_DB(String DefaultUse, String SelectABS_nonsplit)
        {
            InitializeComponent();
            //heatingSystem = system;
            this.DefaultUse = DefaultUse;
            load_table_DB();
            string[][] Image = Program.DB.getValue(DB.type.BaseDB_HCneed, "메뉴아이콘", "하위메뉴아이콘", "하위메뉴명 = '장비일람표'");
            Icon_pictureBox.Load(Program.gPath + Image[0][0]);
            Icon_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            if (SelectABS_nonsplit != null)
            {
                Load_SaveValue(SelectABS_nonsplit);
            }

        }

        void load_table_DB()
        {
            //DataTable ABS_table = new DataTable();
            new StackedHeaderDecorator(ABS_dataGridView, DataGridViewAutoSizeColumnsMode.Fill, datagridviewDesign);
            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
            ABS_dataGridView.Columns.Clear();
            checkBoxColumn.HeaderText = "선택";
            checkBoxColumn.Name = "check";
            ABS_dataGridView.Columns.Add(checkBoxColumn);

            ABS_dataGridView.Columns.Add("A1", "번호");
            ABS_dataGridView.Columns[0].Width =100;
            if (DefaultUse != "기본DB 적용")
            {
                ABS_dataGridView.Columns.Add("A2", "DB유형");
                ABS_dataGridView.Columns.Add("A3", "난방/냉방");
                ABS_dataGridView.Columns.Add("A4", "연료");
                ABS_dataGridView.Columns.Add("A5", "냉방.용량.[kW]");
                ABS_dataGridView.Columns.Add("A6", "냉방.성능.ξ");
                ABS_dataGridView.Columns.Add("A7", "난방.용량.[kW]");
                ABS_dataGridView.Columns.Add("A8", "난방.성능.COP");
                ABS_dataGridView.Columns.Add("A9", "냉수.입구온도.[℃]");
                ABS_dataGridView.Columns.Add("A10", "냉수.출구온도.[℃]");
                ABS_dataGridView.Columns.Add("A11", "온수.입구온도.[℃]");
                ABS_dataGridView.Columns.Add("A12", "온수.출구온도.[℃]");
                ABS_dataGridView.Columns.Add("A13", "대기전력.[W]");
                ABS_dataGridView.Columns.Add("A15", "대수.[EA]");
            }

            if (DefaultUse == "기본DB 적용")
            {
                ABS_dataGridView.Columns.Add("A14", "통합성능.IPLV");
                ABS_dataGridView.Columns.Add("A16", "비고");

                string[][] DefaultDB_Value = Program.DB.getValue(DB.type.BaseDB_Heating, "흡수식냉온수기", "번호,통합성능,비고", "");
                for (int n = 0; n < DefaultDB_Value.Length; n++)
                {
                    int nRow = ABS_dataGridView.Rows.Add();
                    ABS_dataGridView.Rows[nRow].Cells[1].Value = DefaultDB_Value[n][0];
                    ABS_dataGridView.Rows[nRow].Cells[2].Value = DefaultDB_Value[n][1];
                    ABS_dataGridView.Rows[nRow].Cells[3].Value = DefaultDB_Value[n][2];
                }
            }
            else
            {
                string[][] User_Value = Program.DB.getValue(DB.type.ProjDB, "User_ABS", "번호,난방냉방,연료,난방용량,난방성능,온수입구온도,온수출구온도,대기전력,통합성능", "난방냉방 ='냉난방'");
                for (int n = 0; n < User_Value.Length; n++)
                {
                  
                    ABS_dataGridView.Rows.Add();
                    int nRow = ABS_dataGridView.Rows.Count - 1;
                    ABS_dataGridView.Rows[nRow].Cells[1].Value = User_Value[n][0];
                    ABS_dataGridView.Rows[nRow].Cells[2].Value = User_Value[n][1];
                    ABS_dataGridView.Rows[nRow].Cells[3].Value = User_Value[n][2];
                    ABS_dataGridView.Rows[nRow].Cells[4].Value = User_Value[n][3];
                    ABS_dataGridView.Rows[nRow].Cells[5].Value = User_Value[n][4];
                    ABS_dataGridView.Rows[nRow].Cells[6].Value = User_Value[n][5];
                    ABS_dataGridView.Rows[nRow].Cells[7].Value = User_Value[n][6];
                    ABS_dataGridView.Rows[nRow].Cells[8].Value = User_Value[n][7];
                    ABS_dataGridView.Rows[nRow].Cells[9].Value = User_Value[n][8];
                }
            }
            // ABS_dataGridView.DataSource = ABS_table;
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
            foreach (DataGridViewRow row in ABS_dataGridView.Rows)
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
                    SelectABS += ABS_dataGridView.Rows[Convert.ToInt16(SelectRow[k])].Cells[1].Value.ToString();
                }
                else
                {
                    SelectABS += ABS_dataGridView.Rows[Convert.ToInt16(SelectRow[k])].Cells[1].Value.ToString() + "+";
                }
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        private void reset()
        {
            SelectRow.Clear();
            SelectABS_split.Clear();
            SelectABS = null;

            for (int n = 0; n < ABS_dataGridView.Rows.Count; n++)
            {
                ABS_dataGridView.Rows[n].Cells[0].Value = false;
            }

        }
        private void Load_SaveValue(String SelectABS_nonsplit)
        {
            reset();
            try
            {
                string[] token = SelectABS_nonsplit.Split('+');
                SelectABS_split.Clear();
                foreach (var item in token)
                {
                    SelectABS_split.Add(item.ToString());
                }
                for (int k = 0; k < SelectABS_split.Count; k++)
                {
                    for (int n = 0; n < ABS_dataGridView.Rows.Count; n++)
                    {
                        if (ABS_dataGridView.Rows[n].Cells[1].Value.ToString() == SelectABS_split[k].ToString())
                        {
                            ABS_dataGridView.Rows[n].Cells[0].Value = true;
                        }
                    }
                }

            }
            catch { }
        }
    }
}
