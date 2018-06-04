using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using POS.Data;

//对saletmp01表中的所有值的封装
/*主要公共方法：
主要公共方法：
（1）ReturenRecordNumber，返回记录的条数
（2）ReturnMealTicketNumber，返回餐券类型为 2 的记录条数
（3）ReturnMealTicketProdId(int index)，返回餐券类型为2的商品编号
（4）GetSaleTmp01Data(string SALE_ID, string SHOP_ID)，根据要求从saletmp01表中查询结果集
（5）ReturnSale_Sno(int index)，返回SALE_SNO字段
（6）ReturnSale_Price(int index)，返回SALE_PRICE字段
（7）ReturnTotSale(int index)，返回saleoo中的TotSale
（8）ReturnComb_Type(int index)，返回COMB_TYPE字段
（9）ReturnItem_Disc_Tot(int index)，返回ITEM_DISC_TOT字段
（10）ReturnAct_Price(int index)，返回ACT_PRICE字段
（11）ReturnGroup_Prod(int index)，返回GROUP_PROD字段
（12）ReturnProd_Name1(string PROD_ID)，返回PROD_NAME字段
（13）ReturnQty(int index)，返回QTY字段
（14）ReturnProdID(int index)，返回商品ID
（16）ReturnPriceType(int index)，售价类型
（17）ReturnFreeEmp(int index)，售价
（18）ReturnFreeMemo(int index)，关于免费的信息备注
（19）ReturnItemTax(int index)，销售单税金
（20）ReturnMealTicket(int index)，餐券类型
（21）ReturnRelateProd(int index)，相关产品信息编号
（22）ReturnStatusId(int index)，状态
（23）ReturnCombSno(int index)，组合餐序号
（24）ReturnSalePrice(int index)，产品原始价格

*/
namespace POS.Service
{
    /// <summary>
    /// 获取saletmp01中的所有字段
    /// </summary>
    class GetSaleTmp01
    {
        /// <summary>
        ///用于保存访问数据库的返回值
        /// </summary>
        private DataSet dataSet;
        /// <summary>
        /// 保存保存特定行的值
        /// </summary>
        private DataRow[] dataRow;
        /// <summary>
        /// GetSaleTmp01构造函数
        /// </summary>
        /// <param name="sale_id">销售id号</param>
        /// <param name="shop_id">销售单编号</param>
        public GetSaleTmp01(string sale_id,string shop_id)
        {
            GetSaleTmp01Data(sale_id,shop_id);
        }
        /// <summary>
        /// 获得一个GetSale01实体
        /// </summary>
        /// <param name="sale_id">分店编号</param>
        /// <param name="shop_id">销售单编号</param>
        /// <returns>一个GetSale01实体</returns>
        public static GetSaleTmp01 InitGetSale01(string sale_id, string shop_id)
        {
            return new GetSaleTmp01(sale_id, shop_id);
        }
        #region//公共方法
        /// <summary>
        /// 返回记录的条数
        /// </summary>
        public int ReturenRecordNumber
        {
            get { return this.dataSet.Tables[0].Rows.Count; }
        }
        /// <summary>
        /// 返回餐券类型为 2 的记录条数
        /// </summary>
        public int ReturnMealTicketNumber
        {
            get 
            {               
                return dataRow.Count();
            }
        }
        /// <summary>
        /// 返回餐券类型为2的商品编号
        /// </summary>
        /// <param name="index">记录</param>
        /// <returns>可以用兑换券的商品编号</returns>
        public string ReturnMealTicketProdId(int index)
        {
            return dataRow[index]["PROD_ID"].ToString();
        }
        /// <summary>
        /// 根据要求从saletmp01表中查询结果集
        /// </summary>
        /// <param name="SALE_ID">销售单编号</param>
        /// <param name="SHOP_ID">分店编号</param>
        public void GetSaleTmp01Data(string SALE_ID, string SHOP_ID)
        {
            try 
            {
                dataSet = DataGetSaleTmp01.InitDataGetSaleTmp01().GetSaleTmp01(SALE_ID, SHOP_ID);
                dataRow = this.dataSet.Tables[0].Select("MEAL_TICKET = " + 2 + " and PRICE_TYPE='"+0+"'");
            }
            catch {  }
        }
        /// <summary>
        /// 返回SALE_SNO字段
        /// </summary>
        /// <param name="index">销售单序号</param>
        public string ReturnSale_Sno(int index)
        {
            try { return dataSet.Tables[0].Rows[index]["SALE_SNO"].ToString(); }
            catch { return ""; }
        }

        /// <summary>
        /// 返回SALE_PRICE字段
        /// </summary>
        /// <param name="index">售价</param>
        public string ReturnSale_Price(int index)
        {
            try { return dataSet.Tables[0].Rows[index]["SALE_PRICE"].ToString(); }
            catch { return ""; };
        }


        /// <summary>
        /// 返回saleoo中的TotSale
        /// </summary>
        /// <param name="index">指定行</param>
        /// <returns>返回saleoo中的TotSale</returns>
        public decimal ReturnTotSale(int index)
        {
            try
            {
                return Convert.ToDecimal(dataSet.Tables[0].Rows[index]["SALE_PRICE"]) + Convert.ToDecimal(dataSet.Tables[0].Rows[index]["ITEM_DISC"]);
            }
            catch { return 0; }
        }
        /// <summary>
        /// 返回COMB_TYPE字段
        /// </summary>
        /// <param name="index">指定行</param>
        /// <returns>组合商品类型</returns>
        public string ReturnComb_Type(int index)
        {
            try { return dataSet.Tables[0].Rows[index]["COMB_TYPE"].ToString().Trim (); }
            catch { return ""; };
        }
        /// <summary>
        ///  返回ITEM_DISC_TOT字段
        /// </summary>
        /// <param name="index">指定行</param>
        /// <returns>总折扣</returns>
        public string ReturnItem_Disc_Tot(int index)
        {
            try { return dataSet.Tables[0].Rows[index]["ITEM_DISC_TOT"].ToString(); }
            catch { return ""; };
        }
        /// <summary>
        /// 返回ACT_PRICE字段
        /// </summary>
        /// <param name="index"></param>
        /// <returns>促销价</returns>
        public string ReturnAct_Price(int index)
        {
            try { return dataSet.Tables[0].Rows[index]["ACT_PRICE"].ToString(); }
            catch { return ""; };
        }
        /// <summary>
        /// 返回GROUP_PROD字段
        /// </summary>
        /// <param name="index">指定行</param>
        /// <returns>组合产品</returns>
        public string ReturnGroup_Prod(int index)
        {
            try { return dataSet.Tables[0].Rows[index]["GROUP_PROD"].ToString(); }
            catch { return ""; };
        }
        /// <summary>
        /// 返回PROD_NAME字段
        /// </summary>
        /// <param name="PROD_ID">商品ID</param>
        /// <returns>返回PROD_NAME字段</returns>
        public string ReturnProd_Name1(string PROD_ID)
        {
            try
            {
                return GetProduct00.InitGetProduct00().GetProduct00Name(PROD_ID);
            }
            catch { return ""; };
        }
        /// <summary>
        /// 返回QTY字段
        /// </summary>
        /// <param name="index">指定行</param>
        /// <returns>销售量</returns>
        public decimal ReturnQty(int index)
        {
            try { return Convert.ToDecimal(dataSet.Tables[0].Rows[index]["QTY"].ToString()); }
            catch { return 0; };
        }
        /// <summary>
        /// 返回商品ID
        /// </summary>
        /// <param name="index">指定行</param>
        /// <returns>商品ID</returns>
        public string ReturnProdID(int index)
        {
            try { return dataSet.Tables[0].Rows[index]["PROD_ID"].ToString(); }
            catch { return ""; }
        }

        /// <summary>
        /// 售价
        /// </summary>
        /// <param name="index">记录位置</param>
        /// <returns>售价</returns>
        public char ReturnPriceType(int index)
        {
            try { return Convert.ToChar(dataSet.Tables[0].Rows[index]["PRICE_TYPE"].ToString()); }
            catch { return '0'; }
        }
        /// <summary>
        /// 关于免费的信息备注
        /// </summary>
        /// <param name="index">记录位置</param>
        /// <returns>关于免费的信息备注</returns>
        public string ReturnFreeEmp(int index)
        {
            try { return dataSet.Tables[0].Rows[index]["FREE_EMP"].ToString(); }
            catch { return ""; }
        }
        /// <summary>
        /// 销售单税金
        /// </summary>
        /// <param name="index">记录位置</param>
        /// <returns>销售单税金</returns>
        public string ReturnFreeMemo(int index)
        {
            try { return dataSet.Tables[0].Rows[index]["FREE_MEMO"].ToString(); }
            catch { return ""; }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="index">记录位置</param>
        /// <returns></returns>
        public decimal ReturnItemTax(int index)
        {
            try { return Convert.ToDecimal( dataSet.Tables[0].Rows[index]["ITEM_TAX"].ToString()); }
            catch { return 0; }
        }
        /// <summary>
        /// 餐券类型
        /// </summary>
        /// <param name="index">记录位置</param>
        /// <returns>餐券类型</returns>
        public int ReturnMealTicket(int index)
        {
            try { return Convert.ToInt16(dataSet.Tables[0].Rows[index]["MEAL_TICKET"].ToString()); }
            catch { return 0; }
        }
        /// <summary>
        /// 相关产品信息编号
        /// </summary>
        /// <param name="index">记录位置</param>
        /// <returns>相关产品信息编号</returns>
        public string ReturnRelateProd(int index)
        {
            try { return dataSet.Tables[0].Rows[index]["RELATE_PROD"].ToString(); }
            catch { return ""; }
        }
        /// <summary>
        /// 状态
        /// </summary>
        /// <param name="index">记录位置</param>
        /// <returns>状态</returns>
        public string ReturnStatusId(int index)
        {
            try { return dataSet.Tables[0].Rows[index]["STATUS_ID"].ToString(); }
            catch { return ""; }
        }
        /// <summary>
        /// 组合餐序号
        /// </summary>
        /// <param name="index">记录位置</param>
        /// <returns>组合餐序号</returns>
        public short ReturnCombSno(int index)
        {
            try { return Convert.ToInt16(dataSet.Tables[0].Rows[index]["COMB_SNO"].ToString()); }
            catch { return 0; }
        }
        /// <summary>
        /// 产品原始价格
        /// </summary>
        /// <param name="index">记录位置</param>
        /// <returns></returns>
        public decimal ReturnSalePrice(int index)
        {
            try {return Convert.ToDecimal(dataSet.Tables[0].Rows[index]["SALE_ORGINAL_PRICE"].ToString()); }
            catch{return Convert.ToDecimal(dataSet.Tables[0].Rows[index]["SALE_PRICE"].ToString());}
        }
        #endregion
    }
}




