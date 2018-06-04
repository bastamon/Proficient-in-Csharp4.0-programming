using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using POS.Common;

//完成对SaleTmp02 的数据插入
/*主要公共方法：
（1）InsertDataSaleTmp02(string shop_id, string sale_id, int sale_sno, string pay_id, decimal amount,char transfer_status,DateTime last_update, decimal Face_Value)，完成对sertSaleTmp02的插入
*/
namespace POS.Service
{
    /// <summary>
    /// 完成对SaleTmp02 的数据插入
    /// </summary>
    class InsertSaleTmp02 : DBSql
    {
        /// <summary>
        /// 实例化sertSaleTmp02对象
        /// </summary>
        /// <returns>sertSaleTmp02对象</returns>
        public static InsertSaleTmp02 InitInsertSaleTmp02()
        {
            return new InsertSaleTmp02();
        }
        /// <summary>
        /// 完成对sertSaleTmp02的插入
        /// </summary>
        /// <param name="shop_id">分店编号</param>
        /// <param name="sale_id">销售编号</param>
        /// <param name="sale_sno">付款方式序号</param>
        /// <param name="pay_id">付款编号</param>
        /// <param name="amount">现金金额</param>
        /// <param name="transfer_status">传输状态</param>
        /// <param name="last_update">最后更新</param>
        /// <param name="Face_Value">现金面额</param>
        /// <returns>bool</returns>
        public bool InsertDataSaleTmp02(string shop_id, string sale_id, int sale_sno, string pay_id, decimal amount,char transfer_status,DateTime last_update, decimal Face_Value)
        {
            try
            {
                string sql = "INSERT INTO SALETMP02 (SHOP_ID,SALE_ID,SALE_SNO,PAY_ID,AMOUNT,TRANSFER_STATUS,LAST_UPDATE,Face_Value)VALUES ('" + shop_id + "','" + sale_id + "','" + sale_sno + "','" + pay_id + "'," + amount + ",'" + transfer_status + "','" + last_update + "'," + Face_Value + ")";
                return base.RunSQL(sql);
            }
            catch 
            {
                string datetime = last_update.ToString("yyyy-MM-dd HH:mm:ss");
                string sql = "INSERT INTO SALETMP02 (SHOP_ID,SALE_ID,SALE_SNO,PAY_ID,AMOUNT,TRANSFER_STATUS,LAST_UPDATE,Face_Value)VALUES ('" + shop_id + "','" + sale_id + "','" + sale_sno + "','" + pay_id + "'," + amount + ",'" + transfer_status + "','" + datetime + "'," + Face_Value + ")";
                return base.RunSQL(sql);
            }
        }

    }
}
