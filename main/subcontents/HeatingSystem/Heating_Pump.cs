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
    public partial class Heating_Pump : Form
    {

        public string SelectPump;
        ArrayList SelectPump_split = new ArrayList();
        public Heating_Pump(String Pump)
        {
            InitializeComponent(); this.Font = new Font(UTIL.Families[0], 9.75F, FontStyle.Regular);
            //heatingSystem = system;
            load_table_DB();
            string[][] Image = Program.DB.getValue(DB.type.BaseDB_HCneed, "메뉴아이콘", "하위메뉴아이콘", "하위메뉴명 = '장비일람표'");
            if (Image.Length > 0)
            {
                Icon_pictureBox.Load(Program.gPath + Image[0][0]);
                Icon_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            }
            if (Pump != null)
            {
                this.SelectPump = Pump;
                Load_SaveValue();
            }
        }

        void load_table_DB()
        {
            new StackedHeaderDecorator(Pump_dataGridView, DataGridViewAutoSizeColumnsMode.Fill, datagridviewDesign);
            // DataTable Pump_table = new DataTable();
            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
            Pump_dataGridView.Columns.Clear();
            checkBoxColumn.HeaderText = "선택";
            checkBoxColumn.Name = "check";
            Pump_dataGridView.Columns.Add(checkBoxColumn);
            Pump_dataGridView.Columns.Add("A1", "번호");
            Pump_dataGridView.Columns.Add("A2", "명칭");
            Pump_dataGridView.Columns.Add("A3", "종류");
            Pump_dataGridView.Columns.Add("A4", "효율.B.[%]");
            Pump_dataGridView.Columns.Add("A5", " .동력.[kW]");
            Pump_dataGridView.Columns[4].Visible = false;

            string[][] User_Value = Program.DB.getValue(DB.type.ProjDB, "User_Pump", "번호,명칭,종류,B효율,동력", "종류 ='온수순환펌프' OR 종류 = '냉온수순환펌프'OR 종류 = '지열순환펌프'");
            if (User_Value.Length > 0)
            {
                for (int n = 0; n < User_Value.Length; n++)
                {
                    string  B효율 = "", 동력 = "";
                  
                    if (User_Value[n][3] != null && User_Value[n][3] != "")
                    {
                        B효율 = string.Format("{0:F1}", Program.UTIL.ToDoubleOrZero(User_Value[n][3]));
                    }
                    if (User_Value[n][4] != null && User_Value[n][4] != "")
                    {
                        동력 = string.Format("{0:F0}", Program.UTIL.ToDoubleOrZero(User_Value[n][4]));
                    }

                    Pump_dataGridView.Rows.Add();
                    int nRow2 = Pump_dataGridView.Rows.Count - 1;
                    Pump_dataGridView.Rows[nRow2].Cells[1].Value = User_Value[n][0];
                    Pump_dataGridView.Rows[nRow2].Cells[2].Value = User_Value[n][1];
                    Pump_dataGridView.Rows[nRow2].Cells[3].Value = User_Value[n][2];
                    Pump_dataGridView.Rows[nRow2].Cells[4].Value = B효율;
                    Pump_dataGridView.Rows[nRow2].Cells[5].Value = 동력;
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

        private void Save_button_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in Pump_dataGridView.Rows)
            {
                if (Convert.ToBoolean(row.Cells["check"].Value))
                {
                    row.DefaultCellStyle.SelectionBackColor = SystemColors.GradientInactiveCaption;

                    SelectPump = Pump_dataGridView.Rows[Convert.ToInt16(row.Index)].Cells[1].Value.ToString();
                }
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        private void Load_SaveValue()
        {
            for (int n = 0; n < Pump_dataGridView.Rows.Count; n++)
            {
                if (Pump_dataGridView.Rows[n].Cells[1].Value.ToString() == SelectPump.ToString())
                {
                    Pump_dataGridView.Rows[n].Cells[0].Value = true;
                }
            }
        }
    }
}
