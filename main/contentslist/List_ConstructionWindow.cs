using main.contents;
using main.subcontents.ConstructionWindow;
using System;
using System.Collections;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Windows.Forms;

namespace main.contentslist
{
    public partial class List_ConstructionWindow : Form
    {
        static String currentID = "";
        static String inEditing = "Add";

        int Num;
        String WinNum;
        double CountDB;
        int SelectRow;
        DataTable WindowList = new DataTable();
        DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();


        public List_ConstructionWindow()
        {
            InitializeComponent();
            string[][] Image = Program.DB.getValue(DB.type.BaseDB, "메뉴아이콘", "하위메뉴아이콘", "하위메뉴명 = '창호'");
            Icon_pictureBox.Load(Program.gPath + Image[0][0]);
            Icon_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            Program.DB.initTable(DB.type.ProjDB, "SubWindow");
            Program.DB.initTable(DB.type.ProjDB, "ConstructionWindow");
            Create_Table();

        }

        private void GeneralPanel_Paint(object sender, PaintEventArgs e)
        {
            Panel p = (Panel)sender;
            ControlPaint.DrawBorder(e.Graphics, p.DisplayRectangle, Color.FromArgb(153, 180, 209), ButtonBorderStyle.Solid);
        }


        private void Add_button_Click(object sender, EventArgs e)
        {
            WinNum = Program.UTIL.CreateNum("ConstructionWindow", "번호", "WIN");

            Program.getMenuForm().ResetForm(6);

            Load_form(WinNum, "Add", "Main");
        }

        public static bool OnLoadProc(Form form)
        {
            ConstructionWindow f = (ConstructionWindow)form;

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

        public static bool Sub_OnLoadProc(Form form)
        {
            SubWindow f = (SubWindow)form;

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

        private void Load_form(String ID, String editing, String MainSub)
        {
            currentID = ID;
            inEditing = editing;
            if(MainSub =="Main")
            {
                Program.getMenuForm().DoLoadForm(6, OnLoadProc);
            }else
            {
                Program.getMenuForm().DoLoadForm(31, Sub_OnLoadProc);
            }

        }


        public void Create_Table()
        {

            dataGridView1.Columns.Clear();
            checkBoxColumn.HeaderText = "선택";
            checkBoxColumn.Name = "check";
            dataGridView1.Columns.Add(checkBoxColumn);
            WindowList.Columns.Add("번호", typeof(string));
            WindowList.Columns.Add("하위번호", typeof(string));
            WindowList.Columns.Add("창호 명칭", typeof(string));
            WindowList.Columns.Add("Type", typeof(string));
            WindowList.Columns.Add("유효열관류율" + Environment.NewLine + "[W/m²·K]", typeof(string));
            WindowList.Columns.Add("태양열취득률" + Environment.NewLine + "[-]", typeof(string));
            WindowList.Columns.Add("빛투과율" + Environment.NewLine + "[-]", typeof(string));
            WindowList.Columns.Add("면적" + Environment.NewLine + "[m²]", typeof(string));
            WindowList.Columns.Add("유리종류", typeof(string));
            dataGridView1.DataSource = WindowList;


        }

        public void load_List()
        {
            string[][] List = Program.DB.getValue(DB.type.ProjDB, "ConstructionWindow", "번호,창호명칭,Type,태양열취득률,빛투과율,유리종류", "");
            List<object> mainMenu = new List<object>(); // 예시 코드: 메인 메뉴 동적 할당
            String Blank = "";
            WindowList.Rows.Clear();
            for (int n = 0; n < List.Length; n++)
            {
                WindowList.Rows.Add(List[n][0], Blank ,List[n][1], List[n][2], Blank, String.Format("{0:F2}", Convert.ToDouble(List[n][3])), String.Format("{0:F2}", Convert.ToDouble(List[n][4])), Blank, List[n][5]);

                string[][] SubList = Program.DB.getValue(DB.type.ProjDB, "SubWindow", "번호,명칭,창호유효열관류율,창호면적", "상위창호번호 = '" + List[n][0] + "'");

                List<object> subMenu = new List<object>(); // 예시 코드: 메인 메뉴 동적 할당

                for (int k = 0; k < SubList.Length; k++)
                {
                
                    WindowList.Rows.Add(Blank,SubList[k][0], SubList[k][1], List[n][2], String.Format("{0:F2}", Convert.ToDouble(SubList[k][2])), String.Format("{0:F2}", Convert.ToDouble(List[n][3])), String.Format("{0:F2}", Convert.ToDouble(List[n][4])), String.Format("{0:F2}", Convert.ToDouble(SubList[k][3])), List[n][5]);
                    subMenu.Add(new { text = SubList[k][0], id = "{\\\"formID\\\":31,\\\"ID\\\":\\\"" + SubList[k][0] + "\\\"}" }); // 예시 코드: 메인 메뉴 동적 할당

                }
                mainMenu.Add(new { text = List[n][0] + "." + List[n][1], id = "{\\\"formID\\\":6,\\\"ID\\\":\\\"" + List[n][0] + "\\\"}", children = subMenu.ToArray()}); // 예시 코드: 메인 메뉴 동적 할당
            }
            dataGridView1.DataSource = WindowList;
            CountDB = List.Length;
            Program.UTIL.resetMainTree(1, 4, mainMenu.ToArray(), "2"); // 예시 코드: 메인 메뉴 동적 할당
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
                    if (dataGridView1.Rows[k].Cells[1].Value.ToString() != "")
                    {
                        String Delete_WinNum = dataGridView1.Rows[k].Cells[1].Value.ToString();
                        Program.DB.deleteValue(DB.type.ProjDB, "ConstructionWindow", "번호 ='" + Delete_WinNum + "'");
                        load_List();
                    }
                    else
                    {
                        String Delete_WinNum = dataGridView1.Rows[k].Cells[2].Value.ToString();
                        Program.DB.deleteValue(DB.type.ProjDB, "SubWindow", "번호 ='" + Delete_WinNum + "'");
                        load_List();

                    }
                }
            }

        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            int k = dataGridView1.CurrentCell.RowIndex;
            if (k > -1)
            {
                if(dataGridView1.Rows[k].Cells[1].Value.ToString()!="")
                {
                    Load_form(dataGridView1.Rows[k].Cells[1].Value.ToString(), "Edit", "Main");
                }
                else
                { 
                    Load_form(dataGridView1.Rows[k].Cells[2].Value.ToString(), "Edit","Sub");
                }
               
            }

        }

        private void Copy_button_Click(object sender, EventArgs e)
        {
            WinNum = Program.UTIL.CreateNum("ConstructionWindow", "번호", "WIN");
            int k = dataGridView1.CurrentCell.RowIndex;
            if (k > -1)
            {
                String Copy_WinNum = dataGridView1.Rows[k].Cells[1].Value.ToString();
                if (dataGridView1.Rows[k].Cells[1].Value.ToString() != "")
                {
                    Program.DB.CopyValue(DB.type.ProjDB, "ConstructionWindow", "번호 ='" + Copy_WinNum + "'", WinNum);
                    Load_form(WinNum, "Copy", "Main");
                    Load_form(WinNum, "Edit","Main");
                }
                else
                {
                    MessageBox.Show("메인 창호만 복사 가능합니다.");
                }
            }
        }

        public void LoadData(String ID)            // 리스트에서 항목 더블 클릭시 - 뷰를 ID 의 getValue 값으로 채우기
        {
            load_List();
        }
    }
}
