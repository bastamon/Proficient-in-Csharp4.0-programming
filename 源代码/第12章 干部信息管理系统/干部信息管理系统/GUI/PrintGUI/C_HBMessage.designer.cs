namespace HBMISR.GUI.PrintGUI
{
    partial class C_HBMessage
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
        /// 后备干部的信息采集表g构造器
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.BS_HBMessage = new System.Windows.Forms.BindingSource(this.components);
            this.RV_HBMessage = new Microsoft.Reporting.WinForms.ReportViewer();
            this.Class_HBMessageBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.BS_HBMessage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Class_HBMessageBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // RV_HBMessage
            // 
            this.RV_HBMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RV_HBMessage.DocumentMapWidth = 29;
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = this.BS_HBMessage;
            this.RV_HBMessage.LocalReport.DataSources.Add(reportDataSource1);
            this.RV_HBMessage.LocalReport.ReportEmbeddedResource = "HBMISR.Report.HBMessage.rdlc";
            this.RV_HBMessage.Location = new System.Drawing.Point(0, 0);
            this.RV_HBMessage.Name = "RV_HBMessage";
            this.RV_HBMessage.ShowExportButton = false;
            this.RV_HBMessage.Size = new System.Drawing.Size(775, 562);
            this.RV_HBMessage.TabIndex = 0;
            // 
            // Class_HBMessageBindingSource
            // 
            this.Class_HBMessageBindingSource.DataMember = "Class_HBMessage";
            // 
            // C_HBMessage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.RV_HBMessage);
            this.Name = "C_HBMessage";
            this.Size = new System.Drawing.Size(775, 562);
            this.Load += new System.EventHandler(this.C_HBMessage_Load);
            ((System.ComponentModel.ISupportInitialize)(this.BS_HBMessage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Class_HBMessageBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer RV_HBMessage;
        private System.Windows.Forms.BindingSource BS_HBMessage;
        private System.Windows.Forms.BindingSource Class_HBMessageBindingSource;
    }
}
