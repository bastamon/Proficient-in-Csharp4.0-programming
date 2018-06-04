using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using POS.Data;
using POS.Common;
using System.Drawing;
using POS.Models;
//用于得到功能按钮的信息 涉及表： POS_FUNCSET、EMPLOYEE
/*主要公共方法：
（1）、Getfunction(string emp_id,bool isFunctSet)，得到功能按钮的信息，emp：员工编号，isFunctSet：是否为功能设定
（2）、Getfunction1(string emp_id, bool isFunctSet)，得到可更改设定的功能按钮的信息，emp：员工编号，isFunctSet：是否为功能设定
（3）、Getfunction1(string emp_level)，得到该级别可更改设定的功能按钮的信息
（4）、GetEmp_levelName(string emp_id)，得到员工级别名称
*/
namespace POS.Service
{
    /// <summary>
    /// 得到功能按钮的信息
    /// </summary>
    class GetFunctions
    {
        private DataSet dataSet;
        /// <summary>
        /// 获得一个GetFunctions实体
        /// </summary>
        /// <returns></returns>
        public static GetFunctions InitGetProduct1()
        {
            return new GetFunctions();
        }
        /// <summary>
        /// 得到功能按钮的信息
        /// </summary>
        /// <param name="emp_id">员工编号</param>
        /// <param name="isFunctSet">是否为功能设定</param>
        /// <returns>0表示员工编号不</returns>
        public void Getfunction(string emp_id,bool isFunctSet)
        {
            DataSet ds = DataGetFunctons.InitDataGetFunctons().GetEmp_level(emp_id);
            string str = "";
            if (ds.Tables[0].Rows.Count > 0)
            {
                try
                {
                    str = ds.Tables[0].Rows[0]["emp_level"].ToString().Trim();
                    if (str.Equals(""))
                    {
                        dataSet = DataGetFunctons.InitDataGetFunctons().GetFunctionInfo("7", isFunctSet);
                    }
                    else
                    {
                        dataSet = DataGetFunctons.InitDataGetFunctons().GetFunctionInfo(str, isFunctSet);

                    }
                }
                catch { dataSet = DataGetFunctons.InitDataGetFunctons().GetFunctionInfo("7", isFunctSet); }
            }
            else
            {
                //如果根据员工编号找不到对应的员工级别
                dataSet = DataGetFunctons.InitDataGetFunctons().GetFunctionInfo("7", isFunctSet);
            }
          
        }


        /// <summary>
        /// 得到可更改设定的功能按钮的信息
        /// </summary>
        /// <param name="emp_id">员工编号</param>
        /// <param name="isFunctSet">0表示员工编号不</param>
        public void Getfunction1(string emp_id, bool isFunctSet)
        {
            DataSet ds = DataGetFunctons.InitDataGetFunctons().GetEmp_level(emp_id);
            if (ds.Tables[0].Rows.Count > 0&&GetEmp_levelName(emp_id).Tables[0].Rows.Count>1)
            {
                if (ds.Tables[0].Rows[0]["emp_level"].ToString().Equals(""))
                {

                }
                else
                {
                    dataSet = DataGetFunctons.InitDataGetFunctons().GetFunctionInfo((Convert.ToInt32(ds.Tables[0].Rows[0]["emp_level"].ToString())+1).ToString(), isFunctSet);

                }
            }
            else
            {
                //如果根据员工编号找不到对应的员工级别
                //dataSet = DataGetFunctons.InitDataGetFunctons().GetFunctionInfo("7", isFunctSet);
            }

        }
        /// <summary>
        /// 得到该级别可更改设定的功能按钮的信息
        /// </summary>
        /// <param name="emp_level">员工级别</param>
        /// <returns>DataSet数据集</returns>
        public void Getfunction1(string emp_level)
        {
            dataSet = DataGetFunctons.InitDataGetFunctons().GetFunctionInfo(emp_level, true);
        }
        /// <summary>
        /// 得到员工级别名称
        /// </summary>
        /// <param name="emp_id">员工员工编号</param>
        /// <returns>员工级别名称集合</returns>
        public DataSet GetEmp_levelName(string emp_id)
        {
            DataSet ds = DataGetFunctons.InitDataGetFunctons().GetEmp_level(emp_id);
            string emp_level = "";
            try
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    emp_level = ds.Tables[0].Rows[0]["emp_level"].ToString().Trim();
                    if (emp_level == "")
                    {
                        return DataGetFunctons.InitDataGetFunctons().GetEmp_levelName("7");
                    }
                    else
                    {
                        return DataGetFunctons.InitDataGetFunctons().GetEmp_levelName(emp_level);
                    }
                }
                else
                {
                    return DataGetFunctons.InitDataGetFunctons().GetEmp_levelName("7");
                }
            }
            catch
            {
                return DataGetFunctons.InitDataGetFunctons().GetEmp_levelName("7");
            }

        }


        /// <summary>
        /// 返回记录的条数
        /// </summary>
        public int ReturenRecordNumber
        {
            get { return this.dataSet.Tables[0].Rows.Count; }
        }

        /// <summary>
        /// 返回一个功能按钮的ID
        /// </summary>
        /// <param name="index">记录的索引</param>
        /// <returns>功能按钮的ID,转换失败返回0</returns>
        public int ReturnFunct_id(int index)
        {
            try
            {
                return Convert.ToInt32(dataSet.Tables[0].Rows[index]["FUNC_ID"]);
            }
            catch { return 0; }
        }
        /// <summary>
        /// 返回功能按钮上显示的名字
        /// </summary>
        /// <param name="index">记录的索引</param>
        /// <returns>功能按钮上显示的名字</returns>
        public string ReturnProdName1(int index)
        {
            return dataSet.Tables[0].Rows[index]["DISP_NAME"].ToString();
        }
       
      
        /// <summary>
        /// 是否可见
        /// </summary>
        /// <param name="index">记录的索引</param>
        /// <returns>功能按钮可见性，发生异常返回false</returns>
        public bool ReturnVisible(int index)
        {
            try
            {
                return Convert.ToBoolean(dataSet.Tables[0].Rows[index]["VISIBLE"]);
            }
            catch { return false; }
            
        }
        /// <summary>
        /// 功能设定后对功能设定表POS_FUNCSET的部分字段的更新
        /// </summary>
        /// <param name="pos_FuncSet">POS_FUNCSET类的一个实体</param>
        /// <returns>true或false</returns>
        public bool UpdateFunctionSet(POS_FUNCSET pos_FuncSet)
        {
            try
            {
                return DataGetFunctons.InitDataGetFunctons().UpdateFunctionSet(pos_FuncSet);
            }
            catch { return false; }
        }
    
       
        /// <summary>
        /// 返回按钮的背景颜色
        /// </summary>
        /// <param name="index">记录的索引</param>
        /// <returns></returns>
        public Color ReturnBtnColor(int index)
        {
            try
            {
                string btnColor = dataSet.Tables[0].Rows[index]["COLOR"].ToString();
                return ConvertToColor(btnColor);
            }
            catch { return Color.LightSkyBlue; }
        }
        /// <summary>
        /// 返回对字体类型和字体大小的设定
        /// </summary>
        /// <param name="index">记录的索引</param>
        /// <returns></returns>
        public Font ReturnFont(int index)
        {

            string font = "宋体";
            int font_Size = 5;
            try
            {
                font = dataSet.Tables[0].Rows[index]["FONT_NAME"].ToString();
                font_Size = Convert.ToInt32(dataSet.Tables[0].Rows[index]["FONT_SIZE"]);
            }
            catch { }
            return new Font(font, font_Size);
        }
        /// <summary>
        /// 返回字体的颜色
        /// </summary>
        /// <param name="index">记录的索引</param>
        /// <returns></returns>
        public Color ReturnFondColor(int index)
        {
            try
            {
                string fontColor = dataSet.Tables[0].Rows[index]["FONT_COLOR"].ToString();
                return ConvertToColor(fontColor);
            }
            catch { return Color.White; }
        }
        /// <summary>
        /// 返回选中字体颜色
        /// </summary>
        /// <param name="index">记录的索引</param>
        /// <returns></returns>
        public Color ReturnSeletColor(int index)
        {
            try
            {
                string seletColor = dataSet.Tables[0].Rows[index]["Select_Color"].ToString();
                return ConvertToColor(seletColor);
            }
            catch { return Color.Red; }
        }


        /// <summary>
        /// 项目名称
        /// </summary>
        /// <param name="index">记录的索引</param>
        /// <returns>商品名字，发生异常返回空字符串</returns>
        public string ReturnFunctName(int index)
        {
            try
            {
                return dataSet.Tables[0].Rows[index]["FUNC_NAME"].ToString();
            }
            catch { return ""; }
        }
        /// <summary>
        /// 回复所有功能按钮的初始位置和显示名称
        /// </summary>
        /// <returns></returns>
        public bool comeBack()
        {
           return DataGetFunctons.InitDataGetFunctons().comeBack();
        }


        /// <summary>
        /// 位置序号
        /// </summary>
        /// <param name="index">记录的索引</param>
        /// <returns>按钮位置序号，发生异常返回0</returns>
        public int ReturnPositionId(int index)
        {
            try
            {
                return Convert.ToInt32 ( dataSet.Tables[0].Rows[index]["POSITION_ID"]);
            }
            catch { return 0; }
        }


        /// <summary>
        /// 热键
        /// </summary>
        /// <param name="index">记录的索引</param>
        /// <returns>热键，发生异常返回0</returns>
        public int ReturnHotKey(int index)
        {
            try
            {
                return Convert.ToInt32(dataSet.Tables[0].Rows[index]["hotkey"]);
            }
            catch { return 0; }
        }

        /// <summary>
        /// 员工级别
        /// </summary>
        /// <param name="index">记录的索引</param>
        /// <returns>员工级别(转换失败返回7</returns>
        public string ReturnEmpLevel(int index)
        {
            try
            {
                return dataSet.Tables[0].Rows[index]["emp_level"].ToString();
            }
            catch { return "7"; }
        }

        /// <summary>
        /// 把字符十六进制字符串转换成颜色对象
        /// </summary>
        /// <param name="value"></param>
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
    }
}
