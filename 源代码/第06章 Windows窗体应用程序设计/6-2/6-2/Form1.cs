using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace _6_2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            button1.BackgroundImage = Properties.Resources.j_1;//设置button1控件的背景
            button1.BackgroundImageLayout = ImageLayout.Stretch;//设置button1控件的背景布局方式
            button2.Image = Properties.Resources.j_1;//设置button2控件显示图像
            button2.ImageAlign = ContentAlignment.MiddleLeft;//设置图像对齐方式
            button2.Text = "解锁";//设置button2控件的文本
            button3.FlatStyle = FlatStyle.Flat;//设置button3控件的外观样式
            button3.Text = "确定";//设置button3控件的文本
            button4.TextAlign = ContentAlignment.MiddleRight;//设置文本对齐方式
            button4.Text = "确定";//设置button4控件的文本
        }
    }
}
