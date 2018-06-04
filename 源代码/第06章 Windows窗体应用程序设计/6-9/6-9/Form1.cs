using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace _6_9
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string[] courses = new string[7] { "英语", "高等数学", "数理统计", "大学物理", "电子电工", "计算机应用基础", "计算机语言程序设计" };
            for (int i = 0; i < 7; i++)
                comboBox1.Items.Add(courses[i]);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex != -1)
            {
                string c1 = (string)comboBox1.SelectedItem;
                if (!listBox1.Items.Contains(c1))
                    listBox1.Items.Add(c1);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                string c1 = (string)listBox1.SelectedItem;
                listBox1.Items.Remove(c1);
            }
        }
    }
}
