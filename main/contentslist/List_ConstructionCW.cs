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
    public partial class List_ConstructionCW : Form
    {
        static String currentID = "";
        static String inEditing = "Add";

        String CWNum;
        double CountDB;
        int SelectRow;
        DataTable CWList = new DataTable();
        DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();

        public List_ConstructionCW()
        {
            InitializeComponent();

            string[][] Image = Program.DB.getValue(DB.type.BaseDB, "메뉴아이콘", "하위메뉴아이콘", "하위메뉴명 = '커튼월창'");
            Icon_pictureBox.Load(Program.gPath + Image[0][0]);
            Icon_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            Program.DB.initTable(DB.type.ProjDB, "ConstructionCW");
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
                f.CopyForm(currentID);
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
            CWList.Columns.Add("번호", typeof(string));
            CWList.Columns.Add("명칭", typeof(string));
            CWList.Columns.Add("Type", typeof(string));
            CWList.Columns.Add("유효열관류율" + Environment.NewLine + "[W/m²·K]", typeof(string));
            CWList.Columns.Add("태양열취득률" + Environment.NewLine + "[-]", typeof(string));
            CWList.Columns.Add("빛투과율" + Environment.NewLine + "[-]", typeof(string));
            CWList.Columns.Add("면적" + Environment.NewLine + "[m²]", typeof(string));
            CWList.Columns.Add("유리종류", typeof(string));
            dataGridView1.DataSource = CWList;



        }

        public void load_List()
        {
            string[][] List = Program.DB.getValue(DB.type.ProjDB, "ConstructionCW", "번호,명칭,Type,커튼월창유효열관류율,태양열취득률,빛투과율,커튼월면적,고정유리종류", "");
            List<object> mainMenu = new List<object>(); // 예시 코드: 메인 메뉴 동적 할당
            String Blank = "";
            CWList.Rows.Clear();
            for (int n = 0; n < List.Length; n++)
            {
                CWList.Rows.Add(List[n][0], List[n][1], List[n][2], String.Format("{0:F2}", Convert.ToDouble(List[n][3])), String.Format("{0:F2}", Convert.ToDouble(List[n][4])), String.Format("{0:F2}", Convert.ToDouble(List[n][5])), String.Format("{0:F2}", Convert.ToDouble(List[n][6])), List[n][7]);
                mainMenu.Add(new { text = List[n][0] + "." + List[n][1], id = "{\\\"formID\\\":2,\\\"ID\\\":\\\"" + List[n][0] + "\\\"}" }); // 예시 코드: 메인 메뉴 동적 할당
            }
            dataGridView1.DataSource = CWList;
            CountDB = List.Length;
            Program.UTIL.resetMainTree(1, 1, mainMenu.ToArray(), "2"); // 예시 코드: 메인 메뉴 동적 할당
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
                    Load_form(CWNum, "Copy");
                    Load_form(CWNum, "Edit");
              
            }
        }

        public void LoadData(String ID)            // 리스트에서 항목 더블 클릭시 - 뷰를 ID 의 getValue 값으로 채우기
        {
            load_List();
        }
    }
}
