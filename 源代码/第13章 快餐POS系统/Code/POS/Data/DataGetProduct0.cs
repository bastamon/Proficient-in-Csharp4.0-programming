using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using POS.Common;
using System.Data.SqlClient;
using POS.Interface;
// 从PRODUCT00表中得到指定产品类别编号的商品信息 涉及表 ：PRODUCT00
/*主要公共方法：
（1）、GetProduct0(string DEP_ID, string OWNER_SHOP)，获得一个类别的信息
（2）、GetProduct0(string[] prod_id)，根据产品id数组获取产品名字
*/
namespace POS.Data
{
    /// <summary>
    /// 从PRODUCT00表中得到指定产品类别编号的商品信息
    /// </summary>
    class DataGetProduct0 : DBSql
    {
        /// <summary>
        /// 获得一个DataGetProduct0实体
        /// </summary>
        /// <returns>DataGetProduct0对象</returns>
        public  static DataGetProduct0 InitDataGetProduct0()
        {
            return new DataGetProduct0();
        }
       /// <summary>
       /// 获得一个产品类别的相关信息
       /// </summary>
       /// <param name="DEP_ID">产品类别编号</param>
       /// <param name="OWNER_SHOP">所属店号默认： 00000</param>
        /// <returns>DataSet对象</returns>
        public DataSet GetProduct0(string DEP_ID, string OWNER_SHOP)
        {

            SqlParameter[] para = new SqlParameter[2];
            para[0] = new SqlParameter("@DEP_ID", SqlDbType.NVarChar);
            para[0].Value = DEP_ID;
            para[1] = new SqlParameter("@OWNER_SHOP", SqlDbType.NVarChar);
            para[1].Value = OWNER_SHOP;
            //sale_only_in_comb=0排除那么只能做完组合餐的子产品的产品
            return base.CreateDataSet(" select * from PRODUCT00 WHERE DEP_ID='" + DEP_ID + "' and ENABLE = '1'and sale_only_in_comb='0' and (OWNER_SHOP=@OWNER_SHOP or OWNER_SHOP='00000')", para);

        }

        /// <summary>
        /// 根据产品id数组获取产品名字
        /// </summary>
        /// <param name="prod_id">产品id数组</param>
        /// <returns>产品显示名字数组</returns>
        public string[] GetProduct0(string[] prod_id)
        {
            SqlParameter[] para = new SqlParameter[1];
            string[] str = new string[prod_id.Length];
            for (int i = 0; i < prod_id.Length; i++)
            {
                para[0] = new SqlParameter("@prod_id", SqlDbType.NVarChar);
                para[0].Value = prod_id[i];
                try
                {
                    str[i] = base.CreateDataSet("select pos_disp from product00 where prod_id=@prod_id",para).Tables[0].Rows[0]["pos_disp"].ToString();
                }
                catch { str[i] = "无显示名字！"; }
            }
            return str;
        }
    
    }
}
