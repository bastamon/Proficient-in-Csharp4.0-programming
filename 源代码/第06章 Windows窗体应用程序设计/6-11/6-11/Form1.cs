using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace _6_11
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            panel1.BorderStyle = BorderStyle.FixedSingle;//设置panel1控件的边框样式
            panel1.AutoScroll = true;//设置panel1控件自动显示滚动条
        }
    }
}
