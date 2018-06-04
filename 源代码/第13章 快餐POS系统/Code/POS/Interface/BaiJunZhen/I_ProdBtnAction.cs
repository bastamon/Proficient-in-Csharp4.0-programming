using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RepastErpFront.Controls;

namespace RepastErpFront.Interface
{
    interface I_ProdBtnAction
    {
        /// <summary>
        /// 点击商品面板上一个按钮时执行的方法（根据combo_type的值执行相应的功能，
        /// combo_type为0向sale01表中插入数据，combo_type为1或者2弹出一个对话框CombinationForm窗体并prod_Id、CombinationForm窗体要显示的标题、OrderMenu对象、numbe传递过去）
        /// </summary>
        /// <param name="prod_Id">商品id号</param>
        /// <param name="numbe">购买这个商品的数量</param>
        /// <param name="combo_type">0-2：不是组合，标准组合，促销组合</param>
        /// <param name="orderMenu">点单控件对象</param>
        void ProdBtnClick(string prod_Id, int number,string combo_type);
    }
}
