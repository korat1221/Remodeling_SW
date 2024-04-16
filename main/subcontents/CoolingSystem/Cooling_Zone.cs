using main.contents;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.ComponentModel.Design.ObjectSelectorEditor;

namespace main.subcontents
{
    public partial class Cooling_Zone : Form
    {

        double Count_DB;
        ArrayList SelectRow = new ArrayList(); ArrayList SelectZone_split = new ArrayList();
        String SystemNum;
        public string SelectZone;

        public Cooling_Zone(string Num, string SelectZone_nonsplit)
        {
            InitializeComponent();
            load_table_DB();
            SystemNum = Num;

            if (SelectZone_nonsplit != null)
            {
                Load_SaveValue(SelectZone_nonsplit);
            }
        }

        void load_table_DB()
        {
            new StackedHeaderDecorator(CoolingZone_dataGridView, DataGridViewAutoSizeColumnsMode.Fill, datagridviewDesign);
            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
            CoolingZone_dataGridView.Columns.Clear();
            checkBoxColumn.HeaderText = "선택";
            checkBoxColumn.Name = "check";
            CoolingZone_dataGridView.Columns.Add(checkBoxColumn);

            CoolingZone_dataGridView.Columns.Add("A1", "번호");
            CoolingZone_dataGridView.Columns.Add("A2", "층");
            CoolingZone_dataGridView.Columns.Add("A3", "존명칭");
            CoolingZone_dataGridView.Columns.Add("A4", "용도프로필");
            CoolingZone_dataGridView.Columns.Add("A5", "연간 냉방요구량.[kWh/a]");
            CoolingZone_dataGridView.Columns.Add("A6", "최대냉방부하.[kW]");
            CoolingZone_dataGridView.Columns.Add("A7", "면적.[m²]");

            string[][] Value = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_Form", "존번호,존이름,용도프로필,순바닥면적", "냉난방유무 ='냉난방' OR 냉난방유무 = '냉방'");
            if (Value.Length > 0)
            {
                for (int n = 0; n < Value.Length; n++)
                {
                    string[][] 층 = Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope_3D", "층", "존 ='" + Value[n][0] + "'");
                    string[][] 부하 = Program.DB.getValue_SameCheck(DB.type.ProjDB, "Zone_HCneed_Result", "Qb_a,Q_max", "번호 ='" + Value[n][0] + "' AND 난방_냉방 = '냉방'");

                    CoolingZone_dataGridView.Rows.Add();
                    int nRow = CoolingZone_dataGridView.Rows.Count - 1;
                    CoolingZone_dataGridView.Rows[nRow].Cells[1].Value = Value[n][0];
                    if (층.Length > 0)
                    {
                        CoolingZone_dataGridView.Rows[nRow].Cells[2].Value = 층[0][0];
                    }
                    CoolingZone_dataGridView.Rows[nRow].Cells[3].Value = Value[n][1];
                    CoolingZone_dataGridView.Rows[nRow].Cells[4].Value = Value[n][2];
                    if (부하.Length > 0)
                    {
                        CoolingZone_dataGridView.Rows[nRow].Cells[5].Value = string.Format("{0:F2}", Convert.ToDouble(부하[0][0]));
                        CoolingZone_dataGridView.Rows[nRow].Cells[6].Value = string.Format("{0:F2}", Convert.ToDouble(부하[0][1]) / 1000);
                    }
                    CoolingZone_dataGridView.Rows[nRow].Cells[7].Value = string.Format("{0:F1}", Convert.ToDouble(Value[n][3]));
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
            foreach (DataGridViewRow row in CoolingZone_dataGridView.Rows)
            {
                if (Convert.ToBoolean(row.Cells["check"].Value))
                {
                    row.DefaultCellStyle.SelectionBackColor = SystemColors.GradientInactiveCaption;
                    SelectRow.Add(row.Index);
                }
            }
        }

        private void Save_Button_Click(object sender, EventArgs e)
        {
            SelectCheckBox();
            for (int k = 0; k < SelectRow.Count; k++)
            {
                if (k == SelectRow.Count - 1)
                {
                    this.SelectZone += CoolingZone_dataGridView.Rows[Convert.ToInt16(SelectRow[k])].Cells[1].Value.ToString();
                }
                else
                {
                    this.SelectZone += CoolingZone_dataGridView.Rows[Convert.ToInt16(SelectRow[k])].Cells[1].Value.ToString() + "+";
                }
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        public void reset()
        {
            SelectRow.Clear();
            SelectZone_split.Clear();
            this.SelectZone = null;

            for (int n = 0; n < CoolingZone_dataGridView.Rows.Count; n++)
            {
                CoolingZone_dataGridView.Rows[n].Cells[0].Value = false;
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
                for (int n = 0; n < CoolingZone_dataGridView.Rows.Count; n++)
                {
                    if (CoolingZone_dataGridView.Rows[n].Cells[1].Value.ToString() == SelectZone_split[k].ToString())
                    {
                        CoolingZone_dataGridView.Rows[n].Cells[0].Value = true;
                    }
                }
            }
        }
    }
}
