using System;

//namespace OmarALZabir.AspectF
using System.Runtime.CompilerServices;

namespace Utility.通用
{
    public interface ILogger
    {
        void Log(string message, string __方法 = "", string __文件 = "", int __行号 = 0);
        void LogException(Exception x);
    }

    internal class Logger : ILogger
    {
        public void Log(string message, [CallerMemberName]string __方法 = "", [CallerFilePath]string __文件 = "", [CallerLineNumber]int __行号 = 0)
        {
            H日志.记录提示(message, null, __方法, __文件, __行号);
        }

        public void LogException(Exception x)
        {
            H日志.记录异常(x);
        }
    }
}
