using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace _6_14
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Add("大图标");
            comboBox1.Items.Add("小图标");
            comboBox1.Items.Add("列表");
            comboBox1.Items.Add("详细列表");
            comboBox1.SelectedIndex = 3; 
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str = this.comboBox1.SelectedItem.ToString();
            switch (str)
            {
                case "大图标":
                    this.listView1.View = View.LargeIcon;
                    break;
                case "小图标":
                    this.listView1.View = View.SmallIcon;
                    break;
                case "列表":
                    this.listView1.View = View.List;
                    break;
                default:
                    this.listView1.View = View.Details;
                    break;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string[] subItemstr = { this.textBox1.Text, this.textBox2.Text, this.textBox3.Text };   //获取文本框中输入的信息
            ListViewItem subItem = new ListViewItem(subItemstr);           //用获取的信息，构建一个要添加的项

            int itemNumber = this.listView1.Items.Count;       //确定 添加的项的位置，插入到集合中指定的索引处，这里是最后

            this.listView1.Items.Insert(itemNumber, subItem);  //添加项

            this.listView1.Items[itemNumber].ImageIndex = 0;     //设置添加的项的 图像索引 属性
        }

        private void button2_Click(object sender, EventArgs e)
        {
            for (int i = this.listView1.SelectedItems.Count - 1; i >= 0; i--)     //用循环删除选择的多个项，从后往前删
            {
                ListViewItem item = this.listView1.SelectedItems[i];
                this.listView1.Items.Remove(item);                   //一个一个删除
            }
        }
    }
}
