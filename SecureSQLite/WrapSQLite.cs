using System.Runtime.InteropServices;

namespace SecureSQLite
{
    public class WrapSQLite
    {
        [DllImport("wrap_sqlite.dll")]
        static public extern string OpenDB(string sql, int idx);
        [DllImport("wrap_sqlite.dll")]
        static public extern int CloseDB(int idx);
        [DllImport("wrap_sqlite.dll")]
        static public extern int SaveDB(string path, int idx);
        [DllImport("wrap_sqlite.dll")]
        static public extern string QuerySQL(int idx, string sql);
        [DllImport("wrap_sqlite.dll")]
        static public extern int ExecuteSQL(int idx, string sql);
    }
}
