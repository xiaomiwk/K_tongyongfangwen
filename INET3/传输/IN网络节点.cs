using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;

namespace INET.传输
{
    /// <summary>
    /// 所有网络引擎（包括服务端引擎、客户端引擎、TCP引擎、UDP引擎、二进制引擎、文本引擎）的基础接口。    
    /// </summary>
    public interface IN网络节点 : IDisposable
    {
        string 名称 { get; set; }

        /// <summary>
        /// 如果端口设定其值小于等于0，则表示由系统自动分配。      
        /// 如果IP设为any则表示绑定本地所有IP。
        /// 对于客户端引擎，其值会在初始化完成后被修改
        /// 注意，引擎初始化完成后，不能再修改该属性。
        /// </summary>
        IPEndPoint 本机地址 { get; set; }

        E传输层协议 协议 { get; }

        DateTime 开启时间 { get; }

        /// <summary>
        /// 默认8192
        /// </summary>
        int 发送缓冲区大小 { get; set; }

        /// <summary>
        /// 默认8192
        /// </summary>
        int 接收缓冲区大小 { get; set; }

        /// <summary>
        /// 默认为100k。当接收到的消息尺寸超过"最大消息长度"时，将会丢弃数据（对UDP）。
        /// </summary>
        int 最大消息长度 { get; set; }

        /// <summary>
        /// 初始化并启动网络引擎。如果修改了引擎配置参数，在应用新参数之前必须先重新调用该方法初始化引擎。
        /// </summary>
        void 开启();

        /// <summary>
        /// 向指定的端点发送消息。注意：如果引擎已经停止，则直接返回。
        /// </summary>
        void 同步发送(IPEndPoint __接收地址, byte[] __消息);

        /// <summary>
        /// 向指定的端点投递消息。注意：如果引擎已经停止，则直接返回。
        /// </summary>
        void 异步发送(IPEndPoint __接收地址, byte[] __消息);

        /// <summary>
        /// 接收到一个完整的消息时触发该事件。
        /// </summary>
        event Action<IPEndPoint, byte[]> 收到消息;

        event Action<IPEndPoint, byte[]> 发送成功;

        void 关闭();

        bool Disposed { get; }

    }

}
