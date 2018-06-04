using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using POS.Common;
// 往数据库中插入一个指定的存储过程
/*主要方法：
（1）InsertProc(string procCode) 往数据库中添加一个存储过程
 */
namespace POS.Data
{
    /// <summary>
    /// 往数据库中插入一个指定的存储过程
    /// </summary>
    class DataInsertProcCode:DBSql
    {
        
        /// <summary>
        /// 往数据库中插入一个存储过程
        /// </summary>
        /// <param name="procCode">插入存储过程的SQL语句</param>
        /// <returns>bool：（ true 成功 false 失败）</returns>
        public bool InsertProc(string procCode)
        {
            return base.RunSQL(procCode);
        }
    }
}
