using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace INET.传输
{
    /// <summary>
    /// TCP服务端引擎接口。
    /// </summary>
    public interface INTCP服务器 : IN网络节点
    {
        E流分割方式 分割方式 { get; }

        void 断开客户端(IPEndPoint __客户端节点, string __描述 = "");

        void 断开所有客户端();

        /// <summary>
        /// 获取所有在线连接的客户端的地址和连接时间。
        /// </summary>        
        List<Tuple<IPEndPoint,DateTime>> 获取所有客户端();

        bool 验证在线(IPEndPoint __客户端节点);

        event Action<IPEndPoint> 客户端已断开;

        event Action<IPEndPoint> 客户端已连接;

    }
}
