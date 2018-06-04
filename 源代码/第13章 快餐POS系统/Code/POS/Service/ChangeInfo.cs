using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using POS.Common;
using System.Data;
//改变Info里面对应字段的值 涉及表：SALETMP00 、saletmp01
/*主要公共方法：
（1）	changInfo(string str)，改变Info类中里面对应字段的值
（2）	DelSaletmp00(string sale_id)删除saletmp00中指点销售单
（3）	Unlock(string sale_id) 挂单
*/
namespace POS.Service
{
    /// <summary>
    /// 改变Info里面对应字段的值
    /// </summary>
    class ChangeInfo:DBSql
    {
        /// <summary>
        /// 改变Info中对应的值
        /// </summary>
        /// <param name="str">销售单编号</param>
        /// <returns>返回Info中对应值的字符串</returns>
        public string  changInfo(string str)
        {
            char[] charflag={'0','0','0','0'};
            DataSet ds = new DataSet();
            string sqlstr = "select MEAL_KIND,METHOD_ID from SALETMP00 where sale_id='" + str+"'";
            ds= base.CreateDataSet (sqlstr );
            charflag [0] =Convert.ToChar ( ds.Tables[0].Rows[0][0].ToString() );
            charflag [1]=Convert .ToChar ( ds.Tables[0].Rows[0][1].ToString ());
            sqlstr = "select PRICE_TYPE from saletmp01 where sale_id='" + str + "'";
            ds = base.CreateDataSet(sqlstr );
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (ds.Tables[0].Rows[i][0].ToString() == "1")
                {
                    charflag[2] = '1';
                }
                if (ds.Tables[0].Rows[i][0].ToString() == "2")
                {
                    charflag[3] = '2';
                }
            }
            string changinfo = new string(charflag);
            return changinfo;
        }
        /// <summary>
        /// 删除salemp00中指点销售单序号记录
        /// </summary>
        /// <param name="sale_id">销售单号</param>
        /// <returns>bool</returns>
        public bool DelSaletmp00(string sale_id)
        {
           string sql = "delete SALETMP00 WHERE sale_id='" + sale_id + "'";
           return base.RunSQL(sql);
        }
        /// <summary>
        /// 挂单操作
        /// </summary>
        /// <param name="sale_id">销售单号</param>
        /// <returns>bool</returns>
        public bool Unlock(string sale_id)
        {
            string sql = "update SALETMP00 set locked='0' WHERE sale_id='" + sale_id + "'";
            return base.RunSQL(sql);
        }
    }
}
