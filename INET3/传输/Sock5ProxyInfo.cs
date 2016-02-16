using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace INET.传输
{
    public class Sock5ProxyInfo
    {
        public Sock5ProxyInfo()
        {
        }

        public Sock5ProxyInfo(string _proxyServerIP, int _proxyServerPort)
        {
        }

        public Sock5ProxyInfo(string _proxyServerIP, int _proxyServerPort, string _userName, string _password)
        {
        }


        public string Password { get; set; }
        public string ProxyServerIP { get; set; }
        public int ProxyServerPort { get; set; }
        public string UserName { get; set; }
    }
}
