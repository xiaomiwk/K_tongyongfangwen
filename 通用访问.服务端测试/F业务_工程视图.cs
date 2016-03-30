using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Utility.通用;
using 通用访问.服务端测试.IBLL;

namespace 通用访问.服务端测试
{
    public partial class F业务_工程视图 : UserControl
    {
        private IB业务 _IB业务 = H容器.取出<IB业务>();

        public F业务_工程视图()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            显示对象();

            this.do刷新.Click += do刷新_Click;
        }

        private void 显示对象()
        {
            var __工程视图 = _IB业务.查询工程视图();
            this.out号码方案.Text = __工程视图.号码方案;
            this.out网速.Text = __工程视图.网速.ToString();
            this.out资源消耗.Text = __工程视图.资源消耗.ToString();
        }

        void do刷新_Click(object sender, EventArgs e)
        {
            显示对象();
        }
    }
}
