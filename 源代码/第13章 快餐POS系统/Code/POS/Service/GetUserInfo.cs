using POS.Common;
using System.Data;
using POS.Data;
using System.Data.SqlClient;
using System.Text;
using System.Security.Cryptography;
using System;
// 验证帐号密码并返回帐号,密码,分店编号的值 涉及表： EMPLOYEE
/*主要公共方法：
（1）、UserInfo(string userName,string password)，
（2）、IsLogin()，通过判断数据集对象dataSet中是否为空来判断密码用户名是否正确（不为空：登录成功；为空：登陆失败）
主要属性：
（1）、ReturenEmp_Id，返回员工id
（2）、ReturenPass，返回员工密码
（3）、ReturenShop_id，返回分店编号
（4）、ReturenEmp_name，返回员工姓名
（5）、ReturenEmp_level，返回员工等级
*/
namespace POS.Service
{
    /// <summary>
    /// 验证帐号密码并返回帐号,密码,分店编号的值
    /// </summary>
    class GetUserInfo : DBSql
    {
        /// <summary>
        /// 结果集
        /// </summary>
        DataSet dataSet=new DataSet ();

        /// <summary>
        /// 根据用户名和密码获取用户信息
        /// </summary>
        /// <param name="userName">用户输入的用户名</param>
        /// <param name="password">用户输入的密码</param>
        public void UserInfo(string userName)
        {
            this.dataSet = DataGetUserInfo.InitDataGetUserInfo().UserInfo(userName);
        }

        /// <summary>
        /// 通过判断数据集对象dataSet中是否为空来判断密码用户名是否正确（不为空：登录成功；为空：登陆失败）
        /// </summary>
        /// <returns>返回布尔值 ture:登录成功  false:登陆失败</returns>
        public bool IsLogin(string pass)
        {
            if (dataSet != null && dataSet.Tables[0].Rows.Count > 0)
            {
                if (dataSet.Tables[0].Rows[0]["password"].ToString() == InfoToMD5(pass))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        #region 用MD5对信息加密
        /// <summary>
        /// 将给定信息进行MD5加密处理。
        /// </summary>
        /// <param name="info">加密前的信息</param>
        /// <returns>用MD5算法加密处理后的信息</returns>
        private string InfoToMD5(string info)
        {
            byte[] result = Encoding.Unicode.GetBytes(info);
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] output = md5.ComputeHash(result);
            string str = BitConverter.ToString(output).Replace("-", "");
            return str;
        }
        #endregion 

        /// <summary>
        /// 返回员工id
        /// </summary>
        public string ReturenEmp_Id
        {
            get 
            {
                if (this.dataSet != null)
                {
                    return this.dataSet.Tables[0].Rows[0]["emp_id"].ToString();
                }
                else
                {
                    return "";
                }
            }
        }
        /// <summary>
        /// 返回员工密码
        /// </summary>
        public string ReturenPass
        {
            get { return this.dataSet.Tables[0].Rows[0]["password"].ToString(); }
        }
        /// <summary>
        /// 返回分店编号
        /// </summary>
        public string ReturenShop_id
        {
            get 
            {
                if (this.dataSet != null)
                {
                    return this.dataSet.Tables[0].Rows[0]["shop_id"].ToString(); 
                }
                else
                {
                    return "";
                }
                
            }
        }
        /// <summary>
        /// 返回员工姓名
        /// </summary>
        public string ReturenEmp_name
        {
            get 
            {
                if (this.dataSet != null)
                {
                    return this.dataSet.Tables[0].Rows[0]["Emp_name"].ToString(); 
                }
                else
                {
                    return "";
                }
               
            }
        }
        /// <summary>
        /// 返回员工等级
        /// </summary>
        public string ReturenEmp_level
        {

            get {

                try
                {
                    if (this.dataSet != null)
                    {
                        if (this.dataSet.Tables[0].Rows[0]["emp_level"].ToString().Trim() == "")
                        {
                            return "7";
                        }
                        else
                        {
                            return this.dataSet.Tables[0].Rows[0]["emp_level"].ToString();
                        }
                    }
                    else
                    {
                        return "7";
                    }
                }
                catch
                {
                    return "7";
                } 
                
            }
        }
        /// <summary>
        /// 根据用户名从服务器的pos_rounds表中获取登录有关的信息
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns>DataSet</returns>
        public DataSet GetOnlineInfo(string userName)
        {
            SqlParameter[] para = new SqlParameter[1];
            para[0] = new SqlParameter("@USER_ID", SqlDbType.NVarChar);
            para[0].Value = userName;


            ReadIni readIni = new ReadIni("config.ini");
            string srvIp = readIni.ReadString("RepastErp", "txtServerIP");
            string srvPort = readIni.ReadString("RepastErp", "txtPort");
            string srvDBName = readIni.ReadString("RepastErp", "txtIPdataname");
            string srvUserName = readIni.ReadString("RepastErp", "txtFTPuser");
            string srvPassword = readIni.ReadString("RepastErp", "txtFTPpassword");
            string str = " select * from OPENDATASOURCE('SQLOLEDB','Data Source=" + srvIp + "," + srvPort + ";User ID=" + srvUserName + ";Password=" + srvPassword + "' )." + srvDBName + ".dbo.POS_ROUNDS where user_id=@USER_ID and exit_date is null";
            return DBSql.SCreateDataSet(str, para);

        }
    }
}
