using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace POS.Interface
{
    interface I_GetSaleTmp01
    {

        /// <summary>
        /// 根据销售单号查询数据
        /// </summary>
        /// <param name="sale_id"></param>
        DataSet GetOrderInfo(string sale_id);

        /// <summary>
        /// 商品显示的名字（数据库product00中的Pos_disp字段）
        /// </summary>
        /// <param name="i">记录索引</param>
        /// <returns>第i条记录商品名字</returns>
        string Pos_disp(int i);

        /// <summary>
        /// 记录的个数
        /// </summary>
        int RecordNumber{get;}

        /// <summary>
        /// 商品单价（sale01表中的SALE_PRICE字段）
        /// </summary>
        /// <param name="i">记录索引</param>
        /// <returns>第i条记录商品的价格</returns>
        decimal ProdPrice(int i);

        /// <summary>
        /// 商品数量（sale01表中的QTY字段）
        /// </summary>
        /// <param name="i">记录索引</param>
        /// <returns>第i条记录商品的数量</returns>
        decimal ProdNumber(int i);

        /// <summary>
        /// 商品折扣（计算公式:（ITEM_DISC的绝对值+SALE_PRICE）/SALE_PRICE），子产品的返回0
        /// </summary>
        /// <param name="i">记录索引</param>
        /// <returns>折扣数（两位整数）</returns>
        string Discount(int i);

        /// <summary>
        /// 商品小计（sale01中SALE_PRICE的值）
        /// </summary>
        /// <param name="i">记录索引</param>
        /// <returns>商品小计</returns>
        string GroupProdPriceTotal(int i);

        /// <summary>
        /// 最大销售单序号
        /// </summary>
        /// <param name="i">记录索引</param>
        /// <returns>最大销售单号</returns>
        int Sale_Sno(int i);
        string Comb_type(int i);
    }
}
