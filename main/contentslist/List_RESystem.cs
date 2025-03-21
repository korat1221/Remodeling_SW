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
            InitializeComponent(); this.Font = new Font(UTIL.Families[0], 9.75F, FontStyle.Regular);

            Icon_pictureBox.Load(Program.gPath + "images/1sticon/6.RESystem_on.png");
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
            

            return true;
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
            List<object> mainMenu = new List<object>();
            List<object> subMenu = new List<object>();
            List<object> pvsubMenu = new List<object>();
            List<object> fuelcellsubMenu = new List<object>();
            List<object> wpsubMenu = new List<object>();
            List<object> stsubMenu = new List<object>();




            string[][] Value = Program.DB.getValue(DB.type.ProjDB, "PV_Form", "번호,명칭", "");
            if(Value.Length >0)
            {
                for (int n = 0; n < Value.Length; n++)
                {
                    pvsubMenu.Add(new { text = Value[n][0] + "." + Value[n][1], id = "{\\\"formID\\\":21,\\\"ID\\\":\\\"" + Value[n][0] + "\\\"}" }); // 예시 코드: 메인 메뉴 동적 할당  
                }
            }
            Value = Program.DB.getValue(DB.type.ProjDB, "FuelCell_Form", "번호,명칭", "");
            if (Value.Length > 0)
            {
                for (int n = 0; n < Value.Length; n++)
                {
                    fuelcellsubMenu.Add(new { text = Value[n][0] + "." + Value[n][1], id = "{\\\"formID\\\":22,\\\"ID\\\":\\\"" + Value[n][0] + "\\\"}" }); // 예시 코드: 메인 메뉴 동적 할당  
                }
            }
            Value = Program.DB.getValue(DB.type.ProjDB, "WindPower_Form", "번호,명칭", "");
            if (Value.Length > 0)
            {
                for (int n = 0; n < Value.Length; n++)
                {
                    wpsubMenu.Add(new { text = Value[n][0] + "." + Value[n][1], id = "{\\\"formID\\\":23,\\\"ID\\\":\\\"" + Value[n][0] + "\\\"}" }); // 예시 코드: 메인 메뉴 동적 할당  
                }
            }
            Value = Program.DB.getValue(DB.type.ProjDB, "SolarTherm_Form", "번호,명칭", "");
            if (Value.Length > 0)
            {
                for (int n = 0; n < Value.Length; n++)
                {
                    stsubMenu.Add(new { text = Value[n][0] + "." + Value[n][1], id = "{\\\"formID\\\":70,\\\"ID\\\":\\\"" + Value[n][0] + "\\\"}" }); // 예시 코드: 메인 메뉴 동적 할당  
                }
            }
            subMenu.Add(new { text = "태양광시스템", id = "{\\\"formID\\\":53,\\\"ID\\\":\\\"SOLAR_1\\\"}", children = pvsubMenu.ToArray() }); // 예시 코드: 메인 메뉴 동적 할당
            subMenu.Add(new { text = "연료전지", id = "{\\\"formID\\\":54,\\\"ID\\\":\\\"SOLAR_2\\\"}", children = fuelcellsubMenu.ToArray() });  // 예시 코드: 메인 메뉴 동적 할당
            subMenu.Add(new { text = "풍력시스템", id = "{\\\"formID\\\":55,\\\"ID\\\":\\\"SOLAR_3\\\"}", children = wpsubMenu.ToArray() });  // 예시 코드: 메인 메뉴 동적 할당
            subMenu.Add(new { text = "태양열시스템", id = "{\\\"formID\\\":69,\\\"ID\\\":\\\"SOLAR_4\\\"}", children = stsubMenu.ToArray() });  // 예시 코드: 메인 메뉴 동적 할당
            subMenu.Add(new { text = "에너지자립률", id = "{\\\"formID\\\":25,\\\"ID\\\":\\\"SOLAR_5\\\"}" }); // 예시 코드: 메인 메뉴 동적 할당
            mainMenu.Add(new { text ="신재생시스템", id = "{\\\"formID\\\":53,\\\"ID\\\":\\\"" + "신재생시스템" + "\\\"}", children = subMenu.ToArray() }); // 예시 코드: 메인 메뉴 동적 할당
            Program.UTIL.resetMainTree(4, 4, subMenu.ToArray(), "43"); // 예시 코드: 메인 메뉴 동적 할당
        }

        public void LoadData(String ID)            // 리스트에서 항목 더블 클릭시 - 뷰를 ID 의 getValue 값으로 채우기
        {
            load_List();
        }


    }
}
