using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using INET;
using INET.会话;
using INET.传输;
using 通用访问.DTO;

namespace 通用访问.服务端
{
    class BT服务端 : IT服务端
    {
        private INTCP服务器 _IN网络节点;

        private IN上下文 _IN上下文;

        private Dictionary<string, Func<M对象>> _所有对象 = new Dictionary<string, Func<M对象>>();

        private ConcurrentDictionary<string, List<IPEndPoint>> _所有事件订阅 = new ConcurrentDictionary<string, List<IPEndPoint>>();

        private string _事件标识结构 = "{0}-{1}";

        public int 端口 { get; set; }

        public int WebApi端口 { get; set; }

        public List<IPEndPoint> 客户端列表 { get; set; }

        private BWebApi _BWebApi;

        public void 添加对象(string 对象名称, Func<M对象> 获取对象)
        {
            _所有对象[对象名称] = 获取对象;
        }

        public void 删除对象(string __对象名称)
        {
            _所有对象.Remove(__对象名称);
        }

        public void 开启()
        {
            var __编解码器 = new B编解码器(H报文注册.报文字典, null, FT通用访问工厂.文本编码)
            {
                编码拦截 = H报文注册.拦截发送报文,
                解码拦截 = H报文注册.拦截接收报文
            };
            客户端列表 = new List<IPEndPoint>();
            _IN网络节点 = FN网络传输工厂.创建TCP服务器(new IPEndPoint(IPAddress.Any, 端口), __编解码器.消息头长度, __编解码器.解码消息长度);
            _IN网络节点.名称 = "服务器";
            _IN上下文 = new N上下文(__编解码器, _IN网络节点.名称);
            _IN网络节点.收到消息 += (__远端地址, __消息) => _IN上下文.收到报文(__远端地址, __消息);
            _IN网络节点.客户端已连接 += q =>
            {
                if (!客户端列表.Contains(q))
                {
                    客户端列表.Add(q);
                }
                On客户端已连接(q);
            };
            _IN网络节点.客户端已断开 += q =>
            {
                if (客户端列表.Contains(q))
                {
                    客户端列表.Remove(q);
                }
                On客户端已断开(q);
                _IN上下文.注销节点(q);
            };

            _IN上下文.设置发送方法(_IN网络节点.同步发送);
            _IN上下文.订阅报文(H报文注册.查询功能码(typeof(M对象列表查询请求)), () => new N被动会话(new Func<N会话参数, bool>(处理查询对象列表)));
            _IN上下文.订阅报文(H报文注册.查询功能码(typeof(M对象明细查询请求)), () => new N被动会话(new Func<N会话参数, bool>(处理查询对象明细)));
            _IN上下文.订阅报文(H报文注册.查询功能码(typeof(M方法执行请求)), () => new N被动会话(new Func<N会话参数, bool>((处理执行方法))));
            _IN上下文.订阅报文(H报文注册.查询功能码(typeof(M订阅事件)), () => new N被动会话(new Action<N会话参数>((处理订阅事件))));
            _IN上下文.订阅报文(H报文注册.查询功能码(typeof(M注销事件)), () => new N被动会话(new Action<N会话参数>((处理取消订阅事件))));
            _IN上下文.订阅报文(H报文注册.查询功能码(typeof(M属性值查询请求)), () => new N被动会话(new Func<N会话参数, bool>((处理查询属性值))));

            _IN网络节点.最大消息长度 = 10000000;
            _IN网络节点.开启();

            try
            {
                if (WebApi端口 == 0)
                {
                    WebApi端口 = 端口 + 1;
                }
                _BWebApi = new BWebApi(WebApi端口, () => _所有对象);
                _BWebApi.开启();
            }
            catch (Exception ex)
            {
                H日志输出.记录("通用访问WebApi开启失败: " + ex.Message);
            }
        }

        public void 关闭()
        {
            if (_IN网络节点 != null)
            {
                _IN网络节点.断开所有客户端();
                _IN网络节点.关闭();
            }
            if (_BWebApi != null)
            {
                _BWebApi.关闭();
            }
        }

        private bool 处理查询对象列表(N会话参数 __会话参数)
        {
            var __对象列表 = new M对象列表查询结果();
            __对象列表.AddRange(_所有对象.Values.ToList().Select(q => q().概要));
            __会话参数.发送响应(__对象列表);
            return true;
        }

        private bool 处理查询对象明细(N会话参数 __会话参数)
        {
            var __对象名称 = (__会话参数.负载 as M对象明细查询请求).对象名称;
            if (_所有对象.ContainsKey(__对象名称))
            {
                var __对象 = _所有对象[__对象名称]();
                __会话参数.发送响应(__对象.明细);
            }
            else
            {
                H日志输出.记录("无此对象: " + __对象名称);
            }
            return true;
        }

        private bool 处理查询属性值(N会话参数 __会话参数)
        {
            var __请求 = __会话参数.负载 as M属性值查询请求;
            var __对象名称 = __请求.对象名称;
            var __属性名称 = __请求.属性名称;
            if (_所有对象.ContainsKey(__对象名称))
            {
                var __对象 = _所有对象[__对象名称]();
                var __执行成功 = true;
                var __执行描述 = "";
                var __返回值 = "";
                try
                {
                    __返回值 = __对象.计算属性(__属性名称);
                }
                catch (Exception ex)
                {
                    H日志输出.记录(ex);
                    __执行描述 = ex.Message;
                    __执行成功 = false;
                }
                var 响应 = new M属性值查询结果 { 成功 = __执行成功, 描述 = __执行描述, 返回值 = __返回值 };
                __会话参数.发送响应(响应);
            }
            return true;
        }

        private bool 处理执行方法(N会话参数 __会话参数)
        {
            var __请求 = __会话参数.负载 as M方法执行请求;
            var __对象名称 = __请求.对象名称;
            var __方法名称 = __请求.方法名称;
            var __参数 = __请求.实参列表;
            if (_所有对象.ContainsKey(__对象名称))
            {
                var __对象 = _所有对象[__对象名称]();
                var __方法 = __对象.明细.方法列表.Find(q => q.名称 == __方法名称);
                if (__方法 != null)
                {
                    var __执行成功 = true;
                    var __执行描述 = "";
                    var __返回值 = "";
                    try
                    {
                        __返回值 = __对象.执行方法(__方法名称, M实参.列表转字典(__参数), __会话参数.远端);
                    }
                    catch (Exception ex)
                    {
                        H日志输出.记录(ex);
                        __执行描述 = ex.Message;
                        __执行成功 = false;
                    }
                    var 响应 = new M方法执行结果 { 成功 = __执行成功, 描述 = __执行描述, 返回值 = __返回值 };
                    __会话参数.发送响应(响应);
                }
            }
            return true;
        }

        public event Action<IPEndPoint> 客户端已连接;

        protected virtual void On客户端已连接(IPEndPoint obj)
        {
            var handler = 客户端已连接;
            if (handler != null) handler(obj);
        }

        public event Action<IPEndPoint> 客户端已断开;

        protected virtual void On客户端已断开(IPEndPoint obj)
        {
            var handler = 客户端已断开;
            if (handler != null) handler(obj);
        }

        private void 处理订阅事件(N会话参数 __会话参数)
        {
            var __订阅事件 = __会话参数.负载 as M订阅事件;
            var __对象名称 = __订阅事件.对象名称;
            var __事件名称 = __订阅事件.事件名称;
            var __远端 = __会话参数.远端;
            var __事件标识 = string.Format(_事件标识结构, __对象名称, __事件名称);

            lock (_lockobj)
            {
                if (!_所有事件订阅.ContainsKey(__事件标识))
                {
                    _所有事件订阅[__事件标识] = new List<IPEndPoint>();
                }
                if (!_所有事件订阅[__事件标识].Contains(__远端))
                {
                    _所有事件订阅[__事件标识].Add(__远端);
                    H日志输出.记录("接收[M订阅事件]", string.Format("[{0}] {1}.{2}", __远端, __对象名称, __事件名称), TraceEventType.Information);
                }
            }
            //下列代码有同步问题
            //var __订阅地址列表 = _所有事件订阅.GetOrAdd(__事件标识, q => new List<IPEndPoint>());
            //if (!__订阅地址列表.Contains(__远端))
            //{
            //    __订阅地址列表.Add(__远端);
            //    H日志输出.记录("接收[M订阅事件]", string.Format("[{0}] {1}.{2}", __远端, __对象名称, __事件名称), TraceEventType.Information);
            //}
        }

        private object _lockobj = new object();

        private void 处理取消订阅事件(N会话参数 __会话参数)
        {
            var __取消订阅 = __会话参数.负载 as M注销事件;
            var __对象名称 = __取消订阅.对象名称;
            var __事件名称 = __取消订阅.事件名称;
            var __远端 = __会话参数.远端;
            var __事件标识 = string.Format(_事件标识结构, __对象名称, __事件名称);

            List<IPEndPoint> __订阅地址列表;
            if (_所有事件订阅.TryGetValue(__事件标识, out __订阅地址列表))
            {
                if (__订阅地址列表.Contains(__远端))
                {
                    __订阅地址列表.Remove(__远端);
                }
            }
        }

        public void 触发事件(string __对象名称, string __事件名称, Dictionary<string, string> __参数列表 = null, List<IPEndPoint> __地址列表 = null)
        {
            var __事件标识 = string.Format(_事件标识结构, __对象名称, __事件名称);

            List<IPEndPoint> __订阅地址列表;
            if (!_所有事件订阅.TryGetValue(__事件标识, out __订阅地址列表))
            {
                return;
            }
            if (__地址列表 == null)
            {
                __地址列表 = new List<IPEndPoint>(__订阅地址列表);
            }
            __地址列表.ForEach(__远端 =>
            {
                if (客户端列表.Contains(__远端))
                {
                    _IN上下文.发送通知(__远端, new M接收事件
                    {
                        对象名称 = __对象名称,
                        实参列表 = M实参.字典转列表(__参数列表),
                        事件名称 = __事件名称,
                    });
                    H日志输出.记录("触发[M订阅事件]", string.Format("{3}-{4}[{0}] {1}.{2}", __远端, __对象名称, __事件名称, 客户端列表.Count, __地址列表.Count), TraceEventType.Information);
                }
                else
                {
                    _所有事件订阅[__事件标识].Remove(__远端);
                }
            });
        }

    }
}
