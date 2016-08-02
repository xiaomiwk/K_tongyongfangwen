using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace 通用访问.DTO
{
    public class M对象概要
    {
        public string 名称 { get; set; }

        public string 分类 { get; set; }

        public E角色 角色 { get; set; }

        public M对象概要()
        {
        }

        public M对象概要(string 名称, string 分类)
        {
            this.名称 = 名称;
            this.分类 = 分类;
        }
    }
}
