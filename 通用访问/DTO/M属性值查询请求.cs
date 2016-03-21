using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace 通用访问.DTO
{
    public class M属性值查询请求
    {
        public string 对象名称 { get; set; }

        public string 属性名称 { get; set; }

        public override string ToString()
        {
            return string.Format("查询属性({0}.{1})", 对象名称, 属性名称);
        }

    }
}
