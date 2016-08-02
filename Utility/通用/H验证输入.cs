using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using Utility.通用;

namespace System
{
    public static class H验证输入
    {
        /// <param name="输入源">不填默认为“用户”</param>
        /// <exception cref="M输入格式异常"/>
        public static int ToInt(this string 输入值, string 输入项, string 输入源 = "用户")
        {
            int result;
            if (int.TryParse(输入值, out result))
            {
                return result;
            }
            throw new M输入异常(输入项, 输入值, string.Format("{0} 请输入整数", 输入项), 输入源);
        }

        /// <param name="输入源">不填默认为“用户”</param>
        /// <exception cref="M输入格式异常"/>
        public static IPAddress ToIP(this string 输入值, string 输入项, string 输入源 = "用户")
        {
            IPAddress result;
            if (IPAddress.TryParse(输入值, out result))
            {
                return result;
            }
            throw new M输入异常(输入项, 输入值, string.Format("{0} 请输入IP", 输入项), 输入源);
        }
    }
}
