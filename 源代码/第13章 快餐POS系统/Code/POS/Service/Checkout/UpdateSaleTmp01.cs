using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using POS.Common;
//完成对saletmp01表使用兑换券的记录更新
/*主要公共方法：
（1）DataUpDateSaleTmp01(string prod_id,decimal item_disc,decimal item_disc_tot,string sale_id,string shop_id)，完成对saletmp01中的相应字段的更新
（2）DataUpDataSaleTmp01(string prod_id,string sale_id,string shop_id)，撤销之前对saletmp01中对BY_TOKEN，ITEM_DISC，ITEM_DISC_TOT字段的更改
（3）RecoverSaleTmp01(string shop_id, string sale_id)，断电恢复结账对saletmp01销售单的更改
*/
namespace POS.Service
{
    /// <summary>
    /// 完成对saletmp01表使用兑换券的记录更新
    /// </summary>
    class UpdateSaleTmp01 : DBSql
    {
        /// <summary>
        /// 返回UpdateSaleTmp01一个对象
        /// </summary>
        /// <returns>UpdateSaleTmp01</returns>
        public static UpdateSaleTmp01 InitUpdateSaleTmp01()
        {
            return new UpdateSaleTmp01();
        }
        /// <summary>
        /// 完成对saletmp01中的相应字段的更新
        /// </summary>
        /// <param name="prod_id">商品id</param>
        /// <param name="item_disc">折让金额</param>
        /// <param name="item_disc_tot">总折让金额</param>
        /// <param name="sale_id">销售单id</param>
        /// <param name="shop_id">分店id</param>
        /// <returns>bool</returns>
        public bool DataUpDateSaleTmp01(string prod_id,decimal item_disc,decimal item_disc_tot,string sale_id,string shop_id)
        {
            try
            {
                string sql = "UPDATE SALETMP01 SET BY_TOKEN = " + 1 + " ,ITEM_DISC=" + item_disc + ", ITEM_DISC_TOT=" + item_disc_tot + " WHERE SHOP_ID='" + shop_id + "' AND SALE_ID='" + sale_id + "'AND PROD_ID = '" + prod_id + "'";
                return base.RunSQL(sql);
            }
            catch { return false; }
        }
        /// <summary>
        /// 撤销之前对saletmp01中对BY_TOKEN，ITEM_DISC，ITEM_DISC_TOT字段的更改
        /// </summary>
        /// <param name="prod_id">商品id</param>
        /// <param name="sale_id">销售单id</param>
        /// <param name="shop_id">分店id</param>
        /// <returns>bool</returns>
        public bool DataUpDataSaleTmp01(string prod_id,string sale_id,string shop_id)
        {
            try
            {
                string sql = "UPDATE SALETMP01 SET BY_TOKEN = " + 0 + " ,ITEM_DISC=" + 0 + ", ITEM_DISC_TOT=" + 0 + " WHERE SHOP_ID='" + shop_id + "' AND SALE_ID='" + sale_id + "'AND PROD_ID = '" + prod_id + "'";
                return base.RunSQL(sql);
            }
            catch { return false; }
        }
        /// <summary>
        /// 断电恢复结账对saletmp01销售单的更改
        /// </summary>
        /// <param name="shop_id">分店id</param>
        /// <param name="sale_id">销售单id</param>
        /// <returns>bool</returns>
        public bool RecoverSaleTmp01(string shop_id, string sale_id)
        {
            try
            {
                string sql = "UPDATE SALETMP01 SET BY_TOKEN = " + 0 + " ,ITEM_DISC=" + 0 + ", ITEM_DISC_TOT=" + 0 + " WHERE SHOP_ID='" + shop_id + "' AND SALE_ID='" + sale_id + "'AND BY_TOKEN = " + 1 + " ";
                return base.RunSQL(sql);
            }
            catch { return false; }
        }
    }
}
