using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HBMISR.Service
{
    /// <summary>
    /// 属性类
    /// 功能：
    /// 1、该类对应后备干部考察材料的打印预览
    /// </summary>
    class Class_HBData
    {
        private string CID;//编号
        public string CID1
        {
            get { return CID; }
            set { CID = value; }
        }

        private string HBName;//姓名
        public string HBName1
        {
            get { return HBName; }
            set { HBName = value; }
        }

        private string HBmaterial;//考察材料
        public string HBmaterial1
        {
            get { return HBmaterial; }
            set { HBmaterial = value; }
        }

    }
}
