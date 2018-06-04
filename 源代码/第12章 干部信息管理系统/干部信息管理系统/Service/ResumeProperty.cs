using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace HBMISR.Service
{
    /// <summary>
    /// 属性类
    /// 功能：
    /// 1、配合隔个人简历的数据操作，对应数据表TB_Commoninfo
    /// </summary>
    class ResumeProperty
    {
        private string id;


        public string Id
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
        private string betime;

        public string Betime
        {
            get { return betime; }
            set { betime = value; }
        }
        private string entime;

        public string Entime
        {
            get { return entime; }
            set { entime = value; }
        }
        private string content;

        public string Content
        {
            get { return content; }
            set { content = value; }
        }
    }
}
