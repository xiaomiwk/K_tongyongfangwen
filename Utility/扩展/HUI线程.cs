using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Utility.WindowsForm;
using Utility.通用;

namespace Utility.扩展
{
    public static class HUI线程
    {
        private static SynchronizationContext _UI上下文;

        public static void 初始化()
        {
            _UI上下文 = SynchronizationContext.Current;
        }

        public static void 执行(Action __更新UI)
        {
            if (__更新UI == null)
            {
                return;
            }
            if (_UI上下文 == null)
            {
                __更新UI();
                return;
            }
            _UI上下文.Post(q => __更新UI(), null);
        }

        public static void 异步执行(Control __影响区域, Action __异步任务, Action __成功后执行 = null, Action<Exception> __失败后执行 = null, bool __禁用影响区域 = true)
        {
            //获取并验证输入

            //限制界面
            var __等待面板 = new F等待();
            __影响区域.创建局部覆盖控件(__等待面板, null, __禁用影响区域);

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
                __成功后执行?.Invoke();
                __等待面板.隐藏();
            },
                CancellationToken.None, TaskContinuationOptions.OnlyOnRanToCompletion, TaskScheduler.FromCurrentSynchronizationContext());
            __任务.ContinueWith(task =>
            {
                if (__失败后执行 != null)
                {
                    __失败后执行(task.Exception);
                }
                else
                {
                    new F对话框_确定("执行出错!\r\n" + (task.Exception == null ? "" : task.Exception.Message), "").ShowDialog();
                    H日志.记录异常(task.Exception);
                }
                if (task.Exception != null) task.Exception.Handle(q => true);
                __等待面板.隐藏();
            },
                CancellationToken.None, TaskContinuationOptions.OnlyOnFaulted, TaskScheduler.FromCurrentSynchronizationContext());

            //开始任务
            __任务.Start();
        }

        public static void 异步执行(Action __异步任务, Action __成功后执行 = null, Action<Exception> __失败后执行 = null)
        {
            //获取并验证输入

            //限制界面

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
                __成功后执行?.Invoke();
            },
                CancellationToken.None, TaskContinuationOptions.OnlyOnRanToCompletion, TaskScheduler.FromCurrentSynchronizationContext());
            __任务.ContinueWith(task =>
            {
                if (__失败后执行 != null)
                {
                    __失败后执行(task.Exception);
                }
                else
                {
                    new F对话框_确定("执行出错!\r\n" + (task.Exception == null ? "" : task.Exception.Message), "").ShowDialog();
                    H日志.记录异常(task.Exception);
                }
                if (task.Exception != null) task.Exception.Handle(q => true);
            },
                CancellationToken.None, TaskContinuationOptions.OnlyOnFaulted, TaskScheduler.FromCurrentSynchronizationContext());

            //开始任务
            __任务.Start();
        }

        public static void 同步执行(Control __影响区域, Action __同步任务, bool __禁用影响区域 = false, bool __全覆盖 = false)
        {
            var __等待窗口 = new F等待();
            if (__全覆盖)
            {
                __等待窗口.背景颜色 = __影响区域.BackColor;
                __影响区域.创建全覆盖控件(__等待窗口, null);
                __等待窗口.居中();
            }
            else
            {
                __影响区域.创建局部覆盖控件(__等待窗口, null, __禁用影响区域);
            }
            try
            {
                __同步任务();
            }
            finally
            {
                __等待窗口.隐藏();
            }
        }
    }
}
