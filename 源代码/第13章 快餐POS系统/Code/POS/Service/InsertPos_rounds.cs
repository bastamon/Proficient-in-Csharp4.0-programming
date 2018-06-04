using System;
using POS.Models;
using POS.Data;
using System.Data;
using System.Data.SqlClient;
using POS.Common;
// 对Pos_rounds表的插入和更新操作 涉及表： pos_rounds
/*主要公共方法：
（1）、InsertPosrounds(string shop_id, string pos_id, DateTime login_date, decimal cashier_sum, string user_id, string transfer_status, String shift_num, DateTime last_update)，将信息插入Pos_rounds表中
（2）、Update_Posrounds(DateTime exit_date)，更新Pos_rounds表中的退出时间和抽大钞信息
（3）CanLogin(string shop_id, string pos_id, DateTime login_date, decimal cashier_sum, string user_id, string transfer_status, String shift_num, DateTime last_update)能否上线
*/
namespace POS.Service
{
    /// <summary>
    /// 对Pos_rounds表的插入和更新操作
    /// </summary>
    class InsertPos_rounds
    {
        /// <summary>
        /// 得到InsertPos_rounds的一个对象
        /// </summary>
        /// <returns>InsertPos_rounds的一个对象</returns>
        public static InsertPos_rounds InitController() { return new InsertPos_rounds(); }
        /// <summary>
        /// 将信息插入Pos_rounds表中                 
        /// </summary>
        /// <param name="shop_id">1.分店编号</param>
        /// <param name="pos_id">2.POS机编号</param>
        /// <param name="login_date">3.上线时间</param>
        /// <param name="cashier_sum">4. 零用金</param>
        /// <param name="user_id">5.操作员编号</param>
        /// <param name="transfer_status">6.传输状态</param>
        /// <param name="shift_num">7.班次</param>
        /// <param name="last_update">8.最后更新</param>
        /// <returns>调用 DataInsertPos_rounds中InitDataOperation()获得一个对象并调用Insert方法</returns>
        public bool InsertPosrounds(string shop_id, string pos_id, DateTime login_date, decimal cashier_sum, string user_id, string transfer_status, String shift_num, DateTime last_update)
        {
            Pos_rounds pos_rounds = new Pos_rounds();
            pos_rounds.Shop_id = shop_id;
            pos_rounds.Pos_id = pos_id;
            pos_rounds.Login_date = login_date;
            pos_rounds.Cashier_sum = cashier_sum;
            pos_rounds.User_id = user_id;
            pos_rounds.Transfer_status = transfer_status;
            pos_rounds.Shift_num = shift_num;
            pos_rounds.Last_update = last_update;
            return DataInsertPos_rounds.InitDataOperation().Insert(pos_rounds);
        }
        /// <summary>
        /// 更新Pos_rounds表中的退出时间和抽大钞信息
        /// </summary>
        /// <param name="exit_date">退出时间</param>
        /// <returns>调用 DataInsertPos_rounds中InitDataOperation()获得一个对象并调用update方法</returns>
        public bool Update_Posrounds(DateTime exit_date)
        {
            Pos_rounds pos_rounds = new Pos_rounds();
            pos_rounds.Exit_date = exit_date;
            //pos_rounds.Money_out_id = largeBillsNum;
            return DataInsertPos_rounds.InitDataOperation().update(pos_rounds);
       }
    }
}
