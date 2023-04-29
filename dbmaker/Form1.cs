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

namespace dbmaker
{
    public partial class Form1 : Form
    {
        String gPath = "..\\..\\..\\asset\\";

        public Form1()
        {
            InitializeComponent();
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
            using (var dbConn = new SQLiteConnection(@"Data Source=..\\..\\..\\asset\\basedb.sqlite"))
            {
                dbConn.Open();

                SQLiteCommand cmd = new SQLiteCommand();
                cmd.Connection = dbConn;

                cmd.CommandText = "PRAGMA synchronous=OFF";
                cmd.ExecuteNonQuery();
                cmd.CommandText = "PRAGMA journal_mode=OFF";
                cmd.ExecuteNonQuery();

                if (textBox1.Text == "")
                {
                    MessageBox.Show("테이블명을 입력하세요.");
                }
                else if (textBox2.Text == "") 
                {
                    MessageBox.Show("컬럼 리스트를 입력하세요(쉼표로 구분).");
                }
                else
                {
                    try
                    {
                        cmd.CommandText = "DROP TABLE " + textBox1.Text;
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception)
                    {
                    }

                    string[] columns = textBox2.Text.Split(',');
                    string sql = "";

                    foreach (string column in columns)
                    {
                        if (sql != "") sql += ",";
                        sql += column + " VARCHAR (255)";
                    }

                    cmd.CommandText = "CREATE TABLE " + textBox1.Text + " (ID INTEGER PRIMARY KEY," + sql + ")";
                    cmd.ExecuteNonQuery();

                    StreamReader sr = new StreamReader(openFileDialog1.FileName);

                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadLine();
                        string[] data = line.Split(',');

                        // 결과를 출력해본다.

                        //                Console.WriteLine("{0}, {1}, {2}, ... ", data[0], data[1], data[2], ... );

                    }

                    if (dbConn.State != System.Data.ConnectionState.Closed) dbConn.Close();

                    MessageBox.Show("basedb.sqlite 에 테이블을 생성(갱신)하였습니다. asset 폴더의 basedb.sqlite 에서 확인하시기 바랍니다.");
                }
            }
        }
    }
}
