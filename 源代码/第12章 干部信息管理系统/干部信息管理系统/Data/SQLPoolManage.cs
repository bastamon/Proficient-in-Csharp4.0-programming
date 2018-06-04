using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HBMISR.Data
{ 
    class SQLPoolManage
    { 
        //单例模型 
        public static readonly SQLPoolManage sqlPoolManage = new SQLPoolManage();     

        #region 属性 
        SQLPool poolOfSQL; 
        #endregion  

        #region 构造函数 
        /// <summary> 
        /// 初始化 
        /// </summary> 
        public SQLPoolManage() 
        { 
            this.poolOfSQL = new SQLPool(); 
        }

        private DataOperation da = new DataOperation();
        #endregion  

        #region 方法 
        /// <summary> 
        /// 将SQL语句加入SQL池中 
        /// </summary> 
        /// <param name="strSQL"></param> 
        public void PushSQL(string strSQL) 
        { 
            this.poolOfSQL.Push(strSQL); 
        }  

        /// <summary> 
        /// 每隔一段时间，触发ExecuteSQL 
        /// ExecuteSQL用于执行SQL池中的SQL语句 
        /// </summary> 
        /// <param name="obj"></param> 
        public void ExecuteSQL(object obj) 
        { 
            if (this.poolOfSQL.Count > 0) 
            { 
                string[] array = this.poolOfSQL.Clear();  

                //遍历array，执行SQL 
                for (int i = 0; i < array.Length; i++) 
                { 
                    if (array[i].ToString().Trim() != "") 
                    { 
                        try
                        {
                            da.OperateData_sql(array[i].ToString());
                        } 
                        catch {  } 
                    } 
                } 
            } 
        } 
        #endregion 
    } 
}