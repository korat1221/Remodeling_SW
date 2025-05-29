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


namespace main.subcontents.CoolingSystem
{
    public partial class Cooling_Pump : Form
    {
        
        public string SelectP, SelectPN;
        int SelectRow;

        public Cooling_Pump(string SelectP, string SelectPT, string SelectPN) //번호, 타입, 대수 
        {
            InitializeComponent(); this.Font = new Font(UTIL.Families[0], 9.75F, FontStyle.Regular);
            
            string[][] Image = Program.DB.getValue(DB.type.BaseDB_HCneed, "메뉴아이콘", "하위메뉴아이콘", "하위메뉴명 = '장비일람표'");
            if (Image.Length > 0)
            {
                Icon_pictureBox.Load(Program.gPath + Image[0][0]);
                Icon_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            }
            load_table_DB(SelectPT);

            if (SelectP != null)
            {
                Load_SaveValue(SelectP, SelectPN);
            }
        }

        private void load_table_DB(string _SelectPT)
        {
            TableMake();

            if( _SelectPT == "냉수1차" || _SelectPT == "냉수2차")
            {
                string[][] User_Value = Program.DB.getValue(DB.type.ProjDB, "User_Pump", "번호,명칭,종류,A효율,B효율,유량,동력,양정", "종류 ='냉수순환펌프' OR 종류 = '냉온수순환펌프'"); //냉각수순환펌프,지열순환펌프
                if (User_Value.Length > 0)
                {
                    for (int n = 0; n < User_Value.Length; n++)
                    {
                        string A효율 = "", B효율 = "", 유량 = "", 동력 = "", 양정 = "";
                        if (User_Value[n][3] != null && User_Value[n][3] != "")
                        {
                            A효율 = string.Format("{0:F1}", Convert.ToDouble(User_Value[n][3]));
                        }
                        if (User_Value[n][4] != null && User_Value[n][4] != "")
                        {
                            B효율 = string.Format("{0:F1}", Convert.ToDouble(User_Value[n][4]));
                        }
                        if (User_Value[n][5] != null && User_Value[n][5] != "")
                        {
                            유량 = string.Format("{0:F0}", Convert.ToDouble(User_Value[n][5]));
                        }
                        if (User_Value[n][6] != null && User_Value[n][6] != "")
                        {
                            동력 = string.Format("{0:F0}", Convert.ToDouble(User_Value[n][6]));
                        }
                        if (User_Value[n][7] != null && User_Value[n][7] != "")
                        {
                            양정 = string.Format("{0:F0}", Convert.ToDouble(User_Value[n][7]));
                        }

                        int nRow = Pump_dataGridView.Rows.Add();

                        Pump_dataGridView.Rows[nRow].Cells[2].Value = User_Value[n][0];
                        Pump_dataGridView.Rows[nRow].Cells[3].Value = User_Value[n][1];
                        Pump_dataGridView.Rows[nRow].Cells[4].Value = User_Value[n][2];
                        Pump_dataGridView.Rows[nRow].Cells[5].Value = A효율;
                        Pump_dataGridView.Rows[nRow].Cells[6].Value = B효율;
                        Pump_dataGridView.Rows[nRow].Cells[7].Value = 유량;
                        Pump_dataGridView.Rows[nRow].Cells[8].Value = 동력;
                        Pump_dataGridView.Rows[nRow].Cells[9].Value = 양정;
                    }
                }
                else MessageBox.Show("장비일람표에서 펌프를 작성해 주세요.", "Check", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if(_SelectPT == "냉각수1차"|| _SelectPT == "냉각수2차")
            {
                string[][] User_Value = Program.DB.getValue(DB.type.ProjDB, "User_Pump", "번호,명칭,종류,A효율,B효율,유량,동력,양정", "종류 ='냉각수순환펌프'"); //냉각수순환펌프         if (User_Value.Length > 0)
                if (User_Value.Length > 0)
                {
                 
                    for (int n = 0; n < User_Value.Length; n++)
                    {
                        int nRow = Pump_dataGridView.Rows.Add();
                        for (int a = 0; a < User_Value[0].Length; a++)
                        {
                            Pump_dataGridView.Rows[nRow].Cells[a + 2].Value = User_Value[n][a];
                        }
                        Program.UTIL.dataGridView_doubleComa(Pump_dataGridView, nRow, 5, 1);
                        Program.UTIL.dataGridView_doubleComa(Pump_dataGridView, nRow, 6, 1);
                        Program.UTIL.dataGridView_doubleComa(Pump_dataGridView, nRow, 7, 0);
                        Program.UTIL.dataGridView_doubleComa(Pump_dataGridView, nRow, 8, 0);
                        Program.UTIL.dataGridView_doubleComa(Pump_dataGridView, nRow, 9, 0);
                    }
                }
                else MessageBox.Show("장비일람표에서 펌프를 작성해 주세요.", "Check", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (_SelectPT == "지열1차" || _SelectPT == "지열2차")
            {
                string[][] User_Value = Program.DB.getValue(DB.type.ProjDB, "User_Pump", "번호,명칭,종류,A효율,B효율,유량,동력,양정", "종류 = '지열순환펌프'"); //지열순환펌프
                if (User_Value.Length > 0)
                {
                    for (int n = 0; n < User_Value.Length; n++)
                    {
                        int nRow = Pump_dataGridView.Rows.Add();
                        for (int a = 0; a < User_Value[0].Length; a++)
                        {
                            Pump_dataGridView.Rows[nRow].Cells[a + 2].Value = User_Value[n][a];
                        }
                        Program.UTIL.dataGridView_doubleComa(Pump_dataGridView, nRow, 5, 1);
                        Program.UTIL.dataGridView_doubleComa(Pump_dataGridView, nRow, 6, 1);
                        Program.UTIL.dataGridView_doubleComa(Pump_dataGridView, nRow, 7, 0);
                        Program.UTIL.dataGridView_doubleComa(Pump_dataGridView, nRow, 8, 0);
                        Program.UTIL.dataGridView_doubleComa(Pump_dataGridView, nRow, 9, 0);
                    }
                }
                else MessageBox.Show("장비일람표에서 펌프를 작성해 주세요.", "Check", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (_SelectPT == "지하수1차" || _SelectPT == "지하수2차")
            {
                string[][] User_Value = Program.DB.getValue(DB.type.ProjDB, "User_Pump", "번호,명칭,종류,A효율,B효율,유량,동력,양정", "종류 ='지하수순환펌프'"); //지하순환펌프
                if (User_Value.Length > 0)
                {
                    for (int n = 0; n < User_Value.Length; n++)
                    {
                        int nRow = Pump_dataGridView.Rows.Add();
                        for (int a = 0; a < User_Value[0].Length; a++)
                        {
                            Pump_dataGridView.Rows[nRow].Cells[a + 2].Value = User_Value[n][a];
                        }
                        Program.UTIL.dataGridView_doubleComa(Pump_dataGridView, nRow, 5, 1);
                        Program.UTIL.dataGridView_doubleComa(Pump_dataGridView, nRow, 6, 1);
                        Program.UTIL.dataGridView_doubleComa(Pump_dataGridView, nRow, 7, 0);
                        Program.UTIL.dataGridView_doubleComa(Pump_dataGridView, nRow, 8, 0);
                        Program.UTIL.dataGridView_doubleComa(Pump_dataGridView, nRow, 9, 0);
                    }
                }
                else MessageBox.Show("장비일람표에서 펌프를 작성해 주세요.", "Check", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void TableMake()
        {
            new StackedHeaderDecorator(Pump_dataGridView, DataGridViewAutoSizeColumnsMode.Fill, datagridviewDesign);
            // DataTable Pump_table = new DataTable();
            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
            Pump_dataGridView.Columns.Clear();
            checkBoxColumn.HeaderText = "선택";
            checkBoxColumn.Name = "check";
            Pump_dataGridView.Columns.Add(checkBoxColumn);
            Pump_dataGridView.Columns.Add("A1", "설치대수"); //직접입력하는 항목임
            Pump_dataGridView.Columns.Add("A2", "번호");
            Pump_dataGridView.Columns.Add("A3", "명칭");
            Pump_dataGridView.Columns.Add("A4", "종류");
            Pump_dataGridView.Columns.Add("A5", "효율.A[%]");

            Pump_dataGridView.Columns.Add("A6", "효율.B.[%]");
            Pump_dataGridView.Columns.Add("A7", "유량.[CMH]");
            Pump_dataGridView.Columns.Add("A8", "동력.[W]");
            Pump_dataGridView.Columns.Add("A9", "양정.[m]");

            Pump_dataGridView.Columns[5].Visible = false;
            Pump_dataGridView.Columns[7].Visible = false;
            Pump_dataGridView.Columns[9].Visible = false;
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
            SelectRow = 0;
            
            foreach (DataGridViewRow selectrow in Pump_dataGridView.Rows)
            {
                if (Convert.ToBoolean(selectrow.Cells["check"].Value))
                {
                    selectrow.DefaultCellStyle.SelectionBackColor = SystemColors.GradientInactiveCaption;
                    
                    SelectRow = selectrow.Index;
                    
                    if (selectrow.Cells[1].Value == null)
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

            SelectPN = Program.UTIL.dataGridView_doubleComa(Pump_dataGridView, SelectRow, 1, 0).ToString() ;
            SelectP = Pump_dataGridView.Rows[SelectRow].Cells[2].Value.ToString();               
                                                    
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void reset()
        {
            SelectP = null;
            SelectPN = null;
            SelectRow = 0;

            for (int n = 0; n < Pump_dataGridView.Rows.Count; n++)
            {
                Pump_dataGridView.Rows[n].Cells[0].Value = false;
                Pump_dataGridView.Rows[n].Cells[1].Value = null;
            }
        }

        private void Load_SaveValue(string _SelectP, string _SelectPN)
        {
            reset();
           
            for (int i = 0; i<Pump_dataGridView.Rows.Count; i++)
            {
                if (Pump_dataGridView.Rows[i].Cells[2].Value.ToString() == SelectP)
                {
                    Pump_dataGridView.Rows[i].Cells[0].Value = true;
                    Pump_dataGridView.Rows[i].Cells[1].Value = SelectPN;
                }

            }
        }
    }
}
