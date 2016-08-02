using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Utility.WindowsForm
{
    public interface I覆盖控件
    {
        void 隐藏();

        event Action 已隐藏;
    }

    public static class H覆盖控件
    {
        public static void 创建全覆盖控件<T>(this Control 初始控件, T 覆盖控件, Action<T> 处理覆盖控件操作完毕)
            where T : Control, I覆盖控件
        {
            var __容器 = 初始控件.Parent ?? 初始控件;
            覆盖控件.Width = 初始控件.Width;
            覆盖控件.Height = 初始控件.Height;
            __容器.Controls.Add(覆盖控件);
            覆盖控件.Left = 初始控件.Left;
            覆盖控件.Top = 初始控件.Top;
            覆盖控件.Anchor = 初始控件.Anchor;
            //__容器.Controls.SetChildIndex(覆盖控件, 0);
            覆盖控件.BringToFront();
            覆盖控件.Focus();
            覆盖控件.已隐藏 += () =>
            {
                //H调试.记录("创建全覆盖控件", "覆盖控件.Visible = " + 覆盖控件.Visible + " 覆盖控件.Created = " + 覆盖控件.Created);
                if (处理覆盖控件操作完毕 != null)
                {
                    处理覆盖控件操作完毕(覆盖控件);
                }
                if (__容器 != null)
                {
                    __容器.Controls.Remove(覆盖控件);
                }
                if (覆盖控件.Created)
                {
                    覆盖控件.Dispose();
                }
            };
            Application.DoEvents();
        }

        public static void 创建局部覆盖控件<T>(this Control 初始控件, T 覆盖控件, Action<T> 处理覆盖控件操作完毕, bool 禁用 = true)
            where T : Control, I覆盖控件
        {
            var __容器 = 初始控件.Parent ?? 初始控件;
            var x = Math.Max(10, (__容器.Width - 覆盖控件.Width) / 2);
            var y = Math.Max(10, (__容器.Height - 覆盖控件.Height) / 2);
            创建局部覆盖控件(初始控件, 覆盖控件, 处理覆盖控件操作完毕, x, y, 禁用);
        }

        public static void 创建局部覆盖控件<T>(this Control 初始控件, T 覆盖控件, Action<T> 处理覆盖控件操作完毕, int X, int Y, bool 禁用 = true)
            where T : Control, I覆盖控件
        {
            var __容器 = 初始控件.Parent ?? 初始控件;

            __容器.Controls.Add(覆盖控件);
            覆盖控件.Location = new Point(X, Y);
            //__容器.Controls.SetChildIndex(覆盖控件, 0);
            覆盖控件.BringToFront();
            覆盖控件.Focus();
            初始控件.Enabled = !禁用;
            覆盖控件.已隐藏 += () =>
            {
                //H调试.记录("创建局部覆盖控件1", "覆盖控件.Visible = " + 覆盖控件.Visible + " 覆盖控件.Created = " + 覆盖控件.Created);
                if (处理覆盖控件操作完毕 != null)
                {
                    处理覆盖控件操作完毕(覆盖控件);
                }
                if (__容器 != null)
                {
                    __容器.Controls.Remove(覆盖控件);
                }
                初始控件.Enabled = true;
                if (覆盖控件.Created)
                {
                    覆盖控件.Dispose();
                }
            };
            Application.DoEvents();
        }

    }

}
