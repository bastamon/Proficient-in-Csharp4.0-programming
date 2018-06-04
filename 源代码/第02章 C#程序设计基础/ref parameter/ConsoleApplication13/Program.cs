using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication13
{
    class Program
    {
        public static void Swap(ref int n1, ref int n2)
        {
            int temp;
            temp = n1;
            n1 = n2;
            n2 = temp;
        }
        static void Main(string[] args)
        {
            int s1 = 1, s2 = 10;
            Console.WriteLine("交换前两个整数的值分别为：" + s1 + "  " + s2);
            Swap(ref s1, ref s2);
            Console.WriteLine("交换后两个整数的值分别为：" + s1 + "  " + s2);
            Console.Read();
        }

    }
}
