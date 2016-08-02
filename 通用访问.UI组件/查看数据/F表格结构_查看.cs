using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using Utility.通用;
using 通用访问;
using 通用访问.DTO;

namespace 通用访问.UI组件.查看数据
{
    public partial class F表格结构_查看 : Form
    {
        private List<M子成员> _元数据列表;

        private string _值;

        public F表格结构_查看(List<M子成员> __元数据列表, string __值, string __标题 = "")
        {
            _元数据列表 = __元数据列表;
            _值 = __值;
            InitializeComponent();
            this.out标题.Text = __标题;
            this.Text = __标题;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            var __数据表 = new DataTable();
            var __所有列 = new List<string>();
            if (_元数据列表 != null)
            {
                foreach (var __kv in _元数据列表)
                {
                    var __名称 = __kv.名称;
                    var __元数据 = __kv.元数据;
                    this.out元数据.Rows.Add(__名称, __元数据.类型, __元数据.结构, __元数据.描述, __元数据.默认值, __元数据.范围);
                    __所有列.Add(__名称);
                }
            }
            else
            {
                this.splitContainer1.Panel2Collapsed = true;
            }

            try
            {
                var __数组 = JArray.Parse(_值);
                foreach (JObject __对象 in __数组)
                {
                    foreach (JProperty __属性 in __对象.Properties())
                    {
                        if (!__所有列.Contains(__属性.Name))
                        {
                            __所有列.Add(__属性.Name);
                        }
                    }
                }
                __所有列.ForEach(q => __数据表.Columns.Add(q));

                foreach (JObject __对象 in __数组)
                {
                    var __row = __数据表.NewRow();
                    foreach (JProperty __属性 in __对象.Properties())
                    {
                        __row[__属性.Name] = __属性.Value;
                    }
                    __数据表.Rows.Add(__row);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("表结构解析失败: " + ex.Message + Environment.NewLine +_值);
                H调试.记录异常(ex , _值);
            }
            this.out值.DataSource = __数据表;
            this.out值.CellMouseDoubleClick += out值_CellMouseDoubleClick;
        }

        void out值_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            var __单元格 = this.out值[e.ColumnIndex, e.RowIndex].Value;
            var __值 = __单元格 == null ? "" : __单元格.ToString();
            var __名称 = (string)this.out值.Columns[e.ColumnIndex].Name;
            M元数据 __元数据 = null;
            E数据结构 __数据结构;
            if (_元数据列表 != null && e.ColumnIndex < _元数据列表.Count)
            {
                __元数据 = _元数据列表[e.ColumnIndex].元数据;
                __数据结构 = __元数据.结构;
            }
            else
            {
                __数据结构 = HJSON.识别数据结构(__值);
            }
            switch (__数据结构)
            {
                case E数据结构.单值:
                    if (__值.Length > 10)
                    {
                        new F点结构_查看(__元数据, __值, __名称).ShowDialog();
                    }
                    break;
                case E数据结构.对象:
                    new F行结构_查看(__元数据 == null ? null : __元数据.子成员列表, string.IsNullOrEmpty(__值) ? "{}" : __值, this.out标题.Text + " - " + __名称).ShowDialog();
                    break;
                case E数据结构.单值数组:
                    new F列结构_查看(__元数据, string.IsNullOrEmpty(__值) ? "[]" : __值, this.out标题.Text + " - " + __名称).ShowDialog();
                    break;
                case E数据结构.对象数组:
                    new F表格结构_查看(__元数据 == null ? null : __元数据.子成员列表, string.IsNullOrEmpty(__值) ? "[{}]" : __值, this.out标题.Text + " - " + __名称).ShowDialog();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

    }
}
