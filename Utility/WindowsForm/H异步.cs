using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Utility.通用;

namespace Utility.WindowsForm
{
    public static class H异步
    {
        public static void 执行(Control __影响区域, Action __异步任务, Action __成功后执行 = null, Action<Exception> __失败后执行 = null)
        {
            //获取并验证输入

            //限制界面
            var __等待面板 = new F等待();
            __影响区域.创建局部覆盖控件(__等待面板, null);

            //配置任务
            var __任务 = new Task(() =>
            {
                var __停留最小间隔 = 500;
                var __计时器 = new System.Diagnostics.Stopwatch();
                __计时器.Start();
                __异步任务();
                __计时器.Stop();
                if (__计时器.ElapsedMilliseconds < __停留最小间隔)
                {
                    Thread.Sleep((int)(__停留最小间隔 - __计时器.ElapsedMilliseconds));
                }
            });

            //反馈操作结果
            __任务.ContinueWith(task =>
            {
                __等待面板.隐藏();
                if (__成功后执行 != null)
                {
                    __成功后执行();
                }
            },
                CancellationToken.None, TaskContinuationOptions.OnlyOnRanToCompletion,
                 TaskScheduler.FromCurrentSynchronizationContext());
            __任务.ContinueWith(task =>
            {
                __等待面板.隐藏();
                if (__失败后执行 != null)
                {
                    __失败后执行(task.Exception);
                }
                else
                {
                    new F对话框_确定( "执行出错!\r\n" + (task.Exception == null ? "" : task.Exception.Message), "").ShowDialog();
                    H日志.记录异常(task.Exception);
                }
                if (task.Exception != null) task.Exception.Handle(q => true);
            },
                CancellationToken.None, TaskContinuationOptions.OnlyOnFaulted,
                 TaskScheduler.FromCurrentSynchronizationContext());

            //开始任务
            __任务.Start();
        }

        //private static AsyncCallback SyncContextCallback(AsyncCallback callback)
        //{
        //    SynchronizationContext sc = SynchronizationContext.Current;
        //    if (sc == null) return callback;
        //    return asyncResult => sc.Post(result => callback((IAsyncResult)result), asyncResult);
        //}

        //    webRequest.BeginGetResponse(SyncContextCallback(ProcessWebResponse), webRequest);

        //private void ProcessWebResponse(IAsyncResult result)
        //{
        //    // If we get here，this must be the GUI thread，it's OK to update the UI
        //    var webRequest = (WebRequest)result.AsyncState;
        //    using (var webResponse = webRequest.EndGetResponse(result))
        //    {
        //        Text = "Content length: " + webResponse.ContentLength;
        //    }
        //}
    }
}
