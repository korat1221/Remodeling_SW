using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.Entity.Core.Metadata.Edm;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static main.DB;
using System.Xml.Linq;
using static System.ComponentModel.Design.ObjectSelectorEditor;
using main.subcontents.HeatingSystem;
using main.subcontents.ConstructionWall;
using main.subcontents.EquipmentList;
using System.Security.Policy;

namespace main.contents
{
    public partial class EquipmentList : Form
    {
        DataGridViewCheckBoxColumn Boiler_checkBoxColumn = new DataGridViewCheckBoxColumn();
        DataGridViewCheckBoxColumn Pump_checkBoxColumn = new DataGridViewCheckBoxColumn();
        DataGridViewCheckBoxColumn ce_checkBoxColumn = new DataGridViewCheckBoxColumn();
        DataGridViewCheckBoxColumn Solar_checkBoxColumn = new DataGridViewCheckBoxColumn();
        int Boiler_SelectRow, Pump_SelectRow, ce_SelectRow, Solar_SelectRow;


        public EquipmentList()
        {
            InitializeComponent();



            Program.DB.initTable(DB.type.ProjDB, "User_Boiler");
            Program.DB.initTable(DB.type.ProjDB, "User_Pump");
            Program.DB.initTable(DB.type.ProjDB, "User_ce");

            string[][] Image = Program.DB.getValue(DB.type.BaseDB_HCneed, "메뉴아이콘", "하위메뉴아이콘", "하위메뉴명 = '장비일람표'");
            Icon_pictureBox.Load(Program.gPath + Image[0][0]);
            Icon_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            Create_Boiler_Table();
            Create_Pump_Table();
            Create_ce_Table();
            Load_Boiler();
            Load_Pump();
            Load_ce();



        }

        private void GeneralPanel_Paint(object sender, PaintEventArgs e)
        {
            Panel p = (Panel)sender;
            ControlPaint.DrawBorder(e.Graphics, p.DisplayRectangle, Color.FromArgb(153, 180, 209), ButtonBorderStyle.Solid);
        }

        ///////////////////////////////////////////////////보일러/////////////////////////////////////////////////////////////////
        public void Create_Boiler_Table()
        {
            new StackedHeaderDecorator(Boiler_dataGridView, DataGridViewAutoSizeColumnsMode.Fill);
            Boiler_dataGridView.Columns.Clear();
            Boiler_checkBoxColumn.HeaderText = "선택";
            Boiler_checkBoxColumn.Name = "check";
            Boiler_dataGridView.Columns.Add(Boiler_checkBoxColumn);

            Boiler_dataGridView.Columns.Add("A1", "번호");
            Boiler_dataGridView.Columns.Add("A2", "DB유형");
            Boiler_dataGridView.Columns.Add("A3", "명칭");
            Boiler_dataGridView.Columns.Add("A4", "난방/급탕");
            Boiler_dataGridView.Columns.Add("A5", "연료");
            Boiler_dataGridView.Columns.Add("A6", "Type");
            Boiler_dataGridView.Columns.Add("A7", "용량" + Environment.NewLine + "[kW]");
            Boiler_dataGridView.Columns.Add("A8", "효율.전부하효율" + Environment.NewLine + "[%]");
            Boiler_dataGridView.Columns.Add("A9", "효율.부분부하효율" + Environment.NewLine + "[%]");
            Boiler_dataGridView.Columns.Add("A10", "전력.소비전력" + Environment.NewLine + "[W]");
            Boiler_dataGridView.Columns.Add("A11", "전력.대기전력" + Environment.NewLine + "[W]");
            Boiler_dataGridView.Columns.Add("A12", "대수" + Environment.NewLine + "[EA]");
            Boiler_dataGridView.Columns[0].Width = 40;
            Boiler_dataGridView.Columns[1].Width = 60;
            Boiler_dataGridView.Columns[2].Width = 60;
            Boiler_dataGridView.Columns[3].Width = 100;
            Boiler_dataGridView.Columns[4].Width = 90;
            Boiler_dataGridView.Columns[5].Width = 60;
            Boiler_dataGridView.Columns[6].Width = 130;
            Boiler_dataGridView.Columns[8].Width = 80;
            Boiler_dataGridView.Columns[9].Width = 80;
            Boiler_dataGridView.Columns[10].Width = 50;
            Boiler_dataGridView.Columns[11].Width = 50;



            //Boiler_dataGridView.ColumnCount = 13;
            //Boiler_dataGridView.Columns[1].HeaderText = "번호";
            //Boiler_dataGridView.Columns[2].HeaderText = "DB유형";
            //Boiler_dataGridView.Columns[3].HeaderText = "명칭";
            //Boiler_dataGridView.Columns[4].HeaderText = "난방/급탕";
            //Boiler_dataGridView.Columns[5].HeaderText = "연료";
            //Boiler_dataGridView.Columns[6].HeaderText = "Type";
            //Boiler_dataGridView.Columns[7].HeaderText = "용량" + Environment.NewLine + "[kW]";
            //Boiler_dataGridView.Columns[8].HeaderText = "전부하효율" + Environment.NewLine + "[%]";
            //Boiler_dataGridView.Columns[9].HeaderText = "부분부하효율" + Environment.NewLine + "[%]";
            //Boiler_dataGridView.Columns[10].HeaderText = "소비전력" + Environment.NewLine + "[W]";
            //Boiler_dataGridView.Columns[11].HeaderText = "대기전력" + Environment.NewLine + "[W]";
            //Boiler_dataGridView.Columns[12].HeaderText = "대수" + Environment.NewLine + "[EA]";
        }

        private void UserBoiler_Add_button_Click(object sender, EventArgs e)
        {
            int nRow = Boiler_dataGridView.Rows.Add();
            Load_Boiler_Num();

            Boiler_dataGridView.Rows[nRow].Cells[2].Value = "도면";

            DataGridViewComboBoxCell 난방급탕Combo = new DataGridViewComboBoxCell();
            난방급탕Combo.Items.Add("난방");
            난방급탕Combo.Items.Add("급탕");
            난방급탕Combo.Items.Add("난방+급탕");
            Boiler_dataGridView.Rows[nRow].Cells[4] = 난방급탕Combo;


            DataGridViewComboBoxCell 연료Combo = new DataGridViewComboBoxCell();
            연료Combo.Items.Add("LNG");
            연료Combo.Items.Add("LPG");
            연료Combo.Items.Add("기름");
            연료Combo.Items.Add("펠릿");
            연료Combo.Items.Add("전기");
            Boiler_dataGridView.Rows[nRow].Cells[5] = 연료Combo;


            //Boiler_dataGridView.Rows[nRow].Cells[3].Style.BackColor = SystemColors.Info;
            //Boiler_dataGridView.Rows[nRow].Cells[6].Style.BackColor = SystemColors.Control;
            //for (int k = 7; k < 13; k++)
            //{
            //    Boiler_dataGridView.Rows[nRow].Cells[k].Style.BackColor = SystemColors.Info;
            //}
        }

        private void DefaultBoiler_Add_button_Click(object sender, EventArgs e)
        {
            ArrayList SelectBoiler = new ArrayList();
            int nRow = Boiler_dataGridView.Rows.Add();
            Load_Boiler_Num();
            Boiler_dataGridView.Rows[nRow].Cells[2].Value = "기본";

            DataGridViewComboBoxCell 난방급탕Combo = new DataGridViewComboBoxCell();
            난방급탕Combo.Items.Add("난방");
            난방급탕Combo.Items.Add("급탕");
            난방급탕Combo.Items.Add("난방+급탕");
            Boiler_dataGridView.Rows[nRow].Cells[4] = 난방급탕Combo;

            Heating_Boiler heating_Boiler = new Heating_Boiler("기본DB 적용", null);
            DialogResult result = heating_Boiler.ShowDialog();
            if (result == DialogResult.OK)
            {
                try
                {
                    if (heating_Boiler.SelectBoiler != null)
                    {

                        string[] token = heating_Boiler.SelectBoiler.Split(',');
                        SelectBoiler.Clear();
                        foreach (var item in token)
                        {
                            SelectBoiler.Add(item.ToString());
                        }
                        string[][] Value; Double[][] Value2;
                        String 내용;
                        Value = Program.DB.getValue(DB.type.BaseDB_Heating, "보일러", "제품명,연료,종류,전부하효율,부분부하효율,소비전력,대기전력", "번호 = '" + SelectBoiler[0].ToString() + "'");
                        //  Value2 = Program.DB.getValueDouble(DB.type.BaseDB_Heating, "보일러", "대기전력", "번호 = '" + SelectBoiler[0].ToString() + "'");
                        String name = Value[0][0];

                        //         Boiler_dataGridView.Rows[nRow].Cells[3].Style.BackColor = SystemColors.Info;
                        if (Value[0][1] == "가스")
                        {
                            DataGridViewComboBoxCell 연료Combo = new DataGridViewComboBoxCell();
                            연료Combo.Items.Add("LNG");
                            연료Combo.Items.Add("LPG");
                            Boiler_dataGridView.Rows[nRow].Cells[5] = 연료Combo;
                        }
                        else { Boiler_dataGridView.Rows[nRow].Cells[5].Value = Value[0][1]; }
                        Boiler_dataGridView.Rows[nRow].Cells[6].Value = Value[0][2];

                        //            Boiler_dataGridView.Rows[nRow].Cells[7].Style.BackColor = SystemColors.Info;

                        Boiler_dataGridView.Rows[nRow].Cells[8].Value = Convert.ToDouble(Value[0][3]) * 100;
                        Boiler_dataGridView.Rows[nRow].Cells[9].Value = Convert.ToDouble(Value[0][4]) * 100;
                        Boiler_dataGridView.Rows[nRow].Cells[10].Value = Value[0][5];
                        Boiler_dataGridView.Rows[nRow].Cells[11].Value = Value[0][6];
                        //         Boiler_dataGridView.Rows[nRow].Cells[12].Style.BackColor = SystemColors.Info;
                    }
                }
                catch { }
            }
        }

        private void Boiler_dataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (e.ColumnIndex == 5)
                {
                    if (Boiler_dataGridView.Rows[e.RowIndex].Cells[2].Value.ToString() == "도면")
                    { Load_Boiler_Type(e.RowIndex); }
                }
                else if (e.ColumnIndex == 7 || e.ColumnIndex == 8)
                {
                    if (Boiler_dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                    {
                        if (Convert.ToDouble(Boiler_dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value) < 1)
                        {
                            MessageBox.Show("퍼센트 단위로 입력하세요.(Ex : 90.1% ⇒ 90.1)");
                            Boiler_dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = null;
                        }
                    }
                }
                //if (Boiler_dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                //{
                //    Boiler_dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.White;
                //}
            }
        }

        private void Load_Boiler_Type(int nRow)
        {
            DataGridViewComboBoxCell TypeCombo = new DataGridViewComboBoxCell();

            switch (Boiler_dataGridView.Rows[nRow].Cells[5].Value)
            {
                case "LPG":
                    TypeCombo.Items.Clear();
                    TypeCombo.Items.Add("콘덴싱가스보일러");
                    TypeCombo.Items.Add("일반가스보일러");
                    break;
                case "LNG":
                    TypeCombo.Items.Clear();
                    TypeCombo.Items.Add("콘덴싱가스보일러");
                    TypeCombo.Items.Add("일반가스보일러");
                    break;
                case "기름":
                    TypeCombo.Items.Clear();
                    TypeCombo.Items.Add("콘덴싱기름보일러");
                    TypeCombo.Items.Add("일반기름보일러");
                    break;
                case "펠릿":
                    TypeCombo.Items.Clear();
                    TypeCombo.Items.Add("펠릿콘덴싱보일러");
                    TypeCombo.Items.Add("펠릿노통형보일러");
                    break;
                case "전기":
                    TypeCombo.Items.Clear();
                    TypeCombo.Items.Add("전기보일러");
                    break;
            }
            Boiler_dataGridView.Rows[nRow].Cells[6] = TypeCombo;
        }

        private void Boiler_Remove_button_Click(object sender, EventArgs e)
        {
            Boiler_dataGridView.Rows.Remove(Boiler_dataGridView.Rows[Boiler_SelectRow]);
            Load_Boiler_Num();
        }

        private void Boiler_Copy_button_Click(object sender, EventArgs e)
        {
            int nRow = Boiler_dataGridView.Rows.Add();
            Load_Boiler_Num();
            if (Boiler_dataGridView.Rows[Boiler_SelectRow].Cells[2].Value == "도면")
            {
                DataGridViewComboBoxCell 연료Combo = new DataGridViewComboBoxCell();
                연료Combo.Items.Add("LNG");
                연료Combo.Items.Add("LPG");
                연료Combo.Items.Add("기름");
                연료Combo.Items.Add("펠릿");
                연료Combo.Items.Add("전기");
                Boiler_dataGridView.Rows[nRow].Cells[5] = 연료Combo;
            }

            DataGridViewComboBoxCell 난방급탕Combo = new DataGridViewComboBoxCell();
            난방급탕Combo.Items.Add("난방");
            난방급탕Combo.Items.Add("급탕");
            난방급탕Combo.Items.Add("난방+급탕");
            Boiler_dataGridView.Rows[nRow].Cells[4] = 난방급탕Combo;

            for (int k = 2; k < 13; k++)
            {
                if (Boiler_dataGridView.Rows[Boiler_SelectRow].Cells[k].Value != null)
                {
                    Boiler_dataGridView.Rows[nRow].Cells[k].Value = Boiler_dataGridView.Rows[Pump_SelectRow].Cells[k].Value;
                    //           Boiler_dataGridView.Rows[nRow].Cells[k].Style.BackColor = Color.White;
                }
                //else
                //{
                //    Boiler_dataGridView.Rows[nRow].Cells[k].Style.BackColor = SystemColors.Info;
                //}
            }
            if (Boiler_dataGridView.Rows[Boiler_SelectRow].Cells[3].Value != null)
            {
                Boiler_dataGridView.Rows[nRow].Cells[3].Value = Boiler_dataGridView.Rows[Boiler_SelectRow].Cells[3].Value.ToString() + "_복사";
            }
        }
        private void Boiler_dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                Boiler_dataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
                Boiler_SelectRow = e.RowIndex;
                DataGridViewRow row = Boiler_dataGridView.Rows[Boiler_SelectRow];
                DataGridViewRow row2;
                for (int k = 0; k < Boiler_dataGridView.RowCount; k++)
                {
                    if (k != row.Index)
                    {
                        Boiler_dataGridView.Rows[k].Cells[0].Value = false;
                        row2 = Boiler_dataGridView.Rows[k];
                        row2.DefaultCellStyle.BackColor = SystemColors.Window;
                        row2.DefaultCellStyle.ForeColor = SystemColors.WindowText;
                    }
                    else
                    {
                        row.DefaultCellStyle.BackColor = SystemColors.GradientInactiveCaption;
                        row.DefaultCellStyle.ForeColor = SystemColors.WindowText;
                        row = Boiler_dataGridView.Rows[e.RowIndex];
                    }
                }
            }
        }

        private void Load_Boiler_Num()
        {
            for (int k = 0; k < Boiler_dataGridView.RowCount; k++)
            {
                if (k + 1 < 10)
                { Boiler_dataGridView.Rows[k].Cells[1].Value = "UBS0" + (k + 1).ToString(); }
                else { Boiler_dataGridView.Rows[k].Cells[1].Value = "UBS" + (k + 1).ToString(); }
            }
        }

        private void Boiler_Save_button_Click(object sender, EventArgs e)
        {
            Program.DB.deleteValue(DB.type.ProjDB, "User_Boiler", "");

            for (int k = 0; k < Boiler_dataGridView.RowCount; k++)
            {
                String[] Value = new String[12];
                for (int i = 1; i < 13; i++)
                {
                    if (Boiler_dataGridView.Rows[k].Cells[i].Value != null)
                    { Value[i - 1] = Boiler_dataGridView.Rows[k].Cells[i].Value.ToString(); }
                    else { Value[i - 1] = ""; }
                }
                Program.DB.setValue(DB.type.ProjDB, "User_Boiler", "번호,DB유형,명칭,난방급탕,연료,Type,용량,전부하효율,부분부하효율,소비전력,대기전력,대수",
                "'" + Value[0] + "','"
                 + Value[1] + "','" + Value[2] + "','" + Value[3] + "','" + Value[4] + "','" + Value[5] + "','" + Value[6] + "','" + Value[7] + "','" + Value[8] + "','" + Value[9] + "','" + Value[10] + "','"
                 + Value[11]
                 + "'", "번호");
            }
            MessageBox.Show("저장되었습니다.");
        }

        private void Load_Boiler()
        {
            try
            {
                string[][] User_Value = Program.DB.getValue(DB.type.ProjDB, "User_Boiler", "번호,명칭,연료,Type,용량,전부하효율,부분부하효율,소비전력,대기전력,DB유형,난방급탕,대수", "");
                string 용량 = "", 전부하효율 = "", 부분부하효율 = "", 소비전력 = "", 대기전력 = "";
                for (int n = 0; n < User_Value.Length; n++)
                {
                    Boiler_dataGridView.Rows.Add();
                    int nRow = Boiler_dataGridView.Rows.Count - 1;

                    if (User_Value[n][4] != null && User_Value[n][4] != "")
                    {
                        double a = Convert.ToDouble(User_Value[n][4]);
                        용량 = string.Format("{0:F1}", Convert.ToDouble(User_Value[n][4]));
                    }
                    if (User_Value[n][5] != null && User_Value[n][5] != "")
                    {
                        전부하효율 = string.Format("{0:F1}", Convert.ToDouble(User_Value[n][5]));
                    }
                    if (User_Value[n][6] != null && User_Value[n][6] != "")
                    {
                        부분부하효율 = string.Format("{0:F1}", Convert.ToDouble(User_Value[n][6]));
                    }
                    if (User_Value[n][7] != null && User_Value[n][7] != "")
                    {
                        소비전력 = string.Format("{0:F0}", Convert.ToDouble(User_Value[n][7]));
                    }
                    if (User_Value[n][8] != null && User_Value[n][8] != "")
                    {
                        대기전력 = string.Format("{0:F0}", Convert.ToDouble(User_Value[n][8]));
                    }
                    Boiler_dataGridView.Rows[nRow].Cells[7].Value = 용량;
                    Boiler_dataGridView.Rows[nRow].Cells[8].Value = 전부하효율;
                    Boiler_dataGridView.Rows[nRow].Cells[9].Value = 부분부하효율;
                    Boiler_dataGridView.Rows[nRow].Cells[10].Value = 소비전력;
                    Boiler_dataGridView.Rows[nRow].Cells[11].Value = 대기전력;
                    Boiler_dataGridView.Rows[nRow].Cells[1].Value = User_Value[n][0]; //번호
                    Boiler_dataGridView.Rows[nRow].Cells[2].Value = User_Value[n][9]; //DB유형
                    Boiler_dataGridView.Rows[nRow].Cells[3].Value = User_Value[n][1]; //명칭
                    Boiler_dataGridView.Rows[nRow].Cells[4].Value = User_Value[n][10]; //난방급탕
                    Boiler_dataGridView.Rows[nRow].Cells[5].Value = User_Value[n][2]; //연료
                    Boiler_dataGridView.Rows[nRow].Cells[6].Value = User_Value[n][3]; //Type
                    Boiler_dataGridView.Rows[nRow].Cells[12].Value = User_Value[n][11]; //대수
                }
            }
            catch { }
        }
        ///////////////////////////////////////////////////펌프/////////////////////////////////////////////////////////////////
        public void Create_Pump_Table()
        {
            new StackedHeaderDecorator(Pump_dataGridView, DataGridViewAutoSizeColumnsMode.Fill);
            Pump_dataGridView.Columns.Clear();
            Pump_checkBoxColumn.HeaderText = "선택";
            Pump_checkBoxColumn.Name = "check";
            Pump_dataGridView.Columns.Add(Pump_checkBoxColumn);

            Pump_dataGridView.Columns.Add("P1", "번호");
            Pump_dataGridView.Columns.Add("P2", "명칭");
            Pump_dataGridView.Columns.Add("P3", "종류");
            Pump_dataGridView.Columns.Add("P4", "A효율" + Environment.NewLine + "[%]");
            Pump_dataGridView.Columns.Add("P5", "B효율" + Environment.NewLine + "[%]");
            Pump_dataGridView.Columns.Add("P6", "유량" + Environment.NewLine + "[CMH]");
            Pump_dataGridView.Columns.Add("P7", "양정" + Environment.NewLine + "[m]");
            Pump_dataGridView.Columns.Add("P8", "");
            Pump_dataGridView.Columns.Add("P9", "동력" + Environment.NewLine + "[kW]");
            Pump_dataGridView.Columns.Add("P10", "");
            Pump_dataGridView.Columns.Add("P11", "대수" + Environment.NewLine + "[EA]");
            Pump_dataGridView.Columns[0].Width = 40;
            Pump_dataGridView.Columns[1].Width = 60;
            Pump_dataGridView.Columns[2].Width = 130;
            Pump_dataGridView.Columns[3].Width = 130;
            Pump_dataGridView.Columns[7].Width = 100;
            Pump_dataGridView.Columns[8].Width = 30;
            Pump_dataGridView.Columns[9].Width = 100;
            Pump_dataGridView.Columns[10].Width = 30;

            //Pump_dataGridView.ColumnCount = 12;
            //Pump_dataGridView.Columns[1].HeaderText = "번호";
            //Pump_dataGridView.Columns[2].HeaderText = "명칭";
            //Pump_dataGridView.Columns[3].HeaderText = "종류";
            //Pump_dataGridView.Columns[4].HeaderText = "A효율" + Environment.NewLine + "[%]";
            //Pump_dataGridView.Columns[5].HeaderText = "B효율" + Environment.NewLine + "[%]";
            //Pump_dataGridView.Columns[6].HeaderText = "유량" + Environment.NewLine + "[CMH]";
            //Pump_dataGridView.Columns[7].HeaderText = "양정" + Environment.NewLine + "[m]";
            //Pump_dataGridView.Columns[8].HeaderText = "계산";
            //Pump_dataGridView.Columns[9].HeaderText = "동력" + Environment.NewLine + "[kW]";
            //Pump_dataGridView.Columns[10].HeaderText = "계산";
            //Pump_dataGridView.Columns[11].HeaderText = "대수" + Environment.NewLine + "[EA]";
        }

        private void Pump_Add_button_Click(object sender, EventArgs e)
        {
            int nRow = Pump_dataGridView.Rows.Add();
            Load_Pump_Num();
            Pump_dataGridView.Rows[nRow].Cells[2].Style.BackColor = SystemColors.Info;

            DataGridViewComboBoxCell 펌프종류comboBox = new DataGridViewComboBoxCell();
            펌프종류comboBox.Items.Add("냉수순환펌프");
            펌프종류comboBox.Items.Add("온수순환펌프");
            펌프종류comboBox.Items.Add("냉온수순환펌프");
            펌프종류comboBox.Items.Add("급탕순환펌프");
            펌프종류comboBox.Items.Add("냉각수순환펌프");
            펌프종류comboBox.Items.Add("지열순환펌프");
            Pump_dataGridView.Rows[nRow].Cells[3] = 펌프종류comboBox;

            DataGridViewButtonCell PumpHead_ButtonCell = new DataGridViewButtonCell();
            Pump_dataGridView.Rows[nRow].Cells[8] = PumpHead_ButtonCell;
            PumpHead_ButtonCell.Value = "+";
            DataGridViewButtonCell PumpPower_ButtonCell = new DataGridViewButtonCell();
            Pump_dataGridView.Rows[nRow].Cells[10] = PumpPower_ButtonCell;
            PumpPower_ButtonCell.Value = "+";
            for (int k = 4; k < 12; k++)
            {
                Pump_dataGridView.Rows[nRow].Cells[k].Style.BackColor = SystemColors.Info;
            }
            Pump_dataGridView.Rows[nRow].Cells[8].Style.BackColor = Color.White;
            Pump_dataGridView.Rows[nRow].Cells[10].Style.BackColor = Color.White;
        }

        private void Pump_dataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (e.ColumnIndex == 4 || e.ColumnIndex == 5)
                {
                    if (Pump_dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                    {
                        if (Convert.ToDouble(Pump_dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value) < 1)
                        {
                            MessageBox.Show("퍼센트 단위로 입력하세요.(Ex : 90.1% ⇒ 90.1)");
                            Pump_dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = null;
                        }
                    }
                }
                if (Pump_dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                {
                    Pump_dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.White;
                }
            }
        }

        private void Pump_Remove_button_Click(object sender, EventArgs e)
        {
            Pump_dataGridView.Rows.Remove(Pump_dataGridView.Rows[Pump_SelectRow]);
            Load_Pump_Num();
        }

        private void Pump_Copy_button_Click(object sender, EventArgs e)
        {
            int nRow = Pump_dataGridView.Rows.Add();
            Load_Pump_Num();
            DataGridViewComboBoxCell 펌프종류comboBox = new DataGridViewComboBoxCell();
            펌프종류comboBox.Items.Add("냉수순환펌프");
            펌프종류comboBox.Items.Add("온수순환펌프");
            펌프종류comboBox.Items.Add("냉온수순환펌프");
            펌프종류comboBox.Items.Add("급탕순환펌프");
            펌프종류comboBox.Items.Add("냉각수순환펌프");
            펌프종류comboBox.Items.Add("지열순환펌프");
            Pump_dataGridView.Rows[nRow].Cells[3] = 펌프종류comboBox;

            DataGridViewButtonCell PumpHead_ButtonCell = new DataGridViewButtonCell();
            Pump_dataGridView.Rows[nRow].Cells[8] = PumpHead_ButtonCell;
            PumpHead_ButtonCell.Value = "+";
            DataGridViewButtonCell PumpPower_ButtonCell = new DataGridViewButtonCell();
            Pump_dataGridView.Rows[nRow].Cells[10] = PumpPower_ButtonCell;
            PumpPower_ButtonCell.Value = "+";

            for (int k = 2; k < 12; k++)
            {
                if (Pump_dataGridView.Rows[Pump_SelectRow].Cells[k].Value != null)
                {
                    Pump_dataGridView.Rows[nRow].Cells[k].Value = Pump_dataGridView.Rows[Pump_SelectRow].Cells[k].Value;
                    Pump_dataGridView.Rows[nRow].Cells[k].Style.BackColor = Color.White;
                }
                else
                {
                    Pump_dataGridView.Rows[nRow].Cells[k].Style.BackColor = SystemColors.Info;
                }
            }
            if (Pump_dataGridView.Rows[Pump_SelectRow].Cells[2].Value != null)
            {
                Pump_dataGridView.Rows[nRow].Cells[2].Value = Pump_dataGridView.Rows[Pump_SelectRow].Cells[2].Value.ToString() + "_복사";
            }
            Pump_dataGridView.Rows[nRow].Cells[8].Style.BackColor = Color.White;
            Pump_dataGridView.Rows[nRow].Cells[10].Style.BackColor = Color.White;
        }
        private void Pump_dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                Pump_dataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
                Pump_SelectRow = e.RowIndex;
                DataGridViewRow row = Pump_dataGridView.Rows[Pump_SelectRow];
                DataGridViewRow row2;
                for (int k = 0; k < Pump_dataGridView.RowCount; k++)
                {
                    if (k != row.Index)
                    {
                        Pump_dataGridView.Rows[k].Cells[0].Value = false;
                        row2 = Pump_dataGridView.Rows[k];
                        row2.DefaultCellStyle.BackColor = SystemColors.Window;
                        row2.DefaultCellStyle.ForeColor = SystemColors.WindowText;
                    }
                    else
                    {
                        row.DefaultCellStyle.BackColor = SystemColors.GradientInactiveCaption;
                        row.DefaultCellStyle.ForeColor = SystemColors.WindowText;
                        row = Pump_dataGridView.Rows[e.RowIndex];
                    }
                }

                if (e.ColumnIndex == 8)
                {
                    double Lmax; double PumpHead;
                    PumpCal pumpcal_form = new PumpCal(Pump_dataGridView.Rows[e.RowIndex].Cells[1].Value.ToString());
                    DialogResult result = pumpcal_form.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        Lmax = pumpcal_form.Lmax;
                        PumpHead = pumpcal_form.PumpHead;
                        Pump_dataGridView.Rows[e.RowIndex].Cells[7].Value = String.Format("{0:F1}", PumpHead);
                    }
                }
                if (e.ColumnIndex == 10)
                {
                    if (Pump_dataGridView.Rows[e.RowIndex].Cells[5].Value == null)
                    {
                        MessageBox.Show("효율을 입력하세요.");
                    }
                    else if (Pump_dataGridView.Rows[e.RowIndex].Cells[6].Value == null)
                    {
                        MessageBox.Show("유량을 입력하세요.");
                    }
                    else if (Pump_dataGridView.Rows[e.RowIndex].Cells[7].Value == null)
                    {
                        MessageBox.Show("양정을 입력하세요.");
                    }
                    else
                    {
                        double 효율 = Convert.ToDouble(Pump_dataGridView.Rows[e.RowIndex].Cells[5].Value);
                        double 유량 = Convert.ToDouble(Pump_dataGridView.Rows[e.RowIndex].Cells[6].Value);
                        double 양정 = Convert.ToDouble(Pump_dataGridView.Rows[e.RowIndex].Cells[7].Value);
                        double Power;
                        Power = (양정 * 1000 * 9.81) * 유량 / 3600 / (효율 / 100);
                        Pump_dataGridView.Rows[e.RowIndex].Cells[9].Value = String.Format("{0:F1}", Power);
                    }
                }
            }
        }

        private void Load_Pump_Num()
        {
            for (int k = 0; k < Pump_dataGridView.RowCount; k++)
            {
                if (k + 1 < 10)
                { Pump_dataGridView.Rows[k].Cells[1].Value = "PUP0" + (k + 1).ToString(); }
                else { Pump_dataGridView.Rows[k].Cells[1].Value = "PUP" + (k + 1).ToString(); }
            }
        }

        private void Pump_Save_button_Click(object sender, EventArgs e)
        {
            Program.DB.deleteValue(DB.type.ProjDB, "User_Pump", "");

            for (int k = 0; k < Pump_dataGridView.RowCount; k++)
            {
                String[] Value = new String[9];
                for (int i = 1; i < 7; i++)
                {
                    if (Pump_dataGridView.Rows[k].Cells[i].Value != null)
                    { Value[i - 1] = Pump_dataGridView.Rows[k].Cells[i].Value.ToString(); }
                    else { Value[i - 1] = ""; }
                }
                //동력
                if (Pump_dataGridView.Rows[k].Cells[9].Value != null)
                { Value[6] = Pump_dataGridView.Rows[k].Cells[9].Value.ToString(); }
                else { Value[6] = ""; }
                //양정
                if (Pump_dataGridView.Rows[k].Cells[7].Value != null)
                { Value[7] = Pump_dataGridView.Rows[k].Cells[7].Value.ToString(); }
                else { Value[7] = ""; }
                //대수
                if (Pump_dataGridView.Rows[k].Cells[11].Value != null)
                { Value[8] = Pump_dataGridView.Rows[k].Cells[11].Value.ToString(); }
                else { Value[8] = ""; }

                Program.DB.setValue(DB.type.ProjDB, "User_Pump", "번호,명칭,종류,A효율,B효율,유량,동력,양정,대수",
                "'" + Value[0] + "','"
                 + Value[1] + "','" + Value[2] + "','" + Value[3] + "','" + Value[4] + "','" + Value[5] + "','" + Value[6] + "','" + Value[7] + "','"
                 + Value[8]
                 + "'", "번호");
            }
            MessageBox.Show("저장되었습니다.");
        }

        private void Load_Pump()
        {
            try
            {
                string[][] Value = Program.DB.getValue(DB.type.ProjDB, "User_Pump", "번호,명칭,종류,A효율,B효율,유량,동력,양정,대수", "");
                for (int n = 0; n < Value.Length; n++)
                {
                    string A효율 = "", B효율 = "", 유량 = "", 동력 = "", 양정 = "";
                    Pump_dataGridView.Rows.Add();
                    int nRow = Pump_dataGridView.Rows.Count - 1;

                    if (Value[n][3] != null && Value[n][3] != "")
                    {
                        A효율 = string.Format("{0:F1}", Convert.ToDouble(Value[n][3]));
                    }
                    if (Value[n][4] != null && Value[n][4] != "")
                    {
                        B효율 = string.Format("{0:F1}", Convert.ToDouble(Value[n][4]));
                    }
                    if (Value[n][5] != null && Value[n][5] != "")
                    {
                        유량 = string.Format("{0:F0}", Convert.ToDouble(Value[n][5]));
                    }
                    if (Value[n][6] != null && Value[n][6] != "")
                    {
                        동력 = string.Format("{0:F0}", Convert.ToDouble(Value[n][6]));
                    }
                    if (Value[n][7] != null && Value[n][7] != "")
                    {
                        양정 = string.Format("{0:F0}", Convert.ToDouble(Value[n][7]));
                    }

                    Pump_dataGridView.Rows[nRow].Cells[1].Value = Value[n][0];
                    Pump_dataGridView.Rows[nRow].Cells[2].Value = Value[n][1];
                    Pump_dataGridView.Rows[nRow].Cells[3].Value = Value[n][2];
                    Pump_dataGridView.Rows[nRow].Cells[4].Value = A효율;
                    Pump_dataGridView.Rows[nRow].Cells[5].Value = B효율;
                    Pump_dataGridView.Rows[nRow].Cells[6].Value = 유량;
                    Pump_dataGridView.Rows[nRow].Cells[7].Value = 양정;
                    Pump_dataGridView.Rows[nRow].Cells[9].Value = 동력;
                    Pump_dataGridView.Rows[nRow].Cells[11].Value = Value[n][8];
                    DataGridViewButtonCell PumpHead_ButtonCell = new DataGridViewButtonCell();
                    Pump_dataGridView.Rows[nRow].Cells[8] = PumpHead_ButtonCell;
                    PumpHead_ButtonCell.Value = "+";
                    DataGridViewButtonCell PumpPower_ButtonCell = new DataGridViewButtonCell();
                    Pump_dataGridView.Rows[nRow].Cells[10] = PumpPower_ButtonCell;
                    PumpPower_ButtonCell.Value = "+";
                }
            }
            catch { }
        }
        ///////////////////////////////////////////////////공급설비/////////////////////////////////////////////////////////////////
        public void Create_ce_Table()
        {
            new StackedHeaderDecorator(ce_dataGridView, DataGridViewAutoSizeColumnsMode.Fill, ce_datagridviewDesign);
            ce_dataGridView.Columns.Clear();
            ce_checkBoxColumn.HeaderText = "선택";
            ce_checkBoxColumn.Name = "check";
            ce_dataGridView.Columns.Add(ce_checkBoxColumn);

            ce_dataGridView.Columns.Add("A1", "번호");
            ce_dataGridView.Columns.Add("A2", "명칭");
            ce_dataGridView.Columns.Add("A3", "난방/냉방");
            ce_dataGridView.Columns.Add("A4", "종류");
            ce_dataGridView.Columns.Add("A5", "용량.[kW]");
            ce_dataGridView.Columns.Add("A6", "소비전력.[kW]");
            ce_dataGridView.Columns.Add("A7", "온도제어방식");
            ce_dataGridView.Columns.Add("A8", "대수.[EA]");
            ce_dataGridView.Columns[0].Width = 40;
            ce_dataGridView.Columns[1].Width = 50;
            ce_dataGridView.Columns[7].Width = 150;
            //ce_dataGridView.ColumnCount = 9;
            //ce_dataGridView.Columns[1].HeaderText = "번호";
            //ce_dataGridView.Columns[2].HeaderText = "명칭";
            //ce_dataGridView.Columns[3].HeaderText = "난방/냉방";
            //ce_dataGridView.Columns[4].HeaderText = "종류";
            //ce_dataGridView.Columns[5].HeaderText = "용량" + Environment.NewLine + "[kW]";
            //ce_dataGridView.Columns[6].HeaderText = "소비전력" + Environment.NewLine + "[kW]";
            //ce_dataGridView.Columns[7].HeaderText = "온도제어방식";
            //ce_dataGridView.Columns[8].HeaderText = "대수" + Environment.NewLine + "[EA]";
        }

        private Boolean ce_datagridviewDesign(DataGridViewCell cell, int column, int row)
        {
            if (ce_dataGridView.Rows[row].Cells[4].Value != null && ce_dataGridView.Rows[row].Cells[4].Value.ToString() == "복사난방")
            {
                if (column == 5 || column == 6 || column == 8)
                {
                    cell.Style.BackColor = Color.FromArgb(255, 255, 255);
                    cell.Style.ForeColor = Color.Black;
                    cell.Style.SelectionBackColor = Color.FromArgb(255, 255, 255);
                    cell.Style.SelectionForeColor = Color.Black;
                    return true;
                }
                else { return false; }
            }
            else return false;
        }


        private void ce_Add_button_Click(object sender, EventArgs e)
        {
            int nRow = ce_dataGridView.Rows.Add();
            Load_ce_Num();

            DataGridViewComboBoxCell 난방냉방comboBox = new DataGridViewComboBoxCell();
            난방냉방comboBox.Items.Add("난방");
            난방냉방comboBox.Items.Add("냉방");
            난방냉방comboBox.Items.Add("냉난방");
            ce_dataGridView.Rows[nRow].Cells[3] = 난방냉방comboBox;

            DataGridViewComboBoxCell 공급설비종류comboBox = new DataGridViewComboBoxCell();
            공급설비종류comboBox.Items.Add("실내기");
            공급설비종류comboBox.Items.Add("방열기");
            공급설비종류comboBox.Items.Add("팬코일유닛");
            공급설비종류comboBox.Items.Add("파워팬유닛");
            공급설비종류comboBox.Items.Add("복사난방");
            ce_dataGridView.Rows[nRow].Cells[4] = 공급설비종류comboBox;


            DataGridViewComboBoxCell 온도제어방식comboBox = new DataGridViewComboBoxCell();
            온도제어방식comboBox.Items.Add("제어 없음");
            온도제어방식comboBox.Items.Add("실별 온도제어");
            온도제어방식comboBox.Items.Add("on-off 자동온도제어");
            온도제어방식comboBox.Items.Add("재실기준 자동온도제어");
            ce_dataGridView.Rows[nRow].Cells[7] = 온도제어방식comboBox;

        }

        private void ce_dataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (ce_dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                {
                    ce_dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.White;
                }
            }
        }

        private void ce_Remove_button_Click(object sender, EventArgs e)
        {

            Load_ce_Num();
        }

        private void ce_Copy_button_Click(object sender, EventArgs e)
        {
            int nRow = ce_dataGridView.Rows.Add();
            Load_ce_Num();
            DataGridViewComboBoxCell 난방냉방comboBox = new DataGridViewComboBoxCell();
            난방냉방comboBox.Items.Add("난방");
            난방냉방comboBox.Items.Add("냉방");
            난방냉방comboBox.Items.Add("냉난방");
            ce_dataGridView.Rows[nRow].Cells[3] = 난방냉방comboBox;

            DataGridViewComboBoxCell 공급설비종류comboBox = new DataGridViewComboBoxCell();
            공급설비종류comboBox.Items.Add("실내기");
            공급설비종류comboBox.Items.Add("방열기");
            공급설비종류comboBox.Items.Add("팬코일유닛");
            공급설비종류comboBox.Items.Add("파워팬유닛");
            공급설비종류comboBox.Items.Add("복사난방");
            ce_dataGridView.Rows[nRow].Cells[4] = 공급설비종류comboBox;


            DataGridViewComboBoxCell 온도제어방식comboBox = new DataGridViewComboBoxCell();
            공급설비종류comboBox.Items.Add("제어 없음");
            공급설비종류comboBox.Items.Add("실별 온도제어");
            공급설비종류comboBox.Items.Add("on-off 자동온도제어");
            공급설비종류comboBox.Items.Add("재실기준 자동온도제어");
            ce_dataGridView.Rows[nRow].Cells[7] = 공급설비종류comboBox;

            for (int k = 2; k < 9; k++)
            {
                if (ce_dataGridView.Rows[ce_SelectRow].Cells[k].Value != null)
                {
                    ce_dataGridView.Rows[nRow].Cells[k].Value = ce_dataGridView.Rows[ce_SelectRow].Cells[k].Value;
                    ce_dataGridView.Rows[nRow].Cells[k].Style.BackColor = Color.White;
                }
                else
                {
                    ce_dataGridView.Rows[nRow].Cells[k].Style.BackColor = SystemColors.Info;
                }
            }
            if (ce_dataGridView.Rows[ce_SelectRow].Cells[2].Value != null)
            {
                ce_dataGridView.Rows[nRow].Cells[2].Value = ce_dataGridView.Rows[ce_SelectRow].Cells[2].Value.ToString() + "_복사";
            }
        }


        private void Load_ce_Num()
        {
            for (int k = 0; k < ce_dataGridView.RowCount; k++)
            {
                if (k + 1 < 10)
                { ce_dataGridView.Rows[k].Cells[1].Value = "CE0" + (k + 1).ToString(); }
                else { ce_dataGridView.Rows[k].Cells[1].Value = "CE" + (k + 1).ToString(); }
            }
        }

        private void ce_Save_button_Click(object sender, EventArgs e)
        {
            Program.DB.deleteValue(DB.type.ProjDB, "User_ce", "");

            for (int k = 0; k < ce_dataGridView.RowCount; k++)
            {
                String[] Value = new String[8];
                for (int i = 1; i < 9; i++)
                {
                    if (ce_dataGridView.Rows[k].Cells[i].Value != null)
                    { Value[i - 1] = ce_dataGridView.Rows[k].Cells[i].Value.ToString(); }
                    else { Value[i - 1] = ""; }
                }
                Program.DB.setValue(DB.type.ProjDB, "User_Boiler", "번호,DB유형,명칭,난방급탕,연료,Type,용량,전부하효율,부분부하효율,소비전력,대기전력,대수",
                "'" + Value[0] + "','"
                 + Value[1] + "','" + Value[2] + "','" + Value[3] + "','" + Value[4] + "','" + Value[5] + "','" + Value[6] + "','" + Value[7] + "','" + Value[8] + "','" + Value[9] + "','" + Value[10] + "','"
                 + Value[11]
                 + "'", "번호");
            }
            MessageBox.Show("저장되었습니다.");
        }
        private void Load_ce()
        {
            try
            {
                String[][] Value = Program.DB.getValue(DB.type.ProjDB, "User_ce", "번호,명칭,난방냉방,종류,용량,소비전력,온도제어방식,대수", "");
                for (int n = 0; n < Value.Length; n++)
                {
                    ce_dataGridView.Rows.Add();
                    int nRow = ce_dataGridView.Rows.Count - 1;
                    DataGridViewComboBoxCell 난방냉방comboBox = new DataGridViewComboBoxCell();
                    난방냉방comboBox.Items.Add("난방");
                    난방냉방comboBox.Items.Add("냉방");
                    난방냉방comboBox.Items.Add("냉난방");
                    ce_dataGridView.Rows[nRow].Cells[3] = 난방냉방comboBox;

                    DataGridViewComboBoxCell 공급설비종류comboBox = new DataGridViewComboBoxCell();
                    공급설비종류comboBox.Items.Add("실내기");
                    공급설비종류comboBox.Items.Add("방열기");
                    공급설비종류comboBox.Items.Add("팬코일유닛");
                    공급설비종류comboBox.Items.Add("파워팬유닛");
                    공급설비종류comboBox.Items.Add("복사난방");
                    ce_dataGridView.Rows[nRow].Cells[4] = 공급설비종류comboBox;


                    DataGridViewComboBoxCell 온도제어방식comboBox = new DataGridViewComboBoxCell();
                    온도제어방식comboBox.Items.Add("제어 없음");
                    온도제어방식comboBox.Items.Add("실별 온도제어");
                    온도제어방식comboBox.Items.Add("on-off 자동온도제어");
                    온도제어방식comboBox.Items.Add("재실기준 자동온도제어");
                    ce_dataGridView.Rows[nRow].Cells[7] = 온도제어방식comboBox;

                    for (int k = 0; k < 8; k++)
                    { ce_dataGridView.Rows[nRow].Cells[k + 1].Value = Value[n][k]; }
                }
            }
            catch { }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            

        }
    }
}
