using System;
using INET.编解码;

namespace INET.会话
{
    public class N事务
    {
        public Int32 发方事务 { get; set; }

        public Int32 收方事务 { get; set; }

        public string 功能码 { get; set; }

        public string 通道标识 { get; set; }

        public N事务 颠倒()
        {
            var __temp = 收方事务;
            收方事务 = 发方事务;
            发方事务 = __temp;
            return this;
        }
    }
}



