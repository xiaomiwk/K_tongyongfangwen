using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Utility.WindowsForm
{
    public class TreeViewK : TreeView
    {
        protected override void WndProc(ref Message m)
        {
            //过滤双击
            if (m.Msg == 0x203)
            {
                m.Result = IntPtr.Zero;
            }
            else
            {
                base.WndProc(ref m);
            }
        }
    }
}
