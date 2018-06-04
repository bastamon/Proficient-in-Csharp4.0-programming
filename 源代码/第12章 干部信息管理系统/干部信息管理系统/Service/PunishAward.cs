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
    /// 1、配合的奖惩情况表的数据操作，对应数据表TB_Award
    /// </summary>
    class PunishAward
    {
        string cid = "";

        public string Cid
        {
            get { return cid; }
            set { cid = value; }
        }
        string awardClass = "";
        //奖惩类别
        public string AwardClass
        {
            get { return awardClass; }
            set { awardClass = value; }
        }

        string degree = "";
        //奖惩级别
        public string Degree
        {
            get { return degree; }
            set { degree = value; }
        }

        string time = "";
        //奖惩时间
        public string Time
        {
            get { return time; }
            set { time = value; }
        }

        string content = "";
        //奖惩内容
        public string Content
        {
            get { return content; }
            set { content = value; }
        }
    }
}
