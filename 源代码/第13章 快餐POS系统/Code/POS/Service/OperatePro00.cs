using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data ;
using POS.Common;
// 从product00中获取与货号相关的信息 涉及表 ： product00
namespace POS.Service
{
    /// <summary>
    /// 从product00中获取与货号相关的信息
    /// </summary>
    class OperatePro00:DBSql
    {
        string sql;
        /// <summary>
        /// 从product00中获取与货号相关的信息
        /// </summary>
        /// <param name="str">商品编号</param>
        /// <returns>DataSet</returns>
        public DataSet pro_No(string str)
        {
            sql = "select PROD_NAME,POS_DISP,PRICE1 from product00 where prod_id='" + str + "' and enable=1 and OWNER_SHOP='"+Info.shop_id +"'";
            return base.CreateDataSet(sql);
        }

    }
}
