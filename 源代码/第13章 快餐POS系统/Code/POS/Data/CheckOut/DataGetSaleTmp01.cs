using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using POS.Common;
using System.Data;
//对SaleTmp01表的查询返回DataSet一个变量
/*主要公共方法：
（1）GetSaleTmp01(string  sale_id)，从SALETMP01中取数据(包括组合餐中的子产品）
*/
namespace POS.Data
{
    /// <summary>
    /// 对SaleTmp01表的查询返回一个结果集
    /// </summary>
    class DataGetSaleTmp01 : DBSql
    {
        /// <summary>
        /// 获得一个DataGetSaleTmp01实体
        /// </summary>
        /// <returns>DataGetSaleTmp01实体</returns>
        public static DataGetSaleTmp01 InitDataGetSaleTmp01()
        {
            return new DataGetSaleTmp01();
        }
        /// <summary>
        /// 执行父类方法完成表的查询
        /// </summary>
        /// <param name="SALE_ID">销售编号</param>
        ///<param name="SHOP_ID">分店编号</param>
        /// <returns>DataSet的一个变量</returns>
        public DataSet GetSaleTmp01(string SALE_ID,string SHOP_ID)
        {
            string sql = "select * from SALETMP01 where SALE_ID='" + SALE_ID + "'AND SHOP_ID='" + SHOP_ID + "'";
            return base.CreateDataSet(sql);
        }
    }
}
