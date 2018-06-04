using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HNMS.PrintClass
{
    class testdata
    {
        private string myShortName;
        private string myLongName;

        public testdata(string strLongName, string strShortName)
        {
            this.myShortName = strShortName;
            this.myLongName = strLongName;
        }
        public string ShortName
        {
            get { return myShortName; }
        }
        public string LongName
        {
            get { return myLongName; }
        }
        public override string ToString()
        {

            return this.ShortName + "-" + this.LongName;
        }
    }
}
