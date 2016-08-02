using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppFunc = System.Func<System.Collections.Generic.IDictionary<string, object>, System.Threading.Tasks.Task>;

namespace Utility.模式
{
    public interface I管道
    {
        /// <summary>
        /// Inserts a middleware into the OWIN pipeline.
        /// </summary>
        /// <param name="handler"></param>
        /// <returns></returns>
        I管道 Use(Func<I中间件> handler);

        /// <summary>
        /// Inserts into the OWIN pipeline a middleware which does not have a next middleware reference.
        /// </summary>
        /// <param name="handler"></param>
        I管道 Use(AppFunc handler);

        AppFunc Build();
    }

    public interface I中间件
    {
        I中间件 Next { get; set; }

        Task Invoke(IDictionary<string, object> 上下文);

    }

    public class B管道 : I管道
    {
        I中间件 _最后 = null;

        AppFunc _链 = null;

        public AppFunc Build()
        {
            return _链;
        }

        public I管道 Use(AppFunc handler)
        {
            return Use(() => new B中间件(handler));
        }

        public I管道 Use(Func<I中间件> handler)
        {
            var __temp = handler();
            if (_最后 != null)
            {
                _最后.Next = __temp;
            }
            _最后 = __temp;

            if (_链 == null)
            {
                _链 = __temp.Invoke;
            }
            return this;
        }
    }

    public class B中间件 : I中间件
    {
        public I中间件 Next { get; set; }

        AppFunc _handler;

        public B中间件(AppFunc handler)
        {
            _handler = handler;
        }

        public virtual Task Invoke(IDictionary<string, object> 上下文)
        {
            var __task = _handler(上下文);
            if (__task == null)
            {
                if (Next != null)
                {
                    return Next.Invoke(上下文);
                }
                else
                {
                    return Complete();
                }
            }
            else
            {
                return __task;
            }
        }

        protected static Task Complete()
        {
            var __task = new TaskCompletionSource<int>();
            __task.SetResult(0);
            return __task.Task;
        }
    }

}


//class Program
//{
//    static void Main(string[] args)
//    {
//        Server __Server = new Server(5000);
//        //__Server.Start(组装中间件);

//        var __管道 = new B管道();
//        __管道.Use(处理静态页);
//        __管道.Use(处理WebApi);
//        __Server.Start(__管道.Build());
//        Console.WriteLine("按回车键结束");
//        Console.ReadLine();
//    }

//    static Task 组装中间件(IDictionary<string, object> __上下文)
//    {
//        var __路径 = (string)__上下文[OwinKeys.RequestPath];
//        if (__路径.EndsWith(".k"))
//        {
//            return 处理WebApi(__上下文);
//        }
//        else
//        {
//            return 处理静态页(__上下文);
//        }
//    }

//    static Task 处理静态页(IDictionary<string, object> __上下文)
//    {
//        var __路径 = (string)__上下文[OwinKeys.RequestPath];
//        if (__路径.EndsWith(".k"))
//        {
//            return null;
//        }
//        __上下文[OwinKeys.ResponseStatusCode] = 200;
//        var __stream = ((Stream)__上下文[OwinKeys.ResponseBody]);
//        __上下文[OwinKeys.ResponseHeaders] = new Dictionary<string, string[]> { { "Content-Type", new string[] { "text/html;charset=utf-8" } } };
//        var __data = Encoding.UTF8.GetBytes("hello world");

//        //__stream.Write(__data,0, __data.Length);
//        //return Task.FromResult<int>(0);
//        //var __task = new TaskCompletionSource<int>();
//        //__task.SetResult(0);
//        //return __task.Task;

//        return Task.Factory.FromAsync(__stream.BeginWrite, __stream.EndWrite, __data, 0, __data.Length, null, TaskCreationOptions.None);
//    }

//    static Task 处理WebApi(IDictionary<string, object> __上下文)
//    {
//        var __路径 = (string)__上下文[OwinKeys.RequestPath];
//        if (!__路径.EndsWith(".k"))
//        {
//            return null;
//        }

//        __上下文[OwinKeys.ResponseStatusCode] = 200;
//        var __stream = ((Stream)__上下文[OwinKeys.ResponseBody]);
//        __上下文[OwinKeys.ResponseHeaders] = new Dictionary<string, string[]> { { "Content-Type", new string[] { "application/json;charset=utf-8" } } };
//        var __data = Encoding.UTF8.GetBytes("{ pro1:'', pro2:1 }");
//        return Task.Factory.FromAsync(__stream.BeginWrite, __stream.EndWrite, __data, 0, __data.Length, null, TaskCreationOptions.None);
//    }
//}
