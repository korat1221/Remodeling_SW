using main.subcontents.HeatingSystem;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static CustomComboBox;
using static main.MainContents;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using main.subcontents.ThermalBridge;
using static System.ComponentModel.Design.ObjectSelectorEditor;

namespace main.contents
{
    public partial class TB_List : Form
    {
        string sid = "";
        string TBType;
        string[] ID = { "RTB1", "RTB2", "RTB3", "RTB4", "RTB5", "RTB6", "WTB1", "WTB2", "WTB3", "WTB4", "WTB5", "WTB6" };
        string[] ID_Name = { "평지붕+외벽[90]", "평지붕+외벽[270]", "평지붕+내벽", "경사지붕", "경사지붕+외벽[수평]", "경사지붕+외벽[경사]", "층간슬라브+외벽", "외벽+내벽", "외벽+외벽[90]", "외벽+외벽[270]", "바닥+외벽[90]", "바닥+외벽[270]" };
        double Total_length;
        public TB_List()
        {
            InitializeComponent();
            string[][] Image = Program.DB.getValue(DB.type.BaseDB_HCneed, "메뉴아이콘", "하위메뉴아이콘", "하위메뉴명 = '열교정보'");
            Icon_pictureBox.Load(Program.gPath + Image[0][0]);
            Icon_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            create_datagridview1();
        }
        private void onVisibleChanged(object sender, EventArgs e)
        {
            if (main.MainContents.selID != sid)
            {
                this.panel1.Show();
                int split_num;

                #region ID 번호, 명칭 찾기 
                if (main.MainContents.selID.Length == 8)
                { split_num = Convert.ToInt16(main.MainContents.selID.Substring(7, 1)); }
                else { split_num = Convert.ToInt16(main.MainContents.selID.Substring(7, 2)); }
                label4.Text = ID[split_num - 1] + ". " + ID_Name[split_num - 1];
                TBType = ID_Name[split_num - 1];
                #endregion


                Load_TBDB();
            }
        }
        private void create_datagridview1()
        {
            new StackedHeaderDecorator(dataGridView1, DataGridViewAutoSizeColumnsMode.Fill, dataGridView1_RowHandle, true);

            dataGridView1.Columns.Clear();
            dataGridView1.Columns.Add("A0", "번호");
            dataGridView1.Columns.Add("A1", "유형");
            dataGridView1.Columns.Add("A2", "적용 열교");
            dataGridView1.Columns.Add("A3", "열교 명칭");
            dataGridView1.Columns.Add("A4", "선형 열관류율[W/mK]");
            dataGridView1.Columns.Add("A5", "길이[m]");
            dataGridView1.Columns[0].Width = 50;
            dataGridView1.Columns[5].Width = 80;
        }
        private void Load_TBDB()
        {
            dataGridView1.Rows.Clear();

            string[][] Value;
            Value = Program.DB.getValue(DB.type.ProjDB, "ThermalBridge_3D", "번호,열교항목,열교길이,선택열교", "열교항목 = '" + TBType + "'");

            for (int i = 0; i < Value.Length; i++)
            {
                int nRow = dataGridView1.Rows.Add();

                dataGridView1.Rows[nRow].Cells[0].Value = Value[i][0]; ;
                dataGridView1.Rows[nRow].Cells[1].Value = Value[i][1]; ;
                dataGridView1.Rows[nRow].Cells[5].Value = Convert.ToDouble(Value[i][2]).ToString("0.0");
                Total_length += Convert.ToDouble(Value[i][2]);
                dataGridView1.Rows[nRow].Cells[2].Value = Value[i][3]; ;
                if (Value[i][3] != null && Value[i][3] != "")
                {
                    string[][] tb2 = Program.DB.getValue(DB.type.BaseDB_HCneed, "접합부열교", "번호,명칭,값", "번호 ='" + Value[i][3] + "'");
                    if (tb2.Length > 0) { }
                    else
                    {
                        tb2 = Program.DB.getValue(DB.type.ProjDB, "User_TB", "번호,명칭,값", "번호 ='" + Value[i][3] + "'");
                    }

                    if (tb2.Length > 0)
                    {
                        dataGridView1.Rows[i].Cells[2].Value = tb2[0][0]; ;
                        dataGridView1.Rows[i].Cells[3].Value = tb2[0][1]; ;
                        dataGridView1.Rows[i].Cells[4].Value = Convert.ToDouble(tb2[0][2]).ToString("0.000");
                    }
                }
            }

            length_textBox.Text = Total_length.ToString("0.0");

        }

        private bool dataGridView1_RowHandle(DataGridViewCell cell, int column, int row)
        {
            if (row % 2 == 1)
            {
                if (column == 1 || column == 2 || column == 3 || column == 4 || column == 5)
                {
                    cell.Style.BackColor = SystemColors.InactiveBorder;
                    return true;
                }
                else return false;
            }
            else
            {
                if (column == 1 || column == 2 || column == 3 || column == 4 || column == 5)
                {
                    cell.Style.BackColor = Color.FromArgb(255, 255, 255);
                    return true;
                }
                else return false;
            }
        }
    }
}
