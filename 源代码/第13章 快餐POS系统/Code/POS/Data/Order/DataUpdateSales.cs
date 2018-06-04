using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using POS.Models;
using System.Data.SqlClient;
using System.Data;
using POS.Common;
//对SALETMP00表中的总销售额TOT_SALES的字段的和SALETMP01表中的折让金额ITEM_DISC字段、售价类型PRICE_TYPE字段、总折扣ITEM_DISC_TOT字段和促销价ACT_PRICE字段的更新
/*主要公共方法：
（1）SelectPriceDisc(string SHOP_ID, string SALE_ID, string GROUP_PROD)，获得商品销售量QTY，总折扣ITEM_DISC_TOT，售价SALE_PRICE，售价类型COMB_TYPE
（2）SelectAllInfo(string SHOP_ID, string SALE_ID)，获得该销售单的所有信息
（3）SelectAllCombMessage(Sales priceDisc)，获得组合餐中每一子餐的标准数量
（4）UpdateSALETMP01(Sales priceDisc)，点折扣时执行对SALETMP01表的更新语句
（5）UpdateSALETMP01PRICE_TYPE(Sales priceDisc)，点折扣时执行对SALETMP01表的套餐中的子餐的PRICE_TYPE字段的更新语句
（6）UpdateSALETMP00(Sales priceDisc)，点折扣时执行对SALETMP00表的更新语句
（7）UpdateSALETMP00TOT_SALES(Sales priceDisc)，修改产品数量时对SALETMP00表的销售单总销售价TOT_SALES字段的更新语句
（8）UpdateSALETMP01QTY(Sales priceDisc)，修改产品数量时对SALETMP01表中的单产品或组合餐产品的数量QTY字段的更新语句
（9）UpdateSALETMP01QTY2(Sales priceDisc)，修改产品数量时对SALETMP01表中的组合餐的子产品的数量QTY字段的更新语句
（10）UpdateSALETMP01FREE_EMP(SALETMP01 saleTemp01)，对SALETMP01表中的产品的对应单个产品价格做改动时经手员工编号FREE_EMP字段的修改
（11）DeleteSALETMP01Comb(string SHOP_ID, string SALE_ID, string GROUP_PROD)，删除SALETMP01表中的某一组合餐
（12）GetProd_Group(string shop_id,string sale_id)，根据销售单号和店铺号查询Prod_group
（13）DataDeleteSales()，删去saletmp01、saletmp00、saletmp02中的符合条件的记录
（14）DataDeleteSales(string shop_id, string sale_id)，删去saletmp01、saletmp00、saletmp02中的符合条件的记录
（15）DataDelSalesSucess(string shop_id, string sale_id)，更新saletmp01、saletmp00、saletmp02中的符合条件的记录
（16）DataDelSalesSucess(string shop_id, string sale_id) 更新saletmp01、sésaletmp00、sésaletmp02中符合条件的记录
*/
namespace POS.Data
{
    /// <summary>
    /// 对SALETMP00表中的总销售额TOT_SALES的字段的和SALETMP01表中的折让金额ITEM_DISC字段、售价类型PRICE_TYPE字段、总折扣ITEM_DISC_TOT字段和促销价ACT_PRICE字段的更新
    /// </summary>
    class DataUpdateSales : DBSql
    {
        /// <summary>
        /// 获得一个DataUpdateSales实体
        /// </summary>
        /// <returns>返回一个DataUpdateSales实体</returns>
        public static DataUpdateSales InitDataUpdateSales()
        {
            return new DataUpdateSales();
        }
        /// <summary>
        /// 获得该组合餐在当前商品销售单中的全部信息
        /// </summary>
        /// <param name="SHOP_ID">分店编号</param>
        /// <param name="SALE_ID">销售单编号</param>
        /// <param name="GROUP_PROD">组合产品</param>
        /// <returns>返回一个DataSet数据集</returns>
        public DataSet SelectPriceDisc(string SHOP_ID, string SALE_ID, string GROUP_PROD)
        {
            SqlParameter[] para = new SqlParameter[1];
            para[0] = new SqlParameter("@GROUP_PROD", SqlDbType.NVarChar, 400);
            para[0].Value = GROUP_PROD;
            string sql = "SELECT * FROM SALETMP01 where SHOP_ID= " + "'" + SHOP_ID + "'" + " " + "and SALE_ID=" + "'" + SALE_ID + "'" + " " + "and GROUP_PROD= @GROUP_PROD";
            //string sql = "SELECT QTY, ITEM_DISC_TOT, SALE_PRICE, COMB_TYPE,SALE_ORGINAL_PRICE FROM SALETMP01 where SHOP_ID= " + "'" + SHOP_ID + "'" + " " + "and SALE_ID=" + "'" + SALE_ID + "'" + " " + "and GROUP_PROD= @GROUP_PROD";
            return base.CreateDataSet(sql, para);
        }
        /// <summary>
        /// 获得该销售单的所有信息
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
        /// 返回销售记录中有无折扣折让状态
        /// </summary>
        /// <param name="SHOP_ID">分店编号</param>
        /// <param name="SALE_ID">销售单编号</param>
        /// <returns>表</returns>
        public DataTable ReturnDiscStatus(string SHOP_ID, string SALE_ID)
        {
            string sql = "select price_type,count(*) from saletmp01 where price_type!='0' and comb_type!=2 and SHOP_ID= " + "'" + SHOP_ID + "'" + " " + "and SALE_ID=" + "'" + SALE_ID + "' group by price_type";
            return base.CreateDataSet(sql).Tables[0];
        }
        /// <summary>
        /// 获得组合餐中每一子餐的标准数量
        /// </summary>
        /// <param name="priceDisc">传过来一个Sales实体</param>
        /// <returns>返回一个DataSet数据集</returns>
        public DataSet SelectAllCombMessage(Sales priceDisc)
        {
            string sql = "SELECT * FROM COMBINATION where COMB_ID=" + "'" + priceDisc.COMB_ID1 + "'" + " " + "and PROD_ID=" + "'" + priceDisc.PROD_ID1 + "'" + " " + "and COMB_SNO=" + "'" + priceDisc.COMB_SNO1 + "'";
            return base.CreateDataSet(sql);
        }
        /// <summary>
        /// 点折扣时执行对SALETMP01表的更新语句
        /// </summary>
        /// <param name="priceDisc">传过来一个Sales实体</param>
        /// <returns>返回true或false</returns>
        public bool UpdateSALETMP01(Sales priceDisc)
        {
            SqlParameter[] para = new SqlParameter[8];
            para[0] = new SqlParameter("@ITEM_DISC", SqlDbType.Money);
            para[1] = new SqlParameter("@PRICE_TYPE", SqlDbType.NChar, 1);
            para[2] = new SqlParameter("@ITEM_DISC_TOT", SqlDbType.Decimal);
            para[3] = new SqlParameter("@ACT_PRICE", SqlDbType.Decimal);
            para[4] = new SqlParameter("@SHOP_ID", SqlDbType.NVarChar, 12);
            para[5] = new SqlParameter("@SALE_ID", SqlDbType.NVarChar, 32);
            para[6] = new SqlParameter("@GROUP_PROD", SqlDbType.NVarChar, 400);
            para[7] = new SqlParameter("@FREE_EMP", SqlDbType.NVarChar, 10);
            para[0].Value = priceDisc.ITEM_DISC1;
            para[1].Value = priceDisc.PRICE_TYPE1;
            para[2].Value = priceDisc.ITEM_DISC_TOT1;
            para[3].Value = priceDisc.ACT_PRICE1;
            para[4].Value = priceDisc.SHOP_ID1;
            para[5].Value = priceDisc.SALE_ID1;
            para[6].Value = priceDisc.GROUP_PROD1;
            if (priceDisc.PRICE_TYPE1 == "1" || priceDisc.PRICE_TYPE1 == "2")
            {
                para[7].Value = priceDisc.FREE_EMP1;
            }
            else
            {
                para[7].Value = DBNull.Value;
            }
            string sql1 = "Update SALETMP01 set ITEM_DISC=@ITEM_DISC,PRICE_TYPE=@PRICE_TYPE, ITEM_DISC_TOT=@ITEM_DISC_TOT,ACT_PRICE=@ACT_PRICE,FREE_EMP=@FREE_EMP where SHOP_ID=@SHOP_ID and SALE_ID=@SALE_ID and GROUP_PROD=@GROUP_PROD and COMB_TYPE in ('0','1')";
            return base.RunSQL(sql1, para);
        }
        /// <summary>
        /// 点折扣时执行对SALETMP01表的套餐中的子餐的PRICE_TYPE字段的更新语句
        /// </summary>
        /// <param name="priceDisc">传过来一个Sales实体</param>
        /// <returns>返回true或false</returns>
        public bool UpdateSALETMP01PRICE_TYPE(Sales priceDisc)
        {
            SqlParameter[] para = new SqlParameter[2];
            para[0] = new SqlParameter("@GROUP_PROD", SqlDbType.NVarChar, 400);
            para[1] = new SqlParameter("@FREE_EMP", SqlDbType.NVarChar, 10);
            para[0].Value = priceDisc.GROUP_PROD1;
            if (priceDisc.PRICE_TYPE1 == "1" || priceDisc.PRICE_TYPE1 == "2")
            {
                para[1].Value = priceDisc.FREE_EMP1;
            }
            else
            {
                para[1].Value = DBNull.Value;
            }
            string sql1 = "Update SALETMP01 set PRICE_TYPE='" + priceDisc.PRICE_TYPE1 + "',FREE_EMP=@FREE_EMP  where SHOP_ID= " + "'" + priceDisc.SHOP_ID1 + "'" + " " + "and SALE_ID=" + "'" + priceDisc.SALE_ID1 + "'" + " " + "and GROUP_PROD= @GROUP_PROD and COMB_TYPE ='2'";
            return base.RunSQL(sql1, para);
        }
        /// <summary>
        /// 点折扣时执行对SALETMP00表的更新语句
        /// </summary>
        /// <param name="priceDisc">传过来一个Sales实体</param>
        /// <returns>返回true或false</returns>
        public bool UpdateSALETMP00(Sales priceDisc)
        {
            SqlParameter[] para1 = new SqlParameter[4];
            para1[0] = new SqlParameter("@TOT_SALES", SqlDbType.Money);
            para1[1] = new SqlParameter("@SHOP_ID", SqlDbType.NVarChar, 12);
            para1[2] = new SqlParameter("@SALE_ID", SqlDbType.NVarChar, 32);
            para1[3] = new SqlParameter("@LAST_UPDATE", SqlDbType.DateTime);
            para1[0].Value = priceDisc.TOT_SALES1;
            para1[1].Value = priceDisc.SHOP_ID1;
            para1[2].Value = priceDisc.SALE_ID1;
            para1[3].Value = DateTime.Now;
            string sql = "Update SALETMP00 set TOT_SALES=@TOT_SALES,LAST_UPDATE=@LAST_UPDATE  where SHOP_ID=@SHOP_ID and SALE_ID=@SALE_ID";
            return base.RunSQL(sql, para1);
        }
        /// <summary>
        /// 修改产品数量时对SALETMP00表的销售单总销售价TOT_SALES字段的更新语句
        /// </summary>
        /// <param name="priceDisc">传过来一个Sales实体</param>
        /// <returns>返回true或false</returns>
        public bool UpdateSALETMP00TOT_SALES(Sales priceDisc)
        {
            SqlParameter[] para1 = new SqlParameter[3];
            para1[0] = new SqlParameter("@TOT_SALES", SqlDbType.Money);
            para1[1] = new SqlParameter("@TOT_QUAN", SqlDbType.Decimal);
            para1[2] = new SqlParameter("@LAST_UPDATE", SqlDbType.DateTime);
            para1[0].Value = priceDisc.TOT_SALES1;
            para1[1].Value = priceDisc.TOT_QUAN1;
            para1[2].Value = DateTime.Now;
            string sql = "Update SALETMP00 set TOT_SALES=@TOT_SALES,TOT_QUAN=@TOT_QUAN,LAST_UPDATE=@LAST_UPDATE where SHOP_ID= " + "'" + priceDisc.SHOP_ID1 + "'" + " " + "and SALE_ID=" + "'" + priceDisc.SALE_ID1 + "'";
            return base.RunSQL(sql, para1);
        }
        /// <summary>
        /// 修改产品数量时对SALETMP01表中的单产品或组合餐产品的数量QTY字段的更新语句
        /// </summary>
        /// <param name="priceDisc">传过来一个Sales实体</param>
        /// <returns>返回true或false</returns>
        public bool UpdateSALETMP01QTY(Sales priceDisc)
        {
            SqlParameter[] para = new SqlParameter[6];
            para[0] = new SqlParameter("@GROUP_PROD", SqlDbType.NVarChar, 400);
            para[0].Value = priceDisc.GROUP_PROD1;
            para[1] = new SqlParameter("@QTY", SqlDbType.Int);
            para[1].Value = priceDisc.QTY1;
            para[2] = new SqlParameter("@ITEM_DISC", SqlDbType.Money);
            para[2].Value = priceDisc.ITEM_DISC1;
            para[3] = new SqlParameter("@ITEM_DISC_TOT", SqlDbType.Money);
            para[3].Value = priceDisc.ITEM_DISC_TOT1;
            para[4] = new SqlParameter("@SHOP_ID", SqlDbType.NVarChar);
            para[4].Value = priceDisc.SHOP_ID1;
            para[5] = new SqlParameter("@SALE_ID", SqlDbType.NVarChar);
            para[5].Value = priceDisc.SALE_ID1;
            string sql1 = "Update SALETMP01 set QTY=@QTY" + ", ITEM_DISC=@ITEM_DISC,ITEM_DISC_TOT=@ITEM_DISC_TOT where SHOP_ID=@SHOP_ID and SALE_ID=@SALE_ID and GROUP_PROD= @GROUP_PROD and COMB_TYPE in ('0','1')";
            return base.RunSQL(sql1, para);
        }
        /// <summary>
        /// 修改产品数量时对SALETMP01表中的组合餐的子产品的数量QTY字段的更新语句
        /// </summary>
        /// <param name="priceDisc">传过来一个Sales实体</param>
        /// <returns>返回true或false</returns>
        public bool UpdateSALETMP01QTY2(Sales priceDisc)
        {
            SqlParameter[] para = new SqlParameter[1];
            para[0] = new SqlParameter("@GROUP_PROD", SqlDbType.NVarChar, 400);
            para[0].Value = priceDisc.GROUP_PROD1;
            string sql1 = "Update SALETMP01 set QTY=" + priceDisc.QTY1 + "where SHOP_ID= " + "'" + priceDisc.SHOP_ID1 + "'" + " " + "and SALE_ID=" + "'" + priceDisc.SALE_ID1 + "'" + " " + "and GROUP_PROD= @GROUP_PROD and COMB_TYPE='2' and COMB_SNO=" + "'" + priceDisc.COMB_SNO1 + "'";
            return base.RunSQL(sql1, para);
        }
        /// <summary>
        /// 对SALETMP01表中的产品的对应单个产品价格做改动时经手员工编号FREE_EMP字段的修改
        /// </summary>
        /// <param name="saleTemp01">传过来一个SALETMP01实体</param>
        /// <returns>返回true或false</returns>
        public bool UpdateSALETMP01FREE_EMP(SALETMP01 saleTemp01)
        {
            SqlParameter[] para = new SqlParameter[1];
            para[0] = new SqlParameter("@GROUP_PROD", SqlDbType.NVarChar, 400);
            para[0].Value = saleTemp01.GROUP_PROD1;
            string sql1 = "Update SALETMP01 set FREE_EMP='" + saleTemp01.FREE_EMP1 + "'" + "where SHOP_ID= " + "'" + saleTemp01.SHOP_ID1 + "'" + " " + "and SALE_ID=" + "'" + saleTemp01.SALE_ID1 + "'" + " " + "and GROUP_PROD= @GROUP_PROD";
            return base.RunSQL(sql1, para);
        }
        /// <summary>
        /// 删除SALETMP01表中的某一组合餐
        /// </summary>
        /// <param name="SHOP_ID">分店编号</param>
        /// <param name="SALE_ID">销售单编号</param>
        /// <param name="GROUP_PROD">组合产品</param>
        /// <returns>返回true或false</returns>
        public bool DeleteSALETMP01Comb(string SHOP_ID, string SALE_ID, string GROUP_PROD)
        {
            SqlParameter[] para = new SqlParameter[3];
            para[0] = new SqlParameter("@SHOP_ID", SqlDbType.NVarChar, 12);
            para[1] = new SqlParameter("@SALE_ID", SqlDbType.NVarChar, 32);
            para[2] = new SqlParameter("@GROUP_PROD", SqlDbType.NVarChar, 400);
            para[0].Value = SHOP_ID;
            para[1].Value = SALE_ID;
            para[2].Value = GROUP_PROD;
            string sql1 = "Delete from SALETMP01 where SHOP_ID=@SHOP_ID and SALE_ID=@SALE_ID and GROUP_PROD=@GROUP_PROD";
            return base.RunSQL(sql1, para);
        }

        /// <summary>
        /// 根据销售单号和店铺号查询Prod_group
        /// </summary>
        /// <param name="shop_id">店铺编号</param>
        /// <param name="sale_id">销售单号</param>
        /// <returns></returns>
        public DataSet GetProd_Group(string shop_id, string sale_id)
        {
            SqlParameter[] para = new SqlParameter[2];
            para[0] = new SqlParameter("@sale_id", SqlDbType.NVarChar);

            para[0].Value = sale_id;
            para[1] = new SqlParameter("@shop_id", SqlDbType.NVarChar);

            para[1].Value = shop_id;

            return base.CreateDataSet("select * from SALETMP01 where SHOP_ID=@SHOP_ID and SALE_ID=@SALE_ID", para);

        }

        /// <summary>
        /// 删去saletmp01、saletmp00、saletmp02中的符合条件的记录
        /// </summary>
        /// <returns>bool</returns>
        public bool DataDeleteSales()
        {
            bool b = false;
            b = base.RunSQL("delete saletmp02 where sale_id in(select sale_id from saletmp00 where LOCKED=0)");
            b = base.RunSQL("delete saletmp01 where sale_id in(select sale_id from saletmp00 where LOCKED=0)");
            b = base.RunSQL("delete saletmp00 where LOCKED=0");
            return b;
        }

        /// <summary>
        /// 删去saletmp01、saletmp00、saletmp02中的符合条件的记录
        /// </summary>
        /// <returns>bool</returns>
        public bool DataDeleteSales(string shop_id, string sale_id)
        {
            bool b = false;
            b = base.RunSQL("delete saletmp02 where SHOP_ID='" + shop_id + "' and  sale_id ='" + sale_id + "'");
            b = base.RunSQL("delete saletmp01 where SHOP_ID='" + shop_id + "' and  sale_id ='" + sale_id + "'");

            return b;
        }

        /// <summary>
        /// 更新saletmp01、saletmp00、saletmp02中的符合条件的记录
        /// </summary>
        /// <param name="shop_id">分店编号</param>
        /// <param name="sale_id">销售单号</param>
        /// <returns>bool</returns>
        public bool DataDelSalesSucess(string shop_id, string sale_id)
        {
            bool b = false;
            b = base.RunSQL("delete saletmp02 where SHOP_ID='" + shop_id + "' and sale_id= '" + sale_id + "'");
            b = base.RunSQL("UPDATE SALETMP01 SET BY_TOKEN = " + 0 + " ,ITEM_DISC=" + 0 + ", ITEM_DISC_TOT=" + 0 + " WHERE SHOP_ID='" + shop_id + "' AND SALE_ID='" + sale_id + "'AND BY_TOKEN = " + 1 + " ");

            return b;
        }

    }
}
