using System;
using System.Data;
using POS.Data;
using POS.Common;
// 获取Sale00中 VIP卡收入，招待金额，员工餐饮，作废总额等相关数据  涉及表： sale00、sale01
namespace POS.Service
{
    /// <summary>
    /// 获取Sale00中 VIP卡收入，招待金额，员工餐饮，作废总额等相关数据
    /// </summary>
    class GetSale00_data:DBSql
    {

        /// <summary>
        /// VIP卡收入
        /// </summary>
        public string ReturenVIP_amount
        {
            get { return DataGetSale00_data.InitDataOperation().VIP_amount().Tables[0].Rows[0]["VIP_amount"].ToString(); }
        }

        /// <summary>
        ///  招待金额
        /// </summary>
        public string ReturenTreat_amount
        {
            get { return DataGetSale00_data.InitDataOperation().Treat_amount().Tables[0].Rows[0]["Treat_amount"].ToString(); }
        }

        /// <summary>
        /// 员工餐饮
        /// </summary>
        public string ReturenEmp_amount
        {
            get { return DataGetSale00_data.InitDataOperation().Emp_amount ().Tables[0].Rows[0]["Emp_amount"].ToString(); }
        }

        /// <summary>
        /// 作废总额
        /// </summary>
        public string ReturenWaste_amount
        {
            get { return DataGetSale00_data.InitDataOperation().Waste_amount ().Tables[0].Rows[0]["Waste_amount"].ToString(); }
        }


        ///// <summary>
        ///// 现金收入
        ///// </summary>
        //public string Money_income
        //{
        //    get { return DataGetSale00_data.InitDataOperation().Money_income ().Tables[0].Rows[0]["Money_income"].ToString(); }
        //}

    }
}
