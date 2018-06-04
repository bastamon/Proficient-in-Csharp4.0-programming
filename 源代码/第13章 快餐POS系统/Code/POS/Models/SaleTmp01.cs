using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace POS.Models
{
    //映射表 SALETMP01 中数据
    class SaleTmp01
    {
        private string shop_id;
        /// <summary>
        /// 1店铺编号
        /// </summary>
        public string Shop_id
        {
            get { return shop_id; }
            set { shop_id = value; }
        }
        private string sale_id;
        /// <summary>
        /// 2销售单编号
        /// </summary>
        public string Sale_id
        {
            get { return sale_id; }
            set { sale_id = value; }
        }
        private int sale_sno;
        /// <summary>
        /// 3销售单序号c=0
        /// </summary>
        public int Sale_sno
        {
            get { return sale_sno; }
            set { sale_sno = value; }
        }
        private string prod_id;
        /// <summary>
        /// 4商品id
        /// </summary>
        public string Prod_id
        {
            get { return prod_id; }
            set { prod_id = value; }
        }
        private decimal sale_price;
        /// <summary>
        /// 5销售价
        /// </summary>
        public decimal Sale_price
        {
            get { return sale_price; }
            set { sale_price = value; }
        }
        private decimal qty;
        /// <summary>
        /// 6数量
        /// </summary>
        public decimal Qty
        {
            get { return qty; }
            set { qty = value; }
        }
        private decimal item_disc;
        /// <summary>
        /// 7打折c=0
        /// </summary>
        public decimal Item_disc
        {
            get { return item_disc; }
            set { item_disc = value; }
        }
        private string prom_id;

        public string Prom_id
        {
            get { return prom_id; }
            set { prom_id = value; }
        }
        private int prom_sno;

        public int Prom_sno
        {
            get { return prom_sno; }
            set { prom_sno = value; }
        }
        private string price_type;
        /// <summary>
        ///8    0-6:全价,折扣,折让,特卖,每日时段特价,招待,总价赠送
        /// </summary>
        public string Price_type
        {
            get { return price_type; }
            set { price_type = value; }
        }
        private string free_emp;
        /// <summary>
        /// 9员工编号
        /// </summary>
        public string Free_emp
        {
            get { return free_emp; }
            set { free_emp = value; }
        }
        private string free_memo;

        public string Free_memo
        {
            get { return free_memo; }
            set { free_memo = value; }
        }
        private int comb_sale_sno;

        public int Comb_sale_sno
        {
            get { return comb_sale_sno; }
            set { comb_sale_sno = value; }
        }
        private int comb_sno;

        public int Comb_sno
        {
            get { return comb_sno; }
            set { comb_sno = value; }
        }
        private string comb_type;
        /// <summary>
        /// 11组合商品类型
        /// </summary>
        public string Comb_type
        {
            get { return comb_type; }
            set { comb_type = value; }
        }

        private decimal item_tax;
        /// <summary>
        ///12 默认：0
        /// </summary>
        public decimal Item_tax
        {
            get { return item_tax; }
            set { item_tax = value; }
        }
        private bool outincome;

        public bool Outincome
        {
            get { return outincome; }
            set { outincome = value; }
        }
        private int meal_ticket;
        /// <summary>
        ///14 默认：0   0普通商品, 1餐券, 2兑换券
        /// </summary>
        public int Meal_ticket
        {
            get { return meal_ticket; }
            set { meal_ticket = value; }
        }

        private bool  by_token;

        public bool  By_token
        {
            get { return by_token; }
            set { by_token = value; }
        }
        private string  relate_prod;
        /// <summary>
        /// 16如果是一个套餐，那么套餐和它的子产品这个字段的值都是套餐的id,如果是一个单产品，那么这个字段就是这个产品的id
        /// </summary>
        public string   Relate_prod
        {
            get { return relate_prod; }
            set { relate_prod = value; }
        }
        private decimal sale_orginal_price;

        public decimal Sale_orginal_price
        {
            get { return sale_orginal_price; }
            set { sale_orginal_price = value; }
        }

        private decimal item_disc_tot = 0;
        /// <summary>
        /// 21
        /// </summary>
        public decimal Item_disc_tot
        {
            get { return item_disc_tot; }
            set { item_disc_tot = value; }
        }
        private decimal act_price;
        /// <summary>
        /// 20
        /// </summary>
        public decimal Act_price
        {
            get { return act_price; }
            set { act_price = value; }
        }

        private bool isprom;

        public bool Isprom
        {
            get { return isprom; }
            set { isprom = value; }
        }
        private string group_prod;
        /// <summary>
        /// 19 比如一个组合餐，那么，那么它们的这个字段的值为所有子产品的prod_id相加
        /// </summary>
        public string Group_prod
        {
            get { return group_prod; }
            set { group_prod = value; }
        }

        private string status_id;
        /// <summary>
        /// 状态,和STATUS表相连  默认2,1新增，2核准，3作废，4结案
        /// </summary>
        public string Status_id
        {
            get { return status_id; }
            set { status_id = value; }
        }


        private string transfer_status;
        /// <summary>
        /// 传输状态，0代表待传，1代表正在传 ，2代表已经传
        /// </summary>
        public string Transfer_status
        {
            get { return transfer_status; }
            set { transfer_status = value; }
        }

    }
}
