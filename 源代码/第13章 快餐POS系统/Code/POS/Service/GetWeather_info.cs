using System.Data;
using POS.Data;
using System;
//获得天气信息 涉及表： weather表 
/*主要公共方法：
（1）、Weather_info(string shop_id, DateTime w_date)，根据店铺编号和日期查询weather表，并将结果保存到结果集中
主要属性：
（1）、ReturenShop_id，返回分店编号
（2）、ReturenW_date，日期
（4）、ReturenWeather，天气状况
（3）、ReturenLow_temper，最低气温
（4）、ReturenHight_temper，最高气温

*/
namespace POS.Service
{
    class GetWeather_info
    {
         /// <summary>
        /// 结果集
        /// </summary>
        DataSet dataSet = new DataSet();
        
        /// <summary>
        /// 获取天气信息
        /// </summary>
        /// <param name="shop_id">分店编号</param>
        /// <param name="w_date">日期</param>
        public void Weather_info(string shop_id, DateTime w_date)
        {
            this.dataSet = DataGetWeather_info.InitDataOperation().Weather(shop_id,w_date);
        }
       
        /// <summary>
        /// 返回分店编号
        /// </summary>
        public string ReturenShop_id
        {
            get { return this.dataSet.Tables[0].Rows[0]["shop_id"].ToString(); }
        }
        /// <summary>
        /// 日期
        /// </summary>
        public string ReturenW_date
        {
            get { return this.dataSet.Tables[0].Rows[0]["w_date"].ToString(); }
        }
        /// <summary>
        /// 天气状况
        /// </summary>
        public string ReturenWeather
        {
            get { return this.dataSet.Tables[0].Rows[0]["weather"].ToString(); }
        }
        /// <summary>
        /// 最低气温
        /// </summary>
        public string ReturenLow_temper
        {
            get { return this.dataSet.Tables[0].Rows[0]["low_temper"].ToString(); }
        }
        /// <summary>
        /// 最高气温
        /// </summary>
        public string ReturenHight_temper
        {
            get { return this.dataSet.Tables[0].Rows[0]["hight_temper"].ToString(); }
        }
    

    }
}
