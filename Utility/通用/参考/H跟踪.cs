using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Diagnostics;

namespace Utility.通用
{
    public class H跟踪 : IDisposable
    {
        readonly Dictionary<string, object> _上下文 = new Dictionary<string, object>();

        readonly int _开始时间;

        readonly string _信息;

        private static int __标识序列号 = 1;

        private readonly int _标识;

        public H跟踪(string 信息, Dictionary<string, object> 参数, string __方法, string __文件, int __行号)
        {
            _开始时间 = Environment.TickCount;
            _信息 = 信息;
            _标识 = __标识序列号++;
            H调试.记录提示(string.Format("[[{0} {1}", _标识, 信息), 参数, __方法, __文件, __行号);
        }

        public void 设置上下文(string key, object value, [CallerMemberName]string __方法 = "", [CallerFilePath]string __文件 = "", [CallerLineNumber]int __行号 = 0)
        {
            _上下文[key] = value;
            H调试.记录明细(_信息 +" 设置局部上下文", new Dictionary<string, object>
                                       {
                                           { key, value }
                                       }, __方法, __文件, __行号);

        }

        public void 刷新上下文([CallerMemberName]string __方法 = "", [CallerFilePath]string __文件 = "", [CallerLineNumber]int __行号 = 0)
        {
            H调试.记录明细(_信息 + " 刷新局部上下文", _上下文, __方法, __文件, __行号);
        }

        public void Dispose()
        {
            int 耗时 = Environment.TickCount - _开始时间;
            if (_上下文.Count > 0)
            {
                刷新上下文();
            }
            H调试.记录提示(string.Format("]]{0} {1}[耗时: {2:f3}]", _标识 * -1, _信息, 耗时 / 1000.0));
        }
    }
}

