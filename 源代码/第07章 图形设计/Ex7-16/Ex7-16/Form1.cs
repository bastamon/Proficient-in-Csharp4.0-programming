using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Ex7_16
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics gobj = this.CreateGraphics();

            Font hf = new Font("黑体", 20, FontStyle.Bold);         //创建字体
            Font sf = new Font("宋体", 17, FontStyle.Italic);
            Font kf = new Font("楷体", 14, FontStyle.Underline);

            SolidBrush rbrush = new SolidBrush(Color.Red);         //创建画刷
            SolidBrush gbrush = new SolidBrush(Color.Green);
            SolidBrush bbrush = new SolidBrush(Color.Blue);

            gobj.DrawString("深入浅出：C#程序设计从入门到实战", hf, rbrush, 20, 20);   //绘制文本
            gobj.DrawString("深入浅出：C#程序设计从入门到实战", sf, gbrush, 20, 50);
            gobj.DrawString("深入浅出：C#程序设计从入门到实战", kf, bbrush, 20, 80);
        }
    }
}
