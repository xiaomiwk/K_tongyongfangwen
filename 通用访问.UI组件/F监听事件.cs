using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using 通用访问.DTO;

namespace 通用访问.UI组件
{
    public partial class F监听事件 : UserControl
    {
        private bool 显示事件;

        public F监听事件()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.do清空.Click += do清空_Click;
            this.in监听.CheckedChanged += in监听_CheckedChanged;
        }

        void in监听_CheckedChanged(object sender, EventArgs e)
        {
            显示事件 = this.in监听.Checked;
        }

        void do清空_Click(object sender, EventArgs e)
        {
            this.out列表.Rows.Clear();
        }

        public void 处理通知(M接收事件 __事件)
        {
            if (!显示事件)
            {
                return;
            }
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Action<M接收事件>(处理通知), __事件);
                return;
            }
            if (this.out列表.Rows.Count > 1000)
            {
                this.out列表.Rows.Clear();
            }
            this.out列表.Rows.Add(DateTime.Now.ToLongTimeString(), __事件.对象名称, __事件.事件名称, __事件.实参列表 == null ? "" : string.Join(",", __事件.实参列表.Select(k => string.Format("{0}:{1}", k.名称, k.值))));
        }
    }
}
