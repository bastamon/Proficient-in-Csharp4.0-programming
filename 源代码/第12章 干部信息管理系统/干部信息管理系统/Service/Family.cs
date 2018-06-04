using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace HBMISR.Service
{
    /// <summary>
    /// 属性类
    /// 功能：
    /// 1、配合家庭成员数据操作，对应数据表TB_Family
    /// </summary>
    class Family
    {
        private string strSql;

        public string StrSql
        {
            get { return strSql; }
            set { strSql = value; }
        }
        private string cid;

        public string Cid
        {
            get { return cid; }
            set { cid = value; }
        }

        private DataTable table;

        public DataTable Table
        {
            get { return table; }
            set { table = value; }
        }





    }
}
