using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Data.SQLite;

namespace HBMISR.Data
{
    /// <summary>
    /// 操作单位的类
    /// 功能：
    /// 1、检测注册单位是否在冲突，不冲突返回True,冲突返回False
    /// 2、在插入单位时，检测本地
    /// </summary>
    class UnitOperation
    {
        private string filepath;

        /// <summary>
        /// 路径
        /// </summary>
        public string Filepath
        {
            get { return filepath; }
            set { filepath = value; }
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        public UnitOperation()
        {

        }

        /// <summary>
        /// 判断传递过来的单位名称，是否和TB_UNIT中的单位名称冲突，如果不冲突返回true
        /// </summary>
        /// <param name="unitName"></param>
        /// <returns></returns>
        public bool CheckUnit(string unitName)
        {
            bool b = true;
            DataOperation opera1 = new DataOperation(Application.StartupPath + "\\DB\\DBHBMSU.db");
            string sql = "select unitName from TB_UNIT";


            DataTable dt = opera1.GetOneDataTable_sql(sql);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["unitName"].Equals(unitName))
                {
                    b = false;
                    break;
                }
            }
            return b;
        }
       
        /// <summary>
        /// 判断传递过来的单位名称，是否和TB_InputInfo中的单位名称冲突，如果不冲突返回true
        /// </summary>
        /// <param name="unitName"></param>
        /// <returns></returns>
        public bool CheckUnit_System(string unitName)
        {
            bool b = true;
            DataOperation opera1 = new DataOperation();
            string sql = "select unitName from TB_InputInfo";


            DataTable dt = opera1.GetOneDataTable_sql(sql);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["unitName"].Equals(unitName))
                {
                    b = false;
                    break;
                }
            }
            return b;
        }

        /// <summary>
        /// 向TB_LocalUnit中插入单位的相关的信息
        /// </summary>
        /// <param name="unitName">单位名称</param>
        /// <param name="registTime">注册时间</param>
        /// <param name="unitClass">单位类别</param>
        /// <returns></returns>
        public bool insterNewUnit(string unitName, string registTime, string unitClass)
        {
            MySQLiteConnection myconn = new MySQLiteConnection(filepath);
            SQLiteConnection conn = myconn.GetSQLiteConnection();
            try
            {
                conn.Open();
                string sql2 = "insert into TB_LocalUnit(unitName,registTime,unitClass) VALUES(@unitName1,@registTime1,@unitClass1)";
                using (SQLiteCommand cmd = new SQLiteCommand(sql2, conn))
                {
                    cmd.Parameters.Add("@unitName1", System.Data.DbType.String).Value = unitName;
                    cmd.Parameters.Add("@registTime1", System.Data.DbType.String).Value = registTime;
                    cmd.Parameters.Add("@unitClass1", System.Data.DbType.String).Value = unitClass;
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
                conn.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 向DBHBMSU.db中的TB_UNIT中插入单位名称、单位类别、单位时间
        /// </summary>
        /// <param name="unitName"></param>
        /// <param name="registTime"></param>
        /// <param name="unitClass"></param>
        /// <returns></returns>
        public bool insterNewUnit_Globle(string unitName, string registTime, string unitClass)
        {
            MySQLiteConnection myconn = new MySQLiteConnection(Application.StartupPath + "\\DB\\DBHBMSU.db");
            SQLiteConnection conn = myconn.GetSQLiteConnection();
            try
            {
                conn.Open();
                string sql = "insert into TB_UNIT(unitName,registTime,unitClass) VALUES(@unitName1,@registTime1,@unitClass1)";
                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.Add("@unitName1", System.Data.DbType.String).Value = unitName;
                    cmd.Parameters.Add("@registTime1", System.Data.DbType.String).Value = registTime;
                    cmd.Parameters.Add("@unitClass1", System.Data.DbType.String).Value = unitClass;
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
                conn.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
