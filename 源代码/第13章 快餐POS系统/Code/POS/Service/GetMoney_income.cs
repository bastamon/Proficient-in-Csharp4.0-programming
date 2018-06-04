using POS.Common;
using System.Data;
using POS.Data;
using System;
//用于获得现金收入：  涉及表： sale00
namespace POS.Service
{
    /// <summary>
    /// 获得现金收入
    /// </summary>
    class GetMoney_income : DBSql
    {
        /// <summary>
        /// 现金收入
        /// </summary>
        public string ReturenMoney_income
        {
            get { return DataGetMoney_income.InitDataOperation().Moneyincome().Tables[0].Rows[0]["Money_income"].ToString(); }
        }
       
    }
}
