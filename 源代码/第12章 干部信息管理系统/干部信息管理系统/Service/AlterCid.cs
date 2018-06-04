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
    /// 1、配合修改cid操作。对应数据表TB_Commoninfo
    /// </summary>
    public class AlterCid
    {
        //上报端的CID
        private string cid_old;

        public string Cid_old
        {
            get { return cid_old; }
            set { cid_old = value; }
        }

        //新的CID
        private string cid_new;

        public string Cid_new
        {
            get { return cid_new; }
            set { cid_new = value; }
        }

        private string uid_old;

        public string Uid_old
        {
            get { return uid_old; }
            set { uid_old = value; }
        }
        private string uid_new;

        public string Uid_new
        {
            get { return uid_new; }
            set { uid_new = value; }
        }
        private DataTable table;

        public DataTable Table
        {
            get { return table; }
            set { table = value; }
        }
    }
}
