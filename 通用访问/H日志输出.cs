using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace 通用访问
{
    public static class H日志输出
    {
        private static Action<M日志> _记录信息;

        public class M日志
        {
            public string 概要 { get; set; }
            public string 详细 { get; set; }
            public TraceEventType 等级 { get; set; }
            public string 方法 { get; set; }
            public string 文件 { get; set; }
            public int 行号 { get; set; }
            public Exception 异常 { get; set; }
        }

        public static void 设置(Action<M日志> __记录信息)
        {
            _记录信息 = __记录信息;
            INET.H日志输出.设置(q => __记录信息(new M日志{ 等级 = q.等级, 方法 = q.方法, 概要 = q.概要, 行号 = q.行号, 文件 = q.文件, 详细 = q.详细, 异常 = q.异常}));
        }

        internal static void 记录(string __概要, string __详细 = null, TraceEventType __等级 = TraceEventType.Verbose, [CallerMemberName]string __方法 = "", [CallerFilePath]string __文件 = "", [CallerLineNumber]int __行号 = 0)
        {
            Debug.WriteLine("{3,-14} {2,-10}\t{0}\t{1}", __概要, __详细, __等级, DateTime.Now.ToString("HH:mm:ss.FFF"));
            if (_记录信息 != null)
            {
                _记录信息(new M日志 { 概要 = __概要, 详细 = __详细, 等级 = __等级, 方法 = __方法, 文件 = __文件, 行号 = __行号 });
            }
        }

        internal static void 记录(Exception __异常, string __描述 = null, TraceEventType __等级 = TraceEventType.Error, [CallerMemberName]string __方法 = "", [CallerFilePath]string __文件 = "", [CallerLineNumber]int __行号 = 0)
        {
            Debug.WriteLine("{3,-14} {2,-10}\t{0}\t{1}", __描述, __异常, __等级, DateTime.Now.ToString("HH:mm:ss.FFF"));
            if (_记录信息 != null)
            {
                _记录信息(new M日志 { 异常 = __异常, 概要 = __描述, 等级 = __等级, 方法 = __方法, 文件 = __文件, 行号 = __行号 });
            }
        }
    }

}
