using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace 通用访问工具.DTO
{
    public class M方法
    {
        public string 名称 { get; set; }

        public List<M成员元数据> 参数列表 { get; set; }

        public bool 同步 { get; set; }

        public MDTO元数据 返回值元数据 { get; set; }
    }
}
