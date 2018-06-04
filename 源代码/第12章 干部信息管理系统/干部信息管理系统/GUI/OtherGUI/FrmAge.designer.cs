namespace HBMISR.GUI.OtherGUI
{
    partial class FrmAge
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
        /// 自定义年龄段构造器
        /// </summary>
        private void InitializeComponent()
        {
            this.txt_Agebegin = new System.Windows.Forms.TextBox();
            this.txt_Ageend = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_search = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.time1 = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.rb = new System.Windows.Forms.RadioButton();
            this.rb1 = new System.Windows.Forms.RadioButton();
            this.rb0 = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txt_Agebegin
            // 
            this.txt_Agebegin.Font = new System.Drawing.Font("宋体", 10.5F);
            this.txt_Agebegin.Location = new System.Drawing.Point(327, 18);
            this.txt_Agebegin.Name = "txt_Agebegin";
            this.txt_Agebegin.Size = new System.Drawing.Size(37, 23);
            this.txt_Agebegin.TabIndex = 0;
            this.txt_Agebegin.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txt_Agebegin.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_Agebegin_KeyPress);
            // 
            // txt_Ageend
            // 
            this.txt_Ageend.Font = new System.Drawing.Font("宋体", 10.5F);
            this.txt_Ageend.Location = new System.Drawing.Point(394, 18);
            this.txt_Ageend.Name = "txt_Ageend";
            this.txt_Ageend.Size = new System.Drawing.Size(37, 23);
            this.txt_Ageend.TabIndex = 1;
            this.txt_Ageend.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txt_Ageend.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_Ageend_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(364, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = " ---";
            // 
            // btn_search
            // 
            this.btn_search.Font = new System.Drawing.Font("宋体", 11F);
            this.btn_search.Location = new System.Drawing.Point(665, 18);
            this.btn_search.Name = "btn_search";
            this.btn_search.Size = new System.Drawing.Size(64, 23);
            this.btn_search.TabIndex = 3;
            this.btn_search.Text = "查询";
            this.btn_search.UseVisualStyleBackColor = true;
            this.btn_search.Click += new System.EventHandler(this.btn_search_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.flowLayoutPanel1);
            this.groupBox1.Font = new System.Drawing.Font("宋体", 11F);
            this.groupBox1.Location = new System.Drawing.Point(5, 55);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(731, 301);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "年龄详情(人数)";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(10, 20);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(710, 272);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // time1
            // 
            this.time1.CustomFormat = "yyyy\'年\'MM\'月";
            this.time1.Font = new System.Drawing.Font("宋体", 11F);
            this.time1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.time1.Location = new System.Drawing.Point(517, 17);
            this.time1.Name = "time1";
            this.time1.ShowUpDown = true;
            this.time1.Size = new System.Drawing.Size(136, 24);
            this.time1.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 10.5F);
            this.label2.Location = new System.Drawing.Point(275, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 14);
            this.label2.TabIndex = 6;
            this.label2.Text = "年龄段";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 10.5F);
            this.label3.Location = new System.Drawing.Point(451, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 14);
            this.label3.TabIndex = 7;
            this.label3.Text = "截止时间";
            // 
            // rb
            // 
            this.rb.AutoSize = true;
            this.rb.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rb.Location = new System.Drawing.Point(181, 20);
            this.rb.Name = "rb";
            this.rb.Size = new System.Drawing.Size(81, 18);
            this.rb.TabIndex = 13;
            this.rb.TabStop = true;
            this.rb.Text = "所有人员";
            this.rb.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rb.UseVisualStyleBackColor = true;
            this.rb.CheckedChanged += new System.EventHandler(this.rb_CheckedChanged);
            // 
            // rb1
            // 
            this.rb1.AutoSize = true;
            this.rb1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rb1.Location = new System.Drawing.Point(100, 20);
            this.rb1.Name = "rb1";
            this.rb1.Size = new System.Drawing.Size(53, 18);
            this.rb1.TabIndex = 12;
            this.rb1.TabStop = true;
            this.rb1.Text = "正职";
            this.rb1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rb1.UseVisualStyleBackColor = true;
            this.rb1.CheckedChanged += new System.EventHandler(this.rb1_CheckedChanged);
            // 
            // rb0
            // 
            this.rb0.AutoSize = true;
            this.rb0.Checked = true;
            this.rb0.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rb0.Location = new System.Drawing.Point(19, 20);
            this.rb0.Name = "rb0";
            this.rb0.Size = new System.Drawing.Size(53, 18);
            this.rb0.TabIndex = 11;
            this.rb0.TabStop = true;
            this.rb0.Text = "副职";
            this.rb0.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rb0.UseVisualStyleBackColor = true;
            this.rb0.CheckedChanged += new System.EventHandler(this.rb0_CheckedChanged);
            // 
            // FrmAge
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(747, 369);
            this.Controls.Add(this.rb);
            this.Controls.Add(this.rb1);
            this.Controls.Add(this.rb0);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.time1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btn_search);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txt_Ageend);
            this.Controls.Add(this.txt_Agebegin);
            this.MaximumSize = new System.Drawing.Size(757, 399);
            this.MinimumSize = new System.Drawing.Size(757, 399);
            this.Name = "FrmAge";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "自定义年龄分析";
            this.Load += new System.EventHandler(this.AgeForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txt_Agebegin;
        private System.Windows.Forms.TextBox txt_Ageend;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_search;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DateTimePicker time1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton rb;
        private System.Windows.Forms.RadioButton rb1;
        private System.Windows.Forms.RadioButton rb0;
    }
}