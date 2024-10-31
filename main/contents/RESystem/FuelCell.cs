using Eagle._Components.Public;
using Eagle._Interfaces.Public;
using main.contentslist;
using main.subcontents;
using main.subcontents.RESystem_FC;
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
using static System.ComponentModel.Design.ObjectSelectorEditor;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace main.contents
{
    public partial class FuelCell : Form
    {
        String[][] 지역;

        string Num, 프로젝트유형, Name, GenNumnonsplit, SupplyType, StartTime, EndTime, Usehour, Useday, InstallNumbernonsplit; //연료전지번호, 명칭, 연료전지,생산유형,시작시간,종료시간,사용시간,주이용일,설치대수
        string WListNonsplit, HListNonsplit; //급탕, 난방설비 리스트


        public FuelCell()
        {
            InitializeComponent(); this.Font = new Font(UTIL.Families[0], 9.75F, FontStyle.Regular);

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

        private void FCDB_button_Click(object sender, EventArgs e)
        {

            if (Name_textBox.Text == "" || Name_textBox.Text == null)
            {
                MessageBox.Show("먼저 명칭을 입력해 주세요");
            }
            else
            {
                string install = null;
                Name = Name_textBox.Text;
                subcontents.FC FC_DB_form = new subcontents.FC("장비일람표 DB");
                DialogResult result = FC_DB_form.ShowDialog();
                if (result == DialogResult.OK)
                {
                    tableMake();
                    string[] token = FC_DB_form.SelectFCnonsplit.Split('+');
                    FCNameText.Text = token[0] + "외" + (token.Length - 1).ToString() + "개";

                    for (int i = 0; i < token.Length; i++)
                    {
                        string[][] value = Program.DB.getValue(DB.type.ProjDB, "User_FC", "번호,명칭,연료,전기출력,전기효율,열출력,열효율,설치", "번호 =  '" + token[i] + "'");

                        if (value.Length > 0)
                        {
                            install = value[0][7].ToString();
                            FC_dataGridView.Rows.Add();
                            FC_dataGridView.Rows[i].Cells[1].Value = value[0][0];
                            FC_dataGridView.Rows[i].Cells[2].Value = value[0][1];
                            FC_dataGridView.Rows[i].Cells[3].Value = value[0][2];
                            FC_dataGridView.Rows[i].Cells[5].Value = value[0][3];
                            FC_dataGridView.Rows[i].Cells[6].Value = value[0][4];
                            FC_dataGridView.Rows[i].Cells[7].Value = value[0][5];
                            FC_dataGridView.Rows[i].Cells[8].Value = value[0][6];
                            FC_dataGridView.Rows[i].Cells[9].Value = value[0][7];
                        }
                    }
                    SourceImageMake(install); //연료 이미지
                    GenImageMake(install); //생산설비 이미지
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

            for (int j = 0; j < FC_dataGridView.RowCount; j++)
            {
                if (FC_dataGridView.Rows[j].Cells[4].Value == null || FC_dataGridView.Rows[j].Cells[4].Value == "")
                {
                    MessageBox.Show("대수입력을 완료해주세요.");
                    return false;
                }
            }

            if (UseTime_TextBox.Text == null || Useday == null)
            {
                MessageBox.Show("입력을 완료해주세요.");
                return false;
            }

            else
            {
                GenNumnonsplit = null;
                InstallNumbernonsplit = null;
                for (int k = 0; k < FC_dataGridView.RowCount; k++)
                {
                    if (k == FC_dataGridView.RowCount - 1)
                    {
                        this.GenNumnonsplit += FC_dataGridView.Rows[k].Cells[1].Value.ToString();
                        this.InstallNumbernonsplit += FC_dataGridView.Rows[k].Cells[4].Value.ToString();
                    }
                    else
                    {
                        this.GenNumnonsplit += FC_dataGridView.Rows[k].Cells[1].Value.ToString() + "+";
                        this.InstallNumbernonsplit += FC_dataGridView.Rows[k].Cells[4].Value.ToString() + "+";
                    }
                }

                Program.DB.setValue(DB.type.ProjDB, "FuelCell_Form", "번호,프로젝트유형,명칭,연료전지,생산유형,시작시간,종료시간,사용시간,주이용일,설치대수,급탕설비,난방설비",
                "'" + Num + "','" + 프로젝트유형 + "','" + Name + "','" + GenNumnonsplit + "','" + SupplyType + "', '" + StartTime + "','" + EndTime + "','" + Usehour + "', '" + Useday + "', '" + InstallNumbernonsplit + "', '" + WListNonsplit + "','" + HListNonsplit + "'", "번호");
                return true;
            }

        }
        #endregion


        private void Reset()
        {
            Num = null; Name = null; GenNumnonsplit = null; SupplyType = null; StartTime = null; EndTime = null; Usehour = null; Useday = null; InstallNumbernonsplit = null;
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
                GenNumnonsplit = value[0][2];
                SupplyType = value[0][3];
                StartTime = value[0][4];
                EndTime = value[0][5];
                Usehour = value[0][6];
                Useday = value[0][7];
                InstallNumbernonsplit = value[0][8];


                WListNonsplit = value[0][9];
                HListNonsplit = value[0][10];
            }
            tableMake();
            string install = null;
            string[] token_name = GenNumnonsplit.Split('+');
            FCNameText.Text = token_name[0] + "외" + (token_name.Length - 1).ToString() + "개";

            string[] token_instNum = InstallNumbernonsplit.Split('+');

            for (int i = 0; i < token_name.Length; i++)
            {
                string[][] datavalue = Program.DB.getValue(DB.type.ProjDB, "User_FC", "번호,명칭,연료,전기출력,전기효율,열출력,열효율,설치", "번호 =  '" + token_name[i] + "'");
                if (datavalue.Length > 0)
                {
                    install = datavalue[0][7].ToString();
                    FC_dataGridView.Rows.Add();
                    int n = FC_dataGridView.Rows.Count - 1;
                    FC_dataGridView.Rows[n].Cells[1].Value = datavalue[0][0];
                    FC_dataGridView.Rows[n].Cells[2].Value = datavalue[0][1];
                    FC_dataGridView.Rows[n].Cells[3].Value = datavalue[0][2];
                    FC_dataGridView.Rows[n].Cells[4].Value = token_instNum[i];
                    FC_dataGridView.Rows[n].Cells[5].Value = datavalue[0][3];
                    FC_dataGridView.Rows[n].Cells[6].Value = datavalue[0][4];
                    FC_dataGridView.Rows[n].Cells[7].Value = datavalue[0][5];
                    FC_dataGridView.Rows[n].Cells[8].Value = datavalue[0][6];
                    FC_dataGridView.Rows[n].Cells[9].Value = datavalue[0][7];
                }
            }
            SourceImageMake(install); //연료 이미지
            GenImageMake(install); //생산설비 이미지



            FCTypeComboBox.Text = SupplyType;
            string _supply = TypeCombo(SupplyType);
            SupplyTypeImage(_supply);

            Start_comboBox.Text = StartTime;
            End_comboBox.Text = EndTime;
            UseTime_TextBox.Text = Usehour;
            Week_comboBox.Text = Useday;
            if (WListNonsplit != null)
            {
                string[] token = WListNonsplit.Split();
                W_textBox.Text = token[0] + "외" + (token.Length - 1).ToString() + "개";
            }
            if (HListNonsplit != null)
            {
                string[] token = HListNonsplit.Split();
                H_textBox.Text = token[0] + "외" + (token.Length - 1).ToString() + "개";
            }
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
                SupplypictureBox.Visible = true;
                SupplypictureBox.Size = new System.Drawing.Size(350, 220);
                SupplypictureBox.Location = new Point(0, 0);
                SupplypictureBox.Load(Program.gPath + Image[0][0]);
                SupplypictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            }
            else
            {
                SupplypictureBox.Visible = false;
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
            FCSList namelist = new FCSList("급탕", Num);
            DialogResult result = namelist.ShowDialog();
            if (result == DialogResult.OK && namelist.SelectFCList != null)
            {
                WListNonsplit = namelist.SelectFCList;
                string[] token = WListNonsplit.Split();
                W_textBox.Text = token[0] + "외" + (token.Length - 1).ToString() + "개";
            }
        }

        private void H_button_Click(object sender, EventArgs e)
        {
            H_textBox.Text = null;
            FCSList namelist = new FCSList("난방", Num);
            DialogResult result = namelist.ShowDialog();
            if (result == DialogResult.OK && namelist.SelectFCList != null)
            {
                HListNonsplit = namelist.SelectFCList;
                string[] token = HListNonsplit.Split();
                H_textBox.Text = token[0] + "외" + (token.Length - 1).ToString() + "개";
            }
        }

        private void deletebutton_Click(object sender, EventArgs e)
        {
            int SelectRow;
            for (int i = 0; i < FC_dataGridView.Rows.Count; i++)
            {
                if (Convert.ToBoolean(FC_dataGridView.Rows[i].Cells[0].Value))
                {
                    SelectRow = i;
                    FC_dataGridView.Rows.Remove(FC_dataGridView.Rows[SelectRow]);
                }
            }
        }

        private void GeneralPanel_Paint(object sender, PaintEventArgs e)
        {
            GeneralPanel.Location = new Point(1, 3);
            //Panel p = (Panel)sender;
            //ControlPaint.DrawBorder(e.Graphics, p.DisplayRectangle, Color.FromArgb(32, 77, 112), ButtonBorderStyle.Solid);
        }

    }
}
