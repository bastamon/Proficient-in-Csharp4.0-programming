using System;
using System.Windows.Forms;
using System.Threading;

namespace ThreadEx2
{
    public partial class Form1 : Form
    {
        Class1 class1;
        Class1 class2;
        public Form1()
        {
            InitializeComponent();
            class1 = new Class1(this);
            class2 = new Class1(this);
        }

        private void buttonStart1_Click(object sender, EventArgs e)
        {
            class1.shouldStop = false;
            Thread t1 = new Thread(class1.ThreadMethod);
            t1.Name = "Thread1";
            t1.IsBackground = true;
            t1.Start("1");
        }

        private void buttonStart2_Click(object sender, EventArgs e)
        {
            class2.shouldStop = false;
            Thread t2 = new Thread(class2.ThreadMethod);
            t2.Name = "Thread2";
            t2.IsBackground = true;
            t2.Start("2");
        }

        private void buttonStop1_Click(object sender, EventArgs e)
        {
            class1.shouldStop = true; 
        }

        private void buttonStop2_Click(object sender, EventArgs e)
        {
            class2.shouldStop = true;
        }

        delegate void ShowMessageDelegate(string message);
        public void ShowMessage(string message)
        {
            if (richTextBox1.InvokeRequired)
            {
                ShowMessageDelegate d = ShowMessage;
                richTextBox1.Invoke(d, message);
            }
            else
            {
                richTextBox1.AppendText(message + " ");
            }
        }
    }
}
