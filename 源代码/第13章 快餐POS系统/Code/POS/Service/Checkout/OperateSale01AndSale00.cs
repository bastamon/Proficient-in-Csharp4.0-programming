using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using POS.Common;
using System.Data;
//完成对sale01和sale00表的更新操作——针对于退货操作
/*主要公共方法：
（1）GetDataSale00(string sale_id, string sale_user,string datetime1,string datetime2)，对sale00的查询工作
（2）GetDataSale01(string sale_id)，用于返回sale01表的相关数据
（3）GetDataEmployee()，用于得到员工编号
（4）UpdateSale00(string sale_id,decimal totmoney,double qty)，用于更新sale00相应产品使用退货的功能
（5）UpdateSale01(string sale_id, string prod_id)，使用退货更新sale01相关表的相应字段
*/
namespace POS.Service
{
    /// <summary>
    /// 完成退货功能对sale00和sale01表相关更新工作
    /// </summary>
    class OperateSale01AndSale00 : DBSql
    {
        /// <summary>
        /// 得到OperateSale01AndSale00的一个实例
        /// </summary>
        /// <returns>OperateSale01AndSale00</returns>
        public static OperateSale01AndSale00 InitGetSale01AndSale00()
        {
            return new OperateSale01AndSale00();
        }
        #region//对表的操作
        /// <summary>
        /// 对sale00的查询工作
        /// </summary>
        /// <param name="sale_id">销售单号</param>
        /// <param name="sale_user">服务员号</param>
        /// <param name="datetime1">起始时间</param>
        /// <param name="datetime2">结束时间</param>
        /// <returns>DataSet</returns>
        public DataSet GetDataSale00(string sale_id, string sale_user,string datetime1,string datetime2)
        {
            string sql;
            if ("" == sale_id)
            {
                if ("" == sale_user)
                {
                    sql = "SELECT SALE00.SALE_ID as 交易编号,SALE00.POS_ID as Pos号,SALE00.STATUS_ID as 状态,SALE00.LAST_UPDATE as 销售时间,SALE00.SALE_USER as 销售员编号,EMPLOYEE.EMP_NAME as 收银员,SALE00.TOT_QUAN  as 总销售量, SALE00.TOT_SALES as 总销售额,SALE00.CHANGE as 找零 from SALE00,EMPLOYEE where SALE00.STATUS_ID !='3' AND SALE00.MEAL_KIND='0' AND EMPLOYEE.EMP_ID=SALE00.SALE_USER AND CONVERT(NVARCHAR(10), SALE_DATE, 121) BETWEEN '"+datetime1 +"' and  '" +datetime2 + "'";
                }
                else
                {
                    sql = "SELECT SALE00.SALE_ID as 交易编号,SALE00.POS_ID as Pos号,SALE00.STATUS_ID as 状态,SALE00.LAST_UPDATE as 销售时间,SALE00.SALE_USER as 销售员编号,EMPLOYEE.EMP_NAME as 收银员, SALE00.TOT_QUAN  as 总销售量, SALE00.TOT_SALES as 总销售额,SALE00.CHANGE as 找零 from SALE00,EMPLOYEE where SALE00.SALE_USER='" + sale_user + "' AND SALE00.STATUS_ID !='3' AND SALE00.MEAL_KIND='0' AND EMPLOYEE.EMP_ID='" + sale_user + "' AND CONVERT(NVARCHAR(10), SALE_DATE, 121) BETWEEN '" + datetime1 + "' and  '" + datetime2 + "'";
                }
            }
            else 
            {
                if ("" == sale_user)
                {
                    sql = "SELECT SALE00.SALE_ID as 交易编号,SALE00.POS_ID as Pos号,SALE00.STATUS_ID as 状态,SALE00.LAST_UPDATE as 销售时间,SALE00.SALE_USER as 销售员编号,EMPLOYEE.EMP_NAME as 收银员, SALE00.TOT_QUAN  as 总销售量, SALE00.TOT_SALES as 总销售额,SALE00.CHANGE as 找零 from SALE00,EMPLOYEE where SALE00.SALE_ID='" + sale_id + "' AND EMPLOYEE.EMP_ID=SALE00.SALE_USER  AND SALE00.STATUS_ID !='3' AND SALE00.MEAL_KIND='0' AND CONVERT(NVARCHAR(10), SALE_DATE, 121) BETWEEN '" + datetime1 + "' and  '" + datetime2 + "'";
                }
                else 
                {
                    sql = "SELECT SALE00.SALE_ID as 交易编号,SALE00.POS_ID as Pos号,SALE00.STATUS_ID as 状态,SALE00.LAST_UPDATE as 销售时间,SALE00.SALE_USER as 销售员编号,EMPLOYEE.EMP_NAME as 收银员, SALE00.TOT_QUAN  as 总销售量, SALE00.TOT_SALES as 总销售额,SALE00.CHANGE as 找零 from SALE00,EMPLOYEE where SALE00.SALE_ID='" + sale_id + "' AND  SALE00.SALE_USER='" + sale_user + "' AND SALE00.STATUS_ID !='3' AND SALE00.MEAL_KIND='0' AND EMPLOYEE.EMP_ID='" + sale_user + "' AND CONVERT(NVARCHAR(10), SALE_DATE, 121) BETWEEN '" + datetime1 + "' and  '" + datetime2 + "'";
                }
            }            
            return base.CreateDataSet(sql);
        }
        /// <summary>
        /// 用于返回sale01表的相关数据
        /// </summary>
        /// <param name="sale_id">销售单号</param>
        /// <returns>DataSet</returns>
        public DataSet GetDataSale01(string sale_id)
        {
            string sql;
            string sqlfreeemp="select FREE_EMP from sale01 where sale_id='"+sale_id+"'and COMB_TYPE !='2'";
            DataSet da= base.CreateDataSet(sqlfreeemp);            
            int n=da.Tables[0].Rows.Count;
            if (da.Tables[0].Select("FREE_EMP is NULL").Count() == 0)
            {
                //sql = "SELECT SALE01.PROD_ID as 商品编号,PRODUCT00.PROD_NAME as 商品名称,SALE01.SALE_PRICE as 单价,SALE01.QTY as 数量,SALE01.ITEM_DISC as 折扣,(Sale01.sale_price+Sale01.Item_disc)*sale01.Qty as 小计,SALE01.FREE_EMP as 招待人编号,EMPLOYEE.EMP_NAME as 招待人姓名,SALE01.FREE_MEMO as 招待备注,SALE01.SHOP_ID from SALE01,PRODUCT00,EMPLOYEE where SALE01.SALE_ID='" + sale_id + "' AND PRODUCT00.PROD_ID=SALE01.PROD_ID AND EMPLOYEE.EMP_ID=SALE01.FREE_EMP ";
                sql = "SELECT SALE01.PROD_ID as 商品编号,PRODUCT00.PROD_NAME as 商品名称,SALE01.SALE_PRICE as 单价,SALE01.QTY as 数量,SALE01.ITEM_DISC as 折扣,sale01.act_price*sale01.qty as 小计,SALE01.FREE_EMP as 招待人编号,EMPLOYEE.EMP_NAME as 招待人姓名,SALE01.SHOP_ID from SALE01,PRODUCT00,EMPLOYEE where SALE01.SALE_ID='" + sale_id + "' and sale01.COMB_TYPE !='2' AND PRODUCT00.PROD_ID=SALE01.PROD_ID AND EMPLOYEE.EMP_ID=SALE01.FREE_EMP ";
                return base.CreateDataSet(sql);
            }
            else 
            {
                if (n == da.Tables[0].Select("FREE_EMP is NULL").Count())
                {
                    //sql = "SELECT SALE01.PROD_ID as 商品编号,PRODUCT00.PROD_NAME as 商品名称,SALE01.SALE_PRICE as 单价,SALE01.QTY as 数量,SALE01.ITEM_DISC as 折扣,(Sale01.sale_price+Sale01.Item_disc)*sale01.Qty as 小计,null as 招待人编号,null as 招待人姓名,null as 招待备注,SALE01.SHOP_ID from SALE01,PRODUCT00 where SALE01.SALE_ID='" + sale_id + "' and sale01.COMB_TYPE !='2' AND FREE_EMP is NULL AND PRODUCT00.PROD_ID=SALE01.PROD_ID ";
                    sql = "SELECT SALE01.PROD_ID as 商品编号,PRODUCT00.PROD_NAME as 商品名称,SALE01.SALE_PRICE as 单价,SALE01.QTY as 数量,SALE01.ITEM_DISC as 折扣,sale01.act_price*sale01.qty as 小计,null as 招待人编号,null as 招待人姓名,SALE01.SHOP_ID  from SALE01,PRODUCT00 where SALE01.SALE_ID='" + sale_id + "' and sale01.COMB_TYPE !='2' AND FREE_EMP is NULL AND PRODUCT00.PROD_ID=SALE01.PROD_ID ";
                    return base.CreateDataSet(sql);
                }
                else
                {  //sql = "SELECT SALE01.PROD_ID as 商品编号,PRODUCT00.PROD_NAME as 商品名称,SALE01.SALE_PRICE as 单价,SALE01.QTY as 数量,SALE01.ITEM_DISC as 折扣,(Sale01.sale_price+Sale01.Item_disc)*sale01.Qty as 小计,SALE01.FREE_EMP as 招待人编号,EMPLOYEE.EMP_NAME as 招待人姓名,SALE01.FREE_MEMO as 招待备注,SALE01.SHOP_ID from SALE01,PRODUCT00,EMPLOYEE where SALE01.SALE_ID='" + sale_id + "' and sale01.COMB_TYPE !='2' AND SALE01.FREE_EMP is not NULL AND PRODUCT00.PROD_ID=SALE01.PROD_ID AND EMPLOYEE.EMP_ID=SALE01.FREE_EMP";
                    sql = "SELECT SALE01.PROD_ID as 商品编号,PRODUCT00.PROD_NAME as 商品名称,SALE01.SALE_PRICE as 单价,SALE01.QTY as 数量,SALE01.ITEM_DISC as 折扣,sale01.act_price*sale01.qty as 小计,SALE01.FREE_EMP as 招待人编号,EMPLOYEE.EMP_NAME as 招待人姓名,SALE01.SHOP_ID  from SALE01,PRODUCT00,EMPLOYEE where SALE01.SALE_ID='" + sale_id + "' and sale01.COMB_TYPE !='2' AND SALE01.FREE_EMP is not NULL AND PRODUCT00.PROD_ID=SALE01.PROD_ID AND EMPLOYEE.EMP_ID=SALE01.FREE_EMP";
                     da = base.CreateDataSet(sql);
                   // sql = "SELECT SALE01.PROD_ID as 商品编号,PRODUCT00.PROD_NAME as 商品名称,SALE01.SALE_PRICE as 单价,SALE01.QTY as 数量,SALE01.ITEM_DISC as 折扣,(Sale01.sale_price+Sale01.Item_disc)*sale01.Qty as 小计,SALE01.FREE_EMP as 招待人编号,SALE01.FREE_MEMO as 招待备注,SALE01.SHOP_ID from SALE01,PRODUCT00 where SALE01.SALE_ID='" + sale_id + "' and sale01.COMB_TYPE !='2' AND FREE_EMP is NULL AND PRODUCT00.PROD_ID=SALE01.PROD_ID";                 
                     sql = "SELECT SALE01.PROD_ID as 商品编号,PRODUCT00.PROD_NAME as 商品名称,SALE01.SALE_PRICE as 单价,SALE01.QTY as 数量,SALE01.ITEM_DISC as 折扣,sale01.act_price*sale01.qty as 小计,SALE01.FREE_EMP as 招待人编号,SALE01.SHOP_ID from SALE01,PRODUCT00 where SALE01.SALE_ID='" + sale_id + "' and sale01.COMB_TYPE !='2' AND FREE_EMP is NULL AND PRODUCT00.PROD_ID=SALE01.PROD_ID";   
                    da.Tables[0].Merge(base.CreateDataSet(sql).Tables[0],true,MissingSchemaAction.Add);
                    return da;
                }
            }          
        }
        /// <summary>
        /// 用于的到员工编号
        /// </summary>
        /// <returns>DataSet</returns>
        public DataSet GetDataEmployee()
        {
            string sql = "SELECT EMP_ID from EMPLOYEE";
            return base.CreateDataSet(sql);
        }
        /// <summary>
        /// 用于更新sale00相应产品使用退货的功能
        /// </summary>
        /// <param name="sale_id">销售单号</param>
        /// <param name="totmoney">总退货额</param>
        /// <param name="qty">总退货数量</param>
        /// <returns>bool</returns>
        public bool UpdateSale00(string sale_id,decimal totmoney,double qty)
        {
            bool a = false;
            DateTime datetime=DateTime.Now;
            try
            {
                string sql = "UPDATE sale00 SET BACK_SALES=" + totmoney + ", BACK_QUAN=" + qty + ", LAST_UPDATE='" + datetime + "' ,STATUS_ID='3',transfer_status='0' WHERE SALE_ID='" + sale_id + "'";
                a= base.RunSQL(sql);
            }
            catch 
            {
                string da =datetime.ToString("yyyy-MM-dd HH:mm:ss");
                string sql = "UPDATE sale00 SET BACK_SALES=" + totmoney + ", BACK_QUAN=" + qty + ", LAST_UPDATE='" + da + "' ,STATUS_ID='3',transfer_status='0' WHERE SALE_ID='" + sale_id + "'";            
                a=base.RunSQL(sql);
            }
            if (a)
            {
                try
                {
                    ReadIni readIni = new ReadIni("config.ini");
                    string srvIp = readIni.ReadString("RepastErp", "txtServerIP");
                    string srvPort = readIni.ReadString("RepastErp", "txtPort");
                    string srvDBName = readIni.ReadString("RepastErp", "txtIPdataname");
                    string srvUserName = readIni.ReadString("RepastErp", "txtFTPuser");
                    string srvPassword = readIni.ReadString("RepastErp", "txtFTPpassword");
                    string sql = "begin " + "UPDATE sale01 SET  transfer_status='0' WHERE SALE_ID='" + sale_id + "';" + "UPDATE sale02 SET  transfer_status='0' WHERE SALE_ID='" + sale_id + "';" + "delete from OPENDATASOURCE('SQLOLEDB','Data Source=" + srvIp + "," + srvPort + ";User ID=" + srvUserName + ";Password=" + srvPassword + "' )." + srvDBName + ".dbo.sale00 where shop_id='" + Info.shop_id + "' and sale_id='" + sale_id + "';end";
                    
                    DBSql.SRunSQL(sql);
                }
                catch { }
            }
            return a;
        }
        /// <summary>
        /// 使用退货更新sale01相关表的相应字段
        /// </summary>
        /// <param name="sale_id">销售单号</param>
        /// <param name="prod_id">产品编号</param>
        /// <returns>bool</returns>
        public bool UpdateSale01(string sale_id, string prod_id)
        {
            string sql = "UPDATE sale01 SET  STATUS_ID='3',transfer_status='0' WHERE SALE_ID='" + sale_id + "' AND RELATE_PROD='" + prod_id + "'";
            return base.RunSQL(sql);
        }
        #endregion
    }
}
