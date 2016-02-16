using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace 通用访问.DTO
{
    public class M属性
    {
        public string 名称 { get; set; }

        public M元数据 元数据 { get; set; }

        public E角色 角色 { get; set; }

        public M属性()
        {
            角色 = E角色.开发;
        }

        public M属性(string __名称)
        {
            this.名称 = __名称;
        }

        public M属性(string __名称, M元数据 __元数据, E角色 __角色 = E角色.开发)
            : this(__名称)
        {
            this.元数据 = __元数据;
            this.角色 = __角色;
        }
    }
}
