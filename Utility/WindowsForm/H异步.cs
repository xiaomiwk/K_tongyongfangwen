using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Utility.WindowsForm
{
    public static class H异步
    {
        public static void 执行(Control __影响区域, Action __异步任务, Action __成功后执行, Action __失败后执行)
        {
            //获取并验证输入

            //限制界面
            var __等待面板 = new U等待();
            __影响区域.创建局部覆盖控件(__等待面板, null);

            //配置任务
            var __任务 = new Task(__异步任务);

            //反馈操作结果
            __任务.ContinueWith(task =>
            {
                __等待面板.隐藏();
                __成功后执行();
            },
                CancellationToken.None, TaskContinuationOptions.OnlyOnRanToCompletion,
                 TaskScheduler.FromCurrentSynchronizationContext());
            __任务.ContinueWith(task =>
            {
                __等待面板.隐藏();
                __失败后执行();
                if (task.Exception != null) task.Exception.Handle(q => true);
            },
                CancellationToken.None, TaskContinuationOptions.OnlyOnFaulted,
                 TaskScheduler.FromCurrentSynchronizationContext());

            //开始任务
            __任务.Start();
        }

    }
}
