using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HBMISR.Service
{
    /// <summary>
    /// 属性类
    /// 功能：
    /// 1、配合的培养措施数据操作，对应数据表TB_TrainExcise
    /// </summary>
    class Exercise
    {
        //参加培训和实践锻炼情况
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
        private string reportContent = "";

        public string ReportContent
        {
            get { return reportContent; }
            set { reportContent = value; }
        }
        private Int32 reportMatter;

        public Int32 ReportMatter
        {
            get { return reportMatter; }
            set { reportMatter = value; }
        }
    }
}
