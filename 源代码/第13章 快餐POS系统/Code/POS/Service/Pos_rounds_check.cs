using POS.Common;
using System.Data;
// 用于交班查询 涉及表： POS_ROUNDS、EMPLOYEE
/*主要公共方法：
（1）、pos_roundsInquire(string login_date, string exit_date)，查询交班相关的信息
*/
namespace POS.Service
{
    /// <summary>
    /// 交班查询类
    /// </summary>
    class Pos_rounds_check : DBSql
    {
        /// <summary>
        /// 查询交班相关的信息
        /// </summary>
        /// <param name="login_date">登陆时间</param>
        /// <param name="exit_date">退出时间</param>
        /// <returns>调用DBSql中CreateDataSet方法</returns>
        public DataSet pos_roundsInquire(string login_date, string exit_date)
        {
            return base.CreateDataSet("SELECT shift_num,login_date,exit_date,emp_name FROM POS_ROUNDS,EMPLOYEE WHERE POS_ROUNDS.USER_ID=EMPLOYEE.EMP_ID AND (LOGIN_DATE >= '" + login_date + "'  AND LOGIN_DATE<='" + exit_date + "' ) order by shift_num");
        }
    }
}
