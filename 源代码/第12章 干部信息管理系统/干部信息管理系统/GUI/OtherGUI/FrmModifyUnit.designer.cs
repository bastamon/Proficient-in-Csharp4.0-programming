namespace HBMISR.GUI.OtherGUI
{
    partial class FrmModifyUnit
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
            this.label1 = new System.Windows.Forms.Label();
            this.unitName = new System.Windows.Forms.TextBox();
            this.unitClass = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_Sure = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "单位名称";
            // 
            // unitName
            // 
            this.unitName.BackColor = System.Drawing.SystemColors.Window;
            this.unitName.Location = new System.Drawing.Point(86, 52);
            this.unitName.Name = "unitName";
            this.unitName.Size = new System.Drawing.Size(220, 23);
            this.unitName.TabIndex = 5;
            // 
            // unitClass
            // 
            this.unitClass.BackColor = System.Drawing.SystemColors.Window;
            this.unitClass.Enabled = false;
            this.unitClass.Location = new System.Drawing.Point(86, 20);
            this.unitClass.Name = "unitClass";
            this.unitClass.ReadOnly = true;
            this.unitClass.Size = new System.Drawing.Size(220, 23);
            this.unitClass.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(26, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 14);
            this.label2.TabIndex = 2;
            this.label2.Text = "单位类别";
            // 
            // btn_Sure
            // 
            this.btn_Sure.Location = new System.Drawing.Point(86, 84);
            this.btn_Sure.Name = "btn_Sure";
            this.btn_Sure.Size = new System.Drawing.Size(63, 23);
            this.btn_Sure.TabIndex = 6;
            this.btn_Sure.Text = "确认";
            this.btn_Sure.UseVisualStyleBackColor = true;
            this.btn_Sure.Click += new System.EventHandler(this.btn_Sure_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(243, 84);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(63, 23);
            this.button2.TabIndex = 7;
            this.button2.Text = "取消";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.unitClass);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.btn_Sure);
            this.groupBox1.Controls.Add(this.unitName);
            this.groupBox1.Location = new System.Drawing.Point(9, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(328, 119);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            // 
            // FrmModifyUnit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(345, 127);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("宋体", 10.5F);
            this.MaximumSize = new System.Drawing.Size(361, 165);
            this.MinimumSize = new System.Drawing.Size(361, 165);
            this.Name = "FrmModifyUnit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "修改单位信息";
            this.Load += new System.EventHandler(this.ModifyUnit_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox unitClass;
        private System.Windows.Forms.TextBox unitName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_Sure;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}