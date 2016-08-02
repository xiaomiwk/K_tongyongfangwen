using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace INET.编解码
{
    public static class HBin编解码
    {
        public static object 解码(byte[] __负载数据)
        {
            return new BinaryFormatter().Deserialize(new MemoryStream(__负载数据));
        }

        public static byte[] 编码(object __负载)
        {
            var __序列化器 = new BinaryFormatter();
            var __缓存 = new MemoryStream();
            __序列化器.Serialize(__缓存, __负载);
            return __缓存.ToArray();
        }

    }
}
