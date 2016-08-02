using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace INET.传输
{
    /// <summary>
    /// 客户端TCP引擎接口。 
    /// </summary>
    public interface INTCP客户端 : IN网络节点
    {
        IPEndPoint 服务器地址 { get; set; }

        bool 连接正常 { get; }

        bool 自动重连 { get; set; }

        /// <summary>
        /// 默认值为10秒。
        /// </summary>
        int 自动重连重试间隔秒数 { get; set; }

        event Action 已断开;

        event Action 已连接;

        event Action 自动重连开始;

        /// <summary>
        /// 主动关闭与服务器的连接。如果自动重连为true，将引发自动重连。
        /// </summary>
        void 断开();

        /// <summary>
        /// 手动重连。如果当前处于连接状态，则直接返回。
        /// </summary>
        void 手动重连(int __重试次数, int __重试间隔秒数);

        /// <summary>
        /// 将消息异步发送给服务器，不经任何处理，直接发送。注意：如果引擎已经停止，则直接返回。   
        /// </summary>
        void 同步发送(byte[] __消息);

        /// <summary>
        /// 将消息同步发送给服务器，不经任何处理，直接发送。注意：如果引擎已经停止，则直接返回。   
        /// </summary>
        void 异步发送(byte[] __消息);
    }
}
