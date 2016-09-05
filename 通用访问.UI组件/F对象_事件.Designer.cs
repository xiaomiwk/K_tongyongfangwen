namespace 通用访问.UI组件
{
    partial class F对象_事件
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.out值 = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.do清空记录 = new Utility.WindowsForm.U按钮();
            this.out记录 = new System.Windows.Forms.TextBox();
            this.do订阅 = new Utility.WindowsForm.U按钮();
            this.label4 = new System.Windows.Forms.Label();
            this.do取消订阅 = new Utility.WindowsForm.U按钮();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.out值)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(3, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.out值);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.do清空记录);
            this.splitContainer1.Panel2.Controls.Add(this.out记录);
            this.splitContainer1.Panel2.Controls.Add(this.do订阅);
            this.splitContainer1.Panel2.Controls.Add(this.label4);
            this.splitContainer1.Panel2.Controls.Add(this.do取消订阅);
            this.splitContainer1.Size = new System.Drawing.Size(636, 344);
            this.splitContainer1.SplitterDistance = 130;
            this.splitContainer1.TabIndex = 0;
            // 
            // out值
            // 
            this.out值.AllowUserToAddRows = false;
            this.out值.AllowUserToDeleteRows = false;
            this.out值.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.out值.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.out值.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.out值.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column6,
            this.Column5,
            this.Column2,
            this.Column7,
            this.Column3,
            this.Column4});
            this.out值.Location = new System.Drawing.Point(59, 0);
            this.out值.Name = "out值";
            this.out值.ReadOnly = true;
            this.out值.RowTemplate.Height = 23;
            this.out值.Size = new System.Drawing.Size(574, 127);
            this.out值.TabIndex = 31;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 17);
            this.label1.TabIndex = 30;
            this.label1.Text = "参数";
            // 
            // do清空记录
            // 
            this.do清空记录.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(164)))), ((int)(((byte)(221)))));
            this.do清空记录.FlatAppearance.BorderSize = 0;
            this.do清空记录.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.do清空记录.ForeColor = System.Drawing.Color.White;
            this.do清空记录.Location = new System.Drawing.Point(271, 3);
            this.do清空记录.Name = "do清空记录";
            this.do清空记录.Size = new System.Drawing.Size(100, 26);
            this.do清空记录.TabIndex = 36;
            this.do清空记录.Text = "清空记录";
            this.do清空记录.UseVisualStyleBackColor = false;
            this.do清空记录.大小 = new System.Drawing.Size(100, 26);
            this.do清空记录.文字颜色 = System.Drawing.Color.White;
            this.do清空记录.颜色 = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(164)))), ((int)(((byte)(221)))));
            // 
            // out记录
            // 
            this.out记录.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.out记录.Font = new System.Drawing.Font("新宋体", 9F);
            this.out记录.Location = new System.Drawing.Point(59, 35);
            this.out记录.Multiline = true;
            this.out记录.Name = "out记录";
            this.out记录.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.out记录.Size = new System.Drawing.Size(574, 172);
            this.out记录.TabIndex = 35;
            // 
            // do订阅
            // 
            this.do订阅.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(164)))), ((int)(((byte)(221)))));
            this.do订阅.FlatAppearance.BorderSize = 0;
            this.do订阅.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.do订阅.ForeColor = System.Drawing.Color.White;
            this.do订阅.Location = new System.Drawing.Point(59, 3);
            this.do订阅.Name = "do订阅";
            this.do订阅.Size = new System.Drawing.Size(100, 26);
            this.do订阅.TabIndex = 32;
            this.do订阅.Text = "订阅";
            this.do订阅.UseVisualStyleBackColor = false;
            this.do订阅.大小 = new System.Drawing.Size(100, 26);
            this.do订阅.文字颜色 = System.Drawing.Color.White;
            this.do订阅.颜色 = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(164)))), ((int)(((byte)(221)))));
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 35);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(32, 17);
            this.label4.TabIndex = 34;
            this.label4.Text = "记录";
            // 
            // do取消订阅
            // 
            this.do取消订阅.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(164)))), ((int)(((byte)(221)))));
            this.do取消订阅.FlatAppearance.BorderSize = 0;
            this.do取消订阅.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.do取消订阅.ForeColor = System.Drawing.Color.White;
            this.do取消订阅.Location = new System.Drawing.Point(165, 3);
            this.do取消订阅.Name = "do取消订阅";
            this.do取消订阅.Size = new System.Drawing.Size(100, 26);
            this.do取消订阅.TabIndex = 33;
            this.do取消订阅.Text = "取消订阅";
            this.do取消订阅.UseVisualStyleBackColor = false;
            this.do取消订阅.大小 = new System.Drawing.Size(100, 26);
            this.do取消订阅.文字颜色 = System.Drawing.Color.White;
            this.do取消订阅.颜色 = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(164)))), ((int)(((byte)(221)))));
            // 
            // Column1
            // 
            this.Column1.HeaderText = "名称";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 78;
            // 
            // Column6
            // 
            this.Column6.HeaderText = "类型";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            this.Column6.Width = 78;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "结构";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.Width = 78;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "值";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Visible = false;
            this.Column2.Width = 5;
            // 
            // Column7
            // 
            this.Column7.HeaderText = "默认值";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            this.Column7.Width = 78;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "描述";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Width = 78;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "范围";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.Width = 78;
            // 
            // F对象_事件
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.splitContainer1);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "F对象_事件";
            this.Padding = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.Size = new System.Drawing.Size(642, 344);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.out值)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView out值;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox out记录;
        private Utility.WindowsForm.U按钮 do订阅;
        private System.Windows.Forms.Label label4;
        private Utility.WindowsForm.U按钮 do取消订阅;
        private Utility.WindowsForm.U按钮 do清空记录;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
    }
}
