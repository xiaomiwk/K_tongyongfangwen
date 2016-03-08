## 通用访问
通过类似“反射”的方式开发跨平台的分布式应用！包括协议、.NET SDK、通用工具、demo

**特点**

1. 通用工具提供了面向工程人员、开发人员的通用配置界面，可以非常优雅的剔除混在面向客户的软件中的面向工程人员和开发人员的元素

2. 通用工具展示了可视化的可执行的API文档

3. 通用工具提供了远程调试手段，能看能摸正在运行的程序；在IDE和日志以外提供了一种强大的调试诊断手段

4. 基于.NET SDK开发分布式程序，非常方便

**服务器端示例**
![image](https://github.com/xiaomiwk/K_tongyongfangwen/blob/master/1.png?raw=true)
`

            IT服务端 __IT服务端 = FT通用访问工厂.创建服务端();
            __IT服务端.端口 = 8888;
            __IT服务端.开启();
            
            var _对象 = new M对象(_对象名称, "通用服务");
            _对象.添加属性("概要状态", () => HJSON.序列化(查询业务概要(null, null)), E角色.客户, null);
            _对象.添加属性("启动时间", () => 启动时间.ToString(), E角色.客户, null);
            _对象.添加方法("查询未清除告警", __实参列表 =>
            {
                var __条件 = HJSON.反序列化<M查询条件>(__实参列表["条件"]);
                return HJSON.序列化(查询未清除告警(__条件));
            }, E角色.客户, new List<M形参> { new M形参{ 名称 = "条件", 元数据 = new M元数据
            { 
                //每页数量, 页数, 来源设备类型(可选), 来源设备标识(可选), 类别(可选), 重要性(可选), 关键字(可选)
                结构 = E数据结构.对象, 子成员列表 = new List<M子成员>
                {
                    new M子成员("每页数量", "int"),
                    new M子成员("页数", "int"),
                    new M子成员("来源设备类型", new M元数据{  类型 = "string", 描述 = "可选"}),
                    new M子成员("来源设备标识", new M元数据{  类型 = "string", 描述 = "可选"}),
                    new M子成员("类别", new M元数据{  类型 = "string", 描述 = "可选"}),
                    new M子成员("重要性", new M元数据{  类型 = "string", 描述 = "可选"}),
                    new M子成员("关键字", new M元数据{  类型 = "string", 描述 = "可选"}),
                }
            } } }, null);
            _对象.添加事件("上报告警", E角色.客户, new List<M形参>
            {
                new M形参{ 名称 = "事件参数", 元数据 = new M元数据
                { 
                    //标识, 来源设备类型, 来源设备标识, 产生时间, 类别, 重要性, 描述, 原因, 解决方案
                    结构 = E数据结构.对象, 子成员列表 = new List<M子成员>
                    {
                        new M子成员("标识", "string"),
                        new M子成员("来源设备类型", "string"),
                        new M子成员("来源设备标识","string"),
                        new M子成员("产生时间", "string"),
                        new M子成员("类别", "string"),
                        new M子成员("重要性", "string"),
                        new M子成员("描述","string"),
                        new M子成员("原因", "string"),
                        new M子成员("解决方案", "string"),
                    }
                }
                }
            });
            _IT服务端.添加对象(_对象名称, () => _对象);
        }

`

**通用工具**
![image](https://github.com/xiaomiwk/K_tongyongfangwen/blob/master/2.png?raw=true)
`

            _IT客户端 = FT通用访问工厂.创建客户端();
            _IT客户端.连接(_地址);
                
                
            var __概要状态 = HJSON.反序列化<M概要状态>(_IT客户端.执行方法(_对象名称, "查询概要状态", null));

            var __启动时间 = DateTime.Parse(_IT客户端.查询属性值(_对象名称, "启动时间"));

            var __返回值 = _IT客户端.执行方法(_对象名称, "查询未清除告警", new Dictionary<string,string>{{"条件",查询条件 == null ? "" : HJSON.序列化(查询条件)}});
            var __未清除告警 = HJSON.反序列化<List<M上报告警>>(__返回值);


        private Action<M上报告警> _新增了告警;

        public event Action<M上报告警> 新增了告警
        {
            add
            {
                if (_新增了告警 == null)
                {
                    _IT客户端.订阅事件(_对象名称, "上报告警", 处理告警);
                }
                _新增了告警 += value;
            }
            remove
            {
                if (_新增了告警 != null)
                {
                    _新增了告警 -= value;
                    if (_新增了告警 == null)
                    {
                        _IT客户端.注销事件(_对象名称, "上报告警", 处理告警);
                    }
                }
            }
        }

        private void 处理告警(Dictionary<string,string> __实参列表)
        {
            var __上报告警 = HJSON.反序列化<M上报告警>(__实参列表["事件参数"]);
            On新增了告警(__上报告警);
        }

        protected virtual void On新增了告警(M上报告警 obj)
        {
            var handler = _新增了告警;
            if (handler != null) handler(obj);
        }

  }

`
