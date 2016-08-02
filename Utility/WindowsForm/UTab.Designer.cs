namespace Utility.WindowsForm
{
    partial class UTab
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
            this.label2 = new System.Windows.Forms.Label();
            this.out指示当前 = new System.Windows.Forms.Label();
            this.out标题容器 = new System.Windows.Forms.Panel();
            this.u容器1 = new Utility.WindowsForm.U容器();
            this.do向后 = new Utility.WindowsForm.U按钮_轻();
            this.do向前 = new Utility.WindowsForm.U按钮_轻();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.BackColor = System.Drawing.Color.Gainsboro;
            this.label2.Location = new System.Drawing.Point(0, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(739, 1);
            this.label2.TabIndex = 2;
            // 
            // out指示当前
            // 
            this.out指示当前.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(164)))), ((int)(((byte)(221)))));
            this.out指示当前.Location = new System.Drawing.Point(12, 35);
            this.out指示当前.Name = "out指示当前";
            this.out指示当前.Size = new System.Drawing.Size(120, 2);
            this.out指示当前.TabIndex = 3;
            // 
            // out标题容器
            // 
            this.out标题容器.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.out标题容器.Location = new System.Drawing.Point(0, 4);
            this.out标题容器.Name = "out标题容器";
            this.out标题容器.Size = new System.Drawing.Size(690, 28);
            this.out标题容器.TabIndex = 5;
            // 
            // u容器1
            // 
            this.u容器1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.u容器1.Location = new System.Drawing.Point(0, 40);
            this.u容器1.Name = "u容器1";
            this.u容器1.Size = new System.Drawing.Size(739, 311);
            this.u容器1.TabIndex = 4;
            // 
            // do向后
            // 
            this.do向后.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.do向后.BackColor = System.Drawing.Color.White;
            this.do向后.FlatAppearance.BorderSize = 0;
            this.do向后.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.do向后.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(164)))), ((int)(((byte)(221)))));
            this.do向后.Location = new System.Drawing.Point(713, 6);
            this.do向后.Name = "do向后";
            this.do向后.Size = new System.Drawing.Size(23, 23);
            this.do向后.TabIndex = 7;
            this.do向后.Text = ">";
            this.do向后.UseVisualStyleBackColor = false;
            this.do向后.大小 = new System.Drawing.Size(23, 23);
            this.do向后.颜色 = System.Drawing.Color.White;
            // 
            // do向前
            // 
            this.do向前.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.do向前.BackColor = System.Drawing.Color.White;
            this.do向前.FlatAppearance.BorderSize = 0;
            this.do向前.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.do向前.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(164)))), ((int)(((byte)(221)))));
            this.do向前.Location = new System.Drawing.Point(690, 6);
            this.do向前.Name = "do向前";
            this.do向前.Size = new System.Drawing.Size(23, 23);
            this.do向前.TabIndex = 6;
            this.do向前.Text = "<";
            this.do向前.UseVisualStyleBackColor = false;
            this.do向前.大小 = new System.Drawing.Size(23, 23);
            this.do向前.颜色 = System.Drawing.Color.White;
            // 
            // UTab
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.do向后);
            this.Controls.Add(this.do向前);
            this.Controls.Add(this.out标题容器);
            this.Controls.Add(this.u容器1);
            this.Controls.Add(this.out指示当前);
            this.Controls.Add(this.label2);
            this.Name = "UTab";
            this.Size = new System.Drawing.Size(739, 351);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label out指示当前;
        private U容器 u容器1;
        private System.Windows.Forms.Panel out标题容器;
        private U按钮_轻 do向后;
        private U按钮_轻 do向前;
    }
}
