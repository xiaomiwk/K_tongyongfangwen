using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using INET.会话;
using INET.编解码;

namespace INET.模板
{
    /// <summary>
    /// 功能码标识: 功能标识.ToString("X4")
    /// 格式如下
    /// 字段	            类型	    长度(字节)	    说明
    /// 起始标识	        Byte[]	    2	            固定为0xAAAA，用于识别报文开始
    /// 报文内容长度	    Int	        4	            余下所有字段长度
    /// 功能标识	        Int16	    2	            用于识别报文类型
    /// 发送方事务标识	    Int32	    4	            由发送方指定。当会话类型为请求型时，用于区别发送方的不同请求；当会话类型为通知型时，固定为0。
    /// 接收方事务标识	    Int32	    4	            由接收方指定。当会话类型为请求型时，用于区别接收方的不同请求，特别的，会话开始的第一条报文，发送方的该字段填0；当会话类型为通知型时，固定为0。
    /// 负载	            Byte[]	    可变	        IN可编码/IN可解码    
    /// </summary>
    public class N通用编解码 : IN编解码器
    {
        public int 消息头长度 = 6;

        public byte[] 消息头标识 = { 0xAA, 0xAA };

        public Func<byte[], int> 解码消息长度;

        public Dictionary<Int16, Type> 报文字典 { get; set; }

        public Dictionary<Int16, string> 通道字典 { get; set; }

        public N通用编解码()
        {
            解码消息长度 = q =>
            {
                for (int i = 0; i < 消息头标识.Length; i++)
                {
                    if (q[i] != 消息头标识[i])
                    {
                        return -1;
                    }
                }
                return IPAddress.NetworkToHostOrder(BitConverter.ToInt32(q, 2));
            };
        }

        public N通用编解码(Dictionary<Int16, Type> __报文字典, Dictionary<Int16, string> __通道字典)
        {
            报文字典 = __报文字典;
            通道字典 = __通道字典;
        }

        public Tuple<N事务, object> 解码(byte[] 数据)
        {
            var __解码 = new H字段解码(数据);
            var __消息头 = __解码.解码字节数组(消息头长度);
            var __功能码 = __解码.解码Int16();
            if (!报文字典.ContainsKey(__功能码))
            {
                throw new ApplicationException(string.Format("功能码无效: {0}", __功能码));
            }
            var __发方事务 = __解码.解码Int32();
            var __收方事务 = __解码.解码Int32();
            var __负载数据 = __解码.解码字节数组(__解码.剩余字节数);
            var __负载 = 解码(__功能码, __负载数据);

            var __事务 = new N事务 { 发方事务 = __发方事务, 收方事务 = __收方事务, 功能码 = __功能码.ToString("X4") };
            if (通道字典 != null && 通道字典.ContainsKey(__功能码))
            {
                __事务.通道标识 = 通道字典[__功能码];
            }

            return new Tuple<N事务, object>(__事务, __负载);
        }

        public byte[] 编码(N事务 事务, object 负载)
        {
            var __功能码 = 获取功能码1(负载.GetType());
            var __消息内容 = 编码(负载);
            var __消息内容长度 = 10 + __消息内容.Length;
            var __编码 = new H字段编码();
            __编码.编码字段(消息头标识, __消息内容长度, __功能码, 事务.发方事务, 事务.收方事务, __消息内容);
            return __编码.获取结果();
        }

        protected virtual object 解码(Int16 __功能码, byte[] __负载数据)
        {
            var __负载 = (IN可解码)Activator.CreateInstance(报文字典[__功能码]);
            __负载.解码(__负载数据);
            return __负载;
        }

        protected virtual byte[] 编码(object __负载)
        {
            return ((IN可编码)__负载).编码();
        }

        public virtual string 获取功能码(Type __负载类型)
        {
            return 获取功能码1(__负载类型).ToString("X4");
        }

        private Int16 获取功能码1(Type __负载类型)
        {
            foreach (var __kv in 报文字典)
            {
                if (__kv.Value == __负载类型)
                {
                    return __kv.Key;
                }
            }
            throw new ApplicationException("无此类型: " + __负载类型.FullName);
        }

    }
}
