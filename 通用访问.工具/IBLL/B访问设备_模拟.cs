using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Utility.存储;
using Utility.通用;
using 通用访问工具.DTO;

namespace 通用访问工具.IBLL
{
    class B访问设备_模拟 : IB访问设备
    {
        public void 连接(IPEndPoint 设备端口)
        {
            //throw new M预计异常("连接失败");

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
                 对象 = "系统", 概要="欢迎进入", 详细="", 重要性 = "普通"
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
                重要性 = "普通"
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

        public M查询对象列表响应 查询可访问对象()
        {
            return new M查询对象列表响应
            {
                对象列表 = new List<M对象>
                {
                    new M对象{ 分类 = "", 名称 = "基本状态", 角色 = E角色.工程 | E角色.开发},
                    new M对象{ 分类 = "业务参数", 名称 = "业务参数007", 角色 = E角色.工程},
                    new M对象{ 分类 = "开发", 名称 = "对象008", 角色 = E角色.开发},
                }
            };
        }

        private M元数据 获取表元数据(string __名称)
        {
            var __元数据 = 获取行元数据(__名称);
            __元数据.类型 = "Mdemo[]";
            __元数据.结构 = E数据结构.表;
            __元数据.描述 = "表结构";
            return __元数据;

        }

        private M元数据 获取行元数据(string __名称)
        {
            return new M元数据
            {
                名称 = __名称,
                类型 = "Mdemo",
                结构 = E数据结构.行,
                描述 = "行结构",
                默认值 = "",
                范围 = "范围1",
                嵌套 = new List<M元数据>
                {
                    new M元数据 {名称 = "点属性", 类型 = "int", 结构 = E数据结构.点, 描述 = "点结构", 范围 = "范围11"},
                    new M元数据
                    {
                        名称 = "行属性",
                        类型 = "Mdemo1",
                        结构 = E数据结构.行,
                        描述 = "行结构",
                        范围 = "范围12",
                        嵌套 = new List<M元数据>
                        {
                            new M元数据 {名称 = "点属性1", 类型 = "int", 结构 = E数据结构.点, 描述 = "点结构", 范围 = "范围121"},
                            new M元数据 {名称 = "点属性2", 类型 = "string", 结构 = E数据结构.点, 描述 = "点结构", 范围 = "范围122"},
                        }
                    }
                }
            };

        }

        public M查询对象明细响应 查询对象明细(string 对象名称)
        {
            switch (对象名称)
            {
                case "基本状态":
                    return new M查询对象明细响应
                    {
                        属性列表 = new List<M属性>
                        {
                            new M属性
                            {
                                值 = HJSON.序列化(new List<Mdemo>
                                {
                                    new Mdemo{ 点属性 = 111, 行属性 = new Mdemo1{ 点属性1 = 1211, 点属性2 = "1221"}},
                                    new Mdemo{ 点属性 = 112, 行属性 = new Mdemo1{ 点属性1 = 1212, 点属性2 = "1222"}},
                                }),
                                元数据 = 获取表元数据("属性1"),
                            },
                            new M属性
                            {
                                
                                值 = HJSON.序列化(new List<int>{1,2,3}),
                                元数据 = new M元数据{ 名称 = "属性2", 类型 = "int[]", 结构 = E数据结构.列, 描述 = "列结构", 默认值 = "0",范围 = "范围2", },
                            },
                            new M属性
                            {
                                值 = HJSON.序列化(new Mdemo{ 点属性 = 111, 行属性 = new Mdemo1{ 点属性1 = 1211, 点属性2 = "1221"}}),
                                元数据 = 获取行元数据("属性3"),
                            },
                            new M属性
                            {
                                值 = HJSON.序列化("HELLO 你好"),
                                元数据 = new M元数据{ 名称 = "属性4", 类型 = "string", 结构 = E数据结构.点, 描述 = "点结构, 字符串", 默认值 = "HELLO",范围 = "范围4", },
                            },
                            new M属性
                            {
                                值 = HJSON.序列化(1),
                                元数据 = new M元数据{ 名称 = "属性5",类型 = "int", 结构 = E数据结构.点, 描述 = "点结构, 整数", 默认值 = "0", 范围 = "范围5", },
                            },
                        },
                        方法列表 = new List<M方法>
                        {
                            new M方法
                            {
                                名称 = "方法1", 
                                同步 = true, 
                                参数列表 = new List<M元数据>
                                {
                                    new M元数据{ 名称 = "参数11", 类型 = "int", 结构 = E数据结构.点, 描述 = "点结构", 默认值 = "1", 范围 ="范围11" },
                                    new M元数据{ 名称 = "参数12", 类型 = "string", 结构 = E数据结构.点, 描述 = "点结构", 默认值 = "默认值2",范围 = "范围12" },
                                }, 
                                返回值元数据 = new M元数据 { 类型 = "int", 结构 = E数据结构.点, 描述 = "点结构", 默认值 = "默认值11", 范围="范围11" }      
                            },
                            new M方法
                            {
                                名称 = "方法2", 
                                同步 = true, 
                                参数列表 = new List<M元数据>
                                {
                                    new M元数据{ 名称 = "参数0", 类型 = "int", 结构 = E数据结构.点, 描述 = "点结构", 默认值 = "100", 范围 ="范围0" },
                                    获取表元数据("参数1"),
                                }, 
                                返回值元数据 = 获取表元数据("返回值"),
                            },
                            new M方法
                            {
                                名称 = "方法3", 
                                同步 = true, 
                                参数列表 = new List<M元数据>
                                {
                                    new M元数据{ 名称 = "参数0", 类型 = "int", 结构 = E数据结构.点, 描述 = "点结构", 默认值 = "100", 范围 ="范围0" },
                                }, 
                                返回值元数据 = 获取行元数据("返回值"),
                            },
                            new M方法
                            {
                                名称 = "方法4", 
                                同步 = true, 
                                参数列表 = new List<M元数据>
                                {
                                    new M元数据{ 名称 = "参数0", 类型 = "int", 结构 = E数据结构.点, 描述 = "点结构", 默认值 = "100", 范围 ="范围0" },
                                }, 
                                返回值元数据 = new M元数据{ 类型 = "int[]", 结构 = E数据结构.列, 描述 = "列结构", 默认值 = "0",范围 = "范围2", },
                            },
                            new M方法
                            {
                                名称 = "方法5", 
                                同步 = true, 
                                参数列表 = new List<M元数据>
                                {
                                    new M元数据{ 名称 = "参数0", 类型 = "int", 结构 = E数据结构.点, 描述 = "点结构", 默认值 = "100", 范围 ="范围0" },
                                }, 
                            },
                            new M方法
                            {
                                名称 = "方法6", 
                                同步 = false, 
                                参数列表 = new List<M元数据>
                                {
                                    new M元数据{ 名称 = "参数0", 类型 = "int", 结构 = E数据结构.点, 描述 = "点结构", 默认值 = "100", 范围 ="范围0" },
                                }, 
                            },
                        },
                    };
                case "业务参数007":
                    throw new M预计异常("查询对象明细, 对象名称未配置: " + 对象名称);
                case "对象008":
                    throw new M预计异常("查询对象明细, 对象名称未配置: " + 对象名称);
                default:
                    throw new M预计异常("查询对象明细, 对象名称未配置: " + 对象名称);
            }
        }

        public void 异步执行(string 对象名, string 方法名, List<M方法参数> 参数列表)
        {
            Debug.WriteLine("异步执行 {0}.{1}({2})参数列表:", 对象名, 方法名, HJSON.序列化(参数列表));
        }

        public void 同步执行(string 对象名, string 方法名, List<M方法参数> 参数列表)
        {
            Debug.WriteLine("同步执行 {0}.{1}({2})参数列表:", 对象名, 方法名, HJSON.序列化(参数列表));
        }

        public string 同步执行带返回值(string 对象名, string 方法名, List<M方法参数> 参数列表)
        {
            Debug.WriteLine("同步执行带返回值 {0}.{1}({2})参数列表:", 对象名, 方法名, HJSON.序列化(参数列表));
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
                return HJSON.序列化(new List<int>{1,2,3});
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

    }
}
