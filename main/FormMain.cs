using main.contents;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace main
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
            this.FormClosing += FormMain_FormClosing;
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            // 종료 전: 현재 화면의 입력값을 DB에 반영하고 디스크에 저장한다.
            try
            {
                MainContents.Instance?.SaveCurrentForm();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"종료 저장 오류: {ex.Message}");
            }
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            if (Program.DB.openPListDB(Program.gPath))
            {
                string[][] res = Program.DB.querySQL(DB.type.ProjListDB, "SELECT pnum FROM projects WHERE current= 1");

                if (res.Length > 0)
                {
                    ProjectList.CurProjID = res[0][0];
                }
            }

            Program.DB.openDB("projects\\" + ProjectList.CurProjID + ".sqlite");
            MainContents f1 = new MainContents();

            f1.Location.Offset(0, 0);
            f1.Size = new Size(splitContainer1.Panel1.Width, splitContainer1.Panel1.Height);
            f1.TopLevel = false;
            splitContainer1.Panel1.Controls.Add(f1);

            f1.DoResizeMain(new Size(splitContainer1.Panel2.Width, splitContainer1.Panel2.Height));

            f1.Show();
            string[][] value = Program.DB.getValue(DB.type.ProjDB, "BuildingGeneral", "프로젝트번호,프로젝트유형,프로젝트명");
            if (value.Length > 0)
            {
                this.Text = "ZEROFIX        " + value[0][0] + "_" + value[0][2] + "_" + value[0][1];
            }
        }

        private void OnClosed(object sender, FormClosedEventArgs e)
        {
            // 저장은 FormClosing(FormMain_FormClosing)에서 SaveCurrentForm()으로 이미 처리됨.
            // 혹시 남은 변경이 있으면 마지막으로 한 번 더 flush (dirty 아니면 no-op).
            Program.DB.saveProject();

            Program.DB.closeDB();
            Program.DB.closePListDB();
            main.Program.killServer();
        }

        private void OnResize(object sender, EventArgs e)
        {
            try
            {
                MainContents f1 = (MainContents)splitContainer1.Panel1.Controls[0];

                f1.Location.Offset(0, 0);
                f1.Size = new Size(splitContainer1.Panel1.Width, splitContainer1.Panel1.Height);

                f1.DoResizeMain(new Size(splitContainer1.Panel2.Width, splitContainer1.Panel2.Height));
            }
            catch (Exception ex)
            {
                //                MessageBox.Show(ex.ToString());
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Program.getMenuForm().DoLoadForm(40, OnLoadProc1);
        }


        public static bool OnLoadProc1(Form form)
        {
            Intro f = (Intro)form;

            f.LoadData("");

            return true;
        }
        public static bool OnLoadProc2(Form form)
        {
            OpenProject f = (OpenProject)form;

            f.LoadData("");

            return true;
        }


        private void EnergyNeed_Sim_Click(object sender, EventArgs e)
        {
            CALC.run(new string[] { "존계산" });
            MessageBox.Show("계산되었습니다.");
        }

        private void AHUNeed_Sim_Click(object sender, EventArgs e)
        {
            CALC.run(new string[] { "공조계산" });
            MessageBox.Show("계산되었습니다.");

        }

        private void FinalEnergy_Sim_Click(object sender, EventArgs e)
        {
            Program.DB.UseCaches(true);
            CALC.run(new string[] { "모두계산" });
            Program.DB.UseCaches(false);
            MessageBox.Show("계산되었습니다.");
        }

        private void ProjectOpen_Click(object sender, EventArgs e)
        {
            Program.getMenuForm().DoLoadForm(42, OnLoadProc2);
        }

        private void Element_Sim_Click(object sender, EventArgs e)
        {
            Program.DB.UseCaches(true);
            CALC.run(new string[] { "요소기술계산" });
            Program.DB.UseCaches(false);
            MessageBox.Show("요소기술별 계산이 완료되었습니다.");
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            string tempInfoPath = null;
            string tempZipPath = null;
            string tempDbPath = null;
            string saveStage = "현재 화면 및 DB 저장";
            try
            {
                // 현재 프로젝트 ID 확인
                if (string.IsNullOrEmpty(ProjectList.CurProjID))
                {
                    MessageBox.Show("현재 선택된 프로젝트가 없습니다.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // 압축(zfx) 대상이 되는 .sqlite 파일을 디스크에서 읽으므로,
                // 현재 화면의 입력값을 먼저 DB에 반영시키고 디스크에 저장한다.
                MainContents.Instance?.SaveCurrentForm();

                // 프로젝트 파일들이 있는 폴더 경로
                string projectsPath = Path.Combine(Program.gPath, "projects");
                string projectDbPath = Path.Combine(projectsPath, ProjectList.CurProjID + ".sqlite");
                if (!File.Exists(projectDbPath) || new FileInfo(projectDbPath).Length == 0) { throw new FileNotFoundException("프로젝트 DB가 없거나 비어 있어 백업을 저장할 수 없습니다.", projectDbPath); }
                saveStage = "프로젝트 정보 준비";

                // 현재 프로젝트 정보 가져오기 (OpenProject.cs 컨벤션에 맞춰서)
                string[][] currentProject = Program.DB.querySQL(DB.type.ProjListDB, "SELECT ID, pnum, title, type, date FROM projects WHERE current = 1");
                if (currentProject.Length == 0)
                {
                    MessageBox.Show("현재 프로젝트 정보를 찾을 수 없습니다.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // 프로젝트 정보를 JSON으로 생성
                var projectInfo = new
                {
                    ID = currentProject[0][0],
                    pnum = currentProject[0][1],
                    title = currentProject[0][2],
                    type = currentProject[0][3],
                    date = currentProject[0][4]
                };
                string jsonContent = JsonSerializer.Serialize(projectInfo, new JsonSerializerOptions { WriteIndented = true });

                // 임시 info.json 파일 생성
                tempInfoPath = Path.Combine(Path.GetTempPath(), "project_info_" + Guid.NewGuid().ToString("N") + ".json");
                File.WriteAllText(tempInfoPath, jsonContent);

                // 압축할 파일들 찾기
                string[] extensions = { ".sqlite", ".json", ".tree" };
                List<string> filesToZip = new List<string>();
                List<string> fileEntries = new List<string>();

                // 생성한 info.json 파일도 포함
                filesToZip.Add(tempInfoPath);
                fileEntries.Add("project_info.json");

                for (int i = 0; i < extensions.Length; i++)
                {
                    string ext = extensions[i];
                    string filePath = Path.Combine(projectsPath, ProjectList.CurProjID + ext);
                    if (File.Exists(filePath))
                    {
                        filesToZip.Add(filePath);
                        fileEntries.Add(Path.GetFileName(filePath));
                    }
                }

                // 프로젝트 폴더도 포함 (폴더 구조 유지)
                string projectFolder = Path.Combine(projectsPath, ProjectList.CurProjID);
                if (Directory.Exists(projectFolder))
                {
                    string[] projectFiles = Directory.GetFiles(projectFolder, "*.*", SearchOption.AllDirectories);
                    for (int i = 0; i < projectFiles.Length; i++)
                    {
                        string file = projectFiles[i];
                        filesToZip.Add(file);
                        // projects 폴더를 기준으로 한 상대 경로 생성
                        string relativePath = Path.GetRelativePath(projectsPath, file);
                        fileEntries.Add(relativePath);
                    }
                }

                if (filesToZip.Count == 0)
                {
                    MessageBox.Show("압축할 프로젝트 파일들을 찾을 수 없습니다.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // 저장할 ZFX 파일 경로 선택
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "ZFX 파일 (*.zfx)|*.zfx";
                    saveFileDialog.Title = "프로젝트 파일 압축 저장";
                    saveFileDialog.FileName = ProjectList.CurProjID + "_backup.zfx";

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        string zipPath = saveFileDialog.FileName;
                        saveStage = "프로젝트 DB 백업 사본 저장";
                        tempDbPath = Path.Combine(projectsPath, "." + ProjectList.CurProjID + "." + Guid.NewGuid().ToString("N") + ".sqlite");
                        Program.DB.SaveProjectCopy(tempDbPath);
                        saveStage = "프로젝트 압축";
                        tempZipPath = Path.Combine(Path.GetDirectoryName(zipPath), "." + Path.GetFileName(zipPath) + "." + Guid.NewGuid().ToString("N") + ".tmp");

                        // ZIP 파일 생성
                        using (ZipArchive zip = ZipFile.Open(tempZipPath, ZipArchiveMode.Create))
                        {
                            for (int i = 0; i < filesToZip.Count; i++)
                            {
                                string file = filesToZip[i];
                                string entryName = fileEntries[i];
                                // 사용 중인 원본 DB 대신 메모리 DB에서 저장한 사본을 압축한다.
                                if (file == projectDbPath) { file = tempDbPath; }
                                zip.CreateEntryFromFile(file, entryName);
                            }
                        }

                        saveStage = "백업 파일 교체";
                        if (File.Exists(zipPath)) { File.Replace(tempZipPath, zipPath, null); }
                        else { File.Move(tempZipPath, zipPath); }
                        tempZipPath = null;

                        MessageBox.Show($"프로젝트 파일이 성공적으로 저장되었습니다.\n저장 위치: {zipPath}", "완료", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

                // 임시 파일 정리
                try
                {
                    if (File.Exists(tempInfoPath))
                    {
                        File.Delete(tempInfoPath);
                    }
                }
                catch { }
            }
            catch (Exception ex)
            {
                // 예외 발생 시에도 임시 파일 정리
                try
                {
                    if (File.Exists(tempInfoPath))
                    {
                        File.Delete(tempInfoPath);
                    }
                }
                catch { }

                string reason = ex.Message;
                int errorCode = ex.HResult & 65535;
                if (ex is UnauthorizedAccessException || (ex is IOException && errorCode == 5)) { reason = "폴더 또는 파일에 쓰기 권한이 없거나 읽기 전용입니다. 쓰기 가능한 위치를 선택하고 파일 속성을 확인해 주세요."; }
                else if (ex is IOException && (errorCode == 32 || errorCode == 33)) { reason = "파일을 다른 프로그램에서 사용 중입니다. 해당 파일을 사용하는 프로그램을 닫거나 다른 파일명으로 저장해 주세요."; }
                else if (ex is IOException && (errorCode == 112 || errorCode == 39)) { reason = "디스크 공간이 부족합니다. 프로젝트 폴더, 임시 폴더, 백업 저장 위치의 공간을 확인해 주세요."; }
                else if (ex is DirectoryNotFoundException) { reason = "저장 폴더가 없거나 연결이 끊겼습니다. 폴더와 네트워크 또는 외장 디스크 연결을 확인해 주세요."; }
                MessageBox.Show($"{saveStage} 중 오류가 발생했습니다:\n{reason}\n\n백업 완료 전에는 기존 ZFX를 삭제하지 않습니다.\n상세: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // 한 임시 파일의 정리가 실패해도 나머지 임시 파일을 정리한다.
                string[] temporaryPaths = { tempDbPath, tempZipPath, tempInfoPath };
                for (int i = 0; i < temporaryPaths.Length; i++)
                {
                    try
                    {
                        if (temporaryPaths[i] != null && File.Exists(temporaryPaths[i])) { File.Delete(temporaryPaths[i]); }
                    }
                    catch (Exception cleanupError) { System.Diagnostics.Debug.WriteLine("백업 임시 파일 정리 오류: " + temporaryPaths[i] + " " + cleanupError.Message); }
                }
            }
        }
    }
}
