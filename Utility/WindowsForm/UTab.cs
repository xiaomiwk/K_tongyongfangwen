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
    public partial class UTab : UserControl
    {
        public int 标题宽度 { get; set; }

        private const int _分割间距 = 20;

        private readonly List<UTab页> _所有标题 = new List<UTab页>();


        public UTab()
        {
            标题宽度 = 120;
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.out指示当前.BringToFront();
            this.do向后.Click += do向后_Click;
            this.do向前.Click += do向前_Click;
            this.SizeChanged += UTab_SizeChanged;
        }

        void UTab_SizeChanged(object sender, EventArgs e)
        {
            if (_所有标题.Count < 2)
            {
                this.do向前.Visible = false;
                this.do向后.Visible = false;
                return;
            }
            this.do向前.Visible = (this.out标题容器.Width < (_所有标题.Last().标题按钮.Left + _所有标题.Last().标题按钮.Width) || _所有标题.First().标题按钮.Left < 0);
            this.do向后.Visible = this.do向前.Visible;
        }

        void do向前_Click(object sender, EventArgs e)
        {
            if (_所有标题.First().标题按钮.Left > 0)
            {
                return;
            }
            foreach (Control __子控件 in this.out标题容器.Controls)
            {
                __子控件.Left += 标题宽度 + _分割间距;
            }
            this.out指示当前.Left += 标题宽度 + _分割间距;
            UTab_SizeChanged(null, null);
        }

        void do向后_Click(object sender, EventArgs e)
        {
            if (this.out标题容器.Width > _所有标题.Last().标题按钮.Left + _所有标题.Last().标题按钮.Width)
            {
                return;
            }
            foreach (Control __子控件 in this.out标题容器.Controls)
            {
                __子控件.Left -= 标题宽度 + _分割间距;
            }
            this.out指示当前.Left -= 标题宽度 + _分割间距;
            UTab_SizeChanged(null, null);
        }

        public UTab页 添加(string __标题, Control __内容)
        {
            this.u容器1.添加控件(__内容);

            var __标题按钮 = new Button
            {
                BackColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                ForeColor = Color.FromArgb(64, 64, 64),
                Location = new Point(13 + _所有标题.Count * (标题宽度 + _分割间距), 3),
                Size = new Size(标题宽度, 23),
                Text = __标题,
                UseVisualStyleBackColor = false
            };
            __标题按钮.FlatAppearance.BorderSize = 0;
            __标题按钮.Click += (object sender, EventArgs e) =>
            {
                this.u容器1.激活控件(__内容);
                this.out指示当前.Left = __标题按钮.Left;
            };
            this.out标题容器.Controls.Add(__标题按钮);

            Label __分割符 = null;
            if (_所有标题.Count != 0)
            {
                __分割符 = new Label
                {
                    BackColor = Color.Gainsboro,
                    Location = new Point(__标题按钮.Left - _分割间距 / 2, 5),
                    Size = new Size(1, 20)
                };
                this.out标题容器.Controls.Add(__分割符);
            }

            var __tab页 = new UTab页 { 标题 = __标题, 标题按钮 = __标题按钮, 内容 = __内容, 分割符 = __分割符 };
            _所有标题.Add(__tab页);
            激活(__tab页);

            UTab_SizeChanged(null, null);
            return __tab页;
        }

        public UTab页 激活(string __标题)
        {
            var __匹配 = _所有标题.Find(q => q.标题 == __标题);
            if (__匹配 != null)
            {
                激活(__匹配);
            }
            return __匹配;
        }

        private void 激活(UTab页 __匹配)
        {
            this.u容器1.激活控件(__匹配.内容);
            this.out指示当前.Left = __匹配.标题按钮.Left;
        }

        public UTab页 激活(Control __内容)
        {
            var __匹配 = _所有标题.Find(q => q.内容 == __内容);
            if (__匹配 != null)
            {
                激活(__匹配);
            } 
            return __匹配;
        }

        public void 删除(string __标题)
        {
            var __匹配 = _所有标题.Find(q => q.标题 == __标题);
            if (__匹配 != null)
            {
                删除(__匹配);
            }
        }

        public void 删除(Control __内容)
        {
            var __匹配 = _所有标题.Find(q => q.内容 == __内容);
            if (__匹配 != null)
            {
                删除(__匹配);
            }
        }

        private void 删除(UTab页 __匹配)
        {
            this.u容器1.删除控件(__匹配.内容);
            this.out标题容器.Controls.Remove(__匹配.标题按钮);
            if (__匹配.分割符 != null)
            {
                this.out标题容器.Controls.Remove(__匹配.分割符);
            }
            var __索引 = _所有标题.IndexOf(__匹配);
            if (__索引 != _所有标题.Count - 1)
            {
                for (int i = __索引 + 1; i < _所有标题.Count; i++)
                {
                    if (i == 1)
                    {
                        this.out标题容器.Controls.Remove(_所有标题[i].分割符);
                    }
                    else
                    {
                        _所有标题[i].分割符.Left -= 标题宽度 + _分割间距;
                    }
                    _所有标题[i].标题按钮.Left -= 标题宽度 + _分割间距;
                    if (this.u容器1.当前激活控件 == _所有标题[i].内容)
                    {
                        this.out指示当前.Left = _所有标题[i].标题按钮.Left;
                    }
                }
            }
            _所有标题.Remove(__匹配);

            if (this.u容器1.当前激活控件 == __匹配.内容)
            {
                if (_所有标题.Count > 0)
                {
                    激活(_所有标题[0]);
                }
            }
        }

        public Dictionary<string, Control> 所有标签 {
            get
            {
                var __结果 = new Dictionary<string, Control>();
                _所有标题.ForEach(q => { __结果[q.标题] = q.内容; });
                return __结果;
            }
        }
    }


    public class UTab页
    {
        public string 标题 { get; set; }

        public Button 标题按钮 { get; set; }

        public Control 内容 { get; set; }

        public Label 分割符 { get; set; }
    }
}
