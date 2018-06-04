using POS.Models;
using System.Data.SqlClient;
using System.Data;
using POS.Common;
using System;
//对员工上下线的相关操作，以及判段当前员工是否在线 涉及表 ： pos_rounds
/*主要公共方法：
（1）Insert(Pos_rounds pos_rounds)，插入pos_rounds表
（2）update(Pos_rounds pos_rounds)，更新pos_rounds表
（3）CanLogin(Pos_rounds pos_rounds) 查询本店后台判断是否可以上线
*/
namespace POS.Data
{
    /// <summary>
    /// 对pos_rounds表的插入和更新操作
    /// </summary>
    class DataInsertPos_rounds : DBSql
    {
        /// <summary>
        /// 获得一个DataInsertPos_rounds的一个对象
        /// </summary>
        /// <returns>DataInsertPos_rounds的一个对象</returns>
        public static DataInsertPos_rounds InitDataOperation() { return new DataInsertPos_rounds(); }
        /// <summary>
        ///插入pos_rounds表
        /// </summary>
        /// <param name="pos_rounds">Pos_rounds的映射表的类的对象，用于调用其字段赋值</param>
        /// <returns>正确插入返回true 否则 false</returns>
        public bool Insert(Pos_rounds pos_rounds)
        {

            //本地插入交班信息
            bool b = false;
            SqlParameter[] para = new SqlParameter[8];
            para[0] = new SqlParameter("@shop_id", SqlDbType.NVarChar);
            para[1] = new SqlParameter("@pos_id", SqlDbType.NVarChar);
            para[2] = new SqlParameter("@login_date", SqlDbType.DateTime );
            para[3] = new SqlParameter("@cashier_sum", SqlDbType.Decimal);
            para[4] = new SqlParameter("@user_id", SqlDbType.NVarChar);
            para[5] = new SqlParameter("@transfer_status", SqlDbType.NChar);
            para[6] = new SqlParameter("@shift_num", SqlDbType.NVarChar);
            para[7] = new SqlParameter("@last_update", SqlDbType.DateTime);

            para[0].Value = pos_rounds.Shop_id;
            para[1].Value = pos_rounds.Pos_id;
            para[2].Value = pos_rounds.Login_date;
            para[3].Value = pos_rounds.Cashier_sum;
            para[4].Value = pos_rounds.User_id;
            para[5].Value = pos_rounds.Transfer_status;
            para[6].Value = pos_rounds.Shift_num;
            para[7].Value = pos_rounds.Last_update;
           
            b = base.RunSQL("Insert into pos_rounds(shop_id,pos_id,login_date,cashier_sum,user_id,transfer_status,shift_num,last_update) values(@shop_id,@pos_id,@login_date,@cashier_sum,@user_id,@transfer_status,@shift_num,@last_update)", para);

            return b;
        }
        /// <summary>
        /// 更新pos_rounds表
        /// </summary>
        /// <param name="pos_rounds">Pos_rounds的对象pos_rounds</param>
        /// <returns>正确更新返回true 否则 false</returns>
        public bool update(Pos_rounds pos_rounds)
        {
            bool b = false;
            SqlParameter[] para = new SqlParameter[3];
            para[0] = new SqlParameter("@exit_date", SqlDbType.DateTime);
            para[1] = new SqlParameter("@pos_id", SqlDbType.NVarChar);
            para[2] = new SqlParameter("@SHIFT_NUM", SqlDbType.NVarChar);
            para[0].Value = pos_rounds.Exit_date;
            para[1].Value = Info.pos_id;
            para[2].Value = Info.shift_num;
            b = base.RunSQL("update pos_rounds set exit_date=@exit_date  where exit_date is null and pos_id=@pos_id and SHIFT_NUM=@SHIFT_NUM", para);
            return b;
        }
    }
}
