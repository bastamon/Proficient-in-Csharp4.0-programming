using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;

namespace HBMISR.Data
{
    /// <summary>
    /// 数据库连接类
    /// </summary>
    class MySQLiteConnection
    {

        private static SQLiteConnection conn;
        private string datasource;

        #region
        /// <summary>
        /// 构造有参无参函数，初始化.db文件的路径
        /// </summary>
        /// <param name="data"></param>
        public MySQLiteConnection()
        {

        }
        public void SetPath(string datasource)
        {
            this.datasource = datasource;
        }

        /// <summary>
        /// 获取数据源路径
        /// </summary>
        /// <param name="datasource">路径</param>
        public MySQLiteConnection(string datasource)
        {
            this.datasource = datasource;
        }
        #endregion

        #region
        /// <summary>
        /// 获得指定路径的.db文件的数据源连接
        /// </summary>
        /// <returns></returns>
        public SQLiteConnection GetSQLiteConnection()
        {
            string connstr = string.Format("Data Source={0}", datasource);
            try
            {
                conn = new SQLiteConnection(connstr);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return conn;
        }

        /// <summary>
        /// 与数据源获得连接
        /// </summary>
        /// <param name="datasource">数据源路径</param>
        /// <returns>返回连接</returns>
        public SQLiteConnection GetSQLiteConnection(String datasource)
        {
            this.datasource = datasource;

            string connstr = string.Format("Data Source={0}", datasource);
            try
            {
                conn = new SQLiteConnection(connstr);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return conn;
        }
        #endregion
    }
}
