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
    public partial class ProjectList : Form
    {
        public static String ProjectType = "1";
        public static String CurProjID = "2023-11-001";

        Dictionary<string, string> types = new Dictionary<string, string>();

        public ProjectList()
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
                ProjectType_textBox.Text = types[ProjectType];
                if (ProjectType == null) { }
                else if (ProjectType == "1")
                {
                    Icon_pictureBox.Load(Program.gPath + "images/1sticon/0.Intro1.png");
                    Icon_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                }
                else if (ProjectType == "2")
                {
                    Icon_pictureBox.Load(Program.gPath + "images/1sticon/0.Intro2.png");
                    Icon_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                }
                else if (ProjectType == "3")
                {
                    Icon_pictureBox.Load(Program.gPath + "images/1sticon/0.Intro3.png");
                    Icon_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                }
                else
                {
                    Icon_pictureBox.Load(Program.gPath + "images/1sticon/0.Intro2.png");
                    Icon_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                }

                drawList();
            }
            catch { }

        }

        private void drawList()
        {
            dataGridView1.Rows.Clear();

            string[][] res = Program.DB.querySQL(DB.type.ProjListDB, "SELECT ID, pnum, title, type FROM projects WHERE type=" + ProjectType);
            for (int n = 0; n < res.Length; n++)
            {
                dataGridView1.Rows.Add();
                int nRow = dataGridView1.Rows.Count - 1;

                for (int k = 0; k < 4; k++)
                {
                    dataGridView1.Rows[nRow].Cells[k + 1].Value = (k == 3) ? types[res[n][k]] : res[n][k];
                }

                DataGridViewCheckBoxCell cell = dataGridView1.Rows[nRow].Cells[0] as DataGridViewCheckBoxCell;

                cell.Value = !!(res[n][1] == CurProjID);
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
            int k = dataGridView1.CurrentCell.RowIndex;
            if (k > -1)
            {
                CurProjID = dataGridView1.Rows[k].Cells[2].Value.ToString();

                Program.DB.executeSQL(DB.type.ProjListDB, "UPDATE projects SET current = 0");
                Program.DB.executeSQL(DB.type.ProjListDB, "UPDATE projects SET current = 1 WHERE pnum='" + CurProjID + "'");

                Program.DB.openDB("projects\\" + ProjectList.CurProjID + ".sqlite");
                Program.DB.initTables(DB.type.ProjDB);
                Program.getMenuForm().ResetForm(8);
                Program.getMenuForm().DoLoadFormDirect(0);
            }
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

        private void New_button_Click(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;
            int num = 1;
            string[][] res = Program.DB.querySQL(DB.type.ProjListDB, "SELECT pnum FROM projects ORDER BY pnum DESC");

            if (res.Length > 0)
            {
                string[] s = res[0][0].Split('-');

                if (s.Length == 3)
                {
                    num = Int32.Parse(s[2]) + 1;
                }
            }

            string pid = dt.Year + "-" + dt.Month.ToString().PadLeft(2, '0') + "-" + num.ToString().PadLeft(3, '0');

            Program.DB.executeSQL(DB.type.ProjListDB, "INSERT INTO projects (pnum, type, title) VALUES ('" + pid + "'," + ProjectType + ",'')");

            File.Copy("templ.sqlite", Program.gPath + "projects\\" + pid + ".sqlite", true);

            Directory.CreateDirectory(Program.gPath + "threejs\\public\\models\\" + pid);

            drawList();
        }

        private void Delete_button_Click(object sender, EventArgs e)
        {
            int k = dataGridView1.CurrentCell.RowIndex;
            if (k < 0)
            {
                MessageBox.Show("먼저 삭제할 프로젝트를 선택하세요.");
            }
            else
            {
                string[][] res = Program.DB.querySQL(DB.type.ProjListDB, "SELECT COUNT(*) FROM projects");

                if (res.Length <= 0)
                {
                    MessageBox.Show("프로그램 설정 파일이 훼손되었습니다.");
                }
                else if (Int32.Parse(res[0][0]) <= 1)
                {
                    MessageBox.Show("정상적인 프로그램 사용을 위해서 하나 이상의 프로젝트가 남아있어야 합니다.");
                }
                else
                {
                    DialogResult result = MessageBox.Show("선택하신 프로젝트 정보가 영구적으로 삭제됩니다. 계속하시겠습니까 ?", "",
                        MessageBoxButtons.YesNo);

                    if (result == DialogResult.Yes)
                    {
                        string pid = dataGridView1.Rows[k].Cells[2].Value.ToString();

                        Program.DB.executeSQL(DB.type.ProjListDB, "DELETE FROM projects WHERE pnum='" + pid + "'");

                        File.Delete(Program.gPath + "projects\\" + pid + ".sqlite");

                        Directory.Delete(Program.gPath + "threejs\\public\\models\\" + pid, true);

                        if (CurProjID == pid)
                        {
                            res = Program.DB.querySQL(DB.type.ProjListDB, "SELECT pnum FROM projects WHERE type = " + ProjectType + " ORDER BY pnum DESC");

                            if (res.Length > 0)
                            {
                                CurProjID = res[0][0];
                            }
                        }

                        drawList();
                        MessageBox.Show("프로젝트를 삭제하였습니다.");
                    }
                }
            }
        }
    }
}
