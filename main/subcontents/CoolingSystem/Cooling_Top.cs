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
    public partial class Cooling_Top : Form
    {
        List<int> SelectRow = new List<int>();
        List<string> SelectCT_split = new List<string>();
        //List<string> SelectCTN_split = new List<string>();


        string SystemNum;
        public string SelectCT;
        public Cooling_Top(string _Num, string _SelectCT_nonsplit)
        {
            InitializeComponent(); this.Font = new Font(UTIL.Families[0], 9.75F, FontStyle.Regular);
            string[][] Image = Program.DB.getValue(DB.type.BaseDB_HCneed, "메뉴아이콘", "하위메뉴아이콘", "하위메뉴명 = '장비일람표'");
            Icon_pictureBox.Load(Program.gPath + Image[0][0]);
            Icon_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            load_table_DB();
            SystemNum = _Num;

            if (_SelectCT_nonsplit != null)
            {
                Load_SaveValue(_SelectCT_nonsplit);
            }
        }

        private void load_table_DB()
        {
            TableMake();

            string[][] DefaultDB_Value = Program.DB.getValue(DB.type.ProjDB, "User_CoolingTop", " 번호,명칭,형식,냉각능력,냉각수량, 입구온도,출구온도,팬유형,냉방전력소비계수,대기전력,설치", "");
            for (int i = 0; i < DefaultDB_Value.Length; i++)
            {
                CoolerTop_dataGridView.Rows.Add();
                int nRow = CoolerTop_dataGridView.Rows.Count - 1;

                CoolerTop_dataGridView.Rows[nRow].Cells[1].Value = DefaultDB_Value[i][0];//번호
                CoolerTop_dataGridView.Rows[nRow].Cells[2].Value = DefaultDB_Value[i][1]; //명칭
                CoolerTop_dataGridView.Rows[nRow].Cells[3].Value = DefaultDB_Value[i][2]; //형식
                CoolerTop_dataGridView.Rows[nRow].Cells[4].Value = string.Format("{0:F1}", Program.UTIL.ToDoubleOrZero(DefaultDB_Value[i][3]));//냉각능력
                CoolerTop_dataGridView.Rows[nRow].Cells[5].Value = string.Format("{0:F1}", Program.UTIL.ToDoubleOrZero(DefaultDB_Value[i][4]));//냉각수량
                CoolerTop_dataGridView.Rows[nRow].Cells[6].Value = DefaultDB_Value[i][5]; //입구온도
                CoolerTop_dataGridView.Rows[nRow].Cells[7].Value = DefaultDB_Value[i][6]; //출구온도
                CoolerTop_dataGridView.Rows[nRow].Cells[8].Value = DefaultDB_Value[i][7]; //팬유형
                CoolerTop_dataGridView.Rows[nRow].Cells[9].Value = string.Format("{0:F1}", Program.UTIL.ToDoubleOrZero(DefaultDB_Value[i][8]));//냉방전력소비계수
                CoolerTop_dataGridView.Rows[nRow].Cells[10].Value = DefaultDB_Value[i][9]; //대기전력
                CoolerTop_dataGridView.Rows[nRow].Cells[11].Value = DefaultDB_Value[i][10]; //설치
            }
        }

        private void TableMake()
        {
            new StackedHeaderDecorator(CoolerTop_dataGridView, DataGridViewAutoSizeColumnsMode.Fill, datagridviewDesign);
            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();

            CoolerTop_dataGridView.Columns.Clear();
            checkBoxColumn.HeaderText = "선택";
            checkBoxColumn.Name = "check";
            CoolerTop_dataGridView.Columns.Add(checkBoxColumn);

            //CoolerTop_dataGridView.Columns.Add("A1", "설치대수");
            CoolerTop_dataGridView.Columns.Add("A1", "번호");
            CoolerTop_dataGridView.Columns.Add("A2", "명칭");
            CoolerTop_dataGridView.Columns.Add("A3", "형식");
            CoolerTop_dataGridView.Columns.Add("A4", "냉각성능.냉각출력.[kW]");
            CoolerTop_dataGridView.Columns.Add("A5", "냉각성능.냉각수량.[CMH]");
            CoolerTop_dataGridView.Columns.Add("A6", "냉각수온도.입구온도.[℃]");
            CoolerTop_dataGridView.Columns.Add("A7", "냉각수온도.출구온도.[℃]");
            CoolerTop_dataGridView.Columns.Add("A8", "팬.유형");
            CoolerTop_dataGridView.Columns.Add("A9", "팬.소비계수.[W/W]");
            CoolerTop_dataGridView.Columns.Add("A10", "대기전력.[W]");
            CoolerTop_dataGridView.Columns.Add("A11", "설치");
   
            CoolerTop_dataGridView.Columns[0].Width = 40; //선택
            CoolerTop_dataGridView.Columns[1].Width = 70; //번호
            CoolerTop_dataGridView.Columns[2].Width = 70; //명칭
            CoolerTop_dataGridView.Columns[3].Width = 70; //형식
            CoolerTop_dataGridView.Columns[4].Width = 80; //냉각능력
            CoolerTop_dataGridView.Columns[5].Width = 80; //냉각수량
            CoolerTop_dataGridView.Columns[6].Width = 90; //냉각수온도
            CoolerTop_dataGridView.Columns[7].Width = 90; //냉각수온도

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
        private bool SelectCheckBox()
        {
            SelectRow.Clear();

            foreach (DataGridViewRow row in CoolerTop_dataGridView.Rows)
            {
                if (Convert.ToBoolean(row.Cells["check"].Value))
                {
                    row.DefaultCellStyle.SelectionBackColor = SystemColors.GradientInactiveCaption;
                    SelectRow.Add(row.Index);
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
                    //this.SelectCTN += Program.UTIL.dataGridView_doubleComa(CoolerTop_dataGridView, Convert.ToInt32(SelectRow[k]), 1, 0).ToString();
                    this.SelectCT += CoolerTop_dataGridView.Rows[Convert.ToInt32(SelectRow[k])].Cells[1].Value.ToString();

                }
                else
                {
                    //this.SelectCTN += Program.UTIL.dataGridView_doubleComa(CoolerTop_dataGridView, Convert.ToInt32(SelectRow[k]), 1, 0).ToString() + "+";
                    this.SelectCT += CoolerTop_dataGridView.Rows[Convert.ToInt32(SelectRow[k])].Cells[1].Value.ToString() + "+";
                }
            }
           
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void reset()
        {
            SelectRow.Clear();
            SelectCT_split.Clear();
            //SelectCTN_split.Clear();
            this.SelectCT = null;

            for (int n = 0; n < CoolerTop_dataGridView.Rows.Count; n++)
            {
                CoolerTop_dataGridView.Rows[n].Cells[0].Value = false;
            }
        }

        private void Load_SaveValue(string _SelectCT_nonsplit)
        {
            reset();
            string[] token = _SelectCT_nonsplit.Split('+');
            //string[] value = _SelectCTN_nonsplit.Split("+");


            foreach (var item in token)
            {
                SelectCT_split.Add(item.ToString());
            }
            //foreach (var val in value)
            //{
            //    SelectCTN_split.Add(val.ToString());
            //}

            for (int k = 0; k < SelectCT_split.Count; k++)
            {
                for (int n = 0; n < CoolerTop_dataGridView.Rows.Count; n++)
                {
                    if (CoolerTop_dataGridView.Rows[n].Cells[1].Value.ToString() == SelectCT_split[k].ToString())
                    {
                        CoolerTop_dataGridView.Rows[n].Cells[0].Value = true;
                        //CoolerTop_dataGridView.Rows[n].Cells[1].Value = SelectCTN_split[k].ToString();
                    }
                }
            }
        }
        

    }
}
