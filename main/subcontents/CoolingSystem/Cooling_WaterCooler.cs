
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace main.subcontents.CoolingSystem
{
    public partial class Cooling_WaterCooler : Form
    {
        List<int> SelectRow = new List<int>();
        List<string> SelectCG_split = new List<string>();
        List<string> SelectCGN_split = new List<string>();


        string SystemNum;
        public string SelectCG, SelectCGN;

        public Cooling_WaterCooler(string _Num, string _SelectCG_nonsplit, string _SelectCGN_nonsplit)
        {
            InitializeComponent(); this.Font = new Font(UTIL.Families[0], 9.75F, FontStyle.Regular);
            string[][] Image = Program.DB.getValue(DB.type.BaseDB_HCneed, "메뉴아이콘", "하위메뉴아이콘", "하위메뉴명 = '장비일람표'");
            Icon_pictureBox.Load(Program.gPath + Image[0][0]);
            Icon_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            load_table_DB();

            SystemNum = _Num;

            if (_SelectCG_nonsplit != null)
            {
                Load_SaveValue(_SelectCG_nonsplit, _SelectCGN_nonsplit);
            }
        }


        private void load_table_DB() //번호자동생성, 
        {
            TableMake();
                        
            string[][] DefaultDB_Value = Program.DB.getValue(DB.type.ProjDB, "User_WaterCooler", " 번호,명칭,냉방출력,냉방소비전력,EER,대기전력,연료,압축기,설치,냉수입구온도,냉수출구온도", "");
            for (int i = 0; i < DefaultDB_Value.Length; i++)
            {
                int nRow = WaterCooler_dataGridView.Rows.Add();
                
                WaterCooler_dataGridView.Rows[nRow].Cells[2].Value = DefaultDB_Value[i][0];//번호
                WaterCooler_dataGridView.Rows[nRow].Cells[3].Value = DefaultDB_Value[i][1]; //명칭
                WaterCooler_dataGridView.Rows[nRow].Cells[4].Value = string.Format("{0:F1}", Convert.ToDouble(DefaultDB_Value[i][2]));//냉방출력
                WaterCooler_dataGridView.Rows[nRow].Cells[5].Value = string.Format("{0:F1}", Convert.ToDouble(DefaultDB_Value[i][3]));//냉방소비전력
                WaterCooler_dataGridView.Rows[nRow].Cells[6].Value = string.Format("{0:F1}", Convert.ToDouble(DefaultDB_Value[i][4])); //EER
                WaterCooler_dataGridView.Rows[nRow].Cells[7].Value = DefaultDB_Value[i][5]; //대기전력
                WaterCooler_dataGridView.Rows[nRow].Cells[8].Value = DefaultDB_Value[i][6]; //연료
                WaterCooler_dataGridView.Rows[nRow].Cells[9].Value = DefaultDB_Value[i][7]; //압축기
                WaterCooler_dataGridView.Rows[nRow].Cells[10].Value = DefaultDB_Value[i][8]; //설치
                WaterCooler_dataGridView.Rows[nRow].Cells[11].Value = DefaultDB_Value[i][9]; //냉수입구온도
                WaterCooler_dataGridView.Rows[nRow].Cells[12].Value = DefaultDB_Value[i][10]; //냉수출구온도
            }
        }
        private void TableMake()
        {
            new StackedHeaderDecorator(WaterCooler_dataGridView, DataGridViewAutoSizeColumnsMode.Fill, datagridviewDesign);
            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
            WaterCooler_dataGridView.Columns.Clear();
            checkBoxColumn.HeaderText = "선택";
            checkBoxColumn.Name = "check";
            WaterCooler_dataGridView.Columns.Add(checkBoxColumn);

            WaterCooler_dataGridView.Columns.Add("A1", "설치대수");
            WaterCooler_dataGridView.Columns.Add("A2", "번호");
            WaterCooler_dataGridView.Columns.Add("A3", "명칭");
            WaterCooler_dataGridView.Columns.Add("A4", "냉방성능.출력[kW]");
            WaterCooler_dataGridView.Columns.Add("A5", "냉방성능.소비전력[kW]");
            WaterCooler_dataGridView.Columns.Add("A6", "냉방성능.EER[W/w]");
            WaterCooler_dataGridView.Columns.Add("A7", "대기전력[W]");
            WaterCooler_dataGridView.Columns.Add("A8", "연료");
            WaterCooler_dataGridView.Columns.Add("A9", "압축기");
            WaterCooler_dataGridView.Columns.Add("A10", "설치");
            WaterCooler_dataGridView.Columns.Add("A11", "냉수온도.입구[℃]");
            WaterCooler_dataGridView.Columns.Add("A12", "냉수온도.출구[℃]");
        }

        private Boolean datagridviewDesign(DataGridViewCell cell, int column, int row)
        {
            if (column == 1)
            {
                cell.Style.BackColor = Color.FromArgb(255, 248, 206);
                return true;
            }
           

            else if (row % 2 == 1)
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
        private bool SelectCheckBox()
        {
            SelectRow.Clear();
            foreach (DataGridViewRow row in WaterCooler_dataGridView.Rows)
            {
                if (Convert.ToBoolean(row.Cells["check"].Value))
                {
                    row.DefaultCellStyle.SelectionBackColor = SystemColors.GradientInactiveCaption;
                    SelectRow.Add(row.Index);
                    if (row.Cells[1].Value == null)
                    {
                        MessageBox.Show("설치대수를 입력해주세요.", "Check", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return false;
                    }
                }
            }
            return true;
        }
        private void Save_button_Click(object sender, EventArgs e)
        {
            if (SelectCheckBox() == false)
            {
                return;
            }

            for (int k = 0; k < SelectRow.Count; k++)
            {
                if (k == SelectRow.Count - 1)
                {
                    this.SelectCGN += Program.UTIL.dataGridView_doubleComa(WaterCooler_dataGridView, Convert.ToInt32(SelectRow[k]), 1, 0).ToString();
                    this.SelectCG += WaterCooler_dataGridView.Rows[Convert.ToInt32(SelectRow[k])].Cells[2].Value.ToString();

                }
                else
                {
                    this.SelectCGN += Program.UTIL.dataGridView_doubleComa(WaterCooler_dataGridView, Convert.ToInt32(SelectRow[k]), 1, 0).ToString() + "+";
                    this.SelectCG += WaterCooler_dataGridView.Rows[Convert.ToInt32(SelectRow[k])].Cells[2].Value.ToString() + "+";
                }
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        public void reset()
        {
            SelectRow.Clear();
            SelectCG_split.Clear();
            SelectCGN_split.Clear();

            this.SelectCG = null;
            this.SelectCGN = null;

            for (int n = 0; n < WaterCooler_dataGridView.Rows.Count; n++)
            {
                WaterCooler_dataGridView.Rows[n].Cells[0].Value = false;
                WaterCooler_dataGridView.Rows[n].Cells[1].Value = null;
            }
        }
        private void Load_SaveValue(string _SelectCG_nonsplit, string _SelectCGN_nonsplit)
        {
            reset();
            string[] token = _SelectCG_nonsplit.Split('+');
            string[] value = _SelectCGN_nonsplit.Split("+");
           
            foreach (var item in token)
            {
                SelectCG_split.Add(item.ToString());
            }
            foreach (var val in value)
            {
                SelectCGN_split.Add(val.ToString());
            }

            for (int k = 0; k < SelectCG_split.Count; k++)
            {
                for (int n = 0; n < WaterCooler_dataGridView.Rows.Count; n++)
                {
                    if (WaterCooler_dataGridView.Rows[n].Cells[2].Value.ToString() == SelectCG_split[k].ToString())
                    {
                        WaterCooler_dataGridView.Rows[n].Cells[0].Value = true;
                        WaterCooler_dataGridView.Rows[n].Cells[1].Value = value[k];
                    }
                }
            }
        }
    }
}
