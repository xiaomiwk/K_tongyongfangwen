using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using Microsoft.Win32;
using Utility.通用;

namespace Utility.Windows
{
    public static class H服务管理
    {
        public static bool 验证存在(string __服务名)
        {
            var __服务 = ServiceController.GetServices().ToList().Find(q => q.ServiceName == __服务名);
            return __服务 != null;
        }

        public static void 开启(string __服务名)
        {
            var __服务 = ServiceController.GetServices().ToList().Find(q => q.ServiceName == __服务名);
            if (__服务 != null && __服务.Status != ServiceControllerStatus.Running)
            {
                __服务.Start();
                __服务.WaitForStatus(ServiceControllerStatus.Running);
            }
            else
            {
                if (__服务 == null)
                {
                    H调试.记录提示(string.Format("服务 {0} 不存在", __服务名));
                    return;
                }
                H调试.记录提示(string.Format("服务 {0} 正在运行, 无需开启", __服务名));
            }
        }

        public static void 开启(string __服务名, System.TimeSpan __超时)
        {
            var __服务 = ServiceController.GetServices().ToList().Find(q => q.ServiceName == __服务名);
            if (__服务 != null && __服务.Status != ServiceControllerStatus.Running)
            {
                __服务.Start();
                __服务.WaitForStatus(ServiceControllerStatus.Running, __超时);
            }
            else
            {
                if (__服务 == null)
                {
                    H调试.记录提示(string.Format("服务 {0} 不存在", __服务名));
                    return;
                }
                H调试.记录提示(string.Format("服务 {0} 正在运行, 无需开启", __服务名));
            }
        }

        public static void 关闭(string __服务名)
        {
            var __服务 = ServiceController.GetServices().ToList().Find(q => q.ServiceName == __服务名);
            if (__服务 != null && __服务.Status == ServiceControllerStatus.Running)
            {
                __服务.Stop();
                __服务.WaitForStatus(ServiceControllerStatus.Stopped);
            }
            else
            {
                if (__服务 == null)
                {
                    H调试.记录提示(string.Format("服务 {0} 不存在", __服务名));
                    return;
                }
                H调试.记录提示(string.Format("服务 {0} 没在运行中, 当前状态: {1}", __服务名, __服务.Status));
            }
        }

        public static void 关闭(string __服务名, System.TimeSpan __超时)
        {
            var __服务 = ServiceController.GetServices().ToList().Find(q => q.ServiceName == __服务名);
            if (__服务 != null && __服务.Status == ServiceControllerStatus.Running)
            {
                __服务.Stop();
                __服务.WaitForStatus(ServiceControllerStatus.Stopped, __超时);
            }
            else
            {
                if (__服务 == null)
                {
                    H调试.记录提示(string.Format("服务 {0} 不存在", __服务名));
                    return;
                }
                H调试.记录提示(string.Format("服务 {0} 没在运行中, 当前状态: {1}", __服务名, __服务.Status));
            }
        }

        public static void 强制关闭(string 服务名称, string 进程名称)
        {
            H调试.记录提示(string.Format("准备关闭 {0} 服务", 服务名称));
            关闭(服务名称);
            var __服务状态 = 查询状态(服务名称);
            if (__服务状态 != ServiceControllerStatus.Stopped)
            {
                H调试.记录提示(string.Format("未能正常关闭服务, 准备关闭 {0} 的进程'{1}'", 服务名称, 进程名称));
                var __进程列表 = Process.GetProcesses().ToList().FindAll(q => q.ProcessName == 进程名称);
                H调试.记录提示("找到进程数: " + __进程列表.Count);
                if (__进程列表.Count > 0)
                {
                    foreach (var process in __进程列表)
                    {
                        try
                        {
                            process.Kill();
                            process.WaitForExit();
                        }
                        catch (Exception ex)
                        {
                            H调试.记录异常(ex, string.Format("关闭服务失败, 服务名称: {0}, 服务进程: {1}", 服务名称, 进程名称));
                        }
                    }
                }
            }
        }
        
        public static void 重启(string __服务名)
        {
            var __服务 = ServiceController.GetServices().ToList().Find(q => q.ServiceName == __服务名);
            if (__服务 != null && __服务.Status == ServiceControllerStatus.Running)
            {
                __服务.Stop();
                __服务.WaitForStatus(ServiceControllerStatus.Stopped);
                __服务.Start();
                __服务.WaitForStatus(ServiceControllerStatus.Running);
            }
            else
            {
                if (__服务 == null)
                {
                    H调试.记录提示(string.Format("服务 {0} 不存在", __服务名));
                    return;
                }
                H调试.记录提示(string.Format("服务 {0} 没在运行中, 当前状态: {1}", __服务名, __服务.Status));
            }
        }

        public static void 重启(string __服务名, System.TimeSpan __超时)
        {
            var __服务 = ServiceController.GetServices().ToList().Find(q => q.ServiceName == __服务名);
            if (__服务 != null && __服务.Status == ServiceControllerStatus.Running)
            {
                __服务.Stop();
                __服务.WaitForStatus(ServiceControllerStatus.Stopped, __超时);
                __服务.Start();
                __服务.WaitForStatus(ServiceControllerStatus.Running, __超时);
            }
            else
            {
                if (__服务 == null)
                {
                    H调试.记录提示(string.Format("服务 {0} 不存在", __服务名));
                    return;
                }
                H调试.记录提示(string.Format("服务 {0} 没在运行中, 当前状态: {1}", __服务名, __服务.Status));
            }
        }

        public static void 安装(string __服务名, string __程序名, bool __自动启动 = true)
        {
            卸载(__服务名);
            var __脚本 = new StringBuilder();
            __脚本.AppendFormat("sc create {0} binPath= \"{1}\"", __服务名, Utility.存储.H路径.获取绝对路径(__程序名)).AppendLine();
            __脚本.AppendFormat("sc config {0} start= {1}", __服务名, __自动启动 ? "auto" : "demand").AppendLine();
            __脚本.AppendFormat("sc start  {0}", __服务名).AppendLine();
            //__脚本.Append("pause").AppendLine();
            File.WriteAllText("安装.bat", __脚本.ToString(), Encoding.Default);
            Process.Start("安装.bat").WaitForExit();
            File.Delete("安装.bat");
        }

        public static void 卸载(string __服务名)
        {
            var __服务 = ServiceController.GetServices().ToList().Find(q => q.ServiceName == __服务名);
            if (__服务 != null)
            {
                if (__服务.Status != ServiceControllerStatus.Stopped)
                {
                    __服务.Stop();
                    __服务.WaitForStatus(ServiceControllerStatus.Stopped);
                }
                var __脚本 = new StringBuilder();
                __脚本.AppendFormat("sc delete  {0}", __服务名).AppendLine();
                File.WriteAllText("卸载.bat", __脚本.ToString(), Encoding.Default);
                Process.Start("卸载.bat").WaitForExit();
                File.Delete("卸载.bat");
            }
        }

        public static ServiceControllerStatus 查询状态(string __服务名)
        {
            var __服务 = ServiceController.GetServices().ToList().Find(q => q.ServiceName == __服务名);
            if (__服务 != null)
            {
                return __服务.Status;
            }
            throw new M预计异常("该服务未安装");
        }

        public static bool 查询启动状态(string __服务名)
        {
            var __服务 = ServiceController.GetServices().ToList().Find(q => q.ServiceName == __服务名);
            if (__服务 != null)
            {
                return __服务.Status == ServiceControllerStatus.Running;
            }
            throw new M预计异常("该服务未安装");
        }

        public static string 查询服务安装路径(string __服务名)
        {
            var key = @"SYSTEM\CurrentControlSet\Services\" + __服务名;
            var __项 = Registry.LocalMachine.OpenSubKey(key);
            if (__项 == null)
            {
                return null;
            }
            var path = __项.GetValue("ImagePath");
            if (path == null)
            {
                return null;
            }
            return path.ToString().Replace("\"", string.Empty);
        }

    }
}
