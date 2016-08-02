using INET.编解码;
using Utility.存储;
using Utility.扩展;
using 通用访问工具.DTO;

namespace 通用访问工具.报文
{
    internal class P执行方法S : P报文
    {
        public M执行方法响应 响应 { get; set; }

        public override void 解码消息内容(H字段解码 __解码)
        {
            var __内容 = __解码.解码UTF8(__解码.剩余字节数);
            this.响应 = HJSON.反序列化<M执行方法响应>(__内容);
        }

        public override void 编码消息内容(H字段编码 __编码)
        {
            var __字符串 = HJSON.序列化(响应);
            __编码.编码字段(System.Text.Encoding.UTF8.GetBytes(__字符串));
        }

    }
}
