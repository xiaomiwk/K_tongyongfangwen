namespace 通用访问.UI组件
{
    partial class F监听事件
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
            this.out列表 = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.do清空 = new Utility.WindowsForm.U按钮();
            this.in监听 = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.out列表)).BeginInit();
            this.SuspendLayout();
            // 
            // out列表
            // 
            this.out列表.AllowUserToAddRows = false;
            this.out列表.AllowUserToDeleteRows = false;
            this.out列表.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.out列表.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.out列表.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.out列表.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column3,
            this.Column2,
            this.Column6});
            this.out列表.Location = new System.Drawing.Point(3, 3);
            this.out列表.Name = "out列表";
            this.out列表.ReadOnly = true;
            this.out列表.RowTemplate.Height = 23;
            this.out列表.Size = new System.Drawing.Size(665, 409);
            this.out列表.TabIndex = 0;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "时间";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "对象";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "事件";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 200;
            // 
            // Column6
            // 
            this.Column6.HeaderText = "参数";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            this.Column6.Width = 300;
            // 
            // do清空
            // 
            this.do清空.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.do清空.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(164)))), ((int)(((byte)(221)))));
            this.do清空.FlatAppearance.BorderSize = 0;
            this.do清空.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.do清空.ForeColor = System.Drawing.Color.White;
            this.do清空.Location = new System.Drawing.Point(3, 422);
            this.do清空.Name = "do清空";
            this.do清空.Size = new System.Drawing.Size(100, 26);
            this.do清空.TabIndex = 1;
            this.do清空.Text = "清空";
            this.do清空.UseVisualStyleBackColor = false;
            this.do清空.大小 = new System.Drawing.Size(100, 26);
            this.do清空.文字颜色 = System.Drawing.Color.White;
            this.do清空.颜色 = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(164)))), ((int)(((byte)(221)))));
            // 
            // in监听
            // 
            this.in监听.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.in监听.AutoSize = true;
            this.in监听.Location = new System.Drawing.Point(121, 427);
            this.in监听.Name = "in监听";
            this.in监听.Size = new System.Drawing.Size(51, 21);
            this.in监听.TabIndex = 2;
            this.in监听.Text = "监听";
            this.in监听.UseVisualStyleBackColor = true;
            // 
            // F监听事件
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.in监听);
            this.Controls.Add(this.do清空);
            this.Controls.Add(this.out列表);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "F监听事件";
            this.Padding = new System.Windows.Forms.Padding(3, 0, 3, 10);
            this.Size = new System.Drawing.Size(671, 453);
            ((System.ComponentModel.ISupportInitialize)(this.out列表)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView out列表;
        private Utility.WindowsForm.U按钮 do清空;
        private System.Windows.Forms.CheckBox in监听;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
    }
}
