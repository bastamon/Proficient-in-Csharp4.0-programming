using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Example4_3
{
    class Father
    {
        public int x;
        public Father(int a)
        {
            x = a;
        }
    }
    class Son : Father
    {
        public int y;
        public Son(int a,int b):base(a)
        {
            y = b;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Son s1 = new Son(10,20);
            Console.WriteLine("s1.x = {0}, s1.y = {1}", s1.x, s1.y);
            Console.ReadKey();
        }
    }
}
