using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HBMISR.Service
{
    /// <summary>
    /// 属性类
    /// 功能：
    /// 1、配合的删除、恢复后备干部功能使用，存储cid和删除的顺序
    /// </summary>
    public class ListViewDel
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
