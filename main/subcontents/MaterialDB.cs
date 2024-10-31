using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace main.subcontents
{
    public partial class MaterialDB : Form
    {
        double Count_FrameDB;
        int SelectRow;
        String LE_CL_V;
        public String[] Select = new String[11];
        String MaterialType, UserNum;
        int Num;
        List<String> List = new List<String>();


        public MaterialDB()
        {
            InitializeComponent(); this.Font = new Font(UTIL.Families[0], 9.75F, FontStyle.Regular);
            

            //재료유형 리스트 생성 
            MaterialType_comboBox.Items.Add("단열재");
            MaterialType_comboBox.Items.Add("콘크리트");
            MaterialType_comboBox.Items.Add("조적");
            MaterialType_comboBox.Items.Add("패널");
            MaterialType_comboBox.Items.Add("미장");
            MaterialType_comboBox.Items.Add("목재");
            MaterialType_comboBox.Items.Add("금속재");
            MaterialType_comboBox.Items.Add("타일");
            MaterialType_comboBox.Items.Add("지중");
            MaterialType_comboBox.Items.Add("공기층");
            MaterialType_comboBox.SelectedIndex = 0;
        }
        private void MaterialType_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            MaterialType = MaterialType_comboBox.SelectedItem.ToString();
            //테이블 불러오기 
            load_tableDB();
        }
        void load_tableDB()
        {
            new StackedHeaderDecorator(dataGridView, DataGridViewAutoSizeColumnsMode.Fill, dataGridView_RowHandle);
            dataGridView.Columns.Clear();
            dataGridView.Rows.Clear();
            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
            checkBoxColumn.HeaderText = "선택";
            checkBoxColumn.Name = "check";
           dataGridView.Columns.Add(checkBoxColumn);

            dataGridView.Columns.Add("A1", "번호");
            dataGridView.Columns.Add("A2", "DB유형");
            dataGridView.Columns.Add("A3", "재료명");
            dataGridView.Columns.Add("A4", "종류1");
            dataGridView.Columns.Add("A5", "종류2");
            dataGridView.Columns.Add("A6", "λ열전도율.[W/m·K]");
            dataGridView.Columns.Add("A7", "ρ밀도.[kg/m³]");
            dataGridView.Columns.Add("A8", "с비열.[kJ/kg·K]");
            dataGridView.Columns.Add("A9", "투습저항계수.dry");
            dataGridView.Columns.Add("A10", "투습저항계수.wet");
            dataGridView.Columns.Add("A11", "비고");
            dataGridView.Columns[0].Width = 40;
            dataGridView.Columns[1].Width = 60;
            dataGridView.Columns[2].Width = 70;
            dataGridView.Columns[3].Width = 150;


            if (MaterialType=="공기층")
            {
                int nRow = dataGridView.Rows.Add();
                dataGridView.Rows[nRow].Cells[1].Value = "M_000";
                dataGridView.Rows[nRow].Cells[1].Value = "표준";
                dataGridView.Rows[nRow].Cells[2].Value = "공기층";
            }
            else 
            {
                string[][] User_DB = Program.DB.getValue(DB.type.ProjDB, "User_Material", "번호,DB유형,재료명,종류2,종류1,열전도율,밀도,비열,투습저항계수dry,투습저항계수wet,비고", "구분 = '" + MaterialType + "'");
                if (User_DB.Length > 0)
                {
                    for (int n = 0; n < User_DB.Length; n++)
                    {
                        int nRow = dataGridView.Rows.Add();
                        for (int a = 0; a < 11; a++)
                        {
                            dataGridView.Rows[nRow].Cells[a + 1].Value = User_DB[n][a];
                        }
                    }
                }
                string[][] Value = Program.DB.getValue(DB.type.BaseDB_HCneed, "열전도율", "재료명,종류2,종류1,열전도율,밀도,비열,투습저항계수dry,투습저항계수wet,비고", "구분 = '" + MaterialType + "'");
                String dbnum;
                if (Value.Length > 0)
                {
                    for (int n = 0; n < Value.Length; n++)
                    {
                        int nRow = dataGridView.Rows.Add();
                        if (n + 1 < 10)
                        {
                            dbnum = "M_00" + (n + 1).ToString();
                        }
                        else
                        {
                            dbnum = "M_0" + (n + 1).ToString();
                        }

                        dataGridView.Rows[nRow].Cells[1].Value = dbnum;
                        dataGridView.Rows[nRow].Cells[2].Value = "기본";


                        for (int a = 0; a < 9; a++)
                        {
                            dataGridView.Rows[nRow].Cells[a + 3].Value = Value[n][a];
                        }
                    }
                }

            }           
        }
        private bool dataGridView_RowHandle(DataGridViewCell cell, int column, int row)
        {
            if (dataGridView.Rows[row].Cells[2].Value != null && dataGridView.Rows[row].Cells[2].Value.ToString() == "기본")
            {
                cell.Style.BackColor = Color.FromArgb(255, 255, 255);
                cell.Style.ForeColor = Color.Black;
                cell.Style.SelectionBackColor = Color.FromArgb(255, 255, 255);
                cell.Style.SelectionForeColor = Color.Black;
                return true;
            }

            return false; 
        }
        private void AddUserDB_button_Click(object sender, EventArgs e)
        {
           
            int nRow = dataGridView.Rows.Add();
            UserNum = Program.UTIL.CreateNum("User_Material", "번호", "UM_0");
            dataGridView.Rows[nRow].Cells[1].Value = UserNum;
            dataGridView.Rows[nRow].Cells[2].Value = "사용자";
            dataGridView.Rows[nRow].Cells[4].Value = "-";
            dataGridView.Rows[nRow].Cells[5].Value = "-";

            DataGridViewRow MoveRow = dataGridView.Rows[nRow];
            dataGridView.Rows.RemoveAt(nRow);
            dataGridView.Rows.Insert(0, MoveRow);
            dataGridView.CurrentCell = dataGridView[dataGridView.CurrentCell.ColumnIndex, 0];
        }

        private void Delete_button_Click(object sender, EventArgs e)
        {
            int k = dataGridView.CurrentCell.RowIndex;
            if (k > -1)
            {
                if (dataGridView.Rows[k].Cells[2].Value.ToString() == "사용자")
                {
                    if ((MessageBox.Show(dataGridView.Rows[k].Cells[3].Value.ToString() + "을 삭제하시겠습니까?", "삭제 확인", MessageBoxButtons.YesNo) == DialogResult.Yes))
                    {
                        String Delete_Num = dataGridView.Rows[k].Cells[1].Value.ToString();
                        Program.DB.deleteValue(DB.type.ProjDB, "User_Material", "번호 ='" + Delete_Num + "'");
                        load_tableDB();
                    }
                }
                else
                {
                    MessageBox.Show("기본 DB는 삭제할 수 없습니다.");
                }
            }
        }

        //데이터그리드뷰 체크박스 선택 시
        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                dataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
                SelectRow = e.RowIndex;
                DataGridViewRow row = dataGridView.Rows[SelectRow];
                DataGridViewRow row2;
                for (int k = 0; k < Count_FrameDB; k++)
                {
                    if (k != row.Index)
                    {
                        dataGridView.Rows[k].Cells[0].Value = false;
                        row2 = dataGridView.Rows[k];
                        row2.DefaultCellStyle.BackColor = Color.White;
                        row2.DefaultCellStyle.ForeColor = Color.Black;
                    }
                    else
                    {
                        row.DefaultCellStyle.BackColor = SystemColors.GradientInactiveCaption;
                        row.DefaultCellStyle.ForeColor = Color.Black;
                        row = dataGridView.Rows[e.RowIndex];
                    }
                }
            }
        }

        private void Save_button_Click(object sender, EventArgs e)
        {
            #region 사용자 저장
            string[][] 프로젝트유형 = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "프로젝트유형번호");

            for(int a=0; a< dataGridView.Rows.Count; a++)
            {
                if (dataGridView.Rows[a].Cells[2].Value.ToString() == "사용자")
                {
                    if (dataGridView.Rows[a].Cells[3].Value == null || dataGridView.Rows[a].Cells[6].Value == null)
                    {
                        MessageBox.Show("사용자DB의 재료명 및 열전도율은 필수 입력정보 입니다.");
                        goto 사용자DB부족;
                    }
                    else
                    {

                        String[] Value = new String[9];
                        for (int i = 0; i < 9; i++)
                        {
                            if (dataGridView.Rows[a].Cells[3 + i].Value != null)
                            { Value[i] = dataGridView.Rows[a].Cells[3 + i].Value.ToString(); }
                            else { Value[i] = ""; }
                        }

                        Program.DB.setValue(DB.type.ProjDB, "User_Material", "번호,프로젝트유형,DB유형,구분,재료명,종류2,종류1,열전도율,밀도,투습저항계수dry,투습저항계수wet,비열,비고",
                             "'" + dataGridView.Rows[a].Cells[1].Value.ToString() + "','" + 프로젝트유형[0][0] + "','" + "사용자" + "','" + MaterialType + "','"
                             + Value[0] + "','"
                             + Value[1] + "','"
                             + Value[2] + "','"
                             + Value[3] + "','"
                             + Value[4] + "','"
                             + Value[5] + "','"
                             + Value[6] + "','"
                             + Value[7] + "','"
                             + Value[8] + "'", "번호");
                    }
                }
            }
            #endregion

            DataGridViewRow row = dataGridView.Rows[SelectRow];

            for (int i = 1; i < (row.Cells.Count - 2); i++)
            {
                if (row.Cells[i + 2].Value != null)
                { Select[i] = row.Cells[i + 2].Value.ToString(); }
                else { Select[i] = ""; }
            }
            Select[0] = row.Cells[1].Value.ToString();
            Select[10] = MaterialType;

            this.DialogResult = DialogResult.OK;
            this.Close();
            사용자DB부족: int c = 0; 

        }

    }
}
