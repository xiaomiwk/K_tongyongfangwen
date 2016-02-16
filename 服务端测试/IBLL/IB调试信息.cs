using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace 服务端Test.IBLL
{
    interface IB调试信息
    {
        void 增加(string 信息);

        event Action<string> 已增加;
    }

    internal class B调试信息 : IB调试信息
    {
        public void 增加(string 信息)
        {
            On已增加(信息);
        }

        public event Action<string> 已增加;

        protected virtual void On已增加(string obj)
        {
            Action<string> handler = 已增加;
            if (handler != null) handler(obj);
        }
    }
}
