using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utility.通用
{
    public class M预计异常 : Exception
    {
        public string 类别 { get; set; }

        public string 描述 { get; set; }

        public M预计异常(string 描述, string 类别 = null)
            : base(描述)
        {
            this.描述 = 描述;
            this.类别 = 类别;
        }

        public M预计异常(string 描述, Exception 内部异常)
            : base(描述, 内部异常)
        {
            this.描述 = 描述;
        }

        public M预计异常(string 类别, string 描述, Exception 内部异常)
            : base(string.Format("{0} {1}", 类别, 描述), 内部异常)
        {
            this.类别 = 类别;
            this.描述 = 描述;
        }
    }
}
