namespace HBMISR.GUI.NoteGUI
{
    partial class FrmMaterial
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
        /// 考察材料构造器
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.richTextBoxExtended1 = new System.Windows.Forms.RichTextBox();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.Item_Copy = new System.Windows.Forms.ToolStripMenuItem();
            this.Item_Paste = new System.Windows.Forms.ToolStripMenuItem();
            this.name_tB = new System.Windows.Forms.TextBox();
            this.contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("宋体", 10.5F);
            this.button1.Location = new System.Drawing.Point(378, 24);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(63, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "保存";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 10.5F);
            this.label1.Location = new System.Drawing.Point(120, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 14);
            this.label1.TabIndex = 4;
            this.label1.Text = "被考察人员姓名";
            // 
            // richTextBoxExtended1
            // 
            this.richTextBoxExtended1.ContextMenuStrip = this.contextMenuStrip;
            this.richTextBoxExtended1.Font = new System.Drawing.Font("宋体", 10.5F);
            this.richTextBoxExtended1.Location = new System.Drawing.Point(1, 60);
            this.richTextBoxExtended1.Name = "richTextBoxExtended1";
            this.richTextBoxExtended1.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.richTextBoxExtended1.Size = new System.Drawing.Size(565, 555);
            this.richTextBoxExtended1.TabIndex = 5;
            this.richTextBoxExtended1.Text = "";
            this.richTextBoxExtended1.TextChanged += new System.EventHandler(this.richTextBoxExtended1_TextChanged);
            this.richTextBoxExtended1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.richTextBoxExtended1_MouseDown);
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Item_Copy,
            this.Item_Paste});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(101, 48);
            // 
            // Item_Copy
            // 
            this.Item_Copy.Name = "Item_Copy";
            this.Item_Copy.Size = new System.Drawing.Size(100, 22);
            this.Item_Copy.Text = "复制";
            this.Item_Copy.Click += new System.EventHandler(this.Item_Copy_Click);
            // 
            // Item_Paste
            // 
            this.Item_Paste.Name = "Item_Paste";
            this.Item_Paste.Size = new System.Drawing.Size(100, 22);
            this.Item_Paste.Text = "粘贴";
            this.Item_Paste.Click += new System.EventHandler(this.Item_Paste_Click);
            // 
            // name_tB
            // 
            this.name_tB.Enabled = false;
            this.name_tB.Location = new System.Drawing.Point(228, 24);
            this.name_tB.Name = "name_tB";
            this.name_tB.Size = new System.Drawing.Size(124, 23);
            this.name_tB.TabIndex = 6;
            // 
            // FrmMaterial
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(568, 615);
            this.Controls.Add(this.name_tB);
            this.Controls.Add(this.richTextBoxExtended1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(584, 653);
            this.Name = "FrmMaterial";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "考察材料";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMaterial_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.contextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox richTextBoxExtended1;
        private System.Windows.Forms.TextBox name_tB;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem Item_Copy;
        private System.Windows.Forms.ToolStripMenuItem Item_Paste;
    }
}

