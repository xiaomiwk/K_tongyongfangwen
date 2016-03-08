using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Utility.WindowsForm
{
    [ToolboxBitmap(typeof(TrackBar))]
    [DefaultEvent("Scroll")]
    public class TrackBarK : Control
    {
        #region Events

        public event EventHandler ValueChanged;
        private void OnValueChanged()
        {
            if (ValueChanged != null)
                ValueChanged(this, EventArgs.Empty);
        }

        public event ScrollEventHandler Scroll;
        private void OnScroll(ScrollEventType scrollType, int newValue)
        {
            if (Scroll != null)
                Scroll(this, new ScrollEventArgs(scrollType, newValue));
        }


        #endregion

        #region Fields

        private bool _水平 = true;
        [DefaultValue(true)]
        public bool 水平
        {
            get { return _水平; }
            set { _水平 = value; }
        }

        private Color _前景颜色 = Color.FromArgb(255, 102, 102, 102);
        public Color 前景颜色
        {
            get { return _前景颜色; }
            set { _前景颜色 = value; }
        }

        private Color _背景颜色 = Color.FromArgb(255, 204, 204, 204);
        public Color 背景颜色
        {
            get { return _背景颜色; }
            set { _背景颜色 = value; }
        }

        private Color _高亮颜色 = Color.FromArgb(255, 17, 17, 17);
        public Color 高亮颜色
        {
            get { return _高亮颜色; }
            set { _高亮颜色 = value; }
        }

        private bool displayFocusRectangle = false;
        [DefaultValue(false)]
        public bool DisplayFocus
        {
            get { return displayFocusRectangle; }
            set { displayFocusRectangle = value; }
        }

        private int trackerValue = 50;
        [DefaultValue(50)]
        public int Value
        {
            get { return trackerValue; }
            set
            {
                if (value >= barMinimum & value <= barMaximum)
                {
                    trackerValue = value;
                    OnValueChanged();
                    Invalidate();
                }
                else throw new ArgumentOutOfRangeException("Value is outside appropriate range (min, max)");
            }
        }

        private int barMinimum = 0;
        [DefaultValue(0)]
        public int Minimum
        {
            get { return barMinimum; }
            set
            {
                if (value < barMaximum)
                {
                    barMinimum = value;
                    if (trackerValue < barMinimum)
                    {
                        trackerValue = barMinimum;
                        if (ValueChanged != null) ValueChanged(this, new EventArgs());
                    }
                    Invalidate();
                }
                else throw new ArgumentOutOfRangeException("Minimal value is greather than maximal one");
            }
        }


        private int barMaximum = 100;
        [DefaultValue(100)]
        public int Maximum
        {
            get { return barMaximum; }
            set
            {
                if (value > barMinimum)
                {
                    barMaximum = value;
                    if (trackerValue > barMaximum)
                    {
                        trackerValue = barMaximum;
                        if (ValueChanged != null) ValueChanged(this, new EventArgs());
                    }
                    Invalidate();
                }
                else throw new ArgumentOutOfRangeException("Maximal value is lower than minimal one");
            }
        }

        private int smallChange = 1;
        [DefaultValue(1)]
        public int SmallChange
        {
            get { return smallChange; }
            set { smallChange = value; }
        }

        private int largeChange = 5;
        [DefaultValue(5)]
        public int LargeChange
        {
            get { return largeChange; }
            set { largeChange = value; }
        }

        private int mouseWheelBarPartitions = 10;
        [DefaultValue(10)]
        public int MouseWheelBarPartitions
        {
            get { return mouseWheelBarPartitions; }
            set
            {
                if (value > 0)
                    mouseWheelBarPartitions = value;
                else throw new ArgumentOutOfRangeException("MouseWheelBarPartitions has to be greather than zero");
            }
        }

        private bool isHovered = false;
        private bool isPressed = false;
        private bool isFocused = false;

        #endregion

        #region Constructor

        public TrackBarK(int min, int max, int value)
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw |
                     ControlStyles.Selectable |
                     ControlStyles.SupportsTransparentBackColor |
                     ControlStyles.UserMouse |
                     ControlStyles.UserPaint, true);

            BackColor = Color.Transparent;

            Minimum = min;
            Maximum = max;
            Value = value;
        }

        public TrackBarK() : this(0, 100, 50) { }

        #endregion

        #region Paint Methods

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            try
            {
                Color backColor = BackColor;

                //if (!useCustomBackColor)
                //{
                //    backColor = MetroPaint.BackColor.Form(Theme);
                //}

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
            Color thumbColor, barColor;

            if (isHovered && !isPressed && Enabled)
            {
                thumbColor = 高亮颜色;
                barColor = 背景颜色;
            }
            else if (isHovered && isPressed && Enabled)
            {
                thumbColor = 高亮颜色;
                barColor = 背景颜色;
            }
            else if (!Enabled)
            {
                thumbColor = Color.FromArgb(255, 179, 179, 179);
                barColor = Color.FromArgb(255, 230, 230, 230);
            }
            else
            {
                thumbColor = 前景颜色;
                barColor = 背景颜色;
            }

            DrawTrackBar(e.Graphics, thumbColor, barColor);

            if (displayFocusRectangle && isFocused)
                ControlPaint.DrawFocusRectangle(e.Graphics, ClientRectangle);
        }

        private void DrawTrackBar(Graphics g, Color thumbColor, Color barColor)
        {
            if (水平)
            {
                int TrackX = (((trackerValue - barMinimum) * (Width - 6)) / (barMaximum - barMinimum));

                using (SolidBrush b = new SolidBrush(thumbColor))
                {
                    Rectangle barRect = new Rectangle(0, Height / 2 - 2, TrackX, 4);
                    g.FillRectangle(b, barRect);

                    Rectangle thumbRect = new Rectangle(TrackX, Height / 2 - 8, 6, 16);
                    g.FillRectangle(b, thumbRect);
                }

                using (SolidBrush b = new SolidBrush(barColor))
                {
                    Rectangle barRect = new Rectangle(TrackX + 7, Height / 2 - 2, Width - TrackX + 7, 4);
                    g.FillRectangle(b, barRect);
                }
            }
            else
            {
                int TrackX = (((trackerValue - barMinimum) * (Height - 12)) / (barMaximum - barMinimum));

                using (SolidBrush b = new SolidBrush(thumbColor))
                {
                    Rectangle barRect = new Rectangle(Width / 2 - 2, Height - TrackX - 3, 4, TrackX);
                    g.FillRectangle(b, barRect);

                    Rectangle thumbRect = new Rectangle(Width / 2 - 8, Height - TrackX - 3 - 6, 16, 6);
                    g.FillRectangle(b, thumbRect);
                }

                using (SolidBrush b = new SolidBrush(barColor))
                {
                    Rectangle barRect = new Rectangle(Width / 2 - 2, 6, 4, Height - TrackX - 12 - 3);
                    g.FillRectangle(b, barRect);
                }

            }
        }

        #endregion

        #region Focus Methods

        protected override void OnGotFocus(EventArgs e)
        {
            isFocused = true;
            Invalidate();

            base.OnGotFocus(e);
        }

        protected override void OnLostFocus(EventArgs e)
        {
            isFocused = false;
            isHovered = false;
            isPressed = false;
            Invalidate();

            base.OnLostFocus(e);
        }

        protected override void OnEnter(EventArgs e)
        {
            isFocused = true;
            Invalidate();

            base.OnEnter(e);
        }

        protected override void OnLeave(EventArgs e)
        {
            isFocused = false;
            isHovered = false;
            isPressed = false;
            Invalidate();

            base.OnLeave(e);
        }

        #endregion

        #region Keyboard Methods

        protected override void OnKeyDown(KeyEventArgs e)
        {
            isHovered = true;
            isPressed = true;
            Invalidate();

            base.OnKeyDown(e);
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            isHovered = false;
            isPressed = false;
            Invalidate();

            base.OnKeyUp(e);

            switch (e.KeyCode)
            {
                case Keys.Down:
                case Keys.Left:
                    SetProperValue(Value - (int)smallChange);
                    OnScroll(ScrollEventType.SmallDecrement, Value);
                    break;
                case Keys.Up:
                case Keys.Right:
                    SetProperValue(Value + (int)smallChange);
                    OnScroll(ScrollEventType.SmallIncrement, Value);
                    break;
                case Keys.Home:
                    Value = barMinimum;
                    break;
                case Keys.End:
                    Value = barMaximum;
                    break;
                case Keys.PageDown:
                    SetProperValue(Value - (int)largeChange);
                    OnScroll(ScrollEventType.LargeDecrement, Value);
                    break;
                case Keys.PageUp:
                    SetProperValue(Value + (int)largeChange);
                    OnScroll(ScrollEventType.LargeIncrement, Value);
                    break;
            }

            if (Value == barMinimum)
                OnScroll(ScrollEventType.First, Value);

            if (Value == barMaximum)
                OnScroll(ScrollEventType.Last, Value);

            Point pt = PointToClient(Cursor.Position);
            OnMouseMove(new MouseEventArgs(MouseButtons.None, 0, pt.X, pt.Y, 0));
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Tab | ModifierKeys == Keys.Shift)
                return base.ProcessDialogKey(keyData);
            else
            {
                OnKeyDown(new KeyEventArgs(keyData));
                return true;
            }
        }

        #endregion

        #region Mouse Methods

        protected override void OnMouseEnter(EventArgs e)
        {
            isHovered = true;
            Invalidate();

            base.OnMouseEnter(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isPressed = true;
                Invalidate();
            }

            base.OnMouseDown(e);

            if (e.Button == MouseButtons.Left)
            {
                Capture = true;
                OnScroll(ScrollEventType.ThumbTrack, trackerValue);
                OnValueChanged();
                OnMouseMove(e);
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (Capture & e.Button == MouseButtons.Left)
            {
                ScrollEventType set = ScrollEventType.ThumbPosition;
                Point pt = e.Location;
                int p = pt.X;
                float coef = (float)(barMaximum - barMinimum) / (float)(ClientSize.Width - 3);
                if (!水平)
                {
                    p = Height - pt.Y;
                    coef = (float)(barMaximum - barMinimum) / (float)(ClientSize.Height - 6);
                }

                trackerValue = (int)(p * coef + barMinimum);

                if (trackerValue <= barMinimum)
                {
                    trackerValue = barMinimum;
                    set = ScrollEventType.First;
                }
                else if (trackerValue >= barMaximum)
                {
                    trackerValue = barMaximum;
                    set = ScrollEventType.Last;
                }

                OnScroll(set, trackerValue);
                OnValueChanged();

                Invalidate();
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            isPressed = false;
            Invalidate();

            base.OnMouseUp(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            isHovered = false;
            Invalidate();

            base.OnMouseLeave(e);
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);
            int v = e.Delta / 120 * (barMaximum - barMinimum) / mouseWheelBarPartitions;
            SetProperValue(Value + v);
        }

        #endregion

        #region Overridden Methods

        protected override void OnEnabledChanged(EventArgs e)
        {
            base.OnEnabledChanged(e);
            Invalidate();
        }

        #endregion

        #region Helper Methods

        private void SetProperValue(int val)
        {
            if (val < barMinimum) Value = barMinimum;
            else if (val > barMaximum) Value = barMaximum;
            else Value = val;
        }

        #endregion
    }

}
