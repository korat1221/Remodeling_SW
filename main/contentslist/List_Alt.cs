using main.contents;
using main.contents.Alt;
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
    public partial class List_Alt : Form
    {
        static String currentID = "";
        static String inEditing = "Add";

        String Num;
        double CountDB;
        int SelectRow;
        //DataTable WallList = new DataTable();
        DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();

        public List_Alt()
        {
            InitializeComponent(); this.Font = new Font(UTIL.Families[0], 9.75F, FontStyle.Regular);

            string[][] Image = Program.DB.getValue(DB.type.BaseDB_HCneed, "메뉴아이콘", "하위메뉴아이콘", "하위메뉴명 = '외벽'");
            if (Image.Length > 0)
            {
                Icon_pictureBox.Load(Program.gPath + "images/1sticon/8.Remodeling_2.png");
                Icon_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            }
            Create_Table();
        }

        private void GeneralPanel_Paint(object sender, PaintEventArgs e)
        {
            Panel p = (Panel)sender;
            ControlPaint.DrawBorder(e.Graphics, p.DisplayRectangle, System.Drawing.SystemColors.Control, ButtonBorderStyle.Solid);
        }


        private void Add_button_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Add();
            Num = Program.UTIL.CreateNum("Optimal_Form", "번호", "Alt");

            Program.getMenuForm().ResetForm(59);

            Load_form(Num, "Add");
        }

        public static bool OnLoadProc(Form form)
        {
            AltMain  f = (AltMain)form;

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
            Program.getMenuForm().DoLoadForm(59, OnLoadProc);
        }


        public void Create_Table()
        {
            new StackedHeaderDecorator(dataGridView1, DataGridViewAutoSizeColumnsMode.Fill, datagridviewDesign);
            dataGridView1.Columns.Clear();
            checkBoxColumn.HeaderText = "선택";
            checkBoxColumn.Name = "check";
            dataGridView1.Columns.Add(checkBoxColumn);
            dataGridView1.Columns.Add("A1", "번호");
            dataGridView1.Columns.Add("A2", "적용 요소기술");
            dataGridView1.Columns.Add("A3", "에너지절감량.[kWh]");
            dataGridView1.Columns.Add("A4", "예상 순공사비.[원]");
            dataGridView1.Columns.Add("A5", "점수.에너지");
            dataGridView1.Columns.Add("A6", "점수.법규");
            dataGridView1.Columns.Add("A7", "점수.쾌적성");
            dataGridView1.Columns.Add("A8", "점수.경제성");
            dataGridView1.Columns.Add("A9", "점수.종합점수");
            dataGridView1.Columns[0].Width = 40;
            dataGridView1.Columns[1].Width = 60;
            dataGridView1.Columns[2].Width = 250;
            dataGridView1.Columns[5].Width = 40;
            dataGridView1.Columns[6].Width = 40;
            dataGridView1.Columns[7].Width = 40;
            dataGridView1.Columns[8].Width = 40;
            dataGridView1.Columns[9].Width = 60;
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
        }

        private void Remove_button_Click(object sender, EventArgs e)
        {
            int k = dataGridView1.CurrentCell.RowIndex;
            if ((MessageBox.Show(dataGridView1.Rows[k].Cells[2].Value.ToString() + "을 삭제 하시겠습니까?", "삭제 확인", MessageBoxButtons.YesNo) == DialogResult.Yes))
            {
                if (k > -1)
                {
                    String Delete_Num = dataGridView1.Rows[k].Cells[1].Value.ToString();
                    Program.DB.deleteValue(DB.type.ProjDB, "ConstructionWall", "번호 ='" + Delete_Num + "'");
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
            Num = Program.UTIL.CreateNum("Optimal_Form", "번호", "Alt");
            int k = dataGridView1.CurrentCell.RowIndex;
            if (k > -1)
            {
                String Copy_Num = dataGridView1.Rows[k].Cells[1].Value.ToString();

                Program.DB.CopyValue(DB.type.ProjDB, "Optimal_Form", "번호 ='" + Copy_Num + "'", Num);
                Program.DB.executeSQL(DB.type.ProjDB, "UPDATE  Optimal_Form" + " SET 명칭 = '" + dataGridView1.Rows[k].Cells[2].Value.ToString() + "_복사" + "' WHERE  번호 = '" + Num + "'");
                Load_form(Num, "Copy");

            }
        }

        public void LoadData(String ID)            // 리스트에서 항목 더블 클릭시 - 뷰를 ID 의 getValue 값으로 채우기
        {
            load_List();
        }
    }
}
