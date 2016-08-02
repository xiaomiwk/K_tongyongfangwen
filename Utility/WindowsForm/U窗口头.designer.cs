using System.Windows.Forms;

namespace Utility.WindowsForm
{
    partial class U窗体头
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(U窗体头));
            this.out控制按钮容器 = new System.Windows.Forms.FlowLayoutPanel();
            this.do关闭 = new System.Windows.Forms.PictureBox();
            this.do最大化 = new System.Windows.Forms.PictureBox();
            this.do最小化 = new System.Windows.Forms.PictureBox();
            this.do设置 = new System.Windows.Forms.PictureBox();
            this.inLogo = new System.Windows.Forms.PictureBox();
            this.out标题 = new System.Windows.Forms.Label();
            this.out控制按钮容器.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.do关闭)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.do最大化)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.do最小化)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.do设置)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.inLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // out控制按钮容器
            // 
            this.out控制按钮容器.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.out控制按钮容器.Controls.Add(this.do关闭);
            this.out控制按钮容器.Controls.Add(this.do最大化);
            this.out控制按钮容器.Controls.Add(this.do最小化);
            this.out控制按钮容器.Controls.Add(this.do设置);
            this.out控制按钮容器.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.out控制按钮容器.Location = new System.Drawing.Point(218, -1);
            this.out控制按钮容器.Margin = new System.Windows.Forms.Padding(0);
            this.out控制按钮容器.Name = "out控制按钮容器";
            this.out控制按钮容器.Size = new System.Drawing.Size(112, 20);
            this.out控制按钮容器.TabIndex = 0;
            // 
            // do关闭
            // 
            this.do关闭.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.do关闭.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("do关闭.BackgroundImage")));
            this.do关闭.Location = new System.Drawing.Point(84, 0);
            this.do关闭.Margin = new System.Windows.Forms.Padding(0);
            this.do关闭.Name = "do关闭";
            this.do关闭.Size = new System.Drawing.Size(28, 20);
            this.do关闭.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.do关闭.TabIndex = 21;
            this.do关闭.TabStop = false;
            // 
            // do最大化
            // 
            this.do最大化.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.do最大化.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("do最大化.BackgroundImage")));
            this.do最大化.Location = new System.Drawing.Point(56, 0);
            this.do最大化.Margin = new System.Windows.Forms.Padding(0);
            this.do最大化.Name = "do最大化";
            this.do最大化.Size = new System.Drawing.Size(28, 20);
            this.do最大化.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.do最大化.TabIndex = 20;
            this.do最大化.TabStop = false;
            // 
            // do最小化
            // 
            this.do最小化.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.do最小化.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("do最小化.BackgroundImage")));
            this.do最小化.Location = new System.Drawing.Point(28, 0);
            this.do最小化.Margin = new System.Windows.Forms.Padding(0);
            this.do最小化.Name = "do最小化";
            this.do最小化.Size = new System.Drawing.Size(28, 20);
            this.do最小化.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.do最小化.TabIndex = 19;
            this.do最小化.TabStop = false;
            // 
            // do设置
            // 
            this.do设置.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.do设置.BackgroundImage = global::Utility.Properties.Resources.窗体头背景;
            this.do设置.Location = new System.Drawing.Point(0, 0);
            this.do设置.Margin = new System.Windows.Forms.Padding(0);
            this.do设置.Name = "do设置";
            this.do设置.Size = new System.Drawing.Size(28, 20);
            this.do设置.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.do设置.TabIndex = 18;
            this.do设置.TabStop = false;
            // 
            // inLogo
            // 
            this.inLogo.Image = global::Utility.Properties.Resources.K;
            this.inLogo.Location = new System.Drawing.Point(10, 10);
            this.inLogo.Name = "inLogo";
            this.inLogo.Size = new System.Drawing.Size(20, 20);
            this.inLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.inLogo.TabIndex = 1;
            this.inLogo.TabStop = false;
            // 
            // out标题
            // 
            this.out标题.AutoSize = true;
            this.out标题.BackColor = System.Drawing.Color.Transparent;
            this.out标题.ForeColor = System.Drawing.Color.Gray;
            this.out标题.Location = new System.Drawing.Point(36, 13);
            this.out标题.Name = "out标题";
            this.out标题.Size = new System.Drawing.Size(32, 17);
            this.out标题.TabIndex = 2;
            this.out标题.Text = "标题";
            // 
            // U窗体头
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = global::Utility.Properties.Resources.窗体头背景;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Controls.Add(this.out标题);
            this.Controls.Add(this.inLogo);
            this.Controls.Add(this.out控制按钮容器);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "U窗体头";
            this.Size = new System.Drawing.Size(330, 48);
            this.out控制按钮容器.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.do关闭)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.do最大化)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.do最小化)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.do设置)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.inLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private FlowLayoutPanel out控制按钮容器;
        private PictureBox do关闭;
        private PictureBox do最大化;
        private PictureBox do最小化;
        private PictureBox do设置;
        private PictureBox inLogo;
        private Label out标题;

    }
}
