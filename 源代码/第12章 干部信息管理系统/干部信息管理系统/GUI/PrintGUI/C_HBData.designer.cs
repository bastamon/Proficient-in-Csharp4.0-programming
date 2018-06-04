namespace HBMISR.GUI.PrintGUI
{
    partial class C_HBData
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
        /// 打印后备干部考察材料构造器
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.BS_HBdata = new System.Windows.Forms.BindingSource(this.components);
            this.RV_HBData = new Microsoft.Reporting.WinForms.ReportViewer();
            this.Class_HBDataBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.BS_HBdata)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Class_HBDataBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // RV_HBData
            // 
            this.RV_HBData.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = this.BS_HBdata;
            this.RV_HBData.LocalReport.DataSources.Add(reportDataSource1);
            this.RV_HBData.LocalReport.ReportEmbeddedResource = "HBMISR.Report.HBData.rdlc";
            this.RV_HBData.Location = new System.Drawing.Point(0, 0);
            this.RV_HBData.Name = "RV_HBData";
            this.RV_HBData.Size = new System.Drawing.Size(632, 413);
            this.RV_HBData.TabIndex = 0;
            // 
            // Class_HBDataBindingSource
            // 
            this.Class_HBDataBindingSource.DataMember = "Class_HBData";
            // 
            // C_HBData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.RV_HBData);
            this.Name = "C_HBData";
            this.Size = new System.Drawing.Size(632, 413);
            this.Load += new System.EventHandler(this.C_HBData_Load);
            ((System.ComponentModel.ISupportInitialize)(this.BS_HBdata)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Class_HBDataBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer RV_HBData;
        private System.Windows.Forms.BindingSource BS_HBdata;
        private System.Windows.Forms.BindingSource Class_HBDataBindingSource;
    }
}
