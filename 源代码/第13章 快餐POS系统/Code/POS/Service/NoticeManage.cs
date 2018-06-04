using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using POS.Common;
using System.Data;
// 公告管理从本地数据库读出对应的公告管理数据 涉及表： MESSAGE00
/*主要公共方法：
（1）、getNotice ()，从本地数据库读出对应的公告管理数据
*/
namespace POS.Service
{
    class NoticeManage:DBSql
    {
        /// <summary>
        /// 公告管理（从本地数据库读出对应的公告管理数据）
        /// </summary>
        /// <returns></returns>
        public DataTable getNotice()
        {
            string sql = "SELECT * FROM MESSAGE00 WHERE (LAST_UPDATE =(SELECT MAX(LAST_UPDATE) FROM MESSAGE00)) and status!='3'";
            return base.CreateDataSet(sql).Tables[0];
        }
    }
}
