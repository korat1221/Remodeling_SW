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

namespace main.subcontents.DHWSystem
{
    public partial class DHW_BoilerDB : Form
    {
        ArrayList SelectRow = new ArrayList(); ArrayList SelectBoiler_split = new ArrayList();
        String DefaultUse;
        public string SelectBoiler;
        //HeatingSystem heatingSystem;

        // public Heating_Boiler(HeatingSystem system)
        public DHW_BoilerDB(String DefaultUse, String SelectBoiler_nonsplit)
        {
            InitializeComponent();
            //heatingSystem = system;
            this.DefaultUse = DefaultUse;
            load_table_DB();
            string[][] Image = Program.DB.getValue(DB.type.BaseDB_HCneed, "메뉴아이콘", "하위메뉴아이콘", "하위메뉴명 = '장비일람표'");
            if (Image.Length > 0)
            {
                Icon_pictureBox.Load(Program.gPath + Image[0][0]);
                Icon_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            }
            if (SelectBoiler_nonsplit != null)
            {
                Load_SaveValue(SelectBoiler_nonsplit);
            }

        }

        void load_table_DB()
        {
            //DataTable Boiler_table = new DataTable();
            new StackedHeaderDecorator(Boiler_dataGridView, DataGridViewAutoSizeColumnsMode.Fill, datagridviewDesign);
            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
            Boiler_dataGridView.Columns.Clear();
            checkBoxColumn.HeaderText = "선택";
            checkBoxColumn.Name = "check";
            Boiler_dataGridView.Columns.Add(checkBoxColumn);

            Boiler_dataGridView.Columns.Add("A1", "번호");
            Boiler_dataGridView.Columns.Add("A2", "명칭");
            Boiler_dataGridView.Columns.Add("A3", "연료");
            //Boiler_table.Columns.Add("번호", typeof(string));
            //Boiler_table.Columns.Add("명칭", typeof(string));
            //Boiler_table.Columns.Add("연료", typeof(string));
            if (DefaultUse != "기본DB 적용")
            {
                Boiler_dataGridView.Columns.Add("A4", "Type");
                Boiler_dataGridView.Columns.Add("A5", "용량.[kW]");
                //Boiler_table.Columns.Add("Type", typeof(string));
                //Boiler_table.Columns.Add("용량" + Environment.NewLine + "[kW]", typeof(string));
            }
            Boiler_dataGridView.Columns.Add("A6", "전부하효율.[%]");
            Boiler_dataGridView.Columns.Add("A7", "부분부하효율.[%]");
            Boiler_dataGridView.Columns.Add("A8", "소비전력.[W]");
            Boiler_dataGridView.Columns.Add("A9", "대기전력.[W]");
            //Boiler_table.Columns.Add("전부하효율" + Environment.NewLine + "[%]", typeof(string));
            //Boiler_table.Columns.Add("부분부하효율" + Environment.NewLine + "[%]", typeof(string));
            //Boiler_table.Columns.Add("소비전력" + Environment.NewLine + "[W]", typeof(string));
            //Boiler_table.Columns.Add("대기전력" + Environment.NewLine + "[W]", typeof(string));

            if (DefaultUse == "기본DB 적용")
            {
                string[][] DefaultDB_Value = Program.DB.getValue(DB.type.BaseDB_Heating, "보일러", "번호,제품명,연료,전부하효율,부분부하효율,소비전력,대기전력", "");
                if (DefaultDB_Value.Length > 0)
                {
                    for (int n = 0; n < DefaultDB_Value.Length; n++)
                    {
                        Boiler_dataGridView.Rows.Add();
                        int nRow = Boiler_dataGridView.Rows.Count - 1;
                        Boiler_dataGridView.Rows[nRow].Cells[1].Value = DefaultDB_Value[n][0];
                        Boiler_dataGridView.Rows[nRow].Cells[2].Value = DefaultDB_Value[n][1];
                        Boiler_dataGridView.Rows[nRow].Cells[3].Value = DefaultDB_Value[n][2];
                        Boiler_dataGridView.Rows[nRow].Cells[4].Value = (Convert.ToDouble(DefaultDB_Value[n][3]) * 100).ToString();
                        Boiler_dataGridView.Rows[nRow].Cells[5].Value = (Convert.ToDouble(DefaultDB_Value[n][4]) * 100).ToString();
                        Boiler_dataGridView.Rows[nRow].Cells[6].Value = string.Format("{0:F1}", Convert.ToDouble(DefaultDB_Value[n][5]));
                        Boiler_dataGridView.Rows[nRow].Cells[7].Value = string.Format("{0:F0}", Convert.ToDouble(DefaultDB_Value[n][6]));
                        // Boiler_table.Rows.Add(DefaultDB_Value[n][0], DefaultDB_Value[n][1], DefaultDB_Value[n][2], (Convert.ToDouble(DefaultDB_Value[n][3]) * 100).ToString(), (Convert.ToDouble(DefaultDB_Value[n][4]) * 100).ToString(), string.Format("{0:F1}", Convert.ToDouble(DefaultDB_Value[n][5])), string.Format("{0:F0}", Convert.ToDouble(DefaultDB_Value[n][6])));
                    }
                }
            }
            else
            {
                string[][] User_Value = Program.DB.getValue(DB.type.ProjDB, "User_Boiler", "번호,명칭,연료,Type,용량,전부하효율,부분부하효율,소비전력,대기전력", "난방급탕 ='급탕' OR 난방급탕 = '난방+급탕'");
                if (User_Value.Length > 0)
                {
                    for (int n = 0; n < User_Value.Length; n++)
                    {
                        string 용량 = "", 전부하효율 = "", 부분부하효율 = "", 소비전력 = "", 대기전력 = "";
                        if (User_Value[n][4] != null && User_Value[n][4] != "")
                        {
                            용량 = string.Format("{0:F1}", Convert.ToDouble(User_Value[n][4]));
                        }
                        if (User_Value[n][5] != null && User_Value[n][5] != "")
                        {
                            전부하효율 = string.Format("{0:F1}", Convert.ToDouble(User_Value[n][5]));
                        }
                        if (User_Value[n][6] != null && User_Value[n][6] != "")
                        {
                            부분부하효율 = string.Format("{0:F1}", Convert.ToDouble(User_Value[n][6]));
                        }
                        if (User_Value[n][7] != null && User_Value[n][7] != "")
                        {
                            소비전력 = string.Format("{0:F0}", Convert.ToDouble(User_Value[n][7]));
                        }
                        if (User_Value[n][8] != null && User_Value[n][8] != "")
                        {
                            대기전력 = string.Format("{0:F0}", Convert.ToDouble(User_Value[n][8]));
                        }
                        Boiler_dataGridView.Rows.Add();
                        int nRow = Boiler_dataGridView.Rows.Count - 1;
                        Boiler_dataGridView.Rows[nRow].Cells[1].Value = User_Value[n][0];
                        Boiler_dataGridView.Rows[nRow].Cells[2].Value = User_Value[n][1];
                        Boiler_dataGridView.Rows[nRow].Cells[3].Value = User_Value[n][2];
                        Boiler_dataGridView.Rows[nRow].Cells[4].Value = User_Value[n][3];
                        Boiler_dataGridView.Rows[nRow].Cells[5].Value = 용량;
                        Boiler_dataGridView.Rows[nRow].Cells[6].Value = 전부하효율;
                        Boiler_dataGridView.Rows[nRow].Cells[7].Value = 부분부하효율;
                        Boiler_dataGridView.Rows[nRow].Cells[8].Value = 소비전력;
                        Boiler_dataGridView.Rows[nRow].Cells[9].Value = 대기전력;
                        //Boiler_table.Rows.Add(User_Value[n][0], User_Value[n][1], User_Value[n][2], User_Value[n][3], 용량, 전부하효율, 부분부하효율, 소비전력, 대기전력);
                    }
                }
            }
            // Boiler_dataGridView.DataSource = Boiler_table;
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
            foreach (DataGridViewRow row in Boiler_dataGridView.Rows)
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
                    SelectBoiler += Boiler_dataGridView.Rows[Convert.ToInt16(SelectRow[k])].Cells[1].Value.ToString();
                }
                else
                {
                    SelectBoiler += Boiler_dataGridView.Rows[Convert.ToInt16(SelectRow[k])].Cells[1].Value.ToString() + "+";
                }
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        private void reset()
        {
            SelectRow.Clear();
            SelectBoiler_split.Clear();
            SelectBoiler = null;

            for (int n = 0; n < Boiler_dataGridView.Rows.Count; n++)
            {
                Boiler_dataGridView.Rows[n].Cells[0].Value = false;
            }

        }
        private void Load_SaveValue(String SelectBoiler_nonsplit)
        {
            reset();
            string[] token = SelectBoiler_nonsplit.Split('+');
            SelectBoiler_split.Clear();
            foreach (var item in token)
            {
                SelectBoiler_split.Add(item.ToString());
            }
            for (int k = 0; k < SelectBoiler_split.Count; k++)
            {
                for (int n = 0; n < Boiler_dataGridView.Rows.Count; n++)
                {
                    if (Boiler_dataGridView.Rows[n].Cells[1].Value.ToString() == SelectBoiler_split[k].ToString())
                    {
                        Boiler_dataGridView.Rows[n].Cells[0].Value = true;
                    }
                }
            }
        }
    }
}
