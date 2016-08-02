using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;
using Utility.存储;
using Utility.扩展;

namespace Utility.通用
{
    public static class H调试
    {
        static readonly Dictionary<string, object> _全局上下文 = new Dictionary<string, object>();

        static readonly string _行分割 = Environment.NewLine;

        static bool _输出帧 = true;

        public static int 保留天数 = 7;

        public static int 清除频率小时 = 24;

        static bool _已初始化 = false;

        internal static H日志文件输出 _详细日志;

        internal static H日志文件输出 _错误日志;

        /// <summary>
        /// string 消息, string 调试信息, TraceEventType 等级
        /// </summary>
        public static event Action<string, string, TraceEventType> 输出通知;

        public static void 触发输出通知(string 信息, string 内容, TraceEventType 等级)
        {
            var handler = 输出通知;
            if (handler != null) handler(信息, 内容, 等级);
        }

        public static void 设置全局上下文(string key, object value)
        {
            _全局上下文[key] = value;
            记录明细("设置全局上下文", new Dictionary<string, object>
            {
                                           { key, value }
                                       });
        }

        public static void 删除全局上下文(string key)
        {
            if (_全局上下文.ContainsKey(key))
            {
                _全局上下文.Remove(key);
                记录明细("删除全局上下文" + key);
            }
        }

        public static void 刷新全局上下文()
        {
            记录明细("刷新全局上下文", _全局上下文);
        }

        public static Dictionary<string, object> 查询全局上下文()
        {
            return _全局上下文;
        }

        public static void 记录明细(string 信息 = "", Dictionary<string, object> 参数 = null)
        {
            输出(信息, 参数, TraceEventType.Verbose);
        }

        public static void 记录提示(string 信息 = "", Dictionary<string, object> 参数 = null)
        {
            输出(信息, 参数, TraceEventType.Information);
        }

        public static void 记录警告(string 信息, Dictionary<string, object> 参数 = null)
        {
            输出(信息, 参数, TraceEventType.Warning);
        }

        public static void 记录错误(string 信息, Dictionary<string, object> 参数 = null)
        {
            输出(信息, 参数, TraceEventType.Error);
        }

        public static void 记录致命(string 信息, Dictionary<string, object> 参数 = null)
        {
            输出(信息, 参数, TraceEventType.Critical);
        }

        public static void 记录异常(Exception 异常, string 信息 = "", Dictionary<string, object> 参数 = null, TraceEventType 等级 = TraceEventType.Error)
        {
            信息 = 信息 + Environment.NewLine + 获取异常描述(异常);
            输出(信息, 参数, 等级);
        }

        public static void 初始化(bool __处理异常 = true, bool __清除过期日志 = true, int __保留天数 = 7)
        {
            if (_已初始化)
            {
                return;
            }
            _已初始化 = true;

            if (__清除过期日志)
            {
                清除过期调试文件("日志", __保留天数);
            }
            配置日志文件输出();
            var __环境信息 = new Dictionary<string, object>();
            __环境信息["Version".PadRight(20)] = Assembly.GetCallingAssembly().GetName().Version.ToString();
            __环境信息["FileVersion".PadRight(20)] = FileVersionInfo.GetVersionInfo(Assembly.GetCallingAssembly().Location).FileVersion;
            __环境信息["MachineName".PadRight(20)] = Environment.MachineName;
            __环境信息["UserName".PadRight(20)] = Environment.UserName;
            __环境信息["IP".PadRight(20)] = String.Join(";", H网络配置.获取可用IP().Select(q => q.ToString()));
            __环境信息["OSVersion".PadRight(20)] = Environment.OSVersion.VersionString;
            __环境信息["CLR Version".PadRight(20)] = Environment.Version;
            __环境信息["Is64BitProcess".PadRight(20)] = Environment.Is64BitProcess;
            __环境信息["CurrentDirectory".PadRight(20)] = Environment.CurrentDirectory;
            记录提示("程序启动", __环境信息);
            if (__处理异常)
            {
                Application.ThreadException += (sender, ex) => H异常.处理UI线程(ex.Exception);
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

        public static H跟踪 跟踪(string 信息, Dictionary<string, object> 参数 = null)
        {
            return new H跟踪(信息, 参数);
        }

        public static string 获取对象状态描述(string 类型, Dictionary<string, object> 状态字典)
        {
            var __描述 = new StringBuilder();
            __描述.AppendFormat("({0}){{", 类型);
            foreach (var kv in 状态字典)
            {
                __描述.AppendFormat("{0}-{1}; ", kv.Key, kv.Value);
            }
            __描述.Append("}");
            return __描述.ToString();
        }

        public static string 获取异常描述(Exception e)
        {
            var __描述 = new StringBuilder();
            __描述.AppendFormat("描述:\t{0}", e.Message).Append(_行分割);
            __描述.AppendFormat("类型:\t{0}", e.GetType().FullName).Append(_行分割);
            __描述.Append(e.StackTrace).Append(_行分割);
            var __内部异常 = e.InnerException;
            if (__内部异常 != null && __内部异常 != e)
            {
                __描述.Append(_行分割).Append("------------内部异常------------").Append(_行分割).Append(获取异常描述(__内部异常));
            }
            return __描述.ToString();
        }

        private static void 输出(string 信息, Dictionary<string, object> 参数, TraceEventType 等级)
        {
            //格式过滤
            信息 = 字符串过滤(信息);

            //附加帧信息
            var __行号 = String.Empty;
            var __方法 = String.Empty;
            var __类型 = String.Empty;
            var __文件 = String.Empty;
            if (_输出帧)
            {
                try
                {
                    var __栈帧 = new StackTrace(true).GetFrames();
                    if (__栈帧 != null)
                    {
                        var __index = 0;
                        for (int i = 0; i < __栈帧.Length; i++)
                        {
                            __文件 = __栈帧[i].GetFileName();
                            __类型 = __栈帧[i].GetMethod().DeclaringType.Name;
                            if (__文件 != null && !__文件.EndsWith("H调试.cs") && !__文件.EndsWith("H跟踪.cs") && !__类型.StartsWith("LoggerCallHandler"))
                            {
                                __index = i;
                                break;
                            }
                        }
                        __行号 = __栈帧[__index].GetFileLineNumber().ToString();
                        __方法 = __栈帧[__index].GetMethod().ToString();
                        __类型 = __栈帧[__index].GetMethod().DeclaringType.Name;
                        __文件 = __栈帧[__index].GetFileName();
                    }
                }
                catch
                {
                    _输出帧 = false;
                }
            }

            var __完整描述 = new StringBuilder();
            if (!String.IsNullOrEmpty(__类型))
            {
                __完整描述.AppendFormat("{0} : {1} || {2}", __类型, __方法, 信息);
            }
            else
            {
                __完整描述.AppendFormat("{0}", 信息);
            }
            if (!String.IsNullOrEmpty(__文件))
            {
                __完整描述.Append(" || ");
                __完整描述.AppendFormat("{0}({1});", __文件, __行号);
            }
            if (参数 != null)
            {
                //__完整描述.AppendLine();
                //__完整描述.Append("  参数");
                foreach (var kv in 参数)
                {
                    __完整描述.AppendFormat("{2}    {0} : {1};", kv.Key, kv.Value, Environment.NewLine);
                }
            }
            var __字符串 = __完整描述.ToString();
            _详细日志.TraceEvent(new TraceEventCache(), "", 等级, 0, __字符串);
            switch (等级)
            {
                case TraceEventType.Critical:
                case TraceEventType.Error:
                    _错误日志.TraceEvent(new TraceEventCache(), "", 等级, 0, __字符串);
                    break;
            }

            触发输出通知(信息, __字符串, 等级);
        }

        private static string 字符串过滤(object __源)
        {
            if (__源 != null)
            {
                //return __源.ToString().Replace("||", "").Replace(Environment.NewLine, "<br/>").Replace("\n", "").Replace("\r", "");
                return __源.ToString().Replace("||", "!!");
            }
            return String.Empty;
        }

        /// <summary>
        /// 该类初始化时, 会自动清除"日志"目录下过期调试文件
        /// </summary>
        public static void 清除过期调试文件(string __路径, int __保留天数 = 7, int __清除频率小时 = 24)
        {
            ThreadPool.QueueUserWorkItem(
            arg =>
            {
                while (true)
                {
                    //记录明细("清除调试文件");
                    try
                    {
                        var __日志文件路径 = H路径.获取绝对路径(__路径);
                        if (Directory.Exists(__日志文件路径))
                        {
                            var __目录 = new DirectoryInfo(__日志文件路径);
                            foreach (var __文件 in __目录.GetFiles())
                            {
                                if (__文件.LastWriteTime.AddDays(__保留天数) < DateTime.Now)
                                {
                                    try
                                    {
                                        __文件.Delete();
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

        private static void 配置日志文件输出()
        {
            var __程序集名称 = Assembly.GetEntryAssembly().GetName();
            var __日志名称 = __程序集名称.Name + "  " + __程序集名称.Version + "  ";
            _详细日志 = new H日志文件输出("详细日志")
            {
                Append = true,
                AutoFlush = true,
                BaseFileName = __日志名称 + "详细日志",
                Location = LogFileLocation.Custom,
                CustomLocation = H路径.获取绝对路径("日志", true),
                Delimiter = " || ",
                DiskSpaceExhaustedBehavior = DiskSpaceExhaustedOption.DiscardMessages,
                Encoding = Encoding.UTF8,
                LogFileCreationSchedule = LogFileCreationScheduleOption.Daily,
                MaxFileSize = 20000000,
                TraceOutputOptions = TraceOptions.ThreadId | TraceOptions.DateTime | TraceOptions.LogicalOperationStack,
                Filter = new EventTypeFilter(SourceLevels.All),
            };
            Trace.Listeners.Add(_详细日志);
            _错误日志 = new H日志文件输出("错误日志")
            {
                Append = true,
                AutoFlush = true,
                BaseFileName = __日志名称 + "错误日志",
                Location = LogFileLocation.Custom,
                CustomLocation = H路径.获取绝对路径("日志", true),
                Delimiter = " || ",
                DiskSpaceExhaustedBehavior = DiskSpaceExhaustedOption.DiscardMessages,
                Encoding = Encoding.UTF8,
                LogFileCreationSchedule = LogFileCreationScheduleOption.Daily,
                MaxFileSize = 20000000,
                TraceOutputOptions = TraceOptions.ThreadId | TraceOptions.DateTime | TraceOptions.LogicalOperationStack | TraceOptions.Callstack,
                Filter = new EventTypeFilter(SourceLevels.Warning),
            };
            Trace.Listeners.Add(_错误日志);
        }
    }
}
