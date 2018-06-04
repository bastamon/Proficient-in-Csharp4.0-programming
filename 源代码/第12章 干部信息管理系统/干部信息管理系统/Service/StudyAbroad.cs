using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HBMISR.Service
{
    /// <summary>
    /// 属性类
    /// 
    /// 配合海外学习做数据操作，对应TB_SAbord
    /// </summary>
    class StudyAbroad
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
        private string country="";

        public string Country
        {
            get { return country; }
            set { country = value; }
        }
        private string academy = "";

        public string Academy
        {
            get { return academy; }
            set { academy = value; }
        }
        private string degree = "";

        public string Degree
        {
            get { return degree; }
            set { degree = value; }
        }


    }
}
