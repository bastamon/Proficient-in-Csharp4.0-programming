using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using POS.Common;
using POS.View;
using POS.Controls;
using POS.Interface;
using POS.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using POS.Models;
using POS.Service;
using System.Data;
// 插入一条单餐 涉及表: SALETMP00
/*主要公共方法：
（1）InsertSaleTmp1(string prod_Id, int number, decimal discount)，插入单餐（插入表SALETMP01）
*/
namespace POS.Service
{
    /// <summary>
    /// 插入一条单餐
    /// </summary>
    class InsertSingleProd : DBSql
    {
        /// <summary>
        /// 插入单餐（插入表SALETMP01）
        /// </summary>
        /// <param name="prod_Id">商品id号</param>
        /// <param name="number">商品的数量</param>
        /// <param name="discount">折扣</param>
        /// <returns>成功插入返回true</returns>
        public bool InsertSaleTmp1(string prod_Id, int number, decimal discount)
        {
            int count = 0;//商品的原数量
            SALETMP00 saleTmp00 = new SALETMP00();
            saleTmp00.TOT_QUAN1 = Convert.ToDecimal(number);
            if (discount != 0)
            {
                saleTmp00.TOT_SALES1 = Info.sale_price * discount * Convert.ToDecimal(0.01);
            }
            else
            {
                saleTmp00.TOT_SALES1 = Info.sale_price;
            }
            InsertSaleTmp00.InitInsertSaleTmp00().DataUpdateSaleTmp00(saleTmp00.TOT_QUAN1, saleTmp00.TOT_SALES1);
            //首先检查是否有已点过的相同产品，则把那个产品数量加 1并返回
            try
            {
                DataSet dataSet = DataGetSaleTemp01.InitDataDataGetSaleTmp01().GetSales(Info.sale_id);

                for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                {
                    if (prod_Id == dataSet.Tables[0].Rows[i]["prod_id"].ToString() && dataSet.Tables[0].Rows[i]["COMB_TYPE"].ToString ()!="2")
                    {
                        count=int.Parse(dataSet.Tables[0].Rows[i]["qty"].ToString());
                        UpdateSales updateSales = new UpdateSales();
                        updateSales.setSalesQty(prod_Id, 2, count+number);
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                
                throw ex;
            }           

            SaleTmp01 saleTmp01 = new SaleTmp01();

            saleTmp01.Shop_id = Info.shop_id;
            saleTmp01.Sale_id = Info.sale_id;
            saleTmp01.Sale_sno = Info.sale_sno;
            saleTmp01.Prod_id = prod_Id;
            saleTmp01.Sale_price = Info.sale_price;

            saleTmp01.Qty = number;
            if (discount != 0)
            {
                try
                {
                    saleTmp01.Item_disc =(Info.sale_price * discount * Convert.ToDecimal(0.01)- Info.sale_price)*number;
                }

                catch { saleTmp01.Item_disc = 0; }
            }
            else
            {
                saleTmp01.Item_disc = 0;
            }
            saleTmp01.Prom_id = "0";
            saleTmp01.Prom_sno = 0;
            saleTmp01.Price_type = "0";

           // saleTmp01.Free_emp = Info.emp_id;
            saleTmp01.Comb_sale_sno = 0;
            saleTmp01.Comb_sno = 0;
            saleTmp01.Comb_type = "0 ";

            saleTmp01.Item_tax = 0;
            saleTmp01.Outincome = false;
            saleTmp01.Meal_ticket = 0;
            saleTmp01.By_token = false;
            saleTmp01.Relate_prod = prod_Id;

            saleTmp01.Sale_orginal_price = Info.sale_price;
            saleTmp01.Item_disc_tot = 0;
            saleTmp01.Act_price = saleTmp01.Sale_price;
            saleTmp01.Isprom = false;
            saleTmp01.Group_prod = prod_Id;
            saleTmp01.Transfer_status = "0";
            saleTmp01.Status_id = "2";
            try
            {
                return DataInsertSaleTmp01.InitDataInsertSale01().InsertSale01(saleTmp01);

            }
            catch (Exception ex)
            {
                throw ex;
                //return false;
            }
        }

    }
}