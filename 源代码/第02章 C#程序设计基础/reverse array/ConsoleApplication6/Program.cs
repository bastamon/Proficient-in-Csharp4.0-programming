using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication6
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] a = new int[4] { 6, 4, 2, 1 };
            Console.WriteLine("逆序前的数组：");
            Console.Write(a[0].ToString() + "  " + a[1].ToString() + "  " + a[2].ToString() + "  " + a[3].ToString());
            Array.Reverse(a);
            Console.WriteLine();
            Console.WriteLine("逆序后的数组：");
            Console.Write(a[0].ToString() + "  " + a[1].ToString() + "  " + a[2].ToString() + "  " + a[3].ToString());
            Console.Read();

        }
    }
}
