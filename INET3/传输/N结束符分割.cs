using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace INET.传输
{
    class N结束符分割 : IN消息分割
    {
        readonly Dictionary<IPEndPoint, List<byte>> _所有数据流 = new Dictionary<IPEndPoint, List<byte>>();

        readonly List<byte[]> _结束符;

        public int 最大消息长度 { get; set; }

        public N结束符分割(int __最大消息长度, List<byte[]> __结束符)
        {
            this.最大消息长度 = __最大消息长度;
            this._结束符 = __结束符;
        }

        public void 接收数据(IPEndPoint __节点, byte[] __数据)
        {
            if (!_所有数据流.ContainsKey(__节点))
            {
                this._所有数据流[__节点] = new List<byte>();
            }
            this._所有数据流[__节点].AddRange(__数据);
            分析数据流(__节点, this._所有数据流[__节点]);
        }

        public void 清空(IPEndPoint __节点)
        {
            if (_所有数据流.ContainsKey(__节点))
            {
                this._所有数据流.Remove(__节点);
            }
        }

        public event Action<IPEndPoint, byte[]> 已分割消息;

        protected void On已分割消息(IPEndPoint __节点, byte[] __消息)
        {
            try
            {
                var handler = 已分割消息;
                if (handler != null) handler(__节点, __消息);
            }
            catch (Exception e)
            {
                H日志输出.记录(string.Format("处理字节流出错: {0}", BitConverter.ToString(__消息)));
                H日志输出.记录(e);
            }
        }

        private void 分析数据流(IPEndPoint __节点, List<byte> __数据流)
        {
            if (__数据流.Count == 0)
            {
                return;
            }

            if (_结束符 == null || _结束符.Count == 0)
            {
                On已分割消息(__节点, __数据流.ToArray());
                return;
            }

            foreach (var __temp in _结束符)
            {
                if (__temp.Length > __数据流.Count)
                {
                    continue;
                }
                for (int i = 0; i < __数据流.Count - __temp.Length + 1; i++)
                {
                    bool __匹配 = true;
                    for (int j = 0; j < __temp.Length; j++)
                    {
                        if (__数据流[i + j] != __temp[j])
                        {
                            __匹配 = false;
                            break;
                        }
                    }
                    if (__匹配)
                    {
                        var __报文字节 = __数据流.GetRange(0, i + __temp.Length).ToArray();
                        __数据流.RemoveRange(0, __报文字节.Length);
                        if (__报文字节.Length <= 最大消息长度)
                        {
                            On已分割消息(__节点, __报文字节);
                        }
                        分析数据流(__节点, __数据流);//循环解码，处理粘包
                        break;
                    }
                }
            }
        }

        public void 清空所有()
        {
            _所有数据流.Clear();
        }

    }
}
