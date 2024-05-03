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

namespace main.subcontents.Alt
{
    public partial class AltWall : Form
    {
        double Count_DB;
        ArrayList SelectRow = new ArrayList(); ArrayList SelectZone_split = new ArrayList();
        public string SelectZone;
        public AltWall(String SelectValue)
        {
            InitializeComponent();
            load_table_DB();
            if (SelectValue != null)
            {

            }
            else
            {
                Alt_dataGridView.Rows[0].Cells[0].Value = true;
                Load_CheckValue(0);
            }
            Alt_dataGridView.Rows[0].Cells[0].Value = true;
            Load_CheckValue(0);
        }

        void load_table_DB()
        {
            new StackedHeaderDecorator(Alt_dataGridView, DataGridViewAutoSizeColumnsMode.Fill, datagridviewDesign);
            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
            Alt_dataGridView.Columns.Clear();
            checkBoxColumn.HeaderText = "선택";
            checkBoxColumn.Name = "check";
            Alt_dataGridView.Columns.Add(checkBoxColumn);

            Alt_dataGridView.Columns.Add("A1", "번호");
            Alt_dataGridView.Columns.Add("A2", "리모델링안");
            Alt_dataGridView.Columns.Add("A3", "유효열관류율.[W/m²·K]");
            Alt_dataGridView.Columns.Add("A4", "점수.에너지절감");
            Alt_dataGridView.Columns.Add("A5", "점수.탄소절감");
            Alt_dataGridView.Columns.Add("A6", "점수.법규");
            Alt_dataGridView.Columns.Add("A7", "점수.경제성");
            Alt_dataGridView.Columns.Add("A8", "종합 점수");
            Alt_dataGridView.Columns[0].Width = 40;
            Alt_dataGridView.Columns[1].Width = 40;
            Alt_dataGridView.Columns[3].Width = 50;
            Alt_dataGridView.Columns[4].Width = 50;
            Alt_dataGridView.Columns[5].Width = 50;
            Alt_dataGridView.Columns[6].Width = 50;
            Alt_dataGridView.Columns[7].Width = 50;
            Alt_dataGridView.Columns[8].Width = 60;

            double TOE = 0;
            string[][] Pre_elec = Program.DB.getValue(DB.type.ProjDB, "FinalEnergy_Result", "총에너지소요량", "연료='전기' and 월 ='연간'");
            if (Pre_elec.Length > 0)
            {
                TOE = Convert.ToDouble(Pre_elec[0][0]) * 0.00023;
            }
            string[][] Pre_gas = Program.DB.getValue(DB.type.ProjDB, "FinalEnergy_Result", "총에너지소요량", "Not 연료='전기' and Not 연료='전체' and 월 ='연간'");
            if (Pre_gas.Length > 0)
            {
                TOE = TOE + Convert.ToDouble(Pre_gas[0][0]) / 43.1 / 0.277778 * 0.00103;
            }

            string[][] Pre_tot = Program.DB.getValue(DB.type.ProjDB, "FinalEnergy_Result", "총에너지소요량", "연료='전체' and 월 ='연간'");
            if (Pre_tot.Length > 0)
            {
                string[][] Value = Program.DB.querySQL(DB.type.ProjDB, "Select 총에너지소요량,리모델링안 From FinalEnergy_Result_Optimal Where 검토유형='외벽' and 연료='전체' Order by 총에너지소요량 DESC");
                if (Value.Length > 0)
                {
                    for (int i = 0; i < Value.Length; i++)
                    {
                        int nRow = Alt_dataGridView.Rows.Add();
                        Alt_dataGridView.Rows[nRow].Cells[1].Value = (i + 1).ToString();
                        Alt_dataGridView.Rows[nRow].Cells[2].Value = Value[i][1];
                        Alt_dataGridView.Rows[nRow].Cells[3].Value = Cal_UValue(Value[i][1]).ToString("0.00");
                        Alt_dataGridView.Rows[nRow].Cells[4].Value = ((Convert.ToDouble(Pre_tot[0][0]) - Convert.ToDouble(Value[i][0])) / Convert.ToDouble(Pre_tot[0][0]) * 1000).ToString("0") + " 점";
                        Alt_dataGridView.Rows[nRow].Cells[5].Value = ((TOE - Cal_TOE(Value[i][1])) / TOE * 1000).ToString("0") + " 점";
                        Alt_dataGridView.Rows[nRow].Cells[6].Value = (Cal_RuleUvalue() / Cal_UValue(Value[i][1]) * 100).ToString("0") + " 점";

                    }
                }
            }

        }
        private double Cal_UValue(string 리모델링안)
        {
            double R = 0;
            string[][] V = Program.DB.getValue(DB.type.BaseDB_Optimal, "최적안_외벽_인덱스", "외벽유형", "구분='" + 리모델링안 + "'");
            if (V.Length > 0)
            {
                string[][] R_value = Program.DB.getValue(DB.type.BaseDB_Optimal, "최적안_외벽", "열저항합계", "구분='" + V[0][0] + "'");
                if (R_value.Length > 0)
                {
                    R = Convert.ToDouble(R_value[0][0]);
                }
            }


            double Total_Area = 0, Uvalue = 0;
            string[][] Value = Program.DB.querySQL(DB.type.ProjDB, "select a.면적,b.열관류율 FROM ZoneEnvelope_3D AS a INNER JOIN ConstructionWall AS b ON a.구조체번호 = b.번호");
            if (Value.Length > 0)
            {
                for (int k = 0; k < Value.Length; k++)
                {
                    Total_Area += Convert.ToDouble(Value[k][0]);
                    Uvalue += Convert.ToDouble(Value[k][0]) * Convert.ToDouble(Value[k][1]);
                }
                Uvalue = Uvalue / Total_Area;
            }

            return 1 / (1 / Uvalue + R);
        }
        private double Cal_RuleUvalue()
        {
            double Total_Area = 0, Uvalue = 0, RuleValue = 0;
            string[][] Value = Program.DB.querySQL(DB.type.ProjDB, "select a.면적,b.유효열관류율,b.법규열관류율 FROM ZoneEnvelope_3D AS a INNER JOIN ConstructionWall AS b ON a.구조체번호 = b.번호");
            if (Value.Length > 0)
            {
                for (int k = 0; k < Value.Length; k++)
                {
                    Total_Area += Convert.ToDouble(Value[k][0]);
                    Uvalue += Convert.ToDouble(Value[k][0]) * Convert.ToDouble(Value[k][1]);
                    RuleValue += Convert.ToDouble(Value[k][0]) * Convert.ToDouble(Value[k][2]);
                }
                Uvalue = Uvalue / Total_Area;
                RuleValue = RuleValue / Total_Area;
            }
            return RuleValue;
        }
        private double Cal_TOE(string 리모델링안)
        {
            double TOE = 0;
            string[][] Value_elec = Program.DB.querySQL(DB.type.ProjDB, "Select 총에너지소요량,리모델링안 From FinalEnergy_Result_Optimal Where 검토유형='외벽'and 리모델링안='" + 리모델링안 + "' and 연료='전기'");
            if (Value_elec.Length > 0)
            {
                TOE = Convert.ToDouble(Value_elec[0][0]) * 0.00023;
            }
            string[][] Value_gas = Program.DB.querySQL(DB.type.ProjDB, "Select 총에너지소요량,리모델링안 From FinalEnergy_Result_Optimal Where 검토유형='외벽'and 리모델링안='" + 리모델링안 + "' and Not 연료='전기' and Not 연료='전체'");
            if (Value_gas.Length > 0)
            {
                TOE = TOE + Convert.ToDouble(Value_gas[0][0]) / 43.1 / 0.277778 * 0.00103;
            }
            return TOE;
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
            SelectRow.Clear();
            SelectZone_split.Clear();
            this.SelectZone = null;

            for (int n = 0; n < Alt_dataGridView.Rows.Count; n++)
            {
                Alt_dataGridView.Rows[n].Cells[0].Value = false;
            }
        }

        private void Load_SelectZone()
        {
            ArrayList Zone_split = new ArrayList();
            string[][] value = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_Form", "존번호,존이름,기존존", "");
            if (value.Length > 0)
            {
                for (int i = 0; i < value.Length; i++)
                {
                    string[] token = value[i][2].Split('+');
                    Zone_split.Clear();
                    foreach (var item in token)
                    {
                        Zone_split.Add(item.ToString());
                    }
                    for (int k = 0; k < Zone_split.Count; k++)
                    {
                        for (int n = 0; n < Alt_dataGridView.Rows.Count; n++)
                        {
                            if (Alt_dataGridView.Rows[n].Cells[1].Value.ToString() == Zone_split[k].ToString())
                            {
                                if (Alt_dataGridView.Rows[n].Cells[8].Value == null)
                                { Alt_dataGridView.Rows[n].Cells[8].Value = value[i][0] + "." + value[i][1]; }
                                else
                                {
                                    Alt_dataGridView.Rows[n].Cells[8].Value = Alt_dataGridView.Rows[n].Cells[8].Value.ToString() + ", " + value[i][0] + "." + value[i][1];
                                }
                            }
                        }
                    }
                }
            }
        }
        private void Save_button_Click(object sender, EventArgs e)
        {

        }

        private void Alt_dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                for (int i = 0; i < Alt_dataGridView.Rows.Count; i++)
                {
                    if (i != e.RowIndex) { Alt_dataGridView.Rows[i].Cells[0].Value = false; }
                }
                Alt_dataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
                int row = GetSelectedIndex(); 
                if(row >-1)
                {
                    Load_CheckValue(row);
                }
            }
        }
        private void Load_CheckValue(int row)
        {
            Name_textBox.Text = Alt_dataGridView.Rows[row].Cells[2].Value.ToString();

            Energy_textBox.Text = Convert.ToDouble(Alt_dataGridView.Rows[row].Cells[4].Value.ToString().Substring(0, Alt_dataGridView.Rows[row].Cells[4].Value.ToString().Length - 2))/10+" %" ;
            EnergyPoint_textBox.Text = Alt_dataGridView.Rows[row].Cells[4].Value.ToString();

            CO2_textBox.Text = Convert.ToDouble(Alt_dataGridView.Rows[row].Cells[5].Value.ToString().Substring(0, Alt_dataGridView.Rows[row].Cells[5].Value.ToString().Length - 2))/10 + " %";
            CO2Point_textBox.Text = Alt_dataGridView.Rows[row].Cells[5].Value.ToString();

            Rule_textBox.Text = Alt_dataGridView.Rows[row].Cells[3].Value.ToString() + " W/m²·K [" + Cal_RuleUvalue().ToString("0.00") + " W/m²·K]";
            RulePoint_textBox.Text = Alt_dataGridView.Rows[row].Cells[6].Value.ToString();
        }

        private int GetSelectedIndex()
        {
            for (int k = 0; k < Alt_dataGridView.Rows.Count; k++)
            {
                if (Convert.ToBoolean(Alt_dataGridView.Rows[k].Cells[0].Value) == true)
                {
                    return k;
                }
            }
            return -1;
        }
    }
}

