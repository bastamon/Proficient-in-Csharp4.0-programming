using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using POS.Common;
using POS.Data;
using POS.Interface;

// 从saletmp01中读取数据，并按照要求返回  涉及表： SALETMP01、product00
/*主要公共方法：
（1）GetOrderInfo(string sale_id) ，根据销售单号查询数据(排除组合餐中的子产品）
（2）GetBeiCan(string sale_id)，备餐是调用（如果是组合餐包括子产品）
（3）Pos_disp(int i)，商品显示的名字（数据库product00中的Pos_disp字段）
（4）Comb_Type(int i)，获得该产品的类型（是否是组合餐）
（5）RecordNumber，记录的个数
（7）ProdPrice(int i)，商品单价（sale01表中的SALE_PRICE字段）
（8）ProdNumber(int i)，商品数量（sale01表中的QTY字段）
（9）Discount(int i)，商品折扣（计算公式:（ITEM_DISC的绝对值+SALE_PRICE）/SALE_PRICE），子产品的返回0
（10）GroupProdPriceTotal(int i)，商品小计（sale01中SALE_PRICE的值）
（11）Sale_Sno(int i)，最大销售单序号
（12）Comb_type ( int i)，组合商品类型
（13）Act_price(int i)，促销价
（14）Price_type(int i)，售价类型(部分举例：0表示全价，1表示折扣，2表示折让）
*/
namespace POS.Service
{
    /// <summary>
    /// 从saletmp01中读取数据，并按照要求返回
    /// </summary>
    class GetSaleTemp01 : I_GetSaleTmp01
    {
        private DataSet dstaSet; //映射SALETMP01
        /// <summary>
        /// 根据销售单号查询数据(排除组合餐中的子产品）
        /// </summary>
        /// <param name="sale_id">销售单序号</param>
        public DataSet  GetOrderInfo(string sale_id) 
        {
            return this.dstaSet = DataGetSaleTemp01.InitDataDataGetSaleTmp01().GetSaleTmp01Single(sale_id);
        }

        /// <summary>
        /// 备餐是调用（如果是组合餐包括子产品）
        /// </summary>
        /// <param name="sale_id">销售单序号</param>
        public DataSet GetBeiCan(string sale_id)
        {
            return this.dstaSet = DataGetSaleTemp01.InitDataDataGetSaleTmp01().GetSaleTmp01(sale_id);
        }


        /// <summary>
        /// 商品显示的名字（数据库product00中的Pos_disp字段）
        /// </summary>
        /// <param name="i">记录索引</param>
        /// <returns>第i条记录商品名字</returns>
        public string Pos_disp(int i)
        {
            return  dstaSet.Tables[0].Rows[i]["pos_disp"].ToString();
           
        }
        /// <summary>
        /// 获得该产品的类型（是否是组合餐）
        /// </summary>
        /// <param name="i">记录索引</param>
        /// <returns>第i条记录的值</returns>
        public string Comb_Type(int i)
        {
            string COMB_TYPE = dstaSet.Tables[0].Rows[i]["COMB_TYPE"].ToString();
            return COMB_TYPE;
        }

        /// <summary>
        /// 记录的个数
        /// </summary>
        /// <returns>记录个数</returns>
        public int RecordNumber
        {
            get
            {
                return this.dstaSet.Tables[0].Rows.Count;
            }
        }
        /// <summary>
        /// 商品单价（sale01表中的SALE_PRICE字段）
        /// </summary>
        /// <param name="i">记录索引</param>
        /// <returns>第i条记录商品的价格</returns>
        public decimal ProdPrice(int i) { return (decimal)this.dstaSet.Tables[0].Rows[i]["SALE_PRICE"]; }
        /// <summary>
        /// 商品数量（sale01表中的QTY字段）
        /// </summary>
        /// <param name="i">记录索引</param>
        /// <returns>第i条记录商品的数量</returns>
        public decimal ProdNumber(int i) { return (Int32)this.dstaSet.Tables[0].Rows[i]["QTY"]; }
        /// <summary>
        /// 商品折扣（计算公式:（ITEM_DISC的绝对值+SALE_PRICE）/SALE_PRICE），子产品的返回0
        /// </summary>
        /// <param name="i">记录索引</param>
        /// <returns>折扣数（两位整数）</returns>
        public string Discount(int i)
        {
            //若是组合餐
            if (2 == (Int32)this.dstaSet.Tables[0].Rows[i]["COMB_TYPE"])
            {
                return "0";
            }
            else
            {
                return (Math.Abs((Int32)this.dstaSet.Tables[0].Rows[i]["SALE_PRICE"] )/ (Int32)this.dstaSet.Tables[0].Rows[i]["SALE_PRICE"] + 1).ToString();
            }
        }

        /// <summary>
        /// 商品小计（sale01中SALE_PRICE的值）
        /// </summary>
        /// <param name="i">记录索引</param>
        /// <returns>商品小计</returns>
        public string GroupProdPriceTotal(int i) { return this.dstaSet.Tables[0].Rows[i]["SALE_PRICE"].ToString(); }

        /// <summary>
        /// 最大销售单序号
        /// </summary>
        /// <param name="i">记录索引</param>
        /// <returns>最大销售单号</returns>
        public int Sale_Sno(int i) { return (Int32)this.dstaSet.Tables[0].Rows[i]["SALE_SNO"]; }

        /// <summary>
        /// 组合商品类型
        /// </summary>
        /// <param name="i">记录索引</param>
        /// <returns>组合商品类型</returns>
        public string Comb_type ( int i){ return this.dstaSet.Tables[0].Rows[i]["COMB_TYPE"].ToString();  }

        /// <summary>
        ///促销价
        /// </summary>
        /// <param name="i">记录索引</param>
        /// <returns>促销价</returns>
        public decimal Act_price(int i) { return Convert.ToDecimal(this.dstaSet.Tables[0].Rows[i]["Act_price"]); }

        /// <summary>
        /// 售价类型(部分举例：0表示全价，1表示折扣，2表示折让）
        /// </summary>
        /// <param name="i">记录索引</param>
        /// <returns>售价类型</returns>
        public string Price_type(int i) { return Convert.ToString(this.dstaSet.Tables[0].Rows[i]["PRICE_TYPE"]); }

        /// <summary>
        /// 返回折扣金额
        /// </summary>
        /// <param name="i">记录索引</param>
        /// <returns>折扣金额</returns>
        public decimal Item_Disc_Tot(int i) 
        {

            try
            {
                return Convert.ToDecimal(this.dstaSet.Tables[0].Rows[i]["ITEM_DISC_TOT"]);
            }
            catch { return 0; }
        }


        /// <summary>
        /// 返回折扣金额
        /// </summary>
        /// <param name="i">记录索引</param>
        /// <returns>折扣金额</returns>
        public decimal Item_Disc(int i)
        {
            try
            {
                return Convert.ToDecimal(this.dstaSet.Tables[0].Rows[i]["ITEM_DISC"]);
            }
            catch { return 0; }
        }
    }
}
