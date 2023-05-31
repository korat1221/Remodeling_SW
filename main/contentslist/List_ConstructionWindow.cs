using main.contents;
using System.Collections;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Windows.Forms;

namespace main.contentslist
{
    public partial class List_ConstructionWindow : Form
    {
        int Num;
        String WinNum;
        double CountDB;
        int SelectRow;
        DataTable WindowList = new DataTable();
        DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
        ArrayList form = new ArrayList();


        public List_ConstructionWindow()
        {
            InitializeComponent();

            string[][] Image = Program.DB.getValue(DB.type.BaseDB, "메뉴아이콘", "하위메뉴아이콘", "하위메뉴명 = '창호'");
            Icon_pictureBox.Load(Program.gPath + Image[0][0]);
            Icon_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            Program.DB.initTable(DB.type.CalcDB, "ConstructionWindow");
            Create_Table();
        }

        private void GeneralPanel_Paint(object sender, PaintEventArgs e)
        {
            Panel p = (Panel)sender;
            ControlPaint.DrawBorder(e.Graphics, p.DisplayRectangle, Color.FromArgb(153, 180, 209), ButtonBorderStyle.Solid);
        }


        private void Add_button_Click(object sender, EventArgs e)
        {
            ConstructionWindow f = new ConstructionWindow();

            Num = Num + 1;
            if (Num < 10)
            {
                WinNum = "Win0" + Num;
            }
            else
            {
                WinNum = "Win" + Num;
            }

            f.SendWinNum = WinNum;
            Load_form(f);
            form.Add(f);

        }

        private void Load_form(ConstructionWindow f)
        {
            foreach (FormMain openForm in Application.OpenForms)
            {
                if (openForm.Name == "FormMain")
                {

                    f.TopLevel = false;
                    openForm.splitContainer1.Panel2.Controls.Add(f);
                    f.Show();
                    f.BringToFront();
                    return;
                }
            }
        }


        public void Create_Table()
        {

            dataGridView1.Columns.Clear();
            checkBoxColumn.HeaderText = "선택";
            checkBoxColumn.Name = "check";
            dataGridView1.Columns.Add(checkBoxColumn);
            WindowList.Columns.Add("번호", typeof(string));
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
            string[][] List = Program.DB.getValue(DB.type.CalcDB, "ConstructionWindow", "번호,창호명칭,Type,창호유효열관류율,태양열취득률,빛투과율,창호면적,유리종류", "");

            //  List<object> mainMenu = new List<object>(); // 예시 코드: 메인 메뉴 동적 할당

            WindowList.Rows.Clear();
            for (int n = 0; n < List.Length; n++)
            {
                WindowList.Rows.Add(List[n][0], List[n][1], List[n][2], String.Format("{0:F2}", Convert.ToDouble(List[n][3])), String.Format("{0:F2}", Convert.ToDouble(List[n][4])), String.Format("{0:F2}", Convert.ToDouble(List[n][5])), String.Format("{0:F2}", Convert.ToDouble(List[n][6])), List[n][7]);

                //  mainMenu.Add(new { text = List[n][1], id = "6-" + List[n][0] }); // 예시 코드: 메인 메뉴 동적 할당
            }
            dataGridView1.DataSource = WindowList;
            CountDB = List.Length;

            // Program.UTIL.resetMainTree(1, 4, mainMenu.ToArray(), "2") ; // 예시 코드: 메인 메뉴 동적 할당
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
                    String Delete_WinNum = dataGridView1.Rows[k].Cells[1].Value.ToString();
                    Program.DB.deleteValue(DB.type.CalcDB, "ConstructionWindow", "번호 ='" + Delete_WinNum + "'");
                    load_List();
                    //    dataGridView1.Refresh();
                }
            }

        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            int k = dataGridView1.CurrentCell.RowIndex;

            for (int i = 0; i < form.Count; i++)
            {
                ConstructionWindow f = (ConstructionWindow)form[i];

                if (dataGridView1.Rows[k].Cells[1].Value.ToString() == f.SendWinNum)
                {
                    f.SendWinNum = dataGridView1.Rows[k].Cells[1].Value.ToString();
                    // MessageBox.Show(f.SendWinNum);
                    Load_form(f);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            load_List();
        }
    }
}
