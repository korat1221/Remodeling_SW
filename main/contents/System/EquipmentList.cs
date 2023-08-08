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
using main.subcontents.ConstructionWall;

namespace main.contents
{
    public partial class EquipmentList : Form
    {
        DataGridViewCheckBoxColumn Boiler_checkBoxColumn = new DataGridViewCheckBoxColumn();
        DataGridViewCheckBoxColumn Pump_checkBoxColumn = new DataGridViewCheckBoxColumn();
        DataGridViewCheckBoxColumn Solar_checkBoxColumn = new DataGridViewCheckBoxColumn();
        int Boiler_SelectRow, Pump_SelectRow, Solar_SelectRow;


        public EquipmentList()
        {
            InitializeComponent();
            Program.DB.initTable(DB.type.ProjDB, "User_Boiler");

            string[][] Image = Program.DB.getValue(DB.type.BaseDB_HCneed, "메뉴아이콘", "하위메뉴아이콘", "하위메뉴명 = '장비일람표'");
            Icon_pictureBox.Load(Program.gPath + Image[0][0]);
            Icon_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            Create_Boiler_Table();
            Create_Pump_Table();
            Create_Solar_Table();
        }

        private void GeneralPanel_Paint(object sender, PaintEventArgs e)
        {
            Panel p = (Panel)sender;
            ControlPaint.DrawBorder(e.Graphics, p.DisplayRectangle, Color.FromArgb(153, 180, 209), ButtonBorderStyle.Solid);
        }

        ///////////////////////////////////////////////////보일러/////////////////////////////////////////////////////////////////
        public void Create_Boiler_Table()
        {
            Boiler_dataGridView.Columns.Clear();
            Boiler_checkBoxColumn.HeaderText = "선택";
            Boiler_checkBoxColumn.Name = "check";
            Boiler_dataGridView.Columns.Add(Boiler_checkBoxColumn);

            Boiler_dataGridView.ColumnCount = 13;
            Boiler_dataGridView.Columns[1].HeaderText = "번호";
            Boiler_dataGridView.Columns[2].HeaderText = "DB유형";
            Boiler_dataGridView.Columns[3].HeaderText = "명칭";
            Boiler_dataGridView.Columns[4].HeaderText = "난방/급탕";
            Boiler_dataGridView.Columns[5].HeaderText = "연료";
            Boiler_dataGridView.Columns[6].HeaderText = "Type";
            Boiler_dataGridView.Columns[7].HeaderText = "용량" + Environment.NewLine + "[kW]";
            Boiler_dataGridView.Columns[8].HeaderText = "전부하효율" + Environment.NewLine + "[%]";
            Boiler_dataGridView.Columns[9].HeaderText = "부분부하효율" + Environment.NewLine + "[%]";
            Boiler_dataGridView.Columns[10].HeaderText = "소비전력" + Environment.NewLine + "[W]";
            Boiler_dataGridView.Columns[11].HeaderText = "대기전력" + Environment.NewLine + "[W]";
            Boiler_dataGridView.Columns[12].HeaderText = "대수" + Environment.NewLine + "[EA]";
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


            Boiler_dataGridView.Rows[nRow].Cells[3].Style.BackColor = SystemColors.Info;
            Boiler_dataGridView.Rows[nRow].Cells[6].Style.BackColor = SystemColors.Control;
            for (int k = 7; k < 13; k++)
            {
                Boiler_dataGridView.Rows[nRow].Cells[k].Style.BackColor = SystemColors.Info;
            }
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

            Heating_Boiler heating_Boiler = new Heating_Boiler("기본DB 적용");
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

                        Boiler_dataGridView.Rows[nRow].Cells[3].Style.BackColor = SystemColors.Info;
                        if (Value[0][1] == "가스")
                        {
                            DataGridViewComboBoxCell 연료Combo = new DataGridViewComboBoxCell();
                            연료Combo.Items.Add("LNG");
                            연료Combo.Items.Add("LPG");
                            Boiler_dataGridView.Rows[nRow].Cells[5] = 연료Combo;
                        }
                        else { Boiler_dataGridView.Rows[nRow].Cells[5].Value = Value[0][1]; }
                        Boiler_dataGridView.Rows[nRow].Cells[6].Value = Value[0][2];

                        Boiler_dataGridView.Rows[nRow].Cells[7].Style.BackColor = SystemColors.Info;

                        Boiler_dataGridView.Rows[nRow].Cells[8].Value = Convert.ToDouble(Value[0][3]) * 100;
                        Boiler_dataGridView.Rows[nRow].Cells[9].Value = Convert.ToDouble(Value[0][4]) * 100;
                        Boiler_dataGridView.Rows[nRow].Cells[10].Value = Value[0][5];
                        Boiler_dataGridView.Rows[nRow].Cells[11].Value = Value[0][6];
                        Boiler_dataGridView.Rows[nRow].Cells[12].Style.BackColor = SystemColors.Info;
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
                if (Boiler_dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                {
                    Boiler_dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.White;
                }
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
                    Boiler_dataGridView.Rows[nRow].Cells[k].Style.BackColor = Color.White;
                }
                else
                {
                    Boiler_dataGridView.Rows[nRow].Cells[k].Style.BackColor = SystemColors.Info;
                }
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

        ///////////////////////////////////////////////////펌프/////////////////////////////////////////////////////////////////
        public void Create_Pump_Table()
        {
            Pump_dataGridView.Columns.Clear();
            Pump_checkBoxColumn.HeaderText = "선택";
            Pump_checkBoxColumn.Name = "check";
            Pump_dataGridView.Columns.Add(Pump_checkBoxColumn);

            Pump_dataGridView.ColumnCount = 10;
            Pump_dataGridView.Columns[1].HeaderText = "번호";
            Pump_dataGridView.Columns[2].HeaderText = "명칭";
            Pump_dataGridView.Columns[3].HeaderText = "종류";
            Pump_dataGridView.Columns[4].HeaderText = "A효율" + Environment.NewLine + "[%]";
            Pump_dataGridView.Columns[5].HeaderText = "B효율" + Environment.NewLine + "[%]";
            Pump_dataGridView.Columns[6].HeaderText = "유량" + Environment.NewLine + "[CMH]";
            Pump_dataGridView.Columns[7].HeaderText = "동력" + Environment.NewLine + "[kW]";
            Pump_dataGridView.Columns[8].HeaderText = "양정" + Environment.NewLine + "[m]";
            Pump_dataGridView.Columns[9].HeaderText = "대수" + Environment.NewLine + "[EA]";
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

            for (int k = 4; k < 10; k++)
            {
                Pump_dataGridView.Rows[nRow].Cells[k].Style.BackColor = SystemColors.Info;
            }
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

            for (int k = 2; k < 10; k++)
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
                for (int i = 1; i < 10; i++)
                {
                    if (Pump_dataGridView.Rows[k].Cells[i].Value != null)
                    { Value[i - 1] = Pump_dataGridView.Rows[k].Cells[i].Value.ToString(); }
                    else { Value[i - 1] = ""; }
                }
                Program.DB.setValue(DB.type.ProjDB, "User_Pump", "번호,명칭,종류,A효율,B효율,유량,동력,양정,대수",
                "'" + Value[0] + "','"
                 + Value[1] + "','" + Value[2] + "','" + Value[3] + "','" + Value[4] + "','" + Value[5] + "','" + Value[6] + "','" + Value[7] + "','"
                 + Value[8]
                 + "'", "번호");
            }
            MessageBox.Show("저장되었습니다.");
        }


        ///////////////////////////////////////////////////태양열/////////////////////////////////////////////////////////////////
        public void Create_Solar_Table()
        {
            Solar_dataGridView.Columns.Clear();
            Solar_checkBoxColumn.HeaderText = "선택";
            Solar_checkBoxColumn.Name = "check";
            Solar_dataGridView.Columns.Add(Solar_checkBoxColumn);

            Solar_dataGridView.ColumnCount = 13;
            Solar_dataGridView.Columns[1].HeaderText = "번호";
            Solar_dataGridView.Columns[2].HeaderText = "DB유형";
            Solar_dataGridView.Columns[3].HeaderText = "명칭";
            Solar_dataGridView.Columns[4].HeaderText = "난방/급탕";
            Solar_dataGridView.Columns[5].HeaderText = "연료";
            Solar_dataGridView.Columns[6].HeaderText = "Type";
            Solar_dataGridView.Columns[7].HeaderText = "용량" + Environment.NewLine + "[kW]";
            Solar_dataGridView.Columns[8].HeaderText = "전부하효율" + Environment.NewLine + "[%]";
            Solar_dataGridView.Columns[9].HeaderText = "부분부하효율" + Environment.NewLine + "[%]";
            Solar_dataGridView.Columns[10].HeaderText = "소비전력" + Environment.NewLine + "[W]";
            Solar_dataGridView.Columns[11].HeaderText = "대기전력" + Environment.NewLine + "[W]";
            Solar_dataGridView.Columns[12].HeaderText = "대수" + Environment.NewLine + "[EA]";
        }

        private void UserSolar_Add_button_Click(object sender, EventArgs e)
        {
            int nRow = Solar_dataGridView.Rows.Add();
            Load_Solar_Num();

            Solar_dataGridView.Rows[nRow].Cells[2].Value = "도면";

            DataGridViewComboBoxCell 난방급탕Combo = new DataGridViewComboBoxCell();
            난방급탕Combo.Items.Add("난방");
            난방급탕Combo.Items.Add("급탕");
            난방급탕Combo.Items.Add("난방+급탕");
            Solar_dataGridView.Rows[nRow].Cells[4] = 난방급탕Combo;


            DataGridViewComboBoxCell 연료Combo = new DataGridViewComboBoxCell();
            연료Combo.Items.Add("LNG");
            연료Combo.Items.Add("LPG");
            연료Combo.Items.Add("기름");
            연료Combo.Items.Add("펠릿");
            연료Combo.Items.Add("전기");
            Solar_dataGridView.Rows[nRow].Cells[5] = 연료Combo;


            Solar_dataGridView.Rows[nRow].Cells[3].Style.BackColor = SystemColors.Info;
            Solar_dataGridView.Rows[nRow].Cells[6].Style.BackColor = SystemColors.Control;
            for (int k = 7; k < 13; k++)
            {
                Solar_dataGridView.Rows[nRow].Cells[k].Style.BackColor = SystemColors.Info;
            }

        }

        private void DefaultSolar_Add_button_Click(object sender, EventArgs e)
        {
        }

        private void Solar_dataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (e.ColumnIndex == 5)
                {
                    if (Solar_dataGridView.Rows[e.RowIndex].Cells[2].Value.ToString() == "도면")
                    { Load_Solar_Type(e.RowIndex); }
                }
                else if (e.ColumnIndex == 7 || e.ColumnIndex == 8)
                {
                    if (Solar_dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                    {
                        if (Convert.ToDouble(Solar_dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value) < 1)
                        {
                            MessageBox.Show("퍼센트 단위로 입력하세요.(Ex : 90.1% ⇒ 90.1)");
                            Solar_dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = null;
                        }
                    }
                }
                if (Solar_dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                {
                    Solar_dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.White;
                }
            }
        }

        private void Load_Solar_Type(int nRow)
        {
            DataGridViewComboBoxCell TypeCombo = new DataGridViewComboBoxCell();

            switch (Solar_dataGridView.Rows[nRow].Cells[5].Value)
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
            Solar_dataGridView.Rows[nRow].Cells[6] = TypeCombo;
        }

        private void Solar_Remove_button_Click(object sender, EventArgs e)
        {
            Solar_dataGridView.Rows.Remove(Solar_dataGridView.Rows[Solar_SelectRow]);
            Load_Solar_Num();
        }

        private void Solar_Copy_button_Click(object sender, EventArgs e)
        {
            int nRow = Solar_dataGridView.Rows.Add();
            Load_Solar_Num();
            if (Solar_dataGridView.Rows[Solar_SelectRow].Cells[2].Value == "도면")
            {
                DataGridViewComboBoxCell 연료Combo = new DataGridViewComboBoxCell();
                연료Combo.Items.Add("LNG");
                연료Combo.Items.Add("LPG");
                연료Combo.Items.Add("기름");
                연료Combo.Items.Add("펠릿");
                연료Combo.Items.Add("전기");
                Solar_dataGridView.Rows[nRow].Cells[5] = 연료Combo;
            }

            DataGridViewComboBoxCell 난방급탕Combo = new DataGridViewComboBoxCell();
            난방급탕Combo.Items.Add("난방");
            난방급탕Combo.Items.Add("급탕");
            난방급탕Combo.Items.Add("난방+급탕");
            Solar_dataGridView.Rows[nRow].Cells[4] = 난방급탕Combo;

            for (int k = 2; k < 13; k++)
            {
                if (Solar_dataGridView.Rows[Solar_SelectRow].Cells[k].Value != null)
                {
                    Solar_dataGridView.Rows[nRow].Cells[k].Value = Solar_dataGridView.Rows[Pump_SelectRow].Cells[k].Value;
                    Solar_dataGridView.Rows[nRow].Cells[k].Style.BackColor = Color.White;
                }
                else
                {
                    Solar_dataGridView.Rows[nRow].Cells[k].Style.BackColor = SystemColors.Info;
                }
            }
            if (Solar_dataGridView.Rows[Solar_SelectRow].Cells[3].Value != null)
            {
                Solar_dataGridView.Rows[nRow].Cells[3].Value = Solar_dataGridView.Rows[Solar_SelectRow].Cells[3].Value.ToString() + "_복사";
            }
        }
        private void Solar_dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                Solar_dataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
                Solar_SelectRow = e.RowIndex;
                DataGridViewRow row = Solar_dataGridView.Rows[Solar_SelectRow];
                DataGridViewRow row2;
                for (int k = 0; k < Solar_dataGridView.RowCount; k++)
                {
                    if (k != row.Index)
                    {
                        Solar_dataGridView.Rows[k].Cells[0].Value = false;
                        row2 = Solar_dataGridView.Rows[k];
                        row2.DefaultCellStyle.BackColor = SystemColors.Window;
                        row2.DefaultCellStyle.ForeColor = SystemColors.WindowText;
                    }
                    else
                    {
                        row.DefaultCellStyle.BackColor = SystemColors.GradientInactiveCaption;
                        row.DefaultCellStyle.ForeColor = SystemColors.WindowText;
                        row = Solar_dataGridView.Rows[e.RowIndex];
                    }
                }
            }
        }

        private void Load_Solar_Num()
        {
            for (int k = 0; k < Solar_dataGridView.RowCount; k++)
            {
                if (k + 1 < 10)
                { Solar_dataGridView.Rows[k].Cells[1].Value = "UBS0" + (k + 1).ToString(); }
                else { Solar_dataGridView.Rows[k].Cells[1].Value = "UBS" + (k + 1).ToString(); }
            }
        }

        private void Solar_Save_button_Click(object sender, EventArgs e)
        {
            Program.DB.deleteValue(DB.type.ProjDB, "User_Solar", "");

            for (int k = 0; k < Solar_dataGridView.RowCount; k++)
            {
                String[] Value = new String[12];
                for (int i = 1; i < 13; i++)
                {
                    if (Solar_dataGridView.Rows[k].Cells[i].Value != null)
                    { Value[i - 1] = Solar_dataGridView.Rows[k].Cells[i].Value.ToString(); }
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

    }
}
