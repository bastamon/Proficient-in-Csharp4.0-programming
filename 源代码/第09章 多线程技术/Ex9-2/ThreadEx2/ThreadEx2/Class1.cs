using System;
using System.Threading;

namespace ThreadEx2
{
    class Class1
    {
        public volatile bool shouldStop;
        private Form1 m_form1;
        public Class1(Form1 form1)
        {
            m_form1 = form1;
        }

        public void ThreadMethod(Object obj)
        {
            string str = obj as string;
            m_form1.ShowMessage(Thread.CurrentThread.Name + " is running!");
            while (!shouldStop)
            {
                m_form1.ShowMessage(str);
                Thread.Sleep(100);
            }
            m_form1.ShowMessage(Thread.CurrentThread.Name + " is stopped!");
        }
    }
}
