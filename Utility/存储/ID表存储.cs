using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utility.存储
{
    /// <summary>
    /// 标识对象的存储标识, 必须在Int64的属性上使用
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class DBKeyAttribute : Attribute { }

    public interface ID表存储<T>
    {
        void 初始化(string __表名);

        void 增加(T __记录);

        void 增加(List<T> __记录集);

        int 删除(Int64 __标识);

        int 删除(List<Int64> __标识集);

        Int64 删除(Func<T, bool> __验证);

        void 删除所有();

        void 修改(Int64 __标识, T __记录);

        void 修改(List<KeyValuePair<Int64, T>> __记录集);

        Int64 修改(Func<T, bool> __验证, Action<T> __修改);

        List<T> 查询所有();

        List<T> 查询(Func<T, bool> __验证);

        List<T> 查询(Func<T, bool> __验证, Comparison<T> __排序, Int64 __页数, int __每页数量, out Int64 __总条数);

        T 查询(Int64 __标识);

        Int64 总数 { get; }

        bool 支持SQL { get; }

        Int64 删除bySQL(string __whereSql);

        Int64 修改bySQL(string __whereSql, string __setSql);

        List<T> 查询bySQL(string __whereSql, string __sortSql, Int64 __页数, int __每页数量, out Int64 __总条数);

    }


    //public interface ID表存储<T, T标识>
    //{
    //    void 初始化(string __表名, Func<T标识, T标识> __生成标识, Func<T, T标识> __获取标识, Action<T, T标识> __设置标识);

    //    void 增加(T __记录);

    //    void 增加(List<T> __记录集);

    //    void 删除(T __记录);

    //    void 删除(List<T> __记录集);

    //    int 删除(Func<T, bool> __验证);

    //    void 删除所有();

    //    void 修改(T __记录);

    //    void 修改(List<T> __记录集);

    //    int 修改(Func<T, bool> __验证, Action<T> __修改);

    //    List<T> 查询所有();

    //    T 查询(T标识 __标识);

    //    int 总数 { get; }

    //    T 第一条 { get; }

    //    T 最后一条 { get; }

    //    List<T> 查询(Func<T, bool> __验证);
    //}

    //public interface ID表存储<T>
    //where T : class
    //{
    //    void 初始化(string __表名);

    //    long 增加(T __记录);

    //    List<long> 增加(List<T> __记录集);

    //    void 删除(long __标识);

    //    void 删除(List<long> __标识集);

    //    void 删除(Func<T, bool> __验证);

    //    void 删除所有();

    //    void 修改(long __标识, T __记录);

    //    void 修改(List<KeyValuePair<long, T>> __记录集);

    //    List<KeyValuePair<long, T>> 查询所有();

    //    T 查询(long __标识);

    //    int 总数 { get; }

    //    T 第一条 { get; }

    //    T 最后一条 { get; }

    //    List<KeyValuePair<long, T>> 查询(Func<T, bool> __验证);
    //}

}
