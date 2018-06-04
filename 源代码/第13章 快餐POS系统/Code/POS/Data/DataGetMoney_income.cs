using System;
using POS.Common;
using System.Data;
using System.Data.SqlClient;
//获取本班次的总现金收入 涉及表：sale00
/*（1）、Moneyincome（）获取现金收入限制条件：sale_user='" + Info.emp_id + "' and SALE_DATE BETWEEN '" + Info.login_date + "' and '" + Info.exit_date+ "'
 */
namespace POS.Data
{
    /// <summary>
    /// 获取本班次的总现金收入
    /// </summary>
    class DataGetMoney_income : DBSql
    {

        /// <summary>
        /// 创建对象
        /// </summary>
        /// <returns>DataGetMoney_income 对象</returns>
        public static DataGetMoney_income InitDataOperation() { return new DataGetMoney_income(); }

        /// <summary>
        /// 获取现金收入
        /// </summary>
        /// <returns>DataSet对象</returns>
        public DataSet Moneyincome()
        {
            //return base.CreateDataSet("Select sum(amount) as Money_income  from sale02  where SALE02.last_update between '" + Info.login_date + "' and '" + Info.exit_date + "' ");
            //return base.CreateDataSet("Select sum(amount) as Money_income  from sale02,PAYMENT  where (sale02.pay_id=PAYMENT.pay_id or sale02.pay_id='01')  and PAY_TYPE ='1' and SALE02.last_update between '" + Info.login_date + "' and '" + Info.exit_date + "' ");
            return base.CreateDataSet("select sum(tot_sales)-sum(isnull(back_sales,0)) as Money_income from sale00 where sale_user='" + Info.emp_id + "' and SALE_DATE BETWEEN '" + Info.login_date + "' and '" + Info.exit_date+ "'");
        }
    }
}
