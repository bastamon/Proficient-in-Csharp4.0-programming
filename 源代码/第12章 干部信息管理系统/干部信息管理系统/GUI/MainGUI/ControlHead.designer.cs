
namespace HBMISR.GUI.MainGUI
{
    /// <summary>
    /// 页首类
    /// </summary>
    partial class ControlHead
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
      ///页首控件的构造器
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.controlNavigation = new HBMISR.GUI.MainGUI.ControlNavigation();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1364, 100);
            this.panel1.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.Control;
            this.panel3.BackgroundImage = global::HBMISR.Properties.Resources.Newhead;
            this.panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel3.Controls.Add(this.controlNavigation);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1364, 100);
            this.panel3.TabIndex = 1;
            // 
            // controlNavigation
            // 
            this.controlNavigation.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.controlNavigation.BackColor = System.Drawing.Color.Transparent;
            this.controlNavigation.Font = new System.Drawing.Font("宋体", 10.5F);
            this.controlNavigation.ForeColor = System.Drawing.Color.DarkRed;
            this.controlNavigation.Location = new System.Drawing.Point(731, 70);
            this.controlNavigation.Name = "controlNavigation";
            this.controlNavigation.Size = new System.Drawing.Size(633, 27);
            this.controlNavigation.TabIndex = 0;
            this.controlNavigation.TabStop = false;
            // 
            // ControlHead
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "ControlHead";
            this.Size = new System.Drawing.Size(1364, 100);
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        public ControlNavigation controlNavigation;
    }
}
