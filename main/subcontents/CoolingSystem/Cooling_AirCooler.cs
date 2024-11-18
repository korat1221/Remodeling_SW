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
using static System.Net.WebRequestMethods;

namespace main.subcontents.CoolingSystem
{
    public partial class Cooling_AirCooler : Form
    {

        List<int> SelectRow = new List<int>(); 
        List<string> SelectCG_split = new List<string>();
        List<string> SelectCGN_split = new List<string>();
        List<string> SelectCGComp_split = new List<string>();
        
        string SystemNum;
        public string SelectCG, SelectCGComp, SelectCGN;

        public Cooling_AirCooler(string _Num, string _SelectCG_nonsplit, string _SelectCGComp_nonsplit, string _SelectCGN_nonsplit)
        {
            InitializeComponent(); this.Font = new Font(UTIL.Families[0], 9.75F, FontStyle.Regular);  
            string[][] Image = Program.DB.getValue(DB.type.BaseDB_HCneed, "메뉴아이콘", "하위메뉴아이콘", "하위메뉴명 = '장비일람표'");
            Icon_pictureBox.Load(Program.gPath + Image[0][0]);
            Icon_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            load_table_DB();
            SystemNum = _Num;

            if(_SelectCG_nonsplit != null)
            {
                Load_SaveValue(_SelectCG_nonsplit, _SelectCGComp_nonsplit, _SelectCGN_nonsplit);
            }
        }

        private void load_table_DB() //번호자동생성, 
        {
            TableMake();
            
            // 히트펌프에서 설비 찾아오기, 주석처리함_20240724
            /*
            string[][] AirHP = Program.DB.getValue(DB.type.ProjDB, "User_AirHP", "번호,명칭,냉방정격용량,냉방정격소비전력,냉방정격COP,대기전력,연료,설치", "난방냉방 = '냉난방' OR 난방냉방 = '냉방'");

            if (AirHP.Length > 0)
            {
                for (int i = 0; i < AirHP.Length; i++)
                {
                    if (Convert.ToDouble(AirHP[i][2]) > 12)
                    {
                        AirCooler_dataGridView.Rows.Add();
                        int nRow = AirCooler_dataGridView.Rows.Count - 1;
                        AirCooler_dataGridView.Rows[nRow].Cells[2].Value = AirHP[i][0];
                        AirCooler_dataGridView.Rows[nRow].Cells[3].Value = AirHP[i][1];
                        AirCooler_dataGridView.Rows[nRow].Cells[4].Value = string.Format("{0:F1}", Convert.ToDouble(AirHP   [i][2]));
                        AirCooler_dataGridView.Rows[nRow].Cells[5].Value = string.Format("{0:F1}", Convert.ToDouble(AirHP[i][3]));
                        AirCooler_dataGridView.Rows[nRow].Cells[6].Value = string.Format("{0:F1}", Convert.ToDouble(AirHP   [i][4])); //EER


                        DataGridViewComboBoxCell PressorCombo = new DataGridViewComboBoxCell();
                        PressorCombo.Items.Add("왕복동");
                        PressorCombo.Items.Add("스크롤");
                        PressorCombo.Items.Add("스크류");
                        PressorCombo.Items.Add("터보");
                        AirCooler_dataGridView.Rows[nRow].Cells[7] = PressorCombo;

                        AirCooler_dataGridView.Rows[nRow].Cells[8].Value = AirHP    [i][6]; //연료
                        AirCooler_dataGridView.Rows[nRow].Cells[9].Value = AirHP[i][5]; //대기전력
                        AirCooler_dataGridView.Rows[nRow].Cells[10].Value = AirHP[i][7]; //설치
                        AirCooler_dataGridView.Rows[nRow].Cells[11].Value = "직팽식"; //부하공급
                        AirCooler_dataGridView.Rows[nRow].Cells[12].Value = null; //증발기
                        AirCooler_dataGridView.Rows[nRow].Cells[13].Value = null; //냉수입구온도
                        AirCooler_dataGridView.Rows[nRow].Cells[14].Value = null; //냉수출구온도

                    }

                }

            }
            */
            //공냉식냉동기에서 찾아오기
            string[][] AirCooler = Program.DB.getValue(DB.type.ProjDB, "User_AirCooler", " 번호,명칭,냉방출력,냉방소비전력,EER,압축기,연료,대기전력,설치,부하측공급형식,증발기,냉수입구온도,냉수출구온도", ""); //수정 필요

            if(AirCooler.Length > 0)
            {
                for (int i = 0; i < AirCooler.Length; i++)
                {

                    AirCooler_dataGridView.Rows.Add();
                    int nRow = AirCooler_dataGridView.Rows.Count - 1;
                    AirCooler_dataGridView.Rows[nRow].Cells[2].Value = AirCooler[i][0];
                    AirCooler_dataGridView.Rows[nRow].Cells[3].Value = AirCooler[i][1];
                    AirCooler_dataGridView.Rows[nRow].Cells[4].Value = string.Format("{0:F1}", Convert.ToDouble(AirCooler[i][2]));
                    AirCooler_dataGridView.Rows[nRow].Cells[5].Value = string.Format("{0:F1}", Convert.ToDouble(AirCooler[i][3]));
                    AirCooler_dataGridView.Rows[nRow].Cells[6].Value = string.Format("{0:F1}", Convert.ToDouble(AirCooler[i][4])); //EER
                    AirCooler_dataGridView.Rows[nRow].Cells[7].Value = AirCooler[i][5]; //압축기
                    AirCooler_dataGridView.Rows[nRow].Cells[8].Value = AirCooler[i][6]; //연료
                    AirCooler_dataGridView.Rows[nRow].Cells[9].Value = AirCooler[i][7]; //대기전력
                    AirCooler_dataGridView.Rows[nRow].Cells[10].Value = AirCooler[i][8]; //설치
                    AirCooler_dataGridView.Rows[nRow].Cells[11].Value = AirCooler[i][9]; //부하공급
                    AirCooler_dataGridView.Rows[nRow].Cells[12].Value = AirCooler[i][10]; //증발기
                    AirCooler_dataGridView.Rows[nRow].Cells[13].Value = AirCooler[i][11]; //냉수입구온도
                    AirCooler_dataGridView.Rows[nRow].Cells[14].Value = AirCooler[i][12]; //냉수출구온도
                }

            }
            
        }

        private void TableMake()
        {
            new StackedHeaderDecorator(AirCooler_dataGridView, DataGridViewAutoSizeColumnsMode.Fill, datagridviewDesign);
            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
            AirCooler_dataGridView.Columns.Clear();
            checkBoxColumn.HeaderText = "선택";
            checkBoxColumn.Name = "check";
            AirCooler_dataGridView.Columns.Add(checkBoxColumn);

            AirCooler_dataGridView.Columns.Add("A1", "설치대수");
            AirCooler_dataGridView.Columns.Add("A2", "번호");
            AirCooler_dataGridView.Columns.Add("A3", "명칭");
            AirCooler_dataGridView.Columns.Add("A4", "냉방성능.출력.[kW]");
            AirCooler_dataGridView.Columns.Add("A5", "냉방성능.소비전력.[kW]");
            AirCooler_dataGridView.Columns.Add("A6", "냉방성능.EER.[W/W]");
            AirCooler_dataGridView.Columns.Add("A7", "압축기");
            AirCooler_dataGridView.Columns.Add("A8", "연료");
            AirCooler_dataGridView.Columns.Add("A9", "대기전력.[W]");
            AirCooler_dataGridView.Columns.Add("A10", "설치");
            AirCooler_dataGridView.Columns.Add("A11", "부하공급");
            AirCooler_dataGridView.Columns.Add("A12", "증발기");
            AirCooler_dataGridView.Columns.Add("A13", "냉수온도.입구.[℃]");
            AirCooler_dataGridView.Columns.Add("A14", "냉수온도.출구.[℃]");
        }

        private Boolean datagridviewDesign(DataGridViewCell cell, int column, int row)
        {
            if (column == 1) // 추가
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

            foreach (DataGridViewRow row in AirCooler_dataGridView.Rows)
            {
                if (Convert.ToBoolean(row.Cells["check"].Value))
                {
                    row.DefaultCellStyle.SelectionBackColor = SystemColors.GradientInactiveCaption;
                    SelectRow.Add(row.Index);
                    if (row.Cells[1].Value == null)
                    {
                        MessageBox.Show("설치대수를 입력해주세요.", "Check", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly); //추가
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
                    if (AirCooler_dataGridView.Rows[SelectRow[k]].Cells[7].Value == null)
                    {
                        MessageBox.Show("먼저 압축기를 선택해 주세요.", "Check", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                        return;
                    }
                    else 
                    {
                        this.SelectCGN += Program.UTIL.dataGridView_doubleComa(AirCooler_dataGridView, SelectRow[k], 1, 0).ToString();
                        this.SelectCG += AirCooler_dataGridView.Rows[SelectRow[k]].Cells[2].Value.ToString();
                        this.SelectCGComp += AirCooler_dataGridView.Rows[SelectRow[k]].Cells[7].Value.ToString(); //수정함
                        
                    }
                }
                else
                {
                    if (AirCooler_dataGridView.Rows[SelectRow[k]].Cells[6].Value == null)
                    {
                        MessageBox.Show("먼저 압축기를 선택해 주세요!", "Check", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                        return;
                    }
                    else 
                    {
                        this.SelectCGN += Program.UTIL.dataGridView_doubleComa(AirCooler_dataGridView, SelectRow[k], 1, 0).ToString() + "+";
                        this.SelectCG += AirCooler_dataGridView.Rows[SelectRow[k]].Cells[2].Value.ToString() + "+";
                        this.SelectCGComp += AirCooler_dataGridView.Rows[SelectRow[k]].Cells[7].Value.ToString() + "+";
                    }
                    
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
            SelectCGComp_split.Clear();
            this.SelectCG = null;

            for (int n = 0; n < AirCooler_dataGridView.Rows.Count; n++)
            {
                AirCooler_dataGridView.Rows[n].Cells[0].Value = false;
                AirCooler_dataGridView.Rows[n].Cells[1].Value = null; //보완
            }
        }
        private void Load_SaveValue(string _SelectCG_nonsplit, string _SelectCGComp_nonsplit, string _SelectCGN_nonsplit)
        {
            reset();
            string[] token = _SelectCG_nonsplit.Split('+');
            string[] 압축기 = _SelectCGComp_nonsplit.Split('+');
            string[] 설치대수 = _SelectCGN_nonsplit.Split('+'); //추가
                        
            for(int i = 0; i<token.Length ;i++)
            {
                SelectCG_split.Add(token[i]);
                SelectCGComp_split.Add(압축기[i]);
                SelectCGN_split.Add(설치대수[i]);
            }
            for (int k = 0; k < SelectCG_split.Count; k++)
            {
                for (int n = 0; n < AirCooler_dataGridView.Rows.Count; n++)
                {
                    if (AirCooler_dataGridView.Rows[n].Cells[2].Value.ToString() == SelectCG_split[k].ToString())
                    {
                        AirCooler_dataGridView.Rows[n].Cells[0].Value = true;
                        AirCooler_dataGridView.Rows[n].Cells[1].Value = SelectCGN_split[k];
                        AirCooler_dataGridView.Rows[n].Cells[7].Value = SelectCGComp_split[k].ToString();
                    }
                }
            }
        }
    }
}
