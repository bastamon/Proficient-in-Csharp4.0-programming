using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HBMISR.Data;

namespace HBMISR.GUI.OtherGUI
{
    /// <summary>
    /// 窗体类，注册新单位。
    /// </summary>
    public partial class FrmModifyUnit : Form
    {
        /// <summary>
        /// 单位名称
        /// </summary>
        public string strUnitName;
        
        /// <summary>
        /// 单位类别
        /// </summary>
        public string strUnitClass;
        
        /// <summary>
        /// 存储读出的单位信息
        /// </summary>
        DataTable dt;

        /// <summary>
        /// 记录FrmUnit类型的实例
        /// </summary>
        public  FrmUnit frmUnit;
       
        /// <summary>
        /// 图片索引
        /// </summary>
        public int index;

        /// <summary>
        /// 构造函数
        /// </summary>
        public FrmModifyUnit()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ModifyUnit_Load(object sender, EventArgs e)
        {
            DataOperation oper = new DataOperation(Application.StartupPath + "\\DB\\DBHBMSU.db");
            dt = oper.GetOneDataTable_sql("select * from TB_UNIT where unitName='"+strUnitName+"' and unitClass='"+strUnitClass+"'");
            if (dt.Rows.Count > 0)
            {
                this.unitName.Text = dt.Rows[0]["unitName"].ToString();
                this.unitClass.Text = dt.Rows[0]["unitClass"].ToString();
           
            }
        }

        /// <summary>
        /// 修改单位名称，刷新列出单位的窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Sure_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.unitName.Text.Trim().Equals(""))
                {
                    MessageBox.Show("单位名称不能为空！", "提示");
                    this.unitName.Text = "";
                    return;
                }

                DataOperation oper = new DataOperation(Application.StartupPath + "\\DB\\DBHBMSU.db");

                oper.ModifyUnit(this.unitName.Text, this.unitClass.Text, dt.Rows[0]["uid"].ToString());

                MessageBox.Show("修改成功！");

                frmUnit.listView1.SmallImageList = frmUnit.imageList1;//为listView1添加图标

                frmUnit.listView1.Items.Clear();//清除listView1中所有的图标

                string strunitClass = frmUnit.comboBox_select.Text;

                string sql = "select unitName from TB_UNIT where unitClass='" + strunitClass + "'";

                DataTable unitName = oper.GetOneDataTable_sql(sql);

                for (int j = 0; j < unitName.Rows.Count; j++)
                {
                    frmUnit.listView1.Items.Add(unitName.Rows[j]["unitName"].ToString());
                    frmUnit.listView1.Items[j].ImageIndex = 0;
                }

                frmUnit.label_showUnit.Text = "";

                Application.DoEvents();

                this.Dispose();
            }
            catch
            {
                MessageBox.Show("修改失败！","提示");
            }
        }

        /// <summary>
        /// 点击取消按钮事件的处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
