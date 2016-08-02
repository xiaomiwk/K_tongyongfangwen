using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Xml.XPath;
using Utility.WindowsForm;
using Utility.存储;
using Utility.通用;
using 通用访问;
using 通用访问.DTO;

namespace 通用访问工具
{
    public partial class F主窗口 : Form
    {
        private M设备 _当前设备;

        private Point _鼠标位置;

        private TreeNode _当前设备节点;

        public F主窗口()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            this.out标题.Text += " " + Assembly.GetExecutingAssembly().GetName().Version;
            this.do断开设备.Enabled = false;

            this.out设备列表.ShowNodeToolTips = true;
            this.out设备列表.NodeMouseDoubleClick += out设备列表_NodeMouseDoubleClick;

            this.do设备_断开.Click += do设备_断开_Click;
            this.out设备菜单.Opening += out设备菜单_Opening;
            this.out设备列表.MouseDown += TV_MouseDown;
            this.out对象菜单.Opening += out对象菜单_Opening;
            this.do显示对象.Click += do显示对象_Click;

            this.do编辑设备.Click += do编辑设备_Click;
            this.do断开设备.Click += do断开设备_Click;

            this.do工程.Click += do工程_Click;
            this.do开发.Click += do开发_Click;
            this.do客户.Click += do客户_Click;

            FT通用访问工厂.设置日志输出(记录信息, 记录异常);

            加载设备列表();

        }

        void do开发_Click(object sender, EventArgs e)
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
                初始化命令列表(_当前设备, _当前设备.视图);
            }
        }

        void do工程_Click(object sender, EventArgs e)
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
                初始化命令列表(_当前设备, _当前设备.视图);
            }
        }

        void do客户_Click(object sender, EventArgs e)
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
                初始化命令列表(_当前设备, _当前设备.视图);
            }
        }

        void do断开设备_Click(object sender, EventArgs e)
        {
            if (_当前设备 != null && _当前设备.访问入口 != null)
            {
                _当前设备.访问入口.断开();
                _当前设备.命令列表控件.Nodes.Clear();
                this.out提示.Visible = true;
            }
        }

        void do编辑设备_Click(object sender, EventArgs e)
        {
            var __文件名 = H路径.获取绝对路径("设备列表.xml");
            try
            {
                var __原内容 = File.ReadAllText(__文件名);
                Process.Start("notepad.exe", __文件名).WaitForExit();
                var __新内容 = File.ReadAllText(__文件名);
                if (__原内容 != __新内容 && MessageBox.Show("需要重启程序吗?", "重启后生效", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    this.Close();
                    Process.Start(H路径.获取绝对路径("通用访问工具-命令版.exe"));
                }
            }
            catch (Exception)
            {
                throw new ApplicationException("编辑设备列表出错");
            }
        }

        void TV_MouseDown(object sender, MouseEventArgs e)
        {
            _鼠标位置 = e.Location;
        }

        void out设备菜单_Opening(object sender, CancelEventArgs e)
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

        void do设备_断开_Click(object sender, EventArgs e)
        {
            var __tv = out设备菜单.SourceControl as TreeView;
            var __node = __tv.GetNodeAt(_鼠标位置);
            if (__node == null || __node.Tag == null)
            {
                return;
            }
            var __设备 = __node.Tag as M设备;
            if (__设备.访问入口 == null)
            {
                return;
            }
            __设备.访问入口.断开();
            __设备.命令列表控件.Nodes.Clear();
            this.out提示.Visible = true;

        }

        void out对象菜单_Opening(object sender, CancelEventArgs e)
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

        void do显示对象_Click(object sender, EventArgs e)
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

        private void 加载设备列表()
        {
            this.out设备列表.Nodes.Clear();
            var __设备列表 = new List<M设备>();
            var __文件 = H路径.打开文件("设备列表.xml");
            var __XML文档 = XDocument.Load(__文件);
            __文件.Close();
            var __根节点 = __XML文档.Root;
            foreach (XElement __节点 in __根节点.XPathSelectElements("./设备"))
            {
                __设备列表.Add(new M设备
                {
                    分类 = __节点.Attribute("分类").Value,
                    名称 = __节点.Attribute("名称").Value,
                    IP = IPAddress.Parse(__节点.Attribute("IP").Value),
                    端口号 = int.Parse(__节点.Attribute("端口号").Value)
                });
            }
            var __分类节点 = new Dictionary<string, TreeNode>();
            __设备列表.ForEach(q =>
            {
                var __node = new TreeNode(q.名称)
                {
                    Tag = q,
                    ToolTipText = string.Format("{0}:{1}", q.IP, q.端口号)
                };
                if (string.IsNullOrEmpty(q.分类))
                {
                    this.out设备列表.Nodes.Add(__node);
                }
                else
                {
                    if (!__分类节点.ContainsKey(q.分类))
                    {
                        __分类节点[q.分类] = this.out设备列表.Nodes.Add(q.分类);
                    }
                    __分类节点[q.分类].Nodes.Add(__node);
                }
            });
            if (__设备列表.Count < 30)
            {
                this.out设备列表.ExpandAll();
            }
        }

        void out对象列表_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            var __绑定 = e.Node.Tag as Tuple<string, M方法>;
            if (__绑定 == null)
            {
                return;
            }
            _当前设备.命令执行控件.设置当前方法(__绑定.Item1, __绑定.Item2);
        }

        void out设备列表_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            //加载对象
            var __设备 = e.Node.Tag as M设备;
            if (__设备 == null)
            {
                return;
            }
            if (_当前设备节点 != null)
            {
                _当前设备节点.BackColor = Color.Gainsboro;
            }
            _当前设备节点 = e.Node;
            _当前设备节点.BackColor = Color.Yellow;
            this.out提示.Visible = false;
            this.do断开设备.Enabled = true;
            Action __显示连接异常 = () => this.BeginInvoke(new Action(() =>
            {
                e.Node.ForeColor = Color.Red;
                __设备.命令列表控件.ForeColor = Color.Red;
            }));
            Action __显示连接正常 = () => this.BeginInvoke(new Action(() =>
            {
                e.Node.ForeColor = Color.Black;
                __设备.命令列表控件.ForeColor = Color.Black;
            }));
            _当前设备 = __设备;
            if (__设备.访问入口 == null)
            {
                __设备.命令列表控件 = new TreeView { BackColor = Color.WhiteSmoke, BorderStyle = BorderStyle.None, Dock = DockStyle.Fill, ShowNodeToolTips = true, HideSelection = false, ContextMenuStrip = this.out对象菜单 };
                __设备.命令列表控件.NodeMouseDoubleClick += out对象列表_NodeMouseDoubleClick;
                __设备.命令列表控件.MouseDown += TV_MouseDown;

                this.out命令列表容器.添加控件(__设备.命令列表控件);
                this.out命令列表容器.激活控件(__设备.命令列表控件);

                __设备.访问入口 = FT通用访问工厂.创建客户端();
                __设备.命令执行控件 = new F执行方法(__设备.访问入口) { Dock = DockStyle.Fill };
                this.out命令明细容器.添加控件(__设备.命令执行控件);
                this.out命令明细容器.激活控件(__设备.命令执行控件);

                __设备.访问入口.收到了通知 += q => __设备.命令执行控件.输出(string.Format("\r\n{0}  {1} [{2}] {3} 详细: {4}\r\n", DateTime.Now.ToLongTimeString(), q.重要性, q.对象, q.概要, q.详细));
                __设备.访问入口.收到了事件 += q => __设备.命令执行控件.输出(string.Format("\r\n{0}  收到事件: {1}\r\n", DateTime.Now.ToLongTimeString(),HJSON.序列化(q)));
                __设备.访问入口.已断开 += __主动 =>
                {
                    if (!__主动)
                    {
                        __显示连接异常();
                        __设备.命令执行控件.输出(string.Format("\r\n{0}  断开连接\r\n", DateTime.Now.ToLongTimeString()));
                    }
                };
                __设备.访问入口.已连接 += __显示连接正常;
                __设备.视图 = E角色.工程;
                do工程_Click(null, null);//设置样式
                __显示连接正常();
            }
            else
            {
                this.out命令列表容器.激活控件(_当前设备.命令列表控件);
                this.out命令明细容器.激活控件(__设备.命令执行控件);
                if (__设备.视图 == E角色.工程)
                {
                    do工程_Click(null, null); //设置样式
                }
                else
                {
                    do开发_Click(null, null); //设置样式
                }
            }
        }

        private void 记录信息(string __概要, string __详细 = null, TraceEventType __等级 = TraceEventType.Verbose)
        {
            H调试.记录(string.Format("{0}: {1}", __概要, __详细), __等级);
        }

        private void 记录异常(Exception __异常, string __描述 = null, TraceEventType __等级 = TraceEventType.Error)
        {
            H调试.记录异常(__异常, __描述, null, __等级);
        }

        private static void 初始化命令列表(M设备 __设备, E角色 __工程)
        {
            __设备.命令列表控件.Nodes.Clear();
            if (!__设备.访问入口.连接正常)
            {
                __设备.访问入口.连接(new IPEndPoint(__设备.IP, __设备.端口号));
                if (!Equals(__设备.IP, IPAddress.Loopback))
                {
                    Thread.Sleep(1000); //当前因为LINUX设备端有延时? 需要停顿一下才能发送
                }
            }
            var __对象列表 = __设备.访问入口.查询可访问对象().对象列表.Where(q => (q.角色 & __工程) == __工程).ToList();
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
                    __nodes = __设备.命令列表控件.Nodes;
                }
                else
                {
                    if (!__分类节点.ContainsKey(q.分类))
                    {
                        __分类节点[q.分类] = __设备.命令列表控件.Nodes.Add(q.分类);
                    }
                    __nodes = __分类节点[q.分类].Nodes;
                }
                var __方法列表 = __设备.访问入口.查询对象明细(q.名称).方法列表;
                __方法列表.ForEach(k =>
                {
                    if((k.角色 & __工程) == __工程)
                    {
                        __nodes.Add(string.Format("{0}.{1}", q.名称, k.名称)).Tag = new Tuple<string, M方法>(q.名称, k);
                    }
                });
            });
            __设备.命令列表控件.ExpandAll();
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
    }
}
