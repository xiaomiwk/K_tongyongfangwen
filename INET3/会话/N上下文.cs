using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;

namespace INET.会话
{
    public class N上下文 : IN上下文
    {
        private readonly H多事件<string> _报文处理 = new H多事件<string>();

        private readonly H线程队列 _队列;

        private readonly ConcurrentDictionary<int, List<IN处理报文>> _所有请求 = new ConcurrentDictionary<int, List<IN处理报文>>();

        private int _本地业务索引 = 1;

        private Action<IPEndPoint, byte[]> 发送方法;

        public string 名称 { get; set; }

        public IN编解码器 编解码器 { get; set; }

        public N上下文(IN编解码器 __编解码器, string __名称 = "")
        {
            编解码器 = __编解码器;
            名称 = __名称;
            //_队列 = new H任务队列();
            _队列 = new H线程队列();
        }

        public void 设置发送方法(Action<IPEndPoint, byte[]> __方法)
        {
            发送方法 = __方法;
        }

        public void 发送报文(IPEndPoint 远端, N事务 事务, object 负载)
        {
            发送方法(远端, 编解码器.编码(事务, 负载));
        }

        public void 发送通知(IPEndPoint 远端, object 负载)
        {
            var 事务 = new N事务 { 发方事务 = 0, 收方事务 = 0 };
            发送方法(远端, 编解码器.编码(事务, 负载));
        }

        public void 订阅报文(string 报文类型, Func<IN处理报文> 处理请求)
        {
            _报文处理.注册(报文类型, 处理请求);
        }

        public void 注销订阅(string 报文类型, Func<IN处理报文> 处理请求)
        {
            _报文处理.注销(报文类型, 处理请求);
        }

        public void 收到报文(IPEndPoint __来源, byte[] __报文)
        {
            Tuple<N事务, object> __解码;
            try
            {
                __解码 = 编解码器.解码(__报文);
            }
            catch (Exception ex)
            {
                H日志输出.记录(string.Format("{0}: 解码失败, {1}", 名称, ex.Message), string.Format("从 [{0}] 收 {1}", __来源, BitConverter.ToString(__报文)), TraceEventType.Error);
                return;
            }
            var __事务 = __解码.Item1;
            var __负载 = __解码.Item2;
            H日志输出.记录(string.Format("{0}: 从 [{1}] 收", 名称, __来源), string.Format("事务:{0}-{1}; 功能码:{2}; 负载:{3};", __事务.发方事务, __事务.收方事务, __事务.功能码, __负载));

            _队列.添加事项(__来源.ToString(), __事务.通道标识 ?? "", new M数据(__来源, __事务, __负载), 处理报文, true);
        }

        public void 注销节点(IPEndPoint __远端)
        {
            _队列.关闭队列(__远端.ToString());
        }

        void 处理报文(object __参数)
        {
            var __数据 = __参数 as M数据;
            if (__数据 == null)
            {
                return;
            }
            //H日志输出.记录(string.Format("{0}: 处理 [{1}] 的报文", 名称, __数据.来源), string.Format("事务:{0}-{1}; 功能码:{2}; 负载:{3};", __数据.事务.发方事务, __数据.事务.收方事务, __数据.事务.功能码, __数据.负载));
            var __来源 = __数据.来源;
            var __事务 = __数据.事务;
            var __负载 = __数据.负载;
            //通知或请求开始，转发报文后获得会话处理对象，加入到当前会话中
            if (__事务.收方事务 == 0)
            {
                var __请求型会话 = __事务.发方事务 != 0;
                var __本地业务标识 = 0;
                if (__请求型会话)
                {
                    __本地业务标识 = 生成业务标识();
                    __事务.收方事务 = __本地业务标识;
                }
                var __报文处理列表 = _报文处理.触发返回(__事务.功能码);
                foreach (var __处理方法 in __报文处理列表)
                {
                    var __处理会话 = (IN处理报文)__处理方法;
                    __处理会话.本地凭据 = __本地业务标识;
                    __处理会话.远端凭据 = __事务.发方事务;
                    __处理会话.上下文 = this;
                    if (__请求型会话)
                    {
                        H日志输出.记录(string.Format("{1}: 开 {0}", __本地业务标识, 名称));
                        if (!_所有请求.ContainsKey(__本地业务标识))
                        {
                            _所有请求[__本地业务标识] = new List<IN处理报文>();
                        }
                        _所有请求[__本地业务标识].Add(__处理会话);
                    }
                    __处理会话.处理接收(__来源, __事务, __负载, this);
                }
                return;
            }

            //请求过程中，匹配到当前会话中的处理对象，处理报文
            List<IN处理报文> __会话列表;
            if (_所有请求.TryGetValue(__事务.收方事务, out __会话列表))
            {
                foreach (var __处理会话 in __会话列表)
                {
                    __处理会话.远端凭据 = __事务.发方事务;
                    __处理会话.处理接收(__来源, __事务, __负载, this);
                }
            }
        }

        public int 注册请求(IN处理报文 处理请求)
        {
            var __本地业务标识 = 生成业务标识();
            _所有请求[__本地业务标识] = new List<IN处理报文> { 处理请求 };
            H日志输出.记录(string.Format("{1}: 开 {0}", __本地业务标识, 名称));
            return __本地业务标识;
        }

        public void 注销请求(int 凭据)
        {
            H日志输出.记录(string.Format("{1}: 关 {0}", 凭据, 名称));
            List<IN处理报文> temp;
            _所有请求.TryRemove(凭据, out temp);
        }

        private int 生成业务标识()
        {
            var temp = _本地业务索引++;
            return temp == 0 ? _本地业务索引++ : temp;
        }

        private class M数据
        {
            public IPEndPoint 来源 { get; private set; }
            public N事务 事务 { get; private set; }
            public object 负载 { get; private set; }

            public M数据(IPEndPoint __来源, N事务 __事务, object __负载)
            {
                来源 = __来源;
                事务 = __事务;
                负载 = __负载;
            }

            public override string ToString()
            {
                return string.Format("来源:{0}, 负载:{1}", 来源, 负载);
            }
        }
    }
}
