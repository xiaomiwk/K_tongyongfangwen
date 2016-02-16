using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace INET.传输
{
    /// <summary>
    /// Udp引擎基础接口。
    /// </summary>
    public interface INUDP : IN网络节点
    {
        ///// <summary>
        ///// 向指定的端点发送UDP消息。注意：如果引擎已经停止，则直接返回。
        ///// </summary>
        //void 同步发送(IPEndPoint __接收地址, byte[] __消息);

        ///// <summary>
        ///// 向指定的端点投递UDP消息。注意：如果引擎已经停止，则直接返回。
        ///// </summary>
        //void 异步发送(IPEndPoint __接收地址, byte[] __消息);
    }
}
