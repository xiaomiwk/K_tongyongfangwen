using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace 通用访问工具.DTO
{
    public class M执行方法请求
    {
        //对象名称/方法名称/(参数数组){名称/值}
        public string 对象名称 { get; set; }

        public string 方法名称 { get; set; }

        public List<M方法参数> 参数 { get; set; }
    }

    public class M方法参数
    {
        public string 名称 { get; set; }

        public string 值 { get; set; }
    }
}
