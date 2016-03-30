using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using Utility.通用;

namespace 通用访问.UI组件
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            var __参数列表 = Environment.GetCommandLineArgs();
            if (__参数列表.Length < 4)
            {
                return;
            }
            H调试.初始化();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (__参数列表[1] == "对象列表")
            {
                Application.Run(new F对象列表窗口(new IPEndPoint(IPAddress.Parse(__参数列表[2]), int.Parse(__参数列表[3])), __参数列表.Length > 4 ? __参数列表[4] : ""));
            }
            if (__参数列表[1] == "命令列表")
            {
                Application.Run(new F命令列表窗口(new IPEndPoint(IPAddress.Parse(__参数列表[2]), int.Parse(__参数列表[3])), __参数列表.Length > 4 ? __参数列表[4] : ""));
            }
        }
    }
}
