# K_TONGYONGFANGWEN
以“对象”、“属性”、“方法”、“事件”为元素，适用于跨平台的轻量级分布式通信协议，以及.NET SDK、通用工具、demo

**特点**

1. 通用工具提供了面向工程人员、开发人员的通用配置界面，可以非常优雅的剔除混在面向客户的软件中的面向工程人员和开发人员的元素

2. 通用工具展示了可视化的可执行的API文档

3. 通用工具提供了远程调试手段，能看能摸正在运行的程序；在IDE和日志以外提供了一种强大的调试诊断手段

4. 基于.NET SDK开发分布式程序，非常方便

**服务器端示例**

`

            IT服务端 __IT服务端 = FT通用访问工厂.创建服务端();
            __IT服务端.端口 = 8888;
            __IT服务端.开启();
            

    public class B状态_S : IB状态_S
    {
        private IT服务端 _IT服务端 = H容器.取出<IT服务端>();

        private string _对象名称 = "状态";

        private Dictionary<string, Func<M业务概要>> _属性缓存 = new Dictionary<string, Func<M业务概要>>();

        private string _连接符 = "++";

        private List<M上报告警> _待处理问题 = new List<M上报告警>();

        private Func<E健康状态> _设置健康状态 = null;

        public B状态_S()
        {
            配置通用访问();
        }

        private void 配置通用访问()
        {
            var _对象 = new M对象(_对象名称, "通用服务");
            _对象.添加属性("概要状态", () => HJSON.序列化(查询业务概要(null, null)), E角色.客户, null);
            _对象.添加属性("启动时间", () => 启动时间.ToString(), E角色.客户, null);
            _对象.添加属性("健康状态", () => 健康状态.ToString(), E角色.客户, null);
            _对象.添加属性("状态开始时间", () => 状态开始时间.ToString(), E角色.客户, null);
            _对象.添加属性("未清除告警数量", () => _待处理问题.Count.ToString(), E角色.客户, null);
            _对象.添加方法("查询基本信息", __实参列表 => HJSON.序列化(查询概要状态()), E角色.客户);
            _对象.添加方法("查询概要状态", __实参列表 =>
            {
                return HJSON.序列化(查询概要状态());
            }, E角色.客户);
            _对象.添加方法("查询业务概要", __实参列表 =>
            {
                var __类别 = __实参列表["类别"];
                var __属性 = __实参列表["属性"];
                return HJSON.序列化(查询业务概要(__类别, __属性));
            }, E角色.客户, new List<M形参> { new M形参("类别", new M元数据{  类型 = "string", 描述 = "可为空"}),new M形参("属性", new M元数据{  类型 = "string", 描述 = "可为空"})
           }, null);
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
            _对象.添加事件("上报清除", E角色.客户, new List<M形参>
            {
                new M形参{ 名称 = "事件参数", 元数据 = new M元数据
                { 
                    //标识, 来源设备类型, 来源设备标识
                    结构 = E数据结构.对象, 子成员列表 = new List<M子成员>
                    {
                        new M子成员("标识", "string"),
                        new M子成员("来源设备类型", "string"),
                        new M子成员("来源设备标识","string"),
                    }
                }
                }
            });
            _对象.添加事件("健康状态变化", E角色.客户, new List<M形参>
            {
                new M形参{ 名称 = "事件参数", 元数据 = new M元数据
                { 
                    //标识, 来源设备类型, 来源设备标识
                    结构 = E数据结构.对象, 子成员列表 = new List<M子成员>
                    {
                        new M子成员("旧状态", "int"),
                        new M子成员("新状态", "int"),
                    }
                }
                }
            });

            _IT服务端.添加对象(_对象名称, () => _对象);
        }

        public void 设置属性(string 类别, string 属性, Func<M业务概要> 获取属性)
        {
            _属性缓存[类别 + "++" + 属性] = 获取属性;
        }

        public void 删除属性(string 类别, string 属性)
        {
            if (_属性缓存.ContainsKey(类别 + _连接符 + 属性))
            {
                _属性缓存.Remove(类别 + "++" + 属性);
            }
        }

        public M概要状态 查询概要状态()
        {
            return new M概要状态() { 未清除告警数量 = this._待处理问题.Count, 健康状态 = this.健康状态, 启动时间 = this.启动时间, 状态开始时间 = this.状态开始时间 };
        }

        public List<M业务概要> 查询业务概要(string 类别, string 属性)
        {
            if (string.IsNullOrEmpty(类别) && string.IsNullOrEmpty(属性))
            {
                var __结果 = _属性缓存.Values.ToList().Select(q => q()).ToList();
                __结果.Sort((m, n) =>
                {
                    var __类别比较 = m.类别.CompareTo(n.类别);
                    if (__类别比较 != 0)
                    {
                        return __类别比较;
                    }
                    return m.属性.CompareTo(n.属性);
                });
                return __结果;
            }

            if (!string.IsNullOrEmpty(类别) && string.IsNullOrEmpty(属性))
            {
                var __结果 = new List<M业务概要>();
                foreach (var __kv in _属性缓存)
                {
                    if (__kv.Key.StartsWith(类别 + _连接符))
                    {
                        __结果.Add(__kv.Value());
                    }
                }
                return __结果;
            }

            if (string.IsNullOrEmpty(类别) && !string.IsNullOrEmpty(属性))
            {
                var __结果 = new List<M业务概要>();
                foreach (var __kv in _属性缓存)
                {
                    if (__kv.Key.EndsWith(_连接符 + 属性))
                    {
                        __结果.Add(__kv.Value());
                    }
                }
                return __结果;
            }

            if (!string.IsNullOrEmpty(类别) && !string.IsNullOrEmpty(属性))
            {
                if (_属性缓存.ContainsKey(类别 + _连接符 + 属性))
                {
                    return new List<M业务概要> { _属性缓存[类别 + _连接符 + 属性]() };
                }
            }

            throw new ApplicationException();
        }

        public int 未清除告警数量 { get; set; }

        public DateTime 启动时间 { get; private set; }

        public DateTime 状态开始时间 { get; private set; }

        public E健康状态 健康状态 { get; private set; }

        public void 初始化(List<M上报告警> __未清除告警, DateTime __启动时间, Func<E健康状态> __设置健康状态 = null)
        {
            启动时间 = __启动时间;
            _设置健康状态 = __设置健康状态;
            this.健康状态 = E健康状态.优;
            this.状态开始时间 = DateTime.Now;
            if (__未清除告警 != null && __未清除告警.Count > 0)
            {
                _待处理问题.AddRange(__未清除告警);
                计算健康状态(true);
            }
            else
            {
                计算健康状态(false);
            }
        }

        public void 新增告警(M上报告警 告警)
        {
            if (告警.重要性 != E重要性.一般)
            {
                _待处理问题.Add(告警);
                计算健康状态(true);
            }
            _IT服务端.触发事件(_对象名称, "上报告警", new Dictionary<string,string>{{"事件参数",HJSON.序列化(告警)}});
        }

        public void 清除告警(M上报清除 清除)
        {
            var __数量 = _待处理问题.RemoveAll(q => q.标识 == 清除.标识 && q.来源设备标识 == 清除.来源设备标识 && q.来源设备类型 == 清除.来源设备类型);
            if (__数量 > 0)
            {
                计算健康状态(true);
                _IT服务端.触发事件(_对象名称, "上报清除", new Dictionary<string, string> { { "事件参数", HJSON.序列化(清除) } });
            }
        }

        public List<M上报告警> 查询未清除告警(M查询条件 查询条件)
        {
            return _待处理问题.ToList();
        }

        public event Action<M概要状态> 健康状态变化;

        private void 计算健康状态(bool _待处理问题数变化)
        {
            var __新状态 = E健康状态.优;
            if (_设置健康状态 == null)
            {
                if (_待处理问题.Exists(q => q.重要性 == E重要性.紧急))
                {
                    __新状态 = E健康状态.差;
                }
                else if (_待处理问题.Exists(q => q.重要性 == E重要性.重要))
                {
                    __新状态 = E健康状态.中;
                }
                else if (_待处理问题.Exists(q => q.重要性 == E重要性.次要))
                {
                    __新状态 = E健康状态.良;
                }
            }
            else
            {
                __新状态 = _设置健康状态();
            }
            if (__新状态 != this.健康状态)
            {
                //var __旧状态 = this.健康状态;
                this.健康状态 = __新状态;
                this.状态开始时间 = DateTime.Now;
                On健康状态变化(查询概要状态());
                return;
            }
            if (_待处理问题数变化)
            {
                On健康状态变化(查询概要状态());
            }
        }

        protected virtual void On健康状态变化(M概要状态 __新状态)
        {
            var handler = 健康状态变化;
            if (handler != null) handler(__新状态);
            _IT服务端.触发事件(_对象名称, "健康状态变化", new Dictionary<string, string> { { "事件参数", HJSON.序列化(__新状态) } });
        }
    }
}

`

**客户端示例**

`

                _IT客户端 = FT通用访问工厂.创建客户端();
                _IT客户端.连接(_地址);
                
    public class B状态_C : IB状态_C
    {
        private IT客户端 _IT客户端;

        private string _对象名称 = "状态";

        public B状态_C(IT客户端 __IT客户端)
        {
            _IT客户端 = __IT客户端;
        }

        public M概要状态 查询概要状态()
        {
            return HJSON.反序列化<M概要状态>(_IT客户端.执行方法(_对象名称, "查询概要状态", null));
        }

        public List<M业务概要> 查询业务概要(string 类别 = "", string 属性 = "")
        {
            return HJSON.反序列化<List<M业务概要>>(_IT客户端.执行方法(_对象名称, "查询业务概要", new Dictionary<string,string>{{"类别",类别},{"属性",属性}}));
        }

        public int 未清除告警数量
        {
            get
            {
                try
                {
                    return int.Parse(_IT客户端.查询属性值(_对象名称, "未清除告警数量"));
                }
                catch (Exception)
                {
                    return int.MinValue;
                }
            }
        }

        public DateTime 启动时间
        {
            get
            {
                return DateTime.Parse(_IT客户端.查询属性值(_对象名称, "启动时间"));
            }
        }

        public E健康状态 健康状态
        {
            get { return (E健康状态)Enum.Parse(typeof(E健康状态), _IT客户端.查询属性值(_对象名称, "健康状态")); }
        }

        public DateTime 状态开始时间
        {
            get
            {
                return DateTime.Parse(_IT客户端.查询属性值(_对象名称, "状态开始时间"));
            }
        }

        public List<M上报告警> 查询未清除告警(M查询条件 查询条件)
        {
            var __返回值 = _IT客户端.执行方法(_对象名称, "查询未清除告警", new Dictionary<string,string>{{"条件",查询条件 == null ? "" : HJSON.序列化(查询条件)}});
            return HJSON.反序列化<List<M上报告警>>(__返回值);
        }

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

        private Action<M上报清除> _清除了告警;

        public event Action<M上报清除> 清除了告警
        {
            add
            {
                if (_清除了告警 == null)
                {
                    _IT客户端.订阅事件(_对象名称, "上报清除", 处理清除);
                }
                _清除了告警 += value;
            }
            remove
            {
                if (_清除了告警 == null)
                {
                    return;
                }
                _清除了告警 -= value;
                if (_清除了告警 == null)
                {
                    _IT客户端.注销事件(_对象名称, "上报清除", 处理清除);
                }
            }
        }

        private void 处理清除(Dictionary<string,string> __实参列表)
        {
            var __上报告警 = HJSON.反序列化<M上报清除>(__实参列表["事件参数"]);
            On清除了告警(__上报告警);
        }

        protected virtual void On清除了告警(M上报清除 obj)
        {
            var handler = _清除了告警;
            if (handler != null) handler(obj);
        }


        private Action<M概要状态> _健康状态变化;

        public event Action<M概要状态> 健康状态变化
        {
            add
            {
                if (_健康状态变化 == null)
                {
                    _IT客户端.订阅事件(_对象名称, "健康状态变化", 处理健康状态变化);
                }
                _健康状态变化 += value;
            }
            remove
            {
                if (_健康状态变化 == null)
                {
                    return;
                }
                _健康状态变化 -= value;
                if (_健康状态变化 == null)
                {
                    _IT客户端.注销事件(_对象名称, "健康状态变化", 处理健康状态变化);
                }
            }
        }

        private void 处理健康状态变化(Dictionary<string,string> __实参列表)
        {
            var __事件参数 = HJSON.反序列化<M概要状态>(__实参列表["事件参数"]);
            On健康状态变化(__事件参数);
        }

        protected virtual void On健康状态变化(M概要状态 obj)
        {
            var handler = _健康状态变化;
            if (handler != null) handler(obj);
        }

    }
}

`
