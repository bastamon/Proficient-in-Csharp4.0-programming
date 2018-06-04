using POS.Controls;
using POS.Data;
namespace POS.View
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            this.mainTlp = new System.Windows.Forms.TableLayoutPanel();
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.tlpRight = new System.Windows.Forms.TableLayoutPanel();
            this.btnPanelKind = new POS.Controls.BtnPanelKind();
            this.pageControl = new POS.Controls.PageControl();
            this.btnPanelProd = new POS.Controls.BtnPanelProd();
            this.tlpLeft = new System.Windows.Forms.TableLayoutPanel();
            this.tlpLeftDown = new System.Windows.Forms.TableLayoutPanel();
            this.functionPanel = new POS.Controls.FunctionPanel();
            this.number = new POS.Controls.Number();
            this.orderMenu = new POS.Controls.OrderMenu();
            this.showInfo = new POS.Controls.ShowInfo();
            this.roll = new POS.Controls.Roll();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.operPara1 = new POS.Service.OperPara(this.components);
            this.mainTlp.SuspendLayout();
            this.tlpMain.SuspendLayout();
            this.tlpRight.SuspendLayout();
            this.tlpLeft.SuspendLayout();
            this.tlpLeftDown.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainTlp
            // 
            this.mainTlp.ColumnCount = 1;
            this.mainTlp.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mainTlp.Controls.Add(this.tlpMain, 0, 2);
            this.mainTlp.Controls.Add(this.showInfo, 0, 1);
            this.mainTlp.Controls.Add(this.roll, 0, 0);
            this.mainTlp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainTlp.Location = new System.Drawing.Point(0, 0);
            this.mainTlp.Margin = new System.Windows.Forms.Padding(0);
            this.mainTlp.Name = "mainTlp";
            this.mainTlp.RowCount = 3;
            this.mainTlp.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.mainTlp.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.mainTlp.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 90F));
            this.mainTlp.Size = new System.Drawing.Size(1284, 716);
            this.mainTlp.TabIndex = 5;
            // 
            // tlpMain
            // 
            this.tlpMain.ColumnCount = 2;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 46.85836F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 53.14164F));
            this.tlpMain.Controls.Add(this.tlpRight, 1, 0);
            this.tlpMain.Controls.Add(this.tlpLeft, 0, 0);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(0, 70);
            this.tlpMain.Margin = new System.Windows.Forms.Padding(0);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 1;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpMain.Size = new System.Drawing.Size(1284, 646);
            this.tlpMain.TabIndex = 0;
            // 
            // tlpRight
            // 
            this.tlpRight.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.tlpRight.ColumnCount = 1;
            this.tlpRight.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpRight.Controls.Add(this.btnPanelKind, 0, 0);
            this.tlpRight.Controls.Add(this.pageControl, 0, 1);
            this.tlpRight.Controls.Add(this.btnPanelProd, 0, 2);
            this.tlpRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpRight.Location = new System.Drawing.Point(601, 0);
            this.tlpRight.Margin = new System.Windows.Forms.Padding(0);
            this.tlpRight.Name = "tlpRight";
            this.tlpRight.RowCount = 3;
            this.tlpRight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 17.99886F));
            this.tlpRight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 3.998333F));
            this.tlpRight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 78.00281F));
            this.tlpRight.Size = new System.Drawing.Size(683, 646);
            this.tlpRight.TabIndex = 1;
            // 
            // btnPanelKind
            // 
            this.btnPanelKind.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnPanelKind.ColumnRow = new System.Drawing.Size(7, 2);
            this.btnPanelKind.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnPanelKind.IsBtnColorChange = false;
            this.btnPanelKind.Location = new System.Drawing.Point(0, 0);
            this.btnPanelKind.Margin = new System.Windows.Forms.Padding(0);
            this.btnPanelKind.Name = "btnPanelKind";
            this.btnPanelKind.Page = 0;
            this.btnPanelKind.PageControl = this.pageControl;
            this.btnPanelKind.Size = new System.Drawing.Size(683, 116);
            this.btnPanelKind.StringArray = new string[] {
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null};
            this.btnPanelKind.TabIndex = 0;
            this.btnPanelKind.TotalBtn = 0;
            this.btnPanelKind.GetInfo += new POS.Controls.BtnPanelKind.GetInfoEventHandler(this.BtnPanelKind_GetInfo);
            this.btnPanelKind.SetInfo += new POS.Controls.BtnPanelKind.SetInfoEventHandler(this.BtnPanelKind_SetInfo);
            // 
            // pageControl
            // 
            this.pageControl.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.pageControl.BackgroundColor = System.Drawing.SystemColors.ActiveCaption;
            this.pageControl.ButtonPanelKind = this.btnPanelKind;
            this.pageControl.ButtonPanelProd = this.btnPanelProd;
            this.pageControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pageControl.Location = new System.Drawing.Point(0, 116);
            this.pageControl.Margin = new System.Windows.Forms.Padding(0);
            this.pageControl.Name = "pageControl";
            this.pageControl.SetDistance = 10;
            this.pageControl.Size = new System.Drawing.Size(683, 21);
            this.pageControl.TabIndex = 1;
            this.pageControl.TextFont = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            // 
            // btnPanelProd
            // 
            this.btnPanelProd.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnPanelProd.ColumnRow = new System.Drawing.Size(7, 9);
            this.btnPanelProd.Dep_id = null;
            this.btnPanelProd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnPanelProd.Location = new System.Drawing.Point(0, 141);
            this.btnPanelProd.Margin = new System.Windows.Forms.Padding(0);
            this.btnPanelProd.Name = "btnPanelProd";
            this.btnPanelProd.Page = 0;
            this.btnPanelProd.PageControl = this.pageControl;
            this.btnPanelProd.Size = new System.Drawing.Size(683, 505);
            this.btnPanelProd.StringArray = new string[] {
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null};
            this.btnPanelProd.TabIndex = 2;
            this.btnPanelProd.TotalBtn = 0;
            this.btnPanelProd.GetInfo += new POS.Controls.BtnPanelProd.GetInfoEventHandler(this.BtnPanelProd_GetInfo);
            this.btnPanelProd.SetInfo += new POS.Controls.BtnPanelProd.SetInfoEventHandler(this.BtnPanelProd_SetInfo);
            // 
            // tlpLeft
            // 
            this.tlpLeft.ColumnCount = 1;
            this.tlpLeft.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpLeft.Controls.Add(this.tlpLeftDown, 0, 1);
            this.tlpLeft.Controls.Add(this.orderMenu, 0, 0);
            this.tlpLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpLeft.Location = new System.Drawing.Point(0, 0);
            this.tlpLeft.Margin = new System.Windows.Forms.Padding(0);
            this.tlpLeft.Name = "tlpLeft";
            this.tlpLeft.RowCount = 2;
            this.tlpLeft.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 41F));
            this.tlpLeft.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 59F));
            this.tlpLeft.Size = new System.Drawing.Size(601, 646);
            this.tlpLeft.TabIndex = 2;
            // 
            // tlpLeftDown
            // 
            this.tlpLeftDown.ColumnCount = 2;
            this.tlpLeftDown.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 62F));
            this.tlpLeftDown.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 38F));
            this.tlpLeftDown.Controls.Add(this.functionPanel, 0, 0);
            this.tlpLeftDown.Controls.Add(this.number, 1, 0);
            this.tlpLeftDown.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpLeftDown.Location = new System.Drawing.Point(3, 267);
            this.tlpLeftDown.Name = "tlpLeftDown";
            this.tlpLeftDown.RowCount = 1;
            this.tlpLeftDown.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpLeftDown.Size = new System.Drawing.Size(595, 376);
            this.tlpLeftDown.TabIndex = 0;
            // 
            // functionPanel
            // 
            this.functionPanel.AllBtnColor = System.Drawing.Color.Empty;
            this.functionPanel.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.functionPanel.BtnFont = null;
            this.functionPanel.CollumnRow = new System.Drawing.Size(4, 5);
            this.functionPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.functionPanel.IsFunctSet = false;
            this.functionPanel.Location = new System.Drawing.Point(0, 0);
            this.functionPanel.MainForm = null;
            this.functionPanel.Margin = new System.Windows.Forms.Padding(0);
            this.functionPanel.Name = "functionPanel";
            this.functionPanel.Page = 0;
            this.functionPanel.PageBtnNumber = 19;
            this.functionPanel.Size = new System.Drawing.Size(368, 376);
            this.functionPanel.StringArray = new string[0];
            this.functionPanel.TabIndex = 0;
            this.functionPanel.TotalBtn = 0;
            this.functionPanel.GetInfo += new POS.Controls.FunctionPanel.GetInfoEventHandler(this.FunctionPanel_GetInfo);
            // 
            // number
            // 
            this.number.AllBtnColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.number.ButtonFont = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Bold);
            this.number.Dock = System.Windows.Forms.DockStyle.Fill;
            this.number.FunctionName = "";
            this.number.Location = new System.Drawing.Point(368, 0);
            this.number.MainForm = null;
            this.number.Margin = new System.Windows.Forms.Padding(0);
            this.number.Name = "number";
            this.number.Size = new System.Drawing.Size(227, 376);
            this.number.StringArray = new string[] {
        "7",
        "8",
        "9",
        "4",
        "5",
        "6",
        "1",
        "2",
        "3",
        "0",
        ".",
        "修改",
        "",
        "清除"};
            this.number.TabIndex = 1;
            this.number.TextBoxFont = new System.Drawing.Font("宋体", 15F);
            this.number.TextBoxText = "";
            // 
            // orderMenu
            // 
            this.orderMenu.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.orderMenu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.orderMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.orderMenu.Location = new System.Drawing.Point(0, 0);
            this.orderMenu.MainForm = null;
            this.orderMenu.Margin = new System.Windows.Forms.Padding(0);
            this.orderMenu.Name = "orderMenu";
            this.orderMenu.Sale_type = "";
            this.orderMenu.SelectedGroupProd = null;
            this.orderMenu.Size = new System.Drawing.Size(601, 264);
            this.orderMenu.TabIndex = 1;
            this.orderMenu.TotalPrice = "0.0元";
            this.orderMenu.GetInfo += new POS.Controls.OrderMenu.GetInfoEventHandler(this.OrderMenu_GetInfo);
            this.orderMenu.AlterProd += new POS.Controls.OrderMenu.DataAlterEventHandler(this.OrderMenu_AlterProd);
            this.orderMenu.DiscountGroupProd += new POS.Controls.OrderMenu.DataDelEventHandler(this.OrderMenu_DiscountGroupProd);
            this.orderMenu.BeicanClick += new POS.Controls.OrderMenu.BeicanClickEventHandler(this.orderMenu_BeicanClick);
            // 
            // showInfo
            // 
            this.showInfo.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.showInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.showInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.showInfo.Location = new System.Drawing.Point(0, 35);
            this.showInfo.Margin = new System.Windows.Forms.Padding(0);
            this.showInfo.Name = "showInfo";
            this.showInfo.SetAllLabelFont = new System.Drawing.Font("宋体", 13F);
            this.showInfo.SetlblSaleKindForeColor = System.Drawing.Color.Yellow;
            this.showInfo.SetMostLabelForeColor = System.Drawing.Color.Cornsilk;
            this.showInfo.Size = new System.Drawing.Size(1284, 35);
            this.showInfo.TabIndex = 1;
            // 
            // roll
            // 
            this.roll.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.roll.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.roll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.roll.FontName = "宋体";
            this.roll.ForeColor = System.Drawing.Color.Yellow;
            this.roll.IsRoll = true;
            this.roll.Local_X1 = 0;
            this.roll.Location = new System.Drawing.Point(0, 0);
            this.roll.Margin = new System.Windows.Forms.Padding(0);
            this.roll.Name = "roll";
            this.roll.RollText = "今天我一定要加油！！！";
            this.roll.SetBackColor = System.Drawing.Color.Transparent;
            this.roll.SetForeColor = System.Drawing.Color.Yellow;
            this.roll.ShowText = "今天我一定要加油！！！";
            this.roll.Size = new System.Drawing.Size(1284, 35);
            this.roll.TabIndex = 2;
            // 
            // operPara1
            // 
            this.operPara1.RollFont = null;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.ClientSize = new System.Drawing.Size(1284, 716);
            this.ControlBox = false;
            this.Controls.Add(this.mainTlp);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.mainTlp.ResumeLayout(false);
            this.tlpMain.ResumeLayout(false);
            this.tlpRight.ResumeLayout(false);
            this.tlpLeft.ResumeLayout(false);
            this.tlpLeftDown.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel mainTlp;
        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.TableLayoutPanel tlpRight;
        private BtnPanelKind btnPanelKind;
        private PageControl pageControl;
        private System.Windows.Forms.TableLayoutPanel tlpLeft;
        private System.Windows.Forms.TableLayoutPanel tlpLeftDown;
        /// <summary>
        /// 功能面板对象
        /// </summary>
        public FunctionPanel functionPanel;
        private Number number;
        private OrderMenu orderMenu;
        private ShowInfo showInfo;
        private Roll roll;
        private BtnPanelProd btnPanelProd;
        // public Transfer_Back_Front transfer_Back_Front1;
        private Service.OperPara operPara1;
        private System.Windows.Forms.Timer timer1;
        //private Service.Info_ini info_ini1;




    }
}