using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using POS.Data;
using System.Drawing;
//得到Payment表中的所有字段信息
/*主要公共方法：
（1）、ReturnPay_ID(int index)，返回id号
（2）、ReturnRecordCount，返回记录的条数
（3）、ReturnDispName(int index)，返回button的显示信息
（4）、ReturnFace_Value(int index)，返回显示面值
（5）、ReturnBtnColor(int index)，返回button的背景颜色
（6）、ReturnVisible(int index)，返回按钮可见性（1为可见，0，为隐藏）
（7）、ReturnFont(int index)，返回对字体类型和字体大小的设定
（8）、ReturnFondColor(int index)，返回选中字体的颜色
（9）、ReturnPayName(int index)，返回名称
（10）、ReturnPayTypeId(int index)，返回付款类型
（11）、ReturnDataAccur(int index)，返回数量精度
（12）、ReturnTransferStatus(int index)，返回传输状态
*/
namespace POS.Service
{
    /// <summary>
    /// 对Payment表中的所有字段信息的封装
    /// </summary>
    class GetPayment
    {
        /// <summary>
        /// 私有变量用来保存访问数据库返回值
        /// </summary>
        private DataSet dataSet;
        #region//用于访问数据库的到相应表单
        /// <summary>
        /// 构造函数
        /// </summary>
        public GetPayment()
        {
            GetAllPayment();
        }

        /// <summary>
        /// 得到getpaymen的一个是例
        /// </summary>
        /// <returns>getpaymen的一个对象</returns>
        public static GetPayment InitDataSet()
        {
            return new GetPayment();
        }

        /// <summary>
        /// 得到payment表中的所有数据
        /// </summary>
        public void GetAllPayment()
        {
            try
            {
                dataSet = DataGetPayment.InitDataGetPayment().GetPayment();
            }
            catch { }
        }
        #endregion
        #region//公共方法
        /// <summary>
        /// 返回id号
        /// </summary>
        /// <param name="index">记录</param>
        /// <returns>返回id号</returns>
        public string ReturnPay_ID(int index)
        {
            try
            {
                return dataSet.Tables[0].Rows[index]["PAY_ID"].ToString();
            }
            catch { return "01"; }
        }
        /// <summary>
        /// 返回记录的条数
        /// </summary>
        public int ReturnRecordCount
        {
            get { return this.dataSet.Tables[0].Rows.Count; }
        }

        /// <summary>
        /// 返回button的显示信息
        /// </summary>
        /// <param name="index">记录</param>
        /// <returns>button的显示信息</returns>
        public string ReturnDispName(int index)
        {
            try
            {
                return dataSet.Tables[0].Rows[index]["DISP_NAME"].ToString();
            }
            catch { return "现金"; }
        }
        /// <summary>
        /// 返回给定ID号的付款名字
        /// </summary>
        /// <param name="Pay_Id">付款id号</param>
        /// <returns>付款名字</returns>
        public string ReturnPayName(string Pay_Id)
        {
            try
            {
                return dataSet.Tables[0].Select("PAY_ID='" + Pay_Id + "'")[0]["PAY_NAME"].ToString();
            }
            catch { return "现金"; }
        }
        /// <summary>
        /// 返回给定付款名字的显示名字
        /// </summary>
        /// <param name="money">付款名字</param>
        /// <returns>显示名字</returns>
        public string ReturnPayMoneyName(string money)
        {
            try
            {
                return dataSet.Tables[0].Select("PAY_NAME='" + money + "'")[0]["DISP_NAME"].ToString();
            }
            catch { return "现金"; }
        }
        /// <summary>
        /// 返回显示面值
       /// </summary>
       /// <param name="index">记录</param>
        /// <returns>返回显示面值</returns>
        public decimal ReturnFace_Value(int index)
        {
            try
            {
                return Convert.ToDecimal(dataSet.Tables[0].Rows[index]["Face_Value"].ToString());
            }
            catch { return 0; }
        }

        /// <summary>
        /// 返回button的背景颜色
       /// </summary>
       /// <param name="index">记录</param>
        /// <returns>返回button的背景颜色</returns>
        public Color ReturnBtnColor(int index)
        {
            Color color = Color.White;
            try
            {
                color=ConvertToColor(dataSet.Tables[0].Rows[index]["COLOR"].ToString());      
            }
            catch { }
            return color;
        }
        /// <summary>
        /// 返回按钮可见性（1为可见，0，为隐藏）
        /// </summary>
        /// <param name="index">记录</param>
        /// <returns>按钮可见性</returns>
        public int ReturnVisible(int index)
        {
            try
            {
                return Convert.ToInt16(dataSet.Tables[0].Rows[index]["VISABLE"].ToString());
            }
            catch { return 0; }
        }

        /// <summary>
        /// 把一个十六进制的字符串转会为color类型
        /// </summary>
        /// <param name="value">value</param>
        /// <returns>color类型</returns>
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
        /// <param name="index">记录</param>
        /// <returns>返回对字体类型和字体大小</returns>
        public Font ReturnFont(int index)
        {
            string font = "";
            int font_Size = 5;
            try
            {             
                font =dataSet.Tables[0].Rows[index]["FONT_NAME"].ToString();
                font_Size = Convert.ToInt32(dataSet.Tables[0].Rows[index]["FONT_SIZE"]);
                if (0 == font_Size)
                {
                    font_Size = 5;
                }
            }
            catch { }
            return new Font(font, font_Size);
        }

        /// <summary>
        /// 返回选中字体的颜色
      /// </summary>
      /// <param name="index">记录</param>
        /// <returns>字体的颜色</returns>
        public Color ReturnFondColor(int index)
        {
            string fontColor = "";
            Color color = Color.White;
            try
            {               
                fontColor=dataSet.Tables[0].Rows[index]["FONT_COLOR"].ToString();
                color = ConvertToColor(fontColor);
            }
            catch { }
            return color;
        }
        /// <summary>
        /// 返回 名称
        /// </summary>
        /// <param name="index">记录</param>
        /// <returns>名称</returns>
        public string ReturnPayName(int index)
        {
            try
            {
                return dataSet.Tables[0].Rows[index]["PAY_NAME"].ToString();
            }
            catch { return "现金"; }
        }
        /// <summary>
        /// 返回付款类型
        /// </summary>
        /// <param name="index">记录</param>
        /// <returns>返回付款类型</returns>
        public string ReturnPay_Id(int index)
        {
            try
            {
                return dataSet.Tables[0].Rows[index]["PAY_ID"].ToString();
            }
            catch { return "01"; }
        }
        /// <summary>
        /// 数量精度
        /// </summary>
        /// /// <param name="index">记录</param>
        /// <returns>数量精度</returns>
        public decimal ReturnDataAccur(int index)
        {
            try
            {
                return Convert.ToDecimal(dataSet.Tables[0].Rows[index]["DATA_ACCUR"].ToString());
            }
            catch { return 2; }
        }
        /// <summary>
        /// 返回传输状态
        /// </summary>
        /// <param name="index">指定位置</param>
        /// <returns>传输状态</returns>
        public char ReturnTransferStatus(int index)
        {
            try
            {
                return Convert.ToChar(dataSet.Tables[0].Rows[index]["TRANSFER_STATUS"].ToString());
            }
            catch { return '0'; }
        }
        #endregion
    }
}

