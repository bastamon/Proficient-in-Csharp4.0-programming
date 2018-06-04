using System;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace _8_3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string[] ss = { "Beijing", "北京", "♥♦♣♠" };
            richTextBox1.Lines = ss;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FileStream fs1 = File.Create("demo.txt");
            if (radioButton1.Checked)
            {
                BinaryWriter bw1 = new BinaryWriter(fs1);
                foreach (string s in richTextBox1.Lines)
                    bw1.Write(s);
                bw1.Close();
            }
            else
            {
                Encoding encoding = Encoding.ASCII;
                if (radioButton3.Checked)
                    encoding = Encoding.UTF7;
                if (radioButton4.Checked)
                    encoding = Encoding.UTF8;
                if (radioButton5.Checked)
                    encoding = Encoding.UTF32;
                if (radioButton6.Checked)
                    encoding = Encoding.Unicode;
                if (radioButton7.Checked)
                    encoding = Encoding.BigEndianUnicode;
                StreamWriter sw1 = new StreamWriter(fs1, encoding);
                foreach (string s in richTextBox1.Lines)
                    sw1.WriteLine(s);
                sw1.Close();
            }
            fs1.Close();
            richTextBox2.Lines = File.ReadAllLines("demo.txt");
        }
    }
}
