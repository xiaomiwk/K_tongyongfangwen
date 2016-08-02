using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace INET.传输
{
    class N消息头分割 : IN消息分割
    {
        readonly Dictionary<IPEndPoint, List<byte>> _所有数据流 = new Dictionary<IPEndPoint, List<byte>>();

        /// <summary>
        /// byte[]:报文头,int:内容长度
        /// </summary>
        readonly Func<byte[], int> _解码消息内容长度;

        readonly int _消息头长度;

        public int 最大消息长度 { get; set; }

        public N消息头分割(int __最大消息长度, int __消息头长度, Func<byte[], int> __解析消息体长度)
        {
            this.最大消息长度 = __最大消息长度;
            this._消息头长度 = __消息头长度;
            this._解码消息内容长度 = __解析消息体长度;
        }

        public void 接收数据(IPEndPoint __节点, byte[] __数据)
        {
            //H日志输出.记录(string.Format("接收数据:{0}", BitConverter.ToString(__数据.ToArray())));
            if (!_所有数据流.ContainsKey(__节点))
            {
                this._所有数据流[__节点] = new List<byte>();
            }
            this._所有数据流[__节点].AddRange(__数据);
            分析数据流(__节点, this._所有数据流[__节点]);
        }

        public void 清空(IPEndPoint __节点)
        {
            H日志输出.记录("清空解码缓存: " + __节点);
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

            if (__数据流.Count < this._消息头长度)
            {
                H日志输出.记录(string.Format("缓存的字节流长度不足报文头:{0}", BitConverter.ToString(__数据流.ToArray())));
                return;
            }

            //验证报文长度
            var __报文头 = new byte[_消息头长度];
            for (int j = 0; j < _消息头长度; j++)
            {
                __报文头[j] = __数据流[j];
            }
            var __报文内容长度 = _解码消息内容长度(__报文头);
            if (__报文内容长度 > 最大消息长度 || __报文内容长度 < 0)
            {
                //截取掉
                H日志输出.记录(string.Format("报文内容长度 {0} 非法，清空缓存的字节流:{1}", __报文内容长度, BitConverter.ToString(__数据流.ToArray())));
                __数据流.Clear();
                return;
            }

            //验证流长度
            if (__报文内容长度 > __数据流.Count - this._消息头长度)
            {
                //等待
                H日志输出.记录("报文内容未接收完整，等待后续数据");
                return;
            }

            //解码实际报文
            var __报文总长度 = this._消息头长度 + __报文内容长度;
            var __报文字节 = __数据流.GetRange(0, __报文总长度).ToArray();
            __数据流.RemoveRange(0, __报文总长度);
            On已分割消息(__节点, __报文字节);

            //循环解码，处理粘包
            分析数据流(__节点, __数据流);
        }

        public void 清空所有()
        {
            _所有数据流.Clear();
        }

    }
}
