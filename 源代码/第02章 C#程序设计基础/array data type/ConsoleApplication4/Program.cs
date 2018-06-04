using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication4
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] a = new int[4] { 6, 4, 2, 1 };
            Console.WriteLine("排序前的数组：");
            Console.Write(a[0].ToString() + "  " + a[1].ToString() + "  " + a[2].ToString() + "  " + a[3].ToString());
            Array.Sort(a);
            Console.WriteLine();
            Console.WriteLine("排序后的数组：");
            Console.Write(a[0].ToString() + "  " + a[1].ToString() + "  " + a[2].ToString() + "  " + a[3].ToString());
            Console.Read();


        }
    }
}
