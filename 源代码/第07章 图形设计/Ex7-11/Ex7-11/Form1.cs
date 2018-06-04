using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Ex7_11
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics gobj = e.Graphics;                      //定义Graphics对象
            Rectangle rec1 = new Rectangle(20, 20, 200, 160);      //定义矩形1
            SolidBrush sbrush = new SolidBrush(Color.Red);     //定义红色画刷
            gobj.FillRectangle(sbrush, rec1);                   //填充矩形

            Rectangle rec2 = new Rectangle(40, 40, 160, 120);    //定义矩形2
            sbrush = new SolidBrush(Color.Blue);              //定义蓝色画刷
            gobj.FillRectangle(sbrush, rec2);                  //填充矩形
        }
    }
}
