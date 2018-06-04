using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace POS.Models
{
    //向表SALETMP01中插入数据
    class SALETMP01
    {
        private string SHOP_ID;
        /// <summary>
        /// 1分店编号
        /// </summary>
        public string SHOP_ID1
        {
            get { return SHOP_ID; }
            set { SHOP_ID = value; }
        }

        private string SALE_ID;
        /// <summary>
        /// 2销售单编号
        /// </summary>
        public string SALE_ID1
        {
            get { return SALE_ID; }
            set { SALE_ID = value; }
        }

        private Int16 SALE_SNO;
        /// <summary>
        /// 3销售单序号
        /// </summary>
        public Int16 SALE_SNO1
        {
            get { return SALE_SNO; }
            set { SALE_SNO = value; }
        }

        private string PROD_ID;
        /// <summary>
        /// 4商品编号
        /// </summary>
        public string PROD_ID1
        {
            get { return PROD_ID; }
            set { PROD_ID = value; }
        }

        private decimal SALE_PRICE;
        /// <summary>
        /// 5售价
        /// </summary>
        public decimal SALE_PRICE1
        {
            get { return SALE_PRICE; }
            set { SALE_PRICE = value; }
        }

        private decimal QTY;
        /// <summary>
        /// 6销售量
        /// </summary>
        public decimal QTY1
        {
            get { return QTY; }
            set { QTY = value; }
        }

        private decimal ITEM_DISC;
        /// <summary>
        /// 7折让金额
        /// </summary>
        public decimal ITEM_DISC1
        {
            get { return ITEM_DISC; }
            set { ITEM_DISC = value; }
        }

        private string PRICE_TYPE;
        /// <summary>
        /// 8售价类型
        /// </summary>
        public string PRICE_TYPE1
        {
            get { return PRICE_TYPE; }
            set { PRICE_TYPE = value; }
        }

        private string FREE_EMP;
        /// <summary>
        /// 9对应单个产品价格做改动时经手员工编号
        /// </summary>
        public string FREE_EMP1
        {
            get { return FREE_EMP; }
            set { FREE_EMP = value; }
        }

        private string FREE_MEMO;
        /// <summary>
        ///10 关于免费的信息备注
        /// </summary>
        public string FREE_MEMO1
        {
            get { return FREE_MEMO; }
            set { FREE_MEMO = value; }
        }

        private string COMB_TYPE;
        /// <summary>
        ///11 组合商品类型
        /// </summary>
        public string COMB_TYPE1
        {
            get { return COMB_TYPE; }
            set { COMB_TYPE = value; }
        }

        private decimal ITEM_TAX;
        /// <summary>
        ///12 销售单税金
        /// </summary>
        public decimal ITEM_TAX1
        {
            get { return ITEM_TAX; }
            set { ITEM_TAX = value; }
        }

        private int MEAL_TICKET;
        /// <summary>
        ///13 餐券类型
        /// </summary>
        public int MEAL_TICKET1
        {
            get { return MEAL_TICKET; }
            set { MEAL_TICKET = value; }
        }

        private string RELATE_PROD;
        /// <summary>
        ///14 相关产品信息编号
        /// </summary>
        public string RELATE_PROD1
        {
            get { return RELATE_PROD; }
            set { RELATE_PROD = value; }
        }

        private decimal ITEM_DISC_TOT;
        /// <summary>
        ///15 总折扣
        /// </summary>
        public decimal ITEM_DISC_TOT1
        {
            get { return ITEM_DISC_TOT; }
            set { ITEM_DISC_TOT = value; }
        }

        private decimal ACT_PRICE;
        /// <summary>
        ///16 促销价 
        /// </summary>
        public decimal ACT_PRICE1
        {
            get { return ACT_PRICE; }
            set { ACT_PRICE = value; }
        }

        private string GROUP_PROD;
        /// <summary>
        ///17 组合产品
        /// </summary>
        public string GROUP_PROD1
        {
            get { return GROUP_PROD; }
            set { GROUP_PROD = value; }
        }

        private string STATUS;
        /// <summary>
        ///18 状态
        /// </summary>
        public string STATUS1
        {
            get { return STATUS; }
            set { STATUS = value; }
        }

        private Int16 COMB_SNO;
        /// <summary>
        ///19 组合餐序号
        /// </summary>
        public Int16 COMB_SNO1
        {
            get { return COMB_SNO; }
            set { COMB_SNO = value; }
        }


        private decimal SALE_ORGINAL_PRICE;
        /// <summary>
        ///20 产品原始价格
        /// </summary>
        public decimal SALE_ORGINAL_PRICE1
        {
            get { return SALE_ORGINAL_PRICE; }
            set { SALE_ORGINAL_PRICE = value; }
        }
        private string COMB_ID;
        /// <summary>
        ///21 组合餐编号
        /// </summary>
        public string COMB_ID1
        {
            get { return COMB_ID; }
            set { COMB_ID = value; }
        }
        private Boolean BY_TOKEN;
        /// <summary>
        ///22 使用兑换券
        /// </summary>
        public Boolean BY_TOKEN1
        {
            get { return BY_TOKEN; }
            set { BY_TOKEN = value; }
        }
        private Boolean ISPROM;
        /// <summary>
        ///23 是否促销
        /// </summary>
        public Boolean ISPROM1
        {
            get { return ISPROM; }
            set { ISPROM = value; }
        }

        private Int16 COMB_SALE_SNO;
        /// <summary>
        ///24 组合餐销售序号
        /// </summary>
        public Int16 COMB_SALE_SNO1
        {
            get { return COMB_SALE_SNO; }
            set { COMB_SALE_SNO = value; }
        }
    }
}
