using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using POS.Common;
//对SaleTmp02满足条件的记录删除
/*主要公共方法：
（1）DeleteDataSaleTmp02(string shop_id, string sale_id)，完成对SaleTmp02指定行的删除
*/
namespace POS.Service
{
    /// <summary>
    /// 对SaleTmp02中指定销售编号和分店编号记录的删除
    /// </summary>
    class DeleteSaleTmp02Row : DBSql
    {
        /// <summary>
        /// 实例化DeleteSaleTmp02Row
        /// </summary>
        /// <returns>DeleteSaleTmp02Row的一个对象</returns>
        public static DeleteSaleTmp02Row InitDeleteSaleTmp02Row()
        {
            return new DeleteSaleTmp02Row();
        }
        #region//公共方法
        /// <summary>
        /// 完成对SaleTmp02指定行的删除
        /// </summary>
        /// <param name="shop_id">分店编号</param>
        /// <param name="sale_id">销售单编号</param>
        /// <returns>bool类型变量</returns>
        public bool DeleteDataSaleTmp02(string shop_id, string sale_id)
        {
            string sql = "DELETE FROM SALETMP02 WHERE SHOP_ID = '" + shop_id + "' AND SALE_ID ='" + sale_id + "'";
            return base.RunSQL(sql);
        }
        #endregion
    }
}
