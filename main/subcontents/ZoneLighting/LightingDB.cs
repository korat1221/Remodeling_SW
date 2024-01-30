using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace main.subcontents.ZoneLighting
{
    public partial class LightingDB : Form
    {
        
        double Count_LightDB;
        int SelectRow;
        public String[] Select_Light = new string[10];
        String UserNum, UserDB_Name, UserDB_Manufacture, UserDB_LampType, UserDB_Converter;
        double UserDB_lm, UserDB_eff, UserDB_W, UserDB_FL;



       
        public LightingDB()
        {
            InitializeComponent();
            load_table_LightDB();

            //램프유형 콤보박스
            LampType_comboBox.Items.Clear();
            LampType_comboBox.Items.Add("할로겐램프");
            LampType_comboBox.Items.Add("백열램프");
            LampType_comboBox.Items.Add("나트륨램프");
            LampType_comboBox.Items.Add("수은램프");
            LampType_comboBox.Items.Add("메탈할라이트램프");
            LampType_comboBox.Items.Add("형광램프");
            LampType_comboBox.Items.Add("형광전구");
            LampType_comboBox.Items.Add("LED램프");
            LampType_comboBox.Items.Add("LED전구");
            LampType_comboBox.SelectedIndex = 0;

            //안정기/컨버터 콤보박스
            Converter_comboBox.Items.Clear();
            Converter_comboBox.Items.Add("있음");
            Converter_comboBox.Items.Add("없음");
            Converter_comboBox.SelectedIndex = 0;

            //번호
            UserNum = Program.UTIL.CreateNum("User_Lighting", "번호", "UP_0");
            UserNum_textBox.Text = UserNum;
        }


        void load_table_LightDB()
        {
            new StackedHeaderDecorator(Light_dataGridView, DataGridViewAutoSizeColumnsMode.Fill, datagridviewDesign);
            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
            Light_dataGridView.Columns.Clear();
            checkBoxColumn.HeaderText = "선택";
            checkBoxColumn.Name = "check";
            Light_dataGridView.Columns.Add(checkBoxColumn);

            Light_dataGridView.Columns.Add("A1", "번호");
            Light_dataGridView.Columns.Add("A2", "DB유형");
            Light_dataGridView.Columns.Add("A3", "등기구명칭");
            Light_dataGridView.Columns.Add("A4", "램프유형");
            Light_dataGridView.Columns.Add("A5", "제조사");
            Light_dataGridView.Columns.Add("A6", "안정기/컨버터");
            Light_dataGridView.Columns.Add("A7", "광속.Fi[lm]");
            Light_dataGridView.Columns.Add("A8", "소비전력.Pi.[W]");
            Light_dataGridView.Columns.Add("A9", "광효율.ηLB.[lm/W]");
            Light_dataGridView.Columns.Add("A10", "조명계수.FL.[-]");

            //조명 사용자 DB 추가
            string[][] User_Lighting = Program.DB.getValue(DB.type.ProjDB, "User_Lighting", "번호,DB유형,등기구명칭,램프유형,제조사,안정기_컨버터,광속,소비전력,광효율,조명계수", "");
            if (User_Lighting.Length > 0)
            {
                for (int n = 0; n < User_Lighting.Length; n++)
                {
                    Light_dataGridView.Rows.Add();
                    int nRow = Light_dataGridView.Rows.Count - 1;
                    for (int k = 0; k < 10; k++)
                    {
                        Light_dataGridView.Rows[nRow].Cells[k + 1].Value = User_Lighting[n][k];
                    }
                   
                }
            }
            //표준 DB
            string[][] Light = Program.DB.getValue(DB.type.BaseDB_Lighting, "조명_DB", "번호,DB유형,등기구명칭,램프유형,제조사,안정기_컨버터,광속,소비전력,광효율,조명계수", "");
            if (Light.Length > 0)
            {
                for (int n = 0; n < Light.Length; n++)
                {
                    Light_dataGridView.Rows.Add();
                    int nRow = Light_dataGridView.Rows.Count - 1;
                    for (int k = 0; k < 8; k++)
                    {
                        Light_dataGridView.Rows[nRow].Cells[k + 1].Value = Light[n][k];
                    }
                    Light_dataGridView.Rows[nRow].Cells[9].Value = Light[n][8];
                    Light_dataGridView.Rows[nRow].Cells[10].Value = Light[n][9];
                    //table_Light.Rows.Add(Light[n][0],Light[n][1],Light[n][2],Light[n][3],Light[n][4],Light[n][5], Light[n][6], Light[n][7], String.Format("{0:F2}", Convert.ToDouble(Light[n][8])), String.Format("{0:F2}", Convert.ToDouble(Light[n][9])));
                }
            }
            //Light_dataGridView.DataSource = table_Light;
            Count_LightDB = Light.Length;

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
            else return false;
        }

        private void UserDBName_textBox_TextChanged_1(object sender, EventArgs e)
        {
            UserDB_Name = UserDBName_textBox.Text;
        }

        private void UserDB_Manufacture_textBox_TextChanged(object sender, EventArgs e)
        {
            UserDB_Manufacture = UserDB_Manufacture_textBox.Text;
        }

        private void UserDB_lm_textBox_TextChanged(object sender, EventArgs e)
        {
            UserDB_lm = Convert.ToDouble(UserDB_lm_textBox.Text);
        }

        private void UserDB_eff_textBox_TextChanged(object sender, EventArgs e)
        {
            UserDB_eff = Convert.ToDouble(UserDB_eff_textBox.Text);
        }

        private void Converter_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UserDB_Converter = Converter_comboBox.SelectedItem.ToString();
        }


        private void UserDB_W_textBox_TextChanged(object sender, EventArgs e)
        {
            UserDB_W = Convert.ToDouble(UserDB_W_textBox.Text);
        }

        private void UserDB_FL_textBox_TextChanged(object sender, EventArgs e)
        {
            UserDB_FL = Convert.ToDouble(UserDB_FL_textBox.Text);
        }
         
        private void LampType_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UserDB_LampType = LampType_comboBox.SelectedItem.ToString();
        }


        //SetValue 
        private void AddUserDB_button_Click(object sender, EventArgs e)
        {
            string[][] 프로젝트유형 = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "프로젝트유형번호");
            if (UserDB_Name != null && UserDB_Manufacture != null && UserDB_lm != 0 && UserDB_eff != 0 && UserDB_Converter != null && UserDB_W != 0 && UserDB_FL != 0 && UserDB_LampType != null)
            {
                Program.DB.setValue(DB.type.ProjDB, "User_Lighting", "번호,프로젝트유형,DB유형,등기구명칭,램프유형,제조사,안정기_컨버터,광속,소비전력,광효율,조명계수",
                    "'" + UserNum + "','" + 프로젝트유형[0][0] + "','" + "사용자" + "','" + UserDB_Name + "','" + UserDB_LampType + "','" + UserDB_Manufacture + "','" + UserDB_Converter + "','" + UserDB_lm.ToString() + "','" + UserDB_W.ToString() + "','" + UserDB_eff.ToString() + "','" + UserDB_FL.ToString() + "'", "번호");
                load_table_LightDB();
            }
            else
            {
                MessageBox.Show("모든 값을 입력해주세요.");
            }
        }
        private void Deletebutton_Click(object sender, EventArgs e)
        {
            int k = Light_dataGridView.CurrentCell.RowIndex;
            if (k > -1)
            {
                if (Light_dataGridView.Rows[k].Cells[2].Value.ToString() == "사용자")
                {
                    if ((MessageBox.Show(Light_dataGridView.Rows[k].Cells[3].Value.ToString() + "을 삭제하시겠습니까?", "삭제 확인", MessageBoxButtons.YesNo) == DialogResult.Yes))
                    {
                        String Delete_Num = Light_dataGridView.Rows[k].Cells[1].Value.ToString();
                        Program.DB.deleteValue(DB.type.ProjDB, "User_Lighting", "번호 ='" + Delete_Num + "'");
                        load_table_LightDB();
                    }
                }
                else
                {
                    MessageBox.Show("기본 DB는 삭제할 수 없습니다.");
                }
            }


        }



        //데이터그리드뷰 체크박스 선택 시
        private void Light_dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                Light_dataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
                SelectRow = e.RowIndex;
                DataGridViewRow row = Light_dataGridView.Rows[SelectRow];
                DataGridViewRow row2;
                for (int k = 0; k < Count_LightDB; k++)
                {
                    if (k != row.Index)
                    {
                        Light_dataGridView.Rows[k].Cells[0].Value = false;
                        row2 = Light_dataGridView.Rows[k];
                        row2.DefaultCellStyle.BackColor = Color.White;
                        row2.DefaultCellStyle.ForeColor = Color.Black;
                    }
                    else
                    {
                        row.DefaultCellStyle.BackColor = SystemColors.GradientInactiveCaption;
                        row.DefaultCellStyle.ForeColor = Color.Black;
                        row = Light_dataGridView.Rows[e.RowIndex];
                    }
                }
            }
        }

        private void Save_button_Click(object sender, EventArgs e)
        {
            //번호,등기구명칭,램프유형,제조사,안정기_컨버터,광속,소비전력,광효율,조명계수
            DataGridViewRow row = Light_dataGridView.Rows[SelectRow];
            Select_Light[0] = row.Cells[1].Value.ToString(); //번호
            Select_Light[1] = row.Cells[2].Value.ToString(); //DB유형
            Select_Light[2] = row.Cells[3].Value.ToString(); //등기구명칭
            Select_Light[3] = row.Cells[4].Value.ToString(); //램프유형
            Select_Light[4] = row.Cells[5].Value.ToString(); //제조사
            Select_Light[5] = row.Cells[6].Value.ToString(); //안정기_컨버터
            Select_Light[6] = row.Cells[7].Value.ToString(); //광속
            Select_Light[7] = row.Cells[8].Value.ToString(); //소비전력
            Select_Light[8] = row.Cells[9].Value.ToString(); //광효율
            Select_Light[9] = row.Cells[10].Value.ToString(); //조명계수


            this.DialogResult = DialogResult.OK;
            this.Close();

        }

    }
}
