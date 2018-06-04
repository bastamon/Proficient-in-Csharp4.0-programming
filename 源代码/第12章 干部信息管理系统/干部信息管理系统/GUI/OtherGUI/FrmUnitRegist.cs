using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HBMISR.Data;
using HBMISR.Service;

namespace HBMISR.GUI.OtherGUI
{
    /// <summary>
    /// 窗体类，功能：注册单位
    /// </summary>
    public partial class FrmUnitRegist : Form
    {
        /// <summary>
        /// 记录单位
        /// </summary>
        public string unitString=string.Empty;

        /// <summary>
        /// 标记
        /// </summary>
        public Boolean success = false;

        /// <summary>
        /// 属性类对象，记录单位的类别、单位名称、注册时间
        /// </summary>
        Unit unit = new Unit();

        /// <summary>
        /// 操作根目录文件
        /// </summary>
        DataOperation dataoperation = new DataOperation(Application.StartupPath + "\\DB\\DBHBMSU.db");

        UnitOperation unitOperation ;

        /// <summary>
        /// 选择单位的窗体类对象
        /// </summary>
        public FrmUnit unitForm;

        string UnitClass = "";

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="unitclass">单位类别</param>
        public FrmUnitRegist(string unitclass)
        {
            InitializeComponent();
            UnitClass = unitclass;
        }

        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RegisterForm_Load(object sender, EventArgs e)
        {
            unitOperation = new UnitOperation();
            SetUnitClass();
            if (UnitClass != "")
            {
                comboBox_unitKind.Text = UnitClass;
            }
            else
            {
                comboBox_unitKind.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// 根据版本号设置单位的类别
        /// </summary>
        private void SetUnitClass()
        {
            DataOperation dataOperation = new DataOperation(Application.StartupPath + "\\DB\\DBHBMSU.db");
            ReadIni readIni = new ReadIni();
            string tempversion = readIni.ReadString("version");
            if (tempversion.Equals(""))
            {
                tempversion = readIni.ReadString("tempversion");
            }
            else
            {
            
            }

            DataTable dt = dataOperation.GetOneDataTable_sql("select  unitClass from TB_GradeUnitClass where version='" + tempversion + "'");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                comboBox_unitKind.Items.Add(dt.Rows[i]["unitClass"].ToString());
            }
        }

        /// <summary>
        /// 对点击注册按钮事件的处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_regist_Click(object sender, EventArgs e)
        {
            //检测单位名称是否存在
            if (unitOperation.CheckUnit(textBox_unitName.Text.Trim()))
            {
                if (this.textBox_unitName.Text.Trim().Equals(""))
                {
                        MessageBox.Show("单位名称不能为空！","提示");
                }
                else
                {
                    Unit unit = new Unit();
                    unit.UnitName = this.textBox_unitName.Text.ToString().Trim();
                    unit.UnitKind = this.comboBox_unitKind.Text.Trim();
                    unit.RegistTime = this.dateTimePicker1.Text.ToString().Trim();

                    //在DBHBMSU.db文件中插入单位
                    unitOperation.insterNewUnit_Globle(unit.UnitName, unit.RegistTime, unit.UnitKind);
                    MessageBox.Show("注册成功!","提示");

                    if (unitForm != null)
                    {
                        unitForm.comboBox_select.Text = this.comboBox_unitKind.Text.Trim();
                        unitForm.showUnitName();
                    }

                    this.Close();
                    this.Dispose();
                }
            }
            else
            {
                MessageBox.Show("该单位已存在！");
            }

        }

        /// <summary>
        /// 取消按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 对时间进行限制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            int temp = Convert.ToInt32(dateTimePicker1.Value.ToString("yyyyMMdd"));
            int now = Convert.ToInt32(DateTime.Now.ToString("yyyyMMdd"));
            if (temp > now)
            {
                MessageBox.Show("注册时间应小于或等于结束时间！", "提示");
                dateTimePicker1.Value = DateTime.Now;
            }
        }
    }
}