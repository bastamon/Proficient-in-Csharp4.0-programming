using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Example4_9
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
        public static void Greeting(string name,MyDelegate wt)
        {
            wt(name);
        }
        static void Main(string[] args)
        {
            MyDelegate a = new MyDelegate(ChineseGreeting);
            MyDelegate b = new MyDelegate(EnglishGreeting);
            Greeting("张三", a);
            Greeting("李四", b);
            Console.ReadKey();
        }
    }
}
