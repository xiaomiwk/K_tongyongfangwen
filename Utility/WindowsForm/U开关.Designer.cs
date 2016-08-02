namespace Utility.WindowsForm
{
    partial class U开关
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
            this.do关 = new Utility.WindowsForm.U按钮();
            this.do开 = new Utility.WindowsForm.U按钮();
            this.SuspendLayout();
            // 
            // do关
            // 
            this.do关.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(164)))), ((int)(((byte)(221)))));
            this.do关.FlatAppearance.BorderSize = 0;
            this.do关.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.do关.ForeColor = System.Drawing.Color.White;
            this.do关.Location = new System.Drawing.Point(30, 0);
            this.do关.Name = "do关";
            this.do关.Size = new System.Drawing.Size(30, 23);
            this.do关.TabIndex = 33;
            this.do关.Text = "关";
            this.do关.UseVisualStyleBackColor = false;
            this.do关.大小 = new System.Drawing.Size(30, 23);
            this.do关.颜色 = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(164)))), ((int)(((byte)(221)))));
            // 
            // do开
            // 
            this.do开.BackColor = System.Drawing.Color.LightGray;
            this.do开.FlatAppearance.BorderSize = 0;
            this.do开.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.do开.ForeColor = System.Drawing.Color.White;
            this.do开.Location = new System.Drawing.Point(0, 0);
            this.do开.Name = "do开";
            this.do开.Size = new System.Drawing.Size(30, 23);
            this.do开.TabIndex = 34;
            this.do开.Text = "开";
            this.do开.UseVisualStyleBackColor = false;
            this.do开.大小 = new System.Drawing.Size(30, 23);
            this.do开.颜色 = System.Drawing.Color.LightGray;
            // 
            // U开关
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.do开);
            this.Controls.Add(this.do关);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "U开关";
            this.Size = new System.Drawing.Size(60, 23);
            this.ResumeLayout(false);

        }

        #endregion

        private U按钮 do关;
        private U按钮 do开;

    }
}
