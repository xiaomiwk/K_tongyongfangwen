using System;
using System.Net.NetworkInformation;
using System.Web;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Threading;
using Utility.任务;

namespace Utility.Windows
{
    public static class H检测网络
    {
        public static bool 存在网络 { get { return NetworkInterface.GetIsNetworkAvailable(); } }

        #region 利用API方式获取网络链接状态

        private static int NETWORK_ALIVE_LAN = 0x00000001;
        private static int NETWORK_ALIVE_WAN = 0x00000002;
        private static int NETWORK_ALIVE_AOL = 0x00000004;
        [DllImport("sensapi.dll")]
        private extern static bool IsNetworkAlive(ref int flags);
        [DllImport("sensapi.dll")]
        private extern static bool IsDestinationReachable(string dest, IntPtr ptr);
        [DllImport("wininet.dll")]
        private extern static bool InternetGetConnectedState(out int connectionDescription, int reservedValue);

        public static bool IsConnected()
        {
            int desc = 0;
            bool state = InternetGetConnectedState(out desc, 0);
            return state;
        }
        public static bool IsLanAlive()
        {
            return IsNetworkAlive(ref NETWORK_ALIVE_LAN);
        }
        public static bool IsWanAlive()
        {
            return IsNetworkAlive(ref NETWORK_ALIVE_WAN);
        }
        public static bool IsAOLAlive()
        {
            return IsNetworkAlive(ref NETWORK_ALIVE_AOL);
        }
        public static bool IsDestinationAlive(string Destination)
        {
            return (IsDestinationReachable(Destination, IntPtr.Zero));
        }

        #endregion

        /// <summary>           
        /// 在指定时间内尝试连接指定主机上的指定端口。 （默认端口：80,默认链接超时：5000毫秒）           
        /// </summary>           
        /// <param name="HostNameOrIp">主机名称或者IP地址</param>           
        /// <param name="port">端口</param>           
        /// <param name="timeOut">超时时间</param>           
        /// <returns>返回布尔类型</returns>           
        public static bool IsHostAlive(string HostNameOrIp, int? port, int? timeOut)
        {
            TcpClient tc = new TcpClient();
            tc.SendTimeout = timeOut ?? 5000;
            tc.ReceiveTimeout = timeOut ?? 5000;
            if (timeOut.HasValue)
            {
                try
                {
                    H限时执行.执行(() =>
                    {
                        tc.Connect(HostNameOrIp, port ?? 80);
                    }, timeOut.Value);
                    var __结果 = tc.Connected;
                    tc.Close();
                    return __结果;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            bool isAlive;
            try
            {
                tc.Connect(HostNameOrIp, port ?? 80);
                isAlive = true;
            }
            catch
            {
                isAlive = false;
            }
            finally
            {
                tc.Close();
            }
            return isAlive;
        }

        /// <summary>           
        /// 在指定时间内尝试连接指定主机上的指定端口。 （默认端口：80,默认链接超时：5000毫秒）        
        /// </summary>            
        /// <param name= "hostname ">要连接到的远程主机的 DNS 名。</param>          
        /// <param name= "port ">要连接到的远程主机的端口号。 </param>           
        /// <param name= "millisecondsTimeout ">要等待的毫秒数，或 -1 表示无限期等待。</param>         
        /// <returns>已连接的一个 TcpClient 实例。</returns>          
        public static TcpClient Connect(string hostname, int? port, int? millisecondsTimeout)
        {
            try
            {
                ConnectorState cs = new ConnectorState();
                cs.Hostname = hostname;
                cs.Port = port ?? 80;
                ThreadPool.QueueUserWorkItem(new WaitCallback(ConnectThreaded), cs);
                if (cs.Completed.WaitOne(millisecondsTimeout ?? 5000, false))
                {
                    if (cs.TcpClient != null)
                        return cs.TcpClient;
                    return null;
                }
                else
                {
                    cs.Abort();
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }

        private static void ConnectThreaded(object state)
        {
            ConnectorState cs = (ConnectorState)state;
            cs.Thread = Thread.CurrentThread;
            try
            {
                TcpClient tc = new TcpClient(cs.Hostname, cs.Port);
                if (cs.Aborted)
                {
                    try
                    {
                        tc.GetStream().Close();
                    }
                    catch { }
                    try
                    {
                        tc.Close();
                    }
                    catch { }
                }
                else
                {
                    cs.TcpClient = tc;
                    cs.Completed.Set();
                }
            }
            catch (Exception e)
            {
                cs.Exception = e;
                cs.Completed.Set();
            }
        }

        private class ConnectorState
        {
            public string Hostname;
            public int Port;
            public volatile Thread Thread;
            public readonly ManualResetEvent Completed = new ManualResetEvent(false);
            public volatile TcpClient TcpClient;
            public volatile Exception Exception;
            public volatile bool Aborted;
            public void Abort()
            {
                if (Aborted != true)
                {
                    Aborted = true;
                    try
                    {
                        Thread.Abort();
                    }
                    catch { }
                }
            }
        }
    }
}

