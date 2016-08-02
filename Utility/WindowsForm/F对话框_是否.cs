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
    public partial class F对话框_是否 : FormK
    {
        public F对话框_是否(string __内容, string __标题 = "")
        {
            InitializeComponent();

            this.u窗体头1.标题 = __标题;
            this.Text = string.IsNullOrEmpty(__标题) ? "对话框" : __标题;

            this.out消息.Text = __内容;
            if (__内容.Length > 30 || __内容.Count(q => q == '\n') > 2)
            {
                this.out消息.ScrollBars = ScrollBars.Both;
            }

            this.do是.Click += do是_Click;
            this.do否.Click += do否_Click;
        }

        void do否_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.No;
            this.Close();
        }

        void do是_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.Close();
        }
    }
}
