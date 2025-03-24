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
    public partial class List_SolarTherm : Form
    {
        static String currentID = "";
        static String inEditing = "Add";

        String Num;
        double CountDB;
        int SelectRow;
        // DataTable ListTable = new DataTable();
        DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();

        public List_SolarTherm()
        {
            InitializeComponent(); this.Font = new Font(UTIL.Families[0], 9.75F, FontStyle.Regular);

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
            SolarTherm f = (SolarTherm)form;

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
            dataGridView1.Columns.Add("A3", "면적.[m"+ Program.UTIL.Subscript(2, true) + "]");
            dataGridView1.Columns.Add("A4", "용량.[kW]");
            dataGridView1.Columns.Add("A5", "열효율.[%]");
            dataGridView1.Columns.Add("A6", "연간생산량.[kWh/년]");
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
        public void load_table()
        {
            dataGridView1.Rows.Clear();
            string[][] Value = Program.DB.getValue(DB.type.ProjDB, "SolarTherm_Form", "번호,명칭,면적,용량,인버터효율", "");
            if(Value.Length > 0) 
            {
                for(int n=0; n<Value.Length; n++)
                {
                    dataGridView1.Rows.Add();
                    dataGridView1.Rows[n].Cells[1].Value = Value[n][0];
                    dataGridView1.Rows[n].Cells[2].Value = Value[n][1];
                    dataGridView1.Rows[n].Cells[3].Value = Value[n][2];
                    dataGridView1.Rows[n].Cells[4].Value = Value[n][3];
                    dataGridView1.Rows[n].Cells[5].Value = Value[n][4];
                }
            }             
        }

        public void load_List()
        {
            load_table();
            List<object> subMenu = new List<object>();
            List<object> pvsubMenu = new List<object>();
            List<object> fuelcellsubMenu = new List<object>();
            List<object> wpsubMenu = new List<object>();
            List<object> stsubMenu = new List<object>();

            string[][] Value = Program.DB.getValue(DB.type.ProjDB, "SolarTherm_Form", "번호,명칭", "");
            if (Value.Length > 0)
            {
                Value = Program.DB.getValue(DB.type.ProjDB, "PV_Form", "번호,명칭", "");
                if (Value.Length > 0)
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
                subMenu.Add(new { text = "공급의무비율", id = "{\\\"formID\\\":24,\\\"ID\\\":\\\"SOLAR_5\\\"}" }); // 예시 코드: 메인 메뉴 동적 할당
                subMenu.Add(new { text = "에너지자립률", id = "{\\\"formID\\\":25,\\\"ID\\\":\\\"SOLAR_6\\\"}" }); // 예시 코드: 메인 메뉴 동적 할당
                Program.UTIL.resetMainTree(5, 3, subMenu.ToArray(), "69"); // 예시 코드: 메인 메뉴 동적 할당
            }
        }

    
        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            if(dataGridView1.Rows.Count > 0)
            {
                if (dataGridView1.CurrentCell != null)
                {
                    int k = dataGridView1.CurrentCell.RowIndex;

                    if (k >= 0)
                    {
                        var cellValue = dataGridView1.Rows[k].Cells[1].Value;
                        if (cellValue != null)
                        {
                            Load_form(dataGridView1.Rows[k].Cells[1].Value.ToString(), "Edit");
                        }
                        else
                        {
                            MessageBox.Show("선택한 셀에 값이 없습니다.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("잘못된 행 인덱스 입니다.");
                    }
                }
                else
                {
                    MessageBox.Show("선택한 셀에 값이 없습니다.");
                }
            }
            else
            {
                MessageBox.Show("태양열시스템이 설치되지 않았습니다.");
            }
        }
        
        public void LoadData(String ID)   // 리스트에서 항목 더블 클릭시 - 뷰를 ID 의 getValue 값으로 채우기
        {
            load_List();
        }

       
    }
}
