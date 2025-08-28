using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Data.SQLite;
using System.Windows.Forms;

namespace main
{
    internal static class Program
    {
        [DllImport("shell32.dll")]
        static extern bool SHGetSpecialFolderPath(IntPtr hwndOwner, [Out] StringBuilder lpszPath, int nFolder, bool fCreate);

        [DllImport("user32.dll")]
        private static extern bool SetProcessDpiAwarenessContext(IntPtr dpiFlag);

        private static readonly IntPtr DPI_AWARENESS_CONTEXT_UNAWARE = new IntPtr(-1);
        private static readonly IntPtr DPI_AWARENESS_CONTEXT_SYSTEM_AWARE = new IntPtr(-2);
        private static readonly IntPtr DPI_AWARENESS_CONTEXT_PER_MONITOR_AWARE_V2 = new IntPtr(-4);


        private enum PROCESS_DPI_AWARENESS
        {
            Process_DPI_Unaware = 0,
            Process_System_DPI_Aware = 1,
            Process_Per_Monitor_DPI_Aware = 2
        }


        public const string VIRTUAL_PATH_NAME = "ZEROFIX";

        public static String? gPath;
        public static DB DB = new DB();
        public static UTIL UTIL = new UTIL();
        public static CALC CALC = new CALC();

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                SetProcessDpiAwarenessContext(DPI_AWARENESS_CONTEXT_UNAWARE);
            }
            catch
            {
            }

#if !DEBUG
            gPath = get_virtual_store_path();

            String str = System.Reflection.Assembly.GetEntryAssembly().Location;

            str = str.Substring(0, str.LastIndexOf('\\') + 1);

            if (gPath + "net8.0-windows7.0\\" != str)
            {
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = gPath + "net8.0-windows7.0\\main.exe";
                Process.Start(startInfo);
                Application.Exit();
                return;
            }
#else
            String str = System.Reflection.Assembly.GetEntryAssembly().Location;
            gPath = str.Substring(0, str.IndexOf("\\main") + 1) + "asset\\";
#endif

            Directory.SetCurrentDirectory(gPath);
            //            Directory.SetCurrentDirectory(gPath + "threejs\\");
            {
                // PM2 서버 상태 확인 후 시작 또는 재시작
                var checkStatusInfo = new ProcessStartInfo
                {
                    FileName = "node",
                    Arguments = "node_modules\\pm2\\bin\\pm2 list --format json",
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    WindowStyle = ProcessWindowStyle.Hidden,
                    RedirectStandardOutput = true
                };

                var checkProcess = Process.Start(checkStatusInfo);
                string output = checkProcess.StandardOutput.ReadToEnd();
                checkProcess.WaitForExit();

                bool isRunning = output.Contains("\"name\":\"app\"") && output.Contains("\"status\":\"online\"");

                var startInfo = new ProcessStartInfo
                {
                    FileName = "node",
                    Arguments = isRunning ? "node_modules\\pm2\\bin\\pm2 restart app.js" : "node_modules\\pm2\\bin\\pm2 start app.js",
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    WindowStyle = ProcessWindowStyle.Hidden
                };

                Process.Start(startInfo);
            }
            //            Directory.SetCurrentDirectory(gPath);

            //  CALC.init();

            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new FormMain());
        }
        public static void killServer()
        {
            Directory.SetCurrentDirectory(gPath);

            // PM2 서버 중지
            var startInfo = new ProcessStartInfo
            {
                FileName = "node",
                Arguments = "node_modules\\pm2\\bin\\pm2 stop app.js",
                UseShellExecute = false,
                CreateNoWindow = true,
                WindowStyle = ProcessWindowStyle.Hidden
            };

            Process.Start(startInfo);
            //            Process[] processes = Process.GetProcessesByName("node");
            //          Process currentProcess = Process.GetCurrentProcess();

            //        foreach (Process proc in processes)
            //       {
            //         if (proc.Id != currentProcess.Id)
            //           proc.Kill();
            // }
        }

        public static string get_virtual_store_path()
        {
            StringBuilder s = new StringBuilder(260);

            SHGetSpecialFolderPath(IntPtr.Zero, s, 0x001c, false);
            String path = s.ToString() + "\\" + VIRTUAL_PATH_NAME + "\\";

            System.IO.Directory.CreateDirectory(path);

            return path;
        }

        public static MainContents getMenuForm()
        {
            foreach (FormMain openForm in Application.OpenForms)
            {
                if (openForm.Name == "FormMain")
                {
                    return (MainContents)openForm.splitContainer1.Panel1.Controls[0];
                }
            }
            return null;
        }
    }
}