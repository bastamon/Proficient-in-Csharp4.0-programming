using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace Ex7_7
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics gobj = e.Graphics;      //定义Graphics对象
            Rectangle rec1 = new Rectangle(20, 20, 200, 160);    //定义外接矩形1
            Pen pen1 = new Pen(Color.Red);                  //定义红色画笔
            pen1.Width = 1;                                //指定画笔宽度为1
            gobj.DrawPie(pen1, rec1, 0, 90);                   //绘制饼形

            Rectangle rec2 = new Rectangle(40, 40, 160, 120);    //定义外接矩形2
            Pen pen2 = new Pen(Color.Blue);                  //定义蓝色画笔
            pen2.Width = 2;                                //指定画笔宽度为2
            pen2.DashStyle = DashStyle.Dash;                 //指定画笔样式为划线段
            gobj.DrawPie(pen2, rec2, 90, 180);                 //绘制饼形

            Rectangle rec3 = new Rectangle(240, 20, 200, 200);   //定义外接矩形3
            Pen pen3 = new Pen(Color.Red);                  //定义红色画笔
            pen3.Width = 3;                               //指定画笔宽度为3
            gobj.DrawPie(pen3, rec3, 30, 120);                //绘制饼形

            Rectangle rec4 = new Rectangle(260, 40, 160, 160);     //定义外接矩形4
            Pen pen4 = new Pen(Color.Blue);                    //定义蓝色画笔
            pen4.Width = 4;                                  //指定画笔宽度为4
            pen4.DashStyle = DashStyle.Dash;                   //指定画笔样式为划线段
            gobj.DrawPie(pen4, rec4, 120, 120);                  //绘制饼形
        }
    }
}
