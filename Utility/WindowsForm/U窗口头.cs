using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Utility.Properties;

namespace Utility.WindowsForm
{
    [Designer("System.Windows.Forms.Design.ParentControlDesigner, System.Design", typeof(IDesigner))]
    public partial class U窗体头 : UserControl
    {
        private bool _按下鼠标;

        private Point _按下鼠标时位置;

        public bool 显示最大化按钮
        {
            get { return this.do最大化.Visible; }
            set
            {
                this.do最大化.Visible = value;
                设置按钮容器大小();
            }
        }

        public bool 显示最小化按钮
        {
            get { return this.do最小化.Visible; }
            set
            {
                this.do最小化.Visible = value;
                设置按钮容器大小();
            }
        }

        public bool 显示设置按钮
        {
            get { return this.do设置.Visible; }
            set
            {
                this.do设置.Visible = value;
                设置按钮容器大小();
            }
        }

        public bool 显示Logo {
            get { return this.inLogo.Visible; }
            set { this.inLogo.Visible = value; }
        }

        public bool 显示标题
        {
            get { return this.out标题.Visible; }
            set { this.out标题.Visible = value; }
        }

        public string 标题
        {
            get { return this.out标题.Text; }
            set { this.out标题.Text = value; }
        }

        public bool 无背景图片 {
            get { return this.BackgroundImageLayout == ImageLayout.None; }
            set
            {
                if (value)
                {
                    this.BackgroundImageLayout = ImageLayout.None;
                    this.do设置.BackgroundImageLayout = ImageLayout.None;
                    this.do最小化.BackgroundImageLayout = ImageLayout.None;
                    this.do最大化.BackgroundImageLayout = ImageLayout.None;
                    this.do关闭.BackgroundImageLayout = ImageLayout.None;
                }
                else
                {
                    this.BackgroundImageLayout = ImageLayout.Stretch;
                    this.do设置.BackgroundImageLayout = ImageLayout.Tile;
                    this.do最小化.BackgroundImageLayout = ImageLayout.Tile;
                    this.do最大化.BackgroundImageLayout = ImageLayout.Tile;
                    this.do关闭.BackgroundImageLayout = ImageLayout.Tile;
                }
            }
        }

        public U窗体头()
        {
            InitializeComponent();
            this.do设置.Visible = false;
            this.显示Logo = false;
            this.显示标题 = false;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            //if (!this.DesignMode)
            //{
            //    this.ParentForm.MaximumSize = Screen.FromControl(this).WorkingArea.Size;
            //}

            this.do关闭.Click += do关闭_Click;
            this.do最大化.Click += do最大化_Click;
            this.do最小化.Click += do最小化_Click;
            this.MouseDown += FDemo主界面_MouseDown;
            this.MouseMove += FDemo主界面_MouseMove;
            this.MouseUp += FDemo主界面_MouseUp;

            this.do关闭.Image = Resources.按钮_关闭_正常;
            this.do关闭.MouseHover += (a, b) => this.do关闭.Image = Resources.按钮_关闭_悬浮;
            this.do关闭.MouseLeave += (a, b) => this.do关闭.Image = Resources.按钮_关闭_正常;
            this.do关闭.MouseDown += (a, b) => this.do关闭.Image = Resources.按钮_关闭_按下;
            this.do关闭.MouseUp += (a, b) => this.do关闭.Image = Resources.按钮_关闭_正常;

            this.do最大化.Image = Resources.按钮_最大化_正常;
            this.do最大化.MouseHover += (a, b) => this.do最大化.Image = this.ParentForm.WindowState != FormWindowState.Maximized ? Resources.按钮_最大化_悬浮 : Resources.按钮_还原_悬浮;
            this.do最大化.MouseLeave += (a, b) => this.do最大化.Image = this.ParentForm.WindowState != FormWindowState.Maximized ? Resources.按钮_最大化_正常 : Resources.按钮_还原_正常;
            this.do最大化.MouseDown += (a, b) => this.do最大化.Image = this.ParentForm.WindowState != FormWindowState.Maximized ? Resources.按钮_最大化_按下 : Resources.按钮_还原_按下;
            this.do最大化.MouseUp += (a, b) => this.do最大化.Image = this.ParentForm.WindowState != FormWindowState.Maximized ? Resources.按钮_最大化_正常 : Resources.按钮_还原_正常;

            this.do设置.Image = Resources.按钮_设置_正常;
            this.do设置.MouseHover += (a, b) => this.do设置.Image = Resources.按钮_设置_悬浮;
            this.do设置.MouseLeave += (a, b) => this.do设置.Image = Resources.按钮_设置_正常;
            this.do设置.MouseDown += (a, b) => this.do设置.Image = Resources.按钮_设置_按下;
            this.do设置.MouseUp += (a, b) => this.do设置.Image = Resources.按钮_设置_正常;

            this.do最小化.Image = Resources.按钮_最小化_正常;
            this.do最小化.MouseHover += (a, b) => this.do最小化.Image = Resources.按钮_最小化_悬浮;
            this.do最小化.MouseLeave += (a, b) => this.do最小化.Image = Resources.按钮_最小化_正常;
            this.do最小化.MouseDown += (a, b) => this.do最小化.Image = Resources.按钮_最小化_按下;
            this.do最小化.MouseUp += (a, b) => this.do最小化.Image = Resources.按钮_最小化_正常;

            this.do设置.Click += (a, b) => On点击设置();

            设置按钮容器大小();
        }

        private void 设置按钮容器大小()
        {
            int __可见数量 = 0;
            if (this.do最大化.Visible)
            {
                __可见数量++;
            }
            if (this.do最小化.Visible)
            {
                __可见数量++;
            }
            if (this.do设置.Visible)
            {
                __可见数量++;
            }
            this.out控制按钮容器.Width = (__可见数量 + 1) * 28;
            this.out控制按钮容器.Left = this.Width - this.out控制按钮容器.Width;
        }

        void do最小化_Click(object sender, EventArgs e)
        {
            this.ParentForm.WindowState = FormWindowState.Minimized;
        }

        void do最大化_Click(object sender, EventArgs e)
        {
            if (this.ParentForm.WindowState == FormWindowState.Maximized)
            {
                this.ParentForm.WindowState = FormWindowState.Normal;
                this.do最大化.Image = Resources.按钮_最大化_正常;
            }
            else if (this.ParentForm.WindowState == FormWindowState.Normal)
            {
                this.ParentForm.WindowState = FormWindowState.Maximized;
                this.do最大化.Image = Resources.按钮_还原_正常;
            }
        }

        void do关闭_Click(object sender, EventArgs e)
        {
            this.ParentForm.Close();
        }

        void FDemo主界面_MouseDown(object sender, MouseEventArgs e)
        {
            _按下鼠标 = true;
            _按下鼠标时位置 = MousePosition;
        }

        void FDemo主界面_MouseMove(object sender, MouseEventArgs e)
        {
            if (_按下鼠标)
            {
                var __temp = MousePosition;
                var __X偏移 = __temp.X - _按下鼠标时位置.X;
                var __Y偏移 = __temp.Y - _按下鼠标时位置.Y;
                this.ParentForm.Location = new Point(this.ParentForm.Location.X + __X偏移, this.ParentForm.Location.Y + __Y偏移);
                _按下鼠标时位置 = __temp;
            }
        }

        void FDemo主界面_MouseUp(object sender, MouseEventArgs e)
        {
            _按下鼠标 = false;
        }

        public event Action 点击设置;

        protected virtual void On点击设置()
        {
            Action handler = 点击设置;
            if (handler != null) handler();
        }
    }
}
