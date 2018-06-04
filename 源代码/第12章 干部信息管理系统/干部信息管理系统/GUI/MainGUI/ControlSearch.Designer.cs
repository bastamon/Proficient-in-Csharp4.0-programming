namespace HBMISR.GUI.MainGUI
{
    partial class ControlSearch
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
        /// 检索信息条构造器
        /// </summary>
        private void InitializeComponent()
        {
            this.Search = new System.Windows.Forms.Button();
            this.inputContent = new System.Windows.Forms.TextBox();
            this.option = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Search
            // 
            this.Search.BackColor = System.Drawing.Color.Transparent;
            this.Search.Font = new System.Drawing.Font("宋体", 10.5F);
            this.Search.Location = new System.Drawing.Point(416, 6);
            this.Search.Name = "Search";
            this.Search.Size = new System.Drawing.Size(65, 23);
            this.Search.TabIndex = 2;
            this.Search.Text = "搜索";
            this.Search.UseVisualStyleBackColor = false;
            this.Search.Click += new System.EventHandler(this.Search_Click);
            // 
            // inputContent
            // 
            this.inputContent.Font = new System.Drawing.Font("宋体", 10.5F);
            this.inputContent.Location = new System.Drawing.Point(177, 6);
            this.inputContent.Name = "inputContent";
            this.inputContent.Size = new System.Drawing.Size(230, 23);
            this.inputContent.TabIndex = 1;
            // 
            // option
            // 
            this.option.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.option.Font = new System.Drawing.Font("宋体", 10.5F);
            this.option.FormattingEnabled = true;
            this.option.Items.AddRange(new object[] {
            "姓名",
            "性别"});
            this.option.Location = new System.Drawing.Point(75, 6);
            this.option.Margin = new System.Windows.Forms.Padding(0);
            this.option.Name = "option";
            this.option.Size = new System.Drawing.Size(94, 22);
            this.option.TabIndex = 0;
            this.option.SelectedIndexChanged += new System.EventHandler(this.option_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("宋体", 10.5F);
            this.label7.Location = new System.Drawing.Point(20, 10);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(49, 14);
            this.label7.TabIndex = 10;
            this.label7.Text = "检索项";
            // 
            // ControlSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Search);
            this.Controls.Add(this.inputContent);
            this.Controls.Add(this.option);
            this.Controls.Add(this.label7);
            this.Font = new System.Drawing.Font("宋体", 10.5F);
            this.Name = "ControlSearch";
            this.Size = new System.Drawing.Size(496, 37);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Search;
        public System.Windows.Forms.TextBox inputContent;
        private System.Windows.Forms.Label label7;
        public System.Windows.Forms.ComboBox option;
       
    }
}
