using POS.Data;
using System.Data;
// 获取分店名字 涉及表： SHOP00
/*主要公共方法：
（1）ShopName(string shop_id)，调用DataGetShopName中shop方法
（2）ReturenShop_id，返回分店编号
（3）ReturenShop_name，返回分店名字
*/
namespace POS.Service
{
    /// <summary>
    /// 获取分店名字
    /// </summary>
    class GetShopName
    {
        /// <summary>
        /// 结果集
        /// </summary>
        DataSet dataSet = new DataSet();

        /// <summary>
        /// 获取店铺名字
        /// </summary>
        /// <param name="shop_id">分店名字</param>
        public void ShopName(string shop_id)
        {
            this.dataSet = DataGetShopName.InitDataOperation().shop(shop_id);
        }
       
        /// <summary>
        /// 返回分店编号
        /// </summary>
        public string ReturenShop_id
        {
            get { return this.dataSet.Tables[0].Rows[0]["shop_id"].ToString(); }
        }
        /// <summary>
        /// 返回分店名字
        /// </summary>
        public string ReturenShop_name
        {
            get { return this.dataSet.Tables[0].Rows[0]["shop_name"].ToString(); }
        }
    }
}
