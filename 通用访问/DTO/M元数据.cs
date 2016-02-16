using System.Collections.Generic;

namespace 通用访问.DTO
{
    public class M元数据
    {
        public string 类型 { get; set; }

        public E数据结构 结构 { get; set; }

        public string 描述 { get; set; }

        public string 范围 { get; set; }

        public string 默认值 { get; set; }

        public List<M子成员> 子成员列表 { get; set; }

        public M元数据()
        {
            this.子成员列表 = new List<M子成员>();
        }

        public M元数据(string 类型, E数据结构 结构 = E数据结构.单值)
            : this()
        {
            this.类型 = 类型;
            this.结构 = 结构;
        }
    }
}
