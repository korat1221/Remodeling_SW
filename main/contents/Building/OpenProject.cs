using main.contentslist;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Net;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using static System.ComponentModel.Design.ObjectSelectorEditor;
using static main.DB;
using System.Data.Entity.Core.Metadata.Edm;
using System.Security.Cryptography;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;
using System.Data.SQLite;
using main.subcontents.HeatingSystem;

namespace main.contents
{
    public partial class OpenProject : Form
    {
        Dictionary<string, string> types = new Dictionary<string, string>();

        public OpenProject()
        {
            InitializeComponent();

            new StackedHeaderDecorator(dataGridView1, DataGridViewAutoSizeColumnsMode.Fill, datagridviewDesign);

            types.Add("1", "기존건물");
            types.Add("2", "리트로핏");
            types.Add("3", "리모델링");
            types.Add("4", "신규건물");
        }


        private void GeneralPanel_Paint(object sender, PaintEventArgs e)
        {
            Panel p = (Panel)sender;
            ControlPaint.DrawBorder(e.Graphics, p.DisplayRectangle, System.Drawing.Color.FromArgb(153, 180, 209), ButtonBorderStyle.Solid);

        }


        public void LoadData(String ID)            // 리스트에서 항목 더블 클릭시 - 뷰를 ID 의 getValue 값으로 채우기
        {
            try
            {
                drawList();
            }
            catch { }

        }

        private void drawList()
        {
            dataGridView1.Rows.Clear();

            string[][] res = Program.DB.querySQL(DB.type.ProjListDB, "SELECT ID, pnum, title, type FROM projects");
            for (int n = 0; n < res.Length; n++)
            {
                dataGridView1.Rows.Add();
                int nRow = dataGridView1.Rows.Count - 1;

                for (int k = 0; k < 4; k++)
                {
                    dataGridView1.Rows[nRow].Cells[k + 1].Value = (k == 3) ? types[res[n][k]] : res[n][k];
                }

                DataGridViewCheckBoxCell cell = dataGridView1.Rows[nRow].Cells[0] as DataGridViewCheckBoxCell;

                cell.Value = !!(res[n][1] == ProjectList.CurProjID);
            }
        }
        private void Copy_button_Click(object sender, EventArgs e)
        {
            ProjectCopy projectcopy = new ProjectCopy();
            DialogResult result = projectcopy.ShowDialog();
            if (result == DialogResult.OK)
            {
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

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            OpenCurrentProject();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                dataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit);

                for (int k = 0; k < dataGridView1.Rows.Count; k++)
                {
                    if (k != dataGridView1.CurrentCell.RowIndex)
                    {
                        dataGridView1.Rows[k].Cells[0].Value = false;
                    }
                    else
                    {
                        dataGridView1.Rows[k].Cells[0].Value = true;
                    }

                }
            }
        }

        private void Open_button_Click(object sender, EventArgs e)
        {
            OpenCurrentProject();
        }

        private void OpenCurrentProject()
        {
            int k = dataGridView1.CurrentCell.RowIndex;
            if (k > -1)
            {
                ProjectList.CurProjID = dataGridView1.Rows[k].Cells[2].Value.ToString();

                Program.DB.executeSQL(DB.type.ProjListDB, "UPDATE projects SET current = 0");
                Program.DB.executeSQL(DB.type.ProjListDB, "UPDATE projects SET current = 1 WHERE pnum='" + ProjectList.CurProjID + "'");

                Program.DB.openDB("projects\\" + ProjectList.CurProjID + ".sqlite");
                Program.DB.initTables(DB.type.ProjDB);
                Program.getMenuForm().ResetForm(8);
                Program.getMenuForm().DoLoadFormDirect(0);
            }
        }
    }
}
