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
using System.Drawing.Configuration;
using static System.Data.Entity.Infrastructure.Design.Executor;

namespace main.contents
{
    public partial class ProjectList : Form
    {
        public static String ProjectType = null;
        public static String CurProjID = null;

        private bool drawing = false;

        Dictionary<string, string> types = new Dictionary<string, string>();

        public ProjectList()
        {
            InitializeComponent();

            new StackedHeaderDecorator(dataGridView1, DataGridViewAutoSizeColumnsMode.Fill);

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



        public static bool OnLoadProc2(Form form)
        {
            Intro f = (Intro)form;

            f.LoadData("");

            return true;
        }

        public void LoadData(String ID)            // 리스트에서 항목 더블 클릭시 - 뷰를 ID 의 getValue 값으로 채우기
        {
            try
            {

                if (ProjectType == null)
                {
                    MessageBox.Show("프로젝트 유형부터 선택하세요.");
                    Program.getMenuForm().DoLoadForm(40, OnLoadProc2);
                }
                else if (ProjectType == "1")
                {
                    Icon_pictureBox.Load(Program.gPath + "images/1sticon/0.Intro1.png");
                    Icon_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                    drawList(1.ToString());
                }
                else if (ProjectType == "2")
                {
                    Icon_pictureBox.Load(Program.gPath + "images/1sticon/0.Intro2.png");
                    Icon_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                    drawList(1.ToString());
                }
                else if (ProjectType == "3")
                {
                    Icon_pictureBox.Load(Program.gPath + "images/1sticon/0.Intro3.png");
                    Icon_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                    drawList(1.ToString());
                }
                else
                {
                    Icon_pictureBox.Load(Program.gPath + "images/1sticon/0.Intro2.png");
                    Icon_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                    drawList(4.ToString());
                }
                ProjectType_textBox.Text = types[ProjectType];

            }
            catch { }

        }

        private void drawList(String ProjectType2)
        {
            drawing = true;
            dataGridView1.Rows.Clear();

            string[][] res = Program.DB.querySQL(DB.type.ProjListDB, "SELECT ID, pnum, title, type FROM projects WHERE type='" + ProjectType + "'OR type = '" + ProjectType2 + "'");
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
            drawing = false;
        }
        static void CopyDirectory(string sourceDir, string destinationDir, bool recursive)
        {
            // Get information about the source directory
            var dir = new DirectoryInfo(sourceDir);

            // Check if the source directory exists
            if (!dir.Exists)
                throw new DirectoryNotFoundException($"Source directory not found: {dir.FullName}");

            // Cache directories before we start copying
            DirectoryInfo[] dirs = dir.GetDirectories();

            // Create the destination directory
            Directory.CreateDirectory(destinationDir);

            // Get the files in the source directory and copy to the destination directory
            foreach (FileInfo file in dir.GetFiles())
            {
                string targetFilePath = Path.Combine(destinationDir, file.Name);
                file.CopyTo(targetFilePath);
            }

            // If recursive and copying subdirectories, recursively call this method
            if (recursive)
            {
                foreach (DirectoryInfo subDir in dirs)
                {
                    string newDestinationDir = Path.Combine(destinationDir, subDir.Name);
                    CopyDirectory(subDir.FullName, newDestinationDir, true);
                }
            }
        }

        private void Copy_button_Click(object sender, EventArgs e)
        {
            int k = GetSelectedIndex();
            if (k >= 0)
            {
                string pid0 = dataGridView1.Rows[k].Cells[2].Value.ToString();

                //  Program.DB.executeSQL(DB.type.ProjListDB, "UPDATE projects SET type = '" + ProjectType + "', title = WHERE pnum = '" + pid + "'");
                ProjectCopy projectcopy = new ProjectCopy();
                DialogResult result = projectcopy.ShowDialog();
                if (result == DialogResult.OK)
                {
                    string pid = AddProject(pid0, dataGridView1.Rows[k].Cells[3].Value.ToString());

                    SQLiteConnection db = new SQLiteConnection(@"Data Source=" + Program.gPath + "projects\\" + pid + ".sqlite");
                    db.Open();

                    string[][] res = Program.DB.querySQL(db, "SELECT name FROM sqlite_master WHERE type IN('table', 'view') AND name NOT LIKE 'sqlite_%' UNION ALL SELECT name FROM sqlite_temp_master WHERE type IN('table', 'view') ORDER BY 1");
                    for (int n = 0; n < res.Length; n++)
                    {
                        string table = res[n][0];

                        if (projectcopy.tables.Find(p => p == table) == null)
                        {
                            Program.DB.executeSQL(db, "DROP TABLE " + table);
                        }
                    }

                    db.Close();

                    if (projectcopy.model_copy)
                    {
                        Directory.CreateDirectory(Program.gPath + "threejs\\public\\models\\" + pid);

                        CopyDirectory(Program.gPath + "threejs\\public\\models\\" + pid0, Program.gPath + "threejs\\public\\models\\" + pid, true);
                    }

                    drawList(ProjectType.ToString());

                    MessageBox.Show("프로젝트를 복사하였습니다.");
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
                if (column == 3 && cell.Value.ToString() == null)
                {
                    cell.Style.BackColor = Color.FromArgb(255, 255, 243);
                    return true;
                }
                return true;
            }
            else
            {
                cell.Style.BackColor = Color.FromArgb(255, 255, 255);
                cell.Style.ForeColor = Color.Black;
                cell.Style.SelectionBackColor = Color.FromArgb(255, 255, 255);
                cell.Style.SelectionForeColor = Color.Black;
                if (column == 3 && cell.Value.ToString() == null)
                {
                    cell.Style.BackColor = Color.FromArgb(255, 255, 243);
                    return true;
                }
                return true;
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
            AddProject();

            drawList(ProjectType.ToString());
        }
        string AddProject(string pid0 = "", string title = "")
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

            Program.DB.executeSQL(DB.type.ProjListDB, "INSERT INTO projects (pnum, type, title) VALUES ('" + pid + "'," + ProjectType + ",'" + title + "')");

            if (pid0 != "")
            {
                File.Copy(Program.gPath + "projects\\" + pid0 + ".sqlite", Program.gPath + "projects\\" + pid + ".sqlite", true);
            }
            else
            {
                File.Copy("templ.sqlite", Program.gPath + "projects\\" + pid + ".sqlite", true);
            }

            Directory.CreateDirectory(Program.gPath + "threejs\\public\\models\\" + pid);

            return pid;
        }
        private int GetSelectedIndex()
        {
            for (int k = 0; k < dataGridView1.Rows.Count; k++)
            {
                if (Convert.ToBoolean(dataGridView1.Rows[k].Cells[0].Value) == true)
                {
                    return k;
                }
            }
            return -1;
        }

        private void Delete_button_Click(object sender, EventArgs e)
        {
            int k = GetSelectedIndex();
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

                        if (File.Exists(Program.gPath + "projects\\" + pid + ".sqlite"))
                            File.Delete(Program.gPath + "projects\\" + pid + ".sqlite");

                        if (Directory.Exists(Program.gPath + "threejs\\public\\models\\" + pid))
                            Directory.Delete(Program.gPath + "threejs\\public\\models\\" + pid, true);

                        if (CurProjID == pid)
                        {
                            res = Program.DB.querySQL(DB.type.ProjListDB, "SELECT pnum FROM projects WHERE type = " + ProjectType + " ORDER BY pnum DESC");

                            if (res.Length > 0)
                            {
                                CurProjID = res[0][0];
                            }
                        }

                        drawList(ProjectType.ToString());
                        MessageBox.Show("프로젝트를 삭제하였습니다.");
                    }
                }
            }
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (!drawing)
            {
                int k = dataGridView1.CurrentCell != null ? dataGridView1.CurrentCell.RowIndex : -1;

                if (k >= 0 && dataGridView1.Rows[k].Cells[3].Value != null)
                {
                    string pid = dataGridView1.Rows[k].Cells[2].Value.ToString();
                    string title = dataGridView1.Rows[k].Cells[3].Value.ToString();

                    string[][] res = Program.DB.querySQL(DB.type.ProjListDB, "SELECT title FROM projects WHERE pnum='" + pid + "'");

                    if (res.Length > 0)
                    {
                        if (res[0][0] != title)
                        {
                            Program.DB.querySQL(DB.type.ProjListDB, "UPDATE projects SET title='" + title + "' WHERE pnum='" + pid + "'");
                        }
                    }
                }
            }
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

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            OpenCurrentProject();
        }

        private void Save_button_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0) { return; }
            for (int k = 0; k < dataGridView1.Rows.Count; k++)
            {
                if (dataGridView1.Rows[k].Cells[3].Value.ToString() == "")
                { MessageBox.Show("프로젝트명을 전부 입력하세요."); }
                else
                {
                    Program.DB.executeSQL(DB.type.ProjListDB, "UPDATE projects SET title= '"+dataGridView1.Rows[k].Cells[3].Value.ToString()+"' WHERE pnum='" + dataGridView1.Rows[k].Cells[2].Value.ToString() + "'");
                    MessageBox.Show("저장되었습니다.");
                }
            }

        }
    }
}
