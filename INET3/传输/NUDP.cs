using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace INET.传输
{
    class NUDP : INUDP
    {
        private UdpClient _连接;

        public string 名称 { get; set; }

        public IPEndPoint 本机地址 { get; set; }

        public E传输层协议 协议 { get { return E传输层协议.UDP; } }

        public DateTime 开启时间 { get; private set; }

        public int 发送缓冲区大小 { get; set; }

        public int 接收缓冲区大小 { get; set; }

        public int 最大消息长度 { get; set; }

        public NUDP(IPEndPoint __本机地址)
        {
            this.本机地址 = __本机地址;
            this.最大消息长度 = 100000;
            _连接 = this.本机地址 == null ? new UdpClient(new IPEndPoint(IPAddress.Any, 0)) : new UdpClient(本机地址);

            发送缓冲区大小 = _连接.Client.SendBufferSize;
            接收缓冲区大小 = _连接.Client.ReceiveBufferSize;
        }

        void _IN消息分割_分割了报文(IPEndPoint __客户端节点, byte[] __消息)
        {
            On收到消息(__客户端节点, __消息);
        }

        public void 开启()
        {
            if (名称 == null)
            {
                名称 = string.Format("[{0}]", 本机地址);
            }
            开启时间 = DateTime.Now;
            _连接.Client.SendBufferSize = 发送缓冲区大小;
            _连接.Client.ReceiveBufferSize = 接收缓冲区大小;
            //new Thread(处理接收) { IsBackground = true }.Start();
            _连接.BeginReceive(异步接收数据, null);            
        }

        public void 异步接收数据(IAsyncResult ar)
        {
            IPEndPoint __地址 = null;
            try
            {
                var __接收字节 = _连接.EndReceive(ar, ref __地址);
                if (__接收字节 == null || __接收字节.Length == 0)
                {
                    return;
                }
                On收到消息(__地址, __接收字节);
                _连接.BeginReceive(异步接收数据, null);
            }
            catch (Exception ex)
            {
                H日志输出.记录(名称 + string.Format(": 从 [{0}] 接收异常", __地址), ex.Message, TraceEventType.Warning);
            }
        }

        private void 处理接收()
        {
            while (true)
            {
                var __发送地址 = new IPEndPoint(IPAddress.Any, 0);
                try
                {
                    Byte[] __接收内容 = _连接.Receive(ref __发送地址);
                    H日志输出.记录(string.Format("{0}: 从 [{1}] 收", 名称, __发送地址), BitConverter.ToString(__接收内容));
                    On收到消息(__发送地址, __接收内容);
                }
                catch (Exception ex)
                {
                    if (!Disposed)
                    {
                        H日志输出.记录(string.Format("{0}: 从 [{1}] 接收异常", 名称, __发送地址), ex.Message, TraceEventType.Information);
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }

        public void 同步发送(IPEndPoint __远端地址, byte[] __消息)
        {
            if (!Disposed)
            {
                _连接.Send(__消息, __消息.Length, __远端地址);
                On发送成功(__远端地址, __消息);
            }
        }

        public void 异步发送(IPEndPoint __远端地址, byte[] __消息)
        {
            _连接.BeginSend(__消息, __消息.Length, new AsyncCallback(q => {
                try
                {
                    _连接.EndSend(q);
                    On发送成功(__远端地址, __消息);
                }
                catch (Exception ex)
                {
                    H日志输出.记录(名称 + string.Format(": 向 [{0}] 发送失败, {1}", __远端地址, ex.Message));
                }
            }), null);
        }

        public event Action<IPEndPoint, byte[]> 收到消息;

        protected virtual void On收到消息(IPEndPoint __远端地址, byte[] __消息)
        {
            var handler = 收到消息;
            if (handler != null) handler(__远端地址, __消息);
        }

        public event Action<IPEndPoint, byte[]> 发送成功;

        protected virtual void On发送成功(IPEndPoint __远端地址, byte[] __消息)
        {
            var handler = 发送成功;
            if (handler != null) handler(__远端地址, __消息);
        }

        public void 关闭()
        {
            H日志输出.记录(名称 + ": 关闭", null, TraceEventType.Information);
            Dispose();
        }

        public void Dispose()
        {
            Disposed = true;
            _连接.Client.Dispose();
            _连接.Close();
        }

        public void DisposeAsyn()
        {
            new Thread(Dispose) { IsBackground = true }.Start();
        }

        public bool Disposed { get; set; }
    }
}
