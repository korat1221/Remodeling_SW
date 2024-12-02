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
        //DataTable WindowList = new DataTable();
        DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();


        public List_ConstructionWindow()
        {
            InitializeComponent(); this.Font = new Font(UTIL.Families[0], 9.75F, FontStyle.Regular);
            string[][] Image = Program.DB.getValue(DB.type.BaseDB_HCneed, "메뉴아이콘", "하위메뉴아이콘", "하위메뉴명 = '창호'");
            if(Image.Length > 0 )
            {
                Icon_pictureBox.Load(Program.gPath + Image[0][0]);
                Icon_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            }            
            Program.DB.initTable(DB.type.ProjDB, "SubWindow");
            Program.DB.initTable(DB.type.ProjDB, "ConstructionWindow");
            Create_Table();

        }

        private void GeneralPanel_Paint(object sender, PaintEventArgs e)
        {
            Panel p = (Panel)sender;
            ControlPaint.DrawBorder(e.Graphics, p.DisplayRectangle, System.Drawing.SystemColors.Control, ButtonBorderStyle.Solid);
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
            if (MainSub == "Main")
            {
                Program.getMenuForm().DoLoadForm(6, OnLoadProc);
            }
            else
            {
                Program.getMenuForm().DoLoadForm(31, Sub_OnLoadProc);
            }

        }


        public void Create_Table()
        {
            new StackedHeaderDecorator(dataGridView1, DataGridViewAutoSizeColumnsMode.Fill, datagridviewDesign);
            dataGridView1.Columns.Clear();
            checkBoxColumn.HeaderText = "선택";
            checkBoxColumn.Name = "check";
            dataGridView1.Columns.Add(checkBoxColumn);
            dataGridView1.Columns.Add("A1", "번호");
            dataGridView1.Columns.Add("A2", "하위번호");
            dataGridView1.Columns.Add("A3", "창호 명칭");
            dataGridView1.Columns.Add("A4", "Type");
            dataGridView1.Columns.Add("A5", "유효열관류율.[W/m"+Program.UTIL.Subscript(2, true)+"·K]");
            dataGridView1.Columns.Add("A6", "태양열취득률.[-]");
            dataGridView1.Columns.Add("A7", "빛투과율.[-]");
            dataGridView1.Columns.Add("A8", "유리종류");
            dataGridView1.Columns.Add("A9", "면적.[m"+ Program.UTIL.Subscript(2, true) + "]");
            dataGridView1.Columns.Add("A10", "개수.[EA]");
            dataGridView1.Columns[0].Width = 40;


        }

        private Boolean datagridviewDesign(DataGridViewCell cell, int column, int row)
        {
            if (dataGridView1.Rows[row].Cells[1].Value != "")
            {
                cell.Style.BackColor = SystemColors.InactiveBorder;
                cell.Style.ForeColor = Color.Black;
                cell.Style.SelectionBackColor = SystemColors.InactiveBorder;
                cell.Style.SelectionForeColor = Color.Black;
                return true;
            }
            else
                return true;
        }

        public void load_List()
        {
            List<object> mainMenu = new List<object>(); // 예시 코드: 메인 메뉴 동적 할당
            string[][] List = Program.DB.getValue(DB.type.ProjDB, "ConstructionWindow", "번호,창호명칭,Type,태양열취득률,빛투과율,유리종류", "");
            if (List.Length > 0)
            {
                String Blank = "";
                dataGridView1.Rows.Clear();
                for (int n = 0; n < List.Length; n++)
                {
                    dataGridView1.Rows.Add();
                    int nRow = dataGridView1.Rows.Count - 1;
                    dataGridView1.Rows[nRow].Cells[1].Value = List[n][0];
                    dataGridView1.Rows[nRow].Cells[2].Value = Blank;
                    dataGridView1.Rows[nRow].Cells[3].Value = List[n][1];
                    dataGridView1.Rows[nRow].Cells[4].Value = List[n][2];
                    dataGridView1.Rows[nRow].Cells[5].Value = Blank;
                    dataGridView1.Rows[nRow].Cells[6].Value = String.Format("{0:F2}", Convert.ToDouble(List[n][3]));
                    dataGridView1.Rows[nRow].Cells[7].Value = String.Format("{0:F2}", Convert.ToDouble(List[n][4]));
                    dataGridView1.Rows[nRow].Cells[8].Value = List[n][5];
                    dataGridView1.Rows[nRow].Cells[9].Value = Blank;
                    dataGridView1.Rows[nRow].Cells[10].Value = Blank;
                    string[][] SubList = Program.DB.getValue(DB.type.ProjDB, "SubWindow", "번호,명칭,창호유효열관류율,창호면적", "상위창호번호 = '" + List[n][0] + "'");

                    List<object> subMenu = new List<object>(); // 예시 코드: 메인 메뉴 동적 할당

                    for (int k = 0; k < SubList.Length; k++)
                    {
                        dataGridView1.Rows.Add();
                        int nRow2 = dataGridView1.Rows.Count - 1;
                        dataGridView1.Rows[nRow2].Cells[1].Value = Blank;
                        dataGridView1.Rows[nRow2].Cells[2].Value = SubList[k][0];
                        dataGridView1.Rows[nRow2].Cells[3].Value = SubList[k][1];
                        dataGridView1.Rows[nRow2].Cells[4].Value = List[n][2];
                        dataGridView1.Rows[nRow2].Cells[5].Value = String.Format("{0:F2}", Convert.ToDouble(SubList[k][2]));
                        dataGridView1.Rows[nRow2].Cells[6].Value = String.Format("{0:F2}", Convert.ToDouble(List[n][3]));
                        dataGridView1.Rows[nRow2].Cells[7].Value = String.Format("{0:F2}", Convert.ToDouble(List[n][4]));
                        dataGridView1.Rows[nRow2].Cells[8].Value = List[n][5];
                        dataGridView1.Rows[nRow2].Cells[9].Value = String.Format("{0:F2}", Convert.ToDouble(SubList[k][3]));
                        string[][] Area = Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope_3D", "면적", "구조체번호='" + SubList[k][0] + "'");
                        if (Area.Length > 0)
                        {
                            dataGridView1.Rows[nRow2].Cells[10].Value = Area.Length; 
                        }
                        subMenu.Add(new { text = SubList[k][0], id = "{\\\"formID\\\":31,\\\"ID\\\":\\\"" + SubList[k][0] + "\\\"}" }); // 예시 코드: 메인 메뉴 동적 할당
                    }
                    mainMenu.Add(new { text = List[n][0] + "." + List[n][1], id = "{\\\"formID\\\":6,\\\"ID\\\":\\\"" + List[n][0] + "\\\"}", children = subMenu.ToArray() }); // 예시 코드: 메인 메뉴 동적 할당
                }
            }
            CountDB = List.Length;
            Program.UTIL.resetMainTree(1, 4, mainMenu.ToArray(), "29"); // 예시 코드: 메인 메뉴 동적 할당
        }

        private void Remove_button_Click(object sender, EventArgs e)
        {
            int k = dataGridView1.CurrentCell.RowIndex;
            if ((MessageBox.Show(dataGridView1.Rows[k].Cells[3].Value.ToString() + "을 삭제 하시겠습니까?", "삭제 확인", MessageBoxButtons.YesNo) == DialogResult.Yes))
            {
                if (k > -1)
                {
                    if (dataGridView1.Rows[k].Cells[1].Value.ToString() != "")
                    {
                        String Delete_WinNum = dataGridView1.Rows[k].Cells[1].Value.ToString();
                        Program.DB.deleteValue(DB.type.ProjDB, "ConstructionWindow", "번호 ='" + Delete_WinNum + "'");
                        Program.DB.deleteValue(DB.type.ProjDB, "SubWindow", "상위창호번호 ='" + Delete_WinNum + "'");
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
                if (dataGridView1.Rows[k].Cells[1].Value.ToString() != "")
                {
                    Load_form(dataGridView1.Rows[k].Cells[1].Value.ToString(), "Edit", "Main");
                }
                else
                {
                    Load_form(dataGridView1.Rows[k].Cells[2].Value.ToString(), "Edit", "Sub");
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
                    Program.DB.executeSQL(DB.type.ProjDB, "UPDATE  ConstructionWindow" + " SET 창호명칭 = '" + dataGridView1.Rows[k].Cells[3].Value.ToString() + "_복사" + "' WHERE  번호 = '" + WinNum + "'");
                    SubCopy(Copy_WinNum);
                }
                else
                {
                    MessageBox.Show("메인 창호만 복사 가능합니다.");
                }
            }
            Load_form(WinNum, "Copy", "Main");
        }
        private void SubCopy(String Copy_WinNum)
        {

            String[][] Sub = Program.DB.getValue(DB.type.ProjDB, "SubWindow", "번호,명칭", "상위창호번호 = '" + Copy_WinNum + "'");
            if (Sub.Length > 0)
            {
                for (int n = 0; n < Sub.Length; n++)
                {
                    String New_SubNum = WinNum.ToString() + "_" + (n + 1).ToString();
                    String New_SubName = Sub[n][1] + "_복사";
                    Program.DB.CopyValue(DB.type.ProjDB, "SubWindow", "번호 ='" + Sub[n][0] + "'", New_SubNum);
                    Program.DB.executeSQL(DB.type.ProjDB, "UPDATE  SubWindow" + " SET 상위창호번호='" + WinNum + "', 명칭 = '" + New_SubName + "' WHERE  번호 = '" + New_SubNum + "'");
                }
            }
        }

        public void LoadData(String ID)            // 리스트에서 항목 더블 클릭시 - 뷰를 ID 의 getValue 값으로 채우기
        {
            load_List();
        }
    }
}
