using System;
using System.Windows.Forms;
using 通用访问.DTO;

namespace 通用访问.UI组件.查看数据
{
    public partial class F点结构_查看 : Form
    {
        private M元数据 _元数据;

        private string _值;

        public F点结构_查看(M元数据 __元数据, string __值, string __标题 = "")
        {
            _元数据 = __元数据;
            _值 = __值;
            InitializeComponent();
            this.out标题.Text = __标题;
            this.Text = __标题;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (_元数据 != null)
            {
                this.out范围.Text = _元数据.范围;
                this.out类型.Text = _元数据.类型;
                this.out描述.Text = _元数据.描述;
                this.out默认值.Text = _元数据.默认值;
            }
            else
            {
                this.splitContainer1.Panel2Collapsed = true;
            }

            this.out值.Text = _值;
        }
    }
}
