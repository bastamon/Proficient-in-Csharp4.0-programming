using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace _6_12
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            tabControl1.Appearance = TabAppearance.Normal;//设置选项卡的外观样式
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //声明一个字符串变量，用于生成新增选项卡的名称
            string Title = "新增选项卡 " + (tabControl1.TabCount + 1).ToString();
            TabPage MyTabPage = new TabPage(Title);//实例化TabPage
            //使用TabControl控件的TabPages 属性的Add方法添加新的选项卡
            tabControl1.TabPages.Add(MyTabPage);
            MessageBox.Show("现有" + tabControl1.TabCount + "个选项卡");//获取选项卡个数
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)//判断是否选择了要移除的选项卡
            {
                MessageBox.Show("请选择要移除的选项卡");//如果没有选择，弹出提示
            }
            else
            {
                //使用TabControl控件的TabPages属性的Remove方法移除指定的选项卡
                tabControl1.TabPages.Remove(tabControl1.SelectedTab);
            }
        }
    }
}
