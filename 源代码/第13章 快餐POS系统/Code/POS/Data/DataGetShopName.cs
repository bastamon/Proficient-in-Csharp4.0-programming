using System;
using POS.Common;
using System.Data.SqlClient;
using System.Data;
//查询分店SHOP00中相关信息：分店编号、名字 涉及表： SHOP00
/*主要公共方法：
（1）shop(String  shop_id)，查询SHOP00中相关信息
*/
namespace POS.Data
{
        /// <summary>
        /// 查询SHOP00中相关信息
        /// </summary>
        class DataGetShopName : DBSql
        {
            /// <summary>
            /// 获得一个DataGetShopName的一个对象
            /// </summary>
            /// <returns>DataGetShopName的一个对象</returns>
            public static DataGetShopName InitDataOperation() { return new DataGetShopName(); }

           /// <summary>
            ///查询SHOP00中相关信息
           /// </summary>
            /// <param name="shop_id">分店编号</param>
            /// <returns>返回调用基类DBSql的CreateDataSet方法</returns>
            public DataSet shop(String  shop_id)
            {
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter("@shop_id", SqlDbType.NVarChar);
                para[0].Value = shop_id;

                return base.CreateDataSet("select SHOP_ID,SHOP_NAME from SHOP00 where SHOP_ID=@shop_id", para);
            }
        }
}
    

