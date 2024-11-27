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
    public partial class List_Floor : Form
    {
        static String currentID = "";
        static String inEditing = "Add";

        double CountDB;
        int SelectRow;
        // DataTable ListTable = new DataTable();
        DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();

        public List_Floor()
        {
            InitializeComponent(); this.Font = new Font(UTIL.Families[0], 9.75F, FontStyle.Regular);

            Icon_pictureBox.Load(Program.gPath + "images/1sticon/4.Zone_on3.png");
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
            new StackedHeaderDecorator(dataGridView1, DataGridViewAutoSizeColumnsMode.Fill, datagridviewDesign);
            dataGridView1.Columns.Clear();
            checkBoxColumn.HeaderText = "선택";
            checkBoxColumn.Name = "check";
            dataGridView1.Columns.Add(checkBoxColumn);
            dataGridView1.Columns.Add("A1", "층");
            dataGridView1.Columns.Add("A2", "주요 용도프로필");
            dataGridView1.Columns.Add("A3", "존 개수.[EA]");
            dataGridView1.Columns.Add("A4", "순바닥면적.[m²]");
            dataGridView1.Columns.Add("A5", "평균 천장고.[m]");
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

            string[][] List = Program.DB.querySQL(DB.type.ProjDB, "Select Distinct 층 From ZoneEnvelope_3D Order by 층");

            List<object> mainMenu = new List<object>(); // 예시 코드: 메인 메뉴 동적 할당
            if (List.Length > 0)
            {
                string[][] Value;
                dataGridView1.Rows.Clear();
                for (int n = 0; n < List.Length; n++)
                {
                    string[][] Zone = Program.DB.querySQL(DB.type.ProjDB, "Select Distinct 존 From ZoneEnvelope_3D where 층 ='" + List[n][0] + "' Order by 존 ");
                    String[] 존이름 = new String[Zone.Length];
                    String[] 용도프로필 = new String[Zone.Length];
                    double[] 순바닥면적 = new double[Zone.Length];
                    double[] 천장고 = new double[Zone.Length];
                    if (Zone.Length > 0)
                    {
                        for (int k = 0; k < Zone.Length; k++)
                        {
                            try
                            {
                                Value = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_Form", " 존이름, 용도프로필, 순바닥면적, 천장고", "존번호 = '" + Zone[k][0] + "'");
                                if (Value.Length > 0)
                                {
                                    존이름[k] = Value[0][0];
                                    용도프로필[k] = Value[0][1];
                                    순바닥면적[k] = Convert.ToDouble(Value[0][2]);
                                    천장고[k] = Convert.ToDouble(Value[0][3]);
                                }
                                else
                                {
                                    Value = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_3D", " 존이름", "존번호 = '" + Zone[k][0] + "'");
                                    if (Value.Length > 0)
                                    {
                                        존이름[k] = Value[0][0];
                                    }
                                }
                            }
                            catch { }
                        }

                        int index = 0;//순바닥면적 가장 큰 존 인덱스 찾기
                        for (int k = 0; k < Zone.Length; k++)
                        {
                            if (순바닥면적[k] == 순바닥면적.Max())
                            { index = k; }
                        }

                        if (순바닥면적.Contains(0)) //모든 존 정보가 저장되어 있을 때만 
                        {
                            dataGridView1.Rows.Add();
                            int nRow = dataGridView1.Rows.Count - 1;
                            dataGridView1.Rows[nRow].Cells[1].Value = List[n][0];
                            dataGridView1.Rows[nRow].Cells[2].Value = null;
                            dataGridView1.Rows[nRow].Cells[3].Value = null;
                            dataGridView1.Rows[nRow].Cells[4].Value = null;
                            dataGridView1.Rows[nRow].Cells[5].Value = null;
                        }
                        else
                        {
                            dataGridView1.Rows.Add();
                            int nRow = dataGridView1.Rows.Count - 1;
                            dataGridView1.Rows[nRow].Cells[1].Value = List[n][0];
                            dataGridView1.Rows[nRow].Cells[2].Value = 용도프로필[index];
                            dataGridView1.Rows[nRow].Cells[3].Value = Zone.Length;
                            dataGridView1.Rows[nRow].Cells[4].Value = String.Format("{0:F1}", 순바닥면적.Sum());
                            dataGridView1.Rows[nRow].Cells[5].Value = String.Format("{0:F1}", 천장고.Average());
                        }
                        List<object> subMenu = new List<object>();
                        for (int k = 0; k < Zone.Length; k++)
                        {
                            List<object> subsubMenu = new List<object>();
                            subsubMenu.Add(new { text = "존 일반정보", id = "{\\\"formID\\\":12,\\\"ID\\\":\\\"" + Zone[k][0] + "_1" + "\\\"}" }); // 예시 코드: 메인 메뉴 동적 할당
                            subsubMenu.Add(new { text = "존 외피정보", id = "{\\\"formID\\\":13,\\\"ID\\\":\\\"" + Zone[k][0] + "_2" + "\\\"}" }); // 예시 코드: 메인 메뉴 동적 할당
                            subsubMenu.Add(new { text = "존 조명정보", id = "{\\\"formID\\\":14,\\\"ID\\\":\\\"" + Zone[k][0] + "_3" + "\\\"}" }); // 예시 코드: 메인 메뉴 동적 할당

                            int v = Zone[k][0].IndexOf("Zone") + 4;

                            subMenu.Add(new { text = Zone[k][0].Substring(v, Zone[k][0].Length - v) + "_" + 존이름[k], id = "{\\\"formID\\\":12,\\\"ID\\\":\\\"" + Zone[k][0] + "\\\"}", children = subsubMenu.ToArray() }); // 예시 코드: 메인 메뉴 동적 할당
                        }
                        mainMenu.Add(new { text = List[n][0], id = "{\\\"formID\\\":33,\\\"ID\\\":\\\"" + List[n][0] + "\\\"}", children = subMenu.ToArray() }); // 예시 코드: 메인 메뉴 동적 할당
                    }
                }
            }
            CountDB = List.Length;
            Program.UTIL.resetMainTree(3, 0, mainMenu.ToArray(), "32"); // 예시 코드: 메인 메뉴 동적 할당
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            //int k = dataGridView1.CurrentCell.RowIndex;
            //if (k > -1)
            //{
            //    Load_form(dataGridView1.Rows[k].Cells[1].Value.ToString(), "Edit");
            //}

        }


        public void LoadData(String ID)            // 리스트에서 항목 더블 클릭시 - 뷰를 ID 의 getValue 값으로 채우기
        {
            load_List();
        }

    }
}
