using System;
using System.Threading;
using System.Threading.Tasks;
using Utility.通用;

namespace Utility.任务
{
    public class H串行
    {
        Task _外部任务;

        readonly object _任务同步 = new object();

        public TaskScheduler 调度服务 { get; set; }

        public H串行()
        {
            调度服务 = TaskScheduler.Default;
            _外部任务 = Task.Factory.StartNew(() => { });
        }

        public void 执行(Action __动作)
        {
            lock (_任务同步)
            {
                _外部任务 = _外部任务.ContinueWith(q =>
                {
                    try
                    {
                        __动作();
                    }
                    catch (Exception ex)
                    {
                        H日志.记录异常(ex);
                    }
                }, CancellationToken.None, TaskContinuationOptions.None, 调度服务);
            }
        }
    }
}
