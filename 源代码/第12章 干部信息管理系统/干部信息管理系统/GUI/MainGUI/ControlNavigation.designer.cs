namespace HBMISR.GUI.MainGUI
{
    partial class ControlNavigation
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

        #region Component Designer generated code

        /// <summary> 
        ///页首导航条的构造器
        /// </summary>
        private void InitializeComponent()
        {
            this.button_Analyse = new System.Windows.Forms.Button();
            this.button_Insert = new System.Windows.Forms.Button();
            this.button_File = new System.Windows.Forms.Button();
            this.button_Print = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button_User = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button_Analyse
            // 
            this.button_Analyse.BackColor = System.Drawing.Color.WhiteSmoke;
            this.button_Analyse.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Olive;
            this.button_Analyse.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Olive;
            this.button_Analyse.Font = new System.Drawing.Font("黑体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_Analyse.ForeColor = System.Drawing.Color.Maroon;
            this.button_Analyse.Location = new System.Drawing.Point(192, 0);
            this.button_Analyse.Name = "button_Analyse";
            this.button_Analyse.Size = new System.Drawing.Size(80, 25);
            this.button_Analyse.TabIndex = 2;
            this.button_Analyse.Text = "数据分析";
            this.button_Analyse.UseVisualStyleBackColor = false;
            this.button_Analyse.Click += new System.EventHandler(this.button3_Click_Analyse);
            // 
            // button_Insert
            // 
            this.button_Insert.BackColor = System.Drawing.Color.WhiteSmoke;
            this.button_Insert.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Olive;
            this.button_Insert.Font = new System.Drawing.Font("黑体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_Insert.ForeColor = System.Drawing.Color.Maroon;
            this.button_Insert.Location = new System.Drawing.Point(114, 0);
            this.button_Insert.Name = "button_Insert";
            this.button_Insert.Size = new System.Drawing.Size(80, 25);
            this.button_Insert.TabIndex = 1;
            this.button_Insert.Text = "综合管理";
            this.button_Insert.UseVisualStyleBackColor = false;
            this.button_Insert.Click += new System.EventHandler(this.button2_Click_Insert);
            // 
            // button_File
            // 
            this.button_File.BackColor = System.Drawing.Color.WhiteSmoke;
            this.button_File.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Olive;
            this.button_File.Font = new System.Drawing.Font("黑体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_File.ForeColor = System.Drawing.Color.Maroon;
            this.button_File.Location = new System.Drawing.Point(36, 0);
            this.button_File.Name = "button_File";
            this.button_File.Size = new System.Drawing.Size(80, 25);
            this.button_File.TabIndex = 0;
            this.button_File.Text = "文件管理";
            this.button_File.UseVisualStyleBackColor = false;
            this.button_File.Click += new System.EventHandler(this.button1_Click_File);
            // 
            // button_Print
            // 
            this.button_Print.BackColor = System.Drawing.Color.WhiteSmoke;
            this.button_Print.Font = new System.Drawing.Font("黑体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_Print.ForeColor = System.Drawing.Color.Maroon;
            this.button_Print.Location = new System.Drawing.Point(270, 0);
            this.button_Print.Name = "button_Print";
            this.button_Print.Size = new System.Drawing.Size(80, 25);
            this.button_Print.TabIndex = 3;
            this.button_Print.Text = "打印管理";
            this.button_Print.UseVisualStyleBackColor = false;
            this.button_Print.Click += new System.EventHandler(this.button4_Click_Print);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.button_User);
            this.panel1.Controls.Add(this.button_Print);
            this.panel1.Controls.Add(this.button_File);
            this.panel1.Controls.Add(this.button_Insert);
            this.panel1.Controls.Add(this.button_Analyse);
            this.panel1.Font = new System.Drawing.Font("黑体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.panel1.Location = new System.Drawing.Point(0, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(549, 26);
            this.panel1.TabIndex = 0;
            // 
            // button_User
            // 
            this.button_User.BackColor = System.Drawing.Color.WhiteSmoke;
            this.button_User.Font = new System.Drawing.Font("黑体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_User.ForeColor = System.Drawing.Color.Maroon;
            this.button_User.Location = new System.Drawing.Point(348, 0);
            this.button_User.Name = "button_User";
            this.button_User.Size = new System.Drawing.Size(80, 25);
            this.button_User.TabIndex = 4;
            this.button_User.Text = "用户管理";
            this.button_User.UseVisualStyleBackColor = false;
            this.button_User.Click += new System.EventHandler(this.button_User_Click);
            // 
            // ControlNavigation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.panel1);
            this.Name = "ControlNavigation";
            this.Size = new System.Drawing.Size(548, 27);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button_Analyse;
        private System.Windows.Forms.Button button_Insert;
        private System.Windows.Forms.Button button_File;
        private System.Windows.Forms.Button button_Print;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button_User;


    }
}
