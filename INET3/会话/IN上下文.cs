using System;
using System.Collections.Generic;
using System.Net;

namespace INET.会话
{
    public interface IN上下文
    {
        string 名称 { get; }

        IN编解码器 编解码器 { get; }

        void 设置发送方法(Action<IPEndPoint, byte[]> __方法);

        void 发送报文(IPEndPoint __远端, N事务 __事务, object __负载);

        void 发送通知(IPEndPoint __远端, object __负载);

        void 订阅报文(string __功能码, Func<IN处理报文> __处理报文);

        void 注销订阅(string __功能码, Func<IN处理报文> __处理报文);

        void 收到报文(IPEndPoint __远端, byte[] __数据);

        int 注册请求(IN处理报文 __处理请求);

        void 注销请求(int __凭据);

        void 注销节点(IPEndPoint __远端);
    }

}
