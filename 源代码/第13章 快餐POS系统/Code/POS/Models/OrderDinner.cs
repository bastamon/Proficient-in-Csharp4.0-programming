using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace POS.Models
{
    /// <summary>
    /// 次类绑定主界面左上角的DataGridView控件
    /// </summary>
    public class OrderDinner
    {

        private string k;
        /// <summary>
        /// K
        /// </summary>
        public string K
        {
            get { return k; }
            set { k = value; }
        }
        private int xu;
        /// <summary>
        /// 序号
        /// </summary>
        public int Xu
        {
            get { return xu; }
            set { xu = value; }
        }
        private string prodName;
        /// <summary>
        /// 商品名字
        /// </summary>
        public string ProdName
        {
            get { return prodName; }
            set { prodName = value; }
        }
        private decimal price;
        /// <summary>
        /// 单价
        /// </summary>
        public decimal Price
        {
            get { return price; }
            set { price = value; }
        }
        private decimal number;
        /// <summary>
        /// 数量
        /// </summary>
        public decimal Number
        {
            get { return number; }
            set { number = value; }
        }
        private decimal discount;
        /// <summary>
        /// 折扣
        /// </summary>
        public decimal Discount
        {
            get { return discount; }
            set { discount = value; }
        }
        private decimal total;
        /// <summary>
        /// 小记
        /// </summary>
        public decimal Total
        {
            get { return total; }
            set { total = value; }
        }

        private string prod_Id;
        /// <summary>
        /// 商品id（如果是一个组合餐，那么prod_Id是属于一个组合餐的id，不是组合餐包含的自商品的id）
        /// </summary>
        public string Prod_Id
        {
            get { return prod_Id; }
            set { prod_Id = value; }
        }

        private string child_id;
        /// <summary>
        /// 组合餐中自商品的id,如不是组合餐，则不忘这个字段存
        /// </summary>
        public string Child_id
        {
            get { return child_id; }
            set { child_id = value; }
        }

        private string group_prod;
        /// <summary>
        /// ........
        /// </summary>
        public string Group_prod
        {
            get { return group_prod; }
            set { group_prod = value; }
        }

    }
}
