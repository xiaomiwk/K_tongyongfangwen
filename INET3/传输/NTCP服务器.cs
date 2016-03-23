using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace INET.传输
{
    class NTCP服务器 : INTCP服务器
    {
        private TcpListener _侦听器;

        private Thread _侦听线程;

        private readonly Dictionary<IPEndPoint, M客户端> _所有客户端 = new Dictionary<IPEndPoint, M客户端>();

        private readonly object _客户端操作锁 = new object();

        private readonly IN消息分割 _IN消息分割;

        public string 名称 { get; set; }

        public IPEndPoint 本机地址 { get; set; }

        public E传输层协议 协议 { get { return E传输层协议.TCP; } }

        public E流分割方式 分割方式 { get; private set; }

        public int 允许客户端数量 { get; set; }

        /// <summary>
        /// 给每个连接发送数据超时时间（单位：毫秒。默认为-1，表示无限）。如果在指定的时间内未将数据发送完，则关闭对应的连接。       
        /// </summary>
        public int 发送超时毫秒 { get; set; }

        /// <summary>
        /// 每个通道连接上允许最大的等待发送【包括投递】以及正在发送的消息个数。
        /// 当等待发送以及正在发送的消息个数超过该值时，将关闭对应的连接。如果设置为0，则表示不作限制。默认值为0。       
        /// </summary>
        public int 发送等待最大数量 { get; set; }

        public bool 允许监听 { get; set; }

        public DateTime 开启时间 { get; private set; }

        public int 发送缓冲区大小 { get; set; }

        public int 接收缓冲区大小 { get; set; }

        public int 最大消息长度 { get; set; }

        public int 当前客户端数量 { get { return _所有客户端.Count; } }

        private bool _同步处理 = false;

        public NTCP服务器(IPEndPoint __本机地址, List<byte[]> __结束符)
            : this(__本机地址)
        {
            分割方式 = E流分割方式.结束符;
            _IN消息分割 = new N结束符分割(this.最大消息长度, __结束符);
            _IN消息分割.已分割消息 += _IN消息分割_分割了报文;
        }

        public NTCP服务器(IPEndPoint __本机地址, int __消息头长度, Func<byte[], int> __解析消息体长度)
            : this(__本机地址)
        {
            分割方式 = E流分割方式.消息头;
            _IN消息分割 = new N消息头分割(this.最大消息长度, __消息头长度, __解析消息体长度);
            _IN消息分割.已分割消息 += _IN消息分割_分割了报文;
        }

        protected NTCP服务器(IPEndPoint __本机地址)
        {
            this.本机地址 = __本机地址;
            this.允许监听 = false;
            this.最大消息长度 = 100000;
            this.发送缓冲区大小 = 8192;
            this.接收缓冲区大小 = 8192;
            允许客户端数量 = 100;
        }

        void _IN消息分割_分割了报文(IPEndPoint __客户端节点, byte[] __消息)
        {
            On收到消息(__客户端节点, __消息);
        }

        public void 开启()
        {
            if (名称 == null)
            {
                名称 = string.Format("服务器 [{0}]", 本机地址);
            }
            _IN消息分割.最大消息长度 = this.最大消息长度;
            开启时间 = DateTime.Now;
            H日志输出.记录(string.Format("{0}: 监听 {1}", 名称, this.本机地址), null, TraceEventType.Information);
            this.允许监听 = true;
            _侦听器 = new TcpListener(this.本机地址);
            _侦听器.Start();
            if (_同步处理)
            {
                _侦听线程 = new Thread(侦听) { IsBackground = true };
                _侦听线程.Start();
            }
            else
            {
                _侦听器.BeginAcceptTcpClient(异步侦听, _侦听器);
            }
        }

        public void 异步侦听(IAsyncResult ar)
        {
            try
            {
                TcpClient __连接 = _侦听器.EndAcceptTcpClient(ar);
                var __数据流 = __连接.GetStream();
                var __缓存 = new byte[接收缓冲区大小];
                var __客户端 = new M客户端
                {
                    节点 = (IPEndPoint)__连接.Client.RemoteEndPoint,
                    数据流 = __数据流,
                    开始时间 = DateTime.Now,
                    连接 = __连接
                };
                __数据流.BeginRead(__缓存, 0, 接收缓冲区大小, 异步接收数据, new Tuple<NetworkStream, byte[], M客户端>(__数据流, __缓存, __客户端));
                lock (_客户端操作锁)
                {
                    _所有客户端[__客户端.节点] = __客户端;
                }
                H日志输出.记录(string.Format("{0}已连接, 总数: {1}", __客户端.节点, _所有客户端.Count), null, TraceEventType.Information);
                On客户端连接(__客户端.节点);
            }
            catch (Exception ex)
            {
                if (this.允许监听)
                {
                    H日志输出.记录(ex, "侦听异常");
                }
                else
                {
                    return;
                }
            }
            _侦听器.BeginAcceptTcpClient(异步侦听, _侦听器);
        }

        public void 异步接收数据(IAsyncResult ar)
        {
            var __绑定 = ar.AsyncState as Tuple<NetworkStream, byte[], M客户端>;
            var __数据流 = __绑定.Item1;
            var __缓存 = __绑定.Item2;
            var __客户端 = __绑定.Item3;
            try
            {
                int __实际接收长度 = __数据流.EndRead(ar);
                if (__实际接收长度 == 0)
                {
                    if (_所有客户端.ContainsKey(__客户端.节点))
                    {
                        断开客户端(__客户端.节点, "接收长度为0");
                    }
                    return;
                }
                var __实际接收字节 = new byte[__实际接收长度];
                Buffer.BlockCopy(__缓存, 0, __实际接收字节, 0, __实际接收长度);
                //H日志输出.记录(名称 + string.Format(": 从 [{0}] 收", __客户端.节点), BitConverter.ToString(__实际接收字节));
                _IN消息分割.接收数据(__客户端.节点, __实际接收字节);
                __数据流.BeginRead(__缓存, 0, 接收缓冲区大小, 异步接收数据, new Tuple<NetworkStream, byte[], M客户端>(__数据流, __缓存, __客户端));
            }
            catch (Exception ex)
            {
                if (_所有客户端.ContainsKey(__客户端.节点))
                {
                    断开客户端(__客户端.节点, ex.Message);
                }
                return;
            }
        }

        void 侦听(object arg)
        {
            while (true)
            {
                if (!允许监听 || 当前客户端数量 >= 允许客户端数量)
                {
                    Thread.Sleep(1000);
                    continue;
                }
                try
                {
                    var __连接 = _侦听器.AcceptTcpClient();
                    if (!允许监听 || 当前客户端数量 >= 允许客户端数量)
                    {
                        continue;
                    }
                    __连接.SendBufferSize = 发送缓冲区大小;
                    __连接.ReceiveBufferSize = 接收缓冲区大小;
                    var __数据流 = __连接.GetStream();
                    var __接收线程 = new Thread(接收消息)
                    {
                        IsBackground = true
                    };
                    var __客户端 = new M客户端
                    {
                        节点 = (IPEndPoint)__连接.Client.RemoteEndPoint,
                        数据流 = __数据流,
                        开始时间 = DateTime.Now,
                        连接 = __连接
                    };
                    lock (_客户端操作锁)
                    {
                        _所有客户端[__客户端.节点] = __客户端;
                    }
                    H日志输出.记录(string.Format("{0}: {1} 连接", 名称, __客户端.节点), null, TraceEventType.Information);
                    On客户端连接(__客户端.节点);
                    __接收线程.Start(__客户端);
                }
                catch (Exception)
                {
                    break;
                }
            }
            H日志输出.记录(名称 + ": 终止侦听", null, TraceEventType.Information);
        }

        void 接收消息(object arg)
        {
            var __客户端 = arg as M客户端;
            if (__客户端 == null) throw new Exception();
            while (true)
            {
                var __缓存 = new byte[接收缓冲区大小];
                int __实际接收长度;
                try
                {
                    __实际接收长度 = __客户端.数据流.Read(__缓存, 0, 接收缓冲区大小);
                    if (__实际接收长度 == 0)
                    {
                        if (_所有客户端.ContainsKey(__客户端.节点))
                        {
                            断开客户端(__客户端.节点, "接收长度为0");
                        }
                    }
                }
                catch (Exception ex)
                {
                    if (!Disposed && !__客户端.关闭 && _所有客户端.ContainsKey(__客户端.节点))
                    {
                        H日志输出.记录(名称 + string.Format(": 从 [{0}] 接收异常", __客户端.节点), ex.Message, TraceEventType.Information);
                        断开客户端(__客户端.节点);
                    }
                    break;
                }
                var __实际接收字节 = new byte[__实际接收长度];
                Buffer.BlockCopy(__缓存, 0, __实际接收字节, 0, __实际接收长度);
                H日志输出.记录(名称 + string.Format(": 从 [{0}] 收", __客户端.节点), BitConverter.ToString(__实际接收字节));
                _IN消息分割.接收数据(__客户端.节点, __实际接收字节);
            }
            H日志输出.记录(string.Format("{0}: 停止接收来自 [{1}] 的消息", 名称, __客户端.节点), null, TraceEventType.Information);
        }

        public void 断开客户端(IPEndPoint __客户端节点, string __描述 = "")
        {
            lock (_客户端操作锁)
            {
                if (_所有客户端.ContainsKey(__客户端节点))
                {
                    H日志输出.记录(string.Format("{0}, 剩余: {1}个. {2}", __客户端节点, _所有客户端.Count - 1, __描述), null, TraceEventType.Information);
                    var __客户端 = _所有客户端[__客户端节点];
                    __客户端.关闭 = true;
                    __客户端.连接.Close();
                    __客户端.数据流.Close();
                    _所有客户端.Remove(__客户端节点);
                    On客户端已断开(__客户端.节点);
                    _IN消息分割.清空(__客户端节点);
                }
            }
        }

        public void 断开所有客户端()
        {
            H日志输出.记录(名称 + ": 断开所有客户端", null, TraceEventType.Information);
            lock (_客户端操作锁)
            {
                foreach (var kv in _所有客户端)
                {
                    var __客户端 = kv.Value;
                    if (__客户端.数据流 != null) __客户端.数据流.Close();
                    if (__客户端.连接 != null) __客户端.连接.Close();
                }
                _所有客户端.Clear();
                _IN消息分割.清空所有();
            }
        }

        public List<Tuple<IPEndPoint, DateTime>> 获取所有客户端()
        {
            return _所有客户端.ToList().Select(q => new Tuple<IPEndPoint, DateTime>(q.Key, q.Value.开始时间)).ToList();
        }

        public bool 验证在线(IPEndPoint __客户端节点)
        {
            return _所有客户端.ContainsKey(__客户端节点);
        }

        public event Action<IPEndPoint> 客户端已断开;

        protected virtual void On客户端已断开(IPEndPoint __客户端节点)
        {
            var handler = 客户端已断开;
            if (handler != null) handler(__客户端节点);
        }

        public event Action<IPEndPoint> 客户端已连接;

        protected virtual void On客户端连接(IPEndPoint __客户端节点)
        {
            var handler = 客户端已连接;
            if (handler != null) handler(__客户端节点);
        }

        public event Action<bool> 监听状态变化;

        protected virtual void On监听状态变化(bool __正在监听)
        {
            var handler = 监听状态变化;
            if (handler != null) handler(__正在监听);
        }

        public void 同步发送(IPEndPoint __客户端节点, byte[] __消息)
        {
            try
            {
                if (_所有客户端.ContainsKey(__客户端节点))
                {
                    var __客户端 = _所有客户端[__客户端节点];
                    __客户端.数据流.Write(__消息, 0, __消息.Length);

                    On发送成功(__客户端节点, __消息);
                    //H日志输出.记录(名称 + string.Format(": 向 [{0}] 发", __客户端节点), BitConverter.ToString(__消息));
                }
            }
            catch(Exception ex)
            {
                H日志输出.记录(名称 + string.Format(": 向 [{0}] 发送失败, {1}", __客户端节点, ex.Message), BitConverter.ToString(__消息), TraceEventType.Information);
                throw new ApplicationException("发送失败");
            }
        }

        public void 异步发送(IPEndPoint __客户端节点, byte[] __消息)
        {
            if (_所有客户端.ContainsKey(__客户端节点))
            {
                var __客户端 = _所有客户端[__客户端节点];
                __客户端.数据流.BeginWrite(__消息, 0, __消息.Length, null, null);

                On发送成功(__客户端节点, __消息);
                //H日志输出.记录(名称 + string.Format(": 向 [{0}] 发", __客户端节点), BitConverter.ToString(__消息));
            }
        }

        public event Action<IPEndPoint, byte[]> 收到消息;

        protected virtual void On收到消息(IPEndPoint __客户端节点, byte[] __消息)
        {
            var handler = 收到消息;
            if (handler != null) handler(__客户端节点, __消息);
        }

        public event Action<IPEndPoint, byte[]> 发送成功;

        protected virtual void On发送成功(IPEndPoint __客户端节点, byte[] __消息)
        {
            var handler = 发送成功;
            if (handler != null) handler(__客户端节点, __消息);
        }

        public void 关闭()
        {
            H日志输出.记录(名称 + ": 关闭", null, TraceEventType.Information);
            this.允许监听 = false;
            lock (_客户端操作锁)
            {
                foreach (var kv in _所有客户端)
                {
                    var __客户端 = kv.Value;
                    __客户端.关闭 = true;
                    if (__客户端.数据流 != null) __客户端.数据流.Close();
                    if (__客户端.连接 != null) __客户端.连接.Close();
                }
                _所有客户端.Clear();
            }
            if (_侦听器 != null)
            {
                _侦听器.Stop();
            }
            if (_侦听线程 != null)
            {
                _侦听线程.Abort();
            }
        }

        public void Dispose()
        {
            Disposed = true;
            关闭();
            客户端已连接 = null;
            客户端已断开 = null;
            收到消息 = null;
        }

        public bool Disposed { get; private set; }

        class M客户端
        {
            public IPEndPoint 节点;

            public TcpClient 连接;

            public NetworkStream 数据流;

            public DateTime 开始时间;

            //public Queue 发送队列;

            public bool 关闭;

       }

    }
}
