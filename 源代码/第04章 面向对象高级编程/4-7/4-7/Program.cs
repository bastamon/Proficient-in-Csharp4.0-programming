using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Example4_7
{
    class T
    {
        public virtual void Test()
        {
            Console.WriteLine("这是类T的密封方法Test()");
        }
    }
    class S : T
    {
        public sealed override void Test()
        {
            Console.WriteLine("这是类S的方法Test()");
        }
    }
    class W : S
    {
        
    }
    class Program
    {
        static void Main(string[] args)
        {
            W w1 = new W();
            w1.Test( );
            Console.ReadKey();
        }
    }
}
