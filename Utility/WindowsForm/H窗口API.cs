using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Utility.WindowsForm
{
    public static class H窗口API
    {

        [DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, int msg, int wParam, IntPtr lParam);

        public static void 滚动到最下面(this Control __带滚动条控件)
        {
            const int WM_VSCROLL = 0x115;
            const int SB_BOTTOM = 7;
            SendMessage(__带滚动条控件.Handle, WM_VSCROLL, SB_BOTTOM, new IntPtr(0));
        }

    }
}
