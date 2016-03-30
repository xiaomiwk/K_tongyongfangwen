using System;
using System.Collections.Generic;
using System.Net;
using 通用访问工具.DTO;

namespace 通用访问工具.IBLL
{
    public interface IB访问设备
    {
        void 连接(IPEndPoint 设备端口);

        void 断开();

        bool 连接正常 { get; }

        /// <summary>
        /// true:主动断开,false:被动断开
        /// </summary>
        event Action<bool> 已断开;

        bool 自动重连 { get; }

        event Action 已连接;

        event Action<M通知> 收到了通知;

        M查询对象列表响应 查询可访问对象();

        M查询对象明细响应 查询对象明细(string 对象名称);

        void 异步执行(string 对象名, string 方法名, List<M方法参数> 参数列表);

        void 同步执行(string 对象名, string 方法名, List<M方法参数> 参数列表);

        string 同步执行带返回值(string 对象名, string 方法名, List<M方法参数> 参数列表);

    }

}
