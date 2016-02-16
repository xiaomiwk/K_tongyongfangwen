using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using Utility.通用;
using 服务端Test.DTO;
using 通用访问;
using 通用访问.DTO;
using 通用访问.自定义序列化;

namespace 服务端Test.IBLL
{
    public interface IB基本状态
    {
        M基本状态 查询();

        void 更新(M基本状态 状态);

        event Action<M基本状态> 发生了重要变化;

        void 重启();
    }

    internal class B基本状态 : IB基本状态
    {
        private M基本状态 _缓存 = new M基本状态();

        private IB调试信息 _IB调试信息 = H容器.取出<IB调试信息>();

        public B基本状态()
        {
            var __IT服务端 = H容器.取出<IT服务端>();
            //__IT服务端.添加对象("基本状态", 创建通用访问对象);
            var __对象 = 创建通用访问对象();
            __IT服务端.添加对象("基本状态", () => __对象);

            _缓存.版本 = "1.0.0.0";
            _缓存.待处理问题.Add(new M问题 { 等级 = E重要性.普通, 内容 = "xxxx" });
            _缓存.待处理问题.Add(new M问题 { 等级 = E重要性.重要, 内容 = "yyyy" });
            _缓存.开启时间 = DateTime.Now;
            _缓存.连接设备.Add(new M设备连接 { IP = IPAddress.Parse("192.168.0.1"), 标识 = "X1", 类型 = "X", 连接中 = true });
            _缓存.连接设备.Add(new M设备连接 { IP = IPAddress.Parse("192.168.0.2"), 标识 = "Y1", 类型 = "Y", 连接中 = true });
            _缓存.位置 = "威尼斯";
            _缓存.业务状态 = new Dictionary<string, string> { { "参数1", "参数1值" }, { "参数2", "参数2值" } };
        }

        private M对象 创建通用访问对象()
        {
            var __对象 = new M对象("基本状态", "");

            #region 无元数据
            __对象.添加属性("版本", () => _缓存.版本, E角色.所有, null);
            __对象.添加属性("位置", () => _缓存.位置, E角色.所有, null);
            __对象.添加属性("开启时间", () => _缓存.开启时间.ToString(), E角色.所有, null);
            __对象.添加属性("待处理问题", () => HJSON.序列化(_缓存.待处理问题), E角色.所有, null);
            __对象.添加属性("连接设备", () => HJSON.序列化(_缓存.连接设备), E角色.所有, null);
            __对象.添加属性("业务状态", () => HJSON.序列化(_缓存.业务状态), E角色.所有, null);
            #endregion
            __对象.添加方法("重启", 参数列表 => { 重启(); return string.Empty; }, E角色.所有, null, null);
            __对象.添加事件("发生了重要变化", E角色.所有, null);
            return __对象;
        }

        public M基本状态 查询()
        {
            return _缓存;
        }

        public void 更新(M基本状态 状态)
        {
            var __发生重要变化 = 验证是否发生重要变化(_缓存.待处理问题, 状态.待处理问题);
            _缓存.待处理问题 = 状态.待处理问题;
            _缓存.连接设备 = 状态.连接设备;
            _缓存.业务状态 = 状态.业务状态;
            if (__发生重要变化)
            {
                On发生了重要变化(_缓存);
                H容器.取出<IT服务端>().触发事件("基本状态", "发生了重要变化");
            }
        }

        private bool 验证是否发生重要变化(List<M问题> __原问题, List<M问题> __新问题)
        {
            Func<M问题, string> __获取关键字 = q =>
            {
                if (q.等级 != E重要性.普通)
                {
                    return string.Format("{0} {1}", q.等级, q.内容);
                }
                return "";
            };
            var __原问题描述 = new StringBuilder();
            __原问题.ForEach(q => __原问题描述.Append(__获取关键字(q)));
            var __新问题描述 = new StringBuilder();
            __新问题.ForEach(q => __新问题描述.Append(__获取关键字(q)));
            return __原问题描述.ToString() == __新问题描述.ToString();
        }

        public event Action<M基本状态> 发生了重要变化;

        protected virtual void On发生了重要变化(M基本状态 obj)
        {
            var handler = 发生了重要变化;
            if (handler != null) handler(obj);
        }

        public void 重启()
        {
            _IB调试信息.增加("开始重启");
        }
    }
}
