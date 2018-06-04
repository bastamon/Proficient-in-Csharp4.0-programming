using POS.Common;
using System.Data;
//查询shift班次表中的相关信息
/*主要公共方法：
（1）Shift()，从数据库中获取shift表中的相关信息
*/
namespace POS.Data
{
    /// <summary>
    /// 查询shift表中的相关信息
    /// </summary>
    class DataGetShiftName : DBSql
    {
        /// <summary>
        /// 获得一个DataGetShiftName的一个对象
        /// </summary>
        /// <returns>DataGetShiftName的一个对象</returns>
        public static DataGetShiftName InitDataOperation() { return new DataGetShiftName(); }

        /// <summary>
        /// 从数据库中获取shift表中的相关信息
        /// </summary>
        /// <returns>返回调用基类DBSql的CreateDataSet方法</returns>
        public DataSet Shift()
        {
            return base.CreateDataSet("SELECT  SHIFT_ID,SHIFT_NAME,START_TIME,STOP_TIME FROM SHIFT");
        }
    }
}
