using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using POS.Common;
//数据访问层，用于得到组合餐子产品的相关信息 涉及表 ：combination,product00
/*主要公共方法：
（1）GetComb_Products(string COMB_ID)，通过组合餐编号COMB_ID查找获得获得组合餐
（2）GetCombSno(string COMB_ID)，根据组合编号得到comb_sno
*/
namespace POS.Data
{
    /// <summary>
    /// 得到组合餐子产品的信息
    /// </summary>
    class DataGetComb_Products:DBSql
    { 
        //获得一个DataGetComb_Products对象
        public static DataGetComb_Products InitDataGetComb_Products()
        {
            return new DataGetComb_Products();
        }
        /// <summary>
        /// 通过组合餐编号COMB_ID查找获得获得组合餐
        /// </summary>
        /// <param name="COMB_ID">组合餐ID</param>
        /// <returns>DataSet对象</returns>
        public DataSet  GetComb_Products(string COMB_ID)
        {
            SqlParameter[] para = new SqlParameter[1];
            para[0] = new SqlParameter("@COMB_ID", SqlDbType.NVarChar);
            para[0].Value = COMB_ID;
            return base.CreateDataSet("select  combination.*,pos_disp from combination,product00 WHERE product00.prod_id=combination.prod_id and comb_id =@comb_id and combination.enable='1'  order by comb_sno", para);

        }

        /// <summary>
        /// 根据组合编号得到comb_sno
        /// </summary>
        /// <param name="COMB_ID">组合编号</param>
        /// <returns>DataSet数据集</returns>
        public DataSet GetCombSno(string COMB_ID)
        {
            SqlParameter[] para = new SqlParameter[1];
            para[0] = new SqlParameter("@COMB_ID", SqlDbType.NVarChar);
            para[0].Value = COMB_ID;
            return base.CreateDataSet("select distinct comb_sno from combination,product00 WHERE product00.prod_id=combination.prod_id and comb_id =@comb_id and combination.enable='1'", para);
        }

    }
}   
