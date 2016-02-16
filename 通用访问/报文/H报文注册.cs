using System;
using System.Collections.Generic;
using INET;
using INET.会话;
using 通用访问.DTO;

namespace 通用访问.报文
{
    internal class H报文注册
    {
        private static Dictionary<int, Type> _报文字典 = new Dictionary<int, Type>
        {
            {0,typeof(M通知)},
            {1,typeof(M对象列表请求)},
            {2,typeof(M对象列表)},
            {3,typeof(M查询对象)},
            {4,typeof(M对象明细)},
            {5,typeof(M方法请求)},
            {6,typeof(M方法响应)},
            {7,typeof(P心跳)},
        };

        public static int 查询功能码(Type __报文类型)
        {
            foreach (var __KY in _报文字典)
            {
                if (__KY.Value == __报文类型)
                {
                    return __KY.Key;
                }
            }
            throw new ArgumentOutOfRangeException(string.Format("报文类型无效: {0}", __报文类型));
        }

        public static IN事务报文 解码(byte[] __消息)
        {
            var __功能标识 = P报文.解码功能标识(__消息);
            if (!_报文字典.ContainsKey(__功能标识))
            {
                throw new ArgumentOutOfRangeException(string.Format("功能标识无效: {0}", __功能标识));
            }
            var __报文类型 = _报文字典[__功能标识];
            var __报文 = (IN事务报文)Activator.CreateInstance(__报文类型);
            try
            {
                __报文.解码(__消息);
            }
            catch (Exception)
            {
                H日志输出.记录(string.Format("无法解码: {0}", BitConverter.ToString(__消息)));
                return null;
            }

            return __报文;
        }


    }
}
