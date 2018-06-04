using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using POS.Common;
//获得本班次的总促销金额、折扣金额 涉及表： SALE00、SALE01
namespace POS.Data
{
    /// <summary>
    /// 获得本班次的总促销金额、折扣金额
    /// </summary>
    class DataGetSale01_data : DBSql
    {
        /// <summary>
        /// 创建对象
        /// </summary>
        /// <returns>对象</returns>
        public static DataGetSale01_data InitDataOperation() { return new DataGetSale01_data(); }

        /// <summary>
        /// 促销金额
        /// </summary>
        /// <returns>DataSet</returns>
        public DataSet Promotion_amount()
        {
            return base.CreateDataSet("Select sum(SALE01.ITEM_DISC) as Promotion_amount  from SALE00,SALE01 where SALE00.SHOP_ID=SALE01.SHOP_ID and SALE00.SALE_ID=SALE01.SALE_ID	and SALE01.PRICE_TYPE='8' and SALE00.last_update between '" + Info.login_date + "' and '" + Info.exit_date + "'");

        }

        /// <summary>
        /// 折扣总额
        /// </summary>
        /// <returns>DataSet</returns>
        public DataSet Discount_amount()
        {
            return base.CreateDataSet("Select sum(SALE01.ITEM_DISC_TOT) as Discount_amount  from SALE00,SALE01 where SALE00.SHOP_ID=SALE01.SHOP_ID and SALE00.SALE_ID=SALE01.SALE_ID and (SALE01.PRICE_TYPE='1' or SALE01.PRICE_TYPE='2')  and SALE00.last_update between '" + Info.login_date + "' and '" + Info.exit_date + "'");
        }

       
    }
}
