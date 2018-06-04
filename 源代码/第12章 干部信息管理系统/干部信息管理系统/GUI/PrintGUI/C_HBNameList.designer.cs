namespace HBMISR.GUI.PrintGUI
{
    partial class C_HBNameList
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
        ///  用来显示初步人选名册构造器
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.BS_HBNameList = new System.Windows.Forms.BindingSource(this.components);
            this.RV_HBNameList = new Microsoft.Reporting.WinForms.ReportViewer();
            this.Class_HBNameListBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.BS_HBNameList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Class_HBNameListBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // RV_HBNameList
            // 
            this.RV_HBNameList.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "HBMS1__0_PrintClass_Class_HBNameList";
            reportDataSource1.Value = this.BS_HBNameList;
            this.RV_HBNameList.LocalReport.DataSources.Add(reportDataSource1);
            this.RV_HBNameList.LocalReport.ReportEmbeddedResource = "HBMISR.Report.HBNameList.rdlc";
            this.RV_HBNameList.Location = new System.Drawing.Point(0, 0);
            this.RV_HBNameList.Name = "RV_HBNameList";
            this.RV_HBNameList.ShowExportButton = false;
            this.RV_HBNameList.Size = new System.Drawing.Size(658, 457);
            this.RV_HBNameList.TabIndex = 0;
            // 
            // Class_HBNameListBindingSource
            // 
            this.Class_HBNameListBindingSource.DataMember = "Class_HBNameList";
            // 
            // C_HBNameList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.RV_HBNameList);
            this.Name = "C_HBNameList";
            this.Size = new System.Drawing.Size(658, 457);
            this.Load += new System.EventHandler(this.C_HBNameList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.BS_HBNameList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Class_HBNameListBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.BindingSource BS_HBNameList;
        private Microsoft.Reporting.WinForms.ReportViewer RV_HBNameList;
        private System.Windows.Forms.BindingSource Class_HBNameListBindingSource;
    }
}
