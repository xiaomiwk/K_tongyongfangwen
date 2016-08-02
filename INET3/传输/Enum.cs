using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace INET.传输
{
    public enum E传输层协议
    {
        TCP = 0,
        UDP
    }

    public enum E流分割方式
    {
        消息头 = 0,
        结束符
    }

}
