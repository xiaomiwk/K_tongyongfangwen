using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Utility.WindowsForm;
using 通用访问;
using 通用访问.DTO;

namespace 通用访问.UI组件
{
    public partial class F对象窗口 : FormK
    {

        public F对象窗口(string __对象名称, M对象明细查询结果 __对象明细, IT客户端 __访问入口, E角色 __角色)
        {
            InitializeComponent();

            this.out标题.Text = __对象名称;
            this.f对象1.对象名称 = __对象名称;
            this.f对象1.对象明细 = __对象明细;
            this.f对象1.访问入口 = __访问入口;
            this.f对象1.角色 = __角色;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

        }


    }
}
