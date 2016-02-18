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
        /// <summary>
        /// 服务器允许最大的同时在线客户端数, 默认100。
        /// </summary>
        int 允许客户端数量 { get; set; }

        /// <summary>
        /// 给每个连接发送数据超时时间（单位：毫秒。默认为-1，表示无限）。如果在指定的时间内未将数据发送完，则关闭对应的连接。 该属性只对同步发送有效。
        /// </summary>
        int 发送超时毫秒 { get; set; }

        /// <summary> 
        /// 每个通道连接上允许最大的等待发送以及正在发送的消息个数。
        /// 当等待发送以及正在发送的消息个数超过该值时，将关闭对应的连接。如果设置为0，则表示不作限制。默认值为0。       
        /// </summary>
        int 发送等待最大数量 { get; set; }

        bool 允许监听 { get; set; }

        void 断开客户端(IPEndPoint __客户端节点, string __描述 = "");

        void 断开所有客户端();

        /// <summary>
        /// 获取所有在线连接的客户端的地址和连接时间。
        /// </summary>        
        List<Tuple<IPEndPoint,DateTime>> 获取所有客户端();

        bool 验证在线(IPEndPoint __客户端节点);

        bool 信道忙(IPEndPoint __客户端节点);


        event Action<IPEndPoint> 客户端已断开;

        event Action<IPEndPoint> 客户端已连接;

        event Action<bool> 监听状态变化;
    }
}
