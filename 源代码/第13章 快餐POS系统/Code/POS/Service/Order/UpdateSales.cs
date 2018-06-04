using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using POS.Models;
using System.Data;
using POS.Data;
using POS.Common;
using System.Windows.Forms;

namespace POS.Service
{
    /// <summary>
    /// 对SALETMP00表中的总销售额TOT_SALES的字段的和SALETMP01表中的折让金额ITEM_DISC字段、售价类型PRICE_TYPE字段、总折扣ITEM_DISC_TOT字段和促销价ACT_PRICE字段的更新
    /// </summary>
    class UpdateSales
    {
        /// <summary>
        /// 获得一个UpdateSales实体
        /// </summary>
        /// <returns>返回一个UpdateSales实体</returns>
        public static UpdateSales InitUpdateSales()
        {
            return new UpdateSales();
        }
        Sales sales = new Sales();
        string SHOP_ID = Info.shop_id;
        string SALE_ID = Info.sale_id;
        ReadIni readIni = new ReadIni("Info.ini");
        int qty;
        /// <summary>
        /// 存放组合餐的数量
        /// </summary> 
        /// <summary>
        /// 对PriceDisc中QTY1、ITEM_DISC_TOT1、SALE_PRICE1字段的赋值
        /// </summary>
        /// <param name="GROUP_PROD">组合产品</param>
        private void setPart1PriceDisc(string GROUP_PROD)
        {
            DataSet dataSet = DataUpdateSales.InitDataUpdateSales().SelectPriceDisc(SHOP_ID, SALE_ID, GROUP_PROD);
            for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
            {
                string comb_type = "";
                try
                {
                    comb_type = dataSet.Tables[0].Rows[i]["COMB_TYPE"].ToString();
                }
                catch { }
                if (comb_type == "1" || comb_type == "0")
                {
                    sales.QTY1 = Convert.ToInt32(dataSet.Tables[0].Rows[i]["QTY"]);
                    //sales.ITEM_DISC_TOT1 = Convert.ToDecimal(dataSet.Tables[0].Rows[i]["ITEM_DISC_TOT"]);
                    sales.SALE_PRICE1 = Convert.ToDecimal(dataSet.Tables[0].Rows[i]["SALE_PRICE"]);
                    sales.SALE_ORGINAL_PRICE1 = Convert.ToDecimal(dataSet.Tables[0].Rows[i]["SALE_ORGINAL_PRICE"]);
                    sales.COMB_TYPE1 = dataSet.Tables[0].Rows[i]["COMB_TYPE"].ToString();
                    break;
                }
            }
        }
        /// <summary>
        /// 对PriceDisc中QTY1、TOT_SALES1、SALE_PRICE1字段的赋值
        /// </summary>
        /// <param name="GROUP_PROD">组合产品</param>
        private void setPart2PriceDisc(string GROUP_PROD)
        {
            DataSet dataSet = DataUpdateSales.InitDataUpdateSales().SelectPriceDisc(SHOP_ID, SALE_ID, GROUP_PROD);
            for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
            {
                string comb_type = "";
                try
                {
                    comb_type = dataSet.Tables[0].Rows[i]["COMB_TYPE"].ToString();
                }
                catch { }
                if (comb_type == "1" || comb_type == "0")
                {
                    //sales.QTY1 = Convert.ToDecimal(dataSet.Tables[0].Rows[i]["QTY"]);
                    //sales.SALE_PRICE1 = Convert.ToDecimal(dataSet.Tables[0].Rows[i]["SALE_PRICE"]);
                    sales.PRICE_TYPE1 = Convert.ToString(dataSet.Tables[0].Rows[i]["PRICE_TYPE"]);
                    break;
                    //DataSet dataSet1 = DataUpdateSales.InitDataUpdateSales().SelectTotalPrice(SHOP_ID, SALE_ID);
                    //DataRow[] dr = dataSet1.Tables[0].Select("SALE_ID='" + SALE_ID + "'");
                    //priceDisc.TOT_SALES1 = Convert.ToDecimal(dr[0]["TOT_SALES"]);
                }
            }
        }
        /// <summary>
        /// 点折扣时对SALETMP01表中ITEM_DISC_TOT、SALE_PRICE、ITEM_DISC、ITEM_DISC_TOT、ACT_PRICE、PRICE_TYPE和SALETMP00表中TOT_SALES字段的重新赋值
        /// </summary>
        /// <param name="GROUP_PROD">组合产品</param>
        /// <param name="Disc">打折的值</param>
        public bool setPriceDisc(string GROUP_PROD, decimal Disc)
        {
            Info.selectedGroupProd = GROUP_PROD;
            Info.isSelectPre = true;
            this.setPart1PriceDisc(GROUP_PROD);
            decimal disc = 0;
            try
            {
                disc = Disc;
            }
            catch { }
            this.setPart2PriceDisc(GROUP_PROD);
            if (sales.PRICE_TYPE1 == "0" || sales.PRICE_TYPE1 == "1" || disc == 100)
            {
                if (0 < disc && disc <= 100)
                {
                    sales.ACT_PRICE1=sales.SALE_PRICE1*disc / 100;
                    sales.ITEM_DISC1 = sales.QTY1*(sales.ACT_PRICE1 - sales.SALE_PRICE1);
                    //sales.ITEM_DISC_TOT1 = sales.ITEM_DISC_TOT1 + sales.ITEM_DISC1 * sales.QTY1;
                    sales.ITEM_DISC_TOT1 = (sales.ACT_PRICE1 - sales.SALE_ORGINAL_PRICE1) * sales.QTY1;
                    sales.PRICE_TYPE1 = "1";
                    sales.FREE_EMP1 = Info.emp_id;
                    if (disc == 100)
                    {
                        sales.PRICE_TYPE1 = "0";
                    }
                    sales.SHOP_ID1 = SHOP_ID;
                    sales.SALE_ID1 = SALE_ID;
                    sales.GROUP_PROD1 = GROUP_PROD;
                    DataUpdateSales.InitDataUpdateSales().UpdateSALETMP01(sales);
                    if (sales.COMB_TYPE1 == "1")
                    {
                        DataUpdateSales.InitDataUpdateSales().UpdateSALETMP01PRICE_TYPE(sales);
                    }
                    DataSet dataSet1 = DataUpdateSales.InitDataUpdateSales().SelectAllInfo(SHOP_ID, SALE_ID);
                    for (int i = 0; i < dataSet1.Tables[0].Rows.Count; i++)
                    {
                        string comb_type = "";
                        try
                        {
                            comb_type = dataSet1.Tables[0].Rows[i]["COMB_TYPE"].ToString();
                        }
                        catch { }
                        if (comb_type == "1" || comb_type == "0")
                        {
                            sales.TOT_SALES1 += Convert.ToDecimal(dataSet1.Tables[0].Rows[i]["ACT_PRICE"]) * Convert.ToDecimal(dataSet1.Tables[0].Rows[i]["QTY"]); ;
                            //if (dataSet1.Tables[0].Rows[i]["PRICE_TYPE"].ToString() == "2")
                            //{
                            //    sales.TOT_SALES1 -= Convert.ToDecimal(dataSet1.Tables[0].Rows[i]["ITEM_DISC"]);
                            //}
                        }

                    }
                    DataUpdateSales.InitDataUpdateSales().UpdateSALETMP00(sales);
                    if (disc == 100)
                    { return false; }
                    return true;
                }
                return false;
            }
            else
            {
                if (sales.PRICE_TYPE1 == "2")
                {
                    MessageBox.Show("已折让，不能再打折!");
                    return false;
                }
            }
            return true;
        }
        /// <summary>
        /// 折让时对SALETMP01表中的部分字段的更新
        /// </summary>
        /// <param name="GROUP_PROD">组合产品</param>
        /// <param name="DiscPrice">折让值</param>
        public bool setDisc(string GROUP_PROD, decimal DiscPrice)
        {
            Info.selectedGroupProd = GROUP_PROD;

            Info.isSelectPre = true;
            this.setPart1PriceDisc(GROUP_PROD);
            decimal discPrice = 0;
            try
            {
                discPrice = DiscPrice;
            }
            catch { }
            this.setPart2PriceDisc(GROUP_PROD);
            //decimal Disc = 100*(sales.SALE_PRICE1 + sales.ITEM_DISC1) / sales.SALE_PRICE1;//为打折率
            if (sales.PRICE_TYPE1 == "0" || sales.PRICE_TYPE1 == "2")
            {
                if (0 < discPrice && discPrice < sales.SALE_PRICE1 * sales.QTY1)
                {
                    sales.ACT_PRICE1 = sales.SALE_PRICE1 - discPrice / sales.QTY1;
                    sales.ITEM_DISC1 = sales.QTY1*(sales.ACT_PRICE1-sales.SALE_PRICE1);
                    sales.ITEM_DISC_TOT1 = (sales.ACT_PRICE1 - sales.SALE_ORGINAL_PRICE1) * sales.QTY1;
                   
                    sales.PRICE_TYPE1 = "2";
                    sales.FREE_EMP1 = Info.emp_id;
                    if (discPrice == 0)
                    {
                        sales.PRICE_TYPE1 = "0";
                    }
                    sales.SHOP_ID1 = SHOP_ID;
                    sales.SALE_ID1 = SALE_ID;
                    sales.GROUP_PROD1 = GROUP_PROD;
                    DataUpdateSales.InitDataUpdateSales().UpdateSALETMP01(sales);
                    if (sales.COMB_TYPE1 == "1")
                    {
                        DataUpdateSales.InitDataUpdateSales().UpdateSALETMP01PRICE_TYPE(sales);
                    }
                    DataSet dataSet1 = DataUpdateSales.InitDataUpdateSales().SelectAllInfo(SHOP_ID, SALE_ID);
                    for (int i = 0; i < dataSet1.Tables[0].Rows.Count; i++)
                    {
                        string comb_type = "";
                        try
                        {
                            comb_type = dataSet1.Tables[0].Rows[i]["COMB_TYPE"].ToString();
                        }
                        catch { }

                    }
                    //sales.TOT_SALES1 += sales.ITEM_DISC1;
                    sales.TOT_SALES1 = sales.ACT_PRICE1 * sales.QTY1;
                    DataUpdateSales.InitDataUpdateSales().UpdateSALETMP00(sales);
                    return true;
                }
                else
                {
                    MessageBox.Show("折让值不能大于或等于产品销售总价!");
                }
                return false;
            }
            else
            {
                if (sales.PRICE_TYPE1 == "1")
                {
                    MessageBox.Show("已打过折，不能再折让!");
                    return false;
                }
            }
            return true;
        }
        /// <summary>
        /// 是否可以改变数量
        /// </summary>
        /// <param name="GROUP_PROD">组号</param>
        /// <param name="type">0代表在原来的基础上加一，1代表在原来的基础上减一，2代表把给定的字段改成给定的值num</param>
        /// <param name="sale_id">销售单号</param>
        /// <param name="num">数量</param>
        /// <returns></returns>
        public bool CanChangeQty(string GROUP_PROD, int type, string sale_id, decimal num)
        {
            string str = "select * from saletmp01 where GROUP_PROD=@group_prod and sale_id=@sale_id";
            System.Data.SqlClient.SqlParameter[] para = new System.Data.SqlClient.SqlParameter[2];
            para[0] = new System.Data.SqlClient.SqlParameter("@GROUP_PROD", SqlDbType.NVarChar);
            para[1] = new System.Data.SqlClient.SqlParameter("@SALE_ID", SqlDbType.NVarChar);
            para[0].Value = GROUP_PROD;
            para[1].Value = sale_id;

            //DataSet ds= DBSql.SCreateDataSet(str);
            DataSet ds = DBSql.SCreateDataSet(str, para);
            try
            {
                if (ds != null)
                {
                    if (type == 0)
                    {
                        return true;
                    }
                    else
                    {
                        decimal sale_price = Convert.ToDecimal(ds.Tables[0].Rows[0]["SALE_PRICE"]);
                        decimal disc_total = -Convert.ToDecimal(ds.Tables[0].Rows[0]["ITEM_DISC_TOT"]);
                        //2012年11月5日11:02:05
                        string price_type = ds.Tables[0].Rows[0]["PRICE_TYPE"].ToString();
                        int number = Convert.ToInt32(ds.Tables[0].Rows[0]["QTY"]);
                        if (1 == type)
                        { number -= 1; }
                        else
                        { number = Convert.ToInt32(num); }

                        if (sale_price * number <= disc_total && price_type.Equals("2"))
                        {
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }

                }
                else
                {
                    return false;
                }
            }
            catch { return false; }
        }

        /// <summary>
        /// 对SALETMP01表中的产品的对应单个产品价格做改动时经手员工编号FREE_EMP字段的修改
        /// </summary>
        /// <param name="saleTemp01">传过来一个SALETMP01实体</param>
        public void setSaleTemp01Free_Emp(SALETMP01 saleTemp01)
        {
            DataUpdateSales.InitDataUpdateSales().UpdateSALETMP01FREE_EMP(saleTemp01);
        }

        /// <summary>
        /// 点修改点餐产品数量时修改产品的数量
        /// </summary>
        /// <param name="GROUP_PROD">组合产品</param>
        /// <param name="type">0代表在原来的基础上加一，1代表在原来的基础上减一，2代表把给定的字段改成给定的值num</param>
        /// <param name="num">修改产品的数量的值</param>
        public void setSalesQty(string GROUP_PROD, int type, decimal num)
        {
            Info.selectedGroupProd = GROUP_PROD;
            Info.isSelectPre = true;
            sales.SHOP_ID1 = SHOP_ID;
            sales.SALE_ID1 = SALE_ID;
            sales.GROUP_PROD1 = GROUP_PROD;
            DataSet dataSet = DataUpdateSales.InitDataUpdateSales().SelectAllInfo(SHOP_ID, SALE_ID);
            if (type == 0 || type==1)
            {
                for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                {
                    string comb_type = "";
                    try
                    {
                        comb_type = dataSet.Tables[0].Rows[i]["COMB_TYPE"].ToString();
                    }
                    catch { }
                    if (comb_type == "0")
                    {
                        if (dataSet.Tables[0].Rows[i]["GROUP_PROD"].ToString() == sales.GROUP_PROD1)
                        {
                            if (type==0)
                            {
                                sales.QTY1 = Convert.ToInt32(dataSet.Tables[0].Rows[i]["QTY"]) + 1;
                            }
                            else
                            {
                                sales.QTY1 = Convert.ToInt32(dataSet.Tables[0].Rows[i]["QTY"])-1;
                            }
                            if (sales.QTY1 == 0)
                            {
                                MessageBox.Show("产品数量不能为零！");
                                break;
                            }
                            sales.COMB_TYPE1 = dataSet.Tables[0].Rows[i]["COMB_TYPE"].ToString();
                            sales.ITEM_DISC1 = sales.QTY1 * (Convert.ToDecimal(dataSet.Tables[0].Rows[i]["ACT_PRICE"]) - Convert.ToDecimal(dataSet.Tables[0].Rows[i]["SALE_PRICE"]));
                            sales.ITEM_DISC_TOT1 = sales.QTY1 * (Convert.ToDecimal(dataSet.Tables[0].Rows[i]["ACT_PRICE"]) - Convert.ToDecimal(dataSet.Tables[0].Rows[i]["SALE_ORGINAL_PRICE"]));
                            DataUpdateSales.InitDataUpdateSales().UpdateSALETMP01QTY(sales);
                        }

                    }
                }
            }
            
            if (type == 2)
            {
                for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                {
                    string comb_type = "";
                    try
                    {
                        comb_type = dataSet.Tables[0].Rows[i]["COMB_TYPE"].ToString();
                    }
                    catch { }
                    try
                    {
                        sales.QTY1 = Convert.ToInt32(num);
                    }
                    catch
                    {
                        sales.QTY1 = -1;
                    }
                    if (sales.QTY1 == -1) { }
                    else
                    {
                        if (sales.QTY1 == 0)
                        {
                            MessageBox.Show("产品数量不能为零！");
                            break;
                        }
                        qty = sales.QTY1;
                        try
                        {
                            if (comb_type == "0")
                            {

                                sales.COMB_TYPE1 = dataSet.Tables[0].Rows[i]["COMB_TYPE"].ToString();
                                sales.ITEM_DISC1 = sales.QTY1 * (Convert.ToDecimal(dataSet.Tables[0].Rows[i]["ACT_PRICE"]) - Convert.ToDecimal(dataSet.Tables[0].Rows[i]["SALE_PRICE"]));
                               sales.ITEM_DISC_TOT1 = sales.QTY1 * (Convert.ToDecimal(dataSet.Tables[0].Rows[i]["ACT_PRICE"]) - Convert.ToDecimal(dataSet.Tables[0].Rows[i]["SALE_ORGINAL_PRICE"]));
                               DataUpdateSales.InitDataUpdateSales().UpdateSALETMP01QTY(sales);
                            }
                        }
                        catch { }
                    }
                }
            }
            if (sales.QTY1 != 0 && sales.QTY1 != -1)
            {
                DataSet dataSet1 = DataUpdateSales.InitDataUpdateSales().SelectAllInfo(SHOP_ID, SALE_ID);
                for (int i = 0; i < dataSet1.Tables[0].Rows.Count; i++)
                {
                    string comb_type = "";
                    try
                    {
                        comb_type = dataSet1.Tables[0].Rows[i]["COMB_TYPE"].ToString();
                    }
                    catch { }
                    if (comb_type == "1" || comb_type == "0")
                    {
                        sales.TOT_SALES1 += Convert.ToDecimal(dataSet1.Tables[0].Rows[i]["ACT_PRICE"]) * Convert.ToDecimal(dataSet1.Tables[0].Rows[i]["QTY"]); ;
                        if (dataSet1.Tables[0].Rows[i]["PRICE_TYPE"].ToString() == "2")
                        {
                            sales.TOT_SALES1 -= Convert.ToDecimal(dataSet1.Tables[0].Rows[i]["ITEM_DISC"]);
                        }
                    }
                    if (comb_type == "2" || comb_type == "0")
                    {
                        sales.TOT_QUAN1 += Convert.ToDecimal(dataSet1.Tables[0].Rows[i]["QTY"]);
                    }
                }
                DataUpdateSales.InitDataUpdateSales().UpdateSALETMP00TOT_SALES(sales);
            }
            qty = 0;
        }

        /// <summary>
        /// 删除选中的一个商品
        /// </summary>
        /// <param name="group_prod">组商品号</param>
        /// <returns>成功删除返回true</returns>
        public bool DelGroupProd(string group_prod)
        {
            DataUpdateSales.InitDataUpdateSales().DeleteSALETMP01Comb(Info.shop_id, Info.sale_id, group_prod);
            DataSet dataSet1 = DataUpdateSales.InitDataUpdateSales().SelectAllInfo(Info.shop_id, Info.sale_id);
            if (dataSet1.Tables[0].Rows.Count == 0)
            {
                Info.sale_sno = 1;
            }
            else
            {
                Info.sale_sno =Convert.ToInt32(dataSet1.Tables[0].Rows[dataSet1.Tables[0].Rows.Count-1][2].ToString()) + 1;
            }
            readIni.WriteString("RepastErp", "sale_sno", Info.sale_sno.ToString());
            return this.UpdateSALETMP00();
        }

        /// <summary>
        /// 对SALETMP00表的更新
        /// </summary>
        /// <returns>成功更改返回true</returns>
        private bool UpdateSALETMP00()
        {
            DataSet dataSet1 = DataUpdateSales.InitDataUpdateSales().SelectAllInfo(Info.shop_id, Info.sale_id);
            Sales sales = new Sales();
            sales.SHOP_ID1 = Info.shop_id;
            sales.SALE_ID1 = Info.sale_id;
            for (int i = 0; i < dataSet1.Tables[0].Rows.Count; i++)
            {
                string comb_type = "";
                try
                {
                    comb_type = dataSet1.Tables[0].Rows[i]["COMB_TYPE"].ToString();
                }
                catch { }
                if (comb_type == "1" || comb_type == "0")
                {
                    sales.TOT_SALES1 += Convert.ToDecimal(dataSet1.Tables[0].Rows[i]["ACT_PRICE"]) * Convert.ToDecimal(dataSet1.Tables[0].Rows[i]["QTY"]); ;
                    if (dataSet1.Tables[0].Rows[i]["PRICE_TYPE"].ToString() == "2")
                    {
                        sales.TOT_SALES1 -= Convert.ToDecimal(dataSet1.Tables[0].Rows[i]["ITEM_DISC"]);
                    }
                }
                if (comb_type == "2" || comb_type == "0")
                {
                    sales.TOT_QUAN1 += Convert.ToDecimal(dataSet1.Tables[0].Rows[i]["QTY"]);
                }
            }
            return DataUpdateSales.InitDataUpdateSales().UpdateSALETMP00TOT_SALES(sales);
        }

        /// <summary>
        /// 返回销售单中是否有折扣、折让数据
        /// </summary>
        public void ReturnIsDiscStatus()
        {
            DataTable ds = DataUpdateSales.InitDataUpdateSales().ReturnDiscStatus(Info.shop_id, Info.sale_id);
            if (ds!=null&&ds.Rows.Count>0)
            {
                for (int i = 0; i < ds.Rows.Count; i++)
                {
                    if (ds.Rows[i][0].ToString().Equals("1"))
                    { Info.isDistinct = true; }
                    if (ds.Rows[i][0].ToString().Equals("2"))
                    { Info.isDistinct = true; }
                }
            }
            else
            {
                Info.isDistinct = false;
                Info.isAllowance = false;
            }
        }

        /// <summary>
        /// 把一个销售单恢复到打折前和打折扣前的状态
        /// </summary>
        /// <param name="shop_id"></param>
        /// <param name="sale_id"></param>
        /// <returns></returns>
        public bool GoToOriginal(string shop_id, string sale_id)
        {
            try
            {
                DataSet dataSet = DataUpdateSales.InitDataUpdateSales().GetProd_Group(shop_id, sale_id);
                if (dataSet != null || dataSet.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                    {

                        //恢复原价（包括折扣和折让）
                        setPriceDisc(dataSet.Tables[0].Rows[i]["group_prod"].ToString(), 100);

                    }

                }
                return true;
            }
            catch
            {
                return false;
            }

        }

        /// <summary>
        ///恢复失败时删除saletmp00、saletmp01、saletmp02中符合条件的记录
        /// </summary>
        /// <returns>成功删除返回true</returns>
        public bool DeleteSales()
        {
            return DataUpdateSales.InitDataUpdateSales().DataDeleteSales(); ;
        }

        /// <summary>
        ///恢复失败时删除saletmp00、saletmp01、saletmp02中符合条件的记录
        /// </summary>
        /// <returns>成功删除返回true</returns>
        public bool DeleteSales(string shop_id, string sale_id)
        {
            return DataUpdateSales.InitDataUpdateSales().DataDeleteSales(shop_id, sale_id); ;
        }

        /// <summary>
        ///恢复成功时删除更新、saletmp01、saletmp02中符合条件的记录
        /// </summary>
        /// <returns>成功删除返回true</returns>
        public bool DelSalesSucess()
        {
            return DataUpdateSales.InitDataUpdateSales().DataDelSalesSucess(Info.shop_id, Info.sale_id); ;
        }
    }
}
