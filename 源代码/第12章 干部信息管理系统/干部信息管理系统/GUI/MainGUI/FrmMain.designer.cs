using HBMISR.GUI.PrintGUI;
namespace HBMISR.GUI.MainGUI
{
    partial class FrmMain
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.controlHead = new HBMISR.GUI.MainGUI.ControlHead();
            this.panel2 = new System.Windows.Forms.Panel();
            this.controlMain = new HBMISR.GUI.MainGUI.ControlMain();
            this.controlPrint = new HBMISR.GUI.PrintGUI.ControlPrint();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.controlHead);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1348, 104);
            this.panel1.TabIndex = 0;
            // 
            // controlHead
            // 
            this.controlHead.Dock = System.Windows.Forms.DockStyle.Fill;
            this.controlHead.Location = new System.Drawing.Point(0, 0);
            this.controlHead.Name = "controlHead";
            this.controlHead.Size = new System.Drawing.Size(1348, 104);
            this.controlHead.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.controlMain);
            this.panel2.Controls.Add(this.controlPrint);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 104);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1348, 553);
            this.panel2.TabIndex = 1;
            // 
            // controlMain
            // 
            this.controlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.controlMain.Font = new System.Drawing.Font("黑体", 12F);
            this.controlMain.Location = new System.Drawing.Point(0, 0);
            this.controlMain.Name = "controlMain";
            this.controlMain.Size = new System.Drawing.Size(1348, 553);
            this.controlMain.TabIndex = 0;
            // 
            // controlPrint
            // 
            this.controlPrint.BackColor = System.Drawing.SystemColors.Control;
            this.controlPrint.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.controlPrint.Dock = System.Windows.Forms.DockStyle.Fill;
            this.controlPrint.Location = new System.Drawing.Point(0, 0);
            this.controlPrint.Name = "controlPrint";
            this.controlPrint.Size = new System.Drawing.Size(1120, 553);
            this.controlPrint.TabIndex = 0;
            this.controlPrint.Unit = null;
            this.controlPrint.Unitclass = null;
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1348, 657);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "干部信息管理系统(上报端)";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmMain_KeyDown);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private ControlPrint controlPrint;
        private ControlMain controlMain;
        private ControlHead controlHead;
    }
}