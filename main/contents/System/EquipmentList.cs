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
using main.subcontents.RESystem_PV;
using main.subcontents;
using Microsoft.VisualBasic;
using main.subcontents.CoolingSystem;

namespace main.contents
{
    public partial class EquipmentList : Form
    {
        DataGridViewCheckBoxColumn Boiler_checkBoxColumn = new DataGridViewCheckBoxColumn();
        DataGridViewCheckBoxColumn ABS_checkBoxColumn = new DataGridViewCheckBoxColumn();
        DataGridViewCheckBoxColumn DH_checkBoxColumn = new DataGridViewCheckBoxColumn();
        DataGridViewCheckBoxColumn PV_checkBoxColumn = new DataGridViewCheckBoxColumn();
        DataGridViewCheckBoxColumn FC_checkBoxColumn = new DataGridViewCheckBoxColumn();
        DataGridViewCheckBoxColumn WP_checkBoxColumn = new DataGridViewCheckBoxColumn();
        DataGridViewCheckBoxColumn Solar_checkBoxColumn = new DataGridViewCheckBoxColumn();
        DataGridViewCheckBoxColumn AirHP_checkBoxColumn = new DataGridViewCheckBoxColumn();
        DataGridViewCheckBoxColumn GWHP_checkBoxColumn = new DataGridViewCheckBoxColumn();
        DataGridViewCheckBoxColumn GroundHP_checkBoxColumn = new DataGridViewCheckBoxColumn();
        DataGridViewCheckBoxColumn Pump_checkBoxColumn = new DataGridViewCheckBoxColumn();
        DataGridViewCheckBoxColumn ce_checkBoxColumn = new DataGridViewCheckBoxColumn();
        int Boiler_SelectRow, HP_SelectRow, AirCooler_SelectRow, WaterCooler_SelectRow, Pump_SelectRow, ce_SelectRow, Solar_SelectRow, PV_SelectRow, ABS_SelectRow, DH_SelectRow, FC_SelectRow, WP_SelectRow;

        //냉방추가
        DataGridViewCheckBoxColumn AirCooler_checkBoxColumn = new DataGridViewCheckBoxColumn();
        DataGridViewCheckBoxColumn WaterCooler_checkBoxColumn = new DataGridViewCheckBoxColumn();
        public EquipmentList()
        {
            InitializeComponent();

            Program.DB.initTable(DB.type.ProjDB, "User_Boiler");
            Program.DB.initTable(DB.type.ProjDB, "User_ABS");
            Program.DB.initTable(DB.type.ProjDB, "User_ DH");
            Program.DB.initTable(DB.type.ProjDB, "User_PVModule");
            Program.DB.initTable(DB.type.ProjDB, "User_FC");
            Program.DB.initTable(DB.type.ProjDB, "User_WP");
            Program.DB.initTable(DB.type.ProjDB, "User_AirHP");
            Program.DB.initTable(DB.type.ProjDB, "User_GroundHP");
            Program.DB.initTable(DB.type.ProjDB, "User_GroundWHP");
            Program.DB.initTable(DB.type.ProjDB, "User_Pump");
            Program.DB.initTable(DB.type.ProjDB, "User_ce");
            Program.DB.initTable(DB.type.ProjDB, "User_ Solar");
            // 냉방추가
            Program.DB.initTable(DB.type.ProjDB, "User_ AirCooler");
            Program.DB.initTable(DB.type.ProjDB, "User_ WaterCooler");
            Program.DB.initTable(DB.type.ProjDB, "User_ AbsorbCooler");


            string[][] Image = Program.DB.getValue(DB.type.BaseDB_HCneed, "메뉴아이콘", "하위메뉴아이콘", "하위메뉴명 = '장비일람표'");
            Icon_pictureBox.Load(Program.gPath + Image[0][0]);
            Icon_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            Create_Boiler_Table();
            Create_PV_Table();
            Create_FC_Table();
            Create_WP_Table();
            Create_ABS_Table();
            Create_DH_Table();
            Create_Solar_Table();
            Create_AirHP_Table();
            Create_GroundHP_Table();
            Create_GWHP_Table();
            Create_Pump_Table();
            Create_ce_Table();

            //냉방추가
            Create_AirCooler_Table();
            Create_WaterCooler_Table();

            Load_Boiler();
            Load_AirHP();
            Load_GroundHP();
            Load_GWHP();
            Load_AirCooler();
            Load_WaterCooler();
            Load_Pump();
            Load_ce();
            Load_Solar();
            Load_PV();
            Load_FC();
            Load_WP();
            Load_ABS();
            Load_DH();



        }

        private void GeneralPanel_Paint(object sender, PaintEventArgs e)
        {
            Panel p = (Panel)sender;
            ControlPaint.DrawBorder(e.Graphics, p.DisplayRectangle, Color.FromArgb(153, 180, 209), ButtonBorderStyle.Solid);
        }

        ///////////////////////////////////////////////////보일러/////////////////////////////////////////////////////////////////
        #region 1.보일러
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
            Boiler_dataGridView.Columns.Add("A7", "용량.[kW]");
            Boiler_dataGridView.Columns.Add("A8", "효율.전부하.[%]");
            Boiler_dataGridView.Columns.Add("A9", "효율.부분부하.[%]");
            Boiler_dataGridView.Columns.Add("A10", "전력.소비전력.[W]");
            Boiler_dataGridView.Columns.Add("A11", "전력.대기전력.[W]");
            Boiler_dataGridView.Columns.Add("A12", "대수.[EA]");
            Boiler_dataGridView.Columns[0].Width = 40;
            Boiler_dataGridView.Columns[1].Width = 60;
            Boiler_dataGridView.Columns[2].Width = 60;
            Boiler_dataGridView.Columns[3].Width = 100;
            Boiler_dataGridView.Columns[4].Width = 90;
            Boiler_dataGridView.Columns[5].Width = 60;
            Boiler_dataGridView.Columns[6].Width = 130;
            Boiler_dataGridView.Columns[8].Width = 80;
            Boiler_dataGridView.Columns[9].Width = 80;
            Boiler_dataGridView.Columns[10].Width = 80;
            Boiler_dataGridView.Columns[11].Width = 80;
            DataGridViewComboBoxColumn 설치유형Combo = new DataGridViewComboBoxColumn();
            설치유형Combo.HeaderText = "설치";
            설치유형Combo.Items.AddRange("기존", "신규", "철거후신규");
            Boiler_dataGridView.Columns.Add(설치유형Combo);
            Boiler_dataGridView.Columns[13].Width = 100;


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
                    Boiler_dataGridView.Rows[nRow].Cells[k].Value = Boiler_dataGridView.Rows[Boiler_SelectRow].Cells[k].Value;
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
                String[] Value = new String[13];
                for (int i = 1; i < 14; i++)
                {
                    if (Boiler_dataGridView.Rows[k].Cells[i].Value != null)
                    { Value[i - 1] = Boiler_dataGridView.Rows[k].Cells[i].Value.ToString(); }
                    else { Value[i - 1] = ""; }
                }
                Program.DB.setValue(DB.type.ProjDB, "User_Boiler", "번호,DB유형,명칭,난방급탕,연료,Type,용량,전부하효율,부분부하효율,소비전력,대기전력,대수,신규기존",
                "'" + Value[0] + "','"
                 + Value[1] + "','" + Value[2] + "','" + Value[3] + "','" + Value[4] + "','" + Value[5] + "','" + Value[6] + "','" + Value[7] + "','" + Value[8] + "','" + Value[9] + "','" + Value[10] + "','" + Value[11] + "','"
                 + Value[12]
                 + "'", "번호");
            }
            MessageBox.Show("저장되었습니다.");
        }

        private void Load_Boiler()
        {
            try
            {
                string[][] User_Value = Program.DB.getValue(DB.type.ProjDB, "User_Boiler", "번호,명칭,연료,Type,용량,전부하효율,부분부하효율,소비전력,대기전력,DB유형,난방급탕,대수,신규기존", "");
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
                    Boiler_dataGridView.Rows[nRow].Cells[13].Value = User_Value[n][12]; //대수
                }
            }
            catch { }
        }
        #endregion
        ///////////////////////////////////////////////////흡수식냉온수기/////////////////////////////////////////////////////////////////
        #region 2.흡수식냉온수기
        public void Create_ABS_Table()
        {
            new StackedHeaderDecorator(ABS_dataGridView, DataGridViewAutoSizeColumnsMode.Fill, ABS_datagridviewDesign);
            ABS_dataGridView.Columns.Clear();
            ABS_checkBoxColumn.HeaderText = "선택";
            ABS_checkBoxColumn.Name = "check";
            ABS_dataGridView.Columns.Add(ABS_checkBoxColumn);

            ABS_dataGridView.Columns.Add("A1", "번호");
            ABS_dataGridView.Columns.Add("A2", "DB유형");
            ABS_dataGridView.Columns.Add("A3", "난방/냉방");
            ABS_dataGridView.Columns.Add("A4", "연료");
            ABS_dataGridView.Columns.Add("A5", "냉방.용량.[kW]");
            ABS_dataGridView.Columns.Add("A6", "냉방.성능.ξ");
            ABS_dataGridView.Columns.Add("A7", "난방.용량.[kW]");
            ABS_dataGridView.Columns.Add("A8", "난방.성능.COP");
            ABS_dataGridView.Columns.Add("A9", "냉수.입구온도.[℃]");
            ABS_dataGridView.Columns.Add("A10", "냉수.출구온도.[℃]");
            ABS_dataGridView.Columns.Add("A11", "온수.입구온도.[℃]");
            ABS_dataGridView.Columns.Add("A12", "온수.출구온도.[℃]");
            ABS_dataGridView.Columns.Add("A13", "대기전력.[W]");
            ABS_dataGridView.Columns.Add("A14", "통합성능.IPLV");
            ABS_dataGridView.Columns.Add("A15", "대수.[EA]");
            ABS_dataGridView.Columns[0].Width = 40;

            DataGridViewComboBoxColumn 설치유형Combo = new DataGridViewComboBoxColumn();
            설치유형Combo.HeaderText = "설치";
            설치유형Combo.Items.AddRange("기존", "신규", "철거후신규");
            ABS_dataGridView.Columns.Add(설치유형Combo);
            ABS_dataGridView.Columns[16].Width = 100;
        }


        private Boolean ABS_datagridviewDesign(DataGridViewCell cell, int column, int row)
        {

            if (ABS_dataGridView.Rows[row].Cells[2].Value != null && ABS_dataGridView.Rows[row].Cells[2].Value.ToString() == "기본")
            {
                if (column == 14)
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

        private void UserABS_Add_button_Click(object sender, EventArgs e)
        {
            int nRow = ABS_dataGridView.Rows.Add();
            Load_ABS_Num();
            ABS_dataGridView.Rows[nRow].Cells[2].Value = "도면";
            DataGridViewComboBoxCell 난방냉방Combo = new DataGridViewComboBoxCell();
            난방냉방Combo.Items.Add("냉방");
            난방냉방Combo.Items.Add("냉난방");
            ABS_dataGridView.Rows[nRow].Cells[3] = 난방냉방Combo;
            DataGridViewComboBoxCell 연료Combo = new DataGridViewComboBoxCell();
            연료Combo.Items.Add("LNG");
            연료Combo.Items.Add("LPG");
            ABS_dataGridView.Rows[nRow].Cells[4] = 연료Combo;

        }

        private void DefaultABS_Add_button_Click(object sender, EventArgs e)
        {
            ArrayList SelectABS = new ArrayList();
            int nRow = ABS_dataGridView.Rows.Add();
            Load_ABS_Num();
            ABS_dataGridView.Rows[nRow].Cells[2].Value = "기본";
            DataGridViewComboBoxCell 난방냉방Combo = new DataGridViewComboBoxCell();
            난방냉방Combo.Items.Add("냉방");
            난방냉방Combo.Items.Add("냉난방");
            ABS_dataGridView.Rows[nRow].Cells[3] = 난방냉방Combo;
            DataGridViewComboBoxCell 연료Combo = new DataGridViewComboBoxCell();
            연료Combo.Items.Add("LNG");
            연료Combo.Items.Add("LPG");
            ABS_dataGridView.Rows[nRow].Cells[4] = 연료Combo;

            ABS_DB abs_db = new ABS_DB("기본DB 적용", null, "냉난방");
            DialogResult result = abs_db.ShowDialog();
            if (result == DialogResult.OK)
            {
                try
                {
                    if (abs_db.SelectAS != null)
                    {

                        string[][] Value = Program.DB.getValue(DB.type.BaseDB_Heating, "흡수식냉온수기", "통합성능", "번호 = '" + abs_db.SelectAS.ToString() + "'");

                        ABS_dataGridView.Rows[nRow].Cells[14].Value = Value[0][0];
                    }
                }
                catch { }
            }
        }

        private void ABS_Remove_button_Click(object sender, EventArgs e)
        {
            ABS_dataGridView.Rows.Remove(ABS_dataGridView.Rows[ABS_SelectRow]);
            Load_ABS_Num();
        }

        private void ABS_Copy_button_Click(object sender, EventArgs e)
        {
            int nRow = ABS_dataGridView.Rows.Add();
            Load_ABS_Num();



            for (int k = 2; k < 17; k++)
            {
                if (ABS_dataGridView.Rows[ABS_SelectRow].Cells[k].Value != null)
                {
                    ABS_dataGridView.Rows[nRow].Cells[k].Value = ABS_dataGridView.Rows[ABS_SelectRow].Cells[k].Value;
                }
            }
            if (ABS_dataGridView.Rows[ABS_SelectRow].Cells[3].Value != null)
            {
                ABS_dataGridView.Rows[nRow].Cells[3].Value = ABS_dataGridView.Rows[ABS_SelectRow].Cells[3].Value.ToString() + "_복사";
            }

            if (ABS_dataGridView.Rows[nRow].Cells[2].Value == "도면")
            {
                DataGridViewComboBoxCell 난방냉방Combo = new DataGridViewComboBoxCell();
                난방냉방Combo.Items.Add("냉방");
                난방냉방Combo.Items.Add("냉난방");
                ABS_dataGridView.Rows[nRow].Cells[3] = 난방냉방Combo;
                DataGridViewComboBoxCell 연료Combo = new DataGridViewComboBoxCell();
                연료Combo.Items.Add("LNG");
                연료Combo.Items.Add("LPG");
                ABS_dataGridView.Rows[nRow].Cells[4] = 연료Combo;
            }
        }
        private void ABS_dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                ABS_dataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
                ABS_SelectRow = e.RowIndex;
            }
        }
        private void ABS_dataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    if (e.ColumnIndex == 3)
                    {
                        if (ABS_dataGridView.Rows[e.RowIndex].Cells[3].Value != null && ABS_dataGridView.Rows[e.RowIndex].Cells[3].Value.ToString() == "냉방")
                        {
                            ABS_dataGridView.Rows[e.RowIndex].Cells[7].Value = "-";
                            ABS_dataGridView.Rows[e.RowIndex].Cells[8].Value = "-";
                            ABS_dataGridView.Rows[e.RowIndex].Cells[11].Value = "-";
                            ABS_dataGridView.Rows[e.RowIndex].Cells[12].Value = "-";
                        }
                        if (ABS_dataGridView.Rows[e.RowIndex].Cells[3].Value != null && ABS_dataGridView.Rows[e.RowIndex].Cells[3].Value.ToString() == "냉난방")
                        {
                            ABS_dataGridView.Rows[e.RowIndex].Cells[7].Value = null;
                            ABS_dataGridView.Rows[e.RowIndex].Cells[8].Value = null;
                            ABS_dataGridView.Rows[e.RowIndex].Cells[11].Value = null;
                            ABS_dataGridView.Rows[e.RowIndex].Cells[12].Value = null;
                        }
                    }

                }

            }
            catch { }

        }

        private void Load_ABS_Num()
        {
            for (int k = 0; k < ABS_dataGridView.RowCount; k++)
            {
                if (k + 1 < 10)
                { ABS_dataGridView.Rows[k].Cells[1].Value = "UAS0" + (k + 1).ToString(); }
                else { ABS_dataGridView.Rows[k].Cells[1].Value = "UAS" + (k + 1).ToString(); }
            }
        }

        private void ABS_Save_button_Click(object sender, EventArgs e)
        {
            Program.DB.deleteValue(DB.type.ProjDB, "User_ABS", "");

            for (int k = 0; k < ABS_dataGridView.RowCount; k++)
            {
                String[] Value = new String[16];
                for (int i = 1; i < 17; i++)
                {
                    if (ABS_dataGridView.Rows[k].Cells[i].Value != null)
                    { Value[i - 1] = ABS_dataGridView.Rows[k].Cells[i].Value.ToString(); }
                    else { Value[i - 1] = ""; }
                }
                Program.DB.setValue(DB.type.ProjDB, "User_ABS", "번호,DB유형,난방냉방,연료,냉방용량,냉방성능,난방용량,난방성능,냉수입구온도,냉수출구온도,온수입구온도,온수출구온도,대기전력,통합성능,대수,신규기존",
                "'" + Value[0] + "','"
                 + Value[1] + "','" + Value[2] + "','" + Value[3] + "','" + Value[4] + "','" + Value[5] + "','" + Value[6] + "','" + Value[7] + "','" + Value[8] + "','"
                 + Value[9] + "','" + Value[10] + "','" + Value[11] + "','" + Value[12] + "','" + Value[13] + "','" + Value[14] + "','"
                 + Value[15]

                 + "'", "번호");
            }
            MessageBox.Show("저장되었습니다.");
        }

        private void Load_ABS()
        {
            try
            {
                string[][] User_Value = Program.DB.getValue(DB.type.ProjDB, "User_ABS", "번호,DB유형,난방냉방,연료,냉방용량,냉방성능,난방용량,난방성능,냉수입구온도,냉수출구온도,온수입구온도,온수출구온도,대기전력,통합성능,대수,신규기존", "");

                for (int n = 0; n < User_Value.Length; n++)
                {
                    ABS_dataGridView.Rows.Add();
                    int nRow = ABS_dataGridView.Rows.Count - 1;
                    ABS_dataGridView.Rows[nRow].Cells[1].Value = User_Value[n][0];
                    ABS_dataGridView.Rows[nRow].Cells[2].Value = User_Value[n][1];
                    ABS_dataGridView.Rows[nRow].Cells[3].Value = User_Value[n][2];
                    ABS_dataGridView.Rows[nRow].Cells[4].Value = User_Value[n][3];
                    ABS_dataGridView.Rows[nRow].Cells[5].Value = User_Value[n][4];
                    ABS_dataGridView.Rows[nRow].Cells[6].Value = User_Value[n][5];
                    ABS_dataGridView.Rows[nRow].Cells[7].Value = User_Value[n][6];
                    ABS_dataGridView.Rows[nRow].Cells[8].Value = User_Value[n][7];
                    ABS_dataGridView.Rows[nRow].Cells[9].Value = User_Value[n][8];
                    ABS_dataGridView.Rows[nRow].Cells[10].Value = User_Value[n][9];
                    ABS_dataGridView.Rows[nRow].Cells[11].Value = User_Value[n][10];
                    ABS_dataGridView.Rows[nRow].Cells[12].Value = User_Value[n][11];
                    ABS_dataGridView.Rows[nRow].Cells[13].Value = User_Value[n][12];
                    ABS_dataGridView.Rows[nRow].Cells[14].Value = User_Value[n][13];
                    ABS_dataGridView.Rows[nRow].Cells[15].Value = User_Value[n][14];
                    ABS_dataGridView.Rows[nRow].Cells[16].Value = User_Value[n][15];
                }
            }
            catch { }
        }
        #endregion

        ///////////////////////////////////////////////////지역난방/////////////////////////////////////////////////////////////////
        #region 3.지역난방
        public void Create_DH_Table()
        {
            new StackedHeaderDecorator(DH_dataGridView, DataGridViewAutoSizeColumnsMode.Fill, DH_datagridviewDesign);
            DH_dataGridView.Columns.Clear();
            DH_checkBoxColumn.HeaderText = "선택";
            DH_checkBoxColumn.Name = "check";
            DH_dataGridView.Columns.Add(DH_checkBoxColumn);

            DH_dataGridView.Columns.Add("A1", "번호");
            DH_dataGridView.Columns.Add("A2", "DB유형");
            DH_dataGridView.Columns.Add("A3", "명칭");
            DH_dataGridView.Columns.Add("A4", "용도");
            DH_dataGridView.Columns.Add("A5", "용량.[kW]");
            DH_dataGridView.Columns.Add("A6", "1차측.공급온도.[℃]");
            DH_dataGridView.Columns.Add("A7", "1차측.환수온도.[℃]");
            DH_dataGridView.Columns.Add("A8", "2차측.공급온도.[℃]");
            DH_dataGridView.Columns.Add("A9", "2차측.환수온도.[℃]");
            DH_dataGridView.Columns.Add("A10", "대수.[EA]");
            DH_dataGridView.Columns[0].Width = 40;

            DataGridViewComboBoxColumn 설치유형Combo = new DataGridViewComboBoxColumn();
            설치유형Combo.HeaderText = "설치";
            설치유형Combo.Items.AddRange("기존", "신규", "철거후신규");
            DH_dataGridView.Columns.Add(설치유형Combo);
            DH_dataGridView.Columns[11].Width = 100;
        }


        private Boolean DH_datagridviewDesign(DataGridViewCell cell, int column, int row)
        {

            if (DH_dataGridView.Rows[row].Cells[2].Value != null && DH_dataGridView.Rows[row].Cells[2].Value.ToString() == "기본")
            {
                if (column == 14)
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

        private void UserDH_Add_button_Click(object sender, EventArgs e)
        {
            int nRow = DH_dataGridView.Rows.Add();
            Load_DH_Num();
            DH_dataGridView.Rows[nRow].Cells[2].Value = "도면";
            DataGridViewComboBoxCell 용도Combo = new DataGridViewComboBoxCell();
            용도Combo.Items.Add("난방용(복사난방)");
            용도Combo.Items.Add("난방용(공조난방)");
            용도Combo.Items.Add("급탕일반용");
            용도Combo.Items.Add("급탕재열용");
            용도Combo.Items.Add("급탕예열용");
            DH_dataGridView.Rows[nRow].Cells[4] = 용도Combo;

        }

        private void DefaultDH_Add_button_Click(object sender, EventArgs e)
        {
            ArrayList SelectDH = new ArrayList();
            int nRow = DH_dataGridView.Rows.Add();
            Load_DH_Num();
            DH_dataGridView.Rows[nRow].Cells[2].Value = "기본";
            DataGridViewComboBoxCell 용도Combo = new DataGridViewComboBoxCell();
            용도Combo.Items.Add("난방용(복사난방)");
            용도Combo.Items.Add("난방용(공조난방)");
            용도Combo.Items.Add("급탕일반용");
            용도Combo.Items.Add("급탕재열용");
            용도Combo.Items.Add("급탕예열용");
            DH_dataGridView.Rows[nRow].Cells[4] = 용도Combo;
            DH_DB DH_db = new DH_DB("기본DB 적용", null);
            DialogResult result = DH_db.ShowDialog();
            if (result == DialogResult.OK)
            {
                try
                {
                    if (DH_db.SelectDH != null)
                    {

                        string[][] Value = Program.DB.getValue(DB.type.BaseDB_Heating, "지역난방", "용도,공급온도1차,환수온도1차,공급온도2차,환수온도2차", "번호 = '" + DH_db.SelectDH.ToString() + "'");

                        DH_dataGridView.Rows[nRow].Cells[4].Value = Value[0][0];
                        DH_dataGridView.Rows[nRow].Cells[6].Value = Value[0][1];
                        DH_dataGridView.Rows[nRow].Cells[7].Value = Value[0][2];
                        DH_dataGridView.Rows[nRow].Cells[8].Value = Value[0][3];
                        DH_dataGridView.Rows[nRow].Cells[9].Value = Value[0][4];
                    }
                }
                catch { }
            }
        }

        private void DH_Remove_button_Click(object sender, EventArgs e)
        {
            DH_dataGridView.Rows.Remove(DH_dataGridView.Rows[DH_SelectRow]);
            Load_DH_Num();
        }

        private void DH_Copy_button_Click(object sender, EventArgs e)
        {
            int nRow = DH_dataGridView.Rows.Add();
            Load_DH_Num();



            for (int k = 2; k < 12; k++)
            {
                if (DH_dataGridView.Rows[DH_SelectRow].Cells[k].Value != null)
                {
                    DH_dataGridView.Rows[nRow].Cells[k].Value = DH_dataGridView.Rows[DH_SelectRow].Cells[k].Value;
                }
            }
            if (DH_dataGridView.Rows[DH_SelectRow].Cells[3].Value != null)
            {
                DH_dataGridView.Rows[nRow].Cells[3].Value = DH_dataGridView.Rows[DH_SelectRow].Cells[3].Value.ToString() + "_복사";
            }

            if (DH_dataGridView.Rows[nRow].Cells[2].Value == "도면")
            {
                DataGridViewComboBoxCell 용도Combo = new DataGridViewComboBoxCell();
                용도Combo.Items.Add("난방용(복사난방)");
                용도Combo.Items.Add("난방용(공조난방)");
                용도Combo.Items.Add("급탕일반용");
                용도Combo.Items.Add("급탕재열용");
                용도Combo.Items.Add("급탕예열용");
                DH_dataGridView.Rows[nRow].Cells[4] = 용도Combo;
            }
        }
        private void DH_dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DH_dataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
                DH_SelectRow = e.RowIndex;
            }
        }


        private void Load_DH_Num()
        {
            for (int k = 0; k < DH_dataGridView.RowCount; k++)
            {
                if (k + 1 < 10)
                { DH_dataGridView.Rows[k].Cells[1].Value = "UDH0" + (k + 1).ToString(); }
                else { DH_dataGridView.Rows[k].Cells[1].Value = "UDH" + (k + 1).ToString(); }
            }
        }

        private void DH_Save_button_Click(object sender, EventArgs e)
        {
            Program.DB.deleteValue(DB.type.ProjDB, "User_DH", "");

            for (int k = 0; k < DH_dataGridView.RowCount; k++)
            {
                String[] Value = new String[12];
                for (int i = 1; i < 12; i++)
                {
                    if (DH_dataGridView.Rows[k].Cells[i].Value != null)
                    { Value[i - 1] = DH_dataGridView.Rows[k].Cells[i].Value.ToString(); }
                    else { Value[i - 1] = ""; }
                }
                Program.DB.setValue(DB.type.ProjDB, "User_DH", "번호,DB유형,명칭,용도,용량,공급온도1차,환수온도1차,공급온도2차,환수온도2차,대수,신규기존",
                "'" + Value[0] + "','"
                 + Value[1] + "','" + Value[2] + "','" + Value[3] + "','" + Value[4] + "','" + Value[5] + "','" + Value[6] + "','" + Value[7] + "','" + Value[8] + "','" + Value[9] + "','"
                 + Value[10]

                 + "'", "번호");
            }
            MessageBox.Show("저장되었습니다.");
        }

        private void Load_DH()
        {
            try
            {
                string[][] User_Value = Program.DB.getValue(DB.type.ProjDB, "User_DH", "번호,DB유형,명칭,용도,용량,공급온도1차,환수온도1차,공급온도2차,환수온도2차,대수,신규기존", "");

                for (int n = 0; n < User_Value.Length; n++)
                {
                    DH_dataGridView.Rows.Add();
                    int nRow = DH_dataGridView.Rows.Count - 1;
                    DH_dataGridView.Rows[nRow].Cells[1].Value = User_Value[n][0];
                    DH_dataGridView.Rows[nRow].Cells[2].Value = User_Value[n][1];
                    DH_dataGridView.Rows[nRow].Cells[3].Value = User_Value[n][2];
                    DH_dataGridView.Rows[nRow].Cells[4].Value = User_Value[n][3];
                    DH_dataGridView.Rows[nRow].Cells[5].Value = User_Value[n][4];
                    DH_dataGridView.Rows[nRow].Cells[6].Value = User_Value[n][5];
                    DH_dataGridView.Rows[nRow].Cells[7].Value = User_Value[n][6];
                    DH_dataGridView.Rows[nRow].Cells[8].Value = User_Value[n][7];
                    DH_dataGridView.Rows[nRow].Cells[9].Value = User_Value[n][8];
                    DH_dataGridView.Rows[nRow].Cells[10].Value = User_Value[n][9];
                    DH_dataGridView.Rows[nRow].Cells[11].Value = User_Value[n][10];
                }
            }
            catch { }
        }
        #endregion
        ///////////////////////////////////////////////////태양광/////////////////////////////////////////////////////////////////
        #region 4. 태양광
        public void Create_PV_Table()
        {
            new StackedHeaderDecorator(PV_dataGridView, DataGridViewAutoSizeColumnsMode.Fill, PV_datagridviewDesign);
            PV_dataGridView.Columns.Clear();
            PV_checkBoxColumn.HeaderText = "선택";
            PV_checkBoxColumn.Name = "check";
            PV_dataGridView.Columns.Add(PV_checkBoxColumn);

            PV_dataGridView.Columns.Add("A1", "번호");
            PV_dataGridView.Columns.Add("A2", "DB유형");
            PV_dataGridView.Columns.Add("A3", "제품명");
            PV_dataGridView.Columns.Add("A4", "제조사");
            PV_dataGridView.Columns.Add("A5", "제작년도");
            PV_dataGridView.Columns.Add("A6", "Cell Type");
            PV_dataGridView.Columns.Add("A7", "모듈.가로길이.[m]");
            PV_dataGridView.Columns.Add("A8", "모듈.세로길이.[m]");
            PV_dataGridView.Columns.Add("A9", "모듈.정격출력.[W]");
            PV_dataGridView.Columns.Add("A10", "Kpk");
            PV_dataGridView.Columns[0].Width = 40;
            DataGridViewComboBoxColumn 설치유형Combo = new DataGridViewComboBoxColumn();
            설치유형Combo.HeaderText = "설치";
            설치유형Combo.Items.AddRange("기존", "신규", "철거후신규");
            PV_dataGridView.Columns.Add(설치유형Combo);
            PV_dataGridView.Columns[11].Width = 100;
        }


        private Boolean PV_datagridviewDesign(DataGridViewCell cell, int column, int row)
        {
            if (PV_dataGridView.Rows[row].Cells[2].Value != null && PV_dataGridView.Rows[row].Cells[2].Value.ToString() == "도면")
            {
                if (column == 10)
                {
                    cell.Style.BackColor = Color.FromArgb(255, 255, 255);
                    cell.Style.ForeColor = Color.Black;
                    cell.Style.SelectionBackColor = Color.FromArgb(255, 255, 255);
                    cell.Style.SelectionForeColor = Color.Black;
                    return true;
                }
                else { return false; }
            }
            if (PV_dataGridView.Rows[row].Cells[2].Value != null && PV_dataGridView.Rows[row].Cells[2].Value.ToString() == "기본")
            {
                if (column == 7 || column == 8 || column == 9)
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

        private void UserPV_Add_button_Click(object sender, EventArgs e)
        {
            int nRow = PV_dataGridView.Rows.Add();
            Load_PV_Num();
            PV_dataGridView.Rows[nRow].Cells[2].Value = "도면";
            DataGridViewComboBoxCell 제작년도Combo = new DataGridViewComboBoxCell();
            제작년도Combo.Items.Add("25년 이내");
            제작년도Combo.Items.Add("25년 이상");
            PV_dataGridView.Rows[nRow].Cells[5] = 제작년도Combo;

            DataGridViewComboBoxCell 셀타입Combo = new DataGridViewComboBoxCell();
            셀타입Combo.Items.Add("단결정(Single Cry. Si.)");
            셀타입Combo.Items.Add("다결정(Poly Cry. Si.)");
            셀타입Combo.Items.Add("비결정질 Si 박막");
            셀타입Combo.Items.Add("그외 Si 박막");
            셀타입Combo.Items.Add("CIGS 박막");
            셀타입Combo.Items.Add("CdTe 박막");
            PV_dataGridView.Rows[nRow].Cells[6] = 셀타입Combo;

        }

        private void DefaultPV_Add_button_Click(object sender, EventArgs e)
        {
            ArrayList SelectPV = new ArrayList();
            int nRow = PV_dataGridView.Rows.Add();
            Load_PV_Num();
            PV_dataGridView.Rows[nRow].Cells[2].Value = "기본";

            PV_ModuleDB pv_DB = new PV_ModuleDB("기본DB 적용");
            DialogResult result = pv_DB.ShowDialog();
            if (result == DialogResult.OK)
            {
                try
                {
                    if (pv_DB.Select_PVModule[0] != null)
                    {

                        string[][] Value = Program.DB.getValue(DB.type.BaseDB_RESystem, "태양광모듈DB", "제조사,제작년도,CELLTYPE,Kpk", "번호 = '" + pv_DB.Select_PVModule[0].ToString() + "'");

                        PV_dataGridView.Rows[nRow].Cells[4].Value = Value[0][0];
                        PV_dataGridView.Rows[nRow].Cells[5].Value = Value[0][1];
                        PV_dataGridView.Rows[nRow].Cells[6].Value = Value[0][2];
                        PV_dataGridView.Rows[nRow].Cells[10].Value = Value[0][3];
                    }
                }
                catch { }
            }
        }

        private void PV_Remove_button_Click(object sender, EventArgs e)
        {
            PV_dataGridView.Rows.Remove(PV_dataGridView.Rows[PV_SelectRow]);
            Load_PV_Num();
        }

        private void PV_Copy_button_Click(object sender, EventArgs e)
        {
            int nRow = PV_dataGridView.Rows.Add();
            Load_PV_Num();

            for (int k = 2; k < 12; k++)
            {
                if (PV_dataGridView.Rows[PV_SelectRow].Cells[k].Value != null)
                {
                    PV_dataGridView.Rows[nRow].Cells[k].Value = PV_dataGridView.Rows[PV_SelectRow].Cells[k].Value;
                }
            }
            if (PV_dataGridView.Rows[PV_SelectRow].Cells[3].Value != null)
            {
                PV_dataGridView.Rows[nRow].Cells[3].Value = PV_dataGridView.Rows[PV_SelectRow].Cells[3].Value.ToString() + "_복사";
            }

            if (PV_dataGridView.Rows[nRow].Cells[2].Value == "도면")
            {
                DataGridViewComboBoxCell 제작년도Combo = new DataGridViewComboBoxCell();
                제작년도Combo.Items.Add("단결정(Single Cry. Si.)");
                제작년도Combo.Items.Add("다결정(Poly Cry. Si.)");
                PV_dataGridView.Rows[nRow].Cells[5] = 제작년도Combo;

                DataGridViewComboBoxCell 셀타입Combo = new DataGridViewComboBoxCell();
                셀타입Combo.Items.Add("단결정(Single Cry. Si.)");
                셀타입Combo.Items.Add("다결정(Poly Cry. Si.)");
                셀타입Combo.Items.Add("비결정질 Si 박막");
                셀타입Combo.Items.Add("그외 Si 박막");
                셀타입Combo.Items.Add("CIGS 박막");
                셀타입Combo.Items.Add("CdTe 박막");
            }
        }

        private void PV_dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                PV_dataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
                PV_SelectRow = e.RowIndex;
            }
        }

        private void PV_dataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    if (e.ColumnIndex == 7 || e.ColumnIndex == 8 || e.ColumnIndex == 9)
                    {
                        if (PV_dataGridView.Rows[e.RowIndex].Cells[7].Value != null && PV_dataGridView.Rows[e.RowIndex].Cells[8].Value != null && PV_dataGridView.Rows[e.RowIndex].Cells[9].Value != null)
                        { PV_dataGridView.Rows[e.RowIndex].Cells[10].Value = string.Format("{0:F2}", Convert.ToDouble(PV_dataGridView.Rows[e.RowIndex].Cells[9].Value.ToString()) / Convert.ToDouble(PV_dataGridView.Rows[e.RowIndex].Cells[7].Value.ToString()) / Convert.ToDouble(PV_dataGridView.Rows[e.RowIndex].Cells[8].Value.ToString()) / 1000); }
                        // UserDB_Kpk = UserDB_output / (UserDB_height * UserDB_width) / 1000;
                    }

                }

            }
            catch { }

        }

        private void Load_PV_Num()
        {
            for (int k = 0; k < PV_dataGridView.RowCount; k++)
            {
                if (k + 1 < 10)
                { PV_dataGridView.Rows[k].Cells[1].Value = "UPV0" + (k + 1).ToString(); }
                else { PV_dataGridView.Rows[k].Cells[1].Value = "UPV" + (k + 1).ToString(); }
            }
        }

        private void PV_Save_button_Click(object sender, EventArgs e)
        {
            Program.DB.deleteValue(DB.type.ProjDB, "User_PVModule", "");

            for (int k = 0; k < PV_dataGridView.RowCount; k++)
            {
                String[] Value = new String[11];
                for (int i = 1; i < 12; i++)
                {
                    if (PV_dataGridView.Rows[k].Cells[i].Value != null)
                    { Value[i - 1] = PV_dataGridView.Rows[k].Cells[i].Value.ToString(); }
                    else { Value[i - 1] = ""; }
                }
                Program.DB.setValue(DB.type.ProjDB, "User_PVModule", "번호,DB유형,제품명,제조사,제작년도,CELLTYPE,가로길이,세로길이,정격출력,Kpk,신규기존",
                "'" + Value[0] + "','"
                 + Value[1] + "','" + Value[2] + "','" + Value[3] + "','" + Value[4] + "','" + Value[5] + "','" + Value[6] + "','" + Value[7] + "','" + Value[8] + "','" + Value[9] + "','"
                 + Value[10]
                 + "'", "번호");
            }
            MessageBox.Show("저장되었습니다.");
        }

        private void Load_PV()
        {
            try
            {
                string[][] User_Value = Program.DB.getValue(DB.type.ProjDB, "User_PVModule", "번호,DB유형,제품명,제조사,제작년도,CELLTYPE,가로길이,세로길이,정격출력,Kpk,신규기존", "");

                for (int n = 0; n < User_Value.Length; n++)
                {
                    PV_dataGridView.Rows.Add();
                    int nRow = PV_dataGridView.Rows.Count - 1;
                    PV_dataGridView.Rows[nRow].Cells[1].Value = User_Value[n][0];
                    PV_dataGridView.Rows[nRow].Cells[2].Value = User_Value[n][1];
                    PV_dataGridView.Rows[nRow].Cells[3].Value = User_Value[n][2];
                    PV_dataGridView.Rows[nRow].Cells[4].Value = User_Value[n][3];
                    PV_dataGridView.Rows[nRow].Cells[5].Value = User_Value[n][4];
                    PV_dataGridView.Rows[nRow].Cells[6].Value = User_Value[n][5];
                    PV_dataGridView.Rows[nRow].Cells[7].Value = User_Value[n][6];
                    PV_dataGridView.Rows[nRow].Cells[8].Value = User_Value[n][7];
                    PV_dataGridView.Rows[nRow].Cells[9].Value = User_Value[n][8];
                    PV_dataGridView.Rows[nRow].Cells[10].Value = User_Value[n][9];
                    PV_dataGridView.Rows[nRow].Cells[11].Value = User_Value[n][10];
                }
            }
            catch { }
        }
        #endregion

        ///////////////////////////////////////////////////연료전지/////////////////////////////////////////////////////////////////
        #region 5.연료전지
        public void Create_FC_Table()
        {
            new StackedHeaderDecorator(FC_dataGridView, DataGridViewAutoSizeColumnsMode.Fill, FC_datagridviewDesign);
            FC_dataGridView.Columns.Clear();
            FC_checkBoxColumn.HeaderText = "선택";
            FC_checkBoxColumn.Name = "check";
            FC_dataGridView.Columns.Add(FC_checkBoxColumn);

            FC_dataGridView.Columns.Add("A1", "번호");
            FC_dataGridView.Columns.Add("A2", "DB유형");
            FC_dataGridView.Columns.Add("A3", "제품명");
            FC_dataGridView.Columns.Add("A4", "제조사");
            FC_dataGridView.Columns.Add("A5", "연료전지종류");
            FC_dataGridView.Columns.Add("A6", "시스템출력(전기).[kW]");
            FC_dataGridView.Columns.Add("A7", "정격효율.[%]");
            FC_dataGridView.Columns.Add("A8", "발전효율.[%]");
            FC_dataGridView.Columns.Add("A9", "축열탱크");
            FC_dataGridView.Columns[0].Width = 40;

            DataGridViewComboBoxColumn 설치유형Combo = new DataGridViewComboBoxColumn();
            설치유형Combo.HeaderText = "설치";
            설치유형Combo.Items.AddRange("기존", "신규", "철거후신규");
            FC_dataGridView.Columns.Add(설치유형Combo);
            FC_dataGridView.Columns[10].Width = 100;
        }

        private Boolean FC_datagridviewDesign(DataGridViewCell cell, int column, int row)
        {
            if (FC_dataGridView.Rows[row].Cells[2].Value != null && FC_dataGridView.Rows[row].Cells[2].Value.ToString() == "도면")
            {
                if (column == 10)
                {
                    cell.Style.BackColor = Color.FromArgb(255, 255, 255);
                    cell.Style.ForeColor = Color.Black;
                    cell.Style.SelectionBackColor = Color.FromArgb(255, 255, 255);
                    cell.Style.SelectionForeColor = Color.Black;
                    return true;
                }
                else { return false; }
            }

            if (FC_dataGridView.Rows[row].Cells[2].Value != null && FC_dataGridView.Rows[row].Cells[2].Value.ToString() == "기본")
            {
                if (column == 11)
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

        private void UserFC_Add_button_Click(object sender, EventArgs e)
        {
            int nRow = FC_dataGridView.Rows.Add();
            Load_FC_Num();
            FC_dataGridView.Rows[nRow].Cells[2].Value = "도면";
            DataGridViewComboBoxCell 연로전지종류Combo = new DataGridViewComboBoxCell();
            연로전지종류Combo.Items.Add("PEMFC");
            연로전지종류Combo.Items.Add("DMFC");
            연로전지종류Combo.Items.Add("SOFC");
            FC_dataGridView.Rows[nRow].Cells[5] = 연로전지종류Combo;

            DataGridViewComboBoxCell 축열탱크Combo = new DataGridViewComboBoxCell();
            축열탱크Combo.Items.Add("외장형");
            축열탱크Combo.Items.Add("내장형");
            FC_dataGridView.Rows[nRow].Cells[9] = 축열탱크Combo;
        }

        private void DefaultFC_Add_button_Click(object sender, EventArgs e)
        {
            ArrayList SelectFC = new ArrayList();
            int nRow = FC_dataGridView.Rows.Add();
            Load_FC_Num();
            FC_dataGridView.Rows[nRow].Cells[2].Value = "기본";

            FC_DB fc_DB = new FC_DB("기본DB 적용");
            DialogResult result = fc_DB.ShowDialog();
            if (result == DialogResult.OK)
            {
                try
                {
                    if (fc_DB.Select_FC[0] != null)
                    {
                        string[][] Value = Program.DB.getValue(DB.type.BaseDB_RESystem, "연료전지DB", "제조사,연료전지종류,정격효율,발전효율", "번호 = '" + fc_DB.Select_FC[0].ToString() + "'");

                        FC_dataGridView.Rows[nRow].Cells[4].Value = Value[0][0];
                        FC_dataGridView.Rows[nRow].Cells[5].Value = Value[0][1];
                        FC_dataGridView.Rows[nRow].Cells[7].Value = Value[0][2];
                        FC_dataGridView.Rows[nRow].Cells[8].Value = Value[0][3];

                        DataGridViewComboBoxCell 축열탱크Combo = new DataGridViewComboBoxCell();
                        축열탱크Combo.Items.Add("외장형");
                        축열탱크Combo.Items.Add("내장형");
                        FC_dataGridView.Rows[nRow].Cells[9] = 축열탱크Combo;
                    }
                }
                catch { }
            }
        }

        private void FC_Remove_button_Click(object sender, EventArgs e)
        {
            FC_dataGridView.Rows.Remove(FC_dataGridView.Rows[FC_SelectRow]);
            Load_FC_Num();
        }

        private void FC_Copy_button_Click(object sender, EventArgs e)
        {
            int nRow = FC_dataGridView.Rows.Add();
            Load_FC_Num();

            DataGridViewComboBoxCell 연로전지종류Combo = new DataGridViewComboBoxCell();
            연로전지종류Combo.Items.Add("PEMFC");
            연로전지종류Combo.Items.Add("DMFC");
            연로전지종류Combo.Items.Add("SOFC");
            FC_dataGridView.Rows[nRow].Cells[5] = 연로전지종류Combo;

            DataGridViewComboBoxCell 축열탱크Combo = new DataGridViewComboBoxCell();
            축열탱크Combo.Items.Add("외장형");
            축열탱크Combo.Items.Add("내장형");
            FC_dataGridView.Rows[nRow].Cells[9] = 축열탱크Combo;

            for (int k = 2; k < 11; k++)
            {
                if (FC_dataGridView.Rows[FC_SelectRow].Cells[k].Value != null)
                {
                    FC_dataGridView.Rows[nRow].Cells[k].Value = FC_dataGridView.Rows[FC_SelectRow].Cells[k].Value;
                }
            }

            if (FC_dataGridView.Rows[FC_SelectRow].Cells[3].Value != null)
            {
                FC_dataGridView.Rows[nRow].Cells[3].Value = FC_dataGridView.Rows[FC_SelectRow].Cells[3].Value.ToString() + "_복사";
            }

        }

        private void FC_dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                FC_dataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
                FC_SelectRow = e.RowIndex;
            }
        }

        private void Load_FC_Num()
        {
            for (int k = 0; k < FC_dataGridView.RowCount; k++)
            {
                if (k + 1 < 10)
                { FC_dataGridView.Rows[k].Cells[1].Value = "UFC0" + (k + 1).ToString(); }
                else { FC_dataGridView.Rows[k].Cells[1].Value = "UFC" + (k + 1).ToString(); }
            }
        }

        private void FC_Save_button_Click(object sender, EventArgs e)
        {
            Program.DB.deleteValue(DB.type.ProjDB, "User_FC", "");

            for (int k = 0; k < FC_dataGridView.RowCount; k++)
            {
                String[] Value = new String[10];
                for (int i = 1; i < 11; i++)
                {
                    if (FC_dataGridView.Rows[k].Cells[i].Value != null)
                    { Value[i - 1] = FC_dataGridView.Rows[k].Cells[i].Value.ToString(); }
                    else { Value[i - 1] = ""; }
                }
                Program.DB.setValue(DB.type.ProjDB, "User_FC", "번호,DB유형,제품명,제조사,연료전지종류,시스템출력,정격효율,발전효율,축열탱크,신규기존",
                "'" + Value[0] + "','"
                 + Value[1] + "','" + Value[2] + "','" + Value[3] + "','" + Value[4] + "','" + Value[5] + "','" + Value[6] + "','" + Value[7] + "','" + Value[8] + "','" + Value[9] + "'", "번호");
            }
            MessageBox.Show("저장되었습니다.");
        }

        private void Load_FC()
        {
            try
            {
                string[][] User_Value = Program.DB.getValue(DB.type.ProjDB, "User_FC", "번호,DB유형,제품명,제조사,연료전지종류,시스템출력,정격효율,발전효율,축열탱크,신규기존", "");

                for (int n = 0; n < User_Value.Length; n++)
                {
                    FC_dataGridView.Rows.Add();
                    int nRow = FC_dataGridView.Rows.Count - 1;
                    FC_dataGridView.Rows[nRow].Cells[1].Value = User_Value[n][0];
                    FC_dataGridView.Rows[nRow].Cells[2].Value = User_Value[n][1];
                    FC_dataGridView.Rows[nRow].Cells[3].Value = User_Value[n][2];
                    FC_dataGridView.Rows[nRow].Cells[4].Value = User_Value[n][3];
                    FC_dataGridView.Rows[nRow].Cells[5].Value = User_Value[n][4];
                    FC_dataGridView.Rows[nRow].Cells[6].Value = User_Value[n][5];
                    FC_dataGridView.Rows[nRow].Cells[7].Value = User_Value[n][6];
                    FC_dataGridView.Rows[nRow].Cells[8].Value = User_Value[n][7];
                    FC_dataGridView.Rows[nRow].Cells[9].Value = User_Value[n][8];
                    FC_dataGridView.Rows[nRow].Cells[10].Value = User_Value[n][9];
                }
            }
            catch { }
        }
        #endregion
        ///////////////////////////////////////////////////풍력/////////////////////////////////////////////////////////////////
        #region 6.풍력
        public void Create_WP_Table()
        {
            new StackedHeaderDecorator(WP_dataGridView, DataGridViewAutoSizeColumnsMode.Fill, WP_datagridviewDesign);
            WP_dataGridView.Columns.Clear();
            WP_checkBoxColumn.HeaderText = "선택";
            WP_checkBoxColumn.Name = "check";
            WP_dataGridView.Columns.Add(WP_checkBoxColumn);

            WP_dataGridView.Columns.Add("A1", "번호");
            WP_dataGridView.Columns.Add("A2", "DB유형");
            WP_dataGridView.Columns.Add("A3", "제품명");
            WP_dataGridView.Columns.Add("A4", "제조사");
            WP_dataGridView.Columns.Add("A5", "타입");
            WP_dataGridView.Columns.Add("A6", "세부타입");
            WP_dataGridView.Columns.Add("A7", "정격.출력.[kW]");
            WP_dataGridView.Columns.Add("A8", "팬.허브면적.[㎡]");
            WP_dataGridView.Columns.Add("A9", "허브.높이.[m]");
            WP_dataGridView.Columns.Add("A10", "풍속.시동.[m/s]");
            WP_dataGridView.Columns.Add("A11", "풍속.종단.[m/s]");
            WP_dataGridView.Columns.Add("A12", "풍속.최적.[m/s]");
            WP_dataGridView.Columns.Add("A13", "전력계수.시동풍속.Cp,min");
            WP_dataGridView.Columns.Add("A14", "전력계수.최적풍속.Cp,op");
            WP_dataGridView.Columns.Add("A15", "전력계수.종단풍속.Cp,max");
            WP_dataGridView.Columns[0].Width = 40;
            DataGridViewComboBoxColumn 설치유형Combo = new DataGridViewComboBoxColumn();
            설치유형Combo.HeaderText = "설치";
            설치유형Combo.Items.AddRange("기존", "신규", "철거후신규");
            WP_dataGridView.Columns.Add(설치유형Combo);
            WP_dataGridView.Columns[16].Width = 100;
        }

        private Boolean WP_datagridviewDesign(DataGridViewCell cell, int column, int row)
        {
            if (WP_dataGridView.Rows[row].Cells[2].Value != null && WP_dataGridView.Rows[row].Cells[2].Value.ToString() == "도면")
            {
                return false;
            }

            if (WP_dataGridView.Rows[row].Cells[2].Value != null && WP_dataGridView.Rows[row].Cells[2].Value.ToString() == "기본")
            {
                return false;
            }
            else return false;
        }

        private void UserWP_Add_button_Click(object sender, EventArgs e)
        {
            int nRow = WP_dataGridView.Rows.Add();
            Load_WP_Num();
            WP_dataGridView.Rows[nRow].Cells[2].Value = "도면";
            DataGridViewComboBoxCell 타입Combo = new DataGridViewComboBoxCell();
            타입Combo.Items.Add("수직형");
            타입Combo.Items.Add("수평형");
            WP_dataGridView.Rows[nRow].Cells[5] = 타입Combo;

        }

        private void WP_dataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    if (e.ColumnIndex == 5)
                    {
                        if (WP_dataGridView.Rows[e.RowIndex].Cells[5].Value.ToString() == "수평형")
                        {
                            DataGridViewTextBoxCell 수평형 = new DataGridViewTextBoxCell();
                            WP_dataGridView.Rows[e.RowIndex].Cells[6] = 수평형;
                            WP_dataGridView.Rows[e.RowIndex].Cells[6].Value = "수평형";
                        }

                        if (WP_dataGridView.Rows[e.RowIndex].Cells[5].Value.ToString() == "수직형")
                        {
                            DataGridViewComboBoxCell 세부타입Combo = new DataGridViewComboBoxCell();
                            세부타입Combo.Items.Add("사보니우스");
                            세부타입Combo.Items.Add("다리우스");
                            세부타입Combo.Items.Add("H-blade");
                            세부타입Combo.Items.Add("복합형");
                            WP_dataGridView.Rows[e.RowIndex].Cells[6] = 세부타입Combo;
                        }
                    }

                }
            }
            catch { }
        }


        private void WP_Remove_button_Click(object sender, EventArgs e)
        {
            WP_dataGridView.Rows.Remove(WP_dataGridView.Rows[WP_SelectRow]);
            Load_WP_Num();
        }

        private void WP_Copy_button_Click(object sender, EventArgs e)
        {
            int nRow = WP_dataGridView.Rows.Add();
            Load_WP_Num();

            DataGridViewComboBoxCell 타입Combo = new DataGridViewComboBoxCell();
            타입Combo.Items.Add("수직형");
            타입Combo.Items.Add("수평형");
            WP_dataGridView.Rows[nRow].Cells[5] = 타입Combo;

            for (int k = 2; k < 17; k++)
            {
                if (WP_dataGridView.Rows[WP_SelectRow].Cells[k].Value != null)
                {
                    WP_dataGridView.Rows[nRow].Cells[k].Value = WP_dataGridView.Rows[WP_SelectRow].Cells[k].Value;
                }
            }

            if (WP_dataGridView.Rows[WP_SelectRow].Cells[3].Value != null)
            {
                WP_dataGridView.Rows[nRow].Cells[3].Value = WP_dataGridView.Rows[WP_SelectRow].Cells[3].Value.ToString() + "_복사";
            }

        }

        private void WP_dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                WP_dataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
                WP_SelectRow = e.RowIndex;
            }
        }

        private void Load_WP_Num()
        {
            for (int k = 0; k < WP_dataGridView.RowCount; k++)
            {
                if (k + 1 < 10)
                { WP_dataGridView.Rows[k].Cells[1].Value = "UWP0" + (k + 1).ToString(); }
                else { WP_dataGridView.Rows[k].Cells[1].Value = "UWP" + (k + 1).ToString(); }
            }
        }

        private void WP_Save_button_Click(object sender, EventArgs e)
        {
            Program.DB.deleteValue(DB.type.ProjDB, "User_WP", "");

            for (int k = 0; k < WP_dataGridView.RowCount; k++)
            {
                String[] Value = new String[16];
                for (int i = 1; i < 17; i++)
                {
                    if (WP_dataGridView.Rows[k].Cells[i].Value != null)
                    { Value[i - 1] = WP_dataGridView.Rows[k].Cells[i].Value.ToString(); }
                    else { Value[i - 1] = ""; }
                }
                Program.DB.setValue(DB.type.ProjDB, "User_WP", "번호,DB유형,제품명,제조사,타입,세부타입,정격출력,회전면적,허브높이,시동풍속,종단풍속,최적풍속,시동풍속전력계수,최적풍속전력계수,종단풍속전력계수,신규기존",
                "'" + Value[0] + "','"
                 + Value[1] + "','" + Value[2] + "','" + Value[3] + "','" + Value[4] + "','" + Value[5] + "','" + Value[6] + "','" + Value[7] + "','" + Value[8] + "','" + Value[9] + "','" + Value[10] + "','" + Value[11] + "','" + Value[12] + "','" + Value[13] + "','" + Value[14] + "','" + Value[15] + "'", "번호");
            }
            MessageBox.Show("저장되었습니다.");
        }

        private void Load_WP()
        {
            try
            {
                string[][] User_Value = Program.DB.getValue(DB.type.ProjDB, "User_WP", "번호, DB유형, 제품명, 제조사, 타입, 세부타입, 정격출력, 회전면적, 허브높이, 시동풍속, 종단풍속, 최적풍속, 시동풍속전력계수, 최적풍속전력계수, 종단풍속전력계수,신규기존", "");

                for (int n = 0; n < User_Value.Length; n++)
                {
                    WP_dataGridView.Rows.Add();
                    int nRow = WP_dataGridView.Rows.Count - 1;
                    WP_dataGridView.Rows[nRow].Cells[1].Value = User_Value[n][0];
                    WP_dataGridView.Rows[nRow].Cells[2].Value = User_Value[n][1];
                    WP_dataGridView.Rows[nRow].Cells[3].Value = User_Value[n][2];
                    WP_dataGridView.Rows[nRow].Cells[4].Value = User_Value[n][3];
                    WP_dataGridView.Rows[nRow].Cells[5].Value = User_Value[n][4];
                    WP_dataGridView.Rows[nRow].Cells[6].Value = User_Value[n][5];
                    WP_dataGridView.Rows[nRow].Cells[7].Value = User_Value[n][6];
                    WP_dataGridView.Rows[nRow].Cells[8].Value = User_Value[n][7];
                    WP_dataGridView.Rows[nRow].Cells[9].Value = User_Value[n][8];
                    WP_dataGridView.Rows[nRow].Cells[10].Value = User_Value[n][9];
                    WP_dataGridView.Rows[nRow].Cells[11].Value = User_Value[n][10];
                    WP_dataGridView.Rows[nRow].Cells[12].Value = User_Value[n][11];
                    WP_dataGridView.Rows[nRow].Cells[13].Value = User_Value[n][12];
                    WP_dataGridView.Rows[nRow].Cells[14].Value = User_Value[n][13];
                    WP_dataGridView.Rows[nRow].Cells[15].Value = User_Value[n][14];
                    WP_dataGridView.Rows[nRow].Cells[16].Value = User_Value[n][15];
                }
            }
            catch { }
        }

        #endregion
        //////////////////////////////////////////////////외기 히트펌프/////////////////////////////////////////////////////////////////
        #region 7. 외기히트펌프
        public void Create_AirHP_Table()
        {
            new StackedHeaderDecorator(AirHP_dataGridView, DataGridViewAutoSizeColumnsMode.Fill);
            AirHP_dataGridView.Columns.Clear();
            AirHP_checkBoxColumn.HeaderText = "선택";
            AirHP_checkBoxColumn.Name = "check";
            AirHP_dataGridView.Columns.Add(AirHP_checkBoxColumn);

            AirHP_dataGridView.Columns.Add("A1", "번호");
            AirHP_dataGridView.Columns.Add("A2", "DB유형");
            AirHP_dataGridView.Columns.Add("A3", "명칭");

            DataGridViewComboBoxColumn 냉난방Combo = new DataGridViewComboBoxColumn();
            냉난방Combo.HeaderText = "난방/냉방";
            냉난방Combo.Items.AddRange("냉방", "냉난방");
            AirHP_dataGridView.Columns.Add(냉난방Combo);

            DataGridViewComboBoxColumn 연료Combo = new DataGridViewComboBoxColumn();
            연료Combo.HeaderText = "연료";
            연료Combo.Items.AddRange("전기", "가스");
            AirHP_dataGridView.Columns.Add(연료Combo);

            DataGridViewComboBoxColumn 공급유형Combo = new DataGridViewComboBoxColumn();
            공급유형Combo.HeaderText = "부하측.공급유형";
            공급유형Combo.Items.AddRange("직팽식", "수방식");
            AirHP_dataGridView.Columns.Add(공급유형Combo);

            AirHP_dataGridView.Columns.Add("A7", "냉방정격.용량.[kW]");
            AirHP_dataGridView.Columns.Add("A8", "냉방정격.COP.[-]");
            AirHP_dataGridView.Columns.Add("A9", "냉방정격.소비전력.[kW]");
            AirHP_dataGridView.Columns.Add("A10", "난방정격.용량.[kW]");
            AirHP_dataGridView.Columns.Add("A11", "난방정격.COP.[-]");
            AirHP_dataGridView.Columns.Add("A12", "난방정격.소비전력.[kW]");
            AirHP_dataGridView.Columns.Add("A13", "한랭지.용량.[kW]");
            AirHP_dataGridView.Columns.Add("A14", "한랭지.COP.[-]");
            AirHP_dataGridView.Columns.Add("A15", "한랭지.소비전력.[kW]");
            AirHP_dataGridView.Columns.Add("A16", "대기전력.[W]");
            AirHP_dataGridView.Columns.Add("A17", "대수.[EA]");

            DataGridViewComboBoxColumn 설치유형Combo = new DataGridViewComboBoxColumn();
            설치유형Combo.HeaderText = "설치";
            설치유형Combo.Items.AddRange("기존", "신규", "철거후신규");
            AirHP_dataGridView.Columns.Add(설치유형Combo);

            AirHP_dataGridView.Columns[0].Width = 40;
            AirHP_dataGridView.Columns[1].Width = 60;
            AirHP_dataGridView.Columns[2].Width = 60;
            AirHP_dataGridView.Columns[3].Width = 60;
            AirHP_dataGridView.Columns[4].Width = 70;
            AirHP_dataGridView.Columns[5].Width = 70;
            AirHP_dataGridView.Columns[6].Width = 70;

        }

        private void UserAirHP_Add_button_Click(object sender, EventArgs e)
        {
            int nRow = AirHP_dataGridView.Rows.Add();
            Load_AirHP_Num();

            AirHP_dataGridView.Rows[nRow].Cells[2].Value = "도면";
        }

        private void DefaultAirHP_Add_button_Click(object sender, EventArgs e)
        {
            ArrayList SelectHP = new ArrayList();
            int nRow = AirHP_dataGridView.Rows.Add();
            Load_AirHP_Num();
            AirHP_dataGridView.Rows[nRow].Cells[2].Value = "기본";

            AirHP_DB air_db = new AirHP_DB("기본DB 적용", null);
            DialogResult result = air_db.ShowDialog();
            if (result == DialogResult.OK)
            {
                try
                {
                    if (air_db.SelectHP.Contains("등급"))
                    {

                        string[] token = air_db.SelectHP.Split("등급");
                        SelectHP.Clear();
                        foreach (var item in token)
                        {
                            SelectHP.Add(item.ToString());
                        }
                        string[][] CoolingValue = Program.DB.getValue(DB.type.BaseDB_Cooling, "AirCon", "냉방표준성능,대기전력", "명칭='" + SelectHP[0].ToString() + "등급<4kW' and 열원='" + air_db.Carrier + "'");

                        string[][] HeatingValue = Program.DB.getValue(DB.type.BaseDB_Heating, "히트펌프", "정격COP,한랭지COP", "등급='" + SelectHP[0].ToString() + "등급'and 연료='" + air_db.Carrier + "'");

                        AirHP_dataGridView.Rows[nRow].Cells[4].Value = air_db.HC;
                        AirHP_dataGridView.Rows[nRow].Cells[5].Value = air_db.Carrier;
                        if (CoolingValue.Length > 0)
                        {
                            AirHP_dataGridView.Rows[nRow].Cells[8].Value = CoolingValue[0][0];
                            AirHP_dataGridView.Rows[nRow].Cells[16].Value = CoolingValue[0][1];
                        }
                        if (HeatingValue.Length > 0)
                        {
                            AirHP_dataGridView.Rows[nRow].Cells[11].Value = HeatingValue[0][0];
                            AirHP_dataGridView.Rows[nRow].Cells[14].Value = HeatingValue[0][1];
                        }


                    }
                }
                catch { }
            }
        }

        private void AirHP_dataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                double 냉방용량 = 0, 냉방소비전력 = 0, 냉방COP = 0, 난방용량 = 0, 난방소비전력 = 0, 난방COP = 0, 한랭지용량 = 0, 한랭지소비전력 = 0, 한랭지COP = 0;
                if (AirHP_dataGridView.Rows[e.RowIndex].Cells[7].Value != null && Information.IsNumeric(AirHP_dataGridView.Rows[e.RowIndex].Cells[7].Value.ToString()))
                {
                    냉방용량 = Convert.ToDouble(AirHP_dataGridView.Rows[e.RowIndex].Cells[7].Value.ToString());
                }
                if (AirHP_dataGridView.Rows[e.RowIndex].Cells[8].Value != null && Information.IsNumeric(AirHP_dataGridView.Rows[e.RowIndex].Cells[8].Value.ToString()))
                {
                    냉방COP = Convert.ToDouble(AirHP_dataGridView.Rows[e.RowIndex].Cells[8].Value.ToString());
                }
                if (AirHP_dataGridView.Rows[e.RowIndex].Cells[9].Value != null && Information.IsNumeric(AirHP_dataGridView.Rows[e.RowIndex].Cells[9].Value.ToString()))
                {
                    냉방소비전력 = Convert.ToDouble(AirHP_dataGridView.Rows[e.RowIndex].Cells[9].Value.ToString());
                }
                if (AirHP_dataGridView.Rows[e.RowIndex].Cells[10].Value != null && Information.IsNumeric(AirHP_dataGridView.Rows[e.RowIndex].Cells[10].Value.ToString()))
                {
                    난방용량 = Convert.ToDouble(AirHP_dataGridView.Rows[e.RowIndex].Cells[10].Value.ToString());
                }
                if (AirHP_dataGridView.Rows[e.RowIndex].Cells[11].Value != null && Information.IsNumeric(AirHP_dataGridView.Rows[e.RowIndex].Cells[11].Value.ToString()))
                {
                    난방COP = Convert.ToDouble(AirHP_dataGridView.Rows[e.RowIndex].Cells[11].Value.ToString());
                }
                if (AirHP_dataGridView.Rows[e.RowIndex].Cells[12].Value != null && Information.IsNumeric(AirHP_dataGridView.Rows[e.RowIndex].Cells[12].Value.ToString()))
                {
                    난방소비전력 = Convert.ToDouble(AirHP_dataGridView.Rows[e.RowIndex].Cells[12].Value.ToString());
                }
                if (AirHP_dataGridView.Rows[e.RowIndex].Cells[13].Value != null && Information.IsNumeric(AirHP_dataGridView.Rows[e.RowIndex].Cells[13].Value.ToString()))
                {
                    한랭지용량 = Convert.ToDouble(AirHP_dataGridView.Rows[e.RowIndex].Cells[13].Value.ToString());
                }
                if (AirHP_dataGridView.Rows[e.RowIndex].Cells[14].Value != null && Information.IsNumeric(AirHP_dataGridView.Rows[e.RowIndex].Cells[14].Value.ToString()))
                {
                    한랭지COP = Convert.ToDouble(AirHP_dataGridView.Rows[e.RowIndex].Cells[14].Value.ToString());
                }
                if (AirHP_dataGridView.Rows[e.RowIndex].Cells[15].Value != null && Information.IsNumeric(AirHP_dataGridView.Rows[e.RowIndex].Cells[15].Value.ToString()))
                {
                    한랭지소비전력 = Convert.ToDouble(AirHP_dataGridView.Rows[e.RowIndex].Cells[15].Value.ToString());
                }

                if (냉방용량 > 0 && 냉방COP > 0)
                {
                    냉방소비전력 = 냉방용량 / 냉방COP;
                }
                if (냉방용량 > 0 && 냉방소비전력 > 0)
                {
                    냉방COP = 냉방용량 / 냉방소비전력;
                }
                if (난방용량 > 0 && 난방COP > 0)
                {
                    난방소비전력 = 난방용량 / 난방COP;
                }
                if (난방용량 > 0 && 난방소비전력 > 0)
                {
                    난방COP = 난방용량 / 난방소비전력;
                }
                if (한랭지용량 > 0 && 한랭지COP > 0)
                {
                    한랭지소비전력 = 한랭지용량 / 한랭지COP;
                }
                if (한랭지용량 > 0 && 한랭지소비전력 > 0)
                {
                    한랭지COP = 한랭지용량 / 한랭지소비전력;
                }

                if (냉방용량 > 0)
                {
                    AirHP_dataGridView.Rows[e.RowIndex].Cells[7].Value = string.Format("{0:F1}", 냉방용량);
                    AirHP_dataGridView.Rows[e.RowIndex].Cells[8].Value = string.Format("{0:F1}", 냉방COP);
                    AirHP_dataGridView.Rows[e.RowIndex].Cells[9].Value = string.Format("{0:F1}", 냉방소비전력);
                }

                if (AirHP_dataGridView.Rows[e.RowIndex].Cells[4].Value != null && AirHP_dataGridView.Rows[e.RowIndex].Cells[4].Value.ToString() == "냉난방")
                {
                    if (난방용량 > 0)
                    {
                        AirHP_dataGridView.Rows[e.RowIndex].Cells[10].Value = string.Format("{0:F1}", 난방용량);
                        AirHP_dataGridView.Rows[e.RowIndex].Cells[11].Value = string.Format("{0:F1}", 난방COP);
                        AirHP_dataGridView.Rows[e.RowIndex].Cells[12].Value = string.Format("{0:F1}", 난방소비전력);
                        AirHP_dataGridView.Rows[e.RowIndex].Cells[13].Value = string.Format("{0:F1}", 한랭지용량);
                        AirHP_dataGridView.Rows[e.RowIndex].Cells[14].Value = string.Format("{0:F1}", 한랭지COP);
                        AirHP_dataGridView.Rows[e.RowIndex].Cells[15].Value = string.Format("{0:F1}", 한랭지소비전력);
                    }
                }
                else if (AirHP_dataGridView.Rows[e.RowIndex].Cells[4].Value != null && AirHP_dataGridView.Rows[e.RowIndex].Cells[4].Value.ToString() == "냉방")
                {
                    AirHP_dataGridView.Rows[e.RowIndex].Cells[10].Value = "-";
                    AirHP_dataGridView.Rows[e.RowIndex].Cells[11].Value = "-";
                    AirHP_dataGridView.Rows[e.RowIndex].Cells[12].Value = "-";
                    AirHP_dataGridView.Rows[e.RowIndex].Cells[13].Value = "-";
                    AirHP_dataGridView.Rows[e.RowIndex].Cells[14].Value = "-";
                    AirHP_dataGridView.Rows[e.RowIndex].Cells[15].Value = "-";
                }
            }
        }


        private void AirHP_Remove_button_Click(object sender, EventArgs e)
        {
            AirHP_dataGridView.Rows.Remove(AirHP_dataGridView.Rows[HP_SelectRow]);
            Load_AirHP_Num();
        }

        private void AirHP_Copy_button_Click(object sender, EventArgs e)
        {
            int nRow = AirHP_dataGridView.Rows.Add();
            Load_AirHP_Num();
            if (AirHP_dataGridView.Rows[HP_SelectRow].Cells[2].Value == "도면")
            {
                DataGridViewComboBoxCell 연료Combo = new DataGridViewComboBoxCell();
                연료Combo.Items.Add("가스");
                연료Combo.Items.Add("전기");
                AirHP_dataGridView.Rows[nRow].Cells[4] = 연료Combo;

                DataGridViewComboBoxCell 공급유형Combo = new DataGridViewComboBoxCell();
                공급유형Combo.Items.Add("직팽식");
                공급유형Combo.Items.Add("수방식");
                AirHP_dataGridView.Rows[nRow].Cells[5] = 공급유형Combo;
            }


            for (int k = 2; k < 13; k++)
            {
                if (AirHP_dataGridView.Rows[HP_SelectRow].Cells[k].Value != null)
                {
                    AirHP_dataGridView.Rows[nRow].Cells[k].Value = AirHP_dataGridView.Rows[HP_SelectRow].Cells[k].Value;
                }
            }
            if (AirHP_dataGridView.Rows[HP_SelectRow].Cells[3].Value != null)
            {
                AirHP_dataGridView.Rows[nRow].Cells[3].Value = AirHP_dataGridView.Rows[HP_SelectRow].Cells[3].Value.ToString() + "_복사";
            }
        }
        private void AirHP_dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                AirHP_dataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
                HP_SelectRow = e.RowIndex;

            }
        }

        private void Load_AirHP_Num()
        {
            for (int k = 0; k < AirHP_dataGridView.RowCount; k++)
            {
                if (k + 1 < 10)
                { AirHP_dataGridView.Rows[k].Cells[1].Value = "UHP0" + (k + 1).ToString(); }
                else { AirHP_dataGridView.Rows[k].Cells[1].Value = "UHP" + (k + 1).ToString(); }
            }
        }

        private void AirHP_Save_button_Click(object sender, EventArgs e)
        {
            Program.DB.deleteValue(DB.type.ProjDB, "User_AirHP", "");
            for (int k = 0; k < AirHP_dataGridView.RowCount; k++)
            {
                String[] Value = new String[18];
                for (int i = 0; i < 18; i++)
                {
                    if (AirHP_dataGridView.Rows[k].Cells[i + 1].Value != null)
                    {
                        Value[i] = AirHP_dataGridView.Rows[k].Cells[i + 1].Value.ToString();
                    }
                    else { Value[i] = ""; }
                }
                Program.DB.setValue(DB.type.ProjDB, "User_AirHP", "번호,DB유형,명칭,난방냉방,연료,공급유형,냉방정격용량,냉방정격COP,냉방정격소비전력,난방정격용량,난방정격COP,난방정격소비전력,한랭지용량,한랭지COP,한랭지소비전력,대기전력,대수,설치",
                "'" + Value[0] + "','"
                 + Value[1] + "','" + Value[2] + "','" + Value[3] + "','" + Value[4] + "','" + Value[5] + "','" + Value[6] + "','" + Value[7] + "','" + Value[8] + "','" + Value[9] + "','" + Value[10] + "','" + Value[11] + "','" +
                  Value[12] + "','" + Value[13] + "','" + Value[14] + "','" + Value[15] + "','" + Value[16] + "','" + Value[17] + "'", "번호");
            }


            MessageBox.Show("저장되었습니다.");
        }


        private void Load_AirHP()
        {
            try
            {
                string[][] User_Value = Program.DB.getValue(DB.type.ProjDB, "User_AirHP", "번호,DB유형,명칭,난방냉방,연료,공급유형,냉방정격용량,냉방정격COP,냉방정격소비전력,난방정격용량,난방정격COP,난방정격소비전력,한랭지용량,한랭지COP,한랭지소비전력,대기전력,대수,설치", "");
                for (int n = 0; n < User_Value.Length; n++)
                {
                    AirHP_dataGridView.Rows.Add();

                    AirHP_dataGridView.Rows[n].Cells[1].Value = User_Value[n][0]; //번호
                    AirHP_dataGridView.Rows[n].Cells[2].Value = User_Value[n][1];
                    AirHP_dataGridView.Rows[n].Cells[3].Value = User_Value[n][2];
                    AirHP_dataGridView.Rows[n].Cells[4].Value = User_Value[n][3];
                    AirHP_dataGridView.Rows[n].Cells[5].Value = User_Value[n][4];
                    AirHP_dataGridView.Rows[n].Cells[6].Value = User_Value[n][5];
                    AirHP_dataGridView.Rows[n].Cells[7].Value = User_Value[n][6];
                    AirHP_dataGridView.Rows[n].Cells[8].Value = User_Value[n][7];
                    AirHP_dataGridView.Rows[n].Cells[9].Value = User_Value[n][8];
                    AirHP_dataGridView.Rows[n].Cells[10].Value = User_Value[n][9];
                    AirHP_dataGridView.Rows[n].Cells[11].Value = User_Value[n][10];
                    AirHP_dataGridView.Rows[n].Cells[12].Value = User_Value[n][11];
                    AirHP_dataGridView.Rows[n].Cells[13].Value = User_Value[n][12];
                    AirHP_dataGridView.Rows[n].Cells[14].Value = User_Value[n][13];
                    AirHP_dataGridView.Rows[n].Cells[15].Value = User_Value[n][14];
                    AirHP_dataGridView.Rows[n].Cells[16].Value = User_Value[n][15];
                    AirHP_dataGridView.Rows[n].Cells[17].Value = User_Value[n][16];
                    AirHP_dataGridView.Rows[n].Cells[18].Value = User_Value[n][17];
                }
            }
            catch { }

        }
        #endregion
        //////////////////////////////////////////////////지하수 히트펌프/////////////////////////////////////////////////////////////////
        #region 8. 지하수히트펌프
        public void Create_GWHP_Table()
        {
            new StackedHeaderDecorator(GWHP_dataGridView, DataGridViewAutoSizeColumnsMode.Fill);
            GWHP_dataGridView.Columns.Clear();
            GWHP_checkBoxColumn.HeaderText = "선택";
            GWHP_checkBoxColumn.Name = "check";
            GWHP_dataGridView.Columns.Add(GWHP_checkBoxColumn);

            GWHP_dataGridView.Columns.Add("A1", "번호");
            GWHP_dataGridView.Columns.Add("A2", "DB유형");
            GWHP_dataGridView.Columns.Add("A3", "명칭");
            GWHP_dataGridView.Columns.Add("A4", "연료");
            GWHP_dataGridView.Columns.Add("A5", "공급유형");
            GWHP_dataGridView.Columns.Add("A6", "수직/수평");
            GWHP_dataGridView.Columns.Add("A7", "냉방.용량.[kW]");
            GWHP_dataGridView.Columns.Add("A8", "냉방.EER.[kW]");
            GWHP_dataGridView.Columns.Add("A9", "냉방.소비전력.[kW]");
            GWHP_dataGridView.Columns.Add("A10", "난방정격(10℃).용량.[kW]");
            GWHP_dataGridView.Columns.Add("A11", "난방정격(10℃).COP.[kW]");
            GWHP_dataGridView.Columns.Add("A12", "난방정격(10℃).소비전력.[kW]");
            GWHP_dataGridView.Columns.Add("A13", "난방(15℃).용량.[kW]");
            GWHP_dataGridView.Columns.Add("A14", "난방(15℃).COP.[kW]");
            GWHP_dataGridView.Columns.Add("A15", "난방(15℃).소비전력.[kW]");
            GWHP_dataGridView.Columns.Add("A16", "대수.[EA]");
            GWHP_dataGridView.Columns.Add("A17", "냉수온도.입구.[℃]");
            GWHP_dataGridView.Columns.Add("A18", "냉수온도.출구.[℃]");
            GWHP_dataGridView.Columns.Add("A19", "압축기");
            GWHP_dataGridView.Columns.Add("A20", "증발기");
            GWHP_dataGridView.Columns.Add("A21", "설치");

            GWHP_dataGridView.Columns[0].Width = 40;
            GWHP_dataGridView.Columns[1].Width = 60;
            GWHP_dataGridView.Columns[2].Width = 60;
            GWHP_dataGridView.Columns[4].Width = 60;

        }

        private void UserGWHP_Add_button_Click(object sender, EventArgs e)
        {
            int nRow = GWHP_dataGridView.Rows.Add();
            Load_GWHP_Num();

            GWHP_dataGridView.Rows[nRow].Cells[2].Value = "도면";

            DataGridViewComboBoxCell 연료Combo = new DataGridViewComboBoxCell();
            연료Combo.Items.Add("전기");
            연료Combo.Items.Add("가스");
            GWHP_dataGridView.Rows[nRow].Cells[4] = 연료Combo;

            DataGridViewComboBoxCell 공급유형Combo = new DataGridViewComboBoxCell();
            공급유형Combo.Items.Add("직팽식");
            공급유형Combo.Items.Add("수방식");
            GWHP_dataGridView.Rows[nRow].Cells[5] = 공급유형Combo;

            DataGridViewComboBoxCell 수직수평Combo = new DataGridViewComboBoxCell();
            수직수평Combo.Items.Add("수직형");
            수직수평Combo.Items.Add("수평형");
            GWHP_dataGridView.Rows[nRow].Cells[6] = 수직수평Combo;

            DataGridViewComboBoxCell PressCombo = new DataGridViewComboBoxCell();
            PressCombo.Items.AddRange(new string[] { "왕복동", "스크롤", "스크류", "터보" });
            GWHP_dataGridView.Rows[nRow].Cells[19] = PressCombo;
            DataGridViewComboBoxCell EvapocomboBox = new DataGridViewComboBoxCell();
            EvapocomboBox.Items.AddRange(new string[] { "판형", "다관식" });
            GWHP_dataGridView.Rows[nRow].Cells[20] = EvapocomboBox;
            DataGridViewComboBoxCell 설치Combo = new DataGridViewComboBoxCell();
            설치Combo.Items.AddRange(new string[] { "기존", "신규", "철거후신규" });
            GWHP_dataGridView.Rows[nRow].Cells[21] = 설치Combo;
        }


        private void GWHP_dataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {

                double 냉방용량 = 0, 냉방소비전력 = 0, 냉방EER = 0;
                double 난방정격용량 = 0, 난방정격소비전력 = 0, 난방정격COP = 0;
                double 난방15도용량 = 0, 난방15도소비전력 = 0, 난방15도COP = 0;
                if (GWHP_dataGridView.Rows[e.RowIndex].Cells[7].Value != null && Information.IsNumeric(GWHP_dataGridView.Rows[e.RowIndex].Cells[7].Value.ToString()))
                {
                    냉방용량 = Convert.ToDouble(GWHP_dataGridView.Rows[e.RowIndex].Cells[7].Value.ToString());
                }
                if (GWHP_dataGridView.Rows[e.RowIndex].Cells[8].Value != null && Information.IsNumeric(GWHP_dataGridView.Rows[e.RowIndex].Cells[8].Value.ToString()))
                {
                    냉방EER = Convert.ToDouble(GWHP_dataGridView.Rows[e.RowIndex].Cells[8].Value.ToString());
                }
                if (GWHP_dataGridView.Rows[e.RowIndex].Cells[9].Value != null && Information.IsNumeric(GWHP_dataGridView.Rows[e.RowIndex].Cells[9].Value.ToString()))
                {
                    냉방소비전력 = Convert.ToDouble(GWHP_dataGridView.Rows[e.RowIndex].Cells[9].Value.ToString());
                }

                if (e.ColumnIndex == 7 || e.ColumnIndex == 8)
                {
                    if (냉방용량 > 0 && 냉방EER > 0)
                    {
                        냉방소비전력 = 냉방용량 / 냉방EER;
                        GWHP_dataGridView.Rows[e.RowIndex].Cells[9].Value = string.Format("{0:F1}", 냉방소비전력);
                    }
                }
                if (e.ColumnIndex == 7 || e.ColumnIndex == 9)
                {
                    if (냉방용량 > 0 && 냉방소비전력 > 0)
                    {
                        냉방EER = 냉방용량 / 냉방소비전력;
                        GWHP_dataGridView.Rows[e.RowIndex].Cells[8].Value = string.Format("{0:F1}", 냉방EER);
                    }
                }

                if (GWHP_dataGridView.Rows[e.RowIndex].Cells[10].Value != null && Information.IsNumeric(GWHP_dataGridView.Rows[e.RowIndex].Cells[10].Value.ToString()))
                {
                    난방정격용량 = Convert.ToDouble(GWHP_dataGridView.Rows[e.RowIndex].Cells[10].Value.ToString());
                }
                if (GWHP_dataGridView.Rows[e.RowIndex].Cells[11].Value != null && Information.IsNumeric(GWHP_dataGridView.Rows[e.RowIndex].Cells[11].Value.ToString()))
                {
                    난방정격COP = Convert.ToDouble(GWHP_dataGridView.Rows[e.RowIndex].Cells[11].Value.ToString());
                }
                if (GWHP_dataGridView.Rows[e.RowIndex].Cells[12].Value != null && Information.IsNumeric(GWHP_dataGridView.Rows[e.RowIndex].Cells[12].Value.ToString()))
                {
                    난방정격소비전력 = Convert.ToDouble(GWHP_dataGridView.Rows[e.RowIndex].Cells[12].Value.ToString());
                }

                if (e.ColumnIndex == 10 || e.ColumnIndex == 11)
                {
                    if (난방정격용량 > 0 && 난방정격COP > 0)
                    {
                        난방정격소비전력 = 난방정격용량 / 난방정격COP;
                        GWHP_dataGridView.Rows[e.RowIndex].Cells[12].Value = string.Format("{0:F1}", 난방정격소비전력);
                    }
                }
                if (e.ColumnIndex == 10 || e.ColumnIndex == 12)
                {
                    if (난방정격용량 > 0 && 난방정격소비전력 > 0)
                    {
                        난방정격COP = 난방정격용량 / 난방정격소비전력;
                        GWHP_dataGridView.Rows[e.RowIndex].Cells[11].Value = string.Format("{0:F1}", 난방정격COP);
                    }
                }
                if (GWHP_dataGridView.Rows[e.RowIndex].Cells[13].Value != null && Information.IsNumeric(GWHP_dataGridView.Rows[e.RowIndex].Cells[13].Value.ToString()))
                {
                    난방15도용량 = Convert.ToDouble(GWHP_dataGridView.Rows[e.RowIndex].Cells[13].Value.ToString());
                }
                if (GWHP_dataGridView.Rows[e.RowIndex].Cells[14].Value != null && Information.IsNumeric(GWHP_dataGridView.Rows[e.RowIndex].Cells[14].Value.ToString()))
                {
                    난방15도COP = Convert.ToDouble(GWHP_dataGridView.Rows[e.RowIndex].Cells[14].Value.ToString());
                }
                if (GWHP_dataGridView.Rows[e.RowIndex].Cells[15].Value != null && Information.IsNumeric(GWHP_dataGridView.Rows[e.RowIndex].Cells[15].Value.ToString()))
                {
                    난방15도소비전력 = Convert.ToDouble(GWHP_dataGridView.Rows[e.RowIndex].Cells[15].Value.ToString());
                }

                if (e.ColumnIndex == 13 || e.ColumnIndex == 14)
                {
                    if (난방15도용량 > 0 && 난방15도COP > 0)
                    {
                        난방15도소비전력 = 난방15도용량 / 난방15도COP;
                        GWHP_dataGridView.Rows[e.RowIndex].Cells[15].Value = string.Format("{0:F1}", 난방15도소비전력);
                    }
                }
                if (e.ColumnIndex == 13 || e.ColumnIndex == 15)
                {
                    if (난방15도용량 > 0 && 난방15도소비전력 > 0)
                    {
                        난방15도COP = 난방15도용량 / 난방15도소비전력;
                        GWHP_dataGridView.Rows[e.RowIndex].Cells[14].Value = string.Format("{0:F1}", 난방15도COP);
                    }
                }
            }
        }


        private void GWHP_Remove_button_Click(object sender, EventArgs e)
        {
            GWHP_dataGridView.Rows.Remove(GWHP_dataGridView.Rows[HP_SelectRow]);
            Load_GWHP_Num();
        }

        private void GWHP_Copy_button_Click(object sender, EventArgs e)
        {
            int nRow = GWHP_dataGridView.Rows.Add();
            Load_GWHP_Num();
            if (GWHP_dataGridView.Rows[HP_SelectRow].Cells[2].Value == "도면")
            {
                DataGridViewComboBoxCell 연료Combo = new DataGridViewComboBoxCell();
                연료Combo.Items.Add("가스");
                연료Combo.Items.Add("전기");
                GWHP_dataGridView.Rows[nRow].Cells[4] = 연료Combo;

                DataGridViewComboBoxCell 공급유형Combo = new DataGridViewComboBoxCell();
                공급유형Combo.Items.Add("직팽식");
                공급유형Combo.Items.Add("수방식");
                GWHP_dataGridView.Rows[nRow].Cells[5] = 공급유형Combo;

                DataGridViewComboBoxCell 수직수평Combo = new DataGridViewComboBoxCell();
                수직수평Combo.Items.Add("수직형");
                수직수평Combo.Items.Add("수평형");
                GWHP_dataGridView.Rows[nRow].Cells[6] = 수직수평Combo;
            }


            for (int k = 2; k < 18; k++)
            {
                if (GWHP_dataGridView.Rows[HP_SelectRow].Cells[k].Value != null)
                {
                    GWHP_dataGridView.Rows[nRow].Cells[k].Value = GWHP_dataGridView.Rows[HP_SelectRow].Cells[k].Value;
                }
            }
            if (GWHP_dataGridView.Rows[HP_SelectRow].Cells[3].Value != null)
            {
                GWHP_dataGridView.Rows[nRow].Cells[3].Value = GWHP_dataGridView.Rows[HP_SelectRow].Cells[3].Value.ToString() + "_복사";
            }
        }
        private void GWHP_dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                GWHP_dataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
                HP_SelectRow = e.RowIndex;

            }
        }

        private void Load_GWHP_Num()
        {
            for (int k = 0; k < GWHP_dataGridView.RowCount; k++)
            {
                if (k + 1 < 10)
                { GWHP_dataGridView.Rows[k].Cells[1].Value = "UWHP0" + (k + 1).ToString(); }
                else { GWHP_dataGridView.Rows[k].Cells[1].Value = "UWHP" + (k + 1).ToString(); }
            }
        }
        private void GWHP_Save_button_Click(object sender, EventArgs e)
        {

            Program.DB.deleteValue(DB.type.ProjDB, "User_GWHP", "");

            for (int k = 0; k < GWHP_dataGridView.RowCount; k++)
            {
                String[] Value = new String[21];
                for (int i = 1; i < 22; i++)
                {
                    if (GWHP_dataGridView.Rows[k].Cells[i].Value != null)
                    { Value[i - 1] = GWHP_dataGridView.Rows[k].Cells[i].Value.ToString(); }
                    else { Value[i - 1] = ""; }
                }
                Program.DB.setValue(DB.type.ProjDB, "User_GWHP", "번호,DB유형,명칭,연료,공급유형,수직수평,냉방용량,냉방EER,냉방소비전력,난방정격용량,난방정격COP,난방정격소비전력,난방등급2용량,난방등급2COP,난방등급2소비전력,대수,냉수입구온도,냉수출구온도,압축기,증발기,신규기존",
                "'" + Value[0] + "','"
                 + Value[1] + "','" + Value[2] + "','" + Value[3] + "','" + Value[4] + "','" + Value[5] + "','" + Value[6] + "','" + Value[7] + "','" + Value[8] + "','" + Value[9] + "','" + Value[10] + "','" +
                 Value[11] + "','" + Value[12] + "','" + Value[13] + "','" + Value[14] + "','" + Value[15] + "','" + Value[16] + "','" + Value[17] + "','" + Value[18] + "','" + Value[19] + "','" + Value[20]
                 + "'", "번호");
            }

            MessageBox.Show("저장되었습니다.");
        }


        private void Load_GWHP()
        {

            try
            {
                string[][] User_Value = Program.DB.getValue(DB.type.ProjDB, "User_GWHP", "번호,DB유형,명칭,연료,공급유형,수직수평,냉방용량,냉방EER,냉방소비전력,난방정격용량,난방정격COP,난방정격소비전력,난방등급2용량,난방등급2COP,난방등급2소비전력,대수,냉수입구온도,냉수출구온도,압축기,증발기,신규기존", "");
                for (int n = 0; n < User_Value.Length; n++)
                {
                    GWHP_dataGridView.Rows.Add();
                    int nRow = GWHP_dataGridView.Rows.Count - 1;
                    for (int i = 0; i < 21; i++)
                    { GWHP_dataGridView.Rows[nRow].Cells[1 + i].Value = User_Value[n][i]; }
                }
            }
            catch { }
        }
        #endregion

        //////////////////////////////////////////////////지열 히트펌프/////////////////////////////////////////////////////////////////
        #region 9.지열히트펌프
        public void Create_GroundHP_Table()
        {
            new StackedHeaderDecorator(GroundHP_dataGridView, DataGridViewAutoSizeColumnsMode.Fill);
            GroundHP_dataGridView.Columns.Clear();
            GroundHP_checkBoxColumn.HeaderText = "선택";
            GroundHP_checkBoxColumn.Name = "check";
            GroundHP_dataGridView.Columns.Add(GroundHP_checkBoxColumn);

            GroundHP_dataGridView.Columns.Add("A1", "번호");
            GroundHP_dataGridView.Columns.Add("A2", "DB유형");
            GroundHP_dataGridView.Columns.Add("A3", "명칭");
            GroundHP_dataGridView.Columns.Add("A4", "연료");
            GroundHP_dataGridView.Columns.Add("A5", "공급유형");
            GroundHP_dataGridView.Columns.Add("A6", "수직/수평");
            GroundHP_dataGridView.Columns.Add("A7", "냉방.용량.[kW]");
            GroundHP_dataGridView.Columns.Add("A8", "냉방.EER.[kW]");
            GroundHP_dataGridView.Columns.Add("A9", "냉방.소비전력.[kW]");
            GroundHP_dataGridView.Columns.Add("A10", "난방정격(0℃).용량.[kW]");
            GroundHP_dataGridView.Columns.Add("A11", "난방정격(0℃).COP.[kW]");
            GroundHP_dataGridView.Columns.Add("A12", "난방정격(0℃).소비전력.[kW]");
            GroundHP_dataGridView.Columns.Add("A13", "난방(5℃).용량.[kW]");
            GroundHP_dataGridView.Columns.Add("A14", "난방(5℃).COP.[kW]");
            GroundHP_dataGridView.Columns.Add("A15", "난방(5℃).소비전력.[kW]");
            GroundHP_dataGridView.Columns.Add("A16", "대수.[EA]");
            GroundHP_dataGridView.Columns.Add("A17", "냉수온도.입구.[℃]");
            GroundHP_dataGridView.Columns.Add("A18", "냉수온도.출구.[℃]");
            GroundHP_dataGridView.Columns.Add("A19", "압축기");
            GroundHP_dataGridView.Columns.Add("A20", "증발기");
            GroundHP_dataGridView.Columns.Add("A21", "설치");

            GroundHP_dataGridView.Columns[0].Width = 40;
            GroundHP_dataGridView.Columns[1].Width = 60;
            GroundHP_dataGridView.Columns[2].Width = 60;
            GroundHP_dataGridView.Columns[4].Width = 60;

        }

        private void UserGroundHP_Add_button_Click(object sender, EventArgs e)
        {
            int nRow = GroundHP_dataGridView.Rows.Add();
            Load_GroundHP_Num();

            GroundHP_dataGridView.Rows[nRow].Cells[2].Value = "도면";

            DataGridViewComboBoxCell 연료Combo = new DataGridViewComboBoxCell();
            연료Combo.Items.Add("전기");
            연료Combo.Items.Add("가스");
            GroundHP_dataGridView.Rows[nRow].Cells[4] = 연료Combo;

            DataGridViewComboBoxCell 공급유형Combo = new DataGridViewComboBoxCell();
            공급유형Combo.Items.Add("직팽식");
            공급유형Combo.Items.Add("수방식");
            GroundHP_dataGridView.Rows[nRow].Cells[5] = 공급유형Combo;

            DataGridViewComboBoxCell 수직수평Combo = new DataGridViewComboBoxCell();
            수직수평Combo.Items.Add("수직형");
            수직수평Combo.Items.Add("수평형");
            GroundHP_dataGridView.Rows[nRow].Cells[6] = 수직수평Combo;

            DataGridViewComboBoxCell PressCombo = new DataGridViewComboBoxCell();
            PressCombo.Items.AddRange(new string[] { "왕복동", "스크롤", "스크류", "터보" });
            GroundHP_dataGridView.Rows[nRow].Cells[19] = PressCombo;
            DataGridViewComboBoxCell EvapocomboBox = new DataGridViewComboBoxCell();
            EvapocomboBox.Items.AddRange(new string[] { "판형", "다관식" });
            GroundHP_dataGridView.Rows[nRow].Cells[20] = EvapocomboBox;
            DataGridViewComboBoxCell 설치Combo = new DataGridViewComboBoxCell();
            설치Combo.Items.AddRange(new string[] { "기존", "신규", "철거후신규" });
            GroundHP_dataGridView.Rows[nRow].Cells[21] = 설치Combo;
        }


        private void GroundHP_dataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {

                double 냉방용량 = 0, 냉방소비전력 = 0, 냉방EER = 0;
                double 난방정격용량 = 0, 난방정격소비전력 = 0, 난방정격COP = 0;
                double 난방5도용량 = 0, 난방5도소비전력 = 0, 난방5도COP = 0;
                if (GroundHP_dataGridView.Rows[e.RowIndex].Cells[7].Value != null && Information.IsNumeric(GroundHP_dataGridView.Rows[e.RowIndex].Cells[7].Value.ToString()))
                {
                    냉방용량 = Convert.ToDouble(GroundHP_dataGridView.Rows[e.RowIndex].Cells[7].Value.ToString());
                }
                if (GroundHP_dataGridView.Rows[e.RowIndex].Cells[8].Value != null && Information.IsNumeric(GroundHP_dataGridView.Rows[e.RowIndex].Cells[8].Value.ToString()))
                {
                    냉방EER = Convert.ToDouble(GroundHP_dataGridView.Rows[e.RowIndex].Cells[8].Value.ToString());
                }
                if (GroundHP_dataGridView.Rows[e.RowIndex].Cells[9].Value != null && Information.IsNumeric(GroundHP_dataGridView.Rows[e.RowIndex].Cells[9].Value.ToString()))
                {
                    냉방소비전력 = Convert.ToDouble(GroundHP_dataGridView.Rows[e.RowIndex].Cells[9].Value.ToString());
                }

                if (e.ColumnIndex == 7 || e.ColumnIndex == 8)
                {
                    if (냉방용량 > 0 && 냉방EER > 0)
                    {
                        냉방소비전력 = 냉방용량 / 냉방EER;
                        GroundHP_dataGridView.Rows[e.RowIndex].Cells[9].Value = string.Format("{0:F1}", 냉방소비전력);
                    }
                }
                if (e.ColumnIndex == 7 || e.ColumnIndex == 9)
                {
                    if (냉방용량 > 0 && 냉방소비전력 > 0)
                    {
                        냉방EER = 냉방용량 / 냉방소비전력;
                        GroundHP_dataGridView.Rows[e.RowIndex].Cells[8].Value = string.Format("{0:F1}", 냉방EER);
                    }
                }

                if (GroundHP_dataGridView.Rows[e.RowIndex].Cells[10].Value != null && Information.IsNumeric(GroundHP_dataGridView.Rows[e.RowIndex].Cells[10].Value.ToString()))
                {
                    난방정격용량 = Convert.ToDouble(GroundHP_dataGridView.Rows[e.RowIndex].Cells[10].Value.ToString());
                }
                if (GroundHP_dataGridView.Rows[e.RowIndex].Cells[11].Value != null && Information.IsNumeric(GroundHP_dataGridView.Rows[e.RowIndex].Cells[11].Value.ToString()))
                {
                    난방정격COP = Convert.ToDouble(GroundHP_dataGridView.Rows[e.RowIndex].Cells[11].Value.ToString());
                }
                if (GroundHP_dataGridView.Rows[e.RowIndex].Cells[12].Value != null && Information.IsNumeric(GroundHP_dataGridView.Rows[e.RowIndex].Cells[12].Value.ToString()))
                {
                    난방정격소비전력 = Convert.ToDouble(GroundHP_dataGridView.Rows[e.RowIndex].Cells[12].Value.ToString());
                }

                if (e.ColumnIndex == 10 || e.ColumnIndex == 11)
                {
                    if (난방정격용량 > 0 && 난방정격COP > 0)
                    {
                        난방정격소비전력 = 난방정격용량 / 난방정격COP;
                        GroundHP_dataGridView.Rows[e.RowIndex].Cells[12].Value = string.Format("{0:F1}", 난방정격소비전력);
                    }
                }
                if (e.ColumnIndex == 10 || e.ColumnIndex == 12)
                {
                    if (난방정격용량 > 0 && 난방정격소비전력 > 0)
                    {
                        난방정격COP = 난방정격용량 / 난방정격소비전력;
                        GroundHP_dataGridView.Rows[e.RowIndex].Cells[11].Value = string.Format("{0:F1}", 난방정격COP);
                    }
                }
                if (GroundHP_dataGridView.Rows[e.RowIndex].Cells[13].Value != null && Information.IsNumeric(GroundHP_dataGridView.Rows[e.RowIndex].Cells[13].Value.ToString()))
                {
                    난방5도용량 = Convert.ToDouble(GroundHP_dataGridView.Rows[e.RowIndex].Cells[13].Value.ToString());
                }
                if (GroundHP_dataGridView.Rows[e.RowIndex].Cells[14].Value != null && Information.IsNumeric(GroundHP_dataGridView.Rows[e.RowIndex].Cells[14].Value.ToString()))
                {
                    난방5도COP = Convert.ToDouble(GroundHP_dataGridView.Rows[e.RowIndex].Cells[14].Value.ToString());
                }
                if (GroundHP_dataGridView.Rows[e.RowIndex].Cells[15].Value != null && Information.IsNumeric(GroundHP_dataGridView.Rows[e.RowIndex].Cells[15].Value.ToString()))
                {
                    난방5도소비전력 = Convert.ToDouble(GroundHP_dataGridView.Rows[e.RowIndex].Cells[15].Value.ToString());
                }

                if (e.ColumnIndex == 13 || e.ColumnIndex == 14)
                {
                    if (난방5도용량 > 0 && 난방5도COP > 0)
                    {
                        난방5도소비전력 = 난방5도용량 / 난방5도COP;
                        GroundHP_dataGridView.Rows[e.RowIndex].Cells[15].Value = string.Format("{0:F1}", 난방5도소비전력);
                    }
                }
                if (e.ColumnIndex == 13 || e.ColumnIndex == 15)
                {
                    if (난방5도용량 > 0 && 난방5도소비전력 > 0)
                    {
                        난방5도COP = 난방5도용량 / 난방5도소비전력;
                        GroundHP_dataGridView.Rows[e.RowIndex].Cells[14].Value = string.Format("{0:F1}", 난방5도COP);
                    }
                }
            }
        }


        private void GroundHP_Remove_button_Click(object sender, EventArgs e)
        {
            GroundHP_dataGridView.Rows.Remove(GroundHP_dataGridView.Rows[HP_SelectRow]);
            Load_GroundHP_Num();
        }

        private void GroundHP_Copy_button_Click(object sender, EventArgs e)
        {
            int nRow = GroundHP_dataGridView.Rows.Add();
            Load_GroundHP_Num();
            if (GroundHP_dataGridView.Rows[HP_SelectRow].Cells[2].Value == "도면")
            {
                DataGridViewComboBoxCell 연료Combo = new DataGridViewComboBoxCell();
                연료Combo.Items.Add("가스");
                연료Combo.Items.Add("전기");
                GroundHP_dataGridView.Rows[nRow].Cells[4] = 연료Combo;

                DataGridViewComboBoxCell 공급유형Combo = new DataGridViewComboBoxCell();
                공급유형Combo.Items.Add("직팽식");
                공급유형Combo.Items.Add("수방식");
                GroundHP_dataGridView.Rows[nRow].Cells[5] = 공급유형Combo;

                DataGridViewComboBoxCell 수직수평Combo = new DataGridViewComboBoxCell();
                수직수평Combo.Items.Add("수직형");
                수직수평Combo.Items.Add("수평형");
                GroundHP_dataGridView.Rows[nRow].Cells[6] = 수직수평Combo;
            }


            for (int k = 2; k < 18; k++)
            {
                if (GroundHP_dataGridView.Rows[HP_SelectRow].Cells[k].Value != null)
                {
                    GroundHP_dataGridView.Rows[nRow].Cells[k].Value = GroundHP_dataGridView.Rows[HP_SelectRow].Cells[k].Value;
                }
            }
            if (GroundHP_dataGridView.Rows[HP_SelectRow].Cells[3].Value != null)
            {
                GroundHP_dataGridView.Rows[nRow].Cells[3].Value = GroundHP_dataGridView.Rows[HP_SelectRow].Cells[3].Value.ToString() + "_복사";
            }
        }
        private void GroundHP_dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                GroundHP_dataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
                HP_SelectRow = e.RowIndex;

            }
        }

        private void Load_GroundHP_Num()
        {
            for (int k = 0; k < GroundHP_dataGridView.RowCount; k++)
            {
                if (k + 1 < 10)
                { GroundHP_dataGridView.Rows[k].Cells[1].Value = "UGHP0" + (k + 1).ToString(); }
                else { GroundHP_dataGridView.Rows[k].Cells[1].Value = "UGHP" + (k + 1).ToString(); }
            }
        }
        private void GroundHP_Save_button_Click(object sender, EventArgs e)
        {

            Program.DB.deleteValue(DB.type.ProjDB, "User_GroundHP", "");

            for (int k = 0; k < GroundHP_dataGridView.RowCount; k++)
            {
                String[] Value = new String[21];
                for (int i = 1; i < 22; i++)
                {
                    if (GroundHP_dataGridView.Rows[k].Cells[i].Value != null)
                    { Value[i - 1] = GroundHP_dataGridView.Rows[k].Cells[i].Value.ToString(); }
                    else { Value[i - 1] = ""; }
                }
                Program.DB.setValue(DB.type.ProjDB, "User_GroundHP", "번호,DB유형,명칭,연료,공급유형,수직수평,냉방용량,냉방EER,냉방소비전력,난방정격용량,난방정격COP,난방정격소비전력,난방등급2용량,난방등급2COP,난방등급2소비전력,대수,냉수입구온도,냉수출구온도,압축기,증발기,신규기존",
                "'" + Value[0] + "','"
                 + Value[1] + "','" + Value[2] + "','" + Value[3] + "','" + Value[4] + "','" + Value[5] + "','" + Value[6] + "','" + Value[7] + "','" + Value[8] + "','" + Value[9] + "','" + Value[10] + "','" +
                 Value[11] + "','" + Value[12] + "','" + Value[13] + "','" + Value[14] + "','" + Value[15] + "','" + Value[16] + "','" + Value[17] + "','" + Value[18] + "','" + Value[19] + "','" + Value[20]
                 + "'", "번호");
            }

            MessageBox.Show("저장되었습니다.");
        }


        private void Load_GroundHP()
        {

            try
            {
                string[][] User_Value = Program.DB.getValue(DB.type.ProjDB, "User_GroundHP", "번호,DB유형,명칭,연료,공급유형,수직수평,냉방용량,냉방EER,냉방소비전력,난방정격용량,난방정격COP,난방정격소비전력,난방등급2용량,난방등급2COP,난방등급2소비전력,대수,냉수입구온도,냉수출구온도,압축기,증발기,신규기존", "");
                for (int n = 0; n < User_Value.Length; n++)
                {
                    GroundHP_dataGridView.Rows.Add();
                    int nRow = GroundHP_dataGridView.Rows.Count - 1;
                    for (int i = 0; i < 21; i++)
                    { GroundHP_dataGridView.Rows[nRow].Cells[1 + i].Value = User_Value[n][i]; }
                }
            }
            catch { }


        }
        #endregion

        ///////////////////////////////////////////////////펌프/////////////////////////////////////////////////////////////////
        #region 10.펌프
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
            Pump_dataGridView.Columns.Add("P9", "동력" + Environment.NewLine + "[W]");
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
            DataGridViewComboBoxColumn 설치유형Combo = new DataGridViewComboBoxColumn();
            설치유형Combo.HeaderText = "설치";
            설치유형Combo.Items.AddRange("기존", "신규", "철거후신규");
            Pump_dataGridView.Columns.Add(설치유형Combo);
            Pump_dataGridView.Columns[12].Width = 100;

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

            for (int k = 2; k < 13; k++)
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
                String[] Value = new String[10];
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
                //신규기존
                if (Pump_dataGridView.Rows[k].Cells[12].Value != null)
                { Value[9] = Pump_dataGridView.Rows[k].Cells[12].Value.ToString(); }
                else { Value[9] = ""; }

                Program.DB.setValue(DB.type.ProjDB, "User_Pump", "번호,명칭,종류,A효율,B효율,유량,동력,양정,대수",
                "'" + Value[0] + "','"
                 + Value[1] + "','" + Value[2] + "','" + Value[3] + "','" + Value[4] + "','" + Value[5] + "','" + Value[6] + "','" + Value[7] + "','" + Value[8] + "','"
                 + Value[9]
                 + "'", "번호");
            }
            MessageBox.Show("저장되었습니다.");
        }

        private void Load_Pump()
        {
            try
            {
                string[][] Value = Program.DB.getValue(DB.type.ProjDB, "User_Pump", "번호,명칭,종류,A효율,B효율,유량,동력,양정,대수,신규기존", "");
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
                    Pump_dataGridView.Rows[nRow].Cells[12].Value = Value[n][9];
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
        #endregion
        ///////////////////////////////////////////////////공급설비/////////////////////////////////////////////////////////////////
        #region 11.공급설비
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
            DataGridViewComboBoxColumn 설치유형Combo = new DataGridViewComboBoxColumn();
            설치유형Combo.HeaderText = "설치";
            설치유형Combo.Items.AddRange("기존", "신규", "철거후신규");
            ce_dataGridView.Columns.Add(설치유형Combo);
            ce_dataGridView.Columns[9].Width = 100;
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
            공급설비종류comboBox.Items.AddRange(new string[] { "실내기", "방열기", "팬코일유닛", "파워팬유닛", "복사난방", "복사냉방(벽)", "복사냉방(천장)", "바닥매립형컨백터" });
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

            if (ce_dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                ce_dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.White;

                if (ce_dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "냉방")
                {
                    DataGridViewComboBoxCell 공급설비종류comboBox = new DataGridViewComboBoxCell();
                    공급설비종류comboBox.Items.AddRange(new string[] { "실내기", "팬코일유닛", "복사냉방(벽)", "복사냉방(천장)", "바닥매립형컨백터" });
                    ce_dataGridView.Rows[e.RowIndex].Cells[4] = 공급설비종류comboBox;
                    ce_dataGridView.Rows[e.RowIndex].Cells[7].Value = "제어 없음";
                    ce_dataGridView.Rows[e.RowIndex].Cells[7].ReadOnly = true;
                }
                else if (ce_dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "냉난방")
                {
                    DataGridViewComboBoxCell 공급설비종류comboBox = new DataGridViewComboBoxCell();
                    공급설비종류comboBox.Items.AddRange(new string[] { "실내기", "팬코일유닛", "바닥매립형컨백터" });
                    ce_dataGridView.Rows[e.RowIndex].Cells[4] = 공급설비종류comboBox;
                    ce_dataGridView.Rows[e.RowIndex].Cells[7].ReadOnly = false;
                }
                else if (ce_dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "난방")
                {
                    DataGridViewComboBoxCell 공급설비종류comboBox = new DataGridViewComboBoxCell();
                    공급설비종류comboBox.Items.AddRange(new string[] { "실내기", "방열기", "팬코일유닛", "파워팬유닛", "복사난방", "바닥매립형컨백터" });
                    ce_dataGridView.Rows[e.RowIndex].Cells[4] = 공급설비종류comboBox;
                    ce_dataGridView.Rows[e.RowIndex].Cells[7].ReadOnly = false;
                }
                else return;
            }
            else return;


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
            공급설비종류comboBox.Items.AddRange(new string[] { "실내기", "방열기", "팬코일유닛", "파워팬유닛", "복사난방", "복사냉방(벽)", "복사냉방(천장)", "바닥매립형컨백터" });
            ce_dataGridView.Rows[nRow].Cells[4] = 공급설비종류comboBox;


            DataGridViewComboBoxCell 온도제어방식comboBox = new DataGridViewComboBoxCell();
            공급설비종류comboBox.Items.Add("제어 없음");
            공급설비종류comboBox.Items.Add("실별 온도제어");
            공급설비종류comboBox.Items.Add("on-off 자동온도제어");
            공급설비종류comboBox.Items.Add("재실기준 자동온도제어");
            ce_dataGridView.Rows[nRow].Cells[7] = 공급설비종류comboBox;

            for (int k = 2; k < 10; k++)
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
                String[] Value = new String[9];
                for (int i = 1; i < 10; i++)
                {
                    if (ce_dataGridView.Rows[k].Cells[i].Value != null)
                    { Value[i - 1] = ce_dataGridView.Rows[k].Cells[i].Value.ToString(); }
                    else { Value[i - 1] = ""; }
                }
                Program.DB.setValue(DB.type.ProjDB, "User_ce", "번호,명칭,난방냉방,종류,용량,소비전력,온도제어방식,대수,신규기존",
                "'" + Value[0] + "','"
                 + Value[1] + "','" + Value[2] + "','" + Value[3] + "','" + Value[4] + "','" + Value[5] + "','" + Value[6] + "','" + Value[7] + "','"
                 + Value[8]
                 + "'", "번호");
            }
            MessageBox.Show("저장되었습니다.");
        }
        private void Load_ce()
        {
            try
            {
                String[][] Value = Program.DB.getValue(DB.type.ProjDB, "User_ce", "번호,명칭,난방냉방,종류,용량,소비전력,온도제어방식,대수,신규기존", "");
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
                    공급설비종류comboBox.Items.AddRange(new string[] { "실내기", "방열기", "팬코일유닛", "파워팬유닛", "복사난방", "복사냉방(벽)", "복사냉방(천장)", "바닥매립형컨백터" });
                    ce_dataGridView.Rows[nRow].Cells[4] = 공급설비종류comboBox;


                    DataGridViewComboBoxCell 온도제어방식comboBox = new DataGridViewComboBoxCell();
                    온도제어방식comboBox.Items.Add("제어 없음");
                    온도제어방식comboBox.Items.Add("실별 온도제어");
                    온도제어방식comboBox.Items.Add("on-off 자동온도제어");
                    온도제어방식comboBox.Items.Add("재실기준 자동온도제어");
                    ce_dataGridView.Rows[nRow].Cells[7] = 온도제어방식comboBox;

                    for (int k = 0; k < 9; k++)
                    { ce_dataGridView.Rows[nRow].Cells[k + 1].Value = Value[n][k]; }
                }
            }
            catch { }

        }
        #endregion
        ///////////////////////////////////////////////////공냉식냉동기/////////////////////////////////////////////////////////////////
        #region 12. 공냉식냉동기
        public void Create_AirCooler_Table()
        {
            AirCooler_dataGridView.Rows.Clear();
            AirCooler_dataGridView.Columns.Clear();

            new StackedHeaderDecorator(AirCooler_dataGridView, DataGridViewAutoSizeColumnsMode.Fill);
            AirCooler_checkBoxColumn.HeaderText = "선택";
            AirCooler_checkBoxColumn.Name = "check";
            AirCooler_dataGridView.Columns.Add(AirCooler_checkBoxColumn);

            AirCooler_dataGridView.Columns.Add("A1", "번호");
            AirCooler_dataGridView.Columns.Add("A2", "DB유형");
            AirCooler_dataGridView.Columns.Add("A3", "명칭");
            AirCooler_dataGridView.Columns.Add("A4", "냉방성능.출력[kW]");
            AirCooler_dataGridView.Columns.Add("A5", "냉방성능.소비전력[kW]");
            AirCooler_dataGridView.Columns.Add("A6", "냉방성능.EER");

            DataGridViewComboBoxColumn PressCombo = new DataGridViewComboBoxColumn();
            PressCombo.HeaderText = "압축기";
            PressCombo.Items.AddRange(new string[] { "왕복동", "스크롤", "스크류", "터보" });
            AirCooler_dataGridView.Columns.Add(PressCombo);

            DataGridViewComboBoxColumn 연료Combo = new DataGridViewComboBoxColumn();
            연료Combo.HeaderText = "연료";
            연료Combo.Items.AddRange(new string[] { "가스", "전기" });
            AirCooler_dataGridView.Columns.Add(연료Combo);

            AirCooler_dataGridView.Columns.Add("A9", "대기전력[W]");
            AirCooler_dataGridView.Columns.Add("A10", "대수.[EA]");

            DataGridViewComboBoxColumn 설치Combo = new DataGridViewComboBoxColumn();
            설치Combo.HeaderText = "설치";
            설치Combo.Items.AddRange(new string[] { "기존", "신규", "철거후신규" });
            AirCooler_dataGridView.Columns.Add(설치Combo);

            DataGridViewComboBoxColumn SupplycomboBox = new DataGridViewComboBoxColumn();
            SupplycomboBox.HeaderText = "부하공급";
            SupplycomboBox.Items.AddRange(new string[] { "직팽식", "수방식" });
            AirCooler_dataGridView.Columns.Add(SupplycomboBox);

            DataGridViewComboBoxColumn EvapocomboBox = new DataGridViewComboBoxColumn();
            EvapocomboBox.HeaderText = "증발기";
            EvapocomboBox.Items.AddRange(new string[] { "판형", "다관식" });
            AirCooler_dataGridView.Columns.Add(EvapocomboBox);

            AirCooler_dataGridView.Columns.Add("A14", "냉수온도.입구[℃]");
            AirCooler_dataGridView.Columns.Add("A15", "냉수온도.출구[℃]");

            AirCooler_dataGridView.Columns[0].Width = 40;
            AirCooler_dataGridView.Columns[1].Width = 60;
            AirCooler_dataGridView.Columns[2].Width = 60;
            AirCooler_dataGridView.Columns[3].Width = 80;

            AirCooler_dataGridView.Columns[1].ReadOnly = true;
            AirCooler_dataGridView.Columns[2].ReadOnly = true;
        }

        private void DefaultAirCooler_Add_button_Click(object sender, EventArgs e)
        {
            Cooling_AirCooler AirCooler = new Cooling_AirCooler("기본DB 적용"); //기존 표준값 제품 선택을 함
            DialogResult result = AirCooler.ShowDialog();
            if (result == DialogResult.OK)
            {
                if (AirCooler.SelectAirCooler != null)
                {
                    foreach (string SAC in AirCooler.SelectAirCooler)
                    {
                        string[][] DefaultDB_Value = Program.DB.getValue(DB.type.BaseDB_Cooling, "AirCon", "번호,냉방표준성능,대기전력,열원",
                            "번호='" + SAC + "'");
                        //시작
                        int nRow = AirCooler_dataGridView.Rows.Add();
                        Load_AirCooler_Num();
                        AirCooler_dataGridView.Rows[nRow].Cells[2].Value = "기본";
                        AirCooler_dataGridView.Rows[nRow].Cells[3].Value = null;
                        AirCooler_dataGridView.Rows[nRow].Cells[4].Value = null;
                        AirCooler_dataGridView.Rows[nRow].Cells[5].Value = null;
                        AirCooler_dataGridView.Rows[nRow].Cells[6].Value = DefaultDB_Value[0][1];
                        AirCooler_dataGridView.Rows[nRow].Cells[7].Value = null;
                        AirCooler_dataGridView.Rows[nRow].Cells[8].Value = DefaultDB_Value[0][3];
                        AirCooler_dataGridView.Rows[nRow].Cells[9].Value = DefaultDB_Value[0][2];
                        AirCooler_dataGridView.Rows[nRow].Cells[10].Value = null;
                        AirCooler_dataGridView.Rows[nRow].Cells[11].Value = null;
                        AirCooler_dataGridView.Rows[nRow].Cells[12].Value = null;
                        AirCooler_dataGridView.Rows[nRow].Cells[13].Value = null;
                        AirCooler_dataGridView.Rows[nRow].Cells[14].Value = null;
                        AirCooler_dataGridView.Rows[nRow].Cells[15].Value = null;

                    }
                }
            }
        }

        private void UserAirCooler_Add_button_Click(object sender, EventArgs e)
        {
            int nRow = AirCooler_dataGridView.Rows.Add();//한줄 추가
            Load_AirCooler_Num();//번호생성기
            AirCooler_dataGridView.Rows[nRow].Cells[2].Value = "도면";
        }

        private void AirCooler_Remove_button_Click(object sender, EventArgs e)
        {
            int SelectRow;
            for (int i = 0; i < AirCooler_dataGridView.Rows.Count; i++)
            {
                if (Convert.ToBoolean(AirCooler_dataGridView.Rows[i].Cells[0].Value))
                {
                    SelectRow = i;
                    AirCooler_dataGridView.Rows.Remove(AirCooler_dataGridView.Rows[SelectRow]);
                }
            }
            Load_AirCooler_Num();
        }

        private void AirCooler_Copy_button_Click(object sender, EventArgs e)
        {
            for (int j = 0; j < AirCooler_dataGridView.RowCount; j++)
            {
                if (Convert.ToBoolean(AirCooler_dataGridView.Rows[j].Cells[0].Value))
                {
                    int nRow = AirCooler_dataGridView.Rows.Add();
                    for (int k = 2; k < 16; k++)
                    {
                        AirCooler_dataGridView.Rows[nRow].Cells[k].Value = AirCooler_dataGridView.Rows[j].Cells[k].Value;
                    }
                    Load_AirCooler_Num();
                }
            }
        }

        private void Load_AirCooler_Num() //번호생성기
        {
            for (int k = 0; k < AirCooler_dataGridView.RowCount; k++)
            {
                AirCooler_dataGridView.Rows[k].Cells[1].Value = "UAC" + string.Format("{0:00}", k + 1);
            }
        }

        private void AirCooler_dataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

            double 냉방용량 = 0, 냉방소비전력 = 0, 냉방EER = 0;
            if (AirCooler_dataGridView.Rows[e.RowIndex].Cells[4].Value != null && Information.IsNumeric(AirCooler_dataGridView.Rows[e.RowIndex].Cells[4].Value.ToString()))
            {
                냉방용량 = Convert.ToDouble(AirCooler_dataGridView.Rows[e.RowIndex].Cells[4].Value.ToString());
            }
            if (AirCooler_dataGridView.Rows[e.RowIndex].Cells[5].Value != null && Information.IsNumeric(AirCooler_dataGridView.Rows[e.RowIndex].Cells[5].Value.ToString()))
            {
                냉방소비전력 = Convert.ToDouble(AirCooler_dataGridView.Rows[e.RowIndex].Cells[5].Value.ToString());
            }
            if (AirCooler_dataGridView.Rows[e.RowIndex].Cells[6].Value != null && Information.IsNumeric(AirCooler_dataGridView.Rows[e.RowIndex].Cells[6].Value.ToString()))
            {
                냉방EER = Convert.ToDouble(AirCooler_dataGridView.Rows[e.RowIndex].Cells[6].Value.ToString());
            }

            if (e.ColumnIndex == 4 || e.ColumnIndex == 6)
            {
                if (냉방용량 > 0 && 냉방EER > 0)
                {
                    냉방소비전력 = 냉방용량 / 냉방EER;
                    AirCooler_dataGridView.Rows[e.RowIndex].Cells[5].Value = string.Format("{0:F1}", 냉방소비전력);
                }
            }
            if (e.ColumnIndex == 4 || e.ColumnIndex == 5)
            {
                if (냉방용량 > 0 && 냉방소비전력 > 0)
                {
                    냉방EER = 냉방용량 / 냉방소비전력;
                    AirCooler_dataGridView.Rows[e.RowIndex].Cells[6].Value = string.Format("{0:F1}", 냉방EER);
                }
            }

            if (e.ColumnIndex == 12)
            {
                int k = e.RowIndex;
                if (AirCooler_dataGridView.Rows[k].Cells[12].Value.ToString() == "직팽식")
                {
                    AirCooler_dataGridView.Rows[k].Cells[13].Value = "";
                    AirCooler_dataGridView.Rows[k].Cells[14].Value = "";
                    AirCooler_dataGridView.Rows[k].Cells[15].Value = "";

                    AirCooler_dataGridView.Rows[k].Cells[13].ReadOnly = true;
                    AirCooler_dataGridView.Rows[k].Cells[14].ReadOnly = true;
                    AirCooler_dataGridView.Rows[k].Cells[15].ReadOnly = true;
                }
                else if (AirCooler_dataGridView.Rows[k].Cells[12].Value.ToString() == "수방식")
                {
                    AirCooler_dataGridView.Rows[k].Cells[13].ReadOnly = false;
                    AirCooler_dataGridView.Rows[k].Cells[14].ReadOnly = false;
                    AirCooler_dataGridView.Rows[k].Cells[15].ReadOnly = false;
                }
            }

        }

        private void AirCooler_Save_button_Click(object sender, EventArgs e)
        {
            Program.DB.deleteValue(DB.type.ProjDB, "User_AirCooler", "");

            for (int k = 0; k < AirCooler_dataGridView.RowCount; k++)
            {
                String[] Value = new String[15];
                for (int i = 0; i < 15; i++)
                {
                    if (AirCooler_dataGridView.Rows[k].Cells[i + 1].Value != null)
                    {
                        Value[i] = AirCooler_dataGridView.Rows[k].Cells[i + 1].Value.ToString();
                    }
                    else { Value[i] = ""; }
                }
                Program.DB.setValue(DB.type.ProjDB, "User_AirCooler", "번호,DB유형,명칭,냉방출력,냉방소비전력,EER,압축기,연료,대기전력,대수,설치,부하측공급형식,증발기,냉수입구온도,냉수출구온도",
                "'" + Value[0] + "','"
                 + Value[1] + "','" + Value[2] + "','" + Value[3] + "','" + Value[4] + "','" + Value[5] + "','"
                 + Value[6] + "','" + Value[7] + "','" + Value[8] + "','" + Value[9] + "','" + Value[10] + "','"
                 + Value[11] + "','" + Value[12] + "','" + Value[13] + "','" + Value[14] + "'", "번호");
            }
            MessageBox.Show("저장되었습니다.");
        }

        private void Load_AirCooler()
        {
            try
            {
                string[][] User_Value = Program.DB.getValue(DB.type.ProjDB, "User_AirCooler", "번호,DB유형,명칭,냉방출력,냉방소비전력,EER,압축기,연료,대기전력,대수,설치,부하측공급형식,증발기,냉수입구온도,냉수출구온도", "");

                for (int n = 0; n < User_Value.Length; n++)
                {
                    AirCooler_dataGridView.Rows.Add();
                    int nRow = AirCooler_dataGridView.Rows.Count - 1;
                    AirCooler_dataGridView.Rows[nRow].Cells[1].Value = User_Value[n][0];  //번호
                    AirCooler_dataGridView.Rows[nRow].Cells[2].Value = User_Value[n][1];  //DB유형
                    AirCooler_dataGridView.Rows[nRow].Cells[3].Value = User_Value[n][2];  //명칭
                    AirCooler_dataGridView.Rows[nRow].Cells[4].Value = User_Value[n][3];  //냉방성능.냉방출력[kW]
                    AirCooler_dataGridView.Rows[nRow].Cells[5].Value = User_Value[n][4];  //냉방성능.소비전력[kW]
                    AirCooler_dataGridView.Rows[nRow].Cells[6].Value = User_Value[n][5];  //냉방성능.EER
                    AirCooler_dataGridView.Rows[nRow].Cells[7].Value = User_Value[n][6]; //압축기
                    AirCooler_dataGridView.Rows[nRow].Cells[8].Value = User_Value[n][7];  //열원
                    AirCooler_dataGridView.Rows[nRow].Cells[9].Value = User_Value[n][8];  //대기전력[W]
                    AirCooler_dataGridView.Rows[nRow].Cells[10].Value = User_Value[n][9];  //대수
                    AirCooler_dataGridView.Rows[nRow].Cells[11].Value = User_Value[n][10]; //설치
                    AirCooler_dataGridView.Rows[nRow].Cells[12].Value = User_Value[n][11];  //부하측공급방식
                    AirCooler_dataGridView.Rows[nRow].Cells[13].Value = User_Value[n][12];  //증발기
                    AirCooler_dataGridView.Rows[nRow].Cells[14].Value = User_Value[n][13]; //냉수입구온도
                    AirCooler_dataGridView.Rows[nRow].Cells[15].Value = User_Value[n][14];  //냉수출구온도
                }
            }
            catch { }
        }
        #endregion
        ///////////////////////////////////////////////////수냉식냉동기/////////////////////////////////////////////////////////////////
        #region 13. 수냉식냉동기
        public void Create_WaterCooler_Table()
        {
            WaterCooler_dataGridView.Rows.Clear();
            WaterCooler_dataGridView.Columns.Clear();

            new StackedHeaderDecorator(WaterCooler_dataGridView, DataGridViewAutoSizeColumnsMode.Fill);
            WaterCooler_checkBoxColumn.HeaderText = "선택";
            WaterCooler_checkBoxColumn.Name = "check";
            WaterCooler_dataGridView.Columns.Add(WaterCooler_checkBoxColumn);

            WaterCooler_dataGridView.Columns.Add("A1", "번호");
            WaterCooler_dataGridView.Columns.Add("A2", "DB유형");
            WaterCooler_dataGridView.Columns.Add("A3", "명칭");
            WaterCooler_dataGridView.Columns.Add("A4", "냉방성능.출력[kW]");
            WaterCooler_dataGridView.Columns.Add("A5", "냉방성능.소비전력[kW]");
            WaterCooler_dataGridView.Columns.Add("A6", "냉방성능.EER");

            DataGridViewComboBoxColumn PressCombo = new DataGridViewComboBoxColumn();
            PressCombo.HeaderText = "압축기";
            PressCombo.Items.AddRange(new string[] { "왕복동", "스크롤", "스크류", "터보" });
            WaterCooler_dataGridView.Columns.Add(PressCombo);

            DataGridViewComboBoxColumn 연료Combo = new DataGridViewComboBoxColumn();
            연료Combo.HeaderText = "연료";
            연료Combo.Items.AddRange(new string[] { "가스", "전기" });
            WaterCooler_dataGridView.Columns.Add(연료Combo);

            WaterCooler_dataGridView.Columns.Add("A9", "대기전력[W]");
            WaterCooler_dataGridView.Columns.Add("A10", "대수.[EA]");

            DataGridViewComboBoxColumn 설치Combo = new DataGridViewComboBoxColumn();
            설치Combo.HeaderText = "설치";
            설치Combo.Items.AddRange(new string[] { "기존", "신규", "철거후신규" });
            WaterCooler_dataGridView.Columns.Add(설치Combo);

            DataGridViewComboBoxColumn EvapocomboBox = new DataGridViewComboBoxColumn();
            EvapocomboBox.HeaderText = "증발기";
            EvapocomboBox.Items.AddRange(new string[] { "판형", "다관식" });
            WaterCooler_dataGridView.Columns.Add(EvapocomboBox);

            WaterCooler_dataGridView.Columns.Add("A14", "냉수온도.입구[℃]");
            WaterCooler_dataGridView.Columns.Add("A15", "냉수온도.출구[℃]");

            WaterCooler_dataGridView.Columns[0].Width = 40;
            WaterCooler_dataGridView.Columns[1].Width = 60;
            WaterCooler_dataGridView.Columns[2].Width = 60;
            WaterCooler_dataGridView.Columns[3].Width = 80;

            WaterCooler_dataGridView.Columns[1].ReadOnly = true;
            WaterCooler_dataGridView.Columns[2].ReadOnly = true;
        }

        private void DefaultWaterCooler_Add_button_Click(object sender, EventArgs e)
        {
            Cooling_WaterCooler WaterCooler = new Cooling_WaterCooler("기본DB 적용"); //기존 표준값 제품 선택을 함
            DialogResult result = WaterCooler.ShowDialog();
            if (result == DialogResult.OK)
            {
                //if (WaterCooler.SelectWaterCooler != null)
                //{
                //    foreach (string SAC in WaterCooler.SelectWaterCooler)
                //    {
                //        string[][] DefaultDB_Value = Program.DB.getValue(DB.type.BaseDB_Cooling, "AirCon", "번호,냉방표준성능,대기전력,열원",
                //            "번호='" + SAC + "'");
                //        //시작
                //        int nRow = WaterCooler_dataGridView.Rows.Add();
                //        Load_WaterCooler_Num();
                //        WaterCooler_dataGridView.Rows[nRow].Cells[2].Value = "기본";
                //        WaterCooler_dataGridView.Rows[nRow].Cells[3].Value = null;
                //        WaterCooler_dataGridView.Rows[nRow].Cells[4].Value = null;
                //        WaterCooler_dataGridView.Rows[nRow].Cells[5].Value = null;
                //        WaterCooler_dataGridView.Rows[nRow].Cells[6].Value = DefaultDB_Value[0][1];
                //        WaterCooler_dataGridView.Rows[nRow].Cells[7].Value = null;
                //        WaterCooler_dataGridView.Rows[nRow].Cells[8].Value = DefaultDB_Value[0][3];
                //        WaterCooler_dataGridView.Rows[nRow].Cells[9].Value = DefaultDB_Value[0][2];
                //        WaterCooler_dataGridView.Rows[nRow].Cells[10].Value = null;
                //        WaterCooler_dataGridView.Rows[nRow].Cells[11].Value = null;
                //        WaterCooler_dataGridView.Rows[nRow].Cells[12].Value = null;
                //        WaterCooler_dataGridView.Rows[nRow].Cells[13].Value = null;
                //        WaterCooler_dataGridView.Rows[nRow].Cells[14].Value = null;
                //        WaterCooler_dataGridView.Rows[nRow].Cells[15].Value = null;

                //    }
                //}
            }
        }

        private void UserWaterCooler_Add_button_Click(object sender, EventArgs e)
        {
            int nRow = WaterCooler_dataGridView.Rows.Add();//한줄 추가
            Load_WaterCooler_Num();//번호생성기
            WaterCooler_dataGridView.Rows[nRow].Cells[2].Value = "도면";
        }

        private void WaterCooler_Remove_button_Click(object sender, EventArgs e)
        {
            int SelectRow;
            for (int i = 0; i < WaterCooler_dataGridView.Rows.Count; i++)
            {
                if (Convert.ToBoolean(WaterCooler_dataGridView.Rows[i].Cells[0].Value))
                {
                    SelectRow = i;
                    WaterCooler_dataGridView.Rows.Remove(WaterCooler_dataGridView.Rows[SelectRow]);
                }
            }
            Load_WaterCooler_Num();
        }

        private void WaterCooler_Copy_button_Click(object sender, EventArgs e)
        {
            for (int j = 0; j < WaterCooler_dataGridView.RowCount; j++)
            {
                if (Convert.ToBoolean(WaterCooler_dataGridView.Rows[j].Cells[0].Value))
                {
                    int nRow = WaterCooler_dataGridView.Rows.Add();
                    for (int k = 2; k < 15; k++)
                    {
                        WaterCooler_dataGridView.Rows[nRow].Cells[k].Value = WaterCooler_dataGridView.Rows[j].Cells[k].Value;
                    }
                    Load_WaterCooler_Num();
                }
            }
        }

        private void Load_WaterCooler_Num() //번호생성기
        {
            for (int k = 0; k < WaterCooler_dataGridView.RowCount; k++)
            {
                WaterCooler_dataGridView.Rows[k].Cells[1].Value = "UWC" + string.Format("{0:00}", k + 1);
            }
        }

        private void WaterCooler_dataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

            double 냉방용량 = 0, 냉방소비전력 = 0, 냉방EER = 0;
            if (WaterCooler_dataGridView.Rows[e.RowIndex].Cells[4].Value != null && Information.IsNumeric(WaterCooler_dataGridView.Rows[e.RowIndex].Cells[4].Value.ToString()))
            {
                냉방용량 = Convert.ToDouble(WaterCooler_dataGridView.Rows[e.RowIndex].Cells[4].Value.ToString());
            }
            if (WaterCooler_dataGridView.Rows[e.RowIndex].Cells[5].Value != null && Information.IsNumeric(WaterCooler_dataGridView.Rows[e.RowIndex].Cells[5].Value.ToString()))
            {
                냉방소비전력 = Convert.ToDouble(WaterCooler_dataGridView.Rows[e.RowIndex].Cells[5].Value.ToString());
            }
            if (WaterCooler_dataGridView.Rows[e.RowIndex].Cells[6].Value != null && Information.IsNumeric(WaterCooler_dataGridView.Rows[e.RowIndex].Cells[6].Value.ToString()))
            {
                냉방EER = Convert.ToDouble(WaterCooler_dataGridView.Rows[e.RowIndex].Cells[6].Value.ToString());
            }

            if (e.ColumnIndex == 4 || e.ColumnIndex == 6)
            {
                if (냉방용량 > 0 && 냉방EER > 0)
                {
                    냉방소비전력 = 냉방용량 / 냉방EER;
                    WaterCooler_dataGridView.Rows[e.RowIndex].Cells[5].Value = string.Format("{0:F1}", 냉방소비전력);
                }
            }
            if (e.ColumnIndex == 4 || e.ColumnIndex == 5)
            {
                if (냉방용량 > 0 && 냉방소비전력 > 0)
                {
                    냉방EER = 냉방용량 / 냉방소비전력;
                    WaterCooler_dataGridView.Rows[e.RowIndex].Cells[6].Value = string.Format("{0:F1}", 냉방EER);
                }
            }
        }

        private void WaterCooler_Save_button_Click(object sender, EventArgs e)
        {
            Program.DB.deleteValue(DB.type.ProjDB, "User_WaterCooler", "");

            for (int k = 0; k < WaterCooler_dataGridView.RowCount; k++)
            {
                String[] Value = new String[14];
                for (int i = 0; i < 14; i++)
                {
                    if (WaterCooler_dataGridView.Rows[k].Cells[i + 1].Value != null)
                    {
                        Value[i] = WaterCooler_dataGridView.Rows[k].Cells[i + 1].Value.ToString();
                    }
                    else { Value[i] = ""; }
                }
                Program.DB.setValue(DB.type.ProjDB, "User_WaterCooler", "번호,DB유형,명칭,냉방출력,냉방소비전력,EER,압축기,연료,대기전력,대수,설치,증발기,냉수입구온도,냉수출구온도",
                "'" + Value[0] + "','"
                 + Value[1] + "','" + Value[2] + "','" + Value[3] + "','" + Value[4] + "','" + Value[5] + "','"
                 + Value[6] + "','" + Value[7] + "','" + Value[8] + "','" + Value[9] + "','" + Value[10] + "','"
                 + Value[11] + "','" + Value[12] + "','" + Value[13] + "'", "번호");
            }
            MessageBox.Show("저장되었습니다.");
        }

        private void Load_WaterCooler()
        {
            try
            {
                string[][] User_Value = Program.DB.getValue(DB.type.ProjDB, "User_WaterCooler", "번호,DB유형,명칭,냉방출력,냉방소비전력,EER,압축기,연료,대기전력,대수,설치,증발기,냉수입구온도,냉수출구온도", "");

                for (int n = 0; n < User_Value.Length; n++)
                {
                    WaterCooler_dataGridView.Rows.Add();
                    int nRow = WaterCooler_dataGridView.Rows.Count - 1;
                    WaterCooler_dataGridView.Rows[nRow].Cells[1].Value = User_Value[n][0];  //번호
                    WaterCooler_dataGridView.Rows[nRow].Cells[2].Value = User_Value[n][1];  //DB유형
                    WaterCooler_dataGridView.Rows[nRow].Cells[3].Value = User_Value[n][2];  //명칭
                    WaterCooler_dataGridView.Rows[nRow].Cells[4].Value = User_Value[n][3];  //냉방성능.냉방출력[kW]
                    WaterCooler_dataGridView.Rows[nRow].Cells[5].Value = User_Value[n][4];  //냉방성능.소비전력[kW]
                    WaterCooler_dataGridView.Rows[nRow].Cells[6].Value = User_Value[n][5];  //냉방성능.EER
                    WaterCooler_dataGridView.Rows[nRow].Cells[7].Value = User_Value[n][6]; //압축기
                    WaterCooler_dataGridView.Rows[nRow].Cells[8].Value = User_Value[n][7];  //열원
                    WaterCooler_dataGridView.Rows[nRow].Cells[9].Value = User_Value[n][8];  //대기전력[W]
                    WaterCooler_dataGridView.Rows[nRow].Cells[10].Value = User_Value[n][9];  //대수
                    WaterCooler_dataGridView.Rows[nRow].Cells[11].Value = User_Value[n][10]; //설치
                    WaterCooler_dataGridView.Rows[nRow].Cells[12].Value = User_Value[n][11];  //증발기
                    WaterCooler_dataGridView.Rows[nRow].Cells[13].Value = User_Value[n][12]; //냉수입구온도
                    WaterCooler_dataGridView.Rows[nRow].Cells[14].Value = User_Value[n][13];  //냉수출구온도
                }
            }
            catch { }
        }
        #endregion

        ///////////////////////////////////////////////////태양열/////////////////////////////////////////////////////////////////
        #region 14.태양열
        public void Create_Solar_Table()
        {
            new StackedHeaderDecorator(Solar_dataGridView, DataGridViewAutoSizeColumnsMode.Fill);
            Solar_dataGridView.Columns.Clear();
            Solar_checkBoxColumn.HeaderText = "선택";
            Solar_checkBoxColumn.Name = "check";
            Solar_dataGridView.Columns.Add(Solar_checkBoxColumn);

            Solar_dataGridView.Columns.Add("A1", "번호");
            Solar_dataGridView.Columns.Add("A2", "DB유형");
            Solar_dataGridView.Columns.Add("A3", "명칭");
            Solar_dataGridView.Columns.Add("A4", "난방/급탕");
            Solar_dataGridView.Columns.Add("A5", "모듈면적.A[m2]");
            Solar_dataGridView.Columns.Add("A6", "효율.ηo");
            Solar_dataGridView.Columns.Add("A7", "손실계수.1차.k1");
            Solar_dataGridView.Columns.Add("A8", "손실계수.2차.k2");
            Solar_dataGridView.Columns.Add("A9", "50°의 입사각.Khem(50֠)");
            Solar_dataGridView.Columns.Add("A10", "유효 열용량.C");
            Solar_dataGridView.Columns[0].Width = 40;
            DataGridViewComboBoxColumn 설치유형Combo = new DataGridViewComboBoxColumn();
            설치유형Combo.HeaderText = "설치";
            설치유형Combo.Items.AddRange("기존", "신규", "철거후신규");
            Solar_dataGridView.Columns.Add(설치유형Combo);
            Solar_dataGridView.Columns[11].Width = 100;
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

        }

        private void DefaultSolar_Add_button_Click(object sender, EventArgs e)
        {
            ArrayList SelectSolar = new ArrayList();
            int nRow = Solar_dataGridView.Rows.Add();
            Load_Solar_Num();
            Solar_dataGridView.Rows[nRow].Cells[2].Value = "기본";

            DataGridViewComboBoxCell 난방급탕Combo = new DataGridViewComboBoxCell();
            난방급탕Combo.Items.Add("난방");
            난방급탕Combo.Items.Add("급탕");
            난방급탕Combo.Items.Add("난방+급탕");
            Solar_dataGridView.Rows[nRow].Cells[4] = 난방급탕Combo;

            Heating_Solar heating_Solar = new Heating_Solar("기본DB 적용", null);
            DialogResult result = heating_Solar.ShowDialog();
            if (result == DialogResult.OK)
            {
                try
                {
                    if (heating_Solar.SelectSolar != null)
                    {

                        string[] token = heating_Solar.SelectSolar.Split('+');
                        SelectSolar.Clear();
                        foreach (var item in token)
                        {
                            SelectSolar.Add(item.ToString());
                        }
                        string[][] Value; Double[][] Value2;
                        String 내용;
                        Value = Program.DB.getValue(DB.type.BaseDB_Heating, "태양열시스템", "효율,열손실계수1차,열손실계수2차,입사각50도,유효열용량", "번호 = '" + SelectSolar[0].ToString() + "'");
                        String name = Value[0][0];

                        Solar_dataGridView.Rows[nRow].Cells[6].Value = Value[0][0];
                        Solar_dataGridView.Rows[nRow].Cells[7].Value = Value[0][1];
                        Solar_dataGridView.Rows[nRow].Cells[8].Value = Value[0][2];
                        Solar_dataGridView.Rows[nRow].Cells[9].Value = Value[0][3];
                        Solar_dataGridView.Rows[nRow].Cells[10].Value = Value[0][4];
                    }
                }
                catch { }
            }
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

            DataGridViewComboBoxCell 난방급탕Combo = new DataGridViewComboBoxCell();
            난방급탕Combo.Items.Add("난방");
            난방급탕Combo.Items.Add("급탕");
            난방급탕Combo.Items.Add("난방+급탕");
            Solar_dataGridView.Rows[nRow].Cells[4] = 난방급탕Combo;

            for (int k = 2; k < 12; k++)
            {
                if (Solar_dataGridView.Rows[Solar_SelectRow].Cells[k].Value != null)
                {
                    Solar_dataGridView.Rows[nRow].Cells[k].Value = Solar_dataGridView.Rows[Solar_SelectRow].Cells[k].Value;
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
                String[] Value = new String[11];
                for (int i = 1; i < 12; i++)
                {
                    if (Solar_dataGridView.Rows[k].Cells[i].Value != null)
                    { Value[i - 1] = Solar_dataGridView.Rows[k].Cells[i].Value.ToString(); }
                    else { Value[i - 1] = ""; }
                }
                Program.DB.setValue(DB.type.ProjDB, "User_Solar", "번호,DB유형,명칭,난방급탕,모듈면적,효율,열손실계수1차,열손실계수2차,입사각50도,유효열용량,신규기존",
                "'" + Value[0] + "','"
                 + Value[1] + "','" + Value[2] + "','" + Value[3] + "','" + Value[4] + "','" + Value[5] + "','" + Value[6] + "','" + Value[7] + "','" + Value[8] + "','" + Value[9] + "','"
                 + Value[10]
                 + "'", "번호");
            }
            MessageBox.Show("저장되었습니다.");
        }

        private void Load_Solar()
        {
            try
            {
                string[][] User_Value = Program.DB.getValue(DB.type.ProjDB, "User_Solar", "번호,DB유형,명칭,난방급탕,모듈면적,효율,열손실계수1차,열손실계수2차,입사각50도,유효열용량,신규기존", "");

                for (int n = 0; n < User_Value.Length; n++)
                {
                    Solar_dataGridView.Rows.Add();
                    int nRow = Solar_dataGridView.Rows.Count - 1;
                    Solar_dataGridView.Rows[nRow].Cells[1].Value = User_Value[n][0];
                    Solar_dataGridView.Rows[nRow].Cells[2].Value = User_Value[n][1];
                    Solar_dataGridView.Rows[nRow].Cells[3].Value = User_Value[n][2];
                    Solar_dataGridView.Rows[nRow].Cells[4].Value = User_Value[n][3];
                    Solar_dataGridView.Rows[nRow].Cells[5].Value = User_Value[n][4];
                    Solar_dataGridView.Rows[nRow].Cells[6].Value = User_Value[n][5];
                    Solar_dataGridView.Rows[nRow].Cells[7].Value = User_Value[n][6];
                    Solar_dataGridView.Rows[nRow].Cells[8].Value = User_Value[n][7];
                    Solar_dataGridView.Rows[nRow].Cells[9].Value = User_Value[n][8];
                    Solar_dataGridView.Rows[nRow].Cells[10].Value = User_Value[n][9];
                    Solar_dataGridView.Rows[nRow].Cells[11].Value = User_Value[n][10];
                }
            }
            catch { }
        }

        #endregion

    }
}
