using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace 通用访问.DTO
{
    public class M接收事件
    {
        public string 对象名称 { get; set; }

        public string 事件名称 { get; set; }

        public List<M实参> 实参列表 { get; set; }

        public M接收事件()
        {
            实参列表 = new List<M实参>();
        }

        public override string ToString()
        {
            return string.Format("事件({0}.{1})", 对象名称, 事件名称);
        }

    }
}
