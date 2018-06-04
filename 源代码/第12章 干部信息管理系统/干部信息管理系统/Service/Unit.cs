using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HBMISR.Service
{
    /// <summary>
    /// 单位信息
    /// 功能
    /// 1、记录单位的相关信息
    /// </summary>
    class Unit
    {
        private string unitName;

        public string UnitName
        {
            get { return unitName; }
            set { unitName = value; }
        }


        private string unitKind;

        public string UnitKind
        {
            get { return unitKind; }
            set { unitKind = value; }
        }

        private string registTime;

        public string RegistTime
        {
            get { return registTime; }
            set { registTime = value; }
        }
    }
}
