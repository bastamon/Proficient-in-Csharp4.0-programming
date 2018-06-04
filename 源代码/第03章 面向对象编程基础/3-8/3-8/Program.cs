using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _3_8
{
    class Car
    {
    }
    class Test
    {
        
        public Test( )
        {
            Car car1 = new Car();
            
            Console.WriteLine("这是构造函数!");    
        }
        ~Test( )
        {
            
            Console.WriteLine("这是析构函数!");
        }

    }
    class Program
    {
        static void Main(string[] args)
        {
            Test t = new Test( );
            Console.ReadKey();
            GC.Collect();
        }
    }
}
