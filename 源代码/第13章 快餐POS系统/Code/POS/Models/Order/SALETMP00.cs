using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace POS.Models
{
    class SALETMP00
    {
        private string SHOP_ID;
        /// <summary>
        ///1 分店编号
        /// </summary>
        public string SHOP_ID1
        {
            get { return SHOP_ID; }
            set { SHOP_ID = value; }
        }
        private string SALE_ID;
        /// <summary>
        ///2 销售单编号
        /// </summary>
        public string SALE_ID1
        {
            get { return SALE_ID; }
            set { SALE_ID = value; }
        }
        private string POS_ID;
        /// <summary>
        ///3 POS机编号
        /// </summary>
        public string POS_ID1
        {
            get { return POS_ID; }
            set { POS_ID = value; }
        }
        private string SALE_USER;
        /// <summary>
        ///4 售货员编号
        /// </summary>
        public string SALE_USER1
        {
            get { return SALE_USER; }
            set { SALE_USER = value; }
        }
        private decimal TOT_SALES;
        /// <summary>
        ///5 总销售额
        /// </summary>
        public decimal TOT_SALES1
        {
            get { return TOT_SALES; }
            set { TOT_SALES = value; }
        }
        private decimal TOT_QUAN;
        /// <summary>
        ///6 总销售量 
        /// </summary>
        public decimal TOT_QUAN1
        {
            get { return TOT_QUAN; }
            set { TOT_QUAN = value; }
        }
    }
}
