using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace _8_1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int i;
            string[] filen;
            string filea;
            listBox1.Items.Clear();
            if (!Directory.Exists(textBox1.Text))
                MessageBox.Show(textBox1.Text + "文件夹不存在", "信息提示", MessageBoxButtons.OK);
            else
            {
                filen = Directory.GetFiles(textBox1.Text);
                for (i = 0; i <= filen.Length - 1; i++)
                {
                    filea = String.Format("{0}\t\t{1}\t{2}", filen[i], File.GetCreationTime(filen[i]), fileatt(filen[i]));
                    listBox1.Items.Add(filea);
                }
            }
        }

        //自定义函数
        private string fileatt(string filename) //获取文件属性
        {
            string fa = "";
            switch (File.GetAttributes(filename))
            {
                case FileAttributes.Archive:
                    fa = "存档";
                    break;
                case FileAttributes.ReadOnly:
                    fa = "只读";
                    break;
                case FileAttributes.Hidden:
                    fa = "隐藏";
                    break;
                case FileAttributes.Archive | FileAttributes.ReadOnly:
                    fa = "存档+只读";
                    break;
                case FileAttributes.Archive | FileAttributes.Hidden:
                    fa = "存档+隐藏";
                    break;
                case FileAttributes.ReadOnly | FileAttributes.Hidden:
                    fa = "只读+隐藏";
                    break;
                case FileAttributes.Archive | FileAttributes.ReadOnly | FileAttributes.Hidden:
                    fa = "存档+只读+隐藏";
                    break;
            }
            return fa;
        }
    }
}
