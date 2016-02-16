using INET.编解码;
using 通用访问.DTO;
using 通用访问.自定义序列化;

namespace 通用访问.报文
{
    internal class P查询对象明细C : IN可编码, IN可解码
    {
        public M查询对象 请求 { get; set; }

        public override void 解码消息内容(H字段解码 __解码)
        {
            var __内容 = __解码.解码文本(FT通用访问工厂.文本编码, __解码.剩余字节数);
            this.请求 = HJSON.反序列化<M查询对象>(__内容);
        }

        public override void 编码消息内容(H字段编码 __编码)
        {
            var __字符串 = HJSON.序列化(请求);
            __编码.编码字段(FT通用访问工厂.文本编码.GetBytes(__字符串));
        }

    }
}
