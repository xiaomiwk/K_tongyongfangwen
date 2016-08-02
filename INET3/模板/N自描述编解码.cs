using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using INET.会话;
using INET.编解码;

namespace INET.模板
{
    /// <summary>
    /// 使用.NET内置二进制序列化, 可通过继承使用其他编解码方式, 格式如下
    /// 字段	            类型	    长度(字节)	    说明
    /// 起始标识	        Byte[]	    2	            固定为0xAAAA，用于识别报文开始
    /// 报文内容长度	    Int	        4	            余下所有字段长度
    /// 发送方事务标识	    Int32	    4	            由发送方指定。当会话类型为请求型时，用于区别发送方的不同请求；当会话类型为通知型时，固定为0。
    /// 接收方事务标识	    Int32	    4	            由接收方指定。当会话类型为请求型时，用于区别接收方的不同请求，特别的，会话开始的第一条报文，发送方的该字段填0；当会话类型为通知型时，固定为0。
    /// 功能码长度	        Int16	    2	            字节长度
    /// 功能码(*)	        UTF8	    可变	        使用Type.FullName
    /// 负载(*)	            Byte[]	    可变	        BinaryFormatter
    /// </summary>
    public class N自描述编解码 : IN编解码器
    {
        public Int16 消息头长度 = 6;

        public byte[] 消息头标识 = { 0xAA, 0xAA };

        public Func<byte[], int> 解码消息长度;

        public Dictionary<string, string> 通道字典 { get; set; }

        public N自描述编解码()
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

        public N自描述编解码(Dictionary<string, string> __通道字典)
        {
            通道字典 = __通道字典;
        }

        public byte[] 编码(N事务 __事务, object __负载)
        {
            var __功能码 = 获取功能码(__负载.GetType());
            var __功能码编码 = Encoding.UTF8.GetBytes(__功能码);
            var __编码 = new H字段编码();
            var __二进制 = 编码(__负载);
            var __消息内容长度 = 10 + __功能码编码.Length + __二进制.Length;
            __编码.编码字段(消息头标识, __消息内容长度, __事务.发方事务, __事务.收方事务, (Int16)__功能码编码.Length, __功能码编码, __二进制);
            return __编码.获取结果();
        }

        public Tuple<N事务, object> 解码(byte[] __数据)
        {
            var __解码 = new H字段解码(__数据);
            var __消息头 = __解码.解码字节数组(消息头长度);
            var __发方事务 = __解码.解码Int32();
            var __收方事务 = __解码.解码Int32();
            var __功能码长度 = __解码.解码Int16();
            var __功能码 = __解码.解码UTF8(__功能码长度);
            var __负载 = 解码(__功能码, __解码.解码字节数组(__解码.剩余字节数));
            var __事务 = new N事务 { 发方事务 = __发方事务, 收方事务 = __收方事务, 功能码 = __功能码 };
            if (通道字典 != null && 通道字典.ContainsKey(__事务.功能码))
            {
                __事务.通道标识 = 通道字典[__事务.功能码];
            }
            return new Tuple<N事务, object>(__事务, __负载);
        }

        protected virtual object 解码(string __功能码, byte[] __负载数据)
        {
            return HBin编解码.解码(__负载数据);
        }

        public virtual string 获取功能码(Type __负载类型)
        {
            return __负载类型.FullName;
        }

        protected virtual byte[] 编码(object __负载)
        {
            return HBin编解码.编码(__负载);
        }

    }

}
