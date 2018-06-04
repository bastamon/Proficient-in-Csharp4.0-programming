using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace _6_18
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            fontDialog1.AllowVectorFonts = true;//设置用户可以选择矢量字体
            fontDialog1.AllowVerticalFonts = true;//设置字体对话框既显示水平字体，也显示垂直字体
            fontDialog1.FixedPitchOnly = false;//设置用户可以选择不固定间距的字体
            fontDialog1.MaxSize = 72;//设置可选择的最大字
            fontDialog1.MinSize = 5;//设置可选择的最小字
            if (fontDialog1.ShowDialog() == DialogResult.OK)//判断是否选择了字体
            {
                if (richTextBox1.SelectedText == "")//判断是否选择了文本
                    richTextBox1.SelectAll();//全选文本
                richTextBox1.SelectionFont = fontDialog1.Font;//设置选中的文本字体
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            colorDialog1.AllowFullOpen = true;//设置允许用户自定义颜色
            colorDialog1.AnyColor = true;//设置颜色对话框中显示所有颜色
            colorDialog1.SolidColorOnly = false;//设置用户可以在颜色对话框中选择复杂颜色
            if (colorDialog1.ShowDialog() == DialogResult.OK)//判断是否选择了颜色
            {
                if (richTextBox1.SelectedText == "")//判断是否选择了文本
                    richTextBox1.SelectAll();//全选文本
                richTextBox1.SelectionColor = colorDialog1.Color;//将选定的文本颜色设置为颜色对话框中选择的颜色
            }
        }
    }
}
