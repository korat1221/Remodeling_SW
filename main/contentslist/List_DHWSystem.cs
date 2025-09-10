using main.contents;
using main.info;
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
    public partial class List_DHWSystem : Form
    {
        static String currentID = "";
        static String inEditing = "Add";

        String Num;
        double CountDB;
        int SelectRow;
        DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();

        public List_DHWSystem()
        {
            InitializeComponent(); this.Font = new Font(UTIL.Families[0], 9.75F, FontStyle.Regular);

            string[][] Image = Program.DB.getValue(DB.type.BaseDB_HCneed, "메뉴아이콘", "하위메뉴아이콘", "하위메뉴명 = '급탕시스템'");
            if (Image.Length > 0)
            {
                Icon_pictureBox.Load(Program.gPath + Image[0][0]);
                Icon_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            }
            Program.DB.initTable(DB.type.ProjDB, "DHWSystem_Form");
            Create_Table();
        }

        private void GeneralPanel_Paint(object sender, PaintEventArgs e)
        {
            Panel p = (Panel)sender;
            ControlPaint.DrawBorder(e.Graphics, p.DisplayRectangle, System.Drawing.SystemColors.Control, ButtonBorderStyle.Solid);
        }


        private void Add_button_Click(object sender, EventArgs e)
        {
            Num = Program.UTIL.CreateNum("DHWSystem_Form", "번호", "DW");

            Program.getMenuForm().ResetForm(18);

            Load_form(Num, "Add");
        }

        public static bool OnLoadProc(Form form)
        {
            DHWSystem f = (DHWSystem)form;

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
            Program.getMenuForm().DoLoadForm(18, OnLoadProc);
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
        public void Create_Table()
        {
            new StackedHeaderDecorator(dataGridView1, DataGridViewAutoSizeColumnsMode.Fill, datagridviewDesign);
            dataGridView1.Columns.Clear();
            checkBoxColumn.HeaderText = "선택";
            checkBoxColumn.Name = "check";
            dataGridView1.Columns.Add(checkBoxColumn);
            dataGridView1.Columns.Add("A1", "번호");
            dataGridView1.Columns.Add("A2", "명칭");
            dataGridView1.Columns.Add("A3", "주요설비");
            dataGridView1.Columns.Add("A4", "출력.[kW]");
            dataGridView1.Columns.Add("A5", "성능");
            dataGridView1.Columns[0].Width = 40;

        }

        public void load_List()
        {
            List<object> mainMenu = new List<object>(); // 예시 코드: 메인 메뉴 동적 할당
            string[][] List = Program.DB.getValue(DB.type.ProjDB, "DHWSystem_Form", "번호,명칭,주요설비,보일러종류,태양열번호,히트펌프번호,지역난방번호", "");
            if (List.Length > 0)
            {
                String Blank = "";
                dataGridView1.Rows.Clear();
                string[][] SystemValue; string[][] num;
                for (int n = 0; n < List.Length; n++)
                {
                    dataGridView1.Rows.Add();
                    int nRow = dataGridView1.Rows.Count - 1;
                    if (List[n][2] == "보일러")
                    {
                        num = Program.DB.getValue(DB.type.ProjDB, "DHWSystem_Form", "보일러대수", "번호='" + List[n][0] + "'");
                        SystemValue = Program.DB.getValue(DB.type.ProjDB, "User_Boiler", "용량,전부하효율", "번호 ='" + List[n][3] + "'");
                        if (num.Length > 0 && SystemValue.Length > 0)
                        {
                            dataGridView1.Rows[nRow].Cells[4].Value = (Convert.ToDouble(num[0][0]) * Convert.ToDouble(SystemValue[0][0])).ToString("0.0");
                            dataGridView1.Rows[nRow].Cells[5].Value = Convert.ToDouble(SystemValue[0][1]).ToString("0.0") + " %";
                        }
                    }
                    else if (List[n][2] == "태양열시스템")
                    {
                        num = Program.DB.getValue(DB.type.ProjDB, "DHWSystem_Form", "모듈개수", "번호='" + List[n][0] + "'");
                        SystemValue = Program.DB.getValue(DB.type.ProjDB, "User_Solar", "모듈면적,효율", "번호 ='" + List[n][4] + "'");
                        if (num.Length > 0 && SystemValue.Length > 0)
                        {
                            dataGridView1.Rows[nRow].Cells[4].Value = (Convert.ToDouble(num[0][0]) * Convert.ToDouble(SystemValue[0][0])).ToString("0.0") + "m2";
                            dataGridView1.Rows[nRow].Cells[5].Value = Convert.ToDouble(SystemValue[0][1]).ToString("0.0") + " %";
                        }
                    }
                    else if (List[n][2] == "외기 히트펌프")
                    {
                        num = Program.DB.getValue(DB.type.ProjDB, "DHWSystem_Form", "히트펌프대수", "번호='" + List[n][0] + "'");
                        SystemValue = Program.DB.getValue(DB.type.ProjDB, "User_DHWHP", "급탕정격용량,급탕정격COP", "번호 ='" + List[n][5] + "'");
                        if (num.Length > 0 && SystemValue.Length > 0)
                        {
                            dataGridView1.Rows[nRow].Cells[4].Value = (Convert.ToDouble(num[0][0]) * Convert.ToDouble(SystemValue[0][0])).ToString("0.0");
                            dataGridView1.Rows[nRow].Cells[5].Value = Convert.ToDouble(SystemValue[0][1]).ToString("0.0");
                        }
                    }
                    else if (List[n][2] == "지역난방")
                    {
                        SystemValue = Program.DB.getValue(DB.type.ProjDB, "User_DH", "용량", "번호 ='" + List[n][6] + "'");
                        if (SystemValue.Length > 0)
                        {
                            dataGridView1.Rows[nRow].Cells[4].Value = Convert.ToDouble(SystemValue[0][0]).ToString("0.0");
                            dataGridView1.Rows[nRow].Cells[5].Value = "-";
                        }
                    }
                    dataGridView1.Rows[nRow].Cells[1].Value = List[n][0];
                    dataGridView1.Rows[nRow].Cells[2].Value = List[n][1];
                    dataGridView1.Rows[nRow].Cells[3].Value = List[n][2];

                    mainMenu.Add(new { text = List[n][0] + "." + List[n][1], id = "{\\\"formID\\\":18,\\\"ID\\\":\\\"" + List[n][0] + "\\\"}" }); // 예시 코드: 메인 메뉴 동적 할당
                }
            }
            else
            {
                dataGridView1.Rows.Clear();
            }
            CountDB = List.Length;
            Program.UTIL.resetMainTree(5, 0, mainMenu.ToArray(), "49"); // 예시 코드: 메인 메뉴 동적 할당
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
                    Program.DB.deleteValue(DB.type.ProjDB, "DHWSystem_Form", "번호 ='" + Delete_Num + "'");
                    Program.DB.deleteValue(DB.type.ProjDB, "DHWSystem_Result", "번호 ='" + Delete_Num + "'");
                    Program.DB.deleteValue(DB.type.ProjDB, "FC_Form", "설비번호 ='" + Delete_Num + "'");
                    Program.DB.deleteValue(DB.type.ProjDB, "SolarTherm_Form", "설비번호 ='" + Delete_Num + "'");
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
            Num = Program.UTIL.CreateNum("DHWSystem_Form", "번호", "DW");
            int k = dataGridView1.CurrentCell.RowIndex;
            if (k > -1)
            {
                String Copy_Num = dataGridView1.Rows[k].Cells[1].Value.ToString();

                Program.DB.CopyValue(DB.type.ProjDB, "DHWSystem_Form", "번호 ='" + Copy_Num + "'", Num);
                Program.DB.executeSQL(DB.type.ProjDB, "UPDATE  DHWSystem_Form" + " SET 명칭 = '" + dataGridView1.Rows[k].Cells[2].Value.ToString() + "_복사" + "' WHERE  번호 = '" + Num + "'");
                Program.DB.saveProject();
                Load_form(Num, "Copy");

            }
        }

        public void LoadData(String ID)            // 리스트에서 항목 더블 클릭시 - 뷰를 ID 의 getValue 값으로 채우기
        {
            load_List();
        }

        private void infoListDHW_Click(object sender, EventArgs e)
        {
            string basePath = Program.gPath + "Manual\\3.contentslist\\11.DHW";

            // 경로가 존재하는지 확인
            if (Directory.Exists(basePath))
            {
                SlideViewer slideViewer = new SlideViewer(basePath);
                slideViewer.Show();
            }
            else
            {
                MessageBox.Show("The folder path does not exist.");
            }
        }
    }
}
