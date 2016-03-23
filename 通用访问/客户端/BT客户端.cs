using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using INET;
using INET.会话;
using INET.传输;
using INET.模板;
using 通用访问.DTO;

namespace 通用访问.客户端
{
    public class BT客户端 : IT客户端
    {
        private INTCP客户端 _IN网络节点;

        private IN上下文 _IN上下文;

        private N主动会话 _N主动会话;

        private string _事件标识结构 = "{0}:{1}";

        private H多事件<string> _事件订阅 = new H多事件<string>();

        public BT客户端()
        {
            this.已连接 += 重新订阅;
        }

        void 重新订阅()
        {
            var __所有订阅 = _事件订阅.查询所有订阅();
            foreach (var __订阅 in __所有订阅)
            {
                _N主动会话.通知(new M订阅事件 { 对象名称 = __订阅.Key.Split(':')[0], 事件名称 = __订阅.Key.Split(':')[1] });
            }
        }

        public IPEndPoint 设备地址 { get; private set; }

        public void 连接(IPEndPoint __设备地址)
        {
            设备地址 = __设备地址;
            var __编解码器 = new B编解码器(H报文注册.报文字典, null, FT通用访问工厂.文本编码)
            {
                编码拦截 = H报文注册.拦截发送报文,
                解码拦截 = H报文注册.拦截接收报文
            };
            _IN网络节点 = FN网络传输工厂.创建TCP客户端(__设备地址, new IPEndPoint(IPAddress.Any, 0), __编解码器.消息头长度, __编解码器.解码消息长度);
            _IN网络节点.名称 = "客户端";
            _IN网络节点.自动重连 = this.自动重连;
            _IN上下文 = new N上下文(__编解码器, _IN网络节点.名称);
            _N主动会话 = new N主动会话(_IN上下文, __设备地址);
            _IN网络节点.收到消息 += (__远端地址, __消息) => _IN上下文.收到报文(__远端地址, __消息);
            _IN网络节点.已断开 += () =>
            {
                if (连接正常)
                {
                    连接正常 = false;
                    On已断开(false);
                }
                _IN上下文.注销节点(__设备地址);
            };
            _IN网络节点.已连接 += () =>
            {
                连接正常 = true;

                Task.Factory.StartNew(On已连接);
            };
            _IN上下文.设置发送方法((__节点, __消息) => _IN网络节点.同步发送(__消息));
            _IN上下文.订阅报文(H报文注册.查询功能码(typeof(M接收事件)), () => new N被动会话(new Action<N会话参数>(处理事件)));

            _IN网络节点.最大消息长度 = 10000000;
            _IN网络节点.开启();
        }

        public void 断开()
        {
            自动重连 = false;
            _IN网络节点.自动重连 = this.自动重连;
            连接正常 = false;
            _IN网络节点.断开();
            On已断开(true);
        }

        public bool 连接正常 { get; set; }

        public event Action<bool> 已断开;

        protected virtual void On已断开(bool __主动)
        {
            var handler = 已断开;
            if (handler != null) handler(__主动);
        }

        public bool 自动重连 { get; set; }

        public event Action 已连接;

        protected virtual void On已连接()
        {
            Action handler = 已连接;
            if (handler != null) handler();
        }

        public M对象列表查询结果 查询可访问对象()
        {
            var __对象列表 = _N主动会话.请求<M对象列表查询结果>(new M对象列表查询请求());
            if (__对象列表 == null)
            {
                throw new ApplicationException("查询对象列表未响应");
            }
            return __对象列表;
        }

        public M对象明细查询结果 查询对象明细(string 对象名称)
        {
            var __对象明细 = _N主动会话.请求<M对象明细查询结果>(new M对象明细查询请求 { 对象名称 = 对象名称 });
            if (__对象明细 == null)
            {
                throw new ApplicationException("查询对象明细未响应:" + 对象名称);
            }
            return __对象明细;
        }

        public string 查询属性值(string 对象名, string 属性名, int 超时毫秒 = 3000)
        {
            var __请求报文 = new M属性值查询请求
            {
                对象名称 = 对象名,
                属性名称 = 属性名,
            };
            var __响应报文 = _N主动会话.请求<M属性值查询结果>(__请求报文, 超时毫秒);
            if (__响应报文 == null)
            {
                throw new ApplicationException(string.Format("查询属性值: {0} - {1} 时无响应", 对象名, 属性名));
            }
            if (!__响应报文.成功)
            {
                throw new ApplicationException(string.Format("查询属性值: {0} - {1} 时出错, {2}", 对象名, 属性名, __响应报文.描述));
            }
            return __响应报文.返回值;
        }

        public string 执行方法(string 对象名, string 方法名, Dictionary<string, string> 参数列表, int 超时毫秒 = 10000)
        {
            var __实参列表 = M实参.字典转列表(参数列表);
            return 执行方法(对象名, 方法名, __实参列表, 超时毫秒);
        }


        string 执行方法(string 对象名, string 方法名, List<M实参> 参数列表, int 超时毫秒 = 10000)
        {
            var __请求报文 = new M方法执行请求
            {
                对象名称 = 对象名,
                方法名称 = 方法名,
                实参列表 = 参数列表
            };
            var __响应报文 = _N主动会话.请求<M方法执行结果>(__请求报文, 超时毫秒);
            if (__响应报文 == null)
            {
                throw new ApplicationException(string.Format("执行方法: {0} - {1} 时无响应", 对象名, 方法名));
            }
            if (!__响应报文.成功)
            {
                throw new ApplicationException(string.Format("执行方法: {0} - {1} 时出错, {2}", 对象名, 方法名, __响应报文.描述));
            }
            return __响应报文.返回值;
        }

        public void 订阅事件(string 对象名, string 事件名, Action<Dictionary<string, string>> 处理方法)
        {
            var __键 = string.Format(_事件标识结构, 对象名, 事件名);
            if (_事件订阅.查询订阅者数量(__键) == 0)
            {
                H日志输出.记录("发送[M订阅事件]", string.Format("{0}.{1}",  对象名, 事件名), TraceEventType.Information);
                _N主动会话.通知(new M订阅事件 { 对象名称 = 对象名, 事件名称 = 事件名 });
            }
            _事件订阅.注册(string.Format(_事件标识结构, 对象名, 事件名), 处理方法);
        }

        public void 注销事件(string 对象名, string 事件名, Action<Dictionary<string, string>> 处理方法)
        {
            var __键 = string.Format(_事件标识结构, 对象名, 事件名);
            _事件订阅.注销(__键, 处理方法);
            if (_事件订阅.查询订阅者数量(__键) == 0)
            {
                _N主动会话.通知(new M注销事件 { 对象名称 = 对象名, 事件名称 = 事件名 });
            }
        }

        public void 处理事件(N会话参数 __会话参数)
        {
            var __事件 = __会话参数.负载 as M接收事件;
            var __键 = string.Format(_事件标识结构, __事件.对象名称, __事件.事件名称);
            if (_事件订阅.查询订阅者数量(__键) == 0)
            {
                H日志输出.记录("注销[M订阅事件]", string.Format("[{0}] {1}.{2}", __会话参数.远端, __事件.对象名称, __事件.事件名称), TraceEventType.Information);
                _N主动会话.通知(new M注销事件 { 对象名称 = __事件.对象名称, 事件名称 = __事件.事件名称 });
                return;
            }
            H日志输出.记录("触发[M订阅事件]", string.Format("[{0}] {1}.{2}", __会话参数.远端, __事件.对象名称, __事件.事件名称), TraceEventType.Information);
            _事件订阅.触发(__键, M实参.列表转字典(__事件.实参列表));
            On收到了事件(__事件);
        }

        public event Action<M接收事件> 收到了事件;

        protected virtual void On收到了事件(M接收事件 obj)
        {
            var handler = 收到了事件;
            if (handler != null) handler(obj);
        }

    }
}
