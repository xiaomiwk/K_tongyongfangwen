using System;
using System.Net;

namespace INET.会话
{
    public class N被动会话 : N处理报文
    {
        private Func<N会话参数, bool> _处理请求;

        private Action<N会话参数> _处理通知;

        /// <param name="__处理请求">IPEndPoint: __远端, object: 接收到的业务对象, Action(object>: 发送响应, bool: true表示处理完毕</param>
        public N被动会话(Func<N会话参数, bool> __处理请求)
        {
            _处理请求 = __处理请求;
        }

        /// <param name="__处理通知">IPEndPoint: 发送方, object: 接收到的业务对象</param>
        public N被动会话(Action<N会话参数> __处理通知)
        {
            _处理通知 = __处理通知;
        }

        public override void 处理接收(IPEndPoint __远端, N事务 __事务, object __负载, IN上下文 __上下文)
        {
            Action<object> __发送响应 = q => this.发送响应(__远端, __事务, q);
            Action<object> __发送通知 = q => this.发送通知(__远端, q);
            if (_处理请求 != null)
            {
                if (_处理请求(new N会话参数(__远端, __负载, __发送响应, __发送通知)))
                {
                    this.关闭请求();
                }
            }
            else
            {
                _处理通知(new N会话参数(__远端, __负载, __发送响应, __发送通知));
            }
        }
    }
}
