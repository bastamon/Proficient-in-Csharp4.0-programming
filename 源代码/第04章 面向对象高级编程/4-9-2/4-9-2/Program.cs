using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Example4_9_2
{
    public delegate void MyDelegate(string name);
    class Program
    {
        public static void ChineseGreeting(string name)
        {
            Console.WriteLine("早上好, " + name);
        }
        public static void EnglishGreeting(string name)
        {
            Console.WriteLine("Good morning, " + name);
        }
        public static void Greeting(string name, MyDelegate wt)
        {
            wt(name);
        }
        static void Main(string[] args)
        {
            MyDelegate delegate1;
            delegate1 = ChineseGreeting;
            delegate1 += EnglishGreeting;
            Greeting("张三", delegate1);
            Greeting("李四", delegate1);
            Console.ReadKey();
        }

    }
}
