using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Ex6_3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            listBox1.Items.Add("微软公司");
            listBox1.Items.Add("英特尔公司");
            listBox1.Items.Add("谷歌公司");
            listBox1.Items.Add("百度公司");
            listBox1.Items.Add("联想公司");
            listBox1.Items.Add("三星公司");
            listBox1.Items.Add("华为公司");
            listBox1.Items.Add("中兴公司");
            enbutton();         //调用enbutton()方法
        }

        private void enbutton()     //自定义方法
        {
            if (listBox1.Items.Count == 0)
            {
                button1.Enabled = false;
                button2.Enabled = false;
            }
            else
            {
                button1.Enabled = true;
                button2.Enabled = true;
            }
            if (listBox2.Items.Count == 0)
            {
                button3.Enabled = false;
                button4.Enabled = false;
            }
            else
            {
                button3.Enabled = true;
                button4.Enabled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex >= 0)
            {
                listBox2.Items.Add(listBox1.SelectedItem);
                listBox1.Items.RemoveAt(listBox1.SelectedIndex);
            }
            enbutton();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            foreach (object item in listBox1.Items)
                listBox2.Items.Add(item);
            listBox1.Items.Clear();
            enbutton();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (listBox2.SelectedIndex >= 0)
            {
                listBox1.Items.Add(listBox2.SelectedItem);
                listBox2.Items.RemoveAt(listBox2.SelectedIndex);
            }
            enbutton();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            foreach (object item in listBox2.Items)
                listBox1.Items.Add(item);
            listBox2.Items.Clear();
            enbutton();
        }
    }
}
