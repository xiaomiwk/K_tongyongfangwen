using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utility.存储
{
    public interface ID键值对存储<TKey, TRecord>
    {
        void 初始化(string __表名);

        void 增加或修改(TKey key, TRecord record);

        void 删除(TKey key);

        void 删除(TKey fromKey, TKey toKey);

        void 删除所有();

        bool 包含(TKey key);

        List<KeyValuePair<TKey, TRecord>> 查询所有();

        int 总数 { get; }

        bool 查询(TKey key, out TRecord record);

        TRecord this[TKey key] { get; set; }

        TRecord 查询(TKey key);

        List<KeyValuePair<TKey, TRecord>> 查询(TKey from, bool hasFrom, TKey to, bool hasTo);

        KeyValuePair<TKey, TRecord>? 查询下一条(TKey key);

        KeyValuePair<TKey, TRecord>? 查询上一条(TKey key);

        KeyValuePair<TKey, TRecord> 第一条 { get; }

        KeyValuePair<TKey, TRecord> 最后一条 { get; }

    }
}
