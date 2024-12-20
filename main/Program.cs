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
                // create the command-line process
                var cmdProcess = new Process
                {
                    StartInfo =
                    {
                        FileName = "cmd.exe",
                        UseShellExecute = false,
                        CreateNoWindow = true, // this is probably optional
                        ErrorDialog = false, // this is probably optional
                        RedirectStandardOutput = true,
                        RedirectStandardInput = true
                    }
                };

                // register for the output (for reading the output)
                cmdProcess.OutputDataReceived += (object sender, DataReceivedEventArgs e) =>
                {
                    string output = e.Data;
                    // inspect the output text here ...
                };

                // start the cmd process
                cmdProcess.Start();
                cmdProcess.BeginOutputReadLine();

                // execute your command
                cmdProcess.StandardInput.WriteLine("node node_modules\\pm2\\bin\\pm2 start app.js");

                //                ProcessStartInfo startInfo = new ProcessStartInfo();
                //              startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                //            startInfo.FileName = "start.bat";
                //          Process.Start(startInfo);
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
            var cmdProcess = new Process
            {
                StartInfo =
                {
                    FileName = "cmd.exe",
                    UseShellExecute = false,
                    CreateNoWindow = true, // this is probably optional
                    ErrorDialog = false, // this is probably optional
                    RedirectStandardOutput = true,
                    RedirectStandardInput = true
                }
            };

            // register for the output (for reading the output)
            cmdProcess.OutputDataReceived += (object sender, DataReceivedEventArgs e) =>
            {
                string output = e.Data;
                // inspect the output text here ...
            };

            // start the cmd process
            cmdProcess.Start();
            cmdProcess.BeginOutputReadLine();

            // execute your command
            cmdProcess.StandardInput.WriteLine("node node_modules\\pm2\\bin\\pm2 stop app.js");
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