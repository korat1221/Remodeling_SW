using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Data.SQLite;

namespace main
{
    internal static class Program
    {
        [DllImport("shell32.dll")]
        static extern bool SHGetSpecialFolderPath(IntPtr hwndOwner, [Out] StringBuilder lpszPath, int nFolder, bool fCreate);

        public const string VIRTUAL_PATH_NAME = "EnergyCalc";

        public static String? gPath;

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

            if (gPath + "net6.0-windows\\" != str)
            {
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = gPath + "net6.0-windows\\main.exe";
                Process.Start(startInfo);
                Application.Exit();
                return;
            }
#else
            gPath = "C:\\Users\\아이템즈A\\AppData\\Local\\" + VIRTUAL_PATH_NAME + "\\";
#endif

            Directory.SetCurrentDirectory(gPath + "threejs\\");
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
                cmdProcess.StandardInput.WriteLine("node app.js");

                //                ProcessStartInfo startInfo = new ProcessStartInfo();
                //              startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                //            startInfo.FileName = "start.bat";
                //          Process.Start(startInfo);
            }

            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
        }
        public static void killServer()
        {
            Process[] processes = Process.GetProcessesByName("node");
            Process currentProcess = Process.GetCurrentProcess();

            foreach (Process proc in processes)
            {
                if (proc.Id != currentProcess.Id)
                    proc.Kill();
            }
        }

        public static string get_virtual_store_path()
        {
            StringBuilder s = new StringBuilder(260);

            SHGetSpecialFolderPath(IntPtr.Zero, s, 0x001c, false);
            String path = s.ToString() + "\\" + VIRTUAL_PATH_NAME + "\\";

            System.IO.Directory.CreateDirectory(path);

            return path;
        }

        public static string executeSQL(string exec, string query)
        {
            String ret = "";

            using (var dbConn = new SQLiteConnection(@"Data Source=" + gPath + "database.sqlite"))
            {
                dbConn.Open();
                if (exec != "")
                {
                    SQLiteCommand cmd = new SQLiteCommand(exec, dbConn);
                    cmd.ExecuteNonQuery();
                }
                if (query != "")
                {
                    SQLiteCommand cmd = new SQLiteCommand(query, dbConn);
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        string json = string.Empty;
                        List<object> objects = new List<object>();

                        while (reader.Read())
                        {
                            IDictionary<string, object> record = new Dictionary<string, object>();
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                record.Add(reader.GetName(i), reader[i]);
                            }
                            objects.Add(record);
                        }
//                        ret = JsonConvert.SerializeObject(objects);
                    }
                }
                if (dbConn.State != System.Data.ConnectionState.Closed) dbConn.Close();
            }

            return ret;
        }
    }
}