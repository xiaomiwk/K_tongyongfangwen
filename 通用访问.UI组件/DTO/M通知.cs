using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace 通用访问工具.DTO
{
    public class M通知
    {
        /// <summary>
        /// 普遍, 重要, 严重
        /// </summary>
        public string 重要性 { get; set; }

        public E角色 角色 { get; set; }

        public string 对象 { get; set; }

        public string 概要 { get; set; }

        public string 详细 { get; set; }
    }
}
