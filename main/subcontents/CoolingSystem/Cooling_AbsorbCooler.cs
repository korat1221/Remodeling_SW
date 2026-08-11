using main.info;
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
    public partial class Cooling_AbsorbCooler : Form
    {
        List<int> SelectRow = new List<int>();
        List<string> SelectCG_split = new List<string>();
        string SystemNum;
        public string SelectCG;


        public Cooling_AbsorbCooler(string _Num, string _SelectCG_nonsplit)
        {
            InitializeComponent(); this.Font = new Font(UTIL.Families[0], 9.75F, FontStyle.Regular);
            string[][] Image = Program.DB.getValue(DB.type.BaseDB_HCneed, "메뉴아이콘", "하위메뉴아이콘", "하위메뉴명 = '장비일람표'");
            Icon_pictureBox.Load(Program.gPath + Image[0][0]);
            Icon_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            load_table_DB();

            SystemNum = _Num;

            if (_SelectCG_nonsplit != null)
            {
                Load_SaveValue(_SelectCG_nonsplit);
            }

        }
        private void load_table_DB() //번호자동생성, 
        {
            TableMake();

            string[][] DefaultDB_Value = Program.DB.getValue(DB.type.ProjDB, "User_ABS", " 번호,명칭,냉방용량,냉방성능,대기전력,냉수입구온도,냉수출구온도,연료,설치", "");
            for (int i = 0; i < DefaultDB_Value.Length; i++)
            {
                int nRow = AbsorbCooler_dataGridView.Rows.Add();

                AbsorbCooler_dataGridView.Rows[nRow].Cells[1].Value = DefaultDB_Value[i][0];//번호
                AbsorbCooler_dataGridView.Rows[nRow].Cells[2].Value = DefaultDB_Value[i][1]; //명칭
                AbsorbCooler_dataGridView.Rows[nRow].Cells[3].Value = string.Format("{0:F2}", Program.UTIL.ToDoubleOrZero(DefaultDB_Value[i][2]));//냉방출력
                AbsorbCooler_dataGridView.Rows[nRow].Cells[4].Value = string.Format("{0:F2}", Program.UTIL.ToDoubleOrZero(DefaultDB_Value[i][3])); //COP
                AbsorbCooler_dataGridView.Rows[nRow].Cells[5].Value = DefaultDB_Value[i][4]; //대기전력1
                AbsorbCooler_dataGridView.Rows[nRow].Cells[6].Value = DefaultDB_Value[i][5]; //냉수입구온도
                AbsorbCooler_dataGridView.Rows[nRow].Cells[7].Value = DefaultDB_Value[i][6]; //냉수출구온도
                AbsorbCooler_dataGridView.Rows[nRow].Cells[8].Value = DefaultDB_Value[i][7]; //연료
                AbsorbCooler_dataGridView.Rows[nRow].Cells[9].Value = DefaultDB_Value[i][8]; //설치       
            }
        }
        private void TableMake()
        {
            new StackedHeaderDecorator(AbsorbCooler_dataGridView, DataGridViewAutoSizeColumnsMode.Fill, datagridviewDesign);
            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
            AbsorbCooler_dataGridView.Columns.Clear();
            checkBoxColumn.HeaderText = "선택";
            checkBoxColumn.Name = "check";
            AbsorbCooler_dataGridView.Columns.Add(checkBoxColumn);

            AbsorbCooler_dataGridView.Columns.Add("A1", "번호");
            AbsorbCooler_dataGridView.Columns.Add("A2", "명칭");
            AbsorbCooler_dataGridView.Columns.Add("A3", "냉방성능.출력.[kW]");
            AbsorbCooler_dataGridView.Columns.Add("A4", "냉방성능.COP.[W/W]");
            AbsorbCooler_dataGridView.Columns.Add("A5", "대기전력.[W]");
            AbsorbCooler_dataGridView.Columns.Add("A6", "냉수온도.입구.[℃]");
            AbsorbCooler_dataGridView.Columns.Add("A7", "냉수온도.출구.[℃]");
            AbsorbCooler_dataGridView.Columns.Add("A8", "연료");
            AbsorbCooler_dataGridView.Columns.Add("A9", "설치");
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
            foreach (DataGridViewRow row in AbsorbCooler_dataGridView.Rows)
            {
                if (Convert.ToBoolean(row.Cells["check"].Value))
                {
                    row.DefaultCellStyle.SelectionBackColor = SystemColors.GradientInactiveCaption;
                    SelectRow.Add(row.Index);
                }
            }
            return true;
        }
        private void Save_button_Click_1(object sender, EventArgs e)
        {
            if (SelectCheckBox() == false)
            {
                return;
            }

            for (int k = 0; k < SelectRow.Count; k++)
            {
                if (k == SelectRow.Count - 1)
                {
                    this.SelectCG += AbsorbCooler_dataGridView.Rows[Convert.ToInt32(SelectRow[k])].Cells[1].Value.ToString();

                }
                else
                {
                    this.SelectCG += AbsorbCooler_dataGridView.Rows[Convert.ToInt32(SelectRow[k])].Cells[1].Value.ToString() + "+";
                }
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        public void reset()
        {
            SelectRow.Clear();
            SelectCG_split.Clear();
            this.SelectCG = null;
            
            for (int n = 0; n < AbsorbCooler_dataGridView.Rows.Count; n++)
            {
                AbsorbCooler_dataGridView.Rows[n].Cells[0].Value = false;
            }
        }

        private void Load_SaveValue(string _SelectCG_nonsplit)
        {
            reset();
            string[] token = _SelectCG_nonsplit.Split('+');
            
            foreach (var item in token)
            {
                SelectCG_split.Add(item.ToString());
            }
          
            for (int k = 0; k < SelectCG_split.Count; k++)
            {
                for (int n = 0; n < AbsorbCooler_dataGridView.Rows.Count; n++)
                {
                    if (AbsorbCooler_dataGridView.Rows[n].Cells[2].Value.ToString() == SelectCG_split[k].ToString())
                    {
                        AbsorbCooler_dataGridView.Rows[n].Cells[0].Value = true;
                    }
                }
            }
        }
    }
}
