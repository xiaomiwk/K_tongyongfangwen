using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Utility.存储
{
    public abstract class DRDB表存储<T> : ID表存储<T>
    {
        protected string _表名;
        protected string _存储标识;
        protected List<string> _字段列表;

        protected string _增加单条;
        protected string _删除单条;
        protected string _修改单条;
        protected string _查询单条;

        protected string _查询前缀;
        protected string _统计数量;
        protected string _删除所有;
        protected string _条件删除;
        protected string _条件修改;

        protected Func<T, Int64> _获取标识;

        protected Action<T, Int64> _设置标识;

        protected DRDB表存储()
        {
            var __存储标识元数据 = typeof(T).GetProperties().First(q => q.IsDefined(typeof(DBKeyAttribute), false));
            _获取标识 = (T __记录) => (Int64)__存储标识元数据.GetValue(__记录, null);
            _设置标识 = (T __记录, Int64 __标识) => __存储标识元数据.SetValue(__记录, __标识, null);
        }

        public virtual void 初始化(string __表名)
        {
        }

        protected virtual void 设置SQL()
        {
            var __SQL = new StringBuilder();

            #region 单条
            //增加单条
            __SQL.Clear();
            __SQL.AppendFormat("INSERT INTO {0}(", _表名);
            _字段列表.ForEach(q => __SQL.AppendFormat("{0},", q));
            __SQL.Remove(__SQL.Length - 1, 1).Append(") VALUES(");
            _字段列表.ForEach(q => __SQL.AppendFormat("@{0},", q));
            __SQL.Remove(__SQL.Length - 1, 1).Append(");");
            _增加单条 = __SQL.ToString();
            Debug.WriteLine("增加单条: " + _增加单条);

            //查询自增标识
            Debug.WriteLine("查询自增标识: " + 查询自增长标识());

            //删除单条
            _删除单条 = string.Format("DELETE FROM {0} WHERE {1} = @{1};", _表名, _存储标识);
            Debug.WriteLine("删除单条: " + _删除单条);

            //修改单条
            __SQL.Clear();
            __SQL.AppendFormat("UPDATE {0} SET ", _表名);
            _字段列表.ForEach(q => __SQL.AppendFormat("{0} =@{0} ,", q));
            __SQL.Remove(__SQL.Length - 1, 1).AppendFormat("WHERE {0} = @{0};", _存储标识);
            _修改单条 = __SQL.ToString();
            Debug.WriteLine("修改单条: " + _修改单条);

            //查询单条
            _查询单条 = string.Format("SELECT * FROM {0} WHERE {1} = @{1};", _表名, _存储标识);
            Debug.WriteLine("查询单条: " + _查询单条);
            #endregion

            #region 批量
            //查询前缀
            _查询前缀 = string.Format("SELECT * FROM {0} ", _表名);
            Debug.WriteLine("查询前缀: " + _查询前缀);

            //统计数量
            _统计数量 = string.Format("SELECT COUNT(*) FROM {0} WHERE 1 = 1 ", _表名);
            Debug.WriteLine("统计数量: " + _统计数量);

            //删除所有
            _删除所有 = string.Format("DELETE FROM {0};", _表名);
            Debug.WriteLine("删除所有: " + _删除所有);

            //条件删除
            _条件删除 = string.Format("DELETE FROM {0} WHERE 1 = 1 ", _表名);
            Debug.WriteLine("条件删除: " + _条件删除);

            //条件修改
            _条件修改 = string.Format("UPDATE {0} SET {{0}} WHERE 1 = 1 ", _表名);
            Debug.WriteLine("条件修改: " + _条件修改);
            #endregion
        }

        public void 增加(T __记录)
        {
            using (var __连接 = 创建连接())
            {
                //var __标识 = SQLHelper.ExecuteNonQuery(__连接, _增加单条 + 查询自增长标识(), 创建参数(__记录));
                SQLHelper.ExecuteNonQuery(__连接, _增加单条, 创建参数(__记录));
                var __标识 = SQLHelper.ExecuteNonQuery(__连接, 查询自增长标识());
                _设置标识(__记录, __标识);
            }
        }

        protected abstract string 查询自增长标识();

        public void 增加(List<T> __记录集)
        {
            using (var __连接 = 创建连接())
            {
                using (var __事务 = __连接.BeginTransaction())
                {
                    try
                    {
                        __记录集.ForEach(__记录 =>
                        {
                            var __标识 = SQLHelper.ExecuteNonQuery(__连接, _增加单条 + 查询自增长标识(), 创建参数(__记录), __事务);
                            _设置标识(__记录, __标识);
                        });
                        __事务.Commit();
                    }
                    catch (Exception)
                    {
                        __事务.Rollback();
                        throw;
                    }
                }

            }
        }

        public int 删除(Int64 __标识)
        {
            using (var __连接 = 创建连接())
            {
                var __存储标识 = 创建参数(_存储标识, System.Data.DbType.Int32, (int)__标识);
                SQLHelper.ExecuteNonQuery(__连接, _删除单条, new[] { __存储标识 });
                return 1;
            }
        }

        public int 删除(List<Int64> __标识集)
        {
            using (var __连接 = 创建连接())
            {
                using (var __事务 = __连接.BeginTransaction())
                {
                    try
                    {
                        __标识集.ForEach(__标识 =>
                        {
                            var __存储标识 = 创建参数(_存储标识, System.Data.DbType.Int32, (int)__标识);
                            SQLHelper.ExecuteNonQuery(__连接, _删除单条, new[] { __存储标识 }, __事务);
                        });
                        __事务.Commit();
                        return __标识集.Count;
                    }
                    catch (Exception)
                    {
                        __事务.Rollback();
                        throw;
                    }
                }
            }
        }

        public virtual Int64 删除(Func<T, bool> __验证)
        {
            //throw new NotImplementedException(); //可以不实现
            var __待删除 = 查询(__验证);
            return 删除(__待删除.Select(_获取标识).ToList());
        }

        public virtual void 删除所有()
        {
            using (var __连接 = 创建连接())
            {
                SQLHelper.ExecuteNonQuery(__连接, _删除所有);
            }
        }

        public void 修改(Int64 __标识, T __记录)
        {
            using (var __连接 = 创建连接())
            {
                var __存储标识 = 创建参数(_存储标识, System.Data.DbType.Int32, _获取标识(__记录));
                var __参数列表 = new List<DbParameter> { __存储标识 };
                __参数列表.AddRange(创建参数(__记录));
                SQLHelper.ExecuteNonQuery(__连接, _修改单条, __参数列表.ToArray());
            }
        }

        public void 修改(List<KeyValuePair<Int64, T>> __记录集)
        {
            using (var __连接 = 创建连接())
            {
                using (var __事务 = __连接.BeginTransaction())
                {
                    try
                    {
                        __记录集.ForEach(q =>
                        {
                            var __记录 = q.Value;
                            var __存储标识 = 创建参数(_存储标识, System.Data.DbType.Int32, _获取标识(__记录));
                            var __参数列表 = new List<DbParameter> { __存储标识 };
                            __参数列表.AddRange(创建参数(__记录));
                            SQLHelper.ExecuteNonQuery(__连接, _修改单条, __参数列表.ToArray(), __事务);
                        });
                        __事务.Commit();
                    }
                    catch (Exception)
                    {
                        __事务.Rollback();
                        throw;
                    }
                }
            }
        }

        public virtual Int64 修改(Func<T, bool> __验证, Action<T> __修改)
        {
            //throw new NotImplementedException(); //可以不实现
            var __待修改 = 查询(__验证);
            __待修改.ForEach(__修改);
            修改(__待修改.Select(q => new KeyValuePair<Int64, T>(_获取标识(q), q)).ToList());
            return __待修改.Count;
        }

        public virtual List<T> 查询所有()
        {
            var __结果 = new List<T>();
            using (var __连接 = 创建连接())
            {
                using (var __reader = SQLHelper.ExecuteReader(__连接, _查询前缀))
                {
                    while (__reader.Read())
                    {
                        __结果.Add(ToDTO(__reader));
                    }
                }
            }
            return __结果;
        }

        public virtual List<T> 查询(Func<T, bool> __验证)
        {
            var __结果 = new List<T>();
            using (var __连接 = 创建连接())
            {
                using (var __reader = SQLHelper.ExecuteReader(__连接, _查询前缀))
                {
                    while (__reader.Read())
                    {
                        var __temp = ToDTO(__reader);
                        if (__验证 == null || __验证(__temp))
                        {
                            __结果.Add(__temp);
                        }
                    }
                }
            }
            return __结果;
        }

        public virtual List<T> 查询(Func<T, bool> __验证, Comparison<T> __排序, Int64 __页数, int __每页数量, out Int64 __总条数)
        {
            //throw new NotImplementedException(); //可以不实现
            var __匹配列表 = 查询(__验证);
            __总条数 = __匹配列表.Count;
            if (__排序 != null)
            {
                __匹配列表.Sort(__排序);
            }
            var __结果 = new List<T>();
            for (int i = 0; i < __匹配列表.Count; i++)
            {
                if (i < (__页数 - 1) * __每页数量)
                {
                    continue;
                }
                if (i > __页数 * __每页数量)
                {
                    break;
                }
                __结果.Add(__匹配列表[i]);
            }
            return __结果;
        }

        public T 查询(Int64 __标识)
        {
            using (var __连接 = 创建连接())
            {
                var __存储标识 = 创建参数(_存储标识, System.Data.DbType.Int32, (int)__标识);
                using (var __reader = SQLHelper.ExecuteReader(__连接, _查询单条, new[] { __存储标识 }))
                {
                    while (__reader.Read())
                    {
                        return ToDTO(__reader);
                    }
                }
                return default(T);
            }
        }

        public Int64 总数
        {
            get
            {
                using (var __连接 = 创建连接())
                {
                    return (Int64)SQLHelper.ExecuteScalar(__连接, _统计数量);
                }
            }
        }

        public virtual Int64 删除bySQL(string __whereSql)
        {
            using (var __连接 = 创建连接())
            {
                var __SQL = string.IsNullOrEmpty(__whereSql) ? _条件删除 : string.Format(_条件删除 + " AND {0};", __whereSql);
                return SQLHelper.ExecuteNonQuery(__连接, __SQL);
            }
        }

        public virtual Int64 修改bySQL(string __whereSql, string __setSql)
        {
            using (var __连接 = 创建连接())
            {
                var __SQL = string.IsNullOrEmpty(__whereSql)
                    ? string.Format(_条件修改, __setSql)
                    : string.Format(_条件修改 + " AND {1};", __setSql, __whereSql);
                return SQLHelper.ExecuteNonQuery(__连接, __SQL);
            }
        }

        public abstract List<T> 查询bySQL(string __whereSql, string __sortSql, Int64 __页数, int __每页数量, out Int64 __总条数);

        public bool 支持SQL
        {
            get { return true; }
        }

        protected abstract DbConnection 创建连接();

        protected abstract DbParameter 创建参数(string __参数名, System.Data.DbType __类型, object __值);

        protected abstract DbParameter[] 创建参数(T __记录);

        protected abstract T ToDTO(DbDataReader __reader);

    }
}
