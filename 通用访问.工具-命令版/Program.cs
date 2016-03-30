using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Utility.通用;

namespace 通用访问.工具
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            H调试.初始化();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new F主窗口());
        }
    }
}
