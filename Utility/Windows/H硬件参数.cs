using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Net;
using System.Net.NetworkInformation;

namespace Utility.Windows
{
    public static class H硬件参数
    {
        public static string 获取CPUId()
        {
            string cpuInfo = "";//cpu序列号
            var cimobject = new ManagementClass("Win32_Processor");
            var moc = cimobject.GetInstances();
            foreach (ManagementObject mo in moc)
            {
                cpuInfo = mo.Properties["ProcessorId"].Value.ToString();
            }
            return cpuInfo;
        }

        public static string 获取硬盘Id()
        {
            var cimobject = new ManagementClass("Win32_DiskDrive");
            var moc = cimobject.GetInstances();
            foreach (ManagementObject mo in moc)
            {
                if ((string)mo.Properties["DeviceID"].Value == @"\\.\PHYSICALDRIVE0")
                {
                    return (string)mo.Properties["SerialNumber"].Value;

                }
            }
            return string.Empty;
        }
    }
}
