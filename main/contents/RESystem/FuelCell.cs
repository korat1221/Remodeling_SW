using Eagle._Components.Public;
using main.contentslist;
using main.subcontents;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace main.contents
{
    public partial class FuelCell : Form
    {
        String[][] 지역;

        string Num, 프로젝트유형, Name, GenNum, SupplyType, StartTime, EndTime, Usehour, Useday, InstallNumber; //연료전지번호, 명칭, 연료전지,생산유형,시작시간,종료시간,사용시간,주이용일,설치대수
        string WListNonsplit, HListNonsplit; //급탕, 난방설비 리스트


        public FuelCell()
        {
            InitializeComponent();

            지역 = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "지역", "");

            string[][] Image = Program.DB.getValue(DB.type.BaseDB_HCneed, "메뉴아이콘", "하위메뉴아이콘", "하위메뉴명 = '연료전지'");
            Icon_pictureBox.Load(Program.gPath + Image[0][0]);
            Icon_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;

            string[][] 프로젝트 = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "프로젝트유형번호");
            if (프로젝트.Length > 0)
            {
                프로젝트유형 = 프로젝트[0][0];
            }

            //콤보박스 작성
            FCTypeComboBox.Items.AddRange(new string[] { "전기", "전기+급탕", "전기+난방", "전기+급탕+난방" });

            Program.UTIL.FillComboBox(DB.type.BaseDB_HCneed, Start_comboBox, "존일반", "이용일 시작 및 종료시간", "");
            Program.UTIL.FillComboBox(DB.type.BaseDB_HCneed, End_comboBox, "존일반", "이용일 시작 및 종료시간", "");
            Program.UTIL.FillComboBox(DB.type.BaseDB_HCneed, Week_comboBox, "존일반", "주간이용일", "4");

            W_textBox.Visible = false;
            W_button.Visible = false;
            WLabel.Visible = false;
            H_textBox.Visible = false;
            H_button.Visible = false;
            HLabel.Visible = false;
        }

        private void GeneralPanel_Paint(object sender, PaintEventArgs e)
        {
            Panel p = (Panel)sender;
            ControlPaint.DrawBorder(e.Graphics, p.DisplayRectangle, Color.FromArgb(153, 180, 209), ButtonBorderStyle.Solid);
        }

        //private void FuelCell_Load(object sender, EventArgs e)
        //{

        //}

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            Panel p = (Panel)sender;
            ControlPaint.DrawBorder(e.Graphics, p.DisplayRectangle, Color.FromArgb(153, 180, 209), ButtonBorderStyle.Solid);
        }

        private void FCDB_button_Click(object sender, EventArgs e)
        {

            if (Name_textBox.Text == "" || Name_textBox.Text == null)
            {
                MessageBox.Show("먼저 명칭을 입력해 주세요");
            }
            else
            {
                string install;
                Name = Name_textBox.Text;
                subcontents.FC FC_DB_form = new subcontents.FC("장비일람표 DB");
                DialogResult result = FC_DB_form.ShowDialog();
                if (result == DialogResult.OK)
                {
                    tableMake();
                    string[][] value = Program.DB.getValue(DB.type.ProjDB, "User_FC", "번호,명칭,연료,전기출력,전기효율,열출력,열효율,설치", "번호 =  '" + FC_DB_form.SelectFC + "'");
                    if (value.Length > 0)
                    {
                        install = value[0][7].ToString();
                        GenNum = value[0][0].ToString();
                        FCNameText.Text = FC_DB_form.SelectFC;


                        for (int i = 0; i < value.Length; i++)
                        {
                            FC_dataGridView.Rows.Add();
                            int n = FC_dataGridView.Rows.Count - 1;
                            FC_dataGridView.Rows[n].Cells[1].Value = value[i][0];
                            FC_dataGridView.Rows[n].Cells[2].Value = value[i][1];
                            FC_dataGridView.Rows[n].Cells[3].Value = value[i][2];
                            FC_dataGridView.Rows[n].Cells[5].Value = value[i][3];
                            FC_dataGridView.Rows[n].Cells[6].Value = value[i][4];
                            FC_dataGridView.Rows[n].Cells[7].Value = value[i][5];
                            FC_dataGridView.Rows[n].Cells[8].Value = value[i][6];
                            FC_dataGridView.Rows[n].Cells[9].Value = value[i][7];
                        }

                        SourceImageMake(install); //연료 이미지
                        GenImageMake(install); //생산설비 이미지
                    }
                }
            }
        }

        private void tableMake()
        {
            FC_dataGridView.Visible = true;
            
            new StackedHeaderDecorator(FC_dataGridView, DataGridViewAutoSizeColumnsMode.Fill, datagridviewDesign);
            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
            FC_dataGridView.Columns.Clear();
            checkBoxColumn.HeaderText = "선택";
            checkBoxColumn.Name = "check";
            FC_dataGridView.Columns.Add(checkBoxColumn);
            FC_dataGridView.Columns.Add("A1", "번호");
            FC_dataGridView.Columns.Add("A2", "명칭");
            FC_dataGridView.Columns.Add("A3", "연료");
            FC_dataGridView.Columns.Add("A4", "대수");
            FC_dataGridView.Columns.Add("A5", "전기.출력[kW]");
            FC_dataGridView.Columns.Add("A6", "전기.효율[%]");
            FC_dataGridView.Columns.Add("A7", "열.출력[kW]");
            FC_dataGridView.Columns.Add("A8", "열.효율[%]");
            FC_dataGridView.Columns.Add("A9", "설치");
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



        private void SourceImageMake(string _install)
        {
            string[][] Image = Program.DB.getValue(DB.type.BaseDB_RESystem, "연료전지이미지", "이미지", "세부항목 = '연료' And 설치유형 ='" + _install + "'");
            if (Image.Length > 0)
            {
                SourcepictureBox.Size = new System.Drawing.Size(280, 310);

                SourcepictureBox.Location = new Point(0, 40);
                SourcepictureBox.Load(Program.gPath + Image[0][0]);
                SourcepictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            }
        }
        private void GenImageMake(string _install)
        {
            string[][] Image = Program.DB.getValue(DB.type.BaseDB_RESystem, "연료전지이미지", "이미지", "세부항목 = '설비' And 설치유형 ='" + _install + "'");
            if (Image.Length > 0)
            {
                GenpictureBox.Size = new System.Drawing.Size(400, 230);
                GenpictureBox.Location = new Point(0, 50);
                GenpictureBox.Load(Program.gPath + Image[0][0]);
                GenpictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            }
        }

        private void Calc_Time()
        {
            TimeSpan ts;

            if (Start_comboBox.SelectedItem != null && End_comboBox.SelectedItem != null)
            {
                if (Start_comboBox.SelectedItem.ToString() == End_comboBox.SelectedItem.ToString())
                {
                    UseTime_TextBox.Text = 24.ToString();
                }
                else
                {
                    ts = DateTime.Parse(EndTime) - DateTime.Parse(StartTime);
                    if (Double.Parse(ts.Hours.ToString()) >= 0)
                    { UseTime_TextBox.Text = ts.Hours.ToString(); }
                    else
                    {
                        double usetime = Double.Parse(ts.Hours.ToString()) + 24;
                        UseTime_TextBox.Text = usetime.ToString();
                    }
                }

                Usehour = UseTime_TextBox.Text;
            }
        }

        #region //저장하기
        private void Save_button_Click(object sender, EventArgs e)
        {
            if (Save_FC() == true)
            {
                if ((MessageBox.Show("저장 하시겠습니까?", "이전 화면 이동", MessageBoxButtons.YesNo) == DialogResult.Yes))
                {
                    this.DialogResult = DialogResult.OK;
                    this.Hide();
                    Program.getMenuForm().DoLoadForm(54, OnLoadListProc);
                }
            }
            else
            {
                return;
            }
        }
   
        public bool Save_FC()
        {
            if (UseTime_TextBox.Text == null || Useday == null)
            {
                MessageBox.Show("입력을 완료해주세요.");
                return false;
            }
            else if (InstallNumber == null || InstallNumber == "")
            {
                MessageBox.Show("대수입력을 완료해주세요.");
                return false;
            }
            else
            {
                Program.DB.setValue(DB.type.ProjDB, "FuelCell_Form", "번호,프로젝트유형,명칭,연료전지,생산유형,시작시간,종료시간,사용시간,주이용일,설치대수,급탕설비,난방설비",
                "'" + Num + "','" + 프로젝트유형 + "','" + Name + "','" + GenNum + "','" + SupplyType + "', '" + StartTime + "','" + EndTime + "','" + Usehour + "', '" + Useday + "', '" + InstallNumber + "', '" + WListNonsplit + "','" + HListNonsplit + "'", "번호");
                return true;
            }

        }
        #endregion


        private void Reset()
        {
            Num = null; Name = null; GenNum = null; SupplyType = null; StartTime = null; EndTime = null; Usehour = null; Useday = null; InstallNumber = null;
            WListNonsplit = null; HListNonsplit = null;

            W_textBox.Visible = false;
            W_textBox.Text = null;
            W_button.Visible = false;
            H_textBox.Visible = false;
            H_textBox.Text = null;
            H_button.Visible = false;
            FCNameText.Text = null;
            FCTypeComboBox.Text = null;
            Start_comboBox.Text = null;
            End_comboBox.Text = null;
            UseTime_TextBox.Text = null;
            Week_comboBox.Text = null;
            FC_dataGridView.Rows.Clear();
            FC_dataGridView.Visible = false;
        }

        public void LoadData(String ID)
        {
            Reset();
            Num_textBox.Text = ID;
            Num = ID;
            string[][] value = Program.DB.getValue(DB.type.ProjDB, "FuelCell_Form", "프로젝트유형,명칭,연료전지,생산유형,시작시간,종료시간,사용시간,주이용일,설치대수,급탕설비,난방설비", "번호='" + Num + "'");
            if (value.Length > 0)
            {
                Name = value[0][1];
                Name_textBox.Text = Name;
                GenNum = value[0][2];
                SupplyType = value[0][3];
                StartTime = value[0][4];
                EndTime = value[0][5];
                Usehour = value[0][6];
                Useday = value[0][7];
                InstallNumber = value[0][8];
                WListNonsplit = value[0][9];
                HListNonsplit = value[0][10];
            }
            tableMake();
            string[][] datavalue = Program.DB.getValue(DB.type.ProjDB, "User_FC", "번호,명칭,연료,전기출력,전기효율,열출력,열효율,설치", "번호 =  '" + GenNum + "'");
            if (datavalue.Length > 0)
            {
                string install = datavalue[0][7].ToString();
                FCNameText.Text = GenNum;

                for (int i = 0; i < datavalue.Length; i++)
                {
                    FC_dataGridView.Rows.Add();
                    int n = FC_dataGridView.Rows.Count - 1;
                    FC_dataGridView.Rows[n].Cells[1].Value = datavalue[i][0];
                    FC_dataGridView.Rows[n].Cells[2].Value = datavalue[i][1];
                    FC_dataGridView.Rows[n].Cells[3].Value = datavalue[i][2];
                    FC_dataGridView.Rows[n].Cells[4].Value = InstallNumber;
                    FC_dataGridView.Rows[n].Cells[5].Value = datavalue[i][3];
                    FC_dataGridView.Rows[n].Cells[6].Value = datavalue[i][4];
                    FC_dataGridView.Rows[n].Cells[7].Value = datavalue[i][5];
                    FC_dataGridView.Rows[n].Cells[8].Value = datavalue[i][6];
                    FC_dataGridView.Rows[n].Cells[9].Value = datavalue[i][7];
                }
                SourceImageMake(install); //연료 이미지
                GenImageMake(install); //생산설비 이미지
            }
            FCTypeComboBox.Text = SupplyType;
            string _supply = TypeCombo(SupplyType);
            SupplyTypeImage(_supply);

            Start_comboBox.Text = StartTime;
            End_comboBox.Text = EndTime;
            UseTime_TextBox.Text = Usehour;
            Week_comboBox.Text = Useday;
        }
        public static bool OnLoadListProc(Form form)
        {
            List_FuelCell f = (List_FuelCell)form;
            f.load_List();
            return true;
        }
        public void ResetForm(String ID) // 리스트에서 추가 버튼 클릭시 - 뷰 초기화
        {
            Num_textBox.Text = ID;
            Num = ID;
        }

        private void FCTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            SupplyType = FCTypeComboBox.Text;
            string _supply = TypeCombo(SupplyType);
            SupplyTypeImage(_supply);
        }

        private void SupplyTypeImage(string _SupplyType)
        {
            string[][] Image = Program.DB.getValue(DB.type.BaseDB_RESystem, "연료전지이미지", "이미지", "세부항목 = '" + _SupplyType + "'");
            if (Image.Length > 0)
            {
                SupplypictureBox.Size = new System.Drawing.Size(350, 220);
                SupplypictureBox.Location = new Point(0, 0);
                SupplypictureBox.Load(Program.gPath + Image[0][0]);
                SupplypictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            }
        }

        private string TypeCombo(string _SupplyType)
        {
            string supply;
            switch (_SupplyType)
            {
                case "전기+급탕":
                    W_textBox.Visible = true;
                    W_button.Visible = true;
                    WLabel.Visible = true;
                    H_textBox.Visible = false;
                    H_button.Visible = false;
                    HLabel.Visible = false;
                    supply = "급탕";
                    return supply;
                    break;
                case "전기+난방":
                    W_textBox.Visible = false;
                    W_button.Visible = false;
                    WLabel.Visible = false;
                    H_textBox.Visible = true;
                    H_button.Visible = true;
                    HLabel.Visible = true;
                    supply = "난방";
                    return supply;
                    break;
                case "전기+급탕+난방":
                    W_textBox.Visible = true;
                    W_button.Visible = true;
                    WLabel.Visible = true;
                    H_textBox.Visible = true;
                    H_button.Visible = true;
                    HLabel.Visible = true;
                    supply = "급탕난방";
                    return supply;
                    break;
                default:
                    W_textBox.Visible = false;
                    W_button.Visible = false;
                    WLabel.Visible = false;
                    H_textBox.Visible = false;
                    H_button.Visible = false;
                    HLabel.Visible = false;
                    supply = null;
                    return supply;
                    break;
            }
        }
        private void Start_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            StartTime = Start_comboBox.Text;
            Calc_Time();
        }

        private void End_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            EndTime = End_comboBox.Text;
            Calc_Time();
        }

        private void Week_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Useday = Week_comboBox.Text;
        }

        private void W_button_Click(object sender, EventArgs e)
        {
            W_textBox.Text = null;
            //작성필요함
        }

        private void H_button_Click(object sender, EventArgs e)
        {
            H_textBox.Text = null;
            //작성필요함
        }

        private void FC_dataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (e.ColumnIndex == 4)
                {
                    InstallNumber = FC_dataGridView.Rows[e.RowIndex].Cells[4].Value.ToString();
                }
            }
        }
        private void Split(string nonSplit, List<string> type)
        {
            type.Clear();

            if (nonSplit != null)
            {
                string[] token = nonSplit.Split('+');
                foreach (string item in token)
                {
                    string _item = item.Trim();
                    type.Add(_item);
                }
            }
        }
    }
}
