using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Utility.通用;

namespace Utility.任务
{
    public class H任务队列
    {
        private ConcurrentDictionary<string, M队列> _节点字典 = new ConcurrentDictionary<string, M队列>();

        private B性能监控 _监控;

        /// <param name="__分组统计数量"></param>
        /// <param name="__延迟阈值">毫秒</param>
        /// <param name="__耗时阈值">毫秒</param>
        public H任务队列(int __分组统计数量 = 1000, int __延迟阈值 = 3000, int __耗时阈值 = 100)
        {
            _监控 = new B性能监控(__分组统计数量, __延迟阈值, __耗时阈值);
        }

        public void 添加事项<T>(string __队列标识, T __数据, Action<T> __处理数据, bool __监控 = false)
        {
            var __队列 = _节点字典.GetOrAdd(__队列标识, k => new M队列(__队列标识));
            if (__监控)
            {
                __队列.添加事项(__数据, __处理数据, _监控);
            }
            else
            {
                __队列.添加事项(__数据, __处理数据);
            }
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

            private string _节点;

            public M队列(string __名称)
            {
                _节点 = __名称;
            }

            public void 添加事项<T>(T __数据, Action<T> __处理数据, B性能监控 __监控)
            {
                var __接收时间 = Environment.TickCount;
                _任务 = _任务.ContinueWith(q =>
                {
                    if (!_取消标志.IsCancellationRequested)
                    {
                        try
                        {
                            __监控.监控下执行(_节点, __数据, __接收时间,__处理数据);
                        }
                        catch (Exception ex)
                        {
                            H调试.记录异常(ex, _节点);
                        }
                    }
                }, _取消标志.Token);
            }

            public void 添加事项<T>(T __数据, Action<T> __处理数据)
            {
                _任务 = _任务.ContinueWith(q =>
                {
                    if (!_取消标志.IsCancellationRequested)
                    {
                        try
                        {
                            __处理数据(__数据);
                        }
                        catch (Exception ex)
                        {
                            H调试.记录异常(ex, _节点);
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

        private class B性能监控
        {
            private long _总耗时;

            private long _总延时;

            private int _数量;

            private int _分组统计数量;

            private int _延迟阈值;

            private int _耗时阈值;

            public B性能监控(int __分组统计数量, int __延迟阈值, int __耗时阈值)
            {
                _分组统计数量 = __分组统计数量;
                _延迟阈值 = __延迟阈值;
                _耗时阈值 = __耗时阈值;
            }

            public void 监控下执行<T>(string __来源, T __数据, int __接收时间, Action<T> __处理数据)
            {
                var __延迟 = Environment.TickCount - __接收时间;
                Interlocked.Add(ref _总延时, __延迟);

                var __计时器 = new Stopwatch();
                __计时器.Start();
                __处理数据(__数据);
                var __耗时 = __计时器.ElapsedMilliseconds;

                Interlocked.Add(ref _总耗时, __耗时);
                Interlocked.Add(ref _数量, 1);
                if (_数量 >= _分组统计数量)
                {
                    var __日志 = new StringBuilder();
                    __日志.AppendFormat("处理数量 {0},", _数量);
                    __日志.AppendFormat("总耗时 {0},", _总耗时);
                    __日志.AppendFormat("平均处理耗时 {0} 毫秒,", _总耗时 / _数量);
                    __日志.AppendFormat("平均延迟 {0},", _总延时 / _数量);
                    H调试.记录(__日志.ToString(), _总延时 / _数量 > _延迟阈值 ? TraceEventType.Warning : TraceEventType.Information);
                }
                if (__耗时 > _耗时阈值)
                {
                    var __日志 = new StringBuilder();
                    __日志.AppendFormat("处理 [{0}] 的 {1}:", __来源, __数据);
                    __日志.AppendFormat("延迟 {0} 毫秒, ", __延迟);
                    __日志.AppendFormat("耗时 {0} 毫秒. ", __计时器.ElapsedMilliseconds);
                    H调试.记录(__日志.ToString(), TraceEventType.Warning);
                }
            }
        }
    }

}
