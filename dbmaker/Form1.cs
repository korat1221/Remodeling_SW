using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using System.Data.Entity;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Collections.ObjectModel;
using System.Data.Common;

namespace dbmaker
{
    public partial class Form1 : Form
    {
        String gPath = "..\\..\\..\\asset\\";
        String DBName;
        private string PASSWORD = "abcd";
        public Form1()
        {
            InitializeComponent();

            DBName_comboBox.Items.Add("basedb_hcneed");
            DBName_comboBox.Items.Add("basedb_lighting");
            DBName_comboBox.Items.Add("basedb_heating");
            DBName_comboBox.Items.Add("basedb_cooling");
        }

        private void DBName_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            DBName = DBName_comboBox.SelectedItem.ToString();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        public static string executeSQL(string exec, string query)
        {
            String ret = "";
            using (var dbConn = new SQLiteConnection(@"Data Source=..\\..\\..\\asset\\basedb.sqlite"))
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

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            if (DBName == "")
            {
                MessageBox.Show("DB명을 입력하세요.");
            }
            else
            {
                using (var dbConn = new SQLiteConnection(@"Data Source=..\\..\\..\\asset\\" + DBName + ".sqlite"))
                {
                    dbConn.Open();

                    SQLiteCommand cmd = new SQLiteCommand();
                    cmd.Connection = dbConn;

                    cmd.CommandText = "PRAGMA synchronous=OFF";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "PRAGMA journal_mode=OFF";
                    cmd.ExecuteNonQuery();

                    if (table.Text == "")
                    {
                        MessageBox.Show("테이블명을 입력하세요.");
                    }
                    else if (columns.Text == "")
                    {
                        MessageBox.Show("컬럼 리스트를 입력하세요(쉼표로 구분).");
                    }
                    else
                    {
                        try
                        {
                            string sql = "";
                            string[] columns = this.columns.Text.Split(',');

                            try
                            {
                                cmd.CommandText = "DROP TABLE " + table.Text;
                                cmd.ExecuteNonQuery();
                            }
                            catch (Exception)
                            {
                            }

                            foreach (string column in columns)
                            {
                                if (sql != "") sql += ",";
                                sql += column + " VARCHAR (255)";
                            }

                            cmd.CommandText = "CREATE TABLE " + table.Text + " (ID INTEGER PRIMARY KEY AUTOINCREMENT," + sql + ")";
                            cmd.ExecuteNonQuery();

                            int n = 1, i, j, cnt;
                            StreamReader sr = new StreamReader(openFileDialog1.FileName, Encoding.Default);
                            List<string[]> headerColumns = new List<string[]>();

                            while (!sr.EndOfStream)
                            {
                                string line = sr.ReadLine(), s;
                                string[] data = line.Split('\t');

                                if (data.Count() > 1)
                                {
                                    if (startLine.Value <= n && endLine.Value >= n)
                                    {
                                        headerColumns.Add(data);
                                    }
                                    else if (endLine.Value < n)
                                    {
                                        if (headerColumns.Count() > 0)
                                        {
                                            i = (int)startColumn.Value - 1;
                                            cnt = (int)endColumn.Value - (int)startColumn.Value + 1;

                                            while (i < data.Count())
                                            {
                                                sql = "";
                                                j = -1;
                                                while (++j < startColumn.Value - 1)
                                                {
                                                    if (sql != "") sql += ",";
                                                    sql += "'" + data[j] + "'";
                                                }

                                                j = -1;
                                                while (++j < cnt)
                                                {
                                                    if (sql != "") sql += ",";
                                                    sql += "'" + headerColumns[j][i] + "'";
                                                }
                                                if (sql != "") sql += ",";
                                                sql += "'" + data[i] + "'";

                                                cmd.CommandText = "INSERT INTO " + table.Text + " (" + String.Join(",", columns) + ") VALUES (" + sql + ")";
                                                cmd.ExecuteNonQuery();
                                                i++;
                                            }
                                        }
                                        else
                                        {
                                            if (n >= startRow.Value)
                                            {
                                                sql = "";
                                                i = -1;
                                                while (++i < data.Count() && i < columns.Count())
                                                {
                                                    if (sql != "") sql += ",";

                                                    s = data[i];
                                                    s = s.Replace("\"", "");
                                                    sql += "'" + s + "'";
                                                }

                                                cmd.CommandText = "INSERT INTO " + table.Text + " (" + String.Join(",", columns) + ") VALUES (" + sql + ")";
                                                cmd.ExecuteNonQuery();
                                            }
                                        }
                                    }
                                }

                                n++;
                                // 결과를 출력해본다.

                                //                Console.WriteLine("{0}, {1}, {2}, ... ", data[0], data[1], data[2], ... );

                            }

                            if (dbConn.State != System.Data.ConnectionState.Closed) dbConn.Close();

                            MessageBox.Show(DBName + ".sqlite 에 테이블을 생성(갱신)하였습니다. asset 폴더의 " + DBName + ".sqlite 에서 확인하시기 바랍니다.");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AppDomain.CurrentDomain.SetData(string.Format(
                "462734ae-de3e-43e1-9eab-fb5282b94c59_{0}",
                System.Diagnostics.Process.GetCurrentProcess().Id),
                "IPAZEB <info@ipazeb.org>");

            SQLiteCommand.Execute(
                "PRAGMA activate_extensions='see-7bb07b8d471d642e';",
                SQLiteExecuteType.NonQuery,
                "Data Source=:memory:;");

            SQLiteCommand.Execute( /* SELF-TEST */
                "SELECT COUNT(*) FROM sqlite_schema;",
                SQLiteExecuteType.Scalar,
                "Data Source=:memory:;Password=1234;");

            /*
             * NOTE: Use only the file name here, which indicates
             *       that an embedded assembly resource is being
             *       used.
             */
            Environment.SetEnvironmentVariable(
                "Override_SEE_Certificate", "SDS-SEE.exml");

            String str = System.Reflection.Assembly.GetEntryAssembly().Location;

            Directory.SetCurrentDirectory(str.Substring(0, str.IndexOf("\\dbmaker") + 1) + "asset\\");

            using (SQLiteConnection conn = new SQLiteConnection("Data Source=" + textBox1.Text.ToString() + ".sqlite;"))
            {
                conn.SetPassword(PASSWORD);
                conn.Open();

                using (SQLiteCommand cmd = new SQLiteCommand(conn))
                {
                    cmd.CommandText = File.ReadAllText(textBox1.Text.ToString() + ".sql");
                    int i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        MessageBox.Show("Data dumped successfully ...!!!");
                    }
                }

                conn.Close();
                conn.Dispose();
            }
        }
    }
}
