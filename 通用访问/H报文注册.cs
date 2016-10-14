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

        static Dictionary<Type, string> _标识字典 = new Dictionary<Type, string>();

        public static Dictionary<Int16, string> 通道字典 = new Dictionary<Int16, string>
        {
            {1,"请求"},
            {2,"请求"},
            {3,"请求"},
            {4,"请求"},
            {5,"请求"},
            {6,"请求"},
            {7,"请求"},
            {8,"请求"},
            {9,"请求"},
            {11,"请求"},
            {12,"通知"},
        };

        public static string 查询功能码(Type __报文类型)
        {
            if (_标识字典.Count == 0)
            {
                foreach (var __kv in 报文字典)
                {
                    _标识字典[__kv.Value] = __kv.Key.ToString("X4");
                }
            }
            if (_标识字典.ContainsKey(__报文类型))
            {
                return _标识字典[__报文类型];
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
