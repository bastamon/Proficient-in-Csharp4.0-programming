using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using POS.Common;
using POS.Models;
using POS.Data;
//对saletmp00表的更新和插入 涉及表: SALETMP00
/*主要公共方法：
（1）DataInsertSaleTmp00()，完成对saletmp00表的插入
（2）DataUpdateSaleTmp00(Decimal tot_quan,Decimal tot_sales)，对saletmp00表的相应记录的更新
*/
namespace POS.Service
{
    /// <summary>
    /// 完成对saletmp00表的相关操作
    /// </summary>
    class InsertSaleTmp00 : DBSql
    {
        /// <summary>
        /// 得到InsertSaleTmp00一个实例
        /// </summary>
        /// <returns>InsertSaleTmp00的实例</returns>
        public static InsertSaleTmp00 InitInsertSaleTmp00()
        {
            return new InsertSaleTmp00();
        }
        #region//对表的操作
        /// <summary>
        /// 完成对saletmp00表的插入
        /// </summary>
        /// <returns>bool变量</returns>        
        public bool DataInsertSaleTmp00()
        {
            SALETMP00 saleTmp00 = new SALETMP00();
            saleTmp00.TOT_QUAN1 = 0;
            saleTmp00.TOT_SALES1 = 0;
            return DataInsertSales.InitDataInsertSales().insertSaleTemp00(saleTmp00);
        }
        /// <summary>
        /// 对saletmp00表的相应记录的更新
        /// </summary>
        /// <param name="tot_quan">总销售量</param>
        /// <param name="tot_sales">总销售额</param>
        /// <returns>bool变量</returns>
        public bool DataUpdateSaleTmp00(Decimal tot_quan,Decimal tot_sales)
        {
            string datetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            SALETMP00 saleTmp00 = new SALETMP00();
            saleTmp00.TOT_QUAN1=tot_quan+GetSaleTmp00.InitGetSaleTmp00(Info.shop_id,Info.sale_id).SaleTmp00TotQuan();
            saleTmp00.TOT_SALES1 = tot_sales+GetSaleTmp00.InitGetSaleTmp00(Info.shop_id,Info.sale_id).SaleTmp00TotSales();
            string sql = "UPDATE SALETMP00 SET  TOT_SALES=" + saleTmp00.TOT_SALES1 + ",TOT_QUAN=" + saleTmp00.TOT_QUAN1 + ",LAST_UPDATE='" + datetime + "'  WHERE  SHOP_ID='" + Info.shop_id + "' AND SALE_ID='" + Info.sale_id + "'";
            return base.RunSQL(sql);
        }
        ///// <summary>
        /////  完成对saletmp00的METHOD_ID字段的更新操作
        ///// </summary>
        ///// <param name="method_id">销售方法</param>
        ///// <param name="sale_id">销售号id</param>
        ///// <param name="shop_id">分店编号</param>
        ///// <returns>bool</returns>
        //public bool DataUpdateSaleTmp00(int method_id,string sale_id,string shop_id)
        //{
        //    DateTime datetime = DateTime.Now;
        //    try
        //    {
        //        string sql = "UPDATE SALETMP00 SET METHOD_ID=" + method_id + ",LAST_UPDATE='" + datetime + "' WHERE SHOP_ID='" + shop_id + "' AND SALE_ID='" + sale_id + "'";
        //        return base.RunSQL(sql);
        //    }
        //    catch 
        //    {
        //        string da = datetime.ToString("yyyy-MM-dd HH:mm:ss");
        //        string sql = "UPDATE SALETMP00 SET METHOD_ID=" + method_id + ",LAST_UPDATE='" + da + "' WHERE SHOP_ID='" + shop_id + "' AND SALE_ID='" + sale_id + "'";
        //        return base.RunSQL(sql);
        //    }
        //}
        #endregion

    }
}

