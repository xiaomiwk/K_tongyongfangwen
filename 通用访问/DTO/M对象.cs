using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace 通用访问.DTO
{
    public class M对象
    {
        public string 名称
        {
            get { return 概要.名称; }
            set { 概要.名称 = value; }
        }

        public string 分类
        {
            get { return 概要.分类; }
            set { 概要.分类 = value; }
        }

        public E角色 角色
        {
            get { return 概要.角色; }
            set { 概要.角色 = value; }
        }

        internal M对象概要 概要 { get; set; }

        internal M对象明细查询结果 明细 { get; set; }

        /// <summary>
        /// 字典中的键string:方法名; 值:方法; 方法中的string:返回值
        /// </summary>
        Dictionary<string, Func<Dictionary<string, string>, string>> _所有方法 = new Dictionary<string, Func<Dictionary<string, string>, string>>();

        /// <summary>
        /// 字典中的键string:方法名; 值:方法; 方法中的string:返回值
        /// </summary>
        Dictionary<string, Func<Dictionary<string, string>, IPEndPoint, string>> _所有方法_带地址 = new Dictionary<string, Func<Dictionary<string, string>, IPEndPoint, string>>();

        /// <summary>
        /// 字典中的键string:属性名; 值:属性值计算方法
        /// </summary>
        Dictionary<string, Func<string>> _所有属性方法 = new Dictionary<string, Func<string>>();

        public M对象(string __名称, string __分类)
        {
            this.概要 = new M对象概要(__名称, __分类);
            this.明细 = new M对象明细查询结果();
        }

        public void 添加属性(string __名称, Func<string> __计算值, E角色 __角色 = E角色.开发, M元数据 __元数据 = null)
        {
            this.角色 = this.角色 | __角色;
            this.明细.属性列表.Add(new M属性(__名称, __元数据, __角色));
            _所有属性方法[__名称] = __计算值;
        }

        public void 添加方法(string __名称, Func<Dictionary<string, string>, string> __执行方法, E角色 __角色 = E角色.开发, List<M形参> __参数列表 = null, M元数据 __返回值元数据 = null)
        {
            this.角色 = this.角色 | __角色;
            _所有方法[__名称] = __执行方法;
            this.明细.方法列表.Add(new M方法(__名称, __参数列表, __返回值元数据, __角色));
        }

        public void 添加方法(string __名称, Func<Dictionary<string, string>, IPEndPoint, string> __执行方法, E角色 __角色 = E角色.开发, List<M形参> __参数列表 = null, M元数据 __返回值元数据 = null)
        {
            this.角色 = this.角色 | __角色;
            _所有方法_带地址[__名称] = __执行方法;
            this.明细.方法列表.Add(new M方法(__名称, __参数列表, __返回值元数据, __角色));
        }

        internal string 执行方法(string __名称, Dictionary<string, string> __参数列表, IPEndPoint __来源)
        {
            if (_所有方法.ContainsKey(__名称))
            {
                return _所有方法[__名称].Invoke(__参数列表);
            }
            if (_所有方法_带地址.ContainsKey(__名称))
            {
                return _所有方法_带地址[__名称].Invoke(__参数列表, __来源);
            }
            throw new ApplicationException("执行方法失败: 无此方法");
        }

        internal string 计算属性(string __属性名称)
        {
            if (_所有属性方法.ContainsKey(__属性名称))
            {
                return _所有属性方法[__属性名称]();
            }
            throw new ApplicationException("计算属性失败: 无此属性");
        }

        public void 添加事件(string __名称, E角色 __角色 = E角色.开发, List<M形参> __参数列表 = null)
        {
            this.角色 = this.角色 | __角色;
            this.明细.事件列表.Add(new M事件(__名称, __参数列表, __角色));
        }

        #region 尝试简化元数据构造, 已停用
        ///// <summary>
        ///// 通过简易字符串描述元数据, 只支持两层
        ///// </summary>
        ///// <param name="__名称"></param>
        ///// <param name="__参数描述">名称:类型,结构(名称:类型,结构,描述,默认值,范围;),描述,默认值,范围.</param>
        ///// <param name="__返回值描述">类型,结构(名称:类型,结构,描述,默认值,范围;),描述,默认值,范围</param>
        //public void 添加方法(string __名称, string __参数描述, string __返回值描述, Func<Dictionary<string, string>, string> __执行方法, E角色 __角色 = E角色.开发)
        //{
        //    添加方法(__名称, __执行方法, __角色, 形参转换(__参数描述), 元数据转换(__返回值描述));
        //}

        ///// <summary>
        ///// 通过简易字符串描述元数据, 只支持两层
        ///// </summary>
        ///// <param name="__名称"></param>
        ///// <param name="__参数描述">名称:类型,结构(名称:类型,结构,描述,默认值,范围;),描述,默认值,范围.</param>
        ///// <param name="__返回值描述">类型,结构(名称:类型,结构,描述,默认值,范围;),描述,默认值,范围</param>
        //public void 添加方法(string __名称, string __参数描述, string __返回值描述, Func<Dictionary<string, string>, IPEndPoint, string> __执行方法, E角色 __角色 = E角色.开发)
        //{
        //    添加方法(__名称, __执行方法, __角色, 形参转换(__参数描述), 元数据转换(__返回值描述));
        //}

        ///// <summary>
        ///// 通过简易字符串描述元数据, 只支持两层
        ///// </summary>
        ///// <param name="__参数描述">名称:类型,结构(名称:类型,结构,描述,默认值,范围;),描述,默认值,范围.</param>
        //public void 添加事件(string __名称, string __参数描述, E角色 __角色 = E角色.开发)
        //{
        //    添加事件(__名称, __角色, 形参转换(__参数描述));
        //}

        //private List<M形参> 形参转换(string __参数描述)
        //{
        //    List<M形参> __形参列表 = null;
        //    var __形参字典 = 解析元数据(__参数描述, '.');
        //    if (__形参字典 != null)
        //    {
        //        __形参列表 = new List<M形参>();
        //        foreach (var __kv in __形参字典)
        //        {
        //            __形参列表.Add(new M形参 { 名称 = __kv.Key, 元数据 = __kv.Value });
        //        }
        //    }
        //    return __形参列表;
        //}

        //private M元数据 元数据转换(string __描述)
        //{
        //    if (!string.IsNullOrEmpty(__描述))
        //    {
        //        var __元数据 = 解析元数据("返回值:" + __描述, '.');
        //        return __元数据.Values.First();
        //    }
        //    return null;
        //}

        //private Dictionary<string, M元数据> 解析元数据(string __形参描述, char __分隔符)
        //{
        //    if (string.IsNullOrEmpty(__形参描述))
        //    {
        //        return null;
        //    }
        //    var __结果 = new Dictionary<string, M元数据>();
        //    var __列表 = __形参描述.Split(new[] { __分隔符 }, StringSplitOptions.RemoveEmptyEntries);
        //    for (int i = 0; i < __列表.Length; i++)
        //    {
        //        var __名称 = "";

        //        var __冒号位置 = __列表[i].IndexOf(':');
        //        __名称 = __列表[i].Substring(0, __冒号位置).Trim();
        //        var __元数据 = new M元数据("string");
        //        if (__冒号位置 > 0 && __冒号位置 != __形参描述.Length - 1)
        //        {
        //            var __元数据描述 = __列表[i].Substring(__冒号位置 + 1);
        //            if (__元数据描述.Contains('('))
        //            {
        //                var __起始 = __元数据描述.IndexOf('(');
        //                var __结束 = __元数据描述.IndexOf(')');
        //                var __子成员描述 = __元数据描述.Substring(__起始 + 1, __结束 - __起始 - 1);
        //                var __子成员列表 = 解析元数据(__子成员描述, ';');
        //                foreach (var __kv in __子成员列表)
        //                {
        //                    __元数据.子成员列表.Add(new M子成员 { 名称 = __kv.Key, 元数据 = __kv.Value });
        //                }
        //                __元数据描述 = __元数据描述.Remove(__起始, __结束 - __起始 + 1);
        //            }
        //            var __字段列表 = __元数据描述.Split(new[] { ',' }, StringSplitOptions.None);
        //            if (__字段列表.Length > 0)
        //            {
        //                __元数据.类型 = __字段列表[0].Trim();
        //            }
        //            if (__字段列表.Length > 1)
        //            {
        //                if (string.IsNullOrEmpty(__字段列表[1].Trim()))
        //                {
        //                    __元数据.结构 = E数据结构.单值;
        //                }
        //                else
        //                {
        //                    __元数据.结构 = (E数据结构)Enum.Parse(typeof(E数据结构), __字段列表[1].Trim());
        //                }
        //            }
        //            if (__字段列表.Length > 2)
        //            {
        //                __元数据.描述 = __字段列表[2].Trim();
        //            }
        //            if (__字段列表.Length > 3)
        //            {
        //                __元数据.默认值 = __字段列表[3].Trim();
        //            }
        //            if (__字段列表.Length > 4)
        //            {
        //                __元数据.范围 = __字段列表[4].Trim();
        //            }
        //        }
        //        __结果[__名称] = __元数据;

        //    }
        //    return __结果;
        //}

        #endregion
    }
}
