using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
//用于向datagridview的第一列添加序号
/*主要公共方法：
（1）AddSeriNumToDataTable(DataTable dt)，向DataGridView的第一列添加序号
*/
namespace POS.Service
{
    /// <summary>
    /// 向datagridview的第一列添加序号
    /// </summary>
    class AddNum
    {
        /// <summary>
        /// 向datagridview的第一列添加序号
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public  DataTable AddSeriNumToDataTable(DataTable dt)
        {
            DataTable dtNew;
            if (dt.Columns.IndexOf("序号") >= 0)
            {
                dtNew = dt;
            }
            else //添加一序号列,并且在第一列
            {
                int rowLength = dt.Rows.Count;
                int colLength = dt.Columns.Count;
                DataRow[] newRows = new DataRow[rowLength];

                dtNew = new DataTable();
                //在第一列添加“序号”列
                dtNew.Columns.Add("序号");
                for (int i = 0; i < colLength; i++)
                {
                    dtNew.Columns.Add(dt.Columns[i].ColumnName);
                    //复制dt中的数据
                    for (int j = 0; j < rowLength; j++)
                    {
                        if (newRows[j] == null)
                            newRows[j] = dtNew.NewRow();
                        //将其他数据填充到第二列之后，因为第一列为新增的序号列
                        newRows[j][i + 1] = dt.Rows[j][i];
                    }
                }
                foreach (DataRow row in newRows)
                {
                    dtNew.Rows.Add(row);
                }

            }
            //对序号列填充，从1递增
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dtNew.Rows[i]["序号"] = i + 1;
            }

            return dtNew;

        }
        /// <summary>
        /// 向datagridview的第一列添加不连续序号
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public DataTable AddSeriNum(DataTable dt)
        {
            DataTable dtNew;
            if (dt.Columns.IndexOf("序号") >= 0)
            {
                dtNew = dt;
            }
            else //添加一序号列,并且在第一列
            {
                int rowLength = dt.Rows.Count;
                int colLength = dt.Columns.Count;
                DataRow[] newRows = new DataRow[rowLength];

                dtNew = new DataTable();
                //在第一列添加“序号”列
                dtNew.Columns.Add("序号");
                for (int i = 0; i < colLength; i++)
                {
                    dtNew.Columns.Add(dt.Columns[i].ColumnName);
                    //复制dt中的数据
                    for (int j = 0; j < rowLength; j++)
                    {
                        if (newRows[j] == null)
                            newRows[j] = dtNew.NewRow();
                        //将其他数据填充到第二列之后，因为第一列为新增的序号列
                        newRows[j][i + 1] = dt.Rows[j][i];
                    }
                }
                foreach (DataRow row in newRows)
                {
                    dtNew.Rows.Add(row);
                }

            }
            //对序号列填充，从1递增
            for (int i = 0,j=0; i < dt.Rows.Count;i++)
            {
                if (!dt.Rows[i]["组合类型"].ToString().Equals("2"))
                {
                    dtNew.Rows[i]["序号"] = j + 1;
                    j++;
                }
                else
                {
                    dtNew.Rows[i]["序号"] = 0;
                }
            }

            return dtNew;

        }
    }
}
