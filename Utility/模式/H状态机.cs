using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utility.模式
{
    public class H状态机<T状态, T事件>
    {
        class M迁移
        {
            public T状态 原状态 { get; set; }

            public T事件 事件 { get; set; }

            public T状态 新状态 { get; set; }

            public Action<object> 执行方法 { get; set; }

            public M迁移(T状态 原状态, T事件 事件, T状态 新状态, Action<object> 执行方法)
            {
                this.原状态 = 原状态;
                this.事件 = 事件;
                this.新状态 = 新状态;
                this.执行方法 = 执行方法;
            }
        }

        readonly List<M迁移> 迁移表 = new List<M迁移>();

        public T状态 当前状态 { get; protected set; }

        public void 处理事件(T事件 事件, object 参数)
        {
            foreach (var __迁移 in 迁移表)
            {
                if (当前状态.Equals(__迁移.原状态) && 事件.Equals(__迁移.事件))
                {
                    当前状态 = __迁移.新状态;
                    __迁移.执行方法(参数);
                    On事件通知(__迁移.原状态, 事件, __迁移.新状态, 参数);
                    break;
                }
            }

            //switch (当前状态)
            //{
            //    case E状态.状态1:
            //        switch (事件)
            //        {
            //            case E事件.事件1:
            //                break;
            //            case E事件.事件2:
            //                break;
            //            default:
            //                throw new ArgumentOutOfRangeException("事件");
            //        }
            //        break;
            //    case E状态.状态2:
            //        break;
            //    default:
            //        throw new ArgumentOutOfRangeException();
            //}
        }

        /// <summary>
        /// 参数分别是: T状态 原状态, T事件 事件, T状态 新状态, object 事件参数
        /// </summary>
        public event Action<T状态, T事件, T状态, object> 事件通知;

        protected void On事件通知(T状态 原状态, T事件 事件, T状态 新状态, object 事件参数)
        {
            var handler = 事件通知;
            if (handler != null) handler(原状态, 事件, 新状态, 事件参数);
        }

        public void 添加迁移(T状态 原状态, T事件 事件, T状态 新状态, Action<object> 执行方法)
        {
            迁移表.Add(new M迁移(原状态, 事件, 新状态, 执行方法));
        }
    }

}
