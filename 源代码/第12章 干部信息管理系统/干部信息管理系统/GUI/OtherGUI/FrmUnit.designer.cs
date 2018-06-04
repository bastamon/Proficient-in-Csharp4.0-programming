namespace HBMISR.GUI.OtherGUI
{
    partial class FrmUnit
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmUnit));
            this.listView1 = new System.Windows.Forms.ListView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.TStMI_Delete = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMI_Alter = new System.Windows.Forms.ToolStripMenuItem();
            this.button_regist = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBox_select = new System.Windows.Forms.ComboBox();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.button_sure = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.label_showUnit = new System.Windows.Forms.TextBox();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.ContextMenuStrip = this.contextMenuStrip1;
            this.listView1.Font = new System.Drawing.Font("宋体", 10.5F);
            this.listView1.Location = new System.Drawing.Point(0, 31);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(759, 351);
            this.listView1.TabIndex = 1;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.List;
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Font = new System.Drawing.Font("黑体", 11F);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TStMI_Delete,
            this.TSMI_Alter});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(159, 48);
            // 
            // TStMI_Delete
            // 
            this.TStMI_Delete.Enabled = false;
            this.TStMI_Delete.Font = new System.Drawing.Font("宋体", 10.5F);
            this.TStMI_Delete.Name = "TStMI_Delete";
            this.TStMI_Delete.Size = new System.Drawing.Size(158, 22);
            this.TStMI_Delete.Text = "删除该单位";
            this.TStMI_Delete.Click += new System.EventHandler(this.TStMI_Delete_Click);
            // 
            // TSMI_Alter
            // 
            this.TSMI_Alter.Enabled = false;
            this.TSMI_Alter.Font = new System.Drawing.Font("宋体", 10.5F);
            this.TSMI_Alter.Name = "TSMI_Alter";
            this.TSMI_Alter.Size = new System.Drawing.Size(158, 22);
            this.TSMI_Alter.Text = "修改单位信息";
            this.TSMI_Alter.Click += new System.EventHandler(this.TSMI_Alter_Click);
            // 
            // button_regist
            // 
            this.button_regist.Font = new System.Drawing.Font("宋体", 10.5F);
            this.button_regist.Location = new System.Drawing.Point(211, 4);
            this.button_regist.Name = "button_regist";
            this.button_regist.Size = new System.Drawing.Size(101, 22);
            this.button_regist.TabIndex = 0;
            this.button_regist.Text = "注册新单位";
            this.button_regist.UseVisualStyleBackColor = true;
            this.button_regist.Click += new System.EventHandler(this.button_rigist_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 10.5F);
            this.label2.Location = new System.Drawing.Point(3, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 14);
            this.label2.TabIndex = 3;
            this.label2.Text = "单位类别";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 10.5F);
            this.label3.Location = new System.Drawing.Point(3, 393);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 14);
            this.label3.TabIndex = 4;
            this.label3.Text = "已选单位";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // comboBox_select
            // 
            this.comboBox_select.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_select.Font = new System.Drawing.Font("宋体", 10.5F);
            this.comboBox_select.FormattingEnabled = true;
            this.comboBox_select.Location = new System.Drawing.Point(69, 4);
            this.comboBox_select.Name = "comboBox_select";
            this.comboBox_select.Size = new System.Drawing.Size(128, 22);
            this.comboBox_select.TabIndex = 5;
            this.comboBox_select.SelectedIndexChanged += new System.EventHandler(this.comboBox_select_SelectedIndexChanged);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "unit.ico");
            // 
            // button_sure
            // 
            this.button_sure.Enabled = false;
            this.button_sure.Font = new System.Drawing.Font("宋体", 10.5F);
            this.button_sure.Location = new System.Drawing.Point(315, 389);
            this.button_sure.Name = "button_sure";
            this.button_sure.Size = new System.Drawing.Size(101, 22);
            this.button_sure.TabIndex = 0;
            this.button_sure.Text = "创建数据文件";
            this.button_sure.UseVisualStyleBackColor = true;
            this.button_sure.Click += new System.EventHandler(this.button_sure_Click);
            // 
            // label_showUnit
            // 
            this.label_showUnit.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.label_showUnit.Location = new System.Drawing.Point(69, 389);
            this.label_showUnit.Name = "label_showUnit";
            this.label_showUnit.ReadOnly = true;
            this.label_showUnit.Size = new System.Drawing.Size(230, 23);
            this.label_showUnit.TabIndex = 7;
            this.label_showUnit.TextChanged += new System.EventHandler(this.label_showUnit_TextChanged);
            // 
            // FrmUnit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(759, 416);
            this.Controls.Add(this.comboBox_select);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label_showUnit);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button_regist);
            this.Controls.Add(this.button_sure);
            this.Font = new System.Drawing.Font("宋体", 10.5F);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(775, 454);
            this.MinimumSize = new System.Drawing.Size(775, 454);
            this.Name = "FrmUnit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "选择单位";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.UnitForm_FormClosing);
            this.Load += new System.EventHandler(this.FrmUnit_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public  System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.Button button_regist;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.ComboBox comboBox_select;
        public  System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Button button_sure;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem TStMI_Delete;
        private System.Windows.Forms.ToolStripMenuItem TSMI_Alter;
        public System.Windows.Forms.TextBox label_showUnit;
    }
}