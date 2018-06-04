using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication7
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] a = new int[4] { 6, 4, 2, 1 };
            int[] b = new int[5];
            Array.Copy(a, b, a.Length);
            Console.WriteLine("复制后的数组：");
            Console.Write(b[0].ToString() + "  " + b[1].ToString() + "  " + b[2].ToString() + "  " + b[3].ToString());
            Console.Read();

        }
    }
}
