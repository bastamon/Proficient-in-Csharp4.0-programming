using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using Microsoft.Reporting.WinForms;
using HBMISR.Data;
using HBMISR.Service;


namespace HBMISR.GUI.PrintGUI
{
    /// <summary>
    /// 该容器用于打印后备干部的考察材料
    /// </summary>
    public partial class C_HBData : UserControl
    {
        /// <summary>
        /// //记录需要打印的后备干部编号
        /// </summary>
        public ArrayList idlist = new ArrayList();

        /// <summary>
        /// 打印后备干部考察材料初始化
        /// </summary>
        public C_HBData()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 打印后备干部考察材料初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void C_HBData_Load(object sender, EventArgs e)
        {
            this.RV_HBData.SetDisplayMode(DisplayMode.PrintLayout);

            int i = 0;
            string selectid = "";

            for (i = 0; i < idlist.Count; i++)
            {
                if (i == idlist.Count - 1)
                    selectid = selectid + "'" + idlist[i] + "'";
                else
                    selectid = selectid + "'" + idlist[i] + "',";
            }
            string sql = "select CID,name,material from TB_CommonInfo order by rank, joinTeam desc";
            string cond = "cid in (" + selectid + ")";
            DataOperation dataOp = new DataOperation();
            DataTable datatable1 = dataOp.GetOneDataTable_sql(sql);
            DataTable datatable = new DataTable();
            datatable = datatable1.Clone();
            DataView dv = datatable1.AsDataView();
            dv.RowFilter = cond;
            datatable = dv.ToTable();

            List<Class_HBData> HBDataList = new List<Class_HBData>();

            for (i = 0; i < datatable.Rows.Count; i++) 
            {
                Class_HBData c = new Class_HBData();

                c.CID1 = datatable.Rows[i]["CID"].ToString();
                c.HBName1 = datatable.Rows[i]["name"].ToString()+"同志考察材料";
                c.HBmaterial1 = datatable.Rows[i]["material"].ToString();

                HBDataList.Add(c);
            }

            this.BS_HBdata.DataSource = HBDataList;
            this.RV_HBData.RefreshReport();
        }
    }
}
