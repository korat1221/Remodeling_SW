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
    public partial class List_RESystem : Form
    {
        static String currentID = "";
        static String inEditing = "Add";

        double CountDB;
        int SelectRow;
       // DataTable ListTable = new DataTable();
        DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();

        public List_RESystem()
        {
            InitializeComponent();

            Icon_pictureBox.Load(Program.gPath + "images/1sticon/6.RESystem_on.png");
            Icon_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            Create_Table();
        }

        private void GeneralPanel_Paint(object sender, PaintEventArgs e)
        {
            Panel p = (Panel)sender;
            ControlPaint.DrawBorder(e.Graphics, p.DisplayRectangle, Color.FromArgb(153, 180, 209), ButtonBorderStyle.Solid);
        }

        public static bool OnLoadProc(Form form)
        {
            List_Zone f = (List_Zone)form;

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
            dataGridView1.Columns.Add("A1", "유형");
            dataGridView1.Columns.Add("A2", "용량.[kW]");
            dataGridView1.Columns.Add("A3", "효율.[%]");
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
            List<object> subMenu = new List<object>();

            subMenu.Add(new { text = "태양광시스템", id = "{\\\"formID\\\":21,\\\"ID\\\":\\\"SOLAR_1\\\"}" }); // 예시 코드: 메인 메뉴 동적 할당
            subMenu.Add(new { text = "연료전지", id = "{\\\"formID\\\":22,\\\"ID\\\":\\\"SOLAR_2\\\"}" }); // 예시 코드: 메인 메뉴 동적 할당
            subMenu.Add(new { text = "풍력시스템", id = "{\\\"formID\\\":23,\\\"ID\\\":\\\"SOLAR_3\\\"}" }); // 예시 코드: 메인 메뉴 동적 할당
            subMenu.Add(new { text = "공급의무비율", id = "{\\\"formID\\\":24,\\\"ID\\\":\\\"SOLAR_4\\\"}" }); // 예시 코드: 메인 메뉴 동적 할당
            subMenu.Add(new { text = "에너지자립률", id = "{\\\"formID\\\":25,\\\"ID\\\":\\\"SOLAR_5\\\"}" }); // 예시 코드: 메인 메뉴 동적 할당

           Program.UTIL.resetMainTree(4, 5, subMenu.ToArray(), "43"); // 예시 코드: 메인 메뉴 동적 할당
        }


        private void Remove_button_Click(object sender, EventArgs e)
        {
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
        }

        public void LoadData(String ID)            // 리스트에서 항목 더블 클릭시 - 뷰를 ID 의 getValue 값으로 채우기
        {
            load_List();
        }
       

    }
}
