using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using POS.Common;
using System.Data;
//查询saletmp00并根据要求返回结果集中的信息
/*主要公共方法：
（1）SaleTmp00TotSales()，返回指定的总销售额
（2）SaleTmp00TotQuan()，返回指定的销售总量
*/
namespace POS.Service
{
    /// <summary>
    /// 查询saletmp00，根据要求返回结果集中的信息
    /// </summary>
    class GetSaleTmp00 : DBSql
    {
        //定义一个变量
        DataSet dataSet;
        ///<summary>
        /// 有变量的构造函数
        /// </summary>
        /// <param name="shop_id">分店编号</param>
        /// <param name="sale_id">销售单编号</param>
        public GetSaleTmp00(string shop_id,string sale_id)
        {           
            GetDataSaleTmp00(sale_id, shop_id);
        }
        /// <summary>
        /// 有变量的构造函数
        /// </summary>
        /// <param name="shop_id">分店编号</param>
        /// <param name="sale_id">销售单编号</param>
        /// <returns>GetSaleTmp00的一个实例</returns>
        public static GetSaleTmp00 InitGetSaleTmp00(string shop_id,string sale_id)
        {
            return new GetSaleTmp00(shop_id,sale_id);
        }
        #region//公共方法
        /// <summary>
        /// 返回满足指定条件表的表属性集合
        /// </summary>
        /// <param name="sale_id">销售id号</param>
        /// <param name="shop_id">shop_id号</param>
        /// <returns>DataSet类型</returns>
        public DataSet GetDataSaleTmp00(string sale_id,string shop_id)
        {
            
            dataSet = new DataSet();
            string sql = "select TOT_SALES,TOT_QUAN from SALETMP00 where SALE_ID='"+sale_id+"'AND SHOP_ID='"+shop_id+"'";
            dataSet = base.CreateDataSet(sql);
            return dataSet;
            
        }
        /// <summary>
        /// 返回指定的总销售额
        /// </summary>
        /// <returns>总销售额</returns>
        public Decimal SaleTmp00TotSales()
        {
            try { return Convert.ToDecimal(dataSet.Tables[0].Rows[0]["TOT_SALES"].ToString());}
            catch { return Convert.ToDecimal(0); }
            
        }
        /// <summary>
        /// 返回指定的销售总量
        /// </summary>
        /// <returns>销售总量</returns>
        public Decimal SaleTmp00TotQuan()
        {
            try
            {
                return Convert.ToDecimal(dataSet.Tables[0].Rows[0]["TOT_QUAN"].ToString());
            }
            catch 
            {

                return Convert.ToDecimal(0);
            }
        }
        #endregion
    }
}
