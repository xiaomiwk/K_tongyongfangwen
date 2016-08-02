using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace 通用访问.服务端测试.DTO
{
    public class M基本状态
    {
        public DateTime 开启时间 { get; set; }

        public string 位置 { get; set; }

        public string 版本 { get; set; }

        public List<M问题> 待处理问题 { get; set; }

        public Dictionary<string, string> 业务状态 { get; set; }

        public List<M设备连接> 连接设备 { get; set; }

        public M基本状态()
        {
            this.连接设备 = new List<M设备连接>();
            this.待处理问题 = new List<M问题>();
            this.业务状态 = new Dictionary<string, string>();
        }
    }

    public class M问题
    {
        public E重要性 等级 { get; set; }

        public string 内容 { get; set; }
    }

    public enum E重要性
    {
        普通,重要,严重
    }

    public class M设备连接
    {
        public System.Net.IPAddress IP { get; set; }

        public string 类型 { get; set; }

        public string 标识 { get; set; }

        public bool 连接中 { get; set; }
    }
}
