namespace POS.Controls
{
    partial class PageControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PageControl));
            this.lblKind = new System.Windows.Forms.Label();
            this.lblProduct = new System.Windows.Forms.Label();
            this.picProdRight = new System.Windows.Forms.PictureBox();
            this.picProdLeft = new System.Windows.Forms.PictureBox();
            this.picKindRight = new System.Windows.Forms.PictureBox();
            this.picKindLeft = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picProdRight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picProdLeft)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picKindRight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picKindLeft)).BeginInit();
            this.SuspendLayout();
            // 
            // lblKind
            // 
            this.lblKind.AutoSize = true;
            this.lblKind.BackColor = System.Drawing.SystemColors.Control;
            this.lblKind.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblKind.Location = new System.Drawing.Point(60, 4);
            this.lblKind.Name = "lblKind";
            this.lblKind.Size = new System.Drawing.Size(52, 21);
            this.lblKind.TabIndex = 0;
            this.lblKind.Text = "类别";
            // 
            // lblProduct
            // 
            this.lblProduct.AutoSize = true;
            this.lblProduct.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblProduct.Location = new System.Drawing.Point(394, 2);
            this.lblProduct.Name = "lblProduct";
            this.lblProduct.Size = new System.Drawing.Size(49, 20);
            this.lblProduct.TabIndex = 1;
            this.lblProduct.Text = "商品";
            // 
            // picProdRight
            // 
            this.picProdRight.BackColor = System.Drawing.Color.LightSkyBlue;
            this.picProdRight.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("picProdRight.BackgroundImage")));
            this.picProdRight.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.picProdRight.Location = new System.Drawing.Point(442, 1);
            this.picProdRight.Name = "picProdRight";
            this.picProdRight.Size = new System.Drawing.Size(45, 27);
            this.picProdRight.TabIndex = 5;
            this.picProdRight.TabStop = false;
            this.picProdRight.Click += new System.EventHandler(this.picProdLeft_Click);
            this.picProdRight.MouseEnter += new System.EventHandler(this.picProdRight_MouseEnter);
            this.picProdRight.MouseLeave += new System.EventHandler(this.picProdRight_MouseLeave);
            // 
            // picProdLeft
            // 
            this.picProdLeft.BackColor = System.Drawing.Color.LightSkyBlue;
            this.picProdLeft.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("picProdLeft.BackgroundImage")));
            this.picProdLeft.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.picProdLeft.Location = new System.Drawing.Point(350, 1);
            this.picProdLeft.Name = "picProdLeft";
            this.picProdLeft.Size = new System.Drawing.Size(45, 27);
            this.picProdLeft.TabIndex = 4;
            this.picProdLeft.TabStop = false;
            this.picProdLeft.Click += new System.EventHandler(this.picProdRight_Click);
            this.picProdLeft.MouseEnter += new System.EventHandler(this.picProdLeft_MouseEnter);
            this.picProdLeft.MouseLeave += new System.EventHandler(this.picProdLeft_MouseLeave);
            // 
            // picKindRight
            // 
            this.picKindRight.BackColor = System.Drawing.Color.LightSkyBlue;
            this.picKindRight.BackgroundImage = global::POS.Properties.Resources.左箭头;
            this.picKindRight.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.picKindRight.Location = new System.Drawing.Point(112, 1);
            this.picKindRight.Name = "picKindRight";
            this.picKindRight.Size = new System.Drawing.Size(45, 27);
            this.picKindRight.TabIndex = 3;
            this.picKindRight.TabStop = false;
            this.picKindRight.Click += new System.EventHandler(this.picKindLeft_Click);
            this.picKindRight.MouseEnter += new System.EventHandler(this.picKindRight_MouseEnter);
            this.picKindRight.MouseLeave += new System.EventHandler(this.picKindRight_MouseLeave);
            // 
            // picKindLeft
            // 
            this.picKindLeft.BackColor = System.Drawing.Color.LightSkyBlue;
            this.picKindLeft.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("picKindLeft.BackgroundImage")));
            this.picKindLeft.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.picKindLeft.Location = new System.Drawing.Point(13, 1);
            this.picKindLeft.Name = "picKindLeft";
            this.picKindLeft.Size = new System.Drawing.Size(45, 27);
            this.picKindLeft.TabIndex = 2;
            this.picKindLeft.TabStop = false;
            this.picKindLeft.Click += new System.EventHandler(this.picKindRight_Click);
            this.picKindLeft.MouseEnter += new System.EventHandler(this.picKindLeft_MouseEnter);
            this.picKindLeft.MouseLeave += new System.EventHandler(this.picKindLeft_MouseLeave);
            // 
            // PageControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.picProdRight);
            this.Controls.Add(this.picProdLeft);
            this.Controls.Add(this.picKindRight);
            this.Controls.Add(this.picKindLeft);
            this.Controls.Add(this.lblProduct);
            this.Controls.Add(this.lblKind);
            this.Name = "PageControl";
            this.Size = new System.Drawing.Size(502, 29);
            this.SizeChanged += new System.EventHandler(this.PageControl_SizeChanged);
            ((System.ComponentModel.ISupportInitialize)(this.picProdRight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picProdLeft)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picKindRight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picKindLeft)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblKind;
        private System.Windows.Forms.Label lblProduct;
        /// <summary>
        /// ..........
        /// </summary>
        public  System.Windows.Forms.PictureBox picKindLeft;
        /// <summary>
        /// ........
        /// </summary>
        public System.Windows.Forms.PictureBox picKindRight;
        /// <summary>
        /// ........
        /// </summary>
        public System.Windows.Forms.PictureBox picProdLeft;
        /// <summary>
        /// .....
        /// </summary>
        public System.Windows.Forms.PictureBox picProdRight;
    }
}
