namespace POS.View
{
    partial class CheckSale
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
            this.checkSale01 = new POS.Controls.CheckSale0();
            this.SuspendLayout();
            // 
            // checkSale01
            // 
            this.checkSale01.Dock = System.Windows.Forms.DockStyle.Fill;
            this.checkSale01.Location = new System.Drawing.Point(0, 0);
            //this.checkSale01.mainform = null;
            this.checkSale01.Name = "checkSale01";
            this.checkSale01.Size = new System.Drawing.Size(969, 720);
            this.checkSale01.TabIndex = 0;
            // 
            // CheckSale
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(969, 720);
            this.Controls.Add(this.checkSale01);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MinimizeBox = false;
            this.Name = "CheckSale";
            this.ShowInTaskbar = false;
            this.Text = "交易查询";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.CheckSale_FormClosed_1);
            this.Load += new System.EventHandler(this.CheckSale_Load);
            this.LocationChanged += new System.EventHandler(this.CheckSale_LocationChanged);
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.CheckSale0 checkSale01;





    }
}