using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using POS.Common;
using System.Windows.Forms;
using System.Data.SqlClient;
// 用于查询销售单信息  涉及表： sale00、sale01、sale02
/*主要公共方法：
（1）searchBusiness(string sale_id, string tot_sales, string  sale_date0, string  sale_date1)，查询销售单信息
（2）payDetail(string sale_id)，销售单明细
（3）payWays(string sale_id)，销售单的付款方式
（4）amountAndChange(string sale_id)，查询销售单的付款现金与找零
（5）item_Disc_Amount(string sale_id)，得到每项打折的额度
*/
namespace POS.Service
{
    /// <summary>
    /// 分别从sale00，sale01，sale02表中获取信息
    /// </summary>
    class SeeAboutSale:DBSql
    {
       public static  string str;
       public static string numstr;
        string strsql;
         /// <summary>
         /// 查询销售单信息-------1
         /// </summary>
         /// <param name="sale_id">交易号</param>
         /// <param name="tot_sales">金额</param>
        /// <param name="sale_date0">起始日期</param>
         /// <param name="sale_date1">终止日期</param>
         /// <returns></returns>
        public DataSet searchBusiness(string sale_id, string tot_sales, string  sale_date0, string  sale_date1)
        {
            DataSet ds = null;
            //select SALE_ID,POS_ID,SALE_USER,TOT_SALES,STATUS, BACK_SALES, BACK_QUAN from SALE00 ---- 查询销售单信息
            //交易号为空金额不为空
            if (sale_id =="" && tot_sales !="" )
            {
                strsql = "select SALE_ID as 销售单编号,POS_ID as POS号,SALE_USER as 收银员,TOT_SALES as 总销售额 ,*  from SALE00  WHERE TOT_SALES="
                        + Convert.ToDouble(tot_sales) + " AND SALE_DATE BETWEEN '" + sale_date0 + "' and '" + sale_date1 + "'";
                ds = base.CreateDataSet(strsql );
            }
               //交易号不为空，金额为空
            else if (sale_id != "" && tot_sales == "")
            {
                strsql = "select SALE_ID as 销售单编号,POS_ID as POS号,SALE_USER as 收银员,TOT_SALES as 总销售额 ,*  from SALE00 WHERE SALE_ID like '%" +
                    sale_id + "%' and SALE_DATE BETWEEN'" + sale_date0 + "' AND '" + sale_date1 + "'";
                ds = base.CreateDataSet(strsql);
            }
                //交易号为空，并且金额为空
            else if (sale_id == "" && tot_sales == "")
            {
                strsql = "select SALE_ID as 销售单编号,POS_ID as POS号,SALE_USER as 收银员,TOT_SALES as 总销售额  ,* from SALE00 WHERE SALE_DATE BETWEEN'" + sale_date0 + "'AND'" + sale_date1 + "'";
                ds = base.CreateDataSet(strsql);
            }
                //交易号不为空，金额不为空
            else
            {
                strsql = "select SALE_ID as 销售单编号,POS_ID as POS号,SALE_USER as 收银员,TOT_SALES as 总销售额 ,* from SALE00 WHERE  SALE_ID like '%"
                    + sale_id + "%' and TOT_SALES=" + Convert.ToDouble(tot_sales) + " AND SALE_DATE BETWEEN '" + sale_date0 + "' AND '" + sale_date1 + "'";
                ds = base.CreateDataSet(strsql);
            }
            //for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            //{
            //    if (ds.Tables[0].Rows[i]["status_id"].ToString() == "3")
            //        ds.Tables[0].Columns.Add();
            //}
                return ds;
        }

           /// <summary>
           /// 销售单明细------------2
           /// </summary>
        /// <param name="sale_id">销售单编号</param>
           /// <returns></returns>
        public DataTable  payDetail(string sale_id)
        {
            //strsql = "SELECT   sale_sno as 序号, PRODUCT00.PROD_NAME as 商品名称, PRODUCT00.prod_id as 商品ID,SALE01.SALE_PRICE as 单价,SALE01.QTY as 数量,ITEM_DISC_TOT as 折扣,sale01.sale_price*sale01.qty+sale01.ITEM_DISC_TOT as 小计,status_id as 状态  FROM product00,SALE01 where sale01.prod_id=product00.prod_id and sale_id='" + sale_id + "' and comb_type=0 union   all SELECT sale_sno as 序号, PRODUCT00.PROD_NAME as 商品名称, PRODUCT00.prod_id as 商品ID,SALE01.SALE_PRICE as 单价,SALE01.QTY as 数量,ITEM_DISC as 折扣,sale01.sale_price*sale01.qty+sale01.ITEM_DISC as 小计,status_id as 状态 FROM product00,SALE01 where sale01.prod_id=product00.prod_id and sale_id='" + sale_id + "' and comb_type!=0";
            strsql = "SELECT   sale_sno as 序号, PRODUCT00.PROD_NAME as 商品名称, PRODUCT00.prod_id as 商品ID,SALE01.SALE_PRICE as 单价,SALE01.QTY as 数量,SALE01.ITEM_DISC*SALE01.QTY as 折扣,sale01.act_price*sale01.qty as 小计,status_id as 状态,sale01.comb_type as 组合类型  FROM product00,SALE01 where sale01.prod_id=product00.prod_id and sale_id='" + sale_id + "'";
            //dt = base.CreateDataSet(strsql ).Tables [0];






            //strsql = "select * from sale01 where sale_id='" + sale_id + "' and comb_type=0";
            ////DataTable dt = base.CreateDataSet(strsql ).Tables [0];
            ////strsql = "select sale01.sale_sno as 序号,";
            //    strsql = "select sale01.sale_sno as 序号, PRODUCT00.PROD_NAME as 商品名称, PRODUCT00.prod_id as 商品ID,SALE01.SALE_PRICE as 单价,SALE01.QTY as 数量,SALE01.ITEM_DISC_TOT as 折扣,sale01.sale_price*sale01.qty+sale01.ITEM_DISC_TOT as 小计,sale01.comb_type as 组合类型 from sale01,product00 where  product00.prod_id= sale01.prod_id and sale01.sale_id= '" + sale_id + "'";
                 return base.CreateDataSet(strsql ).Tables[0];
        }
        /// <summary>
        /// 销售单的付款方式--------------3
        /// </summary>
        /// <param name="sale_id">销售单编号</param>
        /// <returns></returns>
        public DataTable payWays(string sale_id)
        {
            strsql = "select payment.pay_name as 付款方式,sale02.amount as 金额 from sale02 join payment on sale02.pay_id=payment.pay_id and sale02.sale_id='" + sale_id + "'";
            //strsql = "select payment.pay_name as 付款方式,sale02.amount as 金额 from payment,sale02 where sale02.pay_id=payment.pay_id and sale02.sale_id='" + sale_id + "'";
            //strsql = " select AMOUNT,CHANGE from SALE02,PAYMENT where SALE02. PAY_ID=PAYMENT. PAY_ID and sale_id='" + sale_id + "'";
            return base.CreateDataSet(strsql ).Tables [0];
        }
        /// <summary>
        /// 查询销售单的付款现金与找零
        /// </summary>
        /// <param name="sale_id"></param>
        /// <returns></returns>
        public DataTable amountAndChange(string sale_id)
        {
            strsql = "select amount from sale02 where sale_id='"+sale_id +"'";
            return base.CreateDataSet(strsql).Tables[0];
        }
        /// <summary>
        /// 得到每项打折的额度
        /// </summary>
        /// <param name="sale_id">销售单号</param>
        /// <returns></returns>
        public DataTable item_Disc_Amount(string sale_id)
        {
            strsql = "select sum(ITEM_DISC_TOT) from sale01 where sale_id='" + sale_id + "' group by sale_id";
            return base.CreateDataSet(strsql ).Tables [0];
        }
    }
}
