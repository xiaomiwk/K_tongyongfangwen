using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using 通用访问.客户端;
using 通用访问.服务端;

namespace 通用访问
{
    public static class FT通用访问工厂
    {
        /// <summary>
        /// UTF8
        /// </summary>
        public static Encoding 文本编码 { get; set; }

        static FT通用访问工厂()
        {
            //文本编码 = Encoding.GetEncoding(936);
            文本编码 = Encoding.UTF8;
        }

        public static IT服务端 创建服务端()
        {
            return new BT服务端();
        }

        public static IT客户端 创建客户端()
        {
            //return new BT客户端_模拟();
            return new BT客户端();
        }

    }
}
