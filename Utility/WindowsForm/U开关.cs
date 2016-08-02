using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Utility.WindowsForm
{
    public partial class U开关 : UserControl
    {
        private bool _状态;

        /// <summary>
        /// true:开, false:关
        /// </summary>
        public bool 开启 {
            get { return _状态; }
            set
            {
                _状态 = value;
                if (_状态)
                {
                    this.do开.BackColor = Color.FromArgb(38, 164, 221);
                    this.do关.BackColor = Color.LightGray;
                }
                else
                {
                    this.do开.BackColor = Color.LightGray;
                    this.do关.BackColor = Color.FromArgb(38, 164, 221);
                }
            }
        }

        public U开关()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            开启 = false;
            this.do关.Click += do开关_Click;
            this.do开.Click += do开关_Click;
        }

        void do开关_Click(object sender, EventArgs e)
        {
            开启 = !开启;
            On状态切换(开启);
            OnClick(e);
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            this.do开.Width = this.Width/2;
            this.do关.Width = this.Width/2;
            this.do开.Height = this.Height;
            this.do关.Height = this.Height;
            this.do关.Left = this.Width/2;
        }

        public event Action<bool> 状态切换;

        protected virtual void On状态切换(bool obj)
        {
            var handler = 状态切换;
            if (handler != null) handler(obj);
        }
    }
}
