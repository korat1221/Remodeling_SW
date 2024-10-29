using main.contents;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace main.contentslist
{
    public partial class List_PV : Form
    {
        static String currentID = "";
        static String inEditing = "Add";

        String Num;
        double CountDB;
        int SelectRow;
        // DataTable ListTable = new DataTable();
        DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();

        public List_PV()
        {
            InitializeComponent(); this.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular);

            Icon_pictureBox.Load(Program.gPath + "images/2ndicon/6_1PVSystem.png");
            Icon_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            Create_Table();
        }

        private void GeneralPanel_Paint(object sender, PaintEventArgs e)
        {
            Panel p = (Panel)sender;
            ControlPaint.DrawBorder(e.Graphics, p.DisplayRectangle, System.Drawing.SystemColors.Control, ButtonBorderStyle.Solid);
        }

        public static bool OnLoadProc(Form form)
        {
            PV f = (PV)form;

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
            Program.getMenuForm().DoLoadForm(21, OnLoadProc);
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
            dataGridView1.Columns.Add("A3", "면적.[m²]");
            dataGridView1.Columns.Add("A4", "용량.[kW]");
            dataGridView1.Columns.Add("A5", "효율 Kpk.[-]");
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
        public void load_List()
        {
            dataGridView1.Rows.Clear();
            string[][] Value = Program.DB.getValue(DB.type.ProjDB, "PV_Form", "번호,명칭,면적,용량,모듈번호", "");
           if(Value.Length > 0) 
            {
                for(int n=0; n<Value.Length; n++)
                {
                    dataGridView1.Rows.Add();
                    dataGridView1.Rows[n].Cells[1].Value = Value[n][0];
                    dataGridView1.Rows[n].Cells[2].Value = Value[n][1];
                    dataGridView1.Rows[n].Cells[3].Value = Convert.ToDouble(Value[n][2]).ToString("0.0");
                    dataGridView1.Rows[n].Cells[4].Value = Convert.ToDouble(Value[n][3]).ToString("0.0");
                    string[][] value2 = Program.DB.getValue(DB.type.ProjDB, "User_PVModule", "Kpk", "번호='"+ Value[n][4] + "'");
                    if(value2.Length > 0)
                    {
                        dataGridView1.Rows[n].Cells[5].Value = Convert.ToDouble(value2[0][0]).ToString("0.00");
                    }
                }
            }
             
        }
        private void Add_button_Click(object sender, EventArgs e)
        {
            Num = Program.UTIL.CreateNum("PV_Form", "번호", "PV");

            Program.getMenuForm().ResetForm(21);

            Load_form(Num, "Add");
        }

        private void Remove_button_Click(object sender, EventArgs e)
        {
            int k = dataGridView1.CurrentCell.RowIndex;
            if ((MessageBox.Show(dataGridView1.Rows[k].Cells[2].Value.ToString() + "을 삭제 하시겠습니까?", "삭제 확인", MessageBoxButtons.YesNo) == DialogResult.Yes))
            {
                if (k > -1)
                {
                    String Delete_Num = dataGridView1.Rows[k].Cells[1].Value.ToString();
                    Program.DB.deleteValue(DB.type.ProjDB, "PV_Form", "번호 ='" + Delete_Num + "'");
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
            Num = Program.UTIL.CreateNum("PV_Form", "번호", "PV");
            int k = dataGridView1.CurrentCell.RowIndex;
            if (k > -1)
            {
                String Copy_Num = dataGridView1.Rows[k].Cells[1].Value.ToString();

                Program.DB.CopyValue(DB.type.ProjDB, "PV_Form", "번호 ='" + Copy_Num + "'", Num);
                Program.DB.executeSQL(DB.type.ProjDB, "UPDATE  PV_Form" + " SET 명칭 = '" + dataGridView1.Rows[k].Cells[2].Value.ToString() + "_복사" + "' WHERE  번호 = '" + Num + "'");
                Load_form(Num, "Copy");

            }
        }

        public void LoadData(String ID)            // 리스트에서 항목 더블 클릭시 - 뷰를 ID 의 getValue 값으로 채우기
        {
            load_List();
        }

       
    }
}
