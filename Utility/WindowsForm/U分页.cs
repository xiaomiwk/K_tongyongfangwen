using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Utility.WindowsForm
{
    /// <summary>
    /// this.out分页.跳转 += out分页_跳转;
    /// this.out分页.每页条数 = 10;
    /// this.out分页.总条数 = (int)__总条数;
    /// </summary>
    public partial class U分页 : UserControl
    {
        [DefaultValue(1)]
        public int 当前页码 { get; private set; }

        public int 总页数 { get; private set; }

        private int _总条数;

        public int 总条数
        {
            get
            {
                return _总条数;
            }
            set
            {
                if (_总条数 == value)
                {
                    return;
                }
                _总条数 = value;
                总页数 = _总条数 / 每页条数 + ((_总条数 % 每页条数 == 0) ? 0 : 1);
                if (当前页码 > 总页数 || (当前页码 == 0 && 总页数 > 0))
                {
                    当前页码 = 1;
                    跳转到页(1);
                }
                更新页码显示();
            }
        }

        private int _每页条数 = 10;

        [DefaultValue(10)]
        public int 每页条数
        {
            get
            {
                return _每页条数;
            }
            set
            {
                if (_每页条数 == value)
                {
                    return;
                }
                if (value <= 0)
                {
                    value = 10;
                }
                var __旧每页条数 = _每页条数;
                _每页条数 = value;
                总页数 = _总条数 / _每页条数 + ((_总条数 % _每页条数 == 0) ? 0 : 1);
                当前页码 = __旧每页条数 * 当前页码 / value;
                更新页码显示();
            }
        }

        private void 更新页码显示()
        {
            this.out页码.Text = string.Format("第{0} | {1}页 共{2}条", 当前页码, 总页数, _总条数);
        }

        /// <summary>
        /// int 页码, 从1开始
        /// </summary>
        public event Action<int> 跳转;

        protected virtual void On跳转(int __页码)
        {
            var handler = 跳转;
            if (handler != null) handler(__页码);
        }

        public U分页()
        {
            InitializeComponent();
            当前页码 = 1;
            更新页码显示();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.in跳转页.Text = "";
            this.do第一页.Click += do第一页_Click;
            this.do跳转.Click += do跳转_Click;
            this.do最后一页.Click += do最后一页_Click;
            this.do上一页.Click += do上一页_Click;
            this.do下一页.Click += do下一页_Click;
        }

        void do下一页_Click(object sender, EventArgs e)
        {
            if (当前页码 + 1 <= 总页数)
            {
                跳转到页(当前页码 + 1);
            }
            else
            {
                跳转到页(总页数);
            }
        }

        void do上一页_Click(object sender, EventArgs e)
        {
            if (当前页码 - 1 <= 1)
            {
                跳转到页(1);
            }
            else
            {
                跳转到页(当前页码 - 1);
            }
        }

        void do最后一页_Click(object sender, EventArgs e)
        {
            跳转到页(总页数);
        }

        void do跳转_Click(object sender, EventArgs e)
        {
            int __页码;
            if (int.TryParse(this.in跳转页.Text.Trim(), out __页码))
            {
                跳转到页(__页码);
            }
            else
            {
                MessageBox.Show("请输入正确的页码");
            }
        }

        void do第一页_Click(object sender, EventArgs e)
        {
            跳转到页(1);
        }

        private void 跳转到页(int __页码)
        {
            __页码 = Math.Min(__页码, 总页数);
            if (__页码 < 1)
            {
                __页码 = 1;
            }
            当前页码 = __页码;
            更新页码显示();
            On跳转(__页码);
        }
    }
}
