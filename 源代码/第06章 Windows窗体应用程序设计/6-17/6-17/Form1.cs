using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace _6_17
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.RootFolder = Environment.SpecialFolder.Desktop;//设置浏览对话框的初始路径为桌面
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK) //判断是否选择了文件
            {
                richTextBox1.Text += folderBrowserDialog1.SelectedPath;//将选择的文件显示在文本框中
            }
        }
    }
}
