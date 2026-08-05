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
    public partial class DHWHP_DB : Form
    {
        ArrayList SelectRow = new ArrayList(); ArrayList SelectHP_split = new ArrayList();
        public string SelectHP;
        public string HC, Carrier;

        public DHWHP_DB( String SelectHP_nonsplit)
        {
            InitializeComponent(); this.Font = new Font(UTIL.Families[0], 9.75F, FontStyle.Regular);

            string[][] Image = Program.DB.getValue(DB.type.BaseDB_HCneed, "메뉴아이콘", "하위메뉴아이콘", "하위메뉴명 = '장비일람표'");
            if (Image.Length > 0)
            {
                Icon_pictureBox.Load(Program.gPath + Image[0][0]);
                Icon_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            }
            create_table();
            load_table_DB();
            if (SelectHP_nonsplit != null)
            {
                Load_SaveValue(SelectHP_nonsplit);
            }
        }
              

        void create_table()
        {
            HP_dataGridView.Columns.Clear();
            new StackedHeaderDecorator(HP_dataGridView, DataGridViewAutoSizeColumnsMode.Fill, datagridviewDesign);
            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
            checkBoxColumn.HeaderText = "선택";
            checkBoxColumn.Name = "check";
            HP_dataGridView.Columns.Add(checkBoxColumn);

            HP_dataGridView.Columns.Add("A1", "번호");
            HP_dataGridView.Columns.Add("A2", "명칭");
            HP_dataGridView.Columns.Add("A3", "용량" + Environment.NewLine + "[kW]");
            HP_dataGridView.Columns.Add("A4", "COP" + Environment.NewLine + "[kW]");
            HP_dataGridView.Columns.Add("A5", "소비전력" + Environment.NewLine + "[kW]");
            HP_dataGridView.Columns[0].Width = 40;
        }
        void load_table_DB()
        {           
            HP_dataGridView.Rows.Clear();
            string[][] User_Value = Program.DB.getValue(DB.type.ProjDB, "User_DHWHP", "번호,명칭,급탕정격용량,급탕정격COP,급탕정격소비전력", "");
            if (User_Value.Length > 0)
            {
                for (int n = 0; n < User_Value.Length; n++)
                {

                    HP_dataGridView.Rows.Add();
                    int nRow = HP_dataGridView.Rows.Count - 1;
                    HP_dataGridView.Rows[nRow].Cells[1].Value = User_Value[n][0];
                    HP_dataGridView.Rows[nRow].Cells[2].Value = User_Value[n][1];
                    HP_dataGridView.Rows[nRow].Cells[3].Value = string.Format("{0:F1}", Program.UTIL.ToDoubleOrZero(User_Value[n][2]));
                    HP_dataGridView.Rows[nRow].Cells[4].Value = string.Format("{0:F1}", Program.UTIL.ToDoubleOrZero(User_Value[n][3]));
                    HP_dataGridView.Rows[nRow].Cells[5].Value = string.Format("{0:F1}", Program.UTIL.ToDoubleOrZero(User_Value[n][4]));
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

    }
}
