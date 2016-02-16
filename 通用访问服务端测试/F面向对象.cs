using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Utility.通用;
using 服务端Test.IBLL;
using 通用访问;

namespace 服务端Test
{
    public partial class F面向对象 : UserControl
    {
        public F面向对象()
        {
            InitializeComponent();

            //对象存在动态成员
            //H容器.取出<IT服务端>().添加对象("虚拟对象", () => new H虚拟对象().创建对象());

            //对象没有动态成员
            var __对象 = new H虚拟对象().创建对象();
            H容器.取出<IT服务端>().添加对象("虚拟对象", () => __对象);

        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            this.do更新.Click += do更新_Click;
        }

        void do更新_Click(object sender, EventArgs e)
        {
        }


    }
}
