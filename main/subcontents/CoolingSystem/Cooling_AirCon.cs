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
        List<int> SelectRow = new List<int>();
        List<string> SelectCG_split = new List<string>();
        List<string> SelectCGN_split = new List<string>();

        string SystemNum;
        public string SelectCG, SelectCGN;

        public Cooling_AirCon(string _Num, string _SelectCG_nonsplit, string _SelectCGN_nonsplit)

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

        private void load_table_DB() 
        {
            TableMake();
            
            string[][] DefaultDB_Value = Program.DB.getValue(DB.type.ProjDB, "User_AirHP", "번호,명칭,냉방정격용량,냉방정격소비전력,냉방정격COP,대기전력,연료", "난방냉방 = '냉난방' OR 난방냉방 = '냉방'");
            for (int i = 0; i < DefaultDB_Value.Length; i++)
            {
               // if (Convert.ToDouble(DefaultDB_Value[i][2]) <= 12)
                //{
                    AirCon_dataGridView.Rows.Add();
                    int nRow = AirCon_dataGridView.Rows.Count - 1;
                    AirCon_dataGridView.Rows[nRow].Cells[2].Value = DefaultDB_Value[i][0];
                    AirCon_dataGridView.Rows[nRow].Cells[3].Value = DefaultDB_Value[i][1];
                    AirCon_dataGridView.Rows[nRow].Cells[4].Value = string.Format("{0:F1}", Convert.ToDouble(DefaultDB_Value[i][2]));
                    AirCon_dataGridView.Rows[nRow].Cells[5].Value = string.Format("{0:F1}", Convert.ToDouble(DefaultDB_Value[i][3]));
                    AirCon_dataGridView.Rows[nRow].Cells[6].Value = string.Format("{0:F1}", Convert.ToDouble(DefaultDB_Value[i][4]));
                    AirCon_dataGridView.Rows[nRow].Cells[7].Value = DefaultDB_Value[i][5];
                    AirCon_dataGridView.Rows[nRow].Cells[8].Value = DefaultDB_Value[i][6];
                //}
               
            }
        }

        private void TableMake()
        {
            new StackedHeaderDecorator(AirCon_dataGridView, DataGridViewAutoSizeColumnsMode.Fill, datagridviewDesign);
            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
            AirCon_dataGridView.Columns.Clear();
            checkBoxColumn.HeaderText = "선택";
            checkBoxColumn.Name = "check";
            AirCon_dataGridView.Columns.Add(checkBoxColumn);

            AirCon_dataGridView.Columns.Add("A1", "설치대수");
            AirCon_dataGridView.Columns.Add("A2", "번호");
            AirCon_dataGridView.Columns.Add("A3", "명칭");
            AirCon_dataGridView.Columns.Add("A4", "냉방성능.출력[kW]");
            AirCon_dataGridView.Columns.Add("A5", "냉방성능.소비전력[kW]");
            AirCon_dataGridView.Columns.Add("A6", "냉방성능.EER[W/w]");
            AirCon_dataGridView.Columns.Add("A7", "대기전력[W]");
            AirCon_dataGridView.Columns.Add("A8", "연료");

          
        }

        private Boolean datagridviewDesign(DataGridViewCell cell, int column, int row)
        {
            if (column == 1) // 추가
            {
                cell.Style.BackColor = Color.FromArgb(255, 248, 206);
                return true;
            }
            else  if (row % 2 == 1)
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
            foreach (DataGridViewRow row in AirCon_dataGridView.Rows)
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

            for(int k=0;k<SelectRow.Count ; k++)
            {
                if(k == SelectRow.Count - 1)
                {
                    this.SelectCGN += Program.UTIL.dataGridView_doubleComa(AirCon_dataGridView, Convert.ToInt32(SelectRow[k]), 1, true, 0).ToString();
                    this.SelectCG += AirCon_dataGridView.Rows[Convert.ToInt32(SelectRow[k])].Cells[2].Value.ToString();

                }
                else
                {
                    this.SelectCGN += Program.UTIL.dataGridView_doubleComa(AirCon_dataGridView, Convert.ToInt32(SelectRow[k]), 1, true, 0).ToString() + "+";
                    this.SelectCG += AirCon_dataGridView.Rows[Convert.ToInt32(SelectRow[k])].Cells[2].Value.ToString() + "+";
                }
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        public void reset()
        {
            SelectRow.Clear();
            SelectCG_split.Clear ();
            SelectCGN_split.Clear();

            this.SelectCG = null;
            this.SelectCGN = null;

            for (int n = 0; n < AirCon_dataGridView.Rows.Count; n++)
            {
                AirCon_dataGridView.Rows[n].Cells[0].Value = false;
                AirCon_dataGridView.Rows[n].Cells[1].Value = null; //수정
            }
        }

        private void Load_SaveValue(string _SelectCG_nonsplit, string _SelectCGN_nonsplit)
        {
            reset();
            string[] token = _SelectCG_nonsplit.Split('+');
            string[] value = _SelectCGN_nonsplit.Split("+");
            SelectCG_split.Clear();
            SelectCGN_split.Clear();
            foreach (var item in token)
            {
                SelectCG_split.Add(item.ToString());
            }
            foreach (var val in value)
            {
                SelectCGN_split.Add(val.ToString()); //수정함
            }

            for (int k = 0; k < SelectCG_split.Count; k++)
            {
                for (int n = 0; n < AirCon_dataGridView.Rows.Count; n++)
                {
                    if (AirCon_dataGridView.Rows[n].Cells[2].Value.ToString() == SelectCG_split[k]) //수정
                    {
                        AirCon_dataGridView.Rows[n].Cells[0].Value = true;
                        AirCon_dataGridView.Rows[n].Cells[1].Value = value[k];
                    }
                }
            }
        }
    }
}
