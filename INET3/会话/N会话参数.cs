using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace INET.会话
{
    public class N会话参数
    {
        public IPEndPoint 远端 { get; set; }

        public object 负载 { get; set; } 

        public Action<object> 发送响应 { get; set; } 
        
        public Action<object> 发送通知 { get; set; }

        public N会话参数(IPEndPoint __远端, object __负载, Action<object> __发送响应, Action<object> __发送通知)
        {
            this.远端 = __远端;
            this.负载 = __负载;
            this.发送响应 = __发送响应;
            this.发送通知 = __发送通知;
        }
    }
}
