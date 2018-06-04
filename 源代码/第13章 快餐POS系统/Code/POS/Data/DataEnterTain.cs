using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using POS.Common;
//用于招待更新saletmp00、saletmp01相关操作，以及获得员工级别
/*主要公共方法：
（1）Data_Entertain(string emp_id, string sale_id)，更新saletmp00、saletmp01
（2）GetEmp_level(string emp_id)，根据员工级别得到员工等级
*/
namespace POS.Data
{
    /// <summary>
    /// 用于招待时更新saletmp00、saletmp01
    /// </summary>
    class DataEntertain : DBSql
    {
        /// <summary>
        /// 获得一个EnterTain实例
        /// </summary>
        /// <returns>DataEntertain对象</returns>
        public static DataEntertain InitDataEntertain()
        {
            return new DataEntertain();
        }

        /// <summary>
        /// 更新saletmp00、saletmp01
        /// </summary>
        /// <param name="emp_id">员工编号</param>
        /// <param name="sale_id">销售单编号</param>
        /// <returns>成功执行返回true</returns>
        public bool Data_Entertain(string emp_id, string sale_id)
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
            //更改saletmp00表
            b = base.RunSQL("update saletmp00 set TOT_SALES=0,MEAL_KIND=2 where sale_id=@sale_id", para1);
            //更改saletmp01表
            b = base.RunSQL("update saletmp01 set FREE_EMP=@FREE_EMP,PRICE_TYPE='5' where sale_id=@sale_id", para2);
            return b;
        }

        /// <summary>
        /// 根据员工级别得到员工等级
        /// </summary>
        /// <param name="emp_id">员工编号</param>
        /// <returns>存放员工级别的数据集</returns>
        public DataSet GetEmp_level(string emp_id)
        {
            SqlParameter[] para1 = new SqlParameter[1];
            para1[0] = new SqlParameter("@emp_id", SqlDbType.NVarChar);
            para1[0].Value = emp_id;

            return base.CreateDataSet("select * from EMPLOYEE where emp_id=@emp_id", para1);

        }


    }
}
