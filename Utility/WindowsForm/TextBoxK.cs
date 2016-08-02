using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Utility.WindowsForm
{
    public class TextBoxK : TextBox
    {
        public string 水印 { get; set; }

        private Color _水印颜色 = SystemColors.ControlDark;

        public Color 水印颜色 {
            get { return _水印颜色; }
            set { _水印颜色 = value; }
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);
            //Debug.WriteLine("OnVisibleChanged");
            if (this.Text == "")
            {
                this.Text = 水印;
            }
            if (this.Text == 水印)
            {
                this.ForeColor = 水印颜色;
            }
            else
            {
                this.ForeColor = DefaultForeColor;
            }
        }

        protected override void OnLeave(EventArgs e)
        {
            base.OnLeave(e);
            if (this.Text == "")
            {
                this.Text = 水印;
                this.ForeColor = 水印颜色;
            }
            else
            {
                this.ForeColor = DefaultForeColor;
            }
        }

        protected override void OnEnter(EventArgs e)
        {
            base.OnEnter(e);
            if (this.Text == 水印)
            {
                this.Clear();
                this.ForeColor = DefaultForeColor;
            }
        }


    }
}
