using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using POS.Models;
using System.Data.SqlClient;
using System.Data;
using POS.Common;
//主要完成对SALETMP00和SALETMP01表插入数据(组合餐的插入）
/*类说明：对SALETMP00和SALETMP01表插入数据(组合餐的插入）

主要公共方法：
（1）insertSaleTemp01(SALETMP01 saleTemp01)，向SALETMP01表插入数据，SALETMP01：属性类
（2）insertSaleTemp00(SALETMP00 saleTemp00)，往saleTemp00中插入一条记录，SALETMP00
：属性类
（3）SelectSaleCombInfo(SALETMP01 saleTemp01)，从COMBINATION和PRODUCT00中查找每一组合餐的所有信息
（4）SelectAllInfo(string SHOP_ID, string SALE_ID)，获得该销售单产品的所有信息
（5）SelectProInfo(SALETMP01 saleTemp01)，从PRODUCT00中查找每一产品的所有信息


*/
namespace POS.Data
{
    /// <summary>
    /// 主要完成对SALETMP00和SALETMP01表插入数据(组合餐的插入）
    /// </summary>
    class DataInsertSales : DBSql
    {
        /// <summary>
        /// 获得一个DataInsertSaless实体
        /// </summary>
        /// <returns>返回一个DataInsertSaless实体</returns>
        public static DataInsertSales InitDataInsertSales()
        {
            return new DataInsertSales();
        }
        /// <summary>
        /// 向SALETMP01表插入数据
        /// </summary>
        /// <param name="saleTemp01">传过来一个SALETMP01实体</param>
        /// <returns>返回true或false</returns>
        public bool insertSaleTemp01(SALETMP01 saleTemp01)
        {
            SqlParameter[] para = new SqlParameter[23];
            para[0] = new SqlParameter("@SHOP_ID", SqlDbType.NVarChar, 12);
            para[1] = new SqlParameter("@SALE_ID", SqlDbType.NVarChar, 32);
            para[2] = new SqlParameter("@SALE_SNO", SqlDbType.SmallInt);
            para[3] = new SqlParameter("@PROD_ID", SqlDbType.NVarChar, 20);
            para[4] = new SqlParameter("@SALE_PRICE", SqlDbType.Money);
            para[5] = new SqlParameter("@QTY", SqlDbType.Decimal);
            para[6] = new SqlParameter("@ITEM_DISC", SqlDbType.Money);
            para[7] = new SqlParameter("@PRICE_TYPE", SqlDbType.NChar, 1);
            //para[8] = new SqlParameter("@FREE_EMP", SqlDbType.NVarChar, 10);
            //para[9] = new SqlParameter("@FREE_MEMO", SqlDbType.NVarChar, 100);
            para[8] = new SqlParameter("@COMB_TYPE", SqlDbType.NChar, 1);
            para[9] = new SqlParameter("@ITEM_TAX", SqlDbType.Money);
            para[10] = new SqlParameter("@MEAL_TICKET", SqlDbType.Int);
            para[11] = new SqlParameter("@RELATE_PROD", SqlDbType.NVarChar, 20);
            para[12] = new SqlParameter("@ITEM_DISC_TOT", SqlDbType.Decimal);
            para[13] = new SqlParameter("@ACT_PRICE", SqlDbType.Decimal);
            para[14] = new SqlParameter("@GROUP_PROD", SqlDbType.NVarChar, 400);
            para[15] = new SqlParameter("@STATUS_ID", SqlDbType.NChar, 1);
            para[16] = new SqlParameter("@COMB_SNO", SqlDbType.SmallInt);
            para[17] = new SqlParameter("@SALE_ORGINAL_PRICE", SqlDbType.Decimal);
            para[18] = new SqlParameter("@BY_TOKEN", SqlDbType.Bit);
            para[19] = new SqlParameter("@ISPROM", SqlDbType.Bit);
            para[20] = new SqlParameter("@OUTINCOME", SqlDbType.Bit);
            para[21] = new SqlParameter("@COMB_SALE_SNO", SqlDbType.SmallInt);
            para[22] = new SqlParameter("@TRANSFER_STATUS", SqlDbType.NVarChar);
            para[0].Value = saleTemp01.SHOP_ID1;
            para[1].Value = saleTemp01.SALE_ID1;
            para[2].Value = saleTemp01.SALE_SNO1;
            para[3].Value = saleTemp01.PROD_ID1;
            para[4].Value = saleTemp01.SALE_PRICE1;
            para[5].Value = saleTemp01.QTY1;
            para[6].Value = saleTemp01.ITEM_DISC1;
            para[7].Value = saleTemp01.PRICE_TYPE1;
            //para[8].Value = "";
            //para[9].Value = "";
            para[8].Value = saleTemp01.COMB_TYPE1;
            para[9].Value = 0;
            para[10].Value = saleTemp01.MEAL_TICKET1;
            para[11].Value = saleTemp01.RELATE_PROD1;
            para[12].Value = saleTemp01.ITEM_DISC_TOT1;
            para[13].Value = saleTemp01.ACT_PRICE1;
            para[14].Value = saleTemp01.GROUP_PROD1;
            para[15].Value = 2;
            para[16].Value = saleTemp01.COMB_SNO1;
            para[17].Value = saleTemp01.SALE_ORGINAL_PRICE1;
            para[18].Value = Convert.ToBoolean(0);
            para[19].Value = saleTemp01.ISPROM1;
            para[20].Value = Convert.ToBoolean(0);
            para[21].Value = saleTemp01.COMB_SALE_SNO1;
            para[22].Value = "0";
            if (saleTemp01.COMB_TYPE1 == "1")
            {
                para[21].Value = DBNull.Value;
            }
            string sql = "insert SALETMP01(SHOP_ID, SALE_ID, SALE_SNO, PROD_ID, SALE_PRICE, QTY, ITEM_DISC, PRICE_TYPE, COMB_TYPE , ITEM_TAX, MEAL_TICKET, RELATE_PROD, ITEM_DISC_TOT, ACT_PRICE ,GROUP_PROD, STATUS_ID, COMB_SNO,SALE_ORGINAL_PRICE,BY_TOKEN,ISPROM,OUTINCOME,COMB_SALE_SNO,TRANSFER_STATUS) values(@SHOP_ID, @SALE_ID, @SALE_SNO, @PROD_ID, @SALE_PRICE, @QTY, @ITEM_DISC, @PRICE_TYPE, @COMB_TYPE, @ITEM_TAX, @MEAL_TICKET, @RELATE_PROD, @ITEM_DISC_TOT, @ACT_PRICE ,@GROUP_PROD, @STATUS_ID, @COMB_SNO,@SALE_ORGINAL_PRICE,@BY_TOKEN,@ISPROM,@OUTINCOME,@COMB_SALE_SNO,@TRANSFER_STATUS)";
            
            bool a;
            a = base.RunSQL(sql, para);
            return a;
        }
        /// <summary>
        /// 往saleTemp00中插入一条记录
        /// </summary>
        /// <param name="saleTemp00">SALETMP00属性类</param>
        /// <returns>bool</returns>
        public bool insertSaleTemp00(SALETMP00 saleTemp00)
        {
            SqlParameter[] para = new SqlParameter[16];
            para[0] = new SqlParameter("@SHOP_ID", SqlDbType.NVarChar, 12);
            para[1] = new SqlParameter("@SALE_ID", SqlDbType.NVarChar, 32);
            para[2] = new SqlParameter("@STATUS_ID", SqlDbType.NChar, 1);
            para[3] = new SqlParameter("@POS_ID", SqlDbType.NVarChar, 2);
            para[4] = new SqlParameter("@SALE_DATE", SqlDbType.DateTime);
            para[5] = new SqlParameter("@TOT_SALES", SqlDbType.Money);
            para[6] = new SqlParameter("@TRANSFER_STATUS", SqlDbType.NChar, 1);
            para[7] = new SqlParameter("@LOCKED", SqlDbType.Bit);
            para[8] = new SqlParameter("@TOT_QUAN", SqlDbType.Decimal);
            para[9] = new SqlParameter("@CHANGE", SqlDbType.Money);
            para[10] = new SqlParameter("@METHOD_ID", SqlDbType.Int);
            para[11] = new SqlParameter("@TOT_TAX", SqlDbType.Money);
            para[12] = new SqlParameter("@MEAL_KIND", SqlDbType.SmallInt);
            para[13] = new SqlParameter("@LAST_UPDATE", SqlDbType.DateTime);
            para[14] = new SqlParameter("@SALE_USER", SqlDbType.NVarChar, 10);
            para[15] = new SqlParameter("@STORE_ID", SqlDbType.NVarChar, 20);
            para[0].Value = Info.shop_id;
            para[1].Value = Info.sale_id;
            para[2].Value = "2";
            para[3].Value = Info.pos_id;
            para[4].Value = DateTime.Now;
            para[5].Value = saleTemp00.TOT_SALES1;
            para[6].Value = "0";
            para[7].Value = 0;
            para[8].Value = saleTemp00.TOT_QUAN1;
            para[9].Value = 0;
            para[10].Value = 1;
            para[11].Value = 0;
            para[12].Value = 0;
            para[13].Value = DateTime.Now;
            para[14].Value = Info.emp_id;
            para[15].Value = Info.shop_id;
            string sql = "insert SALETMP00(SHOP_ID, SALE_ID, STATUS_ID, POS_ID, SALE_DATE, TOT_SALES, TRANSFER_STATUS, LOCKED, TOT_QUAN, CHANGE, METHOD_ID, TOT_TAX, MEAL_KIND, LAST_UPDATE, SALE_USER,STORE_ID) values(@SHOP_ID, @SALE_ID, @STATUS_ID, @POS_ID, @SALE_DATE, @TOT_SALES, @TRANSFER_STATUS, @LOCKED, @TOT_QUAN, @CHANGE, @METHOD_ID, @TOT_TAX, @MEAL_KIND, @LAST_UPDATE, @SALE_USER,@STORE_ID)";
            return base.RunSQL(sql, para);
        }

        /// <summary>
        /// 从COMBINATION和PRODUCT00中查找每一组合餐的所有信息
        /// </summary>
        /// <param name="saleTemp01">传过来一个SALETMP01实体</param>
        /// <returns>返回一个DataSet数据集</returns>
        public DataSet SelectSaleCombInfo(SALETMP01 saleTemp01)
        {
            SqlParameter[] para = new SqlParameter[2];
            para[0] = new SqlParameter("@COMB_ID", SqlDbType.NVarChar, 10);
            para[1] = new SqlParameter("@PROD_ID", SqlDbType.NVarChar, 10);
            para[0].Value = saleTemp01.COMB_ID1;
            para[1].Value = saleTemp01.PROD_ID1;
            string sql = "SELECT COMBINATION.*, PRODUCT00.* FROM COMBINATION INNER JOIN PRODUCT00 ON COMBINATION.PROD_ID = PRODUCT00.PROD_ID WHERE (COMBINATION.COMB_ID = @COMB_ID) AND (COMBINATION.PROD_ID= @PROD_ID) and COMB_SNO=" + saleTemp01.COMB_SNO1;
            return base.CreateDataSet(sql, para);
        }
        /// <summary>
        /// 获得默认子产品价格和
        /// </summary>
        /// <param name="saletmp01">传过来一个SALETMP01实体</param>
        /// <returns>默认子产品价格和</returns>
        public decimal ReturnDefaultprice(SALETMP01 saletmp01)
        {
            string sql = "select sum(price*quantity) as 差价 from combination where comb_id='"+saletmp01.COMB_ID1+"' and isdefault='1'";
            try
            {
                return Convert.ToDecimal(base.CreateDataSet(sql).Tables[0].Rows[0]["差价"].ToString());
            }
            catch { return 0; }
        }
        /// <summary>
        /// 获得该销售单产品的所有信息
        /// </summary>
        /// <param name="SHOP_ID">分店编号</param>
        /// <param name="SALE_ID">销售单编号</param>
        /// <returns>返回一个DataSet数据集</returns>
        public DataSet SelectAllInfo(string SHOP_ID, string SALE_ID)
        {
            string sql = "SELECT * FROM SALETMP01 where SHOP_ID= " + "'" + SHOP_ID + "'" + " " + "and SALE_ID=" + "'" + SALE_ID + "'";
            return base.CreateDataSet(sql);
        }
        /// <summary>
        /// 从PRODUCT00中查找每一产品的所有信息
        /// </summary>
        /// <param name="saleTemp01">传过来一个SALETMP01实体</param>
        /// <returns>返回一个DataSet数据集</returns>
        public DataSet SelectProInfo(SALETMP01 saleTemp01)
        {
            SqlParameter[] para = new SqlParameter[1];
            para[0] = new SqlParameter("@PROD_ID", SqlDbType.NVarChar, 10);
            para[0].Value = saleTemp01.PROD_ID1;
            string sql = "SELECT * FROM PRODUCT00 WHERE PROD_ID = @PROD_ID";
            return base.CreateDataSet(sql, para);
        }


    }
}
