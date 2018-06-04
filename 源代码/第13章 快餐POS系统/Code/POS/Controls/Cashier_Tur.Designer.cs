namespace POS.Controls
{
    /// <summary>
    /// 抽大钞
    /// </summary>
    partial class Cashier_Tur
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
            this.cashierTurlabeWeek = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.Cancel = new System.Windows.Forms.Button();
            this.cashierTurlabeDay = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.823678F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 90.17632F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.cashierTurlabeWeek, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.cashierTurlabeDay, 1, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.283875F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 21.10815F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 21.10815F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 21.10815F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 21.10815F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.283519F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(373, 390);
            this.tableLayoutPanel1.TabIndex = 10;
            // 
            // cashierTurlabeWeek
            // 
            this.cashierTurlabeWeek.AutoSize = true;
            this.cashierTurlabeWeek.Font = new System.Drawing.Font("隶书", 15.75F, System.Drawing.FontStyle.Bold);
            this.cashierTurlabeWeek.ForeColor = System.Drawing.Color.White;
            this.cashierTurlabeWeek.Location = new System.Drawing.Point(39, 24);
            this.cashierTurlabeWeek.Name = "cashierTurlabeWeek";
            this.cashierTurlabeWeek.Size = new System.Drawing.Size(263, 42);
            this.cashierTurlabeWeek.TabIndex = 14;
            this.cashierTurlabeWeek.Text = "此收银员本周营业额是：\r\n0元";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.35752F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 66.64248F));
            this.tableLayoutPanel2.Controls.Add(this.Cancel, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(39, 355);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(331, 32);
            this.tableLayoutPanel2.TabIndex = 15;
            // 
            // Cancel
            // 
            this.Cancel.BackColor = System.Drawing.SystemColors.Control;
            this.Cancel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Cancel.Location = new System.Drawing.Point(3, 3);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(104, 26);
            this.Cancel.TabIndex = 7;
            this.Cancel.Text = "退出";
            this.Cancel.UseVisualStyleBackColor = false;
            this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // cashierTurlabeDay
            // 
            this.cashierTurlabeDay.AutoSize = true;
            this.cashierTurlabeDay.Font = new System.Drawing.Font("隶书", 15.75F, System.Drawing.FontStyle.Bold);
            this.cashierTurlabeDay.ForeColor = System.Drawing.Color.White;
            this.cashierTurlabeDay.Location = new System.Drawing.Point(39, 106);
            this.cashierTurlabeDay.Name = "cashierTurlabeDay";
            this.cashierTurlabeDay.Size = new System.Drawing.Size(263, 42);
            this.cashierTurlabeDay.TabIndex = 16;
            this.cashierTurlabeDay.Text = "此收银员本日营业额是：\r\n0元";
            // 
            // Cashier_Tur
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "Cashier_Tur";
            this.Size = new System.Drawing.Size(373, 390);
            this.Load += new System.EventHandler(this.Cashier_Tur_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label cashierTurlabeWeek;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button Cancel;
        private System.Windows.Forms.Label cashierTurlabeDay;

    }
}
