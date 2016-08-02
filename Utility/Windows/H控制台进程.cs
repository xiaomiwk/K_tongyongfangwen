using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Utility.Windows
{
    public static class H控制台进程
    {
        public static string 执行程序(string __程序名称, string __参数)
        {
            using (var __进程 = new Process())
            {
                __进程.StartInfo = new ProcessStartInfo(__程序名称, __参数);
                __进程.StartInfo.CreateNoWindow = false;
                __进程.StartInfo.UseShellExecute = false;
                __进程.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                __进程.StartInfo.RedirectStandardOutput = true;
                __进程.Start();
                var __结果 = __进程.StandardOutput.ReadToEnd();
                return __结果;
            }
        }
    }
}
