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
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace main.subcontents.HeatingSystem
{
    public partial class Heating_AHU : Form
    {
        double Count_DB;
        ArrayList SelectRow = new ArrayList(); ArrayList SelectAHU_split = new ArrayList();
        String SystemNum;
        public string SelectAHU;

        public Heating_AHU(String Num, String SelectZone_nonsplit)
        {
            InitializeComponent(); this.Font = new Font(UTIL.Families[0], 9.75F, FontStyle.Regular);
            load_table_DB();
            SystemNum = Num;
            if(SelectZone_nonsplit != null)
            { 
                Load_SaveValue(SelectZone_nonsplit);
            }
           
        }

        void load_table_DB()
        {
            //  DataTable table_Zone = new DataTable();
            new StackedHeaderDecorator(AHU_dataGridView, DataGridViewAutoSizeColumnsMode.Fill, datagridviewDesign);
            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
            AHU_dataGridView.Columns.Clear();
            checkBoxColumn.HeaderText = "선택";
            checkBoxColumn.Name = "check";
            AHU_dataGridView.Columns.Add(checkBoxColumn);

            AHU_dataGridView.Columns.Add("A1", "번호");
            AHU_dataGridView.Columns.Add("A2", "공조기명칭");
            AHU_dataGridView.Columns.Add("A3", "존 개수.[EA]");
            AHU_dataGridView.Columns.Add("A4", "난방공조요구량.[kWh/a]");
            AHU_dataGridView.Columns.Add("A5", "최대난방부하.[kW]");
            AHU_dataGridView.Columns.Add("A6", "면적.[m"+Program.UTIL.Subscript(2, true)+"]");

            string[][] Value = Program.DB.getValue(DB.type.ProjDB, "AHUSystem_Form", "번호,명칭", "유형 ='공조기'");
            if (Value.Length > 0)
            {
                for (int n = 0; n < Value.Length; n++)
                {
                    double Qb_a = 0, Qmax = 0, Area = 0;
                    int nRow = AHU_dataGridView.Rows.Add();
                    AHU_dataGridView.Rows[nRow].Cells[1].Value = Value[n][0];
                    AHU_dataGridView.Rows[nRow].Cells[2].Value = Value[n][1];
                    string[][] 존 = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_Form", "존번호,순바닥면적", "선택열회수기 = '" + Value[n][0] + "'");
                    if(존.Length > 0)
                    {
                        AHU_dataGridView.Rows[nRow].Cells[3].Value = 존.Length;
                        for(int a=0; a< 존.Length ; a++)
                        {
                           Area += Program.UTIL.ToDoubleOrZero(존[0][1]);
                        }
                    }
                    for (int mth=0; mth< 12; mth++)
                    {
                        string[][] 요구량 = Program.DB.getValue(DB.type.ProjDB, "AHUSystem_Result", "공조요구량, Qmax_tot", "번호 ='" + Value[n][0] + "' And 난방_냉방 = '난방' And 월 = '" + (mth + 1).ToString() + "월'");
                        if(요구량.Length>0)
                        {
                            for (int a = 0; a < 요구량.Length; a++)
                            {
                                Qb_a += Program.UTIL.ToDoubleOrZero(요구량[0][0]);
                            }
                            Qmax = Program.UTIL.ToDoubleOrZero(요구량[0][1]) / 1000;
                        }
                    }
                    AHU_dataGridView.Rows[nRow].Cells[4].Value = Qb_a.ToString("#,##0");
                    AHU_dataGridView.Rows[nRow].Cells[5].Value = Qmax.ToString("#,0.##");
                    AHU_dataGridView.Rows[nRow].Cells[6].Value = Area.ToString("#,0.##");

                    Count_DB = Value.Length;
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
            SelectRow.Clear();
            foreach (DataGridViewRow row in AHU_dataGridView.Rows)
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
            SelectCheckBox();
            for (int k = 0; k < SelectRow.Count; k++)
            {
                if (k == SelectRow.Count - 1)
                {
                    this.SelectAHU += AHU_dataGridView.Rows[Convert.ToInt16(SelectRow[k])].Cells[1].Value.ToString();
                }
                else
                {
                    this.SelectAHU += AHU_dataGridView.Rows[Convert.ToInt16(SelectRow[k])].Cells[1].Value.ToString() + "+";
                }
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
          }
        private void reset()
        {
            SelectRow.Clear();
            SelectAHU_split.Clear();
            this.SelectAHU = null;

            for (int n = 0; n < AHU_dataGridView.Rows.Count; n++)
            {
                AHU_dataGridView.Rows[n].Cells[0].Value = false;
            }
        }

        private void Load_SaveValue(String SelectZone_nonsplit)
        {
            reset();
            string[] token = SelectZone_nonsplit.Split('+');
            SelectAHU_split.Clear();
            foreach (var item in token)
            {
                SelectAHU_split.Add(item.ToString());
            }
            for (int k = 0; k < SelectAHU_split.Count; k++)
            {
                for (int n = 0; n < AHU_dataGridView.Rows.Count; n++)
                {
                    if (AHU_dataGridView.Rows[n].Cells[1].Value.ToString() == SelectAHU_split[k].ToString())
                    {
                        AHU_dataGridView.Rows[n].Cells[0].Value = true;
                    }
                }
            }
        }
    }

}

