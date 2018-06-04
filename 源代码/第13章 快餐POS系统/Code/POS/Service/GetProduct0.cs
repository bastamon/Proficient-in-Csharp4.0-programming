using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data ;
using POS.Data;
using System.Drawing;
//从商品表Product00中查询信息，并按要求返回结果集中的内容 涉及表 ：PRODUCT00
/*主要公共方法：
（1）GetProducts(string DEP_ID,string shop_id)，从商品表Product00中查询信息，并保存到结果集中
（2）GetProducts(string[] prod_id)，根据子产品ID数组得到子产品名字数组
（3）ReturnProdName(int index)，商品名字
（4）ProductNumber，查询结果集中的记录个数
（5）ReturnProId(int index)，返回商品编号
（6）ReturnPosDis(int index)，返回POS显示
（7）ReturnBtnColor(int index)，返回按钮颜色
（8）ReturnFontColor(int index)，返回字体颜色
（9）ReturnFont(int index)，返回字体的类型与字体的大小
（10）ReturnSelectColor(int index)，返回选中时的颜色
（11）ReturnPrice1(int index)，返回商品的定价
（12）ReturnCombo_type(int index)，商品组合类型
*/
namespace POS.Service
{
    /// <summary>
    /// 从商品表Product00中查询信息，并按要求返回结果集中的内容
    /// </summary>
    class GetProduct0
    {
        private DataSet dataset;


        /// <summary>
        /// 获得GetProduct0的实体
        /// </summary>
        /// <returns></returns>
        public static GetProduct0 InitGetProduct0()
        {
            return new GetProduct0();
        }

        /// <summary>
        /// 从商品表Product00中查询信息，并保存到结果集中
        /// </summary>
        /// <param name="DEP_ID">商品类别</param>
        /// <param name="shop_id">分店编号</param>
        public void GetProducts(string DEP_ID,string shop_id)
        {
            //若是加载按钮面板信息执行
            if (DEP_ID.Equals("first"))
            {
                //111218为主餐类的dep_id
                dataset = DataGetProduct0.InitDataGetProduct0().GetProduct0("111218",shop_id);
            }
            else
            {
                dataset = DataGetProduct0.InitDataGetProduct0().GetProduct0(DEP_ID, shop_id);
            }
           
        }

        /// <summary>
        /// 根据子产品ID数组得到子产品名字数组
        /// </summary>
        /// <param name="prod_id">某一类餐饮产品的ID</param>
        public string[] GetProducts(string[] prod_id)
        {
           return  DataGetProduct0.InitDataGetProduct0().GetProduct0(prod_id);
        }

        /// <summary>
        /// 商品名字
        /// </summary>
        /// <param name="index">索引</param>
        /// <returns>商品名字</returns>
        public string ReturnProdName(int index)
        {
            return dataset.Tables[0].Rows[index]["POS_DISP"].ToString();
        }
        /// <summary>
        /// 查询结果集中的记录个数
        /// </summary>
        /// <returns>记录个数</returns>
        public int ProductNumber
        {
            get { return this.dataset.Tables[0].Rows.Count; }
        }
        /// <summary>
        /// 返回商品编号
        /// </summary>
        /// <param name="index">索引</param>
        /// <returns></returns>
        public string ReturnProId(int index)
        {
            return dataset.Tables[0].Rows[index]["PROD_ID"].ToString();
        }
        /// <summary>
        /// 返回POS显示
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public string ReturnPosDis(int index)
        {
            return dataset.Tables[0].Rows[index]["POS_DISP"].ToString();
        }
        /// <summary>
        /// 返回按钮颜色
        /// </summary>
        /// <param name="index">索引</param>
        /// <returns></returns>
        public Color ReturnBtnColor(int index)
        {
            string btnColor = dataset.Tables[0].Rows[index]["Btn_Color"].ToString();
            return ConvertToColor(btnColor );
        }
        /// <summary>
        /// 返回字体颜色
        /// </summary>
        /// <param name="index">索引</param>
        /// <returns></returns>
        public Color ReturnFontColor(int index)
        {
            string fontColor=dataset .Tables [0].Rows [index]["FONT_COLOR"].ToString ();
            return ConvertToColor(fontColor );
        }
        /// <summary>
        /// 返回字体的类型与字体的大小
        /// </summary>
        /// <param name="index">索引</param>
        /// <returns></returns>
        public Font ReturnFont(int index)
        {
            string font;
            int font_Size;

            font = dataset.Tables[0].Rows[index]["FONT"].ToString();
            font_Size =Convert .ToInt32 ( dataset.Tables[0].Rows[index]["FONT_SIZE"]);
            return new Font(font, font_Size);
        }
        /// <summary>
        /// 返回选中时的颜色
        /// </summary>
        /// <param name="index">索引</param>
        /// <returns></returns>
        public Color ReturnSelectColor(int index)
        {
            string selectColor;
            selectColor = dataset.Tables[0].Rows[index]["Select_Color"].ToString();
            return ConvertToColor (selectColor );
        }
        /// <summary>
        /// 返回商品的定价
        /// </summary>
        /// <param name="index">索引</param>
        /// <returns></returns>
        public Decimal ReturnPrice1(int index)
        {
            return Convert.ToDecimal(dataset.Tables[0].Rows[index]["PRICE1"]);
        }
        /// <summary>
        /// 把字符十六进制字符串转换成颜色对象
        /// </summary>
        /// <param name="value">颜色字符串</param>
        /// <returns></returns>
        private Color ConvertToColor(string value)
        {
          try
            {
                //RGB十进制值
                int r = Convert.ToInt32("0x" + value.Substring(3, 2), 16);
                int g = Convert.ToInt32("0x" + value.Substring(5, 2), 16);
                int b = Convert.ToInt32("0x" + value.Substring(7, 2), 16);
                //赋值
                return Color.FromArgb(r, g, b);
            }
            catch { return Color.Red; }
        }

        /// <summary>
        /// 商品组合类型
        /// </summary>
        /// <param name="index">索引</param>
        /// <returns></returns>
        public  string ReturnCombo_type(int index)
        {
            return dataset.Tables[0].Rows[index]["combo_type"].ToString();
        }
    }
}
