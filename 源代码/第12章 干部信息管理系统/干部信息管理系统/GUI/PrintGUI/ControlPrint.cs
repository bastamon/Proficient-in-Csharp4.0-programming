using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using HBMISR.Data;
using HBMISR.Service;
using HBMISR.print_class;

namespace HBMISR.GUI.PrintGUI
{
    /// <summary>
    /// 打印主界面
    /// /// </summary>
    public partial class ControlPrint : UserControl
    {
        /// <summary>
        /// //标记tabControl是否充满整个窗体,true表示充满窗体，false表示没充满窗体
        /// </summary>
        Boolean bScreen = false;

        /// <summary>
        /// //标志tabControl中是否有UserControl，初始值为false，当有时为true
        /// </summary>
        private Boolean userControlIsthere = false;

        /// <summary>
        /// //标志picturebox是否存在，true表示存在，false表示不存在
        /// </summary>
        private Boolean pictureBox1Isthere = true;

        /// <summary>
        /// //单位名称
        /// </summary>
        private string unit;

        public string Unit
        {
            get { return unit; }
            set { unit = value; }
        }
        /// <summary>
        /// //单位类别
        /// </summary>
        private string unitclass;

        public string Unitclass
        {
            get { return unitclass; }
            set { unitclass = value; }
        }

        string comboxtext_qd = "";
        /// <summary>
        /// 打印主界面的构造函数
        /// </summary>
        public ControlPrint()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 打印主界面初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PrintControl_Load(object sender, EventArgs e)
        {
            setlocation();//设置控件的位置
            GetUnitNC();
            comboBox2.SelectedIndex = 1;
        }
        /// <summary>
        /// 获取单位信息
        /// </summary>
        public void GetUnitNC()
        {
            DataOperation da = new DataOperation();
            string sql = "select unitname, unitclass from TB_LocalUnit";
            DataTable dt = da.GetOneDataTable_sql(sql);
            this.unit = dt.Rows[0]["unitname"].ToString();
            this.Unitclass = dt.Rows[0]["unitclass"].ToString();
        }

        /// <summary>
        /// UserControl的大小改变时发生
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PrintControl_SizeChanged(object sender, EventArgs e)
        {
            setlocation();//设置控件的位置
        }

        /// <summary>
        /// 设置控件的位置及大小
        /// </summary>
        private void setlocation()
        {
            this.groupBox_selectHB.Height = this.Height - 42;
            this.tabControl1.Height = this.Height - 35;
            if (bScreen)
            {
                this.tabControl1.Width = this.Width - 15;
            }
            else
            {
                this.tabControl1.Width = this.Width - 300;
            }
            //调整UserControl的大小
            try
            {
                if (userControlIsthere)//如果存在UserControl，则进行设置
                    foreach (UserControl c in this.tabPage1.Controls)
                    {
                        c.Width = this.tabPage1.Width - 2;
                        c.Height = this.tabPage1.Height - 10;
                    }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 双击tabControl1事件监听
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabControl1_DoubleClick(object sender, EventArgs e)
        {
            if (bScreen)//tabControl1铺满整个窗体时恢复原状
            {
                //设置位置
                this.tabControl1.Left = 280;
                //设置宽度
                this.tabControl1.Width = this.Width - 300;
                //调整UserControl的大小
                if (userControlIsthere)//如果存在UserControl，则进行设置
                    foreach (UserControl c in this.tabPage1.Controls)
                    {
                        c.Width = this.tabPage1.Width - 2;
                        c.Height = this.tabPage1.Height - 10;
                    }
                bScreen = false;
            }
            else //tabControl1没铺满整个窗体，使其铺满窗体
            {
                //设置位置
                this.tabControl1.Left = 5;
                //设置宽度
                this.tabControl1.Width = this.Width - 10;
                //调整UserControl的大小
                if (userControlIsthere)//如果存在UserControl，则进行设置
                    foreach (UserControl c in this.tabPage1.Controls)
                    {
                        c.Width = this.tabPage1.Width - 2;
                        c.Height = this.tabPage1.Height - 10;
                    }
                bScreen = true;
            }
        }

        /// <summary>
        /// listView1中添加数据
        /// </summary>
        /// <param name="qd"></param>
        private void showListView(string qd)
        {
            //获得数据信息
            //listView1的属性设置
            listView1.Clear();
            listView1.View = View.Details;
            listView1.LabelEdit = true;
            listView1.GridLines = true;
            listView1.AllowColumnReorder = false;
            listView1.MultiSelect = true;
            listView1.HideSelection = false;
            listView1.CheckBoxes = true;
            string sql = "select TB_CommonInfo.CID,TB_CommonInfo.name,TB_CommonInfo.position from TB_CommonInfo where TB_CommonInfo.isdelete = '0' and TB_CommonInfo.qd = '";
            if (qd.Equals("正职"))
                sql += "1' ";
            if (qd.Equals("副职"))
                sql += "0' ";           
            sql += " and ( TB_CommonInfo.state is null or TB_CommonInfo.state = '' )  and (TB_CommonInfo.promote is null or TB_CommonInfo.promote = '') ";
            sql += " order by rank, joinTeam desc";
            //读取后备干部的编号，姓名，职务  
            DataOperation dataOp = new DataOperation();
            DataTable datatable = dataOp.GetOneDataTable_sql(sql);

                for (int i = 0; i < datatable.Rows.Count; i++)
                {
                    ListViewItem item = new ListViewItem();
                    item.Tag = true;
                    item.SubItems.Add((i + 1).ToString());    //序号
                    item.SubItems.Add(datatable.Rows[i]["name"].ToString());      //姓名
                    item.SubItems.Add(datatable.Rows[i]["position"].ToString());             //职务
                    item.SubItems.Add(datatable.Rows[i]["CID"].ToString());   
                    listView1.Items.Add(item);
                }         
            //添加listview的表头信息
            listView1.Columns.Add("选择", 40, HorizontalAlignment.Center);
            listView1.Columns.Add("序号", 40, HorizontalAlignment.Center);
            listView1.Columns.Add("姓名", 70, HorizontalAlignment.Center);
            listView1.Columns.Add("职务", 115, HorizontalAlignment.Left);
        }
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.listView1.Clear();

            comboxtext_qd = this.comboBox2.SelectedItem.ToString().Substring(0, 2);

            showListView(comboxtext_qd);
        }

        /// <summary>
        /// 得到选择的后备干部的id并返回
        /// </summary>
        /// <returns></returns>
        public ArraylistClass getSelectID()
        {
            ArrayList list1 = new ArrayList();
            ArrayList list2 = new ArrayList();
            //用foreach循环得到选中的后备干部的id
            if (this.comboBox1.Text.Equals("谈话推荐情况") || this.comboBox1.Text.Equals("会议推荐情况"))
            {
                foreach (ListViewItem listviewItem in this.listView1.CheckedItems)
                {
                    if (Convert.ToBoolean(listviewItem.Tag))
                    {
                        string cid = listviewItem.SubItems[4].Text;
                        list1.Add(cid);
                    }
                    else
                    {
                        int id = Convert.ToInt32(listviewItem.SubItems[4].Text);
                        list2.Add(id);
                    }
                }
            }
            else
            {
                foreach (ListViewItem listviewItem in this.listView1.CheckedItems)
                {
                    string cid = listviewItem.SubItems[4].Text;
                    list1.Add(cid);
                }
            }
            ArraylistClass arrylist = new ArraylistClass();
            arrylist.Idlist1 = list1;
            arrylist.Idlist2 = list2;
            return arrylist;
        }

        /// <summary>
        /// 关闭groupBox_PrintViewer中所有打开报表预览容器
        /// </summary>
        private void closeAllControls()
        {
            if (pictureBox1Isthere)//如果pictureBox8存在，释放pictureBox8内存，pictureBox8Isthere设为false
            {
                pictureBox1Isthere = false;
                this.pictureBox1.Dispose();//关闭图片提示
            }

            if (userControlIsthere)
                foreach (UserControl c in this.tabPage1.Controls)
                {
                    c.Dispose();
                }
        }

        /// <summary>
        /// 全选按钮事件处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AllSelect_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in this.listView1.Items)
            {
                item.Checked = true;
            }
        }

        /// <summary>
        /// 反选按钮事件处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OppositeSelect_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in this.listView1.Items)
            {
                item.Checked = !item.Checked;
            }
        }

        /// <summary>
        /// 全不选选按钮事件处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClearSelect_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in this.listView1.Items)
            {
                item.Checked = false;
            }
        }

        /// <summary>
        /// 选择报表事件处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.comboBox1.SelectedIndex != -1)
            {
                this.listView1.Clear();
                showListView(comboxtext_qd);
            }
        }

        /// <summary>
        /// 初步人选名册打印预览
        /// </summary>
        private void HBNameList()
        {
            userControlIsthere = true;//设置UserControl的标记变量 
            ArraylistClass idList = getSelectID();
            ArrayList list1 = (ArrayList)idList.Idlist1;
            if (list1.Count == 0)
            {
                MessageBox.Show("您未选择任何后备干部！");
                return;
            }
            this.tabPage1.Text = "后备干部初步人选名册打印预览";
            closeAllControls();//关闭所有已经打开的UserControl
            C_HBNameList c = new C_HBNameList();
            c.idlist = list1;//将选择的干部编号赋值   
            c.Unit = this.Unit;
            c.Unitclass = this.Unitclass;
            c.Qd = comboxtext_qd;
            //设置c的位置及大小
            c.Left = 0;
            c.Top = 2;
            c.Width = this.tabPage1.Width - 2;
            c.Height = this.tabPage1.Height - 10;
            this.tabPage1.Controls.Add(c);//显示c
            c.Show();
        }

        /// <summary>
        /// 信息采集表打印预览
        /// </summary>
        private void HBMessage()
        {
            userControlIsthere = true;//设置UserControl的标记变量 
            ArraylistClass idList = getSelectID();
            ArrayList list1 = (ArrayList)idList.Idlist1;
            if (list1.Count == 0)
            {
                MessageBox.Show("您未选择任何后备干部!");
                return;
            }
            this.tabPage1.Text = "后备干部信息采集表打印预览";
            closeAllControls();//关闭所有已经打开的UserControl
            C_HBMessage c = new C_HBMessage();
            c.idlist = list1;//将选择的干部编号赋值
            c.Unit = this.Unit;
            c.Unitclass = this.Unitclass;
            c.Qd = comboxtext_qd;
            c.Left = 0;
            c.Top = 2;
            c.Width = this.tabPage1.Width - 2;
            c.Height = this.tabPage1.Height - 10;
            tabPage1.Controls.Add(c);//显示c
            c.Show();
        }

        /// <summary>
        /// 简要情况登记打印预览
        /// </summary>
        private void HBMainMessage()
        {
            userControlIsthere = true;//设置UserControl的标记变量 
            ArraylistClass idList = getSelectID();
            ArrayList list1 = (ArrayList)idList.Idlist1;
            if (list1.Count == 0)
            {
                MessageBox.Show("您未选择任何后备干部");
                return;
            }
            this.tabPage1.Text = "后备干部简要情况登记表打印预览";

            closeAllControls();//关闭所有已经打开的UserControl
            C_HBMainMessage c = new C_HBMainMessage();
            c.idlist = list1;//将选择的干部编号赋值
            c.Unit = this.Unit;
            c.Unitclass = this.Unitclass;
            c.Qd = comboxtext_qd;
            //设置c的位置及大小
            c.Left = 0;
            c.Top = 2;
            c.Width = this.tabPage1.Width - 2;
            c.Height = this.tabPage1.Height - 10;
            tabPage1.Controls.Add(c);//显示c
            c.Show();
        }
 
        /// <summary>
        /// 后备干部考察材料
        /// </summary>
        private void HBData()
        {
            userControlIsthere = true;//设置UserControl的标记变量 
            ArraylistClass idList = getSelectID();
            ArrayList list1 = (ArrayList)idList.Idlist1;
            if (list1.Count == 0)
            {
                MessageBox.Show("您未选择任何后备干部！","提示");
                return;
            }
            this.tabPage1.Text = "后备干部考察材料打印预览";

            closeAllControls();//关闭所有已经打开的UserControl
            C_HBData c = new C_HBData();
            c.idlist = list1;//将选择的干部编号赋值        
            //设置c的位置及大小
            c.Left = 0;
            c.Top = 2;
            c.Width = this.tabPage1.Width - 2;
            c.Height = this.tabPage1.Height - 10;
            tabPage1.Controls.Add(c);//显示c
            c.Show();
        }

        /// <summary>
        /// 导出报表按钮事件处理程序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click_1(object sender, EventArgs e)
        {
            ArraylistClass idList = getSelectID();
            ArrayList list1 = (ArrayList)idList.Idlist1;
            ArrayList list2 = (ArrayList)idList.Idlist2;
            if (list1.Count == 0)
            {
                MessageBox.Show("您未选择任何干部！","提示");
                return;
            }
            if (this.comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("请选择要导出的报表！","提示");
                return;
            }
            if (this.comboBox1.SelectedItem.ToString().Equals("初步人选名册"))
            {
                PeopleNameSheetPrint pnsp = new PeopleNameSheetPrint();
                pnsp.Unit = this.unit;
                pnsp.Qd = comboxtext_qd;
                pnsp.Unitclass = this.Unitclass;
                pnsp.exportword(list1);
            }
            else if (this.comboBox1.SelectedItem.ToString().Equals("信息采集表"))
            {
                InformationPrint ifp = new InformationPrint();
                ifp.Unit = this.Unit;
                ifp.Unitclass = this.Unitclass;
                ifp.Qd = comboxtext_qd;
                ifp.exportword(list1);
            }
            else if (this.comboBox1.SelectedItem.ToString().Equals("简要情况登记表"))
            {
                NameSheetPrint nsp = new NameSheetPrint();
                nsp.exportword(list1);
            }
            else if (this.comboBox1.SelectedItem.ToString().Equals("考察材料"))
            {
                MatrialPrint mp = new MatrialPrint();
                mp.exportword(list1);
            }            
        }

        /// <summary>
        /// 打印预览按钮单击事件处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            ArraylistClass idList = getSelectID();
            ArrayList list1 = (ArrayList)idList.Idlist1;
            if (this.comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("请选择要导出的报表！","提示");
                return;
            }
            if (list1.Count == 0)
            {
                MessageBox.Show("您未选择任何干部！","提示");
                return;
            }
            if (this.comboBox1.SelectedItem.ToString().Equals("初步人选名册"))
            {
                HBNameList();
            }
            else if (this.comboBox1.SelectedItem.ToString().Equals("信息采集表"))
            {
                HBMessage();
            }
            else if (this.comboBox1.SelectedItem.ToString().Equals("简要情况登记表"))
            {
                HBMainMessage();
            }
            else if (this.comboBox1.SelectedItem.ToString().Equals("考察材料"))
            {
                HBData();
            }
        }

    }
}
