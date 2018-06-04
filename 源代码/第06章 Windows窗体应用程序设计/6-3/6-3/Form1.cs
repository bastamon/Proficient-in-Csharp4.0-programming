using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace _6_3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            label1.Font = new Font("楷体", 12);//设置label1控件的字体
            label1.Text = "《C#从入门到实战》";//设置label1控件显示的文字
            label2.ForeColor = Color.Red;//设置label2控件的字体颜色
            label2.Text = "希望能给大家打造良好的C#编程基础！";//设置label2控件显示的文字
        }
    }
}
