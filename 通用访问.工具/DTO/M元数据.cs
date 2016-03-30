using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace 通用访问工具.DTO
{
    public enum E数据结构
    {
        点, 行, 列, 表
    }

    public class M元数据
    {
        public string 名称 { get; set; }

        public string 类型 { get; set; }

        public E数据结构 结构 { get; set; }

        public string 描述 { get; set; }

        public string 范围 { get; set; }

        public string 默认值 { get; set; }

        public List<M元数据> 嵌套 { get; set; }

    }

}
