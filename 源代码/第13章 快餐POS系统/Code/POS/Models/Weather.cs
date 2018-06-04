
namespace POS.Models
{
    class Weather_info
    {
        #region
        /// <summary>
        /// 分店编号
        /// </summary>
        private string shop_id;

        public string Shop_id
        {
            get { return shop_id; }
            set { shop_id = value; }
        }

        private string w_date;

        public string W_date
        {
            get { return w_date; }
            set { w_date = value; }
        }

        private string weather;

        public string Weather
        {
            get { return weather; }
            set { weather = value; }
        }

        private string low_temper;

        public string Low_temper
        {
            get { return low_temper; }
            set { low_temper = value; }
        }

        private string hight_temper;

        public string Hight_temper
        {
            get { return hight_temper; }
            set { hight_temper = value; }
        }
        #endregion
    }
}
