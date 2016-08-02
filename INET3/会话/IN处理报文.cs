using System.Net;

namespace INET.会话
{
    public interface IN处理报文
    {
        int 本地凭据 { get; set; }

        int 远端凭据 { get; set; }

        IN上下文 上下文 { get; set; }

        void 处理接收(IPEndPoint __远端, N事务 __事务, object __负载, IN上下文 __上下文);

        void 发送响应(IPEndPoint __远端, N事务 __事务, object __负载);

        N事务 开启请求(IPEndPoint __远端, object __负载, IN上下文 __上下文);

        void 关闭请求();

        void 关闭请求(int __凭据);

        void 发送通知(IPEndPoint __远端, object __负载);
    }
}
