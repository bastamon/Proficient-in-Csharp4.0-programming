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
    /// 注册新单位窗体
    /// </summary>
    public partial class FrmRegister : Form
    {
        Unit unit = new Unit();
        DataOperation dataoperation = new DataOperation(Application.StartupPath + "\\DB\\DBHBMSU.db");
        UnitOperation unitOperation;

        /// <summary>
        /// 记录FrmUnit类型的实例
        /// </summary>
        public FrmUnit FrmUnit;

        /// <summary>
        /// 注册新单位窗体构造函数
        /// </summary>
        public FrmRegister()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 注册新单位初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RegisterForm_Load(object sender, EventArgs e)
        {
            unitOperation = new UnitOperation();
            SetUnitClass();
            comboBox_unitKind.SelectedIndex = 0;
        }

        /// <summary>
        /// 初始化单位类型
        /// </summary>
        private void SetUnitClass()
        {
            DataOperation dataOperation = new DataOperation(Application.StartupPath + "\\DB\\DBHBMSU.db");
            ReadIni readIni = new ReadIni();
            string tempversion = readIni.ReadString("tempversion");
            if (tempversion == "")
            {
                tempversion = readIni.ReadString("version");
            }
            DataTable dt = dataOperation.GetOneDataTable_sql("select  unitClass from TB_GradeUnitClass where version='" + tempversion + "'");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                comboBox_unitKind.Items.Add(dt.Rows[i]["unitClass"].ToString());
            }
        }

        /// <summary>
        /// 注册按钮监听
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_regist_Click(object sender, EventArgs e)
        {
            if (this.textBox_unitName.Text.Equals("") || this.comboBox_unitKind.Text.Equals(string.Empty))
                MessageBox.Show("所填信息不完整！", "提示");
            else
            {
                if (unitOperation.CheckUnit(textBox_unitName.Text.Trim()))
                {
                    //注册新单位的代码
                    Unit unit = new Unit();
                    unit.UnitName = this.textBox_unitName.Text.ToString().Trim();
                    unit.UnitKind = this.comboBox_unitKind.Text.Trim();
                    unit.RegistTime = this.dateTimePicker1.Text.ToString().Trim();
                    unitOperation.insterNewUnit_Globle(unit.UnitName, unit.RegistTime, unit.UnitKind);//在DBHBMSU.db文件中插入单位
                    MessageBox.Show("注册成功!", "提示");
                    //改变FrmUnit中的单位类别和单位名称，为当前注册的。
                    FrmUnit.label_showUnit.Text = textBox_unitName.Text;
                    FrmUnit.comboBox_select.SelectedIndex = comboBox_unitKind.SelectedIndex;
                    this.Close();
                    this.Dispose();
                }
                else
                {
                    MessageBox.Show("该单位名称已存在，请查证后再注册！", "提示");
                }
            }
        }

        /// <summary>
        /// 取消按钮监听
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        /// <summary>
        /// 限制时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            int temp = Convert.ToInt32(dateTimePicker1.Value.ToString("yyyyMMdd"));
            int now = Convert.ToInt32(DateTime.Now.ToString("yyyyMMdd"));
            if (temp > now)
            {
                MessageBox.Show("注册时间应小于或等于当前时间！", "提示");
                dateTimePicker1.Value = DateTime.Now;
            }
        }
    }
}