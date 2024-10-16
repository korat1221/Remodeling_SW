using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace main.subcontents.RESystem_FC
{
    public partial class FCSList : Form
    {
        public FCSList(string System)
        {
            InitializeComponent();
            string[][] Image = Program.DB.getValue(DB.type.BaseDB_HCneed, "메뉴아이콘", "하위메뉴아이콘", "하위메뉴명 = '연료전지'");
            Icon_pictureBox.Load(Program.gPath + Image[0][0]);
            Icon_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;

            //테이블 작성(축열탱크가 있는 난방또는 급탕설비항목)
            Tablemake();
            if(System == "난방")
            {
                //난방설비리스트
                //string[][] value = 

            }else if(System == "급탕")
            {
                //급탕설비리스트
            }
            
        }

        private void Tablemake()
        {
            new StackedHeaderDecorator(HW_dataGridView, DataGridViewAutoSizeColumnsMode.Fill);
            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
            HW_dataGridView.Columns.Clear();
            checkBoxColumn.HeaderText = "선택";
            checkBoxColumn.Name = "check";
            HW_dataGridView.Columns.Add(checkBoxColumn);
            HW_dataGridView.Columns.Add("A1", "번호"); //난방급탕설비 번호
            HW_dataGridView.Columns.Add("A2", "명칭"); //명칭 이름
            HW_dataGridView.Columns.Add("A3", "축열유무"); //축열유무
            HW_dataGridView.Columns.Add("A4", "축열용량"); //축열용량
        }



        //그리드 디자인

        private void GeneralPanel_Paint(object sender, PaintEventArgs e)
        {
            Panel p = (Panel)sender;
            ControlPaint.DrawBorder(e.Graphics, p.DisplayRectangle, Color.FromArgb(153, 180, 209), ButtonBorderStyle.Solid);
        }
    }
}
