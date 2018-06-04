using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Example4_2
{
    class Father
    {
        public int x;
        protected void FuncA()
        {
            Console.WriteLine("这是类Father的方法成员FuncA()");
        }
    } 
    class Son : Father
    {
        public int y;
        public void FuncB()
        {
            FuncA();
            Console.WriteLine("这是类Son的方法成员FuncB()");
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Son s1 = new Son();
            s1.x = 1;
            s1.y = 5;     
            s1.FuncB();
            Console.WriteLine("s1.x = {0}, s1.y = {1}", s1.x, s1.y);
            Console.ReadKey();
        }
    }
}
