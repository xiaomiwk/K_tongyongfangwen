using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace 通用访问.DTO
{
    public class M事件
    {
        public string 名称 { get; set; }

        public List<M形参> 形参列表 { get; set; }

        public E角色 角色 { get; set; }

        public M事件()
        {
            角色 = E角色.开发;
        }

        public M事件(string __名称, List<M形参> __参数列表 = null, E角色 __角色 = E角色.开发)
        {
            this.名称 = __名称;
            this.形参列表 = __参数列表;
            this.角色 = __角色;
        }
    }

}
