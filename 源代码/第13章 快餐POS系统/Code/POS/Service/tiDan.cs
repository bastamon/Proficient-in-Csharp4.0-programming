using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using POS.Common;
using System.Data;
//提单相关处理 涉及表：saletmp00
/*主要公共方法：
（1）operateSaletmp00()，从saletmp00表中读出LOCKED=1的记录
（2）changLocked(string str)，提单状态时点击确定，把对应的销售单号置0
（3）HongNum() 返回挂单数
*/
namespace POS.Service
{
    /// <summary>
    /// 提单
    /// </summary>
    class tiDan:DBSql
    {
        string sql ;
        /// <summary>
        /// 从saletmp00表中读出LOCKED=1的记录
        /// </summary>
        /// <returns>DataSet</returns>
        public DataSet operateSaletmp00()
        {
            //sql = "select * from saletmp01 where sale_id='"+Info.sale_id +"'";
            //if (base.CreateDataSet(sql).Tables[0].Rows.Count == 0)
            //{
            //    sql = "UPDATE SALETMP00 SET LOCKED = 0 where sale_id='" + Info .sale_id  + "'";
            //    base.RunSQL(sql);
            //}

            sql = "select * from saletmp01 where sale_id='"+Info.sale_id +"'";
            Info.sale_sno = base.CreateDataSet(sql ).Tables [0].Rows.Count+1 ;

            sql = "select sale_id,sale_date from SALETMP00 where LOCKED=1";
            return base.CreateDataSet(sql );
        }

        /// <summary>
        /// 返回挂起单子的数量
        /// </summary>
        /// <returns>int</returns>
        public int HongNum()
        {
            try
            {
                sql = "select * from SALETMP00 where LOCKED=1";
                return base.CreateDataSet(sql).Tables[0].Rows.Count;
            }
            catch { return 0; }
        }
        /// <summary>
        /// 提单状态时点击确定，把对应的销售单号置0
        /// </summary>
        /// <param name="str">销售单id</param>
        /// <returns>bool</returns>
        public bool changLocked(string str)
        {
            sql = "UPDATE SALETMP00 SET LOCKED = 0 where sale_id='"+str +"'";
            return  base.RunSQL(sql);
        }
        ///// <summary>
        ///// 当弹出的对话框所选中的是否时，在saletmp00,和saletmp00表中删除对应的信息
        ///// </summary>
        ///// <param name="sale_id"></param>
        ///// <returns></returns>
        //public bool deleteSale_id(string sale_id)
        //{
        //    sql = "UPDATE SALETMP00 SET LOCKED = '0' where sale_id='" + sale_id  + "'";
        //    base.RunSQL(sql );
        //    sql = "delete from saletmp01 where sale_id='" + sale_id + "'" ;
        //    return base.RunSQL(sql );
        //}
    }
}
