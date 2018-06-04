using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace POS.Models
{
    //对SALETMP00表中的总销售额TOT_SALES的字段的和SALETMP01表中的折让金额ITEM_DISC字段、售价类型PRICE_TYPE字段、总折扣ITEM_DISC_TOT字段和促销价ACT_PRICE字段的更新
    class Sales
    {
        private decimal TOT_SALES;
        /// <summary>
        /// 重新获得SALETMP00表中的总销售额TOT_SALES的字段的值
        /// </summary>
        public decimal TOT_SALES1
        {
            get { return TOT_SALES; }
            set { TOT_SALES = value; }
        }
        private decimal ITEM_DISC;
        /// <summary>
        /// 重新获得SALETMP01表中的折让金额ITEM_DISC字段的值
        /// </summary>
        public decimal ITEM_DISC1
        {
            get { return ITEM_DISC; }
            set { ITEM_DISC = value; }
        }
        private string PRICE_TYPE;
        /// <summary>
        /// SALETMP01表中的售价类型PRICE_TYPE字段的值
        /// </summary>
        public string PRICE_TYPE1
        {
            get { return PRICE_TYPE; }
            set { PRICE_TYPE = value; }
        }
        private decimal ITEM_DISC_TOT;
        /// <summary>
        /// 重新获得SALETMP01表中的总折扣ITEM_DISC_TOT字段的值
        /// </summary>
        public decimal ITEM_DISC_TOT1
        {
            get { return ITEM_DISC_TOT; }
            set { ITEM_DISC_TOT = value; }
        }
        private decimal ACT_PRICE;
        /// <summary>
        /// 重新获得SALETMP01表中的促销价ACT_PRICE字段的值
        /// </summary>
        public decimal ACT_PRICE1
        {
            get { return ACT_PRICE; }
            set { ACT_PRICE = value; }
        }
        private string SHOP_ID;
        /// <summary>
        /// 获得分店编号
        /// </summary>
        public string SHOP_ID1
        {
            get { return SHOP_ID; }
            set { SHOP_ID = value; }
        }
        private string SALE_ID;
        /// <summary>
        /// 获得销售单编号
        /// </summary>
        public string SALE_ID1
        {
            get { return SALE_ID; }
            set { SALE_ID = value; }
        }
        private string GROUP_PROD;
        /// <summary>
        /// 获得组合产品
        /// </summary>
        public string GROUP_PROD1
        {
            get { return GROUP_PROD; }
            set { GROUP_PROD = value; }
        }

        private int QTY;
        /// <summary>
        /// 获得每一商品的销售量
        /// </summary>
        public int QTY1
        {
            get { return QTY; }
            set { QTY = value; }
        }
        private decimal SALE_PRICE;
        /// <summary>
        ///  获得商品售价
        /// </summary>
        public decimal SALE_PRICE1
        {
            get { return SALE_PRICE; }
            set { SALE_PRICE = value; }
        }
        private string COMB_TYPE;
        /// <summary>
        /// 获得组合商品类型
        /// </summary>
        public string COMB_TYPE1
        {
            get { return COMB_TYPE; }
            set { COMB_TYPE = value; }
        }
        private decimal SALE_ORGINAL_PRICE;
        /// <summary>
        /// 获得产品原始价格
        /// </summary>
        public decimal SALE_ORGINAL_PRICE1
        {
            get { return SALE_ORGINAL_PRICE; }
            set { SALE_ORGINAL_PRICE = value; }
        }
        private string PROD_ID;
        /// <summary>
        /// 获得商品编号
        /// </summary>
        public string PROD_ID1
        {
            get { return PROD_ID; }
            set { PROD_ID = value; }
        }
        private Int16 COMB_SNO;
        /// <summary>
        /// 获得组合餐产品序号
        /// </summary>
        public Int16 COMB_SNO1
        {
            get { return COMB_SNO; }
            set { COMB_SNO = value; }
        }
        private string COMB_ID;
        /// <summary>
        /// 获得组合餐编号
        /// </summary>
        public string COMB_ID1
        {
            get { return COMB_ID; }
            set { COMB_ID = value; }
        }
        private decimal TOT_QUAN;
        /// <summary>
        /// SALETMP00表中的总销售量
        /// </summary>
        public decimal TOT_QUAN1
        {
            get { return TOT_QUAN; }
            set { TOT_QUAN = value; }
        }
        private string FREE_EMP;
        /// <summary>
        /// SALETMP01表中的对应单个产品价格做改动时经手员工编号
        /// </summary>
        public string FREE_EMP1
        {
            get { return FREE_EMP; }
            set { FREE_EMP = value; }
        }
        private string FREE_MEMO;
        /// <summary>
        /// SALETMP01表中的关于免费的信息备注
        /// </summary>
        public string FREE_MEMO1
        {
            get { return FREE_MEMO; }
            set { FREE_MEMO = value; }
        }
    }
}
