using System;
using System.Collections;
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
    public partial class AirCooler_DB : Form
    {
        String DefaultUse;
        public string SelectAC;

        public AirCooler_DB(String DefaultUse) //, String SelectHP_nonsplit, String CoolSource, String HC)
        {
            InitializeComponent(); this.Font = new Font(UTIL.Families[0], 9.75F, FontStyle.Regular);
            this.DefaultUse = DefaultUse;
            
            AC_textBox.Text += "▣ DIN V 18599-7: 2018 표23에 제시된 표준값 \r\n";
            AC_textBox.Text += "▣ 공냉식냉동기의 EER은 송풍기 소비전력을 포함한 냉방성능임 \r\n";
            AC_textBox.Text += "▣ 따라서 송풍기 소비전력은 '0'으로 반영됨";
          
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
            new StackedHeaderDecorator(AirCooler_dataGridView, DataGridViewAutoSizeColumnsMode.Fill, datagridviewDesign);
            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
            AirCooler_dataGridView.Columns.Clear();
            checkBoxColumn.HeaderText = "선택";
            checkBoxColumn.Name = "check";
            AirCooler_dataGridView.Columns.Add(checkBoxColumn);

            if (DefaultUse == "기본DB 적용")
            {
                AirCooler_dataGridView.Columns.Add("A1", "번호");
                AirCooler_dataGridView.Columns.Add("A2", "압축기");
                AirCooler_dataGridView.Columns.Add("A3", "냉매");
                AirCooler_dataGridView.Columns.Add("A4", "온도.냉수출구[℃]");
                AirCooler_dataGridView.Columns.Add("A5", "온도.증발기평균[℃]");
                AirCooler_dataGridView.Columns.Add("A6", "냉방성능.EER[W/W]");
                AirCooler_dataGridView.Columns.Add("A7", "비고");
            }
            else
            {

            }
            reset();
        }
        void load_table_DB(String DefaultUse)
        {
            AirCooler_dataGridView.Rows.Clear();
            if (DefaultUse == "기본DB 적용")
            {
                string[][] DefaultDB_Value = Program.DB.getValue(DB.type.BaseDB_Cooling, "AirCooler", "번호,압축기,냉매,냉수출구온도,평균증발기온도,EER,비고");
                if (DefaultDB_Value.Length > 0)
                {
                    for (int n = 0; n < DefaultDB_Value.Length; n++)
                    {
                        AirCooler_dataGridView.Rows.Add();
                        int nRow = AirCooler_dataGridView.Rows.Count - 1;
                        AirCooler_dataGridView.Rows[nRow].Cells[1].Value = DefaultDB_Value[n][0];
                        AirCooler_dataGridView.Rows[nRow].Cells[2].Value = DefaultDB_Value[n][1];
                        AirCooler_dataGridView.Rows[nRow].Cells[3].Value = DefaultDB_Value[n][2];
                        AirCooler_dataGridView.Rows[nRow].Cells[4].Value = DefaultDB_Value[n][3];
                        AirCooler_dataGridView.Rows[nRow].Cells[5].Value = DefaultDB_Value[n][4];
                        AirCooler_dataGridView.Rows[nRow].Cells[6].Value = DefaultDB_Value[n][5];
                        AirCooler_dataGridView.Rows[nRow].Cells[7].Value = DefaultDB_Value[n][6];
                    }
                    AirCooler_dataGridView.Columns[0].Width = 40;
                    AirCooler_dataGridView.Columns[1].Width = 60;
                    AirCooler_dataGridView.Columns[2].Width = 60;
                    AirCooler_dataGridView.Columns[3].Width = 70;
                    AirCooler_dataGridView.Columns[7].Width = 170;
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
            foreach (DataGridViewRow row in AirCooler_dataGridView.Rows)
            {
                if (Convert.ToBoolean(row.Cells["check"].Value))
                {
                    row.DefaultCellStyle.SelectionBackColor = SystemColors.GradientInactiveCaption;
                    SelectAC = row.Cells[1].Value.ToString();
                }
            }
        }

        private void Save_button_Click(object sender, EventArgs e)
        {
            SelectAC = null;
            SelectCheckBox();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        private void reset()
        {
            SelectAC = null;

            for (int n = 0; n < AirCooler_dataGridView.Rows.Count; n++)
            {
                AirCooler_dataGridView.Rows[n].Cells[0].Value = false;
            }

        }
    }
}

