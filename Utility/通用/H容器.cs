using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web.Script.Serialization;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using Utility.存储;

namespace Utility.通用
{
    public static class H容器
    {
        public static IUnityContainer _容器;

        static H容器()
        {
            _容器 = new UnityContainer().AddNewExtension<Interception>();
        }

        public static void 注入<T, T1>(bool __单实例 = true, bool __拦截 = true, string __名称 = "", object[] __构造参数 = null)
            where T : class
            where T1 : class, T
        {
            var __植入列表 = new List<InjectionMember>();
            if (__拦截)
            {
                __植入列表.Add(new Interceptor<InterfaceInterceptor>());
                __植入列表.Add(new InterceptionBehavior<LoggerBehavior>());
            }
            if (__构造参数 != null)
            {
                __植入列表.Add(new InjectionConstructor(__构造参数));
            }
            if (__单实例)
            {
                _容器.RegisterType<T, T1>(__名称, new ContainerControlledLifetimeManager(), __植入列表.ToArray());
            }
            else
            {
                _容器.RegisterType<T, T1>(__名称, __植入列表.ToArray());
            }
        }

        public static void 注入<T>(T __实现, bool __拦截 = true, string __名称 = "")
            where T : class
        {
            _容器.RegisterInstance<T>(__名称, __实现);
        }

        /// <summary>
        ///  程序集批量注入, 示例： H容器.注册("BLL.dll", "BLL.H注册");//H注册.设置();
        /// </summary>
        public static void 注册(string __程序集文件路径, string __注册类, string __注册方法 = "设置")
        {
            var __业务层注册类 = Assembly.LoadFrom(H路径.获取绝对路径(__程序集文件路径)).GetType(__注册类);
            var __业务层注册类实例 = Activator.CreateInstance(__业务层注册类);
            __业务层注册类.GetMethod(__注册方法).Invoke(__业务层注册类实例, null);
        }

        public static bool 可取出<T>(string __名称 = "")
        {
            if (string.IsNullOrEmpty(__名称))
            {
                return _容器.IsRegistered<T>();
            }
            else
            {
                return _容器.IsRegistered<T>(__名称);
            }
        }

        public static T 取出<T>(string __名称 = "")
        {
            _容器.IsRegistered<T>();
            return _容器.Resolve<T>(__名称);
        }

        /// <summary>
        /// 包装实例, 以便提供直接拦截实例中的虚方法
        /// </summary>
        /// <typeparam name="T">实例</typeparam>
        public static T 虚方法拦截<T>(params object[] __构造参数) where T : class
        {
            return Intercept.NewInstance<T>(new VirtualMethodInterceptor(), new[] { new LoggerBehavior() }, __构造参数);
        }

        /// <summary>
        /// 包装实例, 以便提供直接拦截实例实现的接口方法
        /// </summary>
        /// <typeparam name="T">实例</typeparam>
        public static T 接口拦截<T>(T __实现) where T : class
        {
            return Intercept.ThroughProxy(__实现, new InterfaceInterceptor(), new[] { new LoggerBehavior() });
        }

        /// <summary>
        /// 包装实例, 以便提供直接拦截实例实现的多个接口方法, 不建议使用, 建议使用单一接口合并继承
        /// </summary>
        /// <typeparam name="T">实例</typeparam>
        public static T 接口拦截<T>(T __实现, IEnumerable<Type> __additionalInterfaces, Func<MethodBase, object, object[], Tuple<object, object[]>> __action) where T : class
        {
            return Intercept.ThroughProxyWithAdditionalInterfaces(__实现, new InterfaceInterceptor(), new IInterceptionBehavior[] { new LoggerBehavior(), new AdditionalInterfaceBehavior(__additionalInterfaces, __action) }, __additionalInterfaces);
        }

        /// <summary>
        /// 包装实例, 以便提供直接拦截继承MarshalByRefObject的实例或者拦截一个接口
        /// </summary>
        /// <typeparam name="T">实例</typeparam>
        public static T 透明拦截<T>(T __实现) where T : class
        {
            return Intercept.ThroughProxy(__实现, new TransparentProxyInterceptor(), new[] { new LoggerBehavior() });
        }
    }

    [AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = false)]
    public class NoLogAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = false)]
    public class NoLogArgumentsAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = false)]
    public class NoLogReturnValueAttribute : Attribute { }

    public class LoggerBehavior : IInterceptionBehavior
    {
        public IMethodReturn Invoke(IMethodInvocation input, GetNextInterceptionBehaviorDelegate getNext)
        {
            var __title = new StringBuilder();
            var __content = new StringBuilder();
            if (input.Arguments.Count > 0)
            {
                __content.AppendFormat("[参数] {0}", input.MethodBase.IsDefined(typeof(NoLogArgumentsAttribute), false) ? "省略" : ArrayTojson(input.Arguments));
            }
            H调用顺序.Order++;
            __title.Append("".PadLeft(H调用顺序.Order * 2, ' '));
            __title.AppendFormat("[{0}]", H调用顺序.Order);
            __title.AppendFormat("[{0}].{1}", input.MethodBase.DeclaringType, input.MethodBase.Name);
            H调试.记录明细(__title.ToString(), __content.ToString(), null, null, 0);
            __content.Clear();

            IMethodReturn __结果 = getNext()(input, getNext);

            if (__结果.Exception != null)
            {
                //var __frame = new System.Diagnostics.StackTrace(ex, 0, true).GetFrame(0);
                __content.AppendFormat("[Error] {0}: {1}", __结果.Exception.GetType(), __结果.Exception.Message);
                H调试.记录提示(__title.ToString(), __content.ToString(), null, null, 0);
            }
            else
            {
                bool __存在执行结果 = false;
                if (__结果.ReturnValue != null && !input.MethodBase.IsDefined(typeof(NoLogReturnValueAttribute), false))
                {
                    __content.AppendFormat("[返回值] {0}", Tojson(__结果.ReturnValue));
                    __存在执行结果 = true;
                }
                if (__结果.Outputs != null && __结果.Outputs.Count > 0 && !input.MethodBase.IsDefined(typeof(NoLogReturnValueAttribute), false))
                {
                    __content.AppendFormat("[输出参数] {0}", Tojson(__结果.Outputs));
                    __存在执行结果 = true;
                }
                if (__存在执行结果)
                {
                    H调试.记录明细(__title.ToString(), __content.ToString(), null, null, 0);
                }
            }
            H调用顺序.Order--;
            return __结果;
        }

        public IEnumerable<Type> GetRequiredInterfaces()
        {
            return Enumerable.Empty<Type>();
        }

        public bool WillExecute
        {
            get { return true; }
        }

        private static string Tojson(object obj)
        {
            try
            {
                return new JavaScriptSerializer().Serialize(obj);
            }
            catch (Exception)
            {
                return obj.ToString();
            }
        }

        private static string ArrayTojson(IParameterCollection obj)
        {
            try
            {
                if (obj == null)
                {
                    return "";
                }
                if (obj.Count == 1)
                {
                    return new JavaScriptSerializer().Serialize(obj[0]);
                }
                return new JavaScriptSerializer().Serialize(obj);
            }
            catch (Exception)
            {
                return obj.ToString();
            }
        }

    }

    internal static class H调用顺序
    {
        [ThreadStatic]
        public static int Order;
    }

    public class AdditionalInterfaceBehavior : IInterceptionBehavior
    {
        private List<Type> _IAdditionalInterfaces;
        private Func<MethodBase, object, object[], Tuple<object, object[]>> _action;
        public AdditionalInterfaceBehavior(IEnumerable<Type> __IAdditionalInterfaces, Func<MethodBase, object, object[], Tuple<object, object[]>> __action)
        {
            _IAdditionalInterfaces = new List<Type>(__IAdditionalInterfaces);
            _action = __action;
        }

        /// <summary>
        /// Implement this method to execute your behavior processing.
        /// </summary>
        /// <param name="input">Inputs to the current call to the target.</param>
        /// <param name="getNext">Delegate to execute to get the next delegate in the behavior chain.</param>
        /// <returns>Return value from the target.</returns>
        public IMethodReturn Invoke(IMethodInvocation input, GetNextInterceptionBehaviorDelegate getNext)
        {
            if (_IAdditionalInterfaces.Exists(q => q == input.MethodBase.DeclaringType))
            {
                object[] __args = new object[input.Arguments.Count];
                input.Arguments.CopyTo(__args, 0);
                var __result = _action(input.MethodBase, input.Target, __args);
                return input.CreateMethodReturn(__result.Item1, __result.Item2);
                //var __result = input.MethodBase.Invoke(input.Target, __args);
                //return input.CreateMethodReturn(__result);
            }
            return getNext()(input, getNext);
        }

        /// <summary>
        /// Returns the interfaces required by the behavior for the objects it intercepts.
        /// </summary>
        /// <returns>The required interfaces.</returns>
        public IEnumerable<Type> GetRequiredInterfaces()
        {
            return _IAdditionalInterfaces;
        }

        /// <summary>
        /// Returns a flag indicating if this behavior will actually do anything when invoked.
        /// </summary>
        /// <remarks>This is used to optimize interception. If the behaviors won't actually
        /// do anything (for example, PIAB where no policies match) then the interception
        /// mechanism can be skipped completely.</remarks>
        public bool WillExecute
        {
            get { return true; }
        }
    }

}
