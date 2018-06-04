using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HBMISR.Service
{
    /// <summary>
    /// 属性类
    /// 功能：
    /// 1、配合的非后备干部人员数据操作，对应数据表TB_NDialog和TB_NMeeting
    /// </summary>
    class InCommonInfo
    {
        int id = 0;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        string name;

        public string Name
        {
          get { return name; }
          set { name = value; }
        }
        bool qd = false;

        public bool Qd
        {
          get { return qd; }
          set { qd = value; }
        }

        string sex = "";

        public string Sex
        {
            get { return sex; }
            set { sex = value; }
        }


        string unitname = "";

        public string Unitname
        {
            get { return unitname; }
            set { unitname = value; }
        }
        string unitclass = "";

        public string Unitclass
        {
            get { return unitclass; }
            set { unitclass = value; }
        }

        string department;

        public string Department
        {
            get { return department; }
            set { department = value; }
        }

        string position;

        public string Position
        {
          get { return position; }
          set { position = value; }
        }

        int age = 0;

        public int Age
        {
            get { return age; }
            set { age = value; }
        }

        string birthday = "";

        public string Birthday
        {
            get { return birthday; }
            set { birthday = value; }
        }

        int a = 0;

        public int A
        {
            get { return a; }
            set { a = value; }
        }

        int b = 0;

        public int B
        {
            get { return b; }
            set { b = value; }
        }
        int c = 0;

        public int C
        {
            get { return c; }
            set { c = value; }
        }

        int d = 0;

        public int D
        {
            get { return d; }
            set { d = value; }
        }

    }
}
