using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utility.通用;
using 通用访问;
using 通用访问.DTO;
using 通用访问.服务端测试.DTO;

namespace 通用访问.服务端测试.IBLL
{
    public interface IB业务
    {
        M业务对象 查询();

        void 设置(M业务对象 业务);

        #region 下面接口正常情况下是没有的, 这里为了演示才加上

        M对象_工程视图 查询工程视图();

        M对象_开发视图 查询开发视图();

        #endregion
    }

    class B业务 : IB业务
    {
        private string _号码方案 = "浙江";
        private int _网速 = 512;
        private int _资源消耗 = 1000;
        private string _算法1 = "按时间";
        private string _通信协议 = "SIP 1.1";
        private string _字段1 = "8888";

        public B业务()
        {
            var __IT服务端 = H容器.取出<IT服务端>();
            __IT服务端.添加对象("对象1", 创建工程通用访问对象);
            __IT服务端.添加对象("对象2", 创建开发通用访问对象);
        }

        private M对象 创建工程通用访问对象()
        {
            var __对象 = new M对象("对象1", "业务1");
            __对象.添加属性("号码方案", () => _号码方案, E角色.工程, new M元数据 { 类型 = "string", 结构 = E数据结构.单值, 默认值 = "标准", 范围 = "浙江/标准" });
            __对象.添加属性("网速", () => _网速.ToString(), E角色.工程, new M元数据 { 类型 = "int", 结构 = E数据结构.单值 });
            __对象.添加属性("资源消耗", () => _资源消耗.ToString(), E角色.工程, new M元数据 { 类型 = "int", 结构 = E数据结构.单值, 范围 = "0-10000" });
            __对象.添加方法("设置网速", __参数列表 => { _网速 = int.Parse(__参数列表["网速"]); return string.Empty; }, E角色.工程, new List<M形参>
            {
                new M形参("网速", new M元数据{ 类型 = "int", 结构 = E数据结构.单值, 描述 ="测试3次以上取平均值", 默认值 = "2000"}),
            });
            __对象.添加方法("设置号码方案", __参数列表 => { _号码方案 = __参数列表["号码方案"]; return string.Empty; }, E角色.工程, new List<M形参>
            {
                new M形参("号码方案", new M元数据 {类型 = "string", 结构 = E数据结构.单值, 默认值 = "标准", 范围 = "浙江/标准"}),
            }, null);
            return __对象;
        }

        private M对象 创建开发通用访问对象()
        {
            var __对象 = 创建工程通用访问对象();
            __对象.名称 = "对象2";
            __对象.添加属性("算法1", () => _算法1, E角色.开发, new M元数据 { 类型 = "string", 结构 = E数据结构.单值, 默认值 = "按时间", 范围 = "按时间/按距离/综合" });
            __对象.添加属性("通信协议", () => _通信协议, E角色.开发, new M元数据 { 类型 = "string", 结构 = E数据结构.单值 });
            __对象.添加属性("字段1", () => _字段1, E角色.开发, new M元数据 { 类型 = "string", 结构 = E数据结构.单值 });
            __对象.添加方法("设置算法", __参数列表 => { _算法1 = __参数列表["算法1"]; return string.Empty; }, E角色.开发, new List<M形参>
            {
                new M形参("算法", new M元数据{ 类型 = "string", 结构 = E数据结构.单值, 描述 ="", 默认值 = "综合", 范围 = "按时间/按距离/综合"}),
            });

            return __对象;
        }

        public M业务对象 查询()
        {
            throw new NotImplementedException();
        }

        public void 设置(M业务对象 业务)
        {
            throw new NotImplementedException();
        }

        public M对象_工程视图 查询工程视图()
        {
            return new M对象_工程视图 { 号码方案 = _号码方案, 网速 = _网速, 资源消耗 = _资源消耗 };
        }

        public M对象_开发视图 查询开发视图()
        {
            return new M对象_开发视图 { 号码方案 = _号码方案, 网速 = _网速, 资源消耗 = _资源消耗, 算法1 = _算法1, 通信协议 = _通信协议, 字段1 = _字段1 };
        }
    }

}
