using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Ex8_1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();//清空列表项
            SerachFile(textBox2.Text);//搜索文件
        }
        //搜索文件
        public void SerachFile(string fileDirectory)
        {
            DirectoryInfo dir = new DirectoryInfo(fileDirectory);//实例化DirectoryInfo对象
            FileSystemInfo[] f = dir.GetFileSystemInfos();//获取指定路径下的所有文件及子文件夹
            foreach (FileSystemInfo i in f)//遍历获取到的文件及子文件夹
            {
                if (i is DirectoryInfo)//判断是否是文件夹
                {
                    SerachFile(i.FullName);//调用方法自身继续在其中查找
                }
                else
                {
                    if (i.Name == textBox1.Text)//如果找到文件
                    {
                        FileInfo fin = new FileInfo(i.FullName);//实例化FileInfo对象
                        listView1.Items.Add(fin.Name);//显示文件名字
                        listView1.Items[listView1.Items.Count-1].SubItems.Add( fin.FullName);//显示文件全路径
                        listView1.Items[listView1.Items.Count - 1].SubItems.Add(fin.Length.ToString());//显示文件大小
                        listView1.Items[listView1.Items.Count - 1].SubItems.Add(fin.CreationTime.ToString());//显示文件创建时间
                    }
                }
            }
        }
    }
}