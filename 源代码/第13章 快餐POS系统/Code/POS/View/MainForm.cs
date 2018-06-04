using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using POS.Controls;
using POS.Models;
using POS.Common;
using POS.Service;
using POS.Data;
using System.Threading;
using System.Net.Sockets;
using System.Net;
using Common.Pole;
using System.IO;

namespace POS.View
{
    /// <summary>
    /// 主窗体
    /// </summary>
    public partial class MainForm : Form
    {
        #region 字段
        /// <summary>
        /// 进度框
        /// </summary>
        public static ProgressForm progressForm;
        /// <summary>
        /// 是否推出程序
        /// </summary>
        public bool isExit = false;
        /// <summary>
        /// 是否执行程序恢复
        /// </summary>
        public bool recoverSucess = false;
        /// <summary>
        /// 登录界面
        /// </summary>
        Login login;
        /// <summary>
        /// 下线界面
        /// </summary>
        Offline offline;
        /// <summary>
        /// 交班查询控件
        /// </summary>
        Pos_rounds_inquire pos_rounds_inquire;
        /// <summary>
        /// 备餐控件
        /// </summary>
        Beican beican;
        /// <summary>
        /// 加载功能面板信息的服务层的类
        /// </summary>
        private GetFunctions functions;
        /// <summary>
        /// 结账控件
        /// </summary>
        private CheckOut checkOut;
        /// <summary>
        /// 店总额查询控件
        /// </summary>
        private Shop_Tur shop_tur;
        /// <summary>
        /// 单机查询控件
        /// </summary>
        private Pos_Tur pos_tur;
        /// <summary>
        /// 读收银员帐
        /// </summary>
        private Cashier_Tur cashier_tur;
        #endregion
        /// <summary>
        /// 构造函数
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            //向功能面板中传递主窗体
            this.functionPanel.MainForm = this;
            //向数字控件中传递主窗体
            this.number.MainForm = this;
            //向数据传输组件中传递主窗体
            this.orderMenu.MainForm = this;
        }

        #region 属性
        /// <summary>
        /// 登录界面
        /// </summary>
        public Login Login
        {
            get { return login; }
            set { login = value; }
        }
        /// <summary>
        /// 下线界面
        /// </summary>
        public Offline Offline
        {
            get { return offline; }
            set { offline = value; }
        }
        /// <summary>
        /// 交班查询控件
        /// </summary>
        public Pos_rounds_inquire Pos_rounds_inquire
        {
            get { return pos_rounds_inquire; }
            set { pos_rounds_inquire = value; }
        }
        /// <summary>
        /// 备餐控件
        /// </summary>
        public Beican Beican
        {
            get { return beican; }
            set { beican = value; }
        }
        /// <summary>
        /// TableLayoutPanel控件（TlpRight）
        /// </summary>
        public Control TlpRight { get { return tlpRight; } }
        /// <summary>
        /// ShowInfo控件
        /// </summary>
        public ShowInfo ShowInfo1
        {
            get { return this.showInfo; }
            set { }
        }
        /// <summary>
        /// 获取和设置功能面板对象
        /// </summary>
        public FunctionPanel FunctionPanel
        {
            get { return this.functionPanel; }
            set { this.functionPanel = value; }
        }
        /// <summary>
        /// 结账控件
        /// </summary>
        public CheckOut CheckOut
        {
            get { return this.checkOut; }
            set { this.checkOut = value; }
        }
        /// <summary>
        /// 店总额查询控件
        /// </summary>
        public Shop_Tur shop_Tur
        {
            get { return this.shop_tur; }
            set { this.shop_tur = value; }
        }
        /// <summary>
        /// 单机查询控件
        /// </summary>
        public Pos_Tur pos_Tur
        {
            get { return this.pos_tur; }
            set { this.pos_tur = value; }
        }
        /// <summary>
        /// 读收银员帐控件
        /// </summary>
        public Cashier_Tur cashier_Tur
        {
            get { return this.cashier_tur; }
            set { this.cashier_tur = value; }
        }
        /// <summary>
        /// 数字控件
        /// </summary>
        public Number Number
        {
            get { return number; }
            set { number = value; }
        }
        /// <summary>
        /// TableLayoutPanel控件（TlpRight）
        /// </summary>
        public TableLayoutPanel ReturnTlpRight
        {
            get { return tlpRight; }
        }
        /// <summary>
        /// 点单控件
        /// </summary>
        public OrderMenu ReturenOrderMenu
        {
            get { return this.orderMenu; }
        }
        /// <summary>
        /// 返回滚动条
        /// </summary>
        public Roll ReturnRoll
        {
            get { return this.roll; }
        }
        /// <summary>
        /// 基本信息
        /// </summary>
        public ShowInfo ReturnShowInfo
        {
            get { return this.showInfo; }
        }
        /// <summary>
        /// TableLayoutPanel控件（TlpMain）
        /// </summary>
        public TableLayoutPanel ReturnTlpMain
        {
            get { return tlpMain; }
        }
        /// <summary>
        /// TableLayoutPanel控件（mainTlp）
        /// </summary>
        public TableLayoutPanel ReturnMainTlp
        {
            get { return mainTlp; }
        }
        /// <summary>
        /// 商品类别面板
        /// </summary>
        public BtnPanelKind BtnPanelKind
        {
            get { return this.btnPanelKind; }

        }
        /// <summary>
        /// 商品面板
        /// </summary>
        public BtnPanelProd BtnPanelProd
        {
            get { return this.btnPanelProd; }
        }
        /// <summary>
        /// 参数设定组件
        /// </summary>
        public OperPara OperPara
        {
            get { return this.operPara1; }
        }
        #endregion

        #region 事件

        #region 主窗体引发的事件
        /// <summary>
        /// 主窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void MainForm_Load(object sender, EventArgs e)
        {
            this.Visible = false;
            ReadIni readIni = new ReadIni();
            CreateDB();

            Info.printPort = operPara1.GetIniConfig("printPort");
            Info.cusShowPort = operPara1.GetIniConfig("cusShowPort");
            Info.showPort = operPara1.GetIniConfig("showPort");
            Info.ICPort = operPara1.GetIniConfig("ICPort");

            #region 生成连接字符串
            string localIp = this.operPara1.LocalIp;
            if (localIp == "")
            {
                string strHostName = Dns.GetHostName(); //得到本机的主机名 
                localIp = Dns.GetHostEntry(strHostName).AddressList[0].ToString(); //取得本机IP    
            }
            //创建带实例名的连接
            Info.Constr = "server=" + localIp + @"\" + this.operPara1.LocalCase + "," + this.operPara1.LocalPort + ";" + "database=" + this.operPara1.LocalDBName + ";" + "uid=" + this.operPara1.LocalUserName + ";pwd=" + "'" + this.operPara1.LocalPassword + "'";
            #endregion
            try
            {
                Info.insertSingleProd = new InsertSingleProd();
                Info.getProduct = new GetProduct0();
                login = new Login();
                login.MainForm = this;
                login.ShowDialog();

                this.functionPanel.LoadBtnInfo(Info.emp_id, "", true);

                if (recoverSucess)
                {
                    //加载商品类别信息
                    this.btnPanelKind.LoadInfo(0, true);
                    this.Visible = true;
                    this.recoverSucess = false;
                    //若上次处于结账状态，恢复成功时恢复结账前状态
                    UpdateSales.InitUpdateSales().DelSalesSucess();
                    this.orderMenu.LoadInfo();
                    return;
                }

                #region 如果本地数据库中的数据达到一定要求则按条件删除一部分
                DelLocalDB del = new DelLocalDB(this);
                del.DelLocalData();
                #endregion

                //加载商品类别信息
                this.btnPanelKind.LoadInfo(0, true);
                this.Visible = true;
            }
            catch { }
        }
        /// <summary>
        /// 主窗体关闭事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Info.isexit == false)
            {
                if (this.offline != null)
                {
                    if (this.offline.isExitType == false)
                    {
                        if (MessageBox.Show("感谢你的使用，确定未下线退出系统吗？", "快餐ERP系统", MessageBoxButtons.OKCancel) == DialogResult.OK)
                        {
                            e.Cancel = false;
                        }
                        else
                        {
                            e.Cancel = true;
                        }

                    }
                }
            }

        }
        #endregion

        #region 功能面板引发的事件

        /// <summary>
        /// 功能面板加载信息
        /// </summary>
        /// <param name="functionPanel">功能面板对象</param>
        /// <param name="emp_id">员工编号</param>
        /// <param name="emp_level">员工等级</param>
        /// <param name="isLoad">是否初次加载</param>
        private void FunctionPanel_GetInfo(FunctionPanel functionPanel, string emp_id, string emp_level, bool isLoad)
        {
            LoadFuncPanel(functionPanel, emp_id, emp_level, isLoad);
        }

        /// <summary>
        ///  功能面板加载信息
        /// </summary>
        /// <param name="functionPanel">功能面板对象</param>
        /// <param name="emp_id">员工编号</param>
        /// <param name="emp_level">员工级别</param>
        /// <param name="isLoad">是否为初次加载</param>
        /// <returns></returns>
        public bool LoadFuncPanel(FunctionPanel functionPanel, string emp_id, string emp_level, bool isLoad)
        {
            if (isLoad)
            {
                functions = new GetFunctions();
                functions.Getfunction(emp_id, false);
                functionPanel.TotalBtn = functions.ReturenRecordNumber;

                string[] txtArray = new string[functionPanel.TotalBtn];

                Color[] btnBackColor = new Color[functionPanel.TotalBtn];

                Font[] btnTextFont = new Font[functionPanel.TotalBtn];

                Color[] btnFontColor = new Color[functionPanel.TotalBtn];

                int[] position_id = new int[functionPanel.TotalBtn];

                int[] hotkey = new int[functionPanel.TotalBtn];

                bool[] visible = new bool[functionPanel.TotalBtn];

                string[] funct_name = new string[functionPanel.TotalBtn];

                for (int i = 0; i < functionPanel.TotalBtn; i++)
                {
                    txtArray[i] = functions.ReturnProdName1(i);
                    btnBackColor[i] = functions.ReturnBtnColor(i);
                    btnTextFont[i] = functions.ReturnFont(i);
                    btnFontColor[i] = functions.ReturnFondColor(i);
                    position_id[i] = functions.ReturnPositionId(i);
                    hotkey[i] = functions.ReturnHotKey(i);
                    visible[i] = functions.ReturnVisible(i);
                    funct_name[i] = functions.ReturnFunctName(i);

                    if (i < functionPanel.PageBtnNumber && i < functionPanel.Buttons.Length)
                    {
                        functionPanel.Buttons[i].Button.BackColor = btnBackColor[i];
                        // 这一项保存了font_name和font_size
                        functionPanel.Buttons[i].Button.Font = btnTextFont[i];
                        functionPanel.Buttons[i].Position_id = position_id[i];
                        functionPanel.Buttons[i].Hotkey = hotkey[i];
                        functionPanel.Buttons[i].Visible1 = visible[i];
                        functionPanel.Buttons[i].Funct_name = funct_name[i];
                        functionPanel.Buttons[i].Button.ForeColor = btnFontColor[i];

                    }

                }
                functionPanel.StringArray = txtArray;
                functionPanel.btnBackColor = btnBackColor;
                functionPanel.btnFontColor = btnFontColor;
                functionPanel.position_id = position_id;
                functionPanel.hotkey = hotkey;
                functionPanel.visible = visible;
                functionPanel.funct_name = funct_name;
                functionPanel.btnTextFont = btnTextFont;
                number.FunctionName = "";
            }
            return true;
        }

        #endregion

        #region 商品类别面事件处理的方法

        GetProductKind getProductKind1;
        /// <summary>
        /// 给商品类别面板加载信息
        /// </summary>
        /// <param name="btnPanelKind">BtnPanelKind的对象</param>
        /// <param name="isLoad">是否初次加载</param>
        private void BtnPanelKind_GetInfo(BtnPanelKind btnPanelKind, bool isLoad)
        {
            //isLoad为true表示第一次加载商品类别面板
            if (isLoad)
            {
                getProductKind1 = new GetProductKind();
                getProductKind1.GetProduatKind();
                btnPanelKind.TotalBtn = getProductKind1.ReturnRecordCount;
                btnPanelKind.datasetProdKind = getProductKind1.dataSet;
            }
            //非最后一页
            if (btnPanelKind.Page < (btnPanelKind.TotalBtn / btnPanelKind.PageBtnNum))
            {

                for (int i = 0; i < btnPanelKind.PageBtnNum; i++)
                {
                    btnPanelKind.ButtonDIY[i].Button.Text = getProductKind1.ReturnDeName(btnPanelKind.Page * btnPanelKind.PageBtnNum + i);
                    btnPanelKind.ButtonDIY[i].BtnPanelKind = btnPanelKind;
                    btnPanelKind.ButtonDIY[i].Dep_id = getProductKind1.ReturnDepId(btnPanelKind.Page * btnPanelKind.PageBtnNum + i);
                    btnPanelKind.ButtonDIY[i].Button.BackColor = getProductKind1.ReturnBtnColor(btnPanelKind.Page * btnPanelKind.PageBtnNum + i);
                    btnPanelKind.ButtonDIY[i].Button.Font = getProductKind1.ReturnFont(btnPanelKind.Page * btnPanelKind.PageBtnNum + i);
                    btnPanelKind.ButtonDIY[i].Button.ForeColor = getProductKind1.ReturnFondColor(btnPanelKind.Page * btnPanelKind.PageBtnNum + i);
                }
            }
            //最后一页
            if (btnPanelKind.Page == (btnPanelKind.TotalBtn / btnPanelKind.PageBtnNum))
            {

                for (int i = 0; i < btnPanelKind.TotalBtn - btnPanelKind.PageBtnNum * btnPanelKind.Page; i++)
                {
                    btnPanelKind.ButtonDIY[i].Button.Text = getProductKind1.ReturnDeName(btnPanelKind.Page * btnPanelKind.PageBtnNum + i);
                    btnPanelKind.ButtonDIY[i].BtnPanelKind = btnPanelKind;
                    btnPanelKind.ButtonDIY[i].Dep_id = getProductKind1.ReturnDepId(btnPanelKind.Page * btnPanelKind.PageBtnNum + i);
                    btnPanelKind.ButtonDIY[i].Button.BackColor = getProductKind1.ReturnBtnColor(btnPanelKind.Page * btnPanelKind.PageBtnNum + i);
                    btnPanelKind.ButtonDIY[i].Button.Font = getProductKind1.ReturnFont(btnPanelKind.Page * btnPanelKind.PageBtnNum + i);
                    btnPanelKind.ButtonDIY[i].Button.ForeColor = getProductKind1.ReturnFondColor(btnPanelKind.Page * btnPanelKind.PageBtnNum + i);
                }

            }

        }
        /// <summary>
        /// 商品类别面板点后调用商品面板加载信息
        /// </summary>
        /// <param name="dep_id">商品类别号</param>
        private void BtnPanelKind_SetInfo(string dep_id)
        {
            this.btnPanelProd.LoadBtnInfo(dep_id, true);
        }


        #endregion

        #region 商品面板事件处理方法
        /// <summary>
        /// 给商品面板加载信息
        /// </summary>
        /// <param name="buttonPanel">商品面板对象</param>
        /// <param name="isLoad">是否为初次加载</param>
        private void BtnPanelProd_GetInfo(BtnPanelProd buttonPanel, bool isLoad)
        {
            if (Info.getProduct == null)
            {
                Info.getProduct = new GetProduct0();
            }
            if (isLoad)
            {

                Info.getProduct.GetProducts(buttonPanel.Dep_id, Info.shop_id);
                buttonPanel.TotalBtn = Info.getProduct.ProductNumber;

            }



            if (buttonPanel.Page < (buttonPanel.TotalBtn / buttonPanel.PageBtnNum))
            {
                for (int i = 0; i < buttonPanel.PageBtnNum; i++)
                {
                    buttonPanel.ButtonDIY[i].Button.Text = Info.getProduct.ReturnProdName(buttonPanel.Page * buttonPanel.PageBtnNum + i);
                    buttonPanel.ButtonDIY[i].Prod_id = Info.getProduct.ReturnProId(buttonPanel.Page * buttonPanel.PageBtnNum + i);
                    buttonPanel.ButtonDIY[i].Button.BackColor = Info.getProduct.ReturnBtnColor(buttonPanel.Page * buttonPanel.PageBtnNum + i);
                    buttonPanel.ButtonDIY[i].Combo_type = Info.getProduct.ReturnCombo_type(buttonPanel.Page * buttonPanel.PageBtnNum + i);
                    buttonPanel.ButtonDIY[i].Button.Font = Info.getProduct.ReturnFont(buttonPanel.Page * buttonPanel.PageBtnNum + i);
                    buttonPanel.ButtonDIY[i].Sale_price = Info.getProduct.ReturnPrice1(buttonPanel.Page * buttonPanel.PageBtnNum + i);


                }
            }
            if (buttonPanel.Page == (buttonPanel.TotalBtn / buttonPanel.PageBtnNum))
            {
                for (int i = 0; i < buttonPanel.TotalBtn - buttonPanel.PageBtnNum * buttonPanel.Page; i++)
                {
                    buttonPanel.ButtonDIY[i].Button.Text = Info.getProduct.ReturnProdName(buttonPanel.Page * buttonPanel.PageBtnNum + i);
                    buttonPanel.ButtonDIY[i].Prod_id = Info.getProduct.ReturnProId(buttonPanel.Page * buttonPanel.PageBtnNum + i);
                    buttonPanel.ButtonDIY[i].Button.BackColor = Info.getProduct.ReturnBtnColor(buttonPanel.Page * buttonPanel.PageBtnNum + i);
                    buttonPanel.ButtonDIY[i].Combo_type = Info.getProduct.ReturnCombo_type(buttonPanel.Page * buttonPanel.PageBtnNum + i);
                    buttonPanel.ButtonDIY[i].Button.Font = Info.getProduct.ReturnFont(buttonPanel.Page * buttonPanel.PageBtnNum + i);
                    buttonPanel.ButtonDIY[i].Sale_price = Info.getProduct.ReturnPrice1(buttonPanel.Page * buttonPanel.PageBtnNum + i);

                }

            }
        }
        /// <summary>
        /// 点击商品面板的按钮执行的方法
        /// </summary>
        /// <param name="btnPaneProd">商品面板的引用</param>
        /// <param name="prod_id">商品id</param>
        /// <param name="combo_type">组合餐类型</param>
        private void BtnPanelProd_SetInfo(BtnPanelProd btnPaneProd, string prod_id, string combo_type)
        {
            int num;
            if (Info.inputNumber >=(decimal)0.001)
            {
                try
                {
                    num = Convert.ToInt32(Info.inputNumber);
                }
                catch { num = 1; }
            }
            else
            {
                num = 1;
            }
            //单餐
            if ("0" == combo_type)
            {
                Info.insertSingleProd.InsertSaleTmp1(prod_id, num, 0);
            }

            //点单控件加载信息
            this.orderMenu.LoadInfo();
            this.Number.TextBoxText = "";            
        }
        #endregion

        #region 点单控件事件出来方法
        /// <summary>
        /// 给点单控件加载信息
        /// </summary>
        /// <param name="orderMenu">点单控件对象</param>
        private void OrderMenu_GetInfo(OrderMenu orderMenu)
        {

            //OrderDinner的集合
            List<OrderDinner> listOrderDinner = new List<OrderDinner>();

            GetSaleTemp01 getSale01 = new GetSaleTemp01();
            //选择的商品的总价格
            Info.totalPrice = 0;

            DataSet dataSet = getSale01.GetOrderInfo(Info.sale_id);


            //根据记录个数创建index_groupId数组
            orderMenu.Init_Index_Group_Array(dataSet);
            OrderDinner orderDinner;
            if (dataSet.Tables[0].Rows.Count > 0)
            {
                int xuhao = 1;
                for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                {
                    string a = getSale01.Price_type(i);
                    //单餐
                    if (getSale01.Comb_type(i).Equals("0"))
                    {
                        orderDinner = new OrderDinner();
                        orderDinner.ProdName = getSale01.Pos_disp(i);
                        orderDinner.Number = getSale01.ProdNumber(i);
                        orderDinner.Discount = Convert.ToDecimal(dataSet.Tables[0].Rows[i]["ITEM_DISC"]);
                        orderDinner.Price = getSale01.ProdPrice(i);
                        Info.totalPrice += orderDinner.Price * orderDinner.Number + Convert.ToDecimal(dataSet.Tables[0].Rows[i]["ITEM_DISC"]);
                        orderDinner.Xu = xuhao++;

                        orderDinner.Discount = decimal.Round(orderDinner.Discount, 1);
                        orderDinner.Total = getSale01.Act_price(i) * orderDinner.Number;
                        //保留一位小数
                        orderDinner.Price = decimal.Round(orderDinner.Price, 1);
                        orderDinner.Total = decimal.Round(orderDinner.Total, 1);

                        listOrderDinner.Add(orderDinner);
                        if (orderDinner.Number == 1)
                        {
                            PoleDisplayer.Port = Info.cusShowPort;
                            PoleDisplayer.Clear();
                        }
                    }
                }

                this.operPara1.WriteIniInfo("totalPrice", Info.totalPrice.ToString());
                orderMenu.TotalPrice = (Info.totalPrice).ToString();
                //使价格最小精确到角
                orderMenu.TotalPrice = decimal.Round(Convert.ToDecimal(orderMenu.TotalPrice), 1) + "元";
                orderMenu.DgvOrderMenu.DataSource = null;
                orderMenu.DgvOrderMenu.DataSource = listOrderDinner;
            }
            else
            {
                orderMenu.TotalPrice = "0.0元";
                orderMenu.DgvOrderMenu.DataSource = null;
            }
            if (this.orderMenu.DgvOrderMenu.Rows.Count == 0)
            {
                Info.isDistinct = false;
                Info.isAllowance = false;
            }
        }
        /// <summary>
        /// 删除点单控件中选中的一个组商品
        /// </summary>
        /// <param name="orderMenu">点单控件对象</param>
        private void OrderMenu_DeleteGroup(OrderMenu orderMenu)
        {
            UpdateSales updateSale01 = new UpdateSales();

            if (orderMenu.SelectedGroupProd != "")
            {
                //从数据库中删除该组商品
                updateSale01.DelGroupProd(orderMenu.SelectedGroupProd);
                //清空选中的组号
                orderMenu.SelectedGroupProd = "";
                //重新给dataGridView绑定信息
                orderMenu.LoadInfo();
            }
        }
        /// <summary>
        /// 修改商品数量
        /// </summary>
        /// <param name="group_prod">组商品号</param>
        /// <param name="type">修改的类型，0代表自加1，1代表自减1,2代表把商品数量改成number代表的数量</param>
        /// <param name="number">修改的类型，0代表自加1，1代表自减1</param>
        private void OrderMenu_AlterProd(string group_prod, int type, decimal number)
        {
            UpdateSales updateSales = new UpdateSales();
            if (updateSales.CanChangeQty(group_prod, type, Info.sale_id, number))
            {
                updateSales.setSalesQty(group_prod, type, number);
                this.orderMenu.LoadInfo();
            }
            else
            {
                MessageBox.Show("改变数量失败！");
            }
        }
        /// <summary>
        /// 对商品进行折扣
        /// </summary>
        /// <param name="selectedGroupProd">选中的Group_Prod</param>
        private void OrderMenu_DiscountGroupProd(string selectedGroupProd)
        {
            UpdateSales updateSales = new UpdateSales();
            updateSales.setPriceDisc(selectedGroupProd, Info.inputNumber);
            this.orderMenu.LoadInfo();
        }
        /// <summary>
        /// 点击点单控件中的备餐按钮时执行的事件
        /// </summary>
        private void orderMenu_BeicanClick()
        {
            if (this.beican == null)
            {
                this.beican = new Beican();
                this.beican.MainForm = this;
            }
            this.beican.LoadInfo(Info.sale_id);
            this.tlpMain.Controls.Remove(this.tlpRight);
            this.tlpMain.Controls.Add(this.beican, 1, 0);
            this.beican.Dock = DockStyle.Fill;
        }
        #endregion

        #endregion

        /// <summary>
        /// 如果本地数据库不存在则创建它
        /// </summary>
        public void CreateDB()
        {
            #region 如果本地数据库不存在则创建它
            CreateDB createDb = new CreateDB();

            //获取本地连接信息
            int k = createDb.IsExitDB();
            //不存在数据库活着数据库没有附加上去
            if (k == 0)
            {

                if (File.Exists("C:\\RepastErp_data.mdf"))
                {
                    File.Delete("C:\\RepastErp_data.mdf");
                }
                if (File.Exists("C:\\RepastErp_log.ldf"))
                {
                    File.Delete("C:\\RepastErp_log.ldf");
                }

                if (!createDb.CreateDatabase())
                {
                    MessageBox.Show("创建数据库失败！");
                    Application.Exit();
                }


            }
            //本地信息配置有错误
            else if (k == 2)
            {
                MessageBox.Show("创建数据库失败！有可能是本地数据库据端口、用户名及密码配置错误。配置完后重新启动系统。");
                Environment.Exit(0);
            }
            //存在数据库
            else
            {

            }
            #endregion
        }
        /// <summary>
        /// 打印小票，添加打印字符串
        /// </summary>
        /// <param name="list">字符串集合</param>
        /// <param name="info">要添加的字符串</param>
        public void AddExtraInfo(List<string> list, string info)
        {
            int index = 0;
            while (index != -1)
            {
                index = info.IndexOf("RePaStErPx");
                if (index == -1)
                {
                    list.Add(info);
                    break;
                }
                list.Add(info.Substring(0, index));
                info = info.Substring(info.IndexOf("RePaStErPx") + 10);
            }
        }
    }
}
