using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication5
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] a = new int[5] { 2, 6, 4, 2, 1 };
            int b = Array.IndexOf(a, 2);
            Console.WriteLine("元素的首次出现位置为a： " + b.ToString());
            int c = Array.LastIndexOf(a, 2);
            Console.WriteLine("元素的首次出现位置为a： " + c.ToString());
            Console.Read();

        }
    }
}
