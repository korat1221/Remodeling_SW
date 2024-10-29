using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace main.subcontents
{
    public partial class WaterCooler_DB : Form
    {
        String DefaultUse;
        public string SelectWC;

        public WaterCooler_DB(String DefaultUse) //, String SelectHP_nonsplit, String CoolSource, String HC)
        {
            InitializeComponent(); this.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular);
            this.DefaultUse = DefaultUse;

            WC_textBox.Text += "▣ DIN V 18599-7: 2018 표21에 제시된 표준값 \r\n";
            WC_textBox.Text += "▣ 수냉식냉동기의 EER은 냉각탑 제외 냉방성능임 \r\n";


            string[][] Image = Program.DB.getValue(DB.type.BaseDB_HCneed, "메뉴아이콘", "하위메뉴아이콘", "하위메뉴명 = '장비일람표'");
            if (Image.Length > 0)
            {
                Icon_pictureBox.Load(Program.gPath + Image[0][0]);
                Icon_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            }
            create_table(DefaultUse);
            load_table_DB(DefaultUse);
        }


        void create_table(String DefaultUse)
        {
            new StackedHeaderDecorator(WaterCooler_dataGridView, DataGridViewAutoSizeColumnsMode.Fill, datagridviewDesign);
            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
            WaterCooler_dataGridView.Columns.Clear();
            checkBoxColumn.HeaderText = "선택";
            checkBoxColumn.Name = "check";
            WaterCooler_dataGridView.Columns.Add(checkBoxColumn);

            if (DefaultUse == "기본DB 적용")
            {
                WaterCooler_dataGridView.Columns.Add("A1", "번호");
                WaterCooler_dataGridView.Columns.Add("A2", "압축기");
                WaterCooler_dataGridView.Columns.Add("A3", "냉매");
                WaterCooler_dataGridView.Columns.Add("A4", "냉수온도.출구[℃]");
                WaterCooler_dataGridView.Columns.Add("A5", "냉각수온도.입구[℃]");
                WaterCooler_dataGridView.Columns.Add("A6", "냉각수온도.출구[℃]");
                WaterCooler_dataGridView.Columns.Add("A7", "냉방성능.EER[W/W]");
                WaterCooler_dataGridView.Columns.Add("A8", "비고");
            }
            else
            {

            }
            reset();
        }
        void load_table_DB(String DefaultUse)
        {
            WaterCooler_dataGridView.Rows.Clear();
            if (DefaultUse == "기본DB 적용")
            {
                string[][] DefaultDB_Value = Program.DB.getValue(DB.type.BaseDB_Cooling, "WaterCooler", "번호,압축기,냉매,냉수출구온도,냉각수입구온도,냉각수출구온도,EER,비고");
                if (DefaultDB_Value.Length > 0)
                {
                    for (int n = 0; n < DefaultDB_Value.Length; n++)
                    {
                        WaterCooler_dataGridView.Rows.Add();
                        int nRow = WaterCooler_dataGridView.Rows.Count - 1;
                        WaterCooler_dataGridView.Rows[nRow].Cells[1].Value = DefaultDB_Value[n][0];
                        WaterCooler_dataGridView.Rows[nRow].Cells[2].Value = DefaultDB_Value[n][1];
                        WaterCooler_dataGridView.Rows[nRow].Cells[3].Value = DefaultDB_Value[n][2];
                        WaterCooler_dataGridView.Rows[nRow].Cells[4].Value = DefaultDB_Value[n][3];
                        WaterCooler_dataGridView.Rows[nRow].Cells[5].Value = DefaultDB_Value[n][4];
                        WaterCooler_dataGridView.Rows[nRow].Cells[6].Value = DefaultDB_Value[n][5];
                        WaterCooler_dataGridView.Rows[nRow].Cells[7].Value = DefaultDB_Value[n][6];
                        WaterCooler_dataGridView.Rows[nRow].Cells[8].Value = DefaultDB_Value[n][7];
                    }
                    WaterCooler_dataGridView.Columns[0].Width = 40;
                    WaterCooler_dataGridView.Columns[1].Width = 60;
                    WaterCooler_dataGridView.Columns[2].Width = 60;
                    WaterCooler_dataGridView.Columns[3].Width = 70;
                    WaterCooler_dataGridView.Columns[8].Width = 170;
                }
            }
            else
            {

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
            foreach (DataGridViewRow row in WaterCooler_dataGridView.Rows)
            {
                if (Convert.ToBoolean(row.Cells["check"].Value))
                {
                    row.DefaultCellStyle.SelectionBackColor = SystemColors.GradientInactiveCaption;
                    SelectWC = row.Cells[1].Value.ToString();
                }
            }
        }

        private void Save_button_Click(object sender, EventArgs e)
        {
            SelectWC = null;
            SelectCheckBox();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        private void reset()
        {
            SelectWC = null;

            for (int n = 0; n < WaterCooler_dataGridView.Rows.Count; n++)
            {
                WaterCooler_dataGridView.Rows[n].Cells[0].Value = false;
            }

        }
    }
}
