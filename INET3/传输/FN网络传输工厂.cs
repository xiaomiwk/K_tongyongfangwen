using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;

namespace INET.传输
{
    public static class FN网络传输工厂
    {
        /// <summary>
        /// 创建使用二进制协议的TCP服务端引擎。对于返回的引擎实例，可以设置其更多属性，然后调用其Initialize方法启动引擎。
        /// </summary>
        public static INTCP服务器 创建TCP服务器(IPEndPoint __本机地址, int __消息头长度, Func<byte[], int> __解析消息体长度)
        {
            return new NTCP服务器(__本机地址, __消息头长度, __解析消息体长度);
        }

        /// <summary>
        /// 创建使用文本协议的TCP服务端引擎。对于返回的引擎实例，可以设置其更多属性，然后调用其Initialize方法启动引擎。
        /// 注意：返回的引擎实例，可以强转为ITextEngine接口。
        /// </summary>
        public static INTCP服务器 创建TCP服务器(IPEndPoint __本机地址, List<byte[]> __结束符)
        {
            return new NTCP服务器(__本机地址, __结束符);
        }

        /// <summary>
        /// 创建使用二进制协议的TCP客户端引擎。对于返回的引擎实例，可以设置其更多属性，然后调用其Initialize方法启动引擎。
        /// </summary>        
        public static INTCP客户端 创建TCP客户端(IPEndPoint __服务器地址, IPEndPoint __本机地址, int __消息头长度, Func<byte[], int> __解析消息体长度)
        {
            return new NTCP客户端(__服务器地址, __本机地址, __消息头长度, __解析消息体长度);
        }

        /// <summary>
        /// 创建使用文本协议的TCP客户端引擎。对于返回的引擎实例，可以设置其更多属性，然后调用其Initialize方法启动引擎。
        /// 注意：返回的引擎实例，可以强转为ITextEngine接口。
        /// </summary>
        public static INTCP客户端 创建TCP客户端(IPEndPoint __服务器地址, IPEndPoint __本机地址, List<byte[]> __结束符)
        {
            return new NTCP客户端(__服务器地址, __本机地址, __结束符);
        }

        /// <summary>
        /// 创建UDP引擎。对于返回的引擎实例，可以设置其更多属性，然后调用其Initialize方法启动引擎。
        /// </summary>       
        public static INUDP 创建UDP节点(IPEndPoint __本机地址)
        {
            return new NUDP(__本机地址);
        }

    }
}
