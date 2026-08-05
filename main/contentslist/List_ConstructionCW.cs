using main.contents;
using main.info;
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

namespace main.contentslist
{
    public partial class List_ConstructionCW : Form
    {
        static String currentID = "";
        static String inEditing = "Add";

        String CWNum;
        double CountDB;
        int SelectRow;
        // DataTable CWList = new DataTable();
        DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();

        public List_ConstructionCW()
        {
            InitializeComponent(); this.Font = new Font(UTIL.Families[0], 9.75F, FontStyle.Regular);

            string[][] Image = Program.DB.getValue(DB.type.BaseDB_HCneed, "메뉴아이콘", "하위메뉴아이콘", "하위메뉴명 = '커튼월창'");
            if (Image.Length > 0)
            {
                Icon_pictureBox.Load(Program.gPath + Image[0][0]);
                Icon_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            }
            Program.DB.initTable(DB.type.ProjDB, "ConstructionCW");
            Create_Table();
        }

        private void GeneralPanel_Paint(object sender, PaintEventArgs e)
        {
            Panel p = (Panel)sender;
            ControlPaint.DrawBorder(e.Graphics, p.DisplayRectangle, System.Drawing.SystemColors.Control, ButtonBorderStyle.Solid);
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
            new StackedHeaderDecorator(dataGridView1, DataGridViewAutoSizeColumnsMode.Fill, datagridviewDesign);
            dataGridView1.Columns.Clear();
            checkBoxColumn.HeaderText = "선택";
            checkBoxColumn.Name = "check";
            dataGridView1.Columns.Add(checkBoxColumn);

            dataGridView1.Columns.Add("A1", "번호");
            dataGridView1.Columns.Add("A2", "명칭");
            dataGridView1.Columns.Add("A3", "Type");
            dataGridView1.Columns.Add("A4", "유효열관류율.[W/m" + Program.UTIL.Subscript(2, true) + "·K]");
            dataGridView1.Columns.Add("A5", "태양열취득률.[-]");
            dataGridView1.Columns.Add("A6", "빛투과율.[-]");
            dataGridView1.Columns.Add("A7", "유리종류");
            dataGridView1.Columns.Add("A8", "면적.[m" + Program.UTIL.Subscript(2, true) + "]");
            dataGridView1.Columns.Add("A9", "개수.[EA]");
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
            else
            {
                cell.Style.BackColor = Color.FromArgb(255, 255, 255);
                cell.Style.ForeColor = Color.Black;
                cell.Style.SelectionBackColor = Color.FromArgb(255, 255, 255);
                cell.Style.SelectionForeColor = Color.Black;
                return true;
            }
        }

        public void load_List()
        {
            List<object> mainMenu = new List<object>(); // 예시 코드: 메인 메뉴 동적 할당
            string[][] List = Program.DB.getValue(DB.type.ProjDB, "ConstructionCW", "번호,명칭,Type,커튼월창유효열관류율,태양열취득률,빛투과율,커튼월면적,고정유리종류", "");
            if (List.Length > 0)
            {
                String Blank = "";
                dataGridView1.Rows.Clear();
                for (int n = 0; n < List.Length; n++)
                {
                    dataGridView1.Rows.Add();
                    int nRow = dataGridView1.Rows.Count - 1;
                    dataGridView1.Rows[nRow].Cells[1].Value = List[n][0];
                    dataGridView1.Rows[nRow].Cells[2].Value = List[n][1];
                    dataGridView1.Rows[nRow].Cells[3].Value = List[n][2];
                    dataGridView1.Rows[nRow].Cells[4].Value = String.Format("{0:F2}", Program.UTIL.ToDoubleOrZero(List[n][3]));
                    dataGridView1.Rows[nRow].Cells[5].Value = String.Format("{0:F2}", Program.UTIL.ToDoubleOrZero(List[n][4]));
                    dataGridView1.Rows[nRow].Cells[6].Value = String.Format("{0:F2}", Program.UTIL.ToDoubleOrZero(List[n][5]));
                    dataGridView1.Rows[nRow].Cells[7].Value = List[n][7];
                    dataGridView1.Rows[nRow].Cells[8].Value = String.Format("{0:F2}", Program.UTIL.ToDoubleOrZero(List[n][6]));
                    string[][] Area = Program.DB.getValue(DB.type.ProjDB, "ZoneEnvelope_3D", "면적", "구조체번호='" + List[n][0] + "'");
                    if (Area.Length > 0)
                    {
                        dataGridView1.Rows[nRow].Cells[9].Value = Area.Length;
                    }
                    mainMenu.Add(new { text = List[n][0] + "." + List[n][1], id = "{\\\"formID\\\":2,\\\"ID\\\":\\\"" + List[n][0] + "\\\"}" }); // 예시 코드: 메인 메뉴 동적 할당
                }
            }
            //dataGridView1.DataSource = CWList;
            CountDB = List.Length;
            Program.UTIL.resetMainTree(1, 0, mainMenu.ToArray(), "30"); // 예시 코드: 메인 메뉴 동적 할당
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
                Program.DB.saveProject();
                Load_form(CWNum, "Copy");

            }
        }

        public void LoadData(String ID)            // 리스트에서 항목 더블 클릭시 - 뷰를 ID 의 getValue 값으로 채우기
        {
            load_List();
        }

        private void info_Click(object sender, EventArgs e)
        {

            string basePath = Program.gPath + "Manual\\3.contentslist\\7.CW";

            // 경로가 존재하는지 확인
            if (Directory.Exists(basePath))
            {
                SlideViewer slideViewer = new SlideViewer(basePath);
                slideViewer.Show();
            }
            else
            {
                MessageBox.Show("The folder path does not exist.");
            }
        }
    }
}
