using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace _6_5
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox1.PasswordChar = '@';           //设置文本框的PasswordChar属性为字符@            
            textBox2.UseSystemPasswordChar = true; //设置文本框的UseSystemPasswordChar属性设置为True
        }
    }
}
