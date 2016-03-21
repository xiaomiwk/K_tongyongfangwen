using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace 通用访问.DTO
{
    [Flags]
    public enum E角色
    {
        工程 = 1, 开发 = 2, 客户 = 4, 所有 = 7
    }

    public enum E数据结构
    {
        单值, 对象, 单值数组, 对象数组
    }

    //public class E角色
    //{
    //    public const string 工程 = "工程";
    //    public const string 开发 = "开发";
    //    public const string 客户 = "客户";
    //}

    //public class E数据结构
    //{
    //    public const string 单值 = "单值";
    //    public const string 对象 = "对象";
    //    public const string 单值数组 = "单值数组";
    //    public const string 对象数组 = "对象数组";
    //}

}
