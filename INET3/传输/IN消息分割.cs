using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace INET.传输
{
    public interface IN消息分割
    {
        int 最大消息长度 { set; }

        void 接收数据(IPEndPoint 节点, byte[] 消息);

        void 清空(IPEndPoint 节点);

        event Action<IPEndPoint, byte[]> 已分割消息;

        void 清空所有();
    }
}
