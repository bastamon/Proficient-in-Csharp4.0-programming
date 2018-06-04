using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using POS.Data;
using System.Drawing;
//得到ProductKind中的所有数据信息
/*主要公共方法：
（1）GetProduatKind()，获得产品种类信息
（2）ReturnRecordCount，获得产品种类信息
（3）ReturnDepId(int index)，获得指定编号的产品种类信息
（4）ReturnDeName(int index)，返回一个产品类别编号
（5）ReturnTransferStatus(int index)，传输状态
（6）ReturnBtnColor(int index)，返回button的背景颜色
（7）ReturnFont(int index)，返回对字体类型和字体大小的设定
（8）ReturnFondColor(int index)，字体的颜色
（9）ReturnSeletColor(int index)，选中字体颜色
（10）ReturnsNo(int index)，返回类别序号
*/
namespace POS.Service
{
    /// <summary>
    /// 用来获得产品种类
    /// </summary>
    class GetProductKind
    {
        /// <summary>
        /// 用来保存访问数据库返回值
        /// </summary>
       public DataSet dataSet = new DataSet();

        /// <summary>
        /// 获得一个GetProductKind的一个实体
        /// </summary>
        /// <returns>GetProductKind的一个实体</returns>
        public static GetProductKind InitGetProduatKind()
        {
            return  new GetProductKind();
        }
        #region//公共方法
        /// <summary>
        /// 获得产品种类信息
        /// </summary>
        /// <returns>产品种类信息</returns>
        public void GetProduatKind()
        {
            dataSet = DataGetProductKind.InitDataGetProductKind().GetProductKind();
        }
        /// <summary>
        ///  获得指定编号的产品种类信息
        /// </summary>
        /// <param name="prod_id">商品编号</param>
        public void GetProduatKind(string prod_id)
        {
            dataSet = DataGetProductKind.InitDataGetProductKind().GetProductKind(prod_id);
        }
        /// <summary>
        /// 返回记录的条数
        /// </summary>
        public int ReturnRecordCount
        {
            get { return this.dataSet.Tables[0].Rows.Count; }
        }

         /// <summary>
        /// 返回一个产品类别编号
        /// </summary>
        /// <param name="index">记录的索引</param>
        /// <returns>产品类别编号</returns>
        public string ReturnDepId(int index)
        {
            return dataSet.Tables[0].Rows[index]["DEP_ID"].ToString();
        }

        /// <summary>
        /// 返回一个产品类别名称
        /// </summary>
        /// <param name="index">记录的索引</param>
        /// <returns>产品类别名称</returns>
        public string ReturnDeName(int index)
        {
            return dataSet.Tables[0].Rows[index]["DEP_NAME"].ToString();
        }
        /// <summary>
        /// 传输状态
        /// </summary>
        /// <param name="index">记录的索引</param>
        /// <returns>传输状态</returns>
        public string ReturnTransferStatus(int index)
        {
            return dataSet.Tables[0].Rows[index]["TRANSFER_STATUS"].ToString();
        }

        /// <summary>
        /// 返回button的背景颜色
        /// </summary>
        /// <param name="index">记录的索引</param>
        /// <returns>背景颜色</returns>
        public Color ReturnBtnColor(int index)
        {
            string strColor = "";
            Color btnColor = Color.LightSkyBlue;
            try
            {
                strColor = dataSet.Tables[0].Rows[index]["BTN_COLOR"].ToString();
                btnColor = ConvertToColor(strColor);
            }
            catch{}
            return btnColor;
        }

        /// <summary>
        /// 把字符十六进制字符串转换成颜色对象
        /// </summary>
        /// <param name="value">字符串变量</param>
        /// <returns>color变量</returns>
        private Color ConvertToColor(string value)
        {
            //RGB十进制值
            int r = Convert.ToInt32("0x" + value.Substring(3, 2), 16);
            int g = Convert.ToInt32("0x" + value.Substring(5, 2), 16);
            int b = Convert.ToInt32("0x" + value.Substring(7, 2), 16);
            //赋值
            return Color.FromArgb(r, g, b);
        }

        /// <summary>
        /// 返回对字体类型和字体大小的设定
        /// </summary>
        /// <param name="index">记录的索引</param>
        /// <returns>返回对字体类型和字体大小</returns>
        public Font ReturnFont(int index)
        {
            string font = "";
            int font_Size = 5;
            try
            {
                font = dataSet.Tables[0].Rows[index]["FONT"].ToString();
                font_Size = Convert.ToInt32(dataSet.Tables[0].Rows[index]["FONT_SIZE"]);
            }
            catch { }
            return new Font(font, font_Size);
        }

        /// <summary>
        /// 字体的颜色
        /// </summary>
        /// <param name="index">记录的索引</param>
        /// <returns>返回字体的颜色</returns>
        public Color ReturnFondColor(int index)
        {
            string fontColor = "";
            Color color = Color.White;
            try
            {
                fontColor = dataSet.Tables[0].Rows[index]["FONT_COLOR"].ToString();
                color = ConvertToColor(fontColor);
            }
            catch { }
            return color;
        }

        /// <summary>
        /// 选中字体颜色
        /// </summary>
        /// <param name="index">记录的索引</param>
        /// <returns>返回选中字体颜色</returns>
        public Color ReturnSeletColor(int index)
        {
            string seletColor;
            Color color = Color.Red;
            try
            {
                seletColor = dataSet.Tables[0].Rows[index]["Select_Color"].ToString();
                ConvertToColor(seletColor);
            }
            catch { }
            return color;
        }

        /// <summary>
        /// 类别序号
        /// </summary>
        /// <param name="index">记录的索引</param>
        /// <returns>返回类别序号</returns>
        public int ReturnsNo(int index)
        {
            int sNo = Convert.ToInt32(dataSet.Tables[0].Rows[index]["S_NO"]);
            return sNo;
        }
#endregion
    }
}
