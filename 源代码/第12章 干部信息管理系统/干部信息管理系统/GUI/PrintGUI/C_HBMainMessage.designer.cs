namespace HBMISR.GUI.PrintGUI
{
    partial class C_HBMainMessage
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
        /// 打印后备干部简要情况登记表构造器
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.BS_HBMainMessage = new System.Windows.Forms.BindingSource(this.components);
            this.RV_HBMainMessage = new Microsoft.Reporting.WinForms.ReportViewer();
            this.Class_HBMainMessageBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.BS_HBMainMessage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Class_HBMainMessageBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // BS_HBMainMessage
            // 
            this.BS_HBMainMessage.DataSource = typeof(HBMISR.Service.Class_HBMainMessage);
            // 
            // RV_HBMainMessage
            // 
            this.RV_HBMainMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = this.BS_HBMainMessage;
            this.RV_HBMainMessage.LocalReport.DataSources.Add(reportDataSource1);
            this.RV_HBMainMessage.LocalReport.ReportEmbeddedResource = "HBMISR.Report.HBMainMessage.rdlc";
            this.RV_HBMainMessage.Location = new System.Drawing.Point(0, 0);
            this.RV_HBMainMessage.Name = "RV_HBMainMessage";
            this.RV_HBMainMessage.ShowExportButton = false;
            this.RV_HBMainMessage.Size = new System.Drawing.Size(833, 538);
            this.RV_HBMainMessage.TabIndex = 0;
            // 
            // Class_HBMainMessageBindingSource
            // 
            this.Class_HBMainMessageBindingSource.DataMember = "Class_HBMainMessage";
            // 
            // C_HBMainMessage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.RV_HBMainMessage);
            this.Name = "C_HBMainMessage";
            this.Size = new System.Drawing.Size(833, 538);
            this.Load += new System.EventHandler(this.C_HBMainMessage_Load);
            ((System.ComponentModel.ISupportInitialize)(this.BS_HBMainMessage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Class_HBMainMessageBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer RV_HBMainMessage;
        private System.Windows.Forms.BindingSource BS_HBMainMessage;
        private System.Windows.Forms.BindingSource Class_HBMainMessageBindingSource;


    }
}
