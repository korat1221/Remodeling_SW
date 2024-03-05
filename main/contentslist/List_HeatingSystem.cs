using main.contents;
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
    public partial class List_HeatingSystem : Form
    {
        static String currentID = "";
        static String inEditing = "Add";

        String Num;
        double CountDB;
        int SelectRow;
        DataTable List = new DataTable();
        DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();

        public List_HeatingSystem()
        {
            InitializeComponent();

            string[][] Image = Program.DB.getValue(DB.type.BaseDB_HCneed, "메뉴아이콘", "하위메뉴아이콘", "하위메뉴명 = '난방시스템'");
            Icon_pictureBox.Load(Program.gPath + Image[0][0]);
            Icon_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            Program.DB.initTable(DB.type.ProjDB, "HeatingSystem_Form");
            Create_Table();
        }

        private void GeneralPanel_Paint(object sender, PaintEventArgs e)
        {
            Panel p = (Panel)sender;
            ControlPaint.DrawBorder(e.Graphics, p.DisplayRectangle, Color.FromArgb(153, 180, 209), ButtonBorderStyle.Solid);
        }


        private void Add_button_Click(object sender, EventArgs e)
        {
            Num = Program.UTIL.CreateNum("HeatingSystem_Form", "번호", "HS");

            Program.getMenuForm().ResetForm(19);

            Load_form(Num, "Add");
        }

        public static bool OnLoadProc(Form form)
        {
            HeatingSystem f = (HeatingSystem)form;

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
            Program.getMenuForm().DoLoadForm(19, OnLoadProc);
        }


        public void Create_Table()
        {

            dataGridView1.Columns.Clear();
            checkBoxColumn.HeaderText = "선택";
            checkBoxColumn.Name = "check";
            dataGridView1.Columns.Add(checkBoxColumn);
            List.Columns.Add("번호", typeof(string));
            List.Columns.Add("명칭", typeof(string));
            List.Columns.Add("난방설비", typeof(string));
            List.Columns.Add("난방출력 [kW]", typeof(string));
            List.Columns.Add("난방성능", typeof(string));
            dataGridView1.DataSource = List;

        }

        public void load_List()
        {
            List<object> mainMenu = new List<object>(); // 예시 코드: 메인 메뉴 동적 할당
            string[][] List = Program.DB.getValue(DB.type.ProjDB, "HeatingSystem_Form", "번호,명칭,주요설비,보일러종류,외기히트펌프번호", "");
            if (List.Length > 0)
            {
                String Blank = "";
                this.List.Rows.Clear();
                string[][] SystemValue;
                for (int n = 0; n < List.Length; n++)
                {
                    
                    if (List[n][2] == "보일러") 
                    { 
                        SystemValue = Program.DB.getValue(DB.type.ProjDB, "User_Boiler", "용량,전부하효율", "번호 ='" + List[n][3] + "'");
                        if (SystemValue.Length > 0)
                        {
                            this.List.Rows.Add(List[n][0], List[n][1], List[n][2], Convert.ToDouble(SystemValue[0][0]).ToString("0.0"), Convert.ToDouble(SystemValue[0][1]).ToString("0.0") + " %") ;
                        }
                    }
                    else 
                    { 
                        SystemValue = Program.DB.getValue(DB.type.ProjDB, "User_AirHP", "난방정격용량,난방정격COP", "번호 ='" + List[n][4] + "'");
                        if (SystemValue.Length > 0)
                        {
                            this.List.Rows.Add(List[n][0], List[n][1], List[n][2], Convert.ToDouble(SystemValue[0][0]).ToString("0.0"), Convert.ToDouble(SystemValue[0][1]).ToString("0.0") + " [kW/kW]");
                        }
                    }
                                      
                    mainMenu.Add(new { text = List[n][0] + "." + List[n][1], id = "{\\\"formID\\\":19,\\\"ID\\\":\\\"" + List[n][0] + "\\\"}" }); // 예시 코드: 메인 메뉴 동적 할당
                }
                dataGridView1.DataSource = this.List;
            }
            CountDB = List.Length;
            Program.UTIL.resetMainTree(4, 3, mainMenu.ToArray(), "39"); // 예시 코드: 메인 메뉴 동적 할당
        }

        //선택한 열 색 표시
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


        private void Remove_button_Click(object sender, EventArgs e)
        {
            int k = dataGridView1.CurrentCell.RowIndex;
            if ((MessageBox.Show(dataGridView1.Rows[k].Cells[2].Value.ToString() + "을 삭제 하시겠습니까?", "삭제 확인", MessageBoxButtons.YesNo) == DialogResult.Yes))
            {
                if (k > -1)
                {
                    String Delete_Num = dataGridView1.Rows[k].Cells[1].Value.ToString();
                    Program.DB.deleteValue(DB.type.ProjDB, "HeatingSystem_Form", "번호 ='" + Delete_Num + "'");
                    Program.DB.deleteValue(DB.type.ProjDB, "Heating_ce_Form", "난방시스템 ='" + Delete_Num + "'");
                    Program.DB.deleteValue(DB.type.ProjDB, "HeatingSystem_Result", "난방시스템 ='" + Delete_Num + "'");
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
            Num = Program.UTIL.CreateNum("HeatingSystem_Form", "번호", "HS");
            int k = dataGridView1.CurrentCell.RowIndex;
            if (k > -1)
            {
                String Copy_Num = dataGridView1.Rows[k].Cells[1].Value.ToString();

                Program.DB.CopyValue(DB.type.ProjDB, "HeatingSystem_Form", "번호 ='" + Copy_Num + "'", Num);
                Program.DB.executeSQL(DB.type.ProjDB, "UPDATE  HeatingSystem_Form" + " SET 명칭 = '" + dataGridView1.Rows[k].Cells[2].Value.ToString() + "_복사" + "' WHERE  번호 = '" + Num + "'");
                Load_form(Num, "Copy");

            }
        }

        public void LoadData(String ID)            // 리스트에서 항목 더블 클릭시 - 뷰를 ID 의 getValue 값으로 채우기
        {
            load_List();
        }
    }
}
