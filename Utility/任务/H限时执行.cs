using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.任务
{
    public class H限时执行
    {
        public static void 执行(Action 任务, int 限时毫秒)
        {
            Exception __发生错误 = null;
            var __按时返回 = Task.Factory.StartNew(() =>
            {
                try
                {
                    任务();
                }
                catch (Exception ex)
                {
                    __发生错误 = ex;
                }
            }).Wait(限时毫秒);
            if (__发生错误 != null)
            {
                throw __发生错误;
            }
            if (!__按时返回)
            {
                throw new ApplicationException("超时!");
            }
        }

        public static void 执行<T>(Action<T> 任务, T 参数, int 限时毫秒)
        {
            Exception __发生错误 = null;
            var __按时返回 = Task.Factory.StartNew(() =>
            {
                try
                {
                    任务(参数);
                }
                catch (Exception ex)
                {
                    __发生错误 = ex;
                }
            }).Wait(限时毫秒);
            if (__发生错误 != null)
            {
                throw __发生错误;
            }
            if (!__按时返回)
            {
                throw new ApplicationException("超时!");
            }
        }

        public static T 执行<T>(Func<T> 任务, int 限时毫秒)
        {
            T __结果 = default(T);
            Exception __发生错误 = null;
            var __按时返回 = Task.Factory.StartNew(() =>
            {
                try
                {
                    __结果 = 任务();
                }
                catch (Exception ex)
                {
                    __发生错误 = ex;
                }
            }).Wait(限时毫秒);
            if (__发生错误 != null)
            {
                throw __发生错误;
            }
            if (!__按时返回)
            {
                throw new ApplicationException("超时!");
            }
            return __结果;
        }

        public static T 执行<ARG,T>(Func<ARG, T> 任务, ARG 参数, int 限时毫秒)
        {
            T __结果 = default(T);
            Exception __发生错误 = null;
            var __按时返回 = Task.Factory.StartNew(() =>
            {
                try
                {
                    __结果 = 任务(参数);
                }
                catch (Exception ex)
                {
                    __发生错误 = ex;
                }
            }).Wait(限时毫秒);
            if (__发生错误 != null)
            {
                throw __发生错误;
            }
            if (!__按时返回)
            {
                throw new ApplicationException("超时!");
            }
            return __结果;
        }

    }
}
