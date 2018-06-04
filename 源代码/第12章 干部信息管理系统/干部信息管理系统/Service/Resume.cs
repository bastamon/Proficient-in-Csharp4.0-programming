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
    /// 1、配合隔个人简历的数据操作，对应数据表TB_Commoninfo
    /// </summary>
    class Resume
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
        private string startTime;


        public string StartTime
        {
            get { return startTime; }
            set { startTime = value; }
        }
        private string endTime;

        public string EndTime
        {
            get { return endTime; }
            set { endTime = value; }
        }
        private string content;

        public string Content
        {
            get { return content; }
            set { content = value; }
        }
        private string[] resumeLine;

        private  string[] ResumeLine
        {
            get { return resumeLine; }
            set { resumeLine = value; }
        }


        public  DataTable table;

        public DataTable Table
        {
            get { return table; }
            set { table = value; }
        }


        

    }
}
