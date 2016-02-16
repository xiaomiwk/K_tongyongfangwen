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

        private readonly IN消息分割 _IN消息分割;

        public string 名称 { get; set; }

        public IPEndPoint 本机地址 { get; set; }

        public E传输层协议 协议 { get { return E传输层协议.UDP; } }

        public E流分割方式 分割方式 { get; private set; }

        public DateTime 开启时间 { get; private set; }

        public int 发送缓冲区大小 { get; set; }

        public int 接收缓冲区大小 { get; set; }

        public int 最大消息长度 { get; set; }

        public NUDP(IPEndPoint __本机地址, List<byte[]> __结束符)
            : this(__本机地址)
        {
            分割方式 = E流分割方式.结束符;
            _IN消息分割 = new N结束符分割(this.最大消息长度, __结束符);
            _IN消息分割.已分割消息 += _IN消息分割_分割了报文;
        }

        public NUDP(IPEndPoint __本机地址, int __消息头长度, Func<byte[], int> __解析消息体长度)
            : this(__本机地址)
        {
            分割方式 = E流分割方式.消息头;
            _IN消息分割 = new N消息头分割(this.最大消息长度, __消息头长度, __解析消息体长度);
            _IN消息分割.已分割消息 += _IN消息分割_分割了报文;
        }

        protected NUDP(IPEndPoint __本机地址)
        {
            this.本机地址 = __本机地址;
            this.最大消息长度 = 5000;
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
            new Thread(处理接收) { IsBackground = true }.Start();
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
                    _IN消息分割.接收数据(__发送地址, __接收内容);
                    //On收到消息(__发送地址, __接收内容);
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
            _连接.BeginSend(__消息, __消息.Length, __远端地址, null, null);
            On发送成功(__远端地址, __消息);
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
