using main.contents;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace main.contentslist
{
    public partial class List_Zone : Form
    {
        static String currentID = "";
        static String inEditing = "Add";

        String CWNum;
        double CountDB;
        int SelectRow;
        DataTable ListTable = new DataTable();
        DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();

        public List_Zone()
        {
            InitializeComponent();

            string[][] Image = Program.DB.getValue(DB.type.BaseDB_HCneed, "메뉴아이콘", "하위메뉴아이콘", "하위메뉴명 = '커튼월창'");
            Icon_pictureBox.Load(Program.gPath + Image[0][0]);
            Icon_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            Create_Table();
        }

        private void GeneralPanel_Paint(object sender, PaintEventArgs e)
        {
            Panel p = (Panel)sender;
            ControlPaint.DrawBorder(e.Graphics, p.DisplayRectangle, Color.FromArgb(153, 180, 209), ButtonBorderStyle.Solid);
        }


        private void Add_button_Click(object sender, EventArgs e)
        {
            CWNum = Program.UTIL.CreateNum("ConstructionCW", "번호", "CW");

            Program.getMenuForm().ResetForm(2);

            Load_form(CWNum, "Add");
        }

        public static bool OnLoadProc(Form form)
        {
            ConstructionCW f = (ConstructionCW)form;

            if (inEditing == "Edit")
            {
                f.LoadData(currentID);

            }
            else if (inEditing == "Copy")
            {
                f.LoadData(currentID);
            }
            else
            {
                f.ResetForm(currentID);
            }

            return true;
        }

        private void Load_form(String ID, String editing)
        {
            currentID = ID;
            inEditing = editing;
            Program.getMenuForm().DoLoadForm(2, OnLoadProc);
        }


        public void Create_Table()
        {

            dataGridView1.Columns.Clear();
            checkBoxColumn.HeaderText = "선택";
            checkBoxColumn.Name = "check";
            dataGridView1.Columns.Add(checkBoxColumn);
            ListTable.Columns.Add("번호", typeof(string));
            ListTable.Columns.Add("명칭", typeof(string));
            ListTable.Columns.Add("용도프로필", typeof(string));
            ListTable.Columns.Add("순바닥면적" + Environment.NewLine + "[m²]", typeof(string));
            ListTable.Columns.Add("천장고" + Environment.NewLine + "[-]", typeof(string));
            dataGridView1.DataSource = ListTable;



        }

        public void load_List()
        {
            string[][] List = Program.DB.getValue_dedupe(DB.type.ProjDB, "ZoneEnvelope", "존", "");
            List<object> mainMenu = new List<object>(); // 예시 코드: 메인 메뉴 동적 할당
            String Blank = "";
            ListTable.Rows.Clear();
            for (int n = 0; n < List.Length; n++)
            {
                ListTable.Rows.Add(List[n][0],null,null,null,null );
             //  mainMenu.Add(new { text = List[n][0], id = "{\\\"formID\\\":12,\\\"ID\\\":\\\"" + List[n][0] + "\\\"}" }); // 예시 코드: 메인 메뉴 동적 할당
            }
            dataGridView1.DataSource = ListTable;
            CountDB = List.Length;
          //  Program.UTIL.resetMainTree(3, 0, mainMenu.ToArray(), "12"); // 예시 코드: 메인 메뉴 동적 할당
        }

        //선택한 열 색 표시
        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                dataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit);
                SelectRow = e.RowIndex;
                DataGridViewRow row = dataGridView1.Rows[SelectRow];
                DataGridViewRow row2;
                for (int k = 0; k < CountDB; k++)
                {
                    if (k != row.Index)
                    {
                        dataGridView1.Rows[k].Cells[0].Value = false;
                        row2 = dataGridView1.Rows[k];
                        row2.DefaultCellStyle.BackColor = Color.White;
                        row2.DefaultCellStyle.ForeColor = Color.Black;
                    }
                    else
                    {
                        row.DefaultCellStyle.BackColor = SystemColors.GradientInactiveCaption;
                        row.DefaultCellStyle.ForeColor = Color.Black;
                        row = dataGridView1.Rows[e.RowIndex];
                    }
                }
            }
        }


        private void Remove_button_Click(object sender, EventArgs e)
        {
            int k = dataGridView1.CurrentCell.RowIndex;
            if ((MessageBox.Show(dataGridView1.Rows[k].Cells[2].Value.ToString() + "을 삭제 하시겠습니까?", "삭제 확인", MessageBoxButtons.YesNo) == DialogResult.Yes))
            {
                if (k > -1)
                {
                        String Delete_Num = dataGridView1.Rows[k].Cells[1].Value.ToString();
                        Program.DB.deleteValue(DB.type.ProjDB, "ConstructionCW", "번호 ='" + Delete_Num + "'");
                        load_List();
                   
                }
            }

        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            int k = dataGridView1.CurrentCell.RowIndex;
            if (k > -1)
            {
                    Load_form(dataGridView1.Rows[k].Cells[1].Value.ToString(), "Edit");

            }

        }

        private void Copy_button_Click(object sender, EventArgs e)
        {
            CWNum = Program.UTIL.CreateNum("ConstructionCW", "번호", "CW");
            int k = dataGridView1.CurrentCell.RowIndex;
            if (k > -1)
            {
                String Copy_Num = dataGridView1.Rows[k].Cells[1].Value.ToString();
              
                Program.DB.CopyValue(DB.type.ProjDB, "ConstructionCW", "번호 ='" + Copy_Num + "'", CWNum);
                Program.DB.executeSQL(DB.type.ProjDB, "UPDATE  ConstructionCW" + " SET 명칭 = '" + dataGridView1.Rows[k].Cells[2].Value.ToString() + "_복사" + "' WHERE  번호 = '" + CWNum + "'");                
                Load_form(CWNum, "Copy");
              
            }
        }

        public void LoadData(String ID)            // 리스트에서 항목 더블 클릭시 - 뷰를 ID 의 getValue 값으로 채우기
        {
            load_List();
        }
    }
}
