using System;
using System.Net;
using System.Text;

namespace INET.编解码
{
    public class H字段解码
    {
        private readonly Byte[] _缓存数据 = new Byte[0];

        public H字段解码(Byte[] __原始字节)
        {
            _缓存数据 = __原始字节;
        }

        public Byte 解码字节()
        {
            var __结果 = _缓存数据[解码字节数];
            解码字节数 += 1;
            return __结果;
        }

        public Int16 解码Int16()
        {
            var __结果 = IPAddress.NetworkToHostOrder(BitConverter.ToInt16(_缓存数据, 解码字节数));
            解码字节数 += 2;
            return __结果;
        }

        public Int32 解码Int32()
        {
            var __结果 = IPAddress.NetworkToHostOrder(BitConverter.ToInt32(_缓存数据, 解码字节数));
            解码字节数 += 4;
            return __结果;
        }

        public Int64 解码Int64()
        {
            var __结果 = IPAddress.NetworkToHostOrder(BitConverter.ToInt64(_缓存数据, 解码字节数));
            解码字节数 += 8;
            return __结果;
        }

        public UInt16 解码UInt16()
        {
            var __结果 = BitConverter.ToUInt16(_缓存数据, 解码字节数);
            解码字节数 += 2;
            return __结果;
        }

        public UInt32 解码UInt32()
        {
            var __结果 = BitConverter.ToUInt32(_缓存数据, 解码字节数);
            解码字节数 += 4;
            return __结果;
        }

        public UInt64 解码UInt64()
        {
            var __结果 = BitConverter.ToUInt64(_缓存数据, 解码字节数);
            解码字节数 += 8;
            return __结果;
        }

        public Single 解码Single()
        {
            var __结果 = BitConverter.ToSingle(_缓存数据, 解码字节数);
            解码字节数 += 4;
            return __结果;
        }

        public Double 解码Double()
        {
            var __结果 = BitConverter.ToDouble(_缓存数据, 解码字节数);
            解码字节数 += 8;
            return __结果;
        }

        public string 解码ASCII(int 字节长度)
        {
            var __结果 = System.Text.Encoding.ASCII.GetString(_缓存数据, 解码字节数, 字节长度);
            解码字节数 += 字节长度;
            return __结果;
        }

        public string 解码UTF8(int 字节长度)
        {
            var __结果 = System.Text.Encoding.UTF8.GetString(_缓存数据, 解码字节数, 字节长度);
            解码字节数 += 字节长度;
            return __结果;
        }

        public string 解码UTF16(int 字节长度)
        {
            var __结果 = System.Text.Encoding.Unicode.GetString(_缓存数据, 解码字节数, 字节长度);
            解码字节数 += 字节长度;
            return __结果;
        }

        public string 解码GB2132(int 字节长度)
        {
            //936
            var __结果 = System.Text.Encoding.GetEncoding("GB2312").GetString(_缓存数据, 解码字节数, 字节长度);
            解码字节数 += 字节长度;
            return __结果;
        }

        public string 解码文本(Encoding 编码, int 字节长度)
        {
            var __结果 = 编码.GetString(_缓存数据, 解码字节数, 字节长度);
            解码字节数 += 字节长度;
            return __结果;
        }

        public Byte[] 解码字节数组(int 数组长度)
        {
            var __结果 = new byte[数组长度];
            Buffer.BlockCopy(_缓存数据, 解码字节数, __结果, 0, 数组长度);
            解码字节数 += 数组长度;
            return __结果;
        }

        public void 解码报文模块(IN可解码 报文模块)
        {
            var __数据 = new byte[_缓存数据.Length - 解码字节数];
            Buffer.BlockCopy(_缓存数据, 解码字节数, __数据, 0, __数据.Length);
            解码字节数 += 报文模块.解码(__数据);
        }

        public Int32 解码字节数 { get; private set; }

        public Int32 剩余字节数
        {
            get { return _缓存数据.Length - 解码字节数; }
        }
    }
}
