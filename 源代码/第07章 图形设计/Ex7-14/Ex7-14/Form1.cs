using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Ex7_14
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics gobj = e.Graphics;                       //定义Graphics对象
            Rectangle rec1 = new Rectangle(20, 20, 200, 160);     //定义外接矩形1
            SolidBrush sbrush = new SolidBrush(Color.Red);      //定义红色画刷
            gobj.FillPie(sbrush, rec1, 0, 90);                    //填充饼形

            Rectangle rec2 = new Rectangle(40, 40, 160, 120);     //定义外接矩形2
            sbrush = new SolidBrush(Color.Blue);               //定义蓝色画刷
            gobj.FillPie(sbrush, rec2, 90, 180);                  //填充饼形

            Rectangle rec3 = new Rectangle(240, 20, 200, 200);    //定义外接矩形3
            sbrush = new SolidBrush(Color.Red);               //定义红色画刷
            gobj.FillPie(sbrush, rec3, 30, 120);                  //填充饼形

            Rectangle rec4 = new Rectangle(260, 40, 160, 160);    //定义外接矩形4
            sbrush = new SolidBrush(Color.Blue);               //定义蓝色画刷
            gobj.FillPie(sbrush, rec3, 120, 120);                 //填充饼形
        }
    }
}
