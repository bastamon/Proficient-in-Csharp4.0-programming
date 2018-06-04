namespace Ex6_5
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
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.addop = new System.Windows.Forms.ToolStripMenuItem();
            this.subop = new System.Windows.Forms.ToolStripMenuItem();
            this.mulop = new System.Windows.Forms.ToolStripMenuItem();
            this.op = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.divop = new System.Windows.Forms.ToolStripMenuItem();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.op.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(97, 6);
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(131, 104);
            this.textBox3.Name = "textBox3";
            this.textBox3.ReadOnly = true;
            this.textBox3.Size = new System.Drawing.Size(82, 21);
            this.textBox3.TabIndex = 11;
            // 
            // addop
            // 
            this.addop.Name = "addop";
            this.addop.Size = new System.Drawing.Size(152, 22);
            this.addop.Text = "加法";
            this.addop.Click += new System.EventHandler(this.addop_Click);
            // 
            // subop
            // 
            this.subop.Name = "subop";
            this.subop.Size = new System.Drawing.Size(152, 22);
            this.subop.Text = "减法";
            this.subop.Click += new System.EventHandler(this.subop_Click);
            // 
            // mulop
            // 
            this.mulop.Name = "mulop";
            this.mulop.Size = new System.Drawing.Size(152, 22);
            this.mulop.Text = "乘法";
            this.mulop.Click += new System.EventHandler(this.mulop_Click);
            // 
            // op
            // 
            this.op.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addop,
            this.subop,
            this.mulop,
            this.toolStripSeparator1,
            this.divop});
            this.op.Name = "op";
            this.op.Size = new System.Drawing.Size(101, 98);
            this.op.Opened += new System.EventHandler(this.op_Opened);
            // 
            // divop
            // 
            this.divop.Name = "divop";
            this.divop.Size = new System.Drawing.Size(152, 22);
            this.divop.Text = "除法";
            this.divop.Click += new System.EventHandler(this.divop_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(102, 107);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 10;
            this.label3.Text = "结果";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(188, 59);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(82, 21);
            this.textBox2.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(159, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(23, 12);
            this.label2.TabIndex = 8;
            this.label2.Text = "数2";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(51, 59);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(82, 21);
            this.textBox1.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 62);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(23, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "数1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 185);
            this.ContextMenuStrip = this.op;
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.op.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.ToolStripMenuItem addop;
        private System.Windows.Forms.ToolStripMenuItem subop;
        private System.Windows.Forms.ToolStripMenuItem mulop;
        private System.Windows.Forms.ContextMenuStrip op;
        private System.Windows.Forms.ToolStripMenuItem divop;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
    }
}

