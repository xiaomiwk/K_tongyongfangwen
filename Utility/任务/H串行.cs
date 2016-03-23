using System;
using System.Threading;
using System.Threading.Tasks;
using Utility.通用;

namespace Utility.任务
{
    public static class H串行
    {
        static Task _外部任务;

        static readonly object _任务同步 = new object();

        private static TaskScheduler _调度服务;

        static H串行()
        {
            _外部任务 = new Task(() => { }, TaskCreationOptions.PreferFairness);
            _外部任务.Start();
        }

        public static void 初始化(TaskScheduler __调度服务)
        {
            _调度服务 = __调度服务;
        }

        public static void 执行(Action __动作)
        {
            lock (_任务同步)
            {
                _外部任务 = _外部任务.ContinueWith(q =>
                {
                    __动作();
                }, CancellationToken.None, TaskContinuationOptions.PreferFairness, _调度服务);
                _外部任务 = _外部任务.ContinueWith(task =>
                {
                    if (task.Exception != null)
                    {
                        H异常.处理UI线程(task.Exception.GetBaseException());
                        task.Exception.Handle(q => true);
                    }
                }, CancellationToken.None, TaskContinuationOptions.OnlyOnFaulted, _调度服务);
            }
        }

        public static void 执行(Action<object> __动作, object __参数)
        {
            lock (_任务同步)
            {
                _外部任务 = _外部任务.ContinueWith(q =>
                {
                    __动作(__参数);
                }, CancellationToken.None, TaskContinuationOptions.PreferFairness, _调度服务);
                _外部任务 = _外部任务.ContinueWith(task =>
                {
                    if (task.Exception != null)
                    {
                        H异常.处理UI线程(task.Exception.GetBaseException());
                        task.Exception.Handle(q => true);
                    }
                }, CancellationToken.None, TaskContinuationOptions.OnlyOnFaulted, _调度服务);
            }
        }
    }
}
