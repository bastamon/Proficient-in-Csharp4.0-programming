using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Example4_10
{   
    class Test
    {
        public delegate void MyDelegate(string name); 
        public event MyDelegate MakeGreet;
        public void Greeting(string name)
        {
            MakeGreet(name);
        }
    }  
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
        
        static void Main(string[] args)
        {
            Test t = new Test();
            t.MakeGreet += ChineseGreeting;
            t.MakeGreet += EnglishGreeting;
            t.Greeting("张三");
            Console.ReadKey();
        }
    }
}
