using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace _6_8
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton3.Checked)
                MessageBox.Show("您选对了,这是微软公司开发的操作系统", "信息提示", MessageBoxButtons.OK);
            else if (radioButton1.Checked || radioButton4.Checked)
                MessageBox.Show("您选错了,这是程序设计语言", "信息提示", MessageBoxButtons.OK);
            else
                MessageBox.Show("您选错了,这是数据库管理系统", "信息提示", MessageBoxButtons.OK);
        }
    }
}
