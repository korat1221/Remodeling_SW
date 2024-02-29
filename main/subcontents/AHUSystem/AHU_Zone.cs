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
    public partial class AHU_Zone : Form
    {
        double Count_DB;
        ArrayList SelectRow = new ArrayList(); ArrayList SelectZone_split = new ArrayList();
        String SystemNum;
        public string SelectZone;
        string AHUOptions;

        public AHU_Zone(String Num, String SelectZone_nonsplit, string AHUOptions)
        {
            InitializeComponent();

            if (AHUOptions == "공조기")
            {
                this.AHUOptions = AHUOptions;
            }
            else
            {
                this.AHUOptions = "열회수환기_멀티존";
            }

            load_table_DB();
            SystemNum = Num;
            if (SelectZone_nonsplit != null)
            {
                Load_SaveValue(SelectZone_nonsplit);
            }


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

            Zone_dataGridView.Columns.Add("A1", "존번호");
            Zone_dataGridView.Columns.Add("A2", "층");
            Zone_dataGridView.Columns.Add("A3", "존명칭");
            Zone_dataGridView.Columns.Add("A4", "용도프로필");
            Zone_dataGridView.Columns.Add("A5", "연간 난방요구량.[kWh/a]");
            Zone_dataGridView.Columns.Add("A6", "연간 냉방요구량.[kWh/a]");
            Zone_dataGridView.Columns.Add("A7", "면적.[m²]");
            Zone_dataGridView.Columns.Add("A8", "공조시스템");

            string[][] Value = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_Form", "존번호,존이름,용도프로필,순바닥면적", "환기방식 = '" + AHUOptions + "'");
            if (Value.Length > 0)
            {
                for (int n = 0; n < Value.Length; n++)
                {
                    string[][] 층 = Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope_3D", "층", "존 ='" + Value[n][0] + "'");
                    string[][] 요구량_난방 = Program.DB.getValue(DB.type.ProjDB, "Zone_HCneed_Result", "Qb_a", "번호 ='" + Value[n][0] + "' AND 난방_냉방 = '난방'");
                    string[][] 요구량_냉방 = Program.DB.getValue(DB.type.ProjDB, "Zone_HCneed_Result", "Qb_a", "번호 ='" + Value[n][0] + "' AND 난방_냉방 = '냉방'");
                    int nRow = Zone_dataGridView.Rows.Add();
                    Zone_dataGridView.Rows[nRow].Cells[1].Value = Value[n][0];
                    if (층.Length > 0)
                    {
                        Zone_dataGridView.Rows[nRow].Cells[2].Value = 층[0][0];
                    }
                    Zone_dataGridView.Rows[nRow].Cells[3].Value = Value[n][1];
                    Zone_dataGridView.Rows[nRow].Cells[4].Value = Value[n][2];
                    if (요구량_난방.Length > 0)
                    {
                        Zone_dataGridView.Rows[nRow].Cells[5].Value = string.Format("{0:F2}", Convert.ToDouble(요구량_난방[0][0]));
                    }
                    if (요구량_냉방.Length > 0)
                    {
                        Zone_dataGridView.Rows[nRow].Cells[6].Value = string.Format("{0:F2}", Convert.ToDouble(요구량_냉방[0][0]));
                    }
                    Zone_dataGridView.Rows[nRow].Cells[7].Value = string.Format("{0:F1}", Convert.ToDouble(Value[n][3]));
                    if (요구량_난방.Length > 0)
                    {
                        Zone_dataGridView.Rows[nRow].Cells[5].Value = string.Format("{0:F2}", Convert.ToDouble(요구량_난방[0][0]));
                    }

                    Count_DB = Value.Length;
                }
            }
        }

        private void SelectZone_Load()
        {
            string[][] 공조;
            if (AHUOptions == "공조기")
            { 공조 = Program.DB.getValue(DB.type.ProjDB, "AHUSystem_form", "번호,존번호", "유형='공조기'"); }
            else { 공조 = Program.DB.getValue(DB.type.ProjDB, "AHUSystem_form", "번호,존번호", "유형='열회수기'"); }

            if (공조.Length > 0)
            {
                for (int k = 0; k < 공조.Length; k++)
                {

                    if (공조[k][1].Contains("+"))
                    {
                        string[] 존번호 = 공조[k][1].Split('+');
                        for (int nRow = 0; nRow < Zone_dataGridView.Rows.Count; nRow++)
                        {
                            for (int n = 0; n < 존번호.Length; n++)
                            {
                                if (Zone_dataGridView.Rows[nRow].Cells[1].Value.ToString() == 존번호[n])
                                {
                                    Zone_dataGridView.Rows[nRow].Cells[8].Value = 공조[k][0];
                                }
                            }
                        }
                    }
                    else
                    {
                        for (int nRow = 0; nRow < Zone_dataGridView.Rows.Count; nRow++)
                        {
                            if (Zone_dataGridView.Rows[nRow].Cells[1].Value.ToString() == 공조[k][1])
                            {
                                Zone_dataGridView.Rows[nRow].Cells[8].Value = 공조[k][0];
                            }
                        }
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

