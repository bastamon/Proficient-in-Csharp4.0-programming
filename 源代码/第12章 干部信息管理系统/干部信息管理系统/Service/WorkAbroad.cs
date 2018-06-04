using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace HBMISR.Service
{
    /// <summary>
    /// 国外工作
    /// 功能
    /// 1、记录后备干部关于国外工作相关信息
    /// </summary>
    class WorkAbroad
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
        private string country = "";

        public string Country
        {
            get { return country; }
            set { country = value; }
        }
        private string departmentPosition = "";

        public string DepartmentPosition
        {
            get { return departmentPosition; }
            set { departmentPosition = value; }
        }
        private string specialty = "";

        public string Specialty
        {
            get { return specialty; }
            set { specialty = value; }
        }



    }
}
