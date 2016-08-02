using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using INET.编解码;

namespace INET.模板
{
    /// <summary>
    /// 格式如下
    /// 字段	            类型	    长度(字节)	    说明
    /// 起始标识	        Byte[]	    2	            固定为0xAAAA，用于识别报文开始
    /// 报文内容长度	    Int	        4	            余下所有字段长度
    /// 功能标识	        Int16	    2	            用于识别报文类型
    /// 发送方事务标识	    Int32	    4	            由发送方指定。当会话类型为请求型时，用于区别发送方的不同请求；当会话类型为通知型时，固定为0。
    /// 接收方事务标识	    Int32	    4	            由接收方指定。当会话类型为请求型时，用于区别接收方的不同请求，特别的，会话开始的第一条报文，发送方的该字段填0；当会话类型为通知型时，固定为0。
    /// 负载	            Byte[]	    可变	        JavaScriptSerializer    
    /// </summary>
    public class NJson通用编解码 : N通用编解码
    {
        private Encoding _编码;

        public NJson通用编解码(Dictionary<Int16, Type> __报文字典, Dictionary<Int16, string> __优先级字典, Encoding __编码 = null)
        {
            报文字典 = __报文字典;
            通道字典 = __优先级字典;
            _编码 = __编码 ?? Encoding.UTF8;
        }

        protected override object 解码(Int16 __功能码, byte[] __负载数据)
        {
            var __字符串 = _编码.GetString(__负载数据);
            H日志输出.记录(string.Format("解码报文 [{0}]", __功能码.ToString("X4")), __字符串);
            var __类型 = 报文字典[__功能码];
            return HJson编解码.解码(__类型, __字符串);
        }

        protected override byte[] 编码(object __负载)
        {
            return HJson编解码.编码(__负载, _编码);
        }

    }
}
