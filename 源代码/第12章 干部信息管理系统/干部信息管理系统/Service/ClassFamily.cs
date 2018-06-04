using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HBMISR.Service
{
    class ClassFamily
    {
        string cid = "";

        public string Cid
        {
            get { return cid; }
            set { cid = value; }
        }

        string call = "";
        /// <summary>
        /// 称呼
        /// </summary>
        public string Call
        {
            get { return call; }
            set { call = value; }
        }

        string name ="";
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        int age = 0;
        /// <summary>
        /// 年龄
        /// </summary>
        public int Age
        {
            get { return age; }
            set { age = value; }
        }

        string birthday = "";
        /// <summary>
        /// 出生年月
        /// </summary>
        public string Birthday
        {
            get { return birthday; }
            set { birthday = value; }
        }

        string country = "";
        /// <summary>
        /// 国籍
        /// </summary>
        public string Country
        {
            get { return country; }
            set { country = value; }
        }

        string partyClass = "";
        /// <summary>
        /// 党派
        /// </summary>
        public string PartyClass
        {
            get { return partyClass; }
            set { partyClass = value; }
        }

        string nation = "";
        /// <summary>
        /// 民族
        /// </summary>
        public string Nation
        {
            get { return nation; }
            set { nation = value; }
        }

        string workPostion = "";
        /// <summary>
        /// 工作单位及职务
        /// </summary>
        public string WorkPostion
        {
            get { return workPostion; }
            set { workPostion = value; }
        }

        string remark = "";
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            get { return remark; }
            set { remark = value; }
        }
     }
}
