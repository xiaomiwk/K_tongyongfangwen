using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using 通用访问.DTO;

namespace 通用访问.UI组件.编辑数据
{
    public partial class F表格结构_编辑 : Form
    {
        private List<M子成员> _元数据列表;

        public string _值;

        public F表格结构_编辑(List<M子成员> __元数据列表, string __值, string __标题 = "")
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
            if (_元数据列表 != null)
            {
                foreach (var __kv in _元数据列表)
                {
                    var __名称 = __kv.名称;
                    var __元数据 = __kv.元数据;
                    this.out元数据.Rows.Add(__名称, __元数据.类型, __元数据.结构, __元数据.描述, __元数据.默认值, __元数据.范围);
                    this.out值.Columns.Add(__名称, __名称);
                    __数据表.Columns.Add(__名称);
                }
            }
            else
            {
                this.splitContainer1.Panel2Collapsed = true;
            }

            try
            {
                if (!string.IsNullOrEmpty(_值))
                {
                    var __数组 = JArray.Parse(_值);
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
            }
            catch (Exception)
            {
                MessageBox.Show("值格式错误");
            }

            foreach (DataRow __row in __数据表.Rows)
            {
                var __值列表 = new List<object>();
                foreach (var __cell in __row.ItemArray)
                {
                    __值列表.Add(__cell == null ? "" : __cell.ToString());
                }
                this.out值.Rows.Add(__值列表.ToArray());

            }
            this.out值.CellMouseDoubleClick += out值_CellMouseDoubleClick;
            this.do确定.Click += do确定_Click;
        }

        void do确定_Click(object sender, EventArgs e)
        {
            var __sw = new StringWriter();
            var __writer = new JsonTextWriter(__sw);
            __writer.WriteStartArray();
            for (int i = 0; i < this.out值.Rows.Count; i++)
            {
                if (this.out值.Rows[i].IsNewRow)
                {
                    continue;
                }
                __writer.WriteStartObject();
                for (int j = 0; j < this.out值.Columns.Count; j++)
                {
                    var __名称 = this.out值.Columns[j].Name;
                    var __元数据 = _元数据列表[j].元数据;
                    var __value = this.out值[j, i].Value == null ? "" : this.out值[j, i].Value.ToString();
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
            }
            __writer.WriteEndArray();
            __writer.Flush();
            _值 = __sw.GetStringBuilder().ToString();
            this.DialogResult = DialogResult.OK;
        }

        void out值_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            this.out值.EndEdit();
            var __名称 = this.out值.Columns[e.ColumnIndex].Name;
            var __元数据 = _元数据列表[e.ColumnIndex].元数据;
            var __单元格 = this.out值[e.ColumnIndex, e.RowIndex];
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
                            this.out值[e.ColumnIndex,e.RowIndex].Value = __行窗口._值;
                        }
                        break;
                    case E数据结构.单值数组:
                        var __列窗口 = new F列结构_编辑(__元数据, __值, this.out标题.Text + " - " + __名称);
                        if (__列窗口.ShowDialog() == DialogResult.OK)
                        {
                            this.out值[e.ColumnIndex, e.RowIndex].Value = __列窗口._值;
                        }
                        break;
                    case E数据结构.对象数组:
                        var __表格窗口 = new F表格结构_编辑(__元数据.子成员列表, __值, this.out标题.Text + " - " + __名称);
                        if (__表格窗口.ShowDialog() == DialogResult.OK)
                        {
                            this.out值[e.ColumnIndex, e.RowIndex].Value = __表格窗口._值;
                        }
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

    }
}
