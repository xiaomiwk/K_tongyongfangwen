using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace INET.会话
{
    public class H网络队列
    {
        private Action<object> _处理数据;

        private bool _每节点一线程;

        private ConcurrentDictionary<string, M每通道线程> _通道字典 = new ConcurrentDictionary<string, M每通道线程>();

        private ConcurrentDictionary<IPEndPoint, M每节点线程> _节点字典 = new ConcurrentDictionary<IPEndPoint, M每节点线程>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="__处理数据"></param>
        /// <param name="__每节点一线程">线程模型: true表示每个节点一个线程, false表示每个节点的每个通道一个线程</param>
        public H网络队列(Action<object> __处理数据, bool __每节点一线程 = true)
        {
            _处理数据 = __处理数据;
            _每节点一线程 = __每节点一线程;
        }

        public void 添加事项(IPEndPoint __节点, string __通道, object __数据)
        {
            if (__通道 == null)
            {
                __通道 = string.Empty;
            }
            if (_每节点一线程)
            {
                _节点字典.GetOrAdd(__节点, k => new M每节点线程(__节点, _处理数据)).添加事项(__通道, new M事项(__数据));
            }
            else
            {
                var __线程标识 = string.Format("{0}-{1}", __节点, __通道);
                _通道字典.GetOrAdd(__线程标识, k => new M每通道线程(__节点, __通道, _处理数据)).添加事项(new M事项(__数据));
            }
        }

        public void 关闭线程(IPEndPoint __节点)
        {
            M每节点线程 __线程;
            if (_节点字典.TryGetValue(__节点, out __线程))
            {
                __线程.关闭();
                _节点字典.TryRemove(__节点, out __线程);
            }
        }

        public void 关闭线程(string __通道)
        {
            M每通道线程 __线程;
            if (_通道字典.TryGetValue(__通道, out __线程))
            {
                __线程.关闭();
                _通道字典.TryRemove(__通道, out __线程);
            }
        }

        public void 关闭所有()
        {
            _通道字典.Keys.ToList().ForEach(关闭线程);
            _节点字典.Keys.ToList().ForEach(关闭线程);
        }

        class M每通道线程
        {
            private bool _关闭;

            private ConcurrentQueue<M事项> _队列 = new ConcurrentQueue<M事项>();

            private IPEndPoint _节点;

            private string _通道;

            public M每通道线程(IPEndPoint __节点, string __通道, Action<object> __处理数据)
            {
                _节点 = __节点;
                _通道 = __通道;
                new System.Threading.Thread(() =>
                {
                    while (!_关闭)
                    {
                        M事项 __事项;
                        if (_队列.TryDequeue(out __事项))
                        {
                            var __延迟 = Environment.TickCount - __事项.接收时间;
                            if (__延迟 > 500)
                            {
                                H日志输出.记录(string.Format("处理 [{0}][{1}] 的 {2} 延迟 {3} 毫秒;", _节点, _通道, __事项.数据, __延迟));
                            }
                            __处理数据(__事项.数据);
                            System.Threading.Thread.Sleep(0);
                        }
                        else
                        {
                            System.Threading.Thread.Sleep(100);
                        }
                    }
                }) { IsBackground = true }.Start();
            }

            public void 添加事项(M事项 __数据)
            {
                _队列.Enqueue(__数据);
            }

            public void 关闭()
            {
                _关闭 = true;
            }
        }

        class M每节点线程
        {
            private bool _关闭;

            private ConcurrentDictionary<string, ConcurrentQueue<M事项>> _队列字典 = new ConcurrentDictionary<string, ConcurrentQueue<M事项>>();

            private IPEndPoint _节点;

            public M每节点线程(IPEndPoint __节点, Action<object> __处理数据)
            {
                _节点 = __节点;
                new System.Threading.Thread(() =>
                {
                    while (!_关闭)
                    {
                        var __所有通道 = _队列字典.Keys.ToList();
                        __所有通道.ForEach(__通道 =>
                        {
                            ConcurrentQueue<M事项> __队列;
                            if (!_队列字典.TryGetValue(__通道, out __队列))
                            {
                                return;
                            }
                            M事项 __事项;
                            if (__队列.TryDequeue(out __事项))
                            {
                                var __延迟 = Environment.TickCount - __事项.接收时间;
                                if (__延迟 > 500)
                                {
                                    H日志输出.记录(string.Format("处理 [{0}][{1}] 的 {2} 延迟 {3} 毫秒;", _节点, __通道, __事项.数据, __延迟));
                                }
                                __处理数据(__事项.数据);
                            }
                        });
                        System.Threading.Thread.Sleep(10);
                    }
                }){ IsBackground = true }.Start();
            }

            public void 添加事项(string __通道, M事项 __数据)
            {
                var __队列 = _队列字典.GetOrAdd(__通道, k => new ConcurrentQueue<M事项>());
                __队列.Enqueue(__数据);
            }

            public void 关闭()
            {
                _关闭 = true;
            }

        }

        private class M事项
        {
            public int 接收时间 { get; private set; }

            public object 数据 { get; private set; }

            public M事项(object __数据)
            {
                接收时间 = Environment.TickCount;
                数据 = __数据;
            }
        }
    }

}
