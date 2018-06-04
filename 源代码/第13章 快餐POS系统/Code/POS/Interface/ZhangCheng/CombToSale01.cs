using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using POS.Controls;

namespace POS.Interface
{
    interface CombToSale01
    {
        /// <summary>
        /// 推出组合餐设定页面时执行的方法
        /// </summary>
        /// <param name="comb_id">组合餐编号</param>
        /// <param name="select_child_prod_id">该组合餐编号下的子产品id号</param>
        /// <param name="number">购买的产品数量</param>
        /// <param name="orderMenu">点单控件</param>
        /// <param name="discount">折扣</param>
        /// <returns>是否执行成功</returns>
        bool CombinationExitClick(string comb_id, string[] select_child_prod_id,int number, OrderMenu orderMenu,decimal discount);
    }
}
