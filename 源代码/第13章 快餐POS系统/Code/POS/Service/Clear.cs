using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using POS.Common;
//用于清空配置文件中的交班信息 涉及表：saletmp00 、saletmp01、saletmp02
/*主要公共方法：
（1）、ClearTime()，如果满足条件，清空交班点时间
*/
namespace POS.Service
{
    /// <summary>
    /// 清空配置文件中的交班信息
    /// </summary>
    class Clear
    {
        ReadIni readIni = new ReadIni();
        /// <summary>
        /// 如果满足条件，清空交班点时间
        /// </summary>
        public void ClearTime()
        {
            //交班点
            int timePoint;
            //上次上线时间
            DateTime preLoginTime;
            //交班时间
            DateTime changeWorkTime;
            //本次登录时间
            DateTime nowLoginTime=DateTime .Now;

            try
            {
                preLoginTime = Convert.ToDateTime(readIni.ReadString("RepastErp", "preLoginTime"));
                //changeWorkTime = Convert.ToDateTime(readIni.ReadString("RepastErp", "changeWorkTime"));
                //timePoint = Convert.ToInt32(changeWorkTime.Hour);
            }
            catch { return; }

            //截取空格后的字符串
           // string s1 = changeWorkTime.ToString();
            //string s2 = s1.Substring(s1.IndexOf(" ") + 1);
            string s2 = readIni.ReadString("RepastErp", "changeWorkTime");
            timePoint = Convert.ToInt32(s2.Substring (0,2));
            if (timePoint >= 0 && timePoint <= 6)
            {
               
                //截取空格前的字符串
                string s3 = nowLoginTime.ToString();
                string s4 = s3.Substring(0,s3 .IndexOf (" "));
                string s = s2 +" "+ s4;
                //若交班时间是凌晨以后，则交班时间的年月日跟当前时间相同
                changeWorkTime = Convert.ToDateTime(s);
            }
            else
            {
                //截取空格前的字符串
                string s3 = preLoginTime.ToString();
                string s4 = s3.Substring(0, s3.IndexOf(" "));
                string s = s2 + " " + s4;
                //若交班时间是凌晨以前，则交班时间的年月日跟上次登录时间相同
                changeWorkTime = Convert.ToDateTime(s);
            }

            //如第二次上线时间大于交班时间
            if (DateTime.Compare(nowLoginTime, changeWorkTime) >= 0 && DateTime.Compare(changeWorkTime, preLoginTime) >= 0)
            {
                //清空配置文件config.ini中的班次
                readIni.WriteString("RepastErp", "workNumber", "0");
                //清空Info.ini配置文件中的交易号
                ReadIni readIni1 = new ReadIni("Info.ini");
                readIni1.WriteString("RepastErp", "deal_number", "1");
                ReadIni readIniInfo = new ReadIni("Info.ini");
                readIniInfo.WriteString("RepastErp", "isNormalOff", "true");
                string oldSaleId = readIniInfo.ReadString("RepastErp", "sale_id");
                try
                {
                    DBSql.SRunSQL("delete saletmp00 where sale_id=" + oldSaleId + "");
                    DBSql.SRunSQL("delete saletmp01 where sale_id=" + oldSaleId + "");
                    DBSql.SRunSQL("delete saletmp02 where sale_id=" + oldSaleId + "");
                }
                catch { }
            }
        }
    }
}
