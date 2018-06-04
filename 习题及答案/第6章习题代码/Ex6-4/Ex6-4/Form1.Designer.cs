namespace Ex6_4
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.buttonClear = new System.Windows.Forms.Button();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.buttonAddChild = new System.Windows.Forms.Button();
            this.textBoxChild = new System.Windows.Forms.TextBox();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.buttonAddRoot = new System.Windows.Forms.Button();
            this.textBoxRoot = new System.Windows.Forms.TextBox();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.SuspendLayout();
            // 
            // buttonClear
            // 
            this.buttonClear.Location = new System.Drawing.Point(329, 166);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(75, 23);
            this.buttonClear.TabIndex = 13;
            this.buttonClear.Text = "清空";
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // buttonDelete
            // 
            this.buttonDelete.Location = new System.Drawing.Point(226, 166);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(75, 23);
            this.buttonDelete.TabIndex = 12;
            this.buttonDelete.Text = "删除节点";
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // buttonAddChild
            // 
            this.buttonAddChild.Location = new System.Drawing.Point(329, 95);
            this.buttonAddChild.Name = "buttonAddChild";
            this.buttonAddChild.Size = new System.Drawing.Size(75, 23);
            this.buttonAddChild.TabIndex = 11;
            this.buttonAddChild.Text = "添加班级";
            this.buttonAddChild.UseVisualStyleBackColor = true;
            this.buttonAddChild.Click += new System.EventHandler(this.buttonAddChild_Click);
            // 
            // textBoxChild
            // 
            this.textBoxChild.Location = new System.Drawing.Point(215, 95);
            this.textBoxChild.Name = "textBoxChild";
            this.textBoxChild.Size = new System.Drawing.Size(96, 21);
            this.textBoxChild.TabIndex = 10;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "系图标.jpg");
            this.imageList1.Images.SetKeyName(1, "系选中图标.jpg");
            this.imageList1.Images.SetKeyName(2, "班图标.bmp");
            this.imageList1.Images.SetKeyName(3, "班选中图标.bmp");
            // 
            // buttonAddRoot
            // 
            this.buttonAddRoot.Location = new System.Drawing.Point(329, 35);
            this.buttonAddRoot.Name = "buttonAddRoot";
            this.buttonAddRoot.Size = new System.Drawing.Size(75, 23);
            this.buttonAddRoot.TabIndex = 9;
            this.buttonAddRoot.Text = "添加系部";
            this.buttonAddRoot.UseVisualStyleBackColor = true;
            this.buttonAddRoot.Click += new System.EventHandler(this.buttonAddRoot_Click);
            // 
            // textBoxRoot
            // 
            this.textBoxRoot.Location = new System.Drawing.Point(215, 38);
            this.textBoxRoot.Name = "textBoxRoot";
            this.textBoxRoot.Size = new System.Drawing.Size(96, 21);
            this.textBoxRoot.TabIndex = 8;
            // 
            // treeView1
            // 
            this.treeView1.ImageIndex = 0;
            this.treeView1.ImageList = this.imageList1;
            this.treeView1.Location = new System.Drawing.Point(11, 23);
            this.treeView1.Name = "treeView1";
            this.treeView1.SelectedImageIndex = 0;
            this.treeView1.Size = new System.Drawing.Size(197, 200);
            this.treeView1.TabIndex = 7;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(414, 246);
            this.Controls.Add(this.buttonClear);
            this.Controls.Add(this.buttonDelete);
            this.Controls.Add(this.buttonAddChild);
            this.Controls.Add(this.textBoxChild);
            this.Controls.Add(this.buttonAddRoot);
            this.Controls.Add(this.textBoxRoot);
            this.Controls.Add(this.treeView1);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "班级分层";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonClear;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.Button buttonAddChild;
        private System.Windows.Forms.TextBox textBoxChild;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Button buttonAddRoot;
        private System.Windows.Forms.TextBox textBoxRoot;
        private System.Windows.Forms.TreeView treeView1;
    }
}

