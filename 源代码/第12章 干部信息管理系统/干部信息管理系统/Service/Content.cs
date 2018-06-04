using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HBMISR.Service
{
    /// <summary>
    /// 属性类
    /// 功能：
    /// 1、配合重大报告事项的数据操作，对应数据表TB_GreatContent
    /// </summary>
    class Content1
    {

        private Int32 id;

        public Int32 Id
        {
            get { return id; }
            set { id = value; }
        }
        private string cid;

        public string Cid
        {
            get { return cid; }
            set { cid = value; }
        }
        private string content = "";

        public string Content
        {
            get { return content; }
            set { content = value; }
        }
        private Int32 matter;

        public Int32 Matter
        {
            get { return matter; }
            set { matter = value; }
        }

    }
}
