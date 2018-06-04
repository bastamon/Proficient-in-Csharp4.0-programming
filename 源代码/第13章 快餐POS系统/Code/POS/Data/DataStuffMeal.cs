using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using POS.Common;
// 员工餐时对saletmp00、saletmp01表的更新 涉及表：saletmp00、saletmp01
/*主要公共方法：
（1）	DataStuff_Meal(string emp_id, string sale_id)，把一个销售单设置为员工餐
*/
namespace POS.Data
{
    /// <summary>
    /// 员工餐时对saletmp00、saletmp01表的更新
    /// </summary>
    class DataStuffMeal:DBSql
    {
        /// <summary>
        /// 获得一个OffLine实例
        /// </summary>
        /// <returns>DataStuffMeal对象</returns>
        public static DataStuffMeal InitDataStuffMeal()
        {
            return new DataStuffMeal();
        }


        /// <summary>
        /// 把一个销售单设置为员工餐
        /// </summary>
        /// <param name="emp_id">员工编号</param>
        /// <param name="sale_id">销售单号</param>
        /// <returns>成功执行返回true</returns>
        public bool DataStuff_Meal(string emp_id, string sale_id)
        {
            SqlParameter[] para1 = new SqlParameter[1];
            para1[0] = new SqlParameter("@sale_id", SqlDbType.NVarChar);
            para1[0].Value = sale_id;

            SqlParameter[] para2 = new SqlParameter[2];
            para2[0] = new SqlParameter("@FREE_EMP", SqlDbType.NVarChar);
            para2[1] = new SqlParameter("@sale_id", SqlDbType.NVarChar);
            para2[0].Value = emp_id;
            para2[1].Value = sale_id;

            bool b = false;
            //并把SALE00表中的MEAL_KIND字段置为1
            b = base.RunSQL("update saletmp00 set TOT_SALES=0,MEAL_KIND=1 where sale_id=@sale_id", para1);

            b= base.RunSQL("update saletmp01 set FREE_EMP=@FREE_EMP,PRICE_TYPE='7' where sale_id=@sale_id", para2);
            return b;
        }
    }
}
