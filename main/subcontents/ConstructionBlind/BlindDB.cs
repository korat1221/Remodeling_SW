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

namespace main.subcontents.ConstructionBlind
{
    public partial class BlindDB : Form
    {
        public String[] Select_Blind = new string[11];
        String UserNum;
        int SelectRowIndex;
        String SelectNum;
        public BlindDB(String Num)
        {
            this.SelectNum = Num;
            InitializeComponent(); this.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular);
            //heatingSystem = system;
            load_table_DB();
            string[][] Image = Program.DB.getValue(DB.type.BaseDB_HCneed, "메뉴아이콘", "하위메뉴아이콘", "하위메뉴명 = '차양정보'");
            if(Image.Length > 0 )
            {
                Icon_pictureBox.Load(Program.gPath + Image[0][0]);
                Icon_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            }
            Load_Select();
        }

        void load_table_DB()
        {
            new StackedHeaderDecorator(Blind_dataGridView, DataGridViewAutoSizeColumnsMode.Fill);
            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
            Blind_dataGridView.Columns.Clear();
            checkBoxColumn.HeaderText = "선택";
            checkBoxColumn.Name = "check";
            Blind_dataGridView.Columns.Add(checkBoxColumn);

            Blind_dataGridView.Columns.Add("A1", "번호");
            Blind_dataGridView.Columns.Add("A2", "DB유형");
            Blind_dataGridView.Columns.Add("A3", "제품명");
            Blind_dataGridView.Columns.Add("A4", "종류");
            Blind_dataGridView.Columns.Add("A5", "설치");
            Blind_dataGridView.Columns.Add("A6", "투과수준");
            Blind_dataGridView.Columns.Add("A7", "색깔");
            Blind_dataGridView.Columns.Add("A8", "외부반사율");
            Blind_dataGridView.Columns.Add("A9", "내부반사율");
            Blind_dataGridView.Columns.Add("A10", "투과율");
            Blind_dataGridView.Columns.Add("A11", "흡수율");

            string[][] DefaultDB_Value = Program.DB.getValue(DB.type.BaseDB_HCneed, "차양", "구분,DB유형,제품명,종류,설치,투과수준,색깔,외부반사율,내부반사율,투과율,흡수율", "");
            if(DefaultDB_Value.Length > 0)
            {
                for (int n = 0; n < DefaultDB_Value.Length; n++)
                {
                    Blind_dataGridView.Rows.Add();
                    int nRow = Blind_dataGridView.Rows.Count - 1;
                    Blind_dataGridView.Rows[nRow].Cells[1].Value = DefaultDB_Value[n][0];
                    Blind_dataGridView.Rows[nRow].Cells[2].Value = DefaultDB_Value[n][1];
                    Blind_dataGridView.Rows[nRow].Cells[3].Value = DefaultDB_Value[n][2];
                    Blind_dataGridView.Rows[nRow].Cells[4].Value = DefaultDB_Value[n][3];
                    Blind_dataGridView.Rows[nRow].Cells[5].Value = DefaultDB_Value[n][4];
                    Blind_dataGridView.Rows[nRow].Cells[6].Value = DefaultDB_Value[n][5];
                    Blind_dataGridView.Rows[nRow].Cells[7].Value = DefaultDB_Value[n][6];
                    Blind_dataGridView.Rows[nRow].Cells[8].Value = DefaultDB_Value[n][7];
                    Blind_dataGridView.Rows[nRow].Cells[9].Value = DefaultDB_Value[n][8];
                    Blind_dataGridView.Rows[nRow].Cells[10].Value = DefaultDB_Value[n][9];
                    Blind_dataGridView.Rows[nRow].Cells[11].Value = DefaultDB_Value[n][10];
                }
            }
           string[][] UserDB_Value = Program.DB.getValue(DB.type.ProjDB, "User_Blind", "번호,DB유형,제품명,종류,설치,투과수준,색깔,외부반사율,내부반사율,투과율,흡수율", "");
            if(UserDB_Value.Length > 0 )
            {
                for (int n = 0; n < UserDB_Value.Length; n++)
                {
                    Blind_dataGridView.Rows.Add();
                    int nRow = Blind_dataGridView.Rows.Count - 1;
                    DataGridViewComboBoxCell 종류Combo = new DataGridViewComboBoxCell();
                    종류Combo.Items.Add("베네치안");
                    종류Combo.Items.Add("스크린");
                    종류Combo.Items.Add("롤");
                    Blind_dataGridView.Rows[nRow].Cells[4] = 종류Combo;


                    DataGridViewComboBoxCell 설치Combo = new DataGridViewComboBoxCell();
                    설치Combo.Items.Add("외부측");
                    설치Combo.Items.Add("중간");
                    설치Combo.Items.Add("내부측");
                    Blind_dataGridView.Rows[nRow].Cells[5] = 설치Combo;

                    DataGridViewComboBoxCell 투과수준Combo = new DataGridViewComboBoxCell();
                    투과수준Combo.Items.Add("보통");
                    투과수준Combo.Items.Add("없음");
                    Blind_dataGridView.Rows[nRow].Cells[6] = 투과수준Combo;


                    DataGridViewComboBoxCell 색깔Combo = new DataGridViewComboBoxCell();
                    색깔Combo.Items.Add("어두운");
                    색깔Combo.Items.Add("밝은");
                    Blind_dataGridView.Rows[nRow].Cells[7] = 색깔Combo;
                    Blind_dataGridView.Rows[nRow].Cells[1].Value = UserDB_Value[n][0];
                    Blind_dataGridView.Rows[nRow].Cells[2].Value = UserDB_Value[n][1];
                    Blind_dataGridView.Rows[nRow].Cells[3].Value = UserDB_Value[n][2];
                    Blind_dataGridView.Rows[nRow].Cells[4].Value = UserDB_Value[n][3];
                    Blind_dataGridView.Rows[nRow].Cells[5].Value = UserDB_Value[n][4];
                    Blind_dataGridView.Rows[nRow].Cells[6].Value = UserDB_Value[n][5];
                    Blind_dataGridView.Rows[nRow].Cells[7].Value = UserDB_Value[n][6];
                    Blind_dataGridView.Rows[nRow].Cells[8].Value = UserDB_Value[n][7];
                    Blind_dataGridView.Rows[nRow].Cells[9].Value = UserDB_Value[n][8];
                    Blind_dataGridView.Rows[nRow].Cells[10].Value = UserDB_Value[n][9];
                    Blind_dataGridView.Rows[nRow].Cells[11].Value = UserDB_Value[n][10];
                }
            }
        }

        private void Add_button_Click(object sender, EventArgs e)
        {
            int nRow = Blind_dataGridView.Rows.Add();

            UserNum = Program.UTIL.CreateNum("User_Blind", "번호", "USD_0");
            Blind_dataGridView.Rows[nRow].Cells[1].Value = UserNum;
            Blind_dataGridView.Rows[nRow].Cells[2].Value = "사용자";

            DataGridViewComboBoxCell 종류Combo = new DataGridViewComboBoxCell();
            종류Combo.Items.Add("베네치안");
            종류Combo.Items.Add("스크린");
            종류Combo.Items.Add("롤");
            Blind_dataGridView.Rows[nRow].Cells[4] = 종류Combo;


            DataGridViewComboBoxCell 설치Combo = new DataGridViewComboBoxCell();
            설치Combo.Items.Add("외부측");
            설치Combo.Items.Add("중간");
            설치Combo.Items.Add("내부측");
            Blind_dataGridView.Rows[nRow].Cells[5] = 설치Combo;

            DataGridViewComboBoxCell 투과수준Combo = new DataGridViewComboBoxCell();
            투과수준Combo.Items.Add("보통");
            투과수준Combo.Items.Add("없음");
            Blind_dataGridView.Rows[nRow].Cells[6] = 투과수준Combo;


            DataGridViewComboBoxCell 색깔Combo = new DataGridViewComboBoxCell();
            색깔Combo.Items.Add("어두운");
            색깔Combo.Items.Add("밝은");
            Blind_dataGridView.Rows[nRow].Cells[7] = 색깔Combo;
        }

        private void Remove_button_Click(object sender, EventArgs e)
        {
            int k = Blind_dataGridView.CurrentCell.RowIndex;
            if (k > -1)
            {
                if (Blind_dataGridView.Rows[k].Cells[2].Value.ToString() == "사용자")
                {
                    if ((MessageBox.Show(Blind_dataGridView.Rows[k].Cells[3].Value.ToString() + "을 삭제하시겠습니까?", "삭제 확인", MessageBoxButtons.YesNo) == DialogResult.Yes))
                    {
                        String Delete_Num = Blind_dataGridView.Rows[k].Cells[1].Value.ToString();
                        Program.DB.deleteValue(DB.type.ProjDB, "User_Blind", "번호 ='" + Delete_Num + "'");
                        load_table_DB();
                    }
                }
                else
                {
                    MessageBox.Show("기본 DB는 삭제할 수 없습니다.");
                }
            }
        }

        private void Copy_button_Click(object sender, EventArgs e)
        {
            int nRow = Blind_dataGridView.Rows.Add();
            UserNum = Program.UTIL.CreateNum("User_Blind", "번호", "USD_0");
            Blind_dataGridView.Rows[nRow].Cells[1].Value = UserNum;
            Blind_dataGridView.Rows[nRow].Cells[2].Value = "사용자";
            Blind_dataGridView.Rows[nRow].Cells[3].Value = Blind_dataGridView.Rows[SelectRowIndex].Cells[3].Value.ToString() + "_복사";
            DataGridViewComboBoxCell 종류Combo = new DataGridViewComboBoxCell();
            종류Combo.Items.Add("베네치안");
            종류Combo.Items.Add("스크린");
            종류Combo.Items.Add("롤");
            Blind_dataGridView.Rows[nRow].Cells[4] = 종류Combo;


            DataGridViewComboBoxCell 설치Combo = new DataGridViewComboBoxCell();
            설치Combo.Items.Add("외부측");
            설치Combo.Items.Add("중간");
            설치Combo.Items.Add("내부측");
            Blind_dataGridView.Rows[nRow].Cells[5] = 설치Combo;

            DataGridViewComboBoxCell 투과수준Combo = new DataGridViewComboBoxCell();
            투과수준Combo.Items.Add("보통");
            투과수준Combo.Items.Add("없음");
            Blind_dataGridView.Rows[nRow].Cells[6] = 투과수준Combo;


            DataGridViewComboBoxCell 색깔Combo = new DataGridViewComboBoxCell();
            색깔Combo.Items.Add("어두운");
            색깔Combo.Items.Add("밝은");
            for (int k = 4; k < 12; k++)
            {
                Blind_dataGridView.Rows[nRow].Cells[k].Value = Blind_dataGridView.Rows[SelectRowIndex].Cells[k].Value;
            }

           
        }


        private void Save_button_Click(object sender, EventArgs e)
        {
                string[][] DefaultDB_Value = Program.DB.getValue(DB.type.BaseDB_HCneed, "차양", "구분", "");
                if (Blind_dataGridView.Rows.Count > DefaultDB_Value.Length)
                {
                    for (int k = DefaultDB_Value.Length; k < Blind_dataGridView.Rows.Count; k++)
                    {
                        string[][] 프로젝트유형 = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "프로젝트유형번호");
                        Program.DB.setValue(DB.type.ProjDB, "User_Blind", "번호,프로젝트유형,DB유형,제품명,종류,설치,투과수준,색깔,외부반사율,내부반사율,투과율,흡수율",
                           "'" + Blind_dataGridView.Rows[k].Cells[1].Value + "','" +
                           프로젝트유형[0][0] + "','" +
                           Blind_dataGridView.Rows[k].Cells[2].Value + "','" +
                           Blind_dataGridView.Rows[k].Cells[3].Value + "','" +
                           Blind_dataGridView.Rows[k].Cells[4].Value + "','" +
                           Blind_dataGridView.Rows[k].Cells[5].Value + "','" +
                           Blind_dataGridView.Rows[k].Cells[6].Value + "','" +
                           Blind_dataGridView.Rows[k].Cells[7].Value + "','" +
                           Blind_dataGridView.Rows[k].Cells[8].Value + "','" +
                           Blind_dataGridView.Rows[k].Cells[9].Value + "','" +
                           Blind_dataGridView.Rows[k].Cells[10].Value + "','" +
                           Blind_dataGridView.Rows[k].Cells[11].Value + "'", "번호");
                    }
                }

                DataGridViewRow row = Blind_dataGridView.Rows[SelectRowIndex];
                
                for (int i = 1; i < 12; i++)
                {
                    if (row.Cells[i].Value == null) { Select_Blind[i - 1] = null; }
                    else { Select_Blind[i - 1] = row.Cells[i].Value.ToString(); }
                }
                this.DialogResult = DialogResult.OK;
                this.Close();

        

        }
        private void reset()
        {
            for (int n = 0; n < Blind_dataGridView.Rows.Count; n++)
            {
                Blind_dataGridView.Rows[n].Cells[0].Value = false;
            }

        }
        private void Load_Select()
        {
            reset();
          
                for(int n = 0; n< Blind_dataGridView.Rows.Count;n++)
                {
                    if (Blind_dataGridView.Rows[n].Cells[1].Value.ToString() == SelectNum)
                    {
                        Blind_dataGridView.Rows[n].Cells[0].Value = true;
                    }
                }
               
        }

        private void Blind_dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                Blind_dataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
                SelectRowIndex = e.RowIndex;
            }
        }
    }
}
