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
    public class ProgressSpinner : Control
    {
        #region Fields

        private Timer timer;
        private int progress = 50;
        private float angle = 270;

        [DefaultValue(true)]
        public bool Spinning
        {
            get { return timer.Enabled; }
            set { timer.Enabled = value; }
        }

        //[DefaultValue(0)]
        //public int Value
        //{
        //    get { return progress; }
        //    set
        //    {
        //        if (value != -1 && (value < minimum || value > maximum))
        //            throw new ArgumentOutOfRangeException("Progress value must be -1 or between Minimum and Maximum.", (Exception)null);
        //        progress = value;
        //        Refresh();
        //    }
        //}

        private int minimum = 0;
        [DefaultValue(0)]
        public int Minimum
        {
            get { return minimum; }
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("Minimum value must be >= 0.", (Exception)null);
                if (value >= maximum)
                    throw new ArgumentOutOfRangeException("Minimum value must be < Maximum.", (Exception)null);
                minimum = value;
                if (progress != -1 && progress < minimum)
                    progress = minimum;
                Refresh();
            }
        }

        private int maximum = 100;
        [DefaultValue(0)]
        public int Maximum
        {
            get { return maximum; }
            set
            {
                if (value <= minimum)
                    throw new ArgumentOutOfRangeException("Maximum value must be > Minimum.", (Exception)null);
                maximum = value;
                if (progress > maximum)
                    progress = maximum;
                Refresh();
            }
        }

        private bool ensureVisible = true;
        [DefaultValue(true)]
        public bool EnsureVisible
        {
            get { return ensureVisible; }
            set { ensureVisible = value; Refresh(); }
        }

        private float speed;
        [DefaultValue(1f)]
        public float Speed
        {
            get { return speed; }
            set
            {
                if (value <= 0 || value > 10)
                    throw new ArgumentOutOfRangeException("Speed value must be > 0 and <= 10.", (Exception)null);

                speed = value;
            }
        }

        private bool backwards;
        [DefaultValue(false)]
        public bool Backwards
        {
            get { return backwards; }
            set { backwards = value; Refresh(); }
        }

        private Color _前景颜色 = Color.FromArgb(255, 38, 164, 221);
        public Color 前景颜色
        {
            get { return _前景颜色; }
            set { _前景颜色 = value; }
        }

        private Color _背景颜色 = Color.Transparent;
        public Color 背景颜色
        {
            get { return _背景颜色; }
            set { _背景颜色 = value; }
        }

        #endregion

        #region Constructor

        public ProgressSpinner()
        {
            timer = new Timer {Interval = 20};
            timer.Tick += timer_Tick;
            timer.Enabled = true;

            Width = 16;
            Height = 16;
            speed = 1;
            DoubleBuffered = true;
        }

        #endregion

        #region Public Methods

        public void Reset()
        {
            progress = minimum;
            angle = 270;
            Refresh();
        }

        #endregion

        #region Management Methods

        private void timer_Tick(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                angle += 6f * speed * (backwards ? -1 : 1);
                Refresh();
                //Application.DoEvents();
            }
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
            Color foreColor = 前景颜色;

            using (Pen forePen = new Pen(foreColor, (float)Width / 5))
            {
                int padding = (int)Math.Ceiling((float)Width / 10);

                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

                if (progress != -1)
                {
                    float sweepAngle;
                    float progFrac = (float)(progress - minimum) / (float)(maximum - minimum);

                    if (ensureVisible)
                    {
                        sweepAngle = 30 + 300f * progFrac;
                    }
                    else
                    {
                        sweepAngle = 360f * progFrac;
                    }

                    if (backwards)
                    {
                        sweepAngle = -sweepAngle;
                    }

                    e.Graphics.DrawArc(forePen, padding, padding, Width - 2 * padding - 1, Height - 2 * padding - 1, angle, sweepAngle);
                }
                else
                {
                    const int maxOffset = 180;
                    for (int offset = 0; offset <= maxOffset; offset += 15)
                    {
                        int alpha = 290 - (offset * 290 / maxOffset);

                        if (alpha > 255)
                        {
                            alpha = 255;
                        }
                        if (alpha < 0)
                        {
                            alpha = 0;
                        }

                        Color col = Color.FromArgb(alpha, forePen.Color);
                        using (Pen gradPen = new Pen(col, forePen.Width))
                        {
                            float startAngle = angle + (offset - (ensureVisible ? 30 : 0)) * (backwards ? 1 : -1);
                            float sweepAngle = 15 * (backwards ? 1 : -1);
                            e.Graphics.DrawArc(gradPen, padding, padding, Width - 2 * padding - 1, Height - 2 * padding - 1, startAngle, sweepAngle);
                        }
                    }
                }
            }
        }

        #endregion
    }

}
