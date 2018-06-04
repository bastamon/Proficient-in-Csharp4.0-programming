using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace Ex7_1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics gobj = e.Graphics;            //定义Graphics对象
            Pen pen = new Pen(Color.Red);         //定义红色画笔
            pen.Width = 1;                      //指定画笔宽度为1
            gobj.DrawLine(pen, 30, 30, 30, 150);    //绘制直线

            pen = new Pen(Color.Blue);            //定义蓝色画笔
            pen.Width = 2;                      //指定画笔宽度为2
            gobj.DrawLine(pen, 60, 30, 60, 150);    //绘制直线
            pen = new Pen(Color.Green);                 //定义绿色画笔
            pen.Width = 3;                            //指定画笔宽度为3
            pen.DashStyle = DashStyle.DashDotDot;       //指定画笔样式为线点点
            gobj.DrawLine(pen, 90, 30, 90, 150);          //绘制直线

            Rectangle rec1 = new Rectangle(140, 30, 90, 120);     //定义矩形1
            SolidBrush sbrush = new SolidBrush(Color.Red);      //定义红色画刷
            gobj.FillRectangle(sbrush, rec1);                   //实心画刷填充矩形

            Rectangle rec2 = new Rectangle(280, 30, 90, 120);    //定义矩形1
            LinearGradientBrush myBrush = new LinearGradientBrush(rec2, Color.Red, Color.Blue, LinearGradientMode.Horizontal);  //定义渐变画刷
            gobj.FillRectangle(myBrush, rec2);                //渐变画刷填充矩形  

        }
    }
}
