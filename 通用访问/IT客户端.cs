using System;
using System.Collections.Generic;
using System.Net;
using 通用访问.DTO;

namespace 通用访问
{
    public interface IT客户端
    {
        IPEndPoint 设备地址 { get; }

        void 连接(IPEndPoint 设备地址);

        void 断开();

        bool 连接正常 { get; }

        /// <summary>
        /// true:主动断开,false:被动断开
        /// </summary>
        event Action<bool> 已断开;

        bool 自动重连 { get; set; }

        event Action 已连接;

        M对象列表查询结果 查询可访问对象();

        M对象明细查询结果 查询对象明细(string 对象名称);

        string 查询属性值(string 对象名, string 属性名, int 超时毫秒 = 3000);

        string 执行方法(string 对象名, string 方法名, Dictionary<string,string> 参数列表, int 超时毫秒 = 10000);

        void 订阅事件(string 对象名, string 事件名, Action<Dictionary<string, string>> 处理方法);

        void 注销事件(string 对象名, string 事件名, Action<Dictionary<string, string>> 处理方法);

        /// <summary>
        /// 通常用于调试
        /// </summary>
        event Action<M接收事件> 收到了事件;
    }

}
