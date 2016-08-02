using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utility.通用;

namespace Utility.模式
{
    public class H审批<T请求, T上下文>
    {
        private List<I审批<T请求, T上下文>> __步骤列表 = new List<I审批<T请求, T上下文>>();

        public void 注册审批流程(I审批<T请求, T上下文> __步骤, int 顺序)
        {
            __步骤列表.Insert(顺序, __步骤);
        }

        public void 注册审批流程(I审批<T请求, T上下文> __步骤)
        {
            __步骤列表.Add(__步骤);
        }

        public bool 执行(T请求 __业务, T上下文 __上下文)
        {
            foreach (var __审批 in __步骤列表)
            {
                try
                {
                    var __结束 = __审批.处理(__业务, ref __上下文);
                    if (__结束)
                    {
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    H调试.记录异常(ex);
                }
            }
            return true;
        }
    }

    /// <typeparam name="T上下文">通常用字典类型</typeparam>
    public interface I审批<T请求, T上下文>
    {
        string 步骤名称 { get; }

        string 步骤描述 { get; }

        /// <returns>不需要继续处理返回true, 否则返回false</returns>
        bool 处理(T请求 业务, ref T上下文 上下文);
    }
}
