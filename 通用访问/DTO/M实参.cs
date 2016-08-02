using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace 通用访问.DTO
{
    public class M实参
    {
        public string 名称 { get; set; }

        public string 值 { get; set; }

        public static List<M实参> 字典转列表(Dictionary<string, string> 参数列表)
        {
            if (参数列表 == null)
            {
                return null;
            }
            var __结果 = new List<M实参>();
            foreach (var __kv in 参数列表)
            {
                __结果.Add(new M实参 { 名称 = __kv.Key, 值 = __kv.Value });
            }
            return __结果;
        }

        public static Dictionary<string, string> 列表转字典(List<M实参> 参数列表)
        {
            if (参数列表 == null)
            {
                return null;
            }
            var __结果 = new Dictionary<string, string>();
            foreach (var __kv in 参数列表)
            {
                __结果[__kv.名称] = __kv.值;
            }
            return __结果;
        }
    }
}
