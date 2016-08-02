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
    class NTCP客户端 : INTCP客户端
    {
        private TcpClient _连接;

        private NetworkStream _数据流;

        private readonly IN消息分割 _IN消息分割;

        public string 名称 { get; set; }

        public IPEndPoint 服务器地址 { get; set; }

        public IPEndPoint 本机地址 { get; set; }

        public E传输层协议 协议 { get { return E传输层协议.TCP; } }

        public E流分割方式 分割方式 { get; private set; }

        public DateTime 开启时间 { get; private set; }

        public int 发送缓冲区大小 { get; set; }

        public int 接收缓冲区大小 { get; set; }

        public int 最大消息长度 { get; set; }

        public bool 连接正常 { get; private set; }

        private bool _已开启重连;

        public NTCP客户端(IPEndPoint __服务器地址, IPEndPoint __本机地址, List<byte[]> __结束符)
            : this(__服务器地址, __本机地址)
        {
            分割方式 = E流分割方式.结束符;
            _IN消息分割 = new N结束符分割(this.最大消息长度, __结束符);
            _IN消息分割.已分割消息 += _IN消息分割_分割了报文;
        }

        public NTCP客户端(IPEndPoint __服务器地址, IPEndPoint __本机地址, int __消息头长度, Func<byte[], int> __解析消息体长度)
            : this(__服务器地址, __本机地址)
        {
            分割方式 = E流分割方式.消息头;
            _IN消息分割 = new N消息头分割(this.最大消息长度, __消息头长度, __解析消息体长度);
            _IN消息分割.已分割消息 += _IN消息分割_分割了报文;
        }

        protected NTCP客户端(IPEndPoint __服务器地址, IPEndPoint __本机地址)
        {
            this.服务器地址 = __服务器地址;
            this.本机地址 = __本机地址;
            this.最大消息长度 = 100000;
            this.发送缓冲区大小 = 8192;
            this.接收缓冲区大小 = 8192;
            this.自动重连重试间隔秒数 = 10;
        }

        void _IN消息分割_分割了报文(IPEndPoint __客户端节点, byte[] __消息)
        {
            On收到消息(__客户端节点, __消息);
        }

        public void 开启()
        {
            if (名称 == null)
            {
                名称 = string.Format("客户端[{0}]", 本机地址);
            }
            _IN消息分割.最大消息长度 = this.最大消息长度;
            开启时间 = DateTime.Now;
            try
            {
                _连接 = this.本机地址 == null ? new TcpClient(new IPEndPoint(IPAddress.Any, 0)) : new TcpClient(本机地址);
                _连接.SendBufferSize = 发送缓冲区大小;
                _连接.ReceiveBufferSize = 接收缓冲区大小;
                _连接.Connect(服务器地址);
                连接正常 = true;
                H日志输出.记录(名称 + ": 绑定 " + _连接.Client.LocalEndPoint, null, TraceEventType.Information);
            }
            catch (Exception ex)
            {
                H日志输出.记录(ex.Message);
                断开();
            }
            if (连接正常)
            {
                _数据流 = _连接.GetStream();
                var __缓存 = new byte[接收缓冲区大小];
                _数据流.BeginRead(__缓存, 0, 接收缓冲区大小, 异步接收数据, new Tuple<NetworkStream, byte[]>(_数据流, __缓存));
                On已连接();
            }
            if (自动重连 && !_已开启重连)
            {
                _已开启重连 = true;
                new Thread(执行自动重连) { IsBackground = true }.Start();
            }
        }

        public void 异步接收数据(IAsyncResult ar)
        {
            var __绑定 = ar.AsyncState as Tuple<NetworkStream, byte[]>;
            var __数据流 = __绑定.Item1;
            var __缓存 = __绑定.Item2;
            try
            {
                int __实际接收长度 = __数据流.EndRead(ar);
                if (__实际接收长度 == 0)
                {
                    断开();
                    return;
                }
                var __实际接收字节 = new byte[__实际接收长度];
                Buffer.BlockCopy(__缓存, 0, __实际接收字节, 0, __实际接收长度);
                //H日志输出.记录(名称 + string.Format(": 从 [{0}] 收", 服务器地址), BitConverter.ToString(__实际接收字节));
                _IN消息分割.接收数据(服务器地址, __实际接收字节);
                __数据流.BeginRead(__缓存, 0, 接收缓冲区大小, 异步接收数据, new Tuple<NetworkStream, byte[]>(_数据流, __缓存));
            }
            catch (Exception ex)
            {
                if (连接正常)
                {
                    H日志输出.记录(名称 + string.Format(": 从 [{0}] 接收异常", 服务器地址), ex.Message, TraceEventType.Warning);
                    断开();
                }
            }
        }

        private void 执行自动重连()
        {
            var __检测间隔毫秒 = 5000;
            while (自动重连)
            {
                while (连接正常)
                {
                    Thread.Sleep(__检测间隔毫秒);
                }
                if (Disposed)
                {
                    break;
                }
                H日志输出.记录(名称 + ": 开始自动重连");
                On开始自动重连();
                开启();
                if (!连接正常)
                {
                    H日志输出.记录(名称 + ": 自动重连失败");
                    Thread.Sleep(this.自动重连重试间隔秒数 * 1000);
                }
                else
                {
                    H日志输出.记录(名称 + ": 自动重连成功");
                }
            }
            _已开启重连 = false;
        }

        public event Action 已连接;

        protected virtual void On已连接()
        {
            var handler = 已连接;
            if (handler != null) handler();
        }

        public void 断开()
        {
            if (!连接正常)
            {
                return;
            }
            H日志输出.记录(名称 + ": 断开", null, TraceEventType.Information);
            连接正常 = false;
            if (_连接 != null)
            {
                _连接.Close();
            }
            if (_数据流 != null)
            {
                _数据流.Close();
            }
            On已断开();
            _IN消息分割.清空所有();
        }

        public event Action 已断开;

        protected virtual void On已断开()
        {
            var handler = 已断开;
            if (handler != null) handler();
        }

        public bool 自动重连 { get; set; }

        public int 自动重连重试间隔秒数 { get; set; }

        public event Action 自动重连开始;

        protected virtual void On开始自动重连()
        {
            var handler = 自动重连开始;
            if (handler != null) handler();
        }

        public void 手动重连(int __重试次数, int __重试间隔秒数)
        {
            for (int i = 0; i < __重试次数; i++)
            {
                try
                {
                    if (Disposed)
                    {
                        return;
                    }
                    if (连接正常)
                    {
                        break;
                    }
                    开启();
                }
                catch (Exception)
                {
                    Thread.Sleep(__重试间隔秒数 * 1000);
                }
            }
        }

        public void 同步发送(byte[] __消息)
        {
            if (_连接 == null || _连接.Client == null || !_连接.Connected)
            {
                return;
            }
            //H日志输出.记录(名称 + string.Format(": 向 [{0}] 发", 服务器地址), BitConverter.ToString(__消息));
            try
            {
                _数据流.Write(__消息, 0, __消息.Length);
                On发送成功(服务器地址, __消息);
            }
            catch (Exception ex)
            {
                H日志输出.记录(名称 + string.Format(": 向 [{0}] 发送失败, {1}", 服务器地址, ex.Message));
                //throw new ApplicationException("发送失败");
            }
        }

        public void 异步发送(byte[] __消息)
        {
            if (_连接 == null || _连接.Client == null || !_连接.Connected)
            {
                return;
            }
            try
            {
                //H日志输出.记录(名称 + string.Format(": 向 [{0}] 发", 服务器地址), BitConverter.ToString(__消息));
                _数据流.BeginWrite(__消息, 0, __消息.Length, new AsyncCallback(q => {
                    try
                    {
                        _数据流.EndWrite(q);
                        On发送成功(服务器地址, __消息);
                    }
                    catch (Exception ex)
                    {
                        H日志输出.记录(名称 + string.Format(": 向 [{0}] 发送失败, {1}", 服务器地址, ex.Message));
                    }
                }), null);
            }
            catch (Exception ex)
            {
                H日志输出.记录(名称 + string.Format(": 向 [{0}] 发送失败, {1}", 服务器地址, ex.Message));
            }
        }

        public event Action<IPEndPoint, byte[]> 收到消息;

        protected virtual void On收到消息(IPEndPoint __远端节点, byte[] __消息)
        {
            var handler = 收到消息;
            if (handler != null) handler(__远端节点, __消息);
        }

        public event Action<IPEndPoint, byte[]> 发送成功;

        protected virtual void On发送成功(IPEndPoint __远端节点, byte[] __消息)
        {
            var handler = 发送成功;
            if (handler != null) handler(__远端节点, __消息);
        }

        public void 关闭()
        {
            H日志输出.记录(名称 + ": 关闭", null, TraceEventType.Information);
            断开();
        }

        public void Dispose()
        {
            Disposed = true;
            关闭();
        }

        public bool Disposed { get; private set; }

        public void 同步发送(IPEndPoint __接收地址, byte[] __消息)
        {
            this.同步发送(__消息);
        }

        public void 异步发送(IPEndPoint __接收地址, byte[] __消息)
        {
            this.异步发送(__消息);
        }

    }
}
