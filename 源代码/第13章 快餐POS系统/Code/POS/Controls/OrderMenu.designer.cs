namespace POS.Controls
{
    partial class OrderMenu
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
            this.components = new System.ComponentModel.Container();
            this.dgvOrderMenu = new System.Windows.Forms.DataGridView();
            this.xuDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.prodNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.priceDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.numberDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.discountDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.totalDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.orderDinnerBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.orderDinnerBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.btnUp = new System.Windows.Forms.Button();
            this.btnBeiCan = new System.Windows.Forms.Button();
            this.btnMinuse = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnDown = new System.Windows.Forms.Button();
            this.totalPrice1 = new System.Windows.Forms.Label();
            this.totalPrice2 = new System.Windows.Forms.Label();
            this.sale_type = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrderMenu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.orderDinnerBindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.orderDinnerBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvOrderMenu
            // 
            this.dgvOrderMenu.AllowUserToAddRows = false;
            this.dgvOrderMenu.AutoGenerateColumns = false;
            this.dgvOrderMenu.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.dgvOrderMenu.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvOrderMenu.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOrderMenu.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.xuDataGridViewTextBoxColumn,
            this.prodNameDataGridViewTextBoxColumn,
            this.priceDataGridViewTextBoxColumn,
            this.numberDataGridViewTextBoxColumn,
            this.discountDataGridViewTextBoxColumn,
            this.totalDataGridViewTextBoxColumn});
            this.dgvOrderMenu.DataSource = this.orderDinnerBindingSource1;
            this.dgvOrderMenu.Location = new System.Drawing.Point(2, 0);
            this.dgvOrderMenu.Name = "dgvOrderMenu";
            this.dgvOrderMenu.ReadOnly = true;
            this.dgvOrderMenu.RowHeadersVisible = false;
            this.dgvOrderMenu.RowTemplate.Height = 23;
            this.dgvOrderMenu.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvOrderMenu.Size = new System.Drawing.Size(430, 222);
            this.dgvOrderMenu.TabIndex = 15;
            this.dgvOrderMenu.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvOrderMenu_CellMouseClick);
            // 
            // xuDataGridViewTextBoxColumn
            // 
            this.xuDataGridViewTextBoxColumn.DataPropertyName = "Xu";
            this.xuDataGridViewTextBoxColumn.HeaderText = "序";
            this.xuDataGridViewTextBoxColumn.Name = "xuDataGridViewTextBoxColumn";
            this.xuDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // prodNameDataGridViewTextBoxColumn
            // 
            this.prodNameDataGridViewTextBoxColumn.DataPropertyName = "ProdName";
            this.prodNameDataGridViewTextBoxColumn.HeaderText = "商品名称";
            this.prodNameDataGridViewTextBoxColumn.Name = "prodNameDataGridViewTextBoxColumn";
            this.prodNameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // priceDataGridViewTextBoxColumn
            // 
            this.priceDataGridViewTextBoxColumn.DataPropertyName = "Price";
            this.priceDataGridViewTextBoxColumn.HeaderText = "单价";
            this.priceDataGridViewTextBoxColumn.Name = "priceDataGridViewTextBoxColumn";
            this.priceDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // numberDataGridViewTextBoxColumn
            // 
            this.numberDataGridViewTextBoxColumn.DataPropertyName = "Number";
            this.numberDataGridViewTextBoxColumn.HeaderText = "数量";
            this.numberDataGridViewTextBoxColumn.Name = "numberDataGridViewTextBoxColumn";
            this.numberDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // discountDataGridViewTextBoxColumn
            // 
            this.discountDataGridViewTextBoxColumn.DataPropertyName = "Discount";
            this.discountDataGridViewTextBoxColumn.HeaderText = "折扣";
            this.discountDataGridViewTextBoxColumn.Name = "discountDataGridViewTextBoxColumn";
            this.discountDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // totalDataGridViewTextBoxColumn
            // 
            this.totalDataGridViewTextBoxColumn.DataPropertyName = "Total";
            this.totalDataGridViewTextBoxColumn.HeaderText = "小计";
            this.totalDataGridViewTextBoxColumn.Name = "totalDataGridViewTextBoxColumn";
            this.totalDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // orderDinnerBindingSource1
            // 
            this.orderDinnerBindingSource1.DataSource = typeof(POS.Models.OrderDinner);
            // 
            // btnUp
            // 
            this.btnUp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.btnUp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnUp.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnUp.Location = new System.Drawing.Point(430, 192);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(53, 50);
            this.btnUp.TabIndex = 20;
            this.btnUp.Text = "上";
            this.btnUp.UseVisualStyleBackColor = false;
            this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
            // 
            // btnBeiCan
            // 
            this.btnBeiCan.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.btnBeiCan.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBeiCan.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnBeiCan.Location = new System.Drawing.Point(430, 144);
            this.btnBeiCan.Name = "btnBeiCan";
            this.btnBeiCan.Size = new System.Drawing.Size(53, 50);
            this.btnBeiCan.TabIndex = 19;
            this.btnBeiCan.Text = "备餐";
            this.btnBeiCan.UseVisualStyleBackColor = false;
            this.btnBeiCan.Click += new System.EventHandler(this.btnBeiCan_Click);
            // 
            // btnMinuse
            // 
            this.btnMinuse.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.btnMinuse.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnMinuse.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnMinuse.Location = new System.Drawing.Point(430, 96);
            this.btnMinuse.Name = "btnMinuse";
            this.btnMinuse.Size = new System.Drawing.Size(53, 50);
            this.btnMinuse.TabIndex = 18;
            this.btnMinuse.Text = "减";
            this.btnMinuse.UseVisualStyleBackColor = false;
            this.btnMinuse.Click += new System.EventHandler(this.btnMinuse_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.btnAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAdd.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnAdd.Location = new System.Drawing.Point(430, 48);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(53, 50);
            this.btnAdd.TabIndex = 17;
            this.btnAdd.Text = "加";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnDown
            // 
            this.btnDown.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.btnDown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnDown.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnDown.Location = new System.Drawing.Point(430, 0);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(53, 50);
            this.btnDown.TabIndex = 16;
            this.btnDown.Text = "下";
            this.btnDown.UseVisualStyleBackColor = false;
            this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
            // 
            // totalPrice1
            // 
            this.totalPrice1.AutoSize = true;
            this.totalPrice1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.totalPrice1.Location = new System.Drawing.Point(240, 226);
            this.totalPrice1.Name = "totalPrice1";
            this.totalPrice1.Size = new System.Drawing.Size(35, 14);
            this.totalPrice1.TabIndex = 21;
            this.totalPrice1.Text = "总价";
            // 
            // totalPrice2
            // 
            this.totalPrice2.AutoSize = true;
            this.totalPrice2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.totalPrice2.Location = new System.Drawing.Point(357, 226);
            this.totalPrice2.Name = "totalPrice2";
            this.totalPrice2.Size = new System.Drawing.Size(42, 14);
            this.totalPrice2.TabIndex = 22;
            this.totalPrice2.Text = "0.0元";
            // 
            // sale_type
            // 
            this.sale_type.AutoSize = true;
            this.sale_type.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.sale_type.Location = new System.Drawing.Point(162, 227);
            this.sale_type.Name = "sale_type";
            this.sale_type.Size = new System.Drawing.Size(0, 14);
            this.sale_type.TabIndex = 23;
            // 
            // OrderMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.sale_type);
            this.Controls.Add(this.dgvOrderMenu);
            this.Controls.Add(this.btnUp);
            this.Controls.Add(this.btnBeiCan);
            this.Controls.Add(this.btnMinuse);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnDown);
            this.Controls.Add(this.totalPrice1);
            this.Controls.Add(this.totalPrice2);
            this.Name = "OrderMenu";
            this.Size = new System.Drawing.Size(482, 240);
            this.Load += new System.EventHandler(this.OrderMenu_Load);
            this.SizeChanged += new System.EventHandler(this.OrderMenu_SizeChanged);
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrderMenu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.orderDinnerBindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.orderDinnerBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnUp;
        private System.Windows.Forms.Button btnBeiCan;
        private System.Windows.Forms.Button btnMinuse;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnDown;
        private System.Windows.Forms.BindingSource orderDinnerBindingSource;
        private System.Windows.Forms.Label totalPrice1;
        private System.Windows.Forms.Label totalPrice2;
        private System.Windows.Forms.BindingSource orderDinnerBindingSource1;
        private System.Windows.Forms.DataGridViewTextBoxColumn xuDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn prodNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn priceDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn numberDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn discountDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn totalDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridView dgvOrderMenu;
        private System.Windows.Forms.Label sale_type;
    }
}
