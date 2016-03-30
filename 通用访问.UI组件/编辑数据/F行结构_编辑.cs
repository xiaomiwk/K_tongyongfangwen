using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using 通用访问.DTO;

namespace 通用访问.UI组件.编辑数据
{
    public partial class F行结构_编辑 : Form
    {
        private List<M子成员> _元数据列表;

        public string _值;

        private int _值所在列索引 = 3;

        public F行结构_编辑(List<M子成员> __元数据列表, string __值, string __标题 = "")
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

            var __dic = new Dictionary<string, string>();
            try
            {
                if (!string.IsNullOrEmpty(_值))
                {
                    var __对象 = JObject.Parse(_值);
                    foreach (JProperty __属性 in __对象.Properties())
                    {
                        __dic[__属性.Name] = __属性.Value.ToString();
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("值格式错误");
            }

            if (_元数据列表 != null)
            {
                foreach (var __kv in _元数据列表)
                {
                    var __名称 = __kv.名称;
                    var __元数据 = __kv.元数据;
                    var __默认值 = "";
                    switch (__元数据.结构)
                    {
                        case E数据结构.单值:
                            break;
                        case E数据结构.对象:
                            __默认值 = "{}";
                            break;
                        case E数据结构.单值数组:
                            __默认值 = "[]";
                            break;
                        case E数据结构.对象数组:
                            __默认值 = "[{}]";
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                    this.out值.Rows.Add(__名称, __元数据.类型, __元数据.结构, __dic.ContainsKey(__名称) ? __dic[__名称] : __默认值, __元数据.描述, __元数据.默认值, __元数据.范围);
                    __dic.Remove(__名称);
                }
            }
            else
            {
                foreach (var __kv in __dic)
                {
                    this.out值.Rows.Add(__kv.Key, "", "", __kv.Value, "", "", "");
                }
            }
            this.out值.CellDoubleClick += out属性_CellDoubleClick;
            this.do确定.Click += do确定_Click;
        }

        void do确定_Click(object sender, EventArgs e)
        {
            var __sw = new StringWriter();
            JsonWriter __writer = new JsonTextWriter(__sw);
            __writer.WriteStartObject();
            for (int i = 0; i < this.out值.Rows.Count; i++)
            {
                var __名称 = (string)this.out值[0, i].Value;
                var __元数据 = _元数据列表[i].元数据;
                var __value = this.out值[_值所在列索引, i].Value == null ? "" : this.out值[_值所在列索引, i].Value.ToString();
                __writer.WritePropertyName(__名称);
                if (__元数据.结构 == E数据结构.单值)
                {
                    switch (__元数据.类型)
                    {
                        case "string":
                        case "字符串":
                            __writer.WriteValue(__value);
                            break;
                        default:
                            __writer.WriteRawValue(__value);
                            break;
                    }
                }
                else
                {
                    __writer.WriteRawValue(__value);
                }
            }
            __writer.WriteEndObject();
            __writer.Flush();
            _值 = __sw.GetStringBuilder().ToString();
            this.DialogResult = DialogResult.OK;
        }

        void out属性_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            this.out值.EndEdit();
            var __名称 = (string)this.out值[0, e.RowIndex].Value;
            var __元数据 = _元数据列表[e.RowIndex].元数据;
            var __单元格 = this.out值[_值所在列索引, e.RowIndex];
            var __值 = __单元格.Value == null ? "" : __单元格.Value.ToString();
            if (__元数据.结构 != E数据结构.单值)
            {
                switch (__元数据.结构)
                {
                    case E数据结构.单值:
                        break;
                    case E数据结构.对象:
                        var __行窗口 = new F行结构_编辑(__元数据.子成员列表, __值, this.out标题.Text + " - " + __名称);
                        if (__行窗口.ShowDialog() == DialogResult.OK)
                        {
                            this.out值.Rows[e.RowIndex].Cells[_值所在列索引].Value = __行窗口._值;
                        }
                        break;
                    case E数据结构.单值数组:
                        var __列窗口 = new F列结构_编辑(__元数据, __值, this.out标题.Text + " - " + __名称);
                        if (__列窗口.ShowDialog() == DialogResult.OK)
                        {
                            this.out值.Rows[e.RowIndex].Cells[_值所在列索引].Value = __列窗口._值;
                        }
                        break;
                    case E数据结构.对象数组:
                        var __表格窗口 = new F表格结构_编辑(__元数据.子成员列表, __值, this.out标题.Text + " - " + __名称);
                        if (__表格窗口.ShowDialog() == DialogResult.OK)
                        {
                            this.out值.Rows[e.RowIndex].Cells[_值所在列索引].Value = __表格窗口._值;
                        }
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            this.out值.EndEdit();
        }
    }
}
