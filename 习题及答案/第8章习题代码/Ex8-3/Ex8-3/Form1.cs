using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Ex8_3
{
    public partial class Form1 : Form
    {
        string path = "E:\\MyTest.dat";  //文件名path作为Form1类的字段
        const int stnum = 4;  //学生人数常量
        struct Student
        {
            public int sno;
            public string sname;
            public double score;
        }
        Student[] st = new Student[stnum];     //学生数组

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            st[0].sno = 1001; st[0].sname = "张小三"; st[0].score = 89.2;
            st[1].sno = 1002; st[1].sname = "陈红兵"; st[1].score = 87.5;
            st[2].sno = 1003; st[2].sname = "刘  英"; st[2].score = 72.5;
            st[3].sno = 1004; st[3].sname = "张晓华"; st[3].score = 88.5;
            int i;
            if (File.Exists(path))   //存在该文件时删除之
                File.Delete(path);
            FileStream fs = File.OpenWrite(path);
            BinaryWriter sb = new BinaryWriter(fs, Encoding.Default);
            for (i = 0; i < stnum; i++)
            {
                sb.Write(st[i].sno);
                sb.Write(st[i].sname);
                sb.Write(st[i].score);
            }
            sb.Close();
            fs.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string mystr = "";
            FileStream fs = File.OpenRead(path);
            BinaryReader sb = new BinaryReader(fs, Encoding.Default);
            fs.Seek(0, SeekOrigin.Begin);
            while (sb.PeekChar() > -1)
                mystr = mystr + sb.ReadInt32() + " \t" + sb.ReadString() + "\t  " + sb.ReadDouble() + "\r\n";
            sb.Close();
            fs.Close();
            textBox1.Text = mystr;
        }
    }
}
