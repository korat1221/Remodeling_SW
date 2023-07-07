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
    public partial class List_ConstructionRoof : Form
    {
        static String currentID = "";
        static String inEditing = "Add";

        String WallNum;
        double CountDB;
        int SelectRow;
        DataTable WallList = new DataTable();
        DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();

        public List_ConstructionRoof()
        {
            InitializeComponent();

            string[][] Image = Program.DB.getValue(DB.type.BaseDB_HCneed, "메뉴아이콘", "하위메뉴아이콘", "하위메뉴명 = '외벽'");
            Icon_pictureBox.Load(Program.gPath + Image[0][0]);
            Icon_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            Program.DB.initTable(DB.type.ProjDB, "ConstructionRoof");
            Create_Table();
        }

        private void GeneralPanel_Paint(object sender, PaintEventArgs e)
        {
            Panel p = (Panel)sender;
            ControlPaint.DrawBorder(e.Graphics, p.DisplayRectangle, Color.FromArgb(153, 180, 209), ButtonBorderStyle.Solid);
        }


        private void Add_button_Click(object sender, EventArgs e)
        {
            WallNum = Program.UTIL.CreateNum("ConstructionRoof", "번호", "WL");

            Program.getMenuForm().ResetForm(3);

            Load_form(WallNum, "Add");
        }

        public static bool OnLoadProc(Form form)
        {
            ConstructionRoof f = (ConstructionRoof)form;

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
            Program.getMenuForm().DoLoadForm(3, OnLoadProc);
        }


        public void Create_Table()
        {

            dataGridView1.Columns.Clear();
            checkBoxColumn.HeaderText = "선택";
            checkBoxColumn.Name = "check";
            dataGridView1.Columns.Add(checkBoxColumn);
            WallList.Columns.Add("번호", typeof(string));
            WallList.Columns.Add("명칭", typeof(string));
            WallList.Columns.Add("Type", typeof(string));
            WallList.Columns.Add("유효열관류율" + Environment.NewLine + "[W/m²·K]", typeof(string));
            WallList.Columns.Add("흡수율" + Environment.NewLine + "[-]", typeof(string));
            WallList.Columns.Add("면적" + Environment.NewLine + "[m²]", typeof(string));
            dataGridView1.DataSource = WallList;



        }

        public void load_List()
        {
            string[][] List = Program.DB.getValue(DB.type.ProjDB, "ConstructionRoof", "번호,명칭,Type,유효열관류율,흡수율", "");
            List<object> mainMenu = new List<object>(); // 예시 코드: 메인 메뉴 동적 할당
            String Blank = "";
            WallList.Rows.Clear();
            for (int n = 0; n < List.Length; n++)
            {
                WallList.Rows.Add(List[n][0], List[n][1], List[n][2], String.Format("{0:F2}", Convert.ToDouble(List[n][3])), String.Format("{0:F2}", Convert.ToDouble(List[n][4])));
                mainMenu.Add(new { text = List[n][0] + "." + List[n][1], id = "{\\\"formID\\\":3,\\\"ID\\\":\\\"" + List[n][0] + "\\\"}" }); // 예시 코드: 메인 메뉴 동적 할당
            }
            dataGridView1.DataSource = WallList;
            CountDB = List.Length;
            Program.UTIL.resetMainTree(1, 1, mainMenu.ToArray(), "34"); // 예시 코드: 메인 메뉴 동적 할당
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
                    Program.DB.deleteValue(DB.type.ProjDB, "ConstructionRoof", "번호 ='" + Delete_Num + "'");
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
            WallNum = Program.UTIL.CreateNum("ConstructionRoof", "번호", "WL");
            int k = dataGridView1.CurrentCell.RowIndex;
            if (k > -1)
            {
                String Copy_Num = dataGridView1.Rows[k].Cells[1].Value.ToString();

                Program.DB.CopyValue(DB.type.ProjDB, "ConstructionRoof", "번호 ='" + Copy_Num + "'", WallNum);
                Program.DB.executeSQL(DB.type.ProjDB, "UPDATE  ConstructionRoof" + " SET 명칭 = '" + dataGridView1.Rows[k].Cells[2].Value.ToString() + "_복사" + "' WHERE  번호 = '" + WallNum + "'");
                Load_form(WallNum, "Copy");

            }
        }

        public void LoadData(String ID)            // 리스트에서 항목 더블 클릭시 - 뷰를 ID 의 getValue 값으로 채우기
        {
            load_List();
        }
    }
}
