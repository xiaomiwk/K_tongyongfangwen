using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Utility.通用;
using Microsoft.VisualBasic.Devices;

namespace Utility.Windows
{
    public static class H检测内存
    {
        //private static PerformanceCounter _计数器;

        public static Dictionary<string, object> 查询当前程序内存消耗()
        {
            var __结果 = new Dictionary<string, object>();
            try
            {
                var __进程 = Process.GetCurrentProcess();
                __结果["PrivateMemorySize64"] = __进程.PrivateMemorySize64 / 1024;
                __结果["WorkingSet64"] = __进程.WorkingSet64 / 1024; //(Physical memory usage)
                __结果["NonpagedSystemMemorySize64"] = __进程.NonpagedSystemMemorySize64 / 1024;
                __结果["PagedSystemMemorySize64"] = __进程.PagedSystemMemorySize64 / 1024;
                __结果["PagedMemorySize64"] = __进程.PagedMemorySize64 / 1024;
                __结果["VirtualMemorySize64"] = __进程.VirtualMemorySize64 / 1024;
                __结果["PeakPagedMemorySize64"] = __进程.PeakPagedMemorySize64 / 1024;
                __结果["PeakVirtualMemorySize64"] = __进程.PeakVirtualMemorySize64 / 1024;
                __结果["PeakWorkingSet64"] = __进程.PeakWorkingSet64 / 1024;
            }
            catch (Exception ex)
            {
                H调试.记录异常(ex, "查询当前程序内存消耗");
            }
            return __结果;
        }

        public static string 查询当前程序内存消耗描述(bool 详细 = false)
        {
            if (!详细)
            {
                var __进程 = Process.GetCurrentProcess();
                return string.Format("{0}K", __进程.PrivateMemorySize64 / 1024);
            }
            var __结果 = 查询当前程序内存消耗();
            var __描述 = new StringBuilder();
            foreach (var __kv in __结果)
            {
                __描述.AppendFormat("{0}:{1};", __kv.Key, __kv.Value);
            }
            return __描述.ToString();
        }

        /// <summary>
        /// 单位: MBytes
        /// </summary>
        /// <returns></returns>
        public static int 查询系统可用内存()
        {
            //if (_计数器 == null)
            //{
            //    _计数器 = new PerformanceCounter("Memory", "Available MBytes");
            //}
            //return (int)_计数器.NextValue();

            var __ComputerInfo = new ComputerInfo();
            return (int)(__ComputerInfo.AvailablePhysicalMemory / 1024 / 1024);
        }

        /// <summary>
        /// 单位: MBytes
        /// </summary>
        /// <returns></returns>
        public static int 查询系统内存总量()
        {
            var __ComputerInfo = new ComputerInfo();
            return (int)(__ComputerInfo.TotalPhysicalMemory / 1024 / 1024);
        }

    }
}
