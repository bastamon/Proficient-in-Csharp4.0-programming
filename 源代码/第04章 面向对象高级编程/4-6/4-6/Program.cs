using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Example4_6
{
    abstract class T
    {
        public abstract void Test(int x);
    }
    class S:T
    {
        public override void Test(int x)
        {
            Console.WriteLine("x= {0}",x);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            S s1 = new S();
            s1.Test(3);
            Console.ReadKey();
        }
    }
}
