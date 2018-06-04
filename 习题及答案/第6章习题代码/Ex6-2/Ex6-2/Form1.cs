using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Ex6_2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //为年份下拉列表赋值
            for (int i = 1900; i <= 2050; i++)
            {
                comboBox1.Items.Add(i);
            }
            comboBox1.Text = DateTime.Now.Year.ToString();//设置年份下拉列表的默认值为当前年份
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int intYear = Convert.ToInt32(comboBox1.Text);//定义一个变量，用来记录当前选择的年份
            string strText = "";//记录所选年份对应的属相
            switch (intYear % 12)//使用所选年份求余12作为条件判断
            {
                case 4:
                    strText = "鼠";
                    break;
                case 5:
                    strText = "牛";
                    break;
                case 6:
                    strText = "虎";
                    break;
                case 7:
                    strText = "兔";
                    break;
                case 8:
                    strText = "龙";
                    break;
                case 9:
                    strText = "蛇";
                    break;
                case 10:
                    strText = "马";
                    break;
                case 11:
                    strText = "羊";
                    break;
                case 0:
                    strText = "猴";
                    break;
                case 1:
                    strText = "鸡";
                    break;
                case 2:
                    strText = "狗";
                    break;
                case 3:
                    strText = "猪";
                    break;
            }
            foreach (RadioButton rbtn in groupBox1.Controls)//循环遍历groupBox1容器中的单选按钮控件
            {
                if (rbtn.Text == strText)//根据选择年份获取属相信息
                {
                    rbtn.Checked = true;//将表示属相的单选按钮选中
                }
            }
        }
    }
}
