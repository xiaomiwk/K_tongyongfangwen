using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Utility.WindowsForm
{
    public class U按钮_轻 : Button
    {
        public Color 颜色
        {
            get { return this.BackColor; }
            set { this.BackColor = value; }
        }

        public Color 文字颜色
        {
            get { return this.ForeColor; }
            set { this.ForeColor = value; }
        }

        public Size 大小
        {
            get { return this.Size; }
            set { this.Size = value; }
        }

        public U按钮_轻()
        {
            this.BackColor = System.Drawing.Color.White;
            this.ForeColor = System.Drawing.Color.FromArgb(38, 164, 221);
            this.Size = new System.Drawing.Size(100, 26);
            this.FlatAppearance.BorderSize = 0;
            this.FlatStyle = FlatStyle.Flat;
            this.Location = new System.Drawing.Point(3, 2);
            this.UseVisualStyleBackColor = false;
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            //this.BackColor = System.Drawing.Color.FromArgb(38, 164, 221);
            this.BackColor = 颜色;
            this.Size = 大小;
        }
    }
}
