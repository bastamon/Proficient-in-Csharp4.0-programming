using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using POS.Common;
using System.Data;
using POS.Data;
// 获取Sale01中 促销金额，折扣总额等相关数据  涉及表： SALE00、SALE01
namespace POS.Service
{
    /// <summary>
    /// 获取Sale01中 促销金额，折扣总额等相关数据
    /// </summary>
    class GetSale01_data : DBSql
    {

        /// <summary>
        /// 促销金额
        /// </summary>
        public string ReturenPromotion_amount
        {
            get { return DataGetSale01_data.InitDataOperation().Promotion_amount().Tables[0].Rows[0]["Promotion_amount"].ToString(); }
        }

        /// <summary>
        ///  折扣总额
        /// </summary>
        public string ReturenDiscount_amount
        {
            get { return DataGetSale01_data.InitDataOperation().Discount_amount().Tables[0].Rows[0]["Discount_amount"].ToString(); }
        }
    }
}
