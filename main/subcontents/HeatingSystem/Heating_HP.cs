using main.contents;
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
    public partial class Heating_HP : Form
    {
        ArrayList SelectRow = new ArrayList(); ArrayList SelectHP_split = new ArrayList();
        String DefaultUse;
        public string SelectHP;
        public string Carrier;
        public Heating_HP(String DefaultUse, String SelectHP_nonsplit)
        {
            InitializeComponent();
            this.DefaultUse = DefaultUse;
            string[][] Image = Program.DB.getValue(DB.type.BaseDB_HCneed, "메뉴아이콘", "하위메뉴아이콘", "하위메뉴명 = '장비일람표'");
            Icon_pictureBox.Load(Program.gPath + Image[0][0]);
            Icon_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            create_table(DefaultUse);

            Carrier_comboBox.Items.Clear();
            Carrier_comboBox.Items.Add("전기");
            Carrier_comboBox.Items.Add("LNG");
            Carrier_comboBox.Items.Add("LPG");
            Carrier_comboBox.SelectedIndex = 0;
            if (SelectHP_nonsplit != null)
            {
                Load_SaveValue(SelectHP_nonsplit);
            }
            

        }

        private void Carrier_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            if(Carrier_comboBox.SelectedItem != null )
            {
                Carrier = Carrier_comboBox.SelectedItem.ToString();
                if( Carrier !="전기" )
                {
                    load_table_DB(DefaultUse, "가스");
                }
                else { load_table_DB(DefaultUse, Carrier); }
                
            }

        }
        void create_table(String DefaultUse)
        {
            new StackedHeaderDecorator(HP_dataGridView, DataGridViewAutoSizeColumnsMode.Fill, datagridviewDesign);
            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
            HP_dataGridView.Columns.Clear();
            checkBoxColumn.HeaderText = "선택";
            checkBoxColumn.Name = "check";
            HP_dataGridView.Columns.Add(checkBoxColumn);

            if (DefaultUse == "기본DB 적용")
            {
                HP_dataGridView.Columns.Add("A1", "등급");
                HP_dataGridView.Columns.Add("A2", "정격COP");
                HP_dataGridView.Columns.Add("A3", "한랭지COP");
            }
        }
        void load_table_DB(String DefaultUse, String Carrier)
        {
            HP_dataGridView.Rows.Clear();
            if (DefaultUse == "기본DB 적용")
            {
                string[][] DefaultDB_Value = Program.DB.getValue(DB.type.BaseDB_Heating, "히트펌프", "등급,정격COP,한랭지COP", "연료='"+Carrier+"'");
                for (int n = 0; n < DefaultDB_Value.Length; n++)
                {
                    HP_dataGridView.Rows.Add();
                    int nRow = HP_dataGridView.Rows.Count - 1;
                    HP_dataGridView.Rows[nRow].Cells[1].Value = DefaultDB_Value[n][0];
                    HP_dataGridView.Rows[nRow].Cells[2].Value = string.Format("{0:F1}", Convert.ToDouble(DefaultDB_Value[n][1]));
                    HP_dataGridView.Rows[nRow].Cells[3].Value = string.Format("{0:F1}", Convert.ToDouble(DefaultDB_Value[n][2]));
                }
            }
            else
            {
                string[][] User_Value = Program.DB.getValue(DB.type.ProjDB, "User_HP", "번호,명칭,연료,Type,용량,전부하효율,부분부하효율,소비전력,대기전력", "난방급탕 ='난방' OR 난방급탕 = '난방+급탕'");
                for (int n = 0; n < User_Value.Length; n++)
                {
                    string 용량 = "", 전부하효율 = "", 부분부하효율 = "", 소비전력 = "", 대기전력 = "";
                    if (User_Value[n][4] != null && User_Value[n][4] != "")
                    {
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
                    HP_dataGridView.Rows.Add();
                    int nRow = HP_dataGridView.Rows.Count - 1;
                    HP_dataGridView.Rows[nRow].Cells[1].Value = User_Value[n][0];
                    HP_dataGridView.Rows[nRow].Cells[2].Value = User_Value[n][1];
                    HP_dataGridView.Rows[nRow].Cells[3].Value = User_Value[n][2];
                    HP_dataGridView.Rows[nRow].Cells[4].Value = User_Value[n][3];
                    HP_dataGridView.Rows[nRow].Cells[5].Value = 용량;
                    HP_dataGridView.Rows[nRow].Cells[6].Value = 전부하효율;
                    HP_dataGridView.Rows[nRow].Cells[7].Value = 부분부하효율;
                    HP_dataGridView.Rows[nRow].Cells[8].Value = 소비전력;
                    HP_dataGridView.Rows[nRow].Cells[9].Value = 대기전력;
                    //HP_table.Rows.Add(User_Value[n][0], User_Value[n][1], User_Value[n][2], User_Value[n][3], 용량, 전부하효율, 부분부하효율, 소비전력, 대기전력);
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
                    SelectHP += HP_dataGridView.Rows[Convert.ToInt16(SelectRow[k])].Cells[1].Value.ToString() + ",";
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
            try
            {
                string[] token = SelectHP_nonsplit.Split(',');
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
            catch { }
        }

    }
}
