using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utility.通用
{
    public class M输入异常 : M预计异常
    {
        public string 输入源 { get; set; }

        public string 输入项 { get; set; }

        public string 输入值 { get; set; }

        public M输入异常(string 输入项, string 输入值, string 错误信息)
            : base(错误信息)
        {
            this.输入项 = 输入项;
            this.输入值 = 输入值;
            this.输入源 = "用户";
        }

        public M输入异常(string 输入项, string 输入值, string 错误信息, string 输入源)
            : base(错误信息)
        {
            this.输入项 = 输入项;
            this.输入值 = 输入值;
            this.输入源 = 输入源;
        }
    }
}
