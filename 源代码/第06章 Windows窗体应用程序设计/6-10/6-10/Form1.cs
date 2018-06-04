using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace _6_10
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            checkedListBox1.Items.Add("中国");
            checkedListBox1.Items.Add("美国");
            checkedListBox1.Items.Add("俄罗斯");
            checkedListBox1.Items.Add("英国");
            checkedListBox1.Items.Add("法国");
            checkedListBox1.CheckOnClick = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            foreach (object item in checkedListBox1.CheckedItems)
                listBox1.Items.Add(item);
        }
    }
}
