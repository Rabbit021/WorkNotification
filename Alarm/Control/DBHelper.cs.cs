using System;
using System.Collections.Generic;
using System.Threading;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using System.Linq;

namespace Control
{
    public class DBHelper
    {
        private static readonly object obj = new object();
        private static DbConnection connection;

        public static DbConnection GetConnection()
        {
            //            if (connection != null)
            //            {
            //                if (connection.State != ConnectionState.Open)
            //                    connection.Open();
            //                return connection;
            //            }
            connection = GetFileConnection();
            return connection;
        }

        public static DbConnection GetFileConnection()
        {
            var connectionString = "data source=db.sqlite";
            var fileDb = new SQLiteConnection(connectionString);
            return fileDb;
        }
        public static void ConvertMemoryDB()
        {

        }

        public static DbDataAdapter GetDataAdapter(DbCommand cmd)
        {
            return new SQLiteDataAdapter((SQLiteCommand)cmd);
        }

        #region Ö´ÐÐsql

        public static DbCommand PrepareCommand(DbConnection conn, string cmdText,
            Dictionary<string, string> parameter)
        {
            var cmd = conn.CreateCommand();
            PrepareCommand(cmd, conn, cmdText, parameter);
            return cmd;
        }

        public static void PrepareCommand(DbCommand cmd, DbConnection conn, string cmdText,
            Dictionary<string, string> parameter)
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();
            cmd.Parameters.Clear();
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            cmd.CommandType = CommandType.Text;
            if (parameter != null && parameter.Count >= 1)
            {
                foreach (var val in parameter)
                {
                    var para = cmd.CreateParameter();
                    cmd.Parameters.Add(para);
                    para.Direction = ParameterDirection.Input;
                    para.DbType = DbType.String;
                    para.ParameterName = val.Key;
                    para.Value = val.Value;
                }
            }
        }

        public static int ExecuteNonQuery(string cmdText, Dictionary<string, string> parameter)
        {
            using (var conn = GetConnection())
            {
                using (var cmd = PrepareCommand(conn, cmdText, parameter))
                {
                    Monitor.Enter(obj);
                    var result = cmd.ExecuteNonQuery();
                    Monitor.Exit(obj);
                    return result;
                }
            }
        }

        public static int ExecuteNonQuerys(List<KeyValuePair<string, Dictionary<string, string>>> datas)
        {
            var result = 0;
            if (datas == null)
                return result;
            using (var conn = GetConnection())
            {
                using (var cmd = conn.CreateCommand())
                {
                    var trans = conn.BeginTransaction();
                    try
                    {
                        foreach (var data in datas)
                        {
                            PrepareCommand(cmd, conn, data.Key, data.Value);
                            Monitor.Enter(obj);
                            result += cmd.ExecuteNonQuery();
                            Monitor.Exit(obj);
                        }
                        trans.Commit();
                    }
                    catch (Exception exp)
                    {
                        trans.Rollback();
                        result = -1;
                    }
                }
            }
            return result;
        }

        public static DataSet ExecuteDataset(string cmdText, Dictionary<string, string> parameter)
        {
            var ds = new DataSet();
            try
            {
                using (var connection = GetConnection())
                {
                    var command = PrepareCommand(connection, cmdText, parameter);
                    var da = GetDataAdapter(command);
                    da.Fill(ds);
                }
            }
            catch (Exception e)
            {
            }
            return ds;
        }

        public static DataTable ExecuteDataTable(string cmdText, Dictionary<string, string> parameter)
        {
            var dt = new DataTable();
            var ds = ExecuteDataset(cmdText, parameter);
            if (ds == null)
                return dt;
            return ds.Tables.Count > 0 ? ds.Tables[0] : new DataTable();
        }

        public static DbDataReader GetReader(string cmdText, Dictionary<string, string> parameter)
        {
            using (var connection = GetConnection())
            {
                using (var cmd = PrepareCommand(connection, cmdText, parameter))
                {
                    var reader = cmd.ExecuteReader();
                    return reader;
                }
            }
        }

        #endregion
    }

    public static class DBHelperExt
    {
        public static string GetColumnData(this DataRow dr, DataTable dt, string key)
        {
            if (dr == null)
                return string.Empty;
            if (dt.Columns.Contains(key))
                return dr[key].ToString();
            return string.Empty;
        }
    }
}