using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using POS.Common;
using POS.Models;
//根据员工权限得到功能面板按钮信息 设计表 ：POS_FUNCSET、EMPLOYEE
/*主要公共方法：
（1）GetFunctionInfo(string emp_level, bool isFunctSet)，得到功能按钮信息
（2）GetEmp_level(string emp_id)，得到员工级别
（3）GetEmp_levelName(string Emp_id)，得到员工级别名称
（4）UpdateFunctionSet(POS_FUNCSET pos_FuncSet)，功能设定后对功能设定表POS_FUNCSET的部分字段的更新
（5）comeBack()，回复所有功能按钮的初始位置和显示名称
*/
namespace POS.Data
{
    /// <summary>
    /// 根据员工权限得到功能面板信息
    /// </summary>
    class DataGetFunctons : DBSql
    {
        /// <summary>
        /// 获得一个DataGetFunctons实体
        /// </summary>
        /// <returns></returns>
        public static DataGetFunctons InitDataGetFunctons()
        {
            return new DataGetFunctons();
        }

        /// <summary>
        /// 得到功能按钮信息
        /// </summary>
        /// <param name="emp_level">员工级别</param>
        /// <param name="isFunctSet">是否为功能设定，true为是，false为不是</param>
        /// <returns>DataSet对象</returns>
        public DataSet GetFunctionInfo(string emp_level, bool isFunctSet)
        {
            if (isFunctSet)
            {
                return base.CreateDataSet("select * from POS_FUNCSET where EMP_LEVEL >=" + emp_level + " and visible='1' order by position_id");
            }
            else
            {
                return base.CreateDataSet("select * from POS_FUNCSET where EMP_LEVEL >=" + emp_level + " and visible='1' order by position_id");

            }
        }

        /// <summary>
        /// 得到员工级别
        /// </summary>
        /// <param name="emp_id">员工编号</param>
        /// <returns>DataSet对象</returns>
        public DataSet GetEmp_level(string emp_id)
        {
            
            return base.CreateDataSet("select EMP_LEVEL from EMPLOYEE where EMP_ID='" + emp_id + "'");
        }

        /// <summary>
        /// 得到员工级别名称
        /// </summary>
        /// <param name="Emp_id">职位级别</param>
        /// <returns>DataSet对象</returns>
        public DataSet GetEmp_levelName(string Emp_id)
        {
            //if (Emp_id == "1")
            //{
            //    return base.CreateDataSet("select * from emp_level where Emp_id>='" + Emp_id + "' order by Emp_id");
            //}
            //else
            //{
            return base.CreateDataSet("select * from emp_level where Emp_id>='" + Emp_id + "' order by Emp_id");
            //}

        }
        /// <summary>
        /// 功能设定后对功能设定表POS_FUNCSET的部分字段的更新
        /// </summary>
        /// <param name="pos_FuncSet">POS_FUNCSET类的一个实体</param>
        /// <returns>true或false</returns>
        public bool UpdateFunctionSet(POS_FUNCSET pos_FuncSet)
        {
            SqlParameter[] para = new SqlParameter[9];
            para[0] = new SqlParameter("@FUNC_ID", SqlDbType.Int);
            para[1] = new SqlParameter("@POSITION_ID", SqlDbType.Int);
            para[2] = new SqlParameter("@DISP_NAME", SqlDbType.NVarChar);
            para[3] = new SqlParameter("@EMP_LEVEL", SqlDbType.NChar);
            para[4] = new SqlParameter("@COLOR", SqlDbType.NVarChar);
            para[5] = new SqlParameter("@FONT_COLOR", SqlDbType.NVarChar);
            para[6] = new SqlParameter("@FONT_SIZE", SqlDbType.Int);
            para[7] = new SqlParameter("@FONT_NAME", SqlDbType.NVarChar);
            para[8] = new SqlParameter("@HOTKEY", SqlDbType.Int);
            para[0].Value = pos_FuncSet.FUNC_ID1;
            para[1].Value = pos_FuncSet.POSITION_ID1;
            para[2].Value = pos_FuncSet.DISP_NAME1;
            para[3].Value = pos_FuncSet.EMP_LEVEL1;
            para[4].Value = pos_FuncSet.COLOR1;
            para[5].Value = pos_FuncSet.FONT_COLOR1;
            para[6].Value = pos_FuncSet.FONT_SIZE1;
            para[7].Value = pos_FuncSet.FONT_NAME1;
            para[8].Value = pos_FuncSet.HOTKEY1;
            int HOTKEY = pos_FuncSet.HOTKEY1;
            int POSITION_ID = pos_FuncSet.POSITION_ID1;
            string sql1 = "update POS_FUNCSET set HOTKEY=" + pos_FuncSet.BeforeSetHOTKEY + " from POS_FUNCSET where HOTKEY=" + HOTKEY;
            string sql2;
            sql2 = "select * from pos_funcset where position_id="+POSITION_ID ;
            if(base.CreateDataSet (sql2 ).Tables [0].Rows .Count ==0)
            {
                 sql2 = "update POS_FUNCSET set POSITION_ID=" + 1 + " from POS_FUNCSET where POSITION_ID=" + 1;
                //    pos_FuncSet.BefortSetPOSITION_ID = 1;
                //    POSITION_ID = 1;
            }
            else
            {
                sql2 = "update POS_FUNCSET set POSITION_ID=" + pos_FuncSet.BefortSetPOSITION_ID + " from POS_FUNCSET where POSITION_ID=" + POSITION_ID;
            }
                bool a = false;
            if (base.RunSQL(sql1).Equals(true))
            {
                a = true;
            }
            bool b = false;
            if (base.RunSQL(sql2).Equals(true))
            {
                b = true;
            }
            if (a && b)
            {
                string sql3 = "update POS_FUNCSET set POSITION_ID=@POSITION_ID,DISP_NAME=@DISP_NAME,EMP_LEVEL=@EMP_LEVEL," +
                "COLOR=@COLOR,FONT_COLOR=@FONT_COLOR,FONT_SIZE=@FONT_SIZE,FONT_NAME=@FONT_NAME,HOTKEY=@HOTKEY" + " from POS_FUNCSET where FUNC_ID=@FUNC_ID";
                return base.RunSQL(sql3, para);
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 回复所有功能按钮的初始位置和显示名称
        /// </summary>
        /// <returns>true或false</returns>
        public bool comeBack()
        {
            string sql = "update POS_FUNCSET set DISP_NAME=FUNC_NAME,POSITION_ID=DEFAULT_POSITION,COLOR=DEFAULT_COLOR,"+
                "FONT_COLOR=DEFAULT_FONT_COLOR,FONT_SIZE=DEFAULT_FONT_SIZE,FONT_NAME=DEFAULT_FONT_NAME,EMP_LEVEL=DEFAULT_EMP_LEVEL";
            return base.RunSQL(sql);
        }
    }
}
