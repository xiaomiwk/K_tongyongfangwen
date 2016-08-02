using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using INET;
using INET.会话;
using INET.传输;
using Utility.通用;
using 通用访问工具.DTO;
using 通用访问工具.报文;

namespace 通用访问工具.IBLL
{
    public class B访问设备 : N处理报文, IB访问设备
    {
        private INTCP客户端 __IN网络节点;

        private IN上下文 _IN上下文;

        DateTime _最后心跳时间;

        private int _心跳频率 = 10000;

        public void 连接(IPEndPoint __设备端口)
        {
            H日志输出.记录("创建客户端");
            __IN网络节点 = FN网络传输工厂.创建TCP客户端(__设备端口, new IPEndPoint(IPAddress.Any, 5555), P报文.消息头长度, P报文.解码消息长度);
            __IN网络节点.名称 = "客户端";
            //__客户端.自动重连 = true;
            _IN上下文 = new N上下文();
            __IN网络节点.收到消息 += (__远端地址, __消息) =>
            {
                var __报文 = H报文注册.解码(__消息);
                if (__报文 == null)
                {
                    return;
                }
                _IN上下文.收到报文(__远端地址, __报文);
            };
            __IN网络节点.已断开 += () => H日志输出.记录("客户端: 与服务器断开", string.Empty);
            __IN网络节点.已连接 += () =>
            {
                连接正常 = true;
                On已连接();
                On收到了通知(new M通知
                {
                    对象 = "系统",
                    概要 = "欢迎进入",
                    详细 = "",
                    重要性 = "普通"
                });

                Task.Factory.StartNew(() =>
                {
                    byte[] __心跳 = new P心跳().编码();
                    while (__IN网络节点.连接正常)
                    {
                        __IN网络节点.异步发送(__心跳);
                        Thread.Sleep(10000);
                    }
                });
                Task.Factory.StartNew(() =>
                {
                    _最后心跳时间 = DateTime.Now;
                    while (__IN网络节点.连接正常)
                    {
                        if (_最后心跳时间.AddMilliseconds(_心跳频率 * 5) < DateTime.Now)
                        {
                            On已断开(false);
                        }
                        Thread.Sleep(_心跳频率);
                    }
                });
            };
            H日志输出.记录("配置客户端上下文");
            _IN上下文.设置发送方法((__节点, __消息) => __IN网络节点.同步发送(__消息));
            _IN上下文.订阅报文(typeof(P心跳), () => this);
            _IN上下文.订阅报文(typeof(P通知), () => this);

            __IN网络节点.开启();
        }

        public void 断开()
        {
            __IN网络节点.断开();
            连接正常 = false;
            On已断开(true);
            On收到了通知(new M通知
            {
                对象 = "系统",
                概要 = "谢谢访问",
                详细 = "",
                重要性 = "普通"
            });
        }

        public bool 连接正常 { get; set; }

        public event Action<bool> 已断开;

        protected virtual void On已断开(bool __主动)
        {
            var handler = 已断开;
            if (handler != null) handler(__主动);
        }

        public bool 自动重连 { get; set; }

        public event Action 已连接;

        protected virtual void On已连接()
        {
            Action handler = 已连接;
            if (handler != null) handler();
        }

        public event Action<M通知> 收到了通知;

        protected virtual void On收到了通知(M通知 __通知)
        {
            var handler = 收到了通知;
            if (handler != null) handler(__通知);
        }

        public M查询对象列表响应 查询可访问对象()
        {
            var __接收报文 = H会话.请求<P查询对象列表S>(__IN网络节点.服务器地址, new P查询对象列表C(), _IN上下文);
            if (__接收报文 == null)
            {
                throw new M预计异常("查询可访问对象");
            }
            return __接收报文.响应;
        }

        public M查询对象明细响应 查询对象明细(string 对象名称)
        {
            var __请求报文 = new P查询对象明细C() { 请求 = new M查询对象明细请求 { 对象名称 = 对象名称 } };
            var __响应报文 = H会话.请求<P查询对象明细S>(__IN网络节点.服务器地址, __请求报文, _IN上下文);
            if (__响应报文 == null)
            {
                throw new M预计异常("查询对象明细:" + 对象名称);
            }
            return __响应报文.响应;
        }

        public void 异步执行(string 对象名, string 方法名, List<M方法参数> 参数列表)
        {
            var __请求报文 = new P执行方法C
            {
                请求 = new M执行方法请求
                {
                    对象名称 = 对象名,
                    方法名称 = 方法名,
                    参数 = 参数列表
                }
            };
            H会话.通知(__IN网络节点.服务器地址, __请求报文, _IN上下文);
        }

        public void 同步执行(string 对象名, string 方法名, List<M方法参数> 参数列表)
        {
            同步执行带返回值(对象名, 方法名, 参数列表);
        }

        public string 同步执行带返回值(string 对象名, string 方法名, List<M方法参数> 参数列表)
        {
            var __请求报文 = new P执行方法C
            {
                请求 = new M执行方法请求
                {
                    对象名称 = 对象名,
                    方法名称 = 方法名,
                    参数 = 参数列表
                }
            };
            var __响应报文 = H会话.请求<P执行方法S>(__IN网络节点.服务器地址, __请求报文, _IN上下文);
            if (__响应报文 == null)
            {
                throw new M预计异常(string.Format("同步执行: {0} - {1} 时无响应", 对象名, 方法名));
            }
            if (!__响应报文.响应.成功)
            {
                throw new M预计异常(string.Format("同步执行: {0} - {1} 时出错, {2}", 对象名, 方法名, __响应报文.响应.描述));
            }
            return __响应报文.响应.返回值;
        }

        public override void 处理接收(IPEndPoint __远端, IN事务报文 __报文, IN上下文 __上下文)
        {
            if (__报文 is P心跳)
            {
                _最后心跳时间 = DateTime.Now;
                return;
            }
            var __通知 = __报文 as P通知;
            if (__通知 != null)
            {
                On收到了通知(__通知.通知);
                return;
            }
        }
    }
}
