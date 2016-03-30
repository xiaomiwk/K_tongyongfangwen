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

namespace 通用访问.UI组件
{
    public partial class F对象_事件 : UserControl
    {
        private M事件 _事件;

        private int _值所在列索引 = 3;

        private IT客户端 _IT客户端;

        private string _对象名称;

        public F对象_事件(IT客户端 __IT客户端, string __对象名称, M事件 __事件)
        {
            _IT客户端 = __IT客户端;
            _对象名称 = __对象名称;
            _事件 = __事件;
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.do订阅.Click += do订阅_Click;
            this.do取消订阅.Click += do取消订阅_Click;
            this.do清空记录.Click += (m, n) => this.out记录.Clear();
            this.out值.CellMouseDoubleClick += out方法_CellMouseDoubleClick;
            if (_事件.形参列表 != null)
            {
                _事件.形参列表.ForEach(q =>
                {
                    this.out值.Rows[this.out值.Rows.Add(q.名称, q.元数据.类型, q.元数据.结构, q.元数据.默认值, q.元数据.默认值, q.元数据.描述, q.元数据.范围)].Tag = q;
                });
            }
            this.splitContainer1.Panel1Collapsed = _事件.形参列表 == null;
            this.do取消订阅.Enabled = false;
        }

        void out方法_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            var __名称 = (string)this.out值[0, e.RowIndex].Value;
            var __元数据 = _事件.形参列表[e.RowIndex].元数据;
            var __单元格 = this.out值[_值所在列索引, e.RowIndex];
            var __值 = __单元格.Value == null ? "" : __单元格.Value.ToString();
            switch (__元数据.结构)
            {
                case E数据结构.单值:
                    break;
                case E数据结构.对象:
                    var __行窗口 = new F行结构_查看(__元数据.子成员列表, "{}", __名称);
                    __行窗口.ShowDialog();
                    break;
                case E数据结构.单值数组:
                    var __列窗口 = new F列结构_查看(__元数据, "[]", __名称);
                    __列窗口.ShowDialog();
                    break;
                case E数据结构.对象数组:
                    var __表格窗口 = new F表格结构_查看(__元数据.子成员列表, "[{}]", __名称);
                    __表格窗口.ShowDialog();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        void do订阅_Click(object sender, EventArgs e)
        {
            _IT客户端.订阅事件(_对象名称, _事件.名称, 处理事件);
            this.do订阅.Enabled = false;
            this.do取消订阅.Enabled = true;
        }

        void do取消订阅_Click(object sender, EventArgs e)
        {
            _IT客户端.注销事件(_对象名称, _事件.名称, 处理事件);
            this.do订阅.Enabled = true;
            this.do取消订阅.Enabled = false;
        }

        private void 处理事件(Dictionary<string, string> __参数)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Action<Dictionary<string, string>>(处理事件), __参数);
                return;
            }
            this.out记录.AppendText(string.Format("{0}    {1}\r\n\r\n", DateTime.Now.ToString("MM-dd HH:mm:ss"), HJSON.序列化(__参数)));
        }
    }
}
