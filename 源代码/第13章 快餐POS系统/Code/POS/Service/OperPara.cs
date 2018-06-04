using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using POS.Common;
using System.Drawing;
// 操作Ini文件的类
/*主要公共方法：
（1）、WriteIniConfig(string key, string value)，向config.ini中写入信息，key：ini文件中的标签，value：标签对应的值
（2）、GetIniConfig(string key)，读取config.ini信息，key：ini文件中的标签
（3）、WriteIniInfo(string key, string value)，向info.ini中写入信息，key：ini文件中的标签，value：标签对应的值
（4）、GetIniInfo(string key)，读取info.ini信息，key：ini文件中的标签
（5）、RecoverInfo()，非正常下线后再次进入系统后根据配置文件中的信息恢复Ini中的信息
（6）、ClearInfo()，清空程序中Info类中的信息
（7）、ClearIni()，清空ini中的信息
*/
namespace POS.Service
{
    /// <summary>
    /// 操作Ini文件的类
    /// </summary>
    public partial class OperPara : Component
    {
        /// <summary>
        /// 读写Ini文件的类
        /// </summary>
        private ReadIni readIniConfig = new ReadIni("config.ini");
        private ReadIni readIniInfo = new ReadIni("Info.ini");
        /// <summary>
        /// 构造函数1
        /// </summary>
        public OperPara()
        {
            InitializeComponent();

        }

        /// <summary>
        /// 构造函数2
        /// </summary>
        /// <param name="container">container</param>
        public OperPara(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        #region//字段.属性

        #region//外观

        private Font rollFont;
        /// <summary>
        /// 滚动字体
        /// </summary>
        public Font RollFont
        {
            get
            {
                //  return new Font(readIniInfo.ReadString("RepastErp", "cmbfonttype"), 15.75F,FontStyle.Bold);
                return rollFont;

            }
            set { rollFont = value; }
        }
        /// <summary>
        /// 获得字体
        /// </summary>
        public string RoolFontName
        {
            get {

                return readIniConfig.ReadString("RepastErp", "cmbfonttype"); ; 
            }
        }
        /// <summary>
        /// 滚动字体的前景颜色
        /// </summary>
        public Color RollForeColor
        {
            get
            {
                Color rollForeColor;
                try
                {
                    string color = readIniConfig.ReadString("RepastErp", "btnFontcolor");

                    if (color != "")
                    {
                        rollForeColor = ColorTranslator.FromHtml(color);
                    }
                    else
                    {
                        rollForeColor = Color.Yellow;
                    }
                }
                catch { rollForeColor = Color.Yellow; }
                return rollForeColor;
            }

        }
        /// <summary>
        /// 滚动控件的背景颜色
        /// </summary>
        public Color RollBackColor
        {
            get
            {
                Color rollBackColor;
                try
                {
                    string color = readIniConfig.ReadString("RepastErp", "btnBackcolor");
                    if (color != "")
                    {
                        rollBackColor = ColorTranslator.FromHtml(color);
                    }
                    else
                    {
                        rollBackColor = System.Drawing.SystemColors.ActiveCaption;
                    }
                }

                catch { rollBackColor = System.Drawing.SystemColors.ActiveCaption; }
                return rollBackColor;
            }
        }


        /// <summary>
        /// 滚动字体
        /// </summary>
        public string RollText
        {
            get
            {
                try
                {
                    return readIniConfig.ReadString("RepastErp", "txtcontent");
                }
                catch { return ""; }
            }

        }

        /// <summary>
        /// 滚动控件的背景图片的路径
        /// </summary>
        public Image RollBgImage
        {
            get
            {
                try
                {
                    string str = readIniConfig.ReadString("RepastErp", "txtBackpicture");
                    if (str != "")
                    {
                        return Image.FromFile(str);
                    }
                    else
                    {
                        return null;
                    }
                }
                catch { return null; }
            }

        }



        /// <summary>
        /// ShowInfo控件上文字的字体
        /// </summary>
        public Font ShowInfoFont
        {
            get { return null; }

        }

        /// <summary>
        /// ShowInfo控件上文字的前景颜色
        /// </summary>
        public Color ShowInfoForeColor
        {

            get
            {
                Color showInfoForeColor;
                try
                {

                    string color = readIniConfig.ReadString("RepastErp", "btnInfoFontcolor");
                    if (color != "")
                    {
                        showInfoForeColor = ColorTranslator.FromHtml(color);
                    }
                    else
                    {
                        showInfoForeColor = Color.Yellow;
                    }

                }
                catch { showInfoForeColor = Color.Yellow; }
                return showInfoForeColor;
            }

        }

        /// <summary>
        /// ShowInfo控件的背景颜色
        /// </summary>
        public Color ShowInfoBgColor
        {
            get
            {
                Color showInfoBgColor;
                try
                {
                    string color = readIniConfig.ReadString("RepastErp", "btnInfoBackcolor");
                    if (color != "")
                    {
                        showInfoBgColor = ColorTranslator.FromHtml(color);
                    }
                    else
                    {
                        showInfoBgColor = System.Drawing.SystemColors.ActiveCaption;
                    }

                }
                catch { showInfoBgColor = System.Drawing.SystemColors.ActiveCaption; }
                return showInfoBgColor;
            }

        }

        /// <summary>
        /// ShowInfo控件的背景图片路径
        /// </summary>
        public Image ShowInfoBgImage
        {
            get
            {
                Image showInfoBgImage;
                try
                {
                    showInfoBgImage = Image.FromFile(readIniConfig.ReadString("RepastErp", "txtInfoBackpicture"));
                }
                catch { return null; }
                return showInfoBgImage;
            }

        }


        /// <summary>
        /// 点单控件下按钮的图片路径
        /// </summary>
        public Image DownPath
        {
            get
            {
                Image downPath;
                try
                {
                    downPath = Image.FromFile(readIniConfig.ReadString("RepastErp", "txtPicxia"));
                }
                catch { return null; }
                return downPath;
            }

        }

        /// <summary>
        /// 点单控件加按钮的图片路径
        /// </summary>
        public Image AddPath
        {
            get
            {
                Image addPath;
                try
                {
                    addPath = Image.FromFile(readIniConfig.ReadString("RepastErp", "txtPicjia"));
                }
                catch { return null; }
                return addPath;
            }

        }

        /// <summary>
        /// 点单控件减按钮的图片路径
        /// </summary>
        public Image MinusePath
        {
            get
            {
                Image minusePath;
                try
                {
                    minusePath = Image.FromFile(readIniConfig.ReadString("RepastErp", "txtPicjian"));
                }
                catch { return null; }
                return minusePath;
            }

        }

        /// <summary>
        /// 点单控件上按钮的图片路径
        /// </summary>
        public Image UpPath
        {
            get
            {
                Image upPath;
                try
                {
                    upPath = Image.FromFile(readIniConfig.ReadString("RepastErp", "txtPicshang"));
                }
                catch { return null; }
                return upPath;
            }

        }

        /// <summary>
        /// 点单控件备餐按钮的图片路径
        /// </summary>
        public Image BeicanPath
        {
            get
            {
                Image beicanPath;
                try
                {
                    beicanPath = Image.FromFile(readIniConfig.ReadString("RepastErp", "txtPicbeican"));
                }
                catch { return null; }
                return beicanPath;
            }

        }

        /// <summary>
        /// 功能面板控件左按钮的图片路径
        /// </summary>
        public Image LeftPic
        {
            get
            {
                Image leftPic;
                try
                {
                    leftPic = Image.FromFile(readIniConfig.ReadString("RepastErp", "txtFunSecLeft"));
                }
                catch { return null; }
                return leftPic;
            }

        }

        /// <summary>
        /// 功能面板控件右按钮的图片路径
        /// </summary>
        public Image RightPic
        {
            get
            {
                Image rightPic;
                try
                {
                    rightPic = Image.FromFile(readIniConfig.ReadString("RepastErp", "txtFunSecRight"));
                }
                catch { return null; }
                return rightPic;
            }

        }

        /// <summary>
        /// 功能面板控件按钮的行数
        /// </summary>
        public int FunctRow
        {
            get
            {
                int functRow;
                try
                {
                    functRow = Convert.ToInt32(readIniConfig.ReadString("RepastErp", "txtFunSecRows"));
                }
                catch
                {
                    return 5;
                }
                return functRow;
            }

        }

        /// <summary>
        /// 功能面板控件按钮的列数
        /// </summary>
        public int FunctColumn
        {
            get
            {
                int functColumn;
                try
                {
                    functColumn = Convert.ToInt32(readIniConfig.ReadString("RepastErp", "txtFunSecColums"));
                }
                catch
                {
                    return 4;
                }
                return functColumn;
            }

        }


        /// <summary>
        /// 商品类别面板控件按钮的行数
        /// </summary>
        public int ProdKindRow
        {
            get
            {
                int prodKindRow;
                try
                {
                    prodKindRow = Convert.ToInt32(readIniConfig.ReadString("RepastErp", "txtFunRows"));
                }
                catch { return 2; }
                return prodKindRow;
            }

        }

        /// <summary>
        /// 商品类别面板控件按钮的列数
        /// </summary>
        public int ProdKindColumn
        {
            get
            {
                int prodKindColumn;
                try
                {
                    prodKindColumn = Convert.ToInt32(readIniConfig.ReadString("RepastErp", "txtFunColums"));
                }
                catch { return 7; }
                return prodKindColumn;
            }

        }

        /// <summary>
        /// 商品面板控件按钮的行数
        /// </summary>
        public int ProdRow
        {
            get
            {
                int prodRow;
                try
                {
                    prodRow = Convert.ToInt32(readIniConfig.ReadString("RepastErp", "txtGoodsRows"));
                }
                catch { return 9; }
                return prodRow;
            }

        }

        /// <summary>
        /// 商品面板控件按钮的列数
        /// </summary>
        public int ProdColumn
        {
            get
            {
                int prodColumn;
                try
                {
                    prodColumn = Convert.ToInt32(readIniConfig.ReadString("RepastErp", "txtGoodsColums"));
                }
                catch { return 7; }
                return prodColumn;
            }

        }

        #endregion

        #region//基本设定

        #region //小票

        /// <summary>
        /// 小票首行
        /// </summary>
        public string TicketHead
        {
            get
            {
                return readIniConfig.ReadString("RepastErp", "txtBillfirstline");
            }

        }

        /// <summary>
        /// 小票尾行
        /// </summary>
        public string TicketTail
        {
            get 
            { 
                return readIniConfig.ReadString("RepastErp", "txtBilllastline"); 
            }
        }

        #endregion

        #region//服务器信息


        /// <summary>
        /// 服务器IP
        /// </summary>
        public string ServerIP
        {
            get { return readIniConfig.ReadString("RepastErp", "txtServerIP"); }

        }

        /// <summary>
        /// 服务器数据库端口
        /// </summary>
        public string ServerPort
        {
            get { return readIniConfig.ReadString("RepastErp", "txtPort"); }
        }

        /// <summary>
        /// 服务器数据库实例名
        /// </summary>
        public string ServerDBCase
        {
            get { return readIniConfig.ReadString("RepastErp", "txtExampleName"); }
        }

        /// <summary>
        /// 服务器数据库名字
        /// </summary>
        public string ServerDBName
        {
            get { return readIniConfig.ReadString("RepastErp", "txtIPdataname"); }
        }

        /// <summary>
        /// 服务器数据库用户名
        /// </summary>
        public string ServerUserName
        {
            get { return readIniConfig.ReadString("RepastErp", "txtFTPuser"); }

        }

        /// <summary>
        /// 服务器数据库密码
        /// </summary>
        public string ServerPassword
        {
            get { return readIniConfig.ReadString("RepastErp", "txtFTPpassword"); }

        }

        /// <summary>
        ///上传间隔单数
        /// </summary>
        public string UpGapNum
        {
            get { return readIniConfig.ReadString("RepastErp", "nudIntervalBill"); }

        }

        /// <summary>
        /// 上传间隔时间
        /// </summary>
        public string UpGapTime
        {
            get { return readIniConfig.ReadString("RepastErp", "nudIntervalTime"); }

        }
        #endregion

        #region//本地机器信息




        /// <summary>
        /// 本地ip地址
        /// </summary>
        public string LocalIp
        {
            get { return readIniConfig.ReadString("RepastErp", "txtDatabaseIP"); }

        }

        /// <summary>
        /// 本地数据库端口
        /// </summary>
        public string LocalPort
        {
            get { return readIniConfig.ReadString("RepastErp", "txtPort2"); }

        }


        /// <summary>
        /// 本地数据库实例名
        /// </summary>
        public string LocalCase
        {
            get { return readIniConfig.ReadString("RepastErp", "txtExampleName2"); }

        }


        /// <summary>
        /// 本地数据库名字
        /// </summary>
        public string LocalDBName
        {
            get { return readIniConfig.ReadString("RepastErp", "txtDatabaseName"); }

        }


        /// <summary>
        /// 本地数据库用户名字
        /// </summary>
        public string LocalUserName
        {
            get { return readIniConfig.ReadString("RepastErp", "txtDatabaseUser"); }

        }


        /// <summary>
        /// 本地数据库密码
        /// </summary>
        public string LocalPassword
        {
            get { return readIniConfig.ReadString("RepastErp", "txtDatabasePassword"); }
        }

        #endregion



        #endregion


        /// <summary>
        /// POS机编号
        /// </summary>
        public string PosId
        {
            get
            {
                try
                {
                    return readIniConfig.ReadString("RepastErp", "txtPosid");
                }
                catch
                {
                    return "";
                }
            }

        }

        #endregion

        #region//公共方法

        #region//操作ini的方法
        /// <summary>
        /// 向config.ini中写入信息
        /// </summary>
        /// <param name="key">关键字</param>
        /// <param name="value">值</param>
        /// <returns>成功写入返回true</returns>
        public bool WriteIniConfig(string key, string value)
        {
            try
            {
                readIniConfig.WriteString("RepastErp", key, value);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 读取config.ini信息
        /// </summary>
        /// <param name="key">关键字</param>
        /// <returns>关键字对应的值（读取失败返回空字符串）</returns>
        public string GetIniConfig(string key)
        {
            try
            {
                return readIniConfig.ReadString("RepastErp", key);

            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// 向info.ini中写入信息
        /// </summary>
        /// <param name="key">关键字</param>
        /// <param name="value">值</param>
        /// <returns>成功写入返回true</returns>
        public bool WriteIniInfo(string key, string value)
        {
            try
            {
                readIniInfo.WriteString("RepastErp", key, value);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 读取info.ini信息
        /// </summary>
        /// <param name="key">关键字</param>
        /// <returns>关键字对应的值（读取失败返回空字符串）</returns>
        public string GetIniInfo(string key)
        {
            try
            {
                return readIniInfo.ReadString("RepastErp", key);

            }
            catch
            {
                return "";
            }
        }

        #endregion

        /// <summary>
        /// 非正常下线后再次进入系统后根据配置文件中的信息恢复Ini中的信息
        /// </summary>
        /// <returns>若Info中有一项恢复失败即返回false</returns>
        public bool RecoverInfo()
        {
            try
            {
                //分店编号
                Info.shop_id = GetIniInfo("shop_id");

                //分店名字
                Info.shop_name = GetIniInfo("shop_name");

                //pos机编号
                Info.pos_id = GetIniConfig("txtPosid");
                //客显信息
                Info.printmsg = GetIniConfig("printmsg");
                //员工编号
                Info.emp_id = GetIniInfo("emp_id");

                //员工姓名
                Info.emp_name = GetIniInfo("emp_name");

                //班别名字
                //Info.shift_Name = GetIniInfo("shift_Name");

                //班别编号
                Info.shift_num = GetIniConfig("workNumber");

                //上线时间
                Info.login_date = Convert.ToDateTime(GetIniInfo("login_date"));

                //销售单号
                Info.sale_id = GetIniInfo("sale_id");

                //交易号

                Info.deal_number = Convert.ToInt32(GetIniInfo("deal_number"));


                //销售单序号 
                Info.sale_sno = Convert.ToInt32(GetIniInfo("sale_sno"));

                //选择的商品一共需要的总价格
                Info.totalPrice = Convert.ToDecimal(GetIniInfo("totalPrice"));

                //判断是否处于结账状态
                Info.isCheckOut = Convert.ToBoolean(GetIniInfo("isCheckOut"));

                //登录窗体是否退出
                Info.isexit = Convert.ToBoolean(GetIniInfo("isexit"));

                //零用钱
                Info.cashier_sum = Convert.ToDecimal(GetIniInfo("cashier_sum"));

                //钱箱中钱数
                Info.remain_sum = Convert.ToDecimal(GetIniInfo("remain_sum"));

                //结账时是否处于使用兑换券，使用则为true
                Info.isCheckChit = Convert.ToBoolean(GetIniInfo("isCheckChit"));


                //销售方式（1内用、2外带、3外送，4生日聚会等）
                Info.saleMethord = Convert.ToInt32(GetIniInfo("saleMethord"));

                //抽取大钞数量
                Info.largeBillsNum = Convert.ToDecimal(GetIniInfo("largeBillsNum"));

                //是否打折（true表示是）
                Info.isDistinct = Convert.ToBoolean(GetIniInfo("isDistinct"));

                //是否折让（true表示是）
                Info.isAllowance = Convert.ToBoolean(GetIniInfo("isAllowance"));

                //是否可以改餐
                Info.canAlterDinner = Convert.ToBoolean(GetIniInfo("canAlterDinner"));

                //抽大钞编号
                Info.money_out_id = GetIniInfo("money_out_id");


                return true;
            }
            catch
            {
                ClearInfo();
                ClearIni();
                return false;
            }


        }

        /// <summary>
        /// 清空程序中Info类中的信息
        /// </summary>
        /// <returns></returns>
        public void ClearInfo()
        {

            //分店编号
            Info.shop_id = "";

            //分店名字
            Info.shop_name = "";

            //pos机编号
            Info.pos_id = "";

            //员工编号
            Info.emp_id = "";

            //员工姓名
            Info.emp_name = "";

            //班别名字
            //Info.shift_Name = "";

            //班别编号
            Info.shift_num = "1";

            //上线时间
            Info.login_date = DateTime.Now;

            //销售单号
            Info.sale_id = "";

            //交易号
            Info.deal_number = 1;

            //销售单序号 
            Info.sale_sno = 1;

            //选择的商品一共需要的总价格
            Info.totalPrice = 0;

            //在Number控件中输入的输入
            Info.inputNumber = 0;

            //判断是否处于结账状态
            Info.isCheckOut = false;

            //点单控件中被选中的组合餐号group_prod
            Info.selectedGroupProd = "";

            // 选中的商品是否可以对其进行组合餐设定
            Info.canResetComb = false;

            Info.prod_id = "";

            //点单控件中被选中的组商品的数量
            Info.selectedGroupProd_qty = 0;

            //单个商品的价格
            Info.sale_price = 0;

            //登录窗体是否退出
            Info.isexit = false;

            //零用钱
            Info.cashier_sum = 0;

            //结账时是否处于使用兑换券，使用则为true
            Info.isCheckChit = false;

            //是否为提单状态,提单状态时为true否则为false
            Info.IsTiDan = false;

            //销售方式（1内用、2外带、3外送，4生日聚会等）
            Info.saleMethord = 1;

            //抽取大钞数量
            Info.largeBillsNum = 0;

            //是否打折（true表示是）
            Info.isDistinct = false;

            //是否折让（true表示是）
            Info.isAllowance = false;

            //是否可以改餐
            Info.canAlterDinner = true;

            //抽大钞编号
            Info.money_out_id = "";

        }

        /// <summary>
        /// 清空ini中的信息
        /// </summary>
        /// <returns></returns>
        public void ClearIni()
        {
            //分店编号
            WriteIniInfo("shop_id", "");

            //分店名字
            WriteIniInfo("shop_name", "");

            //pos机编号
            WriteIniInfo("pos_id", "");

            //员工编号
            WriteIniInfo("emp_id", "");

            //员工姓名
            WriteIniInfo("emp_name", "");

            //班别名字
            WriteIniInfo("shift_Name", "");

            //班别编号
            WriteIniInfo("shift_num", "");

            //上线时间
            WriteIniInfo("login_date", DateTime.Now.ToString());

            //销售单号
            WriteIniInfo("sale_id", "");

            //交易号
            WriteIniInfo("deal_number", "0");

            //销售单序号 
            WriteIniInfo("sale_sno", "1");

            //选择的商品一共需要的总价格
            WriteIniInfo("totalPrice", "0");

            //判断是否处于结账状态
            WriteIniInfo("isCheckOut", "false");

            //登录窗体是否退出
            WriteIniInfo("isexit", "false");

            //零用钱
            WriteIniInfo("cashier_sum", "0");

            //结账时是否处于使用兑换券，使用则为true
            WriteIniInfo("isCheckChit", "false");

            //销售方式（1内用、2外带、3外送，4生日聚会等）
            WriteIniInfo("saleMethord", "1");

            //抽取大钞数量
            WriteIniInfo("largeBillsNum", " 0");

            //是否打折（true表示是）
            WriteIniInfo("isDistinct", "false");

            //是否折让（true表示是）
            WriteIniInfo("isAllowance", "false");

            //是否可以改餐
            WriteIniInfo("canAlterDinner", "true");

            //抽大钞编号
            WriteIniInfo("money_out_id", "");

        }

        #endregion







    }
}
