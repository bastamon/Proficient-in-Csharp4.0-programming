using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using POS.Common;
using System.Data;
using System.Data.SqlClient;
//根据销售单编号从saletmp01中读取相关数据信息 涉及表： SALETMP01、product00
namespace POS.Data
{
    /// <summary>
    /// 根据销售单编号从saletmp01中读取数据
    /// </summary>
    class DataGetSaleTemp01 : DBSql 
    {
      
        /// <summary>
        /// 获得一个DataGetSaleTmp01实体
        /// </summary>
        /// <returns>DataGetSaleTemp01对象</returns>
        public static DataGetSaleTemp01 InitDataDataGetSaleTmp01() { return new DataGetSaleTemp01(); }

        /// <summary>
        /// 从SALETMP01中取数据(包括组合餐中的子产品）
        /// </summary>
        /// <param name="sale_id">销售单编号</param>
        /// <returns>结果集DataSet</returns>
        public DataSet GetSaleTmp01(string  sale_id)
        {
            return base.CreateDataSet("select SALETMP01.*,pos_disp from SALETMP01,product00 where product00.prod_id=SALETMP01.prod_id  and SALE_ID='" + sale_id + "'");
           
        }

        /// <summary>
        /// 从SALETMP01中取数据(不读组合餐中的子产品）
        /// </summary>
        /// <param name="sale_id">销售单编号</param>
        /// <returns>结果集DataSet</returns>
        public DataSet GetSaleTmp01Single(string sale_id)
        {
            return base.CreateDataSet("select SALETMP01.*,pos_disp from SALETMP01,product00 where product00.prod_id=SALETMP01.prod_id and comb_type !=2 and SALE_ID='" + sale_id + "' order by SALE_SNO");

        }

        /// <summary>
        /// 从SALETMP01表中查询信息
        /// </summary>
        /// <param name="sale_id">销售单编号</param>
        /// <returns>结果集DataSet</returns>
        public DataSet GetSales(string sale_id)
        {
            SqlParameter[] para = new SqlParameter[1];
            para[0] = new SqlParameter("@SALE_ID", SqlDbType.NVarChar);
            para[0].Value = sale_id;
            return base.CreateDataSet("select PROD_ID,comb_type,qty from SALETMP01 where sale_id=@sale_id ", para);
        }
       
    }

    
}
