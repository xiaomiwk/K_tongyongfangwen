using System;
using System.Net;
using System.Threading;

namespace INET.会话
{
    public class N主动会话
    {
        private IN上下文 _上下文;

        private IPEndPoint _远端;

        public N主动会话(IN上下文 __上下文)
        {
            this._上下文 = __上下文;
        }

        public N主动会话(IN上下文 __上下文, IPEndPoint __远端)
        {
            this._上下文 = __上下文;
            this._远端 = __远端;
        }

        public T 请求<T>(IPEndPoint __远端, object __发送对象, int __超时毫秒 = 2000)
        {
            T __结果 = default(T);
            using (var __同步信号 = new AutoResetEvent(false))
            {
                var __B处理报文 = new B处理报文
                {
                    收到报文 = (远端, 事务, 负载, __上下文) =>
                    {
                        var __temp = (T)负载;
                        if (__temp != null)
                        {
                            __结果 = __temp;
                            try
                            {
                                __同步信号.Set();
                            }
                            catch (Exception)
                            {
                            }
                        }
                    }
                };
                var __耗时检测 = System.Diagnostics.Stopwatch.StartNew();
                var __事务=__B处理报文.开启请求(__远端, __发送对象, _上下文);
                var __收到信号 = __同步信号.WaitOne(__超时毫秒);
                __耗时检测.Stop();
                if (__耗时检测.ElapsedMilliseconds > 500)
                {
                    H日志输出.记录(string.Format("{2}: 请求 {0} 时耗时 {1} 毫秒", __发送对象, __耗时检测.ElapsedMilliseconds, _上下文.名称));
                }
                if (!__收到信号)
                {
                    H日志输出.记录(string.Format("{1}: 请求 {0} 时未响应", __发送对象, _上下文.名称));
                }
                __B处理报文.收到报文 = null;
                __B处理报文.关闭请求();
                return __结果;
            }
        }

        public T 请求<T>(object __发送对象, int __超时毫秒 = 2000)
        {
            if (_远端 == null)
            {
                throw new ApplicationException("未指定对端");
            }
            return 请求<T>(_远端, __发送对象, __超时毫秒);
        }

        /// <param name="__远端"></param>
        /// <param name="__发送对象">业务对象, 不是报文</param>
        /// <param name="__处理接收">object: 接收到的业务对象, Action(object>: 响应方法, bool: true表示处理完毕, false表示未完</param>
        /// <param name="__超时毫秒"></param>
        /// <returns></returns>
        public bool 请求(IPEndPoint __远端, object __发送对象, Func<N会话参数, bool> __处理接收, int __超时毫秒 = 2000)
        {
            bool __结果 = false;
            using (var __同步信号 = new AutoResetEvent(false))
            {
                var __B处理报文 = new B处理报文
                {
                    收到报文 = (远端, 事务, 负载, __上下文) =>
                    {
                        Action<object> __发送响应 = q => _上下文.发送报文(__远端, 事务.颠倒(), q);
                        Action<object> __发送通知 = q => _上下文.发送通知(__远端, q);
                        if (__处理接收(new N会话参数(远端, 负载, __发送响应, __发送通知)))
                        {
                            __结果 = true;
                            try
                            {
                                __同步信号.Set();
                            }
                            catch (Exception)
                            {
                            }
                        }
                    }
                };
                var __耗时检测 = System.Diagnostics.Stopwatch.StartNew();
                var __事务 = __B处理报文.开启请求(__远端, __发送对象, _上下文);
                var __收到信号 = __同步信号.WaitOne(__超时毫秒);
                __耗时检测.Stop();
                if (__耗时检测.ElapsedMilliseconds > 1000)
                {
                    H日志输出.记录(string.Format("{2}: 请求 {0} 时耗时 {1} 毫秒", __发送对象, __耗时检测.ElapsedMilliseconds, _上下文.名称));
                }
                if (!__收到信号)
                {
                    H日志输出.记录(string.Format("{1}: 请求 {0} 时未响应", __发送对象, _上下文.名称));
                }
                __B处理报文.收到报文 = null;
                __B处理报文.关闭请求();
                return __结果;
            }
        }

        public bool 请求(object __发送对象, Func<N会话参数, bool> __处理接收, int __超时毫秒 = 2000)
        {
            if (_远端 == null)
            {
                throw new ApplicationException("未指定对端");
            }
            return 请求(_远端, __发送对象, __处理接收, __超时毫秒);
        }

        public void 通知(IPEndPoint __远端, object __发送对象)
        {
            var __发送报文 = new N事务 { 发方事务 = 0, 收方事务 = 0 };
            _上下文.发送报文(__远端, __发送报文, __发送对象);
        }

        public void 通知(object __发送对象)
        {
            if (_远端 == null)
            {
                throw new ApplicationException("未指定对端");
            }
            通知(_远端, __发送对象);
        }

        private class B处理报文 : N处理报文
        {
            public override void 处理接收(IPEndPoint __远端, N事务 __事务, object __负载, IN上下文 __上下文)
            {
                if (收到报文 != null)
                {
                    收到报文(__远端,__事务, __负载, __上下文);
                }
            }

            public Action<IPEndPoint, N事务, object, IN上下文> 收到报文;
        }
    }
}
