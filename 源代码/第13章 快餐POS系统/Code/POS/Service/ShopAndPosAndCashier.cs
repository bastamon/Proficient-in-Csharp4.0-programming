using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using POS.Common;
using System.Data;
// 分别查询本机营业额，本店营业额 涉及表： sale00、sale01、sale02
/*主要公共方法：
（1）ShopTur(int select)，查询本店营业额
（2）PosTur(int select)，本机营业额查询
（3）Cashier(int select)，查询读取收银员帐
（4）GetSaleMoney()用于获得收银员目前销售金额
*/
namespace POS.Service
{
    /// <summary>
    /// 分别查询本机营业额，本店营业额
    /// </summary>
    class ShopAndPosAndCashier : DBSql
    {
        string sqlstr;
      
        /// <summary>
        /// 查询本店营业额
        /// </summary>
        /// <returns>DataSet</returns>
        public DataSet ShopTur(int select)
        {


            //后台服务器信息
            ReadIni readIni = new ReadIni("config.ini");
            string srvIp = readIni.ReadString("RepastErp", "txtServerIP");
            string srvPort = readIni.ReadString("RepastErp", "txtPort");
            string srvDBName = readIni.ReadString("RepastErp", "txtIPdataname");
            string srvUserName = readIni.ReadString("RepastErp", "txtFTPuser");
            string srvPassword = readIni.ReadString("RepastErp", "txtFTPpassword");
            string str = "OPENDATASOURCE('SQLOLEDB','Data Source=" + srvIp + "," + srvPort + ";User ID=" + srvUserName + ";Password=" + srvPassword + "' )." + srvDBName + ".dbo.";

            //#region//查询本店的总营业额
            //if (select == 0)
            //{
            //    sqlstr = " Select shop_id, SUM(TOT_SALES) from SALE00 where shop_id = '" + Info.shop_id + "' GROUP BY shop_id";
            //    return base.CreateDataSet(sqlstr);
            //}
            //#endregion #endregion
            #region//查询本店当月营业额
            if (select == 1)
            {
                string monthstr0 = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-1 0:0:0";
                string monthstr1 = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + (DateTime.Now.Day + 1).ToString() + " 0:0:0";
                sqlstr = "select shop_id ,sum(tot_sales) from "+str+"sale00 where shop_id='" + Info.shop_id + "' and SALE_DATE BETWEEN '" + monthstr0 + "' and '" + monthstr1 + "' GROUP BY shop_id";
                return base.CreateDataSet(sqlstr);
            }
            #endregion
            #region //查询本店本周营业额
            else if (select == 2)
            {
                string weekstr = "";
                int week = Convert.ToInt32(DateTime.Now.DayOfWeek);
                if (DateTime.Now.Day - week > 0)
                    weekstr = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + (DateTime.Now.Day - week).ToString() + " 0:0:0";
                else if (DateTime.Now.Day - week <= 0)
                {
                    switch (DateTime.Now.Month - 1)
                    {
                        case 1:
                            weekstr = DateTime.Now.Year.ToString() + "-" + "1-" + (31 + DateTime.Now.Day - week).ToString() + " 0:0:0";
                            break;
                        case 3:
                            weekstr = DateTime.Now.Year.ToString() + "-" + "3-" + (31 + DateTime.Now.Day - week).ToString() + " 0:0:0";
                            break;
                        case 5:
                            weekstr = DateTime.Now.Year.ToString() + "-" + "5-" + (31 + DateTime.Now.Day - week).ToString() + " 0:0:0";
                            break;
                        case 7:
                            weekstr = DateTime.Now.Year.ToString() + "-" + "7-" + (31 + DateTime.Now.Day - week).ToString() + " 0:0:0";
                            break;
                        case 8:
                            weekstr = DateTime.Now.Year.ToString() + "-" + "8-" + (31 + DateTime.Now.Day - week).ToString() + " 0:0:0";
                            break;
                        case 10:
                            weekstr = DateTime.Now.Year.ToString() + "-" + "10-" + (31 + DateTime.Now.Day - week).ToString() + " 0:0:0";
                            break;
                        case 4:
                            weekstr = DateTime.Now.Year.ToString() + "-" + "4-" + (30 + DateTime.Now.Day - week).ToString() + " 0:0:0";
                            break;
                        case 6:
                            weekstr = DateTime.Now.Year.ToString() + "-" + "6-" + (30 + DateTime.Now.Day - week).ToString() + " 0:0:0";
                            break;
                        case 9:
                            weekstr = DateTime.Now.Year.ToString() + "-" + "9-" + (30 + DateTime.Now.Day - week).ToString() + " 0:0:0";
                            break;
                        case 11:
                            weekstr = DateTime.Now.Year.ToString() + "-" + "11-" + (30 + DateTime.Now.Day - week).ToString() + " 0:0:0";
                            break;
                        case 2:
                            if ((DateTime.Now.Year % 4 == 0 && DateTime.Now.Year % 100 != 0) || DateTime.Now.Year % 400 == 0)
                            {
                                weekstr = DateTime.Now.Year.ToString() + "-" + "2-" + (29 + DateTime.Now.Day - week).ToString() + " 0:0:0";
                                break;
                            }
                            else
                            {
                                weekstr = DateTime.Now.Year.ToString() + "-" + "2-" + (28 + DateTime.Now.Day - week).ToString() + " 0:0:0";
                                break;
                            }
                        default:
                            weekstr = (DateTime.Now.Year - 1).ToString() + "-" + "12-" + (31 + DateTime.Now.Day - week).ToString() + " 0:0:0";
                            break;
                    }
                }
                string weekstr1 = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + (DateTime.Now.Day + 1).ToString() + " 0:0:0";
                sqlstr = "select shop_id ,sum(tot_sales) from " + str + " sale00 where shop_id='" + Info.shop_id + "' and SALE_DATE BETWEEN '" + weekstr + "' and '" + weekstr1 + "' GROUP BY shop_id";
                return base.CreateDataSet(sqlstr);
            }
            #endregion
            #region//查询本店的当日营业额
            else
            {
                string daystr0 = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + " 0:0:0";
                string daystr1 = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + (DateTime.Now.Day + 1).ToString() + " 0:0:0";
                sqlstr = "select shop_id ,sum(tot_sales) from  " + str + " sale00 where shop_id='" + Info.shop_id + "' and SALE_DATE BETWEEN '" + daystr0 + "' and '" + daystr1 + "' GROUP BY shop_id";
                return base.CreateDataSet(sqlstr);
            }
            #endregion
        }

        #region//本机营业额查询
        /// <summary>
        /// 本机营业额查询
        /// </summary>
        /// <param name="select">指示所要显示的值</param>
        /// <returns>数据表DataSet</returns>
        public DataSet PosTur(int select)
        {
            #region//查询POS机总营业额
            if (select == 0)
            {
                sqlstr = " Select POS_ID, SUM(TOT_SALES) from SALE00 where pos_id = '" + Info.pos_id + "' GROUP BY POS_ID";
                //sqlstr =" Select POS_ID, SUM(TOT_SALES) from SALE00 where pos_id = '1' GROUP BY POS_ID";
                return base.CreateDataSet(sqlstr);
            }
            #endregion
            #region//查询pos机的当月营业额
            else if (select == 1)
            {
                string monthstr0 = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-1 0:0:0";
                string monthstr1 = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + (DateTime.Now.Day + 1).ToString() + " 0:0:0";
                sqlstr = "select pos_id ,sum(tot_sales) from sale00 where pos_id='" + Info.pos_id + "' and SALE_DATE BETWEEN '" + monthstr0 + "' and '" + monthstr1 + "' GROUP BY POS_ID";
                return base.CreateDataSet(sqlstr);
            }
            #endregion
            #region //查询pos机的本周营业额
            else if (select == 3)
            {
                ReadIni readini = new ReadIni();
                DateTime dt;
                try
                {
                    dt = Convert.ToDateTime(readini.ReadString("RepastErp", "changeWorkTime"));
                }
                catch { dt = Convert.ToDateTime(DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + " 0:0:0"); }

                string daystr0 = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + " " + dt.Hour.ToString() + ":" + dt.Minute.ToString() + ":" + dt.Second.ToString(); // " 0:0:0";
                string daystr1 = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + (DateTime.Now.Day + 1).ToString() + " " + dt.Hour.ToString() + ":" + dt.Minute.ToString() + ":" + dt.Second.ToString(); // " 0:0:0";
                sqlstr = "select SALE_USER ,sum(tot_sales) from sale00 where sale_user='" + Info.emp_id + "' and SALE_DATE BETWEEN '" + daystr0 + "' and '" + daystr1 + "' GROUP BY sale_user";
                return base.CreateDataSet(sqlstr);
            }
            else if (select == 2)
            {
                string weekstr = "";
                int week = Convert.ToInt32(DateTime.Now.DayOfWeek);
                // int count=DateTime.Now.Day-week ;
                if (DateTime.Now.Day - week > 0)
                    weekstr = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + (DateTime.Now.Day - week).ToString() + " 0:0:0";
                else if (DateTime.Now.Day - week <= 0)
                {
                    switch (DateTime.Now.Month - 1)
                    {
                        case 1:
                            weekstr = DateTime.Now.Year.ToString() + "-" + "1-" + (31 + DateTime.Now.Day - week).ToString() + " 0:0:0";
                            break;
                        case 3:
                            weekstr = DateTime.Now.Year.ToString() + "-" + "3-" + (31 + DateTime.Now.Day - week).ToString() + " 0:0:0";
                            break;
                        case 5:
                            weekstr = DateTime.Now.Year.ToString() + "-" + "5-" + (31 + DateTime.Now.Day - week).ToString() + " 0:0:0";
                            break;
                        case 7:
                            weekstr = DateTime.Now.Year.ToString() + "-" + "7-" + (31 + DateTime.Now.Day - week).ToString() + " 0:0:0";
                            break;
                        case 8:
                            weekstr = DateTime.Now.Year.ToString() + "-" + "8-" + (31 + DateTime.Now.Day - week).ToString() + " 0:0:0";
                            break;
                        case 10:
                            weekstr = DateTime.Now.Year.ToString() + "-" + "10-" + (31 + DateTime.Now.Day - week).ToString() + " 0:0:0";
                            break;
                        case 4:
                            weekstr = DateTime.Now.Year.ToString() + "-" + "4-" + (30 + DateTime.Now.Day - week).ToString() + " 0:0:0";
                            break;
                        case 6:
                            weekstr = DateTime.Now.Year.ToString() + "-" + "6-" + (30 + DateTime.Now.Day - week).ToString() + " 0:0:0";
                            break;
                        case 9:
                            weekstr = DateTime.Now.Year.ToString() + "-" + "9-" + (30 + DateTime.Now.Day - week).ToString() + " 0:0:0";
                            break;
                        case 11:
                            weekstr = DateTime.Now.Year.ToString() + "-" + "11-" + (30 + DateTime.Now.Day - week).ToString() + " 0:0:0";
                            break;
                        case 2:
                            if ((DateTime.Now.Year % 4 == 0 && DateTime.Now.Year % 100 != 0) || DateTime.Now.Year % 400 == 0)
                            {
                                weekstr = DateTime.Now.Year.ToString() + "-" + "2-" + (29 + DateTime.Now.Day - week).ToString() + " 0:0:0";
                                break;
                            }
                            else
                            {
                                weekstr = DateTime.Now.Year.ToString() + "-" + "2-" + (28 + DateTime.Now.Day - week).ToString() + " 0:0:0";
                                break;
                            }
                        default:
                            weekstr = (DateTime.Now.Year - 1).ToString() + "-" + "12-" + (31 + DateTime.Now.Day - week).ToString() + " 0:0:0";
                            break;
                    }
                }
                string weekstr1 = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + (DateTime.Now.Day + 1).ToString() + " 0:0:0";
                sqlstr = "select pos_id ,sum(tot_sales) from sale00 where pos_id='" + Info.pos_id + "' and SALE_DATE BETWEEN '" + weekstr + "' and '" + weekstr1 + "' GROUP BY POS_ID";
                return base.CreateDataSet(sqlstr);
            }
            #endregion
            #region//查询pos机当日营业额
            else
            {
                string daystr0 = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + " 0:0:0";
                string daystr1 = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + (DateTime.Now.Day + 1).ToString() + " 0:0:0";
                sqlstr = "select pos_id ,sum(tot_sales) from sale00 where pos_id='" + Info.pos_id + "' and SALE_DATE BETWEEN '" + daystr0 + "' and '" + daystr1 + "' GROUP BY POS_ID";
                return base.CreateDataSet(sqlstr);
            }
            #endregion
        }
        #endregion
        #region //查询收银员上线到现在销售金额
        /// <summary>
        /// 用于获得销售收银员目前销售金额
        /// </summary>
        /// <returns>decimal</returns>
        public decimal GetSaleMoney()
        {
            ReadIni readini = new ReadIni();
            DateTime dt;
            dt=Info.login_date;
            //try
            //{
            //    dt = Convert.ToDateTime(readini.ReadString("RepastErp", "changeWorkTime"));
            //}
            //catch { dt = Convert.ToDateTime(DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + " 0:0:0"); }

            //string daystr0 = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + " " + dt.Hour.ToString() + ":" + dt.Minute.ToString() + ":" + dt.Second.ToString(); // " 0:0:0";
            //string daystr1 = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + (DateTime.Now.Day + 1).ToString() + " " + dt.Hour.ToString() + ":" + dt.Minute.ToString() + ":" + dt.Second.ToString(); // " 0:0:0";
            sqlstr = "select SALE_USER ,sum(tot_sales)-sum(isnull(back_sales,0)) from sale00 where sale_user='" + Info.emp_id + "' and SALE_DATE BETWEEN '" + dt + "' and '" + DateTime.Now + "' GROUP BY sale_user";
           return Convert.ToDecimal(CreateDataSet(sqlstr).Tables[0].Rows[0][1]);
        }
        #endregion
        #region//查询读收银员帐
        /// <summary>
        /// 查询读取收银员帐
        /// </summary>
        /// <returns>DataSet</returns>
        public DataSet Cashier(int select)
        {

            #region//查询收银员的总营业额
            if (select == 0)
            {
                sqlstr = "Select SALE_USER, SUM(TOT_SALES) from SALE00 where SALE_USER =" + Info.emp_id + " GROUP BY sale_user ";
                return base.CreateDataSet(sqlstr);
            }
            #endregion
            #region//查询收银员的本月营业额
            else if (select == 1)
            {
                string monthstr0 = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-1 0:0:0";
                string monthstr1 = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + (DateTime.Now.Day + 1).ToString() + " 0:0:0";
                sqlstr = "select SALE_USER ,sum(tot_sales) from sale00 where sale_user='" + Info.emp_id + "' and SALE_DATE BETWEEN '" + monthstr0 + "' and '" + monthstr1 + "' GROUP BY sale_user";
                return base.CreateDataSet(sqlstr);
            }
            #endregion
            #region//查询收营员的本周营业额
            else if (select == 2)
            {
                string weekstr = "";
                int week = Convert.ToInt32(DateTime.Now.DayOfWeek);
                if (DateTime.Now.Day - week > 0)
                    weekstr = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + (DateTime.Now.Day - week).ToString() + " 0:0:0";
                else if (DateTime.Now.Day - week <= 0)
                {
                    switch (DateTime.Now.Month - 1)
                    {
                        case 1:
                            weekstr = DateTime.Now.Year.ToString() + "-" + "1-" + (31 + DateTime.Now.Day - week).ToString() + " 0:0:0";
                            break;
                        case 3:
                            weekstr = DateTime.Now.Year.ToString() + "-" + "3-" + (31 + DateTime.Now.Day - week).ToString() + " 0:0:0";
                            break;
                        case 5:
                            weekstr = DateTime.Now.Year.ToString() + "-" + "5-" + (31 + DateTime.Now.Day - week).ToString() + " 0:0:0";
                            break;
                        case 7:
                            weekstr = DateTime.Now.Year.ToString() + "-" + "7-" + (31 + DateTime.Now.Day - week).ToString() + " 0:0:0";
                            break;
                        case 8:
                            weekstr = DateTime.Now.Year.ToString() + "-" + "8-" + (31 + DateTime.Now.Day - week).ToString() + " 0:0:0";
                            break;
                        case 10:
                            weekstr = DateTime.Now.Year.ToString() + "-" + "10-" + (31 + DateTime.Now.Day - week).ToString() + " 0:0:0";
                            break;
                        case 4:
                            weekstr = DateTime.Now.Year.ToString() + "-" + "4-" + (30 + DateTime.Now.Day - week).ToString() + " 0:0:0";
                            break;
                        case 6:
                            weekstr = DateTime.Now.Year.ToString() + "-" + "6-" + (30 + DateTime.Now.Day - week).ToString() + " 0:0:0";
                            break;
                        case 9:
                            weekstr = DateTime.Now.Year.ToString() + "-" + "9-" + (30 + DateTime.Now.Day - week).ToString() + " 0:0:0";
                            break;
                        case 11:
                            weekstr = DateTime.Now.Year.ToString() + "-" + "11-" + (30 + DateTime.Now.Day - week).ToString() + " 0:0:0";
                            break;
                        case 2:
                            if ((DateTime.Now.Year % 4 == 0 && DateTime.Now.Year % 100 != 0) || DateTime.Now.Year % 400 == 0)
                            {
                                weekstr = DateTime.Now.Year.ToString() + "-" + "2-" + (29 + DateTime.Now.Day - week).ToString() + " 0:0:0";
                                break;
                            }
                            else
                            {
                                weekstr = DateTime.Now.Year.ToString() + "-" + "2-" + (28 + DateTime.Now.Day - week).ToString() + " 0:0:0";
                                break;
                            }
                        default:
                            weekstr = (DateTime.Now.Year - 1).ToString() + "-" + "12-" + (31 + DateTime.Now.Day - week).ToString() + " 0:0:0";
                            break;
                    }
                }
                string weekstr1 = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + (DateTime.Now.Day + 1).ToString() + " 0:0:0";
                sqlstr = "select SALE_USER ,sum(tot_sales) from sale00 where  sale_user='" + Info.emp_id + "' and SALE_DATE BETWEEN '" + weekstr + "' and '" + weekstr1 + "' GROUP BY sale_user";
                return base.CreateDataSet(sqlstr);
            }
            #endregion
            #region//查询收银员的今日营业额
            else
            {
                ReadIni readini = new ReadIni();
                DateTime dt ;
                try
                {
                    dt = Convert.ToDateTime(readini.ReadString("RepastErp", "changeWorkTime"));
                }
                catch { dt = Convert.ToDateTime(DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + " 0:0:0"); }

                string daystr0 = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + " " + dt.Hour.ToString() + ":" + dt.Minute.ToString() + ":" + dt.Second.ToString(); // " 0:0:0";
                string daystr1 = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + (DateTime.Now.Day + 1).ToString() + " " + dt.Hour.ToString() + ":" + dt.Minute.ToString() + ":" + dt.Second.ToString(); // " 0:0:0";
                sqlstr = "select SALE_USER ,sum(tot_sales) from sale00 where sale_user='" + Info.emp_id + "' and SALE_DATE BETWEEN '" + daystr0 + "' and '" + daystr1 + "' GROUP BY sale_user";
                return base.CreateDataSet(sqlstr);
            }
            #endregion
        }
        #endregion
    }
}
