using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using POS.Common;
using System.Data;
//查询Payment表的相关记录并返回一个DataSet变量
/*类说明：执行对tPayment表的查询（获取付款面值的按钮信息）
主要公共方法：
（1）、GetPayment(),查询tPayment表并得到一个结果集，限制条件：VISABLE=1
*/
namespace POS.Data
{
    /// <summary>
    /// 执行对tPayment表的查询（获取付款面值的按钮信息）
    /// </summary>
    class DataGetPayment : DBSql
    {
        /// <summary>
        /// 实例化DataGetPayment
        /// </summary>
        /// <returns>DataGetPayment的一个对象</returns>
        public static DataGetPayment InitDataGetPayment()
        {
            return new DataGetPayment();
        }
        /// <summary>
        /// 查询tPayment表
        /// </summary>
        /// <returns>结果集</returns>
        public DataSet GetPayment()
        {
            string sql = "select * from PAYMENT where VISABLE="+1+"";
            return base.CreateDataSet(sql);
        }
    }
}
