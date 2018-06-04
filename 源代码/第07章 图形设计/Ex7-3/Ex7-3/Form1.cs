using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace Ex7_3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics gobj = e.Graphics;                    //定义Graphics对象
            Rectangle rec1 = new Rectangle(20, 20, 200, 160);   //定义矩形1
            Pen pen1 = new Pen(Color.Red);                //定义红色画笔pen1
            pen1.Width = 1;                             //指定画笔宽度为1
            gobj.DrawRectangle(pen1, rec1);                //绘制矩形

            Rectangle rec2 = new Rectangle(40, 40, 160, 120);  //定义矩形2
            Pen pen2 = new Pen(Color.Blue);                //定义蓝色画笔pen2
            pen2.Width = 2;                              //指定画笔宽度为2
            pen2.DashStyle = DashStyle.Dash;               //指定画笔样式为划线段
            gobj.DrawRectangle(pen2, rec2);                //绘制矩形
        }
    }
}
