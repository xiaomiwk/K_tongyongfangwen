using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace 通用访问工具.DTO
{
    [Flags]
    public enum E角色
    {
        工程 = 1, 开发 = 2
    }

    public class M对象
    {
        public string 名称 { get; set; }

        public string 分类 { get; set; }

        public E角色 角色 { get; set; }
    }

    public class M查询对象列表响应
    {
        public List<M对象> 对象列表 { get; set; }
    }
}
