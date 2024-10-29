using Eagle._Containers.Public;
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
    public partial class List_AHUSystem : Form
    {
        static String currentID = "";
        static String inEditing = "Add";

        String Num;
        double CountDB;
        int SelectRow;
        DataTable List = new DataTable();
        DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();

        public List_AHUSystem()
        {
            InitializeComponent(); this.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular);

            string[][] Image = Program.DB.getValue(DB.type.BaseDB_HCneed, "메뉴아이콘", "하위메뉴아이콘", "하위메뉴명 = '공조시스템'");
            Icon_pictureBox.Load(Program.gPath + Image[0][0]);
            Icon_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            Program.DB.initTable(DB.type.ProjDB, "AHUSystem_Form");
            Create_Table();
        }

        private void GeneralPanel_Paint(object sender, PaintEventArgs e)
        {
            Panel p = (Panel)sender;
            ControlPaint.DrawBorder(e.Graphics, p.DisplayRectangle, System.Drawing.SystemColors.Control, ButtonBorderStyle.Solid);
        }


        private void Add_button_Click(object sender, EventArgs e)
        {
            Program.getMenuForm().ResetForm(17);
            Load_form("", "Add");
        }

        public static bool OnLoadProc(Form form)
        {
            AHUSystem f = (AHUSystem)form;

            if (inEditing == "Edit")
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
            Program.getMenuForm().DoLoadForm(17, OnLoadProc);
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
            dataGridView1.Columns.Add("A3", "유형");
            dataGridView1.Columns.Add("A4", "풍량.급기.[m3/h]");
            dataGridView1.Columns.Add("A5", "온도교환효율.난방.[%]");
            dataGridView1.Columns.Add("A6", "온도교환효율.냉방.[%]");
            dataGridView1.Columns.Add("A7", "코일용량.냉방.[kW]");
            dataGridView1.Columns.Add("A8", "코일용량.난방.[kW]");
            dataGridView1.Columns[0].Width = 40;
            dataGridView1.Columns[1].Width = 50;

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

            List<object> mainMenu = new List<object>(); // 예시 코드: 메인 메뉴 동적 할당
            string[][] List = Program.DB.getValue(DB.type.ProjDB, "AHUSystem_Form", "번호,명칭,유형", "");
            string[][] Value;
            if (List.Length > 0)
            {
                String Blank = "";
                dataGridView1.Rows.Clear();
                for (int n = 0; n < List.Length; n++)
                {
                    int nRow = dataGridView1.Rows.Add();
                    dataGridView1.Rows[nRow].Cells[1].Value = List[n][0];
                    dataGridView1.Rows[nRow].Cells[2].Value = List[n][1];
                    dataGridView1.Rows[nRow].Cells[3].Value = List[n][2];

                    if (List[n][2] == "공조기")
                    {
                        Value = Program.DB.getValue(DB.type.ProjDB, "User_AHU", "급기풍량,온도교환효율_난방,온도교환효율_냉방,난방코일출력,냉각코일출력,명칭", "번호='" + List[n][0] + "'");
                        if (Value.Length > 0)
                        {
                            dataGridView1.Rows[nRow].Cells[4].Value = String.Format("{0:F0}", Convert.ToDouble(Value[0][0]));
                            dataGridView1.Rows[nRow].Cells[5].Value = String.Format("{0:F0}", Convert.ToDouble(Value[0][1]));
                            dataGridView1.Rows[nRow].Cells[6].Value = String.Format("{0:F0}", Convert.ToDouble(Value[0][2]));
                            dataGridView1.Rows[nRow].Cells[7].Value = String.Format("{0:F0}", Convert.ToDouble(Value[0][3]));
                            dataGridView1.Rows[nRow].Cells[8].Value = String.Format("{0:F0}", Convert.ToDouble(Value[0][4]));
                        }
                    }
                    else
                    {
                        Value = Program.DB.getValue(DB.type.ProjDB, "User_HRV", "팬풍량,온도교환효율_난방,온도교환효율_냉방,명칭", "번호='" + List[n][0] + "'");
                        if (Value.Length > 0)
                        {
                            dataGridView1.Rows[nRow].Cells[4].Value = String.Format("{0:F0}", Convert.ToDouble(Value[0][0]));
                            dataGridView1.Rows[nRow].Cells[5].Value = String.Format("{0:F0}", Convert.ToDouble(Value[0][1]));
                            dataGridView1.Rows[nRow].Cells[6].Value = String.Format("{0:F0}", Convert.ToDouble(Value[0][2]));
                            dataGridView1.Rows[nRow].Cells[7].Value = "-";
                            dataGridView1.Rows[nRow].Cells[8].Value = "-";
                        }
                    }

                    mainMenu.Add(new { text = List[n][0] + "." + List[n][1], id = "{\\\"formID\\\":17,\\\"ID\\\":\\\"" + List[n][0] + "\\\"}" });
                }
            }
            CountDB = List.Length;
            Program.UTIL.resetMainTree(4, 0, mainMenu.ToArray(), "57");

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
                    Program.DB.deleteValue(DB.type.ProjDB, "AHUSystem_Form", "번호 ='" + Delete_Num + "'");
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


        public void LoadData(String ID)            // 리스트에서 항목 더블 클릭시 - 뷰를 ID 의 getValue 값으로 채우기
        {
            load_List();
        }
    }
}
