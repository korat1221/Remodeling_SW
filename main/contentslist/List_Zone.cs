using main.contents;
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
    public partial class List_Zone : Form
    {
        static String currentID = "";
        static String inEditing = "Add";

        String Num;
        double CountDB;
        int SelectRow;
        //DataTable ListTable = new DataTable();
        DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();

        public List_Zone()
        {
            InitializeComponent(); this.Font = new Font(UTIL.Families[0], 9.75F, FontStyle.Regular);

            Icon_pictureBox.Load(Program.gPath + "images/1sticon/4.Zone_on3.png");
            Icon_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            Create_Table();

        }

        private void GeneralPanel_Paint(object sender, PaintEventArgs e)
        {
            Panel p = (Panel)sender;
            ControlPaint.DrawBorder(e.Graphics, p.DisplayRectangle, System.Drawing.SystemColors.Control, ButtonBorderStyle.Solid);
        }


        private void Add_button_Click(object sender, EventArgs e)
        {
        }

        public static bool OnLoadProc(Form form)
        {
            ZoneGeneral f = (ZoneGeneral)form;

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
            Program.getMenuForm().DoLoadForm(12, OnLoadProc);
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
            dataGridView1.Columns.Add("A3", "용도프로필");
            dataGridView1.Columns.Add("A4", "순바닥면적.[m"+ Program.UTIL.Subscript(2, true) + "]");
            dataGridView1.Columns.Add("A5", "천장고.[m]");
            dataGridView1.Columns[0].Width = 40;
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
        public void load_List(String Num)
        {
            string[][] List = Program.DB.querySQL(DB.type.ProjDB, "Select Distinct 존 From ZoneEnvelope_3D where 층 ='" + Num + "' Order by 존 ");
            String[][] Value;
            String Blank = "";
            //ListTable.Rows.Clear();
            dataGridView1.Rows.Clear();
            if (List.Length > 0)
            {
                for (int n = 0; n < List.Length; n++)
                {
                    Value = Program.DB.querySQL(DB.type.ProjDB, "select a.존번호, a.존이름, a.용도프로필, a.순바닥면적, a.천장고 FROM ZoneGeneral_Form AS a INNER JOIN ZoneEnvelope_3D AS b ON a.존번호 = b.존 where b.층 = '" + Num + "' AND b.존 = '" + List[n][0] + "'");
                    if (Value.Length > 0)
                    {
                        dataGridView1.Rows.Add();
                        int nRow = dataGridView1.Rows.Count - 1;
                        dataGridView1.Rows[nRow].Cells[1].Value = List[n][0];
                        dataGridView1.Rows[nRow].Cells[2].Value = Value[0][1];
                        dataGridView1.Rows[nRow].Cells[3].Value = Value[0][2];
                        if (Value[0][3] != "" || Value[0][4] != "")
                        {
                         dataGridView1.Rows[nRow].Cells[4].Value = String.Format("{0:F1}", Convert.ToDouble(Value[0][3]));
                         dataGridView1.Rows[nRow].Cells[5].Value = String.Format("{0:F1}", Convert.ToDouble(Value[0][4]));
                        } 
                    }
                    else
                    {
                        dataGridView1.Rows.Add();
                        int nRow = dataGridView1.Rows.Count - 1;
                        dataGridView1.Rows[nRow].Cells[1].Value = List[n][0];
                        dataGridView1.Rows[nRow].Cells[2].Value = null;
                        dataGridView1.Rows[nRow].Cells[3].Value = null;
                        dataGridView1.Rows[nRow].Cells[4].Value = null;
                        dataGridView1.Rows[nRow].Cells[5].Value = null;
                    }
                }
            }
            CountDB = List.Length;
        }

        private void Remove_button_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
        }

        private void Copy_button_Click(object sender, EventArgs e)
        {
        }

        public void LoadData(String ID)            // 리스트에서 항목 더블 클릭시 - 뷰를 ID 의 getValue 값으로 채우기
        {           
            Num_textBox.Text = ID + " 정보";
            Num = ID;
            load_List(Num);
        }
        public void ResetForm(String ID) // 리스트에서 추가 버튼 클릭시 - 뷰 초기화
        {
            Num_textBox.Text = ID + " 정보";
            Num = ID;
        }
    }
}
