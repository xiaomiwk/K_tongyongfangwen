using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;

namespace Utility.存储
{
    public static class SQLHelper
    {
        public static int ExecuteNonQuery(DbConnection __连接, string __命令内容, DbParameter[] __命令参数 = null, DbTransaction __事务 = null, CommandType __命令类型 = CommandType.Text)
        {
            using (var __命令 = __连接.CreateCommand())
            {
                设置命令(__连接, __命令, __命令内容, __命令类型, __命令参数, __事务);
                return __命令.ExecuteNonQuery();
            }
        }

        public static object ExecuteScalar(DbConnection __连接, string __命令内容, DbParameter[] __命令参数 = null, DbTransaction __事务 = null, CommandType __命令类型 = CommandType.Text)
        {
            using (var __命令 = __连接.CreateCommand())
            {
                设置命令(__连接, __命令, __命令内容, __命令类型, __命令参数, __事务);
                return __命令.ExecuteScalar();
            }
        }

        public static DbDataReader ExecuteReader(DbConnection __连接, string __命令内容, DbParameter[] __命令参数 = null, DbTransaction __事务 = null, CommandType __命令类型 = CommandType.Text)
        {
            using (var __命令 = __连接.CreateCommand())
            {
                设置命令(__连接, __命令, __命令内容, __命令类型, __命令参数, __事务);
                return __命令.ExecuteReader();
            }
        }

        private static void 设置命令(DbConnection __连接, DbCommand __命令, string __命令内容, CommandType __命令类型, IEnumerable<DbParameter> __命令参数 = null, DbTransaction __事务 = null)
        {
            if (__连接.State != ConnectionState.Open)
                __连接.Open();

            __命令.Connection = __连接;
            __命令.CommandText = __命令内容;
            __命令.CommandType = __命令类型;

            if (__事务 != null)
            {
                __命令.Transaction = __事务;
            }

            if (__命令参数 != null)
            {
                foreach (var parm in __命令参数)
                {
                    __命令.Parameters.Add(parm);
                }
            }
        }

    }
}
