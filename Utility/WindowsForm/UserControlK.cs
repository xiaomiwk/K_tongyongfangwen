using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Utility.WindowsForm
{
    public class UserControlK : UserControl
    {
        public UserControlK()
        {

        }

        protected override void OnLoad(EventArgs e)
        {            
            base.OnLoad(e);
            this.BackColor = System.Drawing.Color.White;
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
        }
    }
}
