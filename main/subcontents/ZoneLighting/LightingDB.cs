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
        String FrameType;
        double Count_LightDB;
        int SelectRow;
        public String[] Select_Light = new string[9];
        String UserNum, UserDBName, UserDB_Manufacture, UserDB_SingleDoubleTriple, UserDB_ArAir, UserDB_LE_CL_V;
        Double UserDB_Ug, UserDB_g, UserDB_Tao, UserDB_RExternal, UserDB_RInternal;

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
            Converter_comboBox.Items.Add("CL");
            Converter_comboBox.Items.Add("LE");
            Converter_comboBox.Items.Add("V");
            Converter_comboBox.SelectedIndex = 0;

            //번호
            UserNum = Program.UTIL.CreateNum("User_Lighting", "번호", "ULP_0");
            UserNum_textBox.Text = UserNum;
        }


        void load_table_LightDB()
        {
            DataTable table_Light = new DataTable();
            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
            Light_dataGridView.Columns.Clear();
            checkBoxColumn.HeaderText = "선택";
            checkBoxColumn.Name = "check";
            Light_dataGridView.Columns.Add(checkBoxColumn);
            table_Light.Columns.Add("번호", typeof(string));
            table_Light.Columns.Add("등기구명칭", typeof(string));
            table_Light.Columns.Add("램프유형", typeof(string));
            table_Light.Columns.Add("제조사", typeof(string));
            table_Light.Columns.Add("안정기/\r\n컨버터", typeof(string));
            table_Light.Columns.Add("광속" + Environment.NewLine + "Fi[lm]", typeof(string));
            table_Light.Columns.Add("소비전력" + Environment.NewLine + "Pi[W]", typeof(string));
            table_Light.Columns.Add("광효율" + Environment.NewLine + "ηLB[lm/W]", typeof(string));
            table_Light.Columns.Add("조명계수" + Environment.NewLine + "FL[-]", typeof(string));


            //try
            //{
            //    string[][] User_CWDoorFrame = Program.DB.getValue(DB.type.ProjDB, "User_CWDoorFrame", "번호,DB유형,제품명,제조사,구분,프레임열관류율,프레임두께", "");
            //    for (int n = 0; n < User_CWDoorFrame.Length; n++)
            //    {
            //        table_CWDoor.Rows.Add(User_CWDoorFrame[n][0], User_CWDoorFrame[n][1], User_CWDoorFrame[n][2], User_CWDoorFrame[n][3], User_CWDoorFrame[n][4], User_CWDoorFrame[n][5], User_CWDoorFrame[n][6]);
            //    }
            //}
            //catch { }

            string[][] Light = Program.DB.getValue(DB.type.BaseDB_Lighting, "조명_DB", "번호,등기구명칭,램프유형,제조사,안정기_컨버터,광속,소비전력,광효율,조명계수", "");

            for (int n = 0; n <Light.Length; n++)
            {
                table_Light.Rows.Add(Light[n][0],Light[n][1],Light[n][2],Light[n][3],Light[n][4],Light[n][5],Light[n][6],Light[n][7],Light[n][8]);
            }
            Light_dataGridView.DataSource = table_Light;
            Count_LightDB = Light.Length;

        }



        //private void UserDBName_textBox_TextChanged(object sender, EventArgs e)
        //{
        //    UserDBName = UserDBName_textBox.Text;
        //}


        //private void ArAir_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    UserDB_ArAir = LampType_comboBox.SelectedItem.ToString();
        //}



        //private void UserDB_Tao_textBox_TextChanged(object sender, EventArgs e)
        //{
        //    UserDB_Tao = Convert.ToDouble(UserDB_Tao_textBox.Text);
        //}

        //private void UserDB_RExternal_textBox_TextChanged(object sender, EventArgs e)
        //{
        //    UserDB_RExternal = Convert.ToDouble(UserDB_RExternal_textBox.Text);
        //}

        //private void UserDB_RInternal_textBox_TextChanged(object sender, EventArgs e)
        //{
        //    UserDB_RInternal = Convert.ToDouble(UserDB_RInternal_textBox.Text);
        //}



        //private void AddUserDB_button_Click(object sender, EventArgs e)
        //{
        //    if (UserDBName != null && UserDB_SingleDoubleTriple != null && UserDB_ArAir != null && UserDB_LE_CL_V != null && UserDB_Ug != 0 && UserDB_g != 0 && UserDB_Tao != 0 && UserDB_RExternal != 0 && UserDB_RInternal != 0)
        //    {
        //        Program.DB.setValue(DB.type.ProjDB, "User_Glass", "번호,DB유형,제품명,제조사,복층_삼중_단창,아르곤_공기,LE_CL_V,열관류율,태양열취득율,빛투과율,외부반사율,내부반사율",
        //            "'" + UserNum + "','" + "사용자" + "','" + UserDBName + "','" + UserDB_Manufacture + "','" + UserDB_SingleDoubleTriple + "','" + UserDB_ArAir + "','" + UserDB_LE_CL_V + "','" + UserDB_Ug.ToString() + "','" + UserDB_g.ToString() + "','" + UserDB_Tao.ToString() + "','" + UserDB_RExternal.ToString() + "','" + UserDB_RInternal.ToString() + "'", "번호");
        //        load_table_LightDB();
        //    }
        //    else
        //    {
        //        MessageBox.Show("모든 값을 입력해주세요.");
        //    }

        //}

        //private void Deletebutton_Click(object sender, EventArgs e)
        //{
        //    int k = Light_dataGridView.CurrentCell.RowIndex;
        //    if (k > -1)
        //    {
        //        if (Light_dataGridView.Rows[k].Cells[2].Value.ToString() == "사용자")
        //        {
        //            if ((MessageBox.Show(Light_dataGridView.Rows[k].Cells[3].Value.ToString() + "을 삭제하시겠습니까?", "삭제 확인", MessageBoxButtons.YesNo) == DialogResult.Yes))
        //            {
        //                String Delete_Num = Light_dataGridView.Rows[k].Cells[1].Value.ToString();
        //                Program.DB.deleteValue(DB.type.ProjDB, "User_Glass", "번호 ='" + Delete_Num + "'");
        //                load_table_LightDB();
        //            }
        //        }
        //        else
        //        {
        //            MessageBox.Show("기본 DB는 삭제할 수 없습니다.");
        //        }
        //    }


        //}

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
            Select_Light[1] = row.Cells[2].Value.ToString(); //등기구명칭
            Select_Light[2] = row.Cells[3].Value.ToString(); //램프유형
            Select_Light[3] = row.Cells[4].Value.ToString(); //제조사
            Select_Light[4] = row.Cells[5].Value.ToString(); //안정기_컨버터
            Select_Light[5] = row.Cells[6].Value.ToString(); //광속
            Select_Light[6] = row.Cells[7].Value.ToString(); //소비전력
            Select_Light[7] = row.Cells[8].Value.ToString(); //광효율
            Select_Light[8] = row.Cells[9].Value.ToString(); //조명계수


            this.DialogResult = DialogResult.OK;
            this.Close();

        }

    }
}
