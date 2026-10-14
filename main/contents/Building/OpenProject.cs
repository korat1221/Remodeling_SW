using main.contentslist;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Text.Json;
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

using System.Data.SQLite;
using main.subcontents.HeatingSystem;
using main.info;

namespace main.contents
{
    public partial class OpenProject : Form
    {
        Dictionary<string, string> types = new Dictionary<string, string>();

        public OpenProject()
        {
            InitializeComponent(); this.Font = new Font(UTIL.Families[0], 9.75F, FontStyle.Regular);

            new StackedHeaderDecorator(dataGridView1, DataGridViewAutoSizeColumnsMode.Fill, datagridviewDesign);

            types.Add("1", "기존건물");
            types.Add("2", "리트로핏");
            types.Add("3", "리모델링");
            types.Add("4", "신규건물");
        }


        private void GeneralPanel_Paint(object sender, PaintEventArgs e)
        {
            Panel p = (Panel)sender;
            ControlPaint.DrawBorder(e.Graphics, p.DisplayRectangle, System.Drawing.SystemColors.Control, ButtonBorderStyle.Solid);

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

            string[][] res = Program.DB.querySQL(DB.type.ProjListDB, "SELECT ID, pnum, title, type,date FROM projects");
            if (res.Length > 0)
            {
                for (int n = 0; n < res.Length; n++)
                {
                    dataGridView1.Rows.Add();
                    int nRow = dataGridView1.Rows.Count - 1;

                    for (int k = 0; k < 5; k++)
                    {
                        dataGridView1.Rows[nRow].Cells[k + 1].Value = (k == 3) ? types[res[n][k]] : res[n][k];
                    }

                    DataGridViewCheckBoxCell cell = dataGridView1.Rows[nRow].Cells[0] as DataGridViewCheckBoxCell;

                    cell.Value = !!(res[n][1] == ProjectList.CurProjID);
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

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int k = GetSelectedIndex();
            if (k >= 0)
            {
                ProjectList.CurProjID = dataGridView1.Rows[k].Cells[2].Value.ToString();

                Program.DB.executeSQL(DB.type.ProjListDB, "UPDATE projects SET current = 0");
                Program.DB.executeSQL(DB.type.ProjListDB, "UPDATE projects SET current = 1 WHERE pnum='" + ProjectList.CurProjID + "'");

                Program.DB.openDB("projects\\" + ProjectList.CurProjID + ".sqlite");
                Program.DB.initTables(DB.type.ProjDB);
                Create_Project_GeneralData(k);
                Program.getMenuForm().resetAll();
                Program.getMenuForm().DoLoadFormDirect(0);
                Program.UTIL.ReloadModel();
            }
        }
        private void Create_Project_GeneralData(int k)
        {
            String ProjectType = dataGridView1.Rows[k].Cells[4].Value.ToString();
            string ProjectTypeNum = null;
            if (ProjectType == null)
            {
            }
            else if (ProjectType == "기존건물")
            {
                ProjectTypeNum = "1";
            }
            else if (ProjectType == "리트로핏")
            {
                ProjectTypeNum = "2";
            }
            else if (ProjectType == "리모델링")
            {
                ProjectTypeNum = "3";
            }
            else
            {
                ProjectTypeNum = "4";
            }

            Program.DB.setValue(DB.type.ProjDB, "BuildingGeneral", "프로젝트번호,프로젝트명,프로젝트유형,프로젝트유형번호"
                    , "'" + dataGridView1.Rows[k].Cells[2].Value.ToString() + "','" + dataGridView1.Rows[k].Cells[3].Value.ToString() + "','" + ProjectType + "','" + ProjectTypeNum + "'"
                    , "프로젝트번호");
            
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
            int k = GetSelectedIndex();
            if (k >= 0)
            {
                ProjectList.CurProjID = dataGridView1.Rows[k].Cells[2].Value.ToString();

                Program.DB.executeSQL(DB.type.ProjListDB, "UPDATE projects SET current = 0");
                Program.DB.executeSQL(DB.type.ProjListDB, "UPDATE projects SET current = 1 WHERE pnum='" + ProjectList.CurProjID + "'");

                Program.DB.openDB("projects\\" + ProjectList.CurProjID + ".sqlite");
                Program.DB.initTables(DB.type.ProjDB);
                Create_Project_GeneralData(k);
                Program.getMenuForm().resetAll();
                Program.getMenuForm().DoLoadFormDirect(0);
                Program.UTIL.ReloadModel();
            }
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

        private string GenerateUniqueProjectName(string baseName)
        {
            string projectsPath = Program.gPath + "projects\\";

            // 프로젝트 번호 형식이 "YYYY-MM-NNN"인 경우 마지막 숫자 부분을 증가
            if (System.Text.RegularExpressions.Regex.IsMatch(baseName, @"^\d{4}-\d{2}-\d{3}$"))
            {
                string prefix = baseName.Substring(0, 8); // "YYYY-MM-"
                string numberPart = baseName.Substring(8); // "NNN"

                if (int.TryParse(numberPart, out int number))
                {
                    string candidateName = baseName;
                    while (File.Exists(Path.Combine(projectsPath, candidateName + ".sqlite")) ||
                           IsProjectNameExistsInDB(candidateName))
                    {
                        number++;
                        candidateName = prefix + number.ToString("D3"); // 3자리로 포맷팅
                    }
                    return candidateName;
                }
            }

            // 기존 방식으로 fallback (형식이 맞지 않는 경우)
            string fallbackName = baseName;
            int counter = 1;

            while (File.Exists(Path.Combine(projectsPath, fallbackName + ".sqlite")) ||
                   IsProjectNameExistsInDB(fallbackName))
            {
                fallbackName = baseName + "-" + counter;
                counter++;
            }

            return fallbackName;
        }

        private bool IsProjectNameExistsInDB(string projectName)
        {
            try
            {
                string[][] result = Program.DB.querySQL(DB.type.ProjListDB,
                    "SELECT COUNT(*) FROM projects WHERE pnum = '" + projectName.Replace("'", "''") + "'");
                if (result.Length > 0 && result[0].Length > 0)
                {
                    return int.Parse(result[0][0]) > 0;
                }
            }
            catch { }
            return false;
        }

        private void AddProjectToDatabase(Dictionary<string, string> projectInfo)
        {
            try
            {
                // 새 ID 생성 (최대 ID + 1)
                string[][] maxIdResult = Program.DB.querySQL(DB.type.ProjListDB, "SELECT MAX(ID) FROM projects");
                int newID = 1; // 기본값
                if (maxIdResult.Length > 0 && maxIdResult[0].Length > 0 && !string.IsNullOrEmpty(maxIdResult[0][0]))
                {
                    if (int.TryParse(maxIdResult[0][0], out int maxId))
                    {
                        newID = maxId + 1;
                    }
                }
                string newIDStr = newID.ToString();

                // 새 프로젝트 레코드 추가 (ID는 숫자로 생성, current=0)
                string insertQuery = string.Format(
                    "INSERT INTO projects (ID, pnum, title, type, date, current) VALUES ({0}, '{1}', '{2}', '{3}', '{4}', 0)",
                    newIDStr,
                    projectInfo["pnum"].Replace("'", "''"), // SQL 인젝션 방지
                    projectInfo["title"].Replace("'", "''"), // SQL 인젝션 방지
                    projectInfo["type"],
                    projectInfo["date"]
                );

                Program.DB.executeSQL(DB.type.ProjListDB, insertQuery);

                // 프로젝트별 DB의 BuildingGeneral 테이블에 프로젝트 번호 업데이트
                string updateQuery = string.Format("UPDATE BuildingGeneral SET 프로젝트번호 = '{0}'", projectInfo["pnum"].Replace("'", "''"));
                Program.DB.executeSQL(projectInfo["pnum"], updateQuery);

                // executeSQL(string, string)이 가져온 프로젝트 DB를 저장한다. 현재 프로젝트 DB는 별도로 저장하지 않는다.
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("프로젝트 DB 등록 중 오류가 발생했습니다.", ex);
            }
        }

        private void info_Click(object sender, EventArgs e)
        {
            string basePath = Program.gPath + "Manual\\1.contents\\0.main\\05.OpenProject";

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

        private void File_button_Click(object sender, EventArgs e)
        {
            try
            {
                // ZFX 파일 선택
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.Filter = "ZFX 파일 (*.zfx)|*.zfx";
                    openFileDialog.Title = "ZFX 파일 열기";
                    openFileDialog.Multiselect = false;

                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        string zfxPath = openFileDialog.FileName;

                        // 임시 폴더에 압축 해제
                        string tempFolder = Path.Combine(Path.GetTempPath(), "zfx_extract_" + Guid.NewGuid().ToString());
                        Directory.CreateDirectory(tempFolder);

                        try
                        {
                            // ZFX 파일 압축 해제
                            ZipFile.ExtractToDirectory(zfxPath, tempFolder);

                            // DB는 필수이며, 내보내기 파일을 폴더째 압축한 경우도 지원한다.
                            string[] sqliteFiles = Directory.GetFiles(tempFolder, "*.sqlite");
                            if (sqliteFiles.Length == 0) { sqliteFiles = Directory.GetFiles(tempFolder, "*.sqlite", SearchOption.AllDirectories); }
                            if (sqliteFiles.Length != 1)
                            {
                                string reason = sqliteFiles.Length == 0 ? "프로젝트 DB(.sqlite)가 없습니다. 원본 프로젝트에서 다시 저장해 주세요." : "프로젝트 DB(.sqlite)가 여러 개입니다. 프로젝트 하나만 포함된 파일을 선택해 주세요.";
                                MessageBox.Show("유효하지 않은 ZFX 파일입니다. " + reason, "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                            if (new FileInfo(sqliteFiles[0]).Length == 0) { throw new InvalidDataException("프로젝트 DB(.sqlite)가 비어 있습니다. 원본 프로젝트에서 다시 저장해 주세요."); }
                            string sourceFolder = Path.GetDirectoryName(sqliteFiles[0]);
                            string originalProjectName = Path.GetFileNameWithoutExtension(sqliteFiles[0]);
                            string projectJsonPath = Path.Combine(sourceFolder, originalProjectName + ".json");
                            string projectTreePath = Path.Combine(sourceFolder, originalProjectName + ".tree");
                            string[] jsonFiles = File.Exists(projectJsonPath) ? new string[] { projectJsonPath } : Array.Empty<string>();
                            string[] treeFiles = File.Exists(projectTreePath) ? new string[] { projectTreePath } : Array.Empty<string>();
                            string infoJsonPath = Path.Combine(sourceFolder, "project_info.json");
                            if (!File.Exists(infoJsonPath)) { infoJsonPath = Path.Combine(tempFolder, "project_info.json"); }

                            // 기본값 설정
                            var projectInfo = new Dictionary<string, string>
                            {
                                ["ID"] = Guid.NewGuid().ToString(),
                                ["pnum"] = originalProjectName,
                                ["title"] = originalProjectName,
                                ["type"] = "3", // 리모델링
                                ["date"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                            };

                            if (File.Exists(infoJsonPath))
                            {
                                try
                                {
                                    string jsonContent = File.ReadAllText(infoJsonPath);
                                    var loadedInfo = JsonSerializer.Deserialize<Dictionary<string, string>>(jsonContent);
                                    if (loadedInfo != null)
                                    {
                                        // info.json에서 읽어온 값들로 덮어쓰기
                                        foreach (var kvp in loadedInfo)
                                        {
                                            projectInfo[kvp.Key] = kvp.Value;
                                        }
                                    }
                                }
                                catch { }
                            }

                            // 고유한 프로젝트 이름 생성 (중복 처리)
                            string uniqueProjectName = GenerateUniqueProjectName(originalProjectName);

                            // 고유한 이름으로 업데이트
                            projectInfo["pnum"] = uniqueProjectName;
                            // title은 원본 값 그대로 사용

                            // 파일들을 projects 폴더로 복사
                            string projectsPath = Program.gPath + "projects\\";

                            for (int i = 0; i < sqliteFiles.Length; i++)
                            {
                                string sqliteFile = sqliteFiles[i];
                                string destFile = Path.Combine(projectsPath, uniqueProjectName + ".sqlite");
                                File.Copy(sqliteFile, destFile, true);
                            }

                            for (int i = 0; i < jsonFiles.Length; i++)
                            {
                                string jsonFile = jsonFiles[i];
                                string fileName = Path.GetFileName(jsonFile);
                                if (fileName != "project_info.json") // info.json은 제외
                                {
                                    string destFile = Path.Combine(projectsPath, uniqueProjectName + ".json");
                                    File.Copy(jsonFile, destFile, true);
                                }
                            }

                            for (int i = 0; i < treeFiles.Length; i++)
                            {
                                string treeFile = treeFiles[i];
                                string destFile = Path.Combine(projectsPath, uniqueProjectName + ".tree");
                                File.Copy(treeFile, destFile, true);
                            }

                            // 프로젝트 폴더가 존재하면 같이 복사 (폴더 이름은 uniqueProjectName으로 변경)
                            string[] tempDirectories = Directory.GetDirectories(sourceFolder);
                            for (int i = 0; i < tempDirectories.Length; i++)
                            {
                                string tempDir = tempDirectories[i];
                                string dirName = Path.GetFileName(tempDir);
                                // sqlite/json/tree 파일과 같은 이름의 폴더를 프로젝트 폴더로 간주
                                if (dirName == originalProjectName)
                                {
                                    string destProjectFolder = Path.Combine(projectsPath, uniqueProjectName);
                                    DirectoryCopy(tempDir, destProjectFolder, true);
                                    break; // 첫 번째로 찾은 폴더만 복사
                                }
                            }

                            // 데이터베이스에 새 프로젝트 레코드 추가
                            AddProjectToDatabase(projectInfo);

                            // DB 변경사항 저장
                            Program.DB.savePListDB();

                            MessageBox.Show($"프로젝트 '{projectInfo["title"]}'을(를) 성공적으로 불러왔습니다.", "완료", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // 리스트 새로고침
                            drawList();
                        }
                        finally
                        {
                            // 임시 폴더 정리
                            try
                            {
                                Directory.Delete(tempFolder, true);
                            }
                            catch { }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"파일 열기 중 오류가 발생했습니다:\n{ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DirectoryCopy(string sourceDir, string destDir, bool copySubDirs)
        {
            DirectoryInfo dir = new DirectoryInfo(sourceDir);
            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException($"Source directory does not exist or could not be found: {sourceDir}");
            }

            DirectoryInfo[] dirs = dir.GetDirectories();
            Directory.CreateDirectory(destDir);

            foreach (FileInfo file in dir.GetFiles())
            {
                string tempPath = Path.Combine(destDir, file.Name);
                file.CopyTo(tempPath, true);
            }

            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string tempPath = Path.Combine(destDir, subdir.Name);
                    DirectoryCopy(subdir.FullName, tempPath, copySubDirs);
                }
            }
        }
    }
}
