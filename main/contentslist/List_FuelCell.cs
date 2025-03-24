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
    public partial class List_FuelCell : Form
    {
        static String currentID = "";
        static String inEditing = "Add";

        String Num;
        double CountDB;
        int SelectRow;
        // DataTable ListTable = new DataTable();
        DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();

        public List_FuelCell()
        {
            InitializeComponent(); this.Font = new Font(UTIL.Families[0], 9.75F, FontStyle.Regular);

            Icon_pictureBox.Load(Program.gPath + "images/2ndicon/6_2FuelCell.png");
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
            FuelCell f = (FuelCell)form;

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
            Program.getMenuForm().DoLoadForm(22, OnLoadProc);
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
            dataGridView1.Columns.Add("A3", "열.용량[kW]");
            dataGridView1.Columns.Add("A4", "열.효율[%]");
            dataGridView1.Columns.Add("A5", "전기.용량[kW]");
            dataGridView1.Columns.Add("A6", "전기.효율[%]");
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
            string[][] Value = Program.DB.getValue(DB.type.ProjDB, "FuelCell_Form", "번호,명칭,연료전지,설치대수", "");

            if (Value.Length > 0)
            {
                for (int n = 0; n < Value.Length; n++) //연료전지별
                {
                    dataGridView1.Rows.Add();
                    dataGridView1.Rows[n].Cells[1].Value = Value[n][0];
                    dataGridView1.Rows[n].Cells[2].Value = Value[n][1];
                    string[] token_name = Value[n][2].Split('+'); 
                    string[] token_number = Value[n][3].Split('+');
                    double elepower = 0, eleeff=0, heatpower = 0, heateff = 0, in_number = 0;
                    for(int i=0; i < token_name.Length; i++)
                    {
                        string[][] Genvalue = Program.DB.getValue(DB.type.ProjDB, "User_FC", "전기출력,전기효율,열출력,열효율", "번호 = '" + token_name[i] + "'");
                        elepower += Convert.ToDouble(Genvalue[0][0]) * Convert.ToDouble(token_number[i]);
                        eleeff += Convert.ToDouble(Genvalue[0][1]) * Convert.ToDouble(token_number[i]) * Convert.ToDouble(Genvalue[0][0]);
                        heatpower += Convert.ToDouble(Genvalue[0][2]) * Convert.ToDouble(token_number[i]);
                        heateff += Convert.ToDouble(Genvalue[0][3]) * Convert.ToDouble(token_number[i] ) * Convert.ToDouble(Genvalue[0][2]);
                        in_number += Convert.ToDouble(token_number[i]);
                    }
                    eleeff = eleeff / elepower;
                    heateff = heateff / heatpower;
                    
                    dataGridView1.Rows[n].Cells[3].Value = string.Format("{0:F1}",heatpower);
                    dataGridView1.Rows[n].Cells[4].Value = string.Format("{0:F1}",heateff);
                    dataGridView1.Rows[n].Cells[5].Value = string.Format("{0:F1}",elepower);
                    dataGridView1.Rows[n].Cells[6].Value = string.Format("{0:F1}",eleeff);
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

            string[][] Value = Program.DB.getValue(DB.type.ProjDB, "PV_Form", "번호,명칭", "");
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

                Program.UTIL.resetMainTree(5, 3, subMenu.ToArray(), "54"); // 예시 코드: 메인 메뉴 동적 할당

            }
        }

        private void Add_button_Click(object sender, EventArgs e)
        {
            Num = Program.UTIL.CreateNum("FuelCell_Form", "번호", "FC");

            Program.getMenuForm().ResetForm(22);

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
                    Program.DB.deleteValue(DB.type.ProjDB, "FuelCell_Form", "번호 ='" + Delete_Num + "'");
                    load_table();

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
            Num = Program.UTIL.CreateNum("FuelCell_Form", "번호", "FC");
            int k = dataGridView1.CurrentCell.RowIndex;
            if (k > -1)
            {
                String Copy_Num = dataGridView1.Rows[k].Cells[1].Value.ToString();

                Program.DB.CopyValue(DB.type.ProjDB, "FuelCell_Form", "번호 ='" + Copy_Num + "'", Num);
                Program.DB.executeSQL(DB.type.ProjDB, "UPDATE  FuelCell_Form" + " SET 명칭 = '" + dataGridView1.Rows[k].Cells[2].Value.ToString() + "_복사" + "' WHERE  번호 = '" + Num + "'");
                Load_form(Num, "Copy");

            }
        }

        public void LoadData(String ID)            // 리스트에서 항목 더블 클릭시 - 뷰를 ID 의 getValue 값으로 채우기
        {
            load_List();
        }

    }
}
