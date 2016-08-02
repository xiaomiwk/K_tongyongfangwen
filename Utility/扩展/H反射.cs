using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Utility.通用;

namespace Utility.扩展
{
    public static class H反射
    {
        /// <returns>item1表示实例, item2表示文件路径</returns>
        public static List<Tuple<T, string>> 获取实例<T>(string __目录, params string[] __文件搜索条件)
        {
            var __所有实例 = new List<Tuple<T, string>>();

            var __列表 = new List<string>();
            if (__文件搜索条件.Length == 0)
            {
                __文件搜索条件 = new string[] { "*.dll"};
            }
            for (int i = 0; i < __文件搜索条件.Length; i++)
            {
                var __文件列表 = Directory.GetFiles(__目录, __文件搜索条件[i]);
                if (__文件列表.Length > 0)
                {
                    __列表.AddRange(__文件列表);
                }
            }
            //加载插件程序集；并查看哪个类型对宿主可用
            foreach (var __dll文件 in __列表)
            {
                try
                {
                    var __程序集 = Assembly.LoadFrom(__dll文件);
                    //检查每个公开导出的类型
                    foreach (Type __类型 in __程序集.GetExportedTypes())
                    {
                        //如果类型是实现了插件接口的类，那么类型就对宿主可用
                        if (__类型.IsClass && typeof(T).IsAssignableFrom(__类型))
                        {
                            var __实例 = (T)Activator.CreateInstance(__类型);
                            __所有实例.Add(new Tuple<T, string>(__实例, new FileInfo(__dll文件).Name));
                            Debug.WriteLine("加载插件成功: " + __类型.AssemblyQualifiedName);
                        }
                    }
                }
                catch (Exception ex)
                {
                    H日志.记录异常(ex);
                }
            }
            return __所有实例;
        }

        public static T 获取实例<T>(string __文件路径)
        {
            try
            {
                var __程序集 = Assembly.LoadFrom(__文件路径);
                //检查每个公开导出的类型
                foreach (Type __类型 in __程序集.GetExportedTypes())
                {
                    //如果类型是实现了插件接口的类，那么类型就对宿主可用
                    if (__类型.IsClass && typeof(T).IsAssignableFrom(__类型))
                    {
                        return (T)Activator.CreateInstance(__类型);
                    }
                }
            }
            catch (Exception ex)
            {
                H日志.记录异常(ex);
                return default(T);
            }
            return default(T);
        }

    }
}
