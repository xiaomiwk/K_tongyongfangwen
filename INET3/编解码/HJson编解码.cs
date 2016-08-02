using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;

namespace INET.编解码
{
    public static class HJson编解码
    {
        public static object 解码(Type __类型, string __字符串, bool __标识类型 = true)
        {
            if (__标识类型)
            {
                return new JavaScriptSerializer(new SimpleTypeResolver()).Deserialize(__字符串, __类型);
            }
            return new JavaScriptSerializer().Deserialize(__字符串, __类型);
        }

        public static object 解码(Type __类型, byte[] __负载数据, Encoding __Encoding, bool __标识类型 = true)
        {
            var __字符串 = __Encoding.GetString(__负载数据);
            if (__标识类型)
            {
                return new JavaScriptSerializer(new SimpleTypeResolver()).Deserialize(__字符串, __类型);
            }
            return new JavaScriptSerializer().Deserialize(__字符串, __类型);
        }

        public static byte[] 编码(object __负载, Encoding __Encoding, bool __标识类型 = true)
        {
            if (__标识类型)
            {
                return __Encoding.GetBytes(new JavaScriptSerializer(new SimpleTypeResolver()).Serialize(__负载));
            }
            return __Encoding.GetBytes(new JavaScriptSerializer().Serialize(__负载));
        }

        public static string 编码(object __负载, bool __标识类型 = true)
        {
            if (__标识类型)
            {
                return new JavaScriptSerializer(new SimpleTypeResolver()).Serialize(__负载);
            }
            return new JavaScriptSerializer().Serialize(__负载);
        }

    }
}
