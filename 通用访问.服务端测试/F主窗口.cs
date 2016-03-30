using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Utility.通用;
using 通用访问.服务端测试.IBLL;

namespace 通用访问.服务端测试
{
    public partial class F主窗口 : Form
    {
        private IB调试信息 _IB调试信息 = H容器.取出<IB调试信息>();

        public F主窗口()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            var __基本状态 = new TabPage("基本状态");
            __基本状态.Controls.Add(new F基本状态() { Dock = DockStyle.Fill });
            this.outTB.Controls.Add(__基本状态);

            var __工程人员视图 = new TabPage("业务-工程人员视图");
            __工程人员视图.Controls.Add(new F业务_工程视图() { Dock = DockStyle.Fill });
            this.outTB.Controls.Add(__工程人员视图);

            var __开发人员视图 = new TabPage("业务-开发人员视图");
            __开发人员视图.Controls.Add(new F业务_开发视图(){ Dock = DockStyle.Fill });
            this.outTB.Controls.Add(__开发人员视图);

            _IB调试信息.已增加 += _IB调试信息_已增加;
        }

        void _IB调试信息_已增加(string __信息)
        {
            记录(__信息);
        }

        public void 记录(string __信息)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Action<string>(记录), __信息);
                return;
            }

            this.out访问记录.Text = string.Format("{0}  {1}{2}{3}", DateTime.Now.ToLongTimeString(), __信息, Environment.NewLine, this.out访问记录.Text);
        }
    }
}
