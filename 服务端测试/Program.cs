using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Utility.通用;
using 服务端Test.IBLL;
using 通用访问;

namespace 服务端Test
{
    static class Program
    {
        public static F主窗口 F主窗口;

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            IT服务端 __IT服务端 = FT通用访问工厂.创建服务端();
            H容器.注入<IT服务端>(__IT服务端,false);
            H容器.注入<IB基本状态, B基本状态>();
            H容器.注入<IB业务, B业务>();
            H容器.注入<IB调试信息, B调试信息>();
            __IT服务端.端口 = 8888;
            __IT服务端.开启();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            F主窗口 = new F主窗口();
            Application.Run(F主窗口);
        }

    }
}
