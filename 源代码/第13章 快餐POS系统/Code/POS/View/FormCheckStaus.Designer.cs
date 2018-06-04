namespace POS.View
{
    partial class CheckForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.lbBack = new System.Windows.Forms.Label();
            this.txtGiveBack = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtPay = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtRequire = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnOk = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.tableLayoutPanel3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.lbBack, 0, 3);
            this.tableLayoutPanel3.Controls.Add(this.txtGiveBack, 1, 3);
            this.tableLayoutPanel3.Controls.Add(this.label1, 0, 2);
            this.tableLayoutPanel3.Controls.Add(this.txtPay, 1, 2);
            this.tableLayoutPanel3.Controls.Add(this.label4, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.txtRequire, 1, 1);
            this.tableLayoutPanel3.Controls.Add(this.panel1, 1, 5);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 7;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(543, 368);
            this.tableLayoutPanel3.TabIndex = 10;
            // 
            // lbBack
            // 
            this.lbBack.AutoSize = true;
            this.lbBack.BackColor = System.Drawing.Color.Transparent;
            this.lbBack.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbBack.Font = new System.Drawing.Font("宋体", 42F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbBack.Location = new System.Drawing.Point(4, 183);
            this.lbBack.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbBack.Name = "lbBack";
            this.lbBack.Size = new System.Drawing.Size(263, 55);
            this.lbBack.TabIndex = 1;
            this.lbBack.Text = " 找  零 ";
            this.lbBack.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtGiveBack
            // 
            this.txtGiveBack.AutoSize = true;
            this.txtGiveBack.BackColor = System.Drawing.Color.Transparent;
            this.txtGiveBack.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtGiveBack.Font = new System.Drawing.Font("宋体", 42F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtGiveBack.Location = new System.Drawing.Point(275, 183);
            this.txtGiveBack.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.txtGiveBack.Name = "txtGiveBack";
            this.txtGiveBack.Size = new System.Drawing.Size(264, 55);
            this.txtGiveBack.TabIndex = 3;
            this.txtGiveBack.Text = "无";
            this.txtGiveBack.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("宋体", 42F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(4, 128);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(263, 55);
            this.label1.TabIndex = 0;
            this.label1.Text = "已收金额";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtPay
            // 
            this.txtPay.AutoSize = true;
            this.txtPay.BackColor = System.Drawing.Color.Transparent;
            this.txtPay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtPay.Font = new System.Drawing.Font("宋体", 42F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtPay.Location = new System.Drawing.Point(275, 128);
            this.txtPay.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.txtPay.Name = "txtPay";
            this.txtPay.Size = new System.Drawing.Size(264, 55);
            this.txtPay.TabIndex = 2;
            this.txtPay.Text = "无";
            this.txtPay.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Font = new System.Drawing.Font("宋体", 42F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(4, 73);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(263, 55);
            this.label4.TabIndex = 5;
            this.label4.Text = "应收金额";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtRequire
            // 
            this.txtRequire.AutoSize = true;
            this.txtRequire.BackColor = System.Drawing.Color.Transparent;
            this.txtRequire.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtRequire.Font = new System.Drawing.Font("宋体", 42F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtRequire.Location = new System.Drawing.Point(275, 73);
            this.txtRequire.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.txtRequire.Name = "txtRequire";
            this.txtRequire.Size = new System.Drawing.Size(264, 55);
            this.txtRequire.TabIndex = 6;
            this.txtRequire.Text = "无";
            this.txtRequire.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnOk);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(274, 259);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(266, 86);
            this.panel1.TabIndex = 8;
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(112, 3);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(95, 80);
            this.btnOk.TabIndex = 7;
            this.btnOk.Text = "确定";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // CheckForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(543, 368);
            this.Controls.Add(this.tableLayoutPanel3);
            this.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "CheckForm";
            this.Text = "结账";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Label lbBack;
        private System.Windows.Forms.Label txtGiveBack;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label txtPay;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label txtRequire;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Timer timer1;
    }
}