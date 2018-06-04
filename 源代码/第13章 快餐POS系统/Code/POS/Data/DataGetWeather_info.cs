using System;
using System.Data;
using System.Data.SqlClient;
using POS.Common;
//查询天气信息：weather表 
/*主要公共方法：
（1）Weather(String shop_id, DateTime w_date)，查询weather表
*/
namespace POS.Data
{
    /// <summary>
    /// 查询weather表（用于查询天气情况）
    /// </summary>
    class DataGetWeather_info : DBSql
    {
        /// <summary>
        /// 获得一个DataGetWeather_info的一个对象
        /// </summary>
        /// <returns>DataGetWeather_info的一个对象</returns>
        public static DataGetWeather_info InitDataOperation() { return new DataGetWeather_info(); }

        
        /// <summary>
        /// 查询weather表
        /// </summary>
        /// <param name="shop_id">分店编号</param>
        /// <param name="w_date">日期</param>
        /// <returns>结果集DataSet</returns>
        public DataSet Weather(String shop_id, DateTime w_date)
        {
            SqlParameter[] para = new SqlParameter[2];
            para[0] = new SqlParameter("@shop_id", SqlDbType.NVarChar);
            para[1] = new SqlParameter("@w_date", SqlDbType.SmallDateTime);
            para[0].Value = shop_id;
            para[1].Value = w_date;
            return base.CreateDataSet("Select *  from weather  where SHOP_ID=@shop_id and w_date = (select max(w_date) from weather)",para);
        }
    }
}
