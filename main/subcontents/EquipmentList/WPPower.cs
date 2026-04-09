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
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using main.info;

namespace main.subcontents.EquipmentList
{
    public partial class WPPower : Form
    {
        string WPNum;
        public double v_start, v_end;
        public WPPower(string WPNum, double v_start, double v_end)
        {
            InitializeComponent(); this.Font = new Font(UTIL.Families[0], 9.75F, FontStyle.Regular);
            this.WPNum = WPNum;
            this.v_start = v_start;
            this.v_end = v_end;
            create_table();
            Load_Value();
        }
        private void create_table()
        {
            new StackedHeaderDecorator(WPPower_dataGridView, DataGridViewAutoSizeColumnsMode.Fill, datagridviewDesign);
            WPPower_dataGridView.Columns.Clear();
            WPPower_dataGridView.Columns.Add("A0", "풍속[m/s]");
            WPPower_dataGridView.Columns.Add("A1", "풍속 구간별 출력[W]");
            WPPower_dataGridView.Columns[0].Width = 80;

            for(int a =0; a<(v_end- v_start) +1; a++ )
            {
                int nRow = WPPower_dataGridView.Rows.Add();
                WPPower_dataGridView.Rows[nRow].Cells[0].Value = v_start + a;
            }
        }

        private Boolean datagridviewDesign(DataGridViewCell cell, int column, int row)
        {
            if (column == 1)
            {
                if (cell.Value == null || cell.Value.ToString() == "")
                {
                    cell.Style.BackColor = SystemColors.Info;
                }
                else
                {
                    cell.Style.BackColor = Color.FromArgb(255, 255, 255);
                }
            }
            return true;
        }

        private void Save_button_Click(object sender, EventArgs e)
        {
            Boolean check = false;
            string Value = "";
            for (int a = 0; a < WPPower_dataGridView.Rows.Count -1; a++)
            { 
                if (WPPower_dataGridView.Rows[a].Cells[1].Value != null && WPPower_dataGridView.Rows[a].Cells[1].Value.ToString() != "")
                {
                    check = true;
                    Value += WPPower_dataGridView.Rows[a].Cells[1].Value.ToString() + "+";
                }
            }
            if (WPPower_dataGridView.Rows[WPPower_dataGridView.Rows.Count - 1].Cells[1].Value != null && WPPower_dataGridView.Rows[WPPower_dataGridView.Rows.Count - 1].Cells[1].Value.ToString() != "")
            {
                check = true;
                Value += WPPower_dataGridView.Rows[WPPower_dataGridView.Rows.Count - 1].Cells[1].Value.ToString() ;
            }

            if (check == true)
            {
                Program.DB.setValue(DB.type.ProjDB, "User_WP", "번호,풍속구간별출력",
                    "'" + WPNum + "','" + Value
                    + "'", "번호");
                Program.DB.saveProject();
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("모든 값을 입력해주세요.");
            }
        }
        private void Load_Value()
        {
            string[][] Value = Program.DB.getValue(DB.type.ProjDB, "User_WP", "풍속구간별출력", "번호='"+WPNum+"'");
            if (Value.Length > 0)
            {
                ArrayList arr = Split_(Value[0][0]);
                for(int a=0;  a< arr.Count; a++)
                {
                    WPPower_dataGridView.Rows[a].Cells[1].Value = arr[a].ToString();
                }
            }
        }

        private ArrayList Split_(String nonSplit)
        {
            ArrayList split = new ArrayList();
            if (nonSplit != null && nonSplit != "")
            {
                if (nonSplit.Contains('+'))
                {
                    string[] token = nonSplit.Split('+');
                    split.Clear();
                    foreach (var item in token)
                    {
                        split.Add(item.ToString());
                    }
                }
                else
                {
                    split.Clear();
                    split.Add(nonSplit);
                }
            }
            else
            {
                split.Clear();
            }
            return split;
        }

        private void WPPower_dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                WPPower_dataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }
        
    }
}
