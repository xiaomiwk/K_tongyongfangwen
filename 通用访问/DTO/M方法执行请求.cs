using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace 通用访问.DTO
{
    public class M方法执行请求
    {
        //对象名称/方法名称/(参数数组){名称/值}
        public string 对象名称 { get; set; }

        public string 方法名称 { get; set; }

        public List<M实参> 实参列表 { get; set; }

        public M方法执行请求()
        {
            实参列表 = new List<M实参>();
        }

        public override string ToString()
        {
            return string.Format("执行方法({0}.{1})", 对象名称, 方法名称);
        }
    }
}
