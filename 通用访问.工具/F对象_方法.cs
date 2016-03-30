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
using 通用访问工具.编辑数据;

namespace 通用访问工具
{
    public partial class F对象_方法 : UserControl
    {
        private M方法 _方法;

        private int _值所在列索引 = 3;

        private IT客户端 _IT客户端;

        private string _对象名称;
        public F对象_方法(IT客户端 __IT客户端, string __对象名称, M方法 __方法)
        {
            _IT客户端 = __IT客户端;
            _对象名称 = __对象名称;
            _方法 = __方法;
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.do执行.Click += do执行_Click;
            this.do解析.Click += do解析_Click;
            this.out值.CellMouseDoubleClick += out方法_CellMouseDoubleClick;
            if (_方法.形参列表 != null)
            {
                _方法.形参列表.ForEach(q =>
                {
                    this.out值.Rows[this.out值.Rows.Add(q.名称, q.元数据.类型, q.元数据.结构, q.元数据.默认值, q.元数据.默认值, q.元数据.描述, q.元数据.范围)].Tag = q;
                });
            }
            this.splitContainer1.Panel1Collapsed = _方法.形参列表 == null;

            this.do解析.Visible = false;

            this.do查看返回值元数据.Enabled = _方法.返回值元数据 != null;
            this.do查看返回值元数据.Click += do查看返回值元数据_Click;
        }

        void do查看返回值元数据_Click(object sender, EventArgs e)
        {
            this.out返回值.Text = HJSON.序列化(_方法.返回值元数据);
        }

        void out方法_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            var __名称 = (string)this.out值[0, e.RowIndex].Value;
            var __元数据 = _方法.形参列表[e.RowIndex].元数据;
            var __单元格 = this.out值[_值所在列索引, e.RowIndex];
            var __值 = __单元格.Value == null ? "" : __单元格.Value.ToString();
            switch (__元数据.结构)
            {
                case E数据结构.点:
                    break;
                case E数据结构.行:
                    var __行窗口 = new F行结构_编辑(__元数据.子成员列表, __值, __名称);
                    if (__行窗口.ShowDialog() == DialogResult.OK)
                    {
                        this.out值[_值所在列索引, e.RowIndex].Value = __行窗口._值;
                    }
                    break;
                case E数据结构.列:
                    var __列窗口 = new F列结构_编辑(__元数据, __值, __名称);
                    if (__列窗口.ShowDialog() == DialogResult.OK)
                    {
                        this.out值[_值所在列索引, e.RowIndex].Value = __列窗口._值;
                    }
                    break;
                case E数据结构.表:
                    var __表格窗口 = new F表格结构_编辑(__元数据.子成员列表, __值, __名称);
                    if (__表格窗口.ShowDialog() == DialogResult.OK)
                    {
                        this.out值[_值所在列索引, e.RowIndex].Value = __表格窗口._值;
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            this.out值.EndEdit();
        }

        void do解析_Click(object sender, EventArgs e)
        {
            var __元数据 = _方法.返回值元数据;
            var __值 = (string)this.do解析.Tag;
            var __数据结构 = __元数据 == null ? HJSON.识别数据结构(__值) : _方法.返回值元数据.结构;
            var __标题 = "返回值 - " + (__元数据 == null ? "" : __元数据.类型);
            switch (__数据结构)
            {
                case E数据结构.点:
                    if (__值.Length > 10)
                    {
                        new F点结构_查看(__元数据, __值, "返回值").ShowDialog();
                    }
                    break;
                case E数据结构.行:
                    new F行结构_查看(__元数据 == null ? null : __元数据.子成员列表, __值, __标题).ShowDialog();
                    break;
                case E数据结构.列:
                    new F列结构_查看(__元数据, __值, __标题).ShowDialog();
                    break;
                case E数据结构.表:
                    new F表格结构_查看(__元数据 == null ? null : __元数据.子成员列表, __值, __标题).ShowDialog();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        void do执行_Click(object sender, EventArgs e)
        {
            var __参数列表 = new List<M实参>();
            for (int i = 0; i < this.out值.Rows.Count; i++)
            {
                var __row = this.out值.Rows[i];
                var __名称 = (string) __row.Cells[0].Value;
                var __值 = (string) __row.Cells[3].Value;
                var __默认值 = "";
                switch (_方法.形参列表[i].元数据.结构)
                {
                    case E数据结构.点:
                        break;
                    case E数据结构.行:
                        __默认值 = "{}";
                        break;
                    case E数据结构.列:
                        __默认值 = "[]";
                        break;
                    case E数据结构.表:
                        __默认值 = "[{}]";
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                __参数列表.Add(new M实参
                {
                    名称 = __名称,
                    值 = __值 ?? __默认值,
                });
            }
            var __返回值 = _IT客户端.执行方法(_对象名称,_方法.名称, __参数列表);
            设置返回值(__返回值);
        }

        public void 设置返回值(string __返回值)
        {
            var __数据结构 = _方法.返回值元数据 == null ? HJSON.识别数据结构(__返回值) : _方法.返回值元数据.结构;
            this.out返回值.Text = string.Format("成功   ({1})\r\n返回值: {0}", __返回值, DateTime.Now.ToLongTimeString());
            if (__数据结构 == E数据结构.点)
            {
            }
            else
            {
                this.do解析.Visible = true;
                this.do解析.Tag = __返回值;
            }
        }
    }
}
