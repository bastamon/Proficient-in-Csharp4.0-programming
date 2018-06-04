using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Common.Pole;
using Common.Print;
using POS.Service;
using POS.Controls;
using POS.View;
using POS.Common;
using System.Threading;

/*结账面板的创建、临时表向后台数据表传送、打印发票等相关操作
 类说明：结账控件
主要公共方法：完成结账、临时表向后台数据表传送、打印发票等相关操作。
（1）、PrintInvoice()，打印发票
（2）、InsertSaleTemp02(string shop_id, string sale_id, int sale_sno, string pay_id, decimal amount, char transfer_status, DateTime last_update, decimal Face_Value)，完成对saletemp02表的插入
（3）、DeleteSaleTmp02(string shop_id, string sale_id)，完成对saletemp02表的指定row删除操作
主要事件：
（1）、ckPanel1_SetInfo(CKPanel ckPanel, string btnText)，面板的处理点击事件
（2）、ckPanel1_GetInfo(CKPanel ckPanel, bool isLoad)，对CkPanel的初始化
嵌套类：
（1）、OperThread，用于线程调用，完成结账功能。

 */
namespace POS.Controls
{
    /// <summary>
    /// 自定义控件类，完成结账面板的创建和相关操作
    /// </summary>
    public partial class CheckOut : UserControl
    {
        #region 字段
        /// <summary>
        /// MainForm的一个变量
        /// </summary>
        private MainForm mainForm;
        /// <summary>
        /// 用于获得当前时间
        /// </summary>
        DateTime dateTime = DateTime.Now;
        /// <summary>
        /// 保存已付金额
        /// </summary>
        private decimal btnInputNumber = 0;
        /// <summary>
        /// 用于保存现金输入值
        /// </summary>
        private decimal btnMoney = 0;
        /// <summary>
        /// 保存精度
        /// </summary>
        private int dataAccur = 2;
        /// <summary>
        /// 用于保存结账类型，包括员工餐，招待，正常结账
        /// </summary>
        private string checkOutType = "结账";
        /// <summary>
        /// 用于保存结账类型，包括员工餐，招待，正常结账
        /// </summary>
        public string CheckOutType1
        {
            get { return checkOutType; }
            set { checkOutType = value; }
        }
        /// <summary>
        /// 销售单序号
        /// </summary>
        private int sale_sno = 1;
        /// <summary>
        /// 销售单序号，供使用兑换券使用
        /// </summary>
        public int Sale_sno
        {
            get { return sale_sno; }
            set { sale_sno = value; }
        }
        /// <summary>
        /// 定义getPayment的一个实例
        /// </summary>
        private GetPayment getPayment = new GetPayment();
        /// <summary>
        /// 保存使用兑换券的总金额
        /// </summary>
        public decimal BtnInputNumber
        {
            get { return btnInputNumber; }
            set
            {
                if (btnChit)
                {
                    btnInputNumber += value;
                }
                else
                {
                    return;
                }
            }
        }
        /// <summary>
        /// 定义bool变量用来表示现金按钮是否选中
        /// </summary>
        private bool btnmoneyclick = false;
        /// <summary>
        /// 定义bool变量用来表示兑换券按钮是否选中
        /// </summary>
        private bool btnChit = false;
        /// <summary>
        /// 定义bool变量用来表示兑换券按钮是否选中
        /// </summary>
        public bool BtnChit
        {
            get { return btnChit; }
            set { btnChit = value; }
        }
        /// <summary>
        /// 用来记录处于第几条记录
        /// </summary>
        private int index = 0;
        /// <summary>
        /// 返回CheckOut的一个实例
        /// </summary>
        public CheckOut CheckOutSize
        {
            get { return this; }
        }
        /// <summary>
        /// 初始化变量值
        /// </summary>
        public MainForm MainForm
        {
            get { return mainForm; }
            set { mainForm = value; }
        }
        /// <summary>
        /// 判断是否打印小票
        /// </summary>
        private bool enablePrint = false;
        /// <summary>
        /// 结构体变量用于保存付账状态
        /// </summary>
        private struct status
        {
            public string name;
            public int count;
        }
        /// <summary>
        /// 结构体数组变量用于保存付账状态
        /// </summary>
        private status[] checkOutMoney;
        /// <summary>
        /// 用于保存打印账单信息
        /// </summary>
        List<string> listPrint = new List<string>();
        #endregion
        /// <summary>
        /// 构造函数
        /// </summary>
        #region 构造函数
        public CheckOut()
        {
            InitializeComponent();
            PoleDisplayer.Port = Info.cusShowPort;

        }
        #endregion
        #region 私有事件
        /// <summary>
        /// 确定按钮button的处理事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void btnExit_Click(object sender, EventArgs e)
        {
            if (!(Decimal.Round(Convert.ToDecimal(this.txtPay.Text), 2) == 0) && (Decimal.Round(Convert.ToDecimal(this.txtPay.Text), 2) - Info.totalPrice < 0))
            {
                MessageBox.Show("应付金额小于已付金额！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                SetPrice();

            }
            else
            {
                if (Decimal.Round(Convert.ToDecimal(this.txtPay.Text), 2) == 0)
                {
                    //应付金额等于已付金额不弹出找零界面
                    if (Info.inputNumber == Info.totalPrice || Info.inputNumber == 0)
                    {

                        btnMoney = btnInputNumber = Info.totalPrice;
                        //默认为现金付款
                        btnmoneyclick = true;
                        //现金为0条记录
                        index = 0;
                    }
                    //应付金额大于已付金额弹出找零界面
                    else if (Info.inputNumber > Info.totalPrice)
                    {
                        btnMoney = btnInputNumber = Info.inputNumber;
                        //默认为现金付款
                        btnmoneyclick = true;
                        //现金为0条记录
                        index = 0;
                        this.txtRequire.Text = decimal.Round(Info.totalPrice, 1).ToString();
                        this.txtPay.Text = decimal.Round(Info.inputNumber, 1).ToString();
                        this.txtGiveBack.Text = decimal.Round((Info.inputNumber - Info.totalPrice), 1).ToString();

                    }
                    else
                    {//应付金额小于已付金额给出提示
                        MessageBox.Show("应付金额小于已付金额！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Info.inputNumber = 0;
                        this.mainForm.OperPara.WriteIniInfo("inputNumber", "0");
                        this.mainForm.Number.TextBoxText = "";
                        return;
                    }


                }
                dateTime = DateTime.Now;
                OperThread operThread = new OperThread(this);
                Thread oThreadOperateSale = new Thread(new ThreadStart(operThread.method));
                oThreadOperateSale.Start();
                RecoverType(checkOutType);
                this.mainForm.ReturnTlpMain.Controls.Remove(this);
                this.mainForm.ReturenOrderMenu.ClearListOrderDinner();
                this.mainForm.ReturnTlpMain.Controls.Add(this.mainForm.ReturnTlpRight, 1, 0);

                // 如果是现金支付，则钱箱中钱数加上支付金额
                if (checkOutType.Equals("结账"))
                {                    
                    Info.remain_sum += Info.totalPrice;
                    this.mainForm.OperPara.WriteIniInfo("remain_sum", Convert.ToString(Info.remain_sum));
                }
                //结束结账状态
                Info.StatusEnd("结账确定", "");
                
                labstate.Text = "";
                checkOutType = "结账";
                ClearcheckOutMoney();
                sale_sno = 1;
                btnInputNumber = 0;
                btnMoney = 0;
                index = 0;
                enablePrint = false;
                this.btnPrint.BackColor = System.Drawing.SystemColors.ActiveCaptionText;


                //完成对saletmp00表的销售单号的插入
                InsertSaleTmp00.InitInsertSaleTmp00().DataInsertSaleTmp00();
                this.mainForm.ShowInfo1.ChangeText(Info.deal_number.ToString(), Info.emp_name, Info.pos_id, Info.emp_id.ToString(), Info.shift_num.ToString());
                Info.inputNumber = 0;
                this.mainForm.OperPara.WriteIniInfo("inputNumber", "0");
                this.mainForm.Number.TextBoxText = "";
            }
        }
        /// <summary>
        /// 取消按钮消息处理事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void btnCancle_Click(object sender, EventArgs e)
        {
            btnClear.Visible = true;
            btnClear.Enabled = true;
            btnPrint.Enabled = true;
            btnPrint.Visible = true;
            index = 0;
            PoleDisplayer.Clear();
            this.MainForm.Number.TextBoxText = "";
            Info.inputNumber = 0;
            this.mainForm.OperPara.WriteIniInfo("inputNumber", "0");
            this.txtPay.Text = "";
            this.txtRequire.Text = "";
            this.txtGiveBack.Text = "";
            this.labstate.Text = "";
            if (checkOutType.Equals("结账"))
            {
                ClearcheckOutMoney();
                RecoverDate();
            }
            else
            {
                btnClear.Enabled = true;
                btnInputNumber = 0;
                checkOutType = "结账";
            }
            //结账取消按钮
            Info.StatusEnd("结账取消", "");
            //结束结账状态            
            sale_sno = 1;
            this.mainForm.ReturnTlpMain.Controls.Remove(this);
            this.mainForm.ReturnTlpMain.Controls.Add(this.mainForm.TlpRight, 1, 0);
            this.mainForm.TlpRight.Dock = DockStyle.Fill;
        }

        /// <summary>
        /// 面板初始化操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckOut_Load(object sender, EventArgs e)
        {
            ckPanel1_GetInfo(this.ckPanel1, true);
        }
        /// <summary>
        /// ckPanel1的加载事件
        /// </summary>
        /// <param name="sender">ckPanel1</param>
        /// <param name="e">e</param>
        private void ckPanel1_Load(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 打印小票
        /// </summary>
        /// <param name="sender">this</param>
        /// <param name="e">e</param>
        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (!enablePrint)
            {
                this.btnPrint.BackColor = System.Drawing.Color.Red;
                enablePrint = true;
            }
            else
            {
                this.btnPrint.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
                enablePrint = false;
            }

        }
        /// <summary>
        /// 选择按钮消息处理事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelect_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 清空按钮消息处理事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClear_Click(object sender, EventArgs e)
        {
            sale_sno = 1;
            Info.inputNumber = 0;
            this.MainForm.Number.TextBoxText = "";
            Info.inputNumber = 0;
            this.mainForm.OperPara.WriteIniInfo("inputNumber", "0");
            this.txtPay.Text = "";
            this.labstate.Text = "";
            ClearcheckOutMoney();
            index = 0;
            RecoverDate();
            SetPrice();
        }
        #endregion
        #region//公共方法
        /// <summary>
        /// 用于线程的调用
        /// </summary>
        /// <param name="sale_id">销售单号</param>
        /// <param name="shop_id">分店号</param>
        /// <param name="btninputmumber">已付金额</param>
        /// <param name="totalPrice">应付金额</param>
        /// <param name="method_id">销售方法</param>
        /// <param name="btnMoneyclick">现金按钮是否选中</param>
        /// <param name="sale_Sno">销售单序号</param>
        /// <param name="indexPayment">处于第几条记录</param>
        /// <param name="datetime">当前时间</param>
        /// <param name="btnmoney">现金输入</param>
        ///<param name="pos_id">Pos机编号</param>
        ///<param name="emp_id">收银员编号</param>
        ///<param name="type">结账类型</param>
        public void InsertSaleThread(string sale_id, string shop_id, decimal btninputmumber, decimal totalPrice, int method_id, bool btnMoneyclick, int sale_Sno, int indexPayment, DateTime datetime, decimal btnmoney, string pos_id, string emp_id, string type)
        {
            PrintInvoice(sale_id, shop_id, pos_id, emp_id, totalPrice, btninputmumber, type, datetime);
            //PoleDisplayer.Clear();
            //对结账确定按钮的对数据插入工作
            if (type.Equals("结账"))
            {
                BtnConfirm(sale_id, shop_id, btninputmumber, totalPrice, method_id, btnMoneyclick, sale_Sno, indexPayment, datetime, btnmoney);
            }
            else
            {
                TypeUpdateTable(type, sale_id, shop_id, emp_id);
            }
            //完成临时表向后台数据表的对应传入
            InsertSallData(sale_id, shop_id, type);
        }
        /// <summary>
        /// 设置Lable按钮的相关信息
        /// </summary>
        public void SetPrice()
        {

            this.txtRequire.Text = decimal.Round(Info.totalPrice, dataAccur).ToString();
            this.txtPay.Text = decimal.Round(btnInputNumber, dataAccur).ToString();
            if (btnInputNumber< (decimal)0.01)
            {
                PoleDisplayer.Clear();
                PoleDisplayer.Sum(decimal.Round(Info.totalPrice, 2).ToString());
            }
            else
            {
                PoleDisplayer.Clear();
                //显示给顾客看
                PoleDisplayer.Get(this.txtPay.Text);
            }
            if (0 <= (Convert.ToDecimal(this.txtPay.Text) - Info.totalPrice))
            {
                this.txtGiveBack.Text = decimal.Round((Convert.ToDecimal(this.txtPay.Text) - Info.totalPrice), dataAccur).ToString();
            }
            else
            {
                this.txtGiveBack.Text = "0.0";
            }
            //PoleDisplayer.Change(this.txtGiveBack.Text);
        }
        /// <summary>
        ///  打印发票
        /// </summary>
        /// <param name="saleid">销售单号</param>
        /// <param name="shopid">分店编号</param>
        /// <param name="posid">pos机号</param>
        /// <param name="empid">收银员编号</param>
        /// <param name="totalPrice">应付金额</param>
        /// <param name="btnInput">已付金额</param>
        /// <param name="type">结账类型</param>
        /// <param name="datetime">当前时间</param>
        public void PrintInvoice(string saleid, string shopid, string posid, string empid, decimal totalPrice, decimal btnInput, string type, DateTime datetime)
        {

            //实例GetSale对象用来返回销售id号
            GetSaleTmp01 getSale01 = new GetSaleTmp01(saleid, shopid);
            int counttotle = getSale01.ReturenRecordNumber;
            //先清空账单
            listPrint.Clear();
            //listPrint.Add(this.mainForm.OperPara.TicketHead);//外界设置
            this.mainForm.AddExtraInfo(listPrint, this.mainForm.OperPara.TicketHead);
            listPrint.Add("日期: " + datetime.ToShortDateString() + "   时间: " + datetime.ToString("HH:mm:ss"));
            listPrint.Add("款台:" + posid + "   收银员:" + empid);
            listPrint.Add("编号:" + saleid);
            if (type.Equals("员工餐") || type.Equals("招待"))
            {
                listPrint.Add("结账类型：" + type);
            }
            listPrint.Add("品名        数量        小计 ");
            listPrint.Add("----------------------------");
            decimal totqunt = 0;
            for (int i = 0; i < counttotle; i++)
            {
                try
                {
                    //组合餐的子产品不打印
                    if (getSale01.ReturnComb_Type(i) != "2")
                    {
                        decimal qty = decimal.Round(getSale01.ReturnQty(i), 0);
                        totqunt += qty;
                        //小计
                        string price = (decimal.Round(Convert.ToDecimal(getSale01.ReturnSale_Price(i)) * qty+Convert.ToDecimal(getSale01.ReturnItem_Disc_Tot(i)),2)).ToString();
                        string prod_name = getSale01.ReturnProd_Name1(getSale01.ReturnProdID(i));
                        string Prod_id = getSale01.ReturnProdID(i);
                        //设置打印内容
                        listPrint.Add(prod_name.PadRight(10,' ')+ qty.ToString() + "      " + price.ToString());
                    }
                    //总金额实现
                }
                catch { }
            }

            // decimal totqunt = decimal.Round(GetSaleTmp00.InitGetSaleTmp00(shopid, saleid).SaleTmp00TotQuan(), 0);
            listPrint.Add("      ");
            listPrint.Add("数量合计:" + "      " + decimal.Round(totqunt, 0).ToString());
            listPrint.Add("合计:" + "      " + decimal.Round(totalPrice, 2).ToString());
            listPrint.Add("现金" + "       " + decimal.Round(btnInput, 2).ToString());
            listPrint.Add("找回" + "       " + decimal.Round((btnInput - totalPrice), 2).ToString());
            this.mainForm.AddExtraInfo(listPrint, this.mainForm.OperPara.TicketTail);



            LPTControl.Port = Info.printPort;
            //如果选择打印按钮则打印小票
            if (enablePrint || type.Equals("员工餐") || type.Equals("招待"))
            {
                //LPTControl.NewRow(2);
                foreach (string s in listPrint)
                {
                    LPTControl.WriteLine(s);
                }
                LPTControl.NewRow(1);
                LPTControl.CutPaper();

            }
            PoleDisplayer.Clear();
            PoleDisplayer.Change(this.txtGiveBack.Text);
            LPTControl.OpenBox();
            LPTControl.Close();
        }

        /// <summary>
        /// 用于重印上一次账单
        /// </summary>
        public void RepeatPrint()
        {
            try
            {
                if (listPrint.Count > 0)
                {
                    LPTControl.Port = Info.printPort;
                    LPTControl.NewRow(1);
                    LPTControl.WriteLine("----账单重印----");
                    foreach (string s in listPrint)
                    {
                        LPTControl.WriteLine(s);
                    }
                    LPTControl.NewRow(1);
                    LPTControl.OpenBox();
                    LPTControl.CutPaper();
                    LPTControl.Close();
                }
                else
                {
                    MessageBox.Show("抱歉，当前没有账单可用于重印！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch { }

        }
        /// <summary>
        /// 完成对saletemp02表的插入
        /// </summary>
        /// <param name="shop_id">分店编号</param>
        /// <param name="sale_id">销售单编号</param>
        /// <param name="sale_sno">付款方式序号</param>
        /// <param name="pay_id">付款编号</param>
        /// <param name="amount">现金金额</param>
        /// <param name="transfer_status">传输状态</param>
        /// <param name="last_update">最后更新</param>
        /// <param name="Face_Value">现金面额</param>
        /// <returns>bool变量</returns>
        public bool InsertSaleTemp02(string shop_id, string sale_id, int sale_sno, string pay_id, decimal amount, char transfer_status, DateTime last_update, decimal Face_Value)
        {
            return InsertSaleTmp02.InitInsertSaleTmp02().InsertDataSaleTmp02(shop_id, sale_id, sale_sno, pay_id, amount, transfer_status, last_update, Face_Value);
        }
        /// <summary>
        /// 完成对saletemp02表的指定row删除操作
        /// </summary>
        /// <param name="shop_id">分店号</param>
        /// <param name="sale_id">销售编号</param>
        /// <returns>bool变量</returns>
        public bool DeleteSaleTmp02(string shop_id, string sale_id)
        {
            return DeleteSaleTmp02Row.InitDeleteSaleTmp02Row().DeleteDataSaleTmp02(shop_id, sale_id);
        }
        /// <summary>
        /// 断电恢复相应结账状态供主界面调用（注：调用前恢复结账面板）
        /// </summary>
        /// <param name="shop_id">分店编号</param>
        /// <param name="sale_id">销售单号</param>
        public void RecoverCheckOut(string shop_id, string sale_id)
        {
            //对saletmp02表的恢复
            DeleteSaleTmp02(shop_id, sale_id);
            //对saletmp01表的恢复
            UpdateSaleTmp01.InitUpdateSaleTmp01().RecoverSaleTmp01(shop_id, sale_id);
            btnInputNumber = 0;
            btnmoneyclick = false;
            btnMoney = 0;
            SetPrice();
        }
        /// <summary>
        /// 结账类型，包括员工餐和招待
        /// </summary>
        /// <param name="type">结账类型（员工餐或者招待）</param>
        public void CheckOutType(string type)
        {
            checkOutType = type;
            btnClear.Enabled = false;
            btnClear.Visible = false;
            btnPrint.Enabled = false;
            btnPrint.Visible = false;
            btnInputNumber = Info.totalPrice;
            SetPrice();
            switch (type)
            {
                case "招待":
                    {
                        labstate.Text = "招待";
                        break;
                    }
                case "员工餐":
                    {
                        labstate.Text = "员工餐";
                        break;
                    }
                default:
                    { break; }
            }
        }
        /// <summary>
        /// 还原为正常 结账状态
        /// </summary>
        /// <param name="type">结账类型</param>
        private void RecoverType(string type)
        {
            if (type.Equals("招待") || type.Equals("员工餐"))
            {
                btnClear.Enabled = true;
                btnClear.Visible = true;
                btnPrint.Enabled = true;
                btnPrint.Visible = true;
            }
            else
            {
                //如果没有找零不用弹出提示界面
                if (btnInputNumber != Info.totalPrice)
                {
                    CheckForm checkForm = new CheckForm();
                    checkForm.StartPosition = FormStartPosition.CenterScreen;
                    checkForm.SetStatus(this.txtRequire.Text, this.txtPay.Text, this.txtGiveBack.Text);
                    checkForm.ShowDialog();                  
                }
                //关闭找零窗体后客显显示欢迎信息
                PoleDisplayer.Clear();
                PoleDisplayer.Display(Info.printmsg, PoleDisplayType.Dark);
            }
        }
        #endregion
        #region//私有方法
        /// <summary>
        /// 结账对saletmp02 和saletmp01 表的数据恢复
        /// </summary>
        private void RecoverDate()
        {
            if (0 < btnInputNumber - btnMoney)
            {
                //删除SeleTmp02中指定pay_id号的记录
                DeleteSaleTmp02(Info.shop_id, Info.sale_id);
            }
            btnInputNumber = 0;
            btnmoneyclick = false;
            btnMoney = 0;
        }
        /// <summary>
        /// 完成对结账确定按钮的对数据插入工作
        /// </summary>
        /// <param name="sale_id">销售单号</param>
        /// <param name="shop_id">分店号</param>
        /// <param name="btninputmumber">已付金额</param>
        /// <param name="totalPrice">应付金额</param>
        /// <param name="method_id">销售方法</param>
        /// <param name="btnMoneyclick">现金按钮是否选中</param>
        /// <param name="sale_Sno">销售单序号</param>
        /// <param name="indexPayment">处于第几条记录</param>
        /// <param name="datetime">当前时间</param>
        /// <param name="btnmoney">现金输入</param>
        private void BtnConfirm(string sale_id, string shop_id, decimal btninputmumber, decimal totalPrice, int method_id, bool btnMoneyclick, int sale_Sno, int indexPayment, DateTime datetime, decimal btnmoney)
        {
            string pay_id = "01";
            decimal Face_Value = 0;
            UInt16 methodid = 1;
            try
            {
                methodid = Convert.ToUInt16(method_id);
            }
            catch { methodid = 1; }
            decimal amount = -(btninputmumber - totalPrice);
            char trans = getPayment.ReturnTransferStatus(indexPayment);
            //对saletmp00表的最后更新
            InsertSaleAll.InitInsertSaleAll().UpdateSaleTmp00(sale_id, shop_id, (btninputmumber - totalPrice), methodid, totalPrice);
            if (0 > amount)//有找零
            {
                if (!btnMoneyclick)
                {
                    pay_id = getPayment.ReturnPay_ID(indexPayment);
                    Face_Value = getPayment.ReturnFace_Value(indexPayment);
                    this.InsertSaleTemp02(shop_id, sale_id, sale_Sno, pay_id, amount, trans, datetime, Face_Value);
                }
                else
                {
                    pay_id = getPayment.ReturnPay_ID(indexPayment);
                    Face_Value = getPayment.ReturnFace_Value(indexPayment);
                    this.InsertSaleTemp02(shop_id, sale_id, sale_sno, pay_id, btnmoney, trans, datetime, Face_Value);
                    this.InsertSaleTemp02(shop_id, sale_id, sale_Sno+1, pay_id, amount, trans, datetime, Face_Value);
                }
            }
            else
            {
                if (btnMoneyclick)
                {
                    amount = btnmoney;
                    pay_id = getPayment.ReturnPay_ID(indexPayment);
                    Face_Value = getPayment.ReturnFace_Value(indexPayment);//index 为现金按钮的面值记录
                    this.InsertSaleTemp02(shop_id, sale_id, sale_Sno, pay_id, amount, trans, datetime, Face_Value);
                }
                else
                {
                    return;
                }
            }
            btnmoneyclick = false;
        }
        /// <summary>
        /// 完成对SaleTmp表对应的Sale表的插入
        /// </summary>
        /// <returns>bool</returns>
        private bool InsertSallData(string sale_Id, string shop_Id, string types)
        {
            try
            {

                try
                {
                    InsertSaleAll.InitInsertSaleAll().InSertSale00Data(sale_Id, shop_Id);
                    InsertSaleAll.InitInsertSaleAll().InSertSale01Data(sale_Id, shop_Id);
                }
                catch { }
                try
                {
                    if (types.Equals("结账"))
                    {
                        InsertSaleAll.InitInsertSaleAll().InSertSale02Data(sale_Id, shop_Id);
                    }
                }
                catch { }
                //try
                //{
                //    InsertSaleAll.InitInsertSaleAll().InSertSale03Data(sale_Id, shop_Id);
                //}
                //catch { }
                try
                {

                    //调用向后台传输数据的方法
                    //读取配置文件中传输间隔单数
                    int i;
                    try
                    {
                        i = Convert.ToInt32(this.mainForm.OperPara.GetIniConfig("nudIntervalBill"));
                    }
                    catch { i = 10; }
                }
                catch { }

                return true;
            }
            catch { return false; }
        }
        /// <summary>
        /// 用于设置付账金额的状态
        /// </summary>
        /// <param name="textname">面板名字</param>
        private void CheckStatus(string textname)
        {
            if (!textname.Equals(getPayment.ReturnPayMoneyName("现金")) && !textname.Equals("兑换券"))
            {
                for (int i = 0; i < checkOutMoney.Length; i++)
                {
                    if (checkOutMoney[i].name.Equals(textname))
                    {
                        checkOutMoney[i].count += 1;
                        break;
                    }
                }
            }
            SetLabState(checkOutMoney);
        }
        /// <summary>
        /// 设置labStatus显示内容
        /// </summary>
        /// <param name="checkoutmoney">自定义结构体变量</param>
        private void SetLabState(status[] checkoutmoney)
        {
            labstate.Text = "";
            for (int i = 0; i < checkoutmoney.Length; i++)
            {
                if (checkoutmoney[i].name.Equals(getPayment.ReturnPayMoneyName("现金")) && btnMoney > 0)
                {
                    labstate.Text += "现金" + ":   " + btnMoney.ToString() + "\n";
                }
                else
                {
                    if (checkoutmoney[i].count != 0)
                    {
                        labstate.Text += checkoutmoney[i].name + "×" + checkoutmoney[i].count + ":" + checkoutmoney[i].name + "\n";
                    }
                }
            }
        }
        /// <summary>
        /// 对checkOutMoney数组还原状态
        /// </summary>
        private void ClearcheckOutMoney()
        {
            for (int i = 0; i < checkOutMoney.Length; i++)
            {
                checkOutMoney[i].count = 0;
            }
        }
        /// <summary>
        /// 用于结账为员工餐和招待相应表的更新工作
        /// </summary>
        /// <param name="type">结账类型</param>
        /// <param name="saleid">销售单号</param>
        /// <param name="shopid">分店编号</param>
        /// <param name="empid">收银员号</param>
        /// <returns>bool</returns>
        private bool TypeUpdateTable(string type, string saleid, string shopid, string empid)
        {
            try
            {
                InsertSaleAll.InitInsertSaleAll().UpdateSaleTmp00(saleid, shopid, type);
                InsertSaleAll.InitInsertSaleAll().UpdateSaleTmp01(saleid, shopid, empid, type);
                return true;
            }
            catch { return false; }
        }
        #endregion
        #region 面板事件
        /// <summary>
        /// 面板的处理点击事件
        /// </summary>
        /// <param name="ckPanel">当前对象</param>
        /// <param name="btnText">button显示的text文本</param>
        private void ckPanel1_SetInfo(CKPanel ckPanel, string btnText, string payId, decimal faceValue)
        {
            int j;
            if (Decimal.Round(Convert.ToDecimal(this.txtPay.Text), 2) - Info.totalPrice < 0)
            {
                for (j = 0; j < getPayment.ReturnRecordCount; j++)
                {
                    if (getPayment.ReturnDispName(j).Equals(btnText))
                    {
                        if (getPayment.ReturnPayName(payId).Equals("现金"))
                        {
                            btnmoneyclick = true;
                            index = j;
                            btnMoney += decimal.Round(Info.inputNumber, dataAccur);

                            btnInputNumber += decimal.Round(Info.inputNumber, dataAccur);
                            Info.inputNumber = 0;
                            this.mainForm.Number.TextBoxText = "";
                            this.mainForm.OperPara.WriteIniInfo("inputNumber", "0");
                            dataAccur = Convert.ToInt32(getPayment.ReturnDataAccur(j));
                            if (0 == dataAccur)
                            {
                                dataAccur = 1;
                            }
                            SetPrice();
                            CheckStatus(btnText);
                            this.mainForm.Number.ClearNum();

                            break;
                        }

                        string pay_id = payId;
                        decimal Face_Value = faceValue;
                        btnInputNumber += Face_Value;
                        this.txtPay.Text = btnInputNumber.ToString();
                        decimal amount = Face_Value;
                        char transferStatus = getPayment.ReturnTransferStatus(j);
                        SetPrice();
                        CheckStatus(btnText);
                        this.InsertSaleTemp02(Info.shop_id, Info.sale_id, sale_sno, pay_id, amount, transferStatus, dateTime, Face_Value);
                        sale_sno += 1;




                    }

                }
            }
            else
            {
                MessageBox.Show("抱歉！已付金额已足够！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 对CkPanel的初始化
        /// </summary>
        /// <param name="ckPanel">this</param>
        /// <param name="isLoad">bool变量</param>
        private void ckPanel1_GetInfo(CKPanel ckPanel, bool isLoad)
        {
            //设置行数和列数
            int recordCount = getPayment.ReturnRecordCount;
            this.ckPanel1.ColumnRow = new Size(4, 4);
            //设置按钮是数量
            this.ckPanel1.TotalBtn = recordCount;
            string[] showString = new string[recordCount];
            Color[] btnColor = new Color[recordCount];
            Font[] txtFont = new Font[recordCount];
            Color[] fontColor = new Color[recordCount];
            string[] pay_id = new string[recordCount];
            decimal[] faceValue = new decimal[recordCount];
            //分配内存空间
            checkOutMoney = new status[recordCount];

            for (int i = 0; i < recordCount; i++)
            {
                showString[i] = getPayment.ReturnDispName(i);
                checkOutMoney[i].name = showString[i];
                checkOutMoney[i].count = 0;
                btnColor[i] = getPayment.ReturnBtnColor(i);
                txtFont[i] = getPayment.ReturnFont(i);
                fontColor[i] = getPayment.ReturnFondColor(i);
                pay_id[i] = getPayment.ReturnPay_Id(i);
                faceValue[i] = getPayment.ReturnFace_Value(i);
            }
            //设置按钮上显示的文字
            this.ckPanel1.StringArray = showString;
            //设置按钮上的背景颜色
            this.ckPanel1.AllBtnColor = btnColor;
            //设置按钮上的字体
            this.ckPanel1.AllBtnFont = txtFont;
            //设置字体颜色
            this.ckPanel1.AllBtnFontColor = fontColor;
            //设置付款类型
            this.ckPanel1.PayId = pay_id;
            //面值
            this.ckPanel1.FaceValue = faceValue;

        }
        #endregion
        #region 嵌套类
        /// <summary>
        /// 用于线程调用
        /// </summary>
        class OperThread
        {
            /// <summary>
            /// 结账控件
            /// </summary>
            private CheckOut check;
            /// <summary>
            ///保存销售单号
            /// </summary>
            string saleid;
            /// <summary>
            /// 分店号
            /// </summary>
            string shopid;
            /// <summary>
            /// 已付现金
            /// </summary>
            decimal inputmumber;
            /// <summary>
            /// 应付金额
            /// </summary>
            decimal tot;
            /// <summary>
            /// 销售方法
            /// </summary>
            int salemethord;
            /// <summary>
            /// 现金按钮是否可用
            /// </summary>
            bool moneyclick;
            /// <summary>
            /// 销售单号
            /// </summary>
            int salesno;
            /// <summary>
            /// 记录
            /// </summary>
            int indexo;
            /// <summary>
            /// 当前时间
            /// </summary>
            DateTime time;
            /// <summary>
            /// 手动现金输入
            /// </summary>
            decimal money;
            /// <summary>
            /// 用于保存结账类型
            /// </summary>
            string types;
            /// <summary>
            /// 用于保存pos机编号
            /// </summary>
            string posid;
            /// <summary>
            /// 用于保存收银员编号
            /// </summary>
            string empid;
            /// <summary>
            /// 构造函数
            /// </summary>
            /// <param name="checkOut">结账控件</param>
            public OperThread(CheckOut checkOut)
            {
                saleid = Info.sale_id;
                shopid = Info.shop_id;
                inputmumber = checkOut.btnInputNumber;
                tot = Info.totalPrice;
                salemethord = Info.saleMethord;
                moneyclick = checkOut.btnmoneyclick;
                salesno = checkOut.sale_sno;
                indexo = checkOut.index;
                time = checkOut.dateTime;
                money = checkOut.btnMoney;
                check = checkOut;
                types = check.checkOutType;
                empid = Info.emp_id;
                posid = Info.pos_id;
            }
            /// <summary>
            /// 线程调用方法
            /// </summary>
            public void method()
            {
                check.InsertSaleThread(saleid, shopid, inputmumber, tot, salemethord, moneyclick, salesno, indexo, time, money, posid, empid, types);
            }
        }
        #endregion

    }

}
