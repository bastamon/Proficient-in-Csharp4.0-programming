using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HBMISR.Service
{
    /// <summary>
    /// 属性类
    /// 功能：
    /// 1、配合的熟悉外语语种数据操作，对应数据表TB_FamilarForeign
    /// </summary>
    class FLanguage
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
        private string foreignKind;

        public string ForeignKind
        {
            get { return foreignKind; }
            set { foreignKind = value; }
        }
        private string level;

        public string Level
        {
            get { return level; }
            set { level = value; }
        }
        private Int32 number;

        public Int32 Number
        {
            get { return number; }
            set { number = value; }
        }
    }
}
