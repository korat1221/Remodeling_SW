using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Web.WebView2.WinForms;
using System.Drawing;

namespace main.info
{
    public class SlideLoader
    {
        private WebView2 webView;
        private FlowLayoutPanel folderPanel;
        private Button btnNext, btnPrevious;
        private int currentSlideIndex = 0;
        private List<string> currentSlides = new List<string>();
        private Button lastClickedButton = null;

        public SlideLoader(WebView2 webView, FlowLayoutPanel folderPanel, Button btnNext, Button btnPrevious)
        {
            this.webView = webView;
            this.folderPanel = folderPanel;
            this.btnNext = btnNext;
            this.btnPrevious = btnPrevious;
        }

        public void LoadFolders(string basePath)
        {
            folderPanel.Controls.Clear(); // 기존 버튼 제거

            if (Directory.Exists(basePath))
            {
                string[] directories = Directory.GetDirectories(basePath);

                if (directories.Length == 0)
                    return; // 폴더가 없는 경우

                bool firstFolderLoaded = false; // 첫 번째 폴더 자동 로드 플래그

                foreach (string dir in directories)
                {
                    string folderName = Path.GetFileName(dir);

                    Button folderButton = new Button
                    {
                        Text = folderName,
                        Width = 140,
                        Height = 40,
                        Margin = new Padding(5),
                        BackColor = Color.LightGray,
                        ForeColor = Color.Black,
                        FlatStyle = FlatStyle.Flat,
                        TextAlign = ContentAlignment.MiddleCenter
                    };

                    folderButton.FlatAppearance.BorderSize = 0;

                    folderButton.Click += (s, e) =>
                    {
                        if (lastClickedButton != null)
                        {
                            lastClickedButton.BackColor = Color.LightGray;
                            lastClickedButton.ForeColor = Color.Black;
                        }

                        folderButton.BackColor = ColorTranslator.FromHtml("#0A619E"); // 선택된 버튼 색 변경
                        folderButton.ForeColor = Color.White;
                        lastClickedButton = folderButton;

                        LoadSlidesFromFolder(dir);
                    };

                    folderPanel.Controls.Add(folderButton);

                    // 첫 번째 폴더 자동 로드
                    if (!firstFolderLoaded)
                    {
                        LoadSlidesFromFolder(dir);
                        folderButton.BackColor = ColorTranslator.FromHtml("#0A619E");
                        folderButton.ForeColor = Color.White;
                        lastClickedButton = folderButton;
                        firstFolderLoaded = true;
                    }
                }
            }
        }

        private void LoadSlidesFromFolder(string folderPath)
        {
            string[] pdfFiles = Directory.GetFiles(folderPath, "*.pdf");
            if (pdfFiles.Length > 0)
            {
                currentSlides = new List<string>(pdfFiles);
            }
            else
            {
                currentSlides = Directory.GetFiles(folderPath, "*.html").ToList();
            }
            currentSlides.Sort();
            currentSlideIndex = 0;

            if (currentSlides.Count > 0)
            {
                LoadSlide(currentSlideIndex);
            }
        }

        private void LoadSlide(int index)
        {
            if (index >= 0 && index < currentSlides.Count)
            {
                bool isPdf = string.Equals(Path.GetExtension(currentSlides[index]), ".pdf", StringComparison.OrdinalIgnoreCase);
                string documentUrl = new Uri(Path.GetFullPath(currentSlides[index])).AbsoluteUri;
                if (isPdf)
                {
                    documentUrl += "#toolbar=0&navpanes=0&view=Fit";
                }
                webView.Source = new Uri(documentUrl);

                btnPrevious.Enabled = index > 0;
                btnNext.Enabled = index < currentSlides.Count - 1;
            }
        }

        public void ShowPreviousSlide()
        {
            if (currentSlideIndex > 0)
            {
                currentSlideIndex--;
                LoadSlide(currentSlideIndex);
            }
        }

        public void ShowNextSlide()
        {
            if (currentSlideIndex < currentSlides.Count - 1)
            {
                currentSlideIndex++;
                LoadSlide(currentSlideIndex);
            }
        }
    }
    public partial class SlideViewer : Form
    {
        private SlideLoader slideLoader;

        public SlideViewer(string basePath)
        {
            this.Text = "ZEROFIX Manual";
            this.StartPosition = FormStartPosition.CenterScreen;
            Rectangle workingArea = Screen.FromControl(this).WorkingArea;
            // Manual PDF pages are 720 x 540 points (4:3); allow space for both button panels and PDF margins.
            int pageWidth = Math.Min(960, Math.Min(workingArea.Width - (this.Width - this.ClientSize.Width) - 36, (workingArea.Height - (this.Height - this.ClientSize.Height) - 136) * 4 / 3));
            this.ClientSize = new Size(pageWidth + 16, pageWidth * 3 / 4 + 116);

            FlowLayoutPanel folderPanel = new FlowLayoutPanel();
            folderPanel.Dock = DockStyle.Top;
            folderPanel.Height = 50;
            folderPanel.FlowDirection = FlowDirection.LeftToRight;
            folderPanel.Padding = new Padding(10);
            folderPanel.Margin = new Padding(0);
            folderPanel.WrapContents = false;
            folderPanel.BackColor = Color.Transparent;
            this.Controls.Add(folderPanel);

            WebView2 webView = new WebView2();
            webView.Dock = DockStyle.Fill;
            this.Controls.Add(webView);

            var panel = new Panel();
            panel.Dock = DockStyle.Bottom;
            panel.Height = 50;

            Button btnPrevious = new Button();
            btnPrevious.Text = "Previous";
            btnPrevious.Width = 100;
            btnPrevious.Left = 10;
            btnPrevious.Click += (s, e) => slideLoader.ShowPreviousSlide();

            Button btnNext = new Button();
            btnNext.Text = "Next";
            btnNext.Width = 100;
            btnNext.Left = 120;
            btnNext.Click += (s, e) => slideLoader.ShowNextSlide();

            panel.Controls.Add(btnPrevious);
            panel.Controls.Add(btnNext);
            this.Controls.Add(panel);
            webView.BringToFront();

            slideLoader = new SlideLoader(webView, folderPanel, btnNext, btnPrevious);

            this.Load += async (s, e) =>
            {
                await webView.EnsureCoreWebView2Async();
                slideLoader.LoadFolders(basePath);
            };
        }
    }
}