namespace 通用访问.UI组件
{
    partial class F命令列表窗口
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F命令列表窗口));
            this.u窗口背景1 = new Utility.WindowsForm.U窗口背景();
            this.u窗体脚1 = new Utility.WindowsForm.U窗体脚();
            this.u窗体头1 = new Utility.WindowsForm.U窗体头();
            this.out标题 = new System.Windows.Forms.Label();
            this.do显示版本 = new System.Windows.Forms.PictureBox();
            this.f命令列表1 = new F命令列表();
            this.u窗口背景1.SuspendLayout();
            this.u窗体头1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.do显示版本)).BeginInit();
            this.SuspendLayout();
            // 
            // u窗口背景1
            // 
            this.u窗口背景1.Controls.Add(this.f命令列表1);
            this.u窗口背景1.Controls.Add(this.u窗体脚1);
            this.u窗口背景1.Controls.Add(this.u窗体头1);
            this.u窗口背景1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.u窗口背景1.Location = new System.Drawing.Point(0, 0);
            this.u窗口背景1.Margin = new System.Windows.Forms.Padding(0);
            this.u窗口背景1.Name = "u窗口背景1";
            this.u窗口背景1.Size = new System.Drawing.Size(1280, 740);
            this.u窗口背景1.TabIndex = 0;
            this.u窗口背景1.边框颜色 = System.Drawing.Color.Gainsboro;
            this.u窗口背景1.面板颜色 = System.Drawing.Color.Empty;
            // 
            // u窗体脚1
            // 
            this.u窗体脚1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.u窗体脚1.BackColor = System.Drawing.Color.White;
            this.u窗体脚1.Cursor = System.Windows.Forms.Cursors.PanSE;
            this.u窗体脚1.Location = new System.Drawing.Point(1269, 724);
            this.u窗体脚1.Name = "u窗体脚1";
            this.u窗体脚1.Size = new System.Drawing.Size(10, 15);
            this.u窗体脚1.TabIndex = 20;
            // 
            // u窗体头1
            // 
            this.u窗体头1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.u窗体头1.BackColor = System.Drawing.Color.White;
            this.u窗体头1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("u窗体头1.BackgroundImage")));
            this.u窗体头1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.u窗体头1.Controls.Add(this.out标题);
            this.u窗体头1.Controls.Add(this.do显示版本);
            this.u窗体头1.Location = new System.Drawing.Point(1, 1);
            this.u窗体头1.Name = "u窗体头1";
            this.u窗体头1.Size = new System.Drawing.Size(1278, 45);
            this.u窗体头1.TabIndex = 4;
            this.u窗体头1.显示最大化按钮 = true;
            this.u窗体头1.显示最小化按钮 = true;
            this.u窗体头1.显示设置按钮 = false;
            // 
            // out标题
            // 
            this.out标题.AutoSize = true;
            this.out标题.BackColor = System.Drawing.Color.Transparent;
            this.out标题.Font = new System.Drawing.Font("微软雅黑", 9.5F);
            this.out标题.ForeColor = System.Drawing.Color.DarkGray;
            this.out标题.Location = new System.Drawing.Point(49, 15);
            this.out标题.Name = "out标题";
            this.out标题.Size = new System.Drawing.Size(112, 19);
            this.out标题.TabIndex = 2;
            this.out标题.Text = "通用访问 (命令版)";
            // 
            // do显示版本
            // 
            this.do显示版本.Cursor = System.Windows.Forms.Cursors.Hand;
            this.do显示版本.Image = global::通用访问.UI组件.Properties.Resources.K;
            this.do显示版本.Location = new System.Drawing.Point(11, 11);
            this.do显示版本.Name = "do显示版本";
            this.do显示版本.Size = new System.Drawing.Size(26, 26);
            this.do显示版本.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.do显示版本.TabIndex = 1;
            this.do显示版本.TabStop = false;
            // 
            // f命令列表1
            // 
            this.f命令列表1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.f命令列表1.Location = new System.Drawing.Point(12, 52);
            this.f命令列表1.Name = "f命令列表1";
            this.f命令列表1.Size = new System.Drawing.Size(1256, 676);
            this.f命令列表1.TabIndex = 21;
            this.f命令列表1.名称 = null;
            this.f命令列表1.地址 = null;
            // 
            // F命令列表窗口
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1280, 740);
            this.Controls.Add(this.u窗口背景1);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(1920, 1050);
            this.Name = "F命令列表窗口";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "通用访问 (命令版)";
            this.u窗口背景1.ResumeLayout(false);
            this.u窗体头1.ResumeLayout(false);
            this.u窗体头1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.do显示版本)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Utility.WindowsForm.U窗口背景 u窗口背景1;
        private Utility.WindowsForm.U窗体头 u窗体头1;
        private System.Windows.Forms.PictureBox do显示版本;
        private System.Windows.Forms.Label out标题;
        private Utility.WindowsForm.U窗体脚 u窗体脚1;
        private F命令列表 f命令列表1;
    }
}

