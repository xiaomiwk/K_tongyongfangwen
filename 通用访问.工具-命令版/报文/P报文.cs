using System;
using System.Net;
using INET.会话;
using INET.编解码;

namespace 通用访问工具.报文
{
    internal abstract class P报文 : IN事务报文
    {
        public const Int16 消息头长度 = 18;

        private static byte[] _消息头标识 = { 0xAA, 0xAA };

        protected short 功能码;

        public int 发方事务标识 { get; set; }

        public int 收方事务标识 { get; set; }

        public int 注册标识 { get; set; }

        public bool 同步处理 { get; set; }

        public bool 需要验证 { get; set; }

        public static readonly Func<byte[], int> 解码消息长度 = q => IPAddress.NetworkToHostOrder(BitConverter.ToInt16(q, 2));

        public static readonly Func<byte[], int> 解码功能标识 = q => IPAddress.NetworkToHostOrder(BitConverter.ToInt16(q, 4));

        protected P报文()
        {
            var __报文类型 = this.GetType();
            功能码 = (short)H报文注册.查询功能码(__报文类型);
            需要验证 = false;
            同步处理 = false;
        }

        public virtual int 解码(byte[] 数据)
        {
            var __解码 = new H字段解码(数据);
            __解码.解码字节数组(6); //6为消息头标识\消息头长度\功能标识的长度 这些已无需解码
            this.发方事务标识 = __解码.解码Int32();
            this.收方事务标识 = __解码.解码Int32();
            //this.注册标识 = __解码.解码Int32();
            解码消息内容(__解码);
            return __解码.解码字节数;
        }

        public abstract void 解码消息内容(H字段解码 __解码);

        public virtual byte[] 编码()
        {
            var __编码 = new H字段编码();
            编码消息内容(__编码);
            return 合成报文(__编码.获取结果());
        }

        private byte[] 合成报文(byte[] __消息内容)
        {
            var __消息内容长度 = __消息内容.Length;
            var __编码 = new H字段编码();
            __编码.编码字段(_消息头标识, (short)__消息内容长度, 功能码, 发方事务标识, 收方事务标识, 注册标识, __消息内容);
            return __编码.获取结果();
        }

        public abstract void 编码消息内容(H字段编码 __编码);

    }

}
