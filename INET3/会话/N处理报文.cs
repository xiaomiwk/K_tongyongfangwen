using System.Net;

namespace INET.会话
{
    public abstract class N处理报文 : IN处理报文
    {
        public int 本地凭据 { get; set; }

        public int 远端凭据 { get; set; }

        public IN上下文 上下文 { get; set; }

        public abstract void 处理接收(IPEndPoint __远端, N事务 事务, object 负载, IN上下文 __上下文);

        public void 发送响应(IPEndPoint __远端, N事务 __事务, object __负载)
        {
            __事务.发方事务 = 本地凭据;
            __事务.收方事务 = 远端凭据;
            上下文.发送报文(__远端, __事务, __负载);
        }

        public N事务 开启请求(IPEndPoint __远端, object __负载, IN上下文 __上下文)
        {
            var __事务 = new N事务();
            上下文 = __上下文;
            本地凭据 = __上下文.注册请求(this);
            __事务.发方事务 = 本地凭据;
            __事务.收方事务 = 0;
            上下文.发送报文(__远端,  __事务, __负载);
            return __事务;
        }

        public void 关闭请求()
        {
            上下文.注销请求(本地凭据);
        }

        public void 关闭请求(int 凭据)
        {
            上下文.注销请求(凭据);
        }

        public void 发送通知(IPEndPoint __远端, object __负载)
        {
            上下文.发送报文(__远端, new N事务 { 发方事务 = 0, 收方事务 = 0 }, __负载);
        }
    }
}
