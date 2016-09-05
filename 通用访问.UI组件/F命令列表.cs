using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using 通用访问;
using 通用访问.DTO;

namespace 通用访问.UI组件
{
    public partial class F命令列表 : UserControl
    {
        private M设备 _当前设备;

        private Point _鼠标位置;

        public IPEndPoint 地址 { get; set; }

        public string 名称 { get; set; }

        public F命令列表()
        {
            InitializeComponent();
        }

        public F命令列表(IPEndPoint __地址, string __名称 = "")
        {
            InitializeComponent();
            this.地址 = __地址;
            this.名称 = __名称;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            this.do断开设备.Enabled = false;

            this.out对象菜单.Opening += out对象菜单_Opening;
            this.do显示对象.Click += do显示对象_Click;

            this.do断开设备.Click += do断开设备_Click;

            this.do工程.Click += do工程_Click;
            this.do开发.Click += do开发_Click;
            this.do客户.Click += do客户_Click;

            if (!DesignMode)
            {
                this.out提示.Location = new Point(0, 0);
                this.out提示.Size = new Size(this.Width, this.Height);
                this.out提示.BringToFront();
                加载设备(地址, 名称);
            }

        }

        private void do开发_Click(object sender, EventArgs e)
        {
            if (_当前设备 != null && _当前设备.访问入口 != null)
            {
                this.do客户.Enabled = true;
                this.do开发.Enabled = false;
                this.do工程.Enabled = true;
                this.do开发.BackColor = Color.Yellow;
                this.do工程.BackColor = Color.FromArgb(38, 164, 221);
                this.do客户.BackColor = Color.FromArgb(38, 164, 221);
                _当前设备.视图 = E角色.开发;
                初始化命令列表();
            }
        }

        private void do工程_Click(object sender, EventArgs e)
        {
            if (_当前设备 != null && _当前设备.访问入口 != null)
            {
                this.do客户.Enabled = true;
                this.do开发.Enabled = true;
                this.do工程.Enabled = false;
                this.do客户.BackColor = Color.FromArgb(38, 164, 221);
                this.do开发.BackColor = Color.FromArgb(38, 164, 221);
                this.do工程.BackColor = Color.Yellow;
                _当前设备.视图 = E角色.工程;
                初始化命令列表();
            }
        }

        private void do客户_Click(object sender, EventArgs e)
        {
            if (_当前设备 != null && _当前设备.访问入口 != null)
            {
                this.do客户.Enabled = false;
                this.do开发.Enabled = true;
                this.do工程.Enabled = true;
                this.do工程.BackColor = Color.FromArgb(38, 164, 221);
                this.do开发.BackColor = Color.FromArgb(38, 164, 221);
                this.do客户.BackColor = Color.Yellow;
                _当前设备.视图 = E角色.客户;
                初始化命令列表();
            }
        }

        public void 断开()
        {
            do断开设备_Click(null, null);
        }

        private void do断开设备_Click(object sender, EventArgs e)
        {
            if (_当前设备 != null && _当前设备.访问入口 != null)
            {
                _当前设备.访问入口.断开();
                _当前设备.命令列表控件.Nodes.Clear();
                this.out提示.Visible = true;
            }
        }

        private void TV_MouseDown(object sender, MouseEventArgs e)
        {
            _鼠标位置 = e.Location;
        }

        private void out对象菜单_Opening(object sender, CancelEventArgs e)
        {
            var __菜单 = sender as ContextMenuStrip;
            if (__菜单 == null) return;
            var __tv = __菜单.SourceControl as TreeView;
            if (__tv == null) return;

            var __node = __tv.GetNodeAt(_鼠标位置);
            if (__node == null || __node.Tag == null)
            {
                e.Cancel = true;
                return;
            }
            __tv.SelectedNode = __node;
        }

        private void do显示对象_Click(object sender, EventArgs e)
        {
            var __tv = out对象菜单.SourceControl as TreeView;
            var __node = __tv.GetNodeAt(_鼠标位置);
            if (__node == null || __node.Tag == null)
            {
                return;
            }
            var __绑定 = __node.Tag as Tuple<string, M方法>;
            if (__绑定 == null)
            {
                return;
            }
            var __对象窗口 = new F对象窗口(__绑定.Item1, _当前设备.访问入口.查询对象明细(__绑定.Item1), _当前设备.访问入口, _当前设备.视图);
            __对象窗口.StartPosition = FormStartPosition.CenterParent;
            __对象窗口.Text = __绑定.Item1;
            __对象窗口.ShowDialog();
        }

        private void out对象列表_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            var __绑定 = e.Node.Tag as Tuple<string, M方法>;
            if (__绑定 == null)
            {
                return;
            }
            _当前设备.命令执行控件.设置当前方法(__绑定.Item1, __绑定.Item2);
        }

        private void 加载设备(IPEndPoint __地址, string __名称 = "")
        {
            //加载对象
            _当前设备 = new M设备 { IP = __地址.Address, 端口号 = __地址.Port, 名称 = __名称 };
            this.do断开设备.Enabled = true;
            Action __显示连接异常 = () => this.BeginInvoke(new Action(() =>
            {
                _当前设备.命令列表控件.ForeColor = Color.Red;
            }));
            Action __显示连接正常 = () => this.BeginInvoke(new Action(() =>
            {
                _当前设备.命令列表控件.ForeColor = Color.Black;
            }));
            if (_当前设备.访问入口 == null)
            {
                _当前设备.命令列表控件 = new TreeView
                {
                    BackColor = Color.WhiteSmoke,
                    BorderStyle = BorderStyle.None,
                    Dock = DockStyle.Fill,
                    ShowNodeToolTips = true,
                    HideSelection = false,
                    ContextMenuStrip = this.out对象菜单
                };
                _当前设备.命令列表控件.NodeMouseDoubleClick += out对象列表_NodeMouseDoubleClick;
                _当前设备.命令列表控件.MouseDown += TV_MouseDown;

                this.out命令列表容器.添加控件(_当前设备.命令列表控件);
                this.out命令列表容器.激活控件(_当前设备.命令列表控件);

                _当前设备.访问入口 = FT通用访问工厂.创建客户端();
                _当前设备.命令执行控件 = new F执行方法(_当前设备.访问入口) { Dock = DockStyle.Fill };
                this.out命令明细容器.添加控件(_当前设备.命令执行控件);
                this.out命令明细容器.激活控件(_当前设备.命令执行控件);

                _当前设备.访问入口.收到了事件 +=
                    q =>
                        _当前设备.命令执行控件.输出(string.Format("\r\n{0}  收到事件: {1}\r\n", DateTime.Now.ToLongTimeString(),
                            HJSON.序列化(q)));
                _当前设备.访问入口.已断开 += __主动 =>
                {
                    if (!__主动)
                    {
                        __显示连接异常();
                        _当前设备.命令执行控件.输出(string.Format("\r\n{0}  断开连接\r\n", DateTime.Now.ToLongTimeString()));
                    }
                };
                _当前设备.访问入口.已连接 += __显示连接正常;
                _当前设备.视图 = E角色.客户;
                do客户_Click(null, null); //设置样式
                __显示连接正常();
            }
            else
            {
                this.out命令列表容器.激活控件(_当前设备.命令列表控件);
                this.out命令明细容器.激活控件(_当前设备.命令执行控件);
                if (_当前设备.视图 == E角色.客户)
                {
                    do客户_Click(null, null);
                }
                if (_当前设备.视图 == E角色.工程)
                {
                    do工程_Click(null, null);
                }
                if (_当前设备.视图 == E角色.开发)
                {
                    do开发_Click(null, null); 
                }
            }
        }

        public void 初始化命令列表()
        {
            this.out提示.Visible = false;
            _当前设备.命令列表控件.Nodes.Clear();
            if (!_当前设备.访问入口.连接正常)
            {
                _当前设备.访问入口.连接(new IPEndPoint(_当前设备.IP, _当前设备.端口号));
                if (!Equals(_当前设备.IP, IPAddress.Loopback))
                {
                    Thread.Sleep(1000); //当前因为LINUX设备端有延时? 需要停顿一下才能发送
                }
            }
            var __对象列表 = _当前设备.访问入口.查询可访问对象().Where(q => (q.角色 & _当前设备.视图) == _当前设备.视图).ToList();
            __对象列表.Sort((m, n) =>
            {
                if (m.分类.CompareTo(n.分类) != 0)
                {
                    return m.分类.CompareTo(n.分类);
                }
                return m.名称.CompareTo(n.名称);
            });
            var __分类节点 = new Dictionary<string, TreeNode>();
            __对象列表.ForEach(q =>
            {
                TreeNodeCollection __nodes;
                if (string.IsNullOrEmpty(q.分类))
                {
                    __nodes = _当前设备.命令列表控件.Nodes;
                }
                else
                {
                    if (!__分类节点.ContainsKey(q.分类))
                    {
                        __分类节点[q.分类] = _当前设备.命令列表控件.Nodes.Add(q.分类);
                    }
                    __nodes = __分类节点[q.分类].Nodes;
                }
                __nodes = __nodes.Add(q.名称).Nodes;
                var __方法列表 = _当前设备.访问入口.查询对象明细(q.名称).方法列表;
                __方法列表.ForEach(k =>
                {
                    if ((k.角色 & _当前设备.视图) == _当前设备.视图)
                    {
                        //__nodes.Add(string.Format("{0}.{1}", q.名称, k.名称)).Tag = new Tuple<string, M方法>(q.名称, k);
                        __nodes.Add(k.名称).Tag = new Tuple<string, M方法>(q.名称, k);
                    }
                });
            });
            _当前设备.命令列表控件.ExpandAll();
        }

        private class M设备
        {
            public string 名称 { get; set; }
            public string 分类 { get; set; }
            public IPAddress IP { get; set; }
            public int 端口号 { get; set; }
            public TreeView 命令列表控件 { get; set; }
            public F执行方法 命令执行控件 { get; set; }
            public IT客户端 访问入口 { get; set; }
            public E角色 视图 { get; set; }
        }

        public IT客户端 访问入口
        {
            get
            {
                if (_当前设备 != null)
                {
                    return _当前设备.访问入口;
                }
                return null;
            }
        }

    }
}