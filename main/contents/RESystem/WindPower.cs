using main.contentslist;
using main.subcontents;
using main.subcontents.HeatingSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace main.contents
{
    public partial class WindPower : Form
    {

        String[][] 지역;
        String Num, Name;
        String SelectWP_nonsplit;
        ArrayList SelectWP_split = new ArrayList();

        public WindPower()
        {
            InitializeComponent(); this.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular);

            #region getvalue

            지역 = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "지역", "");

            string[][] Image = Program.DB.getValue(DB.type.BaseDB_HCneed, "메뉴아이콘", "하위메뉴아이콘", "하위메뉴명 = '풍력시스템'");
            pictureBox1.Load(Program.gPath + Image[0][0]);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;

            #endregion / getvalue

        }
        private void GeneralPanel_Paint(object sender, PaintEventArgs e)
        {
            Panel p = (Panel)sender;
            ControlPaint.DrawBorder(e.Graphics, p.DisplayRectangle, System.Drawing.SystemColors.Control, ButtonBorderStyle.Solid);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Reset()
        {
        }

        public void LoadData(String ID)            // 리스트에서 항목 더블 클릭시 - 뷰를 ID 의 getValue 값으로 채우기
        {
            Reset();

            Num_textBox.Text = ID;
            Num = ID;
        }
        public static bool OnLoadListProc(Form form)
        {
            List_PV f = (List_PV)form;
            f.load_List();
            return true;
        }
        public void ResetForm(String ID) // 리스트에서 추가 버튼 클릭시 - 뷰 초기화
        {
            Num_textBox.Text = ID;
            Num = ID;
        }

        private void WPDB_button_Click(object sender, EventArgs e)
        {
            if (Name_textBox.Text == "" || Name_textBox.Text == null)
            {
                MessageBox.Show("먼저 명칭을 입력해 주세요");
            }
            else
            {
                string install;
                Name = Name_textBox.Text;
                subcontents.WP_DB wp2_DB = new subcontents.WP_DB("장비일람표 DB");
                DialogResult result = wp2_DB.ShowDialog();
                if (result == DialogResult.OK)
                {
                    tableMake();
                    string[][] value = Program.DB.getValue(DB.type.ProjDB, "User_WP", "번호,제품명,타입,세부타입", "번호 =  '" + wp2_DB.SelectWP2 + "'");
                    if (value.Length > 0)
                    {
                        //install = value[0][7].ToString();
                        //GenNum = value[0][0].ToString();
                        //FCNameText.Text = FC_DB_form.SelectFC;


                        for (int i = 0; i < value.Length; i++)
                        {
                            WP_dataGridView.Rows.Add();
                            int n = WP_dataGridView.Rows.Count - 1;
                            WP_dataGridView.Rows[n].Cells[1].Value = value[i][0];
                            WP_dataGridView.Rows[n].Cells[2].Value = value[i][1];
                            WP_dataGridView.Rows[n].Cells[3].Value = value[i][2];
                            WP_dataGridView.Rows[n].Cells[5].Value = value[i][3];
                            WP_dataGridView.Rows[n].Cells[6].Value = value[i][4];
                            WP_dataGridView.Rows[n].Cells[7].Value = value[i][5];
                            WP_dataGridView.Rows[n].Cells[8].Value = value[i][6];
                            WP_dataGridView.Rows[n].Cells[9].Value = value[i][7];
                        }
                    }
                }



                
                //DialogResult result = wp2_DB.ShowDialog();
                //if (result == DialogResult.OK)
                //{
                //    if (wp2_DB.SelectWP2 != null)
                //    {
                //        SelectWP_nonsplit = wp2_DB.SelectWP2;
                //        Split_WP(wp2_DB.SelectWP2);
                //    }
                //}
            }
        }



        private void tableMake()
        {
            WP_dataGridView.Visible = true;

            new StackedHeaderDecorator(WP_dataGridView, DataGridViewAutoSizeColumnsMode.Fill, datagridviewDesign);
            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
            WP_dataGridView.Columns.Clear();
            checkBoxColumn.HeaderText = "선택";
            checkBoxColumn.Name = "check";
            WP_dataGridView.Columns.Add(checkBoxColumn);
            WP_dataGridView.Columns.Add("A1", "번호");
            WP_dataGridView.Columns.Add("A2", "명칭");
            WP_dataGridView.Columns.Add("A3", "연료");
            WP_dataGridView.Columns.Add("A4", "대수");
            WP_dataGridView.Columns.Add("A5", "전기.출력[kW]");
            WP_dataGridView.Columns.Add("A6", "전기.효율[%]");
            WP_dataGridView.Columns.Add("A7", "열.출력[kW]");
            WP_dataGridView.Columns.Add("A8", "열.효율[%]");
            WP_dataGridView.Columns.Add("A9", "설치");
        }


        private bool datagridviewDesign(DataGridViewCell cell, int column, int row)
        {
            if (column == 4) // 추가
            {
                cell.Style.BackColor = Color.FromArgb(255, 248, 206);
                return true;
            }
            return true;
        }


        private void Split_WP(String nonSplit)
        {
            String 내용 = null;
            if (nonSplit != null)
            {
                if (nonSplit.Contains('+'))
                {
                    string[] token = nonSplit.Split('+');
                    SelectWP_split.Clear();
                    foreach (var item in token)
                    {
                        SelectWP_split.Add(item.ToString());
                    }

                    string[][] WPName = Program.DB.getValue(DB.type.ProjDB, "User_WP", "명칭", "번호 = '" + SelectWP_split[0].ToString() + "'");
                    if (WPName.Length > 0)
                    { 내용 = WPName[0][0] + " 외 " + (SelectWP_split.Count - 1).ToString() + "개"; }
                }
                else
                {
                    SelectWP_split.Clear();
                    SelectWP_split.Add(nonSplit);
                    string[][] BoilerName = Program.DB.getValue(DB.type.ProjDB, "User_WP", "명칭", "번호 = '" + SelectWP_split[0].ToString() + "'");
                    if (BoilerName.Length > 0)
                    {
                        내용 = BoilerName[0][0];
                    }
                }
                //Load_WP_Table();

                //    if (MainSystem == "보일러")
                //    {
                //        MainUserList_textBox.Text = 내용;
                //    }
                //    else if (Sub1System == "보일러")
                //    {
                //        Sub1UserList_textBox.Text = 내용;
                //    }
                //    else if (Sub2System == "보일러")
                //    {
                //        Sub2UserList_textBox.Text = 내용;
                //    }
                //    Load_Boiler_Table();
                //}
                //else
                //{
                //    내용 = "";
            }
        }

        //private void Load_WP_Table()
        //{
        //    DataGridViewCheckBoxColumn Boiler_checkBoxColumn = new DataGridViewCheckBoxColumn();
        //    new StackedHeaderDecorator(Boiler_dataGridView, DataGridViewAutoSizeColumnsMode.Fill);
        //    Boiler_dataGridView.Columns.Clear();
        //    Boiler_checkBoxColumn.HeaderText = "선택";
        //    Boiler_checkBoxColumn.Name = "check";
        //    Boiler_dataGridView.Columns.Add(Boiler_checkBoxColumn);
        //    Boiler_dataGridView.Columns.Add("A1", "번호");
        //    Boiler_dataGridView.Columns.Add("A2", "명칭");
        //    Boiler_dataGridView.Columns.Add("A3", "연료");
        //    Boiler_dataGridView.Columns.Add("A4", "Type");
        //    Boiler_dataGridView.Columns.Add("A5", "용량.[kW]");
        //    Boiler_dataGridView.Columns.Add("A6", "효율.전부하효율.[%]");
        //    Boiler_dataGridView.Columns.Add("A7", "효율.부분부하효율.[%]");
        //    Boiler_dataGridView.Columns.Add("A8", "소비전력.[W]");
        //    Boiler_dataGridView.Columns.Add("A9", "대기전력.[W]");
        //    Boiler_dataGridView.Columns.Add("A10", "대수.[EA]");
        //    Boiler_dataGridView.Columns[0].Width = 30;

        //    for (int n = 0; n < SelectBoiler_split.Count; n++)
        //    {
        //        string[][] User_Value = Program.DB.getValue(DB.type.ProjDB, "User_Boiler", "번호,명칭,연료,Type,용량,전부하효율,부분부하효율,소비전력,대기전력", "번호 = '" + SelectBoiler_split[n].ToString() + "'");
        //        if (User_Value.Length > 0)
        //        {
        //            string 용량 = "", 전부하효율 = "", 부분부하효율 = "", 소비전력 = "", 대기전력 = "";
        //            if (User_Value[0][4] != null && User_Value[0][4] != "")
        //            {
        //                double a = Convert.ToDouble(User_Value[0][4]);
        //                용량 = string.Format("{0:F1}", Convert.ToDouble(User_Value[0][4]));
        //            }
        //            if (User_Value[0][5] != null && User_Value[0][5] != "")
        //            {
        //                전부하효율 = string.Format("{0:F1}", Convert.ToDouble(User_Value[0][5]));
        //            }
        //            if (User_Value[0][6] != null && User_Value[0][6] != "")
        //            {
        //                부분부하효율 = string.Format("{0:F1}", Convert.ToDouble(User_Value[0][6]));
        //            }
        //            if (User_Value[0][7] != null && User_Value[0][7] != "")
        //            {
        //                소비전력 = string.Format("{0:F0}", Convert.ToDouble(User_Value[0][7]));
        //            }
        //            if (User_Value[0][8] != null && User_Value[0][8] != "")
        //            {
        //                대기전력 = string.Format("{0:F0}", Convert.ToDouble(User_Value[0][8]));
        //            }
        //            Boiler_dataGridView.Rows.Add();
        //            int nRow = Boiler_dataGridView.Rows.Count - 1;
        //            for (int k = 1; k < 5; k++)
        //            {
        //                Boiler_dataGridView.Rows[nRow].Cells[k].Value = User_Value[0][k];
        //            }
        //            Boiler_dataGridView.Rows[nRow].Cells[5].Value = 용량;
        //            Boiler_dataGridView.Rows[nRow].Cells[6].Value = 전부하효율;
        //            Boiler_dataGridView.Rows[nRow].Cells[7].Value = 부분부하효율;
        //            Boiler_dataGridView.Rows[nRow].Cells[8].Value = 소비전력;
        //            Boiler_dataGridView.Rows[nRow].Cells[9].Value = 대기전력;
        //        }
        //    }
        //}



    }
}
