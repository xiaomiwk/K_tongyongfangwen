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
    public class H线程队列
    {
        private ConcurrentDictionary<string, M队列> _队列缓存 = new ConcurrentDictionary<string, M队列>();

        private B性能监控 _监控;

        /// <param name="__延迟阈值">毫秒</param>
        /// <param name="__耗时阈值">毫秒</param>
        public H线程队列(int __分组统计数量 = 1000, int __延迟阈值 = 3000, int __耗时阈值 = 100)
        {
            _监控 = new B性能监控(__分组统计数量, __延迟阈值, __耗时阈值);
        }

        public void 添加事项<T>(string __线程标识, string __队列标识, T __数据, Action<T> __处理数据, bool __监控 = false)
        {
            var __队列 = _队列缓存.GetOrAdd(__线程标识, k => new M队列(__线程标识));
            if (__监控)
            {
                __队列.添加事项(__队列标识, __数据, __处理数据, _监控);
            }
            else
            {
                __队列.添加事项(__队列标识, __数据, __处理数据);
            }
        }

        public void 关闭队列(string __线程标识)
        {
            M队列 __队列;
            if (_队列缓存.TryRemove(__线程标识, out __队列))
            {
                __队列.关闭();
            }
        }

        public void 关闭所有()
        {
            _队列缓存.Values.ToList().ForEach(q => q.关闭());
        }

        class M队列
        {
            private ConcurrentDictionary<string, ConcurrentQueue<Action>> _队列字典 = new ConcurrentDictionary<string, ConcurrentQueue<Action>>();

            private CancellationTokenSource _取消标志 = new CancellationTokenSource();

            private string _名称;

            private int _待处理数量;

            private Thread _线程;

            public M队列(string __名称)
            {
                _名称 = __名称;
                启动线程();
            }

            public void 添加事项<T>(string __队列标识, T __数据, Action<T> __处理数据, B性能监控 __监控 = null)
            {
                //Debug.WriteLine("{0} 添加事项 {1}", DateTime.Now.ToString("HH:mm:ss.fff"), __数据);
                var __接收时间 = Environment.TickCount;
                var __队列 = _队列字典.GetOrAdd(__队列标识, k => new ConcurrentQueue<Action>());
                __队列.Enqueue(() =>
                {
                    if (!_取消标志.IsCancellationRequested)
                    {
                        try
                        {
                            //Debug.WriteLine("{0} 执行事项 {1}", DateTime.Now.ToString("HH:mm:ss.fff"), __数据);
                            if (__监控 == null)
                            {
                                __处理数据(__数据);
                            }
                            else
                            {
                                __监控.监控下执行(_名称, __数据, __接收时间, __处理数据);
                            }
                        }
                        catch (Exception ex)
                        {
                            H日志输出.记录(ex, _名称);
                        }
                    }
                });
                //if (Interlocked.CompareExchange(ref _待处理数量, 0, -1) == -1)
                //{
                //    启动线程();
                //}
                Interlocked.Increment(ref _待处理数量);
            }

            private void 启动线程()
            {
                _线程 = new Thread(() =>
                {
                    while (!_取消标志.IsCancellationRequested)
                    {
                        var __所有通道 = _队列字典.Keys.ToList();
                        __所有通道.ForEach(__通道 =>
                        {
                            ConcurrentQueue<Action> __队列;
                            if (!_队列字典.TryGetValue(__通道, out __队列))
                            {
                                return;
                            }
                            Action __事项;
                            if (__队列.TryDequeue(out __事项))
                            {
                                Interlocked.Decrement(ref _待处理数量);
                                __事项();
                            }
                        });
                        if (_待处理数量 == 0)
                        {
                            Thread.Sleep(50);
                        }
                    }
                }) { IsBackground = true };
                _线程.Start();
            }

            public void 关闭()
            {
                _取消标志.Cancel();
            }
        }

        class B性能监控
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

            public void 监控下执行<T>(string __队列名称, T __数据, int __接收时间, Action<T> __处理数据)
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
                    H日志输出.记录("统计", __日志.ToString(), _总延时 / _数量 > _延迟阈值 ? TraceEventType.Warning : TraceEventType.Information);
                    Interlocked.Exchange(ref _数量, 0);
                    Interlocked.Exchange(ref _总耗时, 0);
                    Interlocked.Exchange(ref _总延时, 0);
                }
                if (__耗时 > _耗时阈值)
                {
                    var __日志 = new StringBuilder();
                    __日志.AppendFormat("处理 {0} ,", __数据);
                    __日志.AppendFormat("耗时 {0} 毫秒. ", __计时器.ElapsedMilliseconds);
                    H日志输出.记录("耗时告警:" + __队列名称, __日志.ToString(), TraceEventType.Warning);
                }
                if (__延迟 > _延迟阈值)
                {
                    var __日志 = new StringBuilder();
                    __日志.AppendFormat("处理 {0} ,", __数据);
                    __日志.AppendFormat("延迟 {0} 毫秒, ", __延迟);
                    H日志输出.记录("延迟:" + __队列名称, __日志.ToString(), TraceEventType.Warning);
                }
            }
        }
    }

}
