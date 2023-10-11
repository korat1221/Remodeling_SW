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

namespace main.subcontents.CoolingSystem
{
    public partial class Cooling_AirCon : Form
    {
        public List<string> SelectAirCon = new List<string>();
        public Cooling_AirCon()
        {
            InitializeComponent();
            string[][] Image = Program.DB.getValue(DB.type.BaseDB_HCneed, "메뉴아이콘", "하위메뉴아이콘", "하위메뉴명 = '장비일람표'");
            Icon_pictureBox.Load(Program.gPath + Image[0][0]);
            Icon_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            load_table_DB();
        }

        private void load_table_DB() 
        {
            new StackedHeaderDecorator(AirCon_dataGridView, DataGridViewAutoSizeColumnsMode.Fill, datagridviewDesign);
            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
            AirCon_dataGridView.Columns.Clear();
            checkBoxColumn.HeaderText = "선택";
            checkBoxColumn.Name = "check";
            AirCon_dataGridView.Columns.Add(checkBoxColumn);
            
            AirCon_dataGridView.Columns.Add("A1", "번호");
            AirCon_dataGridView.Columns.Add("A2", "명칭");
            AirCon_dataGridView.Columns.Add("A3", "냉방성능.출력[kW]");
            AirCon_dataGridView.Columns.Add("A4", "냉방성능.소비전력[kW]");
            AirCon_dataGridView.Columns.Add("A5", "냉방성능.EER[W/w]");
            AirCon_dataGridView.Columns.Add("A6", "대기전력[W]");
            AirCon_dataGridView.Columns.Add("A7", "연료");
            
            string[][] DefaultDB_Value = Program.DB.getValue(DB.type.ProjDB, "User_AirHP", "번호,명칭,냉방정격용량,냉방정격소비전력,냉방정격COP,대기전력,연료", "");
            for (int i = 0; i < DefaultDB_Value.Length; i++)
            {
                if (Convert.ToDouble(DefaultDB_Value[i][2]) <= 12)
                {
                    AirCon_dataGridView.Rows.Add();
                    int nRow = AirCon_dataGridView.Rows.Count - 1;
                    AirCon_dataGridView.Rows[nRow].Cells[1].Value = DefaultDB_Value[i][0];
                    AirCon_dataGridView.Rows[nRow].Cells[2].Value = DefaultDB_Value[i][1];
                    AirCon_dataGridView.Rows[nRow].Cells[3].Value = string.Format("{0:F1}", Convert.ToDouble(DefaultDB_Value[i][2]));
                    AirCon_dataGridView.Rows[nRow].Cells[4].Value = string.Format("{0:F1}", Convert.ToDouble(DefaultDB_Value[i][3]));
                    AirCon_dataGridView.Rows[nRow].Cells[5].Value = string.Format("{0:F1}", Convert.ToDouble(DefaultDB_Value[i][4]));
                    AirCon_dataGridView.Rows[nRow].Cells[6].Value = DefaultDB_Value[i][5];
                    AirCon_dataGridView.Rows[nRow].Cells[7].Value = DefaultDB_Value[i][6];
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
            foreach (DataGridViewRow row in AirCon_dataGridView.Rows)
            {
                if (Convert.ToBoolean(row.Cells["check"].Value))
                {
                    row.DefaultCellStyle.SelectionBackColor = SystemColors.GradientInactiveCaption;
                }
            }
        }
        private void Save_button_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in AirCon_dataGridView.Rows)
            {
                if (Convert.ToBoolean(row.Cells["check"].Value))
                {
                    SelectAirCon.Add(row.Cells[1].Value.ToString()); //선택항목 이름 저장함
                }
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
