using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Ex6_1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.timer1.Start(); //启动计时器
            this.timer1.Interval = 3000; //设置计时器停留时间
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Hide(); //隐藏启动窗体
            Form2 frm = new Form2(); //实例化Form2对象
            frm.Show(); //显示Form2窗体
            this.timer1.Stop(); //停止计时器
        }
    }
}
