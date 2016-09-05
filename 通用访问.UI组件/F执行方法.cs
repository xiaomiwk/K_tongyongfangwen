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
using 通用访问.UI组件.查看数据;
using 通用访问.UI组件.编辑数据;

namespace 通用访问.UI组件
{
    public partial class F执行方法 : UserControl
    {
        private IT客户端 _IT客户端;

        private M方法 _方法;

        private string _对象;

        private int _值所在列索引 = 3;

        public F执行方法(IT客户端 __IT客户端)
        {
            _IT客户端 = __IT客户端;
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.do执行.Click += do执行_Click;
            this.do解析.Click += do解析_Click;
            this.do解析2.Click += do解析_Click;
            this.out值.CellMouseDoubleClick += out方法_CellMouseDoubleClick;
            this.do清空.Click += do清空_Click;
            this.do解析.Visible = false;
            this.do解析2.Visible = false;
            this.splitContainer1.Panel1Collapsed = true;
            this.do导出.Click += do导出_Click;
            this.out命令.Text = "";
        }

        void do导出_Click(object sender, EventArgs e)
        {
            SaveFileDialog __对话框 = new SaveFileDialog() { Filter = "rtf|*.rtf|txt|*.txt" };
            if (__对话框.ShowDialog() == DialogResult.OK)
            {
                this.out返回值.SaveFile(__对话框.FileName);
            }
        }

        void do清空_Click(object sender, EventArgs e)
        {
            this.out返回值.Clear();
        }

        public void 设置当前方法(string __对象, M方法 __方法, bool __自动执行 = true)
        {
            _对象 = __对象;
            _方法 = __方法;
            this.out命令.Text = string.Format("{0}.{1}", _对象, _方法.名称);
            this.out值.Rows.Clear();
            if (_方法.形参列表 != null)
            {
                _方法.形参列表.ForEach(q =>
                {
                    this.out值.Rows[this.out值.Rows.Add(q.名称, q.元数据.类型, q.元数据.结构, q.元数据.默认值, q.元数据.默认值, q.元数据.描述, q.元数据.范围)].Tag = q;
                });
            }
            if (__自动执行 && (_方法.形参列表 == null || _方法.形参列表.Count == 0))
            {
                执行方法();
                this.splitContainer1.Panel1Collapsed = true;
            }
            else
            {
                this.splitContainer1.Panel1Collapsed = false;
            }
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
                case E数据结构.单值:
                    break;
                case E数据结构.对象:
                    var __行窗口 = new F行结构_编辑(__元数据.子成员列表, __值, __名称);
                    if (__行窗口.ShowDialog() == DialogResult.OK)
                    {
                        this.out值[_值所在列索引, e.RowIndex].Value = __行窗口._值;
                    }
                    break;
                case E数据结构.单值数组:
                    var __列窗口 = new F列结构_编辑(__元数据, __值, __名称);
                    if (__列窗口.ShowDialog() == DialogResult.OK)
                    {
                        this.out值[_值所在列索引, e.RowIndex].Value = __列窗口._值;
                    }
                    break;
                case E数据结构.对象数组:
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
                case E数据结构.单值:
                    break;
                case E数据结构.对象:
                    new F行结构_查看(__元数据 == null ? null : __元数据.子成员列表, __值, __标题).ShowDialog();
                    break;
                case E数据结构.单值数组:
                    new F列结构_查看(__元数据, __值, __标题).ShowDialog();
                    break;
                case E数据结构.对象数组:
                    new F表格结构_查看(__元数据 == null ? null : __元数据.子成员列表, __值, __标题).ShowDialog();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        void do执行_Click(object sender, EventArgs e)
        {
            执行方法();
        }

        private void 执行方法()
        {
            var __参数列表 = new Dictionary<string,string>();
            for (int i = 0; i < this.out值.Rows.Count; i++)
            {
                var __row = this.out值.Rows[i];
                var __名称 = (string) __row.Cells[0].Value;
                var __值 = (string) __row.Cells[3].Value;
                __参数列表[__名称] = __值 ?? "";
            }
            var __秒表 = new System.Diagnostics.Stopwatch();
            __秒表.Start();
            var __返回值 = _IT客户端.执行方法(_对象, _方法.名称, __参数列表);
            __秒表.Stop();
            输出(string.IsNullOrEmpty(__返回值)
                ? string.Format("\r\n{2}  执行 [{0}.{1}] 成功, 耗时: {3}毫秒\r\n", _对象, _方法.名称, DateTime.Now.ToLongTimeString(),
                    __秒表.ElapsedMilliseconds)
                : string.Format("\r\n{2}  执行 [{0}.{1}] 成功, 耗时: {4}毫秒\r\n{3}\r\n", _对象, _方法.名称,
                    DateTime.Now.ToLongTimeString(), __返回值, __秒表.ElapsedMilliseconds));
            var __数据结构 = _方法.返回值元数据 == null ? HJSON.识别数据结构(__返回值) : _方法.返回值元数据.结构;
            if (__数据结构 == E数据结构.单值)
            {
                this.do解析.Visible = false;
                this.do解析.Tag = __返回值;
                this.do解析2.Visible = false;
                this.do解析2.Tag = __返回值;
            }
            else
            {
                this.do解析.Visible = true;
                this.do解析.Tag = __返回值;
                this.do解析2.Visible = true;
                this.do解析2.Tag = __返回值;
            }
        }

        public void 输出(string __文本)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Action<string>(输出), __文本);
                return;
            }

            this.out返回值.AppendText(__文本);
            this.out返回值.ScrollToCaret();
        }
    }
}
