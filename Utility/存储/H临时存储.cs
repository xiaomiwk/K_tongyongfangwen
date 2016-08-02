using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Utility.存储
{
    public class H临时存储<T> : ID表存储<T>
    {
        protected string _存储路径 { get; set; }

        protected SortedDictionary<Int64, T> __表;

        protected readonly object _同步对象 = new object();

        private bool _已初始化;

        private Func<T, Int64> _获取标识;

        private Action<T, Int64> _设置标识;


        public void 初始化(string __存储路径 = null)
        {
            if (_已初始化)
            {
                return;
            }
            _已初始化 = true;
            var __存储标识元数据 = typeof(T).GetProperties().First(q => q.IsDefined(typeof(DBKeyAttribute), false));
            _获取标识 = (T __记录) => (Int64)__存储标识元数据.GetValue(__记录, null);
            _设置标识 = (T __记录, Int64 __标识) => __存储标识元数据.SetValue(__记录, __标识, null);

            if (__存储路径 == null)
            {
                __存储路径 = string.Format("存储\\{0}", typeof(T).Name);
            }
            _存储路径 = __存储路径;
            __表 = (SortedDictionary<Int64, T>)H序列化.二进制读取(_存储路径) ?? new SortedDictionary<Int64, T>();
        }

        private void 保存()
        {
            H序列化.二进制存储(__表, _存储路径);
        }

        public void 增加(T __记录)
        {
            lock (_同步对象)
            {
                var __标识 = __表.Any() ? __表.Last().Key + 1 : 1;
                _设置标识(__记录, __标识);
                __表[__标识] = __记录;
                保存();
            }
        }

        public void 增加(List<T> __记录集)
        {
            lock (_同步对象)
            {
                var __标识 = __表.Any() ? __表.Last().Key + 1 : 1;
                __记录集.ForEach(__记录 =>
                {
                    _设置标识(__记录, __标识);
                    __表[__标识] = __记录;
                    __标识++;
                });
                保存();
            }
        }

        public int 删除(Int64 __标识)
        {
            lock (_同步对象)
            {
                if (__表.ContainsKey(__标识))
                {
                    __表.Remove(__标识);
                    保存();
                    return 1;
                }
                return 0;
            }
        }

        public int 删除(List<Int64> __标识集)
        {
            lock (_同步对象)
            {
                var __数量 = 0;
                __标识集.ForEach(__标识 =>
                {
                    if (__表.ContainsKey(__标识))
                    {
                        __表.Remove(__标识);
                        __数量++;
                    }
                });
                保存();
                return __数量;
            }
        }

        public Int64 删除(Func<T, bool> __验证)
        {
            lock (_同步对象)
            {
                var __匹配列表 =__表.Where(q => __验证(q.Value)).Select(q => q.Key).ToList();
                __匹配列表.ForEach(q => __表.Remove(q));
                保存();
                return __匹配列表.Count;
            }
        }

        public void 删除所有()
        {
            lock (_同步对象)
            {
                __表.Clear();
                保存();
            }
        }

        public void 修改(Int64 __标识, T __记录)
        {
            lock (_同步对象)
            {
                if (__表.ContainsKey(__标识))
                {
                    __表[__标识] = __记录;
                    保存();
                }
            }
        }

        public void 修改(List<KeyValuePair<Int64, T>> __记录集)
        {
            lock (_同步对象)
            {
                __记录集.ForEach(q =>
                {
                    __表[q.Key] = q.Value;
                });
                保存();
            }
        }

        public Int64 修改(Func<T, bool> __验证, Action<T> __修改)
        {
            lock (_同步对象)
            {
                var __匹配列表 = __表.Where(q => __验证(q.Value)).ToList();
                __匹配列表.ForEach(q => __修改(q.Value));
                保存();
                return __匹配列表.Count;
            }
        }

        public List<T> 查询所有()
        {
            lock (_同步对象)
            {
                return new List<T>(__表.Values);
            }
        }

        public List<T> 查询(Func<T, bool> __验证)
        {
            lock (_同步对象)
            {
                return __表.Where(q => __验证(q.Value)).Select(q => q.Value).ToList();
            }
        }

        public List<T> 查询(Func<T, bool> __验证, Comparison<T> __排序, Int64 __页数, int __每页数量, out Int64 __总条数)
        {
            var __匹配列表 = __表.Select(__kv => __kv.Value).Where(__对象 => __验证 == null || __验证(__对象)).ToList();
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
                if (i >= __页数 * __每页数量)
                {
                    break;
                }
                __结果.Add(__匹配列表[i]);
            }
            return __结果;
        }

        /// <summary>
        /// 不支持
        /// </summary>
        public T 查询(Int64 __标识)
        {
            lock (_同步对象)
            {
                if (__表.ContainsKey(__标识))
                {
                    return __表[__标识];
                }
                return default(T);
            }
        }

        public Int64 总数
        {
            get
            {
                lock (_同步对象)
                {
                    return __表.Count;
                }
            }
        }



        public bool 支持SQL
        {
            get { return false; }
        }

        public long 删除bySQL(string __whereSql)
        {
            throw new NotImplementedException();
        }

        public long 修改bySQL(string __whereSql, string __setSql)
        {
            throw new NotImplementedException();
        }

        public List<T> 查询bySQL(string __whereSql, string __sortSql, long __页数, int __每页数量, out long __总条数)
        {
            throw new NotImplementedException();
        }

        public T 第一条
        {
            get
            {
                lock (_同步对象)
                {
                    if (__表.Count > 0)
                    {
                        return __表.First().Value;
                    }
                    return default(T);
                }
            }
        }

        public T 最后一条
        {
            get
            {
                lock (_同步对象)
                {
                    if (__表.Count > 0)
                    {
                        return __表.Last().Value;
                    }
                    return default(T);
                }
            }
        }

        public T 查询上一条(Int64 __标识)
        {
            T __上一条 = default(T);
            foreach (var __kv in __表)
            {
                if (__kv.Key == __标识)
                {
                    return __上一条;
                }
                __上一条 = __kv.Value;
            }
            return __上一条;
        }

        public T 查询下一条(Int64 __标识)
        {
            var __找到 = false;
            foreach (var __kv in __表)
            {
                if (__找到)
                {
                    return __kv.Value;
                }
                if (__kv.Key == __标识)
                {
                    __找到 = true;
                }
            }
            return default(T);
        }


    }
}
