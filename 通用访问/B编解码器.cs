using System;
using System.Collections.Generic;
using System.Text;
using INET.模板;

namespace 通用访问
{
    class B编解码器 : N通用编解码
    {
        private Encoding _编码;

        public Action<Type, string> 解码拦截;

        public Action<object, string> 编码拦截;

        public B编解码器(Dictionary<Int16, Type> __报文字典, Dictionary<Int16, string> __优先级字典, Encoding __编码 = null)
        {
            报文字典 = __报文字典;
            通道字典 = __优先级字典;
            _编码 = __编码 ?? Encoding.UTF8;
        }

        protected override object 解码(Int16 __功能码, byte[] __负载数据)
        {
            var __字符串 = _编码.GetString(__负载数据);
            var __类型 = 报文字典[__功能码];
            if (解码拦截 != null)
            {
                解码拦截(__类型, __字符串);
            }
            return HJSON.反序列化(__类型, __字符串);
        }

        protected override byte[] 编码(object __负载)
        {
            var __字符串 = HJSON.序列化(__负载);
            if (编码拦截 != null)
            {
                编码拦截(__负载, __字符串);
            }
            return _编码.GetBytes(__字符串);
        }

    }
}
