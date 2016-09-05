using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace 通用访问工具.DTO
{
    public class M查询对象明细响应
    {
        public List<M属性> 属性列表 { get; set; }

        public List<M方法> 方法列表 { get; set; }
    }

    public class M方法
    {
        public string 名称 { get; set; }

        public List<M元数据> 参数列表 { get; set; }

        public bool 同步 { get; set; }

        public M元数据 返回值元数据 { get; set; }
    }

    public class M属性
    {
        public M元数据 元数据 { get; set; }

        public string 值 { get; set; }
    }

}
