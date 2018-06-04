using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using HBMISR.Data;
using HBMISR.GUI.MainGUI;
using System.IO;

namespace HBMISR.GUI.OtherGUI
{
    /// <summary>
    /// 窗体类：
    /// 1、选择单位创建数据文件 
    /// 2、注册新单位
    /// </summary>
    public partial class FrmUnit : Form
    {
        public FrmLogin frmlogin;
        public bool LoginMain = false;

        private DataOperation dataOperation = new DataOperation(Application.StartupPath + "\\DB\\DBHBMSU.db");

        /// <summary>
        /// 单位类别
        /// </summary>
        string unitClass = string.Empty;

        public ControlMain ci;

        /// <summary>
        /// 记录comboBox_select控件的索引
        /// </summary>
        public int index = 0;

        /// <summary>
        /// 
        /// </summary>
        public bool CanClearUnitName = true;

        public FrmUnit()
        {
            InitializeComponent();
            this.MaximizeBox = false;
        }
        
        /// <summary>
        /// 点击创建数据文件的事件的处理，创建数据文件。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
private void button_sure_Click(object sender, EventArgs e)
{
    try
    {
        ReadIni readini = new ReadIni();
        if (!label_showUnit.Text.Equals("") && !label_showUnit.Text.Equals(string.Empty))
        {
            saveFileDialog1.Filter = ".hbs文件|*.hbs";
            string newPath="C:\\HBGB";
            if(!Directory.Exists(newPath))
            {
                Directory.CreateDirectory(newPath);
            }
            saveFileDialog1.InitialDirectory =newPath;
            saveFileDialog1.FileName = label_showUnit.Text;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.ControlBox = false;

                button_sure.Enabled = false;

                button_regist.Enabled = false;

                comboBox_select.Enabled = false;

                listView1.Enabled = false;

                string filepath = saveFileDialog1.FileName.ToString();

                Object path = (Object)filepath;//把filePath转换为object的对象
                run1(path);

                readini.WriteString("unitName", label_showUnit.Text);

                readini.WriteString("unitClass", comboBox_select.Text.ToString());

                string s = readini.ReadString("tempversion");

                if (s != "")
                {
                    //记录版本
                    readini.WriteString("version", s);
                    readini.WriteString("tempversion", "");
                }
                if (!LoginMain)
                {
                    frmlogin.IsSuccessful = true;
                }

                if (System.IO.File.Exists(readini.ReadString("filePath")))
                {
                    if (ci != null)
                    {
                        ci.nowUnit.Text = label_showUnit.Text;
                        ci.listView.Items.Clear();
                        ci.insertPanel.Visible = true;
                    }
                }
                else
                {
                    readini.WriteString("filePath", "");//清空ini文件中的路径信息
                    Application.Exit();
                }
                this.Close();
            }
        }
        else
        {
            MessageBox.Show("单位选择不能为空","提示");
        }
    }
    catch (Exception)
    {

    }
}

        /// <summary>
        /// 关闭本窗体
        /// </summary>
        public void CloseCurrentWindow()
        {
            ReadIni readIni = new ReadIni();
            if (System.IO.File.Exists(readIni.ReadString("filePath")))
            {
                if (ci != null)
                {
                    ci.nowUnit.Text = readIni.ReadString("unitName");
                    ci.listView.Items.Clear();
                    ci.insertPanel.Visible = true;
                }
            }
            else
            {
                readIni.WriteString("filePath", "");//清空ini文件中的路径信息
                Application.Exit();
            }
            this.Close();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        private void run1(Object path)//子线程运行的方法
        {
            ReadIni readIni = new ReadIni();
            UnitOperation unitOperation = new UnitOperation();
            
            //在指定位置创建DB文件
            string filepath = path.ToString();

            DataOperation dataOperation = new DataOperation(filepath);
            dataOperation.CreatDB(filepath);
            readIni.WriteString("filepath", filepath);

            //在系统创建的TB_LocalUnit表中插入单位（在上报端这个表中只有一条记录，在上报端它存储本系统所在单位和导入的单位）
            DataOperation dataOperation1 = new DataOperation(Application.StartupPath + "\\DB\\DBHBMSU.db");
            DataTable dt = dataOperation1.GetOneDataTable_sql("select * from TB_Unit where unitName='" + label_showUnit.Text + "'and unitClass='" + comboBox_select.Text.ToString() + "'");  //目的是：提取注册时间

            //获取所创建的数据文件的路径。
            unitOperation.Filepath = filepath;

            //向数据文件中插入单位信息。
            unitOperation.insterNewUnit(dt.Rows[0]["unitName"].ToString(), dt.Rows[0]["registTime"].ToString(), dt.Rows[0]["unitClass"].ToString());
            MessageBox.Show("创建成功！","提示");
        }

        /// <summary>
        /// 窗体加载事件的处理，初始化页面，列出某一单位类别的单位。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmUnit_Load(object sender, EventArgs e)
        {
            
            SetUnitClass();

            try
            {
                comboBox_select.SelectedIndex = 0;
            }
            catch
            {

            }
            unitClass = comboBox_select.Text;
        }

        /// <summary>
        /// 设置单位类别
        /// </summary>
        private void SetUnitClass()
        {
            ReadIni readIni = new ReadIni();
            string tempversion = readIni.ReadString("tempversion");

            if (tempversion.Equals(""))
            {
                tempversion = readIni.ReadString("version");
            }
            DataTable dt = dataOperation.GetOneDataTable_sql("select  unitClass from TB_GradeUnitClass where version='" + tempversion + "'");

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                comboBox_select.Items.Add(dt.Rows[i]["unitClass"].ToString());
            }
        }

        /// <summary>
        /// 单位类别发生改变时，产生的事件。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox_select_SelectedIndexChanged(object sender, EventArgs e)
        {
            showUnitName();

            Application.DoEvents();

            index = comboBox_select.SelectedIndex;

            button_sure.Enabled = false;
        }

        /// <summary>
        /// 列出某一单位类别中的所有单位。
        /// </summary>
        public void showUnitName()
        {
            label_showUnit.Text = "";
            listView1.SmallImageList = imageList1;//为listView1添加图标
            listView1.Items.Clear();//清除listView1中所有的图标
            string sql = "select unitName from TB_UNIT where unitClass='" + comboBox_select.Text + "'";
            DataTable unitName = dataOperation.GetOneDataTable_sql(sql);
            for (int j = 0; j < unitName.Rows.Count; j++)
            {
                listView1.Items.Add(unitName.Rows[j]["unitName"].ToString());
                listView1.Items[j].ImageIndex = 0;
            }
        }

        /// <summary>
        /// 对注册新单位事件的处理，注册新单位。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_rigist_Click(object sender, EventArgs e)
        {
            FrmUnitRegist formUnitRegist = new FrmUnitRegist(comboBox_select.Text);
            formUnitRegist.unitForm = this;
            formUnitRegist.ShowDialog();
            if (formUnitRegist.success)
            {
                comboBox_select.Text = formUnitRegist.unitString;
            }
        }

        /// <summary>
        /// 窗体关闭时所进行的操作，程序退出。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UnitForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(!LoginMain)
            {
                ReadIni readIni = new ReadIni();
                if (readIni.ReadString("version") == "")
                {
                    Application.Exit();
                }
                string filePath1 = readIni.ReadString("filePath");
                if (filePath1.Equals("") ||System.IO.File.Exists(filePath1) == false )
                {
                    Application.Exit();
                }
            }
        }

        /// <summary>
        /// 对鼠标右击删除事件的处理，删除选中的单位。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TStMI_Delete_Click(object sender, EventArgs e)
        {
            try
            {
                listView1.Items.RemoveAt(listView1.SelectedIndices[0]);
                DataOperation dataOperation = new DataOperation(Application.StartupPath + "\\DB\\DBHBMSU.db");
                dataOperation.DeleteListViewUnit(label_showUnit.Text.Trim(), comboBox_select.Text.Trim());
                label_showUnit.Text = "";
                MessageBox.Show("删除成功!");
            }
            catch (Exception)
            {
                MessageBox.Show("删除失败！");
            }
        }

        /// <summary>
        /// 选择不同的单位
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedIndices != null && listView1.SelectedIndices.Count > 0)
            {
                ListView.SelectedIndexCollection c = listView1.SelectedIndices;
                label_showUnit.Text = listView1.Items[c[0]].Text;
                TStMI_Delete.Enabled = true;
                TSMI_Alter.Enabled = true;
            }
            else
            {
                TStMI_Delete.Enabled = false;
                TSMI_Alter.Enabled = false;
            }
        }

        /// <summary>
        /// 修改单位名称
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TSMI_Alter_Click(object sender, EventArgs e)
        {
            FrmModifyUnit modifyUnit = new FrmModifyUnit();
            modifyUnit.strUnitClass = this.comboBox_select.Text;
            modifyUnit.strUnitName = this.label_showUnit.Text;
            modifyUnit.frmUnit = this;
            modifyUnit.index = this.comboBox_select.SelectedIndex;
            modifyUnit.ShowDialog();
        }

        /// <summary>
        /// label_showUnit信息改变的事件处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label_showUnit_TextChanged(object sender, EventArgs e)
        {
            button_sure.Enabled = true;
        }
    }
}