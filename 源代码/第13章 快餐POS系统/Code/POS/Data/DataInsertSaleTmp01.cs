using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using POS.Models;
using POS.Common;
using System.Data.SqlClient;
using System.Data;
// 往SaleTmp01表中插入信息,用于：单餐的插入
namespace POS.Data
{
    /// <summary>
    /// 往SaleTmp01表中插入信息（单餐的插入）
    /// </summary>
    class DataInsertSaleTmp01: DBSql
    {
        ReadIni readIni = new ReadIni("Info.ini");
        /// <summary>
        /// 获得一个DataInsertSaleTmp01实体
        /// </summary>
        /// <returns></returns>
        public static DataInsertSaleTmp01 InitDataInsertSale01() { return new DataInsertSaleTmp01(); }

        /// <summary>
        /// 向表SALETMP01插入数据
        /// </summary>
        /// <param name="saleTmp01">SaleTmp01类对象</param>
        /// <returns>bool</returns>
        public bool InsertSale01(SaleTmp01 saleTmp01)
        {
            SqlParameter[] para = new SqlParameter[25];
            para[0] = new SqlParameter("@SHOP_ID",SqlDbType.NVarChar);
            para[1] = new SqlParameter("@SALE_ID",SqlDbType.NVarChar);
            para[2] = new SqlParameter("@SALE_SNO",SqlDbType.SmallInt);
            para[3] = new SqlParameter("@PROD_ID",SqlDbType.NVarChar);
            para[4] = new SqlParameter("@SALE_PRICE",SqlDbType.Money);
            para[5] = new SqlParameter("@QTY",SqlDbType.Decimal);
            para[6] = new SqlParameter("@ITEM_DISC",SqlDbType.Money);
            para[7] = new SqlParameter("@PROM_ID",SqlDbType.NVarChar);
            para[8] = new SqlParameter("@PROM_SNO",SqlDbType.Int);
            para[9] = new SqlParameter("@PRICE_TYPE",SqlDbType.NChar );
            //para[10] = new SqlParameter("@FREE_EMP",SqlDbType.NVarChar);
            //para[10] = new SqlParameter("@FREE_MEMO",SqlDbType.SmallInt );
            para[10] = new SqlParameter("@COMB_SALE_SNO", SqlDbType.Int);
            para[11] = new SqlParameter("@COMB_SNO", SqlDbType.SmallInt);
            para[12] = new SqlParameter("@COMB_TYPE",SqlDbType.NChar);
            para[13] = new SqlParameter("@ITEM_TAX",SqlDbType.Money );
            para[14] = new SqlParameter("@OUTINCOME",SqlDbType.Bit);
            para[15] = new SqlParameter("@MEAL_TICKET",SqlDbType.Int );
            para[16] = new SqlParameter("@BY_TOKEN", SqlDbType.Bit);
            para[17] = new SqlParameter("@RELATE_PROD",SqlDbType.NVarChar);
            para[18] = new SqlParameter("@SALE_ORGINAL_PRICE",SqlDbType.Decimal);
            para[19] = new SqlParameter("@ITEM_DISC_TOT", SqlDbType.Decimal);
            para[20] = new SqlParameter("@ACT_PRICE",SqlDbType.Decimal);
            para[21] = new SqlParameter("@ISPROM", SqlDbType.Bit);
            para[22] = new SqlParameter("@GROUP_PROD",SqlDbType.NVarChar);
            para[23] = new SqlParameter("@STATUS_ID", SqlDbType.NChar);
            para[24] = new SqlParameter("@TRANSFER_STATUS", SqlDbType.NChar);     
            para[0].Value  =saleTmp01.Shop_id ;
            para[1] .Value =saleTmp01.Sale_id ;
            para[2] .Value =saleTmp01.Sale_sno;
            Info.sale_sno++;
            readIni.WriteString("RepastErp", "sale_sno", Info.sale_sno.ToString());
            para[3] .Value =saleTmp01.Prod_id ;
            para[4] .Value =saleTmp01.Sale_price;
            para[5] .Value =saleTmp01.Qty ;
            para[6] .Value =saleTmp01.Item_disc ;
            para[7].Value = saleTmp01.Prom_id;
            para[8].Value = saleTmp01.Prom_sno;
            para[9] .Value =saleTmp01.Price_type ;
            //para[10] .Value =saleTmp01.Free_emp ;
            //para[10] .Value =saleTmp01.Free_memo;
            para[10].Value = saleTmp01.Comb_sale_sno;
            para[11].Value = saleTmp01.Comb_sno;
            para[12].Value =saleTmp01.Comb_type ;
            para[13].Value =saleTmp01.Item_tax ;
            para[14].Value = saleTmp01.Outincome;
            para[15].Value =saleTmp01.Meal_ticket ;
            para[16].Value = saleTmp01.By_token;
            para[17].Value =saleTmp01.Relate_prod ;
            para[18].Value =saleTmp01.Sale_orginal_price ;
            para[19].Value =saleTmp01.Item_disc_tot;
            para[20].Value =saleTmp01.Act_price;
            para[21].Value = saleTmp01.Isprom;
            para[22].Value =saleTmp01.Group_prod ;
            para[23].Value =saleTmp01.Status_id;
            para[24].Value = saleTmp01.Transfer_status;

	    // 向表SALETMP01中插入数据
	    string sql = "INSERT INTO SALETMP01 ( SHOP_ID, SALE_ID, SALE_SNO, PROD_ID, SALE_PRICE, "
            + "QTY, ITEM_DISC,PROM_ID,PROM_SNO, PRICE_TYPE ,"
            + "COMB_SALE_SNO,COMB_SNO,COMB_TYPE,"
            + "ITEM_TAX,OUTINCOME, MEAL_TICKET,BY_TOKEN,RELATE_PROD,"
            + "SALE_ORGINAL_PRICE, ITEM_DISC_TOT,ACT_PRICE,ISPROM,GROUP_PROD,"
            + "STATUS_ID,TRANSFER_STATUS"
            + "  ) "
            + "VALUES(	@SHOP_ID, @SALE_ID, @SALE_SNO, @PROD_ID, @SALE_PRICE, "
            + "@QTY, @ITEM_DISC,@PROM_ID,@PROM_SNO, @PRICE_TYPE ,"
            + "@COMB_SALE_SNO,@COMB_SNO,@COMB_TYPE,"
            + "@ITEM_TAX,@OUTINCOME, @MEAL_TICKET,@BY_TOKEN,@RELATE_PROD,"
            + "@SALE_ORGINAL_PRICE, @ITEM_DISC_TOT,@ACT_PRICE,@ISPROM,@GROUP_PROD,"
            + "@STATUS_ID,@TRANSFER_STATUS"
                + " )";
            // 调用父类的方法DBSql.RunSql将数据插入到SALETMP01
            return base.RunSQL(sql, para);
        }


    }

   
}
