using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Utility.WindowsForm
{
    [ToolboxBitmap(typeof(ProgressBar))]
    public class ProgressBarK : ProgressBar
    {
        #region Fields

        private bool hideProgressText = true;
        [DefaultValue(true)]
        public bool HideProgressText
        {
            get { return hideProgressText; }
            set { hideProgressText = value; }
        }

        private ProgressBarStyle progressBarStyle = ProgressBarStyle.Continuous;
        [DefaultValue(ProgressBarStyle.Continuous)]
        public ProgressBarStyle ProgressBarStyle
        {
            get { return progressBarStyle; }
            set { progressBarStyle = value; }
        }

        public new int Value
        {
            get { return base.Value; }
            set { if (value > Maximum) return; base.Value = value; Invalidate(); }
        }

        [Browsable(false)]
        public double ProgressTotalPercent
        {
            get { return ((1 - (double)(Maximum - Value) / Maximum) * 100); }
        }

        [Browsable(false)]
        public double ProgressTotalValue
        {
            get { return (1 - (double)(Maximum - Value) / Maximum); }
        }

        [Browsable(false)]
        public string ProgressPercentText
        {
            get { return (string.Format("{0}%", Math.Round(ProgressTotalPercent))); }
        }

        private double ProgressBarWidth
        {
            get { return (((double)Value / Maximum) * ClientRectangle.Width); }
        }

        private Color _前景颜色 = Color.FromArgb(255, 38, 164, 221);
        public Color 前景颜色
        {
            get { return _前景颜色; }
            set { _前景颜色 = value; }
        }

        private Color _背景颜色 = Color.FromArgb(255, 234, 234, 234);
        public Color 背景颜色
        {
            get { return _背景颜色; }
            set { _背景颜色 = value; }
        }

        private Color _文本颜色 = Color.Gray;
        public Color 文本颜色
        {
            get { return _文本颜色; }
            set { _文本颜色 = value; }
        }

        private Brush _Brush;
        public Brush Brush
        {
            get { return _Brush; }
            set { _Brush = value; }
        }

        private int ProgressBarMarqueeWidth
        {
            get { return (ClientRectangle.Width / 3); }
        }

        #endregion

        #region Constructor

        public ProgressBarK()
        {
            Brush = new SolidBrush(前景颜色);
            SetStyle(ControlStyles.SupportsTransparentBackColor |
                     ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw |
                     ControlStyles.UserPaint, true);
        }

        #endregion

        #region Paint Methods

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            try
            {
                Color backColor = 背景颜色;

                if (backColor.A == 255)
                {
                    e.Graphics.Clear(backColor);
                    return;
                }

                base.OnPaintBackground(e);

            }
            catch
            {
                Invalidate();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            try
            {
                if (GetStyle(ControlStyles.AllPaintingInWmPaint))
                {
                    OnPaintBackground(e);
                }

                OnPaintForeground(e);
            }
            catch
            {
                Invalidate();
            }
        }

        protected virtual void OnPaintForeground(PaintEventArgs e)
        {
            if (progressBarStyle == ProgressBarStyle.Continuous)
            {
                if (!DesignMode) StopTimer();

                DrawProgressContinuous(e.Graphics);
            }
            else if (progressBarStyle == ProgressBarStyle.Blocks)
            {
                if (!DesignMode) StopTimer();

                DrawProgressContinuous(e.Graphics);
            }
            else if (progressBarStyle == ProgressBarStyle.Marquee)
            {
                if (!DesignMode && Enabled) StartTimer();
                if (!Enabled) StopTimer();

                if (Value == Maximum)
                {
                    StopTimer();
                    DrawProgressContinuous(e.Graphics);
                }
                else
                {
                    DrawProgressMarquee(e.Graphics);
                }
            }

            DrawProgressText(e.Graphics);

            using (Pen p = new Pen(Color.FromArgb(255,204,204,204)))
            {
                Rectangle borderRect = new Rectangle(0, 0, Width - 1, Height - 1);
                e.Graphics.DrawRectangle(p, borderRect);
            }

        }

        private void DrawProgressContinuous(Graphics graphics)
        {
            graphics.FillRectangle(Brush, 0, 0, (int)ProgressBarWidth, ClientRectangle.Height);
        }

        private int marqueeX = 0;

        private void DrawProgressMarquee(Graphics graphics)
        {
            graphics.FillRectangle(Brush, marqueeX, 0, ProgressBarMarqueeWidth, ClientRectangle.Height);
        }

        private void DrawProgressText(Graphics graphics)
        {
            if (HideProgressText) return;

            Color foreColor = 文本颜色;

            //if (!Enabled)
            //{
            //    foreColor = MetroPaint.ForeColor.ProgressBar.Disabled(Theme);
            //}
            //else
            //{
            //    foreColor = MetroPaint.ForeColor.ProgressBar.Normal(Theme);
            //}

            TextRenderer.DrawText(graphics, ProgressPercentText, Font, ClientRectangle, foreColor, GetTextFormatFlags(ContentAlignment.MiddleCenter));
        }

        #endregion

        #region Private Methods

        private Timer marqueeTimer;
        private bool marqueeTimerEnabled
        {
            get
            {
                return marqueeTimer != null && marqueeTimer.Enabled;
            }
        }

        private void StartTimer()
        {
            if (marqueeTimerEnabled) return;

            if (marqueeTimer == null)
            {
                marqueeTimer = new Timer { Interval = 10 };
                marqueeTimer.Tick += marqueeTimer_Tick;
            }

            marqueeX = -ProgressBarMarqueeWidth;

            marqueeTimer.Stop();
            marqueeTimer.Start();

            marqueeTimer.Enabled = true;

            Invalidate();
        }
        private void StopTimer()
        {
            if (marqueeTimer == null) return;

            marqueeTimer.Stop();

            Invalidate();
        }

        private void marqueeTimer_Tick(object sender, EventArgs e)
        {
            marqueeX++;

            if (marqueeX > ClientRectangle.Width)
            {
                marqueeX = -ProgressBarMarqueeWidth;
            }

            Invalidate();
        }

        private static TextFormatFlags GetTextFormatFlags(ContentAlignment textAlign)
        {
            return GetTextFormatFlags(textAlign, false);
        }

        private static TextFormatFlags GetTextFormatFlags(ContentAlignment textAlign, bool WrapToLine)
        {
            TextFormatFlags controlFlags = TextFormatFlags.Default;

            switch (WrapToLine)
            {
                case true:
                    controlFlags = TextFormatFlags.WordBreak;
                    break;
                case false:
                    controlFlags = TextFormatFlags.EndEllipsis;
                    break;
            }
            switch (textAlign)
            {
                case ContentAlignment.TopLeft:
                    controlFlags |= TextFormatFlags.Top | TextFormatFlags.Left;
                    break;
                case ContentAlignment.TopCenter:
                    controlFlags |= TextFormatFlags.Top | TextFormatFlags.HorizontalCenter;
                    break;
                case ContentAlignment.TopRight:
                    controlFlags |= TextFormatFlags.Top | TextFormatFlags.Right;
                    break;

                case ContentAlignment.MiddleLeft:
                    controlFlags |= TextFormatFlags.VerticalCenter | TextFormatFlags.Left;
                    break;
                case ContentAlignment.MiddleCenter:
                    controlFlags |= TextFormatFlags.VerticalCenter | TextFormatFlags.HorizontalCenter;
                    break;
                case ContentAlignment.MiddleRight:
                    controlFlags |= TextFormatFlags.VerticalCenter | TextFormatFlags.Right;
                    break;

                case ContentAlignment.BottomLeft:
                    controlFlags |= TextFormatFlags.Bottom | TextFormatFlags.Left;
                    break;
                case ContentAlignment.BottomCenter:
                    controlFlags |= TextFormatFlags.Bottom | TextFormatFlags.HorizontalCenter;
                    break;
                case ContentAlignment.BottomRight:
                    controlFlags |= TextFormatFlags.Bottom | TextFormatFlags.Right;
                    break;
            }

            return controlFlags;
        }

        #endregion
    }

}
