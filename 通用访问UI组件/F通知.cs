using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using 通用访问.DTO;

namespace 通用访问工具
{
    public partial class F通知 : UserControl
    {
        public bool 显示事件 { get; set; }

        public F通知()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.do清空.Click += do清空_Click;
            this.in显示事件.CheckedChanged += in显示事件_CheckedChanged;
        }

        void in显示事件_CheckedChanged(object sender, EventArgs e)
        {
            显示事件 = this.in显示事件.Checked;
        }

        void do清空_Click(object sender, EventArgs e)
        {
            this.out列表.Rows.Clear();
        }

        public void 处理通知(M通知 __通知)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Action<M通知>(处理通知), __通知);
                return;
            }
            if (this.out列表.Rows.Count > 1000)
            {
                this.out列表.Rows.Clear();
            }
            this.out列表.Rows.Add(DateTime.Now.ToLongTimeString(), __通知.重要性, __通知.角色, __通知.对象, __通知.概要, __通知.详细);
        }
    }
}
