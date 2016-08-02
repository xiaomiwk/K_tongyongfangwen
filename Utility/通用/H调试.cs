using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;
using Utility.Windows;
using Utility.存储;

namespace Utility.通用
{
    public static class H调试
    {
        public static int 清除频率小时 = 24;

        internal static bool _已初始化;

        public static bool 处理异常 = true;

        public static bool 未处理异常自动退出 = true;

        public static string 日志目录 { get; private set; }

        public static string 查询版本(bool 方法所在程序集 = false)
        {
            if (方法所在程序集)
            {
                return Assembly.GetCallingAssembly().GetName().Version.ToString();
            }
            return Assembly.GetEntryAssembly().GetName().Version.ToString();
        }

        public static void 记录(string __信息, TraceEventType __等级 = TraceEventType.Information, string __内容 = null, [CallerMemberName]string __方法 = "", [CallerFilePath]string __文件 = "", [CallerLineNumber]int __行号 = 0)
        {
            H日志.记录(__信息, __等级, __内容, __方法, __文件, __行号);
        }

        public static void 记录明细(string __信息 = "", string __内容 = null, [CallerMemberName]string __方法 = "", [CallerFilePath]string __文件 = "", [CallerLineNumber]int __行号 = 0)
        {
            H日志.记录(__信息, TraceEventType.Verbose, __内容, __方法, __文件, __行号);
        }

        public static void 记录提示(string __信息 = "", string __内容 = null, [CallerMemberName]string __方法 = "", [CallerFilePath]string __文件 = "", [CallerLineNumber]int __行号 = 0)
        {
            H日志.记录(__信息, TraceEventType.Information, __内容, __方法, __文件, __行号);
        }

        public static void 记录警告(string __信息, string __内容 = null, [CallerMemberName]string __方法 = "", [CallerFilePath]string __文件 = "", [CallerLineNumber]int __行号 = 0)
        {
            H日志.记录(__信息, TraceEventType.Warning, __内容, __方法, __文件, __行号);
        }

        public static void 记录错误(string __信息, string __内容 = null, [CallerMemberName]string __方法 = "", [CallerFilePath]string __文件 = "", [CallerLineNumber]int __行号 = 0)
        {
            H日志.记录(__信息, TraceEventType.Error, __内容, __方法, __文件, __行号);
        }

        public static void 记录致命(string __信息, string __内容 = null, [CallerMemberName]string __方法 = "", [CallerFilePath]string __文件 = "", [CallerLineNumber]int __行号 = 0)
        {
            H日志.记录(__信息, TraceEventType.Critical, __内容, __方法, __文件, __行号);
        }

        public static void 记录异常(Exception __异常, string __信息 = "", string __内容 = null, TraceEventType __等级 = TraceEventType.Error, [CallerMemberName]string __方法 = "", [CallerFilePath]string __文件 = "", [CallerLineNumber]int __行号 = 0)
        {
            H日志.记录异常(__异常, __信息, __内容, __等级, __方法, __文件, __行号);
        }

        public static void 初始化(TraceEventType __日志级别 = TraceEventType.Verbose, string __日志目录 = "日志", string __日志文件名称 = "", int __保留天数 = 30)
        {
            if (_已初始化)
            {
                return;
            }
            _已初始化 = true;

            H日志.初始化(__日志级别, __日志目录, __日志文件名称);
            日志目录 = __日志目录;

            var __环境信息 = new Dictionary<string, object>();
            __环境信息["Version"] = Assembly.GetCallingAssembly().GetName().Version.ToString();
            __环境信息["FileVersion"] = FileVersionInfo.GetVersionInfo(Assembly.GetCallingAssembly().Location).FileVersion;
            __环境信息["MachineName"] = Environment.MachineName;
            __环境信息["UserName"] = Environment.UserName;
            __环境信息["IP"] = String.Join(";", H网络配置.获取可用IP().Select(q => q.ToString()));
            __环境信息["OSVersion"] = Environment.OSVersion.VersionString;
            __环境信息["CLR Version"] = Environment.Version;
            __环境信息["Is64BitProcess"] = Environment.Is64BitProcess;
            __环境信息["CurrentDirectory"] = Environment.CurrentDirectory;
            记录提示("程序启动", H序列化.ToJSON字符串(__环境信息));

            if (__保留天数 != int.MaxValue)
            {
                清除过期调试文件(日志目录, __保留天数);
            }

            if (处理异常)
            {
                Application.ThreadException += (sender, ex) => H异常.处理UI线程(ex.Exception, 未处理异常自动退出);
                AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
                {
                    if (e.ExceptionObject == null) return;
                    var ex = e.ExceptionObject as Exception;
                    if (ex == null)
                    {
                        记录致命(e.ExceptionObject.ToString());
                    }
                    else
                    {
                        H异常.处理非UI线程(ex);
                    }
                };
            }
        }

        public static void 清除过期调试文件(string __路径, int __保留天数 = 30, int __清除频率小时 = 24)
        {
            ThreadPool.QueueUserWorkItem(arg =>
            {
                while (true)
                {
                    try
                    {
                        var __日志路径 = H路径.获取绝对路径(__路径);
                        H日志.记录明细(string.Format("日志路径:{0}; 保留天数:{1}; 清除频率小时:{2}", __日志路径, __保留天数, __清除频率小时));
                        if (Directory.Exists(__日志路径))
                        {
                            var __目录 = new DirectoryInfo(__日志路径);
                            foreach (var __文件 in __目录.GetFiles())
                            {
                                if (__文件.LastWriteTime.AddDays(__保留天数) < DateTime.Now)
                                {
                                    try
                                    {
                                        __文件.Delete();
                                        H日志.记录明细(string.Format("删除日志:{0}; ", __文件));
                                    }
                                    catch (Exception ex)
                                    {
                                        记录异常(ex);
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        //非核心线程,出错仅记录
                        记录异常(ex);
                    }
                    Thread.Sleep(new TimeSpan(__清除频率小时, 0, 0));//一天执行一次
                }
            });
        }

        public static void 截屏(string __文件名 = null)
        {
            if (__文件名 == null)
            {
                __文件名 = string.Format("未处理异常 {0}", DateTime.Now.ToString("yyyy年MM月dd日 HH时mm分ss秒"));
            }
            Image myImg = new Bitmap(Screen.AllScreens[0].Bounds.Width, Screen.AllScreens[0].Bounds.Height);
            Graphics g = Graphics.FromImage(myImg);

            g.CopyFromScreen(new Point(0, 0), new Point(0, 0), Screen.AllScreens[0].Bounds.Size);
            var __保存路径 = H路径.获取绝对路径(string.Format("{0}\\{1}.jpg", 日志目录, __文件名));
            myImg.Save(__保存路径, System.Drawing.Imaging.ImageFormat.Jpeg);
        }

    }
}
