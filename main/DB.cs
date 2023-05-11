using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.Objects;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

/* 
 * DB 사용법
 * 
 * 1. 어느 폼에서든 Program.DB.openDB(프로젝트명); 을 실행하면 신규 DB 셋을 열수 있다. 이때 사용중인 DB 는 자동 종료된다.
 * 
 *    예시) Program.DB.openDB("test.sqlite");
 *    
 * 2. DB 셋이 열린 상태에서는 어느 폼에서든 Program.DB.setValue(DB유형, 테이블명, 필드리스트(,로 구분), 저장값(,로 구분), 키필드); 를 실행하여 값을 저장한다.
 *    이때 db 유형은 DB.type.ProjDB (프로젝트db), DB.type.BaseDB (기초db), DB.type.CalcDB (계산db) 이다.
 *    
 *    예시) Program.DB.setValue(DB.type.ProjDB, "연습테이블2", "연습필드3,연습필드4", "'4','3333'", "연습필드3");
 *    
 * 3. DB 셋이 열린 상태에서는 어느 폼에서든 Program.DB.getValue(DB유형, 테이블명, 필드리스트(,로 구분), 조건); 를 실행하여 값을 불러온다. 
 *    이때 값은 string[][] 의 2차원 문자열 배열로 반환된다.
 *    
 *    예시) string[][] res = Program.DB.getValue(DB.type.ProjDB, "연습테이블2", "연습필드4", "연습필드3 = '4'");
 *    
 * 4. DB 셋 중 프로젝트DB 는 1. 실행시 없으면 자동 생성된다. 이때 같이 생성되어야할 테이블들은 아래의 tables 변수의 SQL 문들이다.
 *    테이블들은 Program.DB.setValue 실행시 없으면 자동 생성된다.
 *    
 * 5. DB 셋은 프로그램 실행시 항상 오픈되어 있으므로 프로그램 실행시에는 외부 프로그램이 DB 셋에 변경값을 조회할수 없고 프로그램 종료후에 가능하다. 
 * 
 */

namespace main
{
    internal class DB
    {
        public enum type
        {
            BaseDB,
            ProjDB,
            CalcDB
        }
        private Dictionary<string, string> tables = new Dictionary<string, string>()
        {
            {"연습테이블1", "CREATE TABLE 연습테이블1 (ID INTEGER PRIMARY KEY AUTOINCREMENT, 연습필드1 VARCHAR (255), 연습필드2 VARCHAR (255))"},
            {"연습테이블2", "CREATE TABLE 연습테이블2 (ID INTEGER PRIMARY KEY AUTOINCREMENT, 연습필드3 VARCHAR (255), 연습필드4 VARCHAR (255))"},
            {"연습테이블3", "CREATE TABLE 연습테이블3 (ID INTEGER PRIMARY KEY AUTOINCREMENT, 연습필드5 VARCHAR (255), 연습필드6 VARCHAR (255))"},
        };

        private SQLiteConnection? baseDB, projDB, calcDB;
        public bool openDB(string projPath)
        {
            closeDB();

            SQLiteCommand cmd = new SQLiteCommand();

            if (GetFileSize("basedb.sqlite") > 0)
            {
                baseDB = new SQLiteConnection(@"Data Source=basedb.sqlite");
                baseDB.Open();

                if (baseDB.State != ConnectionState.Open)
                {
                    return false;
                }

                cmd.Connection = baseDB;
                cmd.CommandText = "PRAGMA synchronous=OFF";
                cmd.ExecuteNonQuery();
                cmd.CommandText = "PRAGMA journal_mode=OFF";
                cmd.ExecuteNonQuery();
            }
            else
            {
                return false;
            }

            if (GetFileSize(projPath) <= 0)
            {
                File.Copy("templ.sqlite", projPath, true);
            }

            projDB = new SQLiteConnection(@"Data Source=" + projPath);
            projDB.Open();

            if (projDB.State != ConnectionState.Open)
            {
                baseDB.Close();
                baseDB.Dispose();

                return false;
            }

            cmd.Connection = projDB;
            cmd.CommandText = "PRAGMA synchronous=OFF";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "PRAGMA journal_mode=OFF";
            cmd.ExecuteNonQuery();

            calcDB = new SQLiteConnection(@"Data Source=:memory:");
            calcDB.Open();
            if (calcDB.State != ConnectionState.Open)
            {
                baseDB.Close();
                baseDB.Dispose();
                projDB.Close();
                projDB.Dispose();
                return false;
            }

            return true;
        }
        public void closeDB()
        {
            if (baseDB != null)
            {
                baseDB.Close();
                baseDB.Dispose();
            }

            if(projDB != null)
            {
                projDB.Close();
                projDB.Dispose();
            }

            if (calcDB != null)
            {
                calcDB.Close();
                calcDB.Dispose();
            }
        }
        public void executeSQL(type dbType, string exec)
        {
            if (exec != "")
            {
                switch (dbType)
                {
                    case type.BaseDB:
                        {
                            SQLiteCommand cmd = new SQLiteCommand(exec, baseDB);
                            cmd.ExecuteNonQuery();
                        }
                        break;
                    case type.ProjDB:
                        {
                            SQLiteCommand cmd = new SQLiteCommand(exec, projDB);
                            cmd.ExecuteNonQuery();
                        }
                        break;
                    case type.CalcDB:
                        {
                            SQLiteCommand cmd = new SQLiteCommand(exec, calcDB);
                            cmd.ExecuteNonQuery();
                        }
                        break;
                }
            }
        }
        public string[][] querySQL(type dbType, string query)
        {
            SQLiteCommand cmd = new SQLiteCommand();
            List<string[]> objects = new List<string[]>();

            if (query != "")
            {
                switch (dbType)
                {
                    case type.BaseDB:
                        cmd.Connection = baseDB;
                        break;
                    case type.ProjDB:
                        cmd.Connection = projDB;
                        break;
                    case type.CalcDB:
                        cmd.Connection = calcDB;
                        break;
                }

                cmd.CommandText = query;

                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    string json = string.Empty;

                    while (reader.Read())
                    {
                        string[] rec = new string[reader.FieldCount];

                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            rec[i] = reader[i].ToString();
                        }
                        objects.Add(rec);
                    }
                }
            }
            return objects.ToArray();
        }
        private long GetFileSize(string filePath)
        {
            long fileSize = 0;
            if (File.Exists(filePath))
            {
                FileInfo info = new FileInfo(filePath);
                fileSize = info.Length;
            }

            return fileSize;
        }
        public void createTable(type dbType, string name, string exec)
        {
            if (exec != "")
            {
                SQLiteCommand? cmd = null;

                switch (dbType)
                {
                    case type.ProjDB:
                        cmd = new SQLiteCommand(projDB);
                        break;
                    case type.CalcDB:
                        cmd = new SQLiteCommand(calcDB);
                        break;
                }

                if (cmd != null)
                {
                    bool found = false;
                    cmd.CommandText = "SELECT name FROM sqlite_master WHERE type='table' AND name='" + name + "';";
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            found = true;
                        }
                    }

                    if (!found)
                    {
                        cmd.CommandText = exec;
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        public void setValue(type dbType, string table, string columns, string values, string key_column)
        {
            try
            {
                createTable(dbType, table, tables[table]);

                string[] cols = columns.Split(',');
                string[] vals = values.Split(',');

                SQLiteCommand cmd = new SQLiteCommand();

                switch (dbType)
                {
                    case type.ProjDB:
                        cmd.Connection = projDB;
                        break;
                    case type.CalcDB:
                        cmd.Connection = calcDB;
                        break;
                }

                string condition = "";
                int n = Array.FindIndex(cols, el => el == key_column);

                if (n >= 0)
                {
                    string cond = cols[n] + " = " + vals[n];
                    cmd.CommandText = "SELECT * FROM " + table + " WHERE " + cond;

                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read() && reader.HasRows)
                        {
                            condition = cond;
                        }
                    }
                }

                if (condition == "")
                {
                    cmd.CommandText = "INSERT INTO " + table + " (" + columns + ") VALUES (" + values + ")";
                }
                else
                {
                    int i = -1;
                    string upd = "";

                    cmd.CommandText = "UPDATE " + table + " SET ";

                    while (++i < cols.Length)
                    {
                        if (upd != "") upd += ",";
                        upd += cols[i] + "=" + vals[i];
                    }

                    cmd.CommandText += upd + " WHERE " + condition;
                }
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public string[][] getValue(type dbType, string table, string columns, string conditions)
        {
            SQLiteCommand cmd = new SQLiteCommand();
            List<string[]> objects = new List<string[]>();

            switch (dbType)
            {
                case type.BaseDB:
                    cmd.Connection = baseDB;
                    break;
                case type.ProjDB:
                    cmd.Connection = projDB;
                    break;
                case type.CalcDB:
                    cmd.Connection = calcDB;
                    break;
            }

            cmd.CommandText = "SELECT " + columns + " FROM " + table + " WHERE " + conditions;

            using (SQLiteDataReader reader = cmd.ExecuteReader())
            {
                string json = string.Empty;

                while (reader.Read())
                {
                    string[] rec = new string[reader.FieldCount];

                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        rec[i] = reader[i].ToString();
                    }
                    objects.Add(rec);
                }
            }

            return objects.ToArray();
        }
    }
}



