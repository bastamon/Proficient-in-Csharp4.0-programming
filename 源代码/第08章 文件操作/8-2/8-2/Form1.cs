using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace _8_2
{
    public partial class Form1 : Form
    {
        string path = "E:\\MyTest.txt";  //文件名path作为Form1类的字段

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (File.Exists(path))  //存在该文件时删除之
                File.Delete(path);
            else
            {
                FileStream fs = File.OpenWrite(path);
                StreamWriter sw = new StreamWriter(fs);
                sw.WriteLine(textBox1.Text);
                sw.Close();
                fs.Close();
                button2.Enabled = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string mystr = "";
            FileStream fs = File.OpenRead(path);
            StreamReader sr = new StreamReader(fs);
            while (sr.Peek() > -1)
                mystr = mystr + sr.ReadLine() + "\r\n";
            sr.Close();
            fs.Close();
            textBox2.Text = mystr;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            button1.Enabled = true;
            button2.Enabled = false;
        }
    }
}
