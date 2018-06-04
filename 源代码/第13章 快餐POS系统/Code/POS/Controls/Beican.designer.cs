namespace POS.Controls
{
    partial class Beican
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
            this.dgvBeican = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.orderDinnerBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.orderDinnerBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.btnExit = new System.Windows.Forms.Button();
            this.tlpBeican = new System.Windows.Forms.TableLayoutPanel();
            this.tlpDown = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBeican)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.orderDinnerBindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.orderDinnerBindingSource)).BeginInit();
            this.tlpBeican.SuspendLayout();
            this.tlpDown.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvBeican
            // 
            this.dgvBeican.AutoGenerateColumns = false;
            this.dgvBeican.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.dgvBeican.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBeican.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2});
            this.dgvBeican.DataSource = this.orderDinnerBindingSource1;
            this.dgvBeican.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvBeican.Location = new System.Drawing.Point(0, 0);
            this.dgvBeican.Margin = new System.Windows.Forms.Padding(0);
            this.dgvBeican.Name = "dgvBeican";
            this.dgvBeican.ReadOnly = true;
            this.dgvBeican.RowTemplate.Height = 23;
            this.dgvBeican.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvBeican.Size = new System.Drawing.Size(520, 558);
            this.dgvBeican.TabIndex = 0;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "ProdName";
            this.dataGridViewTextBoxColumn1.HeaderText = "商品名字";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "Number";
            this.dataGridViewTextBoxColumn2.HeaderText = "数量";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // orderDinnerBindingSource1
            // 
            this.orderDinnerBindingSource1.DataSource = typeof(POS.Models.OrderDinner);
            // 
            // btnExit
            // 
            this.btnExit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnExit.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnExit.Location = new System.Drawing.Point(95, 3);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(117, 50);
            this.btnExit.TabIndex = 1;
            this.btnExit.Text = "确定";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // tlpBeican
            // 
            this.tlpBeican.ColumnCount = 1;
            this.tlpBeican.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpBeican.Controls.Add(this.dgvBeican, 0, 0);
            this.tlpBeican.Controls.Add(this.tlpDown, 0, 1);
            this.tlpBeican.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpBeican.Location = new System.Drawing.Point(0, 0);
            this.tlpBeican.Name = "tlpBeican";
            this.tlpBeican.RowCount = 2;
            this.tlpBeican.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 90F));
            this.tlpBeican.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tlpBeican.Size = new System.Drawing.Size(520, 620);
            this.tlpBeican.TabIndex = 2;
            // 
            // tlpDown
            // 
            this.tlpDown.ColumnCount = 3;
            this.tlpDown.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18F));
            this.tlpDown.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 24F));
            this.tlpDown.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 58F));
            this.tlpDown.Controls.Add(this.btnExit, 1, 0);
            this.tlpDown.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpDown.Location = new System.Drawing.Point(3, 561);
            this.tlpDown.Name = "tlpDown";
            this.tlpDown.RowCount = 1;
            this.tlpDown.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpDown.Size = new System.Drawing.Size(514, 56);
            this.tlpDown.TabIndex = 1;
            // 
            // Beican
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tlpBeican);
            this.Name = "Beican";
            this.Size = new System.Drawing.Size(520, 620);
            this.Load += new System.EventHandler(this.Beican_Load);
            this.SizeChanged += new System.EventHandler(this.Beican_SizeChanged);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBeican)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.orderDinnerBindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.orderDinnerBindingSource)).EndInit();
            this.tlpBeican.ResumeLayout(false);
            this.tlpDown.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvBeican;
        private System.Windows.Forms.BindingSource orderDinnerBindingSource;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.TableLayoutPanel tlpBeican;
        private System.Windows.Forms.BindingSource orderDinnerBindingSource1;
        private System.Windows.Forms.TableLayoutPanel tlpDown;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
    }

  
}
