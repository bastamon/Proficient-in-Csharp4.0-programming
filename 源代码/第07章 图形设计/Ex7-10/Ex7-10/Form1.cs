using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace Ex7_10
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

            Point A = new Point(30, 50);
            Point B = new Point(70, 10);
            Point C = new Point(100, 190);
            Point D = new Point(100, 50);
            Point E = new Point(150, 10);
            Point F = new Point(150, 150);
            Point G = new Point(250, 80);
            Point[] bezier_points1 = { A, B, C, D, E, F, G };  //依次为贝济埃曲线控制点坐标

            Pen pen1 = new Pen(Color.Red);               //定义红色画笔
            pen1.Width = 1;                            //指定画笔宽度为1
            g.DrawBeziers(pen1, bezier_points1);           //绘制贝济埃曲线

            A = new Point(30, 80);
            B = new Point(70, 40);
            C = new Point(100, 220);
            D = new Point(100, 80);
            E = new Point(150, 40);
            F = new Point(150, 180);
            G = new Point(250, 110);
            Point[] bezier_points2 = { A, B, C, D, E, F, G };  //依次为贝济埃曲线控制点坐标

            Pen pen2 = new Pen(Color.Blue);              //定义红色画笔
            pen2.Width = 2;                            //指定画笔宽度为2
            pen2.DashStyle = DashStyle.Dash;              //指定画笔样式为划线段
            g.DrawBeziers(pen2, bezier_points2);           //绘制贝济埃曲线
        }
    }
}
