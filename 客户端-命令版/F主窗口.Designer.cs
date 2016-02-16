namespace 通用访问工具
{
    partial class F主窗口
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("设备001");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("节点4");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("节点5");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("类型001", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3});
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("节点6");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("节点7");
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("节点8");
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("节点1", new System.Windows.Forms.TreeNode[] {
            treeNode5,
            treeNode6,
            treeNode7});
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("节点2");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F主窗口));
            this.out设备菜单 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.do设备_断开 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.do断开设备 = new Utility.WindowsForm.U按钮();
            this.do编辑设备 = new Utility.WindowsForm.U按钮();
            this.u窗口背景1 = new Utility.WindowsForm.U窗口背景();
            this.do客户 = new Utility.WindowsForm.U按钮();
            this.do开发 = new Utility.WindowsForm.U按钮();
            this.do工程 = new Utility.WindowsForm.U按钮();
            this.out提示 = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.out命令明细容器 = new Utility.WindowsForm.U容器();
            this.out命令列表容器 = new Utility.WindowsForm.U容器();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.out设备列表 = new System.Windows.Forms.TreeView();
            this.u窗体头1 = new Utility.WindowsForm.U窗体头();
            this.out标题 = new System.Windows.Forms.Label();
            this.do显示版本 = new System.Windows.Forms.PictureBox();
            this.out对象菜单 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.do显示对象 = new System.Windows.Forms.ToolStripMenuItem();
            this.u窗体脚1 = new Utility.WindowsForm.U窗体脚();
            this.out设备菜单.SuspendLayout();
            this.u窗口背景1.SuspendLayout();
            this.out提示.SuspendLayout();
            this.u窗体头1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.do显示版本)).BeginInit();
            this.out对象菜单.SuspendLayout();
            this.SuspendLayout();
            // 
            // out设备菜单
            // 
            this.out设备菜单.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.do设备_断开});
            this.out设备菜单.Name = "out设备菜单";
            this.out设备菜单.Size = new System.Drawing.Size(99, 26);
            // 
            // do设备_断开
            // 
            this.do设备_断开.Name = "do设备_断开";
            this.do设备_断开.Size = new System.Drawing.Size(98, 22);
            this.do设备_断开.Text = "断开";
            // 
            // do断开设备
            // 
            this.do断开设备.BackColor = System.Drawing.Color.White;
            this.do断开设备.BackgroundImage = global::通用访问工具.Properties.Resources.退出;
            this.do断开设备.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.do断开设备.FlatAppearance.BorderSize = 0;
            this.do断开设备.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.do断开设备.ForeColor = System.Drawing.Color.White;
            this.do断开设备.Location = new System.Drawing.Point(427, 61);
            this.do断开设备.Name = "do断开设备";
            this.do断开设备.Size = new System.Drawing.Size(20, 20);
            this.do断开设备.TabIndex = 14;
            this.toolTip1.SetToolTip(this.do断开设备, "断开当前设备");
            this.do断开设备.UseVisualStyleBackColor = false;
            this.do断开设备.大小 = new System.Drawing.Size(20, 20);
            this.do断开设备.颜色 = System.Drawing.Color.White;
            // 
            // do编辑设备
            // 
            this.do编辑设备.BackColor = System.Drawing.Color.White;
            this.do编辑设备.BackgroundImage = global::通用访问工具.Properties.Resources.编辑;
            this.do编辑设备.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.do编辑设备.FlatAppearance.BorderSize = 0;
            this.do编辑设备.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.do编辑设备.ForeColor = System.Drawing.Color.White;
            this.do编辑设备.Location = new System.Drawing.Point(164, 61);
            this.do编辑设备.Name = "do编辑设备";
            this.do编辑设备.Size = new System.Drawing.Size(20, 20);
            this.do编辑设备.TabIndex = 13;
            this.toolTip1.SetToolTip(this.do编辑设备, "编辑设备列表");
            this.do编辑设备.UseVisualStyleBackColor = false;
            this.do编辑设备.大小 = new System.Drawing.Size(20, 20);
            this.do编辑设备.颜色 = System.Drawing.Color.White;
            // 
            // u窗口背景1
            // 
            this.u窗口背景1.Controls.Add(this.u窗体脚1);
            this.u窗口背景1.Controls.Add(this.do客户);
            this.u窗口背景1.Controls.Add(this.do开发);
            this.u窗口背景1.Controls.Add(this.do工程);
            this.u窗口背景1.Controls.Add(this.out提示);
            this.u窗口背景1.Controls.Add(this.do断开设备);
            this.u窗口背景1.Controls.Add(this.do编辑设备);
            this.u窗口背景1.Controls.Add(this.out命令明细容器);
            this.u窗口背景1.Controls.Add(this.out命令列表容器);
            this.u窗口背景1.Controls.Add(this.label2);
            this.u窗口背景1.Controls.Add(this.label1);
            this.u窗口背景1.Controls.Add(this.out设备列表);
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
            // do客户
            // 
            this.do客户.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(164)))), ((int)(((byte)(221)))));
            this.do客户.FlatAppearance.BorderSize = 0;
            this.do客户.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.do客户.ForeColor = System.Drawing.Color.White;
            this.do客户.Location = new System.Drawing.Point(287, 58);
            this.do客户.Name = "do客户";
            this.do客户.Size = new System.Drawing.Size(40, 23);
            this.do客户.TabIndex = 18;
            this.do客户.Text = "客户";
            this.do客户.UseVisualStyleBackColor = false;
            this.do客户.大小 = new System.Drawing.Size(40, 23);
            this.do客户.颜色 = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(164)))), ((int)(((byte)(221)))));
            // 
            // do开发
            // 
            this.do开发.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(164)))), ((int)(((byte)(221)))));
            this.do开发.FlatAppearance.BorderSize = 0;
            this.do开发.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.do开发.ForeColor = System.Drawing.Color.White;
            this.do开发.Location = new System.Drawing.Point(379, 58);
            this.do开发.Name = "do开发";
            this.do开发.Size = new System.Drawing.Size(40, 23);
            this.do开发.TabIndex = 19;
            this.do开发.Text = "开发";
            this.do开发.UseVisualStyleBackColor = false;
            this.do开发.大小 = new System.Drawing.Size(40, 23);
            this.do开发.颜色 = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(164)))), ((int)(((byte)(221)))));
            // 
            // do工程
            // 
            this.do工程.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(164)))), ((int)(((byte)(221)))));
            this.do工程.FlatAppearance.BorderSize = 0;
            this.do工程.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.do工程.ForeColor = System.Drawing.Color.White;
            this.do工程.Location = new System.Drawing.Point(333, 58);
            this.do工程.Name = "do工程";
            this.do工程.Size = new System.Drawing.Size(40, 23);
            this.do工程.TabIndex = 18;
            this.do工程.Text = "工程";
            this.do工程.UseVisualStyleBackColor = false;
            this.do工程.大小 = new System.Drawing.Size(40, 23);
            this.do工程.颜色 = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(164)))), ((int)(((byte)(221)))));
            // 
            // out提示
            // 
            this.out提示.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.out提示.Controls.Add(this.label7);
            this.out提示.Controls.Add(this.label6);
            this.out提示.Controls.Add(this.label5);
            this.out提示.Controls.Add(this.label4);
            this.out提示.Location = new System.Drawing.Point(453, 52);
            this.out提示.Name = "out提示";
            this.out提示.Size = new System.Drawing.Size(815, 676);
            this.out提示.TabIndex = 17;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(13, 9);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(56, 17);
            this.label7.TabIndex = 10;
            this.label7.Text = "使用说明";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(77, 166);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(259, 17);
            this.label6.TabIndex = 2;
            this.label6.Text = "第三步:  右键点击设备或点击退出按钮断开设备";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(77, 113);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(302, 17);
            this.label5.TabIndex = 1;
            this.label5.Text = "第二步:  双击命令, 查看执行结果或者输入参数后再执行";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(77, 58);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(218, 17);
            this.label4.TabIndex = 0;
            this.label4.Text = "第一步:  双击访问设备, 查看可访问对象";
            // 
            // out命令明细容器
            // 
            this.out命令明细容器.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.out命令明细容器.Location = new System.Drawing.Point(453, 52);
            this.out命令明细容器.Name = "out命令明细容器";
            this.out命令明细容器.Size = new System.Drawing.Size(815, 676);
            this.out命令明细容器.TabIndex = 11;
            // 
            // out命令列表容器
            // 
            this.out命令列表容器.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.out命令列表容器.BackColor = System.Drawing.Color.WhiteSmoke;
            this.out命令列表容器.Location = new System.Drawing.Point(197, 87);
            this.out命令列表容器.Name = "out命令列表容器";
            this.out命令列表容器.Size = new System.Drawing.Size(250, 641);
            this.out命令列表容器.TabIndex = 10;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(193, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 17);
            this.label2.TabIndex = 9;
            this.label2.Text = "命令列表";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 17);
            this.label1.TabIndex = 8;
            this.label1.Text = "设备列表";
            // 
            // out设备列表
            // 
            this.out设备列表.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.out设备列表.BackColor = System.Drawing.Color.Gainsboro;
            this.out设备列表.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.out设备列表.ContextMenuStrip = this.out设备菜单;
            this.out设备列表.Location = new System.Drawing.Point(12, 87);
            this.out设备列表.Name = "out设备列表";
            treeNode1.Name = "节点3";
            treeNode1.Text = "设备001";
            treeNode2.Name = "节点4";
            treeNode2.Text = "节点4";
            treeNode3.Name = "节点5";
            treeNode3.Text = "节点5";
            treeNode4.Name = "节点0";
            treeNode4.Text = "类型001";
            treeNode5.Name = "节点6";
            treeNode5.Text = "节点6";
            treeNode6.Name = "节点7";
            treeNode6.Text = "节点7";
            treeNode7.Name = "节点8";
            treeNode7.Text = "节点8";
            treeNode8.Name = "节点1";
            treeNode8.Text = "节点1";
            treeNode9.Name = "节点2";
            treeNode9.Text = "节点2";
            this.out设备列表.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode4,
            treeNode8,
            treeNode9});
            this.out设备列表.Size = new System.Drawing.Size(178, 641);
            this.out设备列表.TabIndex = 5;
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
            this.u窗体头1.显示设置按钮 = true;
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
            this.do显示版本.Image = global::通用访问工具.Properties.Resources.K;
            this.do显示版本.Location = new System.Drawing.Point(11, 11);
            this.do显示版本.Name = "do显示版本";
            this.do显示版本.Size = new System.Drawing.Size(26, 26);
            this.do显示版本.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.do显示版本.TabIndex = 1;
            this.do显示版本.TabStop = false;
            // 
            // out对象菜单
            // 
            this.out对象菜单.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.do显示对象});
            this.out对象菜单.Name = "out对象菜单";
            this.out对象菜单.Size = new System.Drawing.Size(123, 26);
            // 
            // do显示对象
            // 
            this.do显示对象.Name = "do显示对象";
            this.do显示对象.Size = new System.Drawing.Size(122, 22);
            this.do显示对象.Text = "显示对象";
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
            // F主窗口
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1280, 740);
            this.Controls.Add(this.u窗口背景1);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(1920, 1050);
            this.Name = "F主窗口";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "通用访问 (命令版)";
            this.out设备菜单.ResumeLayout(false);
            this.u窗口背景1.ResumeLayout(false);
            this.u窗口背景1.PerformLayout();
            this.out提示.ResumeLayout(false);
            this.out提示.PerformLayout();
            this.u窗体头1.ResumeLayout(false);
            this.u窗体头1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.do显示版本)).EndInit();
            this.out对象菜单.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Utility.WindowsForm.U窗口背景 u窗口背景1;
        private Utility.WindowsForm.U窗体头 u窗体头1;
        private System.Windows.Forms.TreeView out设备列表;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox do显示版本;
        private System.Windows.Forms.Label out标题;
        private Utility.WindowsForm.U容器 out命令明细容器;
        private Utility.WindowsForm.U容器 out命令列表容器;
        private System.Windows.Forms.ContextMenuStrip out设备菜单;
        private System.Windows.Forms.ToolStripMenuItem do设备_断开;
        private Utility.WindowsForm.U按钮 do编辑设备;
        private System.Windows.Forms.ToolTip toolTip1;
        private Utility.WindowsForm.U按钮 do断开设备;
        private System.Windows.Forms.Panel out提示;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private Utility.WindowsForm.U按钮 do开发;
        private Utility.WindowsForm.U按钮 do工程;
        private System.Windows.Forms.ContextMenuStrip out对象菜单;
        private System.Windows.Forms.ToolStripMenuItem do显示对象;
        private Utility.WindowsForm.U按钮 do客户;
        private Utility.WindowsForm.U窗体脚 u窗体脚1;
    }
}

