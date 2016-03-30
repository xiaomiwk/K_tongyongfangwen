using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Utility.通用;
using 通用访问;
using 通用访问.服务端测试.IBLL;

namespace 通用访问.服务端测试
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
            //var __资源列表 = typeof (FT通用访问工厂).Assembly.GetManifestResourceNames();
            //Assembly.GetManifestResourceStream(typeof(FT通用访问工厂), ).le
            H调试.初始化();
            IT服务端 __IT服务端 = FT通用访问工厂.创建服务端();
            H容器.注入<IT服务端>(__IT服务端);
            H容器.注入<IB基本状态, B基本状态>();
            H容器.注入<IB业务, B业务>();
            H容器.注入<IB调试信息, B调试信息>();
            var __对象 = new B测试().创建对象();
            __IT服务端.添加对象("测试", () => __对象);

            __IT服务端.端口 = 8888;
            __IT服务端.开启();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            F主窗口 = new F主窗口();
            Application.Run(F主窗口);
        }

    }
}
