using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using POS.View;
using POS.Service;
////包含完成对数据库的所有基本操作：sql语句、存储过程等。程序配置信息 、操作Ini文件的类
namespace POS.Common
{
    /// <summary>
    /// 程序配置信息
    /// </summary>
    class Info
    {
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public static string Constr;

        /// <summary>
        /// 天气日期
        /// </summary>
        public static string   w_date;

        /// <summary>
        /// 天气状况
        /// </summary>
        public static string weather;

        /// <summary>
        /// 最低气温
        /// </summary>
        public static int    low_temper;

        /// <summary>
        /// 最高气温
        /// </summary>
        public static int    hight_temper;

        /// <summary>
        /// 密码
        /// </summary>
        public static String password;

        /// <summary>
        /// 分店编号
        /// </summary>
        public static string shop_id = "";

        /// <summary>
        /// 分店名字
        /// </summary>
        public static string shop_name = "";

        /// <summary>
        /// pos机编号
        /// </summary>
        public static string pos_id = "";
        /// <summary>
        /// 客显欢迎信息
        /// </summary>
        public static string printmsg = "欢迎光临！";

        /// <summary>
        /// 员工编号
        /// </summary>
        public static string emp_id = "";

        /// <summary>
        /// 员工姓名
        /// </summary>
        public static string emp_name = "";

        /// <summary>
        /// 员工级别
        /// </summary>
        public static string emp_level = "";

        /// <summary>
        /// 班次
        /// </summary>
        public static string   shift_num="1";

        /// <summary>
        /// 上线时间
        /// </summary>
        public static DateTime login_date;

        /// <summary>
        /// 下线时间
        /// </summary>
        public static DateTime exit_date;

        /// <summary>
        /// 销售单号
        /// </summary>
        public static string sale_id;

        /// <summary>
        /// 交易号
        /// </summary>
        public static int deal_number = 1;


        /// <summary>
        /// 销售单序号 
        /// </summary>
        public static int sale_sno = 1;

        /// <summary>
        /// 选择的商品一共需要的总价格
        /// </summary>
        public static decimal totalPrice;

        /// <summary>
        /// 在Number控件中输入的输入
        /// </summary>
        public static decimal inputNumber;

        /// <summary>
        /// 判断是否处于结账状态
        /// </summary>
        public static bool isCheckOut = false;

        /// <summary>
        /// 点单控件中被选中的group_prod（若是一个组合餐的group_prod字段的值，若是单餐则是单餐的id）
        /// </summary>
        public static string selectedGroupProd = "";


        /// <summary>
        /// 选中的商品是否可以对其进行组合餐设定
        /// </summary>
        public static bool  canResetComb =false;

        /// <summary>
        /// 如是是单餐则是单餐id，若是组合餐则是comb_id
        /// </summary>
        public static string prod_id = "";

        /// <summary>
        /// 点单控件中被选中的组商品的数量
        /// </summary>
        public static decimal selectedGroupProd_qty = 0;

        /// <summary>
        /// 单个商品的价格
        /// </summary>
        public static decimal sale_price;


        /// <summary>
        /// 登录窗体是否退出
        /// </summary>
        public static bool isexit = false;

        /// <summary>
        /// 零用钱
        /// </summary>
        public static decimal cashier_sum = 0;

        /// <summary>
        /// 钱箱中剩余钱数
        /// </summary>
        public static decimal remain_sum = 0;
       
        /// <summary>
        /// 商品面板调用的服务层类（供商品面板加载信息用）
        /// </summary>
        public static GetProduct0 getProduct;


        /// <summary>
        /// 点餐时插入临时表时调用的类
        /// </summary>
        public static InsertSingleProd insertSingleProd;

        /// <summary>
        /// 结账时是否处于使用兑换券，使用则为true
        /// </summary>
        public static bool isCheckChit = false;


        /// <summary>
        /// 是否为提单状态,提单状态时为true否则为false
        /// </summary>
        public static bool IsTiDan=false;

        /// <summary>
        /// 销售方式（1内用、2外带、3外送，4生日餐会等）
        /// </summary>
        public static int saleMethord=1;
        /// <summary>
        /// 一个班次抽取大钞总额
        /// </summary>
        public static decimal  largeBillsNum;

        /// <summary>
        /// 是否打折（true表示是）
        /// </summary>
        public static bool isDistinct = false;
        /// <summary>
        /// 是否折让（true表示是）
        /// </summary>
        public static bool isAllowance = false;

        /// <summary>
        /// 在修改商品的数量或者对商品打折或者折让时这个字段为true，增加新商品时这个字段为false
        /// </summary>
        public static bool isSelectPre = false;

        /// <summary>
        /// 是否可以改餐
        /// </summary>
        public static bool canAlterDinner = true;

        /// <summary>
        /// 是否关闭进度条
        /// </summary>
        public static bool isCloseProgress = false;
        /// <summary>
        /// 抽大钞编号
        /// </summary>
        public static string money_out_id = "";

        /// <summary>
        /// 打印机端口
        /// </summary>
        public static string printPort="";
        /// <summary>
        /// 客显端口
        /// </summary>
        public static string  cusShowPort="";
        /// <summary>
        /// 显示端口
        /// </summary>
        public static string  showPort="";
        /// <summary>
        /// IC卡端口
        /// </summary>
        public static string ICPort = "";
        /// <summary>
        /// 是否需要从数据库中获取最大的交易号
        /// </summary>
        public static bool isGetMaxDealnum = false;
       
        /// <summary>
        /// 功能转换时改变Info类中相应字段的值
        /// </summary>
        /// <param name="function">将要结束的功能</param>
        /// <param name="para">功能转换传递过来的参数</param>
        public static void StatusEnd(string function,string para)
        {
            //对配置文件Info.ini进行操作的对象
            ReadIni readIni = new ReadIni("Info.ini");
            
            //要求对Info里字段的更改都同时反映到配置文件Info.ini中，当断点重启时根据Info.ini中的信息再恢复本类中的信息

            switch (function)
            {
                case "结账取消":
                    Info.inputNumber = 0;
                    readIni.WriteString("RepastErp", "inputNumber", "0");
                    Info.isCheckOut = false;
                    readIni.WriteString("RepastErp", "isCheckOut", "false");
                    break;
                case "结账确定":
                    {
                       
                           
                            Info.isDistinct = false;
                            Info.isAllowance = false;
                            Info.canAlterDinner = true;
                            readIni.WriteString("RepastErp", "canAlterDinner", "true");
                            Info.inputNumber = 0;
                            readIni.WriteString("RepastErp", "inputNumber", "0");
                            Info.isCheckOut = false;
                            readIni.WriteString("RepastErp", "isCheckOut", "false");
                            Info.totalPrice = 0;
                            readIni.WriteString("RepastErp", "totalPrice", "0");
                            Info.saleMethord = 1;
                            readIni.WriteString("RepastErp", "saleMethord", Info.saleMethord.ToString());
                            if (Info.isGetMaxDealnum)
                            {
                                Saletmp00 saletmp00 = new Saletmp00();
                                saletmp00.SetMaxDeal_number();
                                Info.isGetMaxDealnum = false;
                            }
                            Info.deal_number++;//产生新的交易号
                            readIni.WriteString("RepastErp", "deal_number", Info.deal_number.ToString());

                            ////对交易号位数的判定，销售单号中交易号应该为四位
                            string deal_number1 = Convert.ToString(Info.deal_number);

                            switch (deal_number1.Trim().Length)
                            {
                                case 1:
                                    deal_number1 = "000" + deal_number1;
                                    break;
                                case 2:
                                    deal_number1 = "00" + deal_number1;
                                    break;
                                case 3:
                                    deal_number1 = "0" + deal_number1;
                                    break;
                                case 4: break;
                                default:
                                    deal_number1 = "0001";
                                    Info.deal_number = 1;
                                    readIni.WriteString("RepastErp", "deal_number", "1");
                                    break;
                            }
                            Info.sale_id = Info.shop_id + Info.pos_id + DateTime.Now.ToString("yyyyMMddHHmmss") + deal_number1;//生成新的销售单号并赋值给Info.sale_id
                            readIni.WriteString("RepastErp", "sale_id", Info.sale_id);
                            Info.sale_sno = 1;
                            readIni.WriteString("RepastErp", "sale_sno", Info.sale_sno.ToString());
                        
                        break;
                    }
                
                case "兑换券":
                    Info.isCheckChit=true;
                    readIni.WriteString("RepastErp", "isCheckChit", "true");
                    break;
                case "兑换券确定":
                      Info.isCheckChit = false;
                      readIni.WriteString("RepastErp", "isCheckChit","false");
                      break;
                case "兑换券取消":
                      Info.isCheckChit = false;
                      readIni.WriteString("RepastErp", "isCheckChit", "false");
                      break;

                case "挂单":
                    
                    Info.canAlterDinner = true;
                        readIni.WriteString("RepastErp", "canAlterDinner", "true");  
                        Info.inputNumber = 0;
                        readIni.WriteString("RepastErp", "inputNumber", "0");  
                        Info.isCheckOut = false;
                        readIni.WriteString("RepastErp", "isCheckOut", "false");
                        Info.totalPrice = 0;
                        readIni.WriteString("RepastErp", "totalPrice", "0");
                        Info.saleMethord = 1;
                        readIni.WriteString("RepastErp", "saleMethord", Info.saleMethord.ToString());
                        if (Info.isGetMaxDealnum)
                        {
                            Saletmp00 saletmp00 = new Saletmp00();
                            saletmp00.SetMaxDeal_number();
                            Info.isGetMaxDealnum = false;
                        }
                        Info.deal_number++;//产生新的交易号
                        readIni.WriteString("RepastErp", "deal_number", Info.deal_number.ToString ());
                        ////对交易号位数的判定，销售单号中交易号应该为四位
                        string deal_number2 = Convert.ToString(Info.deal_number);

                        switch (deal_number2.Trim ().Length)
                        {
                            case 1:
                                deal_number2 = "000" + deal_number2;
                                break;
                            case 2:
                                deal_number2 = "00" + deal_number2;
                                break;
                            case 3:
                                deal_number2 = "0" + deal_number2;
                                break;
                            case 4: break;
                            default:
                                deal_number2 = "0001";
                                Info.deal_number = 1;
                                readIni.WriteString("RepastErp", "deal_number", "1");
                                break;
                        }
                        Info.sale_id = Info.shop_id + Info.pos_id + DateTime.Now.ToString("yyyyMMddHHmmss") + deal_number2;//生成新的销售单号并赋值给Info.sale_id
                        readIni.WriteString("RepastErp", "sale_id", Info.sale_id);
                        Info.sale_sno = 1;
                        readIni.WriteString("RepastErp", "sale_sno", Info.sale_sno.ToString ());
                    
                    break;
                case "退货": 
                    break;
                    
                case "提单sale_id":
                   // Info.sale_sno = 1;
                    int deal_n;

                    try
                    {
                         deal_n = Convert.ToInt32(para.ToString().Substring(para.ToString().Length - 4, 4));
                    }
                    catch { deal_n = 1; }

                    Info.deal_number = deal_n;
                    Info.sale_sno =Convert.ToInt32 ( readIni.ReadString("RepastErp", "sale_sno"));
                    Info.sale_id = para;
                    Info.IsTiDan = false;
                    Info.canAlterDinner = true;
                    readIni.WriteString("RepastErp", "canAlterDinner", "true");  
                    break;
                case "下线":

                    break;
                default://提单
                    if (function.Substring(1, 1) == "1")
                    {
                        Info.saleMethord = 1;
                        readIni.WriteString("RepastErp", "saleMethord", "1");
                    }
                    else if (function.Substring(1, 1) == "2")
                    {
                        Info.saleMethord = 2;
                        readIni.WriteString("RepastErp", "saleMethord", "2");
                    }
                    else if (function.Substring(1, 1) == "3")
                    {
                        Info.saleMethord = 3;
                        readIni.WriteString("RepastErp", "saleMethord", "3");
                    }
                    else
                    {
                        Info.saleMethord = 4;
                        readIni.WriteString("RepastErp", "saleMethord", "4");
                    }
                    if (function.Substring(2, 1) == "1")
                    {
                        Info.isDistinct = true;
                        readIni.WriteString("RepastErp", "isDistinct", "true");
                    }
                    if (function.Substring(3, 1) == "2")
                    {
                        Info.isAllowance = true;
                        readIni.WriteString("RepastErp", "isAllowance", "true");
                    }
                    break;



            }
        }
    }
}
