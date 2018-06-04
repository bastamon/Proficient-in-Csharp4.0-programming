using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using POS.Common;
using System.Data;
//对ProductKind的查询并返回DataSet的一个变量
/*类说明：查询产品类别信息

主要公共方法：
（1）GetProductKind()，获得产品类别，限制条件：ENABLE=1
（2）GetProductKind(string prod_id)，获得指定商品编号的产品类别信息，限制条件：ENABLE=1 和指定PROD_ID
*/
namespace POS.Data
{
    /// <summary>
    /// 查询产品类别信息
    /// </summary>
    class DataGetProductKind :DBSql
    {
        /// <summary>
        /// 得到DataGetProductKind一个实体
        /// </summary>
        /// <returns>DataGetProductKind</returns>
        public static DataGetProductKind InitDataGetProductKind()
        {
            return new DataGetProductKind();
        }
        /// <summary>
        /// 获得产品类别
        /// </summary>
        /// <returns>DataSet的一个变量</returns>
        public DataSet GetProductKind()
        {
            //sql是数据库中的一个SQL语句
            string sql = "Select DEP_ID,DEP_NAME,BTN_COLOR,FONT_COLOR,FONT,FONT_SIZE,Select_Color,Btn_Shape from DEPARTMENT where ENABLE='1'";
            return base.CreateDataSet(sql);
        }
        /// <summary>
        /// 获得指定商品编号的产品类别信息
        /// </summary>
        /// <param name="prod_id">商品编号</param>
        /// <returns>DataSet</returns>
        public DataSet GetProductKind(string prod_id)
        {
            string sql = "Select DEP_ID,DEP_NAME,BTN_COLOR,FONT_COLOR,FONT,FONT_SIZE,Select_Color,Btn_Shape from DEPARTMENT where ENABLE='1'and PROD_ID='"+prod_id+"'";
            return base.CreateDataSet(sql);
        }
    }
}
