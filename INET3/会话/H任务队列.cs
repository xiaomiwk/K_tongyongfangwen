using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace INET.会话
{
    public class H任务队列
    {
        private Action<object> _处理数据;

        private ConcurrentDictionary<string, M队列> _节点字典 = new ConcurrentDictionary<string, M队列>();

        public H任务队列(Action<object> __处理数据)
        {
            _处理数据 = __处理数据;
        }

        public void 添加事项(string __队列标识, object __数据)
        {
            _节点字典.GetOrAdd(__队列标识, k => new M队列(__队列标识, _处理数据)).添加事项(new M事项(__数据));
        }

        public void 关闭队列(string __队列标识)
        {
            M队列 __队列;
            if (_节点字典.TryGetValue(__队列标识, out __队列))
            {
                __队列.关闭();
                _节点字典.TryRemove(__队列标识, out __队列);
            }
        }

        public void 关闭所有()
        {
            _节点字典.Values.ToList().ForEach(q => q.关闭());
        }

        class M队列
        {
            private Task _任务 = Task.Factory.StartNew(() => { });

            private CancellationTokenSource _取消标志 = new CancellationTokenSource();

            private B性能监控 _监控;

            private Action<object> _处理数据;

            private string _节点;

            public M队列(string __名称, Action<object> __处理数据)
            {
                _监控 = new B性能监控();
                _节点 = __名称;
                _处理数据 = __处理数据;
            }

            public void 添加事项(M事项 __数据)
            {
                _任务 = _任务.ContinueWith(q =>
                {
                    if (!_取消标志.IsCancellationRequested)
                    {
                        try
                        {
                            _监控.监控下执行(_处理数据, __数据, _节点);
                        }
                        catch (Exception ex)
                        {
                            H日志输出.记录(ex, _节点);
                        }
                    }
                }, _取消标志.Token);
            }

            public void 关闭()
            {
                _取消标志.Cancel();
                _任务.Wait();
                _任务.Dispose();
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

        private class B性能监控
        {
            private Queue<long> _耗时缓存 = new Queue<long>();

            private Queue<long> _延时缓存 = new Queue<long>();

            private StringBuilder __日志 = new StringBuilder();

            private Stopwatch __计时器 = new Stopwatch();

            private int _分组统计数量;

            private int _延迟阈值;

            private int _耗时阈值;

            public B性能监控(int __分组统计数量 = 1000, int __延迟阈值 = 3000, int __耗时阈值 = 100)
            {
                _分组统计数量 = __分组统计数量;
                _延迟阈值 = __延迟阈值;
                _耗时阈值 = __耗时阈值;
            }

            public void 监控下执行(Action<object> __处理数据, M事项 __事项, string __来源)
            {
                __计时器.Restart();
                var __延迟 = Environment.TickCount - __事项.接收时间;
                _延时缓存.Enqueue(__延迟);
                __处理数据(__事项.数据);
                _耗时缓存.Enqueue(__计时器.ElapsedMilliseconds);
                if (_耗时缓存.Count >= _分组统计数量)
                {
                    var __总耗时 = _耗时缓存.Sum();
                    var __总延时 = _延时缓存.Sum();
                    var __数量 = _耗时缓存.Count;
                    _耗时缓存.Clear();
                    _延时缓存.Clear();
                    __日志.Clear();
                    __日志.AppendFormat("统计: 数量 {0}, 总耗时, {1}; 平均处理耗时: {2} 毫秒; 平均延迟: {3}", __数量, __总耗时, __总耗时 / __数量, __总延时 / __数量);
                    H日志输出.记录(__日志.ToString(), null, __总延时 / __数量 > _延迟阈值 ? TraceEventType.Warning : TraceEventType.Information);
                }
                if (__计时器.ElapsedMilliseconds > _耗时阈值)
                {
                    __日志.Clear();
                    __日志.AppendFormat("处理 [{0}] 的 {1}:", __来源, __事项.数据);
                    __日志.AppendFormat("延迟 {0} 毫秒, ", __延迟);
                    __日志.AppendFormat("耗时 {0} 毫秒. ", __计时器.ElapsedMilliseconds);
                    H日志输出.记录(__日志.ToString(), null, TraceEventType.Warning);
                }
            }
        }
    }

}
