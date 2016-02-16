using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using 通用访问;
using 通用访问.DTO;
using 通用访问工具.查看数据;

namespace 通用访问工具
{
    public partial class F对象窗口 : Form
    {
        private M对象明细 _对象明细;

        private IT客户端 _访问入口;

        private string _对象名称;

        private E角色 _角色;

        public F对象窗口(string __对象名称, M对象明细 __对象明细, IT客户端 __访问入口, E角色 __角色)
        {
            _对象名称 = __对象名称;
            _对象明细 = __对象明细;
            _访问入口 = __访问入口;
            _角色 = __角色;
            InitializeComponent();

            this.out标题.Text = __对象名称;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            this.out属性.CellDoubleClick += out属性_CellDoubleClick;
            this.do刷新属性.Click += do刷新属性_Click;
            更新角色(_角色);
        }

        public void 更新角色(E角色 __角色)
        {
            _角色 = __角色;
            显示属性(_对象明细.属性列表);

            this.out容器.Panel1Collapsed = this.out属性.Rows.Count == 0;

            if (_对象明细.方法列表.Count == 0 && _对象明细.事件列表.Count == 0)
            {
                this.out容器.Panel2Collapsed = true;
            }
            else
            {
                this.out容器.Panel2Collapsed = false;
                this.out方法列表.TabPages.Clear();
                _对象明细.方法列表.ForEach(q =>
                {
                    if ((q.角色 & _角色) != _角色)
                    {
                        return;
                    }
                    var __方法控件 = new F对象_方法(_访问入口, _对象名称, q) { Dock = DockStyle.Fill };
                    //__方法控件.执行方法 += k => __方法控件.设置返回值(_访问入口.执行方法(_对象名称, q.名称, k));
                    var __控件 = new TabPage(q.名称) { Padding = new Padding(5), BackColor = Color.White, ToolTipText = q.名称 };
                    __控件.Controls.Add(__方法控件);
                    this.out方法列表.TabPages.Add(__控件);
                });
                _对象明细.事件列表.ForEach(q =>
                {
                    if ((q.角色 & _角色) != _角色)
                    {
                        return;
                    }
                    var __事件控件 = new F对象_事件(_访问入口, _对象名称, q) { Dock = DockStyle.Fill };
                    var __控件 = new TabPage(q.名称)
                    {
                        Padding = new Padding(5),
                        BackColor = Color.White,
                        ToolTipText = q.名称 + "(事件)",
                        ImageIndex = 0
                    };
                    __控件.Controls.Add(__事件控件);
                    this.out方法列表.TabPages.Add(__控件);
                });
            }
        }

        void do刷新属性_Click(object sender, EventArgs e)
        {
            var __对象明细 = _访问入口.查询对象明细(_对象名称);
            显示属性(__对象明细.属性列表);
        }

        private void 显示属性(List<M属性> __属性列表)
        {
            this.out属性.Rows.Clear();
            if (__属性列表 != null)
            {
                __属性列表.ForEach(q =>
                {
                    if ((q.角色 & _角色) != _角色)
                    {
                        return;
                    }

                    var __值 = _访问入口.查询属性值(_对象名称, q.名称);
                    if (q.元数据 != null)
                    {
                        this.out属性.Rows[this.out属性.Rows.Add(q.名称, q.元数据.类型, q.元数据.结构, __值, q.元数据.描述, q.元数据.默认值, q.元数据.范围)].Tag = q;
                    }
                    else
                    {
                        this.out属性.Rows[this.out属性.Rows.Add(q.名称, "", "", __值, "", "", "")].Tag = q;
                    }
                });
            }
        }

        void out属性_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //如果是非基元类型, 弹出明细
            var __属性 = this.out属性.Rows[e.RowIndex].Tag as M属性;
            M元数据 __元数据 = __属性.元数据;
            E数据结构 __数据结构;
            var __名称 = __属性.名称;
            var __值 = this.out属性.Rows[e.RowIndex].Cells[3].Value.ToString();
            if (__元数据 != null)
            {
                __数据结构 = __元数据.结构;
            }
            else
            {
                __数据结构 = HJSON.识别数据结构(__值);
            }

            switch (__数据结构)
            {
                case E数据结构.点:
                    if (__值.Length > 10)
                    {
                        new F点结构_查看(__元数据, __值, __名称).ShowDialog();
                    }
                    break;
                case E数据结构.行:
                    new F行结构_查看(__元数据 == null ? null : __元数据.子成员列表, __值, __名称).ShowDialog();
                    break;
                case E数据结构.列:
                    new F列结构_查看(__元数据, __值, __名称).ShowDialog();
                    break;
                case E数据结构.表:
                    new F表格结构_查看(__元数据 == null ? null : __元数据.子成员列表, __值, __名称).ShowDialog();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

    }
}
