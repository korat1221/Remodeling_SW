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

namespace main.subcontents.DHWSystem
{
    public partial class DHW_Solar : Form
    {
        ArrayList SelectRow = new ArrayList(); ArrayList SelectSolar_split = new ArrayList();
        String DefaultUse;
        public string SelectSolar;
        //HeatingSystem heatingSystem;

        // public DHW_Solar(HeatingSystem system)
        public DHW_Solar(String DefaultUse, String SelectSolar_nonsplit)
        {
            InitializeComponent();
            //heatingSystem = system;
            this.DefaultUse = DefaultUse;
            load_table_DB();
            string[][] Image = Program.DB.getValue(DB.type.BaseDB_HCneed, "메뉴아이콘", "하위메뉴아이콘", "하위메뉴명 = '장비일람표'");
            Icon_pictureBox.Load(Program.gPath + Image[0][0]);
            Icon_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            if (SelectSolar_nonsplit != null)
            {
                Load_SaveValue(SelectSolar_nonsplit);
            }

        }

        void load_table_DB()
        {
            //DataTable Solar_table = new DataTable();
            new StackedHeaderDecorator(Solar_dataGridView, DataGridViewAutoSizeColumnsMode.Fill, datagridviewDesign);
            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
            Solar_dataGridView.Columns.Clear();
            checkBoxColumn.HeaderText = "선택";
            checkBoxColumn.Name = "check";
            Solar_dataGridView.Columns.Add(checkBoxColumn);

            Solar_dataGridView.Columns.Add("A1", "번호");
            Solar_dataGridView.Columns.Add("A2", "명칭");
            if (DefaultUse != "기본DB 적용")
            {
                Solar_dataGridView.Columns.Add("A3", "모듈면적.A[m2]");
            }
            Solar_dataGridView.Columns.Add("A4", "효율.η0]");
            Solar_dataGridView.Columns.Add("A5", "손실계수.1차.k1");
            Solar_dataGridView.Columns.Add("A6", "손실계수.2차.k2");
            Solar_dataGridView.Columns.Add("A7", "50°의 입사각.Khem(50֠)");
            Solar_dataGridView.Columns.Add("A8", "유효 열용량.C");
            Solar_dataGridView.Columns[2].Width = 150;

            if (DefaultUse == "기본DB 적용")
            {
                string[][] DefaultDB_Value = Program.DB.getValue(DB.type.BaseDB_Heating, "태양열시스템", "번호,제품명,효율,열손실계수1차,열손실계수2차,입사각50도,유효열용량", "");
                for (int n = 0; n < DefaultDB_Value.Length; n++)
                {
                    Solar_dataGridView.Rows.Add();
                    int nRow = Solar_dataGridView.Rows.Count - 1;
                    Solar_dataGridView.Rows[nRow].Cells[1].Value = DefaultDB_Value[n][0];
                    Solar_dataGridView.Rows[nRow].Cells[2].Value = DefaultDB_Value[n][1];
                    Solar_dataGridView.Rows[nRow].Cells[3].Value = DefaultDB_Value[n][2];
                    Solar_dataGridView.Rows[nRow].Cells[4].Value = DefaultDB_Value[n][3];
                    Solar_dataGridView.Rows[nRow].Cells[5].Value = DefaultDB_Value[n][4];
                    Solar_dataGridView.Rows[nRow].Cells[6].Value = DefaultDB_Value[n][5];
                    Solar_dataGridView.Rows[nRow].Cells[7].Value = DefaultDB_Value[n][6];
                   
                }
            }
            else
            {
                string[][] User_Value = Program.DB.getValue(DB.type.ProjDB, "User_Solar", "번호,명칭,모듈면적,효율,열손실계수1차,열손실계수2차,입사각50도,유효열용량", "난방급탕 ='난방' OR 난방급탕 = '난방+급탕'");
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
                }
            }
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
            foreach (DataGridViewRow row in Solar_dataGridView.Rows)
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
                    SelectSolar += Solar_dataGridView.Rows[Convert.ToInt16(SelectRow[k])].Cells[1].Value.ToString();
                }
                else
                {
                    SelectSolar += Solar_dataGridView.Rows[Convert.ToInt16(SelectRow[k])].Cells[1].Value.ToString() + "+";
                }
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        private void reset()
        {
            SelectRow.Clear();
            SelectSolar_split.Clear();
            SelectSolar = null;

            for (int n = 0; n < Solar_dataGridView.Rows.Count; n++)
            {
                Solar_dataGridView.Rows[n].Cells[0].Value = false;
            }

        }
        private void Load_SaveValue(String SelectSolar_nonsplit)
        {
            reset();
            try
            {
                string[] token = SelectSolar_nonsplit.Split('+');
                SelectSolar_split.Clear();
                foreach (var item in token)
                {
                    SelectSolar_split.Add(item.ToString());
                }
                for (int k = 0; k < SelectSolar_split.Count; k++)
                {
                    for (int n = 0; n < Solar_dataGridView.Rows.Count; n++)
                    {
                        if (Solar_dataGridView.Rows[n].Cells[1].Value.ToString() == SelectSolar_split[k].ToString())
                        {
                            Solar_dataGridView.Rows[n].Cells[0].Value = true;
                        }
                    }
                }

            }
            catch { }
        }
    }
}
