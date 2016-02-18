using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Utility.WindowsForm
{
    public class DataGridViewK : DataGridView
    {
        public DataGridViewK()
        {
            this.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.GridColor = System.Drawing.SystemColors.ControlLight;
            this.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            //this.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.RowHeadersVisible = false;
        }

        //protected override void OnPaint(PaintEventArgs e)
        //{
        //    base.OnPaint(e);
        //    e.Graphics.DrawRectangle(Pens.Blue, new Rectangle(0, 0, this.Width - 1, this.Height - 1));
        //}
    }
}
