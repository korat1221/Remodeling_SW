using main.contents;
using main.subcontents;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.ComponentModel.Design.ObjectSelectorEditor;

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

            Num = Num + 1;
            if (Num < 10)
            {
                WinNum = "Win0" + Num;
            }
            else
            {
                WinNum = "Win" + Num;
            }

            ConstructionWindow constructionwindow = new ConstructionWindow();
            constructionwindow.SendWinNum = WinNum;
            DialogResult result = constructionwindow.ShowDialog();
            if (result == DialogResult.OK)
            {
                load_List();
            }

        }
        void Create_Table()
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

        void load_List()
        {
            string[][] List = Program.DB.getValue(DB.type.CalcDB, "ConstructionWindow", "번호,창호명칭,Type,창호유효열관류율,태양열취득률,빛투과율,창호면적,유리종류", "");

            WindowList.Rows.Clear();
            for (int n = 0; n < List.Length; n++)
            {

                WindowList.Rows.Add(List[n][0], List[n][1], List[n][2], String.Format("{0:F2}", Convert.ToDouble(List[n][3])), String.Format("{0:F2}", Convert.ToDouble(List[n][4])), String.Format("{0:F2}", Convert.ToDouble(List[n][5])), String.Format("{0:F2}", Convert.ToDouble(List[n][6])), List[n][7]);
            }
            dataGridView1.DataSource = WindowList;
            CountDB = List.Length;
        }
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
    }
}
