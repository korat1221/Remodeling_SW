using main.contents;
using main.info;
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
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace main.subcontents.HeatingSystem
{
    public partial class AirHP_DB : Form
    {
        ArrayList SelectRow = new ArrayList(); ArrayList SelectHP_split = new ArrayList();
        String DefaultUse;
        public string SelectHP;
        public string HC, Carrier;

        public AirHP_DB(String DefaultUse, String SelectHP_nonsplit)
        {
            InitializeComponent(); this.Font = new Font(UTIL.Families[0], 9.75F, FontStyle.Regular);
            this.DefaultUse = DefaultUse;
            visible_Carrier_ComboBox(DefaultUse);

            string[][] Image = Program.DB.getValue(DB.type.BaseDB_HCneed, "메뉴아이콘", "하위메뉴아이콘", "하위메뉴명 = '장비일람표'");
            if (Image.Length > 0)
            {
                Icon_pictureBox.Load(Program.gPath + Image[0][0]);
                Icon_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            }


            HC_comboBox.Items.Clear();
            HC_comboBox.Items.AddRange(new string[] { "냉방", "냉난방" });
            HC_comboBox.SelectedIndex = 1;
            Carrier_comboBox.Items.Clear();
            Carrier_comboBox.Items.AddRange(new string[] { "전기", "가스" });

            Carrier_comboBox.SelectedIndex = 0;


            if (SelectHP_nonsplit != null)
            {
                Load_SaveValue(SelectHP_nonsplit);
            }
        }


        private void HC_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (HC_comboBox.SelectedItem != null)
            {
                HC = HC_comboBox.SelectedItem.ToString();

                if (HC != null && Carrier != null)
                {
                    create_table(DefaultUse, HC);
                    if (Carrier != "전기" && DefaultUse == "기존DB 적용")
                    {
                        load_table_DB(DefaultUse, "가스", HC);
                    }
                    load_table_DB(DefaultUse, Carrier, HC);
                }

            }
        }
        private void visible_Carrier_ComboBox(String DefaultValue)
        {
            if (DefaultValue == "기본DB 적용")
            {
                Carrier_comboBox.Visible = true;
                Carrier_label.Visible = true;
            }
            else
            {
                Carrier_comboBox.Visible = false;
                Carrier_label.Visible = false;
            }
        }
        private void Carrier_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (Carrier_comboBox.SelectedItem != null)
            {
                Carrier = Carrier_comboBox.SelectedItem.ToString();
                if (HC != null && Carrier != null)
                {
                    create_table(DefaultUse, HC);
                    if (Carrier != "전기" && DefaultUse == "기존DB 적용")
                    {
                        load_table_DB(DefaultUse, "가스", HC);
                    }
                    load_table_DB(DefaultUse, Carrier, HC);

                }
            }
        }

        void create_table(String DefaultUs, string HC)
        {
            HP_dataGridView.Columns.Clear();
            new StackedHeaderDecorator(HP_dataGridView, DataGridViewAutoSizeColumnsMode.Fill, datagridviewDesign);
            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
            checkBoxColumn.HeaderText = "선택";
            checkBoxColumn.Name = "check";
            HP_dataGridView.Columns.Add(checkBoxColumn);

            if (DefaultUse == "기본DB 적용")
            {
                HP_dataGridView.Columns.Add("A1", "등급");
                HP_dataGridView.Columns.Add("A2", "명칭");
                HP_dataGridView.Columns.Add("A3", "냉방.EER");

                if (HC == "냉난방")
                {
                    HP_dataGridView.Columns.Add("A4", "난방.COP");
                    HP_dataGridView.Columns.Add("A5", "난방.한랭지COP");
                }
                HP_dataGridView.Columns.Add("A6", "대기전력.유형");
                HP_dataGridView.Columns.Add("A7", "대기전력.소비전력[W]");
                HP_dataGridView.Columns.Add("A8", "열원");
            }
            else
            {
                HP_dataGridView.Columns.Add("A1", "번호");
                HP_dataGridView.Columns.Add("A2", "명칭");
                HP_dataGridView.Columns.Add("A3", "연료");
                HP_dataGridView.Columns.Add("A4", "공급유형");
                HP_dataGridView.Columns.Add("A5", "정격.용량" + Environment.NewLine + "[kW]");
                HP_dataGridView.Columns.Add("A6", "정격.COP" + Environment.NewLine + "[W/W]");
                HP_dataGridView.Columns.Add("A7", "정격.소비전력" + Environment.NewLine + "[kW]");
                HP_dataGridView.Columns.Add("A8", "한랭지.용량" + Environment.NewLine + "[kW]");
                HP_dataGridView.Columns.Add("A9", "한랭지.COP" + Environment.NewLine + "[W/W]");
                HP_dataGridView.Columns.Add("A10", "한랭지.소비전력" + Environment.NewLine + "[kW]");
            }
            HP_dataGridView.Columns[0].Width = 40;


        }
        void load_table_DB(String DefaultUse, String Carrier, String HC)
        {

            HP_dataGridView.Rows.Clear();
            if (DefaultUse == "기본DB 적용")
            {
                if (Carrier == "전기")
                {
                    string[][] CV = Program.DB.getValue(DB.type.BaseDB_Cooling, "AirCon", "번호,명칭,냉방표준성능,대기관련구분,대기전력,열원", "열원='" + Carrier + "'");
                    if (CV.Length > 0)
                    {
                        for (int i = 0; i < CV.Length; i++)
                        {
                            string test = CV[i][1].ToString().Substring(0, 3);
                            CV[i][0] = test;
                        }

                        for (int k = 1; k < 6; k++)
                        {
                            string Level = k + "등급";

                            for (int h = 0; h < CV.Length; h++)
                            {
                                if (Level == CV[h][0].ToString())
                                {
                                    if (HC == "냉난방")
                                    {
                                        string[][] DefaultDB_Value = Program.DB.getValue(DB.type.BaseDB_Heating, "히트펌프", "정격COP,한랭지COP", "등급='" + Level + "' And 연료='" + Carrier + "'");
                                        int nRow = HP_dataGridView.Rows.Add();
                                        HP_dataGridView.Rows[nRow].Cells[1].Value = CV[h][0].ToString();
                                        HP_dataGridView.Rows[nRow].Cells[2].Value = CV[h][1].ToString();
                                        HP_dataGridView.Rows[nRow].Cells[3].Value = CV[h][2].ToString();
                                        HP_dataGridView.Rows[nRow].Cells[4].Value = DefaultDB_Value[0][0].ToString();
                                        HP_dataGridView.Rows[nRow].Cells[5].Value = DefaultDB_Value[0][1].ToString();
                                        HP_dataGridView.Rows[nRow].Cells[6].Value = CV[h][3].ToString();
                                        HP_dataGridView.Rows[nRow].Cells[7].Value = CV[h][4].ToString();
                                        HP_dataGridView.Rows[nRow].Cells[8].Value = CV[h][5].ToString();
                                    }
                                    else
                                    {
                                        int nRow = HP_dataGridView.Rows.Add();
                                        HP_dataGridView.Rows[nRow].Cells[1].Value = CV[h][0].ToString();
                                        HP_dataGridView.Rows[nRow].Cells[2].Value = CV[h][1].ToString();
                                        HP_dataGridView.Rows[nRow].Cells[3].Value = CV[h][2].ToString();
                                        HP_dataGridView.Rows[nRow].Cells[4].Value = CV[h][3].ToString();
                                        HP_dataGridView.Rows[nRow].Cells[5].Value = CV[h][4].ToString();
                                        HP_dataGridView.Rows[nRow].Cells[6].Value = CV[h][5].ToString();
                                    }
                                }
                            }
                        }
                    }
                }
                else //가스일경우
                {
                    string[][] CV = Program.DB.getValue(DB.type.BaseDB_Cooling, "AirCon", "번호,명칭,냉방표준성능,대기관련구분,대기전력,열원", "열원='" + Carrier + "'");
                    if (CV.Length > 0)
                    {
                        for (int h = 0; h < CV.Length; h++)
                        {
                            if (HC == "냉난방")
                            {
                                string[][] DefaultDB_Value = Program.DB.getValue(DB.type.BaseDB_Heating, "히트펌프", "정격COP,한랭지COP", "연료='" + Carrier + "'");
                                int nRow = HP_dataGridView.Rows.Add();
                                HP_dataGridView.Rows[nRow].Cells[1].Value = "고효율";
                                HP_dataGridView.Rows[nRow].Cells[2].Value = CV[h][1].ToString();
                                HP_dataGridView.Rows[nRow].Cells[3].Value = CV[h][2].ToString();
                                HP_dataGridView.Rows[nRow].Cells[4].Value = DefaultDB_Value[0][0].ToString();
                                HP_dataGridView.Rows[nRow].Cells[5].Value = DefaultDB_Value[0][1].ToString();
                                HP_dataGridView.Rows[nRow].Cells[6].Value = CV[h][3].ToString();
                                HP_dataGridView.Rows[nRow].Cells[7].Value = CV[h][4].ToString();
                                HP_dataGridView.Rows[nRow].Cells[8].Value = CV[h][5].ToString();
                            }
                            else
                            {
                                int nRow = HP_dataGridView.Rows.Add();
                                HP_dataGridView.Rows[nRow].Cells[1].Value = "고효율";
                                HP_dataGridView.Rows[nRow].Cells[2].Value = CV[h][1].ToString();
                                HP_dataGridView.Rows[nRow].Cells[3].Value = CV[h][2].ToString();
                                HP_dataGridView.Rows[nRow].Cells[4].Value = CV[h][3].ToString();
                                HP_dataGridView.Rows[nRow].Cells[5].Value = CV[h][4].ToString();
                                HP_dataGridView.Rows[nRow].Cells[6].Value = CV[h][5].ToString();
                            }
                        }
                    }
                }
            }
            else
            {
                string[][] User_Value = Program.DB.getValue(DB.type.ProjDB, "User_AirHP", "번호,명칭,연료,공급유형,난방정격용량,난방정격COP,난방정격소비전력,한랭지용량,한랭지COP,한랭지소비전력", "난방냉방 = '냉난방'");
                if (User_Value.Length > 0)
                {
                    for (int n = 0; n < User_Value.Length; n++)
                    {

                        HP_dataGridView.Rows.Add();
                        int nRow = HP_dataGridView.Rows.Count - 1;
                        HP_dataGridView.Rows[nRow].Cells[1].Value = User_Value[n][0];
                        HP_dataGridView.Rows[nRow].Cells[2].Value = User_Value[n][1];
                        HP_dataGridView.Rows[nRow].Cells[3].Value = User_Value[n][2];
                        HP_dataGridView.Rows[nRow].Cells[4].Value = User_Value[n][3];
                        HP_dataGridView.Rows[nRow].Cells[5].Value = string.Format("{0:F1}", Convert.ToDouble(User_Value[n][4]));
                        HP_dataGridView.Rows[nRow].Cells[6].Value = string.Format("{0:F1}", Convert.ToDouble(User_Value[n][5]));
                        HP_dataGridView.Rows[nRow].Cells[7].Value = string.Format("{0:F1}", Convert.ToDouble(User_Value[n][6]));
                        HP_dataGridView.Rows[nRow].Cells[8].Value = string.Format("{0:F1}", Convert.ToDouble(User_Value[n][7]));
                        HP_dataGridView.Rows[nRow].Cells[9].Value = string.Format("{0:F1}", Convert.ToDouble(User_Value[n][8]));
                        HP_dataGridView.Rows[nRow].Cells[10].Value = string.Format("{0:F1}", Convert.ToDouble(User_Value[n][9]));
                    }
                }


            }
            // HP_dataGridView.DataSource = HP_table;
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

        private void SelectCheckBox()
        {
            foreach (DataGridViewRow row in HP_dataGridView.Rows)
            {
                if (Convert.ToBoolean(row.Cells["check"].Value))
                {
                    row.DefaultCellStyle.SelectionBackColor = SystemColors.GradientInactiveCaption;
                    SelectRow.Add(row.Index);
                }
            }
        }

        private void Save_button_Click(object sender, EventArgs e)
        {
            SelectRow.Clear();
            SelectCheckBox();
            for (int k = 0; k < SelectRow.Count; k++)
            {
                if (k == SelectRow.Count - 1)
                {
                    SelectHP += HP_dataGridView.Rows[Convert.ToInt16(SelectRow[k])].Cells[1].Value.ToString();
                }
                else
                {
                    SelectHP += HP_dataGridView.Rows[Convert.ToInt16(SelectRow[k])].Cells[1].Value.ToString() + "+";
                }
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        private void reset()
        {
            SelectRow.Clear();
            SelectHP_split.Clear();
            SelectHP = null;

            for (int n = 0; n < HP_dataGridView.Rows.Count; n++)
            {
                HP_dataGridView.Rows[n].Cells[0].Value = false;
            }

        }
        private void Load_SaveValue(String SelectHP_nonsplit)
        {
            reset();
            string[] token = SelectHP_nonsplit.Split('+');
            SelectHP_split.Clear();
            foreach (var item in token)
            {
                SelectHP_split.Add(item.ToString());
            }
            for (int k = 0; k < SelectHP_split.Count; k++)
            {
                for (int n = 0; n < HP_dataGridView.Rows.Count; n++)
                {
                    if (HP_dataGridView.Rows[n].Cells[1].Value.ToString() == SelectHP_split[k].ToString())
                    {
                        HP_dataGridView.Rows[n].Cells[0].Value = true;
                    }
                }
            }
        }

        private void infoHPdb_Click(object sender, EventArgs e)
        {
            string basePath = Program.gPath + "Manual\\2.subcontents\\12.EquipmentList\\02 HP";

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
