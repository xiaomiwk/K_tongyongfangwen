using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Utility.WindowsForm
{
    public partial class F对话框_确定取消 : FormK
    {
        public F对话框_确定取消(string __内容, string __标题 = "")
        {
            InitializeComponent();

            this.u窗体头1.标题 = __标题;
            this.Text = string.IsNullOrEmpty(__标题) ? "对话框" : __标题;

            this.out消息.Text = __内容;
            if (__内容.Length > 30 || __内容.Count(q => q == '\n') > 2)
            {
                this.out消息.ScrollBars = ScrollBars.Both;
            }

            this.do确定.Click += Do确定Click;
            this.do取消.Click += do取消_Click;
        }

        void do取消_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        void Do确定Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
    }
}
