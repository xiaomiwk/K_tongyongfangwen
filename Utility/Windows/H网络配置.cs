using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.InteropServices;

namespace Utility.Windows
{
    /// <summary>
    /// 网络设置类，设置网络的各种参数（DNS、网关、子网掩码、IP）
    /// </summary>public class
    public static class H网络配置
    {
        static H网络配置()
        {
            NetworkChange.NetworkAddressChanged += (sender, e) => 触发网络配置变化();
        }

        public static event Action 网络配置变化;

        private static void 触发网络配置变化()
        {
            var handler = 网络配置变化;
            if (handler != null) handler();
        }

        public static List<IPAddress> 获取可用IP()
        {
            try
            {
                var __IP列表 = Dns.GetHostAddresses(Dns.GetHostName());
                return __IP列表 == null ? new List<IPAddress>() : __IP列表.Where(item => item.AddressFamily == AddressFamily.InterNetwork).ToList();
            }
            catch (Exception)
            {
                return new List<IPAddress>();
            }
        }

        public static List<IPAddress> 获取IP配置(bool __过滤回路 = true)
        {
            //// XP下不支持方法GetUnicastAddresses
            //var __IP列表 = IPGlobalProperties.GetIPGlobalProperties().GetUnicastAddresses();
            //var __结果 = (from __IP in __IP列表
            //            where
            //                __IP.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork
            //                && (!__过滤回路 || __IP.Address.ToString() != "127.0.0.1")
            //            select __IP.Address).ToList();
            //if (__结果.Count == 0)
            //{
            //    return new List<IPAddress> { IPAddress.Parse("127.0.0.1") };
            //}
            //return __结果;

            var __网络接口列表 = NetworkInterface.GetAllNetworkInterfaces();
            if (__网络接口列表.Length < 1)
            {
                return new List<IPAddress> { IPAddress.Parse("127.0.0.1") };
            }
            var __结果 = new List<IPAddress>();
            foreach (NetworkInterface __网络接口 in __网络接口列表)
            {
                if (__网络接口.NetworkInterfaceType == NetworkInterfaceType.Loopback)
                {
                    continue;
                }
                var __单播地址列表 = __网络接口.GetIPProperties().UnicastAddresses;
                if (__单播地址列表 != null)
                {
                    foreach (UnicastIPAddressInformation __单播地址 in __单播地址列表)
                    {
                        if (__单播地址.Address.AddressFamily == AddressFamily.InterNetwork)
                        {
                            __结果.Add(__单播地址.Address);
                        }
                    }
                }
            }
            if (__结果.Count == 0)
            {
                return new List<IPAddress> { IPAddress.Parse("127.0.0.1") };
            }
            return __结果;
        }

        public static string 获取MAC()
        {
            string NCid = "";
            var mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
            var moc = mc.GetInstances();
            foreach (ManagementObject mo in moc)
            {
                if ((bool)mo["IPEnabled"])
                    NCid = mo["MacAddress"].ToString();
                mo.Dispose();
            }
            return NCid;
        }

        public static void 查询网络(out List<string> __IP, out List<string> __掩码)
        {
            List<string> __网关;
            List<string> __DNS;
            查询网络(out __IP, out __掩码, out  __网关, out  __DNS);
        }

        public static void 添加IP(string __IP, string __掩码)
        {
            List<string> __当前IP列表;
            List<string> __当前掩码列表;
            查询网络(out __当前IP列表, out __当前掩码列表);
            if (__当前IP列表.Contains(__IP))
            {
                return;
            }
            __当前IP列表.Insert(0, __IP);
            __当前掩码列表.Insert(0, __掩码);
            设置网络(__当前IP列表.ToArray(), __当前掩码列表.ToArray());
        }

        public static void 删除IP(string __IP)
        {
            List<string> __当前IP列表;
            List<string> __当前掩码列表;
            查询网络(out __当前IP列表, out __当前掩码列表);
            var __匹配位置 = __当前IP列表.IndexOf(__IP);
            if (__匹配位置 < 0)
            {
                return;
            }
            __当前IP列表.RemoveAt(__匹配位置);
            __当前掩码列表.RemoveAt(__匹配位置);
            设置网络(__当前IP列表.ToArray(), __当前掩码列表.ToArray());
        }


        public static void 查询网络(out List<string> __IP, out List<string> __掩码, out List<string> __网关, out List<string> __DNS)
        {
            __IP = null;
            __掩码 = null;
            __网关 = null;
            __DNS = null;

            //ManagementObjectSearcher query = new ManagementObjectSearcher("SELECT * FROM Win32_NetworkAdapterConfiguration");
            //ManagementObjectCollection queryCollection = query.Get();
            //foreach (ManagementObject mo in queryCollection)
            //{
            //    if (mo["IPEnabled"].ToString() == "True")
            //        mac = mo["MacAddress"].ToString();
            //}
            var wmi = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection moc = wmi.GetInstances();
            foreach (ManagementObject mo in moc)
            {
                //如果没有启用IP设置的网络设备则跳过
                if (!(bool)mo["IPEnabled"])
                    continue;

                //IP地址和掩码
                var __temp = (string[])mo.GetPropertyValue("IPAddress");
                if (__temp != null)
                {
                    __IP = new List<string>((string[])mo.GetPropertyValue("IPAddress"));
                    __IP.RemoveAll(q => !q.Contains("."));
                    __掩码 = new List<string>((string[])mo.GetPropertyValue("IPSubnet"));
                    __掩码.RemoveAll(q => !q.Contains("."));
                }

                //网关地址
                __temp = (string[])mo.GetPropertyValue("DefaultIPGateway");
                if (__temp != null)
                {
                    __网关 = new List<string>(__temp);
                }

                //DNS地址
                __temp = (string[])mo.GetPropertyValue("DNSServerSearchOrder");
                if (__temp != null)
                {
                    __DNS = new List<string>(__temp);
                }
                break;
            }
        }

        /// <summary>
        /// 单项参数为null表示不设置该项
        /// </summary>
        public static void 设置网络(string[] __IP, string[] __掩码, string[] __网关 = null, string[] __DNS = null)
        {
            var wmi = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection moc = wmi.GetInstances();
            foreach (ManagementObject mo in moc)
            {
                //如果没有启用IP设置的网络设备则跳过
                if (!(bool)mo["IPEnabled"])
                    continue;

                //设置IP地址和掩码
                ManagementBaseObject inPar;
                if (__IP != null && __掩码 != null)
                {
                    inPar = mo.GetMethodParameters("EnableStatic");
                    inPar["IPAddress"] = __IP;
                    inPar["SubnetMask"] = __掩码;
                    mo.InvokeMethod("EnableStatic", inPar, null);
                }

                //设置网关地址
                if (__网关 != null)
                {
                    inPar = mo.GetMethodParameters("SetGateways");
                    inPar["DefaultIPGateway"] = __网关;
                    mo.InvokeMethod("SetGateways", inPar, null);
                }

                //设置DNS地址
                if (__DNS != null)
                {
                    inPar = mo.GetMethodParameters("SetDNSServerSearchOrder");
                    inPar["DNSServerSearchOrder"] = __DNS;
                    mo.InvokeMethod("SetDNSServerSearchOrder", inPar, null);
                }
            }
        }

        public static void 启用DHCP服务器()
        {
            var wmi = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection moc = wmi.GetInstances();
            foreach (ManagementObject mo in moc)
            {
                //如果没有启用IP设置的网络设备则跳过
                if (!(bool)mo["IPEnabled"])
                    continue;
                //重置DNS为空                
                mo.InvokeMethod("SetDNSServerSearchOrder", null);
                //开启DHCP                
                mo.InvokeMethod("EnableDHCP", null);
            }
        }

        [DllImport("Iphlpapi.dll")]
        private static extern int SendARP(Int32 dest, Int32 host, ref Int64 mac, ref Int32 length);

        [DllImport("Ws2_32.dll")]
        private static extern Int32 inet_addr(string ip);

        public static string 发送ARP(string remoteIP)
        {
            Int32 ldest = inet_addr(remoteIP); //目的ip 
            try
            {
                Int64 macinfo = 0;
                Int32 len = 6;
                SendARP(ldest, 0, ref macinfo, ref len);
                return Convert.ToString(macinfo, 16);
            }
            catch (Exception err)
            {
                Debug.WriteLine("Error:{0}", err.Message);
            }
            return null;
        }
    }
}



//[DllImport("wininet.dll")]
//private static extern bool InternetGetConnectedState(out int conn, int val);

///// <summary>
///// true表示连通,false表示断开
///// </summary>
///// <returns></returns>
//public static bool 获取网络连接状态()
//{
//    //int result;
//    //return InternetGetConnectedState(out result, 0);
//}

//public static void ShowNetworkInterfaces()
//{
//    IPGlobalProperties computerProperties = IPGlobalProperties.GetIPGlobalProperties();
//    NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
//    Debug.WriteLine("Interface information for {0}.{1}     ",
//                      computerProperties.HostName, computerProperties.DomainName);
//    if (nics == null || nics.Length < 1)
//    {
//        Debug.WriteLine("  No network interfaces found.");
//        return;
//    }

//    Debug.WriteLine("  NetworkAvailable......................... : {0}", NetworkInterface.GetIsNetworkAvailable());
//    Debug.WriteLine("  Number of interfaces .................... : {0}", nics.Length);
//    foreach (NetworkInterface adapter in nics)
//    {
//        IPInterfaceProperties properties = adapter.GetIPProperties();
//        Debug.WriteLine();
//        Debug.WriteLine(adapter.Description);
//        Debug.WriteLine(String.Empty.PadLeft(adapter.Description.Length, '='));
//        Debug.WriteLine("  Interface type .......................... : {0}", adapter.NetworkInterfaceType);
//        Debug.WriteLine("  Physical Address ........................ : {0}",
//                          adapter.GetPhysicalAddress().ToString());
//        Debug.WriteLine("  Operational status ...................... : {0}",
//                          adapter.OperationalStatus);
//        string versions = "";

//        // Create a display string for the supported IP versions.
//        if (adapter.Supports(NetworkInterfaceComponent.IPv4))
//        {
//            versions = "IPv4";
//        }
//        if (adapter.Supports(NetworkInterfaceComponent.IPv6))
//        {
//            if (versions.Length > 0)
//            {
//                versions += " ";
//            }
//            versions += "IPv6";
//        }
//        Debug.WriteLine("  IP version .............................. : {0}", versions);
//        ShowIPAddresses(properties);

//        // The following information is not useful for loopback adapters.
//        if (adapter.NetworkInterfaceType == NetworkInterfaceType.Loopback)
//        {
//            continue;
//        }
//        Debug.WriteLine("  DNS suffix .............................. : {0}",
//                          properties.DnsSuffix);

//        string label;
//        if (adapter.Supports(NetworkInterfaceComponent.IPv4))
//        {
//            IPv4InterfaceProperties ipv4 = properties.GetIPv4Properties();
//            Debug.WriteLine("  IsDhcpEnabled...................................... : {0}", ipv4.IsDhcpEnabled);
//            //    Debug.WriteLine("  MTU...................................... : {0}", ipv4.Mtu);
//            //    if (ipv4.UsesWins)
//            //    {

//            //        IPAddressCollection winsServers = properties.WinsServersAddresses;
//            //        if (winsServers.Count > 0)
//            //        {
//            //            label = "  WINS Servers ............................ :";
//            //            //ShowIPAddresses(label, winsServers);
//            //        }
//            //    }
//        }

//        //Debug.WriteLine("  DNS enabled ............................. : {0}",
//        //                  properties.IsDnsEnabled);
//        //Debug.WriteLine("  Dynamically configured DNS .............. : {0}",
//        //                  properties.IsDynamicDnsEnabled);
//        //Debug.WriteLine("  Receive Only ............................ : {0}",
//        //                  adapter.IsReceiveOnly);
//        //Debug.WriteLine("  Multicast ............................... : {0}",
//        //                  adapter.SupportsMulticast);
//        //ShowInterfaceStatistics(adapter);

//        Debug.WriteLine();
//    }


//}

//public static void ShowIPAddresses(IPInterfaceProperties adapterProperties)
//{
//    IPAddressCollection dnsServers = adapterProperties.DnsAddresses;
//    //if (dnsServers != null)
//    //{
//    //    foreach (IPAddress dns in dnsServers)
//    //    {
//    //        Debug.WriteLine("  DNS Servers ............................. : {0}",
//    //                          dns.ToString()
//    //            );
//    //    }
//    //}
//    //IPAddressInformationCollection anyCast = adapterProperties.AnycastAddresses;
//    //if (anyCast != null)
//    //{
//    //    foreach (IPAddressInformation any in anyCast)
//    //    {
//    //        Debug.WriteLine("  Anycast Address .......................... : {0} {1} {2}",
//    //                          any.Address,
//    //                          any.IsTransient ? "Transient" : "",
//    //                          any.IsDnsEligible ? "DNS Eligible" : ""
//    //            );
//    //    }
//    //    Debug.WriteLine();
//    //}

//    //MulticastIPAddressInformationCollection multiCast = adapterProperties.MulticastAddresses;
//    //if (multiCast != null)
//    //{
//    //    foreach (IPAddressInformation multi in multiCast)
//    //    {
//    //        Debug.WriteLine("  Multicast Address ....................... : {0} {1} {2}",
//    //                          multi.Address,
//    //                          multi.IsTransient ? "Transient" : "",
//    //                          multi.IsDnsEligible ? "DNS Eligible" : ""
//    //            );
//    //    }
//    //    Debug.WriteLine();
//    //}
//    UnicastIPAddressInformationCollection uniCast = adapterProperties.UnicastAddresses;
//    if (uniCast != null)
//    {
//        string lifeTimeFormat = "dddd, MMMM dd, yyyy  hh:mm:ss tt";
//        foreach (UnicastIPAddressInformation uni in uniCast)
//        {
//            DateTime when;

//            Debug.WriteLine("  Unicast Address ......................... : {0}", uni.Address);
//            Debug.WriteLine("     Prefix Origin ........................ : {0}", uni.PrefixOrigin);
//            Debug.WriteLine("     Suffix Origin ........................ : {0}", uni.SuffixOrigin);
//            Debug.WriteLine("     Duplicate Address Detection .......... : {0}",
//                              uni.DuplicateAddressDetectionState);

//            // Format the lifetimes as Sunday, February 16, 2003 11:33:44 PM
//            // if en-us is the current culture.

//            // Calculate the date and time at the end of the lifetimes.    
//            //when = DateTime.UtcNow + TimeSpan.FromSeconds(uni.AddressValidLifetime);
//            //when = when.ToLocalTime();
//            //Debug.WriteLine("     Valid Life Time ...................... : {0}",
//            //                  when.ToString(lifeTimeFormat, System.Globalization.CultureInfo.CurrentCulture)
//            //    );
//            //when = DateTime.UtcNow + TimeSpan.FromSeconds(uni.AddressPreferredLifetime);
//            //when = when.ToLocalTime();
//            //Debug.WriteLine("     Preferred life time .................. : {0}",
//            //                  when.ToString(lifeTimeFormat, System.Globalization.CultureInfo.CurrentCulture)
//            //    );

//            //when = DateTime.UtcNow + TimeSpan.FromSeconds(uni.DhcpLeaseLifetime);
//            //when = when.ToLocalTime();
//            //Debug.WriteLine("     DHCP Leased Life Time ................ : {0}",
//            //                  when.ToString(lifeTimeFormat, System.Globalization.CultureInfo.CurrentCulture)
//            //    );
//        }
//        Debug.WriteLine();
//    }
//}

//public static void GetTcpConnections()
//{
//    IPGlobalProperties properties = IPGlobalProperties.GetIPGlobalProperties();
//    Debug.WriteLine();
//    Debug.WriteLine("IPGlobalProperties GetActiveTcpConnections.");
//    TcpConnectionInformation[] connections = properties.GetActiveTcpConnections();
//    foreach (TcpConnectionInformation t in connections)
//    {
//        Debug.Write("    {0} ", t.LocalEndPoint.Address);
//        Debug.Write(" - {0} ", t.RemoteEndPoint.Address);
//        Debug.WriteLine("{0}", t.State);
//    }
//    Debug.WriteLine();
//}
