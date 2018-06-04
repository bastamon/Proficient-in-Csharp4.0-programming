using System;
using System.Threading;
using System.Windows.Forms;

namespace ThreadEx1
{
    public partial class Form1 : Form
    {
        private volatile bool threadStopped;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Thread t = new Thread(MyThread); //创建线程实例
            t.IsBackground = true;           //设置赋值线程为后台线程
            t.Name = "My Thread";            //辅助线程名称
            threadStopped = false;
            t.Start();                       //启动赋值线程
            Thread.Sleep(1000);               //主线程休眠
            MessageBox.Show("主线程输出！");
            threadStopped = true;            //终止辅助线
        }

        //MyThread方法由一个辅助线程执行
        private void MyThread()
        {
            MessageBox.Show("辅助线程开始！");
            while (!threadStopped)
            {
                //这里添加辅助线程要执行的任务 
                Thread.Sleep(10);
            }
            MessageBox.Show("辅助线程结束！");
            //MyThread方法返回后，辅助线程将终止
        }
    }
}
