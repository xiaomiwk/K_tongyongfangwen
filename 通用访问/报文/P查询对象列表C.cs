using INET.编解码;

namespace 通用访问.报文
{
    internal class P查询对象列表C : IN可编码, IN可解码
    {
        public int 解码(byte[] 数据)
        {
            return 0;
        }

        public byte[] 编码()
        {
            return new byte[0];
        }
    }
}
