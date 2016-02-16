using System.Collections.Generic;
using INET.编解码;
using 通用访问.DTO;

namespace 通用访问.报文
{
    internal class P查询对象列表S : P报文
    {
        public M对象列表 响应 { get; set; }

        public override void 解码消息内容(H字段解码 __解码)
        {
            var __内容 = __解码.解码文本(FT通用访问工厂.文本编码, __解码.剩余字节数);
            //var __内容 = __解码.解码GB2132(__解码.剩余字节数);
            this.响应 = new M对象列表 { 对象列表 = HJSON.反序列化<List<M对象概要>>(__内容) };
        }

        public override void 编码消息内容(H字段编码 __编码)
        {
            var __字符串 = HJSON.序列化(响应.对象列表);
            __编码.编码字段(FT通用访问工厂.文本编码.GetBytes(__字符串));
        }

    }
}
