using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace 通用访问.DTO
{
    public class M形参 : M子成员
    {
        public M形参()
            : base()
        {
        }

        public M形参(string __名称, M元数据 __元数据)
            : base(__名称, __元数据)
        {
        }

        public M形参(string __名称, string __类型)
            : base(__名称, __类型)
        {
        }

        public M形参(string __名称, string __类型, E数据结构 __结构)
            : base(__名称, __类型, __结构)
        {
        }

    }
}
