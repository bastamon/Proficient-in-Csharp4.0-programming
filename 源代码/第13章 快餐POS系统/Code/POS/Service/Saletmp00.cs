using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using POS.Common;
using POS.Data;
using System.Data;
// 挂单相关处理 涉及表： SALETMP00
/*主要公共方法：
（1）	OperateSaletmp00(),点击挂单时令对应的LOCKED值为1
（2）	SetMaxDeal_number()得到最大交易号
（3）	AllowOnline()是否允许下线
*/
namespace POS.Service
{
    /// <summary>
    /// 点击挂单时令对应的LOCKED值为1
    /// </summary>
    class Saletmp00:DBSql
    {
        /// <summary>
        /// 点击挂单时令对应的LOCKED值为1
        /// </summary>
        /// <returns></returns>
        public bool  OperateSaletmp00()
        {
            string sql ;
            bool b = false;
                sql = "UPDATE SALETMP00 SET LOCKED = '1',sale_date='" + DateTime.Now.ToString ("yyyy-MM-dd HH:mm:ss") + "' WHERE sale_id='" + Info.sale_id + "'";
                b= base.RunSQL(sql);
                return b;
        }

        /// <summary>
        /// 得到最大的交易号
        /// </summary>
        public void SetMaxDeal_number()
        {
            string str = "select * from sale00  order by  sale_id desc";
            DataSet ds= base.CreateDataSet(str);
            string str2 = "select * from saletmp00  order by  sale_id desc";
            DataSet ds2 = base.CreateDataSet(str2);
            ReadIni readIni = new ReadIni("Info.ini");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                string s = ds.Tables[0].Rows[0]["sale_id"].ToString();
                int num = Convert.ToInt32(s.Substring(s.Length - 4, 4));
                if (ds2 != null && ds2.Tables[0].Rows.Count > 0)
                {
                    string s2 = ds2.Tables[0].Rows[0]["sale_id"].ToString();
                    int num2 = Convert.ToInt32(s2.Substring(s2.Length - 4, 4));
                   
                    if (num2 > num)
                    {
                        Info.deal_number = num2;
                        readIni.WriteString("RepastErp", "deal_number", num2.ToString());
                    }else{
                            Info.deal_number = num;
                    readIni.WriteString("RepastErp", "deal_number", num.ToString());
                }
                }
                else
                {
                    Info.deal_number = num;
                    readIni.WriteString("RepastErp", "deal_number", num.ToString());
                }

              
            }
            
        }
        /// <summary>
        ///是否允许下线(true :允许      false ：不允许)： 当有挂单时不允许下线
        /// </summary>
        /// <returns>bool</returns>
        public bool AllowOnline()
        {
            string sql = "select count(*) from saletmp00 where locked='1'";
            DataTable dt=base.CreateDataSet(sql).Tables[0];
            if (dt != null && Convert.ToInt16(dt.Rows[0][0])>0)
            { return false; }
            else
            { return true; }
        }


    }
}
