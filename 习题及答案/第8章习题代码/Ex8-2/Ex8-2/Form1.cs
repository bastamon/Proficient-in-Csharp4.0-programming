using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Ex8_2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        //选择文本文件
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();//实例化打开对话框对象
            openFile.Filter = "*.txt|*.txt";//设置文件筛选器
            if (openFile.ShowDialog() == DialogResult.OK)//判断是否选择了文件
            {
                textBox1.Text = openFile.FileName;//显示选择的文件
            }
        }
        //读取文件
        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length == 0)//如果没有要打开的文件路径
                return;//退出当前操作
            using (StreamReader strRead = new StreamReader((System.IO.Stream)File.OpenRead(textBox1.Text),System.Text.Encoding.Default))//以只读方式打开字符编码为ANSI的文本文件
            {
                string pp = "";
                while (strRead.Peek() > -1)//读取文本中的内容
                {
                    pp += strRead.ReadLine();//累加读取的结果
                }
                richTextBox1.Text = pp;
            }
        }
    }
}
