namespace POS.Controls
{
    partial class SinglePageControl
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.picLeft = new System.Windows.Forms.PictureBox();
            this.picRight = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLeft)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picRight)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 34F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33F));
            this.tableLayoutPanel1.Controls.Add(this.picLeft, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.picRight, 3, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(502, 38);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // picLeft
            // 
            this.picLeft.BackgroundImage = global::POS.Properties.Resources.右箭头;
            this.picLeft.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.picLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picLeft.Location = new System.Drawing.Point(115, 3);
            this.picLeft.Name = "picLeft";
            this.picLeft.Size = new System.Drawing.Size(74, 32);
            this.picLeft.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.picLeft.TabIndex = 0;
            this.picLeft.TabStop = false;
            this.picLeft.MouseEnter += new System.EventHandler(this.picLeft_MouseEnter);
            this.picLeft.MouseLeave += new System.EventHandler(this.picLeft_MouseLeave);
            // 
            // picRight
            // 
            this.picRight.BackgroundImage = global::POS.Properties.Resources.左箭头;
            this.picRight.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.picRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picRight.Location = new System.Drawing.Point(311, 3);
            this.picRight.Name = "picRight";
            this.picRight.Size = new System.Drawing.Size(74, 32);
            this.picRight.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.picRight.TabIndex = 1;
            this.picRight.TabStop = false;
            this.picRight.MouseEnter += new System.EventHandler(this.picRight_MouseEnter);
            this.picRight.MouseLeave += new System.EventHandler(this.picRight_MouseLeave);
            // 
            // SinglePageControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "SinglePageControl";
            this.Size = new System.Drawing.Size(502, 38);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picLeft)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picRight)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        /// <summary>
        /// .......
        /// </summary>
        public System.Windows.Forms.PictureBox picLeft;
        /// <summary>
        /// .........
        /// </summary>
        public System.Windows.Forms.PictureBox picRight;

    }
}
