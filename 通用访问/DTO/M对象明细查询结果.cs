using System.Collections.Generic;

namespace 通用访问.DTO
{
    public class M对象明细查询结果
    {
        public List<M属性> 属性列表 { get; set; }

        public List<M方法> 方法列表 { get; set; }

        public List<M事件> 事件列表 { get; set; }

        public M对象明细查询结果()
        {
            this.属性列表 = new List<M属性>();
            this.方法列表 = new List<M方法>();
            this.事件列表 = new List<M事件>();
        }
    }
}
