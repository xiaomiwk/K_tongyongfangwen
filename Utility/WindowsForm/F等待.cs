using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Utility.WindowsForm;

namespace Utility.WindowsForm
{
    public partial class F等待 : UserControl, I覆盖控件
    {
        public F等待(bool __运行取消 = false)
        {
            InitializeComponent();
            this.out提示.Text = string.Empty;
            this.do取消.Visible = __运行取消;
            this.do取消.Click += do取消_Click;
        }

        void do取消_Click(object sender, EventArgs e)
        {
            On已取消();
            隐藏();
        }

        public string 提示
        {
            get { return this.out提示.Text; }
            set { this.out提示.Text = value; }
        }

        public void 隐藏()
        {
            触发已隐藏();
            已隐藏 = null;
        }

        public event Action 已隐藏;

        protected void 触发已隐藏()
        {
            Action handler = 已隐藏;
            if (handler != null) handler();
        }

        public event Action 已取消;
        protected virtual void On已取消()
        {
            var handler = 已取消;
            if (handler != null) handler();
        }
    }
}
