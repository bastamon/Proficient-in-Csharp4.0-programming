using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HBMISR.GUI.NoteGUI;
using HBMISR.Data;

namespace HBMISR.GUI.MainGUI
{
    /// <summary>
    /// 检索信息条
    /// </summary>
    public partial class ControlSearch : UserControl
    {
        public ControlMain controlMain;
        public FrmInformationGathering informationGathering = null;
        public FrmSituationRegist FrmSituationRegist = null;
        public string sql = "select * from TB_CommonInfo where isDelete='0'";

        /// <summary>
        /// 检索信息条的构造函数
        /// </summary>
        public ControlSearch()
        {
            InitializeComponent();          
        }

        /// <summary>
        /// 搜索后备干部信息
        /// </summary>
        /// <returns>后备干部信息</returns>
        public DataTable button_Search_Click()
        {
            if (!option.Text.Equals(""))
            {
                DataOperation dataOperation = new DataOperation();

                if (option.Text.Equals("姓名"))
                {
                    if (controlMain.comboBox1.Text.Equals("正职后备"))
                        sql = "select * from TB_CommonInfo where ((name like '%" + inputContent.Text + "%') or (extend3 like '%" + inputContent.Text + "%')) and  isDelete='" + 0 + "' and qd='1'";
                    else
                        if (controlMain.comboBox1.Text.Equals("副职后备"))
                            sql = "select * from TB_CommonInfo where ((name like '%" + inputContent.Text + "%') or (extend3 like '%" + inputContent.Text + "%')) and  isDelete='" + 0 + "' and qd='0'";
                        else
                            sql = "select * from TB_CommonInfo where ((name like '%" + inputContent.Text + "%') or (extend3 like '%" + inputContent.Text + "%')) and  isDelete='" + 0 + "'";

                    controlMain.isReactChange = false;
                    DataTable dt = dataOperation.GetOneDataTable_sql(sql);
                    return dt;
                }
                if (option.Text.Equals("单位"))
                {
                    if (controlMain.comboBox1.Text.Equals("正职后备"))
                        sql = "select * from TB_CommonInfo left join  TB_LocalUnit where TB_LocalUnit.unitName like '%" + inputContent.Text + "%' and  isDelete='" + 0 + "' and qd='1'";
                    else
                        if (controlMain.comboBox1.Text.Equals("副职后备"))
                            sql = "select * from TB_CommonInfo left join  TB_LocalUnit where TB_LocalUnit.unitName like '%" + inputContent.Text + "%' and  isDelete='" + 0 + "' and qd='0'";
                        else
                            sql = "select * from TB_CommonInfo left join  TB_LocalUnit where TB_LocalUnit.unitName like '%" + inputContent.Text + "%' and  isDelete='" + 0 + "'";
                    controlMain.isReactChange = false;
                    DataTable dt = dataOperation.GetOneDataTable_sql(sql);
                    return dt;

                }

                if (option.Text.Equals("性别"))
                {
                    if(controlMain.comboBox1.Text.Equals("正职后备"))
                        sql = "select * from TB_CommonInfo where sex like'%" + inputContent.Text + "%' and  isDelete='" + 0 + "' and qd='1'";
                    else
                        if(controlMain.comboBox1.Text.Equals("副职后备"))
                            sql = "select * from TB_CommonInfo where sex like'%" + inputContent.Text + "%' and  isDelete='" + 0 + "' and qd='0'";
                        else
                            sql = "select * from TB_CommonInfo where sex like'%" + inputContent.Text + "%' and  isDelete='" + 0 + "'";
                    controlMain.isReactChange = false;
                    DataTable dt = dataOperation.GetOneDataTable_sql(sql);
                    return dt;
                }
                if (option.Text.Equals("可进班子"))
                {
                    sql = "select * from TB_CommonInfo where joinTeam ='1' and  isDelete='" + 0 + "'";
                    DataTable dt = dataOperation.GetOneDataTable_sql(sql);
                    controlMain.isReactChange = false;
                    return dt;
                }
                if (option.Text.Equals("非可进班子"))
                {
                    sql = "select * from TB_CommonInfo where joinTeam ='0' and  isDelete='" + 0 + "'";
                    DataTable dt = dataOperation.GetOneDataTable_sql(sql);
                    controlMain.isReactChange = false;
                    return dt;
                }
            }
            else
            {
                MessageBox.Show("检索项不能为空！", "提示");
            }
            return null;

        }

        /// <summary>
        /// 当搜索选项改变时的事件监听
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void option_SelectedIndexChanged(object sender, EventArgs e)
        {     
            inputContent.Text = "";
        }

        /// <summary>
       /// 搜索按钮的事件监听
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="e"></param>
        public void Search_Click(object sender, EventArgs e)
        {
            if (controlMain != null && controlMain.nowUnit.Text.Equals(""))
            {
                MessageBox.Show("请先创建或打开数据库文件！");
                return;
            }

            if(informationGathering !=null)
            controlMain = informationGathering.ci;
            if (FrmSituationRegist != null)
            controlMain = FrmSituationRegist.ci;
            DataTable dt = button_Search_Click();
            if (informationGathering == null && FrmSituationRegist==null)
            {
                if (dt != null)
                {
                    if (controlMain != null)
                    {
                        controlMain.canClearListView = true;
                        controlMain.ShowSearch(dt);
                        controlMain.canClearListView = false;
                    }
                    if (option.Text.Equals("正职人员"))//如果搜索的是正职人员置主界面的ComboBox中的值为正职
                        controlMain.comboBox1.Text = "正职";
                    else
                    {
                        if (option.Text.Equals("副职人员"))//如果搜索的是副职人员置主界面的ComboBox中的值为副职
                            controlMain.comboBox1.Text = "副职";
                        else
                            if (option.Text.Equals("所有人员"))
                                controlMain.comboBox1.Text = "所有人员";
                    }
                }


            }
            if (informationGathering !=null)
            {

            }

            if (FrmSituationRegist !=null)
            {

            }
            controlMain.isReactChange = true;
        }
    }
}
