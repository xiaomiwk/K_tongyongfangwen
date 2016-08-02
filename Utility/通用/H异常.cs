using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Web;
using System.Windows.Forms;
using Utility.Windows;

namespace Utility.通用
{
    public static class H异常
    {
        /// <summary>
        /// 第一个string 表示基本信息, 第二个表示详细信息
        /// </summary>
        public static Action<string, string> 提示可恢复异常 { get; set; }

        /// <summary>
        /// 第一个string 表示基本信息, 第二个表示详细信息
        /// </summary>
        public static Action<string, string> 提示不可恢复异常 { get; set; }

        public static Func<Exception, bool> 自定义处理 { get; set; }

        private static bool _DotNetException;

        static H异常()
        {
            HMiniDump.自动记录(() => _DotNetException);
            提示可恢复异常 = (m, n) => MessageBox.Show(n, m, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            提示不可恢复异常 = (m, n) =>
            {
                if (HttpContext.Current == null)
                {
                    if (Debugger.IsAttached)
                    {
                        MessageBox.Show(n, m, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show(m, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            };
        }

        internal static void 处理非UI线程(Exception ex)
        {
            H调试.记录异常(ex, "！！！处理非UI线程！！！");
            _DotNetException = true;
            if (自定义处理 != null)
            {
                if (自定义处理(ex))
                {
                    return;
                }
            }

            var __预计异常 = ex.GetBaseException() as M预计异常;
            提示不可恢复异常(__预计异常 != null ? __预计异常.Message : "程序出错, 即将关闭", ex.ToString());
        }

        internal static void 处理UI线程(Exception ex, bool __未处理自动退出 = true)
        {
            if (自定义处理 != null)
            {
                if (自定义处理(ex))
                {
                    return;
                }
            }
            var __异常描述 = ex.ToString();
            var __输入格式异常 = ex as M输入异常;
            if (__输入格式异常 != null)
            {
                提示可恢复异常(__输入格式异常.类别, __输入格式异常.描述);
                return;
            }
            var __Model验证异常 = ex as M验证异常;
            if (__Model验证异常 != null)
            {
                提示可恢复异常(__Model验证异常.类别, __Model验证异常.描述);
                return;
            }

            var __可恢复异常 = ex as M预计异常;
            if (__可恢复异常 != null)
            {
                提示可恢复异常(__可恢复异常.类别, __可恢复异常.描述);
                return;
            }
            if (ex is System.Security.SecurityException)
            {
                提示可恢复异常(ex.Message, string.Empty);
                return;
            }
            if (ex is ApplicationException)
            {
                提示可恢复异常("错误", ex.Message);
                return;
            }
            if (ex is NotImplementedException)
            {
                if (string.IsNullOrEmpty(ex.Message))
                {
                    提示可恢复异常("错误", "功能未实现");
                    return;
                }
                提示可恢复异常("错误", ex.Message);
                return;
            }

            H调试.记录异常(ex, "！！！处理UI线程！！！");
            if (__未处理自动退出)
            {
                H调试.截屏();
                提示不可恢复异常("出现未预计的错误，请将日志发送给开发人员", __异常描述);
                Environment.Exit(0);
            }
            提示可恢复异常("错误", "出现未预计的错误，请将日志发送给开发人员");
        }
    }
}

