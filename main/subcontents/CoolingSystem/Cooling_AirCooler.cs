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
    public partial class Cooling_AirCooler : Form
    {
        string DefaultUse;
        public List<Select> SelectItem = new List<Select>();
        

        public Cooling_AirCooler()
        {
            
            InitializeComponent();  
            string[][] Image = Program.DB.getValue(DB.type.BaseDB_HCneed, "메뉴아이콘", "하위메뉴아이콘", "하위메뉴명 = '장비일람표'");
            Icon_pictureBox.Load(Program.gPath + Image[0][0]);
            Icon_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            load_table_DB();
        }

        
        private void load_table_DB() //번호자동생성, 
        {
            //User_AirCon 삭제함
            
            new StackedHeaderDecorator(AirCooler_dataGridView, DataGridViewAutoSizeColumnsMode.Fill, datagridviewDesign);
            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
            AirCooler_dataGridView.Columns.Clear();
            checkBoxColumn.HeaderText = "선택";
            checkBoxColumn.Name = "check";
            AirCooler_dataGridView.Columns.Add(checkBoxColumn);
            UserTablemake();

            // 에어컨에서 값이 오도록

            string[][] AirHP_Value = Program.DB.getValue(DB.type.ProjDB, "User_AirHP", "번호,명칭,냉방정격용량,냉방정격소비전력,냉방정격COP,대기전력,연료,설치", "");

            if (AirHP_Value.Length > 0)
            {
                for (int i = 0; i < AirHP_Value.Length; i++)
                {
                    if (Convert.ToDouble(AirHP_Value[i][2]) > 12)
                    {
                        AirCooler_dataGridView.Rows.Add();
                        int nRow = AirCooler_dataGridView.Rows.Count - 1;

                        AirCooler_dataGridView.Rows[nRow].Cells[1].Value = AirHP_Value[i][0];
                        AirCooler_dataGridView.Rows[nRow].Cells[2].Value = AirHP_Value[i][1];
                        AirCooler_dataGridView.Rows[nRow].Cells[3].Value = string.Format("{0:F1}", Convert.ToDouble(AirHP_Value[i][2]));
                        AirCooler_dataGridView.Rows[nRow].Cells[4].Value = string.Format("{0:F1}", Convert.ToDouble(AirHP_Value[i][3]));
                        AirCooler_dataGridView.Rows[nRow].Cells[5].Value = string.Format("{0:F1}", Convert.ToDouble(AirHP_Value[i][4])); //EER


                        DataGridViewComboBoxCell PressorCombo = new DataGridViewComboBoxCell();


                        PressorCombo.Items.Add("왕복동");
                        PressorCombo.Items.Add("스크롤");
                        PressorCombo.Items.Add("스크류");
                        PressorCombo.Items.Add("터보");


                        AirCooler_dataGridView.Rows[nRow].Cells[6] = PressorCombo;

                        AirCooler_dataGridView.Rows[nRow].Cells[7].Value = AirHP_Value[i][6]; //연료
                        AirCooler_dataGridView.Rows[nRow].Cells[8].Value = AirHP_Value[i][5]; //대기전력

                        AirCooler_dataGridView.Rows[nRow].Cells[9].Value = AirHP_Value[i][7]; //설치
                        AirCooler_dataGridView.Rows[nRow].Cells[10].Value = "직팽식"; //부하공급
                        AirCooler_dataGridView.Rows[nRow].Cells[11].Value = null; //증발기
                        AirCooler_dataGridView.Rows[nRow].Cells[12].Value = null; //냉수입구온도
                        AirCooler_dataGridView.Rows[nRow].Cells[13].Value = null; //냉수출구온도

                    }

                }

            }

            string[][] DefaultDB_Value = Program.DB.getValue(DB.type.ProjDB, "User_AirCooler", " 번호,명칭,냉방출력,냉방소비전력,EER,압축기,연료,대기전력,설치,부하측공급형식,증발기,냉수입구온도,냉수출구온도", ""); //수정 필요

            if(DefaultDB_Value.Length > 0)
            {
                for (int i = 0; i < DefaultDB_Value.Length; i++)
                {

                    AirCooler_dataGridView.Rows.Add();
                    int nRow = AirCooler_dataGridView.Rows.Count - 1;
                    AirCooler_dataGridView.Rows[nRow].Cells[1].Value = DefaultDB_Value[i][0];
                    AirCooler_dataGridView.Rows[nRow].Cells[2].Value = DefaultDB_Value[i][1];
                    AirCooler_dataGridView.Rows[nRow].Cells[3].Value = string.Format("{0:F1}", Convert.ToDouble(DefaultDB_Value[i][2]));
                    AirCooler_dataGridView.Rows[nRow].Cells[4].Value = string.Format("{0:F1}", Convert.ToDouble(DefaultDB_Value[i][3]));
                    AirCooler_dataGridView.Rows[nRow].Cells[5].Value = string.Format("{0:F1}", Convert.ToDouble(DefaultDB_Value[i][4])); //EER
                    AirCooler_dataGridView.Rows[nRow].Cells[6].Value = DefaultDB_Value[i][5]; //압축기
                    AirCooler_dataGridView.Rows[nRow].Cells[7].Value = DefaultDB_Value[i][6]; //연료
                    AirCooler_dataGridView.Rows[nRow].Cells[8].Value = DefaultDB_Value[i][7]; //대기전력
                    AirCooler_dataGridView.Rows[nRow].Cells[9].Value = DefaultDB_Value[i][8]; //설치
                    AirCooler_dataGridView.Rows[nRow].Cells[10].Value = DefaultDB_Value[i][9]; //부하공급
                    AirCooler_dataGridView.Rows[nRow].Cells[11].Value = DefaultDB_Value[i][10]; //증발기
                    AirCooler_dataGridView.Rows[nRow].Cells[12].Value = DefaultDB_Value[i][11]; //냉수입구온도
                    AirCooler_dataGridView.Rows[nRow].Cells[13].Value = DefaultDB_Value[i][12]; //냉수출구온도
                }

            }
            
        }

        private void UserTablemake()
        {
            new StackedHeaderDecorator(AirCooler_dataGridView, DataGridViewAutoSizeColumnsMode.Fill, datagridviewDesign);
            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
            AirCooler_dataGridView.Columns.Clear();
            checkBoxColumn.HeaderText = "선택";
            checkBoxColumn.Name = "check";
            AirCooler_dataGridView.Columns.Add(checkBoxColumn);

            AirCooler_dataGridView.Columns.Add("A1", "번호");
            AirCooler_dataGridView.Columns.Add("A2", "명칭");
            AirCooler_dataGridView.Columns.Add("A3", "냉방성능.출력[kW]");
            AirCooler_dataGridView.Columns.Add("A4", "냉방성능.소비전력[kW]");
            AirCooler_dataGridView.Columns.Add("A5", "냉방성능.EER[W/w]");
            AirCooler_dataGridView.Columns.Add("A6", "압축기");
            AirCooler_dataGridView.Columns.Add("A7", "연료");
            AirCooler_dataGridView.Columns.Add("A8", "대기전력[W]");
            AirCooler_dataGridView.Columns.Add("A9", "설치");
            AirCooler_dataGridView.Columns.Add("A10", "부하공급");
            AirCooler_dataGridView.Columns.Add("A11", "증발기");
            AirCooler_dataGridView.Columns.Add("A12", "냉수온도.입구[℃]");
            AirCooler_dataGridView.Columns.Add("A13", "냉수온도.출구[℃]");
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
                }
            }
        }
        private void Save_button_Click(object sender, EventArgs e)
        {
            SelectItem.Clear();
            foreach (DataGridViewRow row in AirCooler_dataGridView.Rows)
            {
                if (Convert.ToBoolean(row.Cells["check"].Value))
                {
                    if (row.Cells[6].Value == null)
                    {
                        MessageBox.Show("먼저 압축기를 선택해 주세요!");
                    }
                    else
                    {
                        Select item = new Select();
                        item.SelectAirCooler = row.Cells[1].Value.ToString();
                        item.SelectPressor = row.Cells[6].Value.ToString();
                        SelectItem.Add(item);
                    }
                    
                }
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void AirCooler_dataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            SelectCheckBox();
        }

       
    }

     public class Select
    {
        public string SelectAirCooler = null;
        public string SelectPressor = null;
    }
}
