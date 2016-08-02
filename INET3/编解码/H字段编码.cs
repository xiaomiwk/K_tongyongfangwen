using System;
using System.Net;

namespace INET.编解码
{
    public class H字段编码
    {
        private Byte[] _缓存数据 = new Byte[0];

        public void 编码字段(params object[] 字段列表)
        {
            //计算长度
            var __长度 = 0;
            foreach (var __字段 in 字段列表)
            {
                if (__字段 is Byte)
                {
                    __长度 += 1;
                    continue;
                }
                if (__字段 is Int16 || __字段 is UInt16)
                {
                    __长度 += 2;
                    continue;
                }
                if (__字段 is Int32 || __字段 is UInt32 || __字段 is Single)
                {
                    __长度 += 4;
                    continue;
                }
                if (__字段 is Int64 || __字段 is UInt64 || __字段 is Double)
                {
                    __长度 += 8;
                    continue;
                }
                var __字节数组 = __字段 as Byte[];
                if (__字节数组 != null)
                {
                    __长度 += __字节数组.Length;
                    continue;
                }
                var __M报文模块 = __字段 as IN可编码;
                if (__M报文模块 != null)
                {
                    __长度 += __M报文模块.编码().Length;
                    continue;
                }
            }

            //合并
            var __结果 = new Byte[_缓存数据.Length + __长度];
            if (_缓存数据.Length > 0)
            {
                Buffer.BlockCopy(_缓存数据, 0, __结果, 0, _缓存数据.Length);
            }
            var __索引 = _缓存数据.Length;
            foreach (var __字段 in 字段列表)
            {
                byte[] __字段数据 = null;
                if (__字段 is Byte)
                {
                    __字段数据 = new Byte[] { (Byte)__字段 };
                }
                else if (__字段 is Int16 || __字段 is UInt16)
                {
                    __字段数据 = BitConverter.GetBytes(IPAddress.HostToNetworkOrder((Int16)__字段));
                }
                else if (__字段 is Int32 || __字段 is UInt32)
                {
                    __字段数据 = BitConverter.GetBytes(IPAddress.HostToNetworkOrder((Int32)__字段));
                }
                else if (__字段 is Single)
                {
                    __字段数据 = BitConverter.GetBytes((Single)__字段);
                }
                else if (__字段 is Int64 || __字段 is UInt64)
                {
                    __字段数据 = BitConverter.GetBytes(IPAddress.HostToNetworkOrder((Int64)__字段));
                }
                else if (__字段 is Double)
                {
                    __字段数据 = BitConverter.GetBytes((Double)__字段);
                }
                else if (__字段 is Byte[])
                {
                    __字段数据 = (Byte[])__字段;
                }
                else if (__字段 is IN可编码)
                {
                    __字段数据 = ((IN可编码)__字段).编码();
                }
                if (__字段数据 != null)
                {
                    Buffer.BlockCopy(__字段数据, 0, __结果, __索引, __字段数据.Length);
                    __索引 += __字段数据.Length;
                }
            }
            _缓存数据 = __结果;
        }

        public Byte[] 获取结果()
        {
            return _缓存数据;
        }
    }
}
