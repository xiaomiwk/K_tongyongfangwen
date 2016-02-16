using INET.编解码;
using 通用访问.DTO;

namespace 通用访问.报文
{
    internal class P通知 : P报文
    {
        public M通知 通知 { get; set; }

        public override void 解码消息内容(H字段解码 __解码)
        {
            var __内容 = __解码.解码文本(FT通用访问工厂.文本编码, __解码.剩余字节数);
            this.通知 = HJSON.反序列化<M通知>(__内容);
        }

        public override void 编码消息内容(H字段编码 __编码)
        {
            var __字符串 = HJSON.序列化(通知);
            __编码.编码字段(FT通用访问工厂.文本编码.GetBytes(__字符串));
        }

    }
}
