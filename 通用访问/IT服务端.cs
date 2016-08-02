using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using 通用访问.DTO;

namespace 通用访问
{
    public interface IT服务端
    {
        int 端口 { get; set; }

        int WebApi端口 { get; set; }

        void 添加对象(string 对象名称, Func<M对象> 获取对象);

        void 删除对象(string 对象名称);

        void 开启();

        event Action<IPEndPoint> 客户端已连接;

        event Action<IPEndPoint> 客户端已断开;

        List<IPEndPoint> 客户端列表 { get; }

        void 关闭();

        void 触发事件(string 对象名, string 事件名, Dictionary<string, string> 实参 = null, List<IPEndPoint> 地址列表 = null);
    }
}
