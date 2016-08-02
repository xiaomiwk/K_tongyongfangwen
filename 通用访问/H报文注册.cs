using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using INET;
using 通用访问.DTO;

namespace 通用访问
{
    class H报文注册
    {
        public static Dictionary<Int16, Type> 报文字典 = new Dictionary<Int16, Type>
        {
            {1,typeof(M对象列表查询请求)},
            {2,typeof(M对象列表查询结果)},
            {3,typeof(M对象明细查询请求)},
            {4,typeof(M对象明细查询结果)},
            {5,typeof(M方法执行请求)},
            {6,typeof(M方法执行结果)},
            {7,typeof(M属性值查询请求)},
            {8,typeof(M属性值查询结果)},
            {9,typeof(M订阅事件)},
            {11,typeof(M注销事件)},
            {12,typeof(M接收事件)},
        };

        public static string 查询功能码(Type __报文类型)
        {
            foreach (var __KY in 报文字典)
            {
                if (__KY.Value == __报文类型)
                {
                    return __KY.Key.ToString("X4");
                }
            }
            throw new ApplicationException(string.Format("报文类型无效: {0}", __报文类型));
        }

        public static void 拦截接收报文(Type __类型, string __JSON)
        {
            H日志输出.记录(string.Format("接收 [{0}]", __类型.Name), __JSON, TraceEventType.Information);
        }

        public static void 拦截发送报文(object __对象, string __JSON)
        {
            H日志输出.记录(string.Format("发送 [{0}]", __对象.GetType().Name), __JSON, TraceEventType.Information);
        }
    }
}
