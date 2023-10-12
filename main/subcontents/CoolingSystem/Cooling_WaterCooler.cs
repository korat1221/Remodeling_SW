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
        string DefaultUse;
        public List<string> SelectWaterCooler = new List<string>();
        public Cooling_WaterCooler(string DefaultUse)
        {
            InitializeComponent();
            this.DefaultUse = DefaultUse;
            load_table_DB();
            string[][] Image = Program.DB.getValue(DB.type.BaseDB_HCneed, "메뉴아이콘", "하위메뉴아이콘", "하위메뉴명 = '장비일람표'");
            Icon_pictureBox.Load(Program.gPath + Image[0][0]);
            Icon_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
        }


        private void load_table_DB() //번호자동생성, 
        {

            if (DefaultUse == "기본DB 적용")
            {
                DefaultTablemake();
            }
            else
            {
                UserTablemake();

                string[][] DefaultDB_Value = Program.DB.getValue(DB.type.ProjDB, "User_WaterCooler", " 번호,명칭,냉방출력,냉방소비전력,EER,압축기,연료,대기전력,설치,증발기,냉수입구온도,냉수출구온도", ""); //수정 필요

                for (int i = 0; i < DefaultDB_Value.Length; i++)
                {
                    WaterCooler_dataGridView.Rows.Add();
                    int nRow = WaterCooler_dataGridView.Rows.Count - 1;
                    WaterCooler_dataGridView.Rows[nRow].Cells[1].Value = DefaultDB_Value[i][0];
                    WaterCooler_dataGridView.Rows[nRow].Cells[2].Value = DefaultDB_Value[i][1];
                    WaterCooler_dataGridView.Rows[nRow].Cells[3].Value = string.Format("{0:F1}", Convert.ToDouble(DefaultDB_Value[i][2]));
                    WaterCooler_dataGridView.Rows[nRow].Cells[4].Value = string.Format("{0:F1}", Convert.ToDouble(DefaultDB_Value[i][3]));
                    WaterCooler_dataGridView.Rows[nRow].Cells[5].Value = string.Format("{0:F1}", Convert.ToDouble(DefaultDB_Value[i][4])); //EER
                    WaterCooler_dataGridView.Rows[nRow].Cells[6].Value = DefaultDB_Value[i][5]; //압축기
                    WaterCooler_dataGridView.Rows[nRow].Cells[7].Value = DefaultDB_Value[i][6]; //연료
                    WaterCooler_dataGridView.Rows[nRow].Cells[8].Value = DefaultDB_Value[i][7]; //대기전력
                    WaterCooler_dataGridView.Rows[nRow].Cells[9].Value = DefaultDB_Value[i][8]; //설치
                    WaterCooler_dataGridView.Rows[nRow].Cells[10].Value = DefaultDB_Value[i][9]; //증발기
                    WaterCooler_dataGridView.Rows[nRow].Cells[11].Value = DefaultDB_Value[i][10]; //냉수입구온도
                    WaterCooler_dataGridView.Rows[nRow].Cells[12].Value = DefaultDB_Value[i][11]; //냉수출구온도
                }
            }
        }
        private void DefaultTablemake()
        {
            new StackedHeaderDecorator(WaterCooler_dataGridView, DataGridViewAutoSizeColumnsMode.Fill, datagridviewDesign);
            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
            //WaterCooler_dataGridView.Columns.Clear();
            //checkBoxColumn.HeaderText = "선택";
            //checkBoxColumn.Name = "check";
            //WaterCooler_dataGridView.Columns.Add(checkBoxColumn);

            WaterCooler_dataGridView.Columns.Add("A1", "수냉식냉동기 DB 준비중입니다.");
        }

        private void UserTablemake()
        {
            new StackedHeaderDecorator(WaterCooler_dataGridView, DataGridViewAutoSizeColumnsMode.Fill, datagridviewDesign);
            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
            WaterCooler_dataGridView.Columns.Clear();
            checkBoxColumn.HeaderText = "선택";
            checkBoxColumn.Name = "check";
            WaterCooler_dataGridView.Columns.Add(checkBoxColumn);

            WaterCooler_dataGridView.Columns.Add("A1", "번호");
            WaterCooler_dataGridView.Columns.Add("A2", "명칭");
            WaterCooler_dataGridView.Columns.Add("A3", "냉방성능.출력[kW]");
            WaterCooler_dataGridView.Columns.Add("A4", "냉방성능.소비전력[kW]");
            WaterCooler_dataGridView.Columns.Add("A5", "냉방성능.EER[W/w]");

            DataGridViewComboBoxColumn PressorcomboBox = new DataGridViewComboBoxColumn();
            PressorcomboBox.HeaderText = "압축기";
            PressorcomboBox.Items.AddRange(new string[] { "왕복동", "스크롤", "스크류", "터보" });
            WaterCooler_dataGridView.Columns.Add(PressorcomboBox);

            WaterCooler_dataGridView.Columns.Add("A7", "연료");
            WaterCooler_dataGridView.Columns.Add("A8", "대기전력[W]");

            DataGridViewComboBoxColumn InstallcomboBox = new DataGridViewComboBoxColumn();
            InstallcomboBox.HeaderText = "설치";
            InstallcomboBox.Items.AddRange(new string[] { "기존", "신규", "철거후신규" });
            WaterCooler_dataGridView.Columns.Add(InstallcomboBox);

            DataGridViewComboBoxColumn EvapocomboBox = new DataGridViewComboBoxColumn();
            EvapocomboBox.HeaderText = "증발기";
            EvapocomboBox.Items.AddRange(new string[] { "판형", "다관식" });
            WaterCooler_dataGridView.Columns.Add(EvapocomboBox);

            WaterCooler_dataGridView.Columns.Add("A11", "냉수온도.입구[℃]");
            WaterCooler_dataGridView.Columns.Add("A12", "냉수온도.출구[℃]");
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
                }
            }
        }
        private void Save_button_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in WaterCooler_dataGridView.Rows)
            {
                if (Convert.ToBoolean(row.Cells["check"].Value))
                {
                    SelectWaterCooler.Add(row.Cells[1].Value.ToString()); //선택항목 번호 저장함
                }
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }


        private void WaterCooler_dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            SelectCheckBox();
        }
    }
}
