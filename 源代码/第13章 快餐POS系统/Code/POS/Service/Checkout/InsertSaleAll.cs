using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using POS.Common;
using System.Data;
using System.Data.SqlClient;
//1.完成对saletmp00、saletmp01、saletmp02、saletmp03、向salet00、salet01、salet02、salet03的数据插入
//2.完成对saletmp00、saletmp01、saletmp02、saletmp03的清空操作
/*主要公共方法：
（1）、InSertSale00Data(string sale_id,string shop_id)，完成对sale00的插入以及对salemp00的清除
（2）、Sale00TransferBack()，返回sale00中的行数
（3）、InSertSale01Data(string sale_id, string shop_id)，完成对sale01的插入以及对salemp01的清除
（4）、InSertSale02Data(string sale_id, string shop_id)，完成对sale02的插入以及对salemp02的清除
（5）、UpdateSaleTmp00(string sale_id, string shop_id, decimal change,UInt16 method_id)，更新saletmp00表
*/
namespace POS.Service
{
    /// <summary>
    /// 完成对saletmp00、saletmp01、saletmp02、saletmp03、向salet00、salet01、salet02、salet03的数据插入
    /// 及其对完成对saletmp00、saletmp01、saletmp02、saletmp03的数据清空操作
    /// </summary>
    class InsertSaleAll : DBSql
    {
        /// <summary>
        /// 实例InsertSaleAll的一个对象
        /// </summary>
        /// <returns>InsertSaleAll</returns>
        public static InsertSaleAll InitInsertSaleAll()
        {
            return new InsertSaleAll();
        }
        #region//公共方法完成对表的操作
        /// <summary>
        /// 完成对sale00的插入以及对salemp00的清除
        /// </summary>
        /// <returns>bool类型</returns>
        public bool InSertSale00Data(string sale_id,string shop_id)
        {
            string sql = "insert into SALE00 select * from SALETMP00 where shop_id='" + shop_id + "'and sale_id='" +sale_id + "'";
            base.RunSQL(sql);
            sql = "delete from SALETMP00 where shop_id='" + shop_id + "'and sale_id='" + sale_id + "'";
            return base.RunSQL(sql);
        }
        /// <summary>
        /// 返回sale00中的行数
        /// </summary>
        /// <returns>int</returns>
        public int Sale00TransferBack()
        {
            int count=0;
            string sql = "select sale_id from sale00 where TRANSFER_STATUS='0' ";
            try
            {
                return count = base.CreateDataSet(sql).Tables[0].Rows.Count;
            }
            catch { return count = 0; }           
        }
        /// <summary>
        /// 完成对sale01的插入以及对salemp01的清除
        /// </summary>
        /// <returns>bool类型</returns>
        public bool InSertSale01Data(string sale_id, string shop_id)
        {
           
            string sql = "insert into SALE01 select * from SALETMP01 where shop_id='" + shop_id + "'and sale_id='" + sale_id + "';";
            base.RunSQL(sql);
            sql = "delete from SALETMP01 where shop_id='" + shop_id + "'and sale_id='" + sale_id + "'";
            return base.RunSQL(sql);
        }
        /// <summary>
        /// 完成对sale02的插入以及对salemp02的清除
        /// </summary>
        /// <returns>bool类型</returns>        
        public bool InSertSale02Data(string sale_id, string shop_id)
        {
            string sql = "insert into SALE02 select * from SALETMP02 where shop_id='" + shop_id + "'and sale_id='" + sale_id + "'";
            base.RunSQL(sql);
            sql = "delete from SALETMP02 where shop_id='" + shop_id + "'and sale_id='" + sale_id + "'";
            return base.RunSQL(sql);
        }
        ///// <summary>
        ///// 完成对sale03的插入以及对salemp03的清除
        ///// </summary>
        ///// <returns>bool类型</returns>
        //public bool InSertSale03Data(string sale_id, string shop_id)
        //{
        //    string sql = "insert into SALE03 select * from SALETMP03 where shop_id='" +shop_id + "'and sale_id='" + sale_id + "'";
        //    base.RunSQL(sql);
        //    sql = "delete from SALETMP03 where shop_id='" + shop_id + "'and sale_id='" + sale_id + "'";
        //    return base.RunSQL(sql);
        //}
        /// <summary>
        /// 更新saletmp00表
        /// </summary>
        /// <param name="sale_id">销售编号</param>
        /// <param name="shop_id">分店号</param>
        /// <param name="change">找零</param>
        /// <param name="method_id">销售方法</param>
        /// <returns>bool</returns>
        public bool UpdateSaleTmp00(string sale_id, string shop_id, decimal change,UInt16 method_id,decimal tot_sales)
        {
            DateTime datetime =DateTime.Now;
            try
            {
                string sql = "UPDATE SALETMP00 SET  TOT_SALES="+tot_sales+",METHOD_ID=" + method_id + ",CHANGE=" + change + " ,LAST_UPDATE='" + datetime + "' where SALE_ID='" + sale_id + "' AND SHOP_ID='" + shop_id + "'";
            return base.RunSQL(sql);
            }
            catch
            {
                string da=datetime.ToString("yyyy-MM-dd HH:mm:ss");
                string sql = "UPDATE SALETMP00 SET TOT_SALES=" + tot_sales + ",METHOD_ID=" + method_id + ",CHANGE=" + change + " ,LAST_UPDATE='" + da + "' where SALE_ID='" + sale_id + "' AND SHOP_ID='" + shop_id + "'";
            return base.RunSQL(sql);
            }
        }
       /// <summary>
       /// 用于员工餐和招待对saletmp00表的更新
       /// </summary>
       /// <param name="sale_id">销售单号</param>
       /// <param name="shop_id">分店编号</param>
       /// <param name="type">结账类型</param>
       /// <returns>bool</returns>
        public bool UpdateSaleTmp00(string sale_id, string shop_id,string type)
        {
            string datetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            try
            { 
                string sql="";
                if (type.Equals("招待"))
                {
                    sql = "UPDATE SALETMP00 SET MEAL_KIND=" + 2 + ",TOT_SALES=" + 0 + ",LAST_UPDATE='" + datetime + "' where SALE_ID='" + sale_id + "' AND SHOP_ID='" + shop_id + "'";
                   
                }
                else if (type.Equals("员工餐"))
                {
                    sql = "UPDATE SALETMP00 SET MEAL_KIND=" + 1 + ",TOT_SALES=" + 0 + ",LAST_UPDATE='" + datetime + "' where SALE_ID='" + sale_id + "' AND SHOP_ID='" + shop_id + "'";
                } 
                return base.RunSQL(sql);
            }
            catch { return false; }
        }
        /// <summary>
        /// 对saletmp01表的更新
        /// </summary>
        /// <param name="sale_id">销售单号</param>
        /// <param name="shop_id">分店编号</param>
        /// <param name="emp_id">销售员编号</param>
        /// <param name="type">结账类型</param>
        /// <returns>bool</returns>
        public bool UpdateSaleTmp01(string sale_id, string shop_id, string emp_id, string type)
        {
            try
            {
                string sql = "";
                if (type.Equals("招待"))
                {
                    sql = "UPDATE SALETMP01 SET PRICE_TYPE=" + 5 + ",FREE_EMP='" + emp_id + "',ACT_PRICE=0,item_disc=(case when comb_type='2' then 0 else -sale_price*qty end),item_disc_tot=-sale_orginal_price*qty where SALE_ID='" + sale_id + "' AND SHOP_ID='" + shop_id + "'";

                }
                else if (type.Equals("员工餐"))
                {
                    sql = "UPDATE SALETMP01 SET PRICE_TYPE="+7+",FREE_EMP='" + emp_id + "' where SALE_ID='" + sale_id + "' AND SHOP_ID='" + shop_id + "'";
                }
                return base.RunSQL(sql);
            }
            catch { return false; }
        }
        #endregion
    }
}
   