using System;
using System.Windows.Forms;
using System.Threading;

namespace ThreadEx3
{
    public partial class Form1 : Form
    {
        public volatile bool shouldStop;
        Thread t1;
        Thread t2;
        double total;
        private Object thisLock;
        public Form1()
        {
            InitializeComponent();
            thisLock = new Object();
        }
        private void ThreadMethod()
        {
            double num = 0;
            while(!shouldStop)
            {
                num ++;
                lock (thisLock)
                {
                    total ++;
                }
                Thread.Sleep(1);
            }
            ShowMessage("线程" + Thread.CurrentThread.Name + "执行循环" + num + "次");

        }
        private delegate void ShowMessageDelegate(string message);
        private void ShowMessage(string message)
        {
            if (richTextBox1.InvokeRequired)
            {
                ShowMessageDelegate d = ShowMessage;
                richTextBox1.Invoke(d, message);
            }
            else
            {
                richTextBox1.AppendText(message + "\n\r");
            }
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            shouldStop = false;
            total = 0;
            t1 = new Thread(ThreadMethod);
            t1.Name = "1";
            t2 = new Thread(ThreadMethod);
            t2.Name = "2";
            t1.Start();
            t2.Start();
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            shouldStop = true;
            ShowMessage("两个线程共执行循环次数：" + total);
        }
    }
}
