using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Threading;

namespace Control
{
    public static class DbAccess
    {
        private static Dictionary<string, string> sqlfile = new Dictionary<string, string>();
        public static object obj;
        static DbAccess()
        {
            obj = new object();
            var files = Directory.GetFiles("sql", "*.sql");
            foreach (var itr in files)
            {
                var name = Path.GetFileNameWithoutExtension(itr);
                sqlfile.Add(name, File.ReadAllText(itr));
            }
        }

        static string GetSql(string name)
        {
            var rst = string.Empty;
            sqlfile.TryGetValue(name, out rst);
            return rst;
        }

        public static DataTable GetSqlResult(string filename, Dictionary<string, string> parameter)
        {
            var sql = GetSql(filename);
            if (string.IsNullOrEmpty(sql))
                return null;
            Monitor.Enter(obj);
            var rst = DBHelper.ExecuteDataTable(sql, parameter);
            Monitor.Exit(obj);
            return rst;
        }

        public static int InsertOrUpdate(string filename, Dictionary<string, string> parameter)
        {
            var sql = GetSql(filename);
            if (string.IsNullOrEmpty(sql))
                return 0;
            Monitor.Enter(obj);
            var rst = DBHelper.ExecuteNonQuery(sql, parameter);
            Monitor.Exit(obj);
            return rst;
        }
    }
}