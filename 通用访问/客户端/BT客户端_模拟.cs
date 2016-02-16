using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using 通用访问.DTO;

namespace 通用访问.客户端
{
    class BT客户端_模拟 : IT客户端
    {
        public IPEndPoint 设备地址 { get; set; }

        public void 连接(IPEndPoint 设备端口)
        {
            //throw new ApplicationException("连接失败");

            Task.Factory.StartNew(() =>
            {
                Thread.Sleep(5000);
                On已断开(false);
                Thread.Sleep(5000);
                On已连接();
            });

            连接正常 = true;
            On已连接();
            On收到了通知(new M通知
            {
                对象 = "系统",
                概要 = "欢迎进入",
                详细 = "",
                重要性 = "普通"
            });
        }

        public void 断开()
        {
            连接正常 = false;
            On已断开(true);
            On收到了通知(new M通知
            {
                对象 = "系统",
                概要 = "谢谢访问",
                详细 = "",
                重要性 = "普通",
                角色 = E角色.工程 | E角色.开发
            });
        }

        public bool 连接正常 { get; set; }

        public event Action<bool> 已断开;

        protected virtual void On已断开(bool __主动)
        {
            Action<bool> handler = 已断开;
            if (handler != null) handler(__主动);
        }

        public bool 自动重连 { get; set; }

        public event Action 已连接;

        protected virtual void On已连接()
        {
            Action handler = 已连接;
            if (handler != null) handler();
        }

        public event Action<M通知> 收到了通知;

        protected virtual void On收到了通知(M通知 __通知)
        {
            var handler = 收到了通知;
            if (handler != null) handler(__通知);
        }

        public M对象列表查询结果 查询可访问对象()
        {
            var __结果 = new M对象列表查询结果();
            __结果.AddRange(new List<M对象概要>
            {
                new M对象概要 {分类 = "", 名称 = "基本状态"},
                new M对象概要 {分类 = "业务参数", 名称 = "业务参数007"},
                new M对象概要 {分类 = "开发", 名称 = "对象008"},
            });
            return __结果;
        }

        private M元数据 获取表元数据()
        {
            var __元数据 = 获取行元数据();
            __元数据.类型 = "Mdemo";
            __元数据.结构 = E数据结构.表;
            __元数据.描述 = "表结构";
            return __元数据;

        }

        private M元数据 获取行元数据()
        {
            return new M元数据
            {
                类型 = "Mdemo",
                结构 = E数据结构.行,
                描述 = "行结构",
                默认值 = "",
                范围 = "范围1",
                子成员列表 = new List<M子成员>
                {
                    new M子成员("点属性", "int"),
                    new M子成员("行属性", new M元数据
                    {
                        类型 = "Mdemo1",
                        结构 = E数据结构.行,
                        描述 = "行结构",
                        范围 = "范围12",
                        子成员列表 = new List<M子成员>
                        {
                            new M子成员("点属性1","int"),
                            new M子成员("点属性2","string"),
                        }
                    })
                }
            };
        }

        public M对象明细查询结果 查询对象明细(string 对象名称)
        {
            switch (对象名称)
            {
                case "基本状态":
                    return new M对象明细查询结果
                    {
                        属性列表 = new List<M属性>
                        {
                            new M属性
                            {
                                名称 = "属性1",
                                元数据 = 获取表元数据(),
                            },
                            new M属性
                            {
                                名称 = "属性2", 
                                元数据 = new M元数据{ 类型 = "int", 结构 = E数据结构.列, 描述 = "列结构", 默认值 = "0",范围 = "范围2", },
                            },
                            new M属性
                            {
                                名称 = "属性3",
                                元数据 = 获取行元数据(),
                            },
                            new M属性
                            {
                                名称 = "属性4",
                                元数据 = new M元数据{  类型 = "string", 结构 = E数据结构.点, 描述 = "点结构, 字符串", 默认值 = "HELLO",范围 = "范围4", },
                            },
                            new M属性
                            {
                                名称 = "属性5",
                                元数据 = new M元数据{ 类型 = "int", 结构 = E数据结构.点, 描述 = "点结构, 整数", 默认值 = "0", 范围 = "范围5", },
                            },
                        },
                        方法列表 = new List<M方法>
                        {
                            new M方法
                            {
                                名称 = "方法1", 
                                形参列表 = new List<M形参>
                                {
                                    new M形参( "参数11",  new M元数据{ 类型 = "int", 结构 = E数据结构.点, 描述 = "点结构", 默认值 = "1", 范围 ="范围11" }),
                                    new M形参( "参数12",  new M元数据{ 类型 = "string", 结构 = E数据结构.点, 描述 = "点结构", 默认值 = "默认值2",范围 = "范围12" }),
                                }, 
                                返回值元数据 = new M元数据 { 类型 = "int", 结构 = E数据结构.点, 描述 = "点结构", 默认值 = "默认值11", 范围="范围11" }      
                            },
                            new M方法
                            {
                                名称 = "方法2", 
                                形参列表 = new List<M形参>
                                {
                                    new M形参( "参数0",  new M元数据{ 类型 = "int", 结构 = E数据结构.点, 描述 = "点结构", 默认值 = "100", 范围 ="范围0" }),
                                    new M形参("参数1",获取表元数据()),
                                }, 
                                返回值元数据 = 获取表元数据(),
                            },
                        },
                        事件列表 = new List<M事件>
                        {
                            new M事件
                            {
                                名称 = "事件1", 
                                形参列表 = new List<M形参>
                                {
                                    new M形参( "参数11",  new M元数据{ 类型 = "int", 结构 = E数据结构.点, 描述 = "点结构", 默认值 = "1", 范围 ="范围11" }),
                                    new M形参( "参数12",  new M元数据{ 类型 = "string", 结构 = E数据结构.点, 描述 = "点结构", 默认值 = "默认值2",范围 = "范围12" }),
                                }, 
                            },
                            new M事件
                            {
                                名称 = "事件2", 
                                形参列表 = new List<M形参>
                                {
                                }, 
                            },
                        }
                    };
                default:
                    throw new ApplicationException("查询对象明细, 对象名称未配置: " + 对象名称);
            }
        }

        public string 执行方法(string 对象名, string 方法名, List<M实参> 参数列表)
        {
            Debug.WriteLine("执行方法 {0}.{1}({2})参数列表:", 对象名, 方法名, HJSON.序列化(参数列表));
            if (方法名 == "方法2")
            {
                //表结构
                return HJSON.序列化(new List<Mdemo>
                {
                    new Mdemo {点属性 = 111, 行属性 = new Mdemo1 {点属性1 = 1211, 点属性2 = "1221"}},
                    new Mdemo {点属性 = 112, 行属性 = new Mdemo1 {点属性1 = 1212, 点属性2 = "1222"}},
                });
            }
            if (方法名 == "方法3")
            {
                //行结构
                return HJSON.序列化(new Mdemo { 点属性 = 111, 行属性 = new Mdemo1 { 点属性1 = 1211, 点属性2 = "1221" } });
            }
            if (方法名 == "方法4")
            {
                //行结构
                return HJSON.序列化(new List<int> { 1, 2, 3 });
            }
            if (方法名 == "方法5")
            {
                //复杂字符串
                return @"+----*---------*--------*----------------*-------*--------*--------*--------*--------------*--------------*------------------------+
| No |   类型  |  域名  |        IP      | 心跳  |连接状态|死亡时间|应答时间|   打包登记   |   GIS恢复    |      上次断开时间      |
+----+---------+--------+----------------+-------+--------+--------+--------+--------------+--------------+------------------------+
| 1  | DTC     | r134   |192.168.12.134  |不在线 |UNLINK  | 0      | 0      | 等待打包     | 等待恢复     |MON NOV 16 11:37:45 2015|
| 2  | DTC     | r20    |192.168.12.20   |不在线 |UNLINK  | 0      | 0      | 等待打包     | 等待恢复     |THU JAN 01 00:00:00 1970|
| 3  | DTC     | r136   |192.168.12.136  |不在线 |UNLINK  | 0      | 0      | 等待打包     | 等待恢复     |THU JAN 01 00:00:00 1970|
| 4  | DTC     | r29    |192.168.12.29   |不在线 |UNLINK  | 0      | 0      | 等待打包     | 等待恢复     |THU JAN 01 00:00:00 1970|
| 5  | DTC     | r46    |192.168.12.46   |不在线 |UNLINK  | 0      | 0      | 等待打包     | 等待恢复     |THU JAN 01 00:00:00 1970|
| 6  | DTC     | r42    |192.168.12.42   |不在线 |UNLINK  | 0      | 0      | 等待打包     | 等待恢复     |THU JAN 01 00:00:00 1970|
| 7  | DTC     | r38    |192.168.12.38   |不在线 |UNLINK  | 0      | 0      | 等待打包     | 等待恢复     |THU JAN 01 00:00:00 1970|
| 8  | DTC     | r68    |192.168.12.68   |不在线 |UNLINK  | 0      | 0      | 等待打包     | 等待恢复     |THU JAN 01 00:00:00 1970|
| 9  | DTC     | r85    |192.168.12.85   |不在线 |UNLINK  | 0      | 0      | 等待打包     | 等待恢复     |THU JAN 01 00:00:00 1970|
| 10 | DTC     | r81    |192.168.12.81   |不在线 |UNLINK  | 0      | 0      | 等待打包     | 等待恢复     |THU JAN 01 00:00:00 1970|
| 11 | DTC     | r89    |192.168.12.89   |不在线 |UNLINK  | 0      | 0      | 等待打包     | 等待恢复     |THU JAN 01 00:00:00 1970|
| 12 | DTC     | r88    |192.168.12.88   |在线   |ALIVE   | 29     | 0      | 打包完成     | 恢复完成     |THU JAN 01 00:00:00 1970|
| 13 | DTC     | r59    |192.168.12.59   |不在线 |UNLINK  | 0      | 0      | 等待打包     | 等待恢复     |THU JAN 01 00:00:00 1970|
| 14 | DTC     | r55    |192.168.12.55   |不在线 |UNLINK  | 0      | 0      | 等待打包     | 等待恢复     |THU JAN 01 00:00:00 1970|
| 15 | DTC     | r64    |192.168.12.64   |不在线 |UNLINK  | 0      | 0      | 等待打包     | 等待恢复     |THU JAN 01 00:00:00 1970|
| 16 | DTC     | r93    |192.168.12.93   |不在线 |UNLINK  | 0      | 0      | 等待打包     | 等待恢复     |THU JAN 01 00:00:00 1970|
| 17 | DTC     | r73    |192.168.12.73   |不在线 |UNLINK  | 0      | 0      | 等待打包     | 等待恢复     |THU JAN 01 00:00:00 1970|
| 18 | DTC     | r97    |192.168.12.97   |不在线 |UNLINK  | 0      | 0      | 等待打包     | 等待恢复     |THU JAN 01 00:00:00 1970|
| 19 | DTC     | r206   |192.168.12.206  |在线   |ALIVE   | 25     | 0      | 打包完成     | 恢复完成     |THU JAN 01 00:00:00 1970|
| 20 | DTC     | r51    |192.168.12.51   |不在线 |UNLINK  | 0      | 0      | 等待打包     | 等待恢复     |THU JAN 01 00:00:00 1970|
| 21 | DTC     | r160   |192.168.12.160  |不在线 |UNLINK  | 0      | 0      | 等待打包     | 等待恢复     |THU JAN 01 00:00:00 1970|
| 22 | DTC     | r167   |192.168.12.167  |不在线 |UNLINK  | 0      | 0      | 等待打包     | 等待恢复     |THU JAN 01 00:00:00 1970|
| 23 | DTC     | r143   |192.168.12.143  |不在线 |UNLINK  | 0      | 0      | 等待打包     | 等待恢复     |THU JAN 01 00:00:00 1970|
| 24 | DTC     | r135   |192.168.12.135  |不在线 |UNLINK  | 0      | 0      | 等待打包     | 等待恢复     |THU JAN 01 00:00:00 1970|
| 25 | DTC     | r139   |192.168.12.139  |不在线 |UNLINK  | 0      | 0      | 等待打包     | 等待恢复     |THU JAN 01 00:00:00 1970|
| 26 | DTC     | r151   |192.168.12.151  |不在线 |UNLINK  | 0      | 0      | 等待打包     | 等待恢复     |THU JAN 01 00:00:00 1970|
| 27 | DTC     | r155   |192.168.12.155  |不在线 |UNLINK  | 0      | 0      | 等待打包     | 等待恢复     |THU JAN 01 00:00:00 1970|
| 28 | DTC     | r234   |192.168.12.234  |不在线 |UNLINK  | 0      | 0      | 等待打包     | 等待恢复     |THU JAN 01 00:00:00 1970|
| 29 | DTC     | r163   |192.168.12.163  |不在线 |UNLINK  | 0      | 0      | 等待打包     | 等待恢复     |THU JAN 01 00:00:00 1970|
| 30 | DTC     | r179   |192.168.12.179  |不在线 |UNLINK  | 0      | 0      | 等待打包     | 等待恢复     |THU JAN 01 00:00:00 1970|
| 31 | DTC     | r188   |192.168.12.188  |不在线 |UNLINK  | 0      | 0      | 等待打包     | 等待恢复     |THU JAN 01 00:00:00 1970|
| 32 | DTC     | r196   |192.168.12.196  |不在线 |UNLINK  | 0      | 0      | 等待打包     | 等待恢复     |THU JAN 01 00:00:00 1970|
| 33 | DTC     | r200   |192.168.12.200  |不在线 |UNLINK  | 0      | 0      | 等待打包     | 等待恢复     |THU JAN 01 00:00:00 1970|
| 34 | DTC     | r230   |192.168.12.230  |不在线 |UNLINK  | 0      | 0      | 等待打包     | 等待恢复     |THU JAN 01 00:00:00 1970|
| 35 | EGW10A  | x1     |192.168.12.17   |不在线 |-       | 30     | 0      | -            | -            |-                       |
| 36 | BGW     | b1     |192.168.12.56   |在线   |-       | 28     | 0      | -            | -            |-                       |
| 37 | REC     | e1     |192.168.12.3    |不在线 |-       | 30     | 0      | -            | -            |-                       |
| 38 | GIS     | g1     |192.168.12.3    |在线   |ALIVE   | 25     | 0      | -            | 恢复完成     |THU JAN 01 00:00:00 1970|
| 39 | DCS     | m26    |192.168.12.26   |不在线 |-       | 30     | 0      | -            | -            |-                       |
| 40 | DCS     | m22    |192.168.12.22   |不在线 |-       | 30     | 0      | -            | -            |-                       |
| 41 | DCS     | m24    |192.168.12.24   |不在线 |-       | 30     | 0      | -            | -            |-                       |
+----+---------+--------+----------------+-------+--------+--------+--------+--------------+--------------+------------------------+";
            }
            return "WANGKAI";
        }

        public class Mdemo
        {
            public int 点属性 { get; set; }
            public Mdemo1 行属性 { get; set; }
        }

        public class Mdemo1
        {
            public int 点属性1 { get; set; }
            public string 点属性2 { get; set; }
        }

        public void 订阅事件(string 对象名, string 事件名, Action<List<M实参>> 处理方法)
        {
            if (事件名 == "事件1")
            {
                Task.Factory.StartNew(() =>
                {
                    Thread.Sleep(3000);
                    处理方法(new List<M实参>
                        {
                            new M实参 {名称 = "参数11", 值 = "88"},
                            new M实参 {名称 = "参数12", 值 = "wk"},
                        });
                    On收到了事件(new M接收事件
                    {
                        对象名称 = 对象名,
                        事件名称 = 事件名,
                        实参列表 = new List<M实参>
                        {
                            new M实参 {名称 = "参数11", 值 = "88"},
                            new M实参 {名称 = "参数12", 值 = "wk"},
                        }
                    });
                });
            }
            if (事件名 == "事件2")
            {
                Task.Factory.StartNew(() =>
                {
                    Thread.Sleep(3000);
                    处理方法(null);
                    On收到了事件(new M接收事件
                    {
                        对象名称 = 对象名,
                        事件名称 = 事件名,
                    });
                });
            }
        }

        public void 注销事件(string 对象名, string 事件名, Action<List<M实参>> 处理方法)
        {

        }

        public event Action<M接收事件> 收到了事件;

        protected virtual void On收到了事件(M接收事件 obj)
        {
            var handler = 收到了事件;
            if (handler != null) handler(obj);
        }

        public string 查询属性值(string 对象名, string 属性名)
        {
            switch (属性名)
            {
                case "属性1":
                    return HJSON.序列化(new List<Mdemo>
                    {
                        new Mdemo {点属性 = 111, 行属性 = new Mdemo1 {点属性1 = 1211, 点属性2 = "1221"}},
                        new Mdemo {点属性 = 112, 行属性 = new Mdemo1 {点属性1 = 1212, 点属性2 = "1222"}},
                    });
                case "属性2":
                    return HJSON.序列化(new List<int> {1, 2, 3});
                case "属性3":
                    return HJSON.序列化(new Mdemo {点属性 = 111, 行属性 = new Mdemo1 {点属性1 = 1211, 点属性2 = "1221"}});
                case "属性4":
                    return HJSON.序列化("HELLO 你好");
                case "属性5":
                    return HJSON.序列化(1);
            }
            return "";
        }
    }
}
