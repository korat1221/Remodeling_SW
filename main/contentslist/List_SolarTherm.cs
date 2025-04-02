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

        String 프로젝트유형;
        double CountDB;
        
        DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();

      
        public List_SolarTherm()
        {
            InitializeComponent(); this.Font = new Font(UTIL.Families[0], 9.75F, FontStyle.Regular);

            Icon_pictureBox.Load(Program.gPath + "images/2ndicon/10_1SolarTherm.png");
            Icon_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;

            string[][] value = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "프로젝트유형번호");
            if (value.Length > 0)
            {
                프로젝트유형 = value[0][0].ToString();
            }
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
            f.LoadData(currentID);
            return true;
        }

        private void Load_form(String ID, String editing)
        {
            currentID = ID;
            inEditing = editing;
            Program.getMenuForm().DoLoadForm(70, OnLoadProc);
        }

        public void Create_Table()
        {
            new StackedHeaderDecorator(dataGridView1, DataGridViewAutoSizeColumnsMode.Fill, datagridviewDesign);
            dataGridView1.Columns.Clear();
            checkBoxColumn.HeaderText = "선택";
            checkBoxColumn.Name = "check";
            dataGridView1.Columns.Add(checkBoxColumn);
            dataGridView1.Columns.Add("A1", "번호");
            dataGridView1.Columns.Add("A2", "태양열번호");
            dataGridView1.Columns.Add("A3", "설비번호"); //적용 설비번호
            dataGridView1.Columns.Add("A4", "적용설비"); //난방,급탕
            dataGridView1.Columns.Add("A5", "면적.[m"+ Program.UTIL.Subscript(2, true) + "]");
            dataGridView1.Columns.Add("A6", "용량.[kW]");
            dataGridView1.Columns.Add("A7", "열효율.[%]");
            dataGridView1.Columns.Add("A8", "연간생산량.[kWh/년]");
            
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
            string[][] stdb = Program.DB.getValue(DB.type.ProjDB, "SolarTherm_Form", "번호,태양열번호,설비번호,적용설비,모듈개수", "프로젝트유형 = '" + 프로젝트유형 + "'");

            if (stdb.Length > 0)
            {
                for (int i = 0; i < stdb.Length; i++)
                {
                    dataGridView1.Rows.Add();
                    dataGridView1.Rows[i].Cells[1].Value = stdb[i][0];
                    dataGridView1.Rows[i].Cells[2].Value = stdb[i][1];
                    dataGridView1.Rows[i].Cells[3].Value = stdb[i][2];
                    dataGridView1.Rows[i].Cells[4].Value = stdb[i][3];
                    //적용설비를 바탕으로 작성함
                    string[][] solartherm = Program.DB.getValue(DB.type.ProjDB, "User_Solar", "모듈면적,효율,유효열용량", "번호 = '" + stdb[i][1] + "'");

                    dataGridView1.Rows[i].Cells[5].Value = string.Format("{0:N2}", Convert.ToDouble(stdb[i][4]) * Convert.ToDouble(solartherm[0][0]));
                    dataGridView1.Rows[i].Cells[6].Value = string.Format("{0:N2}", Convert.ToDouble(stdb[i][4]) * Convert.ToDouble(solartherm[0][2]));
                    dataGridView1.Rows[i].Cells[7].Value = string.Format("{0:N0}", Convert.ToDouble(solartherm[0][1]) * 100);
                    //연간 생산량 작성

                    if (stdb[i][3] == "난방")
                    {
                        string[][] stherm = Program.DB.getValue(DB.type.ProjDB, "HeatingSystem_Result", "월, Qh_sol", "번호 = '" + stdb[i][2] + "'");
                        double qh = 0;
                        for (int j = 0; j < 12; j++)
                        {
                            qh += Convert.ToDouble(stherm[j][1]);
                        }
                        dataGridView1.Rows[i].Cells[8].Value = string.Format("{0:N0}", qh);
                    }
                    else if (stdb[i][3] == "급탕")
                    {
                        string[][] stherm = Program.DB.getValue(DB.type.ProjDB, "DHWSystem_Result", "월, Qw_sol", "번호 = '" + stdb[i][2] + "'");
                        double qw = 0;
                        for (int j = 0; j < 12; j++)
                        {
                            qw += Convert.ToDouble(stherm[j][1]);
                        }
                        dataGridView1.Rows[i].Cells[8].Value = string.Format("{0:N0}", qw);
                    }
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
