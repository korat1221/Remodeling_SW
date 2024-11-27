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


namespace main.subcontents.ZoneGeneral
{
    public partial class PreZone : Form
    {
        double Count_DB;
        ArrayList SelectRow = new ArrayList(); ArrayList SelectZone_split = new ArrayList();
        String ZoneNum, Layer;
        public string SelectZone;
        string[][] PreProjNum = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "기존프로젝트", "");
        public PreZone(String Num, String SelectZone_nonsplit, string Layer)
        {
            InitializeComponent(); this.Font = new Font(UTIL.Families[0], 9.75F, FontStyle.Regular);
            ZoneNum = Num;
            this.Layer = Layer;
            load_table_DB();
            if (SelectZone_nonsplit != null)
            {
                Load_SaveValue(SelectZone_nonsplit);
            }
            Load_SelectZone();
        }

        void load_table_DB()
        {
            //  DataTable table_Zone = new DataTable();
            new StackedHeaderDecorator(Zone_dataGridView, DataGridViewAutoSizeColumnsMode.Fill, datagridviewDesign);
            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
            Zone_dataGridView.Columns.Clear();
            checkBoxColumn.HeaderText = "선택";
            checkBoxColumn.Name = "check";
            Zone_dataGridView.Columns.Add(checkBoxColumn);

            Zone_dataGridView.Columns.Add("A1", "번호");
            Zone_dataGridView.Columns.Add("A2", "존명칭");
            Zone_dataGridView.Columns.Add("A3", "용도프로필");
            Zone_dataGridView.Columns.Add("A4", "냉난방");
            Zone_dataGridView.Columns.Add("A5", "연간 요구량.난방.[kWh/a]");
            Zone_dataGridView.Columns.Add("A6", "연간 요구량.냉방.[kWh/a]");
            Zone_dataGridView.Columns.Add("A7", "면적.[m²]");
            Zone_dataGridView.Columns.Add("A8", "리모델링 후");
            Zone_dataGridView.Columns[0].Width = 40;
            Zone_dataGridView.Columns[5].Width = 70;
            Zone_dataGridView.Columns[6].Width = 70;
            Zone_dataGridView.Columns[7].Width = 60;

            string[][] Value = Program.DB.querySQL(PreProjNum[0][0], "Select DISTINCT a.존번호 From ZoneGeneral_Form as a Inner Join ZoneEnvelope_3D as b on a.존번호 = b.존 where b.층 ='" + Layer + "' Order by a.존번호");
            if (Value.Length > 0)
            {
                for (int n = 0; n < Value.Length; n++)
                {
                    string[][] Value2 = Program.DB.querySQL(PreProjNum[0][0], "Select 존번호,존이름,용도프로필,순바닥면적,냉난방유무 From ZoneGeneral_Form where 존번호='" + Value[n][0] + "'");

                    string[][] 요구량 = Program.DB.getValue(PreProjNum[0][0], "Zone_HCneed_Result", "Qb_a", "번호 ='" + Value[n][0] + "' AND 난방_냉방 = '난방'");
                    string[][] 요구량2 = Program.DB.getValue(PreProjNum[0][0], "Zone_HCneed_Result", "Qb_a", "번호 ='" + Value[n][0] + "' AND 난방_냉방 = '냉방'");
                    if (Value2.Length > 0)
                    {
                        Zone_dataGridView.Rows.Add();
                        int nRow = Zone_dataGridView.Rows.Count - 1;
                        Zone_dataGridView.Rows[nRow].Cells[1].Value = Value2[0][0];

                        Zone_dataGridView.Rows[nRow].Cells[2].Value = Value2[0][1];
                        Zone_dataGridView.Rows[nRow].Cells[3].Value = Value2[0][2];
                        Zone_dataGridView.Rows[nRow].Cells[4].Value = Value2[0][4];
                        if (요구량.Length > 0)
                        {
                            Zone_dataGridView.Rows[nRow].Cells[5].Value = string.Format("{0:F2}", Convert.ToDouble(요구량[0][0]));
                            Zone_dataGridView.Rows[nRow].Cells[6].Value = string.Format("{0:F2}", Convert.ToDouble(요구량2[0][0]));
                        }
                        Zone_dataGridView.Rows[nRow].Cells[7].Value = string.Format("{0:F1}", Convert.ToDouble(Value2[0][3]));
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
            SelectRow.Clear();
            foreach (DataGridViewRow row in Zone_dataGridView.Rows)
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
                    this.SelectZone += Zone_dataGridView.Rows[Convert.ToInt16(SelectRow[k])].Cells[1].Value.ToString();
                }
                else
                {
                    this.SelectZone += Zone_dataGridView.Rows[Convert.ToInt16(SelectRow[k])].Cells[1].Value.ToString() + "+";
                }
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        private void reset()
        {
            SelectRow.Clear();
            SelectZone_split.Clear();
            this.SelectZone = null;

            for (int n = 0; n < Zone_dataGridView.Rows.Count; n++)
            {
                Zone_dataGridView.Rows[n].Cells[0].Value = false;
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
                        for (int n = 0; n < Zone_dataGridView.Rows.Count; n++)
                        {
                            if (Zone_dataGridView.Rows[n].Cells[1].Value.ToString() == Zone_split[k].ToString())
                            {
                                if (Zone_dataGridView.Rows[n].Cells[8].Value == null)
                                { Zone_dataGridView.Rows[n].Cells[8].Value = value[i][0] + "." + value[i][1]; }
                                else
                                {
                                    Zone_dataGridView.Rows[n].Cells[8].Value = Zone_dataGridView.Rows[n].Cells[8].Value.ToString() + ", " + value[i][0] + "." + value[i][1];
                                }
                            }
                        }
                    }
                }
            }
        }
        private void Load_SaveValue(String SelectZone_nonsplit)
        {
            reset();
            string[] token = SelectZone_nonsplit.Split('+');
            SelectZone_split.Clear();
            foreach (var item in token)
            {
                SelectZone_split.Add(item.ToString());
            }
            for (int k = 0; k < SelectZone_split.Count; k++)
            {
                for (int n = 0; n < Zone_dataGridView.Rows.Count; n++)
                {
                    if (Zone_dataGridView.Rows[n].Cells[1].Value.ToString() == SelectZone_split[k].ToString())
                    {
                        Zone_dataGridView.Rows[n].Cells[0].Value = true;
                    }
                }
            }
        }
    }

}

