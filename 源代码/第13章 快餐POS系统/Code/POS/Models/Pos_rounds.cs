using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace POS.Models
{
    /// <summary>
    /// 映射表 POS_ROUNDS 中的数据（POS交班表）
    /// </summary>
    class Pos_rounds
    {
        #region //字段和属性
        /// <summary>
        /// 1.分店编号
        /// </summary>
        private string shop_id;

        /// <summary>
        /// 1.分店编号
        /// </summary>
        public string Shop_id
        {
            get { return shop_id; }
            set { shop_id = value; }
        }

        /// <summary>
        /// 2.POS机编号
        /// </summary>
        private string pos_id;

        /// <summary>
        /// 2.POS机编号
        /// </summary>
        public string Pos_id
        {
            get { return pos_id; }
            set { pos_id = value; }
        }

        /// <summary>
        /// 3.上线时间
        /// </summary>
        private DateTime  login_date;

        /// <summary>
        /// 3.上线时间
        /// </summary>
        public DateTime  Login_date
        {
            get { return login_date; }
            set { login_date = value; }
        }

        /// <summary>
        ///4. 零用金
        /// </summary>
        private decimal  cashier_sum;

        /// <summary>
        ///4. 零用金
        /// </summary>
        public decimal Cashier_sum
        {
            get { return cashier_sum; }
            set { cashier_sum = value; }
        }

        /// <summary>
        /// 5.下线时间
        /// </summary>
        private DateTime  exit_date;

        /// <summary>
        /// 5.下线时间
        /// </summary>
        public DateTime Exit_date
        {
            get { return exit_date; }
            set { exit_date = value; }
        }

        /// <summary>
        /// 6.操作员编号
        /// </summary>
        private string user_id;

        /// <summary>
        /// 6.操作员编号
        /// </summary>
        public string User_id
        {
            get { return user_id; }
            set { user_id = value; }
        }

        /// <summary>
        /// 7.传输状态
        /// </summary>
        private string transfer_status;

        /// <summary>
        /// 7.传输状态
        /// </summary>
        public string Transfer_status
        {
            get { return transfer_status; }
            set { transfer_status = value; }
        }

        /// <summary>
        /// 8.班次
        /// </summary>
        private String  shift_num;

        /// <summary>
        /// 8.班次
        /// </summary>
        public String Shift_num
        {
            get { return shift_num; }
            set { shift_num = value; }
        }

        /// <summary>
        /// 9.最后更新
        /// </summary>
        private DateTime last_update;

        /// <summary>
        /// 9.最后更新
        /// </summary>
        public DateTime Last_update
        {
            get { return last_update; }
            set { last_update = value; }
        }

        /// <summary>
        /// 10.抽大钞编号
        /// </summary>
        private String money_out_id;

        /// <summary>
        /// 10.抽大钞编号
        /// </summary>
        public String Money_out_id
        {
            get { return money_out_id; }
            set { money_out_id = value; }
        }

        #endregion
    }
}
