using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Utility.通用;

namespace Utility.WindowsForm
{
    public partial class U容器 : UserControl
    {
        readonly Dictionary<Control, Panel> _所有容器 = new Dictionary<Control, Panel>();

        public Control 当前激活控件 { get; private set; }

        public U容器()
        {
            InitializeComponent();
        }

        public void 添加控件(Control __内容)
        {
            if (_所有容器.ContainsKey(__内容))
            {
                return;
            }
            var __容器 = new Panel { Dock = DockStyle.Fill };
            __容器.Controls.Add(__内容);
            _所有容器.Add(__内容, __容器);
            try
            {
                this.Controls.Add(__容器);
            }
            finally
            {
                __容器.BringToFront();
            }
            foreach (var item in _所有容器)
            {
                item.Value.Controls[0].Visible = (item.Key == __内容);
            }
        }

        public void 激活控件(Control __内容)
        {
            if (_所有容器.ContainsKey(__内容))
            {
                _所有容器[__内容].BringToFront();
                foreach (var item in _所有容器)
                {
                    item.Value.Controls[0].Visible = (item.Key == __内容);
                }
                当前激活控件 = __内容;
            }
        }

        public void 删除控件(Control __内容)
        {
            if (_所有容器.ContainsKey(__内容))
            {
                this.Controls.Remove(_所有容器[__内容]);
                _所有容器.Remove(__内容);
            }
            if (当前激活控件 == __内容 && _所有容器.Count > 0)
            {
                激活控件(_所有容器.First().Key);
            }
        }
    }
}
