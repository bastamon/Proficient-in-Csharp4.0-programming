using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using POS.View;
using POS.Common;
using POS.Service;
using POS.Controls;
using Common.Print;
using System.Xml;
using POS.Models;

namespace POS.Controls
{
    /// <summary>
    /// 功能面板
    /// </summary>
    public partial class FunctionPanel : UserControl
    {

        /// <summary>
        /// 构造方法
        /// </summary>
        public FunctionPanel()
        {
            InitializeComponent();

        }


        #region//委托
        /// <summary>
        /// ............
        /// </summary>
        /// <param name="buttonPanel">FunctionPanel对象</param>
        /// <param name="emp_id">员工编号</param>
        /// <param name="emp_level">员工级别</param>
        /// <param name="isLoad">是否初次加载</param>
        public delegate void GetInfoEventHandler(FunctionPanel buttonPanel, string emp_id, string emp_level, bool isLoad);
        /// <summary>
        /// .............
        /// </summary>
        /// <param name="functions">ModelFunctions对象</param>
        public delegate void SetInfoEventHandler(ModelFunctions functions);

        /// <summary>
        /// 加载功能按钮信息
        /// </summary>
        [Description("加载功能按钮信息"), Category("自定义")]
        public event GetInfoEventHandler GetInfo;


        /// <summary>
        /// 点击功能按钮时引发
        /// </summary>
        [Description("点击功能按钮时引发"), Category("自定义")]
        public event SetInfoEventHandler SetInfo;

        #endregion

        #region//字段

        /// <summary>
        /// 主界面右侧所处的功能
        /// </summary>
        public string rightContrl = "交易";

        /// <summary>
        /// 按钮背景颜色
        /// </summary>
        public Color[] btnBackColor;
        /// <summary>
        /// 按钮上文字的字体
        /// </summary>
        public Font[] btnTextFont;
        /// <summary>
        /// 按钮上字体的颜色
        /// </summary>
        public Color[] btnFontColor;
        /// <summary>
        /// 按钮位置
        /// </summary>
        public int[] position_id;
        /// <summary>
        /// 热键
        /// </summary>
        public int[] hotkey;
        /// <summary>
        /// 可见性
        /// </summary>
        public bool[] visible;
        /// <summary>
        /// 功能名字，非显示名字
        /// </summary>
        public string[] funct_name;

        /// <summary>
        /// 功能按钮ID
        /// </summary>
        public int[] funct_id;

        /// <summary>
        /// 面板上按钮的索引
        /// </summary>
        public int[] index;

        string emp_id;

        string emp_level;
        /// <summary>
        /// 按钮级别
        /// </summary>
        public string[] empLevel;
        /// <summary>
        /// 一个页面上按钮数量
        /// </summary>
        private int pageBtnNumber = 19;

        /// <summary>
        /// 页码数
        /// </summary>
        public int page = 0;

        /// <summary>
        /// 功能面板上的按钮
        /// </summary>
        FunctionButton[] buttons;

        /// <summary>
        /// 面板中的翻页按钮
        /// </summary>
        private System.Windows.Forms.Button[] pageBtn = new Button[2];


        /// <summary>
        /// 主窗体
        /// </summary>
        private MainForm mainForm;


        /// <summary>
        /// 空能按钮的总数
        /// </summary>
        private int totalBtn = 19;


        /// <summary>
        /// 面板上按钮的行数和列数
        /// </summary>
        private Size collumnRow = new Size(5, 4);

        /// <summary>
        /// 面板上button的统一颜色
        /// </summary>
        private Color color = new Color();

        /// <summary>
        /// 给每个button设置不同的显示的文字
        /// </summary>
        private string[] stringArry;

        /// <summary>
        /// 给每个button设置Font
        /// </summary>
        private Font btnFont;

        /// <summary>
        /// 是否为功能设定，true表示是，false表示不是
        /// </summary>
        private bool isFunctSet = false;

        #endregion

        #region//属性

        /// <summary>
        /// 面板中的翻页按钮
        /// </summary>
        public Button[] PageBtn
        {
            get { return pageBtn; }
            set { pageBtn = value; }
        }

        /// <summary>
        /// 是否为功能设定，true表示是，false表示不是
        /// </summary>
        [Description("是否为功能设定，true表示是，false表示不是"), Category("自定义")]
        public bool IsFunctSet
        {
            get { return isFunctSet; }
            set { isFunctSet = value; }
        }


        /// <summary>
        /// 一个页面上按钮数量
        /// </summary>
        public int PageBtnNumber
        {
            get { return pageBtnNumber; }
            set { pageBtnNumber = value; }
        }

        /// <summary>
        /// 页码数
        /// </summary>
        public int Page
        {
            get { return page; }
            set { page = value; }
        }


        /// <summary>
        /// 功能面板上的按钮
        /// </summary>
        public FunctionButton[] Buttons
        {
            get { return buttons; }
            set { buttons = value; }
        }


        /// <summary>
        /// 主窗体
        /// </summary>
        public MainForm MainForm
        {
            get { return mainForm; }
            set { mainForm = value; }
        }




        /// <summary>
        ///空能按钮的总数
        /// </summary>
        [Description("空能按钮的总数"), Category("自定义")]
        public int TotalBtn
        {
            get { return totalBtn; }
            set
            {

                totalBtn = value;
                if (pageBtn != null)
                {
                    int m = value >= pageBtnNumber ? pageBtnNumber : value;
                    try
                    {
                        for (int i = 0; i < m; i++)
                        {
                            buttons[i].Button.Visible = true;

                        }
                    }
                    catch { }
                    for (int i = value; i < pageBtnNumber; i++)
                    {
                        buttons[i].Button.Visible = false;

                    }

                    pageBtn[0].Visible = false;
                    pageBtn[1].Visible = false;

                    if (pageBtnNumber < totalBtn)
                    {
                        pageBtn[1].Visible = true;
                    }

                }
            }

        }


        /// <summary>
        /// 面板上按钮的行数和列数
        /// </summary>
        [Description("面板上按钮的行数和列数"), Category("自定义")]
        public Size CollumnRow
        {
            get { return collumnRow; }
            set
            {

                collumnRow = value;
                pageBtnNumber = value.Height * value.Width;

                if (buttons != null)
                {
                    for (int i = 0; i < buttons.Length; i++)
                    {
                        buttons[i].Dispose();
                    }
                }
                buttons = new FunctionButton[pageBtnNumber];
                for (int i = 0; i < buttons.Length; i++)
                {

                    buttons[i] = new FunctionButton();
                    //面板上button的tab索引
                    this.buttons[i].TabIndex = i;
                    //给每个button的tag做标记
                    this.buttons[i].Tag = i.ToString();
                    this.buttons[i].FunctionPanel = this;
                    this.buttons[i].Margin = new Padding(0, 0, 0, 0);
                    //将buttons[i]加入控件
                    this.Controls.Add(buttons[i]);
                }

                if (pageBtn != null && pageBtn[0] != null)
                {
                    pageBtn[0].Dispose();
                    pageBtn[1].Dispose();
                }
                pageBtn[0] = new Button();
                pageBtn[1] = new Button();
                pageBtn[0].Text = "<<";
                pageBtn[1].Text = ">>";
                pageBtn[0].Click += new EventHandler(RightClick);
                pageBtn[1].Click += new EventHandler(LeftClick);
                pageBtn[0].BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
                pageBtn[1].BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;

                pageBtn[0].Visible = false;
                pageBtn[1].Visible = false;
                //若一个页面上所能容纳的按钮数小于按钮总数，让翻页按钮可见
                if (pageBtnNumber < totalBtn)
                {
                    pageBtn[1].Visible = true;

                }

                this.pageBtn[0].Margin = new Padding(0, 0, 0, 0);
                this.pageBtn[1].Margin = new Padding(0, 0, 0, 0);

                this.Controls.Add(pageBtn[0]);
                this.Controls.Add(pageBtn[1]);

                SetSize();

            }
        }


        /// <summary>
        /// 面板上button的统一颜色
        /// </summary>
        [Description("面板上button的统一颜色"), Category("自定义")]
        public Color AllBtnColor
        {
            get { return color; }
            set
            {

                color = value;
                if (buttons != null)
                {
                    for (int i = 0; i < buttons.Length; i++)
                    {
                        this.buttons[i].Button.BackColor = value;
                    }
                    pageBtn[0].BackColor = value;
                    pageBtn[1].BackColor = value;
                }

            }
        }


        /// <summary>
        /// 给每个button设置不同的显示的文字
        /// </summary>
        [Description("给每个button设置显示的文字，第一行是第一个button，第二行是第二个button,依次类推"), Category("自定义")]
        public string[] StringArray
        {
            get { return stringArry; }
            set
            {

                stringArry = value;
                if (!isFunctSet)
                {
                    if (buttons != null && stringArry != null)
                    {

                        for (int i = 0; i < stringArry.Length && i < buttons.Length; i++)
                        {
                            buttons[i].Button.Text = stringArry[i];

                        }
                    }
                }
            }
        }




        /// <summary>
        /// 给每个button设置Font
        /// </summary>
        [Description("给每个button设置Font"), Category("自定义")]
        public Font BtnFont
        {
            get { return btnFont; }
            set
            {
                btnFont = value;
                if (buttons != null)
                {

                    for (int i = 0; i < buttons.Length; i++)
                    {
                        buttons[i].Button.Font = value;

                    }
                }
            }
        }
        #endregion

        /// <summary>
        /// 得到指定的一个按钮的信息
        /// </summary>
        /// <param name="i">按钮的索引</param>
        /// <returns>封装好的按钮上的信息的对象</returns>
        public ModelFunctions GetOneFunctInfo(int i)
        {
            //点击后把本按钮的信息封装到一个类中传到调用处
            ModelFunctions functions = new ModelFunctions();
            functions.Color = this.buttons[i].Button.BackColor;
            functions.Disp_name = this.buttons[i].Dis_name;
            functions.Font_color = this.buttons[i].Button.ForeColor;
            functions.Font_name = this.buttons[i].Button.Font.Name;
            functions.Font_size = this.buttons[i].Button.Font.Size;
            functions.Funct_id = this.buttons[i].Funct_id;
            functions.Funct_name = this.buttons[i].Funct_name;
            functions.Hotkey = this.buttons[i].Hotkey;
            functions.Visible = this.buttons[i].Visible1;
            functions.Emp_level = this.buttons[i].Emp_level;
            functions.Index = this.buttons[i].Index;
            return functions;
        }

        /// <summary>
        /// 功能设定时点击调用
        /// </summary>
        /// <param name="functions"></param>
        public void BtnSetInfo(ModelFunctions functions)
        {
            //功能设定时引发
            SetInfo(functions);
        }

        /// <summary>
        /// button点击事件调用的方法
        /// </summary>
        /// <param name="funtType">功能的类别</param>
        public void BtnClick(string funtType)
        {
            //根据funtType的值执行相应的功能————这里省略了N多代码……………………………………………………
            switch (funtType)
            {
                //注释：CanChangeStatus()函数用于判断各个功能之间的转换逻辑
                #region//数量
                case "数量":

                    if (CanChangeStatus("数量"))
                    {

                        this.mainForm.Number.FunctionName = "数量";
                        if (this.mainForm.Number.TextBoxText != "")
                        {
                            try
                            {

                                decimal num = Convert.ToDecimal(Info.inputNumber);
                                if (num != 0)
                                {
                                    this.mainForm.ReturenOrderMenu.AltGroupIdNumber(num);
                                    Info.selectedGroupProd_qty = num;
                                    this.mainForm.Number.ClearNum();
                                    if (this.mainForm.Beican != null && this.mainForm.Beican.IsNowBeiCan)
                                    {
                                        this.mainForm.Beican.LoadInfo(Info.sale_id);
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("商品数量不能为零！");
                                }


                            }
                            catch
                            {
                                MessageBox.Show("您输入的不正确！");
                            }
                        }
                    }

                    break;
                #endregion

                #region//删除
                case "删除":

                    if (CanChangeStatus("删除"))
                    {
                        this.mainForm.Number.FunctionName = "删除";
                        UpdateSales delSale = new UpdateSales();
                        if (delSale.DelGroupProd(Info.selectedGroupProd))
                        {
                            delSale.ReturnIsDiscStatus();
                            this.mainForm.ReturenOrderMenu.LoadInfo();
                            if (this.mainForm.Beican != null && this.mainForm.Beican.IsNowBeiCan)
                            {
                                this.mainForm.Beican.LoadInfo(Info.sale_id);
                            }
                        }
                    }

                    break;
                #endregion

                #region//交易取消
                case "交易取消":
                    if (CanChangeStatus("交易取消"))
                    {
                        this.mainForm.ReturenOrderMenu.ClearListOrderDinner();
                        UpdateSales.InitUpdateSales().DeleteSales(Info.shop_id, Info.sale_id);
                        //恢复状态位
                        Info.isAllowance = false;
                        Info.isDistinct = false;
                        Info.canAlterDinner = true;
                        Info.inputNumber = 0;
                        Info.sale_sno = 1;
                        this.mainForm.OperPara.WriteIniInfo("inputNumber", "0");
                        this.mainForm.Number.TextBoxText = "";
                        if (this.mainForm.Beican != null&&this.mainForm.Beican.IsNowBeiCan)
                        {
                            this.mainForm.Beican.LoadInfo(Info.sale_id);
                        }
                    }
                    break;
                #endregion

                #region//查单
                case "查单":
                    if (CanChangeStatus("查单"))
                    {
                        this.Visible = false;
                        new CheckSale(this.mainForm).ShowDialog();
                        this.Visible = true;
                    }
                    break;
                #endregion

                #region//结账
                case "结账":


                    if (CanChangeStatus("结账"))
                    {

                        if (0 != Info.totalPrice)
                        {
                            if (this.mainForm.CheckOut == null)
                            {
                                this.mainForm.CheckOut = new CheckOut();
                                this.mainForm.CheckOut.MainForm = this.mainForm;
                            }
                            Info.isCheckOut = true;
                            
                            this.mainForm.CheckOut.SetPrice();
                            this.mainForm.CheckOut.Dock = DockStyle.Fill;
                            this.mainForm.ReturnTlpMain.Controls.Remove(this.mainForm.TlpRight);
                            this.mainForm.ReturnTlpMain.Controls.Add(this.mainForm.CheckOut, 1, 0);
                        }
                        else
                        {
                            MessageBox.Show("当前没有商品可用于结账！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                    }

                    break;
                #endregion

                #region//店总额查询
                case "店总额查询":
                    if (CanChangeStatus("店总额查询"))
                    {
                        rightContrl = "店总额查询";
                        if (this.mainForm.shop_Tur == null)
                        {
                            this.mainForm.shop_Tur = new Shop_Tur();
                        }
                        else
                        {
                            this.mainForm.shop_Tur.Dispose();
                            this.mainForm.shop_Tur = new Shop_Tur();
                        }
                        this.mainForm.shop_Tur.MainForm = this.mainForm;
                        this.mainForm.ReturnTlpMain.Controls.Remove(this.mainForm.TlpRight);
                        this.mainForm.ReturnTlpMain.Controls.Add(this.mainForm.shop_Tur, 1, 0);
                        this.mainForm.shop_Tur.Dock = DockStyle.Fill;

                    }
                    break;
                #endregion

                #region//下线
                case "下线":
                    if (CanChangeStatus("下线"))
                    {

                        Offline offline = new Offline();
                        offline.MainForm = this.mainForm;
                        offline.Visible = false;
                        offline.ShowDialog();
                        if (offline.isExitType)
                        {
                            this.mainForm.OperPara.WriteIniInfo("isNormalOff", "true");
                            this.mainForm.Visible = false;
                            this.mainForm.Login.isOffLine = true;
                            mainForm.Login.TxtEmp_Id.Focus();
                            mainForm.Login.isUp = true;
                            this.mainForm.Login.Visible = true;
                            //初始化面板页码
                            mainForm.functionPanel.page = 0;
                        }
                    }

                    break;

                #endregion

                #region//交班查询
                case "交班查询":
                    if (CanChangeStatus("交班查询"))
                    {
                        rightContrl = "交班查询";
                        if (this.mainForm.Pos_rounds_inquire == null)
                        {
                            this.mainForm.Pos_rounds_inquire = new Pos_rounds_inquire();
                            this.mainForm.Pos_rounds_inquire.MainForm = this.mainForm;
                        }
                        this.mainForm.ReturnTlpMain.Controls.Remove(this.mainForm.TlpRight);
                        this.mainForm.ReturnTlpMain.Controls.Add(this.mainForm.Pos_rounds_inquire, 1, 0);
                        this.mainForm.Pos_rounds_inquire.Dock = DockStyle.Fill;

                    }
                    break;

                #endregion

                #region//交易查询
                //case "备注": break;
                //case "桌号": break;
                //case "印发票": break;
                case "交易查询":
                    if (CanChangeStatus("交易查询"))
                    {
                        //this.mainForm.CheckSale01.MainForm = this.mainForm;
                        new CheckSale(this.mainForm).ShowDialog();
                        
                    }
                    break;
                #endregion

                #region//单机查询
                case "单机查询":
                    if (CanChangeStatus("单机查询"))
                    {
                        rightContrl = "单机查询";
                        if (this.mainForm.pos_Tur == null)
                        {
                            this.mainForm.pos_Tur = new Pos_Tur();
                            this.mainForm.pos_Tur.MainForm = this.mainForm;
                        }
                        else
                        {
                            this.mainForm.pos_Tur.Dispose();
                            this.mainForm.pos_Tur = new Pos_Tur();
                            this.mainForm.pos_Tur.MainForm = this.mainForm;
                        }

                        this.mainForm.ReturnTlpMain.Controls.Remove(this.mainForm.TlpRight);
                        this.mainForm.ReturnTlpMain.Controls.Add(this.mainForm.pos_Tur, 1, 0);
                        this.mainForm.pos_Tur.Dock = DockStyle.Fill;

                    }
                    break;
                #endregion

                #region//读收银员帐
                case "读收银员帐":
                    if (CanChangeStatus("读收银员帐"))
                    {
                        rightContrl = "读收银员帐";
                        if (this.mainForm.cashier_Tur == null)
                        {
                            this.mainForm.cashier_Tur = new Cashier_Tur();
                            this.mainForm.cashier_Tur.MainForm = this.mainForm;
                        }
                        else
                        {
                            this.mainForm.cashier_Tur.Dispose();
                            this.mainForm.cashier_Tur = new Cashier_Tur();
                            this.mainForm.cashier_Tur.MainForm = this.mainForm;
                        }
                        this.mainForm.ReturnTlpMain.Controls.Remove(this.mainForm.TlpRight);
                        this.mainForm.ReturnTlpMain.Controls.Add(this.mainForm.cashier_Tur, 1, 0);
                        this.mainForm.cashier_Tur.Dock = DockStyle.Fill;

                    }
                    break;
                #endregion

                #region//修改密码
                case "修改密码":

                    if (CanChangeStatus("修改密码"))
                    {
                        new ChangePassword().ShowDialog();
                    }

                    break;
                #endregion
            }

        }

        /// <summary>
        /// 给功能面板加载信息
        /// </summary>
        /// <param name="emp_id">员工编号</param>
        /// <param name="emp_level">员工级别</param>
        /// <param name="isLoad">是否初次加载</param>
        public void LoadBtnInfo(string emp_id, string emp_level, bool isLoad)
        {
            try
            {
                this.emp_id = emp_id;
                this.emp_level = emp_level;
                if (isFunctSet)
                {
                    this.pageBtn[1].Visible = false;
                }
                GetInfo(this, emp_id, emp_level, isLoad);
            }
            catch { }
        }

        /// <summary>
        /// 给页面上的商品按钮设置文字
        /// </summary>
        /// <param name="page">页码数</param>
        /// <param name="isLoad">是否初次加载</param>
        public void GetBtnInfo(int page, bool isLoad)
        {
            //触发事件的方法
            GetInfo(this, emp_id, emp_level, isLoad);
        }
        #region 私有方法
        /// <summary>
        /// 设置面板上button的位置和大小
        /// </summary>
        private void SetSize()
        {
            int rows = collumnRow.Height;
            int collumns = collumnRow.Width;
            int n = 0;

            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < collumns; c++)
                {
                    if (r == rows - 1 && c == collumns - 1)
                    {
                        pageBtn[0].Location = new Point(c * (this.Width / collumns), r * (this.Height / rows));
                        pageBtn[1].Location = new Point(c * (this.Width / collumns) + this.Width / (collumns * 2), r * (this.Height / rows));
                    }
                    else
                    {


                        if (n < pageBtnNumber)
                            this.buttons[n++].Location = new System.Drawing.Point(c * (this.Width / collumns), r * (this.Height / rows));
                    }
                }
            }

            for (int i = 0; i < buttons.Length; i++)
            {
                this.buttons[i].Size = new Size(this.Width / collumns, this.Height / rows);
            }

            pageBtn[0].Size = new Size(this.Width / (collumns * 2), this.Height / rows);
            pageBtn[1].Size = new Size(this.Width / (collumns * 2), this.Height / rows);
        }
        /// <summary>
        /// 功能之间的逻辑控制
        /// </summary>
        /// <param name="status">用户点击的功能</param>
        /// <returns>可以进行status功能返回true，否则返回false</returns>
        private bool CanChangeStatus(string status)
        {
            switch (status)
            {

                #region//数量0
                case "数量":

                    if (Info.isCheckChit || Info.isCheckOut || Info.IsTiDan || mainForm.ReturenOrderMenu.DgvOrderMenu.Rows.Count == 0)
                    {
                        if (mainForm.ReturenOrderMenu.DgvOrderMenu.Rows.Count == 0)
                        {
                            MessageBox.Show("无交易" + "状态下暂时不能进行此项操作！");
                            return false;
                        }
                        if (Info.isCheckOut)
                        {
                            MessageBox.Show("结账" + "状态下暂时不能进行此项操作！");
                            return false;
                        }
                        if (Info.isCheckChit)
                        {
                            MessageBox.Show("兑换券" + "状态下暂时不能进行此项操作！");
                            return false;
                        }
                        if (Info.IsTiDan)
                        {
                            MessageBox.Show("提单" + "状态下暂时不能进行此项操作！");
                            return false;
                        }
                        else
                        {
                            MessageBox.Show(status + "状态下暂时不能进行此项操作！");
                            return false;
                        }
                    }
                    else
                    {
                        return true;
                    }
                #endregion

                #region//删除1
                case "删除":

                    if (Info.isCheckChit || Info.isCheckOut || Info.IsTiDan || mainForm.ReturenOrderMenu.DgvOrderMenu.Rows.Count == 0)
                    {
                        if (mainForm.ReturenOrderMenu.DgvOrderMenu.Rows.Count == 0)
                        {
                            MessageBox.Show("无交易" + "状态下暂时不能进行此项操作！");
                            return false;
                        }
                        if (Info.isCheckOut)
                        {
                            MessageBox.Show("结账" + "状态下暂时不能进行此项操作！");
                            return false;
                        }
                        if (Info.isCheckChit)
                        {
                            MessageBox.Show("兑换券" + "状态下暂时不能进行此项操作！");
                            return false;
                        }
                        if (Info.IsTiDan)
                        {
                            MessageBox.Show("提单" + "状态下暂时不能进行此项操作！");
                            return false;
                        }
                        else
                        {
                            MessageBox.Show(status + "状态下暂时不能进行此项操作！");
                            return false;
                        }
                    }
                    else
                    {
                        return true;
                    }

                #endregion

                #region//交易取消3
                case "交易取消":
                    if (Info.isCheckChit || Info.isCheckOut || Info.IsTiDan || mainForm.ReturenOrderMenu.DgvOrderMenu.Rows.Count == 0)
                    {
                        if (mainForm.ReturenOrderMenu.DgvOrderMenu.Rows.Count == 0)
                        {
                            MessageBox.Show("无交易" + "状态下暂时不能进行此项操作！");
                            return false;
                        }
                        if (Info.isCheckOut)
                        {
                            MessageBox.Show("结账" + "状态下暂时不能进行此项操作！");
                            return false;
                        }
                        if (Info.isCheckChit)
                        {
                            MessageBox.Show("兑换券" + "状态下暂时不能进行此项操作！");
                            return false;
                        }
                        if (Info.IsTiDan)
                        {
                            MessageBox.Show("提单" + "状态下暂时不能进行此项操作！");
                            return false;
                        }
                        else
                        {
                            MessageBox.Show(status + "状态下暂时不能进行此项操作！");
                            return false;
                        }
                    }
                    else
                    {
                        return true;
                    }

                #endregion

                #region//查单5
                case "查单":

                    if (Info.isCheckChit || Info.isCheckOut || Info.IsTiDan || mainForm.ReturenOrderMenu.DgvOrderMenu.Rows.Count != 0)
                    {
                        if (mainForm.ReturenOrderMenu.DgvOrderMenu.Rows.Count != 0)
                        {
                            MessageBox.Show("交易" + "状态下暂时不能进行此项操作！");
                            return false;
                        }
                        if (Info.isCheckOut)
                        {
                            MessageBox.Show("结账" + "状态下暂时不能进行此项操作！");
                            return false;
                        }
                        if (Info.isCheckChit)
                        {
                            MessageBox.Show("兑换券" + "状态下暂时不能进行此项操作！");
                            return false;
                        }
                        if (Info.IsTiDan)
                        {
                            MessageBox.Show("提单" + "状态下暂时不能进行此项操作！");
                            return false;
                        }
                        else
                        {
                            MessageBox.Show(status + "状态下暂时不能进行此项操作！");
                            return false;
                        }
                    }
                    else
                    {
                        return true;
                    }

                #endregion

                #region//结账6
                case "结账":

                    if (Info.isCheckChit || Info.isCheckOut || Info.IsTiDan || mainForm.ReturenOrderMenu.DgvOrderMenu.Rows.Count == 0)
                    {
                        if (mainForm.ReturenOrderMenu.DgvOrderMenu.Rows.Count == 0)
                        {
                            MessageBox.Show("无交易" + "状态下暂时不能进行此项操作！");
                            return false;
                        }
                        if (Info.isCheckOut)
                        {
                            MessageBox.Show("结账" + "状态下暂时不能进行此项操作！");
                            return false;
                        }
                        if (Info.isCheckChit)
                        {
                            MessageBox.Show("兑换券" + "状态下暂时不能进行此项操作！");
                            return false;
                        }
                        if (Info.IsTiDan)
                        {
                            MessageBox.Show("提单" + "状态下暂时不能进行此项操作！");
                            return false;
                        }
                        else
                        {
                            MessageBox.Show(status + "状态下暂时不能进行此项操作！");
                            return false;
                        }
                    }
                    else
                    {
                        return true;
                    }
                #endregion

                #region//店总额查询7

                case "店总额查询":

                    if (Info.isCheckChit || Info.isCheckOut || Info.IsTiDan || mainForm.ReturenOrderMenu.DgvOrderMenu.Rows.Count != 0 || rightContrl != "交易")
                    {
                        if (rightContrl != "店总额查询")
                        {
                            MessageBox.Show(rightContrl + "状态下暂时不能进行此项操作！");
                            return false;
                        }
                        if (mainForm.ReturenOrderMenu.DgvOrderMenu.Rows.Count != 0)
                        {
                            MessageBox.Show("交易" + "状态下暂时不能进行此项操作！");
                            return false;
                        }
                        if (Info.isCheckOut)
                        {
                            MessageBox.Show("结账" + "状态下暂时不能进行此项操作！");
                            return false;
                        }
                        if (Info.isCheckChit)
                        {
                            MessageBox.Show("兑换券" + "状态下暂时不能进行此项操作！");
                            return false;
                        }
                        if (Info.IsTiDan)
                        {
                            MessageBox.Show("提单" + "状态下暂时不能进行此项操作！");
                            return false;
                        }
                        else
                        {
                            MessageBox.Show(status + "状态下暂时不能进行此项操作！");
                            return false;
                        }
                    }
                    else
                    {
                        return true;
                    }
                #endregion

                #region//下线9
                case "下线":

                    if (Info.isCheckChit || Info.isCheckOut || Info.IsTiDan || mainForm.ReturenOrderMenu.DgvOrderMenu.Rows.Count != 0)
                    {
                        if (mainForm.ReturenOrderMenu.DgvOrderMenu.Rows.Count != 0)
                        {
                            MessageBox.Show("交易" + "状态下暂时不能进行此项操作！");
                            return false;
                        }
                        if (Info.isCheckOut)
                        {
                            MessageBox.Show("结账" + "状态下暂时不能进行此项操作！");
                            return false;
                        }
                        if (Info.isCheckChit)
                        {
                            MessageBox.Show("兑换券" + "状态下暂时不能进行此项操作！");
                            return false;
                        }
                        if (Info.IsTiDan)
                        {
                            MessageBox.Show("提单" + "状态下暂时不能进行此项操作！");
                            return false;
                        }
                        else
                        {
                            MessageBox.Show(status + "状态下暂时不能进行此项操作！");
                            return false;
                        }
                    }
                    else if(!new Saletmp00().AllowOnline())
                    {
                        MessageBox.Show("挂单状态下不允许下线，请先提单！");
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                #endregion

                #region//交班查询10
                case "交班查询":

                    if (Info.isCheckChit || Info.isCheckOut || Info.IsTiDan || mainForm.ReturenOrderMenu.DgvOrderMenu.Rows.Count != 0 || rightContrl != "交易")
                    {
                        if (rightContrl != "交班查询")
                        {
                            MessageBox.Show(rightContrl + "状态下暂时不能进行此项操作！");
                            return false;
                        }
                        if (mainForm.ReturenOrderMenu.DgvOrderMenu.Rows.Count != 0)
                        {
                            MessageBox.Show("交易" + "状态下暂时不能进行此项操作！");
                            return false;
                        }
                        if (Info.isCheckOut)
                        {
                            MessageBox.Show("结账" + "状态下暂时不能进行此项操作！");
                            return false;
                        }
                        if (Info.isCheckChit)
                        {
                            MessageBox.Show("兑换券" + "状态下暂时不能进行此项操作！");
                            return false;
                        }
                        if (Info.IsTiDan)
                        {
                            MessageBox.Show("提单" + "状态下暂时不能进行此项操作！");
                            return false;
                        }
                        else
                        {
                            MessageBox.Show(status + "状态下暂时不能进行此项操作！");
                            return false;
                        }
                    }
                    else
                    {
                        return true;
                    }
                #endregion

                #region//交易查询12
                case "交易查询":

                    if (Info.isCheckChit || Info.isCheckOut || Info.IsTiDan || mainForm.ReturenOrderMenu.DgvOrderMenu.Rows.Count != 0)
                    {
                        if (mainForm.ReturenOrderMenu.DgvOrderMenu.Rows.Count != 0)
                        {
                            MessageBox.Show("交易" + "状态下暂时不能进行此项操作！");
                            return false;
                        }
                        if (Info.isCheckOut)
                        {
                            MessageBox.Show("结账" + "状态下暂时不能进行此项操作！");
                            return false;
                        }
                        if (Info.isCheckChit)
                        {
                            MessageBox.Show("兑换券" + "状态下暂时不能进行此项操作！");
                            return false;
                        }
                        if (Info.IsTiDan)
                        {
                            MessageBox.Show("提单" + "状态下暂时不能进行此项操作！");
                            return false;
                        }
                        else
                        {
                            MessageBox.Show(status + "状态下暂时不能进行此项操作！");
                            return false;
                        }
                    }
                    else
                    {
                        return true;
                    }
                #endregion

                #region//单机查询15
                case "单机查询":

                    if (Info.isCheckChit || Info.isCheckOut || Info.IsTiDan || mainForm.ReturenOrderMenu.DgvOrderMenu.Rows.Count != 0 || rightContrl != "交易")
                    {
                        if (rightContrl != "单机查询")
                        {
                            MessageBox.Show(rightContrl + "状态下暂时不能进行此项操作！");
                            return false;
                        }
                        if (mainForm.ReturenOrderMenu.DgvOrderMenu.Rows.Count != 0)
                        {
                            MessageBox.Show("交易" + "状态下暂时不能进行此项操作！");
                            return false;
                        }
                        if (Info.isCheckOut)
                        {
                            MessageBox.Show("结账" + "状态下暂时不能进行此项操作！");
                            return false;
                        }
                        if (Info.isCheckChit)
                        {
                            MessageBox.Show("兑换券" + "状态下暂时不能进行此项操作！");
                            return false;
                        }
                        if (Info.IsTiDan)
                        {
                            MessageBox.Show("提单" + "状态下暂时不能进行此项操作！");
                            return false;
                        }
                        else
                        {
                            MessageBox.Show(status + "状态下暂时不能进行此项操作！");
                            return false;
                        }
                    }
                    else
                    {
                        return true;
                    }
                #endregion

                #region//读收银员账19
                case "读收银员帐":
                    if (Info.isCheckChit || Info.isCheckOut || Info.IsTiDan || mainForm.ReturenOrderMenu.DgvOrderMenu.Rows.Count != 0 || rightContrl != "交易")
                    {
                        if (rightContrl != "读收银员帐")
                        {
                            MessageBox.Show(rightContrl + "状态下暂时不能进行此项操作！");
                            return false;
                        }
                        if (mainForm.ReturenOrderMenu.DgvOrderMenu.Rows.Count != 0)
                        {
                            MessageBox.Show("交易" + "状态下暂时不能进行此项操作！");
                            return false;
                        }
                        if (Info.isCheckOut)
                        {
                            MessageBox.Show("结账" + "状态下暂时不能进行此项操作！");
                            return false;
                        }
                        if (Info.isCheckChit)
                        {
                            MessageBox.Show("兑换券" + "状态下暂时不能进行此项操作！");
                            return false;
                        }
                        if (Info.IsTiDan)
                        {
                            MessageBox.Show("提单" + "状态下暂时不能进行此项操作！");
                            return false;
                        }
                        else
                        {
                            MessageBox.Show(status + "状态下暂时不能进行此项操作！");
                            return false;
                        }
                    }
                    else
                    {
                        return true;
                    }
                #endregion

                #region//修改密码
                case "修改密码":

                    if (Info.isCheckChit || Info.isCheckOut || Info.IsTiDan || mainForm.ReturenOrderMenu.DgvOrderMenu.Rows.Count != 0)
                    {
                        if (mainForm.ReturenOrderMenu.DgvOrderMenu.Rows.Count != 0)
                        {
                            MessageBox.Show("交易" + "状态下暂时不能进行此项操作！");
                            return false;
                        }
                        if (Info.isCheckOut)
                        {
                            MessageBox.Show("结账" + "状态下暂时不能进行此项操作！");
                            return false;
                        }
                        if (Info.isCheckChit)
                        {
                            MessageBox.Show("兑换券" + "状态下暂时不能进行此项操作！");
                            return false;
                        }
                        if (Info.IsTiDan)
                        {
                            MessageBox.Show("提单" + "状态下暂时不能进行此项操作！");
                            return false;
                        }
                        else
                        {
                            MessageBox.Show(status + "状态下暂时不能进行此项操作！");
                            return false;
                        }
                    }
                    else
                    {
                        return true;
                    }
                #endregion


            }
            return false;

        }
        #endregion

        #region 事件
        /// <summary>
        /// 面板加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FunctionPanel_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 面板大小发生变化的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FunctionPanel_SizeChanged(object sender, EventArgs e)
        {
            if (buttons != null)
                SetSize();
        }

        /// <summary>
        /// 左翻页按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LeftClick(Object sender, EventArgs e)
        {
            BtnRight();
        }



        /// <summary>
        /// 右翻页按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RightClick(Object sender, EventArgs e)
        {

            BtnLeft();
        }

        /// <summary>
        /// 参数设定时调用，以改变功能面板上的文字信息
        /// </summary>
        public void ReloadFunc()
        {

            int pageBtnNumber = collumnRow.Width * collumnRow.Height - 1;

            page = 0;
            //清空按钮上的所有文字
            for (int i = 0; i < pageBtnNumber; i++)
            {
                this.buttons[i].Button.Text = "";

            }
            //给按钮上设置信息
            for (int i = 0; i < pageBtnNumber && i < funct_name.Length; i++)
            {
                this.buttons[i].Button.Text = stringArry[i + page * pageBtnNumber];
                this.buttons[i].Button.BackColor = btnBackColor[i + page * pageBtnNumber];
                this.buttons[i].Button.ForeColor = btnFontColor[i + page * pageBtnNumber];
                this.buttons[i].Button.Font = btnTextFont[i + page * pageBtnNumber];
                this.buttons[i].Position_id = position_id[i + page * pageBtnNumber];
                this.buttons[i].Hotkey = hotkey[i + page * pageBtnNumber];
                this.buttons[i].Visible1 = visible[i + page * pageBtnNumber];
                this.buttons[i].Funct_name = funct_name[i + page * pageBtnNumber];
                this.buttons[i].Dis_name = stringArry[i + page * pageBtnNumber];
                if (isFunctSet)
                {
                    this.buttons[i].Emp_level = empLevel[i + page * pageBtnNumber];
                    this.buttons[i].Button.Text = funct_name[i + page * pageBtnNumber];
                    this.buttons[i].Funct_id = funct_id[i + page * pageBtnNumber];
                    this.buttons[i].Index = index[i + page * pageBtnNumber];
                }
                else
                {
                    this.buttons[i].Button.Text = stringArry[i + page * pageBtnNumber];
                }
            }

            //设置翻页按钮可见性
            this.pageBtn[1].Visible = false;
            if (pageBtnNumber < funct_name.Length)
            {
                this.pageBtn[1].Visible = true;

            }
            else
            {
                this.pageBtn[1].Visible = false;
            }
        }

        /// <summary>
        /// 向左翻页方法
        /// </summary>
        public void BtnLeft()
        {
            //pageBtn[0]表示左面的按钮，但是它负责向右翻页
            this.pageBtn[1].Visible = true;
            int pageBtnNumber = collumnRow.Width * collumnRow.Height - 1;
            //页码数加一
            if (page > 0)
            {
                page--;
            }

            if (page < (totalBtn / pageBtnNumber))
            {
                this.SetVisibleNumber(pageBtnNumber);
                for (int i = 0; i < pageBtnNumber; i++)
                {
                    this.buttons[i].Button.Text = "";

                }

                if (Page < (TotalBtn / pageBtnNumber))
                {
                    for (int i = 0; i < pageBtnNumber; i++)
                    {

                        this.buttons[i].Button.BackColor = btnBackColor[i + page * pageBtnNumber];
                        this.buttons[i].Button.ForeColor = btnFontColor[i + page * pageBtnNumber];
                        this.buttons[i].Button.Font = btnTextFont[i + page * pageBtnNumber];
                        this.buttons[i].Position_id = position_id[i + page * pageBtnNumber];
                        this.buttons[i].Hotkey = hotkey[i + page * pageBtnNumber];
                        this.buttons[i].Visible1 = visible[i + page * pageBtnNumber];
                        this.buttons[i].Funct_name = funct_name[i + page * pageBtnNumber];
                        this.buttons[i].Dis_name = stringArry[i + page * pageBtnNumber];
                        if (isFunctSet)
                        {
                            this.buttons[i].Emp_level = empLevel[i + page * pageBtnNumber];
                            this.buttons[i].Button.Text = funct_name[i + page * pageBtnNumber];
                            this.buttons[i].Funct_id = funct_id[i + page * pageBtnNumber];
                            this.buttons[i].Index = index[i + page * pageBtnNumber];
                        }
                        else
                        {
                            this.buttons[i].Button.Text = stringArry[i + page * pageBtnNumber];
                        }
                    }
                }
                if (Page == (TotalBtn / pageBtnNumber))
                {
                    for (int i = 0; i < TotalBtn - pageBtnNumber * Page; i++)
                    {

                        this.buttons[i].Button.Text = stringArry[i + page * pageBtnNumber];
                        this.buttons[i].Button.BackColor = btnBackColor[i + page * pageBtnNumber];
                        this.buttons[i].Button.ForeColor = btnFontColor[i + page * pageBtnNumber];
                        this.buttons[i].Button.Font = btnTextFont[i + page * pageBtnNumber];
                        this.buttons[i].Position_id = position_id[i + page * pageBtnNumber];
                        this.buttons[i].Hotkey = hotkey[i + page * pageBtnNumber];
                        this.buttons[i].Visible1 = visible[i + page * pageBtnNumber];
                        this.buttons[i].Funct_name = funct_name[i + page * pageBtnNumber];
                        this.buttons[i].Dis_name = stringArry[i + page * pageBtnNumber];
                        if (isFunctSet)
                        {
                            this.buttons[i].Emp_level = empLevel[i + page * pageBtnNumber];
                            this.buttons[i].Button.Text = funct_name[i + page * pageBtnNumber];
                            this.buttons[i].Funct_id = funct_id[i + page * pageBtnNumber];
                            this.buttons[i].Index = index[i + page * pageBtnNumber];
                        }
                        else
                        {
                            this.buttons[i].Button.Text = stringArry[i + page * pageBtnNumber];
                        }
                    }
                }

                //翻到第一页让右按钮消失
                if (page == 0)
                {
                    this.pageBtn[0].Visible = false;

                }
                GetBtnInfo(page, false);
            }
        }

        /// <summary>
        /// 向右翻页方法
        /// </summary>
        public void BtnRight()
        {
            this.pageBtn[0].Visible = true;
            int pageBtnNumber = collumnRow.Width * collumnRow.Height - 1;
            //页码数加一
            page++;
            //非最后一页
            if (page < (totalBtn / pageBtnNumber))
            {
                //清空按钮上的所有文字
                for (int i = 0; i < pageBtnNumber; i++)
                {
                    this.buttons[i].Button.Text = "";

                }

                if (Page < (TotalBtn / pageBtnNumber))
                {
                    for (int i = 0; i < pageBtnNumber; i++)
                    {
                        this.buttons[i].Button.Text = stringArry[i + page * pageBtnNumber];
                        this.buttons[i].Button.BackColor = btnBackColor[i + page * pageBtnNumber];
                        this.buttons[i].Button.ForeColor = btnFontColor[i + page * pageBtnNumber];
                        this.buttons[i].Button.Font = btnTextFont[i + page * pageBtnNumber];
                        this.buttons[i].Position_id = position_id[i + page * pageBtnNumber];
                        this.buttons[i].Hotkey = hotkey[i + page * pageBtnNumber];
                        this.buttons[i].Visible1 = visible[i + page * pageBtnNumber];
                        this.buttons[i].Funct_name = funct_name[i + page * pageBtnNumber];
                        this.buttons[i].Dis_name = stringArry[i + page * pageBtnNumber];
                        if (isFunctSet)
                        {
                            this.buttons[i].Emp_level = empLevel[i + page * pageBtnNumber];
                            this.buttons[i].Button.Text = funct_name[i + page * pageBtnNumber];
                            this.buttons[i].Funct_id = funct_id[i + page * pageBtnNumber];
                            this.buttons[i].Index = index[i + page * pageBtnNumber];
                        }
                        else
                        {
                            this.buttons[i].Button.Text = stringArry[i + page * pageBtnNumber];
                        }
                    }
                }
                if (Page == (TotalBtn / pageBtnNumber))
                {
                    for (int i = 0; i < TotalBtn - pageBtnNumber * Page; i++)
                    {
                        this.buttons[i].Button.Text = stringArry[i + page * pageBtnNumber];
                        this.buttons[i].Button.BackColor = btnBackColor[i + page * pageBtnNumber];
                        this.buttons[i].Button.ForeColor = btnFontColor[i + page * pageBtnNumber];
                        this.buttons[i].Button.Font = btnTextFont[i + page * pageBtnNumber];
                        this.buttons[i].Position_id = position_id[i + page * pageBtnNumber];
                        this.buttons[i].Hotkey = hotkey[i + page * pageBtnNumber];
                        this.buttons[i].Visible1 = visible[i + page * pageBtnNumber];
                        this.buttons[i].Funct_name = funct_name[i + page * pageBtnNumber];
                        this.buttons[i].Dis_name = stringArry[i + page * pageBtnNumber];
                        if (isFunctSet)
                        {
                            this.buttons[i].Emp_level = empLevel[i + page * pageBtnNumber];
                            this.buttons[i].Button.Text = funct_name[i + page * pageBtnNumber];
                            this.buttons[i].Funct_id = funct_id[i + page * pageBtnNumber];
                            this.buttons[i].Index = index[i + page * pageBtnNumber];
                        }
                        else
                        {
                            this.buttons[i].Button.Text = stringArry[i + page * pageBtnNumber];
                        }
                    }
                }

                //翻到最后一页页让左按钮消失
                if ((page + 1) * pageBtnNumber == totalBtn)
                {
                    this.pageBtn[1].Visible = false;

                }
            }
            //最后一页
            if (page == (totalBtn / pageBtnNumber))
            {
                //清空按钮上的所有文字
                for (int i = 0; i < pageBtnNumber; i++)
                {
                    this.buttons[i].Button.Text = "";

                }
                //设置一页上显示的按钮数
                this.SetVisibleNumber(totalBtn - pageBtnNumber * page);

                if (Page < (TotalBtn / pageBtnNumber))
                {
                    for (int i = 0; i < pageBtnNumber; i++)
                    {
                        this.buttons[i].Button.Text = stringArry[i + page * pageBtnNumber];
                        this.buttons[i].Button.BackColor = btnBackColor[i + page * pageBtnNumber];
                        this.buttons[i].Button.ForeColor = btnFontColor[i + page * pageBtnNumber];
                        this.buttons[i].Button.Font = btnTextFont[i + page * pageBtnNumber];
                        this.buttons[i].Position_id = position_id[i + page * pageBtnNumber];
                        this.buttons[i].Hotkey = hotkey[i + page * pageBtnNumber];
                        this.buttons[i].Visible1 = visible[i + page * pageBtnNumber];
                        this.buttons[i].Funct_name = funct_name[i + page * pageBtnNumber];
                        this.buttons[i].Dis_name = stringArry[i + page * pageBtnNumber];
                        if (isFunctSet)
                        {
                            this.buttons[i].Emp_level = empLevel[i + page * pageBtnNumber];
                            this.buttons[i].Button.Text = funct_name[i + page * pageBtnNumber];
                            this.buttons[i].Funct_id = funct_id[i + page * pageBtnNumber];
                            this.buttons[i].Index = index[i + page * pageBtnNumber];
                        }
                        else
                        {
                            this.buttons[i].Button.Text = stringArry[i + page * pageBtnNumber];
                        }
                    }
                }
                if (Page == (TotalBtn / pageBtnNumber))
                {
                    for (int i = 0; i < TotalBtn - pageBtnNumber * Page; i++)
                    {

                        this.buttons[i].Button.Text = stringArry[i + page * pageBtnNumber];
                        this.buttons[i].Button.BackColor = btnBackColor[i + page * pageBtnNumber];
                        this.buttons[i].Button.ForeColor = btnFontColor[i + page * pageBtnNumber];
                        this.buttons[i].Button.Font = btnTextFont[i + page * pageBtnNumber];
                        this.buttons[i].Position_id = position_id[i + page * pageBtnNumber];
                        this.buttons[i].Hotkey = hotkey[i + page * pageBtnNumber];
                        this.buttons[i].Visible1 = visible[i + page * pageBtnNumber];
                        this.buttons[i].Funct_name = funct_name[i + page * pageBtnNumber];
                        this.buttons[i].Dis_name = stringArry[i + page * pageBtnNumber];
                        if (isFunctSet)
                        {
                            this.buttons[i].Emp_level = empLevel[i + page * pageBtnNumber];
                            this.buttons[i].Button.Text = funct_name[i + page * pageBtnNumber];
                            this.buttons[i].Funct_id = funct_id[i + page * pageBtnNumber];
                            this.buttons[i].Index = index[i + page * pageBtnNumber];
                        }
                        else
                        {
                            this.buttons[i].Button.Text = stringArry[i + page * pageBtnNumber];
                        }
                    }
                }

                this.pageBtn[1].Visible = false;

            }
            GetBtnInfo(page, false);
        }


        /// <summary>
        /// button面板上显示button的数量
        /// </summary>
        /// <param name="number">button面板上显示button的数量</param>
        private void SetVisibleNumber(int number)
        {

            for (int i = 0; i < number; i++)
            {

                try
                {
                    buttons[i].Button.Visible = true;
                }
                catch { }
            }
            for (int i = number; i < pageBtnNumber; i++)
            {
                try
                {
                    buttons[i].Button.Visible = false;
                }
                catch { }

            }

        }


        #endregion

        #region 原来FunctionPanel.Designer.cs中自动生成的代码
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // FunctionPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "FunctionPanel";
            this.Size = new System.Drawing.Size(300, 300);
            this.Load += new System.EventHandler(this.FunctionPanel_Load);
            this.SizeChanged += new System.EventHandler(this.FunctionPanel_SizeChanged);
            this.ResumeLayout(false);

        }

        #endregion


        #endregion
    }

    /// <summary>
    /// 功能按钮
    /// </summary>
    public partial class FunctionButton : UserControl
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public FunctionButton()
        {
            InitializeComponent();
            this.button.Dock = DockStyle.Fill;
        }

        #region 原来FuncButton.Designer.cs中自动生成的代码
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button
            // 
            this.button.Location = new System.Drawing.Point(0, 0);
            this.button.Name = "button";
            this.button.Click += new EventHandler(button_Click);
            this.button.Size = new System.Drawing.Size(67, 68);
            this.button.TabIndex = 0;
            this.button.UseVisualStyleBackColor = true;
            // 
            // FuncButton
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.button);
            this.Name = "FuncButton";
            this.Size = new System.Drawing.Size(67, 68);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button;
        #endregion

        #region 字段
        /// <summary>
        /// 按钮在面板上的索引
        /// </summary>
        private int index;


        /// <summary>
        /// 位置序号
        /// </summary>
        private int position_id;


        /// <summary>
        /// 是否显示（可见）
        /// </summary>
        private bool visible;

        /// <summary>
        /// 按钮颜色
        /// </summary>
        private Color color;



        /// <summary>
        /// 字体大小
        /// </summary>
        private Size font_size;

        /// <summary>
        /// 字体
        /// </summary>
        private string font_name;


        /// <summary>
        /// 功能编号
        /// </summary>
        private int funct_id;


        /// <summary>
        /// 项目名称（区别于显示名称）
        /// </summary>
        private string funct_name;
        /// <summary>
        /// 显示名字
        /// </summary>
        private string dis_name;


        /// <summary>
        /// FunctionPanel对象
        /// </summary>
        private FunctionPanel functionPanel;

        /// <summary>
        /// 员工级别
        /// </summary>
        private string emp_level;

        #endregion

        #region 属性

        /// <summary>
        /// 按钮在面板上的索引
        /// </summary>
        public int Index
        {
            get { return index; }
            set { index = value; }
        }


        /// <summary>
        /// 显示名字
        /// </summary>
        public string Dis_name
        {
            get { return dis_name; }
            set { dis_name = value; }
        }

        /// <summary>
        /// 位置序号
        /// </summary>
        public int Position_id
        {
            get { return position_id; }
            set { position_id = value; }
        }

        /// <summary>
        /// 热键
        /// </summary>
        private int hotkey;


        /// <summary>
        /// 是否显示（可见）
        /// </summary>
        public bool Visible1
        {
            get { return visible; }
            set { visible = value; }
        }

        /// <summary>
        /// 按钮颜色
        /// </summary>
        public Color Color
        {
            get { return color; }
            set { color = value; }
        }

        /// <summary>
        /// 字体大小
        /// </summary>
        public Size Font_size
        {
            get { return font_size; }
            set { font_size = value; }
        }

        /// <summary>
        /// 字体
        /// </summary>
        public string Font_name
        {
            get { return font_name; }
            set { font_name = value; }
        }

        /// <summary>
        /// 热键
        /// </summary>
        public int Hotkey
        {
            get { return hotkey; }
            set { hotkey = value; }
        }



        /// <summary>
        /// 功能编号
        /// </summary>
        public int Funct_id
        {
            get { return funct_id; }
            set { funct_id = value; }
        }

        /// <summary>
        /// 项目名称（区别于显示名称）
        /// </summary>
        public string Funct_name
        {
            get { return funct_name; }
            set { funct_name = value; }
        }

        /// <summary>
        /// 控件内部的按钮
        /// </summary>
        public Button Button
        {
            get { return button; }
            set { button = value; }
        }


        /// <summary>
        /// FunctionPanel对象
        /// </summary>
        public FunctionPanel FunctionPanel
        {
            get { return functionPanel; }
            set { functionPanel = value; }
        }

        /// <summary>
        /// 员工级别
        /// </summary>
        public string Emp_level
        {
            get { return emp_level; }
            set { emp_level = value; }
        }


        #endregion

        #region 事件
        /// <summary>
        /// 按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Click(Object sender, EventArgs e)
        {
            if (functionPanel != null)
            {
                //功能设定时执行
                if (functionPanel.IsFunctSet)
                {
                    //点击后把本按钮的信息封装到一个类中传到调用处
                    ModelFunctions functions = new ModelFunctions();
                    functions.Color = this.button.BackColor;

                    functions.Disp_name = this.dis_name;
                    functions.Font_color = this.button.ForeColor;
                    functions.Font_name = this.button.Font.Name;
                    functions.Font_size = this.button.Font.Size;
                    functions.Funct_id = this.funct_id;
                    functions.Position_id = this.position_id;
                    functions.Funct_name = this.funct_name;
                    functions.Hotkey = this.hotkey;
                    functions.Visible = this.visible;
                    functions.Emp_level = this.emp_level;
                    functions.Index = this.index;
                    this.functionPanel.BtnSetInfo(functions);

                }
                //非功能设定时执行
                else
                {
                    this.button.Enabled = false;
                    this.functionPanel.BtnClick(this.funct_name);
                    this.button.Enabled = true;
                }
            }
        }

        #endregion





    }


}
