using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HBMISR.Service
{
    /// <summary>
    /// 属性类
    /// 功能：
    /// 1、配合后备干部排序功能的实现
    /// </summary>
    class Order
    {
        private string cid;

        public string Cid
        {
            get { return cid; }
            set { cid = value; }
        }

        private Int32 number;

        public Int32 Number
        {
            get { return number; }
            set { number = value; }
        }


    }
}
