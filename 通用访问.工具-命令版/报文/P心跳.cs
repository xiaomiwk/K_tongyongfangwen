using INET.编解码;
using Utility.扩展;

namespace 通用访问工具.报文
{
    internal class P心跳 : P报文
    {
        public P心跳()
        {
            this.同步处理 = true;
        }

        public override void 解码消息内容(H字段解码 __解码)
        {
            //无字段,无需代码
        }

        public override void 编码消息内容(H字段编码 __解码)
        {
        }
    }
}
