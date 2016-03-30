using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using Utility.通用;
using 通用访问;
using 通用访问.DTO;

namespace 通用访问.UI组件.查看数据
{
    public partial class F行结构_查看 : Form
    {
        private List<M子成员> _元数据列表;

        private string _值;

        private int _值所在列索引 = 3;

        public F行结构_查看(List<M子成员> __元数据列表, string __值, string __标题 = "")
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

            try
            {
                var __对象 = JObject.Parse(_值);
                var __dic = new Dictionary<string, string>();
                foreach (JProperty __属性 in __对象.Properties())
                {
                    __dic[__属性.Name] = __属性.Value.ToString();
                }
                if (_元数据列表 != null)
                {
                    foreach (var __kv in _元数据列表)
                    {
                        var __名称 = __kv.名称;
                        var __元数据 = __kv.元数据;
                        this.out值.Rows.Add(__名称, __元数据.类型, __元数据.结构, __dic.ContainsKey(__名称) ? __dic[__名称] : "", __元数据.描述, __元数据.默认值, __元数据.范围);
                        __dic.Remove(__名称);
                    }
                }
                foreach (var __kv in __dic)
                {
                    this.out值.Rows.Add(__kv.Key, "", "", __kv.Value, "", "", "");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("行结构解析失败: " + ex.Message + Environment.NewLine + _值);
                H调试.记录异常(ex, _值);
            }
            this.out值.CellDoubleClick += out属性_CellDoubleClick;
        }

        void out属性_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            var __名称 = (string)this.out值[0, e.RowIndex].Value;
            var __单元格 = this.out值[_值所在列索引, e.RowIndex].Value;
            var __值 = __单元格 == null ? "" : __单元格.ToString();
            M元数据 __元数据 = null;
            E数据结构 __数据结构;
            if (_元数据列表 != null && e.RowIndex < _元数据列表.Count)
            {
                __元数据 = _元数据列表[e.RowIndex].元数据;
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
