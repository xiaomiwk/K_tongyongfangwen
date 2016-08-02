using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using Utility.存储;

namespace Utility.通用
{
    public static class H容器
    {
        public static IUnityContainer _IUnityContainer;

        private static Interception _Interception;

        private static PolicyDefinition _PolicyDefinition;

        static H容器()
        {
            _IUnityContainer = new UnityContainer();
            _Interception = _IUnityContainer.AddNewExtension<Interception>().Configure<Interception>();
            _PolicyDefinition = _Interception.AddPolicy("MyPolicy");
            _PolicyDefinition.AddMatchingRule<AlwaysMatchingRule>();
            _PolicyDefinition.AddCallHandler<LoggerCallHandler>(new ContainerControlledLifetimeManager());
        }

        public static void 注册(string __程序集文件路径, string __注册类, string __注册方法 = "设置")
        {
            var __业务层注册类 = Assembly.LoadFrom(H路径.获取绝对路径(__程序集文件路径)).GetType(__注册类);
            var __业务层注册类实例 = Activator.CreateInstance(__业务层注册类);
            __业务层注册类.GetMethod(__注册方法).Invoke(__业务层注册类实例, null);
        }

        public static void 注入<T, T1>(bool __单实例 = true, bool __拦截 = true, string __名称 = null, object[] __构造参数 = null) where T1 : T
        {
            if (__单实例)
            {
                if (string.IsNullOrEmpty(__名称))
                {
                    if (__构造参数 == null)
                    {
                        _IUnityContainer.RegisterType<T, T1>(new ContainerControlledLifetimeManager());
                    }
                    else
                    {
                        _IUnityContainer.RegisterType<T, T1>(new ContainerControlledLifetimeManager(), new InjectionConstructor(__构造参数));
                    }
                }
                else
                {
                    if (__构造参数 == null)
                    {
                        _IUnityContainer.RegisterType<T, T1>(__名称, new ContainerControlledLifetimeManager());
                    }
                    else
                    {
                        _IUnityContainer.RegisterType<T, T1>(__名称, new ContainerControlledLifetimeManager(), new InjectionConstructor(__构造参数));
                    }
                }
            }
            else
            {
                if (string.IsNullOrEmpty(__名称))
                {
                    if (__构造参数 == null)
                    {
                        _IUnityContainer.RegisterType<T, T1>();
                    }
                    else
                    {
                        _IUnityContainer.RegisterType<T, T1>(new InjectionConstructor(__构造参数));
                    }
                }
                else
                {
                    if (__构造参数 == null)
                    {
                        _IUnityContainer.RegisterType<T, T1>(__名称);
                    }
                    else
                    {
                        _IUnityContainer.RegisterType<T, T1>(__名称, new InjectionConstructor(__构造参数));
                    }
                }
            }
            if (__拦截 && typeof(T).IsPublic)
            {
                _Interception.SetInterceptorFor<T>(new InterfaceInterceptor());//InterfaceInterceptor,TransparentProxyInterceptor,VirtualMethodInterceptor
                //_PolicyDefinition.AddMatchingRule<TypeMatchingRule>(new InjectionConstructor(typeof(T1).FullName, true));
            }
        }

        public static void 注入<T>(T __实现, bool __拦截 = true, string __名称 = null)
        {
            _IUnityContainer.RegisterInstance<T>(__名称, __实现);
            if (__拦截 && typeof(T).IsPublic)
            {
                _Interception.SetInterceptorFor<T>(new VirtualMethodInterceptor());//InterfaceInterceptor,TransparentProxyInterceptor,VirtualMethodInterceptor
                //_PolicyDefinition.AddMatchingRule<TypeMatchingRule>(new InjectionConstructor(typeof(T).FullName, true));
            }
        }

        public static T 取出<T>(string __名称 = null)
        {
            if (string.IsNullOrEmpty(__名称))
            {
                return _IUnityContainer.Resolve<T>();
            }
            return _IUnityContainer.Resolve<T>(__名称);
        }

        /// <summary>
        /// 包装实例, 以便提供直接拦截实例中的虚方法
        /// </summary>
        /// <typeparam name="T">实例</typeparam>
        public static T 拦截虚方法<T>()
        {
            _Interception.SetInterceptorFor<T>(new VirtualMethodInterceptor());//InterfaceInterceptor,TransparentProxyInterceptor,VirtualMethodInterceptor
            return _IUnityContainer.Resolve<T>();
        }

        /// <summary>
        /// 包装实例, 以便提供直接拦截实例实现的第一个接口方法(未测试)
        /// </summary>
        /// <typeparam name="T">实例</typeparam>
        public static T 拦截接口<T>()
        {
            _IUnityContainer.RegisterType<T>(new Interceptor<TransparentProxyInterceptor>(), new InterceptionBehavior<PolicyInjectionBehavior>());
            return _IUnityContainer.Resolve<T>();
        }
    }

    class AlwaysMatchingRule : IMatchingRule
    {
        [InjectionConstructor]
        public AlwaysMatchingRule()
        {
        }

        public bool Matches(MethodBase member)
        {
            return true;
        }
    }

    [AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = false)]
    public class NoLogAttribute : Attribute { }

    class LoggerCallHandler : ICallHandler
    {
        private const string _缩进 = "    ";

        public IMethodReturn Invoke(IMethodInvocation __方法, GetNextHandlerDelegate __获取嵌套方法)
        {
            if (__方法.MethodBase.IsDefined(typeof(NoLogAttribute), false))
            {
                return __获取嵌套方法()(__方法, __获取嵌套方法);
            }
            var __执行前 = new StringBuilder();
            var __缩进 = "";
            for (int i = 0; i < Order; i++)
            {
                __缩进 += _缩进;
            }
            var __函数 = string.Format("[{0}] {1} : {2}", Order, 获取原始类(__方法.Target), __方法.MethodBase);
            __执行前.Append(__缩进).Append(__函数);
            var __输入参数数量 = __方法.Arguments.Count;
            for (int i = 0; i < __输入参数数量; i++)
            {
                if (i == 0)
                {
                    __执行前.AppendLine(" || ");
                }
                var __参数 = __方法.Arguments[i];
                if (__输入参数数量 == 1)
                {
                    __执行前.Append(__缩进).Append(_缩进).AppendFormat("输入{0} : {1}", i + 1, __参数);
                }
                else
                {
                    __执行前.Append(__缩进).Append(_缩进).AppendFormat("输入{0}/{2} : {1}", i + 1, __参数, __输入参数数量);
                }
                if (i != __输入参数数量 - 1)
                {
                    __执行前.AppendLine();
                }
            }
            输出(__执行前.ToString());
            Order++;
            IMethodReturn __结果 = __获取嵌套方法()(__方法, __获取嵌套方法);
            Order--;
            __缩进 = "";
            for (int i = 0; i < Order; i++)
            {
                __缩进 += _缩进;
            }
            var __执行后 = new StringBuilder();
            if (__结果.ReturnValue != null)
            {
                __执行后.Append(__缩进).Append(_缩进).AppendLine("结果 : " + __结果.ReturnValue);
            }
            var __输出参数数量 = __结果.Outputs.Count;
            for (int i = 0; i < __输出参数数量; i++)
            {
                var __参数 = __结果.Outputs[i];
                if (__输出参数数量 == 1)
                {
                    __执行后.Append(__缩进).Append(_缩进).AppendFormat("输出{0} : {1}", i + 1, __参数).AppendLine();
                }
                else
                {
                    __执行后.Append(__缩进).Append(_缩进).AppendFormat("输出{0}/{2} : {1}", i + 1, __参数, __输出参数数量).AppendLine();
                }
            }
            if (__结果.Exception != null)
            {
                __执行后.AppendFormat("异常 : [{0}] {1}", __结果.Exception.GetType(), __结果.Exception.Message);
                __执行后.AppendLine().Append(Environment.StackTrace);
            }
            if (__执行后.Length > 0)
            {
                __函数 = string.Format("[{0}] {1} : {2} || {3}", Order, 获取原始类(__方法.Target), __方法.MethodBase, Environment.NewLine);
                __执行后.Insert(0, __函数).Insert(0, __缩进);
                输出(__执行后.ToString().Remove(__执行后.Length - 1));
            }
            return __结果;
        }

        public int Order { get; set; }

        private static string 获取原始类(object Target)
        {
            if (Target == null)
            {
                return "null";
            }
            var __描述 = Target.ToString();
            if (__描述.StartsWith("DynamicModule.") && __描述.Split(new string[] { "_" }, StringSplitOptions.RemoveEmptyEntries).Length > 1)
            {
                var __起始 = __描述.IndexOf('_');
                var __结束 = __描述.LastIndexOf('_');
                return __描述.Substring(__起始 + 1, __结束 - __起始 - 1);
            }
            return __描述;
        }

        private static void 输出(string __信息)
        {
            H调试.记录(__信息, TraceEventType.Verbose);
        }
    }
}
