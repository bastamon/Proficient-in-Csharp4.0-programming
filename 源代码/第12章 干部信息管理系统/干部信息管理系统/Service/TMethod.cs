using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HBMISR.Service
{
    /// <summary>
    /// 系统设置
    /// 功能：
    /// 设定报告事项、报告内容
    /// </summary>
    class TMethod
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
        private Int32 options;

        public Int32 Options
        {
            get { return options; }
            set { options = value; }
        }
        private Boolean   explain;

        public Boolean Explain
        {
            get { return explain; }
            set { explain = value; }
        }
        private string note14;

        public string Note14
        {
            get { return note14; }
            set { note14 = value; }
        }
    }
}
