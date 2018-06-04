using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace Ex7_2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics gobj = e.Graphics;             //定义Graphics对象
            Pen pen = new Pen(Color.Red);          //定义红色画笔
            pen.Width = 1;                       //指定画笔宽度为1
            gobj.DrawLine(pen, 30, 30, 30, 150);
            pen.Width = 2;                       //指定画笔宽度为2
            gobj.DrawLine(pen, 60, 30, 60, 150);
            pen.Width = 3;                       //指定画笔宽度为3
            gobj.DrawLine(pen, 90, 30, 90, 150);

            pen.DashStyle = DashStyle.Dash;        //指定画笔样式为划线段
            gobj.DrawLine(pen, 120, 30, 120, 150);
            pen.DashStyle = DashStyle.DashDot;     //指定画笔样式为划线点
            gobj.DrawLine(pen, 150, 30, 150, 150);
            pen.DashStyle = DashStyle.DashDotDot;  //指定画笔样式为划线点点
            gobj.DrawLine(pen, 180, 30, 180, 150);       
        }
    }
}
