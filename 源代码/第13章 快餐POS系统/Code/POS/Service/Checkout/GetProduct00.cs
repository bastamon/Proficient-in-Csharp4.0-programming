using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using POS.Common;
using System.Data;

//得到指定产品id号的产品名字
/*主要公共方法：
（1）GetProduct00Name(string prod_id)，指定产品id号的产品名字
*/
namespace POS.Service
{
    /// <summary>
    /// 指定产品id号的产品名字
    /// </summary>
    class GetProduct00 : DBSql 
    {
        /// <summary>
        /// 实例化GetProduct00
        /// </summary>
        /// <returns>GetProduct00的一个对象</returns>
        public static GetProduct00 InitGetProduct00()
        {
            return new GetProduct00();
        }
        /// <summary>
        /// 指定产品id号的产品名字
        /// </summary>
        /// <param name="prod_id">产品编号</param>
        /// <returns>名字</returns>
        public string GetProduct00Name(string prod_id)
        {
            string sql = "select PROD_NAME from PRODUCT00 where PROD_ID='" + prod_id + "'";
            DataSet dataSet = base.CreateDataSet(sql);
            return dataSet.Tables[0].Rows[0]["PROD_NAME"].ToString();

        }
    }
}
