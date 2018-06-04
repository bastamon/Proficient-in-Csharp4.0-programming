using POS.Common;
using System.Data;
using System.Data.SqlClient;
//对EMPLOYEE中的查询: 登录时调用，验证用户信息 涉及表： EMPLOYEE
/*主要公共方法：
（1）UserInfo(string userName, string password)，对EMPLOYEE中的查询
*/
namespace POS.Data
{
    /// <summary>
    /// 对EMPLOYEE中的查询（登录时调用，验证用户信息）
    /// </summary>
    class DataGetUserInfo : DBSql
    {
        /// <summary>
        /// 获得一个DataGetUserInfo的一个对象
        /// </summary>
        /// <returns>DataGetUserInfo的一个对象</returns>
        public static DataGetUserInfo InitDataGetUserInfo() { return new DataGetUserInfo(); }

        /// <summary>
        /// 对EMPLOYEE中的查询
        /// </summary>
        /// <param name="userName">用户输入的用户名</param>m
        /// <returns>结果集 DataSet</returns>
        public DataSet UserInfo(string userName)
        {
            SqlParameter[] para = new SqlParameter[1];
            para[0] = new SqlParameter("@EMP_ID", SqlDbType.NVarChar);
            para[0].Value = userName;
            return base.CreateDataSet("SELECT EMP_ID,PASSword,shop_id,Emp_name,emp_level FROM EMPLOYEE where EMP_ID=@EMP_ID", para);
        }
    }
}
