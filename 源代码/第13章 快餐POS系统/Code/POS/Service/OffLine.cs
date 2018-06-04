using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using POS.Data;
using POS.Common;
// 下线时调用，判断是否可以下线 涉及表： 涉及表： SALETMP01、product00
/*主要公共方法：
（1）、IsOffLine()，可以下线返回true,不可以返回false
*/
namespace POS.Service
{
    /// <summary>
    /// 下线时调用，判断是否可以下线
    /// </summary>
    class OffLine
    {
        /// <summary>
        /// 获得一个OffLine实例
        /// </summary>
        /// <returns></returns>
        public static OffLine InitOffLine()
        {
            return new OffLine();
        }

        /// <summary>
        /// 可以下线返回true,不可以返回false
        /// </summary>
        /// <returns></returns>
        public bool IsOffLine()
        {
            //saletemp01中的记录大于0
            if (DataGetSaleTemp01.InitDataDataGetSaleTmp01().GetSaleTmp01(Info.sale_id).Tables[0].Rows.Count > 0)
            {
                return false;
            }
            else
            { 
                return true;
            }
            
        }
    }
}
