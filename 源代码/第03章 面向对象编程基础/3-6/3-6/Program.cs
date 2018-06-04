using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _3_6
{
    class Test
    {
        int[] a={1,2,3,4,5};
        string[] s={"张三","李四","王五"};
        public int this[int index]
        {
            get
            {
                return a[index];
            }
            set
            {
                a[index] = value;
            }
        }
        
    }
    class Program
    {
        static void Main(string[] args)
        {
            Test t = new Test( );
            Console.WriteLine(t[1]);
            Console.ReadKey( );
        }
    }
}
