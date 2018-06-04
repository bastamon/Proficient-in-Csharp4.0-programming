using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using POS.Data;
//用于员工餐的处理 涉及表： saletmp00、saletmp01
namespace POS.Service
{
    /// <summary>
    /// 员工餐
    /// </summary>
    class StuffMeal
    {
        /// <summary>
        /// 获得一个OffLine实例
        /// </summary>
        /// <returns>StuffMeal对象</returns>
        public static StuffMeal InitStuffMeal()
        {
            return new StuffMeal();
        }

        /// <summary>
        /// 把一个销售单设置为员工餐
        /// </summary>
        /// <param name="emp_id">员工编号</param>
        /// <param name="sale_id">销售单号</param>
        /// <returns>成功执行返回true</returns>
        public bool Stuff_Meal(string emp_id,string sale_id)
        {
          return  DataStuffMeal.InitDataStuffMeal().DataStuff_Meal(emp_id, sale_id);
        }


    }
}
