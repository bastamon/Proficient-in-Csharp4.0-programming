using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace Ex7_15
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

            Point origin = new Point(this.Width / 2, this.Height / 2);        //原点位置
            int length = 100;                                        //正六边形各边的长度
            int sin_length = Convert.ToInt32(length * Math.Sin(60 * Math.PI / 180));  //Sin60长度

            Point A = new Point(origin.X - (length / 2), origin.Y - sin_length);
            Point B = new Point(origin.X + (length / 2), origin.Y - sin_length);
            Point C = new Point(origin.X + length, origin.Y);
            Point D = new Point(origin.X + (length / 2), origin.Y + sin_length);
            Point E = new Point(origin.X - (length / 2), origin.Y + sin_length);
            Point F = new Point(origin.X - length, origin.Y);
            Point[] points1 = { A, B, C, D, E, F };    //依次为顺时针正六边形六个顶点

            SolidBrush sbrush = new SolidBrush(Color.Red);            //定义红色画刷
            g.FillClosedCurve(sbrush, points1, FillMode.Alternate, 0.4f);  //填充闭合曲线

            length = 50;                                          //正六边形各边的长度
            sin_length = Convert.ToInt32(length * Math.Sin(60 * Math.PI / 180));  //Sin60长度

            A = new Point(origin.X - (length / 2), origin.Y - sin_length);
            B = new Point(origin.X + (length / 2), origin.Y - sin_length);
            C = new Point(origin.X + length, origin.Y);
            D = new Point(origin.X + (length / 2), origin.Y + sin_length);
            E = new Point(origin.X - (length / 2), origin.Y + sin_length);
            F = new Point(origin.X - length, origin.Y);
            Point[] points2 = { A, B, C, D, E, F };     //依次为顺时针正六边形六个顶点
            sbrush = new SolidBrush(Color.Blue);                        //定义蓝色画刷
            g.FillClosedCurve(sbrush, points2, FillMode.Alternate, 0.2f);      //填充闭合曲线
        }
    }
}
