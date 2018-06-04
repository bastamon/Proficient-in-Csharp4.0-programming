using System;
using System.Data;
using System.Data.SqlClient;
using POS.Common;
// 获取Sale00中 VIP卡收入，招待金额，员工餐饮，作废总额等相关数据 涉及表： sale00、sale01
/*主要公方法：
（1）VIP_amount（）VIP收入
（2）Treat_amount()  招待金额
（3）Emp_amount（）员工餐金额
（4）Waste_amount() 作废金额
（5）Money_income() 现金收入
*/
namespace POS.Data
{
    /// <summary>
    /// 获取Sale00中 VIP卡收入，招待金额，员工餐饮，作废总额等相关数据
    /// </summary>
    class DataGetSale00_data : DBSql
    {
        /// <summary>
        /// 创建对象
        /// </summary>
        /// <returns>DataGetSale00_data对象</returns>
        public static DataGetSale00_data InitDataOperation() { return new DataGetSale00_data(); }

        /// <summary>
        /// VIP卡收入
        /// </summary>
        /// <returns>VIP卡收入的数据集 DataSet</returns>
        public DataSet VIP_amount()
        {
            return base.CreateDataSet("SELECT sum(TOT_SALES) as VIP_amount from sale00 where VIP_ID != '' and last_update between '" + Info.login_date + "' and '" + Info.exit_date + "'");
        }

        /// <summary>
        /// 招待金额
        /// </summary>
        /// <returns>招待金额的数据集 DataSet</returns>
        public DataSet Treat_amount()
        {
            return base.CreateDataSet("select sum(sale_price*qty) as Treat_amount from sale01 where sale_id in( select sale_id from sale00 where MEAL_KIND = '2' and last_update between '" + Info.login_date + "' and '" + Info.exit_date + "') and comb_type!=2");
        }

        /// <summary>
        /// 员工餐饮
        /// </summary>
        /// <returns>员工餐饮的数据集 DataSet</returns>
        public DataSet Emp_amount()
        {
            return base.CreateDataSet("select sum(sale_price*qty) as Emp_amount from sale01 where sale_id in( select sale_id from sale00 where MEAL_KIND = '1' and last_update between '" + Info.login_date + "' and '" + Info.exit_date + "') and comb_type!=2");
        }

        /// <summary>
        /// 作废总额
        /// </summary>
        /// <returns>作废总额的数据集 DataSet</returns>
        public DataSet Waste_amount()
        {
            return base.CreateDataSet("SELECT sum(isnull(BACK_SALES,0)) as Waste_amount from sale00 where STATUS_ID  = '3' and last_update between '" + Info.login_date + "' and '" + Info.exit_date + "'");
        }

        /// <summary>
        /// 现金收入
        /// </summary>
        /// <returns>现金收入数据集 DataSet</returns>
        public DataSet Money_income()
        {
            return base.CreateDataSet("SELECT sum(TOT_SALES)-sum(isnull(BACK_SALES,0)) as money_income from sale00 where last_update between '" + Info.login_date + "' and '" + Info.exit_date + "'");
        }


    }
}
