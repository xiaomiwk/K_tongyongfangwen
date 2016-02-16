using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Utility.扩展
{
    public static class H检测耗时
    {
        public static IDisposable 创建(string __操作描述, Action<string> __输出方法 = null)
        {
            return new AutoStopwatch(__操作描述, __输出方法);
        }
    }

    class AutoStopwatch : Stopwatch, IDisposable
    {
        private Action<string> _输出方法;

        private string _操作描述;

        public AutoStopwatch(string __操作描述, Action<string> __输出方法 = null)
        {
            _操作描述 = __操作描述;
            _输出方法 = __输出方法;
            Start();
        }

        public void Dispose()
        {
            Stop();
            var __描述 = string.Format("{0} 耗时 {1}", _操作描述, this.Elapsed);
            Debug.WriteLine(__描述);
            if (_输出方法 != null)
            {
                _输出方法(__描述);
            }
        }
    }
}
