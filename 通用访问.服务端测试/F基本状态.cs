using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;
using Utility.通用;
using 通用访问.服务端测试.DTO;
using 通用访问.服务端测试.IBLL;

namespace 通用访问.服务端测试
{
    public partial class F基本状态 : UserControl
    {
        private IB基本状态 _IB基本状态 = H容器.取出<IB基本状态>();

        public F基本状态()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            var __重要性 = Enum.GetNames(typeof (E重要性));
            this.in问题1_等级.Items.Clear();
            this.in问题1_等级.Items.AddRange(__重要性);
            this.in问题2_等级.Items.Clear();
            this.in问题2_等级.Items.AddRange(__重要性);
            设置状态();
            this.do更新.Click += do更新_Click;
        }

        private void 设置状态()
        {
            var __状态 = _IB基本状态.查询();
            this.out版本.Text = __状态.版本;
            this.out开启时间.Text = __状态.开启时间.ToString("yyyy-MM-dd hh:mm:ss");
            this.out位置.Text = __状态.位置;
            if (__状态.连接设备 != null && __状态.连接设备.Count > 0)
            {
                var __设备 = __状态.连接设备[0];
                this.in设备1_IP.Text = __设备.IP.ToString();
                this.in设备1_标识.Text = __设备.标识;
                this.in设备1_类型.Text = __设备.类型;
                this.in设备1_连接中.Checked = __设备.连接中;
            }
            else
            {
                this.in设备1_IP.Text = "";
                this.in设备1_标识.Text = "";
                this.in设备1_类型.Text = "";
                this.in设备1_连接中.Checked = false;
            }
            if (__状态.连接设备 != null && __状态.连接设备.Count > 1)
            {
                var __设备 = __状态.连接设备[1];
                this.in设备2_IP.Text = __设备.IP.ToString();
                this.in设备2_标识.Text = __设备.标识;
                this.in设备2_类型.Text = __设备.类型;
                this.in设备2_连接中.Checked = __设备.连接中;
            }
            else
            {
                this.in设备2_IP.Text = "";
                this.in设备2_标识.Text = "";
                this.in设备2_类型.Text = "";
                this.in设备2_连接中.Checked = false;
            }

            if (__状态.待处理问题 != null && __状态.待处理问题.Count > 0)
            {
                var __问题 = __状态.待处理问题[0];
                this.in问题1_等级.SelectedItem = __问题.等级.ToString();
                this.in问题1_内容.Text = __问题.内容;
            }
            else
            {
                this.in问题1_等级.SelectedItem = "普通";
                this.in问题1_内容.Text = "";
            }
            if (__状态.待处理问题 != null && __状态.待处理问题.Count > 1)
            {
                var __问题 = __状态.待处理问题[1];
                this.in问题2_等级.SelectedItem = __问题.等级.ToString();
                this.in问题2_内容.Text = __问题.内容;
            }
            else
            {
                this.in问题2_等级.SelectedItem = "普通";
                this.in问题2_内容.Text = "";
            }

            if (__状态.业务状态.Keys.Count > 0)
            {
                this.in状态1_名称.Text = __状态.业务状态.Keys.First();
                this.in状态1_值.Text = __状态.业务状态.Values.First();
            }
            else
            {
                this.in状态1_名称.Text = "";
                this.in状态1_值.Text = "";
            }
            if (__状态.业务状态.Keys.Count > 1)
            {
                this.in状态2_名称.Text = __状态.业务状态.Keys.Last();
                this.in状态2_值.Text = __状态.业务状态.Values.Last();
            }
            else
            {
                this.in状态2_名称.Text = "";
                this.in状态2_值.Text = "";
            }

        }

        void do更新_Click(object sender, EventArgs e)
        {
            var __状态 = new M基本状态();
            __状态.连接设备.Add(new M设备连接 { IP = IPAddress.Parse(this.in设备1_IP.Text), 标识 = this.in设备1_标识.Text, 类型 = this.in设备1_类型.Text, 连接中 = this.in设备1_连接中.Checked });
            __状态.连接设备.Add(new M设备连接 { IP = IPAddress.Parse(this.in设备2_IP.Text), 标识 = this.in设备2_标识.Text, 类型 = this.in设备2_类型.Text, 连接中 = this.in设备2_连接中.Checked });
            if (!this.in问题1_删除.Checked)
            {
                __状态.待处理问题.Add(new M问题 { 等级 = (E重要性)Enum.Parse(typeof(E重要性), this.in问题1_等级.SelectedItem.ToString()), 内容 = this.in问题1_内容.Text });
            }
            if (!this.in问题2_删除.Checked)
            {
                __状态.待处理问题.Add(new M问题 { 等级 = (E重要性)Enum.Parse(typeof(E重要性), this.in问题2_等级.SelectedItem.ToString()), 内容 = this.in问题2_内容.Text });
            }
            if (!string.IsNullOrEmpty(this.in状态1_名称.Text))
            {
                __状态.业务状态[this.in状态1_名称.Text] = this.in状态1_值.Text;
            }
            if (!string.IsNullOrEmpty(this.in状态2_名称.Text))
            {
                __状态.业务状态[this.in状态2_名称.Text] = this.in状态2_值.Text;
            }
            _IB基本状态.更新(__状态);
        }
    }
}
