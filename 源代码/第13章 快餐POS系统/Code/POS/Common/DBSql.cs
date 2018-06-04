using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
//包含完成对数据库的所有基本操作：sql语句、存储过程等。程序配置信息 、操作Ini文件的类
namespace POS.Common
{
    /// <summary>
    /// 完成对数据库的所有基本操作,供访问数据库调用
    /// </summary>
    class DBSql
    {
        public  string connStr = Info.Constr;
        private SqlConnection connection = null;
        
        private SqlCommand command = null;
        private SqlDataReader datareader = null;

        private SqlDataAdapter dataadapter = null;
        private DataSet dataset = null;


        public DBSql()
        {

        }
        /// <summary>
        /// 带参数的构造函数，创建一个指定连接字符串的对象
        /// </summary>
        /// <param name="conStr">连接字符串</param>
        public DBSql(string conStr)
        {
            this.connStr = conStr;
        }

        /// <summary>
        /// 打开数据库连接对象 
        /// </summary>
        /// <returns>SqlConnection对象</returns>
        private SqlConnection CreateConnection()
        {
            try
            {
                this.connection = new SqlConnection(connStr);
                this.connection.Open();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return this.connection;
        }

        /// <summary>
        /// 关闭数据库连接对象
        /// </summary>
        private void CloseConnection()
        {
            try
            {
                if (connection != null)
                {
                    this.connection.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 创建SqlCommand对象
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <returns>SqlCommand对象</returns>
        private SqlCommand CreateCommand(string sql)
        {
            try
            {
                this.connection = this.CreateConnection();
                this.command = new SqlCommand(sql, this.connection);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return this.command;
        }

        /// <summary>
        /// 创建SqlDataReader对象
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <returns>SqlDataReader对象</returns>
        private SqlDataReader CreateDataReader(string sql)
        {
            try
            {
                this.command = this.CreateCommand(sql);
                this.datareader = this.command.ExecuteReader();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return this.datareader;
        }

        /// <summary>
        /// 创建SqlDataReader对象
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="flag">是否返回列主键信息 true:返回 false: 不返回</param>
        /// <returns>SqlDataReader对象</returns>
        private SqlDataReader CreateDataReader(string sql, bool flag)
        {
            try
            {
                this.command = this.CreateCommand(sql);
                if(flag==true)
                    this.datareader = this.command.ExecuteReader(CommandBehavior.KeyInfo | CommandBehavior.SchemaOnly);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return this.datareader;
        }

        /// <summary>
        /// 关闭SqlDataReader对象
        /// </summary>
        private void CloseDataReader()
        {
            try
            {
                if (datareader != null)
                {
                    this.datareader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 创建SqlDataAdapter对象
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <returns>SqlDataAdapter对象</returns>
        private SqlDataAdapter CreateDataAdapter(string sql)
        {
            try
            {
                this.connection = this.CreateConnection();
                this.dataadapter = new SqlDataAdapter(sql,this.connection );
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return this.dataadapter;
        }

        /// <summary>
        /// 创建结果集对象
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <returns>DataSet对象</returns>
        public DataSet CreateDataSet(string sql)
        {
            try
            {
                this.dataadapter = this.CreateDataAdapter(sql);
                this.dataset = new DataSet();
                this.dataadapter.Fill(this.dataset);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.CloseDataReader();
                this.CloseConnection();
            }
            return this.dataset;
        }

        /// <summary>
        /// 创建结果集对象--静态方法
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <returns>DataSet对象</returns>
        public static DataSet SCreateDataSet(string sql)
        {
            DBSql dsSql = new DBSql();
            try
            {
                dsSql.dataadapter = dsSql.CreateDataAdapter(sql);
                dsSql.dataset = new DataSet();
                dsSql.dataadapter.Fill(dsSql.dataset);
            }
            catch
            {
                return null;
            }
            finally
            {
                dsSql.CloseDataReader();
                dsSql.CloseConnection();
            }
            return dsSql.dataset;
        }


        /// <summary>
        /// 创建结果集对象
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="sp">参数数组</param>
        /// <returns>DataSet结果集</returns>
        public DataSet CreateDataSet(string sql, SqlParameter[] sp)
        {
            try
            {
                
                this.dataadapter = this.CreateDataAdapter(sql );
                if (sp != null)
                {
                    for (int i = 0; i < sp.Length; i++)
                    {
                        this.dataadapter.SelectCommand.Parameters.Add(sp[i]);//将参数加到Command中
                    }
                }
                this.dataset = new DataSet();
                this.dataadapter.Fill(this.dataset);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                this.CloseDataReader();
                this.CloseConnection();
            }
            return this.dataset;
        }


        /// <summary>
        /// 创建结果集对象--静态方法
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="sp">参数数组</param>
        /// <returns>DataSet结果集</returns>
        public static DataSet SCreateDataSet(string sql, SqlParameter[] sp)
        {
            DBSql dsSql = new DBSql();
            try
            {

                dsSql.dataadapter = dsSql.CreateDataAdapter(sql);
                if (sp != null)
                {
                    for (int i = 0; i < sp.Length; i++)
                    {
                        dsSql.dataadapter.SelectCommand.Parameters.Add(sp[i]);//将参数加到Command中
                    }
                }
                dsSql.dataset = new DataSet();
                dsSql.dataadapter.Fill(dsSql.dataset);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                dsSql.CloseDataReader();
                dsSql.CloseConnection();
            }
            return dsSql.dataset;
        }



        /// <summary>
        /// 执行更新操作
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <returns>bool</returns>
        public bool RunSQL(string sql)
        {
            try
            {
                this.command = this.CreateCommand(sql);
                if (this.command.ExecuteNonQuery() > 0)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
                throw ex;
            }
            finally
            {
                this.CloseConnection();
            }
            return false;
        }

        /// <summary>
        /// 执行更新操作--静态方法
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <returns>bool</returns>
        public static bool SRunSQL(string sql)
        {
            DBSql dbSql = new DBSql();
            try
            {
                dbSql.command = dbSql.CreateCommand(sql);
                if (dbSql.command.ExecuteNonQuery() > 0)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
                throw ex;
            }
            finally
            {
                dbSql.CloseConnection();
            }
            return false;
        }


        /// <summary>
        /// 执行更新操作--静态方法
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <returns>bool</returns>
        public static bool STRunSQL(string sql)
        {
            DBSql dbSql = new DBSql();
            try
            {
                dbSql.command = dbSql.CreateCommand(sql);
                dbSql.command.ExecuteNonQuery();
                return true;
                
            }
            catch (Exception ex)
            {
                return false;
                throw ex;
            }
            finally
            {
                dbSql.CloseConnection();
            }
        }


        /// <summary>
        /// 执行带参数的SQL语句
        /// </summary>
        /// <param name="cmdText">sql语句</param>
        /// <param name="sp">SqlParameter数组</param>
        public  bool RunSQL(string cmdText, SqlParameter[] sp)
        {
            try
            {
                this.command = this.CreateCommand(cmdText);
                if (sp != null)
                {
                    for (int i = 0; i < sp.Length; i++)
                    {
                        command.Parameters.Add(sp[i]);//将参数加到Command中
                    }
                } 
                command.ExecuteNonQuery();
                return true;
            }
            catch (SqlException ex)
            {
                return false;
                throw ex;
            }
            finally
            {
                this.CloseConnection();
            }
        }


        /// <summary>
        /// 执行带参数的SQL语句--静态方法
        /// </summary>
        /// <param name="cmdText">sql语句</param>
        /// <param name="sp">SqlParameter数组</param>
        public static  bool SRunSQL(string cmdText, SqlParameter[] sp)
        {
            DBSql dbSql = new DBSql();
            try
            {
                dbSql.command = dbSql.CreateCommand(cmdText);
                if (sp != null)
                {
                    for (int i = 0; i < sp.Length; i++)
                    {
                        dbSql.command.Parameters.Add(sp[i]);//将参数加到Command中
                    }
                }
                dbSql.command.ExecuteNonQuery();
                return true;
            }
            catch (SqlException ex)
            {
                return false;
                throw ex;
            }
            finally
            {
                dbSql.CloseDataReader();
                dbSql.CloseConnection();
            }
        }
    }

}
