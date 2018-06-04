using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using POS.Data;

// 往数据库中插入一个存储过程
namespace POS.Service
{
    /// <summary>
    /// 往数据库中插入一个存储过程
    /// </summary>
    class Insert
    {
        /// <summary>
        /// 得到Controller的一个对象
        /// </summary>
        /// <returns></returns>
        public static Insert InitController() { return new Insert();}
        /// <summary>
        /// 往数据库中插入一个存储过程
        /// </summary>
        /// <param name="sql">插入存储过程的SQL语句</param>
        /// <returns></returns>
        public bool InsertProc(string sql)
        {
            DataInsertProcCode dataInsertProc = new DataInsertProcCode();
            return dataInsertProc.InsertProc(sql);
        }    
    }
}
