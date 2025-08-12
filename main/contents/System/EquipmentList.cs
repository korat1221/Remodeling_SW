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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using main.subcontents.RESystem_WP;
using Newtonsoft.Json.Linq;
using main.info;

namespace main.contents
{
    public partial class EquipmentList : Form
    {
        DataGridViewCheckBoxColumn Boiler_checkBoxColumn = new DataGridViewCheckBoxColumn();
        DataGridViewCheckBoxColumn AHU_checkBoxColumn = new DataGridViewCheckBoxColumn();
        DataGridViewCheckBoxColumn HRV_checkBoxColumn = new DataGridViewCheckBoxColumn();
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
        DataGridViewCheckBoxColumn DHWHP_checkBoxColumn = new DataGridViewCheckBoxColumn();
        int Boiler_SelectRow, HP_SelectRow, AirCooler_SelectRow, WaterCooler_SelectRow, Pump_SelectRow, ce_SelectRow, Solar_SelectRow, PV_SelectRow, ABS_SelectRow, DH_SelectRow, FC_SelectRow, WP_SelectRow, AHU_SelectRow, HRV_SelectRow, CoolingTop_SelectRow, Fan_SelectRow;
        string[][] 프로젝트유형;
        //냉방추가
        DataGridViewCheckBoxColumn AirCooler_checkBoxColumn = new DataGridViewCheckBoxColumn();
        DataGridViewCheckBoxColumn WaterCooler_checkBoxColumn = new DataGridViewCheckBoxColumn();
        DataGridViewCheckBoxColumn CoolingTop_checkBoxColumn = new DataGridViewCheckBoxColumn();
        //배기팬추가
        DataGridViewCheckBoxColumn Fan_checkBoxColumn = new DataGridViewCheckBoxColumn();

        public EquipmentList()
        {
            InitializeComponent(); this.Font = new Font(UTIL.Families[0], 9.75F, FontStyle.Regular);
            프로젝트유형 = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "프로젝트유형번호");//User_GroundWHP
            Program.DB.initTable(DB.type.ProjDB, "User_Boiler");
            Program.DB.initTable(DB.type.ProjDB, "User_ABS");
            Program.DB.initTable(DB.type.ProjDB, "User_ DH");
            Program.DB.initTable(DB.type.ProjDB, "User_PV");
            Program.DB.initTable(DB.type.ProjDB, "User_FC");
            Program.DB.initTable(DB.type.ProjDB, "User_WP");
            Program.DB.initTable(DB.type.ProjDB, "User_AirHP");
            Program.DB.initTable(DB.type.ProjDB, "User_GroundHP");
            Program.DB.initTable(DB.type.ProjDB, "User_GroundWHP");
            Program.DB.initTable(DB.type.ProjDB, "User_Pump");
            Program.DB.initTable(DB.type.ProjDB, "User_ce");
            Program.DB.initTable(DB.type.ProjDB, "User_ Solar");
            Program.DB.initTable(DB.type.ProjDB, "User_ AHU");
            Program.DB.initTable(DB.type.ProjDB, "User_ HRV");
            Program.DB.initTable(DB.type.ProjDB, "User_DHWHP");
            // 냉방추가
            Program.DB.initTable(DB.type.ProjDB, "User_AirCooler");
            Program.DB.initTable(DB.type.ProjDB, "User_WaterCooler");
            Program.DB.initTable(DB.type.ProjDB, "User_AbsorbCooler");
            Program.DB.initTable(DB.type.ProjDB, "User_CoolingTop");

            //배기팬 추가
            Program.DB.initTable(DB.type.ProjDB, "User_Fan");

            string[][] Image = Program.DB.getValue(DB.type.BaseDB_HCneed, "메뉴아이콘", "하위메뉴아이콘", "하위메뉴명 = '장비일람표'");
            if (Image.Length > 0)
            {
                Icon_pictureBox.Load(Program.gPath + Image[0][0]);
                Icon_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            }
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
            Create_AHU_Table();
            Create_HRV_Table();
            Create_DHWHP_Table();
            //냉방추가
            Create_AirCooler_Table();
            Create_WaterCooler_Table();
            Create_CoolingTop_Table();

            //배기팬추가
            Create_Fan_Table();
            //단위계산
            unit_comboBox.Items.AddRange(new string[] { "열량", "유량", "수량" });

            LoadData();
        }

        public void LoadData()
        {
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
            Load_AHU();
            Load_HRV();
            Load_DHWHP();
            //냉방추가
            Load_CoolingTop();
            Load_Qmax();
            //배기팬추가
            Load_Fan();
        }

        private void GeneralPanel_Paint(object sender, PaintEventArgs e)
        {
            Panel p = (Panel)sender;
            ControlPaint.DrawBorder(e.Graphics, p.DisplayRectangle, System.Drawing.SystemColors.Control, ButtonBorderStyle.Solid);
        }
        private void Load_Qmax()
        {
            string[][] value = Program.DB.querySQL(DB.type.ProjDB, "Select Sum(Q_max) From Zone_HCneed_Result Where 난방_냉방='난방' and 비이용일_이용일='이용일' and  월='1월'");
            if (value.Length > 0 && value[0][0] != "")
            {
                Qhmax_textBox.Text = (Convert.ToDouble(value[0][0]) / 1000).ToString();
                Program.UTIL.textBox_doubleComa(Qhmax_textBox, true, 1);

            }
            value = Program.DB.querySQL(DB.type.ProjDB, "Select Sum(Q_max) From Zone_HCneed_Result Where 난방_냉방='냉방' and 비이용일_이용일='이용일' and  월='1월'");
            if (value.Length > 0 && value[0][0] != "")
            {
                Qcmax_textBox.Text = (Convert.ToDouble(value[0][0]) / 1000).ToString();
                Program.UTIL.textBox_doubleComa(Qcmax_textBox, true, 1);
            }
            string[][] ZoneValue = Program.DB.getValue(DB.type.ProjDB, "ZoneGeneral_Form", "일일급탕요구량,용도프로필");
            if (ZoneValue.Length > 0)
            {
                double Qmax_w = 0;
                for (int a = 0; a < ZoneValue.Length; a++)
                {
                    string[][] Usage = Program.DB.getValue(DB.type.BaseDB_HCneed, "용도프로필", "급탕시간당비율", "용도명 = '" + ZoneValue[0][1] + "'");
                    if (Usage.Length > 0)
                    {
                        if (ZoneValue[a][0] != "" && Usage[0][0] != "")
                        {
                            Qmax_w += Convert.ToDouble(ZoneValue[a][0]) * Convert.ToDouble(Usage[0][0]);
                        }
                    }
                }
                Qwmax_textBox.Text = (Qmax_w).ToString();
                Program.UTIL.textBox_doubleComa(Qwmax_textBox, true, 1);

            }
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
            Boiler_dataGridView.Columns[4].Width = 90;
            Boiler_dataGridView.Columns[5].Width = 60;
            Boiler_dataGridView.Columns[6].Width = 130;
            Boiler_dataGridView.Columns[7].Width = 80;
            Boiler_dataGridView.Columns[8].Width = 80;
            Boiler_dataGridView.Columns[9].Width = 80;
            Boiler_dataGridView.Columns[10].Width = 80;
            Boiler_dataGridView.Columns[11].Width = 80;
            Boiler_dataGridView.Columns[12].Width = 60;
            DataGridViewComboBoxColumn 설치유형Combo = new DataGridViewComboBoxColumn();
            설치유형Combo.HeaderText = "설치";
            설치유형Combo.Items.AddRange("기존", "신규", "철거후신규");
            Boiler_dataGridView.Columns.Add(설치유형Combo);
            Boiler_dataGridView.Columns[13].Width = 60;


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

            Heating_Boiler heating_Boiler = new Heating_Boiler("기본DB 적용", null);
            DialogResult result = heating_Boiler.ShowDialog();
            if (result == DialogResult.OK)
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
                    if (Value.Length > 0)
                    {
                        String name = Value[0][0];
                        if (Value[0][1] == "가스")
                        {
                            DataGridViewComboBoxCell 연료Combo = new DataGridViewComboBoxCell();
                            연료Combo.Items.Add("LNG");
                            연료Combo.Items.Add("LPG");
                            Boiler_dataGridView.Rows[nRow].Cells[5] = 연료Combo;
                        }
                        else { Boiler_dataGridView.Rows[nRow].Cells[5].Value = Value[0][1]; }
                        Boiler_dataGridView.Rows[nRow].Cells[6].Value = Value[0][2];
                        Boiler_dataGridView.Rows[nRow].Cells[8].Value = Convert.ToDouble(Value[0][3]) * 100;
                        Boiler_dataGridView.Rows[nRow].Cells[9].Value = Convert.ToDouble(Value[0][4]) * 100;
                        Boiler_dataGridView.Rows[nRow].Cells[10].Value = Value[0][5];
                        Boiler_dataGridView.Rows[nRow].Cells[11].Value = Value[0][6];
                    }
                }
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
                else if (e.ColumnIndex == 8 || e.ColumnIndex == 9)
                {
                    if (Boiler_dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                    {
                        if (Program.UTIL.dataGridView_doubleComa(Boiler_dataGridView, e.RowIndex, e.ColumnIndex, 1) < 1 && Program.UTIL.dataGridView_doubleComa(Boiler_dataGridView, e.RowIndex, e.ColumnIndex, 1) != 0)
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

            for (int k = 2; k < 14; k++)
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
                    {
                        double parsedValue;
                        if (double.TryParse(Boiler_dataGridView.Rows[k].Cells[i].Value.ToString(), out parsedValue))
                        {
                            Value[i - 1] = parsedValue.ToString();
                        }
                        else
                        {
                            Value[i - 1] = Boiler_dataGridView.Rows[k].Cells[i].Value.ToString();
                        }
                    }
                    else { Value[i - 1] = ""; }
                }
                Program.DB.setValue(DB.type.ProjDB, "User_Boiler", "번호,프로젝트유형,DB유형,명칭,난방급탕,연료,Type,용량,전부하효율,부분부하효율,소비전력,대기전력,대수,신규기존",
                "'" + Value[0] + "','" + 프로젝트유형[0][0] + "','"
                 + Value[1] + "','" + Value[2] + "','" + Value[3] + "','" + Value[4] + "','" + Value[5] + "','" + Value[6] + "','" + Value[7] + "','" + Value[8] + "','" + Value[9] + "','" + Value[10] + "','" + Value[11] + "','"
                 + Value[12]
                 + "'", "번호");
            }
            Program.DB.saveProject();
            MessageBox.Show("저장되었습니다.");
        }

        private void Load_Boiler()
        {
            Boiler_dataGridView.Rows.Clear();
            string[][] User_Value = Program.DB.getValue(DB.type.ProjDB, "User_Boiler", "번호,명칭,연료,Type,용량,전부하효율,부분부하효율,소비전력,대기전력,DB유형,난방급탕,대수,신규기존", "");
            if (User_Value.Length > 0)
            {
                string 용량 = "", 전부하효율 = "", 부분부하효율 = "", 소비전력 = "", 대기전력 = "";
                for (int n = 0; n < User_Value.Length; n++)
                {
                    Boiler_dataGridView.Rows.Add();
                    int nRow = Boiler_dataGridView.Rows.Count - 1;


                    if (User_Value[n][9] == "도면")
                    {

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
                    }
                    else
                    {
                        DataGridViewComboBoxCell 난방급탕Combo = new DataGridViewComboBoxCell();
                        난방급탕Combo.Items.Add("난방");
                        난방급탕Combo.Items.Add("급탕");
                        난방급탕Combo.Items.Add("난방+급탕");
                        Boiler_dataGridView.Rows[nRow].Cells[4] = 난방급탕Combo;
                    }


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
        }
        #endregion
        ///////////////////////////////////////////////////흡수식냉온수기/////////////////////////////////////////////////////////////////
        #region 2.흡수식냉온수기
        public void Create_ABS_Table()
        {
            new StackedHeaderDecorator(ABS_dataGridView, DataGridViewAutoSizeColumnsMode.Fill);
            ABS_dataGridView.Columns.Clear();
            ABS_checkBoxColumn.HeaderText = "선택";
            ABS_checkBoxColumn.Name = "check";
            ABS_dataGridView.Columns.Add(ABS_checkBoxColumn);

            ABS_dataGridView.Columns.Add("A1", "번호");
            ABS_dataGridView.Columns.Add("A2", "DB유형");
            ABS_dataGridView.Columns.Add("A3", "명칭");
            ABS_dataGridView.Columns.Add("A4", "난방냉방");
            ABS_dataGridView.Columns.Add("A5", "연료");
            ABS_dataGridView.Columns.Add("A6", "지역난방");

            ABS_dataGridView.Columns.Add("A7", "냉방.용량[kW]");
            ABS_dataGridView.Columns.Add("A8", "냉방.성능[COP]");
            ABS_dataGridView.Columns.Add("A9", "난방.용량[kW]");
            ABS_dataGridView.Columns.Add("A10", "난방.성능[COP]");
            ABS_dataGridView.Columns.Add("A11", "냉수온도.입구[℃]");
            ABS_dataGridView.Columns.Add("A12", "냉수온도.출구[℃]");
            ABS_dataGridView.Columns.Add("A13", "온수온도.입구[℃]");
            ABS_dataGridView.Columns.Add("A14", "온수온도.출구[℃]");
            ABS_dataGridView.Columns.Add("A15", "대기전력.[W]");
            ABS_dataGridView.Columns.Add("A16", "통합성능.IPLV");
            ABS_dataGridView.Columns.Add("A17", "대수.[EA]");
            ABS_dataGridView.Columns.Add("A18", "설치");
            ABS_dataGridView.Columns[0].Width = 40;
            ABS_dataGridView.Columns[1].Width = 60;
            ABS_dataGridView.Columns[2].Width = 50;
            ABS_dataGridView.Columns[3].Width = 100;
            ABS_dataGridView.Columns[5].Width = 60;
            ABS_dataGridView.Columns[17].Width = 60;
            ABS_dataGridView.Columns[18].Width = 60;
            ABS_dataGridView.Columns[16].Visible = false;
        }
        private void UserABS_Add_button_Click(object sender, EventArgs e)
        {
            int nRow = ABS_dataGridView.Rows.Add();
            Load_ABS_Num();
            ABS_dataGridView.Rows[nRow].Cells[2].Value = "도면";

            DataGridViewComboBoxCell 난방냉방Combo = new DataGridViewComboBoxCell();
            난방냉방Combo.Items.AddRange(new string[] { "냉방", "냉난방" });
            ABS_dataGridView.Rows[nRow].Cells[4] = 난방냉방Combo;

            DataGridViewComboBoxCell 연료Combo = new DataGridViewComboBoxCell();
            연료Combo.Items.AddRange(new string[] { "가스", "지역난방" });
            ABS_dataGridView.Rows[nRow].Cells[5] = 연료Combo;


            DataGridViewComboBoxCell 설치Combo = new DataGridViewComboBoxCell();
            설치Combo.Items.AddRange(new string[] { "기존", "신규", "철거후신규" });
            ABS_dataGridView.Rows[nRow].Cells[18] = 설치Combo;
        }
        private void DefaultABS_Add_button_Click(object sender, EventArgs e)
        {
            ArrayList SelectABS = new ArrayList();

            int nRow = ABS_dataGridView.Rows.Add();
            Load_ABS_Num();
            ABS_dataGridView.Rows[nRow].Cells[2].Value = "기본";

            DataGridViewComboBoxCell 난방냉방Combo = new DataGridViewComboBoxCell();
            난방냉방Combo.Items.AddRange(new string[] { "냉방", "냉난방" });
            ABS_dataGridView.Rows[nRow].Cells[4] = 난방냉방Combo;

            DataGridViewComboBoxCell 연료Combo = new DataGridViewComboBoxCell();
            연료Combo.Items.AddRange(new string[] { "가스", "지역난방" });
            ABS_dataGridView.Rows[nRow].Cells[5] = 연료Combo;


            DataGridViewComboBoxCell 설치Combo = new DataGridViewComboBoxCell();
            설치Combo.Items.AddRange(new string[] { "기존", "신규", "철거후신규" });
            ABS_dataGridView.Rows[nRow].Cells[18] = 설치Combo;

            ABS_DB abs_db = new ABS_DB("기본DB 적용", null, "냉난방");
            DialogResult result = abs_db.ShowDialog();
            if (result == DialogResult.OK)
            {
                if (abs_db.SelectAS != null)
                {

                    string[][] Value = Program.DB.getValue(DB.type.BaseDB_Heating, "흡수식냉온수기", "통합성능", "번호 = '" + abs_db.SelectAS.ToString() + "'");
                    if (Value.Length > 0)
                    {
                        ABS_dataGridView.Rows[nRow].Cells[16].Value = Value[0][0];
                    }

                }
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

            for (int k = 2; k < 19; k++)
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

            if (ABS_dataGridView.Rows[nRow].Cells[2].Value != null && ABS_dataGridView.Rows[nRow].Cells[2].Value == "도면")
            {
                DataGridViewComboBoxCell 난방냉방Combo = new DataGridViewComboBoxCell();
                난방냉방Combo.Items.AddRange(new string[] { "냉방", "냉난방" });
                ABS_dataGridView.Rows[nRow].Cells[4] = 난방냉방Combo;

                DataGridViewComboBoxCell 연료Combo = new DataGridViewComboBoxCell();
                연료Combo.Items.AddRange(new string[] { "가스", "지역난방" });
                ABS_dataGridView.Rows[nRow].Cells[5] = 연료Combo;

                DataGridViewComboBoxCell 설치Combo = new DataGridViewComboBoxCell();
                설치Combo.Items.AddRange(new string[] { "기존", "신규", "철거후신규" });
                ABS_dataGridView.Rows[nRow].Cells[18] = 설치Combo;
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
            if (e.RowIndex >= 0)
            {
                if (e.ColumnIndex == 4)
                {
                    if (ABS_dataGridView.Rows[e.RowIndex].Cells[4].Value != null)
                    {
                        string currentValue = ABS_dataGridView.Rows[e.RowIndex].Cells[4].Value.ToString();

                        if (currentValue == "냉방")
                        {
                            ABS_dataGridView.Rows[e.RowIndex].Cells[9].Value = "-";
                            ABS_dataGridView.Rows[e.RowIndex].Cells[10].Value = "-";
                            ABS_dataGridView.Rows[e.RowIndex].Cells[13].Value = "-";
                            ABS_dataGridView.Rows[e.RowIndex].Cells[14].Value = "-";
                        }
                        else
                        {
                            ABS_dataGridView.Rows[e.RowIndex].Cells[9].Value = "";
                            ABS_dataGridView.Rows[e.RowIndex].Cells[10].Value = "";
                            ABS_dataGridView.Rows[e.RowIndex].Cells[13].Value = "";
                            ABS_dataGridView.Rows[e.RowIndex].Cells[14].Value = "";
                        }
                    }

                }
                else if (e.ColumnIndex == 5)
                {
                    if (ABS_dataGridView.Rows[e.RowIndex].Cells[5].Value != null && ABS_dataGridView.Rows[e.RowIndex].Cells[5].Value.ToString() == "지역난방")
                    {
                        DataGridViewComboBoxCell 지역난방Combo = new DataGridViewComboBoxCell();
                        string[][] value = Program.DB.getValue(DB.type.ProjDB, "User_DH", "번호", "용도='흡수식'");
                        for (int a = 0; a < value.Length; a++)
                        {
                            지역난방Combo.Items.Add(value[a][0]);
                        }
                        ABS_dataGridView.Rows[e.RowIndex].Cells[6] = 지역난방Combo;
                    }
                    else
                    {
                        ABS_dataGridView.Rows[e.RowIndex].Cells[6].Value = "-";
                    }
                }
            }
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
                String[] Value = new String[18];
                for (int i = 1; i < 19; i++) //19개 항목중 18개만 작성함
                {
                    if (ABS_dataGridView.Rows[k].Cells[i].Value != null)
                    {
                        double parsedValue;
                        if (double.TryParse(ABS_dataGridView.Rows[k].Cells[i].Value.ToString(), out parsedValue))
                        {
                            Value[i - 1] = parsedValue.ToString();
                        }
                        else
                        {
                            Value[i - 1] = ABS_dataGridView.Rows[k].Cells[i].Value.ToString();
                        }
                    }
                    else { Value[i - 1] = ""; }
                }
                Program.DB.setValue(DB.type.ProjDB, "User_ABS", "번호,프로젝트유형,DB유형,명칭,난방냉방,연료,지역난방,냉방용량,냉방성능,난방용량,난방성능,냉수입구온도,냉수출구온도,온수입구온도,온수출구온도,대기전력,통합성능,대수,설치",
                "'" + Value[0] + "','" + 프로젝트유형[0][0] + "','"
                 + Value[1] + "','" + Value[2] + "','" + Value[3] + "','" + Value[4] + "','" + Value[5] + "','" + Value[6] + "','" + Value[7] + "','" + Value[8] + "','"
                 + Value[9] + "','" + Value[10] + "','" + Value[11] + "','" + Value[12] + "','" + Value[13] + "','" + Value[14] + "','" + Value[15] + "','" + Value[16] + "','"
                 + Value[17] + "'", "번호");
            }
            Program.DB.saveProject();
            MessageBox.Show("저장되었습니다.");
        }
        private void Load_ABS()
        {
            ABS_dataGridView.Rows.Clear();
            string[][] User_Value = Program.DB.getValue(DB.type.ProjDB, "User_ABS", "번호,DB유형,명칭,난방냉방,연료,지역난방,냉방용량,냉방성능,난방용량,난방성능,냉수입구온도,냉수출구온도,온수입구온도,온수출구온도,대기전력,통합성능,대수,설치", "");
            if (User_Value.Length > 0) //명칭이 있어야함
            {
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
                    ABS_dataGridView.Rows[nRow].Cells[17].Value = User_Value[n][16];
                    ABS_dataGridView.Rows[nRow].Cells[18].Value = User_Value[n][17];
                }
            }
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
            용도Combo.Items.Add("난방용");
            용도Combo.Items.Add("난방용");
            용도Combo.Items.Add("급탕일반용");
            용도Combo.Items.Add("급탕재열용");
            용도Combo.Items.Add("급탕예열용");
            용도Combo.Items.Add("흡수식");
            DH_dataGridView.Rows[nRow].Cells[4] = 용도Combo;
            DH_DB DH_db = new DH_DB("기본DB 적용", null, null);
            DialogResult result = DH_db.ShowDialog();
            if (result == DialogResult.OK)
            {
                if (DH_db.SelectDH != null)
                {

                    string[][] Value = Program.DB.getValue(DB.type.BaseDB_Heating, "지역난방", "용도,공급온도1차,환수온도1차,공급온도2차,환수온도2차", "번호 = '" + DH_db.SelectDH.ToString() + "'");
                    if (Value.Length > 0)
                    {
                        if (Value[0][0].Substring(0, 2) == "난방")
                        {
                            DH_dataGridView.Rows[nRow].Cells[4].Value = "난방용";
                        }
                        else
                        {
                            DH_dataGridView.Rows[nRow].Cells[4].Value = Value[0][0];
                        }
                        DH_dataGridView.Rows[nRow].Cells[6].Value = Value[0][1];
                        DH_dataGridView.Rows[nRow].Cells[7].Value = Value[0][2];
                        DH_dataGridView.Rows[nRow].Cells[8].Value = Value[0][3];
                        DH_dataGridView.Rows[nRow].Cells[9].Value = Value[0][4];
                    }
                }
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
                    {
                        double parsedValue;
                        if (double.TryParse(DH_dataGridView.Rows[k].Cells[i].Value.ToString(), out parsedValue))
                        {
                            Value[i - 1] = parsedValue.ToString();
                        }
                        else
                        {
                            Value[i - 1] = DH_dataGridView.Rows[k].Cells[i].Value.ToString();
                        }
                    }
                    else { Value[i - 1] = ""; }
                }
                Program.DB.setValue(DB.type.ProjDB, "User_DH", "번호,프로젝트유형,DB유형,명칭,용도,용량,공급온도1차,환수온도1차,공급온도2차,환수온도2차,대수,신규기존",
                "'" + Value[0] + "','" + 프로젝트유형[0][0] + "','"
                 + Value[1] + "','" + Value[2] + "','" + Value[3] + "','" + Value[4] + "','" + Value[5] + "','" + Value[6] + "','" + Value[7] + "','" + Value[8] + "','" + Value[9] + "','"
                 + Value[10]

                 + "'", "번호");
            }
            Program.DB.saveProject();
            MessageBox.Show("저장되었습니다.");
        }

        private void Load_DH()
        {
            DH_dataGridView.Rows.Clear();
            string[][] User_Value = Program.DB.getValue(DB.type.ProjDB, "User_DH", "번호,DB유형,명칭,용도,용량,공급온도1차,환수온도1차,공급온도2차,환수온도2차,대수,신규기존", "");
            if (User_Value.Length > 0)
            {
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
            PV_dataGridView.Columns.Add("A3", "명칭");
            PV_dataGridView.Columns.Add("A4", "Cell Type");
            PV_dataGridView.Columns.Add("A5", "모듈(변경가능).길이.[m]");
            PV_dataGridView.Columns.Add("A6", "모듈(변경가능).높이.[m]");
            PV_dataGridView.Columns.Add("A7", "모듈(변경가능).정격출력.[W]");
            PV_dataGridView.Columns.Add("A8", "Kpk");


            DataGridViewComboBoxColumn 설치유형Combo = new DataGridViewComboBoxColumn();
            설치유형Combo.HeaderText = "설치";
            설치유형Combo.Items.AddRange("기존", "신규", "철거후신규");
            PV_dataGridView.Columns.Add(설치유형Combo);

            PV_dataGridView.Columns[0].Width = 40;

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
                if (column == 9)
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

            DataGridViewComboBoxCell Cell = new DataGridViewComboBoxCell();
            Cell.Items.AddRange(new string[] { "단결정", "다결정", "a_Si박막형", "화합물CIGS박막형", "화합물CdTe박막형" });
            PV_dataGridView.Rows[nRow].Cells[4] = Cell;
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
                if (pv_DB.SelectPV != null)
                {
                    string[][] Value = Program.DB.getValue(DB.type.BaseDB_RESystem, "태양광모듈DB", "CELLTYPE,Kpk", "번호 = '" + pv_DB.SelectPV + "'");
                    if (Value.Length > 0)
                    {

                        PV_dataGridView.Rows[nRow].Cells[4].Value = Value[0][0].ToString();
                        PV_dataGridView.Rows[nRow].Cells[8].Value = Value[0][1].ToString();
                    }

                }
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
            double Length, Height, Power; double Power_m2;
            if (e.ColumnIndex == 5 || e.ColumnIndex == 6 || e.ColumnIndex == 7)
            {
                if (Program.UTIL.data_inputcheck(PV_dataGridView, e.RowIndex, 5, 1) && Program.UTIL.data_inputcheck(PV_dataGridView, e.RowIndex, 6, 1) && Program.UTIL.data_inputcheck(PV_dataGridView, e.RowIndex, 7, 1))
                {
                    Length = Convert.ToDouble(PV_dataGridView.Rows[e.RowIndex].Cells[5].Value.ToString());
                    Height = Convert.ToDouble(PV_dataGridView.Rows[e.RowIndex].Cells[6].Value.ToString());
                    Power = Convert.ToDouble(PV_dataGridView.Rows[e.RowIndex].Cells[7].Value.ToString());
                    Power_m2 = Power / 1000 / (Length * Height);
                    PV_dataGridView.Rows[e.RowIndex].Cells[8].Value = string.Format("{0:F3}", Power_m2);
                }
                else if (Program.UTIL.data_inputcheck(PV_dataGridView, e.RowIndex, 5, 1) && Program.UTIL.data_inputcheck(PV_dataGridView, e.RowIndex, 6, 1) && Program.UTIL.data_inputcheck(PV_dataGridView, e.RowIndex, 8, 1))
                {
                    Length = Convert.ToDouble(PV_dataGridView.Rows[e.RowIndex].Cells[5].Value.ToString());
                    Height = Convert.ToDouble(PV_dataGridView.Rows[e.RowIndex].Cells[6].Value.ToString());
                    Power_m2 = Convert.ToDouble(PV_dataGridView.Rows[e.RowIndex].Cells[8].Value.ToString());
                    Power = Power_m2 * 1000 * (Length * Height);
                    PV_dataGridView.Rows[e.RowIndex].Cells[7].Value = Power.ToString("0");
                }
            }
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
            Program.DB.deleteValue(DB.type.ProjDB, "User_PV", "");

            for (int k = 0; k < PV_dataGridView.RowCount; k++)
            {
                String[] Value = new String[11];
                for (int i = 1; i < 10; i++)
                {
                    if (PV_dataGridView.Rows[k].Cells[i].Value != null)
                    {
                        double parsedValue;
                        if (double.TryParse(PV_dataGridView.Rows[k].Cells[i].Value.ToString(), out parsedValue))
                        {
                            Value[i - 1] = parsedValue.ToString();
                        }
                        else if (PV_dataGridView.Rows[k].Cells[i].Value != "")
                        {
                            Value[i - 1] = PV_dataGridView.Rows[k].Cells[i].Value.ToString();
                        }
                        else
                        {
                            MessageBox.Show(string.Format("{0}에서 {1}항목의 빈칸을 채워주세요", PV_dataGridView.Rows[k].Cells[i].Value.ToString(), PV_dataGridView.Columns[i].HeaderText), "주의", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show(string.Format("{0}에서 '{1}'항목의 빈칸을 채워주세요", PV_dataGridView.Rows[k].Cells[1].Value.ToString(), PV_dataGridView.Columns[i].HeaderText), "주의", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                Program.DB.setValue(DB.type.ProjDB, "User_PV", "번호,프로젝트유형,DB유형,명칭,CELLTYPE,길이,높이,정격출력,Kpk,설치",
                "'" + Value[0] + "','" + 프로젝트유형[0][0] + "','"
                 + Value[1] + "','" + Value[2] + "','" + Value[3] + "','" + Value[4] + "','" + Value[5] + "','" + Value[6] + "','" + Value[7] + "','" + Value[8] + "'", "번호");
            }
            Program.DB.saveProject();
            MessageBox.Show("저장되었습니다.");
        }

        private void Load_PV()
        {
            PV_dataGridView.Rows.Clear();
            string[][] User_Value = Program.DB.getValue(DB.type.ProjDB, "User_PV", "번호,DB유형,명칭,CELLTYPE,길이,높이,정격출력,Kpk,설치", "");
            if (User_Value.Length > 0)
            {
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

                }
            }
        }
        #endregion
        ///////////////////////////////////////////////////연료전지/////////////////////////////////////////////////////////////////
        #region 5.연료전지
        public void Create_FC_Table()
        {
            new StackedHeaderDecorator(FC_dataGridView, DataGridViewAutoSizeColumnsMode.Fill);
            FC_dataGridView.Columns.Clear();
            FC_checkBoxColumn.HeaderText = "선택";
            FC_checkBoxColumn.Name = "check";
            FC_dataGridView.Columns.Add(FC_checkBoxColumn);

            FC_dataGridView.Columns.Add("A1", "번호");
            FC_dataGridView.Columns.Add("A2", "DB유형");
            FC_dataGridView.Columns.Add("A3", "명칭");
            FC_dataGridView.Columns.Add("A4", "연료");
            FC_dataGridView.Columns.Add("A5", "전기.출력[kW]");
            FC_dataGridView.Columns.Add("A6", "전기.효율[%]]");
            FC_dataGridView.Columns.Add("A7", "열.출력[kW]");
            FC_dataGridView.Columns.Add("A8", "열.효율[%]");
            FC_dataGridView.Columns.Add("A9", "대수");
            DataGridViewComboBoxColumn 설치유형Combo = new DataGridViewComboBoxColumn();
            설치유형Combo.HeaderText = "설치";
            설치유형Combo.Items.AddRange("기존", "신규", "철거후신규");
            FC_dataGridView.Columns.Add(설치유형Combo);

            FC_dataGridView.Columns[0].Width = 40;
            FC_dataGridView.Columns[10].Width = 100;
        }

        private void UserFC_Add_button_Click(object sender, EventArgs e)
        {
            int nRow = FC_dataGridView.Rows.Add();
            Load_FC_Num();
            FC_dataGridView.Rows[nRow].Cells[2].Value = "도면";

            DataGridViewComboBoxCell 연료 = new DataGridViewComboBoxCell();
            연료.Items.AddRange(new string[] { "가스", "수소" });
            FC_dataGridView.Rows[nRow].Cells[4] = 연료;
        }

        private void DefaultFC_Add_button_Click(object sender, EventArgs e) //기본DB를 바탕으로 작성됨
        {
            ArrayList SelectFC = new ArrayList();
            int nRow = FC_dataGridView.Rows.Add();
            Load_FC_Num();
            FC_dataGridView.Rows[nRow].Cells[2].Value = "기본";

            subcontents.FC fc_DB = new subcontents.FC("기본DB 적용", null);
            DialogResult result = fc_DB.ShowDialog();
            if (result == DialogResult.OK)
            {
                string[] token = fc_DB.SelectFCnonsplit.Split('+');
                if (token[0] != null)
                {
                    string[][] Value = Program.DB.getValue(DB.type.BaseDB_RESystem, "연료전지DB", "전기출력,전기효율,열출력,열효율", "번호 = '" + token[0] + "'");
                    if (Value.Length > 0)
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            if (Value[0][i] == null)
                            {
                                FC_dataGridView.Rows[nRow].Cells[5 + i].Value = null;
                            }
                            else
                            {
                                FC_dataGridView.Rows[nRow].Cells[5 + i].Value = Value[0][i];
                            }
                        }
                        DataGridViewComboBoxCell 연료 = new DataGridViewComboBoxCell();
                        연료.Items.AddRange(new string[] { "가스", "수소" });
                        FC_dataGridView.Rows[nRow].Cells[4] = 연료;
                    }
                }
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
                    {
                        double parsedValue;
                        if (double.TryParse(FC_dataGridView.Rows[k].Cells[i].Value.ToString(), out parsedValue))
                        {
                            Value[i - 1] = parsedValue.ToString();
                        }
                        else
                        {
                            Value[i - 1] = FC_dataGridView.Rows[k].Cells[i].Value.ToString();
                        }
                    }
                    else { Value[i - 1] = ""; }
                }
                Program.DB.setValue(DB.type.ProjDB, "User_FC", "번호,프로젝트유형,DB유형,명칭,연료,전기출력,전기효율,열출력,열효율,대수,설치",
                "'" + Value[0] + "','" + 프로젝트유형[0][0] + "','"
                 + Value[1] + "','" + Value[2] + "','" + Value[3] + "','" + Value[4] + "','" + Value[5] + "','" + Value[6] + "','" + Value[7] + "','" + Value[8] + "','" + Value[9] + "'", "번호");
            }
            Program.DB.saveProject();
            MessageBox.Show("저장되었습니다.");
        }

        private void Load_FC()
        {
            FC_dataGridView.Rows.Clear();
            string[][] User_Value = Program.DB.getValue(DB.type.ProjDB, "User_FC", "번호,DB유형,명칭,연료,전기출력,전기효율,열출력,열효율,대수,설치", "");
            if (User_Value.Length > 0)
            {
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
            WP_dataGridView.Columns.Add("A7", "정격출력.[kW]");
            WP_dataGridView.Columns.Add("A8", "허브면적.[m" + Program.UTIL.Subscript(2, true) + "]");
            WP_dataGridView.Columns.Add("A9", "허브높이.[m]");
            WP_dataGridView.Columns.Add("A10", "풍속.시동.[m/s]");
            WP_dataGridView.Columns.Add("A11", "풍속.최적.[m/s]");
            WP_dataGridView.Columns.Add("A12", "풍속.종단.[m/s]");
            WP_dataGridView.Columns.Add("A13", "전력계수.시동풍속.Cp,min");
            WP_dataGridView.Columns.Add("A14", "전력계수.최적풍속.Cp,op");
            WP_dataGridView.Columns.Add("A15", "전력계수.종단풍속.Cp,max");
            DataGridViewComboBoxColumn 설치유형Combo = new DataGridViewComboBoxColumn();
            설치유형Combo.HeaderText = "설치";
            설치유형Combo.Items.AddRange("기존", "신규", "철거후신규");
            WP_dataGridView.Columns.Add(설치유형Combo);
            WP_dataGridView.Columns[0].Width = 40;
            WP_dataGridView.Columns[6].Width = 80;
            WP_dataGridView.Columns[16].Width = 60;
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



        private void DefaultWP_ADD_button_Click(object sender, EventArgs e)
        {
            ArrayList SelectWP = new ArrayList();
            int nRow = WP_dataGridView.Rows.Add();
            Load_WP_Num();
            WP_dataGridView.Rows[nRow].Cells[2].Value = "기본";

            //subcontents.WP_DB wp_DB = new subcontents.WP_DB("기본DB 적용");
            subcontents.RESystem_WP.WP_DB wp_DB = new subcontents.RESystem_WP.WP_DB("기본DB 적용");
            DialogResult result = wp_DB.ShowDialog();
            if (result == DialogResult.OK)
            {
                if (wp_DB.Select_WP[0] != null)
                {
                    string[][] Value = Program.DB.getValue(DB.type.BaseDB_RESystem, "풍력DB", "제품명,제조사,타입,세부타입,정격출력,회전면적,허브높이,시동풍속,최적풍속,종단풍속,시동풍속전력계수,최적풍속전력계수,종단풍속전력계수", "번호 = '" + wp_DB.Select_WP[0].ToString() + "'");
                    if (Value.Length > 0)
                    {
                        WP_dataGridView.Rows[nRow].Cells[3].Value = Value[0][0];
                        WP_dataGridView.Rows[nRow].Cells[4].Value = Value[0][1];
                        WP_dataGridView.Rows[nRow].Cells[5].Value = Value[0][2];
                        WP_dataGridView.Rows[nRow].Cells[6].Value = Value[0][3];
                        WP_dataGridView.Rows[nRow].Cells[7].Value = Value[0][4];
                        WP_dataGridView.Rows[nRow].Cells[8].Value = Value[0][5];
                        WP_dataGridView.Rows[nRow].Cells[9].Value = Value[0][6];
                        WP_dataGridView.Rows[nRow].Cells[10].Value = Value[0][7];
                        WP_dataGridView.Rows[nRow].Cells[11].Value = Value[0][8];
                        WP_dataGridView.Rows[nRow].Cells[12].Value = Value[0][9];
                        WP_dataGridView.Rows[nRow].Cells[13].Value = Value[0][10];
                        WP_dataGridView.Rows[nRow].Cells[14].Value = Value[0][11];
                        WP_dataGridView.Rows[nRow].Cells[15].Value = Value[0][12];
                    }
                }
            }
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
                    {
                        double parsedValue;
                        if (double.TryParse(WP_dataGridView.Rows[k].Cells[i].Value.ToString(), out parsedValue))
                        {
                            Value[i - 1] = parsedValue.ToString();
                        }
                        else
                        {
                            Value[i - 1] = WP_dataGridView.Rows[k].Cells[i].Value.ToString();
                        }
                    }
                    else { Value[i - 1] = ""; }
                }
                Program.DB.setValue(DB.type.ProjDB, "User_WP", "번호,프로젝트유형,DB유형,제품명,제조사,타입,세부타입,정격출력,회전면적,허브높이,시동풍속,최적풍속,종단풍속,시동풍속전력계수,최적풍속전력계수,종단풍속전력계수,신규기존",
                "'" + Value[0] + "','" + 프로젝트유형[0][0] + "','"
                 + Value[1] + "','" + Value[2] + "','" + Value[3] + "','" + Value[4] + "','" + Value[5] + "','" + Value[6] + "','" + Value[7] + "','" + Value[8] + "','" + Value[9] + "','" + Value[10] + "','" + Value[11] + "','" + Value[12] + "','" + Value[13] + "','" + Value[14] + "','" + Value[15] + "'", "번호");
            }
            Program.DB.saveProject();
            MessageBox.Show("저장되었습니다.");
        }

        private void Load_WP()
        {
            WP_dataGridView.Rows.Clear();
            string[][] User_Value = Program.DB.getValue(DB.type.ProjDB, "User_WP", "번호, DB유형, 제품명, 제조사, 타입, 세부타입, 정격출력, 회전면적, 허브높이, 시동풍속, 최적풍속, 종단풍속, 시동풍속전력계수, 최적풍속전력계수, 종단풍속전력계수,신규기존", "");
            if (User_Value.Length > 0)
            {
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
            AirHP_dataGridView.Columns[2].Width = 50;
            AirHP_dataGridView.Columns[3].Width = 100;
            AirHP_dataGridView.Columns[4].Width = 65;
            AirHP_dataGridView.Columns[5].Width = 60;
            AirHP_dataGridView.Columns[6].Width = 65;
            AirHP_dataGridView.Columns[16].Width = 60;
            AirHP_dataGridView.Columns[17].Width = 60;
            AirHP_dataGridView.Columns[18].Width = 60;

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
                if (air_db.SelectHP.Contains("등급"))
                {

                    string[] token = air_db.SelectHP.Split("등급");
                    SelectHP.Clear();
                    foreach (var item in token)
                    {
                        SelectHP.Add(item.ToString());
                    }
                    string[][] CoolingValue = Program.DB.getValue(DB.type.BaseDB_Cooling, "AirCon", "냉방표준성능,대기전력", "명칭='" + SelectHP[0].ToString() + "등급<4kW' and 열원='" + air_db.Carrier + "'");
                    if (CoolingValue.Length > 0)
                    {
                        AirHP_dataGridView.Rows[nRow].Cells[8].Value = CoolingValue[0][0];
                        AirHP_dataGridView.Rows[nRow].Cells[16].Value = CoolingValue[0][1];
                    }

                    if (air_db.HC=="냉난방")
                    {
                        string[][] HeatingValue = Program.DB.getValue(DB.type.BaseDB_Heating, "히트펌프", "정격COP,한랭지COP", "등급='" + SelectHP[0].ToString() + "등급'and 연료='" + air_db.Carrier + "'");
                        if (HeatingValue.Length > 0)
                        {
                            AirHP_dataGridView.Rows[nRow].Cells[11].Value = HeatingValue[0][0];
                            AirHP_dataGridView.Rows[nRow].Cells[14].Value = HeatingValue[0][1];
                        }
                    }
                   

                    AirHP_dataGridView.Rows[nRow].Cells[4].Value = air_db.HC;
                    AirHP_dataGridView.Rows[nRow].Cells[5].Value = air_db.Carrier;
                   
                   


                }
            }
        }

        private void AirHP_dataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                try
                {
                    if (e.ColumnIndex == 7)
                    {
                        if (AirHP_dataGridView.Rows[e.RowIndex].Cells[8].Value != null && AirHP_dataGridView.Rows[e.RowIndex].Cells[8].Value.ToString() != "")
                        {
                            if (AirHP_dataGridView.Rows[e.RowIndex].Cells[7].Value != null && AirHP_dataGridView.Rows[e.RowIndex].Cells[7].Value.ToString() != "-" && AirHP_dataGridView.Rows[e.RowIndex].Cells[8].Value.ToString() != "-")
                            {
                                AirHP_dataGridView.Rows[e.RowIndex].Cells[9].Value = (Program.UTIL.dataGridView_doubleComa(AirHP_dataGridView, e.RowIndex, 7, 1) / Program.UTIL.dataGridView_doubleComa(AirHP_dataGridView, e.RowIndex, 8, 1)).ToString("0.00");
                            }
                        }
                    }
                    if (e.ColumnIndex == 8)
                    {
                        if (AirHP_dataGridView.Rows[e.RowIndex].Cells[7].Value != null && AirHP_dataGridView.Rows[e.RowIndex].Cells[7].Value.ToString() != "")
                        {
                            if (AirHP_dataGridView.Rows[e.RowIndex].Cells[7].Value.ToString() != "-" && AirHP_dataGridView.Rows[e.RowIndex].Cells[8].Value != null && AirHP_dataGridView.Rows[e.RowIndex].Cells[8].Value.ToString() != "-")
                            {
                                AirHP_dataGridView.Rows[e.RowIndex].Cells[9].Value = (Program.UTIL.dataGridView_doubleComa(AirHP_dataGridView, e.RowIndex, 7, 1) / Program.UTIL.dataGridView_doubleComa(AirHP_dataGridView, e.RowIndex, 8, 1)).ToString("0.00");
                            }
                        }
                    }
                    if (e.ColumnIndex == 10)
                    {
                        if (AirHP_dataGridView.Rows[e.RowIndex].Cells[11].Value != null && AirHP_dataGridView.Rows[e.RowIndex].Cells[11].Value.ToString() != "")
                        {
                            if (AirHP_dataGridView.Rows[e.RowIndex].Cells[10].Value != null && AirHP_dataGridView.Rows[e.RowIndex].Cells[10].Value.ToString() != "-" && AirHP_dataGridView.Rows[e.RowIndex].Cells[11].Value.ToString() != "" && AirHP_dataGridView.Rows[e.RowIndex].Cells[11].Value.ToString() != "-")
                            {
                                AirHP_dataGridView.Rows[e.RowIndex].Cells[12].Value = (Program.UTIL.dataGridView_doubleComa(AirHP_dataGridView, e.RowIndex, 10, 1) / Program.UTIL.dataGridView_doubleComa(AirHP_dataGridView, e.RowIndex, 11, 1)).ToString("0.00");
                            }
                        }
                    }
                    if (e.ColumnIndex == 11)
                    {
                        if (AirHP_dataGridView.Rows[e.RowIndex].Cells[10].Value != null && AirHP_dataGridView.Rows[e.RowIndex].Cells[10].Value.ToString() != "")
                        {
                            if (AirHP_dataGridView.Rows[e.RowIndex].Cells[10].Value.ToString() != "-" && AirHP_dataGridView.Rows[e.RowIndex].Cells[11].Value != null && AirHP_dataGridView.Rows[e.RowIndex].Cells[11].Value.ToString() != "-")
                            {
                                AirHP_dataGridView.Rows[e.RowIndex].Cells[12].Value = (Program.UTIL.dataGridView_doubleComa(AirHP_dataGridView, e.RowIndex, 10, 1) / Program.UTIL.dataGridView_doubleComa(AirHP_dataGridView, e.RowIndex, 11, 1)).ToString("0.00");
                            }
                        }
                    }
                    if (e.ColumnIndex == 13)
                    {
                        if (AirHP_dataGridView.Rows[e.RowIndex].Cells[14].Value != null && AirHP_dataGridView.Rows[e.RowIndex].Cells[14].Value.ToString() != "")
                        {
                            if (AirHP_dataGridView.Rows[e.RowIndex].Cells[13].Value != null && AirHP_dataGridView.Rows[e.RowIndex].Cells[13].Value.ToString() != "-" && AirHP_dataGridView.Rows[e.RowIndex].Cells[14].Value.ToString() != "-")
                            {
                                AirHP_dataGridView.Rows[e.RowIndex].Cells[15].Value = (Program.UTIL.dataGridView_doubleComa(AirHP_dataGridView, e.RowIndex, 13, 1) / Program.UTIL.dataGridView_doubleComa(AirHP_dataGridView, e.RowIndex, 14, 1)).ToString("0.00");
                            }
                        }
                    }
                    if (e.ColumnIndex == 14)
                    {
                        if (AirHP_dataGridView.Rows[e.RowIndex].Cells[13].Value != null && AirHP_dataGridView.Rows[e.RowIndex].Cells[13].Value.ToString() != "")
                        {
                            if (AirHP_dataGridView.Rows[e.RowIndex].Cells[13].Value.ToString() != "-" && AirHP_dataGridView.Rows[e.RowIndex].Cells[14].Value != null && AirHP_dataGridView.Rows[e.RowIndex].Cells[14].Value.ToString() != "-")
                            {
                                AirHP_dataGridView.Rows[e.RowIndex].Cells[15].Value = (Program.UTIL.dataGridView_doubleComa(AirHP_dataGridView, e.RowIndex, 13, 1) / Program.UTIL.dataGridView_doubleComa(AirHP_dataGridView, e.RowIndex, 14, 1)).ToString("0.00");
                            }
                        }
                    }
                }
                catch { }



                if (e.ColumnIndex == 4)
                {
                    if (AirHP_dataGridView.Rows[e.RowIndex].Cells[4].Value != null)
                    {
                        string currentValue = AirHP_dataGridView.Rows[e.RowIndex].Cells[4].Value.ToString();

                        if (currentValue == "냉방")
                        {
                            AirHP_dataGridView.Rows[e.RowIndex].Cells[10].Value = "-";
                            AirHP_dataGridView.Rows[e.RowIndex].Cells[11].Value = "-";
                            AirHP_dataGridView.Rows[e.RowIndex].Cells[12].Value = "-";
                            AirHP_dataGridView.Rows[e.RowIndex].Cells[13].Value = "-";
                            AirHP_dataGridView.Rows[e.RowIndex].Cells[14].Value = "-";
                            AirHP_dataGridView.Rows[e.RowIndex].Cells[15].Value = "-";
                        }
                        else
                        {
                            AirHP_dataGridView.Rows[e.RowIndex].Cells[10].Value = "";
                            AirHP_dataGridView.Rows[e.RowIndex].Cells[11].Value = "";
                            AirHP_dataGridView.Rows[e.RowIndex].Cells[12].Value = "";
                            AirHP_dataGridView.Rows[e.RowIndex].Cells[13].Value = "";
                            AirHP_dataGridView.Rows[e.RowIndex].Cells[14].Value = "";
                            AirHP_dataGridView.Rows[e.RowIndex].Cells[15].Value = "";
                        }
                    }

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


            for (int k = 2; k < 19; k++)
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
                for (int i = 1; i < 19; i++)
                {
                    if (AirHP_dataGridView.Rows[k].Cells[i].Value != null)
                    {
                        double parsedValue;
                        if (double.TryParse(AirHP_dataGridView.Rows[k].Cells[i].Value.ToString(), out parsedValue))
                        {
                            Value[i - 1] = parsedValue.ToString();
                        }
                        else
                        {
                            Value[i - 1] = AirHP_dataGridView.Rows[k].Cells[i].Value.ToString();
                        }
                    }
                    else { Value[i - 1] = ""; }
                }
                Program.DB.setValue(DB.type.ProjDB, "User_AirHP", "번호,프로젝트유형,DB유형,명칭,난방냉방,연료,공급유형,냉방정격용량,냉방정격COP,냉방정격소비전력,난방정격용량,난방정격COP,난방정격소비전력,한랭지용량,한랭지COP,한랭지소비전력,대기전력,대수,설치",
                "'" + Value[0] + "','" + 프로젝트유형[0][0] + "','"
                 + Value[1] + "','" + Value[2] + "','" + Value[3] + "','" + Value[4] + "','" + Value[5] + "','" + Value[6] + "','" + Value[7] + "','" + Value[8] + "','" + Value[9] + "','" + Value[10] + "','" + Value[11] + "','" +
                  Value[12] + "','" + Value[13] + "','" + Value[14] + "','" + Value[15] + "','" + Value[16] + "','" + Value[17] + "'", "번호");
            }
            Program.DB.saveProject();

            MessageBox.Show("저장되었습니다.");
        }


        private void Load_AirHP()
        {
            AirHP_dataGridView.Rows.Clear();
            string[][] User_Value = Program.DB.getValue(DB.type.ProjDB, "User_AirHP", "번호,DB유형,명칭,난방냉방,연료,공급유형,냉방정격용량,냉방정격COP,냉방정격소비전력,난방정격용량,난방정격COP,난방정격소비전력,한랭지용량,한랭지COP,한랭지소비전력,대기전력,대수,설치", "");
            if (User_Value.Length > 0)
            {
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

        }
        #endregion
        //////////////////////////////////////////////////지하수 히트펌프/////////////////////////////////////////////////////////////////
        #region 8. 지하수히트펌프
        public void Create_GWHP_Table()
        {
            new StackedHeaderDecorator(GWHP_dataGridView, DataGridViewAutoSizeColumnsMode.None);
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
            GWHP_dataGridView.Columns.Add("A7", "냉방정격.용량.[kW]");
            GWHP_dataGridView.Columns.Add("A8", "냉방정격.EER.[kW]");
            GWHP_dataGridView.Columns.Add("A9", "냉방정격.소비전력.[kW]");
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
            GWHP_dataGridView.Columns.Add("A22", "대기전력.[W]");

            GWHP_dataGridView.Columns[0].Width = 40;
            GWHP_dataGridView.Columns[1].Width = 60;
            GWHP_dataGridView.Columns[2].Width = 60;
            GWHP_dataGridView.Columns[3].Width = 60;
            GWHP_dataGridView.Columns[4].Width = 60;
            GWHP_dataGridView.Columns[5].Width = 60;
            GWHP_dataGridView.Columns[6].Width = 60;
            GWHP_dataGridView.Columns[7].Width = 60;
            GWHP_dataGridView.Columns[8].Width = 60;
            GWHP_dataGridView.Columns[9].Width = 60;
            GWHP_dataGridView.Columns[10].Width = 60;
            GWHP_dataGridView.Columns[11].Width = 60;
            GWHP_dataGridView.Columns[12].Width = 60;
            GWHP_dataGridView.Columns[13].Width = 60;
            GWHP_dataGridView.Columns[14].Width = 60;
            GWHP_dataGridView.Columns[15].Width = 60;
            GWHP_dataGridView.Columns[16].Width = 60;
            GWHP_dataGridView.Columns[17].Width = 60;
            GWHP_dataGridView.Columns[18].Width = 60;
            GWHP_dataGridView.Columns[18].Width = 60;
            GWHP_dataGridView.Columns[19].Width = 60;
            GWHP_dataGridView.Columns[20].Width = 60;
            GWHP_dataGridView.Columns[21].Width = 60;
            GWHP_dataGridView.Columns[22].Width = 60;


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
                try
                {
                    if (e.ColumnIndex == 7)
                    {
                        if (GWHP_dataGridView.Rows[e.RowIndex].Cells[8].Value != null && GWHP_dataGridView.Rows[e.RowIndex].Cells[8].Value.ToString() != "")
                        {
                            if (GWHP_dataGridView.Rows[e.RowIndex].Cells[7].Value.ToString() != "-" && GWHP_dataGridView.Rows[e.RowIndex].Cells[8].Value.ToString() != "-")
                            {
                                GWHP_dataGridView.Rows[e.RowIndex].Cells[9].Value = (Program.UTIL.dataGridView_doubleComa(GWHP_dataGridView, e.RowIndex, 7, 1) / Program.UTIL.dataGridView_doubleComa(GWHP_dataGridView, e.RowIndex, 8, 1)).ToString("0.00");
                            }
                        }
                    }
                    if (e.ColumnIndex == 8)
                    {
                        if (GWHP_dataGridView.Rows[e.RowIndex].Cells[7].Value != null && GWHP_dataGridView.Rows[e.RowIndex].Cells[7].Value.ToString() != "")
                        {
                            if (GWHP_dataGridView.Rows[e.RowIndex].Cells[7].Value.ToString() != "-" && GWHP_dataGridView.Rows[e.RowIndex].Cells[8].Value.ToString() != "-")
                            {
                                GWHP_dataGridView.Rows[e.RowIndex].Cells[9].Value = (Program.UTIL.dataGridView_doubleComa(GWHP_dataGridView, e.RowIndex, 7, 1) / Program.UTIL.dataGridView_doubleComa(GWHP_dataGridView, e.RowIndex, 8, 1)).ToString("0.00");
                            }
                        }
                    }
                    if (e.ColumnIndex == 10)
                    {
                        if (GWHP_dataGridView.Rows[e.RowIndex].Cells[11].Value != null && GWHP_dataGridView.Rows[e.RowIndex].Cells[11].Value.ToString() != "")
                        {
                            if (GWHP_dataGridView.Rows[e.RowIndex].Cells[10].Value.ToString() != "-" && GWHP_dataGridView.Rows[e.RowIndex].Cells[11].Value.ToString() != "-")
                            {
                                GWHP_dataGridView.Rows[e.RowIndex].Cells[12].Value = (Program.UTIL.dataGridView_doubleComa(GWHP_dataGridView, e.RowIndex, 10, 1) / Program.UTIL.dataGridView_doubleComa(GWHP_dataGridView, e.RowIndex, 11, 1)).ToString("0.00");
                            }
                        }
                    }
                    if (e.ColumnIndex == 11)
                    {
                        if (GWHP_dataGridView.Rows[e.RowIndex].Cells[10].Value != null && GWHP_dataGridView.Rows[e.RowIndex].Cells[10].Value.ToString() != "")
                        {
                            if (GWHP_dataGridView.Rows[e.RowIndex].Cells[10].Value.ToString() != "-" && GWHP_dataGridView.Rows[e.RowIndex].Cells[11].Value.ToString() != "-")
                            {
                                GWHP_dataGridView.Rows[e.RowIndex].Cells[12].Value = (Program.UTIL.dataGridView_doubleComa(GWHP_dataGridView, e.RowIndex, 10, 1) / Program.UTIL.dataGridView_doubleComa(GWHP_dataGridView, e.RowIndex, 11, 1)).ToString("0.00");
                            }
                        }
                    }
                    if (e.ColumnIndex == 13)
                    {
                        if (GWHP_dataGridView.Rows[e.RowIndex].Cells[14].Value != null && GWHP_dataGridView.Rows[e.RowIndex].Cells[14].Value.ToString() != "")
                        {
                            if (GWHP_dataGridView.Rows[e.RowIndex].Cells[13].Value.ToString() != "-" && GWHP_dataGridView.Rows[e.RowIndex].Cells[14].Value.ToString() != "-")
                            {
                                GWHP_dataGridView.Rows[e.RowIndex].Cells[15].Value = (Program.UTIL.dataGridView_doubleComa(GWHP_dataGridView, e.RowIndex, 13, 1) / Program.UTIL.dataGridView_doubleComa(GWHP_dataGridView, e.RowIndex, 14, 1)).ToString("0.00");
                            }
                        }
                    }
                    if (e.ColumnIndex == 14)
                    {
                        if (GWHP_dataGridView.Rows[e.RowIndex].Cells[13].Value != null && GWHP_dataGridView.Rows[e.RowIndex].Cells[13].Value.ToString() != "")
                        {
                            if (GWHP_dataGridView.Rows[e.RowIndex].Cells[13].Value.ToString() != "-" && GWHP_dataGridView.Rows[e.RowIndex].Cells[14].Value.ToString() != "-")
                            {
                                GWHP_dataGridView.Rows[e.RowIndex].Cells[15].Value = (Program.UTIL.dataGridView_doubleComa(GWHP_dataGridView, e.RowIndex, 13, 1) / Program.UTIL.dataGridView_doubleComa(GWHP_dataGridView, e.RowIndex, 14, 1)).ToString("0.00");
                            }
                        }
                    }
                }
                catch { }
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

            Program.DB.deleteValue(DB.type.ProjDB, "User_GroundWHP", "");

            for (int k = 0; k < GWHP_dataGridView.RowCount; k++)
            {
                String[] Value = new String[22];
                for (int i = 1; i < 23; i++)
                {
                    if (GWHP_dataGridView.Rows[k].Cells[i].Value != null)
                    {
                        double parsedValue;
                        if (double.TryParse(GWHP_dataGridView.Rows[k].Cells[i].Value.ToString(), out parsedValue))
                        {
                            Value[i - 1] = parsedValue.ToString();
                        }
                        else
                        {
                            Value[i - 1] = GWHP_dataGridView.Rows[k].Cells[i].Value.ToString();
                        }
                    }
                    else { Value[i - 1] = ""; }
                }
                Program.DB.setValue(DB.type.ProjDB, "User_GroundWHP", "번호,프로젝트유형,DB유형,명칭,연료,공급유형,수직수평,냉방용량,냉방EER,냉방소비전력,난방정격용량,난방정격COP,난방정격소비전력,난방등급2용량,난방등급2COP,난방등급2소비전력,대수,냉수입구온도,냉수출구온도,압축기,증발기,설치,대기전력",
                "'" + Value[0] + "','" + 프로젝트유형[0][0] + "','"
                 + Value[1] + "','" + Value[2] + "','" + Value[3] + "','" + Value[4] + "','" + Value[5] + "','" + Value[6] + "','" + Value[7] + "','" + Value[8] + "','" + Value[9] + "','" + Value[10] + "','" +
                 Value[11] + "','" + Value[12] + "','" + Value[13] + "','" + Value[14] + "','" + Value[15] + "','" + Value[16] + "','" + Value[17] + "','" + Value[18] + "','" + Value[19] + "','" + Value[20]
                 + "','" + Value[21] + "'", "번호");
            }
            Program.DB.saveProject();

            MessageBox.Show("저장되었습니다.");
        }
        private void Load_GWHP()
        {
            GWHP_dataGridView.Rows.Clear();
            string[][] User_Value = Program.DB.getValue(DB.type.ProjDB, "User_GroundWHP", "번호,DB유형,명칭,연료,공급유형,수직수평,냉방용량,냉방EER,냉방소비전력,난방정격용량,난방정격COP,난방정격소비전력,난방등급2용량,난방등급2COP,난방등급2소비전력,대수,냉수입구온도,냉수출구온도,압축기,증발기,설치,대기전력", "");
            if (User_Value.Length > 0)
            {
                for (int n = 0; n < User_Value.Length; n++)
                {
                    GWHP_dataGridView.Rows.Add();
                    int nRow = GWHP_dataGridView.Rows.Count - 1;
                    for (int i = 0; i < 22; i++)
                    { GWHP_dataGridView.Rows[nRow].Cells[1 + i].Value = User_Value[n][i]; }
                }
            }
        }
        #endregion
        //////////////////////////////////////////////////지열 히트펌프/////////////////////////////////////////////////////////////////
        #region 9.지열히트펌프
        public void Create_GroundHP_Table()
        {
            new StackedHeaderDecorator(GroundHP_dataGridView, DataGridViewAutoSizeColumnsMode.None);
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
            GroundHP_dataGridView.Columns.Add("A7", "냉방정격.용량.[kW]");
            GroundHP_dataGridView.Columns.Add("A8", "냉방정격.COP.[W/W]");
            GroundHP_dataGridView.Columns.Add("A9", "냉방정격.소비전력.[kW]");
            GroundHP_dataGridView.Columns.Add("A10", "난방정격(0℃).용량.[kW]");
            GroundHP_dataGridView.Columns.Add("A11", "난방정격(0℃).COP.[W/W]");
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
            GroundHP_dataGridView.Columns.Add("A22", "대기전력.[W]");

            GroundHP_dataGridView.Columns[0].Width = 40;
            GroundHP_dataGridView.Columns[1].Width = 60;
            GroundHP_dataGridView.Columns[2].Width = 60;
            GroundHP_dataGridView.Columns[3].Width = 60;
            GroundHP_dataGridView.Columns[4].Width = 60;
            GroundHP_dataGridView.Columns[5].Width = 60;
            GroundHP_dataGridView.Columns[6].Width = 60;
            GroundHP_dataGridView.Columns[7].Width = 60;
            GroundHP_dataGridView.Columns[8].Width = 60;
            GroundHP_dataGridView.Columns[9].Width = 60;
            GroundHP_dataGridView.Columns[10].Width = 60;
            GroundHP_dataGridView.Columns[11].Width = 60;
            GroundHP_dataGridView.Columns[12].Width = 60;
            GroundHP_dataGridView.Columns[13].Width = 60;
            GroundHP_dataGridView.Columns[14].Width = 60;
            GroundHP_dataGridView.Columns[15].Width = 60;
            GroundHP_dataGridView.Columns[16].Width = 60;
            GroundHP_dataGridView.Columns[17].Width = 60;
            GroundHP_dataGridView.Columns[18].Width = 60;
            GroundHP_dataGridView.Columns[18].Width = 60;
            GroundHP_dataGridView.Columns[19].Width = 60;
            GroundHP_dataGridView.Columns[20].Width = 60;
            GroundHP_dataGridView.Columns[21].Width = 60;
            GroundHP_dataGridView.Columns[22].Width = 60;

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
                try
                {
                    if (e.ColumnIndex == 7)
                    {
                        if (GroundHP_dataGridView.Rows[e.RowIndex].Cells[8].Value != null && GroundHP_dataGridView.Rows[e.RowIndex].Cells[8].Value.ToString() != "")
                        {
                            if (GroundHP_dataGridView.Rows[e.RowIndex].Cells[7].Value.ToString() != "-" && GroundHP_dataGridView.Rows[e.RowIndex].Cells[8].Value.ToString() != "-")
                            {
                                GroundHP_dataGridView.Rows[e.RowIndex].Cells[9].Value = (Program.UTIL.dataGridView_doubleComa(GroundHP_dataGridView, e.RowIndex, 7, 1) / Program.UTIL.dataGridView_doubleComa(GroundHP_dataGridView, e.RowIndex, 8, 1)).ToString("0.00");
                            }
                        }
                    }
                    if (e.ColumnIndex == 8)
                    {
                        if (GroundHP_dataGridView.Rows[e.RowIndex].Cells[7].Value != null && GroundHP_dataGridView.Rows[e.RowIndex].Cells[7].Value.ToString() != "")
                        {
                            if (GroundHP_dataGridView.Rows[e.RowIndex].Cells[7].Value.ToString() != "-" && GroundHP_dataGridView.Rows[e.RowIndex].Cells[8].Value.ToString() != "-")
                            {
                                GroundHP_dataGridView.Rows[e.RowIndex].Cells[9].Value = (Program.UTIL.dataGridView_doubleComa(GroundHP_dataGridView, e.RowIndex, 7, 1) / Program.UTIL.dataGridView_doubleComa(GroundHP_dataGridView, e.RowIndex, 8, 1)).ToString("0.00");
                            }
                        }
                    }
                    if (e.ColumnIndex == 10)
                    {
                        if (GroundHP_dataGridView.Rows[e.RowIndex].Cells[11].Value != null && GroundHP_dataGridView.Rows[e.RowIndex].Cells[11].Value.ToString() != "")
                        {
                            if (GroundHP_dataGridView.Rows[e.RowIndex].Cells[10].Value.ToString() != "-" && GroundHP_dataGridView.Rows[e.RowIndex].Cells[11].Value.ToString() != "-")
                            {
                                GroundHP_dataGridView.Rows[e.RowIndex].Cells[12].Value = (Program.UTIL.dataGridView_doubleComa(GroundHP_dataGridView, e.RowIndex, 10, 1) / Program.UTIL.dataGridView_doubleComa(GroundHP_dataGridView, e.RowIndex, 11, 1)).ToString("0.00");
                            }
                        }
                    }
                    if (e.ColumnIndex == 11)
                    {
                        if (GroundHP_dataGridView.Rows[e.RowIndex].Cells[10].Value != null && GroundHP_dataGridView.Rows[e.RowIndex].Cells[10].Value.ToString() != "")
                        {
                            if (GroundHP_dataGridView.Rows[e.RowIndex].Cells[10].Value.ToString() != "-" && GroundHP_dataGridView.Rows[e.RowIndex].Cells[11].Value.ToString() != "-")
                            {
                                GroundHP_dataGridView.Rows[e.RowIndex].Cells[12].Value = (Program.UTIL.dataGridView_doubleComa(GroundHP_dataGridView, e.RowIndex, 10, 1) / Program.UTIL.dataGridView_doubleComa(GroundHP_dataGridView, e.RowIndex, 11, 1)).ToString("0.00");
                            }
                        }
                    }
                    if (e.ColumnIndex == 13)
                    {
                        if (GroundHP_dataGridView.Rows[e.RowIndex].Cells[14].Value != null && GroundHP_dataGridView.Rows[e.RowIndex].Cells[14].Value.ToString() != "")
                        {
                            if (GroundHP_dataGridView.Rows[e.RowIndex].Cells[13].Value.ToString() != "-" && GroundHP_dataGridView.Rows[e.RowIndex].Cells[14].Value.ToString() != "-")
                            {
                                GroundHP_dataGridView.Rows[e.RowIndex].Cells[15].Value = (Program.UTIL.dataGridView_doubleComa(GroundHP_dataGridView, e.RowIndex, 13, 1) / Program.UTIL.dataGridView_doubleComa(GroundHP_dataGridView, e.RowIndex, 14, 1)).ToString("0.00");
                            }
                        }
                    }
                    if (e.ColumnIndex == 14)
                    {
                        if (GroundHP_dataGridView.Rows[e.RowIndex].Cells[13].Value != null && GroundHP_dataGridView.Rows[e.RowIndex].Cells[13].Value.ToString() != "")
                        {
                            if (GroundHP_dataGridView.Rows[e.RowIndex].Cells[13].Value.ToString() != "-" && GroundHP_dataGridView.Rows[e.RowIndex].Cells[14].Value.ToString() != "-")
                            {
                                GroundHP_dataGridView.Rows[e.RowIndex].Cells[15].Value = (Program.UTIL.dataGridView_doubleComa(GroundHP_dataGridView, e.RowIndex, 13, 1) / Program.UTIL.dataGridView_doubleComa(GroundHP_dataGridView, e.RowIndex, 14, 1)).ToString("0.00");
                            }
                        }
                    }
                }
                catch { }
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


            for (int k = 2; k < 19; k++)
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
                String[] Value = new String[22];
                for (int i = 1; i < 23; i++)
                {
                    if (GroundHP_dataGridView.Rows[k].Cells[i].Value != null)
                    {
                        double parsedValue;
                        if (double.TryParse(GroundHP_dataGridView.Rows[k].Cells[i].Value.ToString(), out parsedValue))
                        {
                            Value[i - 1] = parsedValue.ToString();
                        }
                        else
                        {
                            Value[i - 1] = GroundHP_dataGridView.Rows[k].Cells[i].Value.ToString();
                        }
                    }
                    else { Value[i - 1] = ""; }
                }
                Program.DB.setValue(DB.type.ProjDB, "User_GroundHP", "번호,프로젝트유형,DB유형,명칭,연료,공급유형,수직수평,냉방용량,냉방EER,냉방소비전력,난방정격용량,난방정격COP,난방정격소비전력,난방등급2용량,난방등급2COP,난방등급2소비전력,대수,냉수입구온도,냉수출구온도,압축기,증발기,설치,대기전력",
                "'" + Value[0] + "','" + 프로젝트유형[0][0] + "','"
                 + Value[1] + "','" + Value[2] + "','" + Value[3] + "','" + Value[4] + "','" + Value[5] + "','" + Value[6] + "','" + Value[7] + "','" + Value[8] + "','" + Value[9] + "','" + Value[10] + "','" +
                 Value[11] + "','" + Value[12] + "','" + Value[13] + "','" + Value[14] + "','" + Value[15] + "','" + Value[16] + "','" + Value[17] + "','" + Value[18] + "','" + Value[19] + "','" + Value[20]
                 + "','" + Value[21] + "'", "번호");
            }
            Program.DB.saveProject();
            MessageBox.Show("저장되었습니다.");
        }
        private void Load_GroundHP()
        {
            GroundHP_dataGridView.Rows.Clear();
            string[][] User_Value = Program.DB.getValue(DB.type.ProjDB, "User_GroundHP", "번호,DB유형,명칭,연료,공급유형,수직수평,냉방용량,냉방EER,냉방소비전력,난방정격용량,난방정격COP,난방정격소비전력,난방등급2용량,난방등급2COP,난방등급2소비전력,대수,냉수입구온도,냉수출구온도,압축기,증발기,설치,대기전력", "");
            if (User_Value.Length > 0)
            {
                for (int n = 0; n < User_Value.Length; n++)
                {
                    GroundHP_dataGridView.Rows.Add();
                    int nRow = GroundHP_dataGridView.Rows.Count - 1;
                    for (int i = 0; i < 22; i++)
                    { GroundHP_dataGridView.Rows[nRow].Cells[1 + i].Value = User_Value[n][i]; }
                }
            }
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
            Pump_dataGridView.Columns.Add("P6", "동력" + Environment.NewLine + "[W]");
            Pump_dataGridView.Columns.Add("P7", "");
            Pump_dataGridView.Columns.Add("P8", "대수" + Environment.NewLine + "[EA]");
            Pump_dataGridView.Columns[0].Width = 40;
            Pump_dataGridView.Columns[1].Width = 60;
            Pump_dataGridView.Columns[3].Width = 130;
            Pump_dataGridView.Columns[7].Width = 30;
            DataGridViewComboBoxColumn 설치유형Combo = new DataGridViewComboBoxColumn();
            설치유형Combo.HeaderText = "설치";
            설치유형Combo.Items.AddRange("기존", "신규", "철거후신규");
            Pump_dataGridView.Columns.Add(설치유형Combo);
            Pump_dataGridView.Columns[8].Width = 100;
            Pump_dataGridView.Columns[9].Width = 100;
            Pump_dataGridView.Columns[4].Visible = false;
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
            펌프종류comboBox.Items.Add("급탕펌프");
            펌프종류comboBox.Items.Add("냉각수순환펌프");
            펌프종류comboBox.Items.Add("지열순환펌프");
            Pump_dataGridView.Rows[nRow].Cells[3] = 펌프종류comboBox;

            DataGridViewButtonCell PumpPower_ButtonCell = new DataGridViewButtonCell();
            Pump_dataGridView.Rows[nRow].Cells[7] = PumpPower_ButtonCell;
            PumpPower_ButtonCell.Value = "+";
            for (int k = 4; k < 9; k++)
            {
                Pump_dataGridView.Rows[nRow].Cells[k].Style.BackColor = SystemColors.Info;
            }
            Pump_dataGridView.Rows[nRow].Cells[9].Style.BackColor = Color.White;
        }

        private void Pump_dataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (e.ColumnIndex == 4 || e.ColumnIndex == 5)
                {
                    if (Pump_dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                    {
                        try
                        {
                            if (Convert.ToDouble(Pump_dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value) < 1)
                            {
                                MessageBox.Show("퍼센트 단위로 입력하세요.(Ex : 90.1% ⇒ 90.1)");
                                Pump_dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = null;
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
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
            펌프종류comboBox.Items.Add("급탕펌프");
            펌프종류comboBox.Items.Add("냉각수순환펌프");
            펌프종류comboBox.Items.Add("지열순환펌프");
            Pump_dataGridView.Rows[nRow].Cells[3] = 펌프종류comboBox;

            DataGridViewButtonCell PumpPower_ButtonCell = new DataGridViewButtonCell();
            Pump_dataGridView.Rows[nRow].Cells[7] = PumpPower_ButtonCell;
            PumpPower_ButtonCell.Value = "+";

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
            Pump_dataGridView.Rows[nRow].Cells[7].Style.BackColor = Color.White;
        }
        private void Pump_dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                Pump_dataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
                Pump_SelectRow = e.RowIndex;
                if (e.ColumnIndex == 7)
                {
                    double eta = Pump_dataGridView.Rows[e.RowIndex].Cells[5].Value != null ? Convert.ToDouble(Pump_dataGridView.Rows[e.RowIndex].Cells[5].Value.ToString()) : 0;
                    PumpPower pumppower_form = new PumpPower(Pump_dataGridView.Rows[e.RowIndex].Cells[1].Value.ToString(), eta);
                    DialogResult result = pumppower_form.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        double PumpPower = pumppower_form.Power;
                        Pump_dataGridView.Rows[e.RowIndex].Cells[6].Value = String.Format("{0:F1}", PumpPower);
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
                Program.DB.setValue(DB.type.ProjDB, "User_Pump", "번호,프로젝트유형,명칭,종류,B효율,동력,대수,신규기존",
                "'" + Pump_dataGridView.Rows[k].Cells[1].Value.ToString() + "','" + 프로젝트유형[0][0] + "','"
                 + Pump_dataGridView.Rows[k].Cells[2].Value.ToString() + "','" + Pump_dataGridView.Rows[k].Cells[3].Value.ToString() + "','" + Pump_dataGridView.Rows[k].Cells[5].Value.ToString() + "','"
                 + Pump_dataGridView.Rows[k].Cells[6].Value.ToString() + "','" + Pump_dataGridView.Rows[k].Cells[8].Value.ToString() + "','" + Pump_dataGridView.Rows[k].Cells[9].Value.ToString()
                 + "'", "번호");
            }
            Program.DB.saveProject();
            MessageBox.Show("저장되었습니다.");
        }

        private void Load_Pump()
        {
            Pump_dataGridView.Rows.Clear();
            string[][] Value = Program.DB.getValue(DB.type.ProjDB, "User_Pump", "번호,명칭,종류,B효율,동력,대수,신규기존", "");
            if (Value.Length > 0)
            {
                for (int n = 0; n < Value.Length; n++)
                {
                    string B효율 = "", 동력 = "";
                    Pump_dataGridView.Rows.Add();
                    int nRow = Pump_dataGridView.Rows.Count - 1;

                    if (Value[n][3] != null && Value[n][3] != "")
                    {
                        B효율 = string.Format("{0:F1}", Convert.ToDouble(Value[n][3]));
                    }
                    if (Value[n][4] != null && Value[n][4] != "")
                    {
                        동력 = string.Format("{0:F0}", Convert.ToDouble(Value[n][4]));
                    }

                    Pump_dataGridView.Rows[nRow].Cells[1].Value = Value[n][0];
                    Pump_dataGridView.Rows[nRow].Cells[2].Value = Value[n][1];
                    Pump_dataGridView.Rows[nRow].Cells[3].Value = Value[n][2];
                    Pump_dataGridView.Rows[nRow].Cells[4].Value = 100;
                    Pump_dataGridView.Rows[nRow].Cells[5].Value = B효율;
                    Pump_dataGridView.Rows[nRow].Cells[6].Value = 동력;
                    Pump_dataGridView.Rows[nRow].Cells[8].Value = Value[n][5];
                    Pump_dataGridView.Rows[nRow].Cells[9].Value = Value[n][6];
                    DataGridViewButtonCell PumpPower_ButtonCell = new DataGridViewButtonCell();
                    Pump_dataGridView.Rows[nRow].Cells[7] = PumpPower_ButtonCell;
                    PumpPower_ButtonCell.Value = "+";
                }
            }
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
            ce_dataGridView.Columns.Add("A5", "냉방.용량.[kW]");
            ce_dataGridView.Columns.Add("A6", "냉방.소비전력.[kW]");
            ce_dataGridView.Columns.Add("A7", "난방.용량.[kW]");
            ce_dataGridView.Columns.Add("A8", "난방.소비전력.[kW]");
            ce_dataGridView.Columns.Add("A9", "온도제어방식");
            ce_dataGridView.Columns.Add("A10", "대수.[EA]");
            ce_dataGridView.Columns[0].Width = 40;
            ce_dataGridView.Columns[1].Width = 50;
            ce_dataGridView.Columns[9].Width = 150;
            DataGridViewComboBoxColumn 설치유형Combo = new DataGridViewComboBoxColumn();
            설치유형Combo.HeaderText = "설치";
            설치유형Combo.Items.AddRange("기존", "신규", "철거후신규");
            ce_dataGridView.Columns.Add(설치유형Combo);
            ce_dataGridView.Columns[11].Width = 100;
        }

        private Boolean ce_datagridviewDesign(DataGridViewCell cell, int column, int row)
        {
            if (ce_dataGridView.Rows[row].Cells[4].Value != null && (ce_dataGridView.Rows[row].Cells[4].Value.ToString() == "복사난방" || ce_dataGridView.Rows[row].Cells[4].Value.ToString() == "CAV유닛"))
            {
                if (column == 5 || column == 6 || column == 7 || column == 8)
                {
                    cell.Style.BackColor = Color.FromArgb(255, 255, 255);
                    cell.Style.ForeColor = Color.Black;
                    cell.Style.SelectionBackColor = Color.FromArgb(255, 255, 255);
                    cell.Style.SelectionForeColor = Color.Black;
                    return true;
                }
                else { return false; }
            }
            else if (ce_dataGridView.Rows[row].Cells[4].Value != null && ce_dataGridView.Rows[row].Cells[4].Value.ToString() == "CAV유닛")
            {
                if (column == 5 || column == 6 || column == 7 || column == 8)
                {
                    cell.Style.BackColor = Color.FromArgb(255, 255, 255);
                    cell.Style.ForeColor = Color.Black;
                    cell.Style.SelectionBackColor = Color.FromArgb(255, 255, 255);
                    cell.Style.SelectionForeColor = Color.Black;
                    return true;
                }
                else { return false; }
            }
            else if (ce_dataGridView.Rows[row].Cells[4].Value != null && ce_dataGridView.Rows[row].Cells[4].Value.ToString() == "방열기")
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
            else if (ce_dataGridView.Rows[row].Cells[4].Value != null && ce_dataGridView.Rows[row].Cells[4].Value.ToString() == "VAV유닛")
            {
                if (column == 5 || column == 6 || column == 7)
                {
                    cell.Style.BackColor = Color.FromArgb(255, 255, 255);
                    cell.Style.ForeColor = Color.Black;
                    cell.Style.SelectionBackColor = Color.FromArgb(255, 255, 255);
                    cell.Style.SelectionForeColor = Color.Black;
                    return true;
                }
                else { return false; }
            }
            else if (ce_dataGridView.Rows[row].Cells[3].Value != null && ce_dataGridView.Rows[row].Cells[3].Value.ToString() == "난방")
            {
                if (column == 5 || column == 6)
                {
                    cell.Style.BackColor = Color.FromArgb(255, 255, 255);
                    cell.Style.ForeColor = Color.Black;
                    cell.Style.SelectionBackColor = Color.FromArgb(255, 255, 255);
                    cell.Style.SelectionForeColor = Color.Black;
                    return true;
                }
                else { return false; }
            }
            else if (ce_dataGridView.Rows[row].Cells[3].Value != null && ce_dataGridView.Rows[row].Cells[3].Value.ToString() == "냉방")
            {
                if (column == 7 || column == 8)
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
            공급설비종류comboBox.Items.AddRange(new string[] { "실내기", "방열기", "팬코일유닛", "파워팬유닛", "복사난방", "복사냉방(벽)", "복사냉방(천장)", "바닥매립형컨백터", "FPU", "VAV", "CAV" });
            ce_dataGridView.Rows[nRow].Cells[4] = 공급설비종류comboBox;

            DataGridViewComboBoxCell 온도제어방식comboBox = new DataGridViewComboBoxCell();
            온도제어방식comboBox.Items.Add("제어 없음");
            온도제어방식comboBox.Items.Add("실별 온도제어");
            온도제어방식comboBox.Items.Add("on-off 자동온도제어");
            온도제어방식comboBox.Items.Add("재실기준 자동온도제어");
            ce_dataGridView.Rows[nRow].Cells[9] = 온도제어방식comboBox;

        }

        private void ce_dataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

            if (ce_dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                ce_dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.White;

                if (ce_dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "냉방")
                {
                    DataGridViewComboBoxCell 공급설비종류comboBox = new DataGridViewComboBoxCell();
                    공급설비종류comboBox.Items.AddRange(new string[] { "실내기", "팬코일유닛", "복사냉방(벽)", "복사냉방(천장)", "바닥매립형컨백터", "파워팬유닛", "VAV유닛", "CAV유닛" });
                    ce_dataGridView.Rows[e.RowIndex].Cells[4] = 공급설비종류comboBox;
                    ce_dataGridView.Rows[e.RowIndex].Cells[9].Value = "제어 없음";
                    ce_dataGridView.Rows[e.RowIndex].Cells[9].ReadOnly = true;
                }
                else if (ce_dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "냉난방")
                {
                    DataGridViewComboBoxCell 공급설비종류comboBox = new DataGridViewComboBoxCell();
                    공급설비종류comboBox.Items.AddRange(new string[] { "실내기", "팬코일유닛", "바닥매립형컨백터", "파워팬유닛", "VAV유닛", "CAV유닛" });
                    ce_dataGridView.Rows[e.RowIndex].Cells[4] = 공급설비종류comboBox;
                    ce_dataGridView.Rows[e.RowIndex].Cells[9].ReadOnly = false;
                }
                else if (ce_dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "난방")
                {
                    DataGridViewComboBoxCell 공급설비종류comboBox = new DataGridViewComboBoxCell();
                    공급설비종류comboBox.Items.AddRange(new string[] { "실내기", "방열기", "팬코일유닛", "복사난방", "바닥매립형컨백터", "파워팬유닛", "VAV유닛", "CAV유닛" });
                    ce_dataGridView.Rows[e.RowIndex].Cells[4] = 공급설비종류comboBox;
                    ce_dataGridView.Rows[e.RowIndex].Cells[9].ReadOnly = false;
                }
                //추가항목
                else if (ce_dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "VAV유닛" || ce_dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "CAV유닛")
                {
                    ce_dataGridView.Rows[e.RowIndex].Cells[5].Value = "0";
                    ce_dataGridView.Rows[e.RowIndex].Cells[6].Value = "0";
                    ce_dataGridView.Rows[e.RowIndex].Cells[5].ReadOnly = true;
                    ce_dataGridView.Rows[e.RowIndex].Cells[6].ReadOnly = true;
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
                String[] Value = new String[11];
                for (int i = 1; i < 12; i++)
                {
                    if (ce_dataGridView.Rows[k].Cells[i].Value != null)
                    {
                        double parsedValue;
                        if (double.TryParse(ce_dataGridView.Rows[k].Cells[i].Value.ToString(), out parsedValue))
                        {
                            Value[i - 1] = parsedValue.ToString();
                        }
                        else
                        {
                            Value[i - 1] = ce_dataGridView.Rows[k].Cells[i].Value.ToString();
                        }
                    }
                    else { Value[i - 1] = ""; }
                }
                Program.DB.setValue(DB.type.ProjDB, "User_ce", "번호,프로젝트유형,명칭,난방냉방,종류,용량_냉방,소비전력_냉방,용량_난방,소비전력_난방,온도제어방식,대수,신규기존",
                "'" + Value[0] + "','" + 프로젝트유형[0][0] + "','"
                 + Value[1] + "','" + Value[2] + "','" + Value[3] + "','" + Value[4] + "','" + Value[5] + "','" + Value[6] + "','" + Value[7] + "','"
                 + Value[8] + "','" + Value[9] + "','" + Value[10]
                 + "'", "번호");
            }
            Program.DB.saveProject();
            MessageBox.Show("저장되었습니다.");
        }
        private void Load_ce()
        {
            ce_dataGridView.Rows.Clear();
            String[][] Value = Program.DB.getValue(DB.type.ProjDB, "User_ce", "번호,명칭,난방냉방,종류,용량_냉방,소비전력_냉방,용량_난방,소비전력_난방,온도제어방식,대수,신규기존", "");
            if (Value.Length > 0)
            {
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
                    ce_dataGridView.Rows[nRow].Cells[9] = 온도제어방식comboBox;

                    for (int k = 0; k < 11; k++)
                    { ce_dataGridView.Rows[nRow].Cells[k + 1].Value = Value[n][k]; }
                }
            }

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
            AirCooler_dataGridView.Columns.Add("A4", "냉방성능.출력.[kW]");
            AirCooler_dataGridView.Columns.Add("A5", "냉방성능.소비전력.[kW]");
            AirCooler_dataGridView.Columns.Add("A6", "냉방성능.EER.[W/W]");

            DataGridViewComboBoxColumn PressCombo = new DataGridViewComboBoxColumn();
            PressCombo.HeaderText = "압축기";
            PressCombo.Items.AddRange(new string[] { "왕복동", "스크롤", "스크류", "터보" });
            AirCooler_dataGridView.Columns.Add(PressCombo);

            DataGridViewComboBoxColumn 연료Combo = new DataGridViewComboBoxColumn();
            연료Combo.HeaderText = "연료";
            연료Combo.Items.AddRange(new string[] { "가스", "전기" });
            AirCooler_dataGridView.Columns.Add(연료Combo);

            AirCooler_dataGridView.Columns.Add("A9", "대기전력.[W]");
            AirCooler_dataGridView.Columns.Add("A10", "대수.[EA]");

            DataGridViewComboBoxColumn 설치Combo = new DataGridViewComboBoxColumn();
            설치Combo.HeaderText = "설치";
            설치Combo.Items.AddRange(new string[] { "기존", "신규", "철거후신규" });
            AirCooler_dataGridView.Columns.Add(설치Combo);

            DataGridViewComboBoxColumn SupplycomboBox = new DataGridViewComboBoxColumn();
            SupplycomboBox.HeaderText = "부하공급";
            SupplycomboBox.Items.AddRange(new string[] { "직팽식", "수방식" });
            AirCooler_dataGridView.Columns.Add(SupplycomboBox);

            AirCooler_dataGridView.Columns.Add("A13", "송풍기.소비전력.[kW]");
            AirCooler_dataGridView.Columns.Add("A14", "냉수온도.입구.[℃]");
            AirCooler_dataGridView.Columns.Add("A15", "냉수온도.출구.[℃]");

            AirCooler_dataGridView.Columns[0].Width = 40;
            AirCooler_dataGridView.Columns[1].Width = 60;
            AirCooler_dataGridView.Columns[2].Width = 60;
            AirCooler_dataGridView.Columns[3].Width = 100;

            AirCooler_dataGridView.Columns[1].ReadOnly = true;
            AirCooler_dataGridView.Columns[2].ReadOnly = true;
        }

        private void DefaultAirCooler_Add_button_Click(object sender, EventArgs e)
        {
            ArrayList SelectHP = new ArrayList();
            int nRow = AirCooler_dataGridView.Rows.Add();
            Load_AirCooler_Num();
            AirCooler_dataGridView.Rows[nRow].Cells[2].Value = "기본";


            AirCooler_DB air_db = new AirCooler_DB("기본DB 적용");
            DialogResult result = air_db.ShowDialog();
            if (result == DialogResult.OK)
            {
                try
                {
                    string token = air_db.SelectAC;
                    string[][] CoolingValue = Program.DB.getValue(DB.type.BaseDB_Cooling, "AirCooler", "압축기,냉수출구온도,EER", "번호='" + token + "'");
                    if (CoolingValue.Length > 0)
                    {
                        AirCooler_dataGridView.Rows[nRow].Cells[6].Value = CoolingValue[0][2];//EER
                        AirCooler_dataGridView.Rows[nRow].Cells[7].Value = CoolingValue[0][0];//압축기
                        AirCooler_dataGridView.Rows[nRow].Cells[8].Value = "전기";
                        AirCooler_dataGridView.Rows[nRow].Cells[12].Value = "수방식";
                        AirCooler_dataGridView.Rows[nRow].Cells[13].Value = "0";

                        int temp = 0;
                        if (Convert.ToInt32(CoolingValue[0][1]) == 6) temp = 12;
                        else if (Convert.ToInt32(CoolingValue[0][1]) == 14) temp = 18;

                        AirCooler_dataGridView.Rows[nRow].Cells[14].Value = temp;//입구온도
                        AirCooler_dataGridView.Rows[nRow].Cells[15].Value = CoolingValue[0][1];//출구온도
                    }

                }
                catch { }
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
            if (e.ColumnIndex == 4)
            {
                if (AirCooler_dataGridView.Rows[e.RowIndex].Cells[6].Value != null && AirCooler_dataGridView.Rows[e.RowIndex].Cells[6].Value.ToString() != "")
                {
                    if (AirCooler_dataGridView.Rows[e.RowIndex].Cells[4].Value.ToString() != "-" && AirCooler_dataGridView.Rows[e.RowIndex].Cells[6].Value.ToString() != "-")
                    {
                        AirCooler_dataGridView.Rows[e.RowIndex].Cells[5].Value = (Program.UTIL.dataGridView_doubleComa(AirCooler_dataGridView, e.RowIndex, 4, 1) / Program.UTIL.dataGridView_doubleComa(AirCooler_dataGridView, e.RowIndex, 6, 1)).ToString("0.00");
                    }
                }
            }
            if (e.ColumnIndex == 6)
            {
                if (AirCooler_dataGridView.Rows[e.RowIndex].Cells[4].Value != null && AirCooler_dataGridView.Rows[e.RowIndex].Cells[4].Value.ToString() != "")
                {
                    if (AirCooler_dataGridView.Rows[e.RowIndex].Cells[4].Value.ToString() != "-" && AirCooler_dataGridView.Rows[e.RowIndex].Cells[6].Value.ToString() != "-")
                    {
                        AirCooler_dataGridView.Rows[e.RowIndex].Cells[5].Value = (Program.UTIL.dataGridView_doubleComa(AirCooler_dataGridView, e.RowIndex, 4, 1) / Program.UTIL.dataGridView_doubleComa(AirCooler_dataGridView, e.RowIndex, 6, 1)).ToString("0.00");
                    }
                }
            }
            if (e.ColumnIndex == 12)
            {
                int k = e.RowIndex;
                if (AirCooler_dataGridView.Rows[k].Cells[12].Value.ToString() == "직팽식")
                {
                    AirCooler_dataGridView.Rows[k].Cells[13].Value = "0";
                    AirCooler_dataGridView.Rows[k].Cells[14].Value = "";
                    AirCooler_dataGridView.Rows[k].Cells[15].Value = "";

                    AirCooler_dataGridView.Rows[k].Cells[13].ReadOnly = false;
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
                String[] Value = new String[16];

                for (int i = 1; i < 16; i++)
                {
                    if (AirCooler_dataGridView.Rows[k].Cells[i].Value != null && AirCooler_dataGridView.Rows[k].Cells[i].Value != "")
                    {
                        double parsedValue;
                        if (double.TryParse(AirCooler_dataGridView.Rows[k].Cells[i].Value.ToString(), out parsedValue))
                        {
                            Value[i - 1] = parsedValue.ToString();
                        }
                        else
                        {
                            Value[i - 1] = AirCooler_dataGridView.Rows[k].Cells[i].Value.ToString();
                        }
                    }
                    else if (i == 13 || i == 14 || i == 15)
                    {
                        Value[i - 1] = null;
                    }
                    else
                    {
                        string v = AirCooler_dataGridView.Columns[i + 1].HeaderText;
                        MessageBox.Show(string.Format("{0} 를 입력해 주세요.", v));
                        return;
                    }
                    if (Value[11] == "직팽식")
                    {
                        Value[15] = null;
                    }
                    else if (Value[11] == "수방식")
                    {
                        Value[15] = "판형";
                    }
                }
                Program.DB.setValue(DB.type.ProjDB, "User_AirCooler", "번호,DB유형,명칭,냉방출력,냉방소비전력,EER,압축기,연료,대기전력,대수,설치,부하측공급형식,송풍기전력,냉수입구온도,냉수출구온도,증발기", //16개항목임
                "'" + Value[0] + "','" + Value[1] + "','" + Value[2] + "','" + Value[3] + "','" + Value[4] + "','" + Value[5] + "','"
                 + Value[6] + "','" + Value[7] + "','" + Value[8] + "','" + Value[9] + "','" + Value[10] + "','"
                 + Value[11] + "','" + Value[12] + "','" + Value[13] + "','" + Value[14] + "','" + Value[15] + "'", "번호");
            }
            Program.DB.saveProject();
            MessageBox.Show("저장되었습니다.");
        }
        private void Load_AirCooler()
        {
            AirCooler_dataGridView.Rows.Clear();
            string[][] User_Value = Program.DB.getValue(DB.type.ProjDB, "User_AirCooler", "번호,DB유형,명칭,냉방출력,냉방소비전력,EER,압축기,연료,대기전력,대수,설치,부하측공급형식,송풍기전력,냉수입구온도,냉수출구온도", "");
            if (User_Value.Length > 0)
            {
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
                    AirCooler_dataGridView.Rows[nRow].Cells[13].Value = User_Value[n][12];  //송풍기전력[kW]
                    AirCooler_dataGridView.Rows[nRow].Cells[14].Value = User_Value[n][13]; //냉수입구온도
                    AirCooler_dataGridView.Rows[nRow].Cells[15].Value = User_Value[n][14];  //냉수출구온도
                }
            }
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

            WaterCooler_dataGridView.Columns.Add("A13", "냉수온도.입구[℃]");
            WaterCooler_dataGridView.Columns.Add("A14", "냉수온도.출구[℃]");

            WaterCooler_dataGridView.Columns[0].Width = 40;
            WaterCooler_dataGridView.Columns[1].Width = 60;
            WaterCooler_dataGridView.Columns[2].Width = 60;
            WaterCooler_dataGridView.Columns[3].Width = 100;

            WaterCooler_dataGridView.Columns[1].ReadOnly = true;
            WaterCooler_dataGridView.Columns[2].ReadOnly = true;
        }

        private void DefaultWaterCooler_Add_button_Click(object sender, EventArgs e)
        {
            int nRow = WaterCooler_dataGridView.Rows.Add();
            Load_WaterCooler_Num();
            WaterCooler_dataGridView.Rows[nRow].Cells[2].Value = "기본";

            WaterCooler_DB Water_db = new WaterCooler_DB("기본DB 적용");
            DialogResult result = Water_db.ShowDialog();
            if (result == DialogResult.OK)
            {
                string[][] DefaultDB_Value = Program.DB.getValue(DB.type.BaseDB_Cooling, "WaterCooler", "EER,압축기,냉수출구온도", "번호='" + Water_db.SelectWC + "'");
                if (DefaultDB_Value.Length > 0)
                {
                    WaterCooler_dataGridView.Rows[nRow].Cells[6].Value = DefaultDB_Value[0][0];
                    WaterCooler_dataGridView.Rows[nRow].Cells[7].Value = DefaultDB_Value[0][1];
                    WaterCooler_dataGridView.Rows[nRow].Cells[8].Value = "전기";
                    int temp = 0;
                    if (Convert.ToInt32(DefaultDB_Value[0][2]) == 6) temp = 12;
                    else if (Convert.ToInt32(DefaultDB_Value[0][2]) == 14) temp = 18;

                    WaterCooler_dataGridView.Rows[nRow].Cells[13].Value = temp;
                    WaterCooler_dataGridView.Rows[nRow].Cells[14].Value = DefaultDB_Value[0][2];
                }
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

            if (e.ColumnIndex == 4)
            {
                if (WaterCooler_dataGridView.Rows[e.RowIndex].Cells[6].Value != null && WaterCooler_dataGridView.Rows[e.RowIndex].Cells[6].Value.ToString() != "")
                {
                    if (WaterCooler_dataGridView.Rows[e.RowIndex].Cells[4].Value.ToString() != "-" && WaterCooler_dataGridView.Rows[e.RowIndex].Cells[6].Value.ToString() != "-")
                    {
                        WaterCooler_dataGridView.Rows[e.RowIndex].Cells[5].Value = (Program.UTIL.dataGridView_doubleComa(WaterCooler_dataGridView, e.RowIndex, 4, 1) / Program.UTIL.dataGridView_doubleComa(WaterCooler_dataGridView, e.RowIndex, 6, 1)).ToString("0.00");
                    }
                }
            }
            if (e.ColumnIndex == 6)
            {
                if (WaterCooler_dataGridView.Rows[e.RowIndex].Cells[4].Value != null && WaterCooler_dataGridView.Rows[e.RowIndex].Cells[4].Value.ToString() != "")
                {
                    WaterCooler_dataGridView.Rows[e.RowIndex].Cells[5].Value = (Program.UTIL.dataGridView_doubleComa(WaterCooler_dataGridView, e.RowIndex, 4, 1) / Program.UTIL.dataGridView_doubleComa(WaterCooler_dataGridView, e.RowIndex, 6, 1)).ToString("0.00");
                }
            }
        }

        private void WaterCooler_Save_button_Click(object sender, EventArgs e)
        {
            Program.DB.deleteValue(DB.type.ProjDB, "User_WaterCooler", "");

            for (int k = 0; k < WaterCooler_dataGridView.RowCount; k++)
            {
                String[] Value = new String[14];
                for (int i = 1; i < 15; i++)
                {
                    if (WaterCooler_dataGridView.Rows[k].Cells[i].Value != null)
                    {
                        double parsedValue;
                        if (double.TryParse(WaterCooler_dataGridView.Rows[k].Cells[i].Value.ToString(), out parsedValue))
                        {
                            Value[i - 1] = parsedValue.ToString();
                        }
                        else
                        {
                            Value[i - 1] = WaterCooler_dataGridView.Rows[k].Cells[i].Value.ToString();
                        }
                    }
                    else { Value[i - 1] = ""; }
                }
                Program.DB.setValue(DB.type.ProjDB, "User_WaterCooler", "번호,DB유형,명칭,냉방출력,냉방소비전력,EER,압축기,연료,대기전력,대수,설치,증발기,냉수입구온도,냉수출구온도",
                "'" + Value[0] + "','"
                 + Value[1] + "','" + Value[2] + "','" + Value[3] + "','" + Value[4] + "','" + Value[5] + "','"
                 + Value[6] + "','" + Value[7] + "','" + Value[8] + "','" + Value[9] + "','" + Value[10] + "','"
                 + Value[11] + "','" + Value[12] + "','" + Value[13] + "'", "번호");
            }
            Program.DB.saveProject();
            MessageBox.Show("저장되었습니다.");
        }


        private void Load_WaterCooler()
        {
            WaterCooler_dataGridView.Rows.Clear();
            string[][] User_Value = Program.DB.getValue(DB.type.ProjDB, "User_WaterCooler", "번호,DB유형,명칭,냉방출력,냉방소비전력,EER,압축기,연료,대기전력,대수,설치,증발기,냉수입구온도,냉수출구온도", "");
            if (User_Value.Length > 0)
            {
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
            Solar_dataGridView.Columns.Add("A5", "모듈면적.A[m" + Program.UTIL.Subscript(2, true) + "]");
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
                    if (Value.Length > 0)
                    {
                        Solar_dataGridView.Rows[nRow].Cells[6].Value = Value[0][0];
                        Solar_dataGridView.Rows[nRow].Cells[7].Value = Value[0][1];
                        Solar_dataGridView.Rows[nRow].Cells[8].Value = Value[0][2];
                        Solar_dataGridView.Rows[nRow].Cells[9].Value = Value[0][3];
                        Solar_dataGridView.Rows[nRow].Cells[10].Value = Value[0][4];
                    }
                }
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
                    {
                        double parsedValue;
                        if (double.TryParse(Solar_dataGridView.Rows[k].Cells[i].Value.ToString(), out parsedValue))
                        {
                            Value[i - 1] = parsedValue.ToString();
                        }
                        else
                        {
                            Value[i - 1] = Solar_dataGridView.Rows[k].Cells[i].Value.ToString();
                        }
                    }
                    else { Value[i - 1] = ""; }
                }
                Program.DB.setValue(DB.type.ProjDB, "User_Solar", "번호,프로젝트유형,DB유형,명칭,난방급탕,모듈면적,효율,열손실계수1차,열손실계수2차,입사각50도,유효열용량,신규기존",
                "'" + Value[0] + "','" + 프로젝트유형[0][0] + "','"
                 + Value[1] + "','" + Value[2] + "','" + Value[3] + "','" + Value[4] + "','" + Value[5] + "','" + Value[6] + "','" + Value[7] + "','" + Value[8] + "','" + Value[9] + "','"
                 + Value[10]
                 + "'", "번호");
            }
            Program.DB.saveProject();
            MessageBox.Show("저장되었습니다.");
        }

        private void Load_Solar()
        {
            Solar_dataGridView.Rows.Clear();
            string[][] User_Value = Program.DB.getValue(DB.type.ProjDB, "User_Solar", "번호,DB유형,명칭,난방급탕,모듈면적,효율,열손실계수1차,열손실계수2차,입사각50도,유효열용량,신규기존", "");
            if (User_Value.Length > 0)
            {
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
        }

        #endregion
        ///////////////////////////////////////////////////공조/////////////////////////////////////////////////////////////////
        #region 15.공조
        public void Create_AHU_Table()
        {
            new StackedHeaderDecorator(AHU_dataGridView, DataGridViewAutoSizeColumnsMode.None, AHU_dataGridView_RowHandle);
            AHU_dataGridView.Columns.Clear();
            AHU_checkBoxColumn.HeaderText = "선택";
            AHU_checkBoxColumn.Name = "check";
            AHU_dataGridView.Columns.Add(AHU_checkBoxColumn);

            AHU_dataGridView.Columns.Add("A1", "번호");
            AHU_dataGridView.Columns.Add("A2", "명칭");

            DataGridViewComboBoxColumn 설치유형Combo = new DataGridViewComboBoxColumn();
            설치유형Combo.HeaderText = "설치";
            설치유형Combo.Items.AddRange("기존", "신규", "철거후신규");
            AHU_dataGridView.Columns.Add(설치유형Combo);

            DataGridViewComboBoxColumn 공조방식Combo = new DataGridViewComboBoxColumn();
            공조방식Combo.HeaderText = "공조방식";
            공조방식Combo.Items.AddRange("변풍량", "정풍량");
            AHU_dataGridView.Columns.Add(공조방식Combo);

            AHU_dataGridView.Columns.Add("A5", "열회수기.유형");
            AHU_dataGridView.Columns.Add("A6", "회수 효율.온도.냉방.[%]");
            AHU_dataGridView.Columns.Add("A7", "회수 효율.온도.난방.[%]");
            AHU_dataGridView.Columns.Add("A8", "회수 효율.유효전열.냉방.[%]");
            AHU_dataGridView.Columns.Add("A9", "회수 효율.유효전열.난방.[%]");
            AHU_dataGridView.Columns.Add("A10", "회수 효율.절대습도.냉방.[%]");
            AHU_dataGridView.Columns.Add("A11", "회수 효율.절대습도.난방.[%]");
            AHU_dataGridView.Columns.Add("A12", "냉각코일.출력.[kW]");
            AHU_dataGridView.Columns.Add("A13", "냉각코일.입구온도.[℃_DB]");
            AHU_dataGridView.Columns.Add("A14", "냉각코일.입구온도.[℃_WB]");
            AHU_dataGridView.Columns.Add("A15", "냉각코일.출구온도.[℃_DB]");
            AHU_dataGridView.Columns.Add("A16", "냉각코일.출구온도.[℃_WB]");
            AHU_dataGridView.Columns.Add("A17", "난방코일.출력.[kW]");
            AHU_dataGridView.Columns.Add("A18", "난방코일.입구온도.[℃_DB]");
            AHU_dataGridView.Columns.Add("A19", "난방코일.출구온도.[℃_DB]");
            AHU_dataGridView.Columns.Add("A20", "가습기.유형");
            AHU_dataGridView.Columns.Add("A21", "가습기.제어유형");
            AHU_dataGridView.Columns.Add("A22", "가습기.습도수준");
            AHU_dataGridView.Columns.Add("A23", "가습기.용량.[kg/h]");
            AHU_dataGridView.Columns.Add("A24", "송풍기.풍량.급기.[CMH]");
            AHU_dataGridView.Columns.Add("A25", "송풍기.풍량.배기.[CMH]");
            AHU_dataGridView.Columns.Add("A26", "송풍기.정압.급기.[Pa]");
            AHU_dataGridView.Columns.Add("A27", "송풍기.정압.배기.[Pa]");
            AHU_dataGridView.Columns.Add("A28", "송풍기.팬동력.급기.[kW]");
            AHU_dataGridView.Columns.Add("A29", "송풍기.팬동력.배기.[kW]");
            AHU_dataGridView.Columns.Add("A30", "송풍기.모터제어");
            AHU_dataGridView.Columns[0].Width = 40;
            AHU_dataGridView.Columns[1].Width = 60;
            AHU_dataGridView.Columns[2].Width = 60;
            AHU_dataGridView.Columns[3].Width = 60;
            AHU_dataGridView.Columns[4].Width = 80;
            AHU_dataGridView.Columns[5].Width = 100;
            AHU_dataGridView.Columns[6].Width = 40;
            AHU_dataGridView.Columns[7].Width = 40;
            AHU_dataGridView.Columns[8].Width = 40;
            AHU_dataGridView.Columns[9].Width = 40;
            AHU_dataGridView.Columns[10].Width = 40;
            AHU_dataGridView.Columns[11].Width = 40;
            AHU_dataGridView.Columns[12].Width = 40;
            AHU_dataGridView.Columns[13].Width = 55;
            AHU_dataGridView.Columns[14].Width = 55;
            AHU_dataGridView.Columns[15].Width = 55;
            AHU_dataGridView.Columns[16].Width = 55;
            AHU_dataGridView.Columns[17].Width = 40;
            AHU_dataGridView.Columns[18].Width = 55;
            AHU_dataGridView.Columns[19].Width = 55;
            AHU_dataGridView.Columns[20].Width = 100;
            AHU_dataGridView.Columns[21].Width = 80;
            AHU_dataGridView.Columns[22].Width = 80;
            AHU_dataGridView.Columns[23].Width = 45;
            AHU_dataGridView.Columns[24].Width = 50;
            AHU_dataGridView.Columns[25].Width = 50;
            AHU_dataGridView.Columns[26].Width = 40;
            AHU_dataGridView.Columns[27].Width = 40;
            AHU_dataGridView.Columns[28].Width = 40;
            AHU_dataGridView.Columns[29].Width = 40;
            AHU_dataGridView.Columns[30].Width = 100;
        }
        private bool AHU_dataGridView_RowHandle(DataGridViewCell cell, int column, int row)
        {
            if (column == 10)
            {
                cell.Style.BackColor = Color.FromArgb(255, 255, 255);
                return true;
            }
            if (column == 11)
            {
                cell.Style.BackColor = Color.FromArgb(255, 255, 255);
                return true;
            }
            else return false;
        }
        private void UserAHU_Add_button_Click(global::System.Object sender, global::System.EventArgs e)
        {
            int nRow = AHU_dataGridView.Rows.Add();
            Load_AHU_Num();

            DataGridViewComboBoxCell 열회수기유형Combo = new DataGridViewComboBoxCell();
            열회수기유형Combo.Items.Add("없음");
            열회수기유형Combo.Items.Add("판형");
            열회수기유형Combo.Items.Add("일반회전형");
            열회수기유형Combo.Items.Add("흡수식회전형");
            열회수기유형Combo.Items.Add("흡착식회전형");
            AHU_dataGridView.Rows[nRow].Cells[5] = 열회수기유형Combo;


            DataGridViewComboBoxCell 가습기유형Combo = new DataGridViewComboBoxCell();
            가습기유형Combo.Items.Add("없음");
            가습기유형Combo.Items.Add("접촉형");
            가습기유형Combo.Items.Add("회전분사형");
            가습기유형Combo.Items.Add("고압분사형");
            가습기유형Combo.Items.Add("스팀형");
            AHU_dataGridView.Rows[nRow].Cells[20] = 가습기유형Combo;

            DataGridViewComboBoxCell 가습기제어유형Combo = new DataGridViewComboBoxCell();
            가습기제어유형Combo.Items.Add("on/off제어");
            가습기제어유형Combo.Items.Add("인버터제어");
            AHU_dataGridView.Rows[nRow].Cells[21] = 가습기제어유형Combo;

            DataGridViewComboBoxCell 습도수준Combo = new DataGridViewComboBoxCell();
            습도수준Combo.Items.Add("항온항습");
            습도수준Combo.Items.Add("습도고려");
            습도수준Combo.Items.Add("고려안함");
            AHU_dataGridView.Rows[nRow].Cells[22] = 습도수준Combo;

            DataGridViewComboBoxCell 송풍기모터제어Combo = new DataGridViewComboBoxCell();
            송풍기모터제어Combo.Items.Add("on/off제어");
            송풍기모터제어Combo.Items.Add("2단제어");
            송풍기모터제어Combo.Items.Add("3단제어");
            송풍기모터제어Combo.Items.Add("인버터제어");
            AHU_dataGridView.Rows[nRow].Cells[30] = 송풍기모터제어Combo;

        }

        private void AHU_Remove_button_Click(global::System.Object sender, global::System.EventArgs e)
        {
            AHU_dataGridView.Rows.Remove(AHU_dataGridView.Rows[AHU_SelectRow]);
            Load_AHU_Num();
        }

        private void AHU_Copy_button_Click(global::System.Object sender, global::System.EventArgs e)
        {
            int nRow = AHU_dataGridView.Rows.Add();
            Load_AHU_Num();

            DataGridViewComboBoxCell 열회수기유형Combo = new DataGridViewComboBoxCell();
            열회수기유형Combo.Items.Add("없음");
            열회수기유형Combo.Items.Add("판형");
            열회수기유형Combo.Items.Add("일반회전형");
            열회수기유형Combo.Items.Add("흡수식회전형");
            열회수기유형Combo.Items.Add("흡착식회전형");
            AHU_dataGridView.Rows[nRow].Cells[5] = 열회수기유형Combo;

            DataGridViewComboBoxCell 가습기유형Combo = new DataGridViewComboBoxCell();
            가습기유형Combo.Items.Add("없음");
            가습기유형Combo.Items.Add("접촉형");
            가습기유형Combo.Items.Add("회전분사형");
            가습기유형Combo.Items.Add("고압분사형");
            가습기유형Combo.Items.Add("스팀형");
            AHU_dataGridView.Rows[nRow].Cells[20] = 가습기유형Combo;


            DataGridViewComboBoxCell 가습기제어유형Combo = new DataGridViewComboBoxCell();
            가습기제어유형Combo.Items.Add("on/off제어");
            가습기제어유형Combo.Items.Add("인버터제어");
            AHU_dataGridView.Rows[nRow].Cells[21] = 가습기제어유형Combo;

            DataGridViewComboBoxCell 습도수준Combo = new DataGridViewComboBoxCell();
            습도수준Combo.Items.Add("항온항습");
            습도수준Combo.Items.Add("습도고려");
            습도수준Combo.Items.Add("고려안함");
            AHU_dataGridView.Rows[nRow].Cells[22] = 습도수준Combo;

            DataGridViewComboBoxCell 송풍기모터제어Combo = new DataGridViewComboBoxCell();
            송풍기모터제어Combo.Items.Add("on/off제어");
            송풍기모터제어Combo.Items.Add("2단제어");
            송풍기모터제어Combo.Items.Add("3단제어");
            송풍기모터제어Combo.Items.Add("인버터제어");
            AHU_dataGridView.Rows[nRow].Cells[30] = 송풍기모터제어Combo;

            for (int k = 2; k < 31; k++)
            {
                if (AHU_dataGridView.Rows[AHU_SelectRow].Cells[k].Value != null)
                {
                    AHU_dataGridView.Rows[nRow].Cells[k].Value = AHU_dataGridView.Rows[AHU_SelectRow].Cells[k].Value;
                }
            }
            if (AHU_dataGridView.Rows[AHU_SelectRow].Cells[2].Value != null)
            {
                AHU_dataGridView.Rows[nRow].Cells[2].Value = AHU_dataGridView.Rows[AHU_SelectRow].Cells[2].Value.ToString() + "_복사";
            }
        }


        private void Load_AHU_Num()
        {
            for (int k = 0; k < AHU_dataGridView.RowCount; k++)
            {
                if (k + 1 < 10)
                { AHU_dataGridView.Rows[k].Cells[1].Value = "AHU0" + (k + 1).ToString(); }
                else { AHU_dataGridView.Rows[k].Cells[1].Value = "AHU" + (k + 1).ToString(); }
            }
        }

        private void AHU_dataGridView_CellContentClick(global::System.Object sender, global::System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                AHU_dataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
                AHU_SelectRow = e.RowIndex;
            }
        }
        private void AHU_dataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            double temp_eta, all_eta, Humidity_eta;
            if (e.RowIndex >= 0)
            {
                if (e.ColumnIndex == 6 || e.ColumnIndex == 8)
                {
                    if (AHU_dataGridView.Rows[e.RowIndex].Cells[6].Value != null && AHU_dataGridView.Rows[e.RowIndex].Cells[8].Value != null)
                    {
                        temp_eta = Program.UTIL.dataGridView_doubleComa(AHU_dataGridView, e.RowIndex, 6, 0);
                        all_eta = Program.UTIL.dataGridView_doubleComa(AHU_dataGridView, e.RowIndex, 8, 0);
                        Humidity_eta = Calc_HumidityEta_Cooling(temp_eta, all_eta);
                        AHU_dataGridView.Rows[e.RowIndex].Cells[10].Value = Humidity_eta.ToString("0.0");
                    }
                }
                if (e.ColumnIndex == 7 || e.ColumnIndex == 9)
                {
                    if (AHU_dataGridView.Rows[e.RowIndex].Cells[7].Value != null && AHU_dataGridView.Rows[e.RowIndex].Cells[9].Value != null)
                    {
                        temp_eta = Program.UTIL.dataGridView_doubleComa(AHU_dataGridView, e.RowIndex, 7, 0);
                        all_eta = Program.UTIL.dataGridView_doubleComa(AHU_dataGridView, e.RowIndex, 9, 0);
                        Humidity_eta = Calc_HumidityEta_Heating(temp_eta, all_eta);
                        AHU_dataGridView.Rows[e.RowIndex].Cells[11].Value = Humidity_eta.ToString("0.0");
                    }
                }
            }
        }
        private double Calc_HumidityEta_Heating(double temp_eta, double all_eta)
        {
            double 외기온도 = 2, 실내온도 = 22, 외기상대습도 = 0.75, 실내상대습도 = 0.4;

            double 외기절대습도 = 0.622 * (611.2 * Math.Exp(17.62 * 외기온도 / (243.12 + 외기온도))) * 외기상대습도 / (101325 - (611.2 * Math.Exp(17.62 * 외기온도 / (243.12 + 외기온도))) * 외기상대습도);
            double 실내절대습도 = 0.622 * (611.2 * Math.Exp(17.62 * 실내온도 / (243.12 + 실내온도))) * 실내상대습도 / (101325 - (611.2 * Math.Exp(17.62 * 실내온도 / (243.12 + 실내온도))) * 실내상대습도);
            double 외기엔탈피 = 1.006 * 외기온도 + 외기절대습도 * (2500 + 1.86 * 외기온도);
            double 실내엔탈피 = 1.006 * 실내온도 + 실내절대습도 * (2500 + 1.86 * 실내온도);

            double 열교환후온도 = 외기온도 - temp_eta / 100 * (외기온도 - 실내온도);
            double 총엔탈피 = 외기엔탈피 - all_eta / 100 * (외기엔탈피 - 실내엔탈피);
            double 수증기엔탈피 = 총엔탈피 - 열교환후온도 * 1.006;
            double 교환후습도 = 수증기엔탈피 / (2500 + 1.86 * 열교환후온도);
            double eta = (외기절대습도 - 교환후습도) / (외기절대습도 - 실내절대습도) * 100;
            return eta;
        }
        private double Calc_HumidityEta_Cooling(double temp_eta, double all_eta)
        {
            double 외기온도 = 35, 실내온도 = 24, 외기상대습도 = 0.4, 실내상대습도 = 0.5;

            double 외기절대습도 = 0.622 * (611.2 * Math.Exp(17.62 * 외기온도 / (243.12 + 외기온도))) * 외기상대습도 / (101325 - (611.2 * Math.Exp(17.62 * 외기온도 / (243.12 + 외기온도))) * 외기상대습도);
            double 실내절대습도 = 0.622 * (611.2 * Math.Exp(17.62 * 실내온도 / (243.12 + 실내온도))) * 실내상대습도 / (101325 - (611.2 * Math.Exp(17.62 * 실내온도 / (243.12 + 실내온도))) * 실내상대습도);
            double 외기엔탈피 = 1.006 * 외기온도 + 외기절대습도 * (2500 + 1.86 * 외기온도);
            double 실내엔탈피 = 1.006 * 실내온도 + 실내절대습도 * (2500 + 1.86 * 실내온도);

            double 열교환후온도 = 외기온도 - temp_eta / 100 * (외기온도 - 실내온도);
            double 총엔탈피 = 외기엔탈피 - all_eta / 100 * (외기엔탈피 - 실내엔탈피);
            double 수증기엔탈피 = 총엔탈피 - 열교환후온도 * 1.006;
            double 교환후습도 = 수증기엔탈피 / (2500 + 1.86 * 열교환후온도);
            double eta = (외기절대습도 - 교환후습도) / (외기절대습도 - 실내절대습도) * 100;
            return eta;
        }
        private void AHU_Save_button_Click(global::System.Object sender, global::System.EventArgs e)
        {
            Program.DB.deleteValue(DB.type.ProjDB, "User_AHU", "");

            for (int k = 0; k < AHU_dataGridView.RowCount; k++)
            {
                String[] Value = new String[30];
                for (int i = 1; i < 31; i++)
                {
                    if (AHU_dataGridView.Rows[k].Cells[i].Value != null)
                    {
                        double parsedValue;
                        if (double.TryParse(AHU_dataGridView.Rows[k].Cells[i].Value.ToString(), out parsedValue))
                        {
                            Value[i - 1] = parsedValue.ToString();
                        }
                        else
                        {
                            Value[i - 1] = AHU_dataGridView.Rows[k].Cells[i].Value.ToString();
                        }
                    }
                    else { Value[i - 1] = ""; }
                }
                Program.DB.setValue(DB.type.ProjDB, "User_AHU", "번호,프로젝트유형," +
                    "명칭,설치유형,공조방식",
                "'" + Value[0] + "','" + 프로젝트유형[0][0] + "','"
                 + Value[1] + "','" + Value[2] + "','"
                 + Value[3]
                 + "'", "번호");
                Program.DB.setValue(DB.type.ProjDB, "User_AHU", "번호," +
                    "열회수유형,온도교환효율_냉방,온도교환효율_난방,전열교환효율_냉방,전열교환효율_난방,습도교환효율_냉방,습도교환효율_난방",
                "'" + Value[0] + "','"
                 + Value[4] + "','" + Value[5] + "','" + Value[6] + "','"
                 + Value[7] + "','" + Value[8] + "','" + Value[9] + "','"
                 + Value[10]
                 + "'", "번호");
                Program.DB.setValue(DB.type.ProjDB, "User_AHU", "번호," +
                    "냉각코일출력,냉각코일_입구_건구온도,냉각코일_입구_습구온도,냉각코일_출구_건구온도,냉각코일_출구_습구온도,난방코일출력,난방코일_입구온도,난방코일_출구온도",
                "'" + Value[0] + "','"
                 + Value[11] + "','" + Value[12] + "','" + Value[13] + "','"
                 + Value[14] + "','" + Value[15] + "','" + Value[16] + "','" + Value[17] + "','"
                 + Value[18]
                 + "'", "번호");
                Program.DB.setValue(DB.type.ProjDB, "User_AHU", "번호," +
                    "가습기유형,가습기제어유형,가습기습도수준,가습기용량",
                "'" + Value[0] + "','"
                 + Value[19] + "','" + Value[20] + "','" + Value[21] + "','"
                 + Value[22]
                 + "'", "번호");
                Program.DB.setValue(DB.type.ProjDB, "User_AHU", "번호," +
                   "급기풍량,배기풍량,급기정압,배기정압,급기팬동력,배기팬동력,모터제어",
               "'" + Value[0] + "','"
                + Value[23] + "','" + Value[24] + "','" + Value[25] + "','"
                + Value[26] + "','" + Value[27] + "','" + Value[28] + "','"
                + Value[29]
                + "'", "번호");
            }
            Program.DB.saveProject();
            MessageBox.Show("저장되었습니다.");

        }
        private void Load_AHU()
        {
            AHU_dataGridView.Rows.Clear();
            string[][] Value = Program.DB.getValue(DB.type.ProjDB, "User_AHU", "번호,명칭,설치유형,공조방식,열회수유형,온도교환효율_냉방,온도교환효율_난방,전열교환효율_냉방,전열교환효율_난방,습도교환효율_냉방,습도교환효율_난방,냉각코일출력,냉각코일_입구_건구온도,냉각코일_입구_습구온도,냉각코일_출구_건구온도,냉각코일_출구_습구온도,난방코일출력,난방코일_입구온도,난방코일_출구온도,가습기유형,가습기제어유형,가습기습도수준,가습기용량,급기풍량,배기풍량,급기정압,배기정압,급기팬동력,배기팬동력,모터제어", "");
            if (Value.Length > 0)
            {
                for (int n = 0; n < Value.Length; n++)
                {
                    int nRow = AHU_dataGridView.Rows.Add();
                    DataGridViewComboBoxCell 열회수기유형Combo = new DataGridViewComboBoxCell();
                    열회수기유형Combo.Items.Add("없음");
                    열회수기유형Combo.Items.Add("판형");
                    열회수기유형Combo.Items.Add("일반회전형");
                    열회수기유형Combo.Items.Add("흡수식회전형");
                    열회수기유형Combo.Items.Add("흡착식회전형");
                    AHU_dataGridView.Rows[nRow].Cells[5] = 열회수기유형Combo;

                    DataGridViewComboBoxCell 가습기유형Combo = new DataGridViewComboBoxCell();
                    가습기유형Combo.Items.Add("없음");
                    가습기유형Combo.Items.Add("접촉형");
                    가습기유형Combo.Items.Add("회전분사형");
                    가습기유형Combo.Items.Add("고압분사형");
                    가습기유형Combo.Items.Add("스팀형");
                    AHU_dataGridView.Rows[nRow].Cells[20] = 가습기유형Combo;

                    DataGridViewComboBoxCell 가습기제어유형Combo = new DataGridViewComboBoxCell();
                    가습기제어유형Combo.Items.Add("on/off제어");
                    가습기제어유형Combo.Items.Add("인버터제어");
                    AHU_dataGridView.Rows[nRow].Cells[21] = 가습기제어유형Combo;

                    DataGridViewComboBoxCell 습도수준Combo = new DataGridViewComboBoxCell();
                    습도수준Combo.Items.Add("항온항습");
                    습도수준Combo.Items.Add("습도고려");
                    습도수준Combo.Items.Add("고려안함");
                    AHU_dataGridView.Rows[nRow].Cells[22] = 습도수준Combo;

                    DataGridViewComboBoxCell 송풍기모터제어Combo = new DataGridViewComboBoxCell();
                    송풍기모터제어Combo.Items.Add("on/off제어");
                    송풍기모터제어Combo.Items.Add("2단제어");
                    송풍기모터제어Combo.Items.Add("3단제어");
                    송풍기모터제어Combo.Items.Add("인버터제어");
                    AHU_dataGridView.Rows[nRow].Cells[30] = 송풍기모터제어Combo;

                    for (int i = 0; i < 30; i++)
                    { AHU_dataGridView.Rows[nRow].Cells[i + 1].Value = Value[n][i]; }

                }
            }
        }
        #endregion
        ///////////////////////////////////////////////////열회수환기장치/////////////////////////////////////////////////////////////////
        #region 16.열회수환기장치
        public void Create_HRV_Table()
        {
            new StackedHeaderDecorator(HRV_dataGridView, DataGridViewAutoSizeColumnsMode.Fill, HRV_dataGridView_RowHandle);
            HRV_dataGridView.Columns.Clear();
            HRV_checkBoxColumn.HeaderText = "선택";
            HRV_checkBoxColumn.Name = "check";
            HRV_dataGridView.Columns.Add(HRV_checkBoxColumn);

            HRV_dataGridView.Columns.Add("A1", "번호");
            HRV_dataGridView.Columns.Add("A2", "명칭");

            DataGridViewComboBoxColumn 설치유형Combo = new DataGridViewComboBoxColumn();
            설치유형Combo.HeaderText = "설치";
            설치유형Combo.Items.AddRange("기존", "신규", "철거후신규");
            HRV_dataGridView.Columns.Add(설치유형Combo);

            HRV_dataGridView.Columns.Add("A4", "열회수기.유형");
            HRV_dataGridView.Columns.Add("A5", "회수 효율.온도.냉방.[%]");
            HRV_dataGridView.Columns.Add("A6", "회수 효율.온도.난방.[%]");
            HRV_dataGridView.Columns.Add("A7", "회수 효율.유효전열.냉방.[%]");
            HRV_dataGridView.Columns.Add("A8", "회수 효율.유효전열.난방.[%]");
            HRV_dataGridView.Columns.Add("A9", "회수 효율.절대습도.냉방.[%]");
            HRV_dataGridView.Columns.Add("A10", "회수 효율.절대습도.난방.[%]");
            HRV_dataGridView.Columns.Add("A11", "팬.풍량.[CMH]");
            HRV_dataGridView.Columns.Add("A12", "팬.정압.[Pa]");
            HRV_dataGridView.Columns.Add("A13", "팬.모터제어");
            HRV_dataGridView.Columns.Add("A14", "소비전력.[W]");
            HRV_dataGridView.Columns[0].Width = 40;
        }
        private bool HRV_dataGridView_RowHandle(DataGridViewCell cell, int column, int row)
        {
            if (column == 9)
            {
                cell.Style.BackColor = Color.FromArgb(255, 255, 255);
                return true;
            }
            if (column == 10)
            {
                cell.Style.BackColor = Color.FromArgb(255, 255, 255);
                return true;
            }
            else return false;
        }
        private void UserHRV_Add_button_Click(global::System.Object sender, global::System.EventArgs e)
        {
            int nRow = HRV_dataGridView.Rows.Add();
            Load_HRV_Num();

            DataGridViewComboBoxCell 열회수기유형Combo = new DataGridViewComboBoxCell();
            열회수기유형Combo.Items.Add("판형");
            열회수기유형Combo.Items.Add("일반회전형");
            열회수기유형Combo.Items.Add("흡수식회전형");
            열회수기유형Combo.Items.Add("흡착식회전형");
            HRV_dataGridView.Rows[nRow].Cells[4] = 열회수기유형Combo;

            DataGridViewComboBoxCell 모터제어Combo = new DataGridViewComboBoxCell();
            모터제어Combo.Items.Add("on/off제어");
            모터제어Combo.Items.Add("2단제어");
            모터제어Combo.Items.Add("3단제어");
            모터제어Combo.Items.Add("인버터제어");
            HRV_dataGridView.Rows[nRow].Cells[13] = 모터제어Combo;
        }
        private void Load_HRV_Num()
        {
            for (int k = 0; k < HRV_dataGridView.RowCount; k++)
            {
                if (k + 1 < 10)
                { HRV_dataGridView.Rows[k].Cells[1].Value = "HRV0" + (k + 1).ToString(); }
                else { HRV_dataGridView.Rows[k].Cells[1].Value = "HRV" + (k + 1).ToString(); }
            }
        }

        private void HRV_Remove_button_Click(global::System.Object sender, global::System.EventArgs e)
        {
            HRV_dataGridView.Rows.Remove(HRV_dataGridView.Rows[HRV_SelectRow]);
            Load_HRV_Num();
        }

        private void HRV_Copy_button_Click(global::System.Object sender, global::System.EventArgs e)
        {
            int nRow = HRV_dataGridView.Rows.Add();
            Load_HRV_Num();

            DataGridViewComboBoxCell 열회수기유형Combo = new DataGridViewComboBoxCell();
            열회수기유형Combo.Items.Add("없음");
            열회수기유형Combo.Items.Add("판형");
            열회수기유형Combo.Items.Add("일반회전형");
            열회수기유형Combo.Items.Add("흡수식회전형");
            열회수기유형Combo.Items.Add("흡착식회전형");
            HRV_dataGridView.Rows[nRow].Cells[4] = 열회수기유형Combo;

            DataGridViewComboBoxCell 모터제어Combo = new DataGridViewComboBoxCell();
            모터제어Combo.Items.Add("on/off제어");
            모터제어Combo.Items.Add("2단제어");
            모터제어Combo.Items.Add("3단제어");
            모터제어Combo.Items.Add("인버터제어");
            HRV_dataGridView.Rows[nRow].Cells[13] = 모터제어Combo;

            for (int k = 2; k < 15; k++)
            {
                if (HRV_dataGridView.Rows[HRV_SelectRow].Cells[k].Value != null)
                {
                    HRV_dataGridView.Rows[nRow].Cells[k].Value = HRV_dataGridView.Rows[HRV_SelectRow].Cells[k].Value;
                }
            }
            if (HRV_dataGridView.Rows[HRV_SelectRow].Cells[2].Value != null)
            {
                HRV_dataGridView.Rows[nRow].Cells[2].Value = HRV_dataGridView.Rows[HRV_SelectRow].Cells[2].Value.ToString() + "_복사";
            }
        }

        private void HRV_Save_button_Click(global::System.Object sender, global::System.EventArgs e)
        {
            Program.DB.deleteValue(DB.type.ProjDB, "User_HRV", "");

            for (int k = 0; k < HRV_dataGridView.RowCount; k++)
            {
                String[] Value = new String[14];
                for (int i = 1; i < 15; i++)
                {
                    if (HRV_dataGridView.Rows[k].Cells[i].Value != null)
                    {
                        double parsedValue;
                        if (double.TryParse(HRV_dataGridView.Rows[k].Cells[i].Value.ToString(), out parsedValue))
                        {
                            Value[i - 1] = parsedValue.ToString();
                        }
                        else
                        {
                            Value[i - 1] = HRV_dataGridView.Rows[k].Cells[i].Value.ToString();
                        }
                    }
                    else { Value[i - 1] = ""; }
                }
                Program.DB.setValue(DB.type.ProjDB, "User_HRV", "번호,프로젝트유형," +
                    "명칭,설치유형",
                "'" + Value[0] + "','" + 프로젝트유형[0][0] + "','"
                 + Value[1] + "','" + Value[2]
                 + "'", "번호");
                Program.DB.setValue(DB.type.ProjDB, "User_HRV", "번호," +
                    "열회수유형,온도교환효율_냉방,온도교환효율_난방,전열교환효율_냉방,전열교환효율_난방,습도교환효율_냉방,습도교환효율_난방",
                "'" + Value[0] + "','"
                 + Value[3] + "','" + Value[4] + "','" + Value[5] + "','"
                 + Value[6] + "','" + Value[7] + "','" + Value[8] + "','"
                 + Value[9]
                 + "'", "번호");
                Program.DB.setValue(DB.type.ProjDB, "User_HRV", "번호," +
                   "팬풍량,팬정압,모터제어,팬동력",
               "'" + Value[0] + "','"
                + Value[10] + "','" + Value[11] + "','" + Value[12] + "','"
                + Value[13]
                + "'", "번호");
            }
            Program.DB.saveProject();
            MessageBox.Show("저장되었습니다.");
        }

        private void HRV_dataGridView_CellContentClick(global::System.Object sender, global::System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                HRV_dataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
                HRV_SelectRow = e.RowIndex;
            }
        }

        private void HRV_dataGridView_CellValueChanged(global::System.Object sender, global::System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            double temp_eta, all_eta, Humidity_eta;
            if (e.RowIndex >= 0)
            {
                if (e.ColumnIndex == 5 || e.ColumnIndex == 7)
                {
                    if (HRV_dataGridView.Rows[e.RowIndex].Cells[5].Value != null && HRV_dataGridView.Rows[e.RowIndex].Cells[7].Value != null)
                    {
                        if (HRV_dataGridView.Rows[e.RowIndex].Cells[5].Value.ToString() != "" && HRV_dataGridView.Rows[e.RowIndex].Cells[7].Value.ToString() != "")
                        {
                            temp_eta = Program.UTIL.dataGridView_doubleComa(HRV_dataGridView, e.RowIndex, 5, 0);
                            all_eta = Program.UTIL.dataGridView_doubleComa(HRV_dataGridView, e.RowIndex, 7, 0);
                            Humidity_eta = Calc_HumidityEta_Cooling(temp_eta, all_eta);
                            HRV_dataGridView.Rows[e.RowIndex].Cells[9].Value = Humidity_eta.ToString("0.0");
                        }
                    }
                }
                if (e.ColumnIndex == 6 || e.ColumnIndex == 8)
                {
                    if (HRV_dataGridView.Rows[e.RowIndex].Cells[6].Value != null && HRV_dataGridView.Rows[e.RowIndex].Cells[8].Value != null)
                    {
                        if (HRV_dataGridView.Rows[e.RowIndex].Cells[6].Value.ToString() != "" && HRV_dataGridView.Rows[e.RowIndex].Cells[8].Value.ToString() != "")
                        {
                            temp_eta = Program.UTIL.dataGridView_doubleComa(HRV_dataGridView, e.RowIndex, 6, 0);
                            all_eta = Program.UTIL.dataGridView_doubleComa(HRV_dataGridView, e.RowIndex, 8, 0);
                            Humidity_eta = Calc_HumidityEta_Heating(temp_eta, all_eta);
                            HRV_dataGridView.Rows[e.RowIndex].Cells[10].Value = Humidity_eta.ToString("0.0");
                        }
                    }
                }
            }
        }

        private void Load_HRV()
        {
            HRV_dataGridView.Rows.Clear();
            string[][] Value = Program.DB.getValue(DB.type.ProjDB, "User_HRV", "번호,명칭,설치유형,열회수유형,온도교환효율_냉방,온도교환효율_난방,전열교환효율_냉방,전열교환효율_난방,습도교환효율_냉방,습도교환효율_난방,팬풍량,팬정압,모터제어,팬동력", "");
            if (Value.Length > 0)
            {
                for (int n = 0; n < Value.Length; n++)
                {
                    int nRow = HRV_dataGridView.Rows.Add();
                    DataGridViewComboBoxCell 열회수기유형Combo = new DataGridViewComboBoxCell();
                    열회수기유형Combo.Items.Add("없음");
                    열회수기유형Combo.Items.Add("판형");
                    열회수기유형Combo.Items.Add("일반회전형");
                    열회수기유형Combo.Items.Add("흡수식회전형");
                    열회수기유형Combo.Items.Add("흡착식회전형");
                    HRV_dataGridView.Rows[nRow].Cells[4] = 열회수기유형Combo;

                    DataGridViewComboBoxCell 모터제어Combo = new DataGridViewComboBoxCell();
                    모터제어Combo.Items.Add("on/off제어");
                    모터제어Combo.Items.Add("2단제어");
                    모터제어Combo.Items.Add("3단제어");
                    모터제어Combo.Items.Add("인버터제어");
                    HRV_dataGridView.Rows[nRow].Cells[13] = 모터제어Combo;

                    for (int i = 0; i < 14; i++)
                    { HRV_dataGridView.Rows[nRow].Cells[i + 1].Value = Value[n][i]; }

                }
            }
        }
        #endregion
        ///////////////////////////////////////////////////냉각탑//////////////////////////////////////////////////////////////////////////
        #region 15.냉각탑
        public void Create_CoolingTop_Table()
        {
            CoolingTop_dataGridView.Rows.Clear();
            CoolingTop_dataGridView.Columns.Clear();

            new StackedHeaderDecorator(CoolingTop_dataGridView, DataGridViewAutoSizeColumnsMode.Fill);

            List<string> Item = new List<string>();
            Item.Clear();
            string[][] var = Program.DB.getValue_SameCheck(DB.type.BaseDB_Cooling, "냉방설비이미지", "설비유형", "항목유형='C열원설비'");
            for (int k = 0; k < var.Length; k++)
            {
                Item.Add(var[k][0]);
            }
            CoolingTop_checkBoxColumn.HeaderText = "선택";
            CoolingTop_checkBoxColumn.Name = "check";
            CoolingTop_dataGridView.Columns.Add(CoolingTop_checkBoxColumn);

            CoolingTop_dataGridView.Columns.Add("A1", "번호");
            CoolingTop_dataGridView.Columns.Add("A2", "DB유형");
            CoolingTop_dataGridView.Columns.Add("A3", "명칭");

            DataGridViewComboBoxColumn TypecomboBox = new DataGridViewComboBoxColumn();
            TypecomboBox.HeaderText = "형식";
            TypecomboBox.Name = "control";
            for (int i = 0; i < Item.Count; i++)  //추가
            {
                TypecomboBox.Items.Add(Item[i]);
            }
            CoolingTop_dataGridView.Columns.Add(TypecomboBox);

            CoolingTop_dataGridView.Columns.Add("A5", "성능.냉각용량.[kW]");
            //버튼 추가
            CoolingTop_dataGridView.Columns.Add("A6", "");
            CoolingTop_dataGridView.Columns.Add("A7", "성능.냉각수량.[CMH]");

            CoolingTop_dataGridView.Columns.Add("A8", "온도.입구.[℃]");
            CoolingTop_dataGridView.Columns.Add("A9", "온도.출구.[℃]");
            CoolingTop_dataGridView.Columns.Add("A10", "대기전력.[W]");
            CoolingTop_dataGridView.Columns.Add("A11", "소비전력.[kW]");



            DataGridViewComboBoxColumn CtrlTypecomboBox = new DataGridViewComboBoxColumn();
            CtrlTypecomboBox.HeaderText = "제어유형";
            CtrlTypecomboBox.Items.AddRange(new string[] { "제어없음", "항온공급", "가변온도공급" });
            CoolingTop_dataGridView.Columns.Add(CtrlTypecomboBox);


            DataGridViewComboBoxColumn FanTypecomboBox = new DataGridViewComboBoxColumn();
            FanTypecomboBox.HeaderText = "팬유형";
            FanTypecomboBox.Items.AddRange(new string[] { "축류형", "원심형" });
            CoolingTop_dataGridView.Columns.Add(FanTypecomboBox);

            CoolingTop_dataGridView.Columns.Add("A14", "대수.[EA]");
            CoolingTop_dataGridView.Columns.Add("A15", "전력소비계수.[W/W]");


            DataGridViewComboBoxColumn 설치유형Combo = new DataGridViewComboBoxColumn();
            설치유형Combo.HeaderText = "설치";
            설치유형Combo.Items.AddRange("기존", "신규", "철거후신규");
            CoolingTop_dataGridView.Columns.Add(설치유형Combo);

            CoolingTop_dataGridView.Columns[0].Width = 40;
            CoolingTop_dataGridView.Columns[1].Width = 50;
            CoolingTop_dataGridView.Columns[2].Width = 50;
            CoolingTop_dataGridView.Columns[3].Width = 60;
            CoolingTop_dataGridView.Columns[4].Width = 90;
            CoolingTop_dataGridView.Columns[5].Width = 80;
            CoolingTop_dataGridView.Columns[6].Width = 30;
            CoolingTop_dataGridView.Columns[7].Width = 90;
            CoolingTop_dataGridView.Columns[8].Width = 50;
            CoolingTop_dataGridView.Columns[9].Width = 50;


            CoolingTop_dataGridView.Columns[1].ReadOnly = true;
            CoolingTop_dataGridView.Columns[2].ReadOnly = true;
            CoolingTop_dataGridView.Columns[15].ReadOnly = true;
        }
        private void CoolerTop_Add_button_Click(object sender, EventArgs e)
        {
            int nRow = CoolingTop_dataGridView.Rows.Add();
            CoolingTop_dataGridView.Rows[nRow].Cells[2].Value = "도면";
            Load_CoolingTop_Num();
            DataGridViewButtonCell CT_ButtonCell = new DataGridViewButtonCell();
            CoolingTop_dataGridView.Rows[nRow].Cells[6] = CT_ButtonCell;
            CT_ButtonCell.Value = "!";
            CoolingTop_dataGridView.Rows[nRow].Cells[6].Style.BackColor = Color.White;
        }



        private void CoolingTop_dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                CoolingTop_dataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
                CoolingTop_SelectRow = e.RowIndex;
                if (e.ColumnIndex == 6)
                {
                    if (CoolingTop_dataGridView.Rows[e.RowIndex].Cells[4].Value == null || CoolingTop_dataGridView.Rows[e.RowIndex].Cells[4].Value == "")
                    {
                        MessageBox.Show("형식을 먼저 선택해주세요.");
                    }
                    else
                    {
                        string Typ = CoolingTop_dataGridView.Rows[e.RowIndex].Cells[4].Value.ToString();
                        double CTPower, CTFluid, InTemp, OutTemp, FanPower, FanConsume;
                        CTopCal CT = new CTopCal(Typ);
                        DialogResult result = CT.ShowDialog();
                        if (result == DialogResult.OK)
                        {
                            CTPower = CT.CTPower;
                            CTFluid = CT.CTFluid;
                            InTemp = CT.InTemp;
                            OutTemp = CT.OutTemp;
                            FanPower = CT.FanPower;
                            FanConsume = CT.FanConsum;
                            CoolingTop_dataGridView.Rows[e.RowIndex].Cells[5].Value = String.Format("{0:F1}", CTPower);
                            CoolingTop_dataGridView.Rows[e.RowIndex].Cells[7].Value = String.Format("{0:F1}", CTFluid);
                            CoolingTop_dataGridView.Rows[e.RowIndex].Cells[8].Value = String.Format("{0:F1}", InTemp);
                            CoolingTop_dataGridView.Rows[e.RowIndex].Cells[9].Value = String.Format("{0:F1}", OutTemp);
                            CoolingTop_dataGridView.Rows[e.RowIndex].Cells[10].Value = "10";
                            CoolingTop_dataGridView.Rows[e.RowIndex].Cells[11].Value = String.Format("{0:F1}", FanPower);
                            CoolingTop_dataGridView.Rows[e.RowIndex].Cells[13].Value = CT.Fan;
                            CoolingTop_dataGridView.Rows[e.RowIndex].Cells[15].Value = String.Format("{0:F3}", FanConsume);
                        }
                    }
                }
            }
        }
        private void CoolerTop_Remove_button_Click(object sender, EventArgs e)
        {
            int SelectRow;
            for (int i = 0; i < CoolingTop_dataGridView.Rows.Count; i++)
            {
                if (Convert.ToBoolean(CoolingTop_dataGridView.Rows[i].Cells[0].Value))
                {
                    SelectRow = i;
                    CoolingTop_dataGridView.Rows.Remove(CoolingTop_dataGridView.Rows[SelectRow]);
                }
            }
            Load_CoolingTop_Num();
        }
        private void CoolerTop_Copy_button_Click(object sender, EventArgs e)
        {
            for (int j = 0; j < CoolingTop_dataGridView.RowCount; j++)
            {
                if (Convert.ToBoolean(CoolingTop_dataGridView.Rows[j].Cells[0].Value))
                {
                    int nRow = CoolingTop_dataGridView.Rows.Add();
                    for (int k = 2; k < 16; k++)
                    {
                        CoolingTop_dataGridView.Rows[nRow].Cells[k].Value = CoolingTop_dataGridView.Rows[j].Cells[k].Value;
                    }
                    Load_CoolingTop_Num();
                }
            }
        }
        private void Load_CoolingTop_Num()
        {
            for (int k = 0; k < CoolingTop_dataGridView.RowCount; k++)
            {
                if (k + 1 < 10)
                { CoolingTop_dataGridView.Rows[k].Cells[1].Value = "UCT0" + (k + 1).ToString(); }
                else { CoolingTop_dataGridView.Rows[k].Cells[1].Value = "UCT" + (k + 1).ToString(); }
            }
        }
        private void CoolingTop_Save_button_Click_1(object sender, EventArgs e)
        {
            Program.DB.deleteValue(DB.type.ProjDB, "User_CoolingTop", "");

            for (int k = 0; k < CoolingTop_dataGridView.RowCount; k++)
            {
                string[] Value = new string[16];

                for (int i = 1; i < 17; i++)
                {
                    if (CoolingTop_dataGridView.Rows[k].Cells[i].Value != null)
                    {
                        double parsedValue;
                        if (double.TryParse(CoolingTop_dataGridView.Rows[k].Cells[i].Value.ToString(), out parsedValue))
                        {
                            Value[i - 1] = parsedValue.ToString();
                        }
                        else
                        {
                            Value[i - 1] = CoolingTop_dataGridView.Rows[k].Cells[i].Value.ToString();
                        }
                    }
                    else { Value[i - 1] = ""; }
                }
                //버튼 추가수정함
                Program.DB.setValue(DB.type.ProjDB, "User_CoolingTop", "번호,DB유형,명칭,형식,냉각능력,냉각수량,입구온도,출구온도,대기전력,소비전력,제어유형,팬유형,대수,냉방전력소비계수,설치",
                "'" + Value[0] + "','"
                 + Value[1] + "','" + Value[2] + "','" + Value[3] + "','" + Value[4] + "','" + Value[6] + "','"
                 + Value[7] + "','" + Value[8] + "','" + Value[9] + "','" + Value[10] + "','" + Value[11] + "','"
                 + Value[12] + "','" + Value[13] + "','" + Value[14] + "','" + Value[15] + "'", "번호");
            }
            Program.DB.saveProject();
            MessageBox.Show("저장되었습니다.");
        }
        private void CoolingTop_dataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex >= 0)
            {
                try
                {
                    if (e.ColumnIndex == 5)
                    {
                        if (CoolingTop_dataGridView.Rows[e.RowIndex].Cells[11].Value != null && CoolingTop_dataGridView.Rows[e.RowIndex].Cells[11].Value.ToString() != "")
                        {
                            if (CoolingTop_dataGridView.Rows[e.RowIndex].Cells[5].ToString() != "-" && CoolingTop_dataGridView.Rows[e.RowIndex].Cells[11].Value.ToString() != "-")
                            {
                                CoolingTop_dataGridView.Rows[e.RowIndex].Cells[15].Value = (Program.UTIL.dataGridView_doubleComa(CoolingTop_dataGridView, e.RowIndex, 5, 1) / Program.UTIL.dataGridView_doubleComa(CoolingTop_dataGridView, e.RowIndex, 11, 1)).ToString("0.00");
                            }
                        }
                    }
                    if (e.ColumnIndex == 11)
                    {
                        if (CoolingTop_dataGridView.Rows[e.RowIndex].Cells[5].Value != null && CoolingTop_dataGridView.Rows[e.RowIndex].Cells[5].Value.ToString() != "")
                        {
                            if (CoolingTop_dataGridView.Rows[e.RowIndex].Cells[5].ToString() != "-" && CoolingTop_dataGridView.Rows[e.RowIndex].Cells[11].Value.ToString() != "-")
                            {
                                CoolingTop_dataGridView.Rows[e.RowIndex].Cells[15].Value = (Program.UTIL.dataGridView_doubleComa(CoolingTop_dataGridView, e.RowIndex, 5, 1) / Program.UTIL.dataGridView_doubleComa(CoolingTop_dataGridView, e.RowIndex, 11, 1)).ToString("0.00");
                            }
                        }
                    }
                }
                catch { }
            }

        }
        private void Load_CoolingTop()
        {
            CoolingTop_dataGridView.Rows.Clear();
            string[][] User_Value = Program.DB.getValue(DB.type.ProjDB, "User_CoolingTop", "번호,DB유형,명칭,형식,냉각능력,냉각수량,입구온도,출구온도,대기전력,소비전력,제어유형,팬유형,대수,냉방전력소비계수,설치", "");
            if (User_Value.Length > 0)
            {
                for (int n = 0; n < User_Value.Length; n++)
                {
                    CoolingTop_dataGridView.Rows.Add();
                    int nRow = CoolingTop_dataGridView.Rows.Count - 1;
                    CoolingTop_dataGridView.Rows[nRow].Cells[1].Value = User_Value[n][0];//번호
                    CoolingTop_dataGridView.Rows[nRow].Cells[2].Value = User_Value[n][1];//DB유형
                    CoolingTop_dataGridView.Rows[nRow].Cells[3].Value = User_Value[n][2];//명칭
                    CoolingTop_dataGridView.Rows[nRow].Cells[4].Value = User_Value[n][3];//형식
                    CoolingTop_dataGridView.Rows[nRow].Cells[5].Value = User_Value[n][4];//냉각능력

                    CoolingTop_dataGridView.Rows[nRow].Cells[7].Value = User_Value[n][5];
                    CoolingTop_dataGridView.Rows[nRow].Cells[8].Value = User_Value[n][6];
                    CoolingTop_dataGridView.Rows[nRow].Cells[9].Value = User_Value[n][7];
                    CoolingTop_dataGridView.Rows[nRow].Cells[10].Value = User_Value[n][8];
                    CoolingTop_dataGridView.Rows[nRow].Cells[11].Value = User_Value[n][9];
                    CoolingTop_dataGridView.Rows[nRow].Cells[12].Value = User_Value[n][10];
                    CoolingTop_dataGridView.Rows[nRow].Cells[13].Value = User_Value[n][11];
                    CoolingTop_dataGridView.Rows[nRow].Cells[14].Value = User_Value[n][12];
                    CoolingTop_dataGridView.Rows[nRow].Cells[15].Value = User_Value[n][13];
                    CoolingTop_dataGridView.Rows[nRow].Cells[16].Value = User_Value[n][14];
                }
            }
        }
        #endregion
        //////////////////////////////////////////////////난방급탕 히트펌프/////////////////////////////////////////////////////////////////
        #region 7. 난방급탕히트펌프
        public void Create_DHWHP_Table()
        {
            new StackedHeaderDecorator(DHWHP_dataGridView, DataGridViewAutoSizeColumnsMode.Fill);
            DHWHP_dataGridView.Columns.Clear();
            DHWHP_checkBoxColumn.HeaderText = "선택";
            DHWHP_checkBoxColumn.Name = "check";
            DHWHP_dataGridView.Columns.Add(DHWHP_checkBoxColumn);

            DHWHP_dataGridView.Columns.Add("A1", "번호");
            DHWHP_dataGridView.Columns.Add("A2", "DB유형");
            DHWHP_dataGridView.Columns.Add("A3", "명칭");

            DataGridViewComboBoxColumn 난방급탕 = new DataGridViewComboBoxColumn();
            난방급탕.HeaderText = "난방/급탕";
            난방급탕.Items.AddRange("급탕", "난방+급탕");
            DHWHP_dataGridView.Columns.Add(난방급탕);

            DHWHP_dataGridView.Columns.Add("A5", "급탕정격.용량.[kW]");
            DHWHP_dataGridView.Columns.Add("A6", "급탕정격.COP.[-]");
            DHWHP_dataGridView.Columns.Add("A7", "급탕정격.소비전력.[kW]");
            DHWHP_dataGridView.Columns.Add("A8", "난방정격.용량.[kW]");
            DHWHP_dataGridView.Columns.Add("A9", "난방정격.COP.[-]");
            DHWHP_dataGridView.Columns.Add("A10", "난방정격.소비전력.[kW]");
            DHWHP_dataGridView.Columns.Add("A11", "한랭지.용량.[kW]");
            DHWHP_dataGridView.Columns.Add("A12", "한랭지.COP.[-]");
            DHWHP_dataGridView.Columns.Add("A13", "한랭지.소비전력.[kW]");
            DHWHP_dataGridView.Columns.Add("A14", "대기전력.[W]");
            DHWHP_dataGridView.Columns.Add("A15", "대수.[EA]");

            DataGridViewComboBoxColumn 설치유형Combo = new DataGridViewComboBoxColumn();
            설치유형Combo.HeaderText = "설치";
            설치유형Combo.Items.AddRange("기존", "신규", "철거후신규");
            DHWHP_dataGridView.Columns.Add(설치유형Combo);

            DHWHP_dataGridView.Columns[0].Width = 40;
            DHWHP_dataGridView.Columns[1].Width = 60;
            DHWHP_dataGridView.Columns[2].Width = 60;
            DHWHP_dataGridView.Columns[3].Width = 60;
            DHWHP_dataGridView.Columns[4].Width = 70;

        }

        private void UserDHWHP_Add_button_Click(object sender, EventArgs e)
        {
            int nRow = DHWHP_dataGridView.Rows.Add();
            Load_DHWHP_Num();

            DHWHP_dataGridView.Rows[nRow].Cells[2].Value = "도면";
        }


        private void DHWHP_dataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                try
                {
                    if (e.ColumnIndex == 5)
                    {
                        if (DHWHP_dataGridView.Rows[e.RowIndex].Cells[6].Value != null && DHWHP_dataGridView.Rows[e.RowIndex].Cells[6].Value.ToString() != "")
                        {
                            if (DHWHP_dataGridView.Rows[e.RowIndex].Cells[5].Value.ToString() != "-" && DHWHP_dataGridView.Rows[e.RowIndex].Cells[6].Value.ToString() != "-")
                            {
                                DHWHP_dataGridView.Rows[e.RowIndex].Cells[7].Value = (Program.UTIL.dataGridView_doubleComa(DHWHP_dataGridView, e.RowIndex, 5, 1) / Program.UTIL.dataGridView_doubleComa(DHWHP_dataGridView, e.RowIndex, 6, 1)).ToString("0.00");
                            }
                        }
                    }
                    if (e.ColumnIndex == 6)
                    {
                        if (DHWHP_dataGridView.Rows[e.RowIndex].Cells[5].Value != null && DHWHP_dataGridView.Rows[e.RowIndex].Cells[5].Value.ToString() != "")
                        {
                            if (DHWHP_dataGridView.Rows[e.RowIndex].Cells[5].Value.ToString() != "-" && DHWHP_dataGridView.Rows[e.RowIndex].Cells[6].Value.ToString() != "-")
                            {
                                DHWHP_dataGridView.Rows[e.RowIndex].Cells[7].Value = (Program.UTIL.dataGridView_doubleComa(DHWHP_dataGridView, e.RowIndex, 5, 1) / Program.UTIL.dataGridView_doubleComa(DHWHP_dataGridView, e.RowIndex, 6, 1)).ToString("0.00");
                            }
                        }
                    }
                    if (e.ColumnIndex == 8)
                    {
                        if (DHWHP_dataGridView.Rows[e.RowIndex].Cells[9].Value != null && DHWHP_dataGridView.Rows[e.RowIndex].Cells[9].Value.ToString() != "")
                        {
                            if (DHWHP_dataGridView.Rows[e.RowIndex].Cells[8].Value.ToString() != "-" && DHWHP_dataGridView.Rows[e.RowIndex].Cells[9].Value.ToString() != "-")
                            {
                                DHWHP_dataGridView.Rows[e.RowIndex].Cells[10].Value = (Program.UTIL.dataGridView_doubleComa(DHWHP_dataGridView, e.RowIndex, 8, 1) / Program.UTIL.dataGridView_doubleComa(DHWHP_dataGridView, e.RowIndex, 9, 1)).ToString("0.00");
                            }
                        }
                    }
                    if (e.ColumnIndex == 9)
                    {
                        if (DHWHP_dataGridView.Rows[e.RowIndex].Cells[8].Value != null && DHWHP_dataGridView.Rows[e.RowIndex].Cells[8].Value.ToString() != "")
                        {
                            if (DHWHP_dataGridView.Rows[e.RowIndex].Cells[8].Value.ToString() != "-" && DHWHP_dataGridView.Rows[e.RowIndex].Cells[9].Value.ToString() != "-")
                            {
                                DHWHP_dataGridView.Rows[e.RowIndex].Cells[10].Value = (Program.UTIL.dataGridView_doubleComa(DHWHP_dataGridView, e.RowIndex, 8, 1) / Program.UTIL.dataGridView_doubleComa(DHWHP_dataGridView, e.RowIndex, 9, 1)).ToString("0.00");
                            }
                        }
                    }
                    if (e.ColumnIndex == 11)
                    {
                        if (DHWHP_dataGridView.Rows[e.RowIndex].Cells[12].Value != null && DHWHP_dataGridView.Rows[e.RowIndex].Cells[12].Value.ToString() != "")
                        {
                            if (DHWHP_dataGridView.Rows[e.RowIndex].Cells[11].Value.ToString() != "-" && DHWHP_dataGridView.Rows[e.RowIndex].Cells[12].Value.ToString() != "-")
                            {
                                DHWHP_dataGridView.Rows[e.RowIndex].Cells[13].Value = (Program.UTIL.dataGridView_doubleComa(DHWHP_dataGridView, e.RowIndex, 11, 1) / Program.UTIL.dataGridView_doubleComa(DHWHP_dataGridView, e.RowIndex, 12, 1)).ToString("0.00");
                            }
                        }
                    }
                    if (e.ColumnIndex == 12)
                    {
                        if (DHWHP_dataGridView.Rows[e.RowIndex].Cells[11].Value != null && DHWHP_dataGridView.Rows[e.RowIndex].Cells[11].Value.ToString() != "")
                        {
                            if (DHWHP_dataGridView.Rows[e.RowIndex].Cells[11].Value.ToString() != "-" && DHWHP_dataGridView.Rows[e.RowIndex].Cells[12].Value.ToString() != "-")
                            {
                                DHWHP_dataGridView.Rows[e.RowIndex].Cells[13].Value = (Program.UTIL.dataGridView_doubleComa(DHWHP_dataGridView, e.RowIndex, 11, 1) / Program.UTIL.dataGridView_doubleComa(DHWHP_dataGridView, e.RowIndex, 12, 1)).ToString("0.00");
                            }
                        }
                    }
                }
                catch { }


                if (DHWHP_dataGridView.Rows[e.RowIndex].Cells[4].Value != null && DHWHP_dataGridView.Rows[e.RowIndex].Cells[4].Value.ToString() == "급탕")
                {
                    DHWHP_dataGridView.Rows[e.RowIndex].Cells[8].Value = "-";
                    DHWHP_dataGridView.Rows[e.RowIndex].Cells[9].Value = "-";
                    DHWHP_dataGridView.Rows[e.RowIndex].Cells[10].Value = "-";
                    DHWHP_dataGridView.Rows[e.RowIndex].Cells[11].Value = "-";
                    DHWHP_dataGridView.Rows[e.RowIndex].Cells[12].Value = "-";
                    DHWHP_dataGridView.Rows[e.RowIndex].Cells[13].Value = "-";
                }

            }
        }


        private void DHWHP_Remove_button_Click(object sender, EventArgs e)
        {
            DHWHP_dataGridView.Rows.Remove(DHWHP_dataGridView.Rows[HP_SelectRow]);
            Load_DHWHP_Num();
        }

        private void DHWHP_Copy_button_Click(object sender, EventArgs e)
        {
            int nRow = DHWHP_dataGridView.Rows.Add();
            Load_DHWHP_Num();

            for (int k = 2; k < 11; k++)
            {
                if (DHWHP_dataGridView.Rows[HP_SelectRow].Cells[k].Value != null)
                {
                    DHWHP_dataGridView.Rows[nRow].Cells[k].Value = DHWHP_dataGridView.Rows[HP_SelectRow].Cells[k].Value;
                }
            }
            if (DHWHP_dataGridView.Rows[HP_SelectRow].Cells[3].Value != null)
            {
                DHWHP_dataGridView.Rows[nRow].Cells[3].Value = DHWHP_dataGridView.Rows[HP_SelectRow].Cells[3].Value.ToString() + "_복사";
            }
        }
        private void DHWHP_dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DHWHP_dataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
                HP_SelectRow = e.RowIndex;

            }
        }

        private void Load_DHWHP_Num()
        {
            for (int k = 0; k < DHWHP_dataGridView.RowCount; k++)
            {
                if (k + 1 < 10)
                { DHWHP_dataGridView.Rows[k].Cells[1].Value = "UHP0" + (k + 1).ToString(); }
                else { DHWHP_dataGridView.Rows[k].Cells[1].Value = "UHP" + (k + 1).ToString(); }
            }
        }

        private void DHWHP_Save_button_Click(object sender, EventArgs e)
        {
            Program.DB.deleteValue(DB.type.ProjDB, "User_DHWHP", "");
            for (int k = 0; k < DHWHP_dataGridView.RowCount; k++)
            {
                String[] Value = new String[16];
                for (int i = 1; i < 17; i++)
                {
                    if (DHWHP_dataGridView.Rows[k].Cells[i].Value != null)
                    {
                        double parsedValue;
                        if (double.TryParse(DHWHP_dataGridView.Rows[k].Cells[i].Value.ToString(), out parsedValue))
                        {
                            Value[i - 1] = parsedValue.ToString();
                        }
                        else
                        {
                            Value[i - 1] = DHWHP_dataGridView.Rows[k].Cells[i].Value.ToString();
                        }
                    }
                    else { Value[i - 1] = ""; }
                }
                Program.DB.setValue(DB.type.ProjDB, "User_DHWHP", "번호,프로젝트유형,DB유형,명칭,난방급탕,급탕정격용량,급탕정격COP,급탕정격소비전력,난방정격용량,난방정격COP,난방정격소비전력,한랭지용량,한랭지COP,한랭지소비전력,대기전력,대수,설치",
                "'" + Value[0] + "','" + 프로젝트유형[0][0] + "','"
                 + Value[1] + "','" + Value[2] + "','" + Value[3] + "','" + Value[4] + "','" + Value[5] + "','" + Value[6] + "','" + Value[7] + "','" + Value[8] + "','" + Value[9] + "','" + Value[10] + "','" + Value[11] + "','" +
                  Value[12] + "','" + Value[13] + "','" + Value[14] + "','" + Value[15] + "'", "번호");
            }
            Program.DB.saveProject();

            MessageBox.Show("저장되었습니다.");
        }


        private void Load_DHWHP()
        {
            DHWHP_dataGridView.Rows.Clear();
            string[][] User_Value = Program.DB.getValue(DB.type.ProjDB, "User_DHWHP", "번호,DB유형,명칭,난방급탕,급탕정격용량,급탕정격COP,급탕정격소비전력,난방정격용량,난방정격COP,난방정격소비전력,한랭지용량,한랭지COP,한랭지소비전력,대기전력,대수,설치", "");
            if (User_Value.Length > 0)
            {
                for (int n = 0; n < User_Value.Length; n++)
                {
                    DHWHP_dataGridView.Rows.Add();

                    DHWHP_dataGridView.Rows[n].Cells[1].Value = User_Value[n][0]; //번호
                    DHWHP_dataGridView.Rows[n].Cells[2].Value = User_Value[n][1];
                    DHWHP_dataGridView.Rows[n].Cells[3].Value = User_Value[n][2];
                    DHWHP_dataGridView.Rows[n].Cells[4].Value = User_Value[n][3];
                    DHWHP_dataGridView.Rows[n].Cells[5].Value = User_Value[n][4];
                    DHWHP_dataGridView.Rows[n].Cells[6].Value = User_Value[n][5];
                    DHWHP_dataGridView.Rows[n].Cells[7].Value = User_Value[n][6];
                    DHWHP_dataGridView.Rows[n].Cells[8].Value = User_Value[n][7];
                    DHWHP_dataGridView.Rows[n].Cells[9].Value = User_Value[n][8];
                    DHWHP_dataGridView.Rows[n].Cells[10].Value = User_Value[n][9];
                    DHWHP_dataGridView.Rows[n].Cells[11].Value = User_Value[n][10];
                    DHWHP_dataGridView.Rows[n].Cells[12].Value = User_Value[n][11];
                    DHWHP_dataGridView.Rows[n].Cells[13].Value = User_Value[n][12];
                    DHWHP_dataGridView.Rows[n].Cells[14].Value = User_Value[n][13];
                    DHWHP_dataGridView.Rows[n].Cells[15].Value = User_Value[n][14];
                    DHWHP_dataGridView.Rows[n].Cells[16].Value = User_Value[n][15];
                }
            }

        }
        #endregion

        //////////////////////////////////////////////////배기팬/////////////////////////////////////////////////////////////////
        #region 18.배기팬
        void Create_Fan_Table()
        {
            new StackedHeaderDecorator(Fan_dataGridView, DataGridViewAutoSizeColumnsMode.Fill);
            Fan_dataGridView.Columns.Clear();
            Fan_checkBoxColumn.HeaderText = "선택";
            Fan_checkBoxColumn.Name = "check";
            Fan_dataGridView.Columns.Add(Fan_checkBoxColumn);

            Fan_dataGridView.Columns.Add("A1", "번호");
            Fan_dataGridView.Columns.Add("A2", "명칭");

            DataGridViewComboBoxColumn 설치유형Combo = new DataGridViewComboBoxColumn();
            설치유형Combo.HeaderText = "설치";
            설치유형Combo.Items.AddRange("기존", "신규", "철거후신규");
            Fan_dataGridView.Columns.Add(설치유형Combo);

            Fan_dataGridView.Columns.Add("A4", "팬.풍량.[CMH]");
            Fan_dataGridView.Columns.Add("A5", "팬.정압.[Pa]");
            Fan_dataGridView.Columns.Add("A6", "팬.모터제어");
            Fan_dataGridView.Columns.Add("A7", "소비전력.[W]");
            Fan_dataGridView.Columns[0].Width = 40;
        }
        private void UserFan_Add_button_Click(object sender, EventArgs e)
        {
            int nRow = Fan_dataGridView.Rows.Add();
            Load_Fan_Num();

            DataGridViewComboBoxCell 모터제어Combo = new DataGridViewComboBoxCell();
            모터제어Combo.Items.Add("on/off제어");
            모터제어Combo.Items.Add("2단제어");
            모터제어Combo.Items.Add("3단제어");
            모터제어Combo.Items.Add("인버터제어");
            Fan_dataGridView.Rows[nRow].Cells[6] = 모터제어Combo;
        }
        private void Fan_Remove_button_Click(object sender, EventArgs e)
        {
            Fan_dataGridView.Rows.Remove(Fan_dataGridView.Rows[Fan_SelectRow]);
            Load_Fan_Num();
        }
        private void Fan_Copy_button_Click(object sender, EventArgs e)
        {
            int nRow = Fan_dataGridView.Rows.Add();
            Load_Fan_Num();

            DataGridViewComboBoxCell 모터제어Combo = new DataGridViewComboBoxCell();
            모터제어Combo.Items.Add("on/off제어");
            모터제어Combo.Items.Add("2단제어");
            모터제어Combo.Items.Add("3단제어");
            모터제어Combo.Items.Add("인버터제어");
            Fan_dataGridView.Rows[nRow].Cells[6] = 모터제어Combo;

            for (int k = 2; k < 8; k++)
            {
                if (Fan_dataGridView.Rows[Fan_SelectRow].Cells[k].Value != null)
                {
                    Fan_dataGridView.Rows[nRow].Cells[k].Value = Fan_dataGridView.Rows[Fan_SelectRow].Cells[k].Value;
                }
            }
            if (Fan_dataGridView.Rows[Fan_SelectRow].Cells[2].Value != null)
            {
                Fan_dataGridView.Rows[nRow].Cells[2].Value = Fan_dataGridView.Rows[Fan_SelectRow].Cells[2].Value.ToString() + "_복사";
            }
        }
        private void Fan_dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                Fan_dataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
                Fan_SelectRow = e.RowIndex;
            }
        }

        private void Load_Fan_Num()
        {
            for (int k = 0; k < Fan_dataGridView.RowCount; k++)
            {
                if (k + 1 < 10)
                { Fan_dataGridView.Rows[k].Cells[1].Value = "FAN0" + (k + 1).ToString(); }
                else { Fan_dataGridView.Rows[k].Cells[1].Value = "FAN" + (k + 1).ToString(); }
            }
        }
        private void Fan_Save_button_Click(object sender, EventArgs e)
        {
            Program.DB.deleteValue(DB.type.ProjDB, "User_Fan", "");

            for (int k = 0; k < Fan_dataGridView.RowCount; k++) //행개수
            {
                string[] Value = new string[8];
                for (int i = 1; i < 8; i++) //열개수
                {
                    if (Fan_dataGridView.Rows[k].Cells[i].Value != null)
                    {
                        double parsedValue;
                        if (double.TryParse(Fan_dataGridView.Rows[k].Cells[i].Value.ToString(), out parsedValue))
                        {
                            Value[i - 1] = parsedValue.ToString();
                        }
                        else
                        {
                            Value[i - 1] = Fan_dataGridView.Rows[k].Cells[i].Value.ToString();
                        }
                    }
                    else { Value[i - 1] = ""; }
                }

                Program.DB.setValue(DB.type.ProjDB, "User_Fan", "번호,프로젝트유형,명칭,설치유형,풍량,정압,모터제어,소비전력", "'" + Value[0] + "','" + 프로젝트유형[0][0] + "','" + Value[1] + "','" + Value[2] + "','" + Value[3] + "','" + Value[4] + "','" + Value[5] + "','" + Value[6] + "'", "번호");
            }
            Program.DB.saveProject();
            MessageBox.Show("저장되었습니다.");
        }
        void Load_Fan()
        {
            Fan_dataGridView.Rows.Clear();
            string[][] Value = Program.DB.getValue(DB.type.ProjDB, "User_Fan", "번호,명칭,설치유형,풍량,정압,모터제어,소비전력", "");
            if (Value.Length > 0)
            {
                for (int n = 0; n < Value.Length; n++)
                {
                    int nRow = Fan_dataGridView.Rows.Add();

                    DataGridViewComboBoxCell 모터제어Combo = new DataGridViewComboBoxCell();
                    모터제어Combo.Items.Add("on/off제어");
                    모터제어Combo.Items.Add("2단제어");
                    모터제어Combo.Items.Add("3단제어");
                    모터제어Combo.Items.Add("인버터제어");
                    Fan_dataGridView.Rows[nRow].Cells[6] = 모터제어Combo;

                    for (int i = 0; i < 7; i++)
                    { Fan_dataGridView.Rows[nRow].Cells[i + 1].Value = Value[n][i]; }

                }
            }
        }
        #endregion

        private void unit_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (unit_comboBox.SelectedIndex == 0)
            {
                unitselect_comboBox.Visible = true;
                unitselect_comboBox.Items.AddRange(new string[] { "kcal", "USRT", "RT", "CRT(냉각톤)" });
            }
            else if (unit_comboBox.SelectedIndex == 1)
            {
                unitselect_comboBox.Visible = true;
                unitselect_comboBox.Text = "LPM";
                input_textBox.Visible = true;

            }
            else
            {
                unitselect_comboBox.Visible = true;
                unitselect_comboBox.Text = " CMM";
                input_textBox.Visible = true;
            }
        }
        private void unitselect_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            input_textBox.Visible = true;
        }

        private void input_textBox_TextChanged_1(object sender, EventArgs e)
        {
            string value = unitselect_comboBox.Text;
            output_text.Text = null;
            double v;
            if (double.TryParse(input_textBox.Text, out v))
            {
                v = Convert.ToDouble(input_textBox.Text);
                output_text.Visible = true;
            }
            else
            {
                MessageBox.Show("input값에 숫자를 입력하세요.");
                return;
            }
            switch (value)
            {
                case "kcal":
                    output_text.Text = Convert.ToString(string.Format("{0:F2} kW", v / 860));
                    break;
                case "USRT":
                    output_text.Text = Convert.ToString(string.Format("{0:F2} kW", v * 3024 / 860));
                    break;
                case "RT":
                    output_text.Text = Convert.ToString(string.Format("{0:F2} kW", v * 3320 / 860));
                    break;
                case "CRT(냉각톤)":
                    output_text.Text = Convert.ToString(string.Format("{0:F2} kW", v * 3900 / 860));
                    break;
                case "LPM":
                    output_text.Text = Convert.ToString(string.Format("{0:F2} CMH", v * 60 / 1000));
                    break;
                case "CMM":
                    output_text.Text = Convert.ToString(string.Format("{0:F2} CMH", v * 60));
                    break;
                default:
                    break;
            }
        }

        private void Qmax_button_Click(object sender, EventArgs e)
        {
            string[][] 결과 = Program.DB.getValue_SameCheck(DB.type.ProjDB, "Zone_HCneed_Result", "Qb_a,Q_max", "");
            if (결과.Length > 0)
            {
                ZoneResult pumpcal_form = new ZoneResult();
                DialogResult result = pumpcal_form.ShowDialog();
                if (result == DialogResult.OK)
                {
                }
            }
            else
            {
                MessageBox.Show("에너지요구량 해석 시뮬레이션을 진행하시고 확인하세요.");
            }

        }

        private void info_Click(object sender, EventArgs e)
        {
            string basePath = Program.gPath + "Manual\\1.contents\\22.EquipmentList\\01 general";

            // 경로가 존재하는지 확인
            if (Directory.Exists(basePath))
            {
                SlideViewer slideViewer = new SlideViewer(basePath);
                slideViewer.Show();
            }
            else
            {
                MessageBox.Show("The folder path does not exist.");
            }
        }

        private void infohp_Click(object sender, EventArgs e)
        {
            string basePath = Program.gPath + "Manual\\1.contents\\22.EquipmentList\\02 HP";

            // 경로가 존재하는지 확인
            if (Directory.Exists(basePath))
            {
                SlideViewer slideViewer = new SlideViewer(basePath);
                slideViewer.Show();
            }
            else
            {
                MessageBox.Show("The folder path does not exist.");
            }
        }

        private void infoboiler_Click(object sender, EventArgs e)
        {
            string basePath = Program.gPath + "Manual\\1.contents\\22.EquipmentList\\03 Boiler";

            // 경로가 존재하는지 확인
            if (Directory.Exists(basePath))
            {
                SlideViewer slideViewer = new SlideViewer(basePath);
                slideViewer.Show();
            }
            else
            {
                MessageBox.Show("The folder path does not exist.");
            }
        }

        private void infoDH_Click(object sender, EventArgs e)
        {
            string basePath = Program.gPath + "Manual\\1.contents\\22.EquipmentList\\04 DH";

            // 경로가 존재하는지 확인
            if (Directory.Exists(basePath))
            {
                SlideViewer slideViewer = new SlideViewer(basePath);
                slideViewer.Show();
            }
            else
            {
                MessageBox.Show("The folder path does not exist.");
            }
        }

        private void infoAS_Click(object sender, EventArgs e)
        {
            string basePath = Program.gPath + "Manual\\1.contents\\22.EquipmentList\\05 AS";

            // 경로가 존재하는지 확인
            if (Directory.Exists(basePath))
            {
                SlideViewer slideViewer = new SlideViewer(basePath);
                slideViewer.Show();
            }
            else
            {
                MessageBox.Show("The folder path does not exist.");
            }
        }

        private void infoAC_Click(object sender, EventArgs e)
        {
            string basePath = Program.gPath + "Manual\\1.contents\\22.EquipmentList\\06 AC";

            // 경로가 존재하는지 확인
            if (Directory.Exists(basePath))
            {
                SlideViewer slideViewer = new SlideViewer(basePath);
                slideViewer.Show();
            }
            else
            {
                MessageBox.Show("The folder path does not exist.");
            }
        }

        private void infoWaterCooler_Click(object sender, EventArgs e)
        {
            string basePath = Program.gPath + "Manual\\1.contents\\22.EquipmentList\\07 WaterCooler";

            // 경로가 존재하는지 확인
            if (Directory.Exists(basePath))
            {
                SlideViewer slideViewer = new SlideViewer(basePath);
                slideViewer.Show();
            }
            else
            {
                MessageBox.Show("The folder path does not exist.");
            }
        }

        private void infoPV_Click(object sender, EventArgs e)
        {
            string basePath = Program.gPath + "Manual\\1.contents\\22.EquipmentList\\08 PV";

            // 경로가 존재하는지 확인
            if (Directory.Exists(basePath))
            {
                SlideViewer slideViewer = new SlideViewer(basePath);
                slideViewer.Show();
            }
            else
            {
                MessageBox.Show("The folder path does not exist.");
            }
        }

        private void infoSolar_Click(object sender, EventArgs e)
        {

            string basePath = Program.gPath + "Manual\\1.contents\\22.EquipmentList\\09 Solar";

            // 경로가 존재하는지 확인
            if (Directory.Exists(basePath))
            {
                SlideViewer slideViewer = new SlideViewer(basePath);
                slideViewer.Show();
            }
            else
            {
                MessageBox.Show("The folder path does not exist.");
            }
        }

        private void infoDHWHP_Click(object sender, EventArgs e)
        {

            string basePath = Program.gPath + "Manual\\1.contents\\22.EquipmentList\\10 DHWHP";

            // 경로가 존재하는지 확인
            if (Directory.Exists(basePath))
            {
                SlideViewer slideViewer = new SlideViewer(basePath);
                slideViewer.Show();
            }
            else
            {
                MessageBox.Show("The folder path does not exist.");
            }
        }

        private void infoGHP_Click(object sender, EventArgs e)
        {
            string basePath = Program.gPath + "Manual\\1.contents\\22.EquipmentList\\11 GHP";

            // 경로가 존재하는지 확인
            if (Directory.Exists(basePath))
            {
                SlideViewer slideViewer = new SlideViewer(basePath);
                slideViewer.Show();
            }
            else
            {
                MessageBox.Show("The folder path does not exist.");
            }
        }

        private void infoGWHP_Click(object sender, EventArgs e)
        {
            string basePath = Program.gPath + "Manual\\1.contents\\22.EquipmentList\\12 GWHP";

            // 경로가 존재하는지 확인
            if (Directory.Exists(basePath))
            {
                SlideViewer slideViewer = new SlideViewer(basePath);
                slideViewer.Show();
            }
            else
            {
                MessageBox.Show("The folder path does not exist.");
            }
        }

        private void infoFC_Click(object sender, EventArgs e)
        {
            string basePath = Program.gPath + "Manual\\1.contents\\22.EquipmentList\\13 FC";

            // 경로가 존재하는지 확인
            if (Directory.Exists(basePath))
            {
                SlideViewer slideViewer = new SlideViewer(basePath);
                slideViewer.Show();
            }
            else
            {
                MessageBox.Show("The folder path does not exist.");
            }
        }

        private void infoWP_Click(object sender, EventArgs e)
        {
            string basePath = Program.gPath + "Manual\\1.contents\\22.EquipmentList\\14 WP";

            // 경로가 존재하는지 확인
            if (Directory.Exists(basePath))
            {
                SlideViewer slideViewer = new SlideViewer(basePath);
                slideViewer.Show();
            }
            else
            {
                MessageBox.Show("The folder path does not exist.");
            }
        }

        private void infoCoolingTop_Click(object sender, EventArgs e)
        {
            string basePath = Program.gPath + "Manual\\1.contents\\22.EquipmentList\\17 CoolingTop";

            // 경로가 존재하는지 확인
            if (Directory.Exists(basePath))
            {
                SlideViewer slideViewer = new SlideViewer(basePath);
                slideViewer.Show();
            }
            else
            {
                MessageBox.Show("The folder path does not exist.");
            }
        }

        private void infoPump_Click(object sender, EventArgs e)
        {
            string basePath = Program.gPath + "Manual\\1.contents\\22.EquipmentList\\18 Pump";

            // 경로가 존재하는지 확인
            if (Directory.Exists(basePath))
            {
                SlideViewer slideViewer = new SlideViewer(basePath);
                slideViewer.Show();
            }
            else
            {
                MessageBox.Show("The folder path does not exist.");
            }
        }

        private void infoce_Click(object sender, EventArgs e)
        {
            string basePath = Program.gPath + "Manual\\1.contents\\22.EquipmentList\\19 ce";

            // 경로가 존재하는지 확인
            if (Directory.Exists(basePath))
            {
                SlideViewer slideViewer = new SlideViewer(basePath);
                slideViewer.Show();
            }
            else
            {
                MessageBox.Show("The folder path does not exist.");
            }
        }
    }
}
