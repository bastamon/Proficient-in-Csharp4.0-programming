using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HBMISR.Service
{
    class Train
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
        private string reportContent;

        public string ReportContent
        {
            get { return reportContent; }
            set { reportContent = value; }
        }
        private string reportMatter;

        public string ReportMatter
        {
            get { return reportMatter; }
            set { reportMatter = value; }
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
        private string Content;

        public string Content1
        {
            get { return Content; }
            set { Content = value; }
        }
    }
}
