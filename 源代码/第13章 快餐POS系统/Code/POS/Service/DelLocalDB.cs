using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using POS.View;
using POS.Common;
using System.Data;
//按照要求删除本地数据库中的数据
/*主要公共方法：
（1）、DelLocalData ()，删除本地数据库中的数据
（2）、DelSaletmp()删除临时表中符合记录的数据
*/
namespace POS.Service
{
    /// <summary>
    /// 按照要求删除本地数据库中的数据
    /// </summary>
    class DelLocalDB
    {
        private MainForm mainForm;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="mainForm">主窗体</param>
        public DelLocalDB(MainForm mainForm)
        {
            this.mainForm = mainForm;
        }

        /// <summary>
        /// 按照要求删除本地数据库中的数据
        /// </summary>
        /// <returns>成功删除返回true</returns>
        public bool DelLocalData()
        {
            DataSet ds = DBSql.SCreateDataSet("select * from pos_transfer where transfer_mode='out' or transfer_mode='shift'");
            bool b = false;
            ReadIni readIni = new ReadIni("config.ini");
            int day;
            try
            {
                day = Convert.ToInt32(mainForm.OperPara.GetIniConfig("clearDay"));
                if (day < 1)
                {
                    day = 7;
                }
            }
            catch { day = 7; }

            DateTime d = DateTime.Now;
            string str;
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                try
                {
                    //SALE01中没有LAST_UPDATE字段且先执行SALE00的删除操作，再执行SALE01的删除操作
                    if (ds.Tables[0].Rows[i]["transfer_table"].ToString() == "SALE01")
                    {
                        continue;
                    }
                    else
                    {//删除day天前的数据
                        str = " delete " + ds.Tables[0].Rows[i]["transfer_table"] + " where DATEDIFF(day,LAST_UPDATE,'" + d + "')>" + day;
                        b = DBSql.SRunSQL(str);
                    }
                }
                catch
                {
                    b = false;
                }
            }
            str = " delete SALE01 where sale_id not in (select sale_id from sale00)";
            b = DBSql.SRunSQL(str);

            str = " delete saletmp00 where DATEDIFF(day,LAST_UPDATE,'" + d + "')>" + day;
            b = DBSql.SRunSQL(str);
            str = " delete saletmp01 where sale_id not in (select sale_id from saletmp00)";
            b = DBSql.SRunSQL(str);

            str = " delete saletmp02 where DATEDIFF(day,LAST_UPDATE,'" + d + "')>" + day;
            b = DBSql.SRunSQL(str);
            return b;
        }
        /// <summary>
        /// 删除临时表中符合要求的记录
        /// </summary>
        public void DelSaletmp()
        {
            DBSql.SRunSQL("delete saletmp03 where sale_id in(select sale_id from saletmp00 where locked=0 and sale_id not in('" + Info.sale_id + "'))");
            DBSql.SRunSQL("delete saletmp02 where sale_id in(select sale_id from saletmp00 where locked=0 and sale_id not in('" + Info.sale_id + "'))");
            DBSql.SRunSQL("delete saletmp01 where sale_id in(select sale_id from saletmp00 where locked=0 and sale_id not in('" + Info.sale_id + "'))");
            DBSql.SRunSQL("delete saletmp00 where locked=0 and sale_id not in('" + Info.sale_id + "')");

        }
    }
}
