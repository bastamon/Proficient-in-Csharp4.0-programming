using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace Ex7_9
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            Point origin = new Point(this.Width / 2, this.Height / 2);           //原点位置
            int length = 100;                                           //正六边形各边的长度
            int sin_length = Convert.ToInt32(length * Math.Sin(60 * Math.PI / 180));   //Sin60长度

            Point A = new Point(origin.X - (length / 2), origin.Y - sin_length);
            Point B = new Point(origin.X + (length / 2), origin.Y - sin_length);
            Point C = new Point(origin.X + length, origin.Y);
            Point D = new Point(origin.X + (length / 2), origin.Y + sin_length);
            Point E = new Point(origin.X - (length / 2), origin.Y + sin_length);
            Point F = new Point(origin.X - length, origin.Y);
            Point[] points1 = { A, B, C, D, E, F };   //依次为顺时针正六边形六个顶点

            Pen pen1 = new Pen(Color.Red);                         //定义红色画笔
            pen1.Width = 1;                                      //指定画笔宽度为1
            g.DrawClosedCurve(pen1, points1, 0.4f, FillMode.Alternate);  //绘制闭合曲线

            length = 50;                                          //正六边形各边的长度
            sin_length = Convert.ToInt32(length * Math.Sin(60 * Math.PI / 180));   //Sin60长度

            A = new Point(origin.X - (length / 2), origin.Y - sin_length);
            B = new Point(origin.X + (length / 2), origin.Y - sin_length);
            C = new Point(origin.X + length, origin.Y);
            D = new Point(origin.X + (length / 2), origin.Y + sin_length);
            E = new Point(origin.X - (length / 2), origin.Y + sin_length);
            F = new Point(origin.X - length, origin.Y);
            Point[] points2 = { A, B, C, D, E, F };                     //依次为顺时针正六边形六个顶点

            Pen pen2 = new Pen(Color.Blue);                         //定义蓝色画笔
            pen2.Width = 2;                                       //指定画笔宽度为2
            pen2.DashStyle = DashStyle.Dash;                        //指定画笔样式为划线段
            g.DrawClosedCurve(pen2, points2, 0.2f, FillMode.Winding);   //绘制闭合曲线    
        }
    }
}
