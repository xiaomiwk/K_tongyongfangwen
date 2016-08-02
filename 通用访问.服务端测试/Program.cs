using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Windows.Forms;
using Utility.通用;
using 通用访问;
using 通用访问.DTO;
using 通用访问.服务端测试.DTO;
using 通用访问.服务端测试.IBLL;

namespace 通用访问.服务端测试
{
    static class Program
    {
        public static F主窗口 F主窗口;

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            H调试.初始化();
            IT服务端 __IT服务端 = FT通用访问工厂.创建服务端();
            H容器.注入<IT服务端>(__IT服务端);
            H容器.注入<IB基本状态, B基本状态>();
            H容器.注入<IB业务, B业务>();
            H容器.注入<IB调试信息, B调试信息>();
            var __对象 = new B测试().创建对象();
            __IT服务端.添加对象("测试", () => __对象);

            __IT服务端.端口 = 8888;
            __IT服务端.开启();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            F主窗口 = new F主窗口();
            Application.Run(F主窗口);
        }

static void 服务端()
{
    //创建服务器
    var __服务端 = FT通用访问工厂.创建服务端();
    __服务端.端口 = 8888;
    __服务端.开启();

    //实际对象
    var __基本状态 = new M基本状态();
    __基本状态.版本 = "1.0.0.0";
    __基本状态.待处理问题.Add(new M问题 { 等级 = E重要性.普通, 内容 = "xxxx" });
    __基本状态.待处理问题.Add(new M问题 { 等级 = E重要性.重要, 内容 = "yyyy" });
    __基本状态.开启时间 = DateTime.Now;
    __基本状态.连接设备.Add(new M设备连接 { IP = IPAddress.Parse("192.168.0.1"), 标识 = "X1", 类型 = "X", 连接中 = true });
    __基本状态.连接设备.Add(new M设备连接 { IP = IPAddress.Parse("192.168.0.2"), 标识 = "Y1", 类型 = "Y", 连接中 = true });
    __基本状态.位置 = "威尼斯";
    __基本状态.业务状态 = new Dictionary<string, string> { { "参数1", "参数1值" }, { "参数2", "参数2值" } };

    //可通信对象
    var __对象 = new M对象("基本状态", "");
    __对象.添加属性("版本", () => __基本状态.版本, E角色.所有, null);//最后一个参数是元数据定义
    __对象.添加属性("位置", () => __基本状态.位置, E角色.所有, null);
    __对象.添加属性("开启时间", () => __基本状态.开启时间.ToString(), E角色.所有, null);
    __对象.添加属性("待处理问题", () => HJSON.序列化(__基本状态.待处理问题), E角色.所有, null);
    __对象.添加属性("连接设备", () => HJSON.序列化(__基本状态.连接设备), E角色.所有, null);
    __对象.添加属性("业务状态", () => HJSON.序列化(__基本状态.业务状态), E角色.所有, null);
    __对象.添加方法("重启", __实参 => {
        //处理实参 

        return string.Empty;  //返回空字符串或者json格式的对象
    }, E角色.所有, null, null); //最后两个参数是对参数和返回值的元数据定义
    __对象.添加事件("发生了重要变化", E角色.所有, null);

    //将对象加入到服务器
    __服务端.添加对象("基本状态", () => __对象);

    //触发服务器端事件
    __服务端.触发事件("基本状态", "发生了重要变化", null, null);//最后两个参数是实参和客户端地址列表(可选)

    //创建客户端
    var __客户端 = FT通用访问工厂.创建客户端();
    __客户端.连接(new IPEndPoint(IPAddress.Any, 8888));
    var __版本 = __客户端.查询属性值("基本状态", "版本");
    var __待处理问题 = HJSON.反序列化<List<M问题>>(__客户端.查询属性值("基本状态", "待处理问题"));
    __客户端.执行方法("基本状态", "重启", null); //最后一个参数是实参
    __客户端.订阅事件("基本状态", "发生了重要变化", __实参 => {
        //处理实参
    });
}


    }
}
