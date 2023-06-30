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
    public partial class List_Floor : Form
    {
        static String currentID = "";
        static String inEditing = "Add";

        double CountDB;
        int SelectRow;
        DataTable ListTable = new DataTable();
        DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();

        public List_Floor()
        {
            InitializeComponent();

            Icon_pictureBox.Load(Program.gPath + "images/1sticon/4.Zone_on3.png");
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
            Program.getMenuForm().DoLoadForm(33, OnLoadProc);
        }


        public void Create_Table()
        {
            dataGridView1.Columns.Clear();
            checkBoxColumn.HeaderText = "선택";
            checkBoxColumn.Name = "check";
            dataGridView1.Columns.Add(checkBoxColumn);
            ListTable.Columns.Add("층", typeof(string));
            ListTable.Columns.Add("주요 용도프로필", typeof(string));
            ListTable.Columns.Add("존 개수" + Environment.NewLine + "[EA]", typeof(string));
            ListTable.Columns.Add("순바닥면적" + Environment.NewLine + "[m²]", typeof(string));
            ListTable.Columns.Add("평균 층고" + Environment.NewLine + "[m]", typeof(string));
            ListTable.Columns.Add("평균 천장고" + Environment.NewLine + "[m]", typeof(string));

            dataGridView1.DataSource = ListTable;
        }

        public void load_List()
        {

            string[][] List = Program.DB.getValue_dedupe(DB.type.ProjDB, "ZoneEnvelope_3D", "층", "");
            List<object> mainMenu = new List<object>(); // 예시 코드: 메인 메뉴 동적 할당
            String Blank = "";
            ListTable.Rows.Clear();
            for (int n = 0; n < List.Length; n++)
            {
                ListTable.Rows.Add(List[n][0], null, null, null, null, null);
                string[][] SubList = Program.DB.getValue_dedupe(DB.type.ProjDB, "ZoneEnvelope_3D", "존", "");
                
                List<object> subMenu = new List<object>();
                 for (int k = 0; k < SubList.Length; k++)
                {
                    List<object> subsubMenu = new List<object>();
                    subsubMenu.Add(new { text = "존 일반정보", id = "{\\\"formID\\\":12,\\\"ID\\\":\\\"" + SubList[k][0]+"_1" + "\\\"}" }); // 예시 코드: 메인 메뉴 동적 할당
                    subsubMenu.Add(new { text = "존 외피정보", id = "{\\\"formID\\\":13,\\\"ID\\\":\\\"" + SubList[k][0] + "_2" + "\\\"}" }); // 예시 코드: 메인 메뉴 동적 할당
                    subsubMenu.Add(new { text = "존 조명정보", id = "{\\\"formID\\\":14,\\\"ID\\\":\\\"" + SubList[k][0] + "_3" + "\\\"}" }); // 예시 코드: 메인 메뉴 동적 할당
                    subsubMenu.Add(new { text = "존 설비정보", id = "{\\\"formID\\\":15,\\\"ID\\\":\\\"" + SubList[k][0] + "_4" + "\\\"}" }); // 예시 코드: 메인 메뉴 동적 할당

                    subMenu.Add(new { text = SubList[k][0], id = "{\\\"formID\\\":12,\\\"ID\\\":\\\"" + SubList[k][0] + "\\\"}", children = subsubMenu.ToArray()}); // 예시 코드: 메인 메뉴 동적 할당
                }
                mainMenu.Add(new { text = List[n][0] , id = "{\\\"formID\\\":33,\\\"ID\\\":\\\"" + List[n][0] + "\\\"}", children = subMenu.ToArray() }); // 예시 코드: 메인 메뉴 동적 할당
            }
            dataGridView1.DataSource = this.ListTable;
            CountDB = List.Length;
           Program.UTIL.resetMainTree(3, 0, mainMenu.ToArray(), "32"); // 예시 코드: 메인 메뉴 동적 할당
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
