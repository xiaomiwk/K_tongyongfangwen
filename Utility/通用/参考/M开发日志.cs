using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Utility.通用
{
    public class M开发日志
    {
        public DateTime 时间 { get; set; }

        public TraceEventType 重要性 { get; set; } 

        public string 模块 { get; set; }

        public string 函数 { get; set; }

        public string 线程 { get; set; }

        public string 描述 { get; set; }

        public string 文件 { get; set; }

        public int 行号 { get; set; }

    }
}
