using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Utility.WindowsForm
{
    public class FormK : Form
    {
        protected override void OnLoad(EventArgs e)
        {            
            base.OnLoad(e);
            if (!DesignMode)
            {
                this.MaximumSize = Screen.FromControl(this).WorkingArea.Size;
                //this.StartPosition = FormStartPosition.CenterScreen;
                this.Left = (this.MaximumSize.Width - this.Width) / 2;
                this.Top = (this.MaximumSize.Height - this.Height) / 2;
            }
            this.BackColor = System.Drawing.Color.White;
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
        }

        protected override CreateParams CreateParams
        {
            get
            {
                const int WS_MINIMIZEBOX = 0x00020000;
                CreateParams cp = base.CreateParams;
                cp.Style = cp.Style | WS_MINIMIZEBOX;
                return cp;
                //return base.CreateParams;
            }
        }
    }
}
