using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Transactions;

//namespace OmarALZabir.AspectF
namespace Utility.通用
{
    public static class AspectExtensions
    {
        [DebuggerStepThrough]
        public static AspectF Retry(this AspectF aspects, int retryDuration,
                                    int retryCount, Action<Exception> errorHandler, Action<IEnumerable<Exception>> retryFailed)
        {
            return aspects.Combine(work =>
            {
                List<Exception> errors = null;
                do
                {
                    try
                    {
                        work();
                        return;
                    }
                    catch (Exception x)
                    {
                        if (null == errors)
                            errors = new List<Exception>();
                        errors.Add(x);
                        if (errorHandler != null)
                        {
                            errorHandler(x);
                        }
                        System.Threading.Thread.Sleep(retryDuration);
                    }
                } while (retryCount-- > 0);
                if (retryFailed != null)
                {
                    retryFailed(errors);
                }
            });
        }

        [DebuggerStepThrough]
        public static AspectF Delay(this AspectF aspect, int milliseconds)
        {
            return aspect.Combine(work =>
                                      {
                                          System.Threading.Thread.Sleep(milliseconds);
                                          work();
                                      });
        }

        [DebuggerStepThrough]
        public static AspectF DelayAfter(this AspectF aspect, int timespan, Action<int> __记录耗时 = null)
        {
            return aspect.Combine(work =>
            {
                var __秒表 = new Stopwatch();
                __秒表.Start();
                work();
                __秒表.Stop();
                var __休眠 = timespan - (int)__秒表.Elapsed.TotalMilliseconds;
                if (__记录耗时 != null)
                {
                    __记录耗时((int)__秒表.Elapsed.TotalMilliseconds);
                }
                if (__休眠 > 0)
                {
                    Thread.Sleep(__休眠);
                }
            });
        }

        [DebuggerStepThrough]
        public static AspectF MustBeNonDefault<T>(this AspectF aspect, params T[] args)
            where T : IComparable
        {
            return aspect.Combine(work =>
                                      {
                                          for (var i = 0; i < args.Length; i++)
                                          {
                                              T arg = args[i];
                                              if (arg == null || arg.Equals(default(T)))
                                                  throw new ArgumentException(
                                                      string.Format("Parameter at index {0} is null", i));
                                          }

                                          work();
                                      });
        }

        [DebuggerStepThrough]
        public static AspectF MustBeNonNull(this AspectF aspect, params object[] args)
        {
            return aspect.Combine(work =>
                                      {
                                          for (var i = 0; i < args.Length; i++)
                                          {
                                              var arg = args[i];
                                              if (arg == null)
                                                  throw new ArgumentException(
                                                      string.Format("Parameter at index {0} is null", i));
                                          }

                                          work();
                                      });
        }

        [DebuggerStepThrough]
        public static AspectF Until(this AspectF aspect, Func<bool> test)
        {
            return aspect.Combine(work =>
                                      {
                                          while (!test())
                                          {
                                          }

                                          work();
                                      });
        }

        [DebuggerStepThrough]
        public static AspectF While(this AspectF aspect, Func<bool> test)
        {
            return aspect.Combine(work =>
                                      {
                                          while (test())
                                              work();
                                      });
        }

        [DebuggerStepThrough]
        public static AspectF WhenTrue(this AspectF aspect, params Func<bool>[] conditions)
        {
            return aspect.Combine(work =>
                                      {
                                          if (conditions.Any(condition => !condition()))
                                              return;

                                          work();
                                      });
        }

        [DebuggerStepThrough]
        public static AspectF HowLong(this AspectF aspect, Action<TimeSpan> action)
        {
            return aspect.Combine(work =>
                                      {
                                          DateTime start = DateTime.Now;

                                          work();

                                          DateTime end = DateTime.Now.ToUniversalTime();
                                          action(end - start);
                                      });
        }

        [DebuggerStepThrough]
        public static AspectF RunAsync(this AspectF aspect, Action completeCallback = null)
        {
            return aspect.Combine(work => work.BeginInvoke(asyncresult =>
                                                               {
                                                                   work.EndInvoke(asyncresult);
                                                                   if (completeCallback != null)
                                                                   {
                                                                       completeCallback();
                                                                   }
                                                               }, null));
        }

        public static AspectF Expected<TException>(this AspectF aspect) where TException : Exception
        {
            return aspect.Combine(work =>
                                      {
                                          try
                                          {
                                              work();
                                          }
                                          catch (TException x)
                                          {
                                              Debug.WriteLine(x.ToString());
                                          }
                                      });
        }

        public static AspectF Transaction(this AspectF aspect)
        {
            return aspect.Combine(work =>
                                      {
                                          using (var scope = new TransactionScope(TransactionScopeOption.Required))
                                          {
                                              work();
                                              scope.Complete();
                                          }
                                      });
        }

        /// <summary>
        /// Returns the instance of old object with new operations applied on.
        /// </summary>
        /// <typeparam name="TReturnType">The type of the object new operations will be applied on.</typeparam>
        /// <param name="aspect"></param>
        /// <param name="item">The object need to be modified.</param>
        /// <param name="action">The delegate which performs on the object supplied.</param>
        /// <returns>Returns the old object with new operations applied on.</returns>
        [DebuggerStepThrough]
        public static TReturnType Use<TReturnType>(this AspectF aspect, TReturnType item, Action<TReturnType> action)
        {
            return aspect.Return(() =>
                                     {
                                         action(item);
                                         return item;
                                     });
        }

    }
}