using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Utility.WindowsForm
{
    [Designer("System.Windows.Forms.Design.ParentControlDesigner, System.Design", typeof(IDesigner))]
    public class U窗口背景 : UserControl
    {
        public Color 边框颜色 { get; set; }

        public Color 面板颜色 { get; set; }

        private Label __左边;
        private Label __右边;
        private Label __上边;
        private Label __下边;
        public U窗口背景()
        {
            边框颜色 = Color.Gainsboro;

            InitializeComponent();
            __左边 = new Label { BackColor = 边框颜色, Dock = DockStyle.Left, Width = 1 };
            __右边 = new Label { BackColor = 边框颜色, Dock = DockStyle.Right, Width = 1 };
            __上边 = new Label { BackColor = 边框颜色, Dock = DockStyle.Top, Height = 1 };
            __下边 = new Label { BackColor = 边框颜色, Dock = DockStyle.Bottom, Height = 1 };
            this.Controls.Add(__左边);
            this.Controls.Add(__右边);
            this.Controls.Add(__上边);
            this.Controls.Add(__下边);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.BackColor = 面板颜色;
            __左边.BackColor = 边框颜色;
            __右边.BackColor = 边框颜色;
            __上边.BackColor = 边框颜色;
            __下边.BackColor = 边框颜色;
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // U窗口背景
            // 
            this.BackColor = System.Drawing.Color.White;
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "U窗口背景";
            this.Size = new System.Drawing.Size(617, 443);
            this.ResumeLayout(false);

        }
    }
}
