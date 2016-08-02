using System;
using INET.编解码;

namespace INET.会话
{

    public interface IN编解码器
    {
        Tuple<N事务, object> 解码(byte[] 数据);

        byte[] 编码(N事务 事务, object 负载);

    }
}



