using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ClassLibrary;

namespace ReferenceDll
{
    public partial class Form1 : Form
    {
        long num1, num2, num3;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "")
            {
                num1 = Convert.ToInt64(textBox1.Text.Trim());
                num2 = Convert.ToInt64(textBox2.Text.Trim());
                Class1 cl = new Class1();
                num3 = cl.Add(num1, num2);
                textBox3.Text = num3.ToString();
            }
            else
            {
                MessageBox.Show("请输入操作数一和操作数二！", "提示！");
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "")
            {
                num1 = Convert.ToInt64(textBox1.Text.Trim());
                num2 = Convert.ToInt64(textBox2.Text.Trim());
                Class1 cl = new Class1();
                num3 = cl.Multiply(num1, num2);
                textBox3.Text = num3.ToString();
            }
            else
            {
                MessageBox.Show("请输入操作数一和操作数二！", "提示！");
            }
        }
    }
}
