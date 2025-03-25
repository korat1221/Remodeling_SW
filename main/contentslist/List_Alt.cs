using main.contents;
using main.contents.Alt;
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
    public partial class List_Alt : Form
    {
        static String currentID = "";
        static String inEditing = "Add";

        String Num;
        double CountDB;
        int SelectRow;
        DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();

        public List_Alt()
        {
            InitializeComponent(); this.Font = new Font(UTIL.Families[0], 9.75F, FontStyle.Regular);

            string[][] Image = Program.DB.getValue(DB.type.BaseDB_HCneed, "메뉴아이콘", "하위메뉴아이콘", "하위메뉴명 = '외벽'");
            if (Image.Length > 0)
            {
                Icon_pictureBox.Load(Program.gPath + "images/1sticon/8.Remodeling_2.png");
                Icon_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            }
            Create_Table();
        }

        private void GeneralPanel_Paint(object sender, PaintEventArgs e)
        {
            Panel p = (Panel)sender;
            ControlPaint.DrawBorder(e.Graphics, p.DisplayRectangle, System.Drawing.SystemColors.Control, ButtonBorderStyle.Solid);
        }


        private void Add_button_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Add();
            Num = Program.UTIL.CreateNum("Optimal_Form", "번호", "Alt");

            Program.getMenuForm().ResetForm(59);

            Load_form(Num, "Add");
        }

        public static bool OnLoadProc(Form form)
        {
            AltMain  f = (AltMain)form;

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
            Program.getMenuForm().DoLoadForm(59, OnLoadProc);
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
            dataGridView1.Columns.Add("A3", "적용 기술");
            dataGridView1.Columns.Add("A4", "에너지절감률.[%]");
            dataGridView1.Columns.Add("A5", "예상 순공사비.[천원]");
            dataGridView1.Columns.Add("A6", "종합점수.[점]");
            dataGridView1.Columns.Add("A7", "");
            dataGridView1.Columns[0].Width = 40;
            dataGridView1.Columns[1].Width = 50;
            dataGridView1.Columns[2].Width = 60;
            dataGridView1.Columns[4].Width = 60;
            dataGridView1.Columns[5].Width = 60;
            dataGridView1.Columns[6].Width = 60;
            dataGridView1.Columns[7].Width = 5;
            dataGridView1.Columns[7].Visible = false;
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
            string[][] List = Program.DB.getValue(DB.type.ProjDB, "Optimal_Form", "번호,명칭,종합점수", "");
            if (List.Length > 0)
            {
                String Blank = "";
                dataGridView1.Rows.Clear();
                for (int n = 0; n < List.Length; n++)
                {
                    dataGridView1.Rows.Add();
                    int nRow = dataGridView1.Rows.Count - 1;
                    dataGridView1.Rows[nRow].Cells[1].Value = List[n][0];
                    dataGridView1.Rows[nRow].Cells[2].Value = List[n][1];
                    Load_alt(List[n][0], nRow);
                    dataGridView1.Rows[nRow].Cells[6].Value = Convert.ToDouble(List[n][2]).ToString("0.0") + " 점";
                    mainMenu.Add(new { text = List[n][0] + "." + List[n][1], id = "{\\\"formID\\\":59,\\\"ID\\\":\\\"" + List[n][0] + "\\\"}" }); // 예시 코드: 메인 메뉴 동적 할당
                }

            }
            CountDB = List.Length;
            Program.UTIL.resetMainTree(7, 0, mainMenu.ToArray(), "58"); // 예시 코드: 메인 메뉴 동적 할당
        }

        private void Load_alt(string AltNum, int row )
        {
            double TotalCost = 0, TotalSaving = 0, TotalSavingPercent = 0; 
           string[][] Load = Program.DB.getValue(DB.type.ProjDB, "Optimal_Form", "요소기술1,리모델링안1,요소기술2,리모델링안2,요소기술3,리모델링안3,요소기술4,리모델링안4,요소기술5,리모델링안5," +
               "요소기술6,리모델링안6,요소기술7,리모델링안7,요소기술8,리모델링안8,요소기술9,리모델링안9,요소기술10,리모델링안10", "번호 = '" + AltNum + "'");
            if (Load.Length > 0)
            {
                for (int a = 0; a < 10; a++)
                {
                    if (Load[0][a * 2] != null && Load[0][a * 2] != "")
                    {
                       if(a == 0) { dataGridView1.Rows[row].Cells[3].Value = Load[0][a * 2] + "[" + Load[0][a * 2 + 1] + "]"; }
                        else { dataGridView1.Rows[row].Cells[3].Value = dataGridView1.Rows[row].Cells[3].Value.ToString()+ ", " + Load[0][a * 2] + "[" + Load[0][a * 2 + 1] + "]"; }
                        string[][] Value = Program.DB.querySQL(DB.type.ProjDB, "Select 리모델링값,순공사비,에너지절감량,에너지절감률,종합점수 From Optimal_PreResult Where 검토유형='"+ Load[0][a * 2] + "' and 리모델링안='" + Load[0][a * 2  + 1] + "'");
                        if (Value.Length > 0)
                        {
                            TotalCost += Convert.ToDouble(Value[0][1]);
                            TotalSaving += Convert.ToDouble(Value[0][3]);
                        }
                    }
                    else
                    {
                        break;
                    }
                }

                dataGridView1.Rows[row].Cells[4].Value = TotalSaving.ToString("0.0") + " %";
                dataGridView1.Rows[row].Cells[5].Value = (TotalCost / 1000).ToString("#,##0");
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
                    Program.DB.deleteValue(DB.type.ProjDB, "Optimal_Form", "번호 ='" + Delete_Num + "'");
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
            Num = Program.UTIL.CreateNum("Optimal_Form", "번호", "Alt");
            int k = dataGridView1.CurrentCell.RowIndex;
            if (k > -1)
            {
                String Copy_Num = dataGridView1.Rows[k].Cells[1].Value.ToString();

                Program.DB.CopyValue(DB.type.ProjDB, "Optimal_Form", "번호 ='" + Copy_Num + "'", Num);
                Program.DB.executeSQL(DB.type.ProjDB, "UPDATE  Optimal_Form" + " SET 명칭 = '" + dataGridView1.Rows[k].Cells[2].Value.ToString() + "_복사" + "' WHERE  번호 = '" + Num + "'");
                Load_form(Num, "Copy");
                Program.DB.saveProject();

            }
        }

        public void LoadData(String ID)            // 리스트에서 항목 더블 클릭시 - 뷰를 ID 의 getValue 값으로 채우기
        {
            load_List();
        }
    }
}
